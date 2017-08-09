IF (OBJECT_ID('DBO.REPEX_INVOICE_OUT_EXPORT_INVOICE_PRICE_SAL') IS NULL) EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_OUT_EXPORT_INVOICE_PRICE_SAL AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_OUT_EXPORT_INVOICE_PRICE_SAL(
    @ID_GLOBAL UNIQUEIDENTIFIER
)
AS
    SELECT
        SUPPLIER_NAME = C.NAME,
        SVAT_SUPPLIER = IOI.SVAT_SUPPLIER,
        SUM_SUPPLIER = IOI.SUM_SUPPLIER,
        SVAT_RETAIL = IOI.SVAT_RETAIL,
        SUM_RETAIL = IOI.SUM_RETAIL,
        INCOMING_NUMBER = IO.MNEMOCODE,
        INCOMING_DATE = [IO].[DATE],
        INCOMING_BILL_NUMBER = CONVERT(VARCHAR(40),NULL),
        INCOMING_BILL_DATE = CONVERT(DATETIME, NULL),
        COMMENT = IO.COMMENT
    FROM INVOICE_OUT IO
    INNER JOIN (SELECT
                    IOI.ID_INVOICE_OUT_GLOBAL,
                    SVAT_SUPPLIER = SUM(IOI.PVAT_SAL * IOI.QUANTITY),
                    SUM_SUPPLIER = SUM(IOI.PRICE_SAL * IOI.QUANTITY),
                    SVAT_RETAIL = CONVERT(MONEY, NULL),
                    SUM_RETAIL = CONVERT(MONEY, NULL)
                FROM INVOICE_OUT_ITEM IOI
                GROUP BY IOI.ID_INVOICE_OUT_GLOBAL) IOI ON IOI.ID_INVOICE_OUT_GLOBAL=IO.ID_INVOICE_OUT_GLOBAL
    INNER JOIN STORE S ON S.ID_STORE = IO.ID_STORE
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
    WHERE IO.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

    SELECT
        NUMERATOR = SR.NUMERATOR,
        DENOMINATOR = SR.DENOMINATOR,
        UNIT_NAME = U.SHORT_NAME,

        GOODS_CODE = G.CODE,
        GOODS = G.NAME,
        PRODUCER = P.NAME,
        COUNTRY = C.NAME,
        IMPORTANT = CASE WHEN G.IMPORTANT=1 THEN CONVERT(BIT,1) ELSE CONVERT(BIT, 0) END,
        REGISTER_PRICE = G.REGISTER_PRICE,
        REGISTRATION_DATE = G.REGISTRATION_DATE,
      
        QUANTITY = IOI.QUANTITY,
        PRODUCER_PRICE = L.PRICE_PROD,
      
        SUPPLIER_VAT_PER_UNIT = IOI.PVAT_SAL,
        SUPPLIER_ADPRICE = 0,
        SUPPLIER_PRICE = IOI.PRICE_SAL - IOI.PVAT_SAL,
        SUPPLIER_VAT = L.VAT_SAL,
        SUPPLIER_PRICE_VAT = IOI.PRICE_SAL,
        SUPPLIER_SUM = (IOI.PRICE_SAL - IOI.PVAT_SAL) * IOI.QUANTITY,
        SUPPLIER_VAT_SUM = IOI.PVAT_SAL * IOI.QUANTITY,
        SUPPLIER_SUM_VAT = IOI.PRICE_SAL * IOI.QUANTITY,
      
        RETAIL_ADPRICE = CONVERT(MONEY, NULL),
        RETAIL_PRICE = CONVERT(MONEY, NULL),
        RETAIL_VAT = CONVERT(MONEY, NULL),
        RETAIL_PRICE_VAT = CONVERT(MONEY, NULL),
        RETAIL_SUM = CONVERT(MONEY, NULL),
        RETAIL_VAT_SUM = CONVERT(MONEY, NULL),
        RETAIL_SUM_VAT = CONVERT(MONEY, NULL),
      
        SERIES_NUMBER = S.SERIES_NUMBER,
        BEST_BEFORE = S.BEST_BEFORE,
        GTD_NUMBER = L.GTD_NUMBER,
        BAR_CODE = (SELECT TOP 1 CODE FROM BAR_CODE WHERE ID_GOODS = G.ID_GOODS AND DATE_DELETED IS NULL)
    FROM INVOICE_OUT_ITEM IOI
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = IOI.ID_LOT_GLOBAL
    INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
    INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
    INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
    INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
    INNER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
    LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
    WHERE IOI.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

    SELECT
        ID_SERIES = C.ID_SERIES,
        CERT_NUMBER = C.CERT_NUMBER, 
        CERT_ORGAN = C.ISSUED_BY,
        CERT_DATE = C.CERT_DATE,
        CERT_END_DATE = C.CERT_END_DATE
    FROM CERTIFICATE C
    WHERE EXISTS (SELECT NULL 
                  FROM LOT L
                  WHERE L.ID_SERIES = C.ID_SERIES
                  AND EXISTS (SELECT NULL
                              FROM INVOICE_OUT_ITEM IOI
                              WHERE IOI.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
                              AND IOI.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL))

RETURN
GO