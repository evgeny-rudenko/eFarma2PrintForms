IF OBJECT_ID('REPEX_INVOICE_STORE_PLACE') IS NULL EXEC('CREATE PROCEDURE REPEX_INVOICE_STORE_PLACE AS RETURN')
GO
ALTER PROCEDURE REPEX_INVOICE_STORE_PLACE
    @XMLPARAM NTEXT
AS
/* PARAMETERS */
DECLARE @HDOC INT
DECLARE @ID_INVOICE BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INVOICE = ID_INVOICE
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INVOICE BIGINT 'ID_INVOICE')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

-- ������ ������� � ���������
SELECT DISTINCT
  --������������ ������
  G.[NAME] + ' [' + PROD.NAME + ', ' + COALESCE(CNR.NAME, '') + ']' AS GOODS_NAME,
  --������������ ��������
  --U.[SHORT_NAME] + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')' AS UNIT_NAME,
  --����������
  ITEM.QUANTITY	AS QUANTITY,
  --���� ������������� �� �������
  --ROUND(ITEM.PRODUCER_PRICE_PER_UNIT, 2) AS PRODUCER_PRICE_PER_UNIT,
  --���� ���������� �� ������� (��� ���)
  ITEM.SUPPLIER_PRICE AS CONTRACTOR_PRICE_PER_UNIT,
  --���� ���������� �� ������� (� ���)
  ITEM.SUPPLIER_PRICE_VAT AS CONTRACTOR_PRICE_PER_UNIT_VAT,
  --����� ���������� (� ���)
  ITEM.SUPPLIER_SUM_VAT AS CONTRACTOR_SUM_VAT,
  -- ������ ���
  ITEM.SUPPLIER_VAT AS VAT_RATE,
  --��������� ��������
  ITEM.RETAIL_ADPRICE AS RETAIL_ADPRICE,  
  --ROUND((ITEM.RETAIL_PRICE-ITEM.CONTRACTOR_PRICE_PER_UNIT_VAT)/ITEM.CONTRACTOR_PRICE_PER_UNIT_VAT*100,2) AS RETAIL_ADPRICE,
  --��������� ���� (� ���) 
  ITEM.RETAIL_PRICE_VAT AS RETAIL_PRICE,
  --��������� ����� (� ���) 
  ITEM.RETAIL_SUM_VAT AS RETAIL_SUM,
  ITEM.ID_INVOICE_ITEM,
  STP.NAME AS STORE_PLACE
FROM DBO.INVOICE INV
  INNER JOIN DBO.INVOICE_ITEM ITEM ON ITEM.ID_INVOICE = INV.ID_INVOICE
  INNER JOIN DBO.GOODS G ON G.ID_GOODS = ITEM.ID_GOODS
  INNER JOIN DBO.PRODUCER PROD ON G.ID_PRODUCER = PROD.ID_PRODUCER
  LEFT OUTER JOIN DBO.COUNTRY CNR ON PROD.ID_COUNTRY = CNR.ID_COUNTRY
  LEFT JOIN DBO.STORE_PLACE STP ON ITEM.ID_STORE_PLACE = STP.ID_STORE_PLACE
--  INNER JOIN DBO.SCALING_RATIO SR ON SR.ID_SCALING_RATIO = ITEM.ID_SCALING_RATIO
--  INNER JOIN DBO.UNIT U ON SR.ID_UNIT = U.ID_UNIT
WHERE (INV.ID_INVOICE = @ID_INVOICE)
ORDER BY GOODS_NAME
  
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
