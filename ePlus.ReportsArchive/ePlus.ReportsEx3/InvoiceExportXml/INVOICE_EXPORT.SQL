SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO
--------------------------------------------------------------------------
IF OBJECT_ID('DBO.FN_CERTIFICATE') IS NULL BEGIN
    EXEC('CREATE FUNCTION DBO.FN_CERTIFICATE() RETURNS INT BEGIN RETURN NULL END')
    GRANT EXEC ON [DBO].[FN_CERTIFICATE] TO [PUBLIC]
END
GO
ALTER FUNCTION DBO.FN_CERTIFICATE(@ID_SERIES BIGINT)
RETURNS NVARCHAR(4000) BEGIN
    DECLARE @RESULT NVARCHAR(4000) SET @RESULT = ''

    SELECT @RESULT = @RESULT +
        ISNULL(C.CERT_NUMBER + ' ', '') + ISNULL(C.ISSUED_BY, ',') + ','
    FROM CERTIFICATE C 
    WHERE C.ID_SERIES = @ID_SERIES

    SET @RESULT = LEFT(RTRIM(@RESULT), 500)    
    
    SET @RESULT = CASE WHEN @RESULT IS NOT NULL AND RIGHT(@RESULT, 1) = ',' 
        THEN LEFT(@RESULT, LEN(@RESULT)-1) ELSE @RESULT END

    RETURN @RESULT
END
GO
--------------------------------------------------------------------------
IF OBJECT_ID('DBO.FN_BAR_CODE') IS NULL BEGIN
    EXEC('CREATE FUNCTION DBO.FN_BAR_CODE() RETURNS INT BEGIN RETURN NULL END')
    GRANT EXEC ON [DBO].[FN_BAR_CODE] TO [PUBLIC]
END
GO
ALTER FUNCTION DBO.FN_BAR_CODE(@ID_GOODS BIGINT)
RETURNS NVARCHAR(4000) BEGIN
    DECLARE @RESULT NVARCHAR(4000) SET @RESULT = ''

    SELECT @RESULT = @RESULT + ISNULL(NULLIF(B.CODE, '') + ', ', '')
    FROM BAR_CODE B WHERE B.ID_GOODS = @ID_GOODS

    SET @RESULT = LEFT(RTRIM(@RESULT), 500)    
    
    SET @RESULT = CASE WHEN @RESULT IS NOT NULL AND RIGHT(@RESULT, 1) = ',' 
        THEN LEFT(@RESULT, LEN(@RESULT)-1) ELSE @RESULT END

    RETURN @RESULT
END
GO
IF OBJECT_ID('REPEX_INVOICE_EXPORT') IS NULL EXEC('CREATE PROCEDURE REPEX_INVOICE_EXPORT AS RETURN')
GO
ALTER PROCEDURE REPEX_INVOICE_EXPORT
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT TOP 1 SYSTEM_ID = L.ID_DOCUMENT,
    INVOICE_NUM = MAX(LOT_NAME),
    INVOICE_DATE = CONVERT(VARCHAR, GETDATE(), 104),
    DEP_ID = CAST(NULL AS VARCHAR(10)),
    ORDER_ID = CAST(NULL AS VARCHAR(10))
FROM LOT L
WHERE QUANTITY_REM > 0 AND (@ID_GLOBAL IS NULL OR L.ID_DOCUMENT = @ID_GLOBAL)
GROUP BY L.ID_DOCUMENT

SELECT SYSTEM_ID = L.ID_DOCUMENT,
    CODE = G.CODE,
	[NAME] = G.NAME,
	VENDOR = P.NAME,
	QTTY = CAST(L.QUANTITY_REM AS BIGINT),
    SPRICE = CAST(L.PRICE_SUP AS NUMERIC(12, 2)), --���� ���������� ��� ���
    RPRICE = CAST(L.PRICE_SAL AS NUMERIC(12, 2)), --��������� � ���
    DIVISIOR = SR.DENOMINATOR,
	[COUNTRY] = C.NAME,
    NDS = CAST(L.VAT_SUP AS INT), --������ ���
    SNDSUM = CAST(ROUND((L.VAT_SUP * L.PRICE_SUP) / 100, 2)  AS NUMERIC(12, 2)),
    VALID_DATE = CONVERT(VARCHAR, S.BEST_BEFORE, 104),
    SERT_NUM = DBO.FN_CERTIFICATE(S.ID_SERIES),
    VENDORBARCODE = DBO.FN_BAR_CODE(G.ID_GOODS)
FROM LOT L(NOLOCK)
	INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN SCALING_RATIO SR(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
	INNER JOIN UNIT U(NOLOCK) ON U.ID_UNIT = SR.ID_UNIT
	LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT JOIN COUNTRY C(NOLOCK) ON C.ID_COUNTRY = P.ID_COUNTRY
    LEFT JOIN SERIES S(NOLOCK) ON S.ID_SERIES = L.ID_SERIES
WHERE QUANTITY_REM > 0 AND (@ID_GLOBAL IS NULL OR L.ID_DOCUMENT = @ID_GLOBAL)
    AND SR.DENOMINATOR = 1 AND SR.NUMERATOR = 1
ORDER BY G.NAME

RETURN 0
GO
--exec REPEX_INVOICE_EXPORT @XMLPARAM = N'<XML></XML>'