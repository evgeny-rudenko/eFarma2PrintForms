SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_BOOK_SALES_SIMPLEX') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_BOOK_SALES_SIMPLEX AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_BOOK_SALES_SIMPLEX
@XMLPARAM NTEXT AS

DECLARE @HDOC INT

DECLARE @DATE_FROM DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ALL_CONTRACTORS BIT

DECLARE @CONTRACTORS TABLE(CONTRACTOR_NAME VARCHAR(128))

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM OUT
SELECT 
	@DATE_FROM = DATE_FROM,
	@DATE_TO = DATE_TO

FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FROM DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT ID_CONTRACTOR
INTO #CONTRACTORS FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTORS = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT

SELECT
	DATE_AND_NUM = T.DATE_AND_NUM,
	CONTRACTOR_NAME = T.CONTRACTOR_NAME,
	INN = T.INN,
	KPP = T.KPP,
	DATE_PAYMENT = T.DATE_PAYMENT,
	SUM_SAL = T.SUM_SAL,
	VAT_RATE_18 = T.VAT_RATE_18,
	SUM_VAT_RATE_18 = T.SUM_VAT_RATE_18,
	VAT_RATE_10 = T.VAT_RATE_10,
	SUM_VAT_RATE_10 = T.SUM_VAT_RATE_10,
	SUM_VAT_RATE_0 = T.SUM_VAT_RATE_0
FROM
(
SELECT
	DATE_AND_NUM = CONVERT(VARCHAR, I.DOC_DATE, 104) + CASE WHEN ISNULL(I.DOC_NUM, '') = '' THEN '' ELSE ' / ' + I.DOC_NUM END,
	DOC_NUMBER = I.DOC_NUM,
	DOC_DATE = I.DOC_DATE,
	CONTRACTOR_NAME = C.NAME,
	INN = C.INN,
	KPP = C.KPP,
	DATE_PAYMENT = I.DATE_PAYMENT,
	SUM_SAL = SUM(II.PRICE_SAL*II.QUANTITY),
	VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN II.PSUM_SAL ELSE 0 END),--L.PVAT_SAL * II.QUANTITY
	SUM_VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END),--(L.PRICE_SAL - L.PVAT_SAL) * II.QUANTITY
	VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN II.PSUM_SAL ELSE 0 END),
	SUM_VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END),
	SUM_VAT_RATE_0 = SUM(CASE WHEN L.VAT_SAL = 0 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END)
FROM INVOICE_OUT I
	INNER JOIN INVOICE_OUT_ITEM II ON II.ID_INVOICE_OUT_GLOBAL = I.ID_INVOICE_OUT_GLOBAL	
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR SELF ON SELF.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = I.ID_CONTRACTOR_TO 
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
WHERE I.STATE = 'PROC'
	AND (I.[DATE] BETWEEN @DATE_FROM AND @DATE_TO)
	AND (@ALL_CONTRACTORS = 1 OR SELF.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
GROUP BY I.MNEMOCODE,I.DOC_NUM,I.DOC_DATE,C.NAME,C.INN,C.KPP,I.DATE_PAYMENT

UNION ALL

SELECT
	DATE_AND_NUM = CONVERT(VARCHAR, CS.DATE_CLOSE, 104) + ' / ' + CS.MNEMOCODE,
	DOC_NUMBER = CS.MNEMOCODE,
	DOC_DATE = CS.DATE_CLOSE,
	CONTRACTOR_NAME = MAX(CASE WHEN CR.NUMBER_CASH_REGISTER IS NULL THEN '' ELSE '��� ' + CAST(CR.NUMBER_CASH_REGISTER AS VARCHAR) END),
	INN = NULL,
	KPP = NULL,
	DATE_PAYMENT = CS.DATE_CLOSE,
	SUM_SAL = SUM((CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * CHI.SUMM),
	VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN (CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * CHI.SUMM * L.VAT_SAL / (100 + L.VAT_SAL) ELSE 0 END),
	SUM_VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN (CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * (CHI.SUMM - CHI.SUMM * L.VAT_SAL / (100 + L.VAT_SAL)) ELSE 0 END),
	VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN (CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * CHI.SUMM * L.VAT_SAL / (100 + L.VAT_SAL) ELSE 0 END),
	SUM_VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN (CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * (CHI.SUMM - CHI.SUMM * L.VAT_SAL / (100 + L.VAT_SAL)) ELSE 0 END),
	SUM_VAT_RATE_0 = SUM(CASE WHEN L.VAT_SAL = 0 THEN (CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END) * CHI.SUMM ELSE 0 END)
FROM CASH_SESSION CS
	INNER JOIN CASH_REGISTER CR ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CHEQUE CH ON CH.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
WHERE CS.DATE_CLOSE BETWEEN @DATE_FROM AND @DATE_TO
	AND (@ALL_CONTRACTORS = 1 OR CR.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
GROUP BY CS.ID_CASH_SESSION_GLOBAL, CS.MNEMOCODE, CS.DATE_CLOSE, CONVERT(VARCHAR, CS.DATE_CLOSE, 104) + ' / ' + CS.MNEMOCODE
) AS T
ORDER BY DOC_DATE, DOC_NUMBER

INSERT INTO @CONTRACTORS
SELECT
	CONTRACTOR_NAME = CASE WHEN ISNULL(FULL_NAME, '') = '' THEN NAME ELSE FULL_NAME END
FROM CONTRACTOR 
WHERE ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS)

DECLARE @CONTRACTOR_STRING VARCHAR(4000)

SELECT 
	@CONTRACTOR_STRING = ISNULL(@CONTRACTOR_STRING+' ,'+C.CONTRACTOR_NAME, C.CONTRACTOR_NAME)
FROM @CONTRACTORS C
SELECT CONTRACTOR_NAME = @CONTRACTOR_STRING

SELECT
	DIRECTOR_FIO,
	BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN
GO

/*
exec REPEX_BOOK_SALES_SIMPLEX N'
<XML>
	<DATE_FROM>2009-01-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2009-08-18T00:00:00.000</DATE_TO>
	<ID_CONTRACTOR>5277</ID_CONTRACTOR>
</XML>'*/
