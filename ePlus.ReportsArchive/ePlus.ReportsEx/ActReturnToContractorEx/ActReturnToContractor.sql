SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_ACT_RETURN_TO_CONTRACTOR') IS NULL EXEC('CREATE PROCEDURE REPEX_ACT_RETURN_TO_CONTRACTOR AS RETURN')
GO
ALTER PROCEDURE REPEX_ACT_RETURN_TO_CONTRACTOR
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT ,	@ID_ACT_RETURN_TO_CONTRACTOR BIGINT , @ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL UNIQUEIDENTIFIER
		
EXEC	SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
		
	SELECT	TOP 1 @ID_ACT_RETURN_TO_CONTRACTOR = ID_ACT_RETURN_TO_CONTRACTOR , @ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL
	FROM	OPENXML(@HDOC , '/XML') WITH(ID_ACT_RETURN_TO_CONTRACTOR BIGINT 'ID_ACT_RETURN_TO_CONTRACTOR' , ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL UNIQUEIDENTIFIER 'ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL')
		
EXEC	SP_XML_REMOVEDOCUMENT @HDOC
		
SELECT	TOP 1 CO_ID = CO.ID_CONTRACTOR ,
	CO_NAME = CASE WHEN CO.FULL_NAME IS NULL OR CO.FULL_NAME = '' THEN CO.NAME ELSE CO.FULL_NAME END ,
	CO_FULLNAME = CO.FULL_NAME ,
	CO_INN = CO.INN ,
	CO_ADDRESS = CO.ADDRESS ,
	CO_PHONE = CO.PHONE ,
	CO_BANK = CO.BANK ,
	CO_BANKADDRESS = CO.BANK_ADDRESS ,
	CO_ACCOUNT = CO.ACCOUNT ,
	CO_CORRACCOUNT = CO.CORR_ACCOUNT ,
	CO_BIK = CO.BIK
FROM	CONTRACTOR CO(NOLOCK)
WHERE	CO.ID_CONTRACTOR = (SELECT TOP 1 AC.ID_CONTRACTOR_FROM FROM ACT_RETURN_TO_CONTRACTOR AC(NOLOCK) WHERE ID_ACT_RETURN_TO_CONTRACTOR = @ID_ACT_RETURN_TO_CONTRACTOR OR ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = @ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL)
		
SELECT	TOP 1 AC_ID = AC.ID_ACT_RETURN_TO_CONTRACTOR ,
	AC_NUMBER = AC.MNEMOCODE ,
	AC_DATE = CONVERT(VARCHAR , AC.DATE , 104) ,
	AC_STORE = (SELECT TOP 1 S.NAME FROM STORE S(NOLOCK) WHERE S.ID_STORE = AC.ID_STORE) ,
	AC_COMPANY = (SELECT TOP 1 CASE WHEN C.FULL_NAME IS NULL OR C.FULL_NAME = '' THEN C.NAME ELSE C.FULL_NAME END FROM CONTRACTOR C(NOLOCK) WHERE C.ID_CONTRACTOR = AC.ID_CONTRACTOR_TO) ,
	I_NUMBER = (SELECT TOP 1 COALESCE(I.INCOMING_NUMBER , I.MNEMOCODE , '') FROM INVOICE I(NOLOCK) WHERE I.ID_INVOICE = AC.ID_DOCUMENT_BASE) ,
	I_DATE = (SELECT TOP 1 COALESCE(CONVERT(VARCHAR , I.INCOMING_DATE , 104) , CONVERT(VARCHAR , I.DOCUMENT_DATE , 104) , '') FROM INVOICE I(NOLOCK) WHERE I.ID_INVOICE = AC.ID_DOCUMENT_BASE)
FROM	ACT_RETURN_TO_CONTRACTOR AC(NOLOCK)
WHERE	ID_ACT_RETURN_TO_CONTRACTOR = @ID_ACT_RETURN_TO_CONTRACTOR OR ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = @ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL
		
SELECT	G_ID = G.ID_GOODS ,
	G_MODELCODE = G.MNEMOCODE ,
	G_RUSNAME = G.NAME ,
	G_SERIALNUMBER = (SELECT TOP 1 SN.SERIES_NUMBER FROM SERIES SN(NOLOCK) WHERE SN.ID_SERIES = ISNULL(L.ID_SERIES , 0)) ,
	G_BESTBEFORE = (SELECT TOP 1 CONVERT(VARCHAR , SN.BEST_BEFORE , 104) FROM SERIES SN(NOLOCK) WHERE SN.ID_SERIES = ISNULL(L.ID_SERIES , 0)) ,
	G_NAMESCALING = dbo.FN_SCALE_NAME_SHORT(ACI.ID_SCALING_RATIO),
	ACI_QUANTITY = CAST(ACI.QUANTITY AS DECIMAL(18 , 2)) ,

	ACI_PRICESUPP = CAST(ACI.PRICE_VAT AS DECIMAL(18 , 2)) ,
	ACI_PVATSUPP = CAST(ACI.PRICE_VAT - ACI.PRICE AS DECIMAL(18 , 2)) ,
	ACI_SUMSUPP = CAST(0 AS DECIMAL(18 , 2)) ,
	ACI_PRICESAL = CAST(L.PRICE_SAL AS DECIMAL(18 , 2)) ,
	ACI_PVATSAL = CAST(L.PVAT_SAL AS DECIMAL(18 , 2)) ,
	ACI_SUMSAL = CAST(0 AS DECIMAL(18 , 2)) ,
	ACI_SUMDISCOUNT = CAST(0 AS DECIMAL(18 , 2)) ,
	ACI_TAXSALE = CAST(0 AS DECIMAL(18 , 2))
FROM GOODS G(NOLOCK) , LOT L(NOLOCK) , ACT_RETURN_TO_CONTRACTOR_ITEM ACI(NOLOCK)
WHERE G.ID_GOODS = ACI.ID_GOODS AND L.ID_LOT_GLOBAL = ACI.ID_LOT_GLOBAL AND (ACI.ID_ACT_RETURN_TO_CONTRACTOR = @ID_ACT_RETURN_TO_CONTRACTOR OR ACI.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = @ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL)
ORDER BY G_RUSNAME
		
RETURN 0
GO