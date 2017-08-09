IF (OBJECT_ID('USP_LOADING_REG_CERT') IS NULL) EXEC ('CREATE PROCEDURE USP_LOADING_REG_CERT AS RETURN')
GO
ALTER PROCEDURE USP_LOADING_REG_CERT
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @RETURN INT SET @RETURN = 0
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM

SELECT * INTO #INVOICES
FROM OPENXML(@HDOC, '/XML/INVOICES/ID_INVOICE_GLOBAL') WITH(ID_INVOICE_GLOBAL UNIQUEIDENTIFIER '.')

select * into #items
from openxml(@hdoc, '/XML/ITEMS/ITEM') WITH(
    CODE varchar(10) 'CODE',
    NUMREESTR varchar(10) 'NUMREESTR',
    SER varchar(20) 'SER',
    NUM money 'NUM',
--    POST varchar(100) 'POST',
    ORGAN varchar(300) 'ORGAN')

declare @t table(id_lot_global UNIQUEIDENTIFIER, CODE varchar(10), NUMREESTR varchar(10),SER varchar(20),NUM money,ORGAN varchar(300), id_reg_cert UNIQUEIDENTIFIER) --POST varchar(100),

insert into @t(id_lot_global, CODE, NUMREESTR,SER,NUM,ORGAN, id_reg_cert) --POST,
select id_lot_global,-- = max(convert(varchar(36),l.id_lot_global)),
       ii.CODE,
       ii.NUMREESTR,
       ii.SER,
       ii.NUM,
--       ii.POST,
       ii.ORGAN,
       null
from #items ii
 inner join goods g on left(g.code,10) = ii.code
 inner join series s on s.series_number = ii.ser and s.id_goods = g.id_goods
-- inner join contractor c on c.name = ii.post
 inner join lot l on l.id_goods = g.id_goods and l.id_series = s.id_series --and l.id_supplier = c.id_contractor
 inner join invoice_item i on i.id_invoice_item_global = l.id_document_item and ii.num = i.quantity and i.id_reg_cert_global is null
-- inner join reg_cert rc on rc.ID_REG_CERT_GLOBAL = l.ID_REG_CERT_GLOBAL
where not exists(select null from #items _ii 
                 where _ii.code = ii.code 
                 and _ii.ser = ii.ser
                 and _ii.num = ii.num
--                 and _ii.post = ii.post
                 group by code,ser,num--,post 
                 having count(*)>1)
group by ii.CODE, ii.NUMREESTR, ii.SER, ii.NUM, ii.ORGAN,l.id_lot_global --ii.POST,

update t
    set id_reg_cert = newid()
from @t t
-- where exists(select
--                 id_lot_global,
--                 count(*)
--             from @t
--             group by id_lot_global
--             having count(*)=1)

begin tran
    insert into reg_cert(
        ID_REG_CERT_GLOBAL,
        NAME,
        DATE,
        ORGAN)
    select
        ID_REG_CERT,
        numreestr,
        getdate(),
        organ
    from @t t
    where id_reg_cert is not null
    IF @@ERROR != 0 GOTO ERROR

    update l
        set ID_REG_CERT_GLOBAL = t.ID_REG_CERT
    from lot l
    inner join @t t on t.id_lot_global = l.id_lot_global

    IF @@ERROR != 0 GOTO ERROR

    update ii
        set ID_REG_CERT_GLOBAL = t.id_reg_cert
    from invoice_item ii
    inner join lot l on l.id_document_item = ii.id_invoice_item_global
    inner join @t t on t.id_lot_global = l.id_lot_global
    where ii.ID_REG_CERT_GLOBAL is null
--and t.id_lot_global = 'E3B0FB7B-EB7B-4630-8895-AED766F96D7E'

    IF @@ERROR != 0 GOTO ERROR
    --не нашлось
--     select code,numreestr,ser,num,post 
--     from (
--         select code,numreestr,ser,num,post 
--         from #items 
--         group by code,ser,num,post 
--         having count(*)=1
--         
--         union
--     
--         select
--             code,numreestr,ser,num,post        
--         from @t t1
--         where exists(
--             select
--                 id_lot_global            
--             from @t t
--             where t1.id_lot_global = t.id_lot_global
--             group by id_lot_global
--             having count(*)<>1)
--         group by CODE, NUMREESTR, SER, NUM, POST)y

    COMMIT TRANSACTION
    select code,NUMREESTR,ser,num--,post 
    from (
        select code,NUMREESTR,ser,num--,post 
        from #items 
        group by code,ser,num,NUMREESTR --post,
        having count(*)>1
        
        union
    
        select
            code,numreestr,ser,num--,post        
        from @t t1
        where exists(
            select
                id_lot_global            
            from @t t
            where t1.id_lot_global = t.id_lot_global
            group by id_lot_global
            having count(*)<>1)
        group by CODE, NUMREESTR, SER, NUM--, POST
        
        union
        
        select
            code,numreestr,ser,num--,post        
        from #items i
        where not exists(select null from @t t 
                         where t.code = i.code 
                         and t.ser = i.ser
                         and t.num = i.num
--                         and t.post = i.post
                         and t.numreestr = i.numreestr))y

FINISH:
    RETURN @RETURN

ERROR:
    SET @RETURN = @@ERROR
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
    GOTO FINISH

RETURN
GO

--exec USP_LOADING_REG_CERT null