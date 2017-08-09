SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO
IF OBJECT_ID('DBO.REPEX_INVENTORY_SESSION_INV19_RIGLA') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVENTORY_SESSION_INV19_RIGLA AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVENTORY_SESSION_INV19_RIGLA
	@XMLPARAM NTEXT
AS
	
SET LANGUAGE 'us_english'

-- IS_SAL = 0 - ����������� ��������� ��������� (�� �����. ����. ����)
-- IS_SAL = 1 - ��������� ����
-- IS_SAL = 2 - ������� ����
DECLARE @IS_SAL SMALLINT	
DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

DECLARE @CONST_VAT VARCHAR(256), @CONST VARCHAR(256)
IF @IS_SAL = 0 BEGIN
    SELECT TOP 1 @CONST_VAT = VALUE FROM SYS_OPTION(NOLOCK) WHERE CODE = 'INVENTORY_CALC_VAT'
	SELECT TOP 1 @CONST = VALUE FROM SYS_OPTION(NOLOCK) WHERE CODE = 'INVENTORY_CALC_PRICE'
	SET @CONST = CASE ISNULL(@CONST, '') WHEN '' THEN 'SAL' ELSE @CONST END
END

DECLARE @OKEI_CODE VARCHAR(40), @NAME VARCHAR(100)
SELECT TOP 1 @OKEI_CODE = OKEI_CODE, @NAME = NAME FROM UNIT(NOLOCK) WHERE MNEMOCODE = 'BOX'
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @ID_GLOBAL = ID_GLOBAL,
        @IS_SAL = IS_SAL
    FROM OPENXML(@HDOC , '/XML') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        IS_SAL SMALLINT 'IS_SAL')
    SELECT ID_STORE = ID_STORE
    INTO #STORE FROM OPENXML(@HDOC , '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.')
EXEC SP_XML_REMOVEDOCUMENT @HDOC
DECLARE @ALL_STORE BIT
SET @ALL_STORE = CASE WHEN EXISTS(SELECT TOP 1 1 FROM #STORE) THEN 0 ELSE 1 END

DECLARE @SVED TABLE (ID_INVENTORY_GLOBAL UNIQUEIDENTIFIER NULL, IS_SAL SMALLINT NULL, DOC_DATE DATETIME NULL, USE_VAT BIT NULL, [FULL] BIT NULL)
INSERT @SVED(ID_INVENTORY_GLOBAL, IS_SAL, DOC_DATE, USE_VAT, [FULL])
SELECT
    ID_INVENTORY_GLOBAL = I.ID_INVENTORY_GLOBAL,
    IS_SAL = @IS_SAL,
    DOC_DATE = I.DOC_DATE,
    USE_VAT = C.USE_VAT,
    [FULL] = I.[FULL]
FROM INVENTORY_SVED I(NOLOCK) 
    INNER JOIN STORE S(NOLOCK) ON S.ID_STORE = I.ID_STORE
    INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_SESSION_GLOBAL = @ID_GLOBAL

SELECT 
	CONTRACTOR = CASE WHEN ISNULL(C.FULL_NAME, '') = '' THEN C.NAME ELSE C.FULL_NAME END,
	STORE = S.NAME,
	DOC_NUM = I.DOC_NUM,
	DOC_DATE = CONVERT(VARCHAR(10), I.DOC_DATE, 104)
FROM INVENTORY_SVED I(NOLOCK)
	INNER JOIN @SVED SVED ON SVED.ID_INVENTORY_GLOBAL = I.ID_INVENTORY_GLOBAL
	INNER JOIN STORE S(NOLOCK) ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE @ALL_STORE = 1 OR EXISTS(SELECT TOP 1 1 FROM #STORE WHERE #STORE.ID_STORE = S.ID_STORE)

SELECT 
	L.ID_GOODS,
	QUANTITY = SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
	SUM_PRICE = ABS(CASE 
        WHEN (@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1 THEN ISNULL(SUM(SUM_ACC), 0)
        ELSE ISNULL(SUM(SUM_SUP), 0) END),
	SUM_SVAT =  ABS(CASE
	    WHEN (@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1 THEN ISNULL(SUM(SUM_ACC - SVAT_ACC), 0)
        ELSE ISNULL(SUM(SUM_SUP-SVAT_SUP), 0) END),
    [TYPE] = CASE WHEN (LM.QUANTITY_ADD - LM.QUANTITY_SUB) > 0 THEN 'IZL' ELSE 'NEDOS' END,
    [SIGN] = CASE WHEN (LM.QUANTITY_ADD - LM.QUANTITY_SUB) > 0 THEN 1 ELSE -1 END,
    ID_STORE
INTO #TMP FROM LOT_MOVEMENT LM(NOLOCK)
    INNER JOIN @SVED SVED ON SVED.ID_INVENTORY_GLOBAL = LM.ID_DOCUMENT
    INNER JOIN LOT L(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
    INNER JOIN SCALING_RATIO SR(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
WHERE @ALL_STORE = 1 OR EXISTS(SELECT TOP 1 1 FROM #STORE WHERE #STORE.ID_STORE = L.ID_STORE) 
GROUP BY L.ID_GOODS, L.ID_STORE,
	CASE WHEN (LM.QUANTITY_ADD - LM.QUANTITY_SUB) > 0 THEN 'IZL' ELSE 'NEDOS' END,
	CASE WHEN SVED.USE_VAT = 1 THEN CASE WHEN @CONST_VAT = 'SUP' THEN L.VAT_SUP ELSE L.VAT_SAL END ELSE 0 END,
	CASE WHEN (LM.QUANTITY_ADD - LM.QUANTITY_SUB) > 0 THEN 1 ELSE -1 END

SELECT
	GOODS_NAME = G.NAME,
	GOODS_CODE = G.CODE,
	OKEI_CODE = @OKEI_CODE,
	UNIT_NAME = '1/1' + ' ' + @NAME,
	Q_ADD = (CASE WHEN (Q_ADD + Q_SUB)>0 THEN (Q_ADD + Q_SUB) ELSE 0 END),
	SUMPRICE_ADD = CASE WHEN SUMPRICE_ADD - SUMPRICE_SUB>0 THEN SUMPRICE_ADD - SUMPRICE_SUB ELSE 0 END,
	SUMVAT_ADD = CASE WHEN SUMVAT_ADD - SUMVAT_SUB>0 THEN SUMVAT_ADD - SUMVAT_SUB ELSE 0 END,
	Q_SUB = (CASE WHEN (Q_ADD + Q_SUB)<0 THEN ABS(Q_ADD + Q_SUB) ELSE 0 END),
	SUMPRICE_SUB = CASE WHEN SUMPRICE_ADD - SUMPRICE_SUB<0 THEN ABS(SUMPRICE_ADD - SUMPRICE_SUB) ELSE 0 END,
	SUMVAT_SUB	= CASE WHEN SUMVAT_ADD - SUMVAT_SUB<0 THEN  ABS(SUMVAT_ADD - SUMVAT_SUB) ELSE 0 END,
	Q_REV = (Q_ADD + Q_SUB),
	SUMPRICE_REV = SUMPRICE_ADD - SUMPRICE_SUB,
	SUMVAT_REV = SUMVAT_ADD - SUMVAT_SUB
INTO #RES FROM GOODS G(NOLOCK) INNER JOIN (
	SELECT
		A.ID_GOODS ,
		Q_ADD = (SELECT ISNULL(SUM(B.QUANTITY),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'IZL'),
		SUMPRICE_ADD = (SELECT ISNULL(SUM(B.SUM_PRICE),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'IZL'),
		SUMVAT_ADD = (SELECT ISNULL(SUM(B.SUM_SVAT),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'IZL'),
		Q_SUB = (SELECT ISNULL(SUM(B.QUANTITY),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'NEDOS'),
		SUMPRICE_SUB = (SELECT ISNULL(SUM(B.SUM_PRICE),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'NEDOS'),
		SUMVAT_SUB = (SELECT ISNULL(SUM(B.SUM_SVAT),0) FROM #TMP B WHERE B.ID_GOODS = A.ID_GOODS AND B.[TYPE] = 'NEDOS')
	FROM #TMP A GROUP BY A.ID_GOODS) T ON T.ID_GOODS = G.ID_GOODS
	
SELECT * FROM #RES
WHERE Q_REV <> 0 OR SUMPRICE_REV <> 0 OR SUMVAT_REV <> 0
ORDER BY GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR (nolock)
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO
--EXEC REPEX_INVENTORY_SESSION_INV19_RIGLA @xmlParam=N'<XML><ID_GLOBAL>11111111-1111-1111-1111-111111111111</ID_GLOBAL><IS_SAL>1</IS_SAL><ID_STORE>139</ID_STORE><ID_STORE>134</ID_STORE><ID_STORE>135</ID_STORE><ID_STORE>136</ID_STORE></XML>'
--GO


