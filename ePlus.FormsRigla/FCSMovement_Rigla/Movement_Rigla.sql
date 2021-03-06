SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_MOVEMENT_RIGLA') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_MOVEMENT_RIGLA AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_MOVEMENT_RIGLA
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT top 10
	DOC_NUM = I.MNEMOCODE,
	DOC_DATE = I.DATE,
	CONTRACTOR_TO = ISNULL(CASE WHEN ISNULL(CT.FULL_NAME, '') = '' THEN CT.NAME ELSE CT.FULL_NAME END, ''),
	AUTH_VALID = ''
FROM  MOVEMENT I
LEFT JOIN CONTRACTOR CT ON I.ID_PAYER = CT.ID_CONTRACTOR
WHERE I.ID_MOVEMENT_GLOBAL = @ID_GLOBAL

SELECT
	GOODS_NAME = G.NAME,
	QUANTITY = II.QUANTITY * SR.NUMERATOR / CAST(SR.DENOMINATOR AS MONEY),
	PRICE_SAL = II.PRICE_SALE * SR.DENOMINATOR / CAST(SR.NUMERATOR AS MONEY),
	SUM_SAL = II.QUANTITY * II.PRICE_SALE
FROM MOVEMENT_ITEM II
INNER JOIN LOT L ON L.ID_LOT = II.ID_LOT_FROM
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
WHERE II.ID_MOVEMENT_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

SELECT
	SELF_NAME = CASE WHEN ISNULL(FULL_NAME, '') != '' THEN FULL_NAME ELSE [NAME] END +
		COALESCE(' ���:' + INN, '') +
		COALESCE(' �����:' + ADDRESS, '') +
		COALESCE(' �������:' + PHONE, '') +
		COALESCE(' ���:' + KPP, '') +
		COALESCE(' ����:' + BANK + ' ' + BANK_ADDRESS , ' ����:' + BANK, ' ����:' + BANK_ADDRESS, '') +
		COALESCE(' �/c:' + ACCOUNT, '') +
		COALESCE(' �/�:' + CORR_ACCOUNT, '') +
		COALESCE(' ���:' + BIK, ''),
	ADDRESS = CASE WHEN ISNULL(NAME, '') != '' THEN NAME ELSE FULL_NAME END,
	DIR = DIRECTOR_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

SELECT 
	C_NAME = CT.CONTRACT_NAME,
	C_DATE = CONVERT(VARCHAR(10), CR.START_DATE, 104),
	A_NUM = '',
	A_DATE = ''
FROM MOVEMENT I
INNER JOIN CONTRACTOR CT ON I.ID_PAYER = CT.ID_CONTRACTOR
LEFT JOIN CONTRACTS CR ON CR.ID_CONTRACTOR = I.ID_PAYER
WHERE I.ID_MOVEMENT_GLOBAL = @ID_GLOBAL

RETURN
GO

--exec DBO.REPEX_MOVEMENT_RIGLA '<XML><ID_GLOBAL>FDE96528-8A6D-4D50-B3F1-CA1DEC7F7BD3</ID_GLOBAL></XML>'