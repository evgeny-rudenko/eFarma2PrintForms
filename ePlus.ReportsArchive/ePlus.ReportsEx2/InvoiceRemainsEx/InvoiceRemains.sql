SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_REMAINS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_REMAINS AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_REMAINS
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @NO_DETAIL BIT
DECLARE @COUNT_INVOICE INT
DECLARE @COUNT_SUPPLIER INT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT
	@DATE_FR = DATE_FR, 
	@DATE_TO = DATE_TO, 
	@NO_DETAIL = NO_DETAIL
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR', 
	DATE_TO DATETIME 'DATE_TO', 
	NO_DETAIL BIT 'NO_DETAIL'
)


SELECT * INTO #INVOICE
FROM OPENXML(@HDOC, '//ID_INVOICE')
WITH(ID_INVOICE BIGINT '.')

SET @COUNT_INVOICE = @@ROWCOUNT

SELECT * INTO #SUPPLIER
FROM OPENXML(@HDOC, '//ID_SUPPLIER')
WITH(ID_SUPPLIER BIGINT '.')

SET @COUNT_SUPPLIER = @@ROWCOUNT

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

SELECT
    I.MNEMOCODE AS DOCUM,
    CONVERT(VARCHAR, MAX(I.DOCUMENT_DATE),104) AS DOC_DATE,
    I_I.ID_GOODS,
    G.[NAME] AS GOODS_NAME,
    PROD.[NAME] AS PRODUCER_NAME,
    C.ID_CONTRACTOR AS ID_SUPPLIER,
    C.[NAME] AS SUPPLIER_NAME,
    QUANTITY = SUM(I_I.QUANTITY),
    QUANTITY_REM = SUM(LOT.QUANTITY_REM),
    PRICE = ROUND(I_I.SUPPLIER_PRICE_VAT, 2),
    RETAIL_PRICE = ROUND(I_I.RETAIL_PRICE_VAT, 2),
    SUMM = SUM(I_I.SUPPLIER_SUM_VAT),
    SUMM_RETAIL = SUM(I_I.RETAIL_SUM_VAT)
FROM INVOICE I
	INNER JOIN INVOICE_ITEM I_I ON I.ID_INVOICE_GLOBAL = I_I.ID_INVOICE_GLOBAL
	INNER JOIN CONTRACTOR C ON I.ID_CONTRACTOR_SUPPLIER = C.ID_CONTRACTOR
	INNER JOIN LOT LOT ON I_I.ID_INVOICE_ITEM_GLOBAL = LOT.ID_DOCUMENT_ITEM AND I_I.ID_INVOICE_GLOBAL = LOT.ID_DOCUMENT
	INNER JOIN GOODS G ON I_I.ID_GOODS = G.ID_GOODS
	INNER JOIN DBO.PRODUCER PROD ON G.ID_PRODUCER = PROD.ID_PRODUCER
WHERE EXISTS (SELECT 
                  NULL
              FROM LOT_MOVEMENT LM 
              WHERE LM.ID_LOT_GLOBAL = LOT.ID_LOT_GLOBAL)
AND (
        (@COUNT_INVOICE=0
        AND (I.DOCUMENT_DATE BETWEEN @DATE_FR AND @DATE_TO)
        AND (@COUNT_SUPPLIER=0 OR I.ID_CONTRACTOR_SUPPLIER IN (SELECT ID_SUPPLIER FROM #SUPPLIER)))
        OR  
        (I.ID_INVOICE IN (SELECT ID_INVOICE FROM #INVOICE))
    )
GROUP BY I.MNEMOCODE, I_I.ID_GOODS, G.[NAME], PROD.[NAME], C.ID_CONTRACTOR, C.[NAME],
ROUND(I_I.SUPPLIER_PRICE_VAT, 2), ROUND(I_I.RETAIL_PRICE_VAT, 2)
ORDER BY C.[NAME], G.[NAME], I.MNEMOCODE

RETURN
GO

--exec DBO.REPEX_INVOICE_REMAINS '<XML><DATE_TO>2009-02-09T14:19:46.046</DATE_TO></XML>'
