SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.FN_CERT_LIST_4_REPEX_INVOICE_EKATERINBURG') IS NULL)	EXEC ('CREATE FUNCTION DBO.FN_CERT_LIST_4_REPEX_INVOICE_EKATERINBURG() RETURNS VARCHAR(4000) AS BEGIN RETURN CONVERT(VARCHAR(4000), NULL) END')
GO
ALTER FUNCTION DBO.FN_CERT_LIST_4_REPEX_INVOICE_EKATERINBURG(
    @ID_SERIES BIGINT
)
RETURNS VARCHAR(4000)
AS
BEGIN
    DECLARE @RESULT VARCHAR(4000)
    SELECT 
        @RESULT = ISNULL(@RESULT + ' ;' + C.CERT_NUMBER, ISNULL(C.CERT_NUMBER, ''))
    FROM CERTIFICATE C
    WHERE ID_SERIES = @ID_SERIES
    RETURN @RESULT
END
GO

IF OBJECT_ID('REPEX_INVOICE_EKATERINBURG') IS NULL EXEC('CREATE PROCEDURE REPEX_INVOICE_EKATERINBURG AS RETURN')
GO

ALTER PROCEDURE REPEX_INVOICE_EKATERINBURG
    @XMLPARAM NTEXT
AS

DECLARE @HDOC INT
DECLARE @ID_INVOICE BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INVOICE = ID_INVOICE
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INVOICE BIGINT 'ID_INVOICE')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT DISTINCT
	G.[NAME] + ' [' + P.NAME + ', ' + COALESCE(C.NAME, '') + ']' AS GOODS_NAME,
	II.QUANTITY	AS QUANTITY,
	II.SUPPLIER_PRICE AS CONTRACTOR_PRICE_PER_UNIT,
	II.SUPPLIER_PRICE_VAT AS CONTRACTOR_PRICE_PER_UNIT_VAT,
	II.SUPPLIER_SUM_VAT AS CONTRACTOR_SUM_VAT,
	II.SUPPLIER_VAT AS VAT_RATE,
	II.RETAIL_ADPRICE AS RETAIL_ADPRICE,  
	II.RETAIL_PRICE_VAT AS RETAIL_PRICE,
	II.RETAIL_SUM_VAT AS RETAIL_SUM,
	S.SERIES_NUMBER,
	S.BEST_BEFORE,
	RC.[NAME] AS REG_CERT_NAME,
	FED_CERT_NAME = DBO.FN_CERT_LIST_4_REPEX_INVOICE_EKATERINBURG(S.ID_SERIES),
	II.ID_INVOICE_ITEM
FROM INVOICE I
	INNER JOIN INVOICE_ITEM AS II ON II.ID_INVOICE = I.ID_INVOICE
	INNER JOIN GOODS AS G ON G.ID_GOODS = II.ID_GOODS
	INNER JOIN PRODUCER AS P ON G.ID_PRODUCER = P.ID_PRODUCER
	LEFT JOIN SERIES AS S ON S.ID_SERIES = II.ID_SERIES
	LEFT JOIN REG_CERT AS RC ON RC.ID_REG_CERT_GLOBAL = II.ID_REG_CERT_GLOBAL
	LEFT JOIN COUNTRY AS C ON P.ID_COUNTRY = C.ID_COUNTRY
WHERE I.ID_INVOICE = 4876
ORDER BY II.ID_INVOICE_ITEM
  
SELECT 
  -- ����� ��������� ���������
  INV.MNEMOCODE AS INVOICE_NUMBER,
  -- ���� ��������� ���������
  INV.DOCUMENT_DATE AS INVICE_DATE,
  -- �������� ��������
  COALESCE(INV.INCOMING_NUMBER, '')+
    COALESCE(' �� '+CONVERT(VARCHAR, INV.INCOMING_DATE, 104), '') 
    AS INCOMING_NUMBER,
  -- ����������
  CASE WHEN ISNULL(CNR_CUST.FULL_NAME,'') <> '' THEN CNR_CUST.FULL_NAME ELSE CNR_CUST.[NAME] END AS CUSTOMER_NAME,
  CASE WHEN ISNULL(CNR_CUST.ADDRESS,'') <> '' THEN ' �����:'+CNR_CUST.ADDRESS ELSE '' END AS CUSTOMER_ADDRESS,
  -- ��������� 
  CASE WHEN ISNULL(CNR_SUP.FULL_NAME,'') <> '' THEN CNR_SUP.FULL_NAME ELSE CNR_SUP.[NAME] END
  +CASE WHEN ISNULL(CNR_SUP.INN,'') <> '' THEN ' ���:'+CNR_SUP.INN ELSE '' END AS SUPPLIER_NAME,
  -- ����� 
  ST.[NAME] AS STORE_NAME,
-- ������� ������ (�� ������ ���� NULL ����� � ������ ��������)
  (SELECT COALESCE(CNT.[NAME], '') 
  FROM DBO.CONTRACTOR CNT
  WHERE CNT.ID_CONTRACTOR = ST.ID_CONTRACTOR) AS COMPANY

FROM DBO.INVOICE INV
  INNER JOIN DBO.CONTRACTOR CNR_SUP ON CNR_SUP.ID_CONTRACTOR = INV.ID_CONTRACTOR_SUPPLIER
  INNER JOIN DBO.STORE ST ON ST.ID_STORE = INV.ID_STORE 
  INNER JOIN DBO.CONTRACTOR CNR_CUST ON ST.ID_CONTRACTOR = CNR_CUST.ID_CONTRACTOR
WHERE (INV.ID_INVOICE = @ID_INVOICE)

--�������� �����
SELECT 
  --����� ���. ��� ���
  INV.SUM_SUPPLIER-INV.SVAT_SUPPLIER AS CONTRACTOR_SUM,
  --����� ���. � ���  
  INV.SUM_SUPPLIER AS CONTRACTOR_SUM_VAT,
  --����� ��� ���.
  INV.SVAT_SUPPLIER AS SUM_CONTRACTOR_VAT,
  --����� ���. ��� �� ��. 10%
  SUM(CASE WHEN ITEM.SUPPLIER_VAT = 10.0 THEN ITEM.SUPPLIER_VAT_SUM ELSE 0.0 END) AS SUM_CONTRACTOR_VAT_10,  --����� ���. ��� �� ��. 18%
  SUM(CASE WHEN ITEM.SUPPLIER_VAT = 18.0 THEN ITEM.SUPPLIER_VAT_SUM ELSE 0.0 END) AS SUM_CONTRACTOR_VAT_18,
  --�������� ��������� �����
  INV.SUM_RETAIL AS SUM_RETAIL_SUM,
  --�������� ��������� �����
  0.00 AS SUM_PROD_SUM,
  --����� ����. ��� �� ��. 10%
  SUM(CASE WHEN ITEM.RETAIL_VAT = 10.0 THEN ITEM.RETAIL_VAT_SUM ELSE 0.0 END) AS SUM_RETAIL_VAT_10,
  --����� ����. ��� �� ��. 18%
  SUM(CASE WHEN ITEM.RETAIL_VAT = 18.0 THEN ITEM.RETAIL_VAT_SUM ELSE 0.0 END) AS SUM_RETAIL_VAT_18,
  --����� ������ � ������
  0.00 AS SUM_PROD_TAX
FROM DBO.INVOICE INV
  INNER JOIN DBO.INVOICE_ITEM ITEM ON ITEM.ID_INVOICE = INV.ID_INVOICE
WHERE INV.ID_INVOICE = @ID_INVOICE
GROUP BY INV.SUM_SUPPLIER-INV.SVAT_SUPPLIER, INV.SUM_SUPPLIER,INV.SVAT_SUPPLIER,INV.SUM_RETAIL
RETURN 0
GO