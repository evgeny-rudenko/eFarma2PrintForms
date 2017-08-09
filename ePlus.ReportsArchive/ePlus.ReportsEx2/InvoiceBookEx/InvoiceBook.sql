SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_BOOK') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_BOOK AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_BOOK
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT

DECLARE	@DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @DETAIL BIT
DECLARE @ALL_STORES BIT
DECLARE @SHOW_REMAIN BIT
   
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
		
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@DETAIL = DETAIL,
	@SHOW_REMAIN = SHOW_REMAIN
FROM OPENXML(@HDOC , '/XML') 
	WITH(DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	DETAIL BIT 'DETAIL',
	SHOW_REMAIN BIT 'SHOW_REMAIN')
    
SELECT ID_STORE = STORE INTO #STORES
FROM OPENXML(@HDOC, '/XML/STORE') WITH(STORE BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_STORES = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUTPUT, @DATE_TO OUTPUT
EXEC USP_RANGE_DAYS @DATE_FR OUTPUT, @DATE_TO OUTPUT

DECLARE @TOTAL_CONTRACTOR_SUM_VAT MONEY

SELECT
	CONTRACTOR_NAME = C.NAME,
	CONTRACTOR_SUM_VAT = SUM(DM.SUM_SUP),
	RETAIL_SUM = SUM(DM.SUM_ACC)
INTO #TEMP_T
FROM DOC_MOVEMENT DM
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
WHERE ((@SHOW_REMAIN <> 1 AND DM.ID_TABLE = 2) OR (@SHOW_REMAIN = 1 AND DM.ID_TABLE IN (2,30)))
	AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_STORES = 1 OR DM.ID_STORE IN (SELECT ID_STORE FROM #STORES))
GROUP BY DM.ID_CONTRACTOR_FROM, C.NAME

SELECT @TOTAL_CONTRACTOR_SUM_VAT = SUM(CONTRACTOR_SUM_VAT) FROM #TEMP_T

SELECT
	CONTRACTOR_NAME = T.CONTRACTOR_NAME,
	CONTRACTOR_SUM_VAT = T.CONTRACTOR_SUM_VAT,
	RETAIL_SUM = T.RETAIL_SUM,
	PERCENTAGE = T.CONTRACTOR_SUM_VAT * 100 / @TOTAL_CONTRACTOR_SUM_VAT
FROM #TEMP_T T
ORDER BY T.CONTRACTOR_SUM_VAT * 100 / @TOTAL_CONTRACTOR_SUM_VAT DESC

IF (@DETAIL <> 1)
BEGIN

SELECT
	CONTRACTOR_NAME = C.NAME,
	CONTRACTOR_SUM_VAT = SUM(DM.SUM_SUP),
	RETAIL_SUM = SUM(DM.SUM_ACC),
	SUPPLIER_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_SUP ELSE 0 END),
	SUPPLIER_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_SUP ELSE 0 END),
	RETAIL_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_ACC ELSE 0 END),
	RETAIL_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_ACC ELSE 0 END)
FROM DOC_MOVEMENT DM
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
WHERE ((@SHOW_REMAIN <> 1 AND DM.ID_TABLE = 2) OR (@SHOW_REMAIN = 1 AND DM.ID_TABLE IN (2,30)))
	AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_STORES = 1 OR DM.ID_STORE IN (SELECT ID_STORE FROM #STORES))
GROUP BY C.NAME

END
ELSE
BEGIN

IF (@SHOW_REMAIN = 1)
BEGIN

SELECT
	CONTRACTOR_NAME = MAX(C.NAME),
	MNEMOCODE = I.MNEMOCODE,
	[DATE] = DM.DATE_OP,
	INCOMING_NUMBER = I.INCOMING_NUMBER,
	INCOMING_DATE = I.INCOMING_DATE,
	CONTRACTOR_SUM_VAT = SUM(DM.SUM_SUP),
	RETAIL_SUM = SUM(DM.SUM_ACC),
	SUPPLIER_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_SUP ELSE 0 END),
	SUPPLIER_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_SUP ELSE 0 END),
	RETAIL_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_ACC ELSE 0 END),
	RETAIL_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_ACC ELSE 0 END)
FROM DOC_MOVEMENT DM
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
	INNER JOIN INVOICE I ON I.ID_INVOICE_GLOBAL = DM.ID_DOCUMENT
WHERE DM.ID_TABLE = 2
	AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_STORES = 1 OR I.ID_STORE IN (SELECT ID_STORE FROM #STORES))
GROUP BY DM.ID_DOCUMENT, I.MNEMOCODE, DM.DATE_OP, I.INCOMING_NUMBER, I.INCOMING_DATE

UNION ALL

SELECT
	CONTRACTOR_NAME = MAX(C.NAME),
	MNEMOCODE = I.MNEMOCODE,
	[DATE] = i.document_date,
	INCOMING_NUMBER = NULL,
	INCOMING_DATE = NULL,
	CONTRACTOR_SUM_VAT = SUM(ii.supplier_sum_vat),
	RETAIL_SUM = SUM(ii.retail_sum_vat),
	SUPPLIER_VAT_RATE_10 = SUM(CASE WHEN ii.supplier_vat = 10 THEN ii.supplier_vat_sum ELSE 0 END),
	SUPPLIER_VAT_RATE_18 = SUM(CASE WHEN ii.supplier_vat = 18 THEN ii.supplier_vat_sum ELSE 0 END),
	RETAIL_VAT_RATE_10 = SUM(CASE WHEN ii.retail_vat = 10 THEN ii.retail_vat_sum ELSE 0 END),
	RETAIL_VAT_RATE_18 = SUM(CASE WHEN ii.retail_vat = 18 THEN ii.retail_vat_sum ELSE 0 END)	
FROM IMPORT_REMAINS I
	INNER JOIN IMPORT_REMAINS_ITEM II ON II.ID_IMPORT_REMAINS_GLOBAL = I.id_import_remains_global
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = ii.id_supplier	
	inner join doc_movement dm on dm.id_document = i.id_import_remains_global
WHERE i.document_date BETWEEN @DATE_FR AND @DATE_TO
	and i.document_state = 'PROC'
	AND (@ALL_STORES = 1 OR i.ID_STORE IN (SELECT ID_STORE FROM #STORES))
GROUP BY ii.id_supplier, I.MNEMOCODE, i.document_date
ORDER BY CONTRACTOR_NAME

END
ELSE
BEGIN

SELECT
	CONTRACTOR_NAME = MAX(C.NAME),
	MNEMOCODE = I.MNEMOCODE,
	[DATE] = DM.DATE_OP,
	INCOMING_NUMBER = I.INCOMING_NUMBER,
	INCOMING_DATE = I.INCOMING_DATE,
	CONTRACTOR_SUM_VAT = SUM(DM.SUM_SUP),
	RETAIL_SUM = SUM(DM.SUM_ACC),
	SUPPLIER_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_SUP ELSE 0 END),
	SUPPLIER_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_SUP ELSE 0 END),
	RETAIL_VAT_RATE_10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN DM.SVAT_ACC ELSE 0 END),
	RETAIL_VAT_RATE_18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN DM.SVAT_ACC ELSE 0 END)
FROM DOC_MOVEMENT DM
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
	INNER JOIN INVOICE I ON I.ID_INVOICE_GLOBAL = DM.ID_DOCUMENT
WHERE DM.ID_TABLE = 2
	AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_STORES = 1 OR I.ID_STORE IN (SELECT ID_STORE FROM #STORES))
GROUP BY DM.ID_DOCUMENT, I.MNEMOCODE, DM.DATE_OP, I.INCOMING_NUMBER, I.INCOMING_DATE

END

END

SELECT COMPANY = CASE WHEN ISNULL(FULL_NAME, '') = '' THEN [NAME] ELSE FULL_NAME END
FROM CONTRACTOR
WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()

RETURN 0
GO

/*
EXEC REPEX_INVOICE_BOOK '
<XML>
	<DATE_FR>2009-07-28</DATE_FR>
	<DATE_TO>2009-07-28</DATE_TO>
	<DETAIL>1</DETAIL>
	<SHOW_REMAIN>1</SHOW_REMAIN>
</XML>'*/
