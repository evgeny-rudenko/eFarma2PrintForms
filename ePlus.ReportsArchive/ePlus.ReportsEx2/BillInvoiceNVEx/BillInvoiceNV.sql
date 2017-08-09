SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_BILL_INVOICE_NV') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_BILL_INVOICE_NV AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_BILL_INVOICE_NV
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT TOP 1 
	@ID_GLOBAL = ID_GLOBAL
FROM OPENXML (@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	[CONTRACTOR_FROM_NAME] = CASE WHEN ISNULL(C_FR.FULL_NAME, '') != '' THEN C_FR.FULL_NAME + ', ' + C_FR.[NAME] ELSE C_FR.[NAME] END,
	[CONTRACTOR_FROM_ADDRESS] = C_FR.ADDRESS,
	[CONTRACTOR_FROM_INN] = C_FR.INN + ISNULL('/' + C_FR.KPP, ''),

	[GCONTRACTOR] = CASE WHEN ISNULL(C_FR.FULL_NAME, '') != '' THEN C_FR.FULL_NAME ELSE C_FR.[NAME] END + ', �����: ' + C_FR.ADDRESS + ', ����������� �������������: ' + S.NAME,
	[CONTRACTOR_TO_NAME] = CASE WHEN ISNULL(C_TO.FULL_NAME, '') != '' THEN C_TO.FULL_NAME ELSE C_TO.[NAME] END,
	[CONTRACTOR_TO_ADDRESS] = C_TO.ADDRESS,
	[CONTRACTOR_TO_INN] = C_TO.INN + ISNULL('/' + C_TO.KPP, ''),

	[GCONTRACTOR_TO_NAME] = CASE WHEN ISNULL(C_TO.FULL_NAME, '') != '' THEN C_TO.FULL_NAME ELSE C_TO.[NAME] END,
	[GCONTRACTOR_TO_ADDRESS] = C_TO.ADDRESS,

	[INVOICE_OUT_NAME] = B.DOC_NUM,
	[INVOICE_OUT_DATE] = B.DOC_DATE
FROM BILL B
	INNER JOIN STORE S ON S.ID_STORE = B.ID_STORE
	INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR C_TO ON C_TO.ID_CONTRACTOR = B.ID_CONTRACTOR
WHERE B.ID_BILL_GLOBAL = @ID_GLOBAL

SELECT
	[GOODS_NAME] = G.NAME,
	[UNIT_NAME] = U.SHORT_NAME,
	[UNIT_OKEI_CODE] = U.OKEI_CODE,
	[QUANTITY] = BI.QUANTITY,
	[PRICE_SAL] = BI.PRICE_SAL - BI.PVAT_SAL,
	[SUM_SAL_WITHOUT_VAT] = (BI.PRICE_SAL - BI.PVAT_SAL) * BI.QUANTITY,
	[VAT_SAL] = CASE WHEN BI.PVAT_SAL = 0 THEN CAST('��� ���' AS VARCHAR(10)) ELSE CAST(L.VAT_SAL AS VARCHAR(9)) + '%' END,
	[PSUM_SAL] = BI.PVAT_SAL * BI.QUANTITY,
	[SUM_SAL] = BI.PRICE_SAL * BI.QUANTITY,
	[COUNTRY] = C.NAME,
	[COUNTRY_SHORT] = CASE WHEN C.NAME = '������' THEN NULL ELSE C.MNEMOCODE END ,
	[COUNTRY_CODE] = CASE WHEN C.NAME = '������' THEN NULL ELSE C.COD_CODE END,
	GTD_NUMBER = II.GTD_NUMBER
FROM BILL_ITEM BI
	INNER JOIN GOODS G ON G.ID_GOODS = BI.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	INNER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
	LEFT JOIN LOT L ON L.ID_LOT = BI.ID_LOT	
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
	LEFT JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	LEFT JOIN INVOICE_ITEM II ON L.ID_DOCUMENT_ITEM = II.ID_INVOICE_ITEM_GLOBAL
WHERE BI.ID_BILL_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

--exec REPEX_BILL_INVOICE_NV '<XML><ID_GLOBAL>0BD61555-6AF3-4F42-99EA-213824BD81DC</ID_GLOBAL></XML>'