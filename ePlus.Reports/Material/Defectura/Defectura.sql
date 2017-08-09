SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_DEFECTURA') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_DEFECTURA AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_DEFECTURA
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT

DECLARE @GROUPS BIT
DECLARE @OA BIT

DECLARE @DATE_FROM DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @WORK_DAYS INT

DECLARE @INS_RESERVE INT
DECLARE @DAYS INT
DECLARE @SORT VARCHAR(64) 

DECLARE @ALL_GOODS BIT
DECLARE @ALL_STORES BIT

DECLARE @ID_CONTRACTOR BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
SELECT
	@DATE_FROM = DATE_FROM,
	@DATE_TO = DATE_TO,
	@SORT = SORT,
	@OA = OA,
	@GROUPS = GROUPS,
	@INS_RESERVE = INS_RESERVE,
	@DAYS = [DAYS],
	@ID_CONTRACTOR = ID_CONTRACTOR
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FROM DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO',
	SORT VARCHAR(64) 'SORT',
	OA BIT 'OA',
	GROUPS BIT 'GROUPS',
	INS_RESERVE INT 'INS_RESERVE',
	[DAYS] INT 'DAYS',
	ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
)

SELECT * INTO #GOODS
FROM OPENXML(@HDOC, '/XML/ID_GOODS') WITH(ID_GOODS BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_GOODS = 1
ELSE SET @ALL_GOODS = 0

SELECT * INTO #STORES
FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(ID_STORE BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_STORES = 1
ELSE SET @ALL_STORES = 0

/* ФОРМИРУЕТСЯ ТАБЛИЦА С УДАЛЕНКАМИ */
declare @del table(
        id_goods bigint,
        id_store bigint,
        id_contractor bigint
    )

    insert into @del(
        id_goods,
        id_store,
        id_contractor
    )
    select distinct
        id_goods,
        id_store,
        id_contractor
    from defectura_deleted dd
    where dd.id_goods is not null
    and dd.id_goods_classifier is null

    insert into @del
    select distinct
        g.id_goods,
        dd.id_store,
        dd.id_contractor
    from defectura_deleted dd
    inner join goods_classifier_2_goods gc2g on gc2g.id_goods_classifier = dd.id_goods_classifier
    inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    inner join goods g on g.id_goods_global = gc2g.id_goods
    where dd.id_goods is null

	-- указанный контрагент
	AND DD.ID_CONTRACTOR = @ID_CONTRACTOR

    and dd.id_goods_classifier is not null
    and gc2g.date_deleted is null
    and gc.date_deleted is null
    and not exists (select null 
                    from @del d 
                    where d.id_goods = g.id_goods
                    and isnull(d.id_store,0)=isnull(dd.id_store,0)
                    and isnull(d.id_contractor,0)=isnull(dd.id_contractor,0))

    insert into @del(
        id_goods,
        id_store,
        id_contractor
    )
    select distinct 
        id_goods, 
        null,
        null
    from stop_list s
    where s.id_goods is not null
    and s.id_goods_classifier is null
    and not exists (select null 
                    from @del d 
                    where d.id_goods = s.id_goods
                    and d.id_store is null
                    and d.id_contractor is null)

    insert into @del(
        id_goods,
        id_store,
        id_contractor
    )
    select distinct 
        g.id_goods, 
        null,
        null
    from stop_list s
    inner join goods_classifier_2_goods gc2g on gc2g.id_goods_classifier = s.id_goods_classifier
    inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    inner join goods g on g.id_goods_global = gc2g.id_goods
    where s.id_goods_classifier is not null
    and s.id_goods is null
    and gc2g.date_deleted is null
    and gc.date_deleted is null
    and not exists (select null 
                    from @del d 
                    where d.id_goods = g.id_goods
                    and d.id_store is null
                    and d.id_contractor is null)
   

    create table #t(
        id bigint not null identity primary key,
        id_goods bigint,
        id_goods_classifier uniqueidentifier,
		--id_contractor bigint,
        rem money,
        [min] money,
        SOLD_DATE DATETIME, 
        SOLD_QTY MONEY, 
        
        cl_min money,
        cl_rem money,  
        GR_SOLD_QTY MONEY, 
    
        id_supplier bigint,
        date_op datetime,
        price_sup MONEY
    )

    create index #ix_#t$id_goods on #t(id_goods)
    create index #ix_#t$id_goods_classifier on #t(id_goods_classifier)
    
    declare @l table(
        id_lot_global uniqueidentifier not null primary key
    )

    insert into @l(id_lot_global)
    select id_lot_global
    from lot l
    inner join store s on s.id_store = l.id_store
    where 
    (@ALL_STORES = 1 OR (S.ID_STORE IN (SELECT ID_STORE FROM #STORES)))

	-- указанный контрагент
	AND S.ID_CONTRACTOR = @ID_CONTRACTOR

    and  
    exists (select null
                from lot_movement lm
                where lm.code_op in ('CHEQUE', 'INVOICE_OUT', 'MOVE')
                and date_op between @date_from and @date_to
                and lm.quantity_sub>0
                and lm.id_lot_global = l.id_lot_global)
    
    insert into #t(
        id_goods
    )
    select
        l.id_goods
    from @l t
    inner join lot l on l.id_lot_global = t.id_lot_global
    INNER JOIN GOODS g ON l.ID_GOODS = g.ID_GOODS AND ((@OA = 1 AND G.REQUIRIED = 1) OR ISNULL(@OA, 0) = 0)
    inner join store s on s.id_store = l.id_store
    and not exists (select null
                    from @del d
                    where d.id_goods = l.id_goods
                    and (d.id_store = l.id_store or d.id_store is null)
                    and (d.id_contractor = s.id_contractor or d.id_contractor is null))
    WHERE (@ALL_GOODS = 1 OR (L.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS)))
    AND (@ALL_STORES = 1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
    group by l.id_goods

    update t set
        id_goods_classifier = gc2g.id_goods_classifier,
        [min] = ISNULL(AP.QUANTITY_MIN, 0)--g.quantity_min
    from #t t
    inner join goods g on g.id_goods = t.id_goods AND ((@OA = 1 AND G.REQUIRIED = 1) OR ISNULL(@OA, 0) = 0)
	
	-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
	LEFT JOIN ASSORTMENT_PLAN AP ON T.ID_GOODS = AP.ID_GOODS AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
		AND AP.DATE_DELETED IS NULL
	
    left join (select
                   gc2g.id_goods,
                   gc2g.id_goods_classifier
               from goods_classifier_2_goods gc2g
               inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
               where 1=1
               and gc2g.date_deleted is null
               and gc.date_deleted is null
               ) gc2g on gc2g.id_goods = g.id_goods_global
    
    -- в случае группировки по аналогам, получение дополнительных товаров аналогов для учета их остатков
    insert into #t(
        id_goods,
        id_goods_classifier,
        [min]
    )
    select
        g.id_goods,
        gc2g.id_goods_classifier,
        ISNULL(AP.QUANTITY_MIN, 0)--g.quantity_min
    from goods_classifier_2_goods gc2g
    inner join goods_classifier gc on gc.id_goods_classifier = gc2g.id_goods_classifier
    inner join goods g on g.id_goods_global = gc2g.id_goods

	-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
	LEFT JOIN ASSORTMENT_PLAN AP ON G.ID_GOODS = AP.ID_GOODS AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
		AND AP.DATE_DELETED IS NULL

    where @GROUPS = 1
           AND ((@OA = 1 AND G.REQUIRIED = 1) OR ISNULL(@OA, 0) = 0)
    AND (@ALL_GOODS = 1 OR (G.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS)))
    and gc2g.date_deleted is null
    and gc.date_deleted is null
    and exists (select null 
                from #t t
                where t.id_goods_classifier =gc2g.id_goods_classifier)
    and not exists (select null
                    from #t t
                    where t.id_goods = g.id_goods)
    group by g.id_goods, gc2g.id_goods_classifier, AP.quantity_min
    
    -- заполнение остатков по товарам на конец указанного периода
    update t set
        rem = lm.rem
    from #t t
    inner join (select
                    l.id_goods,
                    rem = sum((lm.quantity_add - lm.quantity_sub - lm.quantity_res) * convert(money, sr.numerator) / sr.denominator)
                from lot l
                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
                inner join scaling_ratio sr on sr.id_scaling_ratio = l.id_scaling_ratio
                inner join store s on s.id_store = l.id_store

				-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
				LEFT JOIN ASSORTMENT_PLAN AP ON L.ID_GOODS = AP.ID_GOODS 
					AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
					AND AP.DATE_DELETED IS NULL

                where 
                (@ALL_STORES = 1 OR (S.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
                AND lm.date_op<=@date_to
                and not exists (select null
                                from @del d
                                where d.id_goods = l.id_goods
                                and (d.id_store = s.id_store or d.id_store is null)
                                and (d.id_contractor = s.id_contractor or d.id_contractor is null))
				-- УКАЗАННЫЙ КОНТРАГЕНТ
				AND s.id_contractor = @ID_CONTRACTOR
                group by l.id_goods, s.id_contractor) lm on lm.id_goods = t.id_goods
    
    -- заполнение количества и даты последней продажи
    update t set
        T.SOLD_DATE = SOLD.DATE_OP, 
        T.SOLD_QTY = SOLD.QUANTITY_SUB
    from #t t
    LEFT JOIN 
    (
		SELECT DISTINCT 
			L.ID_GOODS, 
			LM.DATE_OP, 
			Q.QUANTITY_SUB
		FROM LOT_MOVEMENT LM
		INNER JOIN LOT L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
		INNER JOIN SCALING_RATIO SR ON L.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
		-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
		LEFT JOIN ASSORTMENT_PLAN AP ON L.ID_GOODS = AP.ID_GOODS 
			AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
			AND AP.DATE_DELETED IS NULL
		LEFT JOIN
		(
			SELECT 
				L.ID_GOODS, 
				QUANTITY_SUB = SUM(LM.QUANTITY_SUB * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR)
			FROM LOT_MOVEMENT LM
			INNER JOIN LOT L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
			INNER JOIN SCALING_RATIO SR ON L.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
			-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
			LEFT JOIN ASSORTMENT_PLAN AP ON L.ID_GOODS = AP.ID_GOODS 
				AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
				AND AP.DATE_DELETED IS NULL
			where lm.code_op in ('CHEQUE', 'INVOICE_OUT')
				and lm.quantity_sub>0
				AND (@ALL_GOODS = 1 OR (L.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS)))
				AND (@ALL_STORES = 1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
				AND LM.DATE_OP BETWEEN @date_from and @date_to
			GROUP BY L.ID_GOODS
		) Q ON SR.ID_GOODS = Q.ID_GOODS
		where lm.code_op in ('CHEQUE', 'INVOICE_OUT')
		and lm.quantity_sub>0
		AND LM.DATE_OP = (SELECT MAX(LM1.DATE_OP) FROM LOT_MOVEMENT LM1
		INNER JOIN LOT L1 ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
		where lm1.code_op in ('CHEQUE', 'INVOICE_OUT')
		and lm1.quantity_sub>0
		AND L1.ID_GOODS = L.ID_GOODS
		)
		--AND (@ALL_GOODS = 1 OR (L.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS)))
		--AND (@ALL_STORES = 1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
    ) SOLD ON t.id_goods = SOLD.ID_GOODS
    
    
-----------получаем данные по приходам-----------------

select 
	lm.id_lot_movement,
	l.id_goods,
	lm.date_op,
	l.price_sup,
	l.id_supplier
into #t1
from lot_movement lm
inner join lot l on lm.id_lot_global = l.id_lot_global 
                                        and lm.id_document=l.id_document 
                                        and lm.id_document_item = l.id_document_item

inner join store s on s.id_store = l.id_store
-- ПО УКАЗАННОМУ КОНТРАГЕНТУ
		LEFT JOIN ASSORTMENT_PLAN AP ON L.ID_GOODS = AP.ID_GOODS 
			AND AP.ID_CONTRACTOR = @ID_CONTRACTOR 
			AND AP.DATE_DELETED IS NULL
where lm.code_op in ('INVOICE', 'IMPORT_REMAINS', 'MOVEMENT')
/*and 
        (@ALL_STORES = 1 OR (S.ID_STORE IN (SELECT ID_STORE FROM #STORES)))*/
        AND (@ALL_GOODS = 1 OR (L.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS)))
        and exists (select null
                    from #t t
                    where t.id_goods = l.id_goods)
order by l.id_goods

select * 
into #tmp1
from #t1 t
where not exists(select null from #t1 t1 where
t1.date_op > t.date_op 
and t.id_goods = t1.id_goods)
order by t.id_goods
-------------------------------------------------------


    -- получение данных о последней поставке для товара
    update t set
        id_supplier = tmp.id_supplier,
        date_op  = tmp.date_op,
        price_sup = tmp.price_sup
    from #t t
    inner join (
		select 
			id_goods,
			date_op,
			price_sup,
			id_supplier
		from #tmp1 t
		where not exists(select null from #tmp1 t1 where
		(t1.date_op = t.date_op and t1.id_lot_movement > t.id_lot_movement)
		and t.id_goods = t1.id_goods)
) tmp on tmp.id_goods = t.id_goods

    
    -- заполнение минимального остатка, суммы остатка и проданного количества для группы аналогов 
    update t set
        cl_min = t1.[min],
        cl_rem = t1.rem, 
        GR_SOLD_QTY = T1.SOLD_QTY
    from #t t
    inner join (select
                    t.id_goods_classifier,
                    [min] = max(t.[min]),
                    rem = sum(t.rem), 
                    SOLD_QTY = SUM(T.SOLD_QTY)
                from #t t
                where t.id_goods_classifier is not null
                group by t.id_goods_classifier) t1 on t1.id_goods_classifier = t.id_goods_classifier
    
    -- результирующая таблица
    declare @t1 table(
        id_goods_global uniqueidentifier,
        id_goods bigint,
        id_goods_classifier uniqueidentifier,
        goods varchar(255),
        [min] money,
        rem money, 
        SOLD_DATE DATETIME, 
        SOLD_QTY MONEY, 
        supplier varchar(100),
        date_op datetime,
        price_sup money,
        date_deleted datetime
    )

    insert into @t1(
        id_goods_global,
        id_goods,
        id_goods_classifier, 
        goods,
        [min],
        rem,
        supplier,
        date_op,
        price_sup, 
        SOLD_DATE, 
        SOLD_QTY
    )
    -- товары вне групп аналогов
    select
        g.id_goods_global,
        t.id_goods,
        null,
        goods = g.name,
        [min] = t.[min],
        rem = t.rem,
        supplier = sup.name,
        date_op,
        price_sup, 
        SOLD_DATE, 
        SOLD_QTY
    from #t t
    inner join goods g on g.id_goods = t.id_goods
    inner join producer p on p.id_producer = g.id_producer
    left join contractor sup on sup.id_contractor = t.id_supplier
    where ((@GROUPS = 1 and t.id_goods_classifier is null) or isnull(@GROUPS,0)=0)
    AND ((@OA = 1 AND G.REQUIRIED = 1) OR ISNULL(@OA, 0) = 0)
    union all
    -- группы аналогов
    select
        null,
        null,
        t.id_goods_classifier,
        gc.name,
        cl_min,
        cl_rem,
    
        supplier = NULL,
        NULL,
        NULL, 
        NULL, 
        GR_SOLD_QTY
    from #t t
    inner join goods_classifier gc on gc.id_goods_classifier = t.id_goods_classifier
    where (select count(*)
           from #t t1
           where t1.id_goods_classifier = t.id_goods_classifier
           and (isnull(t1.date_op,'1900-01-01')>isnull(t.date_op,'1900-01-01') or (isnull(t1.date_op,'1900-01-01')=isnull(t.date_op,'1900-01-01') and t1.id>t.id)))<1
    and gc.date_deleted is null
    and @GROUPS = 1 and t.id_goods_classifier is not NULL
    
    update t set
        date_deleted = dd.date_deleted
    from @t1 t,
    defectura_deleted dd
    where
    (
		(@ALL_STORES = 1 OR (dd.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
        or
        (dd.id_store is null)
    )
    and
    (
        (t.id_goods is not null and dd.id_goods = t.id_goods)
        or
        (t.id_goods_classifier is not null and dd.id_goods_classifier = t.id_goods_classifier)
    )
    
    -- ПОЛУЧАЕМ КОЛИЧЕСТВО РАБОЧИХ ДНЕЙ
SELECT 
	@WORK_DAYS = COUNT (DISTINCT CONVERT(DATETIME, LEFT(CONVERT(VARCHAR(20), LM.DATE_OP, 121),10), 121))
FROM LOT_MOVEMENT LM
INNER JOIN LOT L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
where lm.code_op in ('CHEQUE', 'INVOICE_OUT')
and lm.quantity_sub>0
AND LM.DATE_OP BETWEEN @date_from and @date_to
-- УКАЗАННЫЙ КОНТРАГЕНТ
AND EXISTS (SELECT NULL FROM STORE S WHERE S.ID_STORE = L.ID_STORE 
	AND S.ID_CONTRACTOR = @ID_CONTRACTOR)
    
    select 
        T.ID_GOODS,
        ID_GOODS_CLASSIFIER,
        GOODS_NAME = goods,
        QTY_MIN = CASE WHEN (@GROUPS = 1 AND ID_GOODS_CLASSIFIER IS NOT NULL) THEN NULL ELSE [min] END,
        QTY_REMAIN = rem,
        LAST_SUPPLIER = supplier,
        LAST_LOT_DATE = T.date_op,
        LAST_PRICE_SUP = price_sup,
        SOLD_DATE = T.SOLD_DATE, 
        SOLD_QTY = T.SOLD_QTY, 
        REST_ENOUGH_DAYS = CASE WHEN ROUND((rem/(SOLD_QTY/@WORK_DAYS)), 0) <= ROUND((rem/(SOLD_QTY/@WORK_DAYS)), 1) 
			THEN ROUND((rem/(SOLD_QTY/@WORK_DAYS)), 0) ELSE ROUND((rem/(SOLD_QTY/@WORK_DAYS)), 0) - 1 END, 
		GOODS_RESERVE = @DAYS * T.SOLD_QTY / @WORK_DAYS, 
		BUY_QTY = @DAYS * T.SOLD_QTY / @WORK_DAYS - T.rem, 
		BUY_SUMM = (@DAYS * T.SOLD_QTY / @WORK_DAYS - T.rem) * T.price_sup 
    INTO #RESULT
    from @t1 t
    where ((isnull(t.[min], 0))>=0 and t.rem<=t.[min]/*) or (isnull(t.[min],0)=0 and t.rem<=0)*/)
    AND t.date_deleted is NULL
    AND (ISNULL(@INS_RESERVE, 0) = 0 OR ISNULL(@GROUPS, 0) = 1 OR 
		(ISNULL(@INS_RESERVE, 0) > 0 AND ISNULL(T.[min], 0) <= @INS_RESERVE AND ISNULL(@GROUPS, 0) <> 1))
	AND @DAYS * T.SOLD_QTY / @WORK_DAYS - T.rem >= 0
    order by goods, [min], rem
    
    DROP TABLE #t
    
    DECLARE @SQL NVARCHAR(400)
    SET @SQL = 'SELECT * FROM #RESULT
    ORDER BY ' + ISNULL(@SORT, 'GOODS_NAME')
    
    PRINT @SQL
    EXEC SP_EXECUTESQL @SQL
    
    --DROP TABLE #RESULT
    
RETURN
GO

/*
exec DBO.REPEX_DEFECTURA N'
<XML>
	<DATE_FROM>2010-04-20T00:00:00.000</DATE_FROM>
	<DATE_TO>2010-12-31T18:33:57.968</DATE_TO>
	<SORT>GOODS_NAME</SORT>
	<OA>0</OA>
	<GROUPS>0</GROUPS>
	<DAYS>5</DAYS>
	<ID_CONTRACTOR>5297</ID_CONTRACTOR>
</XML>'*/