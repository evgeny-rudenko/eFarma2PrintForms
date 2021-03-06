SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
--------------------------------------------------------------------------
GO
IF OBJECT_ID('DBO.REP_MOVEGOODS_ES_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REP_MOVEGOODS_ES_EX AS RETURN')
    GRANT EXEC ON DBO.REP_MOVEGOODS_ES_EX TO PUBLIC
END
GO
ALTER PROCEDURE DBO.REP_MOVEGOODS_ES_EX
    @XMLPARAM NTEXT
AS
DECLARE @ALL_STORE BIT, @ALL_GOODS BIT, @ALL_PRODUCER BIT, 
        @ALL_SUPPLIER BIT, @ALL_KIND BIT, @ONLY_WITH_MOV BIT, @ONLY_INVOICE BIT,
        @SQL NVARCHAR(4000),
        @SQL_INNER NVARCHAR(4000),
        @SQL_WHERE NVARCHAR(4000),
        @SQL_WHERE_ID_TABLE NVARCHAR(4000)
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME

DECLARE @USE_LOT_DATE BIT
DECLARE @LOT_DATE_FROM DATETIME, @LOT_DATE_TO DATETIME
DECLARE @SORT_LOT_DATE_ORDER BIT -- 0-ASC, 1-DESC, 

DECLARE @HDOC INT

SET QUOTED_IDENTIFIER ON 

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1
        @USE_LOT_DATE = USE_LOT_DATE,
        @LOT_DATE_FROM = LOT_DATE_FROM,
        @LOT_DATE_TO = LOT_DATE_TO,
        @SORT_LOT_DATE_ORDER = SORT_LOT_DATE_ORDER,

        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @ONLY_WITH_MOV = MOV,           -- ������ ������ � ��������� � ������� (�� ���������� ����� ��� ��������)
        @ONLY_INVOICE = ONLY_INVOICE    -- ������ ������ � �������� � �������
	FROM OPENXML(@HDOC, '/XML')  WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        MOV BIT 'MOV',
        ONLY_INVOICE BIT 'ONLY_INVOICE',
        USE_LOT_DATE BIT 'USE_LOT_DATE',
        LOT_DATE_FROM DATETIME 'LOT_DATE_FROM',
        LOT_DATE_TO DATETIME 'LOT_DATE_TO',
        SORT_LOT_DATE_ORDER BIT 'SORT_LOT_DATE_ORDER'
    )
    -- ��� �������:
    -- �� ������� LOT
    -- ������ (LOT.ID_GOODS)
	SELECT * INTO #GOODS2 FROM OPENXML(@HDOC, '//KOD_ES') WITH(KOD_ES BIGINT '.') WHERE KOD_ES != 0
	IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0
    -- ���������� (LOT.ID_SUPPLIER)
	SELECT * INTO #SUPPLIER FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR != 0
	IF @@ROWCOUNT = 0 SET @ALL_SUPPLIER = 1 ELSE SET @ALL_SUPPLIER = 0
    -- ������ (LOT.ID_STORE)
	SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.') WHERE ID_STORE != 0
	IF @@ROWCOUNT = 0 SET @ALL_STORE = 1 ELSE SET @ALL_STORE = 0
    
    -- �� ������� GOODS
    -- ���� ������� (GOODS.ID_GOODS_KIND)
	SELECT * INTO #GOODS_KIND FROM OPENXML(@HDOC, '//ID_GOODS_KIND') WITH(ID_GOODS_KIND BIGINT '.') WHERE ID_GOODS_KIND != 0
	IF @@ROWCOUNT = 0 SET @ALL_KIND = 1 ELSE SET @ALL_KIND = 0
    -- ������������� (GOODS.ID_PRODUCER)
	SELECT * INTO #PRODUCER2 FROM OPENXML(@HDOC, '//KOD_PRODUCER') WITH(KOD_PRODUCER BIGINT '.') WHERE KOD_PRODUCER != 0
	IF @@ROWCOUNT = 0 SET @ALL_PRODUCER = 1 ELSE SET @ALL_PRODUCER = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC DBO.USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
--EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT
SET @ONLY_WITH_MOV = ISNULL(@ONLY_WITH_MOV, 0)
SET @ONLY_INVOICE = ISNULL(@ONLY_INVOICE, 0)

-------------------------------------------
DECLARE @S DATETIME
SET @S = GETDATE()
-------------------------------------------
IF OBJECT_ID('TEMPDB..#BEG_REM') IS NOT NULL DROP TABLE #BEG_REM
CREATE TABLE #BEG_REM (
    ID_LOT_GLOBAL UNIQUEIDENTIFIER PRIMARY KEY,
    BEGIN_COUNT DECIMAL(20,2),
    BEGIN_SUM DECIMAL(20,4),
    BEGIN_RETAIL_SUM DECIMAL(20,4)
)
IF OBJECT_ID('TEMPDB..#MOV_IN_PERION') IS NOT NULL DROP TABLE #MOV_IN_PERION
CREATE TABLE #MOV_IN_PERION (
    ID_LOT_GLOBAL UNIQUEIDENTIFIER,
    QUANTITY_ADD MONEY,
    QUANTITY_SUB MONEY,
    QUANTITY_RES MONEY,
    SUM_SUP MONEY,   
    DISCOUNT_ACC MONEY,
    SUM_ACC  MONEY
)  
IF OBJECT_ID('TEMPDB..#MOV_IN_PERIOD_EXT') IS NOT NULL DROP TABLE #MOV_IN_PERIOD_EXT
CREATE TABLE #MOV_IN_PERIOD_EXT (
    ID_LOT_GLOBAL UNIQUEIDENTIFIER PRIMARY KEY,
    INVOICE_COUNT DECIMAL(20,2),
    INVOCE_SUM DECIMAL(20,4),
    INVOCE_RETAIL_SUM DECIMAL(20,4),
    EXPENSE_COUNT DECIMAL(20,2),
    EXPENSE_SUM DECIMAL(20,4),
    EXPENSE_RETAIL_SUM DECIMAL(20,4)
    )
IF OBJECT_ID('TEMPDB..#MOV_AND_REM') IS NOT NULL DROP TABLE #MOV_AND_REM
CREATE TABLE #MOV_AND_REM (
    LOT_NAME VARCHAR(40),
    ID_GOODS BIGINT,
    BEGIN_COUNT DECIMAL(20,2),
    BEGIN_SUM DECIMAL(20,4),
    BEGIN_RETAIL_SUM DECIMAL(20,4),
    INVOICE_COUNT DECIMAL(20,2),
    INVOCE_SUM DECIMAL(20,4),
    INVOCE_RETAIL_SUM DECIMAL(20,4),
    EXPENSE_COUNT DECIMAL(20,2),
    EXPENSE_SUM DECIMAL(20,4),
    EXPENSE_RETAIL_SUM DECIMAL(20,4),
    SERIES VARCHAR(100),
    LOT_DATE DATETIME
    )
-- ��������������� ������������� ������� �� ����� ����������� ��� �������
DECLARE @DATE_BEG_REM DATETIME,
        @TIMING_MODE BIT

SET @TIMING_MODE = 0            -- ����� ������ �������� ���������� ��������

-- SET @DATE_BEG_REM = @DATE_FR - 1        -- ������� �� ����� ����������� ���
-- EXEC USP_LPR_CORRECT_DAY @DATE_BEG_REM

SET @SQL_INNER = ''
SET @SQL_WHERE = ''
SET @SQL_WHERE_ID_TABLE = ''
-- ������ �������� �� �������, �����������, �������, "������ ������"

-- �� ���� �������� ����� ���������� � �������� LOT:
IF @ALL_STORE = 0 OR @ALL_SUPPLIER = 0 OR @ALL_GOODS = 0 -- ������, ����������, ������
    SET @SQL_INNER = @SQL_INNER + '
    INNER JOIN LOT L(NOLOCK) ON T.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL'
-- ������������ ������ WHERE �� LOT
IF @ALL_STORE = 0 
    SET @SQL_WHERE = @SQL_WHERE + '
    AND (L.ID_STORE IN (SELECT ID_STORE FROM #STORE))'
IF @ALL_SUPPLIER = 0
    SET @SQL_WHERE = @SQL_WHERE + '
    AND (L.ID_SUPPLIER IN (SELECT ID_CONTRACTOR FROM #SUPPLIER))'
IF @ALL_GOODS = 0
    SET @SQL_WHERE = @SQL_WHERE + '
    AND (L.ID_GOODS IN (
        SELECT G.ID_GOODS FROM GOODS G
        INNER JOIN (SELECT G.ID_GOODS_GLOBAL
            FROM #GOODS2 G2
            INNER JOIN ES_EF2 F2 ON F2.KOD_ES = G2.KOD_ES
            INNER JOIN ES_ES_2_GOODS G ON G.C_ES = F2.GUID_ES) TAB ON TAB.ID_GOODS_GLOBAL = G.ID_GOODS_GLOBAL))'
-- �� ���� �������� ����� ���������� � ��������� LOT � GOODS:
IF @ALL_KIND = 0 OR @ALL_PRODUCER = 0 -- ���� �������, �������������
BEGIN
    IF LTRIM(RTRIM(@SQL_INNER)) = ''  
    SET @SQL_INNER = '
    INNER JOIN LOT L(NOLOCK) ON T.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL'

    SET @SQL_INNER = @SQL_INNER + '
    INNER JOIN GOODS G(NOLOCK) ON L.ID_GOODS = G.ID_GOODS'
END
-- ������������ ������ WHERE �� GOODS
IF @ALL_KIND = 0 
    SET @SQL_WHERE = @SQL_WHERE + '
    AND (G.ID_GOODS_KIND IN (SELECT ID_GOODS_KIND FROM #GOODS_KIND))'
IF @ALL_PRODUCER = 0 
    SET @SQL_WHERE = @SQL_WHERE + '
    AND (G.ID_PRODUCER IN (SELECT G.ID_PRODUCER 
        FROM #PRODUCER2 P2
        INNER JOIN ES_EF2 F2 ON F2.PRODUCER_COD = P2.KOD_PRODUCER
        INNER JOIN ES_ES_2_GOODS G2 ON G2.C_ES = F2.GUID_ES
        INNER JOIN GOODS G ON G.ID_GOODS_GLOBAL = G2.ID_GOODS_GLOBAL
        ))'

-- �������� �������
SET @SQL = '
INSERT #BEG_REM (
    ID_LOT_GLOBAL,
    BEGIN_COUNT,
    BEGIN_SUM,
    BEGIN_RETAIL_SUM
)
SELECT
    T.ID_LOT_GLOBAL,
    T.QUANTITY_REM * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR,
    T.SUM_SUP,
    T.SUM_RET
FROM (SELECT 
	  LM.ID_LOT_GLOBAL,
	  QUANTITY_REM = SUM(LM.QUANTITY_ADD- LM.QUANTITY_SUB),
 	  SUM_SUP = SUM(CASE WHEN LM.QUANTITY_ADD >  0 THEN LM.SUM_SUP
                             WHEN LM.QUANTITY_SUB <> 0 THEN -1 * LM.SUM_SUP * SIGN(LM.QUANTITY_SUB) 
                             ELSE 0 
                        END),
 	  SUM_RET = SUM(CASE WHEN LM.QUANTITY_ADD >  0 THEN LM.SUM_ACC
                             WHEN LM.QUANTITY_SUB <> 0 THEN -1 * (LM.SUM_ACC + LM.DISCOUNT_ACC) * SIGN(LM.QUANTITY_SUB) 
                             ELSE 0 
                        END)
       FROM LOT_MOVEMENT LM
       WHERE LM.DATE_OP <= @DATE_FR
       AND LM.QUANTITY_RES = 0
	GROUP BY LM.ID_LOT_GLOBAL) T
INNER JOIN LOT ON LOT.ID_LOT_GLOBAL = T.ID_LOT_GLOBAL
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = LOT.ID_SCALING_RATIO'+ @SQL_INNER + @SQL_WHERE

EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME', @DATE_FR = @DATE_FR

IF @TIMING_MODE = 1 PRINT '��������� �������� �� ������ ' + CAST(DATEDIFF(MS, @S, GETDATE()) AS VARCHAR(20))
SET @S = GETDATE()
--=================================================================
-- �������� �������� � �������
-- ������ "������ ������"
IF @ONLY_INVOICE = 1 
    SET @SQL_WHERE_ID_TABLE = '
    AND (T.ID_TABLE = 2)'

SET @SQL = '
INSERT #MOV_IN_PERION (
    ID_LOT_GLOBAL,
    QUANTITY_ADD,
    QUANTITY_SUB,
    QUANTITY_RES,
    SUM_SUP,   
    DISCOUNT_ACC,
    SUM_ACC 
)  
SELECT
    T.ID_LOT_GLOBAL,
    T.QUANTITY_ADD,
    T.QUANTITY_SUB,
    T.QUANTITY_RES,
    T.SUM_SUP,   
    T.DISCOUNT_ACC,
    T.SUM_ACC      
FROM LOT_MOVEMENT T (NOLOCK) ' + @SQL_INNER + '
WHERE T.DATE_OP >=@DATE_FR AND T.DATE_OP <=@DATE_TO 
AND T.QUANTITY_RES = 0'
+ @SQL_WHERE + @SQL_WHERE_ID_TABLE

EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME, @DATE_TO DATETIME', @DATE_FR = @DATE_FR, @DATE_TO = @DATE_TO

CREATE INDEX IX_#MOV_IN_PERION$ID_LOT_GLOBAL ON #MOV_IN_PERION(ID_LOT_GLOBAL) 

--select * from #MOV_IN_PERION    -- �������

--
IF @TIMING_MODE = 1 
    PRINT '��������� �������� ��� 1 (�� LOT_MOVEMENT � #MOV_IN_PERION)  ' + CAST(DATEDIFF(MS, @S, GETDATE()) AS VARCHAR(20))
SET @S = GETDATE()
--
-- ������ ���������� ����������, �.�. � ���� ������� ���������� � LOT ��� ���� ����������
SET @SQL_INNER = ''
-- �� ���� �������� ����� ���������� � �������� GOODS:
IF @ALL_KIND = 0 OR @ALL_PRODUCER = 0 -- ���� �������, �������������
BEGIN
    SET @SQL_INNER = @SQL_INNER + '
    INNER JOIN GOODS G(NOLOCK) ON L.ID_GOODS = G.ID_GOODS'
END

SET @SQL = '
INSERT INTO #MOV_IN_PERIOD_EXT(
    ID_LOT_GLOBAL,
    INVOICE_COUNT,
    INVOCE_SUM,
    INVOCE_RETAIL_SUM,
    EXPENSE_COUNT,
    EXPENSE_SUM,
    EXPENSE_RETAIL_SUM
)
SELECT
    L.ID_LOT_GLOBAL,
    INVOICE_COUNT = SUM(T.QUANTITY_ADD * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)), 

    INVOCE_SUM = SUM(CASE WHEN T.QUANTITY_ADD > 0 THEN T.SUM_SUP ELSE 0 END),
    INVOCE_RETAIL_SUM = SUM(CASE WHEN T.QUANTITY_ADD > 0 THEN T.SUM_ACC ELSE 0 END),

    EXPENSE_COUNT = SUM(T.QUANTITY_SUB * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),

    EXPENSE_SUM = SUM(CASE WHEN T.QUANTITY_SUB <> 0 THEN T.SUM_SUP * SIGN(T.QUANTITY_SUB) ELSE 0 END),
    EXPENSE_RETAIL_SUM = SUM(CASE WHEN T.QUANTITY_SUB <> 0 THEN (T.SUM_ACC+T.DISCOUNT_ACC) * SIGN(T.QUANTITY_SUB) ELSE 0 END)
FROM #MOV_IN_PERION T
    INNER JOIN LOT L(NOLOCK) ON L.ID_LOT_GLOBAL = T.ID_LOT_GLOBAL
    INNER JOIN SCALING_RATIO SR(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO ' + @SQL_INNER +
'
WHERE 1 = 1 ' + @SQL_WHERE + 'GROUP BY L.ID_LOT_GLOBAL'

EXEC SP_EXECUTESQL @SQL

--select * from #MOV_IN_PERIOD_EXT    -- �������

--
IF @TIMING_MODE = 1 
    PRINT '��������� �������� ��� 2 (������� � #MOV_IN_PERIOD_EXT)  ' + CAST(DATEDIFF(MS, @S, GETDATE()) AS VARCHAR(20))
SET @S = GETDATE()

--=================================================================
SET @SQL_WHERE = ''
-- ������ "�� ���������� ����� ��� ��������"
IF @ONLY_WITH_MOV = 1 
    SET @SQL_WHERE = @SQL_WHERE + 'AND (ISNULL(T.INVOICE_COUNT, 0) <> 0 OR ISNULL(T.EXPENSE_COUNT, 0) <> 0)'
--
CREATE TABLE #LOT_BY_LOT_DATE(
    ID_LOT_GLOBAL UNIQUEIDENTIFIER
)

IF (@USE_LOT_DATE=1) BEGIN
    EXEC USP_RANGE_DAYS @LOT_DATE_FROM OUT, @LOT_DATE_TO OUT
    EXEC USP_RANGE_NORM @LOT_DATE_FROM OUT, @LOT_DATE_TO OUT
    INSERT INTO #LOT_BY_LOT_DATE
    SELECT
        L.ID_LOT_GLOBAL
    FROM LOT L
    INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL AND LM.ID_DOCUMENT=L.ID_DOCUMENT AND LM.ID_DOCUMENT_ITEM = L.ID_DOCUMENT_ITEM
    WHERE LM.DATE_OP BETWEEN CONVERT(DATETIME, FLOOR(CONVERT(MONEY, @LOT_DATE_FROM))) AND CONVERT(DATETIME, CEILING(CONVERT(MONEY, @LOT_DATE_TO)))

    SET @SQL_WHERE = @SQL_WHERE + '
    AND (EXISTS (SELECT NULL FROM #LOT_BY_LOT_DATE LLL WHERE LLL.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL))'
END

SET @SQL = '
INSERT INTO #MOV_AND_REM(
    LOT_NAME,
    ID_GOODS, 
    BEGIN_COUNT,
    BEGIN_SUM,
    BEGIN_RETAIL_SUM, 
    INVOICE_COUNT,
    INVOCE_SUM,
    INVOCE_RETAIL_SUM,
    EXPENSE_COUNT,
    EXPENSE_SUM,
    EXPENSE_RETAIL_SUM,
    SERIES,
    LOT_DATE
)
SELECT 
    L.LOT_NAME,
    L.ID_GOODS, 
    R.BEGIN_COUNT,
    R.BEGIN_SUM,
    R.BEGIN_RETAIL_SUM, 
    T.INVOICE_COUNT,
    T.INVOCE_SUM,
    T.INVOCE_RETAIL_SUM,
    T.EXPENSE_COUNT,
    T.EXPENSE_SUM,
    T.EXPENSE_RETAIL_SUM,
    SERIES = S.SERIES_NUMBER+'' ''+CONVERT(VARCHAR, S.BEST_BEFORE,104),
    LM.DATE_OP
FROM LOT L
INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL AND LM.ID_DOCUMENT=L.ID_DOCUMENT AND LM.ID_DOCUMENT_ITEM = L.ID_DOCUMENT_ITEM
    LEFT JOIN #MOV_IN_PERIOD_EXT T ON L.ID_LOT_GLOBAL = T.ID_LOT_GLOBAL
    LEFT JOIN #BEG_REM R ON L.ID_LOT_GLOBAL = R.ID_LOT_GLOBAL
    LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
WHERE (T.ID_LOT_GLOBAL IS NOT NULL OR R.ID_LOT_GLOBAL IS NOT NULL)
' + @SQL_WHERE

EXEC SP_EXECUTESQL @SQL

CREATE INDEX IX_#MOV_AND_REM$ID_GOODS ON #MOV_AND_REM(ID_GOODS) 
IF @TIMING_MODE = 1
    PRINT '��������� �������� � �������� (������� � #MOV_AND_REM) ' + CAST(DATEDIFF(MS, @S, GETDATE()) AS VARCHAR(20))
SET @S = GETDATE()
-- �������������� ������� 
-- �������
-- IF OBJECT_ID('_V2') IS NOT NULL DROP TABLE _V2
SET @SQL = '
SELECT 
    G2.ID_GOODS_GLOBAL, 
    GOODS_NAME = F2.[NAME] + '' ('' + PROD.PRODUCER_NAME + '')'',
    STORE_NAME = T.LOT_NAME + ISNULL('', ''+T.SERIES,''''),   
    T.LOT_NAME,    
    BEGIN_COUNT = SUM(T.BEGIN_COUNT),
    BEGIN_SUM = SUM(T.BEGIN_SUM),
    BEGIN_RETAIL_SUM = SUM(T.BEGIN_RETAIL_SUM), 
	INVOICE_COUNT = SUM(T.INVOICE_COUNT),
    INVOCE_SUM = SUM(T.INVOCE_SUM),
    INVOCE_RETAIL_SUM = SUM(T.INVOCE_RETAIL_SUM),
    EXPENSE_COUNT = SUM(T.EXPENSE_COUNT),
    EXPENSE_SUM = SUM(T.EXPENSE_SUM),
    EXPENSE_RETAIL_SUM = SUM(T.EXPENSE_RETAIL_SUM),
    END_COUNT = SUM(ISNULL(T.BEGIN_COUNT, 0) + ISNULL(T.INVOICE_COUNT, 0) - ISNULL(T.EXPENSE_COUNT, 0)),
    END_SUM = SUM(ISNULL(T.BEGIN_SUM, 0) + ISNULL(T.INVOCE_SUM, 0) - ISNULL(T.EXPENSE_SUM, 0)),
    END_RETAIL_SUM = SUM(ISNULL(T.BEGIN_RETAIL_SUM, 0) + ISNULL(T.INVOCE_RETAIL_SUM, 0) - ISNULL(T.EXPENSE_RETAIL_SUM, 0)),
    LOT_DATE = T.LOT_DATE
FROM #MOV_AND_REM T
 	INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = T.ID_GOODS
    INNER JOIN ES_ES_2_GOODS G2(NOLOCK) ON G2.ID_GOODS_GLOBAL = G.ID_GOODS_GLOBAL
    INNER JOIN ES_EF2 F2(NOLOCK) ON F2.GUID_ES = G2.C_ES
    LEFT JOIN ES_PRODUCER PROD(NOLOCK) ON PROD.KOD_PRODUCER = F2.PRODUCER_COD
GROUP BY G2.ID_GOODS_GLOBAL, F2.[NAME], PROD.PRODUCER_NAME, T.LOT_NAME, T.SERIES, T.LOT_DATE'

    DECLARE @SORT_ORDER VARCHAR(4)
    SET @SORT_ORDER = CASE WHEN @SORT_LOT_DATE_ORDER=0 THEN 'ASC' ELSE 'DESC' END

    SET @SQL = @SQL + 
    CASE 
    WHEN @SORT_LOT_DATE_ORDER IS NULL THEN ' 
    ORDER BY GOODS_NAME'
    ELSE ' 
    ORDER BY LOT_DATE ' + @SORT_ORDER
    END

 EXEC SP_EXECUTESQL @SQL

print @SQL
--
IF @TIMING_MODE = 1 PRINT '�������� ������� ' + CAST(DATEDIFF(MS, @S, GETDATE()) AS VARCHAR(20))
--
SELECT TOP 1 COMPANY = ISNULL(NAME, '')
FROM CONTRACTOR(NOLOCK)
WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()

RETURN 0
GO
--------------------------------------------------------------------------
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec REP_MOVEGOODS_ES_EX @xmlParam = N'<XML><DATE_FR>2008-10-01T15:33:21.109</DATE_FR><DATE_TO>2008-10-20T15:33:21.109</DATE_TO><MOV>1</MOV><ONLY_INVOICE>0</ONLY_INVOICE><KOD_PRODUCER>973</KOD_PRODUCER></XML>'
