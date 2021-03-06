SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.FN_CERT_LIST_4_REPEX_INVOICE_OUT_TORG12') IS NULL)	EXEC ('CREATE FUNCTION DBO.FN_CERT_LIST_4_REPEX_INVOICE_OUT_TORG12() RETURNS VARCHAR(4000) AS BEGIN RETURN CONVERT(VARCHAR(4000), NULL) END')
GO
ALTER FUNCTION DBO.FN_CERT_LIST_4_REPEX_INVOICE_OUT_TORG12(
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

IF OBJECT_ID('DBO.REPEX_INVOICE_OUT_TORG12') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_OUT_TORG12 AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_OUT_TORG12(
    @XMLPARAM NTEXT
) AS

DECLARE @HDOC INT, @ID_INVOICE_OUT_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1 @ID_INVOICE_OUT_GLOBAL = ID_INVOICE_OUT_GLOBAL
    FROM OPENXML(@HDOC, '/XML') WITH(        
        ID_INVOICE_OUT_GLOBAL UNIQUEIDENTIFIER 'ID_INVOICE_OUT_GLOBAL'
    )
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	GOODS = G.NAME + ' [' + P.NAME + ', ' + COALESCE(C.NAME, '') + ']',
	GOODS_FEDERAL_CODE = G.FEDERAL_CODE,
	UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
	OKEI_CODE = U.OKEI_CODE,
	QUANTITY = II.QUANTITY,
	CONTRACTOR_PRICE_PER_UNIT = II.PRICE_SAL - II.PVAT_SAL,
	SUM_CONTRACTOR_PRICE = II.SUM_SAL - II.PSUM_SAL,
	VAT_RATE = CASE WHEN II.PSUM_SAL = 0 THEN CAST('��� ���' AS VARCHAR(10)) ELSE CAST(L.VAT_SAL AS VARCHAR(10)) END,
	SUM_VAT = II.PSUM_SAL,
	SUM_CONTRACTOR_PRICE_VAT = II.SUM_SAL,
    CERTIFICATION_INFO = DBO.FN_CERT_LIST_4_REPEX_INVOICE_OUT_TORG12(S.ID_SERIES),
    S.BEST_BEFORE,
    S.SERIES_NUMBER
FROM INVOICE_OUT I
	INNER JOIN INVOICE_OUT_ITEM II ON II.ID_INVOICE_OUT_GLOBAL = I.ID_INVOICE_OUT_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT OUTER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
	LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_INVOICE_OUT_GLOBAL
ORDER BY GOODS

SELECT TOP 1
	INVOICE_NUMBER = I.MNEMOCODE,
	INVICE_DATE = I.DATE,
	SUPPLIER_NAME = CASE WHEN ISNULL(SUP.FULL_NAME, '') != '' THEN SUP.FULL_NAME ELSE SUP.[NAME] END +
		COALESCE(' ���:' + SUP.INN, '') +
		COALESCE(' �����:' + SUP.ADDRESS, '') +
		COALESCE(' �������:' + SUP.PHONE, '') +
		COALESCE(' ���:' + SUP.KPP, '') +
		COALESCE(' ����:' + SUP.BANK + ' ' + SUP.BANK_ADDRESS , ' ����:' + SUP.BANK, ' ����:' + SUP.BANK_ADDRESS, '') +
		COALESCE(' �/c:' + SUP.ACCOUNT, '') +
		COALESCE(' �/�:' + SUP.CORR_ACCOUNT, '') +
		COALESCE(' ���:' + SUP.BIK, ''),
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
	PAYER_NAME = CASE WHEN ISNULL(PAY.FULL_NAME, '') != '' THEN PAY.FULL_NAME ELSE PAY.[NAME] END +
		COALESCE(' ���:' + PAY.INN, '') +
		COALESCE(' �����:' + PAY.ADDRESS, '') +
		COALESCE(' �������:' + PAY.PHONE, '') +
		COALESCE(' ���:' + PAY.KPP, '') +
		COALESCE(' ����:' + PAY.BANK + ' ' + PAY.BANK_ADDRESS , ' ����:' + PAY.BANK, ' ����:' + PAY.BANK_ADDRESS, '') +
		COALESCE(' �/c:' + PAY.ACCOUNT, '') +
		COALESCE(' �/�:' + PAY.CORR_ACCOUNT, '') +
		COALESCE(' ���:' + PAY.BIK, ''),
	STORE_NAME = S.NAME,
	INCOMING_NUMBER =
		COALESCE(INPUT.MNEMOCODE, M.MNEMOCODE, '') +
		COALESCE(' �� ' + CONVERT(VARCHAR, INPUT.DOCUMENT_DATE, 4), ' �� ' + CONVERT(VARCHAR, M.DATE, 4), '')
FROM INVOICE_OUT I
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR CONT ON CONT.ID_CONTRACTOR = I.ID_CONTRACTOR_TO
	LEFT JOIN CONTRACTOR PAY ON PAY.ID_CONTRACTOR = I.ID_PAYER
	LEFT JOIN INVOICE INPUT ON INPUT.ID_INVOICE_GLOBAL = I.ID_DOC_BASE_GLOBAL
	LEFT JOIN MOVEMENT M ON M.ID_MOVEMENT_GLOBAL = I.ID_DOC_BASE_GLOBAL
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_INVOICE_OUT_GLOBAL

RETURN 0
GO

--exec REPEX_INVOICE_OUT_TORG12 '<XML><ID_INVOICE_OUT_GLOBAL>8BED4149-1CA4-4064-8A7F-E2242D0812D0</ID_INVOICE_OUT_GLOBAL></XML>'