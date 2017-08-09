IF (OBJECT_ID('USP_EXPORT_INVOICE_OUT_2_XML_REP') IS NULL) EXEC ('CREATE PROCEDURE USP_EXPORT_INVOICE_OUT_2_XML_REP AS RETURN')
GO

ALTER PROCEDURE USP_EXPORT_INVOICE_OUT_2_XML_REP(  
    @ID_GLOBAL UNIQUEIDENTIFIER  
)  
AS  
    DECLARE @DOC TABLE(  
        FID BIGINT NOT NULL PRIMARY KEY IDENTITY,  
        DOC_NAME VARCHAR(300),  
        ID_SUPPLIER BIGINT  
    )  
      
    DECLARE @HEADER TABLE(  
        SID BIGINT NOT NULL PRIMARY KEY IDENTITY,  
        FID BIGINT NOT NULL,  
        
        SUPPLIER_CODE bigint, 
        CONTRACTOR_CODE BIGINT,
        --SUPPLIER_NAME VARCHAR(300),  
        --CONTRACTOR_TO VARCHAR(300),  
        SVAT_SUPPLIER MONEY,  
        SUM_SUPPLIER MONEY,  
        SVAT_RETAIL MONEY,  
        SUM_RETAIL MONEY,  
        INCOMING_NUMBER VARCHAR(300),  
        INCOMING_DATE DATETIME,  
        INCOMING_BILL_NUMBER VARCHAR(300),  
        INCOMING_BILL_DATE DATETIME,  
        COMMENT VARCHAR(300)  
    )  

    DECLARE @ITEM TABLE(  
        TID BIGINT NOT NULL PRIMARY KEY IDENTITY,  
        SID BIGINT NOT NULL,  
--        FID BIGINT NOT NULL,  
  
        NUMERATOR INT,   
        DENOMINATOR INT,  
        UNIT_NAME VARCHAR(300),  
          
        GOODS_CODE VARCHAR(300),  
        GOODS VARCHAR(300),  
        PRODUCER VARCHAR(300),  
        COUNTRY VARCHAR(300),   
        IMPORTANT BIT,  
        REGISTER_PRICE MONEY,  
        REGISTRATION_DATE DATETIME,  
          
        QUANTITY MONEY,  
        PRODUCER_PRICE MONEY,  
          
        SUPPLIER_VAT_PER_UNIT MONEY,  
        SUPPLIER_ADPRICE MONEY,  
        SUPPLIER_PRICE MONEY,  
        SUPPLIER_VAT MONEY,  
        SUPPLIER_PRICE_VAT MONEY,  
        SUPPLIER_SUM MONEY,  
        SUPPLIER_VAT_SUM MONEY,  
        SUPPLIER_SUM_VAT MONEY,  
          
        RETAIL_ADPRICE MONEY,  
        RETAIL_PRICE MONEY,  
        RETAIL_VAT MONEY,  
        RETAIL_PRICE_VAT MONEY,  
        RETAIL_SUM MONEY,  
        RETAIL_VAT_SUM MONEY,  
        RETAIL_SUM_VAT MONEY,  
          
        SERIES_NUMBER VARCHAR(300),  
        BEST_BEFORE DATETIME,  
        GTD_NUMBER VARCHAR(300),  
        BAR_CODE VARCHAR(300),  
        BOX VARCHAR(25),
        ID_SERIES BIGINT  
    )  
  
    DECLARE @CERT TABLE(  
        TID BIGINT NOT NULL,  
--        SID BIGINT NOT NULL,  
--        FID BIGINT NOT NULL,  
  
        CERT_NUMBER VARCHAR(300),  
        CERT_ORGAN VARCHAR(300),  
        CERT_DATE DATETIME,   
        CERT_END_DATE DATETIME  
    )  
  
    INSERT INTO @DOC(  
        DOC_NAME,  
        ID_SUPPLIER  
    )  
    SELECT DISTINCT   
        I.MNEMOCODE+'_'+dbo.FN_DATE_2_VARCHAR(I.DATE),  
        C.ID_CONTRACTOR   
    FROM INVOICE_OUT I  
    inner join store s on s.id_store = i.id_store
--    INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = I.ID_INVOICE_OUT_GLOBAL  
--    INNER JOIN LOT L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL  
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = s.id_contractor--L.ID_SUPPLIER   
    WHERE ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL  

    INSERT INTO @HEADER(  
        FID,  
--        SID,  
--        SUPPLIER_NAME,  
--        CONTRACTOR_TO,  
        SUPPLIER_CODE,
        CONTRACTOR_CODE,

        SVAT_SUPPLIER,  
        SUM_SUPPLIER,  
        SVAT_RETAIL,  
        SUM_RETAIL,  
        INCOMING_NUMBER,  
        INCOMING_DATE,  
        INCOMING_BILL_NUMBER,  
        INCOMING_BILL_DATE,  
        COMMENT  
    )  
    SELECT  
        D.FID,  
        C.A_COD,
        PAY.A_COD,
--         C.NAME,  
--         PAY.NAME,  
  
        SVAT_SUPPLIER = SUM(L.PVAT_SUP*II.QUANTITY),  
        SUM_SUPPLIER = SUM(L.PRICE_SUP*II.QUANTITY),  
        SVAT_RETAIL = SUM(L.PVAT_SAL*II.QUANTITY),  
        SUM_RETAIL = SUM(L.PRICE_SAL*II.QUANTITY),  
        I.MNEMOCODE,  
        I.DATE,  
        I.DOC_NUM,  
        I.DOC_DATE,  
        I.COMMENT  
    FROM INVOICE_OUT I   
    INNER JOIN INVOICE_OUT_ITEM II ON II.ID_INVOICE_OUT_GLOBAL = I.ID_INVOICE_OUT_GLOBAL  
--    INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = II.ID_INVOICE_OUT_GLOBAL AND LM.ID_DOCUMENT_ITEM = II.ID_INVOICE_OUT_ITEM_GLOBAL  
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL  
    INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE  
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR--L.ID_SUPPLIER  
    INNER JOIN CONTRACTOR PAY ON PAY.ID_CONTRACTOR = I.ID_PAYER  
    INNER JOIN @DOC D ON D.ID_SUPPLIER = C.ID_CONTRACTOR--L.ID_SUPPLIER  
    WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL  
    GROUP BY D.FID,C.A_COD,PAY.A_COD,I.MNEMOCODE,I.DATE,I.COMMENT, I.doc_num, i.doc_date


    INSERT INTO @ITEM(  
        SID,  
--        FID,  
  
        NUMERATOR,   
        DENOMINATOR,  
        UNIT_NAME,  
          
        GOODS_CODE,  
        GOODS,  
        PRODUCER,  
        COUNTRY,   
        IMPORTANT,  
        REGISTER_PRICE,  
        REGISTRATION_DATE,  
          
        QUANTITY,  
        PRODUCER_PRICE,  
          
        SUPPLIER_VAT_PER_UNIT,  
        SUPPLIER_ADPRICE,  
        SUPPLIER_PRICE,  
        SUPPLIER_VAT,  
        SUPPLIER_PRICE_VAT,  
        SUPPLIER_SUM,  
        SUPPLIER_VAT_SUM,  
        SUPPLIER_SUM_VAT,  
          
        RETAIL_ADPRICE,  
        RETAIL_PRICE,  
        RETAIL_VAT,  
        RETAIL_PRICE_VAT,  
        RETAIL_SUM,  
        RETAIL_VAT_SUM,  
        RETAIL_SUM_VAT,  
          
        SERIES_NUMBER,  
        BEST_BEFORE,  
        GTD_NUMBER,  
        BAR_CODE,  
        BOX,
        ID_SERIES  
    )  
    SELECT  
        1, --D.FID,  
--        H.FID,  
  
        NUMERATOR = SR.NUMERATOR,   
        DENOMINATOR = SR.DENOMINATOR,  
        UNIT_NAME = U.SHORT_NAME,  
          
        GOODS_CODE = G.CODE,--(SELECT TOP 1 CODE FROM GOODS_CODE WHERE ID_CONTRACTOR = L.ID_SUPPLIER AND ID_GOODS = G.ID_GOODS),  
        GOODS = G.NAME,  
        PRODUCER = P.NAME,  
        COUNTRY = C.NAME,   
        IMPORTANT = CONVERT(BIT, G.IMPORTANT),  
        REGISTER_PRICE = G.REGISTER_PRICE,  
        REGISTRATION_DATE = G.REGISTRATION_DATE,  
          
        QUANTITY = II.QUANTITY,  
        PRODUCER_PRICE = L.PRICE_PROD,  
          
        SUPPLIER_VAT_PER_UNIT = L.PVAT_SUP,  
        SUPPLIER_ADPRICE = L.ADPRICE_SUP,  
        SUPPLIER_PRICE = L.PRICE_SUP - L.PVAT_SUP,  
        SUPPLIER_VAT = L.VAT_SUP,  
        SUPPLIER_PRICE_VAT = L.PRICE_SUP,  
        SUPPLIER_SUM = (L.PRICE_SUP * II.QUANTITY) - (L.PVAT_SUP * II.QUANTITY),  
        SUPPLIER_VAT_SUM = L.PVAT_SUP * II.QUANTITY,  
        SUPPLIER_SUM_VAT = L.PRICE_SUP * II.QUANTITY,  
          
        RETAIL_ADPRICE = L.ADPRICE_SAL,  
        RETAIL_PRICE = L.PRICE_SAL - L.PVAT_SAL,  
        RETAIL_VAT = VAT_SAL,  
        RETAIL_PRICE_VAT = L.PRICE_SAL,  
        RETAIL_SUM = (L.PRICE_SAL * II.QUANTITY) - (L.PVAT_SAL * II.QUANTITY),  
        RETAIL_VAT_SUM = L.PVAT_SAL * II.QUANTITY,  
        RETAIL_SUM_VAT = L.PRICE_SAL * II.QUANTITY,  
          
        S.SERIES_NUMBER,  
        S.BEST_BEFORE,  
        L.GTD_NUMBER,  
        L.INTERNAL_BARCODE,  
        L.BOX,
        S.ID_SERIES  
    FROM INVOICE_OUT_ITEM II  
--     INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = II.ID_INVOICE_OUT_GLOBAL   
--                               AND LM.ID_DOCUMENT_ITEM = II.ID_INVOICE_OUT_ITEM_GLOBAL  
--                               AND LM.QUANTITY_SUB>0 -- 1409 (экспорт накладной)  
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL--LM.ID_LOT_GLOBAL  
    INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS  
    INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER  
    INNER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY  
    INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO  
    INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT  
    LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES  
--    INNER JOIN @DOC D ON D.ID_SUPPLIER = L.ID_SUPPLIER  
--    INNER JOIN @HEADER H ON D.FID = H.FID  
    WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL  
 

    INSERT INTO @CERT(  
        TID,  
--         SID,  
--         FID,  
  
        CERT_NUMBER,  
        CERT_ORGAN,  
        CERT_DATE,  
        CERT_END_DATE  
    )  
    SELECT  
        I.TID,  
--         I.SID,  
--         I.FID,  
  
        CERT_NUMBER,  
        ISSUED_BY,  
        CERT_DATE,  
   CERT_END_DATE  
    FROM @ITEM I   
    INNER JOIN CERTIFICATE C ON C.ID_SERIES = I.ID_SERIES  
      
  
    SELECT   
        FID,  
        DOC_NAME  
    FROM @DOC  
  
    SELECT   
        FID,  
        SID,  
        SUPPLIER_CODE,
        CONTRACTOR_CODE,
--         SUPPLIER_NAME,  
--         CONTRACTOR_TO,
        SVAT_SUPPLIER,  
        SUM_SUPPLIER,  
        SVAT_RETAIL,  
        SUM_RETAIL,  
        INCOMING_NUMBER,  
        INCOMING_DATE,  
        INCOMING_BILL_NUMBER,  
        INCOMING_BILL_DATE,  
        COMMENT  
    FROM @HEADER          
  
    SELECT  
--        FID,  
        SID,  
        TID,  
  
        NUMERATOR,   
        DENOMINATOR,  
        UNIT_NAME,  
          
        GOODS_CODE,  
        GOODS,  
        PRODUCER,  
        COUNTRY,   
        IMPORTANT,  
        REGISTER_PRICE,  
        REGISTRATION_DATE,  
          
        QUANTITY,  
        PRODUCER_PRICE,  
          
        SUPPLIER_VAT_PER_UNIT,  
        SUPPLIER_ADPRICE,  
        SUPPLIER_PRICE,  
        SUPPLIER_VAT,  
        SUPPLIER_PRICE_VAT,  
        SUPPLIER_SUM,  
        SUPPLIER_VAT_SUM,  
        SUPPLIER_SUM_VAT,  
          
        RETAIL_ADPRICE,  
        RETAIL_PRICE,  
        RETAIL_VAT,  
        RETAIL_PRICE_VAT,  
        RETAIL_SUM,  
        RETAIL_VAT_SUM,  
        RETAIL_SUM_VAT,  
          
        SERIES_NUMBER,  
        BEST_BEFORE,  
        GTD_NUMBER,  
        BAR_CODE,
        BOX
    FROM @ITEM  
  
    SELECT  
--        FID,  
--        SID,  
        TID,  
        CERT_NUMBER,  
        CERT_ORGAN,  
        CERT_DATE,   
        CERT_END_DATE  
    FROM @CERT  
  
RETURN  
GO


-- exec USP_EXPORT_INVOICE_OUT_2_XML_REP @ID_GLOBAL='6C3D51D1-2B36-4967-A585-332141A4DDCC'
--  exec USP_EXPORT_INVOICE_OUT_2_XML_REP @ID_GLOBAL='752BFEB0-A0CF-47AA-AA2E-20F1091BE080'
-- 

