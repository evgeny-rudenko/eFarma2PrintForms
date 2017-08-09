SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REP_ABC_REQUEST_CALCULATION_CALC_NEEDS') IS NULL) EXEC ('CREATE PROCEDURE REP_ABC_REQUEST_CALCULATION_CALC_NEEDS AS RETURN')
GO

ALTER PROCEDURE [dbo].[REP_ABC_REQUEST_CALCULATION_CALC_NEEDS]
    @XMLPARAM NTEXT
AS
    DECLARE @ID_CONTRACTOR BIGINT
    DECLARE @DATE_TO DATETIME
    DECLARE @DATE_TO_OLD DATETIME           
    DECLARE @DATE_TO_OLD2 DATETIME           
    DECLARE @TypeGroup nvarchar(100)
    DECLARE @SORTKIND nvarchar(100) 
    DECLARE @APerc INT
    DECLARE @BPerc INT
    DECLARE @CPerc INT
    DECLARE @temp nvarchar(100)    
    DECLARE @HDOC INT
    DECLARE @DAYS_PERIOD INT
    declare @all_store bit
    declare @all_goods bit
    declare @all_goods_kind bit
    declare @total_qty_sub money
    declare @total_retail_sum money
    declare @total_pribil  money
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT
        @DAYS_PERIOD = DAYS_PERIOD
    FROM OPENXML(@HDOC, '//XML') WITH(
        DAYS_PERIOD INT 'DAYS_PERIOD'
    )

    SELECT SUB_OP_TYPE
    INTO #SUB_OP_TYPE
    FROM OPENXML(@HDOC, '/XML/SUB_OP_TYPE') WITH (
        SUB_OP_TYPE VARCHAR(11) '.'
    )

    SELECT ID_STORE
    INTO #STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH (
        ID_STORE VARCHAR(11) '.'
    )
    if (@@rowcount=0)
        set @all_store = 1
    else    
        set @all_store = 0
      
    select ID_GOODS
    INTO #GOODS
    FROM OPENXML(@HDOC, '/XML/GOODS') WITH (
        ID_GOODS BIGINT '.'
    )
    if (@@rowcount=0)
        set @all_goods = 1

    SELECT ID_GOODS_KIND
    INTO #GOODS_KIND
    FROM OPENXML(@HDOC, '/XML/ID_GOODS_KIND') WITH(
        ID_GOODS_KIND BIGINT '.'
    )
    IF (@@ROWCOUNT=0)
        set @all_goods_kind = 1 

    SELECT @ID_CONTRACTOR = ID_CONTRACTOR
    --INTO #CONTRACTOR
    FROM OPENXML(@HDOC, '//XML') WITH(
        ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
    )
    IF (@@ROWCOUNT=0)
        set @ID_CONTRACTOR = 1 

    SELECT @DATE_TO = convert(datetime, DATE_TO, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_TO nvarchar(100) 'DATE_TO'
    )
    IF (@@ROWCOUNT=0)
        set @DATE_TO = getdate()
 --////////////////////
    SELECT @DATE_TO_OLD = convert(datetime, DATE_TO_OLD, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_TO_OLD nvarchar(100) 'DATE_TO_OLD'
    )
    IF (@@ROWCOUNT=0)
        set @DATE_TO_OLD = getdate()

    SELECT @DATE_TO_OLD2 = convert(datetime, DATE_TO_OLD2, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_TO_OLD2 nvarchar(100) 'DATE_TO_OLD2'
    )
    IF (@@ROWCOUNT=0)
        set @DATE_TO_OLD2 = getdate()


    SELECT @TypeGroup = CalcType    
    FROM OPENXML(@HDOC, '//XML') WITH(
        CalcType nvarchar(100) 'CalcType'
    )
    IF (@@ROWCOUNT=0)
        set @TypeGroup = 'Qty'

    SELECT @APerc = APercent    
    FROM OPENXML(@HDOC, '//XML') WITH(
        APercent BIGINT 'APercent'
    )
    IF (@@ROWCOUNT=0)
        set @APerc = 75 

    SELECT @BPerc = BPercent    
    FROM OPENXML(@HDOC, '//XML') WITH(
        BPercent BIGINT 'BPercent'
    )
    IF (@@ROWCOUNT=0)
        set @BPerc = 20

    SELECT @SORTKIND = SORTKIND    
    FROM OPENXML(@HDOC, '//XML') WITH(
        SORTKIND nvarchar(100) 'SORTKIND'
    )
    IF (@@ROWCOUNT=0)
        set @SORTKIND = 'SORTAIMPERC'


    set @CPerc = 100 - (@APerc + @BPerc)    

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    declare @date_from datetime
    declare @date_from_old datetime
    declare @date_from_old2 datetime
    set @date_to = convert(datetime, ceiling(convert(money, @date_to)))
    set @date_from = convert(datetime, floor(convert(money, dateadd(d, (@days_period+1) * -1, @date_to))))

    set @date_to_old = convert(datetime, ceiling(convert(money, @date_to_old)))
    set @date_to_old2 = convert(datetime, ceiling(convert(money, @date_to_old2)))
    set @date_from_old = convert(datetime, floor(convert(money, dateadd(d, (@days_period+1) * -1, @date_to_old))))
    set @date_from_old2 = convert(datetime, floor(convert(money, dateadd(d, (@days_period+1) * -1, @date_to_old2))))

    declare @calendar table(date datetime not null primary key clustered)
    declare @date_on datetime
    set @date_on = @date_from
    while @date_on<=@date_to begin
        insert into @calendar values(@date_on)
        set @date_on = dateadd(d, 1, @date_on)
    end


    create table #all(
        ID int identity(1,1) not null primary key clustered,
        id_goods uniqueidentifier not null,
        goods_name varchar(max),
        code varchar(max),
        id_goods_classifier uniqueidentifier,
        goods_classifier_name varchar(max),
        id_producer uniqueidentifier,
        producer_name varchar(max),
        min_qty money,    
        qty_sub money,
        supplier_sum money,
        retail_sum money,
        remain money,
        last_price money, -- потом она заменяется на среднюю
        last_price_date datetime,
        id_supplier uniqueidentifier,
        supplier_name varchar(max),
        on_the_way money,
        pribil money,
        abc char,
        aim_percent money,
        Total_aim_percent money,
        days_period int,
        qty_sub_per_day money,
        rate_oborot money,
        zakazat money,
        remain_begin money
    )
    CREATE INDEX IX_#ALL$ID_GOODS ON #ALL(ID_GOODS)

    
    create table #all_old(
        ID int identity(1,1) not null primary key clustered,
        id_goods uniqueidentifier not null,
        goods_name varchar(max),
        code varchar(max),
        id_goods_classifier uniqueidentifier,
        goods_classifier_name varchar(max),
        id_producer uniqueidentifier,
        producer_name varchar(max),
        min_qty money,    
        qty_sub money,
        supplier_sum money,
        retail_sum money,
        remain money,
        last_price money,
        last_price_date datetime,
        id_supplier uniqueidentifier,
        supplier_name varchar(max),
        on_the_way money,
        pribil money,
        abc char,
        aim_percent money,
        Total_aim_percent money
    )
    CREATE INDEX IX_#ALL_OLD$ID_GOODS ON #ALL_OLD(ID_GOODS)


    create table #all_old2(
        ID int identity(1,1) not null primary key clustered,
        id_goods uniqueidentifier not null,
        qty_sub money
    )
    CREATE INDEX IX_#ALL_OLD2$ID_GOODS ON #ALL_OLD2(ID_GOODS)


  print 'Chekpoint1'
--select newid()
    insert into #all
    select 
    --g.id_goods_global, ISNULL(gc.id_goods_classifier,'00000000-0000-0000-0000-000000000000'), p.id_producer_global,
        g.id_goods_global
        ,goods_name = max(g.name)
        ,code = max(g.code)
        ,id_goods_classifier = ISNULL(gc.id_goods_classifier,'00000000-0000-0000-0000-000000000000')
        ,goods_classifier_name = max(ISNULL(gc.name,''))
        ,id_producer = p.id_producer_global
        ,producer_name = max(p.name)
        ,min_qty = max(g.quantity_min)
        ,qty_sub = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)
        ,supplier_sum = sum(lm.sum_sup)
        ,retail_sum = sum(lm.sum_acc)
        ,remain = 0--convert(money, null)
        ,last_price = 0--convert(money, null)
        ,last_price_date = null--convert(datetime, null)
        ,id_supplier = null--convert(uniqueidentifier, null)
        ,supplier_name = ''--convert(varchar(100), null)
        ,on_the_way = 0--convert(money, null)
        ,pribil = isnull(sum(lm.sum_acc),0) - isnull(sum(lm.sum_sup),0)
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
        ,days_period = @DAYS_PERIOD
        ,qty_sub_per_day = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)/@DAYS_PERIOD
        ,rate_oborot = 0
        ,zakazat = 0
        ,remain_begin = 0

    from lot_movement lm
    inner join lot l on l.id_lot_global = lm.id_lot_global
    inner join store s on s.id_store = l.id_store
    inner join goods g on g.id_goods = l.id_goods
    inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
    inner join producer p on p.id_producer = g.id_producer
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    where code_op IN (SELECT SUB_OP_TYPE FROM #SUB_OP_TYPE)
    and date_op between @date_from and @date_to
    and ((@all_store=1 or s.id_store in (select id_store from #store))
    or (s.id_contractor = @id_contractor or @id_contractor = 1))
    and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
    and (@all_goods_kind=1 or g.id_goods_kind in (select id_goods_kind from #GOODS_KIND))
--    and (l.id_store in (select id_store from store where id_contractor=@id_contractor))
    group by g.id_goods_global, ISNULL(gc.id_goods_classifier,'00000000-0000-0000-0000-000000000000'), p.id_producer_global


--select * from goods_classifier
--return
 print 'Chekpoint2'

    insert into #all_old
    select 
        g.id_goods_global
        ,goods_name = max(g.name)
        ,code = max(g.code)
        ,id_goods_classifier = gc.id_goods_classifier
        ,goods_classifier_name = max(gc.name)
        ,id_producer = p.id_producer_global
        ,producer_name = max(p.name)
        ,min_qty = max(g.quantity_min)
        ,qty_sub = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)
        ,supplier_sum = sum(lm.sum_sup)
        ,retail_sum = sum(lm.sum_acc)
        ,remain = convert(money, null)
        ,last_price = convert(money, null)
        ,last_price_date = convert(datetime, null)
        ,id_supplier = convert(uniqueidentifier, null)
        ,supplier_name = convert(varchar(100), null)
        ,on_the_way = convert(money, null)
        ,pribil = isnull(sum(lm.sum_acc),0) - isnull(sum(lm.sum_sup),0)
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
/*      ,days_period = @DAYS_PERIOD
        ,days_period = 0
        ,qty_sub_per_day = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)/@DAYS_PERIOD
        ,qty_sub_per_day = 0
        ,rate_oborot = 0
        ,zakazat = 0  */
    from lot_movement lm
    inner join lot l on l.id_lot_global = lm.id_lot_global
    inner join store s on s.id_store = l.id_store
    inner join goods g on g.id_goods = l.id_goods
    inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
    inner join producer p on p.id_producer = g.id_producer
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    where code_op IN (SELECT SUB_OP_TYPE FROM #SUB_OP_TYPE)
    and date_op between @date_from_old and @date_to_old
    and ((@all_store=1 or s.id_store in (select id_store from #store))
    or (s.id_contractor = @id_contractor or @id_contractor = 1))
    and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
    and (@all_goods_kind=1 or g.id_goods_kind in (select id_goods_kind from #GOODS_KIND))
--    and (l.id_store in (select id_store from store where id_contractor=@id_contractor))
    group by g.id_goods_global, gc.id_goods_classifier, p.id_producer_global


print 'Chekpoint3'

/*    insert into #all
    select
        g.id_goods_global
        ,goods_name = g.name
        ,code = g.code
        ,id_goods_classifier = gc.id_goods_classifier
        ,goods_classifier_name = gc.name
        ,id_producer = p.id_producer_global
        ,producer_name = p.name
        ,min_qty = g.quantity_min
        ,qty_sub = 0
        ,supplier_sum = 0
        ,retail_sum = 0
        ,remain = convert(money, null)
        ,last_price = convert(money, null)
        ,last_price_date = convert(datetime, null)
        ,id_supplier = convert(uniqueidentifier, null)
        ,supplier_name = convert(varchar(100), null)
        ,on_the_way = convert(money, null)
        ,pribil = 0
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
        ,days_period = 0
        ,qty_sub_per_day = 0
        ,rate_oborot = 0
        ,zakazat = 0  
        ,remain_begin = 0
    from goods g
    inner join producer p on p.id_producer = g.id_producer
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    where g.id_goods_global not in (select id_goods_global from #all)
    and (/*@all_goods=1 or */g.id_goods in (select id_goods from #goods))
*/

  print 'Chekpoint4'   

/*    insert into #all_old
    select
        g.id_goods_global
        ,goods_name = g.name
        ,code = g.code
        ,id_goods_classifier = gc.id_goods_classifier
        ,goods_classifier_name = gc.name
        ,id_producer = p.id_producer_global
        ,producer_name = p.name
        ,min_qty = g.quantity_min
        ,qty_sub = 0
        ,supplier_sum = 0
        ,retail_sum = 0
        ,remain = convert(money, null)
        ,last_price = convert(money, null)
        ,last_price_date = convert(datetime, null)
        ,id_supplier = convert(uniqueidentifier, null)
        ,supplier_name = convert(varchar(100), null)
        ,on_the_way = convert(money, null)
        ,pribil = 0
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
      /*  ,days_period = @DAYS_PERIOD
        ,qty_sub_per_day = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)/@DAYS_PERIOD
        ,rate_oborot = 0
        ,zakazat = 0  */
    from goods g
    inner join producer p on p.id_producer = g.id_producer
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    where g.id_goods_global not in (select id_goods_global from #all_old)
    and (/*@all_goods=1 or */g.id_goods in (select id_goods from #goods))

*/

 print 'Chekpoint5'   

/*    insert into #all
    select
        g.id_goods_global
        ,goods_name = g.name
        ,code = g.code
        ,id_goods_classifier = gc.id_goods_classifier
        ,goods_classifier_name = gc.name
        ,id_producer = p.id_producer_global
        ,producer_name = p.name
        ,min_qty = g.quantity_min
        ,qty_sub = 0
        ,supplier_sum = 0
        ,retail_sum = 0
        ,remain = convert(money, null)
        ,last_price = convert(money, null)
        ,last_price_date = convert(datetime, null)
        ,id_supplier = convert(uniqueidentifier, null)
        ,supplier_name = convert(varchar(100), null)
        ,on_the_way = convert(money, null)
        ,pribil = 0
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
        ,days_period = @DAYS_PERIOD
        ,qty_sub_per_day = 0
        ,rate_oborot = 0
        ,zakazat = 0  
        ,remain_begin = 0
    from goods g
    inner join producer p on p.id_producer = g.id_producer
    inner join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    inner join #all a on a.id_goods_classifier = gc.id_goods_classifier
    where not exists (select null from #all a1 where a1.id_goods=g.id_goods_global)
*/

print 'Chekpoint6'
/*    insert into #all_old
    select
        g.id_goods_global
        ,goods_name = g.name
        ,code = g.code
        ,id_goods_classifier = gc.id_goods_classifier
        ,goods_classifier_name = gc.name
        ,id_producer = p.id_producer_global
        ,producer_name = p.name
        ,min_qty = g.quantity_min
        ,qty_sub = 0
        ,supplier_sum = 0
        ,retail_sum = 0
        ,remain = convert(money, null)
        ,last_price = convert(money, null)
        ,last_price_date = convert(datetime, null)
        ,id_supplier = convert(uniqueidentifier, null)
        ,supplier_name = convert(varchar(100), null)
        ,on_the_way = convert(money, null)
        ,pribil = 0
        ,abc = ''
        ,aim_percent = 0
        ,Total_aim_percent = 0
     /*   ,days_period = @DAYS_PERIOD
        ,qty_sub_per_day = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)/@DAYS_PERIOD
        ,rate_oborot = 0
        ,zakazat = 0  */
    from goods g
    inner join producer p on p.id_producer = g.id_producer
    inner join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    inner join #all_old a on a.id_goods_classifier = gc.id_goods_classifier
    where not exists (select null from #all_old a1 where a1.id_goods=g.id_goods_global)
*/
print 'Chekpoint7'
    -- остатки на конец периода
    update a set
        remain = r.qty_remain
    from #all a
    inner join (select 
                    g.id_goods_global
                    ,qty_remain = sum((lm.quantity_add - lm.quantity_sub - lm.quantity_res) * convert(money, sr.numerator) / sr.denominator)
                from lot l
                inner join store s on s.id_store = l.id_store    
                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
                inner join goods g on g.id_goods = l.id_goods
                inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
                where g.id_goods_global in  (select id_goods from #all)
                and ((@all_store=1 or s.id_store in (select id_store from #store))
                or (s.id_contractor = @id_contractor or @id_contractor = 1))
                and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
--                and (l.id_store in (select id_store from store where id_contractor=@id_contractor)) 
                and lm.date_op <=@date_to
                group by g.id_goods_global 
               ) r ON r.id_goods_global = a.id_goods


    -- остатки на начало периода
    update a set
        remain_begin = r.qty_remain
    from #all a
    inner join (select 
                    g.id_goods_global
                    ,qty_remain = sum((lm.quantity_add - lm.quantity_sub - lm.quantity_res) * convert(money, sr.numerator) / sr.denominator)
                from lot l
                inner join store s on s.id_store = l.id_store    
                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
                inner join goods g on g.id_goods = l.id_goods
                inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
                where g.id_goods_global in  (select id_goods from #all)
                and ((@all_store=1 or s.id_store in (select id_store from #store))
                or (s.id_contractor = @id_contractor or @id_contractor = 1))
                and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
--                and (l.id_store in (select id_store from store where id_contractor=@id_contractor)) 
                and lm.date_op <=@date_from
                group by g.id_goods_global 
               ) r ON r.id_goods_global = a.id_goods

     
    -- последние цены и поставщик
    update a set
        last_price = lp.price_sup,
        id_supplier = c.id_contractor_global,
        last_price_date = lp.date_op,
        supplier_name = c.name
    from #all a
    inner join (select 
                    g.id_goods_global,
                    l.id_supplier,
                    lm.date_op,
                    price_sup = price_sup * convert(money, sr.denominator) / sr.numerator
                from lot l
                inner join store s on s.id_store = l.id_store
                inner join goods g on g.id_goods = l.id_goods
                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global and lm.id_document = l.id_document and lm.id_document_item = l.id_document_item
                inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio 
                where lm.date_op = (select 
                                        date_op = max(date_op)
                                    from lot l1
                                    inner join store s1 on s1.id_store = l1.id_store 
                                    inner join goods g1 on g1.id_goods = l1.id_goods
                                    inner join lot_movement lm1 on lm1.id_lot_global = l1.id_lot_global and lm1.id_document = l1.id_document and lm1.id_document_item = l1.id_document_item
                                    where 
                                      ((@all_store=1 or s1.id_store in (select id_store from #store))
                                    or (s.id_contractor = @id_contractor or @id_contractor = 1))
                                    and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
                                    and (g1.id_goods_global = g.id_goods_global)
                                    and (g1.id_goods_global in (select id_goods from #all))
--                                    and (l.id_store in (select id_store from store where id_contractor=@id_contractor))
                                    and lm.date_op<=@date_to)
                and ((@all_store=1 or s.id_store in (select id_store from #store))
                or (s.id_contractor = @id_contractor or @id_contractor = 1))
                and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
                and (g.id_goods_global in (select id_goods from #all))
--                and (l.id_store in (select id_store from store where id_contractor=@id_contractor))
    ) lp on lp.id_goods_global = a.id_goods
    inner join contractor c on c.id_contractor = lp.id_supplier
 
    update a set
        on_the_way = r.on_the_way
    from #all a
    inner join (select 
                   ri.id_goods,
                   on_the_way = sum(rim.qty_add-rim.qty_sub)     
                from request_item ri 
                inner join request_item_movement rim on rim.id_request_item = ri.id_request_item
                inner join request r on r.id_request = ri.id_request
                where ri.id_goods in (select id_goods from #all)
                and ((@all_store=1 or r.id_store in (select id_store from #store))
                or (r.id_contractor = @id_contractor or @id_contractor = 1))
                and r.doc_date<=@date_to
                group by ri.id_goods) r on a.id_goods=r.id_goods

    delete from #all
    where isnull(qty_sub,0)=0 
    and isnull(remain,0)=0
    and id_goods_classifier is not null
    and isnull(@all_goods,0)=0

    insert into #all_old2
    select 
        g.id_goods_global
        ,qty_sub = sum(lm.quantity_sub * convert(money, sr.numerator) /sr.denominator)
    from lot_movement lm
    inner join lot l on l.id_lot_global = lm.id_lot_global
    inner join store s on s.id_store = l.id_store
    inner join goods g on g.id_goods = l.id_goods
    inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
    inner join producer p on p.id_producer = g.id_producer
    left join goods_classifier_2_goods gc2g on gc2g.id_goods = g.id_goods_global
    left join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    where code_op IN (SELECT SUB_OP_TYPE FROM #SUB_OP_TYPE)
    and date_op between @date_from_old2 and @date_to_old2
    and ((@all_store=1 or s.id_store in (select id_store from #store))
    or (s.id_contractor = @id_contractor or @id_contractor = 1))
    and (@all_goods=1 or l.id_goods in (select id_goods from #goods))
    and (@all_goods_kind=1 or g.id_goods_kind in (select id_goods_kind from #GOODS_KIND))
--    and (l.id_store in (select id_store from store where id_contractor=@id_contractor))
    group by g.id_goods_global, gc.id_goods_classifier, p.id_producer_global



    select @total_qty_sub = isnull(sum(qty_sub),1), @total_retail_sum = isnull(sum(retail_sum),1), @total_pribil = isnull(sum(pribil),1)
    from #all

    if (@total_qty_sub = 0) set @total_qty_sub = 1
    if (@total_retail_sum = 0) set @total_retail_sum = 1
    if (@total_pribil = 0) set @total_pribil = 1

print 'Chekpoint8'
   
   update #all set
     aim_percent = CASE @TypeGroup
         WHEN 'Qty' THEN (qty_sub/@total_qty_sub)*100
         WHEN 'Sum' THEN (retail_sum/@total_retail_sum)*100
         WHEN 'Profit' THEN (pribil/@total_pribil)*100                  
     END,   
     rate_oborot = CASE (remain + remain_begin)
         WHEN 0 THEN 1
         ELSE qty_sub / ((remain + remain_begin)/2.0)
      END,
     last_price = CASE (qty_sub)
         WHEN 0 THEN 1
         ELSE  retail_sum / qty_sub
     END

-- Declare the variables to store the values returned by FETCH.
DECLARE @r_qty_sub money, @r_retail_sum money, @r_pribil money, @r_aim_percent money,
        @r_remain money, @r_qty_sub_a_old money, @r_qty_sub_a_old2 money, @zakazat money,
 @Total_temp_aim_percent money,
 @Total_aim_percent money,
 @Perc INT,
 @v_abc nvarchar(1),
 @r_abc nvarchar(1)


set @Total_temp_aim_percent = 0
set @Total_aim_percent = 0
set @Perc = @APerc
set @v_abc = 'A'

print 'Chekpoint9'   

DECLARE abc_cursor CURSOR FOR
SELECT isnull(a.qty_sub,0), isnull(a.retail_sum,0), isnull(a.pribil,0), isnull(a.aim_percent,0), isnull(a.remain,0), isnull(a_old.qty_sub,0), isnull(a_old2.qty_sub,0)
from #all a
left join  #all_old a_old on (a.id_goods = a_old.id_goods)
left join  #all_old2 a_old2 on (a.id_goods = a_old2.id_goods)
ORDER BY a.aim_percent DESC
OPEN abc_cursor

-- Perform the first fetch and store the values in variables.
-- Note: The variables are in the same order as the columns
-- in the SELECT statement. 
print 'Chekpoint91' 
FETCH NEXT FROM abc_cursor
INTO @r_qty_sub, @r_retail_sum, @r_pribil, @r_aim_percent, @r_remain, @r_qty_sub_a_old, @r_qty_sub_a_old2
-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN
   set @Total_temp_aim_percent = @Total_temp_aim_percent + @r_aim_percent
   set @Total_aim_percent = @Total_aim_percent + @r_aim_percent

   if (@Total_temp_aim_percent <= @Perc)
   begin
      set @r_abc = @v_abc 
   end
   else
   begin
     if (@v_abc = 'A')
     begin
       set @Total_temp_aim_percent  = @r_aim_percent
       set @Perc = @BPerc 
       set @v_abc = 'B'
       set @r_abc = @v_abc
     end
     else
       if (@v_abc = 'B')
       begin
         set @Total_temp_aim_percent  = @r_aim_percent
         set @Perc = @CPerc 
         set @v_abc = 'C'
         set @r_abc = @v_abc
       end
   end   

  set @zakazat = case @r_abc
     when 'A' THEN ((@r_qty_sub + @r_qty_sub_a_old + @r_qty_sub_a_old2)/3.0)*0.5 - @r_remain
     when 'B' THEN ((@r_qty_sub + @r_qty_sub_a_old + @r_qty_sub_a_old2)/3.0)*(3.0/4.0) - @r_remain
     when 'C' THEN 0
  END
  

   UPDATE #all SET
     abc = @r_abc,
     Total_aim_percent = @Total_aim_percent,
     zakazat = @zakazat
   FROM #all
   WHERE CURRENT OF abc_cursor;

   -- This is executed as long as the previous fetch succeeds.
   FETCH NEXT FROM abc_cursor
   INTO @r_qty_sub, @r_retail_sum, @r_pribil, @r_aim_percent, @r_remain, @r_qty_sub_a_old, @r_qty_sub_a_old2
END

CLOSE abc_cursor
DEALLOCATE abc_cursor


--
print 'Chekpoint10'   

if @SORTKIND = 'SORTAIMPERC'
begin
    select 
        a.*, 
        isnull(a_old.qty_sub,0) qty_sub_old,
        isnull(a_old.supplier_sum,0) supplier_sum_old,
        isnull(a_old.retail_sum,0) retail_sum_old,
        isnull(a_old.pribil,0) pribil_old,
        isnull(a_old2.qty_sub,0) qty_sub_old2
    from #all a
    left join  #all_old a_old on (a.id_goods = a_old.id_goods)
    left join  #all_old2 a_old2 on (a.id_goods = a_old2.id_goods)
    order by a.Total_aim_percent asc
end
else
begin
    select 
        a.*, 
        isnull(a_old.qty_sub,0) qty_sub_old,
        isnull(a_old.supplier_sum,0) supplier_sum_old,
        isnull(a_old.retail_sum,0) retail_sum_old,
        isnull(a_old.pribil,0) pribil_old,
        isnull(a_old2.qty_sub,0) qty_sub_old2
    from #all a
    left join  #all_old a_old on (a.id_goods = a_old.id_goods)
    left join  #all_old2 a_old2 on (a.id_goods = a_old2.id_goods)
    order by a.abc, a.goods_name asc
end  
RETURN 0
GO

/*
SET NOCOUNT ON
exec REP_ABC_REQUEST_CALCULATION_CALC_NEEDS @xmlParam=N'
<XML>
<DAYS_PERIOD>120</DAYS_PERIOD>
<SUB_OP_TYPE>CHEQUE</SUB_OP_TYPE>
<SUB_OP_TYPE>INVOICE_OUT</SUB_OP_TYPE>
<SUB_OP_TYPE>MOVE</SUB_OP_TYPE>
<ID_CONTRACTOR>6287</ID_CONTRACTOR>
<DATE_TO>23.01.2014 17:43:13</DATE_TO>
<DATE_TO_OLD>25.09.2013</DATE_TO_OLD>
<DATE_TO_OLD2>28.05.2013</DATE_TO_OLD2>
<SORTKIND>SORTAIMPERC</SORTKIND>
<APercent>75</APercent>
<BPercent>20</BPercent>
<CalcType>Sum</CalcType>
</XML>'
*/