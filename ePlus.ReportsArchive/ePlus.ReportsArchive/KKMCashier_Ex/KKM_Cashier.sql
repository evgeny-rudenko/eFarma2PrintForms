SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REP_KKMCASHIER_EX') IS NULL EXEC('CREATE PROCEDURE DBO.REP_KKMCASHIER_EX AS RETURN')
GO
ALTER PROCEDURE DBO.REP_KKMCASHIER_EX
    @XMLPARAM NTEXT
AS

DECLARE @ALL_CASH_REGISTER BIT, @ALL_CASHIER BIT, @ALL_PRODUCER BIT, @ALL_CONTRACTOR BIT, @ALL_GOODS BIT
DECLARE @HDOC INT, @ID_DETAIL INT, @QTY_DAYS INT, @GOODS BIT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @QUERY_SAL NVARCHAR(4000)
DECLARE @QUERY_RET NVARCHAR(4000)
DECLARE @QUERY NVARCHAR(4000)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @ID_DETAIL = ID_DETAIL,
        @QTY_DAYS = QTY_DAYS,
        @GOODS = GOODS
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        ID_DETAIL INT 'ID_DETAIL',
        QTY_DAYS INT 'QTY_DAYS',
        GOODS BIT 'GOODS'
    )
    SELECT * INTO #CASHIER FROM OPENXML(@HDOC, '//ID_CASHIER') WITH(ID_CASHIER BIGINT '.') WHERE ID_CASHIER != 0
    IF @@ROWCOUNT = 0 SET @ALL_CASHIER = 1 ELSE SET @ALL_CASHIER = 0
    SELECT * INTO #CASH_REGISTER FROM OPENXML(@HDOC, '//ID_CASH_REGISTER') WITH(ID_CASH_REGISTER BIGINT '.') WHERE ID_CASH_REGISTER != 0
    IF @@ROWCOUNT = 0 SET @ALL_CASH_REGISTER = 1 ELSE SET @ALL_CASH_REGISTER = 0
    SELECT * INTO #PRODUCER FROM OPENXML(@HDOC, '//ID_PRODUCER') WITH(ID_PRODUCER BIGINT '.') WHERE ID_PRODUCER != 0
    IF @@ROWCOUNT = 0 SET @ALL_PRODUCER = 1 ELSE SET @ALL_PRODUCER = 0
    SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR != 0
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1 ELSE SET @ALL_CONTRACTOR = 0
    SELECT * INTO #GOODS FROM OPENXML(@HDOC, '//ID_GOODS') WITH(ID_GOODS BIGINT '.') WHERE ID_GOODS != 0
    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0
EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT


--SELECT CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, DATE_CHEQUE, 112), 8)) FROM CHEQUE

--�������
	SELECT 
       CASHIER = CH.ID_USER_DATA,
       CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        COUNT_CHEQUE_SAL = COUNT(DISTINCT(CONVERT(VARCHAR(200),CH.ID_CHEQUE_GLOBAL))),  --���������� ����� ������
		SUMM_QUANTITY_SAL_ALL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),--COUNT(CONVERT(DECIMAL(10,4),CONVERT(VARCHAR(100),CH_I.ID_CHEQUE_ITEM_GLOBAL)))
		SUMM_SUPPLIER_SAL_ALL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ���������� �����
		SUMM_RETAIL_SAL_ALL = SUM(CH_I.QUANTITY * CH_I.PRICE), --����� ��������� �����
        SUMM_DISCOUNT_SAL_ALL = SUM(CH_I.SUMM_DISCOUNT),--����� ������
		SUMM_SAL_ALL = SUM(CH_I.SUMM), --����� ������� ����� �� �����
		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')
	INTO #COUNT_CHEQUE_SAL_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER	
    WHERE CH.CHEQUE_TYPE IN ('SALE') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME, 
--		CS.ID_CASH_SESSION,
		CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

--��������
	SELECT 
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        COUNT_CHEQUE_RET = COUNT(DISTINCT(CONVERT(VARCHAR(200),CH.ID_CHEQUE_GLOBAL))),
		SUMM_QUANTITY_RET_ALL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
		SUMM_SUPPLIER_RET_ALL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ���������� �����
		SUMM_RETAIL_RET_ALL = SUM(CH_I.QUANTITY * CH_I.PRICE), --����� ��������� �����
        SUMM_DISCOUNT_RET_ALL = SUM(CH_I.SUMM_DISCOUNT),--����� ������
		SUMM_RET_ALL = SUM(CH_I.SUMM), --����� �������
		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')
	INTO #COUNT_CHEQUE_RET_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('RETURN') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME, 
--		CS.ID_CASH_SESSION,
		CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,--CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

	SELECT 
		SALE_TABLE.*,
		COUNT_CHEQUE_RET = ISNULL(RET_TABLE.COUNT_CHEQUE_RET,0),
		SUMM_QUANTITY_RET_ALL = ISNULL(RET_TABLE.SUMM_QUANTITY_RET_ALL,0),
		SUMM_SUPPLIER_RET_ALL = ISNULL(RET_TABLE.SUMM_SUPPLIER_RET_ALL,0),
		SUMM_RETAIL_RET_ALL = ISNULL(RET_TABLE.SUMM_RETAIL_RET_ALL,0),
		SUMM_DISCOUNT_RET_ALL = ISNULL(RET_TABLE.SUMM_DISCOUNT_RET_ALL,0),
		SUMM_RET_ALL = ISNULL(RET_TABLE.SUMM_RET_ALL,0)
	INTO #TOTAL_DATES
	FROM #COUNT_CHEQUE_SAL_TABLE SALE_TABLE
	LEFT JOIN #COUNT_CHEQUE_RET_TABLE RET_TABLE ON SALE_TABLE.CASHIER = RET_TABLE.CASHIER AND SALE_TABLE.CASH_REGISTER = RET_TABLE.CASH_REGISTER AND SALE_TABLE.DATE_OPEN = RET_TABLE.DATE_OPEN AND SALE_TABLE.DATE_CLOSE = RET_TABLE.DATE_CLOSE--(@GOODS=0 OR SALE_TABLE.GOODS = RET_TABLE.GOODS)
	
--���� �� ��������
    SELECT		
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        SUMM_QUANTITY_SAL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),  --����������
        SUMM_DISCOUNT_SAL = SUM(CH_I.SUMM_DISCOUNT), --����� ������
        SUMM_RETAIL_SAL = SUM(CH_I.QUANTITY * CH_I.PRICE),  --����� ���������
        SUMM_SUPPLIER_SAL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ����������
        SUMM_SAL = SUM(CH_I.SUMM),	--����� �������
        COUNT_CHEQUE_SAL = SUM(CASE CH.CHEQUE_TYPE WHEN 'SALE' THEN 1 ELSE CONVERT(INT, 0) END), --���������� ����� ������
 		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997'),
	    GOODS = (CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END),
 	    GOODS_NAME = (CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END),
 	    PRICE_SUP_SAL = SUM(CASE WHEN @GOODS = 1 THEN L.PRICE_SUP * CH_I.QUANTITY ELSE NULL END),
 	    PRICE_SAL = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM ELSE NULL END),
 	    PRICE_WITH_DISCOUNT_SAL = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM_DISCOUNT ELSE NULL END)
	INTO #SALE_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('SALE') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
 		CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END,
		CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

--���� �� ���������
    SELECT		
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        SUMM_QUANTITY_RET = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
        SUMM_DISCOUNT_RET = SUM(CH_I.SUMM_DISCOUNT),
        SUMM_RETAIL_RET = SUM(CH_I.QUANTITY * CH_I.PRICE),
        SUMM_SUPPLIER_RET = SUM(CH_I.QUANTITY * L.PRICE_SUP),
        SUMM_RET = SUM(CH_I.SUMM),
        COUNT_CHEQUE_RET = SUM(CASE CH.CHEQUE_TYPE WHEN 'RETURN' THEN 1 ELSE CONVERT(INT,0) END),
 		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997'),
	    GOODS = (CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END),
 	    GOODS_NAME = (CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END),
 	    PRICE_SUP_RET = SUM(CASE WHEN @GOODS = 1 THEN L.PRICE_SUP * CH_I.QUANTITY ELSE NULL END),
 	    PRICE_RET = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM ELSE NULL END),
 	    PRICE_WITH_DISCOUNT_RET = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM_DISCOUNT ELSE NULL END)
	INTO #RET_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('RETURN') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
   GROUP BY CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
 		CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END,
		CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

	SELECT 
		ST.*,
		SUMM_QUANTITY_RET = ISNULL(RT.SUMM_QUANTITY_RET,0),
		SUMM_DISCOUNT_RET = ISNULL(RT.SUMM_DISCOUNT_RET,0),
		SUMM_RETAIL_RET = ISNULL(RT.SUMM_RETAIL_RET,0),
		SUMM_SUPPLIER_RET = ISNULL(RT.SUMM_SUPPLIER_RET,0),
		SUMM_RET = ISNULL(RT.SUMM_RET,0),
		COUNT_CHEQUE_RET = ISNULL(RT.COUNT_CHEQUE_RET,0),
 	    PRICE_SUP_RET = ISNULL(RT.PRICE_SUP_RET,0),
 	    PRICE_RET = ISNULL(RT.PRICE_RET,0),
 	    PRICE_WITH_DISCOUNT_RET = ISNULL(RT.PRICE_WITH_DISCOUNT_RET,0)
	INTO #TOTAL_GOODS
	FROM #SALE_TABLE ST
	LEFT JOIN #RET_TABLE RT ON ST.CASHIER = RT.CASHIER AND ST.CASH_REGISTER = RT.CASH_REGISTER AND ST.DATE_OPEN = RT.DATE_OPEN AND ST.DATE_CLOSE = RT.DATE_CLOSE AND (@GOODS=0 OR ST.GOODS = RT.GOODS)

	IF(@GOODS = 1)
	BEGIN
		SELECT 
			TG.CASHIER,
			TG.CASHIER_NAME,
			TG.CASH_REGISTER,
			TG.NAME_CASH_REGISTER,
			SUMM_QUANTITY_SAL = ISNULL(NULL,0),
			SUMM_DISCOUNT_SAL = ISNULL(NULL,0),
			SUMM_RETAIL_SAL = ISNULL(NULL,0),
			SUMM_SUPPLIER_SAL = ISNULL(NULL,0),
			SUMM_SAL = ISNULL(NULL,0),
			COUNT_CHEQUE_SAL = ISNULL(NULL,0),
			TG.DATE_OPEN,
			TG.DATE_CLOSE,
			TG.GOODS,
			TG.GOODS_NAME,
			PRICE_SUP_SAL = ISNULL(NULL,0),
			PRICE_SAL = ISNULL(NULL,0),
			PRICE_WITH_DISCOUNT_SAL = ISNULL(NULL,0),
			SUMM_QUANTITY_RET = TG.SUMM_QUANTITY_RET,
			SUMM_DISCOUNT_RET = TG.SUMM_DISCOUNT_RET,
			SUMM_RETAIL_RET = TG.SUMM_RETAIL_RET,
			SUMM_SUPPLIER_RET = TG.SUMM_SUPPLIER_RET,
			SUMM_RET = TG.SUMM_RET,
			COUNT_CHEQUE_RET = TG.COUNT_CHEQUE_RET,
			PRICE_SUP_RET = TG.PRICE_SUP_RET,
			PRICE_RET = TG.PRICE_RET,
			PRICE_WITH_DISCOUNT_RET = TG.PRICE_WITH_DISCOUNT_RET
		INTO #RTV
		FROM #RET_TABLE TG
		WHERE NOT EXISTS (SELECT GOODS FROM #TOTAL_GOODS TGU WHERE TGU.CASHIER = TG.CASHIER AND TGU.CASH_REGISTER = TG.CASH_REGISTER AND TGU.DATE_OPEN = TG.DATE_OPEN AND TGU.DATE_CLOSE = TG.DATE_CLOSE AND TGU.GOODS = TG.GOODS)

	END
	
	CREATE TABLE #TOTAL_GOODS_UNION(
			CASHIER BIGINT NULL,
			CASHIER_NAME NVARCHAR(256) NULL,
			CASH_REGISTER BIGINT NULL,
			NAME_CASH_REGISTER NVARCHAR(256) NULL,
			SUMM_QUANTITY_SAL DECIMAL(18, 4) NULL,
			SUMM_DISCOUNT_SAL DECIMAL(18, 4) NULL,
			SUMM_RETAIL_SAL DECIMAL(18, 4) NULL,
			SUMM_SUPPLIER_SAL DECIMAL(18, 4) NULL,
			SUMM_SAL DECIMAL(18, 4) NULL,
			COUNT_CHEQUE_SAL DECIMAL(18, 4) NULL,
			DATE_OPEN DATETIME,
			DATE_CLOSE DATETIME,
			GOODS BIGINT NULL,
			GOODS_NAME NVARCHAR(256) NULL,
			PRICE_SUP_SAL DECIMAL(18, 4) NULL,
			PRICE_SAL DECIMAL(18, 4) NULL,
			PRICE_WITH_DISCOUNT_SAL DECIMAL(18, 4) NULL,
			SUMM_QUANTITY_RET DECIMAL(18, 4) NULL,
			SUMM_DISCOUNT_RET DECIMAL(18, 4) NULL,
			SUMM_RETAIL_RET DECIMAL(18, 4) NULL,
			SUMM_SUPPLIER_RET DECIMAL(18, 4) NULL,
			SUMM_RET DECIMAL(18, 4) NULL,
			COUNT_CHEQUE_RET DECIMAL(18, 4) NULL,
			PRICE_SUP_RET DECIMAL(18, 4) NULL,
			PRICE_RET DECIMAL(18, 4) NULL,
			PRICE_WITH_DISCOUNT_RET DECIMAL(18, 4) NULL)

	IF(@GOODS = 1)
	BEGIN
		INSERT INTO #TOTAL_GOODS_UNION 
		SELECT 
			UN.* 
		FROM (SELECT 
			* 
		FROM #TOTAL_GOODS
		UNION
		SELECT * FROM #RTV) UN
	END
	ELSE
	BEGIN
		INSERT INTO #TOTAL_GOODS_UNION  
		SELECT 
			* 
		FROM #TOTAL_GOODS
	END

	SELECT
		TD.*,
		TG.SUM_COUNT_CHEQUE_SAL,
		TG.SUM_COUNT_CHEQUE_RET
	FROM #TOTAL_DATES TD
	LEFT JOIN (
	SELECT 
		CASHIER,
		CASH_REGISTER,
		DATE_OPEN,
		DATE_CLOSE,
		SUM_COUNT_CHEQUE_SAL = SUM(COUNT_CHEQUE_SAL),
		SUM_COUNT_CHEQUE_RET = SUM(COUNT_CHEQUE_RET) 
	FROM #TOTAL_GOODS_UNION 
	GROUP BY CASHIER,CASH_REGISTER,DATE_OPEN,DATE_CLOSE) TG ON TD.CASHIER = TG.CASHIER AND TD.CASH_REGISTER = TG.CASH_REGISTER AND TD.DATE_OPEN = TG.DATE_OPEN AND TD.DATE_CLOSE = TG.DATE_CLOSE

	SELECT * FROM #TOTAL_GOODS_UNION ORDER BY CASHIER_NAME,NAME_CASH_REGISTER,DATE_OPEN,GOODS_NAME

SELECT DATE_FR = @DATE_FR, DATE_TO = @DATE_TO

RETURN 0
GO


SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REP_KKMCASHIER_EX_SERVICE') IS NULL EXEC('CREATE PROCEDURE DBO.REP_KKMCASHIER_EX_SERVICE AS RETURN')
GO
ALTER PROCEDURE DBO.REP_KKMCASHIER_EX_SERVICE
    @XMLPARAM NTEXT
AS

DECLARE @ALL_CASH_REGISTER BIT, @ALL_CASHIER BIT, @ALL_PRODUCER BIT, @ALL_CONTRACTOR BIT, @ALL_GOODS BIT
DECLARE @HDOC INT, @ID_DETAIL INT, @QTY_DAYS INT, @GOODS BIT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @QUERY_SAL NVARCHAR(4000)
DECLARE @QUERY_RET NVARCHAR(4000)
DECLARE @QUERY NVARCHAR(4000)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @ID_DETAIL = ID_DETAIL,
        @QTY_DAYS = QTY_DAYS,
        @GOODS = GOODS
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        ID_DETAIL INT 'ID_DETAIL',
        QTY_DAYS INT 'QTY_DAYS',
        GOODS BIT 'GOODS'
    )
    SELECT * INTO #CASHIER FROM OPENXML(@HDOC, '//ID_CASHIER') WITH(ID_CASHIER BIGINT '.') WHERE ID_CASHIER != 0
    IF @@ROWCOUNT = 0 SET @ALL_CASHIER = 1 ELSE SET @ALL_CASHIER = 0
    SELECT * INTO #CASH_REGISTER FROM OPENXML(@HDOC, '//ID_CASH_REGISTER') WITH(ID_CASH_REGISTER BIGINT '.') WHERE ID_CASH_REGISTER != 0
    IF @@ROWCOUNT = 0 SET @ALL_CASH_REGISTER = 1 ELSE SET @ALL_CASH_REGISTER = 0
    SELECT * INTO #PRODUCER FROM OPENXML(@HDOC, '//ID_PRODUCER') WITH(ID_PRODUCER BIGINT '.') WHERE ID_PRODUCER != 0
    IF @@ROWCOUNT = 0 SET @ALL_PRODUCER = 1 ELSE SET @ALL_PRODUCER = 0
    SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR != 0
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1 ELSE SET @ALL_CONTRACTOR = 0
    SELECT * INTO #GOODS FROM OPENXML(@HDOC, '//ID_GOODS') WITH(ID_GOODS BIGINT '.') WHERE ID_GOODS != 0
    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

--�������
	SELECT 
       CASHIER = CH.ID_USER_DATA,
       CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        COUNT_CHEQUE_SAL = COUNT(DISTINCT(CONVERT(VARCHAR(200),CH.ID_CHEQUE_GLOBAL))),  --���������� ����� ������
		SUMM_QUANTITY_SAL_ALL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),--COUNT(CONVERT(DECIMAL(10,4),CONVERT(VARCHAR(100),CH_I.ID_CHEQUE_ITEM_GLOBAL)))
		SUMM_SUPPLIER_SAL_ALL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ���������� �����
		SUMM_RETAIL_SAL_ALL = SUM(CH_I.QUANTITY * CH_I.PRICE), --����� ��������� �����
        SUMM_DISCOUNT_SAL_ALL = SUM(CH_I.SUMM_DISCOUNT),--����� ������
		SUMM_SAL_ALL = SUM(CH_I.SUMM), --����� ������� ����� �� �����
		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')
	INTO #COUNT_CHEQUE_SAL_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        LEFT JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        LEFT JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        LEFT JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        LEFT JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER	
    WHERE CH.CHEQUE_TYPE IN ('SALE') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME,
		CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')
select * from  #COUNT_CHEQUE_SAL_TABLE
--��������
	SELECT 
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        COUNT_CHEQUE_RET = COUNT(DISTINCT(CONVERT(VARCHAR(200),CH.ID_CHEQUE_GLOBAL))),
		SUMM_QUANTITY_RET_ALL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
		SUMM_SUPPLIER_RET_ALL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ���������� �����
		SUMM_RETAIL_RET_ALL = SUM(CH_I.QUANTITY * CH_I.PRICE), --����� ��������� �����
        SUMM_DISCOUNT_RET_ALL = SUM(CH_I.SUMM_DISCOUNT),--����� ������
		SUMM_RET_ALL = SUM(CH_I.SUMM), --����� �������
		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')
	INTO #COUNT_CHEQUE_RET_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('RETURN') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME, 

		CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,--CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

	SELECT 
		SALE_TABLE.*,
		COUNT_CHEQUE_RET = ISNULL(RET_TABLE.COUNT_CHEQUE_RET,0),
		SUMM_QUANTITY_RET_ALL = ISNULL(RET_TABLE.SUMM_QUANTITY_RET_ALL,0),
		SUMM_SUPPLIER_RET_ALL = ISNULL(RET_TABLE.SUMM_SUPPLIER_RET_ALL,0),
		SUMM_RETAIL_RET_ALL = ISNULL(RET_TABLE.SUMM_RETAIL_RET_ALL,0),
		SUMM_DISCOUNT_RET_ALL = ISNULL(RET_TABLE.SUMM_DISCOUNT_RET_ALL,0),
		SUMM_RET_ALL = ISNULL(RET_TABLE.SUMM_RET_ALL,0)
	INTO #TOTAL_DATES
	FROM #COUNT_CHEQUE_SAL_TABLE SALE_TABLE
	LEFT JOIN #COUNT_CHEQUE_RET_TABLE RET_TABLE ON SALE_TABLE.CASHIER = RET_TABLE.CASHIER AND SALE_TABLE.CASH_REGISTER = RET_TABLE.CASH_REGISTER AND SALE_TABLE.DATE_OPEN = RET_TABLE.DATE_OPEN AND SALE_TABLE.DATE_CLOSE = RET_TABLE.DATE_CLOSE--(@GOODS=0 OR SALE_TABLE.GOODS = RET_TABLE.GOODS)
	
--���� �� ��������
    SELECT		
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        SUMM_QUANTITY_SAL = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),  --����������
        SUMM_DISCOUNT_SAL = SUM(CH_I.SUMM_DISCOUNT), --����� ������
        SUMM_RETAIL_SAL = SUM(CH_I.QUANTITY * CH_I.PRICE),  --����� ���������
        SUMM_SUPPLIER_SAL = SUM(CH_I.QUANTITY * L.PRICE_SUP), --����� ����������
        SUMM_SAL = SUM(CH_I.SUMM),	--����� �������
        COUNT_CHEQUE_SAL = SUM(CASE CH.CHEQUE_TYPE WHEN 'SALE' THEN 1 ELSE CONVERT(INT, 0) END), --���������� ����� ������
 		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997'),
	    GOODS = (CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END),
 	    GOODS_NAME = (CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END),
 	    PRICE_SUP_SAL = SUM(CASE WHEN @GOODS = 1 THEN L.PRICE_SUP * CH_I.QUANTITY ELSE NULL END),
 	    PRICE_SAL = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM ELSE NULL END),
 	    PRICE_WITH_DISCOUNT_SAL = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM_DISCOUNT ELSE NULL END)
	INTO #SALE_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        LEFT JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        LEFT JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        LEFT JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        LEFT JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('SALE') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
    GROUP BY CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
 		CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END,
		CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

--���� �� ���������
    SELECT		
        CASHIER = CH.ID_USER_DATA,
        CASHIER_NAME = U.FULL_NAME,
        CASH_REGISTER = CR.ID_CASH_REGISTER,
        NAME_CASH_REGISTER = CR.NAME_CASH_REGISTER,
        SUMM_QUANTITY_RET = SUM(CH_I.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
        SUMM_DISCOUNT_RET = SUM(CH_I.SUMM_DISCOUNT),
        SUMM_RETAIL_RET = SUM(CH_I.QUANTITY * CH_I.PRICE),
        SUMM_SUPPLIER_RET = SUM(CH_I.QUANTITY * L.PRICE_SUP),
        SUMM_RET = SUM(CH_I.SUMM),
        COUNT_CHEQUE_RET = SUM(CASE CH.CHEQUE_TYPE WHEN 'RETURN' THEN 1 ELSE CONVERT(INT,0) END),
 		DATE_OPEN = CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
	    DATE_CLOSE = CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997'),
	    GOODS = (CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END),
 	    GOODS_NAME = (CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END),
 	    PRICE_SUP_RET = SUM(CASE WHEN @GOODS = 1 THEN L.PRICE_SUP * CH_I.QUANTITY ELSE NULL END),
 	    PRICE_RET = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM ELSE NULL END),
 	    PRICE_WITH_DISCOUNT_RET = SUM(CASE WHEN @GOODS = 1 THEN CH_I.SUMM_DISCOUNT ELSE NULL END)
	INTO #RET_TABLE
    FROM CASH_SESSION CS(NOLOCK)
        INNER JOIN CHEQUE CH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
        LEFT JOIN [META_USER] U(NOLOCK) ON CH.ID_USER_DATA = U.USER_NUM
        INNER JOIN CASH_REGISTER CR(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
        INNER JOIN CHEQUE_ITEM CH_I(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
        INNER JOIN SCALING_RATIO SR(NOLOCK) ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
        INNER JOIN LOT L(NOLOCK) ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = L.ID_SUPPLIER
        INNER JOIN GOODS G(NOLOCK) ON CH_I.ID_GOODS = G.ID_GOODS
        LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    WHERE CH.CHEQUE_TYPE IN ('RETURN') AND CH.DOCUMENT_STATE = 'PROC'
        AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
        AND (@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
        AND (@ALL_CASHIER = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIER))
        AND (@ALL_PRODUCER = 1 OR P.ID_PRODUCER IN (SELECT * FROM #PRODUCER))
        AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
        AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT * FROM #GOODS))
   GROUP BY CH.ID_USER_DATA, U.FULL_NAME, CR.ID_CASH_REGISTER, CR.NAME_CASH_REGISTER,
 		CASE WHEN @GOODS = 1 THEN G.ID_GOODS ELSE NULL END,
		CASE WHEN @GOODS = 1 THEN (ISNULL(G.[NAME], '') + ';������������: ' + ISNULL(P.[NAME], '') + '; ���������: ' + ISNULL(C.[NAME], '')) ELSE NULL END,
		CONVERT(DATETIME, LEFT(CONVERT(VARCHAR, CH.DATE_CHEQUE, 112), 8)),
		CONVERT(DATETIME, CONVERT(VARCHAR, CH.DATE_CHEQUE, 112) + ' 23:59:59.997')

--��������� ������� � ������� �� �������
	SELECT 
		ST.*,
		SUMM_QUANTITY_RET = ISNULL(RT.SUMM_QUANTITY_RET,0),
		SUMM_DISCOUNT_RET = ISNULL(RT.SUMM_DISCOUNT_RET,0),
		SUMM_RETAIL_RET = ISNULL(RT.SUMM_RETAIL_RET,0),
		SUMM_SUPPLIER_RET = ISNULL(RT.SUMM_SUPPLIER_RET,0),
		SUMM_RET = ISNULL(RT.SUMM_RET,0),
		COUNT_CHEQUE_RET = ISNULL(RT.COUNT_CHEQUE_RET,0),
 	    PRICE_SUP_RET = ISNULL(RT.PRICE_SUP_RET,0),
 	    PRICE_RET = ISNULL(RT.PRICE_RET,0),
 	    PRICE_WITH_DISCOUNT_RET = ISNULL(RT.PRICE_WITH_DISCOUNT_RET,0)
	INTO #TOTAL_GOODS
	FROM #SALE_TABLE ST
	LEFT JOIN #RET_TABLE RT ON ST.CASHIER = RT.CASHIER AND ST.CASH_REGISTER = RT.CASH_REGISTER AND ST.DATE_OPEN = RT.DATE_OPEN AND ST.DATE_CLOSE = RT.DATE_CLOSE AND (@GOODS=0 OR ST.GOODS = RT.GOODS)

	IF(@GOODS = 1)
	BEGIN
		SELECT 
			TG.CASHIER,
			TG.CASHIER_NAME,
			TG.CASH_REGISTER,
			TG.NAME_CASH_REGISTER,
			SUMM_QUANTITY_SAL = ISNULL(NULL,0),
			SUMM_DISCOUNT_SAL = ISNULL(NULL,0),
			SUMM_RETAIL_SAL = ISNULL(NULL,0),
			SUMM_SUPPLIER_SAL = ISNULL(NULL,0),
			SUMM_SAL = ISNULL(NULL,0),
			COUNT_CHEQUE_SAL = ISNULL(NULL,0),
			TG.DATE_OPEN,
			TG.DATE_CLOSE,
			TG.GOODS,
			TG.GOODS_NAME,
			PRICE_SUP_SAL = ISNULL(NULL,0),
			PRICE_SAL = ISNULL(NULL,0),
			PRICE_WITH_DISCOUNT_SAL = ISNULL(NULL,0),
			SUMM_QUANTITY_RET = TG.SUMM_QUANTITY_RET,
			SUMM_DISCOUNT_RET = TG.SUMM_DISCOUNT_RET,
			SUMM_RETAIL_RET = TG.SUMM_RETAIL_RET,
			SUMM_SUPPLIER_RET = TG.SUMM_SUPPLIER_RET,
			SUMM_RET = TG.SUMM_RET,
			COUNT_CHEQUE_RET = TG.COUNT_CHEQUE_RET,
			PRICE_SUP_RET = TG.PRICE_SUP_RET,
			PRICE_RET = TG.PRICE_RET,
			PRICE_WITH_DISCOUNT_RET = TG.PRICE_WITH_DISCOUNT_RET
		INTO #RTV
		FROM #RET_TABLE TG
		WHERE NOT EXISTS (SELECT GOODS FROM #TOTAL_GOODS TGU WHERE TGU.CASHIER = TG.CASHIER AND TGU.CASH_REGISTER = TG.CASH_REGISTER AND TGU.DATE_OPEN = TG.DATE_OPEN AND TGU.DATE_CLOSE = TG.DATE_CLOSE AND TGU.GOODS = TG.GOODS)

	END
	
	CREATE TABLE #TOTAL_GOODS_UNION(
			CASHIER BIGINT NULL,
			CASHIER_NAME NVARCHAR(256) NULL,
			CASH_REGISTER BIGINT NULL,
			NAME_CASH_REGISTER NVARCHAR(256) NULL,
			SUMM_QUANTITY_SAL DECIMAL(18, 4) NULL,
			SUMM_DISCOUNT_SAL DECIMAL(18, 4) NULL,
			SUMM_RETAIL_SAL DECIMAL(18, 4) NULL,
			SUMM_SUPPLIER_SAL DECIMAL(18, 4) NULL,
			SUMM_SAL DECIMAL(18, 4) NULL,
			COUNT_CHEQUE_SAL DECIMAL(18, 4) NULL,
			DATE_OPEN DATETIME,
			DATE_CLOSE DATETIME,
			GOODS BIGINT NULL,
			GOODS_NAME NVARCHAR(256) NULL,
			PRICE_SUP_SAL DECIMAL(18, 4) NULL,
			PRICE_SAL DECIMAL(18, 4) NULL,
			PRICE_WITH_DISCOUNT_SAL DECIMAL(18, 4) NULL,
			SUMM_QUANTITY_RET DECIMAL(18, 4) NULL,
			SUMM_DISCOUNT_RET DECIMAL(18, 4) NULL,
			SUMM_RETAIL_RET DECIMAL(18, 4) NULL,
			SUMM_SUPPLIER_RET DECIMAL(18, 4) NULL,
			SUMM_RET DECIMAL(18, 4) NULL,
			COUNT_CHEQUE_RET DECIMAL(18, 4) NULL,
			PRICE_SUP_RET DECIMAL(18, 4) NULL,
			PRICE_RET DECIMAL(18, 4) NULL,
			PRICE_WITH_DISCOUNT_RET DECIMAL(18, 4) NULL)

	IF(@GOODS = 1)
	BEGIN
		INSERT INTO #TOTAL_GOODS_UNION 
		SELECT 
			UN.* 
		FROM (SELECT 
			* 
		FROM #TOTAL_GOODS
		UNION
		SELECT * FROM #RTV) UN
	END
	ELSE
	BEGIN
		INSERT INTO #TOTAL_GOODS_UNION  
		SELECT 
			* 
		FROM #TOTAL_GOODS
	END

	SELECT
		TD.*,
		TG.SUM_COUNT_CHEQUE_SAL,
		TG.SUM_COUNT_CHEQUE_RET
	FROM #TOTAL_DATES TD
	LEFT JOIN (
	SELECT 
		CASHIER,
		CASH_REGISTER,
		DATE_OPEN,
		DATE_CLOSE,
		SUM_COUNT_CHEQUE_SAL = SUM(COUNT_CHEQUE_SAL),
		SUM_COUNT_CHEQUE_RET = SUM(COUNT_CHEQUE_RET) 
	FROM #TOTAL_GOODS_UNION 
	GROUP BY CASHIER,CASH_REGISTER,DATE_OPEN,DATE_CLOSE) TG ON TD.CASHIER = TG.CASHIER AND TD.CASH_REGISTER = TG.CASH_REGISTER AND TD.DATE_OPEN = TG.DATE_OPEN AND TD.DATE_CLOSE = TG.DATE_CLOSE

SELECT * FROM #TOTAL_GOODS_UNION ORDER BY CASHIER_NAME,NAME_CASH_REGISTER,DATE_OPEN,GOODS_NAME
	
SELECT DATE_FR = @DATE_FR, DATE_TO = @DATE_TO

RETURN 0
GO

/*
exec REP_KKMCASHIER_EX_SERVICE N'
<XML>
	<DATE_FR>2009-04-22T00:00:00.000</DATE_FR>
	<DATE_TO>2009-04-22T00:00:00.000</DATE_TO>
	<ID_DETAIL>2</ID_DETAIL>
	<GOODS>1</GOODS>
</XML>'*/