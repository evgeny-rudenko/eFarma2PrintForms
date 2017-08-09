SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_INVOICE_PROTOCOL_MOSCOW') IS NULL EXEC('CREATE PROCEDURE REPEX_INVOICE_PROTOCOL_MOSCOW AS RETURN')
GO
ALTER PROCEDURE REPEX_INVOICE_PROTOCOL_MOSCOW
	(@XMLPARAM NTEXT) AS 

DECLARE @HDOC INT
DECLARE @ID_INVOICE BIGINT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INVOICE = ID_INVOICE
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INVOICE BIGINT 'ID_INVOICE')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

-- ����� ������
SELECT  
  CC.FULL_NAME AS BUYER_NAME,  
  CS.FULL_NAME AS SUPPLIER_NAME
FROM DBO.INVOICE AS I
  INNER JOIN DBO.CONTRACTOR AS CS ON CS.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
  INNER JOIN DBO.STORE AS S ON S.ID_STORE = I.ID_STORE 
  INNER JOIN DBO.CONTRACTOR AS CC ON S.ID_CONTRACTOR = CC.ID_CONTRACTOR
WHERE I.ID_INVOICE = @ID_INVOICE

-- ������� ������
SELECT G.[NAME] + ', ' + CAST(SR.NUMERATOR AS VARCHAR(10))+ '/' + CAST(SR.DENOMINATOR AS VARCHAR(10)) AS TORG_NAME,
	S.SERIES_NUMBER,
	P.[NAME] AS PRODUCER_NAME,
	II.SUPPLIER_PRICE_VAT,
	II.SUPPLIER_ADPRICE,
	II.RETAIL_ADPRICE,
	II.RETAIL_PRICE_VAT
FROM DBO.INVOICE AS I 
	INNER JOIN DBO.INVOICE_ITEM AS II ON I.ID_INVOICE = II.ID_INVOICE
	INNER JOIN DBO.GOODS AS G ON G.ID_GOODS = II.ID_GOODS
	INNER JOIN DBO.SCALING_RATIO AS SR ON G.ID_GOODS = SR.ID_GOODS
	INNER JOIN DBO.PRODUCER AS P ON G.ID_PRODUCER = P.ID_PRODUCER
	LEFT JOIN DBO.SERIES AS S ON S.ID_SERIES = II.ID_SERIES
WHERE I.ID_INVOICE = @ID_INVOICE

RETURN 0
GO