SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REPEX_INVOICE_OUT_BILL') IS NULL) EXEC ('CREATE PROCEDURE REPEX_INVOICE_OUT_BILL AS RETURN')
GO
ALTER PROCEDURE REPEX_INVOICE_OUT_BILL
    @XMLPARAM NTEXT 
AS

DECLARE	@HDOC INT  
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT	TOP 1 @ID_GLOBAL = ID_GLOBAL 
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	BILL_NUMBER = '��' + RIGHT(I.MNEMOCODE, 9),
	BILL_DATE   = CONVERT(VARCHAR , I.[DATE], 104),
	SUPPLIER_INFO = ISNULL('��� ' + P.INN + ', ', '') + ISNULL('��� ' + P.KPP + ', ', '') + CASE WHEN ISNULL(P.[FULL_NAME], '') = '' THEN P.NAME ELSE P.FULL_NAME END + ' ' + ISNULL(P.BANK + ', ', '') + ISNULL(P.ADDRESS + ', ', '') + ISNULL('���.: ' + P.PHONE, ''),
	RECEPIENT_INFO = ISNULL('��� ' + R.INN + ', ', '') + ISNULL('��� ' + R.KPP + ', ', '') + CASE WHEN ISNULL(R.[FULL_NAME], '') = '' THEN R.NAME ELSE R.FULL_NAME END + ' ' + ISNULL(R.BANK + ', ', '') + ISNULL(R.ADDRESS + ', ', '') + ISNULL('���.: ' + R.PHONE, ''),
	COMPANY_NAME = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN CASE WHEN (S.LEGAL_PERS_SHORT IS NULL OR S.LEGAL_PERS_SHORT = '') THEN S.NAME ELSE S.LEGAL_PERS_SHORT END ELSE CASE WHEN (BC.LEGAL_PERS_SHORT IS NULL OR BC.LEGAL_PERS_SHORT = '') THEN BC.NAME ELSE BC.LEGAL_PERS_SHORT END END,
	COMPANY_ADDRESS = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.ADDRESS ELSE BC.ADDRESS END,
	COMPANY_PHONE = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.PHONE ELSE BC.PHONE END,
	COMPANY_INN = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.INN ELSE BC.INN END,
	COMPANY_KPP = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.KPP ELSE BC.KPP END,
	COMPANY_BANK = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.BANK ELSE BC.BANK END,
	COMPANY_BANK_ADDRESS = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.BANK_ADDRESS ELSE BC.BANK_ADDRESS END,
	COMPANY_ACCOUNT = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.ACCOUNT ELSE BC.ACCOUNT END,
	COMPANY_CORR_BANK = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.CORR_BANK ELSE BC.CORR_BANK END,
	COMPANY_BIK = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.BIK ELSE BC.BIK END,
	COMPANY_CORR_ACCOUNT = CASE WHEN B.ID_BILL_GLOBAL IS NULL THEN S.CORR_ACCOUNT ELSE BC.CORR_ACCOUNT END,
	DIRECTOR_FIO = S.DIRECTOR_FIO,
	BUH_FIO = S.BUH_FIO
FROM INVOICE_OUT I
	INNER JOIN STORE ST ON I.ID_STORE = ST.ID_STORE
	INNER JOIN CONTRACTOR S ON ST.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR R ON I.ID_CONTRACTOR_TO = R.ID_CONTRACTOR
	INNER JOIN CONTRACTOR P ON P.ID_CONTRACTOR = I.ID_PAYER
	LEFT JOIN BILL B ON B.ID_BILL_GLOBAL = I.ID_DOC_BASE_GLOBAL
	LEFT JOIN CONTRACTOR BC ON BC.ID_CONTRACTOR = B.ID_CONTRACTOR_PAY
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

SELECT
	GOODS_NAME = G.[NAME] + ', �������������: ' + P.[NAME] + '(' + C.NAME + '), ��� ' + CONVERT(VARCHAR(4), L.VAT_SAL) + '%',
	QUANTITY = II.QUANTITY,
	UNIT_NAME = U.SHORT_NAME,
	RETAIL_PRICEV = II.PRICE_SAL, 
	RETAIL_SUMV   = II.PRICE_SAL * (CASE WHEN II.QUANTITY <> 0 THEN II.QUANTITY ELSE 1 END),
	VAT = II.PVAT_SAL * II.QUANTITY,
    VAT_SUM10 = CASE WHEN L.VAT_SAL = 10 THEN II.PVAT_SAL * II.QUANTITY ELSE 0 END,
    VAT_SUM18 = CASE WHEN L.VAT_SAL = 18 THEN II.PVAT_SAL * II.QUANTITY ELSE 0 END
FROM INVOICE_OUT_ITEM II
	INNER JOIN LOT L ON II.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	INNER JOIN COUNTRY C ON P.ID_COUNTRY = C.ID_COUNTRY
WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

RETURN
GO

--exec REPEX_INVOICE_OUT_BILL '<XML><ID_GLOBAL>5836DD3A-1639-4EC8-BC75-23E68EE619F0</ID_GLOBAL></XML>'