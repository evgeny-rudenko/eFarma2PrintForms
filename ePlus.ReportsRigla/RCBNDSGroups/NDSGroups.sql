SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO


IF OBJECT_ID('DBO.FN_LOT_MOVEMENT_OP') IS NULL EXEC ('CREATE FUNCTION DBO.FN_LOT_MOVEMENT_OP(@ID_LOT_MOVEMENT BIGINT) RETURNS VARCHAR AS BEGIN RETURN NULL END')
GO
ALTER FUNCTION DBO.FN_LOT_MOVEMENT_OP(
    @ID_LOT_MOVEMENT BIGINT)
RETURNS VARCHAR(45)
AS
BEGIN
    declare @op varchar(45)
    select @op = 
          CASE
              WHEN QUANTITY_ADD>0 AND QUANTITY_SUB=0 AND QUANTITY_RES=0 AND CODE_OP NOT IN ('PC_REBIND')
              THEN 'ADD'     -- Приход
              WHEN QUANTITY_ADD>0 AND QUANTITY_SUB=0 AND QUANTITY_RES=0 AND CODE_OP IN ('PC_REBIND')
              THEN 'REBIND_ADD'++CONVERT(VARCHAR(36), ID_LOT_GLOBAL)   -- Приход по распределению
              WHEN QUANTITY_ADD=0 AND QUANTITY_SUB>0 AND QUANTITY_RES=0 AND CODE_OP NOT IN ('PROD')
              THEN 'SUB'     -- Расход
              WHEN QUANTITY_ADD=0 AND QUANTITY_SUB>0 AND QUANTITY_RES=0 AND CODE_OP IN ('PROD')
              THEN 'PROD-SUB-'+CONVERT(VARCHAR(36), ID_LOT_GLOBAL)     -- Расход по производству
              WHEN QUANTITY_ADD=0 AND QUANTITY_SUB=0 AND QUANTITY_RES>0
              THEN 'RES'     -- Резерв
              WHEN QUANTITY_ADD=0 AND QUANTITY_SUB=0 AND QUANTITY_RES<0
              THEN 'SUB_RES' -- Снятие с резерва
              WHEN QUANTITY_ADD=0 AND QUANTITY_SUB<0 AND QUANTITY_RES=0
              THEN 'RETURN'  -- Возврат
              WHEN CODE_OP = 'INVENTORY_SVED' AND (QUANTITY_ADD - QUANTITY_SUB) > 0 
              then 'ADD'   --инвентаризация излишек
              WHEN CODE_OP = 'INVENTORY_SVED' AND (QUANTITY_ADD - QUANTITY_SUB) < 0 
              then 'SUB'   --инвентаризация недостача
              ELSE NULL      -- Непонятно
          END
    FROM LOT_MOVEMENT WHERE ID_LOT_MOVEMENT = @ID_LOT_MOVEMENT
RETURN @op
END
GO

IF OBJECT_ID('DBO.REPEX_NDS_GROUPS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_NDS_GROUPS AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_NDS_GROUPS
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @DATE DATETIME, @ID_AU bigint
DECLARE @ALL_CONTRACTOR BIT, @ALL_STORE BIT, @ALL_GOODS_GROUPS BIT
DECLARE @IS_NDS_SAL BIT, @CO BIT, @RESTS_ONLY BIT, @NO_GROUPS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @DATE = DATE, @CO = CO, @RESTS_ONLY = RESTS_ONLY, 
		@IS_NDS_SAL = IS_NDS_SAL, @NO_GROUPS = NO_GROUPS
	FROM OPENXML(@HDOC, '/XML') 
	WITH(DATE DATETIME 'DATE', CO BIT 'CO', 
		RESTS_ONLY BIT 'RESTS_ONLY', IS_NDS_SAL BIT 'IS_NDS_SAL', NO_GROUPS BIT 'NO_GROUPS')

	SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
	WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR <> 0
	IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1
	
DECLARE @STORES TABLE(ID_STORE BIGINT)
	INSERT INTO @STORES
	SELECT ID_STORE 
	FROM OPENXML(@HDOC, '//ID_STORE') 
	WITH(ID_STORE BIGINT '.') WHERE ID_STORE <> 0
	IF @@ROWCOUNT = 0 SET @ALL_STORE = 1

	SELECT * INTO #GOODS_GROUP
	FROM OPENXML(@HDOC, '//ID_GOODS_GROUP') 
	WITH(ID_GOODS_GROUP BIGINT '.') WHERE ID_GOODS_GROUP <> 0
	IF @@ROWCOUNT = 0 SET @ALL_GOODS_GROUPS = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_DAYS @DATE OUT, NULL
EXEC DBO.USP_RANGE_NORM @DATE OUT, NULL
--select @DATE
--select @IS_NDS_SAL
--return
IF ISNULL(@NO_GROUPS, 0) = 0
BEGIN

		SELECT 
		L.ID_GOODS, 
		L.ID_LOT_GLOBAL,
		--ID_GOODS_GROUP = NULL,--GG.ID_GOODS_GROUP,
		--GROUP_NAME = '',--GG.[NAME],
		G.CODE,
		GOODS_NAME = G.[NAME],
		AMOUNT_OST = SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES),
		VAT = CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
		L.PRICE_SAL,
		PRICE_SUP = L.PRICE_SUP - L.PVAT_SUP
	INTO #OST
    from lot_movement lm
    inner join lot l on l.id_lot_global = lm.id_lot_global
    inner join GOODS G ON G.ID_GOODS = L.ID_GOODS 
    inner join store s on s.id_store = l.id_store 
	WHERE ((@CO = 1 AND (@ALL_CONTRACTOR = 1 OR S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)))
		OR ((@CO <> 1 OR @CO IS NULL) AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)))
	AND (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND LM.DATE_OP < @DATE
	AND (@ALL_GOODS_GROUPS = 1 OR EXISTS (SELECT NULL FROM GOODS_2_GROUP G2G WHERE G2G.ID_GOODS = L.ID_GOODS 
		AND G2G.ID_GOODS_GROUP IN (SELECT ID_GOODS_GROUP FROM #GOODS_GROUP)))
	GROUP BY L.ID_GOODS, G.CODE, G.[NAME], CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END, 
		L.PRICE_SAL, L.ID_LOT_GLOBAL, L.PRICE_SUP - L.PVAT_SUP
	HAVING (SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES) > 0 AND @RESTS_ONLY = 1) OR @RESTS_ONLY = 0


	SELECT 
		--OST.ID_GOODS_GROUP,
		--OST.GROUP_NAME,
		OST.ID_GOODS,
		OST.CODE,
		OST.GOODS_NAME,
		A.AMOUNT_OST,
		NDS = ISNULL(OST.VAT, 0),
		OST.PRICE_SAL,
		SUMM_SAL = SUM(OST.PRICE_SAL*OST.AMOUNT_OST),
		OST.PRICE_SUP,
		SUMM_SUP = SUM(OST.PRICE_SUP*OST.AMOUNT_OST)
	INTO #TEMP
	FROM #OST OST
	INNER JOIN (
		SELECT 
			T.ID_GOODS,
			AMOUNT_OST = SUM(T.AMOUNT_OST),
			NDS = ISNULL(T.VAT, 0),
			T.PRICE_SAL,
			T.PRICE_SUP
		FROM #OST T
		GROUP BY T.ID_GOODS, ISNULL(T.VAT, 0), T.PRICE_SAL, T.PRICE_SUP
	) A ON A.ID_GOODS = OST.ID_GOODS AND A.NDS = ISNULL(OST.VAT, 0) AND 
		A.PRICE_SAL = OST.PRICE_SAL AND A.PRICE_SUP = OST.PRICE_SUP
	GROUP BY /*OST.ID_GOODS_GROUP, OST.GROUP_NAME, */OST.ID_GOODS, OST.CODE, OST.GOODS_NAME, 
		A.AMOUNT_OST, ISNULL(OST.VAT, 0), OST.PRICE_SAL, OST.PRICE_SUP

	SELECT 
		A.*,
		G2G.ID_GOODS_GROUP,
		GROUP_NAME = GG.[NAME]
	FROM
	(
	SELECT 
		--T.ID_GOODS_GROUP,
		--T.GROUP_NAME,
		T.ID_GOODS,
		T.CODE,
		T.GOODS_NAME,
		T.AMOUNT_OST,
		PRICE_SUP_0 = T.PRICE_SUP,
		SUMM_SUP_0 = T.SUMM_SUP,
		PRICE_SAL_0 = T.PRICE_SAL,
		SUMM_SAL_0 = T.SUMM_SAL,
		PRICE_SUP_10 = NULL,
		SUMM_SUP_10 = NULL,
		PRICE_SAL_10 = NULL,
		SUMM_SAL_10 = NULL,
		PRICE_SUP_18 = NULL,
		SUMM_SUP_18 = NULL,
		PRICE_SAL_18 = NULL,
		SUMM_SAL_18 = NULL
	FROM #TEMP T
	WHERE T.NDS = 0
	UNION ALL
	SELECT 
		--T.ID_GOODS_GROUP,
		--T.GROUP_NAME,
		T.ID_GOODS,
		T.CODE,
		T.GOODS_NAME,
		T.AMOUNT_OST,
		PRICE_SUP_0 = NULL,
		SUMM_SUP_0 = NULL,
		PRICE_SAL_0 = NULL,
		SUMM_SAL_0 = NULL,
		PRICE_SUP_10 = T.PRICE_SUP,
		SUMM_SUP_10 = T.SUMM_SUP,
		PRICE_SAL_10 = T.PRICE_SAL,
		SUMM_SAL_10 = T.SUMM_SAL,
		PRICE_SUP_18 = NULL,
		SUMM_SUP_18 = NULL,
		PRICE_SAL_18 = NULL,
		SUMM_SAL_18 = NULL
	FROM #TEMP T
	WHERE T.NDS = 10
	UNION ALL
	SELECT 
		--T.ID_GOODS_GROUP,
		--T.GROUP_NAME,
		T.ID_GOODS,
		T.CODE,
		T.GOODS_NAME,
		T.AMOUNT_OST,
		PRICE_SUP_0 = NULL,
		SUMM_SUP_0 = NULL,
		PRICE_SAL_0 = NULL,
		SUMM_SAL_0 = NULL,
		PRICE_SUP_10 = NULL,
		SUMM_SUP_10 = NULL,
		PRICE_SAL_10 = NULL,
		SUMM_SAL_10 = NULL,
		PRICE_SUP_18 = T.PRICE_SUP,
		SUMM_SUP_18 = T.SUMM_SUP,
		PRICE_SAL_18 = T.PRICE_SAL,
		SUMM_SAL_18 = T.SUMM_SAL
	FROM #TEMP T
	WHERE T.NDS = 18
	) A
	LEFT JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS = A.ID_GOODS
	LEFT JOIN GOODS_GROUP GG ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP
	ORDER BY GG.[NAME], CAST(A.CODE AS BIGINT)
	
	SELECT 
		SUMM_SUP_0 = (SELECT SUM(T.SUMM_SUP) FROM #TEMP T WHERE T.NDS = 0 GROUP BY T.NDS),
		SUMM_SAL_0 = (SELECT SUM(T.SUMM_SAL) FROM #TEMP T WHERE T.NDS = 0 GROUP BY T.NDS),
		SUMM_SUP_10 = (SELECT SUM(T.SUMM_SUP) FROM #TEMP T WHERE T.NDS = 10 GROUP BY T.NDS),
		SUMM_SAL_10 = (SELECT SUM(T.SUMM_SAL) FROM #TEMP T WHERE T.NDS = 10 GROUP BY T.NDS),
		SUMM_SUP_18 = (SELECT SUM(T.SUMM_SUP) FROM #TEMP T WHERE T.NDS = 18 GROUP BY T.NDS),
		SUMM_SAL_18 = (SELECT SUM(T.SUMM_SAL) FROM #TEMP T WHERE T.NDS = 18 GROUP BY T.NDS)
END
ELSE
BEGIN



set @ID_AU = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)
	--select @ID_AU
	
	
	----------------------------
	select s.id_store, s1.ID_CONTRACTOR, s1.NAME
	into #STORE
	from @STORES s
	inner join STORE s1 on s1.ID_STORE = s.id_store
	
IF(@@ROWCOUNT=0)  
	BEGIN  
		INSERT INTO @STORES  
		SELECT  
			ID_STORE  
		FROM STORE S  
		WHERE S.ID_CONTRACTOR = @ID_AU
	END  
---------------------------------

create table #ID_DOC_MOVEMENT(
id_document uniqueidentifier Not null primary key
)
    insert into #ID_DOC_MOVEMENT
    select distinct
        f.id_document    
    from (select 
            id_document,
            id_store_fr=id_store
          from doc_movement
          where code_op='sub' and id_table in (8,37,38,39)) f
    inner join (select 
                    id_document,
                    id_store_to=id_store
                from doc_movement
                where code_op='add' and id_table in (8,37,38,39)) w on w.id_document = f.id_document
    where not exists(select null 
                     from #STORE s1, #STORE s2 
                     where (s1.id_store = f.id_store_fr and s2.id_store = w.id_store_to)or(s1.id_store = w.id_store_to and s2.id_store = f.id_store_fr))




	SELECT 
		L.ID_GOODS, 
		L.ID_LOT_GLOBAL,
		--ID_GOODS_GROUP = NULL,--GG.ID_GOODS_GROUP,
		--GROUP_NAME = '',--GG.[NAME],
		G.CODE,
		GOODS_NAME = G.[NAME],
		AMOUNT_OST = SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB /*- LM.QUANTITY_RES*/),
		VAT = CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
		L.PRICE_SAL,
		PRICE_SUP = L.PRICE_SUP - L.PVAT_SUP
		--,lm.id_document
	INTO #OST1
	--select * 
    from lot_movement lm
    inner join lot l on l.id_lot_global = lm.id_lot_global
    inner join GOODS G ON G.ID_GOODS = L.ID_GOODS 
    inner join store s on s.id_store = l.id_store 
	WHERE ((@CO = 1 AND (@ALL_CONTRACTOR = 1 OR S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)))
		OR ((@CO <> 1 OR @CO IS NULL) AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)))
	AND (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND LM.DATE_OP < @DATE
	AND (@ALL_GOODS_GROUPS = 1 OR EXISTS (SELECT NULL FROM GOODS_2_GROUP G2G WHERE G2G.ID_GOODS = L.ID_GOODS 
		AND G2G.ID_GOODS_GROUP IN (SELECT ID_GOODS_GROUP FROM #GOODS_GROUP)))
	GROUP BY L.ID_GOODS, G.CODE, G.[NAME], CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END, 
		L.PRICE_SAL, L.ID_LOT_GLOBAL, L.PRICE_SUP - L.PVAT_SUP--,lm.id_document
	HAVING (SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB /* - LM.QUANTITY_RES */) > 0 AND @RESTS_ONLY = 1) OR @RESTS_ONLY = 0

--select * from #OST1
--select @DATE
/*	SELECT 
		OST.ID_GOODS,
		OST.CODE,
		OST.GOODS_NAME,
		A.AMOUNT_OST,
		NDS = ISNULL(OST.VAT, 0),
		OST.PRICE_SAL,
		SUMM_SAL = SUM(OST.PRICE_SAL*OST.AMOUNT_OST),
		OST.PRICE_SUP,
		SUMM_SUP = SUM(OST.PRICE_SUP*OST.AMOUNT_OST)
	--INTO #TEMP1
	FROM #OST1 OST
	inner JOIN (
		SELECT 
			T.ID_GOODS,
			AMOUNT_OST = SUM(T.AMOUNT_OST),
			NDS = ISNULL(T.VAT, 0),
			T.PRICE_SAL,
			T.PRICE_SUP
			--,id_document
		FROM #OST1 T
		GROUP BY T.ID_GOODS, ISNULL(T.VAT, 0), T.PRICE_SAL, T.PRICE_SUP--,id_document
	) A ON A.ID_GOODS = OST.ID_GOODS AND A.NDS = ISNULL(OST.VAT, 0) AND 
		A.PRICE_SAL = OST.PRICE_SAL AND A.PRICE_SUP = OST.PRICE_SUP-- and a.id_document = ost.id_document
	GROUP BY OST.ID_GOODS, OST.CODE, OST.GOODS_NAME, 
		A.AMOUNT_OST, ISNULL(OST.VAT, 0), OST.PRICE_SAL, OST.PRICE_SUP
		*/
---------------------------------------------------------------------------------		
	    SELECT   
		ID_GOODS,
       ID_LOT_GLOBAL,
       CODE,
		GOODS_NAME,
		AMOUNT_OST,
		VAT,
		PRICE_SAL,
		PRICE_SUP ,
			NUM_DOC ,
			
            sum_acc, 
            sum_sup ,  
            svat_sup ,  
        /*    sum_acc_0 ,    
            sum_acc_10 ,  
            sum_acc_18 ,  
            sum_sup_0 ,  
            sum_sup_10 ,  
            sum_sup_18
            */
            PRICE_SUP_0 = case when VAT = 0 then PRICE_SUP else NULL end,
		SUMM_SUP_0 = sum_sup_0,
		PRICE_SAL_0 = case when VAT = 0 then PRICE_SAL else NULL end,
		SUMM_SAL_0 = sum_acc_0,
		
		PRICE_SUP_10 = case when VAT = 10 then PRICE_SUP else NULL end,
		SUMM_SUP_10 = sum_sup_10,
		PRICE_SAL_10 = case when VAT = 10 then PRICE_SAL else NULL end,
		SUMM_SAL_10 = sum_acc_10,
		
		PRICE_SUP_18 = case when VAT = 18 then PRICE_SUP else NULL end,
		SUMM_SUP_18 = sum_sup_18,
		PRICE_SAL_18 = case when VAT = 18 then PRICE_SAL else NULL end,
		SUMM_SAL_18 = sum_acc_18
            
   INTO #VO 
    from
        (
      
        select 
       ID_GOODS=L.ID_GOODS,
       ID_LOT_GLOBAL=L.ID_LOT_GLOBAL,
       CODE=G.CODE,
		GOODS_NAME = G.[NAME],
		AMOUNT_OST = LM.QUANTITY_ADD - LM.QUANTITY_SUB,
		VAT = CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
		PRICE_SAL=L.PRICE_SAL,
		PRICE_SUP = L.PRICE_SUP - L.PVAT_SUP,
			NUM_DOC =ad.doc_num+' от '+CONVERT(VARCHAR(6),AD.DOC_DATE,104) + right(convert(varchar(4),datepart(yyyy,AD.DOC_DATE)),2),
            sum_acc = isnull(sum(case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end * (lm.sum_acc+lm.discount_acc)),0), --с учетом скидки  
            sum_sup = isnull(sum(case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end *(lm.sum_sup - lm.svat_sup)),0),  
            svat_sup = isnull(sum(case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end * lm.svat_sup),0),  
            sum_acc_0 = isnull(sum(case when L.VAT_SAL = 0 then (lm.sum_acc+lm.discount_acc) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0),    
            sum_acc_10 = isnull(sum(case when L.VAT_SAL = 10 then (lm.sum_acc+lm.discount_acc) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0),  
            sum_acc_18 = isnull(sum(case when L.VAT_SAL = 18 then (lm.sum_acc+lm.discount_acc) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0),  
            sum_sup_0 = isnull(sum(case when L.VAT_SAL = 0 then (lm.sum_sup - lm.svat_sup) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0),  
            sum_sup_10 = isnull(sum(case when L.VAT_SAL = 10 then (lm.sum_sup - lm.svat_sup) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0),  
            sum_sup_18 = isnull(sum(case when L.VAT_SAL = 18 then (lm.sum_sup - lm.svat_sup) else 0 end * case DBO.FN_LOT_MOVEMENT_OP(lm.id_lot_movement) when 'ADD' then 1 when 'SUB' then -1 end),0)  
        from lot_movement lm --VAT = CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
        inner join lot l on l.id_lot_global = lm.id_lot_global  
        inner join goods g on g.id_goods = l.id_goods  
        inner join tax_type tt on tt.id_tax_type = g.id_tax_type  
        inner join all_document ad on ad.id_document_global = lm.id_document
        inner join store s on s.id_store = l.id_store 
        --right join #GOODS Fg on fg.id_goods = G.id_goods
        where 
        ---------------------------------------------
        ((@CO = 1 AND (@ALL_CONTRACTOR = 1 OR S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)))
		OR ((@CO <> 1 OR @CO IS NULL) AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)))
	AND (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	and
	---------------------------------------------
		 exists (select id_store from @stores s where s.id_store = l.id_store and (l.id_store is not null)) and 
        (lm.id_table = 30  
                   or (lm.id_table = 2 and exists(select null from contractor c where name='СТУ' and exists(select null from store s where s.id_store = l.id_store and s.id_contractor = c.id_contractor)))--c.id_contractor = dm.id_contractor_from))  
                   or (lm.id_table = 2 and exists(select null from contractor c where name<>'СТУ'and exists(select null from store s where s.id_store = l.id_store and s.id_contractor = c.id_contractor))and lm.date_op<@date)--c.id_contractor = dm.id_contractor_from)and dm.date_op<@date_fr)  
                   or (lm.id_table in (3,6,12,13,20,21,24) and lm.date_op < @date)  
                   or (lm.id_table in (8,37,38,39) and lm.date_op < @date
                                       and (  
                                           (1=1 and   
                                            exists(select null from #id_doc_movement idm where idm.id_document = lm.id_document))))  
                  )   
                  group by 
					ad.doc_num+' от '+CONVERT(VARCHAR(6),AD.DOC_DATE,104) + right(convert(varchar(4),datepart(yyyy,AD.DOC_DATE)),2)
					,L.ID_GOODS,
					L.ID_LOT_GLOBAL,
					       G.CODE,
		G.[NAME],
		LM.QUANTITY_ADD - LM.QUANTITY_SUB,
		 CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
		L.PRICE_SAL,
		L.PRICE_SUP - L.PVAT_SUP
				/*	order by 
					ad.doc_num+' от '+CONVERT(VARCHAR(6),AD.DOC_DATE,104) + right(convert(varchar(4),datepart(yyyy,AD.DOC_DATE)),2)*/
        --group by lm.id_document,lm.op
   /*
   ((@CO = 1 AND (@ALL_CONTRACTOR = 1 OR S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)))
		OR ((@CO <> 1 OR @CO IS NULL) AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)))
	AND (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
   */

        UNION ALL --чеки
  
            SELECT 
                   ID_GOODS=L.ID_GOODS,
				   ID_LOT_GLOBAL=L.ID_LOT_GLOBAL,
				   CODE=G.CODE,
					GOODS_NAME = G.[NAME],
					AMOUNT_OST = LM.QUANTITY_ADD - LM.QUANTITY_SUB,
					VAT = CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
					PRICE_SAL=L.PRICE_SAL,
					PRICE_SUP = L.PRICE_SUP - L.PVAT_SUP,
					
            NUM_DOC =CH.mnemocode+' от '+ CONVERT(VARCHAR(6),ch.DATE_CHEQUE,104) + right(convert(varchar(4),datepart(yyyy,ch.DATE_CHEQUE)),2),
            SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT)),0),
            SUM_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP)),0),
            SVAT_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * PVAT_SUP),0),
            SUM_ACC_0 = ISNULL(SUM(CASE WHEN L.VAT_SAL=0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
            SUM_ACC_10 = ISNULL(SUM(CASE WHEN L.VAT_SAL=10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
            SUM_ACC_18 = ISNULL(SUM(CASE WHEN L.VAT_SAL=18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
            SUM_SUP_0 = ISNULL(SUM(CASE WHEN L.VAT_SAL=0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0),
            SUM_SUP_10 = ISNULL(SUM(CASE WHEN L.VAT_SAL=10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0),
            SUM_SUP_18 = ISNULL(SUM(CASE WHEN L.VAT_SAL=18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0)
            --select * from CHEQUE
            FROM CHEQUE_ITEM CH_I
            INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
            INNER JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL 
            INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
            INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
            INNER JOIN LOT L ON  L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL
            INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
            INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
            inner join lot_movement lm on CH_I.id_cheque_item_global = lm.id_document_item and lm.Id_document = ch.id_cash_session_global
             inner join store s on s.id_store = l.id_store 
            --right join #GOODS Fg on fg.id_goods = G.id_goods
            WHERE 
                    ---------------------------------------------
        ((@CO = 1 AND (@ALL_CONTRACTOR = 1 OR S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)))
		OR ((@CO <> 1 OR @CO IS NULL) AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)))
	AND (@ALL_STORE = 1 OR L.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	and
	---------------------------------------------
            CH.DATE_CHEQUE < @DATE AND
             C.ID_CONTRACTOR = @ID_AU AND CH.DOCUMENT_STATE = 'PROC' AND CH.CHEQUE_TYPE in ('SALE','RETURN')
            AND exists(select null from @stores s where s.id_store = l.id_store)
           --and exists (select id_store from @stores s where s.id_store = l.id_store and (l.id_store is not null))
            group by 
				CH.mnemocode+' от '+ CONVERT(VARCHAR(6),ch.DATE_CHEQUE,104) + right(convert(varchar(4),datepart(yyyy,ch.DATE_CHEQUE)),2)
									,L.ID_GOODS,
					L.ID_LOT_GLOBAL,
					       G.CODE,
		G.[NAME],
		LM.QUANTITY_ADD - LM.QUANTITY_SUB,
		 CASE @IS_NDS_SAL WHEN 1 THEN L.VAT_SAL ELSE L.VAT_SUP END,
		L.PRICE_SAL,
		L.PRICE_SUP - L.PVAT_SUP

     
       ) VO
   /*    where 
       ((@USE_DIAGN_REPORT=0) or (@INV_REMAINDER = 1))
       and */
       --(ROUND(SUM_ACC,2) + ROUND(SUM_SUP,2) +ROUND(SVAT_SUP,2)+ ROUND(SUM_ACC_0,2) + ROUND(SUM_ACC_10,2)+ ROUND(SUM_ACC_18,2)+ROUND(SUM_SUP_0,2)+ROUND(SUM_SUP_10,2)+ROUND(SUM_SUP_18,2) <> 0)


--select * from #VO
--return
---------------------------------------------------------------------------------
/*
SELECT 
		OST.ID_GOODS,
		OST.CODE,
		OST.GOODS_NAME,
		AMOUNT_OST = sum(AMOUNT_OST),
		NDS = ISNULL(OST.VAT, 0),
		OST.PRICE_SAL,
		SUMM_SAL = SUM(OST.PRICE_SAL*OST.AMOUNT_OST),
		OST.PRICE_SUP,
		SUMM_SUP = SUM(OST.PRICE_SUP*OST.AMOUNT_OST)
		--,id_document
	INTO #TEMP1
	FROM #OST1 OST
	/*
	inner JOIN (
		SELECT 
			T.ID_GOODS,
			AMOUNT_OST = SUM(T.AMOUNT_OST),
			NDS = ISNULL(T.VAT, 0),
			T.PRICE_SAL,
			T.PRICE_SUP
			--,id_document
		FROM #OST1 T
		GROUP BY T.ID_GOODS, ISNULL(T.VAT, 0), T.PRICE_SAL, T.PRICE_SUP--,id_document
	) A ON A.ID_GOODS = OST.ID_GOODS AND A.NDS = ISNULL(OST.VAT, 0) AND 
		A.PRICE_SAL = OST.PRICE_SAL AND A.PRICE_SUP = OST.PRICE_SUP-- and a.id_document = ost.id_document */
	GROUP BY OST.ID_GOODS, OST.CODE, OST.GOODS_NAME, 
	/*	A.AMOUNT_OST,*/ ISNULL(OST.VAT, 0), OST.PRICE_SAL, OST.PRICE_SUP
	--,id_document
	*/
----------------------------------------------------------------------------------	
/* select 
SUMM_SUP=SUM( SUM_sup) ,
SUMM_SAL=SUM( SUM_ACC),
num_doc
 from #VO
 group by num_doc
order by num_doc

 select 
SUMM_SUP=SUM( SUM_sup) ,
SUMM_SAL=SUM( SUM_ACC)
 from #VO
*/

	SELECT 
		ID_GOODS,
		CODE,
		GOODS_NAME,
		AMOUNT_OST=sum(AMOUNT_OST),
		PRICE_SUP_0=AVG(PRICE_SUP_0),
		SUMM_SUP_0=SUM(SUMM_SUP_0),
		PRICE_SAL_0=AVG(PRICE_SAL_0),
		SUMM_SAL_0=SUM(SUMM_SAL_0),
		PRICE_SUP_10=AVG(PRICE_SUP_10),
		SUMM_SUP_10=SUM(SUMM_SUP_10),
		PRICE_SAL_10=AVG(PRICE_SAL_10),
		SUMM_SAL_10=SUM(SUMM_SAL_10),
		PRICE_SUP_18=AVG(PRICE_SUP_18),
		SUMM_SUP_18=SUM(SUMM_SUP_18),
		PRICE_SAL_18=AVG(PRICE_SAL_18),
		SUMM_SAL_18=SUM(SUMM_SAL_18)
	FROM #vo
	group by 
	ID_GOODS,
		CODE,
		GOODS_NAME
	having 
	(SUM(abs(SUMM_SUP_0)) + SUM(abs(SUMM_SAL_0)) + SUM(abs(SUMM_SUP_10)) + SUM(abs(SUMM_SAL_10))+ SUM(abs(SUMM_SUP_18))+ SUM(abs(SUMM_SAL_18))<>0)
	ORDER BY CAST(CODE AS BIGINT)
	
	
	SELECT 
		SUMM_SUP_0 = (SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 0 GROUP BY T.VAT),
		SUMM_SAL_0 = (SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 0 GROUP BY T.VAT),
		SUMM_SUP_10 = (SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 10 GROUP BY T.VAT),
		SUMM_SAL_10 = (SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 10 GROUP BY T.VAT),
		SUMM_SUP_18 = (SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 18 GROUP BY T.VAT),
		SUMM_SAL_18 = (SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 18 GROUP BY T.VAT)

	SELECT 
		SUMM_SUP_0 = (SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 0 GROUP BY T.VAT)+(SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 10 GROUP BY T.VAT)+(SELECT SUM(T.SUM_sup) FROM #vo T WHERE T.VAT = 18 GROUP BY T.VAT),
		SUMM_SAL_0 = (SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 0 GROUP BY T.VAT)+(SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 10 GROUP BY T.VAT)+(SELECT SUM(T.SUM_ACC) FROM #vo T WHERE T.VAT = 18 GROUP BY T.VAT)
		
END

RETURN 0
GO
/*
exec DBO.REPEX_NDS_GROUPS N'
<XML>
<DATE>2011-06-20T12:45:46.392</DATE>
<RESTS_ONLY>1</RESTS_ONLY>
<IS_NDS_SAL>1</IS_NDS_SAL>
<NO_GROUPS>1</NO_GROUPS>
<ID_CONTRACTOR>5767</ID_CONTRACTOR>
<CO>0</CO>
</XML>'
*/