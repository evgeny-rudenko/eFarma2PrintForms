SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_OUT_REGISTRY') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_OUT_REGISTRY AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_INVOICE_OUT_REGISTRY
    @XMLPARAM NTEXT AS
    
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME

DECLARE @ALL_STORE BIT
DECLARE @ALL_CONTRACTOR BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO	
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #ID_STORES FROM OPENXML(@HDOC, '/XML/ID_STORES') WITH(ID_STORE BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_STORE = 1

SELECT * INTO #ID_CONTRACTORS FROM OPENXML(@HDOC, '/XML/ID_CONTRACTORS') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CONTRACTOR = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

CREATE TABLE #VAT10 (
	ID_INVOICE_OUT BIGINT, 
	VAT_SUM MONEY
)

CREATE TABLE #VAT18 (
	ID_INVOICE_OUT BIGINT, 
	VAT_SUM MONEY
)

INSERT INTO #VAT10
        ( ID_INVOICE_OUT, VAT_SUM )
SELECT 
	INO.ID_INVOICE_OUT, 
	SUM(IOI.PSUM_SAL) AS VAT_SUM
FROM INVOICE_OUT INO
INNER JOIN INVOICE_OUT_ITEM IOI ON INO.ID_INVOICE_OUT_GLOBAL = IOI.ID_INVOICE_OUT_GLOBAL
LEFT JOIN LOT L ON IOI.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
WHERE L.VAT = 10
AND INO.STATE = 'PROC'
AND INO.DOC_DATE BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_CONTRACTOR = 1 OR INO.ID_CONTRACTOR_TO IN (SELECT ID_CONTRACTOR FROM #ID_CONTRACTORS))
	AND (@ALL_STORE = 1 OR INO.ID_STORE IN (SELECT ID_STORE FROM #ID_STORES))
GROUP BY INO.ID_INVOICE_OUT

CREATE INDEX I_VAT10 ON #VAT10(ID_INVOICE_OUT)

INSERT INTO #VAT18
        ( ID_INVOICE_OUT, VAT_SUM )
SELECT 
	INO.ID_INVOICE_OUT, 
	SUM(IOI.PSUM_SAL) AS VAT_SUM
FROM INVOICE_OUT INO
INNER JOIN INVOICE_OUT_ITEM IOI ON INO.ID_INVOICE_OUT_GLOBAL = IOI.ID_INVOICE_OUT_GLOBAL
LEFT JOIN LOT L ON IOI.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
WHERE L.VAT = 18
AND INO.STATE = 'PROC'
AND INO.DOC_DATE BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_CONTRACTOR = 1 OR INO.ID_CONTRACTOR_TO IN (SELECT ID_CONTRACTOR FROM #ID_CONTRACTORS))
	AND (@ALL_STORE = 1 OR INO.ID_STORE IN (SELECT ID_STORE FROM #ID_STORES))
GROUP BY INO.ID_INVOICE_OUT

CREATE INDEX I_VAT18 ON #VAT18(ID_INVOICE_OUT)

SELECT DISTINCT
	INO.ID_CONTRACTOR_TO AS ID_CONTRACTOR_TO, 
	C.NAME AS CONTRACTOR_TO_NAME, 
	INO.MNEMOCODE AS DOC_NUM, 
	INO.SUM_SAL AS SUM_SAL, 
	(ISNULL(V10.VAT_SUM, 0)+ISNULL(V18.VAT_SUM, 0)) AS VAT_SUM, 
	ISNULL(V10.VAT_SUM, 0) AS VAT10_SUM, 
	ISNULL(V18.VAT_SUM, 0) AS VAT18_SUM, 
	INO.SUM_SUP AS SUM_SUP
FROM INVOICE_OUT INO
INNER JOIN INVOICE_OUT_ITEM IOI ON INO.ID_INVOICE_OUT_GLOBAL = IOI.ID_INVOICE_OUT_GLOBAL
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = INO.ID_CONTRACTOR_TO
LEFT JOIN #VAT10 V10 ON INO.ID_INVOICE_OUT = V10.ID_INVOICE_OUT
LEFT JOIN #VAT18 V18 ON INO.ID_INVOICE_OUT = V18.ID_INVOICE_OUT
WHERE INO.DOC_DATE BETWEEN @DATE_FR AND @DATE_TO
AND INO.STATE = 'PROC'
AND (@ALL_CONTRACTOR = 1 OR INO.ID_CONTRACTOR_TO IN (SELECT ID_CONTRACTOR FROM #ID_CONTRACTORS))
AND (@ALL_STORE = 1 OR INO.ID_STORE IN (SELECT ID_STORE FROM #ID_STORES))
ORDER BY C.NAME, INO.MNEMOCODE
	
DROP TABLE #VAT10
DROP TABLE #VAT18

RETURN 
GO