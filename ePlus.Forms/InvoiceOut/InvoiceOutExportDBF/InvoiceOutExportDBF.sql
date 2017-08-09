
IF OBJECT_ID('DBO.USP_EXPORT_INVOICE_OUT_2_DBF') IS NULL EXEC('CREATE PROCEDURE DBO.USP_EXPORT_INVOICE_OUT_2_DBF AS RETURN')
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_EXPORT_INVOICE_OUT_2_DBF]
    @ID_GLOBAL UNIQUEIDENTIFIER 
AS
    DECLARE @DOC TABLE(
        FID BIGINT NOT NULL PRIMARY KEY IDENTITY,
        DOC_NAME VARCHAR(300),
        ID_SUPPLIER BIGINT
    )
    
    DECLARE @HEADER TABLE(
        SID BIGINT NOT NULL PRIMARY KEY IDENTITY,
        FID BIGINT NOT NULL,

        SUP_NAME VARCHAR(300),
        SVAT_SUP MONEY,
        SUM_SUP MONEY,
        SVAT_SAL MONEY,
        SUM_SAL MONEY,
        DOC_NUM VARCHAR(300),
        DOC_DATE DATETIME,
        BILL_NUM VARCHAR(300),
        BILL_DATE DATETIME,
        COMMENT VARCHAR(300),
        STORE_CODE VARCHAR(40)
    )

    DECLARE @ITEM TABLE(
        TID BIGINT NOT NULL PRIMARY KEY IDENTITY,
        SID BIGINT NOT NULL,
        FID BIGINT NOT NULL,

        NUMERATOR INT, 
        DENOMINATOR INT,
        UNIT_NAME VARCHAR(300),
        
        GOODS_CODE VARCHAR(300),
        GOODS VARCHAR(300),
        PRODUCER VARCHAR(300),
        COUNTRY VARCHAR(300), 
        IMPORTANT BIT,
        REG_PRICE MONEY,
        REG_DATE DATETIME,
        
        QUANTITY MONEY,
        PROD_PRICE MONEY,
        
        SUP_VAT_U MONEY,
        SUP_ADPRICE MONEY,
        SUP_PRICE MONEY,
        SUP_VAT MONEY,
        SUP_PRICE_VAT MONEY,
        SUP_SUM MONEY,
        SUP_VAT_SUM MONEY,
        SUP_SUM_VAT MONEY,
        
        SAL_ADPRICE MONEY,
        SAL_PRICE MONEY,
        SAL_VAT MONEY,
        SAL_PRICE_VAT MONEY,
        SAL_SUM MONEY,
        SAL_VAT_SUM MONEY,
        SAL_SUM_VAT MONEY,
        
        SERIES_NUMBER VARCHAR(300),
        BEST_BEFORE DATETIME,
        GTD_NUM VARCHAR(300),
        BAR_CODE VARCHAR(300),

        ID_SERIES BIGINT
    )

    DECLARE @CERT TABLE(
        TID BIGINT NOT NULL,
        SID BIGINT NOT NULL,
        FID BIGINT NOT NULL,

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
    INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = I.ID_INVOICE_OUT_GLOBAL
    INNER JOIN LOT L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = L.ID_SUPPLIER 
    WHERE ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

    INSERT INTO @HEADER(
        FID,
        SUP_NAME,
        SVAT_SUP,
        SUM_SUP,
        SVAT_SAL,
        SUM_SAL,
        DOC_NUM,
        DOC_DATE,
        BILL_NUM,
        BILL_DATE,
        COMMENT,
        STORE_CODE        
    )
    SELECT
        D.FID,
        C.NAME,
        SVAT_SUP = SUM(L.PVAT_SUP*II.QUANTITY),
        SUM_SUP = SUM(L.PRICE_SUP*II.QUANTITY),
        SVAT_SAL = SUM(L.PVAT_SAL*II.QUANTITY),
        SUM_SAL = SUM(L.PRICE_SAL*II.QUANTITY),
        I.MNEMOCODE,
        I.DATE,
        NULL,
        NULL,
        I.COMMENT,
        (SELECT TOP 1 S.MNEMOCODE FROM dbo.STORE S WHERE S.ID_CONTRACTOR = I.ID_CONTRACTOR_TO)
    FROM INVOICE_OUT I
    INNER JOIN INVOICE_OUT_ITEM II ON II.ID_INVOICE_OUT_GLOBAL = I.ID_INVOICE_OUT_GLOBAL
    INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = II.ID_INVOICE_OUT_GLOBAL AND LM.ID_DOCUMENT_ITEM = II.ID_INVOICE_OUT_ITEM_GLOBAL
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = L.ID_SUPPLIER
    INNER JOIN @DOC D ON D.ID_SUPPLIER = L.ID_SUPPLIER
    WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
    GROUP BY D.FID,C.NAME,I.MNEMOCODE,I.DATE,I.COMMENT,I.ID_CONTRACTOR_TO
    
    INSERT INTO @ITEM(
        SID,
        FID,

        NUMERATOR, 
        DENOMINATOR,
        UNIT_NAME,
        
        GOODS_CODE,
        GOODS,
        PRODUCER,
        COUNTRY, 
        IMPORTANT,
        REG_PRICE,
        REG_DATE,
        
        QUANTITY,
        PROD_PRICE,
        
        SUP_VAT_U,
        SUP_ADPRICE,
        SUP_PRICE,
        SUP_VAT,
        SUP_PRICE_VAT,
        SUP_SUM,
        SUP_VAT_SUM,
        SUP_SUM_VAT,
        
        SAL_ADPRICE,
        SAL_PRICE,
        SAL_VAT,
        SAL_PRICE_VAT,
        SAL_SUM,
        SAL_VAT_SUM,
        SAL_SUM_VAT,
        
        SERIES_NUMBER,
        BEST_BEFORE,
        GTD_NUM,
        BAR_CODE,

        ID_SERIES
    )
    SELECT
        H.SID,
        H.FID,

        NUMERATOR = SR.NUMERATOR, 
        DENOMINATOR = SR.DENOMINATOR,
        UNIT_NAME = U.SHORT_NAME,
        
        GOODS_CODE = (SELECT TOP 1 CODE FROM GOODS_CODE WHERE ID_CONTRACTOR = L.ID_SUPPLIER AND ID_GOODS = G.ID_GOODS),
        GOODS = G.NAME,
        PRODUCER = P.NAME,
        COUNTRY = C.NAME, 
        IMPORTANT = CONVERT(BIT, G.IMPORTANT),
        REG_PRICE = G.REGISTER_PRICE,
        REG_DATE = G.REGISTRATION_DATE,
        
        QUANTITY = II.QUANTITY,
        PROD_PRICE = L.PRICE_PROD,
        
        SUP_VAT_U = L.PVAT_SUP,
        SUP_ADPRICE = CASE ISNULL(L.PRICE_PROD,0) WHEN 0 THEN NULL ELSE ((L.PRICE_SUP * 100)/L.PRICE_PROD)-100 END,
        SUP_PRICE = L.PRICE_SUP - L.PVAT_SUP,
        SUP_VAT = L.VAT_SUP,
        SUP_PRICE_VAT = L.PRICE_SUP,
        SUP_SUM = (L.PRICE_SUP * II.QUANTITY) - (L.PVAT_SUP * II.QUANTITY),
        SUP_VAT_SUM = L.PVAT_SUP * II.QUANTITY,
        SUP_SUM_VAT = L.PRICE_SUP * II.QUANTITY,
        
        SAL_ADPRICE = CASE ISNULL(L.PRICE_SUP,0) WHEN 0 THEN NULL ELSE ((L.PRICE_SAL * 100)/L.PRICE_SUP)-100 END,
        SAL_PRICE = L.PRICE_SAL - L.PVAT_SAL,
        SAL_VAT = VAT_SAL,
        SAL_PRICE_VAT = L.PRICE_SAL,
        SAL_SUM = (L.PRICE_SAL * II.QUANTITY) - (L.PVAT_SAL * II.QUANTITY),
        SAL_VAT_SUM = L.PVAT_SAL * II.QUANTITY,
        SAL_SUM_VAT = L.PRICE_SAL * II.QUANTITY,
        
        S.SERIES_NUMBER,
        S.BEST_BEFORE,
        NULL,
        L.INTERNAL_BARCODE,
        
        S.ID_SERIES
    FROM INVOICE_OUT_ITEM II
    INNER JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT = II.ID_INVOICE_OUT_GLOBAL 
                              AND LM.ID_DOCUMENT_ITEM = II.ID_INVOICE_OUT_ITEM_GLOBAL
                              AND LM.QUANTITY_SUB>0 -- 1409 (экспорт накладной)
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
    INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
    INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
    INNER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
    INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
    INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
    LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
    INNER JOIN @DOC D ON D.ID_SUPPLIER = L.ID_SUPPLIER
    INNER JOIN @HEADER H ON D.FID = H.FID
    WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

    INSERT INTO @CERT(
        TID,
        SID,
        FID,

        CERT_NUMBER,
        CERT_ORGAN,
        CERT_DATE,
        CERT_END_DATE
    )
    SELECT
        I.TID,
        I.SID,
        I.FID,

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
        SUP_NAME,
        SVAT_SUP,
        SUM_SUP,
        SVAT_SAL,
        SUM_SAL,
        DOC_NUM,
        DOC_DATE,
        BILL_NUM,
        BILL_DATE,
        COMMENT
        --STORE_M
    FROM @HEADER        

    SELECT
        I.FID,
        I.SID,
        I.TID,
        ------------------------------------------
        H.STORE_CODE,
        H.SUP_NAME,
        H.SVAT_SUP,
        H.SUM_SUP,
        H.SVAT_SAL,
        H.SUM_SAL,
        H.DOC_NUM,
        H.DOC_DATE,
        H.BILL_NUM,
        H.BILL_DATE,
        H.COMMENT,
        
        ------------------------------------------
        
        NUMERATOR, 
        DENOMINATOR,
        UNIT_NAME,
        
        GOODS_CODE,
        GOODS,
        PRODUCER,
        COUNTRY, 
        IMPORTANT,
        REG_PRICE,
        REG_DATE,
        
        QUANTITY,
        PROD_PRICE,
        
        SUP_VAT_U,
        SUP_ADPRICE,
        SUP_PRICE,
        SUP_VAT,
        SUP_PRICE_VAT,
        SUP_SUM,
        SUP_VAT_SUM,
        SUP_SUM_VAT,
        
        SAL_ADPRICE,
        SAL_PRICE,
        SAL_VAT,
        SAL_PRICE_VAT,
        SAL_SUM,
        SAL_VAT_SUM,
        SAL_SUM_VAT,
        
        SERIES_NUMBER,
        BEST_BEFORE,
        GTD_NUM,
        BAR_CODE,
        -----------------------------
        C.CERT_NUMBER,
        C.ISSUED_BY,
        C.CERT_DATE,
        C.CERT_END_DATE
        
    FROM @ITEM I
LEFT JOIN CERTIFICATE C ON C.ID_SERIES = I.ID_SERIES
inner join @HEADER H  ON H.FID = I.FID

    SELECT 
        FID,
        SID,
        TID,
        CERT_NUMBER,
        CERT_ORGAN,
        CERT_DATE, 
        CERT_END_DATE
    FROM @CERT

RETURN
GO

--exec USP_EXPORT_INVOICE_OUT_2_DBF'91D47851-9FC6-4E3E-8582-0D5DC7097B49'