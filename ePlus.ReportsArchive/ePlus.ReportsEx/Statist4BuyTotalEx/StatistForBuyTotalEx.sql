SET NOCOUNT ON
--USE EPLUS_DEV10
-------------------------------------------------------------------------------------------
IF OBJECT_ID('DBO.REP_STATIST_FORBUY_TOTAL_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REP_STATIST_FORBUY_TOTAL_EX AS RETURN')
    GRANT EXECUTE ON DBO.REP_STATIST_FORBUY_TOTAL_EX TO PUBLIC
END
GO
ALTER PROCEDURE DBO.REP_STATIST_FORBUY_TOTAL_EX
    @XMLPARAM NTEXT
AS
        
DECLARE @HDOC INT, @TYPE_DOC INT, @ALL_STORE BIT, @IS_ES BIT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @ORDER NVARCHAR(256)
DECLARE @ORDER_BY BIT
DECLARE @SQL NVARCHAR(4000)
DECLARE @ROW_COUNT BIGINT, @DAY_COUNT BIGINT, @DOC_DAYS BIGINT, @MAX_DAYS BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM OUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @DOC_DAYS = DOC_DAYS,
        @MAX_DAYS = MAX_DAYS,
        @ROW_COUNT = ROW_COUNT,
        @TYPE_DOC = TYPE_DOC,
        @ORDER_BY = ORDER_BY,
        @IS_ES = IS_ES
    FROM OPENXML(@HDOC , '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        DOC_DAYS INT 'DOC_DAYS',
        MAX_DAYS INT 'MAX_DAYS',
        ROW_COUNT BIGINT 'ROW_COUNT',
        TYPE_DOC INT 'TYPE_DOC',
--        ORDER_BY NVARCHAR(256) 'ORDER_BY',
		ORDER_BY BIT 'ORDER_BY',
        IS_ES BIT 'IS_ES')        

    SELECT ID_STORE 
	INTO #STORE 
	FROM OPENXML(@HDOC , '/XML/STORE') WITH(ID_STORE BIGINT 'ID_STORE') 
	WHERE ID_STORE != 0

    IF @@ROWCOUNT = 0 SET @ALL_STORE = 1 ELSE SET @ALL_STORE = 0
EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
SET @DAY_COUNT = DATEDIFF(DAY , @DATE_FR , @DATE_TO) + 1

SELECT ID_TABLE_DATA INTO #TABLE_DATA FROM TABLE_DATA(NOLOCK) WHERE
    (@TYPE_DOC = 0 AND ID_TABLE_DATA IN (7, 12, 21, 19)) OR
    (@TYPE_DOC = 1 AND ID_TABLE_DATA IN (12, 21)) OR
    (@TYPE_DOC = 2 AND ID_TABLE_DATA IN (7, 12, 19))

SELECT
    G_ID = G.ID_GOODS,
    G_MODELCODE = MAX(G.MNEMOCODE), 
    G_RUSNAME = CASE
        WHEN @IS_ES = 1 AND LEN(MAX(ES.ES_NAME)) > 0 THEN MAX(ES.ES_NAME)
        ELSE MAX(G.NAME) END,
    II_CONTRACTORPRICE = ISNULL((
        SELECT TOP 1 CAST(L1.PRICE_SUP AS DECIMAL(18, 2)) FROM LOT L1(NOLOCK)
        INNER JOIN (SELECT ID_LOT = MAX(ID_LOT) FROM LOT(NOLOCK) GROUP BY ID_GOODS) TAB ON TAB.ID_LOT = L1.ID_LOT
        INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
        WHERE LM1.ID_TABLE = 2 AND L1.ID_GOODS = G.ID_GOODS), (
        SELECT TOP 1 CAST(L1.PRICE_SUP AS DECIMAL(18, 2)) FROM LOT L1(NOLOCK)
        INNER JOIN (SELECT ID_LOT = MAX(ID_LOT) FROM LOT(NOLOCK) GROUP BY ID_GOODS) TAB ON TAB.ID_LOT = L1.ID_LOT
        INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
        WHERE L1.ID_GOODS = G.ID_GOODS)),
    II_RETAILPRICE = ISNULL((
        SELECT TOP 1 CAST(L1.PRICE_SAL AS DECIMAL(18, 2)) FROM LOT L1(NOLOCK)
        INNER JOIN (SELECT ID_LOT = MAX(ID_LOT) FROM LOT(NOLOCK) GROUP BY ID_GOODS) TAB ON TAB.ID_LOT = L1.ID_LOT
        INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
        WHERE LM1.ID_TABLE = 2 AND L1.ID_GOODS = G.ID_GOODS), (
        SELECT TOP 1 CAST(L1.PRICE_SAL AS DECIMAL(18, 2)) FROM LOT L1(NOLOCK)
        INNER JOIN (SELECT ID_LOT = MAX(ID_LOT) FROM LOT(NOLOCK) GROUP BY ID_GOODS) TAB ON TAB.ID_LOT = L1.ID_LOT
        INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
        WHERE L1.ID_GOODS = G.ID_GOODS)),
    OG_SUMQTY =
        ISNULL(SUM(CASE WHEN LM.QUANTITY_SUB < 0 THEN 0.00 ELSE CAST(LM.QUANTITY_SUB AS DECIMAL(18, 2))* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) END), 0) 
        - (
        ISNULL(SUM(CASE WHEN LM.QUANTITY_SUB > 0 THEN 0.00 ELSE -1 * CAST(LM.QUANTITY_SUB AS DECIMAL(18, 2))* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) END), 0) 
        +
        ISNULL(SUM(CASE WHEN LM.ID_TABLE = 12 THEN CAST(LM.QUANTITY_ADD AS DECIMAL(18, 2))* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) ELSE 0.00 END), 0)),
    OG_AVGQTY = CAST(NULL AS DECIMAL(18 , 2)) ,
    LM_STOCKQTY_FROM = (
        SELECT 	
			SUM(ISNULL(CAST(LM1.QUANTITY_ADD AS DECIMAL(18 , 2)) , 0) - ISNULL(CAST(LM1.QUANTITY_SUB AS DECIMAL(18 , 2)) , 0)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))
        FROM LOT L1(NOLOCK) 
		INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON L1.ID_LOT_GLOBAL = LM1.ID_LOT_GLOBAL
		INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L1.ID_SCALING_RATIO
        WHERE L1.ID_GOODS = G.ID_GOODS AND LM1.DATE_OP <= @DATE_FR),
    LM_STOCKQTY_TO = (
        SELECT 
			SUM(ISNULL(CAST(LM1.QUANTITY_ADD AS DECIMAL(18 , 2)) , 0) - ISNULL(CAST(LM1.QUANTITY_SUB AS DECIMAL(18 , 2)) , 0)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))
        FROM LOT L1(NOLOCK) 
		INNER JOIN LOT_MOVEMENT LM1(NOLOCK) ON L1.ID_LOT_GLOBAL = LM1.ID_LOT_GLOBAL
		INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L1.ID_SCALING_RATIO
        WHERE L1.ID_GOODS = G.ID_GOODS AND LM1.DATE_OP <= @DATE_TO),
    G_FACTDAYS = CAST(NULL AS DECIMAL(18 , 2)), 
    G_DIFFDAYS = CAST(NULL AS DECIMAL(18 , 2)),
    G_DIFFQTY = CAST(NULL AS DECIMAL(18 , 2))
INTO #ALL FROM LOT_MOVEMENT LM(NOLOCK)
    INNER JOIN LOT L(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
    INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
    LEFT JOIN (
        SELECT ES_NAME = ES.NAME, ID_GOODS_GLOBAL = E2G.ID_GOODS_GLOBAL
        FROM ES_EF2 ES(NOLOCK) INNER JOIN ES_ES_2_GOODS E2G(NOLOCK) ON E2G.C_ES = ES.GUID_ES
        INNER JOIN (SELECT ID_ES_ES_2_GOODS = MAX(ID_ES_ES_2_GOODS) FROM ES_ES_2_GOODS(NOLOCK)
        GROUP BY ID_GOODS_GLOBAL) TAB ON TAB.ID_ES_ES_2_GOODS = E2G.ID_ES_ES_2_GOODS) ES ON ES.ID_GOODS_GLOBAL = G.ID_GOODS_GLOBAL
WHERE (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
    AND LM.ID_TABLE IN (SELECT ID_TABLE_DATA FROM #TABLE_DATA)
    AND LM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
GROUP BY G.ID_GOODS
        
UPDATE #ALL SET
    OG_SUMQTY = CASE WHEN OG_SUMQTY IS NULL OR OG_SUMQTY < 0 THEN 0 ELSE OG_SUMQTY END ,
    LM_STOCKQTY_FROM = CASE WHEN LM_STOCKQTY_FROM IS NULL OR LM_STOCKQTY_FROM < 0 THEN 0 ELSE LM_STOCKQTY_FROM END,
    LM_STOCKQTY_TO = CASE WHEN LM_STOCKQTY_TO IS NULL OR LM_STOCKQTY_TO < 0 THEN 0 ELSE LM_STOCKQTY_TO END
UPDATE #ALL SET OG_AVGQTY = OG_SUMQTY / @DAY_COUNT
UPDATE #ALL SET G_FACTDAYS = ROUND(LM_STOCKQTY_TO - (OG_AVGQTY*(@DOC_DAYS + @MAX_DAYS)), 0)

DELETE FROM #ALL WHERE G_FACTDAYS > 0
UPDATE #ALL SET G_FACTDAYS = ABS(G_FACTDAYS)
-- UPDATE #ALL SET G_FACTDAYS = CASE
--     WHEN OG_AVGQTY IS NULL OR OG_AVGQTY = 0 THEN 0
--     ELSE ROUND(LM_STOCKQTY_TO / OG_AVGQTY , 0) END
UPDATE #ALL SET G_DIFFDAYS = ABS(@MAX_DAYS - G_FACTDAYS)
UPDATE #ALL SET G_DIFFQTY = ROUND(G_DIFFDAYS * OG_AVGQTY , 2)
        
--DECLARE @ORDER VARCHAR(128)

-- 	SET @SQL = 'SELECT ' + CASE WHEN @ROW_COUNT > 0 THEN 'TOP ' + CAST(@ROW_COUNT AS VARCHAR) ELSE '' END + ' 
-- 	* 
-- 	FROM #All 
-- 	WHERE G_DIFFQTY > 0 AND G_FACTDAYS < ' + CAST(@DOC_DAYS AS VARCHAR) + ' ORDER BY '+ CASE WHEN ISNULL(@ORDER_BY,0) IS NULL THEN 'G_RUSNAME ' END--, LM_STOCKQTY_TO

SET @ORDER = 'LM_STOCKQTY_TO ASC, G_RUSNAME ASC'
IF(@ORDER_BY = 0) SET @ORDER = 'G_RUSNAME ASC'

SET @SQL = 'SELECT ' + CASE WHEN @ROW_COUNT > 0 THEN 'TOP ' + CAST(@ROW_COUNT AS VARCHAR) ELSE '' END + ' 
* 
FROM #All ' + 
CASE WHEN @ORDER IS NOT NULL AND @ORDER != '' THEN ' ORDER BY ' + @ORDER ELSE '' END
        
EXECUTE(@SQL)
        
RETURN 0
GO



--exec rep_statist_forbuy @xmlParam = N'<XML><DATE_FR>2008-08-01T00:00:00.000</DATE_FR><DATE_TO>2008-08-31T00:00:00.000</DATE_TO><DOC_DAYS>55</DOC_DAYS><MAX_DAYS>55</MAX_DAYS><ORDER_BY>G_RUSNAME</ORDER_BY><ROW_COUNT>0</ROW_COUNT><TYPE_DOC>0</TYPE_DOC><IS_ES>0</IS_ES></XML>'
--exec REP_STATIST_FORBUY_TOTAL_EX @xmlParam = N'<XML><DATE_FR>2008-08-01T10:52:53.812</DATE_FR><DATE_TO>2008-08-31T10:52:53.812</DATE_TO><IS_ES>0</IS_ES><DOC_DAYS>55</DOC_DAYS><MAX_DAYS>55</MAX_DAYS><ROW_COUNT>0</ROW_COUNT><TYPE_DOC>0</TYPE_DOC><ORDER_BY>0</ORDER_BY></XML>'
--exec REP_STATIST_FORBUY_TOTAL_EX @xmlParam = N'<XML><DATE_FR>2008-11-14T10:52:53.812</DATE_FR><DATE_TO>2008-11-20T10:52:53.812</DATE_TO><IS_ES>0</IS_ES><DOC_DAYS>4</DOC_DAYS><MAX_DAYS>4</MAX_DAYS><ROW_COUNT>0</ROW_COUNT><TYPE_DOC>0</TYPE_DOC><ORDER_BY>1</ORDER_BY><STORE>156</STORE></XML>'