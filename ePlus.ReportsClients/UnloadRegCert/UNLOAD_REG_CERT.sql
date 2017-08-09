IF (OBJECT_ID('USP_GET_ITEMS_BY_ID_DOCUMENT') IS NULL) EXEC ('CREATE PROCEDURE USP_GET_ITEMS_BY_ID_DOCUMENT AS RETURN')
GO
ALTER PROCEDURE USP_GET_ITEMS_BY_ID_DOCUMENT
    @XMLPARAM NTEXT
--    @ID_DOCUMENT UNIQUEIDENTIFIER
AS
DECLARE @HDOC INT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM

SELECT * INTO #IDS
FROM OPENXML(@HDOC, '/XML/ID_ITEM_GLOBAL') WITH(ID_ITEM_GLOBAL UNIQUEIDENTIFIER '.')

    select 
        id_invoice_item_global,
        mnemocode = inv.mnemocode,
        goods_name = g.name,
        g.code,
        producer = p.name,
        quantity = i.quantity*sr.numerator/sr.denominator,
        supplier = inv.ID_CONTRACTOR_SUPPLIER,
        series = s.series_number,
        cert_number = c.cert_number,
        cert_date = c.cert_date,
        cert_center = c.issued_by
    from invoice_item i
    inner join invoice inv on inv.ID_INVOICE_GLOBAL = i.ID_INVOICE_GLOBAL
    inner join goods g on g.id_goods = i.id_goods
    inner join producer p on p.id_producer = g.id_producer
    inner join scaling_ratio sr on sr.id_scaling_ratio = i.id_scaling_ratio
    left join series s on s.id_series = i.id_series
    left join certificate c on c.id_series = s.id_series
--    where ID_INVOICE_GLOBAL = @ID_DOCUMENT
    where exists(select null from #IDS ii where ii.ID_ITEM_GLOBAL = i.ID_INVOICE_GLOBAL)

--    select mnemocode from invoice where ID_INVOICE_GLOBAL = @ID_DOCUMENT
RETURN
GO
--exec USP_GET_ITEMS_BY_ID_DOCUMENT @XMLPARAM=N'<XML><ID_ITEM_GLOBAL>57de79cd-63d8-4e6d-bdd0-b94ad86638d9</ID_ITEM_GLOBAL><ID_ITEM_GLOBAL>d8973849-60e7-4c6e-9827-9bc6da2d9bf6</ID_ITEM_GLOBAL><ID_ITEM_GLOBAL>b69a58b2-c9ca-411d-9fd6-72a4c42f44eb</ID_ITEM_GLOBAL></XML>'
--drop procedure USP_GET_ITEMS_BY_ID
--exec USP_GET_ITEMS_BY_ID_DOCUMENT null--@ID_DOCUMENT='EBC4149C-C4DC-44F4-829E-651079E836F3'
----------------------------------------------------------------------------------------
IF (OBJECT_ID('USP_GET_ITEMS_BY_ID') IS NULL) EXEC ('CREATE PROCEDURE USP_GET_ITEMS_BY_ID AS RETURN')
GO
ALTER PROCEDURE USP_GET_ITEMS_BY_ID
    @XMLPARAM NTEXT, @ID_DOCUMENT UNIQUEIDENTIFIER
AS
DECLARE @HDOC INT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM

SELECT * INTO #IDS
FROM OPENXML(@HDOC, '/XML/ITEM') WITH(ID_INVOICE_ITEM_GLOBAL UNIQUEIDENTIFIER '.')

select 
        id_invoice_item_global,
        name = g.name,
        code = g.code,
        prod = p.name+' '+con.name,
        num = i.quantity*sr.numerator/sr.denominator,
        post = cc.name,--(select c.name from contractor c where exists(select null from invoice i where ID_INVOICE_GLOBAL = @ID_DOCUMENT and i.ID_CONTRACTOR_SUPPLIER = c.id_contractor)),
        ser = s.series_number,
        nsert = c.cert_number,
        datasert = c.cert_date,
        centrsert = c.issued_by
    from invoice_item i
    inner join invoice inv on inv.ID_INVOICE_GLOBAL = i.ID_INVOICE_GLOBAL
    inner join contractor cc on cc.id_contractor = inv.ID_CONTRACTOR_SUPPLIER
    inner join goods g on g.id_goods = i.id_goods
    inner join producer p on p.id_producer = g.id_producer
    inner join country con on con.id_country = p.id_country
    inner join scaling_ratio sr on sr.id_scaling_ratio = i.id_scaling_ratio
    left join series s on s.id_series = i.id_series
    left join certificate c on c.id_series = s.id_series
    where exists(select null from #IDS ids where ids.ID_INVOICE_ITEM_GLOBAL = i.ID_INVOICE_ITEM_GLOBAL)
    order by g.name
RETURN
GO

--exec USP_GET_ITEMS_BY_ID @XMLPARAM=N'<XML><ITEM><ID_INVOICE_ITEM_GLOBAL>b0ef9ae5-bf49-400f-99d2-a1cb5b94b4e5</ID_INVOICE_ITEM_GLOBAL></ITEM></XML>',@ID_DOCUMENT='F2E804E9-948D-49B6-8057-3AA4DA216B4B'
--exec USP_GET_ITEMS_BY_ID @XMLPARAM=N'<XML><ITEM><ID_INVOICE_ITEM_GLOBAL>5f219f0b-8f47-45cf-841e-9ed252f9539b</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>6eced23f-3d6f-487e-acef-ae15fdbe1969</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>259c1ada-fbb2-4b0c-9aa7-259f37a1160a</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>1390adbe-9c68-4df6-bd5e-387124bbf850</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>88edc7c8-8398-4dfe-8bf3-ab3f31472063</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>610bb84a-6b56-4eef-8c9d-3684634089ac</ID_INVOICE_ITEM_GLOBAL></ITEM><ITEM><ID_INVOICE_ITEM_GLOBAL>ef8c28fd-471e-4c86-85cc-9db80c105862</ID_INVOICE_ITEM_GLOBAL></ITEM></XML>',@ID_DOCUMENT='00000000-0000-0000-0000-000000000000'