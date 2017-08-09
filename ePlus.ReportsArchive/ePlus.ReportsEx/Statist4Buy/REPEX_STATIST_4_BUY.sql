IF (OBJECT_ID('REPEX_STATIST_4_BUY') IS NULL) EXEC ('CREATE PROCEDURE REPEX_STATIST_4_BUY AS RETURN')
GO
ALTER PROCEDURE REPEX_STATIST_4_BUY(
    @XMLPARAM NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @DATE_FROM DATETIME
    DECLARE @DATE_TO DATETIME
    DECLARE @STORE TABLE (ID_STORE BIGINT)
    DECLARE @REM_DAYS INT
    DECLARE @ORDER_DAYS INT
    DECLARE @ALL_STORE BIT
    DECLARE @TOPN varchar(4000)
    DECLARE @ORDER_EXPR VARCHAR(4000)

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT 
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @REM_DAYS = REM_DAYS,
        @ORDER_DAYS = ORDER_DAYS,
        @TOPN = TOPN,
        @ORDER_EXPR = ORDER_EXPR
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
        REM_DAYS INT 'REM_DAYS',
        ORDER_DAYS INT 'ORDER_DAYS',
        TOPN VARCHAR(4000) 'TOPN',
        ORDER_EXPR VARCHAR(4000) 'ORDER_EXPR'
    )

    INSERT INTO @STORE
    SELECT
        ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    )
     
    IF (@@ROWCOUNT=0)
        SET @ALL_STORE = 1   

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    exec usp_range_days @date_from out, @date_to out
    exec usp_range_norm @date_from out, @date_to out
    
    
    declare @sales_apt table(
        id_goods bigint,
        id_store bigint,
        quantity_rem money,
        quantity_sub money
    )

    insert into @sales_apt
    select distinct
        l.id_goods,
        l.id_store,
        quantity_rem = isnull(a.quantity_rem,0),
        quantity_sub = isnull(b.quantity_sub,0)
    from lot l
    inner join store s on s.id_store = l.id_store
    left join (select
                   l.id_goods,
                   l.id_store,
                   quantity_rem = sum((lm.quantity_add-lm.quantity_sub-lm.quantity_res) * convert(money, sr.numerator) / sr.denominator)
               from lot_movement lm 
               inner join lot l on l.id_lot_global = lm.id_lot_global
               inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
               where lm.date_op <= @DATE_TO
               group by l.id_goods, l.id_store) a on a.id_goods = l.id_goods and a.id_store = l.id_store
    left join (select 
                   l.id_goods,
                   l.id_store,
                   quantity_sub = sum(lm.quantity_sub * convert(money, sr.numerator) / sr.denominator)                
               from LOT_MOVEMENT LM 
               inner join lot l on l.id_lot_global = lm.id_lot_global
               inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
               where lm.code_op in ('CHEQUE')
               and lm.date_op between @DATE_FROM and @DATE_TO
               group by l.id_goods, l.id_store) b on b.id_goods = l.id_goods and b.id_store = l.id_store
    where s.id_contractor <> DBO.FN_CONST_CONTRACTOR_SELF()
    --and s.date_deleted is null
    and (@all_store =1 or (s.id_store in (select null from @store)))

    create table #main1(
        goods varchar(100),
        sale_co money,
        remain_co money,
        sale_apt money,
        rem_apt money
    )

    create table #main(
        goods varchar(100),
        sale_co money,
        remain_co money,
        sale_apt money,
        rem_apt money
    )
    insert into #main
    select
        goods = isnull(gc.name, g.name),
        sale_co = sum(isnull(sale_co,0)),
        remain_co = sum(isnull(remain_co,0)+isnull(on_the_way_co,0)),
        sale_apt = sum(isnull(sale_apt,0)),
        rem_apt = sum(isnull(rem_apt,0))
    from (select 
              id_goods = l.id_goods,
              sale_co = max(lm.quantity_sub),
              on_the_way_co = max(otw.on_the_way),
              remain_co = max(rem.quantity_rem),
              sale_apt = max(sa.quantity_sub),
              rem_apt = max(rem_apt.quantity_rem)
          from lot l
    --       inner join store s on s.id_store = l.id_store
    --       inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
          left join (select 
                          l.id_goods,
                          quantity_sub = sum(lm.quantity_sub * convert(money, sr.numerator) / sr.denominator)
                      from LOT_MOVEMENT LM 
                      inner join lot l on l.id_lot_global = lm.id_lot_global
                      inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
                      inner join store s on s.id_store = l.id_store
                      where lm.code_op in ('MOVE', 'INVOICE_OUT')
                      and lm.date_op between @date_from and @date_to
    --                  and s.date_deleted is null
                      and s.id_contractor = DBO.FN_CONST_CONTRACTOR_SELF()
                      group by l.id_goods) lm on lm.id_goods = l.id_goods
          left join (select 
                         id_goods,
                         quantity_sub = sum(quantity_sub)
                     from @sales_apt sa
                     group by id_goods) sa on sa.id_goods = l.id_goods
          left join (select 
                         id_goods,
                         quantity_rem = sum(quantity_rem)
                     from @sales_apt sa
                     group by id_goods) rem_apt on rem_apt.id_goods = l.id_goods
          left join (select
                         ii.id_goods,
                         on_the_way = sum(quantity * convert(money, sr.numerator) / sr.denominator)
                     from invoice_item ii
                     inner join invoice i on i.id_invoice_global = ii.id_invoice_global
                     inner join scaling_ratio sr on sr.id_scaling_ratio = ii.id_scaling_ratio
                     inner join store s on s.id_store = i.id_store
                     where i.document_state = 'SAVE'
                     and i.on_the_way = 1
    --                 and s.date_deleted is null
                     and s.id_contractor = DBO.FN_CONST_CONTRACTOR_SELF()
                     group by ii.id_goods) otw on otw.id_goods = l.id_goods
          left join (select 
                          l.id_goods,
                          quantity_rem = sum(l.quantity_rem * convert(money, sr.numerator) / sr.denominator)                      
                      from lot l
                      inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
                      inner join store s on s.id_store = l.id_store
--                       where exists (select null from lot_movement lm 
--                                     where lm.code_op in ('MOVE', 'INVOICE_OUT')
--                                     and lm.date_op between @date_from and @date_to
--                                     and lm.id_lot_global = l.id_lot_global)
    --                  and s.date_deleted is null
                      where s.id_contractor = DBO.FN_CONST_CONTRACTOR_SELF()
                      group by l.id_goods) rem on rem.id_goods = l.id_goods
    --       where s.date_deleted is null
    --       and s.id_contractor = DBO.FN_CONST_CONTRACTOR_SELF()
          group by l.id_goods
    ) a
    inner join goods g on g.id_goods = a.id_goods
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    group by isnull(gc.name, g.name)
    having (sum(isnull(sale_co,0))>0 or sum(isnull(sale_apt,0))>0)
    and (
    (isnull(@rem_days,0)=0 and sum(isnull(sale_co,0)) > (sum(isnull(remain_co,0))+sum(isnull(on_the_way_co,0))+sum(isnull(rem_apt,0))))
    or
    (isnull(@rem_days,0)>0 and ((sum(isnull(sale_co,0)) / (datediff(d,@date_from, @date_to)+1)) * @rem_days) > (sum(isnull(remain_co,0))+sum(isnull(on_the_way_co,0))+sum(isnull(rem_apt,0))))
    )

    declare @query nvarchar(4000)
    set @query = 'insert into #main1
                  select '+ISNULL(@topN,'')+'
                      GOODS,
                      SALE_CO,
                      REMAIN_CO,
                      SALE_APT,
                      REM_APT
                  from #main
                  '+isnull(@order_expr,'')    

    exec sp_executesql @query

    delete from #main
    insert into #main(
        GOODS,
        SALE_CO,
        REMAIN_CO,
        SALE_APT,
        REM_APT
    )
    select
        GOODS,
        SALE_CO,
        REMAIN_CO,
        SALE_APT,
        REM_APT
    from #main1

    set @query = 'select
                      GOODS,
                      SALE_CO,
                      REMAIN_CO,
                      SALE_APT,
                      REM_APT
                  from #main
                  '+isnull(@order_expr,'')    
    exec sp_executesql @query
        
    
-- Ост(R): 63                    sa.quantity_rem
-- продано(S): 100               sa.quantity_sub
-- период(P): 10 дней            (datediff(d,@date_from, @date_to)+1)
-- дней остатков(DR): 7          @rem_days
-- дней заявки(DZ): 1            @order_days
-- 
-- Ср. ск(SS): S / P             (sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1))
-- Надо (N): DR * SS             (@rem_days * (sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1)))
-- Закупка: N - (R-(SS*DZ))      (@rem_days * (sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1))) -(sa.quantity_rem - ((sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1)) *@order_days))

  select 
      GOODS,
      STORE,
      QUANTITY_REM,
      QUANTITY_SUB,
      ORDER_DAYS,
      ORDER_QTY = CEILING(CASE WHEN ORDER_QTY < 0 THEN 0 ELSE ORDER_QTY END)
  from (select 
            GOODS = isnull(gc.name, g.name),
            STORE = s.name,
            QUANTITY_REM = sa.quantity_rem,
            QUANTITY_SUB = sa.quantity_sub,
            ORDER_DAYS = @order_days,
            ORDER_QTY = (case when @rem_days=0 then 1 else @rem_days end * (sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1))) -(sa.quantity_rem - ((sa.quantity_sub /  (datediff(d,@date_from, @date_to)+1)) *@order_days))
        from @sales_apt sa
        inner join goods g on g.id_goods = sa.id_goods
        inner join store s on s.id_store =sa .id_store
        left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
        left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
        where exists (select null from #main m where m.goods = isnull(gc.name, g.name)) 
      ) a
  order by store asc
--where sa.quantity_rem > 0 or sa.quantity_sub > 0

select
    goods = isnull(gc.name, g.name),
    quantity_add = lm.quantity_add * convert(money, sr.numerator) / sr.denominator,
    price_sup = l.price_sup,
    price_sal = l.price_sal,
    date_op = lm.date_op,
    supplier = sup.name,
    lot_name = i.mnemocode,
    quantity_rem = l.quantity_rem * convert(money, sr.numerator) / sr.denominator
into #lots
from lot l
inner join lot_movement lm on lm.id_lot_global = l.id_lot_global and l.id_document=lm.id_document and l.id_document_item = lm.id_document_item
inner join store s on s.id_store = l.id_store
inner join contractor sup on sup.id_contractor = l.id_supplier
inner join invoice_item ii on ii.id_invoice_global = l.id_document and ii.id_invoice_item_global = l.id_document_item
inner join goods g on g.id_goods = l.id_goods
inner join invoice i on i.id_invoice_global = ii.id_invoice_global
inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
where s.id_contractor = DBO.FN_CONST_CONTRACTOR_SELF()
and lm.quantity_add >0
and exists (select null from #main m where m.goods = isnull(gc.name, g.name))
--and s.date_deleted is null
-- and i.document_state = 'PROC'
 
select 
    GOODS,
    QUANTITY_ADD,
    PRICE_SUP,
    PRICE_SAL,
    DATE_OP,
    SUPPLIER,
    LOT_NAME,
    QUANTITY_REM
from #lots l
where (select 
           count(*)
       from #lots l1
       where l1.goods = l.goods
       and l1.date_op >= l.date_op
       group by goods) <=3
order by goods asc, date_op desc
 
RETURN
GO

--exec REPEX_STATIST_4_BUY @xmlParam = N'<XML><DATE_FROM>2008-09-01T15:21:09.859</DATE_FROM><DATE_TO>2008-09-12T15:21:09.859</DATE_TO><REM_DAYS>0</REM_DAYS><ORDER_DAYS>0</ORDER_DAYS><TOPN></TOPN><ORDER_EXPR>ORDER BY REMAIN_CO+REM_APT DESC</ORDER_EXPR></XML>'