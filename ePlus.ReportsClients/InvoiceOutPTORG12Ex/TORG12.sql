SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INV3') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INV3 AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INV3 @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	CONTRACTOR = '������� �������� ��������� ��� ��������� �������������������� ',
	STORE = '����� 23 ���� ������ ����  ����� 5544 23 ��� ����� ��������� ���� �������',
	DOC_NUM = '324234-3242-3',
	DOC_DATE = GETDATE()
	
SELECT TOP 100
	GOODS_NAME = G.NAME,
	GOODS_CODE = '11',
	OKEI_CODE = '333',
	UNIT_NAME = '3343',
	RETAIL_PRICE = QUANTITY_MIN,
	QUANTITY = QUANTITY_MIN
FROM GOODS G

RETURN 0
GO

--exec REPEX_INV3 '<XML><ID_GLOBAL>5A7081AC-015A-478C-8DAA-AFA9BCCE7EE9</ID_GLOBAL></XML>'