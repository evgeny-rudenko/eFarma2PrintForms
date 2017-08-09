SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_Z_REPORT') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_Z_REPORT AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_Z_REPORT
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME,
	@DATE_TO DATETIME,
	@ALL_CONTRACTOR BIT,
	@ALL_CASH_REGISTER BIT,
	@DETAIL BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT TOP 1 
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@DETAIL = DETAIL
FROM OPENXML(@HDOC, '/XML') WITH
	(DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	DETAIL BIT 'DETAIL')

SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTOR = 1
SELECT * INTO #CASH_REGISTER FROM OPENXML(@HDOC, '//ID_CASH_REGISTER') WITH(ID_CASH_REGISTER BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CASH_REGISTER = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

--SELECT @DATE_FR, @DATE_TO, @ALL_CASH_REGISTER, @ALL_CONTRACTOR
--SELECT @DETAIL
SELECT
	--T1.ID_CASH_SESSION_GLOBAL,
	T1.CONTRACTOR_NAME, 
	T1.NAME_CASH_REGISTER, 
	T1.NUMBER_CASH_REGISTER,
 	T1.DATE_OPEN,
 	T1.DATE_CLOSE,
 	T1.SUM_PAYMENT,
 	T1.SUM_REQUISITIONING,
 	T1.SUM_SALES,
 	T1.SUM_SALES_RETURNS,
 	T1.CASH,
 	T1.SUM_DISCOUNT,
 	T2.NAL,
 	T2.NOTNAL
FROM
(
SELECT
	CS.ID_CASH_SESSION_GLOBAL,
	C.NAME AS CONTRACTOR_NAME, 
	CR.NAME_CASH_REGISTER, 
	CR.NUMBER_CASH_REGISTER,
 	CS.DATE_OPEN,
 	CS.DATE_CLOSE,
 	CS.SUM_PAYMENT,
 	CS.SUM_REQUISITIONING,
 	CS.SUM_SALES,
 	CS.SUM_SALES_RETURNS,
 	CS.CASH,
 	CS.SUM_DISCOUNT
FROM CASH_SESSION CS
	LEFT JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
	LEFT JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
WHERE
  	CS.DATE_OPEN >= @DATE_FR AND CS.DATE_CLOSE <= @DATE_TO
  	AND	(@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
  	AND	(@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
) AS T1
INNER JOIN
(
SELECT
	CH.ID_CASH_SESSION_GLOBAL,
	NAL = SUM(CASE WHEN ISNULL(CP.CARD_NUMBER, '') = '' THEN CP.SUMM ELSE 0 END),
	NOTNAL = SUM(CASE WHEN ISNULL(CP.CARD_NUMBER, '') != '' THEN CP.SUMM ELSE 0 END)
 FROM DBO.CHEQUE CH
	INNER JOIN DBO.CHEQUE_PAYMENT CP ON CH.ID_CHEQUE_GLOBAL = CP.ID_CHEQUE_GLOBAL
	INNER JOIN DBO.CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
	LEFT JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
	LEFT JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
WHERE CS.DATE_OPEN >= @DATE_FR AND CS.DATE_CLOSE <= @DATE_TO
  	AND	(@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
  	AND	(@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
GROUP BY CH.ID_CASH_SESSION_GLOBAL
) AS T2 ON T1.ID_CASH_SESSION_GLOBAL = T2.ID_CASH_SESSION_GLOBAL
ORDER BY T1.CONTRACTOR_NAME, T1.NUMBER_CASH_REGISTER, T1.DATE_CLOSE


IF (@DETAIL = 1)
BEGIN 
SELECT
	NUMBER_CASH_REGISTER = CR.NUMBER_CASH_REGISTER,
	NAME = '��� �������� �����: ' + CP.PAYMENT_TYPE_NAME,
	NOTNAL = SUM(CP.SUMM)
 FROM DBO.CHEQUE CH
	INNER JOIN DBO.CHEQUE_PAYMENT CP ON CH.ID_CHEQUE_GLOBAL = CP.ID_CHEQUE_GLOBAL
	INNER JOIN DBO.CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
	LEFT JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
	LEFT JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
WHERE CS.DATE_OPEN >= @DATE_FR AND CS.DATE_CLOSE <= @DATE_TO
	AND ISNULL(CP.CARD_NUMBER, '') != ''
	AND	(@ALL_CASH_REGISTER = 1 OR CS.ID_CASH_REGISTER IN (SELECT * FROM #CASH_REGISTER))
  	AND	(@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR))
GROUP BY CR.NUMBER_CASH_REGISTER, '��� �������� �����: ' + CP.PAYMENT_TYPE_NAME
END

RETURN 0
GO

/*
EXEC REPEX_Z_REPORT N'
<XML>
	<DATE_FR>2009-01-01T13:18:27.750</DATE_FR>
	<DATE_TO>2009-10-14T13:18:27.750</DATE_TO>
	<DETAIL>0</DETAIL>
</XML>'*/