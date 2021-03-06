SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_OUT_INVOICE') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_OUT_INVOICE AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_OUT_INVOICE
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	[CONTRACTOR_FROM_NAME] = CASE WHEN ISNULL(C_FR.FULL_NAME, '') != '' THEN C_FR.FULL_NAME + ', ' + C_FR.[NAME] ELSE C_FR.[NAME] END,
	[CONTRACTOR_FROM_ADDRESS] = C_FR.ADDRESS,
	[CONTRACTOR_FROM_INN] = C_FR.INN + ISNULL('/' + C_FR.KPP, ''),

	[GCONTRACTOR] = CASE WHEN ISNULL(C_FR.FULL_NAME, '') != '' THEN C_FR.FULL_NAME ELSE C_FR.[NAME] END + ', �����: ' + C_FR.ADDRESS + ', ����������� �������������: ' + S.NAME,
	[CONTRACTOR_TO_NAME] = CASE WHEN ISNULL(C_P.FULL_NAME, '') != '' THEN C_P.FULL_NAME ELSE C_P.[NAME] END,
	[CONTRACTOR_TO_ADDRESS] = C_P.ADDRESS,
	[CONTRACTOR_TO_INN] = C_P.INN + ISNULL('/' + C_P.KPP, ''),

	[GCONTRACTOR_TO_NAME] = CASE WHEN ISNULL(C_TO.FULL_NAME, '') != '' THEN C_TO.FULL_NAME ELSE C_TO.[NAME] END,
	[GCONTRACTOR_TO_ADDRESS] = C_TO.ADDRESS,

	[INVOICE_OUT_NAME] = I.MNEMOCODE,
	[INVOICE_OUT_DATE] = I.DATE,
	[SUMM_WITHOUT_TAX] = I.SUM_SAL - I.SVAT_SAL,
	[SUMM_TAX] = I.SVAT_SAL,
	[SUMM_WITH_TAX] = I.SUM_SAL
FROM INVOICE_OUT I
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR C_TO ON C_TO.ID_CONTRACTOR = I.ID_CONTRACTOR_TO
	INNER JOIN CONTRACTOR C_P ON C_P.ID_CONTRACTOR = I.ID_PAYER
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

SELECT
	[GOODS_NAME] = G.NAME,
	[UNIT_NAME] = U.SHORT_NAME,
	[UNIT_OKEI_CODE] = U.OKEI_CODE,
	[QUANTITY] = II.QUANTITY,
	[PRICE_SAL] = II.PRICE_SAL - II.PVAT_SAL,
	[SUM_SAL_WITHOUT_VAT] = II.SUM_SAL - II.PSUM_SAL,
	[VAT_SAL] = CASE WHEN II.PSUM_SAL = 0 THEN CAST('��� ���' AS VARCHAR(10)) ELSE CAST(L.VAT_SAL AS VARCHAR(9)) + '%' END,
	[PSUM_SAL] = II.PSUM_SAL,
	[SUM_SAL] = II.SUM_SAL,
	[COUNTRY] = C.NAME,
	[COUNTRY_SHORT] = CASE WHEN C.NAME = '������' THEN NULL ELSE C.MNEMOCODE END ,
	[COUNTRY_CODE] = CASE WHEN C.NAME = '������' THEN NULL ELSE C.COD_CODE END,
	GTD_NUMBER = INV_I.GTD_NUMBER
FROM INVOICE_OUT_ITEM II
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	LEFT JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	LEFT JOIN INVOICE_ITEM INV_I ON L.ID_DOCUMENT_ITEM = INV_I.ID_INVOICE_ITEM_GLOBAL
WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

--exec REPEX_INVOICE_OUT_INVOICE '<XML><ID_GLOBAL>CC8E3657-B14C-4668-9ADF-4E706E0AB807</ID_GLOBAL></XML>'