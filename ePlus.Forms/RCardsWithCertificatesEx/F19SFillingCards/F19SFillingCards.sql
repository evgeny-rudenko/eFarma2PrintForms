IF (OBJECT_ID('DBO.REP_RACKCARDS_EX') IS NULL) EXEC('CREATE PROCEDURE DBO.REP_RACKCARDS_EX AS RETURN')
GO
ALTER PROCEDURE DBO.REP_RACKCARDS_EX
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT 
        ID_INVOICE
    INTO #INVOICE
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INVOICE UNIQUEIDENTIFIER 'ID_INVOICE')

EXEC SP_XML_REMOVEDOCUMENT @HDOC
DECLARE @CERTIFICATE TABLE(
    CERTIFICATE VARCHAR(8000),
    ID_INVOICE_ITEM_GLOBAL UNIQUEIDENTIFIER
)
INSERT INTO @CERTIFICATE(    
    CERTIFICATE,
    ID_INVOICE_ITEM_GLOBAL)
SELECT  
    CERTIFICATE = CASE WHEN C.CERT_NUMBER IS NOT NULL THEN C.CERT_NUMBER +', ' ELSE '' END +
                  CASE WHEN C.CERT_DATE IS NOT NULL THEN CONVERT(VARCHAR(10),C.CERT_DATE,104) +', ' ELSE '' END+ 
                  CASE WHEN C.ISSUED_BY IS NOT NULL THEN C.ISSUED_BY +', ' ELSE '' END+
                  CASE WHEN S.SERIES_NUMBER IS NOT NULL THEN S.SERIES_NUMBER+', ' ELSE '' END+
                  CASE WHEN C.CERT_END_DATE IS NOT NULL THEN CONVERT(VARCHAR(10),C.CERT_END_DATE,104) ELSE '' END,
    ID_INVOICE_ITEM_GLOBAL
FROM DBO.INVOICE INV
INNER JOIN DBO.INVOICE_ITEM II ON II.ID_INVOICE = INV.ID_INVOICE
LEFT JOIN SERIES S ON S.ID_SERIES = II.ID_SERIES
LEFT JOIN CERTIFICATE C ON C.ID_SERIES = S.ID_SERIES
WHERE EXISTS (SELECT NULL FROM #INVOICE WHERE #INVOICE.ID_INVOICE = INV.ID_INVOICE_GLOBAL)

DECLARE @IDS TABLE(ID_INVOICE_ITEM_GLOBAL UNIQUEIDENTIFIER)
DECLARE @ROWCOUNT INT
INSERT INTO @IDS
SELECT DISTINCT ID_INVOICE_ITEM_GLOBAL FROM @CERTIFICATE
SET @ROWCOUNT = @@ROWCOUNT

    DECLARE @ID_INVOICE_ITEM_GLOBAL UNIQUEIDENTIFIER
    DECLARE @G VARCHAR(8000)

    DECLARE @CERT TABLE(    
        CERTIFICATE VARCHAR(8000),
        ID_INVOICE_ITEM_GLOBAL UNIQUEIDENTIFIER)

WHILE(@ROWCOUNT>0)BEGIN
    SET @G=NULL
    SELECT TOP 1 @ID_INVOICE_ITEM_GLOBAL = ID_INVOICE_ITEM_GLOBAL FROM @IDS
    SELECT 
        @G = ISNULL(@G+'; '+C.CERTIFICATE, C.CERTIFICATE)
    FROM @CERTIFICATE C
    WHERE ID_INVOICE_ITEM_GLOBAL = @ID_INVOICE_ITEM_GLOBAL
          
    INSERT INTO @CERT(
        CERTIFICATE,
        ID_INVOICE_ITEM_GLOBAL)
    SELECT DISTINCT @G,@ID_INVOICE_ITEM_GLOBAL

    DELETE FROM @IDS WHERE ID_INVOICE_ITEM_GLOBAL = @ID_INVOICE_ITEM_GLOBAL
    SET @ROWCOUNT = @ROWCOUNT-1
END


SELECT
  ROW_NUM = IDENTITY(INT, 1, 1),
  CONTRACTOR = CNTR.[NAME],
  GOODS = G.[NAME],
  PRODUCER = PROD.[NAME],
  QUANTITY = II.QUANTITY,
  CONTRACTOR_PRICE_PER_UNIT_VAT = ROUND(II.SUPPLIER_PRICE_VAT, 2),
  CONTRACTOR_PRICE_PER_UNIT = ROUND(II.SUPPLIER_PRICE, 2),
  RETAIL_PRICE_VAT = ROUND(II.RETAIL_PRICE_VAT, 2),
  SUPPLIER = SUPL.[NAME],
  [DATE] = INV.DOCUMENT_DATE,
  NUMBER = INV.MNEMOCODE,
  INCOMING_NUMBER = INV.INCOMING_NUMBER + ' �� '+ CONVERT(VARCHAR, INV.INCOMING_DATE, 104),
  SERIES = SE.SERIES_NUMBER,
  BEST_BEFORE = SE.BEST_BEFORE,
  TAX = II.SUPPLIER_VAT,
  IMPORTANT = (CASE G.IMPORTANT WHEN 0 THEN '���' WHEN 1 THEN '��' END),
  REQUIRIED = (CASE G.REQUIRIED WHEN 0 THEN '���' WHEN 1 THEN '��' END),
  IN_DRUG = (CASE G.IN_DRUG WHEN 0 THEN '���' WHEN 1 THEN '��' END),
  MNEMOCODE = G.MNEMOCODE,
  CODE = II.BAR_CODE,
  CERT = CERT.CERTIFICATE,
  REG_CERTIFICATE = RC.[NAME] + ', '+RC.ORGAN+', '+CONVERT(VARCHAR(10),RC.[DATE],104),
  PRODUCER_PRICE = ISNULL(ii.producer_price,''),
  RETAIL_ADPRICE = ISNULL(ii.retail_adprice,'')
 INTO #R --select * 
FROM DBO.INVOICE INV
  INNER JOIN DBO.INVOICE_ITEM II ON II.ID_INVOICE_GLOBAL = INV.ID_INVOICE_GLOBAL
  INNER JOIN DBO.STORE ST ON ST.ID_STORE = INV.ID_STORE
  INNER JOIN DBO.CONTRACTOR CNTR ON CNTR.ID_CONTRACTOR = ST.ID_CONTRACTOR
  INNER JOIN DBO.GOODS G ON G.ID_GOODS = II.ID_GOODS
  INNER JOIN DBO.CONTRACTOR SUPL ON SUPL.ID_CONTRACTOR = INV.ID_CONTRACTOR_SUPPLIER
  LEFT JOIN DBO.PRODUCER PROD ON PROD.ID_PRODUCER = G.ID_PRODUCER
  LEFT JOIN DBO.COUNTRY CNR ON CNR.ID_COUNTRY = PROD.ID_COUNTRY
  LEFT JOIN DBO.SERIES SE ON II.ID_SERIES = SE.ID_SERIES
  LEFT JOIN REG_CERT RC ON RC.ID_REG_CERT_GLOBAL = II.ID_REG_CERT_GLOBAL
  LEFT JOIN @CERT CERT ON CERT.ID_INVOICE_ITEM_GLOBAL = II.ID_INVOICE_ITEM_GLOBAL
WHERE EXISTS (SELECT NULL FROM #INVOICE WHERE #INVOICE.ID_INVOICE = INV.ID_INVOICE_GLOBAL)
ORDER BY INV.DOCUMENT_DATE

SELECT
	GROUP_ = ceiling(ceiling(ROW_NUM/4.0)/2.0),
	*
FROM (SELECT * FROM #R R1 WHERE R1.ROW_NUM % 2 <> 0) R1 
LEFT JOIN (SELECT 
				ROW_NUM1 = ROW_NUM,
				CONTRACTOR1 = CONTRACTOR,
				GOODS1 = GOODS,
				PRODUCER1 = PRODUCER,
				QUANTITY1 = QUANTITY,
				CONTRACTOR_PRICE_PER_UNIT_VAT1 = CONTRACTOR_PRICE_PER_UNIT_VAT,
				CONTRACTOR_PRICE_PER_UNIT1 = CONTRACTOR_PRICE_PER_UNIT,
				RETAIL_PRICE_VAT1 = RETAIL_PRICE_VAT,
				SUPPLIER1 = SUPPLIER,
				DATE1 = [DATE],
				NUMBER1 = NUMBER,
				INCOMING_NUMBER1 = INCOMING_NUMBER,
				SERIES1 = SERIES,
				BEST_BEFORE1 = BEST_BEFORE,
				TAX1 = TAX,
				IMPORTANT1 = IMPORTANT,
				REQUIRIED1 = REQUIRIED,
				IN_DRUG1 = IN_DRUG,
				MNEMOCODE1 = MNEMOCODE,
				CODE1 = CODE,
				CERT1 = CERT,
				REG_CERTIFICATE1 = REG_CERTIFICATE,
				PRODUCER_PRICE1 = PRODUCER_PRICE,
				RETAIL_ADPRICE1 = RETAIL_ADPRICE
			FROM #R R2 WHERE R2.ROW_NUM % 2 = 0) R2 ON R1.ROW_NUM+1 = R2.ROW_NUM1

RETURN
GO

--exec REP_RACKCARDS_EX @XMLPARAM = N'<XML><ID_INVOICE>D14FE47D-4E72-4BFE-BEEE-EFBD98F9B1DE</ID_INVOICE></XML>'

-- select * from goods order by name
-- select * 
-- from series s 
-- inner join certificate c on c.id_series = s.id_series
-- where s.id_goods = 188906
