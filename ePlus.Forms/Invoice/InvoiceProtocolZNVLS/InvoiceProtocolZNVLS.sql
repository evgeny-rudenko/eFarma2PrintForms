SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_PROTOCOL_ZNVLS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_PROTOCOL_ZNVLS AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_PROTOCOL_ZNVLS
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	FR_NAME = CASE WHEN ISNULL(C_FR.FULL_NAME, '') = '' THEN C_FR.NAME ELSE C_FR.FULL_NAME END,
	TO_NAME = CASE WHEN ISNULL(C_TO.FULL_NAME, '') = '' THEN C_TO.NAME ELSE C_TO.FULL_NAME END
FROM INVOICE I
	INNER JOIN STORE S ON I.ID_STORE = S.ID_STORE
	INNER JOIN CONTRACTOR C_TO ON C_TO.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
WHERE I.ID_INVOICE_GLOBAL = @ID_GLOBAL

SELECT
	GOODS_NAME = G.NAME,
	SERIES_NAME = S.SERIES_NUMBER,
	PRODUCER_NAME = P.NAME,
	REGISTER_PRICE = L.REGISTER_PRICE,
	--6
	--7
	PRICE_PROD = II.PRODUCER_PRICE,
	SUP_ADPRICE = II.SUPPLIER_ADPRICE,
	SUM_ADPRICE = II.SUPPLIER_PRICE * II.SUPPLIER_ADPRICE / (100 + II.SUPPLIER_ADPRICE),
	PRICE_SUP = II.SUPPLIER_PRICE
FROM INVOICE_ITEM II
	INNER JOIN GOODS G ON G.ID_GOODS = II.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	INNER JOIN LOT L ON L.ID_DOCUMENT_ITEM = II.ID_INVOICE_ITEM_GLOBAL
	LEFT JOIN SERIES S ON S.ID_SERIES = II.ID_SERIES	
WHERE II.ID_INVOICE_GLOBAL = @ID_GLOBAL AND G.IMPORTANT = 1
ORDER BY G.NAME

RETURN 0
GO

--exec DBO.REPEX_INVOICE_PROTOCOL_ZNVLS N'<XML><ID_GLOBAL>B2F5AA9D-A02C-4851-984F-179E1315DA58</ID_GLOBAL></XML>'