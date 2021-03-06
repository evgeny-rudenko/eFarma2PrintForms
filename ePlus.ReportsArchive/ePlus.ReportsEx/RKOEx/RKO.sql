SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_RKO') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_RKO AS RETURN')
GO
ALTER PROCEDURE REPEX_RKO
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @ID_CASH_ORDER BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT @ID_CASH_ORDER = ID_CASH_ORDER
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_CASH_ORDER BIGINT 'ID_CASH_ORDER')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	SELF_NAME = CASE WHEN ISNULL(S.FULL_NAME, '') != '' THEN S.FULL_NAME ELSE S.[NAME] END,
	NUMBER = I.NUMBER,
	MNEMOCODE = I.MNEMOCODE,
	[DATE] = I.[DATE],
	[SUM] = CASE WHEN I.INCLUDE_VAT = 0 AND I.ID_DOCUMENT IS NULL THEN I.[SUM] + I.VAT_SUM ELSE I.[SUM] END,
	CLIENT_NAME = CASE WHEN ISNULL(C.FULL_NAME, '') != '' THEN C.FULL_NAME ELSE C.[NAME] END,
	BASE = I.BASE,
	SUPPLEMENT = I.SUPPLEMENT,
	SUB = ' ',
	PO = I.PO	
FROM CASH_ORDER I
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = I.ID_CONTRACTOR
	INNER JOIN CONTRACTOR S ON S.ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()
WHERE I.ID_CASH_ORDER = @ID_CASH_ORDER

DECLARE @ID UNIQUEIDENTIFIER
SET @ID = (SELECT ID_DOCUMENT FROM CASH_ORDER WHERE ID_CASH_ORDER = @ID_CASH_ORDER)
DECLARE @NDS VARCHAR(100)
DECLARE @ID_TAX_TYPE BIGINT
DECLARE @TAX_RATE MONEY
/*
IF (@ID IS NOT NULL)
BEGIN

SELECT
	SUM_VAT_10 = CASE WHEN AD.ID_TABLE = 2 THEN
						ISNULL((SELECT SUM(SUPPLIER_VAT_SUM) FROM INVOICE_ITEM WHERE ID_INVOICE_GLOBAL = I.ID_DOCUMENT AND SUPPLIER_VAT = 10), 0)
					WHEN AD.ID_TABLE = 8 THEN
						ISNULL((SELECT SUM(MI.QUANTITY * (MI.PRICE_SUPPLIER_VAT - MI.PRICE_SUPPLIER)) FROM MOVEMENT_ITEM MI INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = MI.ID_TAX_TYPE WHERE MI.ID_MOVEMENT_GLOBAL = I.ID_DOCUMENT AND TT.TAX_RATE = 10), 0)
				END,
						
	SUM_VAT_18 = CASE WHEN AD.ID_TABLE = 2 THEN
						ISNULL((SELECT SUM(SUPPLIER_VAT_SUM) FROM INVOICE_ITEM WHERE ID_INVOICE_GLOBAL = I.ID_DOCUMENT AND SUPPLIER_VAT = 18), 0)
					WHEN AD.ID_TABLE = 8 THEN
						ISNULL((SELECT SUM(MI.QUANTITY * (MI.PRICE_SUPPLIER_VAT - MI.PRICE_SUPPLIER)) FROM MOVEMENT_ITEM MI INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = MI.ID_TAX_TYPE WHERE MI.ID_MOVEMENT_GLOBAL = I.ID_DOCUMENT AND TT.TAX_RATE = 18), 0)
				END,
	SUM_NO = 0
FROM CASH_ORDER I
	LEFT JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = I.ID_DOCUMENT
WHERE I.ID_CASH_ORDER = @ID_CASH_ORDER

END
ELSE
BEGIN*/
	SET @ID_TAX_TYPE = (SELECT ID_TAX_TYPE FROM CASH_ORDER WHERE ID_CASH_ORDER = @ID_CASH_ORDER)
	SET @TAX_RATE = (SELECT TAX_RATE FROM TAX_TYPE WHERE ID_TAX_TYPE = @ID_TAX_TYPE)
	IF (@TAX_RATE IS NULL)
	BEGIN
		SELECT SUM_VAT_10 = 0, SUM_VAT_18 = 0, SUM_NO = ISNULL(VAT_SUM, 0) FROM CASH_ORDER WHERE ID_CASH_ORDER = @ID_CASH_ORDER
	END
	ELSE
	BEGIN
		IF (@TAX_RATE = 0)
			SELECT SUM_VAT_10 = 0, SUM_VAT_18 = 0, SUM_NO = 0
		ELSE IF (@TAX_RATE = 10)
			SELECT SUM_VAT_10 = VAT_SUM, SUM_VAT_18 = 0, SUM_NO = 0 FROM CASH_ORDER WHERE ID_CASH_ORDER = @ID_CASH_ORDER
		ELSE IF (@TAX_RATE = 18)
			SELECT SUM_VAT_10 = 0, SUM_VAT_18 = VAT_SUM, SUM_NO = 0 FROM CASH_ORDER WHERE ID_CASH_ORDER = @ID_CASH_ORDER
	END
--END

RETURN 0
GO

--exec REPEX_RKO '<XML><ID_CASH_ORDER>17</ID_CASH_ORDER></XML>'