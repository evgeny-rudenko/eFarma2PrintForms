IF (OBJECT_ID('USP_INVOICE_OUT_SAVE_4_RIGLA_LOADER') IS NULL) EXEC ('CREATE PROCEDURE USP_INVOICE_OUT_SAVE_4_RIGLA_LOADER AS RETURN')
GO

ALTER PROCEDURE [dbo].[USP_INVOICE_OUT_SAVE_4_RIGLA_LOADER](
    @ID_INVOICE_OUT_GLOBAL UNIQUEIDENTIFIER,
    @ID_USER_MODIFIED UNIQUEIDENTIFIER
--    @XML_DATA NTEXT,
--	@XML_LOGGER_CONTEXT VARCHAR(4000) = NULL
) AS

DECLARE @RETURN INT SET @RETURN = 0

DECLARE @ROWCOUNT INT, @ERROR INT

BEGIN TRANSACTION

    update ii
    set PRICE_SAL = l.price_sup, 
        PVAT_SAL = l.price_sup * (1-l.vat/100) /100,
        PSUM_SAL = l.price_sup * (1-l.vat/100) /100 * ii.quantity,
        SUM_SAL = l.price_sup * ii.quantity,
        vat_discount = 100/l.price_sal * (l.price_sal - l.price_sup),
        SUM_DISCOUNT = (l.price_sal -l.price_sup) * ii.quantity,
        adprice_sal = 0 --формула (price_sal - price_sup)*100/price_sup
    from invoice_out_item ii
    inner join lot l on l.id_lot_global = ii.id_lot_global
    where id_invoice_out_global = @id_invoice_out_global


    UPDATE i
    SET ID_USER_MODIFIED = @ID_USER_MODIFIED,
        DATE_MODIFIED = GETDATE(),
        SVAT_SAL = ii.svat_sal,
        SUM_SAL = ii.sum_sal,
--         INVOICE_OUT.VAT_DISCOUNT = #DOCUMENTS.VAT_DISCOUNT,
        SUM_DISCOUNT = ii.sum_discount
    FROM INVOICE_OUT i
    inner join (
                select
                    ID_INVOICE_OUT_GLOBAL,
                    svat_sal = sum(pvat_sal * quantity),
                    sum_sal = sum(price_sal * quantity),
                    sum_discount = sum(sum_discount)    
                from invoice_out_item
                where ID_INVOICE_OUT_GLOBAL = @ID_INVOICE_OUT_GLOBAL
                group by id_invoice_out_global
                ) ii on ii.ID_INVOICE_OUT_GLOBAL = i.ID_INVOICE_OUT_GLOBAL
    WHERE i.ID_INVOICE_OUT_GLOBAL = @ID_INVOICE_OUT_GLOBAL
    
    SELECT @ROWCOUNT=@@ROWCOUNT, @ERROR=@@ERROR

COMMIT TRANSACTION

FINISH:
    RETURN @RETURN

ERROR:
    SET @RETURN = @@ERROR
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
    GOTO FINISH
