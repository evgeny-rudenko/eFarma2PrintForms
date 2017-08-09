SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
-----------------------------------------------------------------------------------------
IF OBJECT_ID('STATIST_SALE_LOST_PROFIT_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE STATIST_SALE_LOST_PROFIT_EX AS RETURN')
    GRANT EXEC ON [STATIST_SALE_LOST_PROFIT_EX] TO [PUBLIC]
END
GO
ALTER PROCEDURE STATIST_SALE_LOST_PROFIT_EX
    @XMLPARAM NTEXT
AS

DECLARE	@SQL NVARCHAR(4000), @TOP NVARCHAR(4000), @G_ROWCOUNTALL BIGINT, @DATE DATETIME, @DATE_FROM2 DATETIME
DECLARE	@ALL_GOODS BIT, @ALL_STORE BIT, @ALL_TRADES BIT, @ALL_GROUPS BIT, @G_SUMOUTALL DECIMAL(18, 2), @G_SUMADDALL DECIMAL(18, 2)
DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME, @TYPE_REPORT TINYINT, @PERCENT TINYINT, @QUANTITY TINYINT, @CUR TINYINT
DECLARE	@ORDER_BY NVARCHAR(4000), @ROW_COUNT SMALLINT, @TYPE_OUT TINYINT, @PARTS BIT, @DAY_COUNT INT
DECLARE @USE_GOODS_REPORT_NAME BIT
DECLARE @ORDER NVARCHAR(4000)
DECLARE @GOODS TABLE(ID_GOODS BIGINT)
DECLARE @DATA TABLE(TABLES_DATA VARCHAR(16))
DECLARE @SORT_ORDER NVARCHAR(5)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
--        @PERCENT = [PERCENT],
--        @QUANTITY = QUANTITY,
        @ORDER_BY = ORDER_BY,
--        @ROW_COUNT = ROW_COUNT,
        @USE_GOODS_REPORT_NAME = USE_GOODS_REPORT_NAME,
		@SORT_ORDER = SORT_ORDER
	FROM OPENXML(@HDOC , '/XML') WITH(
		DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
--         [PERCENT] TINYINT 'PERCENT',
--         QUANTITY TINYINT 'QUANTITY',
         ORDER_BY NVARCHAR(4000) 'ORDER_BY',
--         ROW_COUNT SMALLINT 'ROW_COUNT',
        USE_GOODS_REPORT_NAME BIT 'USE_GOODS_REPORT_NAME',
		SORT_ORDER NVARCHAR(5) 'SORT_ORDER'
	)
    SELECT TYPE_NUM INTO #TYPE_OUT FROM OPENXML(@HDOC, '/XML/TYPE_OUT/TYPE_NUM') WITH(
        TYPE_NUM INT '.'
    )
    
	SELECT ID_STORE INTO #STORE FROM OPENXML(@HDOC , '/XML/STORE/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    )
    WHERE ID_STORE != 0

    IF @@ROWCOUNT = 0 SET @ALL_STORE = 1 ELSE SET @ALL_STORE = 0

    SELECT ID_GROUP INTO #GROUPS FROM OPENXML(@HDOC, '/XML/GROUPS/ID_GROUP') WITH(
        ID_GROUP BIGINT '.'
    )
    WHERE ID_GROUP !=0
    IF @@ROWCOUNT = 0 SET @ALL_GROUPS = 1 ELSE SET @ALL_GROUPS = 0    

    SELECT ID_TRADE_NAME INTO #TRADE_NAME FROM OPENXML(@HDOC, '/XML/TRADE_NAME/ID_TRADE_NAME') WITH(
        ID_TRADE_NAME BIGINT '.'
    )
    IF @@ROWCOUNT = 0 SET @ALL_TRADES = 1 ELSE SET @ALL_TRADES = 0
    
	SELECT ID_GOODS INTO #GOODS FROM OPENXML(@HDOC, '/XML/GOODS/ID_GOODS') WITH(
        ID_GOODS BIGINT '.'
    )
    WHERE ID_GOODS != 0
    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0

    INSERT INTO @GOODS(ID_GOODS)
    SELECT 
        ID_GOODS = T.ID_GOODS
    FROM(
        SELECT DISTINCT ID_GOODS
        FROM GOODS G
        WHERE @ALL_GOODS = 0 AND EXISTS(SELECT NULL FROM #GOODS GS WHERE GS.ID_GOODS = G.ID_GOODS)
--        AND EXISTS (SELECT NULL FROM #TRADE_NAME TN WHERE TN.ID_TRADE_NAME = G.ID_TRADE_NAME) 
        AND NOT(@ALL_GOODS = 1 AND @ALL_GROUPS = 0)
    
        UNION        

        SELECT DISTINCT ID_GOODS
        FROM GOODS_2_GROUP G2G
        WHERE EXISTS (SELECT NULL FROM #GROUPS G WHERE G.ID_GROUP = G2G.ID_GOODS_GROUP)
        AND NOT EXISTS(SELECT NULL FROM #GOODS G WHERE G.ID_GOODS = G2G.ID_GOODS)
        AND NOT(@ALL_GOODS = 0 AND @ALL_GROUPS = 1)
--        UNION
        
--         SELECT DISTINCT G.ID_GOODS
--         FROM GOODS G
--         WHERE EXISTS(SELECT NULL FROM #TRADE_NAME TN WHERE TN.ID_TRADE_NAME = G.ID_TRADE_NAME)
--         AND NOT EXISTS(SELECT NULL FROM @GOODS G2G WHERE G.ID_GOODS = G2G.ID_GOODS)

    )T			

    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0
	EXEC SP_XML_REMOVEDOCUMENT @HDOC
			
--	EXEC USP_RANGE_DAYS @DATE_FROM OUTPUT , @DATE_TO OUTPUT
	SET @DAY_COUNT = DATEDIFF(DAY , @DATE_FROM , @DATE_TO) + 1
	SET @DAY_COUNT = CASE WHEN @DAY_COUNT < 1 THEN 1 ELSE @DAY_COUNT END

    --таблица товаров,по которым было движение
    declare @lm_goods table(
        id_goods bigint primary key,
        name varchar(255),
   		G_CODE NVARCHAR(16) NULL,
        ID_TRADE_NAME  BIGINT
    )

    insert into @lm_goods
    select id_goods,name,g.code,ID_TRADE_NAME
    from goods g
    where exists (select null from lot l
              inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
              and l.id_goods = g.id_goods)

    --календарик
    DECLARE @T TABLE(
        DT DATETIME
    )
    DECLARE @C DATETIME
    SET @C = FLOOR(CONVERT(MONEY,@DATE_FROM))
    
    --записываем дни
    WHILE @C <= FLOOR(CONVERT(MONEY,@DATE_TO)) BEGIN
        INSERT INTO @T
        SELECT
            @C
        SET @C = FLOOR(CONVERT(MONEY,DATEADD(dd,1,@C)))-- CONVERT(DATETIME, LEFT(CONVERT(VARCHAR(20), DATEADD(dd,1,@C), 121),10) + ' 23:59:59.997', 121)
    END


--для подсчета остатков на каждый день
    declare @lm_move table(
        id_goods BIGINT,
        id_lot_global UNIQUEIDENTIFIER,
        id_document UNIQUEIDENTIFIER,
        code_op VARCHAR(16),
        date_op DATETIME,
        quantity_add MONEY,
        quantity_sub MONEY,
        quantity_res MONEY
    )
    insert into @lm_move
    select 
        l.id_goods, l.id_lot_global, lm.id_document, lm.code_op,lm.date_op,lm.quantity_add, lm.quantity_sub, lm.quantity_res 
    from lot l
    inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
    inner join @lm_goods g on g.id_goods = l.id_goods
-- --    left join doc_movement dm on dm.id_document=lm.id_document
--      where 
--          (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
--  --        (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
--     	AND 
--          (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where l.ID_GOODS = ID_GOODS))    
--          AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
--       	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP IN ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED')) --LM.CODE

--  SELECT * FROM @lm_move
-- 
-- select l.id_goods, l.id_lot_global, lm.date_op,lm.quantity_add, lm.quantity_sub, lm.quantity_res 
--                                from lot l
--                                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
--     inner join @lm_goods g on g.id_goods = l.id_goods
--       where 
--           (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
-- --  --        (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
--      	AND 
--           (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where l.ID_GOODS = ID_GOODS))    
--           AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
--       	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP IN ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED')) --LM.CODE




    INSERT INTO @DATA
    SELECT
        TABLES_DATA = CASE TYPE_NUM 
                            WHEN 1 THEN 'CHEQUE'
                            WHEN 2 THEN 'INVOICE_OUT'
                            WHEN 3 THEN 'MOVE' END
    FROM #TYPE_OUT

	CREATE TABLE #TABLE_DATA(
		G_ID BIGINT NULL,
		G_NAME NVARCHAR(256) NULL,    --наимен товара
		G_CODE NVARCHAR(16) NULL,
        G_QTYSALE DECIMAL(18, 4) NULL,    --продано
        G_QTYRETURN DECIMAL(18, 4) NULL,    --возвращено
        G_QTY_EXP DECIMAL(18, 4) NULL,    --расход
        G_PRICE_SUP DECIMAL(18,4) NULL,    --последняя цена постащика
        QDAYS_ZERO DECIMAL(18, 4) NULL,    --кол дней отсутствия на остатке        
        QDAYS_SALE DECIMAL(18,4) NULL,    --кол дней продаж
        QDAYS_OST DECIMAL (18,4) NULL,    --кол дней присутствия на остатке
        AVG_SALE DECIMAL (18,4) NULL,    --средняя скорость продаж      
        LOST_PROFIT DECIMAL (18,4) NULL --упущенная выгода
	)


    CREATE TABLE #TABLE_GROUP(
        ID_GOODS BIGINT,
        G_NAME VARCHAR(255),
        G_CODE NVARCHAR(16) NULL,	
    	G_QTYSALE MONEY,
    	G_QTYRETURN MONEY,
        --итого расход
        G_QTY_EXP MONEY, 
        G_PRICE_SUP MONEY,
        --дни,когда товара не было на остатках
        QDAYS_ZERO BIGINT,
        --дни,когда товар продавался
        QDAYS_SALE BIGINT,
        --дни,когда товар был на остатке
        QDAYS_OST BIGINT,
        --скорость продаж
        AVG_SALE DECIMAL(18,4),
        --упущенная выгода
        LOST_PROFIT MONEY
    )

    DECLARE @QTY_ZERO TABLE(
        id_goods BIGINT NULL,
        id_contractor BIGINT NULL,
        count_day BIGINT NULL
    )

    DECLARE @QTY_SALE TABLE(
        id_goods BIGINT NULL,
        id_contractor BIGINT NULL,
        count_day BIGINT NULL
    )

    DECLARE @QTY_OST TABLE(
        id_goods BIGINT NULL,
        QUANTITY DECIMAL(18,4) NULL,
        CALENDAR_DT DATETIME NULL
    )
--ДНИ,КОГДА ТОВАР БЫЛ НА ОСТАТКЕ
    --дни без учета приход-полный расход
    INSERT INTO @QTY_OST(id_goods,QUANTITY,CALENDAR_DT)
    select 
        id_goods,
        QUANTITY = sum(QUANTITY),
        CALENDAR_DT = CALENDAR_DT--count(distinct CALENDAR_DT)
    from (
            select 
               id_goods = lm.id_goods,
               QUANTITY = (isnull(lm.quantity_add,0) - isnull(lm.quantity_sub,0) - isnull(lm.quantity_res,0)),
               CALENDAR_DT = calendar.dt
            from (select c.dt from @t c) calendar
             left join (SELECT * FROM @lm_move
--(select l.id_goods, l.id_lot_global, lm.code_op,lm.date_op,lm.quantity_add, lm.quantity_sub, lm.quantity_res 
--                        from lot l
--                        inner join lot_movement lm on lm.id_lot_global = l.id_lot_global
--                        inner join @lm_goods g on g.id_goods = l.id_goods
--                        left join doc_movement dm on dm.id_document=lm.id_document
                        where 
-- --                             (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
                          (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = ID_STORE))
                         	AND (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where ID_GOODS = ID_GOODS))    
                             AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = ID_TRADE_NAME))
                         	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = CODE_OP)OR (CODE_OP in ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED'))) --LM.CODE_OP IN (SELECT TABLES_DATA FROM @DATA)--('INVOICE_OUT','CHEQUE','ACT_R2B')
                       ) 
                      lm 
--                left join doc_movement dm on dm.id_document=lm.id_document
                on FLOOR(CONVERT(MONEY,LM.DATE_OP)) <= calendar.dt
                --раб.день
            where exists(select null from lot_movement lm1 where lm1.code_op = 'cheque' and FLOOR(CONVERT(MONEY,LM1.DATE_OP))=calendar.dt)
--        and (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
    ) rt
    group by id_goods,calendar_dt
    having sum(QUANTITY)>0 

    --дни приход-расход
    insert into @QTY_OST(id_goods,QUANTITY,CALENDAR_DT)
    select 
           id_goods = lm.id_goods,
           QUANTITY = sum(isnull(lm.quantity_add,0) - isnull(lm.quantity_sub,0) - isnull(lm.quantity_res,0)),
           CALENDAR_DT =(calendar.dt)
    from (select c.dt from @t c) calendar
    left join (select l.id_goods, l.id_lot_global, lm.date_op,lm.quantity_add, lm.quantity_sub, lm.quantity_res 
               from lot l
               inner join lot_movement lm on lm.id_lot_global = l.id_lot_global 
               inner join @lm_goods g on g.id_goods = l.id_goods
--               left join goods g on g.id_goods = l.id_goods
--               left join doc_movement dm on dm.id_document=lm.id_document
               where (lm.quantity_add>0 or lm.quantity_sub<0) and
                    (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
--                    (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
                	AND (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where l.ID_GOODS = ID_GOODS))    
                    AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
                	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP='ACT_R2B') --LM.CODE_OP IN (SELECT TABLES_DATA FROM @DATA)--('INVOICE_OUT','CHEQUE','ACT_R2B')
--                    AND (EXISTS (select id_goods from lot l1 inner join lot_movement lm1 on lm1.id_lot_global = l1.id_lot_global where l1.id_goods = g.id_goods))
               )lm on FLOOR(CONVERT(MONEY,LM.DATE_OP)) = calendar.dt
    where (lm.id_goods is not null) and (not exists(select null from @QTY_OST where id_goods = lm.id_goods and CALENDAR_DT = calendar.dt))
                    and exists(select null from doc_movement dm where id_table = 19 and FLOOR(CONVERT(MONEY,dm.DATE_OP))=calendar.dt)
    group by lm.id_goods,calendar.dt
    --having sum(isnull(lm.quantity_add,0) - isnull(lm.quantity_sub,0) - isnull(lm.quantity_res,0))>0 ) 
    order by lm.id_goods

-- SELECT 
-- ID_GOODS_QTY = ((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),
-- LM.code_op,
-- LM.DATE_OP,
-- NUMERATOR = SR.NUMERATOR,
-- DENOMINATOR = CONVERT(MONEY, SR.DENOMINATOR)
-- from @lm_GOODS G
-- left join lot l on l.id_goods = g.id_goods
-- LEFT JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
-- LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO   
-- WHERE G.ID_GOODS = 188813 AND (LM.DATE_OP BETWEEN @DATE_FROM AND @DATE_TO)
--     AND (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
-- 	AND (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where l.ID_GOODS = ID_GOODS))    
--     AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
-- 	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP IN ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED'))

declare @last_inv table(
    name varchar(255),
    id_goods bigint,
    ID_LOT_GLOBAL uniqueidentifier,
    DATE_OP datetime,
    price_sal money,
    price_sup money
)
    insert into @last_inv
        SELECT -- цена последнего прихода
            G.NAME,
            L2.ID_GOODS,
            L2.ID_LOT_GLOBAL,
            LM2.DATE_OP,
            PRICE_SAL = L2.PRICE_SAL,
			PRICE_SUP = L2.PRICE_SUP
        FROM LOT L2
        INNER JOIN GOODS G ON G.ID_GOODS = L2.ID_GOODS
        INNER JOIN LOT_MOVEMENT LM2 ON LM2.ID_LOT_GLOBAL = L2.ID_LOT_GLOBAL
                                  AND LM2.ID_DOCUMENT = L2.ID_DOCUMENT
                                  AND LM2.ID_DOCUMENT_ITEM = L2.ID_DOCUMENT_ITEM

        WHERE LM2.CODE_OP IN ('INVOICE','IMPORT_REMAINS')--LM2.CODE_OP NOT IN ('ACT_REV', 'ACT_DIS')
		AND (SELECT 
			     COUNT(*)
			 FROM LOT L1
			 INNER JOIN LOT_MOVEMENT LM1 ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
			       	                    AND LM1.ID_DOCUMENT = L1.ID_DOCUMENT
					                    AND LM1.ID_DOCUMENT_ITEM = L1.ID_DOCUMENT_ITEM
		     WHERE L1.ID_GOODS = L2.ID_GOODS AND LM1.DATE_OP<=@DATE_TO AND LM1.CODE_OP IN ('INVOICE','IMPORT_REMAINS')
			     AND (LM1.DATE_OP>LM2.DATE_OP
		     OR (LM1.DATE_OP=LM2.DATE_OP AND LM1.ID_LOT_MOVEMENT >= LM2.ID_LOT_MOVEMENT))
			 GROUP BY L1.ID_GOODS)=1

CREATE TABLE #FINAL(
    ID_GOODS BIGINT,
    G_NAME VARCHAR(255),
    G_CODE NVARCHAR(16) NULL,	
	G_QTYSALE MONEY,
	G_QTYRETURN MONEY,
    --итого расход
    G_QTY_EXP MONEY, 
    G_PRICE_SUP MONEY,
    --дни,когда товара не было на остатках
    QDAYS_ZERO BIGINT,
    --дни,когда товар продавался
    QDAYS_SALE BIGINT,
    --дни,когда товар был на остатке
    QDAYS_OST BIGINT,
    --скорость продаж
    AVG_SALE DECIMAL(18,4),
    --упущенная выгода
    LOST_PROFIT MONEY
)
INSERT INTO #FINAL
select
    ID_GOODS = G.ID_GOODS,
    G_NAME = G.NAME,	
    G_CODE = min(G.G_CODE),
--    l.id_supplier,	
	G_QTYSALE = SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),
	G_QTYRETURN = SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),
    --итого расход
    G_QTY_EXP = (SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))-
                SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))), 
    G_PRICE_SUP = MAX(B.PRICE_SUP),
    --дни,когда товара не было на остатках
    QDAYS_ZERO = MAX(ISNULL(QZER.QDAYS,0)),
    --дни,когда товар продавался
    QDAYS_SALE = MAX(ISNULL(prod.QDAYS_SALE,0)),
    --дни,когда товар был на остатке
    QDAYS_OST = MAX(ISNULL(OST.QDAYS_OST,0)),
    --скорость продаж
    AVG_SALE = CASE WHEN MAX(ISNULL(OST.QDAYS_OST,0))!=0 THEN (SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))-
                SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)))/MAX(ISNULL(OST.QDAYS_OST,0)) ELSE 0 END,
    --упущенная выгода
    LOST_PROFIT = CASE WHEN MAX(ISNULL(OST.QDAYS_OST,0))!=0 THEN (SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))-
                SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)))/MAX(ISNULL(OST.QDAYS_OST,0)) * MAX(ISNULL(QZER.QDAYS,0)) * MAX(B.PRICE_SUP) ELSE 0 END
from @lm_GOODS G
left join lot l on l.id_goods = g.id_goods
LEFT JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO   
----left join doc_movement dm on dm.id_document=lm.id_document
LEFT JOIN @last_inv b on B.ID_GOODS = G.ID_GOODS -- цена последнего прихода
LEFT JOIN (select id_goods, QDAYS=count(dt) --дни,когда товара не было на остатках
            from (      
                   select
                      calendar.id_goods,
                      calendar.dt
                   from (select c.dt, g.id_goods
                         from @t c, @LM_GOODS g) calendar
                   left join @lm_move lm on FLOOR(CONVERT(MONEY,LM.DATE_OP)) <= calendar.dt
-- (select l.id_goods, l.id_lot_global, lm.date_op,lm.quantity_add, lm.quantity_sub, lm.quantity_res 
--                                from lot l
--                                inner join lot_movement lm on lm.id_lot_global = l.id_lot_global)                                         
                         and lm.id_goods = calendar.id_goods
                    where not exists(select null from @QTY_OST q where q.id_goods = calendar.id_goods and calendar.dt = CALENDAR_DT)
                --раб.день
                    and exists(select null from doc_movement dm where id_table = 19 and FLOOR(CONVERT(MONEY,dm.DATE_OP))=calendar.dt)
                    group by calendar.dt, calendar.id_goods
                    having sum(isnull(lm.quantity_add,0) - isnull(lm.quantity_sub,0) - isnull(lm.quantity_res,0))=0
              ) a group by id_goods
          )qzer on qzer.id_goods = g.id_goods
LEFT JOIN (--дни,когда товар продавался
            select
                QDAYS_SALE = count(distinct c.dt),
                id_goods
            from (select id_goods,name,dt from @t,goods) c
            where exists(select id_goods from lot l inner join lot_movement lm on lm.id_lot_global = l.id_lot_global and id_goods = c.id_goods and FLOOR(CONVERT(MONEY,LM.DATE_OP)) = c.dt 
                         where lm.code_op in ('cheque','invoice_out','movement'))
                --раб.день
                    and exists(select null from doc_movement dm where id_table = 19 and FLOOR(CONVERT(MONEY,dm.DATE_OP))=c.dt)
            group by id_goods,name
          ) prod on prod.id_goods = g.id_goods
LEFT JOIN ( --дни,когда товар был на остатке
            Select     
                id_goods,
                QDAYS_OST = count(distinct CALENDAR_DT)
            from @QTY_OST
            group by id_goods
         )OST on OST.ID_GOODS = G.ID_GOODS
where exists(select id_goods from lot l inner join lot_movement lm on lm.id_lot_global = l.id_lot_global and g.id_goods=l.id_goods)
    AND (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
--    and (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
	AND (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where l.ID_GOODS = ID_GOODS))    
    AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP IN ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED')) --LM.CODE_OP IN (SELECT TABLES_DATA FROM @DATA)--('INVOICE_OUT','CHEQUE','ACT_R2B')
and (LM.DATE_OP BETWEEN @DATE_FROM AND @DATE_TO)
group by G.ID_GOODS,G.NAME
having  CASE WHEN MAX(ISNULL(OST.QDAYS_OST,0))!=0 THEN (SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))-
                SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)))/MAX(ISNULL(OST.QDAYS_OST,0)) * MAX(ISNULL(QZER.QDAYS,0)) * MAX(B.PRICE_SUP) ELSE 0 END > 0

	IF (@USE_GOODS_REPORT_NAME = 1)
	BEGIN

	    UPDATE #FINAL SET
	        G_NAME = ISNULL(GC.NAME, G_NAME),
	        G_CODE = G.CODE
	    FROM GOODS G 
 	    LEFT JOIN GOODS_CLASSIFIER_2_GOODS GC2G ON GC2G.ID_GOODS = G.ID_GOODS_GLOBAL
	    LEFT JOIN GOODS_CLASSIFIER GC ON GC.ID_GOODS_CLASSIFIER = GC2G.ID_GOODS_CLASSIFIER
	    WHERE G.ID_GOODS = #FINAL.ID_GOODS

        INSERT INTO #TABLE_GROUP
		SELECT 
			ID_GOODS = MAX(TD.ID_GOODS),
            G_NAME,	
            min(G_CODE) as G_CODE,
        	G_QTYSALE = SUM(TD.G_QTYSALE),
        	G_QTYRETURN = SUM(TD.G_QTYRETURN),
            --итого расход
            G_QTY_EXP = SUM(TD.G_QTY_EXP), 
            G_PRICE_SUP = SUM(G_PRICE_SUP),
            --дни,когда товара не было на остатках
            QDAYS_ZERO = SUM(QDAYS_ZERO),
            --дни,когда товар продавался
            QDAYS_SALE = SUM(QDAYS_SALE),
            --дни,когда товар был на остатке
            QDAYS_OST = SUM(QDAYS_OST),
            --скорость продаж
            AVG_SALE = SUM(AVG_SALE),
            --упущенная выгода
            LOST_PROFIT = SUM(LOST_PROFIT) 
--		INTO #TABLE_GROUP
		FROM #FINAL TD
		GROUP BY G_NAME
	END

    SET @SQL = 'SELECT ' + CASE WHEN @USE_GOODS_REPORT_NAME = 0 THEN ' * FROM #FINAL ORDER BY '
                        ELSE ' * FROM #TABLE_GROUP ORDER BY ' END + @ORDER_BY + ' ' + @SORT_ORDER
    exec (@SQL)

RETURN 0
GO

/*
exec STATIST_SALE_LOST_PROFIT_EX @xmlParam = N'
<XML>
	<DATE_FROM>2009-05-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2009-08-24T00:00:00.000</DATE_TO>
	<ORDER_BY>LOST_PROFIT</ORDER_BY>
	<TYPE_OUT>
		<TYPE_NUM>1</TYPE_NUM>
		<TYPE_NUM>2</TYPE_NUM>
		<TYPE_NUM>3</TYPE_NUM>
	</TYPE_OUT>
	<USE_GOODS_REPORT_NAME>1</USE_GOODS_REPORT_NAME>
	<STORE></STORE>
	<GOODS></GOODS>
	<GROUPS></GROUPS>
	<TRADE_NAME></TRADE_NAME>
	<SORT_ORDER>DESC</SORT_ORDER>
</XML>'*/


