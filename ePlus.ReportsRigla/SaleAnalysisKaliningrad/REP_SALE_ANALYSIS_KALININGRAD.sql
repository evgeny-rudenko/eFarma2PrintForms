 IF (OBJECT_ID('REP_SALE_ANALYSIS_KALININGRAD') IS NULL) 
    EXEC ('CREATE PROCEDURE REP_SALE_ANALYSIS_KALININGRAD AS RETURN')
GO
ALTER PROCEDURE REP_SALE_ANALYSIS_KALININGRAD(
	@XMLPARAM NTEXT)
AS
DECLARE @DATE_FROM DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @REMAINS_ONLY BIT

DECLARE @HDOC INT, @ERROR INT, @ROWCOUNT INT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM
    SELECT TOP 1 * INTO #PARAMS FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
	REMAINS_ONLY BIT 'REMAINS_ONLY')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT @DATE_FROM = DATE_FR, @DATE_TO = DATE_TO, @REMAINS_ONLY = REMAINS_ONLY FROM #PARAMS
EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT

DECLARE @ID_CONTRACTOR_STU BIGINT
SELECT TOP 1 @ID_CONTRACTOR_STU = ID_CONTRACTOR FROM CONTRACTOR WHERE [NAME] = '���'

-- A_COD=39007
-- ��� �����
-- 
-- 
-- A_COD=39003
-- �����������
-- 
-- A_COD=39004
-- ��������
-- 
-- 
-- A_COD=39005
-- �����������
-- 
-- 
-- A_COD=39001
-- ��������
-- 
-- 
-- A_COD=39002
-- ���������
-- 
-- 
-- A_COD=39006
-- ����������

SELECT
	G.ID_GOODS,
	GOODS_NAME = G.[NAME],
	CODE = G.CODE,
	CODE_1C = GC.CODE,
	CATEGORY1 = GK.[NAME],
	CATEGORY2 = (SELECT TOP 1 GG.[NAME] FROM GOODS_GROUP GG INNER JOIN GOODS_2_GROUP G2G ON GG.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP WHERE G2G.ID_GOODS = G.ID_GOODS),
	TOTAL_SALES = CONVERT(INT, NULL),

	DRUGSTORE1_SALES = CONVERT(INT, NULL),
	DRUGSTORE2_SALES = CONVERT(INT, NULL),
	DRUGSTORE3_SALES = CONVERT(INT, NULL),
	DRUGSTORE4_SALES = CONVERT(INT, NULL),
	DRUGSTORE5_SALES = CONVERT(INT, NULL),
	DRUGSTORE6_SALES = CONVERT(INT, NULL),

	DRUGSTORE1_REMAINS = CONVERT(MONEY, NULL),
	DRUGSTORE2_REMAINS = CONVERT(MONEY, NULL),
	DRUGSTORE3_REMAINS = CONVERT(MONEY, NULL),
	DRUGSTORE4_REMAINS = CONVERT(MONEY, NULL),
	DRUGSTORE5_REMAINS = CONVERT(MONEY, NULL),
	DRUGSTORE6_REMAINS = CONVERT(MONEY, NULL),

	DRUGSTORE1_LAST_PRICE = CONVERT(MONEY, NULL),
	DRUGSTORE2_LAST_PRICE = CONVERT(MONEY, NULL),
	DRUGSTORE3_LAST_PRICE = CONVERT(MONEY, NULL),
	DRUGSTORE4_LAST_PRICE = CONVERT(MONEY, NULL),
	DRUGSTORE5_LAST_PRICE = CONVERT(MONEY, NULL),
	DRUGSTORE6_LAST_PRICE = CONVERT(MONEY, NULL),

	CENTER_STORE_REMAINS = CONVERT(MONEY, NULL),
	TOTAL_REMAINS = CONVERT(MONEY, NULL),
	LAST_PRICE = CONVERT(MONEY, NULL),
	REMAINS_SALES = CONVERT(MONEY, NULL),
	ADD_PRICE_PERCENT = CONVERT(MONEY, NULL),
	LAST_PRICE_SUP = CONVERT(MONEY, NULL)
INTO #RESULT
FROM
	GOODS G
	LEFT JOIN GOODS_CODE GC ON GC.ID_GOODS = G.ID_GOODS AND GC.ID_CONTRACTOR = @ID_CONTRACTOR_STU
	LEFT JOIN GOODS_KIND GK ON GK.ID_GOODS_KIND = G.ID_GOODS_KIND
WHERE G.ID_GOODS IN (SELECT ID_GOODS FROM LOT) OR ISNULL(@REMAINS_ONLY, 0) = 0

SELECT 
	G.ID_GOODS,
	G.[NAME],
	QUANTITY_SALE = CONVERT(MONEY, NULL),
	C.ID_CONTRACTOR,
	REMAINS = CONVERT(MONEY, NULL),
	LAST_LOT_PRICE = CONVERT(MONEY, NULL)
INTO #TAB
FROM GOODS G(NOLOCK)
INNER JOIN LOT L(NOLOCK) ON L.ID_GOODS = G.ID_GOODS
INNER JOIN STORE S(NOLOCK) ON S.ID_STORE = L.ID_STORE
INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
GROUP BY G.ID_GOODS, G.[NAME], C.ID_CONTRACTOR

UPDATE #TAB SET QUANTITY_SALE = T.QUANTITY_SALE
FROM
	(SELECT 
		G.ID_GOODS,
		G.[NAME],
		QUANTITY_SALE = SUM(LM.QUANTITY_SUB),
		C.ID_CONTRACTOR
	FROM GOODS G(NOLOCK)
	INNER JOIN LOT L(NOLOCK) ON L.ID_GOODS = G.ID_GOODS
	INNER JOIN LOT_MOVEMENT LM(NOLOCK) ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
	INNER JOIN STORE S(NOLOCK) ON S.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE LM.CODE_OP IN ('CHEQUE', 'INVOICE_OUT') AND LM.DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
	AND C.A_COD IN (39003, 39004, 39005, 39001, 39002, 39006)
	GROUP BY G.ID_GOODS, G.NAME, C.ID_CONTRACTOR) T, #TAB
WHERE #TAB.ID_GOODS = T.ID_GOODS AND #TAB.ID_CONTRACTOR = T.ID_CONTRACTOR

UPDATE #TAB SET REMAINS = T.REMAINS
FROM
	(SELECT 
		SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB) AS REMAINS,
		L.ID_GOODS,
		C.ID_CONTRACTOR
	FROM
		LOT L(NOLOCK)
		INNER JOIN STORE S(NOLOCK) ON S.ID_STORE = L.ID_STORE
		INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
		INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
	WHERE LM.DATE_OP <= @DATE_TO
	GROUP BY L.ID_GOODS, C.ID_CONTRACTOR) T, #TAB
WHERE #TAB.ID_GOODS = T.ID_GOODS AND #TAB.ID_CONTRACTOR = T.ID_CONTRACTOR

SELECT L.* 
INTO #LATEST_LOT
FROM
LOT L
INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
INNER JOIN
	(SELECT MAX(T.ID_LOT_MOVEMENT) AS ID_LOT_MOVEMENT, L.ID_GOODS
		FROM
		LOT_MOVEMENT LM
		INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		INNER JOIN
			(SELECT MIN(ID_LOT_MOVEMENT) AS ID_LOT_MOVEMENT, LM.ID_LOT_GLOBAL
				FROM LOT_MOVEMENT LM
				GROUP BY LM.ID_LOT_GLOBAL) T ON T.ID_LOT_MOVEMENT = LM.ID_LOT_MOVEMENT
	GROUP BY L.ID_GOODS) T ON T.ID_LOT_MOVEMENT = LM.ID_LOT_MOVEMENT

SELECT T.ID_CONTRACTOR, L.*
INTO #LATEST_LOT_2_CONTRACTOR
FROM
LOT L
INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
INNER JOIN
	(SELECT MAX(T.ID_LOT_MOVEMENT) AS ID_LOT_MOVEMENT, L.ID_GOODS, S.ID_CONTRACTOR
		FROM
		LOT_MOVEMENT LM
		INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
		INNER JOIN
			(SELECT MIN(ID_LOT_MOVEMENT) AS ID_LOT_MOVEMENT, LM.ID_LOT_GLOBAL
				FROM LOT_MOVEMENT LM
				GROUP BY LM.ID_LOT_GLOBAL) T ON T.ID_LOT_MOVEMENT = LM.ID_LOT_MOVEMENT
	GROUP BY L.ID_GOODS, S.ID_CONTRACTOR) T ON T.ID_LOT_MOVEMENT = LM.ID_LOT_MOVEMENT

-- ��������� �������������� �������
UPDATE #RESULT SET TOTAL_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB GROUP BY ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

-- ������� �� ����������� ������
UPDATE #RESULT SET CENTER_STORE_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39007 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET TOTAL_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB GROUP BY ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET LAST_PRICE = L.PRICE_SAL
FROM #LATEST_LOT L, #RESULT
WHERE L.ID_GOODS = #RESULT.ID_GOODS

UPDATE #RESULT SET LAST_PRICE_SUP = L.PRICE_SUP
FROM #LATEST_LOT L, #RESULT
WHERE L.ID_GOODS = #RESULT.ID_GOODS

UPDATE #RESULT SET REMAINS_SALES = TOTAL_REMAINS / TOTAL_SALES
WHERE TOTAL_SALES <> 0

UPDATE #RESULT SET ADD_PRICE_PERCENT = (LAST_PRICE - LAST_PRICE_SUP) * 100 / LAST_PRICE_SUP
WHERE LAST_PRICE_SUP <> 0

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE1_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39003 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE1_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39003 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE1_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39003) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE2_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39004 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE2_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39004 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE2_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39004) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE3_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39005 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE3_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39005 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE3_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39005) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE4_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39001 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE4_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39001 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE4_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39001) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE5_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39002 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE5_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39002 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE5_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39002) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

--===========================��������� ��� ����� ������==============================
UPDATE #RESULT SET DRUGSTORE6_SALES = T.TOTAL_SALES
FROM
(SELECT SUM(QUANTITY_SALE) AS TOTAL_SALES, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39006 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE6_REMAINS = T.REMAINS
FROM
(SELECT SUM(REMAINS) AS REMAINS, ID_GOODS FROM #TAB T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39006 GROUP BY T.ID_GOODS) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS

UPDATE #RESULT SET DRUGSTORE6_LAST_PRICE = T.PRICE_SAL
FROM
(SELECT PRICE_SAL, ID_GOODS FROM #LATEST_LOT_2_CONTRACTOR T
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR
	WHERE C.A_COD = 39006) T, #RESULT
WHERE #RESULT.ID_GOODS = T.ID_GOODS
--===========================��������� ��� ����� ������==============================

SELECT * FROM #RESULT

RETURN 0
GO

--exec REP_SALE_ANALYSIS_KALININGRAD @xmlParam = N'<XML><DATE_FR>2007-12-12T00:00:00.000</DATE_FR><DATE_TO>2009-01-25T00:00:00.000</DATE_TO><REMAINS_ONLY>1</REMAINS_ONLY></XML>'