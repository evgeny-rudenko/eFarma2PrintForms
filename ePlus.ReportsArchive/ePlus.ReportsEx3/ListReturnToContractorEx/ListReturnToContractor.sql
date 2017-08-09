SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.FN_CERT_LIST_4_REPEX_LIST_RETURN_TO_CONTRACTOR') IS NULL) EXEC ('CREATE FUNCTION DBO.FN_CERT_LIST_4_REPEX_LIST_RETURN_TO_CONTRACTOR() RETURNS VARCHAR(4000) AS BEGIN RETURN CONVERT(VARCHAR(4000), NULL) END')
GO
ALTER FUNCTION DBO.FN_CERT_LIST_4_REPEX_LIST_RETURN_TO_CONTRACTOR(
    @ID_SERIES BIGINT
)
RETURNS VARCHAR(4000)
AS
BEGIN
    DECLARE @RESULT VARCHAR(4000)
    SELECT 
        @RESULT = ISNULL(@RESULT + '; ' + CONVERT(NVARCHAR(12), C.CERT_DATE, 104) + ' ����� ' + C.ISSUED_BY, ISNULL(CONVERT(NVARCHAR(12), C.CERT_DATE, 104) + ' ����� ' + C.ISSUED_BY, ''))
    FROM CERTIFICATE C
    WHERE ID_SERIES = @ID_SERIES
    RETURN @RESULT
END
GO

IF OBJECT_ID('DBO.REPEX_LIST_RETURN_TO_CONTRACTOR') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_LIST_RETURN_TO_CONTRACTOR AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_LIST_RETURN_TO_CONTRACTOR
    @XMLPARAM NTEXT
AS

DECLARE @HDOC INT, @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
    FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	GOODS = G.NAME + ' [' + P.NAME + ', ' + COALESCE(C.NAME, '') + ']',
	GOODS_FEDERAL_CODE = G.FEDERAL_CODE,
	UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
	OKEI_CODE = U.OKEI_CODE,
	QUANTITY = AI.QUANTITY,
	CONTRACTOR_PRICE_PER_UNIT = AI.PRICE,
	SUM_CONTRACTOR_PRICE = AI.PRICE * AI.QUANTITY,
	VAT_RATE = CAST(ROUND(100 * ((AI.PRICE_VAT - AI.PRICE) / AI.PRICE), 0) AS DECIMAL(18, 2)),
	SUM_VAT = (AI.PRICE_VAT - AI.PRICE) * AI.QUANTITY,
	SUM_CONTRACTOR_PRICE_VAT = AI.PRICE_VAT * AI.QUANTITY,
    CERTIFICATION_INFO = DBO.FN_CERT_LIST_4_REPEX_LIST_RETURN_TO_CONTRACTOR(S.ID_SERIES),
    S.BEST_BEFORE,
    S.SERIES_NUMBER
FROM ACT_RETURN_TO_CONTRACTOR A
	INNER JOIN ACT_RETURN_TO_CONTRACTOR_ITEM AI ON AI.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = A.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = AI.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT OUTER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
    LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
WHERE A.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = @ID_GLOBAL
ORDER BY GOODS

SELECT TOP 1
	INVOICE_NUMBER = A.MNEMOCODE,
	INVICE_DATE = A.DATE,
	SUPPLIER_NAME = COALESCE(NULLIF(RTRIM(LTRIM(SUP.FULL_NAME)),''), SUP.[NAME], ''),
	SUPPLIER_OKPO = SUP.OKPO,
	SUPPLIER_DESCRIPT =
		CASE WHEN ISNULL(SUP.FULL_NAME, '') != '' THEN SUP.FULL_NAME ELSE SUP.[NAME] END +
		COALESCE(' ���:' + SUP.INN, '') +
		COALESCE(' �����:' + SUP.ADDRESS, '') +
		COALESCE(' �������:' + SUP.PHONE, '') +
		COALESCE(' ���:' + SUP.KPP, '') +
		COALESCE(' ����:' + SUP.BANK + ' ' + SUP.BANK_ADDRESS , ' ����:' + SUP.BANK, ' ����:' + SUP.BANK_ADDRESS, '') +
		COALESCE(' �/c:' + SUP.ACCOUNT, '') +
		COALESCE(' �/�:' + SUP.CORR_ACCOUNT, '') +
		COALESCE(' ���:' + SUP.BIK, ''),
	CUSTOMER_NAME = COALESCE(NULLIF(RTRIM(LTRIM(CONT.FULL_NAME)),''), CONT.[NAME], ''),
	CUSTOMER_OKPO = CONT.OKPO,
	CUSTOMER_DESCRIPT = 
		CASE WHEN ISNULL(CONT.FULL_NAME, '') != '' THEN CONT.FULL_NAME ELSE CONT.NAME END +
		COALESCE(' ���:' + CONT.INN, '') +
		COALESCE(' �����:' + CONT.ADDRESS, '') +
		COALESCE(' �������:' + CONT.PHONE, '') +
		COALESCE(' ���:' + CONT.KPP, '') +
		COALESCE(' ����:' + CONT.BANK + ' ' + CONT.BANK_ADDRESS, ' ����:' + CONT.BANK, ' ����:' + CONT.BANK_ADDRESS, '') +
		COALESCE(' �/c:' + CONT.ACCOUNT, '') +
		COALESCE(' �/�:' + CONT.CORR_ACCOUNT, '') +
		COALESCE(' ���:' + CONT.BIK, '') , 
	PAYER_NAME = COALESCE(NULLIF(LTRIM(RTRIM(PAY.FULL_NAME)),''), PAY.[NAME], ''),
	STORE_NAME = S.NAME,
	INCOMING_NUMBER =
		COALESCE(I.INCOMING_NUMBER, I.MNEMOCODE, '') +
		COALESCE(' �� ' + CONVERT(VARCHAR, I.INCOMING_DATE, 104) , ' �� ' + CONVERT(VARCHAR, I.DOCUMENT_DATE, 104), '')
FROM ACT_RETURN_TO_CONTRACTOR A
	INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE
	INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR CONT ON CONT.ID_CONTRACTOR = A.ID_CONTRACTOR_TO
	LEFT JOIN CONTRACTOR PAY ON PAY.ID_CONTRACTOR = A.ID_PAYER
	LEFT JOIN INVOICE I ON I.ID_INVOICE = A.ID_DOCUMENT_BASE
WHERE A.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = @ID_GLOBAL

RETURN 0
GO

--exec REPEX_LIST_RETURN_TO_CONTRACTOR '<XML><ID_GLOBAL>3AC5A92A-C95C-4D45-9396-BAAA2B7A8753</ID_GLOBAL></XML>'