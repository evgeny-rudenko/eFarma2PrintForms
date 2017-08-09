SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
---------------
IF OBJECT_ID('FN_LOT_MOVEMENT_OP') IS NOT NULL DROP FUNCTION DBO.FN_LOT_MOVEMENT_OP
GO
---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('[MV_LOT_MOVEMENT_SIGN_OP1]') IS NOT NULL EXEC('DROP VIEW [MV_LOT_MOVEMENT_SIGN_OP1]')
GO

CREATE  VIEW [dbo].[MV_LOT_MOVEMENT_SIGN_OP1] AS         
    SELECT 
	    L.ID_LOT_MOVEMENT,
	    L.ID_LOT_global,
	    L.ID_DOCUMENT,
	    L.ID_TABLE,
        L.ID_DOCUMENT_ITEM,

        L.OP,
        L.QUANTITY_ADD,
        L.QUANTITY_SUB,
        L.QUANTITY_RES,
	    L.SUM_ACC,
	    L.SUM_SUP,
	    L.DISCOUNT_ACC,
	    L.SVAT_SUP,
	    L.DATE_OP,
	    L.CODE_OP,

        QUANTITY = (IsNULL(L.QUANTITY_ADD,0)+abs(IsNULL(L.QUANTITY_SUB,0))+IsNULL(L.QUANTITY_RES,0)),
        SIGN_OP1 = 
            CASE
                -- Приход (ADD)
                WHEN QUANTITY_ADD>0 AND QUANTITY_SUB=0 AND QUANTITY_RES=0 AND CODE_OP NOT IN ('PC_REBIND')  THEN  1    
                -- Расход (SUB)
                WHEN QUANTITY_ADD=0 AND QUANTITY_SUB>0 AND QUANTITY_RES=0 AND CODE_OP NOT IN ('PROD')       THEN -1   
                -- Возврат (RETURN)
                WHEN QUANTITY_ADD=0 AND QUANTITY_SUB<0 AND QUANTITY_RES=0                                   THEN  1   
                -- инвентаризация излишек (ADD)
                WHEN CODE_OP = 'INVENTORY_SVED' AND (QUANTITY_ADD - QUANTITY_SUB) > 0                       THEN  1    
                --инвентаризация недостача (SUB)
                WHEN CODE_OP = 'INVENTORY_SVED' AND (QUANTITY_ADD - QUANTITY_SUB) < 0                       THEN -1   
                -- Непонятно
                ELSE NULL 
            END,
        OP1 = 
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
        FROM 
            LOT_MOVEMENT L WITH(NOLOCK) 
GO
---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('DBO.FN_CHECQUE_TYPE_PAYMENT2') IS NULL 
    EXEC ('CREATE FUNCTION DBO.FN_CHECQUE_TYPE_PAYMENT2(@ID_CHEQUE_GLOBAL uniqueidentifier) RETURNS varchar(50) AS BEGIN RETURN NULL END')
GO

ALTER FUNCTION FN_CHECQUE_TYPE_PAYMENT2 (@ID_CHEQUE_GLOBAL uniqueidentifier)
RETURNS varchar(50) AS 
BEGIN
    DECLARE @RETURN varchar(50)

    SELECT TOP 1 @RETURN = CP.TYPE_PAYMENT
        FROM CHEQUE_PAYMENT CP WITH(NOLOCK)
        WHERE CP.ID_CHEQUE_GLOBAL=@ID_CHEQUE_GLOBAL
        
RETURN @RETURN
END
GO

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('REP_EX_TO_MR_RIGLA') IS NULL BEGIN 
    EXEC ('CREATE PROCEDURE REP_EX_TO_MR_RIGLA AS RETURN') END
GO

ALTER PROCEDURE REP_EX_TO_MR_RIGLA @XMLPARAM NTEXT
AS

PRINT '------------------------------------------------------------------------------------'
PRINT 'START'
PRINT '------------------------------------------------------------------------------------'
PRINT CONVERT(nvarchar,GETDATE(),121)
DECLARE @dStart datetime
SET @dStart = GETDATE()


DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME,@DATE_TO DATETIME,@ID_AU BIGINT, @IS_FILTERED BIT
DECLARE @USE_VAT BIT, @IS_EXIST_GOODS BIT
DECLARE @USE_DIAGN_REPORT BIT,
		@INV_REMAINDER BIT,
		@INV BIT,
		@INV_CONTR_W_VAT BIT,
		@INV_CONTR_NW_VAT BIT,
		@RETURN_BUYER BIT,
		@INV_FROM_AP BIT,
		@EXCESS_BY_INVENT BIT,
		@EXPENS BIT,
		@RECEIPTS BIT,
		@SERVICE BIT,
		@EXPENS_DISCOUNT BIT,
		@SK BIT,
		@CASH BIT,
		@CASHLESS BIT,
		@RECIPES_GROSS BIT,
		@RECIPES_GROSS_DISCOUNT BIT,
		@RETURN_TO_CONTRAC BIT,
		@COMPLAINT BIT,
		@BACK_SALE BIT,
		@MOVE_IN_CONTR BIT,
		@WRITE_OFF BIT,
		@SHORTAGE_BY_INV BIT,
		@REVALUATION BIT,
		@DISMANTLING BIT

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @ID_AU = ID_AU,
        @IS_FILTERED = IS_FILTERED,
        @USE_DIAGN_REPORT = USE_DIAGN_REPORT,
        @INV_REMAINDER = INV_REMAINDER,
		@INV = INV,
		@INV_CONTR_W_VAT = INV_CONTR_W_VAT,
		@INV_CONTR_NW_VAT  = INV_CONTR_NW_VAT,
		@RETURN_BUYER = RETURN_BUYER,
		@INV_FROM_AP = INV_FROM_AP,
		@EXCESS_BY_INVENT = EXCESS_BY_INVENT,
		@EXPENS = EXPENS,
		@RECEIPTS = RECEIPTS,
		@SERVICE = [SERVICE],
		@EXPENS_DISCOUNT = EXPENS_DISCOUNT,
		@SK = SK,
		@CASH = CASH,
		@CASHLESS = CASHLESS,
		@RECIPES_GROSS = RECIPES_GROSS,
		@RECIPES_GROSS_DISCOUNT = RECIPES_GROSS_DISCOUNT,
		@RETURN_TO_CONTRAC = RETURN_TO_CONTRAC,
		@COMPLAINT  = COMPLAINT,
		@BACK_SALE  = BACK_SALE,
		@MOVE_IN_CONTR = MOVE_IN_CONTR,
		@WRITE_OFF = WRITE_OFF,
		@SHORTAGE_BY_INV = SHORTAGE_BY_INV,
		@REVALUATION = REVALUATION,
		@DISMANTLING = DISMANTLING
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR', 
        DATE_TO DATETIME 'DATE_TO',
        ID_AU BIGINT 'ID_AU',
        IS_FILTERED BIT 'IS_FILTERED',
        USE_DIAGN_REPORT BIT 'USE_DIAGN_REPORT',
        INV_REMAINDER BIT 'INV_REMAINDER',
		INV BIT 'INV',
		INV_CONTR_W_VAT BIT 'INV_CONTR_W_VAT',
		INV_CONTR_NW_VAT BIT 'INV_CONTR_NW_VAT',
		RETURN_BUYER BIT 'RETURN_BUYER',
		INV_FROM_AP BIT 'INV_FROM_AP',
		EXCESS_BY_INVENT BIT 'EXCESS_BY_INVENT',
		EXPENS BIT 'EXPENS',
		RECEIPTS BIT 'RECEIPTS',
		[SERVICE] BIT 'SERVICE',
		EXPENS_DISCOUNT BIT 'EXPENS_DISCOUNT',
		SK BIT 'SK',
		CASH BIT 'CASH',
		CASHLESS BIT 'CASHLESS',
		RECIPES_GROSS BIT 'RECIPES_GROSS',
		RECIPES_GROSS_DISCOUNT BIT 'RECIPES_GROSS_DISCOUNT',
		RETURN_TO_CONTRAC BIT 'RETURN_TO_CONTRAC',
		COMPLAINT BIT 'COMPLAINT',
		BACK_SALE BIT 'BACK_SALE',
		MOVE_IN_CONTR BIT 'MOVE_IN_CONTR',
		WRITE_OFF BIT 'WRITE_OFF',
		SHORTAGE_BY_INV BIT 'SHORTAGE_BY_INV',
		REVALUATION BIT 'REVALUATION',
		DISMANTLING BIT 'DISMANTLING'
    )
---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
DECLARE @STORES TABLE(ID_STORE BIGINT PRIMARY KEY)    
INSERT INTO @STORES 
    SELECT DISTINCT ID_STORE 
        FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.') TAB

select s.id_store, s1.ID_CONTRACTOR, s1.NAME
	into #STORE
	from @STORES s
	inner join STORE s1 on s1.ID_STORE = s.id_store

IF(@@ROWCOUNT=0)  
BEGIN  
	INSERT INTO @STORES  
    	SELECT DISTINCT ID_STORE  
    		FROM STORE S WITH(NOLOCK) 
	    	WHERE S.ID_CONTRACTOR = @ID_AU  
END  
ELSE 
BEGIN
    DECLARE @STORE VARCHAR(1024), @CONTRACTORS VARCHAR(1024)
	EXEC DBO.USP_TABLE_NAMES '#STORE', @STORE OUT		
END
---------------------------------------------------------------------------------------------------
-- фильтр по АП
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#GOODS') is not null drop table #GOODS

SET @IS_EXIST_GOODS =1
	
SELECT ID_GOODS
    INTO #GOODS
    FROM OPENXML(@HDOC, '//ID_GOODS') WITH(ID_GOODS BIGINT '.') TAB
    
IF(@@ROWCOUNT=0)  
BEGIN 
    insert INTO #GOODS
		SELECT ID_GOODS
		from GOODS
		
	insert INTO #GOODS
		SELECT 0
		
	SET @IS_EXIST_GOODS = 0
END

--select * from #GOODS
--select @USE_DIAGN_REPORT, @SERVICE
--select * from CONTRACTOR where A_COD = 1
--return

---------------------------------------------------------------------------------------------------
SELECT @USE_VAT = USE_VAT FROM CONTRACTOR WHERE ID_CONTRACTOR = @ID_AU

---------------------------------------------------------------------------------------------------
-- НОРМАЛИЗАЦИЯ ГРАНИЦ ДАТ
---------------------------------------------------------------------------------------------------
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT


---------------------------------------------------------------------------------------------------
create table #ID_DOC_MOVEMENT(
id_document uniqueidentifier Not null primary key
)

if (@IS_FILTERED=1)
BEGIN    
    insert into #ID_DOC_MOVEMENT
        select distinct f.id_document    
        from 
            (
                select id_document, id_store_fr=id_store
                    from doc_movement WITH(NOLOCK)
                    where code_op='sub' and id_table in (8,37,38,39)
             ) f
             inner join 
             (
                select id_document, id_store_to=id_store
                    from doc_movement WITH(NOLOCK)
                    where code_op='add' and id_table in (8,37,38,39)
             ) w on w.id_document = f.id_document
        where 
            not exists
            (
                select null 
                    from @stores s1, @stores s2 
                    where 
                        (s1.id_store = f.id_store_fr and s2.id_store = w.id_store_to)
                        or(s1.id_store = w.id_store_to and s2.id_store = f.id_store_fr)
            )
END

--select * from #ID_DOC_MOVEMENT
--select * from all_document where id_document_global ='FA0AC242-8CF8-48AA-A754-025EEF69EE9F'
--return


PRINT 'Предварительная настройка'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()


---------------------------------------------------------------------------------------------------
-- Кэширую информацию, какие чеки имеют тип продаж 'TYPE1','TYPE2','TYPE3'
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#CHEQUE_PM_CACHE') IS NOT NULL DROP TABLE #CHEQUE_PM_CACHE

SELECT 
    ID_CHEQUE_GLOBAL, 
    TYPE_PAYMENT=MIN(CP.TYPE_PAYMENT)
    INTO #CHEQUE_PM_CACHE
    FROM CHEQUE_PAYMENT CP WITH(NOLOCK)   
    WHERE TYPE_PAYMENT IN ('TYPE1','TYPE2','TYPE3')
    GROUP BY ID_CHEQUE_GLOBAL

CREATE INDEX IX_#CHEQUE_PM_CACHE$ID_CHEQUE_GLOBAL ON #CHEQUE_PM_CACHE(ID_CHEQUE_GLOBAL)

---------------------------------------------------------------------------------------------------
-- кеширую шапку чеков за отчетный период (движение)
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#CHEQUE_CACHE') IS NOT NULL DROP TABLE #CHEQUE_CACHE

SELECT 
    CH.ID_CASH_SESSION_GLOBAL,
    CH.ID_CHEQUE_GLOBAL,
    CH.CHEQUE_TYPE,
    CH.SUM_DISCOUNT,
    [DATE_CHEQUE]=CONVERT(datetime,FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),
    [TYPE_PAYMENT]=PM.TYPE_PAYMENT,
    [SIGN] = CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END
    INTO #CHEQUE_CACHE
	FROM 
		CASH_SESSION CS WITH(NOLOCK)  
		INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
		INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
		INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
        INNER JOIN #CHEQUE_PM_CACHE PM WITH(NOLOCK) ON PM.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
	WHERE 
		CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to 
		AND C.ID_CONTRACTOR = @ID_AU 
		AND CH.DOCUMENT_STATE = 'PROC' 
        AND CH.CHEQUE_TYPE IN ('SALE','RETURN') 

CREATE INDEX IX_#CHEQUE_CACHE$ID_CHEQUE_GLOBAL ON #CHEQUE_CACHE(ID_CHEQUE_GLOBAL)

-----------------------------------------------------------------------------------------------
-- Кеширую данные Лота и его НДС.
-----------------------------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#LOT_GOODS_RATE') is not null DROP TABLE #LOT_GOODS_RATE

SELECT L.ID_LOT_GLOBAL, L.ID_GOODS, L.ID_STORE, L.PRICE_SUP, L.PVAT_SUP, TT.TAX_RATE
    INTO #LOT_GOODS_RATE
    FROM
        LOT L WITH(NOLOCK)
        INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
        INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE 
        RIGHT JOIN #GOODS Fg WITH(NOLOCK) on FG.ID_GOODS = L.ID_GOODS
    GROUP BY 
        L.ID_LOT_GLOBAL, L.ID_GOODS, TT.TAX_RATE, L.ID_STORE, L.PRICE_SUP, L.PVAT_SUP

CREATE INDEX IX_#LOT_GOODS_RATE$ID_LOT_GLOBAL ON #LOT_GOODS_RATE(ID_LOT_GLOBAL)

PRINT 'Кэширую кассовые смены-чеки'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()


---------------------------------------------------------------------------------------------------
-- Услуги(считаются если не задан код АП)
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#SERVICES') is not null drop table #SERVICES
create table #SERVICES 
(
    SECTION_NUMBER  BIGINT,  
    ORDER_BY  MONEY,--0,  
    ID_TABLE  BIGINT,  
    TABLE_NAME  VARCHAR(MAX),  
    CONTRACTOR  VARCHAR(MAX),  
    SECTION_NAME  VARCHAR(MAX),  
    DOC_NUM  VARCHAR(MAX),
    DOC_NUM_SUP VARCHAR(MAX),  
    DATE_DOC  datetime,
    PREFIX  VARCHAR(MAX),
    SUM_ACC  MONEY,
    SUM_SUP  MONEY,  
    SVAT_SUP  MONEY,  
    SUM_ACC_0  MONEY,  
    SUM_ACC_10  MONEY,
    SUM_ACC_18  MONEY,
    SUM_SUP_0  MONEY,  
    SUM_SUP_10  MONEY,  
    SUM_SUP_18  MONEY,
    CASH VARCHAR(MAX),
    KIND VARCHAR(MAX),
    RETURN_OR_BUY VARCHAR(MAX),
    DATE_GR DATETIME
)

---------------------------------------------------------------------------------------------------
-- если не выбраны коды АП то загружаем услуги
---------------------------------------------------------------------------------------------------
IF (@IS_EXIST_GOODS=0) 
BEGIN
	IF (@USE_DIAGN_REPORT=0)
	BEGIN
		INSERT INTO #SERVICES
		SELECT  
			SECTION_NUMBER = 2,  
			ORDER_BY = 7,--0,  
			ID_TABLE = 19,  
			TABLE_NAME = 'Выручка итого в т.ч.',  
			CONTRACTOR = '',  
			SECTION_NAME = 'в т.ч. услуга  итого',
			DOC_NUM = CONVERT(VARCHAR(8),CH.DATE_CHEQUE,4),
			DOC_NUM_SUP='',  
			DATE_DOC = getdate(),
			PREFIX = '',
            SUM_ACC = ISNULL(SUM(CH.SIGN * CH_I.SUMM),0), --сумма наличных продаж  
			SUM_SUP = CONVERT(MONEY,0),  
			SVAT_SUP = CONVERT(MONEY,0),  
			SUM_ACC_0 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 0 THEN CH.SIGN * CH_I.SUMM ELSE 0 END),0),  
			SUM_ACC_10 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN CH.SIGN * CH_I.SUMM ELSE 0 END),0),  
			SUM_ACC_18 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN CH.SIGN * CH_I.SUMM ELSE 0 END),0),  
			SUM_SUP_0 = CONVERT(MONEY,0),  
			SUM_SUP_10 = CONVERT(MONEY,0),  
			SUM_SUP_18 = CONVERT(MONEY,0),
			CASH='',
			KIND='',
			RETURN_OR_BUY = '',
			DATE_GR = null
		FROM 
		    --CASH_SESSION CS WITH(NOLOCK)  
		    --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
		    --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
		    --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
            #CHEQUE_CACHE CH WITH(NOLOCK)
		    INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
		    INNER JOIN SERVICE_4_SALE_ITEM S4S WITH(NOLOCK) ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL  
		    inner JOIN SERVICE S WITH(NOLOCK) ON S.ID_SERVICE = S4S.ID_SERVICE  
		    inner JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE  
		--WHERE 
		    --ch.DATE_CHEQUE BETWEEN @date_fr AND @date_to 
			--AND C.ID_CONTRACTOR = @ID_AU 
			--AND CH.DOCUMENT_STATE = 'PROC'  
			--AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
            --CH.[CHECQUE_TYPE_PAYMENT2] IN ('TYPE1','TYPE2','TYPE3')
		GROUP BY 
            CH.DATE_CHEQUE
		HAVING   
		    ISNULL(SUM(CH.SIGN * CH_I.SUMM),0)<>0
	END
    ELSE --of IF (@USE_DIAGN_REPORT=0)
	BEGIN
    	insert into #SERVICES
	    SELECT  
			SECTION_NUMBER = 2,  
			ORDER_BY = 7.5,--0,  
			ID_TABLE = 19,  
			TABLE_NAME = 'Выручка итого в т.ч.',  
			CONTRACTOR = '',  
			SECTION_NAME = '',--'в т.ч. услуга  итого',  
			DOC_NUM =CH.ID_CHEQUE,
			DOC_NUM_SUP='',  
			DATE_DOC = null,
			PREFIX = '',
			SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM),0), --сумма наличных продаж  
			SUM_SUP = CONVERT(MONEY,0),  
			SVAT_SUP = CONVERT(MONEY,0),  
			SUM_ACC_0 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),  
			SUM_ACC_10 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),  
			SUM_ACC_18 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),  
			SUM_SUP_0 = CONVERT(MONEY,0),  
			SUM_SUP_10 = CONVERT(MONEY,0),  
			SUM_SUP_18 = CONVERT(MONEY,0),
			CASH = CR.NAME_CASH_REGISTER,
			KIND = '3 услуги из них',
			RETURN_OR_BUY = CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
			DATE_GR = convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE)))--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104)

		FROM 
		    CASH_SESSION CS WITH(NOLOCK)  
		    INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
		    INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
		    INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
		    INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
		    INNER JOIN SERVICE_4_SALE_ITEM S4S WITH(NOLOCK) ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL  
		    inner JOIN SERVICE S WITH(NOLOCK) ON S.ID_SERVICE = S4S.ID_SERVICE  
		    inner JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE  
		WHERE 
		    ch.DATE_CHEQUE BETWEEN @date_fr AND @date_to   
			AND C.ID_CONTRACTOR = @ID_AU 
			AND CH.DOCUMENT_STATE = 'PROC' 
			AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3') 
		GROUP BY 
	        convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE))),
			CR.NAME_CASH_REGISTER,
			CH.ID_CHEQUE,
			CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END
	 END
END
ELSE --IF (@IS_EXIST_GOODS=0)
BEGIN
    IF (@USE_DIAGN_REPORT=0)
        INSERT INTO #SERVICES  
	    SELECT  
		    SECTION_NUMBER = 2,  
		    ORDER_BY = 7,--0,  
		    ID_TABLE = 19,  
		    TABLE_NAME = 'Выручка итого в т.ч.',  
		    CONTRACTOR = '',  
		    SECTION_NAME = 'в т.ч. услуга  итого',  
		    DOC_NUM ='',  
		    DOC_NUM_SUP='',  
		    DATE_DOC = getdate(),
		    PREFIX = '',
		    SUM_ACC = CONVERT(MONEY,0), --сумма наличных продаж  
		    SUM_SUP = CONVERT(MONEY,0),  
		    SVAT_SUP = CONVERT(MONEY,0),  
		    SUM_ACC_0 = CONVERT(MONEY,0),  
		    SUM_ACC_10 =  CONVERT(MONEY,0),
		    SUM_ACC_18 =  CONVERT(MONEY,0),
		    SUM_SUP_0 = CONVERT(MONEY,0),  
		    SUM_SUP_10 = CONVERT(MONEY,0),  
		    SUM_SUP_18 = CONVERT(MONEY,0),
		    CASH = '',
		    KIND = '',
		    RETURN_OR_BUY = '',
		    DATE_GR = null
        WHERE NOT EXISTS (SELECT NULL FROM #SERVICES  WITH(NOLOCK))
END

PRINT 'если не выбраны коды АП то загружаем услуги'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

--select getdate(), CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,getdate()))),104),CONVERT(VARCHAR,getdate(),104)
--select * from #SERVICES
--return


---------------------------------------------------------------------------------------------------
-- Скидки для раздела Выручка итого в т.ч. "скидка и прочее итого"
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#DISCOUNT') is not null drop table #DISCOUNT
create table #DISCOUNT 
(
    SECTION_NUMBER  BIGINT,  
    ORDER_BY  MONEY,--0,  
    ID_TABLE  BIGINT,  
    TABLE_NAME  VARCHAR(MAX),  
    CONTRACTOR  VARCHAR(MAX),  
    SECTION_NAME  VARCHAR(MAX),  
    DOC_NUM  VARCHAR(MAX),
    DOC_NUM_SUP VARCHAR(MAX),  
    DATE_DOC  datetime,
    PREFIX  VARCHAR(MAX),
    SUM_ACC  MONEY,
    SUM_SUP  MONEY,  
    SVAT_SUP  MONEY,  
    SUM_ACC_0  MONEY,  
    SUM_ACC_10  MONEY,
    SUM_ACC_18  MONEY,
    SUM_SUP_0  MONEY,  
    SUM_SUP_10  MONEY,  
    SUM_SUP_18  MONEY,
    CASH VARCHAR(MAX),
    KIND VARCHAR(MAX),
    RETURN_OR_BUY VARCHAR(MAX),
    DATE_GR DATETIME
)

---------------------------------------------------------------------------------------------------
-- скидки (#DISCOUNT)
---------------------------------------------------------------------------------------------------
IF (@USE_DIAGN_REPORT=0) 
BEGIN
	--скидки 

    -----------------------------------------------------------------------------------------------
	INSERT INTO #DISCOUNT 
	SELECT  
		SECTION_NUMBER = 2,  
		ORDER_BY = 8,--0,  
		ID_TABLE = 19,  
		TABLE_NAME = 'Выручка итого в т.ч.',  
		CONTRACTOR = '',  
		SECTION_NAME = 'скидка и прочее итого',  
		DOC_NUM = '',--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)+ ' №'+CS.MNEMOCODE,  
		DOC_NUM_SUP = '',  
		DATE_DOC = null,--CS.DATE_CLOSE, 
		PREFIX = '',
 
		SUM_ACC    = ISNULL(SUM(-LM.SIGN_OP1 * LM.DISCOUNT_ACC),0),  
		SUM_SUP    = CONVERT(MONEY,0),  
		SVAT_SUP   = CONVERT(MONEY,0),  

		SUM_ACC_0  =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) =  0 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
		SUM_ACC_10 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
		SUM_ACC_18 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  

		SUM_SUP_0  = CONVERT(MONEY,0),  
		SUM_SUP_10 = CONVERT(MONEY,0),  
		SUM_SUP_18 = CONVERT(MONEY,0),
		
		CASH='',
		KIND='',
		RETURN_OR_BUY = '',
		DATE_GR = NULL
        
	FROM 
	    --CASH_SESSION CS WITH(NOLOCK)  
	    --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
	    --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
	    --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL and document_state = 'PROC'  
        #CHEQUE_CACHE CH WITH(NOLOCK)

	    INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
        INNER JOIN MV_LOT_MOVEMENT_SIGN_OP1 LM   ON LM.ID_DOCUMENT_ITEM = CH_I.ID_CHEQUE_ITEM_GLOBAL

	    --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL  
	    --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
	    --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = g.ID_TAX_TYPE 
        --RIGHT JOIN #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
        INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL   
        --RIGHT JOIN #GOODS Fg WITH(NOLOCK) on FG.ID_GOODS = L.ID_GOODS
	    --LEFT  JOIN DLS_RECIPE_ITEM DI_S WITH(NOLOCK) ON DI_S.ID_RECIPE_ITEM_GLOBAL = CH_I.ID_CHEQUE_ITEM_GLOBAL   
	WHERE 
	    --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to   
		--AND C.ID_CONTRACTOR = @ID_AU   
		--AND CH.DOCUMENT_STATE = 'PROC' 
		--AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
        --CH.[CHECQUE_TYPE_PAYMENT2] IN ('TYPE1','TYPE2','TYPE3')
		--AND 
        EXISTS(SELECT NULL FROM @STORES S1 WHERE S1.ID_STORE = L.ID_STORE)
END
ELSE -- IF (@USE_DIAGN_REPORT=0) 
BEGIN
    --скидки  
    INSERT INTO #DISCOUNT
    SELECT  
	    SECTION_NUMBER = 2,  
	    ORDER_BY = 7.5,-- 8,--0,  
	    ID_TABLE = 19,  
	    TABLE_NAME = 'Выручка итого в т.ч.',  
	    CONTRACTOR = '',  
	    SECTION_NAME = '',--'скидка и прочее итого',  
	    DOC_NUM = CH.ID_CHEQUE,--'',--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)+ ' №'+CS.MNEMOCODE,  
	    DOC_NUM_SUP = '',  
	    DATE_DOC = null,--getdate(),--null,--CS.DATE_CLOSE, 
	    PREFIX ='',
	    
	    SUM_ACC = ISNULL(SUM(-LM.SIGN_OP1 * LM.DISCOUNT_ACC),0),  
	    SUM_SUP = CONVERT(MONEY,0),  
	    SVAT_SUP = CONVERT(MONEY,0),  
	    SUM_ACC_0  =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) =  0 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    SUM_ACC_10 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    SUM_ACC_18 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    
	    SUM_SUP_0 = CONVERT(MONEY,0),  
	    SUM_SUP_10 = CONVERT(MONEY,0),  
	    SUM_SUP_18 = CONVERT(MONEY,0),
	    CASH = CR.NAME_CASH_REGISTER,
	    KIND = '4 скидки из них',
	    RETURN_OR_BUY =CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
	    DATE_GR = convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE)))--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104)
    FROM 
        CASH_SESSION CS WITH(NOLOCK)  
        INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
        INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
        INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL and document_state = 'PROC'  
        INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  

        INNER JOIN MV_LOT_MOVEMENT_SIGN_OP1 LM   ON LM.ID_DOCUMENT_ITEM = CH_I.ID_CHEQUE_ITEM_GLOBAL
        INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL  

        --LEFT JOIN DLS_RECIPE_ITEM DI_S WITH(NOLOCK) ON DI_S.ID_RECIPE_ITEM_GLOBAL = CH_I.ID_CHEQUE_ITEM_GLOBAL   
        INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
        inner JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = g.ID_TAX_TYPE 
        right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
    WHERE 
        CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to
	    AND C.ID_CONTRACTOR = @ID_AU 
	    AND CH.DOCUMENT_STATE = 'PROC'   
	    AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
	    AND EXISTS(SELECT NULL FROM @STORES S1 WHERE S1.ID_STORE = L.ID_STORE)
	    and CH.SUM_DISCOUNT<>0
    group by 
	    convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE))),
	    CH.ID_CHEQUE,
	    CR.NAME_CASH_REGISTER,
	    CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END
END

				
PRINT 'Скидки для раздела Выручка итого в т.ч. "скидка и прочее итого"'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()
--select * from #DISCOUNT
--return

--!!!
---------------------------------------------------------------------------------------------------
-- Возмещение компаниями: Пфайзер и АстраЗенека при реализации товара в аптеках.
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#INDEMNITY') is not null drop table #INDEMNITY
create table #INDEMNITY
(
    SECTION_NUMBER  BIGINT,  
    ORDER_BY  MONEY,--0,  
    ID_TABLE  BIGINT,  
    TABLE_NAME  VARCHAR(MAX),  
    CONTRACTOR  VARCHAR(MAX),  
    SECTION_NAME  VARCHAR(MAX),  
    DOC_NUM  VARCHAR(MAX),
    DOC_NUM_SUP VARCHAR(MAX),  
    DATE_DOC  DATETIME,
    PREFIX  VARCHAR(MAX),
    SUM_ACC  MONEY,
    SUM_SUP  MONEY,  
    SVAT_SUP  MONEY,  
    SUM_ACC_0  MONEY,  
    SUM_ACC_10  MONEY,
    SUM_ACC_18  MONEY,
    SUM_SUP_0  MONEY,  
    SUM_SUP_10  MONEY,  
    SUM_SUP_18  MONEY,
    CASH VARCHAR(MAX),
    KIND VARCHAR(MAX),
    RETURN_OR_BUY VARCHAR(MAX),
    DATE_GR DATETIME
)
---------------------------------------------------------------------------------------------------
-- Получаю список карт, по которым вычисляю суммы продаж.
---------------------------------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#D2CARD_List') is not null drop table #D2CARD_List

---------------------------------------------------------------------------------------------------
SELECT DISTINCT DC.ID_DISCOUNT2_CARD_GLOBAL, DCT.NAME, NAME_MASK=CAST(NULL as VARCHAR(50)) 
    INTO #D2CARD_List
    FROM 
        DISCOUNT2_CARD DC
        INNER JOIN DISCOUNT2_CARD_TYPE DCT ON DCT.ID_DISCOUNT2_CARD_TYPE_GLOBAL = DC.ID_DISCOUNT2_CARD_TYPE_GLOBAL
    WHERE 
        DCT.NAME LIKE '%ПФАЙЗЕР%' OR DCT.NAME LIKE '%АСТРАЗЕНЕКА%'
---------------------------------------------------------------------------------------------------
-- По выбранным картам формирую группы.
---------------------------------------------------------------------------------------------------
UPDATE #D2CARD_List SET NAME_MASK='ПФАЙЗЕР' WHERE NAME LIKE '%ПФАЙЗЕР%'
UPDATE #D2CARD_List SET NAME_MASK='АСТРАЗЕНЕКА' WHERE NAME LIKE '%АСТРАЗЕНЕКА%'
---------------------------------------------------------------------------------------------------
-- индексирую таблицу
---------------------------------------------------------------------------------------------------
DECLARE @SQL nvarchar(4000)
SET @SQL = 'CREATE INDEX IX_#D2CARD_List_'+REPLACE(CAST(NewId() as nvarchar(36)),'-','_')+' ON #D2CARD_List(ID_DISCOUNT2_CARD_GLOBAL)'
EXEC(@SQL)

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
IF (@USE_DIAGN_REPORT=0) 
BEGIN
	--скидки 
    INSERT INTO #INDEMNITY
    SELECT  
	    SECTION_NUMBER = 2,  
	    ORDER_BY = 10,
	    ID_TABLE = 19,  
	    TABLE_NAME = 'Выручка итого в т.ч.',  
	    CONTRACTOR = '',  
	    SECTION_NAME = 'в т.ч. операции с '+tCARD_List1.NAME_MASK,
	    DOC_NUM = CONVERT(VARCHAR(8),CONVERT(DATETIME,FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),4),
	    DOC_NUM_SUP = '',  
	    DATE_DOC = CONVERT(datetime,FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),
	    PREFIX ='',
	    
	    SUM_ACC = ISNULL(SUM(-LM.SIGN_OP1 * LM.DISCOUNT_ACC),0),  
	    SUM_SUP = CONVERT(MONEY,0),  
	    SVAT_SUP = CONVERT(MONEY,0),  
	    SUM_ACC_0  =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) =  0 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    SUM_ACC_10 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    SUM_ACC_18 =  ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
	    
	    SUM_SUP_0 = CONVERT(MONEY,0),  
	    SUM_SUP_10 = CONVERT(MONEY,0),  
	    SUM_SUP_18 = CONVERT(MONEY,0),
	    CASH = '',
	    KIND = 'ВОЗМЕЩЕНИЕ РЕАЛИЗАЦИИ',
	    RETURN_OR_BUY = CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
	    DATE_GR = CONVERT(DATETIME,FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE)))

        FROM 
            --CASH_SESSION CS WITH(NOLOCK)  
            --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
            --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
            --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL and document_state = 'PROC'  
            #CHEQUE_CACHE CH WITH(NOLOCK)
            INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
            INNER JOIN DISCOUNT2_MAKE_ITEM MI WITH(NOLOCK) ON MI.ID_CHEQUE_ITEM_GLOBAL = CH_I.ID_CHEQUE_ITEM_GLOBAL 
            INNER JOIN #D2CARD_List tCARD_List1  WITH(NOLOCK) ON tCARD_List1.ID_DISCOUNT2_CARD_GLOBAL = MI.ID_DISCOUNT2_CARD_GLOBAL
            INNER JOIN MV_LOT_MOVEMENT_SIGN_OP1 LM   ON LM.ID_DOCUMENT_ITEM = CH_I.ID_CHEQUE_ITEM_GLOBAL

            --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL  
            --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
            --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = g.ID_TAX_TYPE 
            --RIGHT JOIN #GOODS Fg WITH(NOLOCK) on fg.ID_GOODS = G.ID_GOODS
            INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL   

        WHERE 
            --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to
	        --AND C.ID_CONTRACTOR = @ID_AU 
	        --AND CH.DOCUMENT_STATE = 'PROC'   
	        --AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
            --CH.[CHECQUE_TYPE_PAYMENT2] IN ('TYPE1','TYPE2','TYPE3')
	        --AND 
            EXISTS(SELECT NULL FROM @STORES S1 WHERE S1.ID_STORE = L.ID_STORE)
	        AND CH.SUM_DISCOUNT<>0

        GROUP BY 
            CONVERT(DATETIME,FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),
            tCARD_List1.NAME_MASK,
	        CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END
END

---------------------------------------------------------------------------------------------------
PRINT 'Возмещение компаниями: Пфайзер и АстраЗенека при реализации товара в аптеках.'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()
--select * from #INDEMNITY
--return
---------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- операции со СК для раздела Выручка итого в т.ч.
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#SK') is not null drop table #SK
create table #SK
(
    SECTION_NUMBER  BIGINT,  
    ORDER_BY  MONEY,--0,  
    ID_TABLE  BIGINT,  
    TABLE_NAME  VARCHAR(MAX),  
    CONTRACTOR  VARCHAR(MAX),  
    SECTION_NAME  VARCHAR(MAX),  
    DOC_NUM  VARCHAR(MAX),
    DOC_NUM_SUP VARCHAR(MAX),  
    DATE_DOC  datetime,
    PREFIX  VARCHAR(MAX),
    SUM_ACC  MONEY,
    SUM_SUP  MONEY,  
    SVAT_SUP  MONEY,  
    SUM_ACC_0  MONEY,  
    SUM_ACC_10  MONEY,
    SUM_ACC_18  MONEY,
    SUM_SUP_0  MONEY,  
    SUM_SUP_10  MONEY,  
    SUM_SUP_18  MONEY,
    CASH VARCHAR(MAX),
    KIND VARCHAR(MAX),
    RETURN_OR_BUY VARCHAR(MAX),
    DATE_GR DATETIME
)

---------------------------------------------------------------------------------------------------
IF (@USE_DIAGN_REPORT=0) 
BEGIN
	--скидки 
	INSERT INTO #SK
	SELECT  
		SECTION_NUMBER = 2,  
		ORDER_BY = 9, --0,  
		ID_TABLE = 19,  
		TABLE_NAME = 'Выручка итого в т.ч.',  
		CONTRACTOR = '',  
		SECTION_NAME = 'в т.ч. операции со СК итого',  
		DOC_NUM = CONVERT(VARCHAR(8),CH.DATE_CHEQUE,4),
		DOC_NUM_SUP='',  
		DATE_DOC = getdate(),--CONVERT(DATETIME,CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)),--CS.DATE_CLOSE,  
		PREFIX ='',
		SUM_ACC = sum(ISNULL(dri.sum_sk,0)),  
		SUM_SUP = CONVERT(MONEY,0),  
		SVAT_SUP = CONVERT(MONEY,0),  
		SUM_ACC_0 = sum(case when CONVERT(BIGINT,TAX_RATE) = 0 then ISNULL(dri.sum_sk,0) else 0 end),  
		SUM_ACC_10 = sum(case when CONVERT(BIGINT,TAX_RATE) = 10 then ISNULL(dri.sum_sk,0) else 0 end),  
		SUM_ACC_18 = sum(case when CONVERT(BIGINT,TAX_RATE) = 18 then ISNULL(dri.sum_sk,0) else 0 end),  
		SUM_SUP_0 = CONVERT(MONEY,0),  
		SUM_SUP_10 = CONVERT(MONEY,0),  
		SUM_SUP_18 = CONVERT(MONEY,0),
		CASH='',
		KIND='',
		RETURN_OR_BUY = '',
		DATE_GR = NULL  
	FROM 
	    --CASH_SESSION CS WITH(NOLOCK)  
	    --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
	    --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
	    --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
        #CHEQUE_CACHE CH
	    LEFT  JOIN DLS_RECIPE_ITEM dri WITH(NOLOCK) on dri.ID_RECIPE_GLOBAL = CH.ID_CHEQUE_GLOBAL   
	    --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL  
	    --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
	    --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE 
	    --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
        INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL 
	WHERE 
	    --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to   
		--AND C.ID_CONTRACTOR = @ID_AU   
		ch.cheque_type='sale'  
		--AND CH.DOCUMENT_STATE = 'PROC' 
		--AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
		AND exists(select null from @stores s1 where s1.id_store = l.id_store)
	GROUP BY CONVERT(VARCHAR(8),CH.DATE_CHEQUE,4)
	having sum(ISNULL(dri.sum_sk,0))<>0  
END
ELSE --IF (@USE_DIAGN_REPORT=0) 
BEGIN
    INSERT INTO #SK
    SELECT  
	    SECTION_NUMBER = 2,  
	    ORDER_BY = 7.5, --0,  
	    ID_TABLE = 19,  
	    TABLE_NAME = 'Выручка итого в т.ч.',  
	    CONTRACTOR = '',  
	    SECTION_NAME = '',--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104),--'в т.ч. операции со СК итого',  
	    DOC_NUM = CH.ID_CHEQUE,--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104),--+ ' №'+CS.MNEMOCODE,  
	    DOC_NUM_SUP='',  
	    DATE_DOC = null,--getdate(),--CONVERT(DATETIME,CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)),--CS.DATE_CLOSE,  
	    PREFIX ='',
	    SUM_ACC = sum(ISNULL(dri.sum_sk,0)),  
	    SUM_SUP = CONVERT(MONEY,0),  
	    SVAT_SUP = CONVERT(MONEY,0),  
	    SUM_ACC_0 = sum(case when CONVERT(BIGINT,TAX_RATE) = 0 then ISNULL(dri.sum_sk,0) else 0 end),  
	    SUM_ACC_10 = sum(case when CONVERT(BIGINT,TAX_RATE) = 10 then ISNULL(dri.sum_sk,0) else 0 end),  
	    SUM_ACC_18 = sum(case when CONVERT(BIGINT,TAX_RATE) = 18 then ISNULL(dri.sum_sk,0) else 0 end),  
	    SUM_SUP_0 = CONVERT(MONEY,0),  
	    SUM_SUP_10 = CONVERT(MONEY,0),  
	    SUM_SUP_18 = CONVERT(MONEY,0),
	    CASH = CR.NAME_CASH_REGISTER,
	    KIND = '5 операции со СК из них',
	    RETURN_OR_BUY =CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
	    DATE_GR = convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE)))--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104)
    FROM 
        CASH_SESSION CS WITH(NOLOCK)  
        INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
        INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
        INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
        LEFT JOIN DLS_RECIPE_ITEM dri WITH(NOLOCK) on dri.ID_RECIPE_GLOBAL = CH.ID_CHEQUE_GLOBAL   
        INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL  
        INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
        INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE 
        right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
    WHERE 
        CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to   
	    AND C.ID_CONTRACTOR = @ID_AU   
	    AND ch.cheque_type='sale'  
	    AND CH.DOCUMENT_STATE = 'PROC' 
	    AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
	    AND exists(select null from @stores s1 where s1.id_store = l.id_store)
    GROUP BY 
        CH.ID_CHEQUE,
        convert(datetime,floor(CONVERT(MONEY,CH.DATE_CHEQUE))),
        CR.NAME_CASH_REGISTER,
        CASE WHEN CH.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END
    having sum(ISNULL(dri.sum_sk,0))<>0  
END
    
---------------------------------------------------------------------------------------------------
PRINT 'операции со СК для раздела Выручка итого в т.ч.'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()
--select * from #SK
--return
---------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Чеки для раздела Выручка итого в т.ч.
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#CHEQUE') is not null drop table #CHEQUE
create table #CHEQUE
(
    SECTION_NUMBER  BIGINT,  
    ORDER_BY  MONEY,--0,  
    ID_TABLE  BIGINT,  
    TABLE_NAME  VARCHAR(MAX),  
    CONTRACTOR  VARCHAR(MAX),  
    SECTION_NAME  VARCHAR(MAX),  
    DOC_NUM  VARCHAR(MAX),
    DOC_NUM_SUP VARCHAR(MAX),  
    DATE_DOC  datetime,
    PREFIX  VARCHAR(MAX),
    SUM_ACC  MONEY,
    SUM_SUP  MONEY,  
    SVAT_SUP  MONEY,  
    SUM_ACC_0  MONEY,  
    SUM_ACC_10  MONEY,
    SUM_ACC_18  MONEY,
    SUM_SUP_0  MONEY,  
    SUM_SUP_10  MONEY,  
    SUM_SUP_18  MONEY,
    CASH VARCHAR(MAX),
    KIND VARCHAR(MAX),
    RETURN_OR_BUY VARCHAR(MAX),
    DATE_GR DATETIME
)

---------------------------------------------------------------------------------------------------
IF (@USE_DIAGN_REPORT=0) 
BEGIN
    --чеки
	INSERT INTO #CHEQUE
	SELECT  
		SECTION_NUMBER = 2,  
		ORDER_BY = case CP.TYPE_PAYMENT WHEN 'TYPE1' then 10
										WHEN 'TYPE2' then 11 
										WHEN 'TYPE3' then 12 END,
		ID_TABLE = 19,  
		TABLE_NAME = 'Выручка итого в т.ч.',  
		CONTRACTOR = '',  
		SECTION_NAME = CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN 'наличная итого'  
											WHEN 'TYPE2' THEN 'безналичная итого'  
											WHEN 'TYPE3' THEN 'смешанная итого' END,  

		DOC_NUM = CONVERT(VARCHAR(8),CP.DATE_CHEQUE,4),
		DOC_NUM_SUP='',  
		DATE_DOC = CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CP.DATE_CHEQUE))),--CS.DATE_CLOSE,  
		PREFIX ='',
		SUM_ACC = ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
								                  WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
								                  WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END),0) -  
				  ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
								                  WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
								                  WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END),0),
								   
		SUM_SUP   = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY),0),  
		
		SVAT_SUP  = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PVAT_SUP * CP.QUANTITY * CASE WHEN @USE_VAT=1 THEN 0 ELSE 1 END),0),  

		SUM_ACC_0 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 
		                            THEN --услуга  
							        (
									    CASE WHEN convert(bigint,tt.tax_rate) = 0 
										     THEN   
											    (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) 
											 ELSE 0 END
								    )  
									ELSE --товар
									(
									    CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 0 
									         THEN 
									            (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)
												  				      WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
												  				      WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
											 ELSE 0 END            
								    )
									END),0
						  ),  

		SUM_ACC_10 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 
		                             THEN --услуга  
									 (
									    CASE WHEN convert(bigint,tt.tax_rate) = 10 
									         THEN   
									            (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0) 
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0) 
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) 
											 ELSE 0 END
							         )  
									 ELSE --товар  
									 (
									    CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 10 
									         THEN 
									            (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0) 
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	  WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0) 
																	  WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
									 								  
									         ELSE 0 END          
                                     )													         
									 END),0
						   ),  
  

		SUM_ACC_18 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 
		                             THEN --услуга  
									 (
									     CASE WHEN convert(bigint,tt.tax_rate) = 18 
									          THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
											 						     WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
											 						     WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												   (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	     WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	     WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) 
						                      ELSE 0 END
									 )  
									 ELSE --товар
									 (
									     CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 18 
									          THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
												     				     WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
												     				     WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
											       (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	     WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	     WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
						             
									          ELSE 0 END   
									 )
									 END),0
					      ),  
					      
		SUM_SUP_0  = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) =  0 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),  
		SUM_SUP_10 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),  
		SUM_SUP_18 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),
		
		CASH='',
		KIND='',
		RETURN_OR_BUY = '',
		DATE_GR = null
	FROM CASH_SESSION CS WITH(NOLOCK)  
	INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
	INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
	INNER JOIN 
	(
        SELECT  
		    CH.ID_CHEQUE_GLOBAL,  
		    TYPE_PAYMENT = CH.TYPE_PAYMENT,  
		    
		    DATE_CHEQUE    = CH.DATE_CHEQUE,
		    ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL,
		    CHEQUE_TYPE    = CH.CHEQUE_TYPE,  
		    QUANTITY       = CH_I.QUANTITY,  
		    ID_GOODS       = CH_I.ID_GOODS,  
		    ID_LOT_GLOBAL  = CH_I.ID_LOT_GLOBAL,  
			
			CALC_SUMM      = CASE WHEN CH.CHEQUE_TYPE = 'SALE'   THEN  CH_I.SUMM 
			                      WHEN CH.CHEQUE_TYPE = 'RETURN' THEN -CH_I.SUMM 
			                      ELSE 0 END, 
			
		    SUM_NAL        = CASE WHEN CH.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по налу  
		    SUM_RET_NAL    = CASE WHEN CH.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END, -- возврат по налу  
		    SUM_CREDIT     = CASE WHEN CH.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по карте  
		    SUM_RET_CREDIT = CASE WHEN CH.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END, -- возврат по карте  
		    SUM_Type3      = CASE WHEN CH.TYPE_PAYMENT = 'TYPE3' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по налу  
		    SUM_RET_Type3  = CASE WHEN CH.TYPE_PAYMENT = 'TYPE3' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END  -- продажа по налу  
            FROM 
                --(
                --    SELECT CP.ID_CHEQUE_GLOBAL, MIN(CP.TYPE_PAYMENT) as TYPE_PAYMENT
                --        FROM CHEQUE C INNER JOIN CHEQUE_PAYMENT CP ON CP.ID_CHEQUE_GLOBAL=C.ID_CHEQUE_GLOBAL
                --        WHERE C.DATE_CHEQUE BETWEEN @date_fr and @date_to 
                --        GROUP BY CP.ID_CHEQUE_GLOBAL
                --) 
                --CP 
                #CHEQUE_CACHE CH WITH(NOLOCK)
                INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
            --WHERE 
                --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to
				--AND CH.DOCUMENT_STATE = 'PROC'  
                --AND CP.TYPE_PAYMENT IN ('TYPE1','TYPE2','TYPE3') 
	) 
	CP ON CS.ID_CASH_SESSION_GLOBAL = CP.ID_CASH_SESSION_GLOBAL  

	LEFT JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CP.ID_LOT_GLOBAL  
	LEFT join goods g WITH(NOLOCK) on g.id_goods = l.id_goods
	LEFT JOIN TAX_TYPE TL WITH(NOLOCK) ON TL.ID_TAX_TYPE = g.ID_TAX_TYPE  
	LEFT JOIN SERVICE_4_SALE_ITEM S4S WITH(NOLOCK) ON S4S.ID_SERVICE_4_SALE = CP.ID_LOT_GLOBAL  
	LEFT JOIN SERVICE S WITH(NOLOCK) ON S.ID_SERVICE = S4S.ID_SERVICE  
	LEFT JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE  
	right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = ISNULL(G.id_goods,0)	
	
	WHERE 
		  C.ID_CONTRACTOR = @ID_AU 
          --это услуга или товар с нужного нам склада
	      AND (S4S.ID_SERVICE_4_SALE is not null or exists(select null from lot l1 where l1.id_lot_global = l.id_lot_global and exists(select null from @stores s1 where s1.id_store = l1.id_store)))
	GROUP BY  
	      CP.TYPE_PAYMENT, 
		  CONVERT(VARCHAR(8),CP.DATE_CHEQUE,4),   
		  CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CP.DATE_CHEQUE)))  
			 
END
ELSE -- IF (@USE_DIAGN_REPORT=0) 
BEGIN 
    -----------------------------------------------------------------------------------------------
    --чеки = ДИАГНОСТИЧЕСКИЙ ДЕТАЛЬНЫЙ ЗАПРОС... (на данный момент - риглой не используется)
    -----------------------------------------------------------------------------------------------
	INSERT INTO #CHEQUE
	SELECT  
		SECTION_NUMBER = 2,  
		ORDER_BY = case CP.TYPE_PAYMENT WHEN 'TYPE1' then 7.5
										when 'TYPE2' then 7.5
										when 'TYPE3' then 7.5 end,--0,  
		ID_TABLE = 19,  
		TABLE_NAME = 'Выручка итого в т.ч.',  
		CONTRACTOR = '',  
		SECTION_NAME ='',
		DOC_NUM = CP.ID_CHEQUE,
		DOC_NUM_SUP='',  
		DATE_DOC = null,
		PREFIX ='',        
		SUM_ACC = ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
										          WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
										          WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END),0)-  
				  ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
										          WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
										          WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END),0),
		
		SUM_SUP   = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY),0),  
		SVAT_SUP  = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PVAT_SUP * CP.QUANTITY * CASE WHEN @USE_VAT=1 THEN 0 ELSE 1 END),0),  
		SUM_ACC_0 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 --услуга  
		                            THEN (CASE WHEN convert(bigint,tt.tax_rate) = 0 
		                                       THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
		                                                                  WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0) 
																	      WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												    (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	      WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0) 
																	      WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) ELSE 0 END
										  )
									ELSE CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 0 
									          THEN  (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
									                                      WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
													 				      WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												    (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	      WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0) 
																	      WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
											  ELSE 0 END --товар  
								END),0
						  ),  
						  
		SUM_ACC_10 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 --услуга
		                             THEN (CASE WHEN convert(bigint,tt.tax_rate) = 10 
		                                        THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
		                                                                   WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												     (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
												                           WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) ELSE 0 END
				                          )  
									 ELSE  CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 10 
									            THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
									                                       WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0)
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												     (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	       WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0) 
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
											    ELSE 0 END --товар  
								END),0
						   ),  
  
		SUM_ACC_18 = ISNULL(SUM(CASE WHEN CP.ID_GOODS = 0 --услуга
		                             THEN (CASE WHEN convert(bigint,tt.tax_rate) = 18 
		                                        THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
																	       WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0) 
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												     (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	       WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END) 
							                    ELSE 0 END
										  )  
									 ELSE  CASE WHEN CP.ID_GOODS <> 0 and convert(bigint,tl.tax_rate) = 18 
											    THEN (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_NAL,0)   
																	       WHEN 'TYPE2' THEN IsNULL(CP.SUM_CREDIT,0) 
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_Type3,0) END)-  
												     (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN IsNULL(CP.SUM_RET_NAL,0)  
																	       WHEN 'TYPE2' THEN IsNULL(CP.SUM_RET_CREDIT,0)
																	       WHEN 'TYPE3' THEN IsNULL(CP.SUM_RET_Type3,0) END)  
												ELSE 0 END --товар  
								 END),0
							),  
								 
		SUM_SUP_0  = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) =  0 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),  
		SUM_SUP_10 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),  
		SUM_SUP_18 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tl.tax_rate) = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),
		CASH = CR.NAME_CASH_REGISTER,
		KIND = CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN '1 наличная из них'  
									WHEN 'TYPE2' THEN '2 безналичная из них'
									WHEN 'TYPE3' THEN '3 смешанная из них' END,
		RETURN_OR_BUY =CASE WHEN CP.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
		DATE_GR = convert(datetime,floor(CONVERT(MONEY,CP.DATE_CHEQUE)))
	
	FROM CASH_SESSION CS WITH(NOLOCK)  
	INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
	INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
	INNER JOIN 
	(
        SELECT  
			CH.ID_CHEQUE,
	        CP.ID_CHEQUE_GLOBAL,  
	        TYPE_PAYMENT = CP.TYPE_PAYMENT,  
	    
	        DATE_CHEQUE = CH.DATE_CHEQUE,
	        ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL,
	        CHEQUE_TYPE = CH.CHEQUE_TYPE,  
	        QUANTITY = CH_I.QUANTITY,  
	        ID_GOODS = CH_I.ID_GOODS,  
	        ID_LOT_GLOBAL  = CH_I.ID_LOT_GLOBAL,  
    		
		    CALC_SUMM      = CASE WHEN CH.CHEQUE_TYPE = 'SALE'   THEN  CH_I.SUMM 
		                          WHEN CH.CHEQUE_TYPE = 'RETURN' THEN -CH_I.SUMM 
		                          ELSE 0 END, 
    		
	        SUM_NAL        = CASE WHEN CP.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по налу  
	        SUM_RET_NAL    = CASE WHEN CP.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END, -- возврат по налу  
	        SUM_CREDIT     = CASE WHEN CP.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по карте  
	        SUM_RET_CREDIT = CASE WHEN CP.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END, -- возврат по карте  
	        SUM_Type3      = CASE WHEN CP.TYPE_PAYMENT = 'TYPE3' AND CH.CHEQUE_TYPE = 'SALE'   THEN CH_I.SUMM ELSE 0 END, -- продажа по налу  
	        SUM_RET_Type3  = CASE WHEN CP.TYPE_PAYMENT = 'TYPE3' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END  -- продажа по налу  
            FROM 
                (
                    SELECT CP.ID_CHEQUE_GLOBAL, MIN(CP.TYPE_PAYMENT) as TYPE_PAYMENT
                        FROM CHEQUE C INNER JOIN CHEQUE_PAYMENT CP ON CP.ID_CHEQUE_GLOBAL=C.ID_CHEQUE_GLOBAL
                        WHERE C.DATE_CHEQUE BETWEEN @date_fr and @date_to 
                        GROUP BY CP.ID_CHEQUE_GLOBAL
                ) CP 
                INNER JOIN CHEQUE CH WITH(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CP.ID_CHEQUE_GLOBAL  
                INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
            WHERE 
                CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to
			    AND CH.DOCUMENT_STATE = 'PROC'  
                AND CP.TYPE_PAYMENT IN ('TYPE1','TYPE2','TYPE3') 
	) 
	CP ON CP.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL  
				
	LEFT  JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CP.ID_LOT_GLOBAL  
	LEFT  JOIN goods g WITH(NOLOCK) on g.id_goods = l.id_goods
	LEFT  JOIN TAX_TYPE TL WITH(NOLOCK) ON TL.ID_TAX_TYPE = g.ID_TAX_TYPE  
	LEFT  JOIN SERVICE_4_SALE_ITEM S4S WITH(NOLOCK) ON S4S.ID_SERVICE_4_SALE = CP.ID_LOT_GLOBAL  
	LEFT  JOIN SERVICE S WITH(NOLOCK) ON S.ID_SERVICE = S4S.ID_SERVICE  
	LEFT  JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE  
	RIGHT JOIN #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
	
	WHERE 
		C.ID_CONTRACTOR = @ID_AU 
	    and (S4S.ID_SERVICE_4_SALE is not null or exists(select null from lot l1 where l1.id_lot_global = L.id_lot_global and exists(select null from @stores s1 where s1.id_store = l1.id_store)))
	GROUP BY 
    	CASE WHEN CP.CHEQUE_TYPE = 'RETURN' THEN  '2 по возврату' ELSE '1 по продаже' END,
	    CR.NAME_CASH_REGISTER, 
	    CP.ID_CHEQUE, 
		CP.TYPE_PAYMENT,  
		CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CP.DATE_CHEQUE))) 
END
            

PRINT 'Чеки для раздела Выручка итого в т.ч.'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()
--select * from #CHEQUE ORDER BY DOC_NUM
--return



---------------------------------------------------------------------------------------------------
-- Инвентаризация
---------------------------------------------------------------------------------------------------
DECLARE @CONST_VAT VARCHAR(10)
SELECT TOP 1 @CONST_VAT = VALUE FROM SYS_OPTION WHERE CODE = 'INVENTORY_CALC_VAT'


if OBJECT_ID('Tempdb..#LM_DISCOUNT') is not null drop table #LM_DISCOUNT

SELECT
		ID_DOCUMENT = LM.ID_DOCUMENT,
        ID_TABLE = LM.ID_TABLE,  
        DATE_OP = LM.DATE_OP,  
        DISCOUNT_ACC = LM.DISCOUNT_ACC, 
        LM.SUM_ACC,  
        LM.SUM_SUP,
        LM.SVAT_SUP, 
        tax_rate=L.tax_rate,
        DOC_NUM=AD.DOC_NUM,
        ID_LOT_MOVEMENT=LM.ID_LOT_MOVEMENT,
        OP1=LM.OP1
        into #LM_DISCOUNT 
        
        FROM 
            --LOT_MOVEMENT LM WITH(NOLOCK)
            MV_LOT_MOVEMENT_SIGN_OP1 LM WITH(NOLOCK)  

            --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
            --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
            --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
            INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL 

            INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT  
            LEFT JOIN CONTRACTOR C_TO WITH(NOLOCK) ON C_TO.ID_CONTRACTOR = AD.ID_CONTRACTOR_TO AND LM.ID_TABLE in (8,21)  
            LEFT JOIN ACT_DEDUCTION AD1 WITH(NOLOCK) ON AD1.ID_ACT_DEDUCTION_GLOBAL = LM.ID_DOCUMENT  
            LEFT JOIN ENUMERATION_VALUE EV WITH(NOLOCK) ON EV.VALUE = AD1.DEDUCTION_REASON  
            --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
        
        WHERE 
            (LM.DATE_OP >= @DATE_FR AND LM.DATE_OP <= @DATE_TO)  
            AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = AD.ID_STORE1)  
            AND (LM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WHERE IS_SUPPLIER=0 AND ID_INVOICE_OUT_GLOBAL = LM.ID_DOCUMENT))
           
           
--select * from #LM_DISCOUNT
--return 
							

declare @TEMP TABLE(ID_DOCument uniqueidentifier, ID_GOODS BIGINT, 
	ID_STORE BIGINT, 
    VAT MONEY, QUANTITY MONEY, SVAT_SUP MONEY, SUM_SUP MONEY, SVAT_ACC MONEY, SUM_ACC MONEY,
    TYPE VARCHAR(20), [SIGN] smallint)
  

INSERT @TEMP(ID_DOCument,ID_GOODS, ID_STORE, VAT, QUANTITY, SVAT_SUP, SUM_SUP, SVAT_ACC, SUM_ACC, [TYPE], [SIGN])
select
	ID_DOCument = lm.id_document,
	l.id_goods
	,ID_STORE
	,VAT = tt.TAX_RATE--CASE WHEN @USE_VAT = 1 THEN CASE WHEN @CONST_VAT = 'SUP' THEN L.VAT_SUP ELSE L.VAT_SAL END ELSE 0 END	
	,QUANTITY = SUM(CONVERT(MONEY, (LM.QUANTITY_ADD - LM.QUANTITY_SUB)) * CONVERT(MONEY, SR.NUMERATOR) / CONVERT(MONEY, SR.DENOMINATOR))
	,SVAT_SUP = SUM(ABS(lm.SVAT_SUP))
	,SUM_SUP = SUM(ABS(lm.SUM_SUP))
	,SVAT_ACC = SUM(ABS(lm.SVAT_ACC))
	,SUM_ACC = SUM(ABS(lm.SUM_ACC))
	,[TYPE] = case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'ADD' else 'SUB' end	
	,[Sign] = case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 1 else -1 end	
from lot_movement lm WITH(NOLOCK) 
INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT
inner join LOT l WITH(NOLOCK) on l.ID_LOT_GLOBAL = lm.ID_LOT_GLOBAL
INNER JOIN SCALING_RATIO SR WITH(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
inner join goods g WITH(NOLOCK) on g.id_goods = l.id_goods
inner join TAX_TYPE Tt WITH(NOLOCK) on Tt.ID_TAX_TYPE = g.ID_TAX_TYPE
where 
	  LM.DATE_OP >= @DATE_FR AND LM.DATE_OP <= @DATE_TO and
      EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = AD.ID_STORE1)
      and lm.id_table = 24
      and lm.OP<>'SUB_RES'
group by lm.id_document, 
	case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'ADD' else 'SUB' end
	,tt.TAX_RATE
	,ID_STORE, l.ID_GOODS
	, case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 1 else -1 end	
	

		
if OBJECT_ID('Tempdb..#Tmp2') is not null drop table #Tmp2
select 
	id_document,
	ID_GOODS,
	ID_STORE
	,VAT
	,QUANTITY = SUM(QUANTITY)
	,SVAT_SUP = SUM(SVAT_SUP*[SIGN])
	,SUM_SUP = SUM(SUM_SUP*[SIGN])
	,SVAT_ACC = SUM(SVAT_ACC*[SIGN])
	,SUM_ACC = SUM(SUM_ACC*[SIGN])
	--,[TYPE]
into #Tmp2
from @TEMP 
group by ID_STORE, VAT, ID_GOODS, id_document

if OBJECT_ID('Tempdb..#Tmp3') is not null drop table #Tmp3
select
	id_document,
	ID_STORE
	,VAT
	,QUANTITY = ABS(SUM(QUANTITY))
	,SVAT_SUP = ABS(SUM(SVAT_SUP))
	,SUM_SUP = ABS(SUM(SUM_SUP))
	,SVAT_ACC = ABS(SUM(SVAT_ACC))
	,SUM_ACC = ABS(SUM(SUM_ACC))
	,[TYPE]= case when sum_acc < 0 then 'SUB' else 'ADD' end
into #Tmp3
from #Tmp2
group by id_document, ID_STORE, VAT, case when sum_acc < 0 then 'SUB' else 'ADD' end

declare @inventory table (id_table bigint,id_document uniqueidentifier, ID_CONTRACTOR bigint, 
						  VAT MONEY, SVAT_SUP MONEY, SUM_SUP MONEY, SVAT_ACC MONEY, 
						  SUM_ACC MONEY, sign_op smallint, doc_num varchar(500), doc_date datetime,
						  contractor varchar(100),sum_sum_acc money, sum_sum_sup money)			

insert into	@inventory(id_table, id_document, ID_CONTRACTOR, 
						  VAT, sum_sum_acc, sum_sum_sup, SVAT_SUP, SUM_SUP, SVAT_ACC, 
						  SUM_ACC, sign_op, doc_num, contractor, doc_date)						  
SELECT
	24,
	t.id_document, 
	S.ID_CONTRACTOR,
	t.VAT,
	sum_sum_acc = 0,
	sum_sum_sup = 0,
	SVAT_SUP = t.SVAT_SUP,
	SUM_SUP = t.SUM_SUP,
	SVAT_ACC = t.SVAT_ACC,
	SUM_ACC = t.SUM_ACC
	,SIGN_OP = CASE WHEN T.TYPE = 'ADD' THEN 1 ELSE -1 END
	,doc_num = '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,ad.DOC_DATE))),104)
	,contractor = c.name,
	doc_date = ad.doc_date
From #TMP3 T WITH(NOLOCK)
INNER JOIN STORE S WITH(NOLOCK) ON S.ID_STORE = T.ID_STORE
inner join contractor c WITH(NOLOCK) on c.id_contractor = s.id_contractor
INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = T.ID_DOCUMENT

update i
set sum_sum_acc = ii.sum_sum_acc,
	sum_sum_sup = ii.sum_sum_sup
from @inventory i
inner join (select 
				id_document,
				SIGN_op,
				sum_sum_acc = sum(sum_acc),
				sum_sum_sup = sum(sum_sup-SVAT_SUP)
			from @inventory
			group by id_document, sign_op) ii on ii.id_document = i.id_document and ii.SIGN_op = i.SIGN_op						

PRINT 'Инвентаризация'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()


---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
-- ВХОДЯЩИЙ Остаток
---------------------------------------------------------------------------------------------------
CREATE TABLE #VO_ITEM 
(
    idRec       int IDENTITY(1,1) PRIMARY KEY,
	NUM_DOC     VARCHAR(100),
    sum_acc     money,
    sum_sup     money,
    svat_sup    money,
            
    sum_acc_0   money,
    sum_acc_10  money,
    sum_acc_18  money,
      
    sum_sup_0   money,
    sum_sup_10  money,
    sum_sup_18  money
)
CREATE INDEX IX_#VO_ITEM$NUM_DOC ON #VO_ITEM(NUM_DOC)   

---------------------------------------------------------------------------------------------------
-- Вначале отдельными запросами добавляю записи во временную таблицу #VO_ITEM
---------------------------------------------------------------------------------------------------
INSERT INTO #VO_ITEM
    SELECT
	    --NUM_DOC    = ad.doc_num+' от '+CONVERT(VARCHAR(8),AD.DOC_DATE,4),
	    NUM_DOC    = NULL,
        sum_acc    = case when lm.id_table = 24 then isnull(sum(lm.sign_op * (ABS(lm.sum_acc)+ABS(lm.discount_acc))),0) else isnull(sum(lm.sign_op1 * (lm.sum_acc+lm.discount_acc)),0) end, --с учетом скидки  
        sum_sup    = case when lm.id_table = 24 then isnull(sum(lm.sign_op * (ABS(lm.sum_sup) - ABS(lm.svat_sup))),0)   else isnull(sum(lm.sign_op1 * (lm.sum_sup - lm.svat_sup)),0) end,  
        svat_sup   = case when lm.id_table = 24 then isnull(sum(lm.sign_op *  ABS(lm.svat_sup)),0)                      else isnull(sum(lm.sign_op1 *  lm.svat_sup),0) end,  
        sum_acc_0  = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate =  0 then (ABS(lm.sum_acc)+ABS(lm.discount_acc)) else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate =  0 then (lm.sum_acc+lm.discount_acc) else 0 end * lm.sign_op1),0) end,    
        sum_acc_10 = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate = 10 then (ABS(lm.sum_acc)+ABS(lm.discount_acc)) else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate = 10 then (lm.sum_acc+lm.discount_acc) else 0 end * lm.sign_op1),0) end,  
        sum_acc_18 = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate = 18 then (ABS(lm.sum_acc)+ABS(lm.discount_acc)) else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate = 18 then (lm.sum_acc+lm.discount_acc) else 0 end * lm.sign_op1),0) end,  
        sum_sup_0  = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate =  0 then (ABS(lm.sum_sup)-ABS(lm.svat_sup))     else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate =  0 then (lm.sum_sup - lm.svat_sup)   else 0 end * lm.sign_op1),0) end,  
        sum_sup_10 = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate = 10 then (ABS(lm.sum_sup)-ABS(lm.svat_sup))     else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate = 10 then (lm.sum_sup - lm.svat_sup)   else 0 end * lm.sign_op1),0) end,  
        sum_sup_18 = case when lm.id_table = 24 then isnull(sum(case when L.tax_rate = 18 then (ABS(lm.sum_sup)-ABS(lm.svat_sup))     else 0 end * lm.sign_op),0) else isnull(sum(case when L.tax_rate = 18 then (lm.sum_sup - lm.svat_sup)   else 0 end * lm.sign_op1),0) end  
        FROM
            (
                select 
                     lm1.SIGN_OP1    
	                ,lm1.sum_acc
	                ,lm1.discount_acc
	                ,lm1.sum_sup
	                ,lm1.svat_sup
	                ,lm1.id_lot_global
	                ,lm1.id_document
	                ,lm1.id_table
	                ,lm1.date_op
	                ,l.id_goods
	                ,l.id_store  /*lm1.* */ 
	                ,sign_op = case when (lm1.QUANTITY_ADD - lm1.QUANTITY_SUB) > 0 then 1 else -1 end
                from 
                  MV_LOT_MOVEMENT_SIGN_OP1 lm1
                  inner join lot l WITH(NOLOCK) on l.id_lot_global = lm1.id_lot_global 
                where (1=1)
                    and (l.id_store is not null)
                    and ((lm1.id_table = 24 and lm1.OP<>'SUB_RES') OR (lm1.id_table <> 24))
                    and exists (select id_store from @stores s where s.id_store = l.id_store) 
                    and 
                    (
                        lm1.id_table = 30                                                               
                        or (lm1.id_table = 2 and exists(select null from contractor c where name ='СТУ' and exists(select null from store s where s.id_store = l.id_store and s.id_contractor = c.id_contractor)))
                        or (lm1.id_table = 2 and exists(select null from contractor c where name<>'СТУ' and exists(select null from store s where s.id_store = l.id_store and s.id_contractor = c.id_contractor)) and lm1.date_op<@date_fr)
                        or (lm1.id_table in (3,6,12,13,20,21,24) and lm1.date_op < @date_fr)  
                        or (
                                lm1.id_table in (8,37,38,39) 
                                and lm1.date_op < @date_fr   
                                and (@is_filtered=0 or (@is_filtered=1 and exists(select null from #id_doc_movement idm where idm.id_document = lm1.id_document)))
                           )
                )   
            ) lm
            inner join all_document ad WITH(NOLOCK) on ad.id_document_global = lm.id_document

            --inner join goods g WITH(NOLOCK) on g.id_goods = lm.id_goods  
            --inner join tax_type tt WITH(NOLOCK) on tt.id_tax_type = g.id_tax_type  
            --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
            INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = lm.ID_LOT_GLOBAL 
        group by 
            lm.id_table --,ad.doc_num+' от '+CONVERT(VARCHAR(8),AD.DOC_DATE,4)

PRINT 'ВХОДЯЩИЙ Остаток'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()


---------------------------------------------------------------------------------------------------
-- вычит корректировки ТО
---------------------------------------------------------------------------------------------------
INSERT INTO #VO_ITEM
    SELECT
        NUM_DOC    = ad.doc_num+' от '+CONVERT(VARCHAR(8),DM.DATE_OP,4),
		sum_acc    = isnull(sum(case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end * (dm.sum_acc/*+lm.discount_acc*/)),0), --с учетом скидки
		sum_sup    = isnull(sum(case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end *(dm.sum_sup-dm.svat_sup)),0),
		svat_sup   = isnull(sum(case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end * dm.svat_sup),0),
		sum_acc_0  = isnull(sum(case when dm.vat_rate =  0 then (dm.sum_acc/*+lm.discount_acc*/) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0),  
		sum_acc_10 = isnull(sum(case when dm.vat_rate = 10 then (dm.sum_acc/*+lm.discount_acc*/) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0),
		sum_acc_18 = isnull(sum(case when dm.vat_rate = 18 then (dm.sum_acc/*+lm.discount_acc*/) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0),
		sum_sup_0  = isnull(sum(case when dm.vat_rate =  0 then (dm.sum_sup - dm.svat_sup) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0),
		sum_sup_10 = isnull(sum(case when dm.vat_rate = 10 then (dm.sum_sup - dm.svat_sup) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0),
		sum_sup_18 = isnull(sum(case when dm.vat_rate = 18 then (dm.sum_sup - dm.svat_sup) else 0 end * case dm.CODE_OP when 'ADD' then 1 when 'SUB' then -1 end),0)
	from 
    	doc_movement  dm WITH(NOLOCK)
	    inner join all_document ad WITH(NOLOCK) on ad.id_document_global = dm.id_document
	where
	    dm.id_table = 26 
	    and DM.DATE_OP < @DATE_FR  --and DOC_DATE < @DATE_FR 
		and @IS_EXIST_GOODS = 0
	group by 
		ad.doc_num+' от '+CONVERT(VARCHAR(8),DM.DATE_OP,4)

---------------------------------------------------------------------------------------------------
-- чеки
---------------------------------------------------------------------------------------------------
INSERT INTO #VO_ITEM
    SELECT 
        --NUM_DOC = CH.mnemocode+' от '+ CONVERT(VARCHAR(8),ch.DATE_CHEQUE,4),
        NUM_DOC = NULL,
        
        --SUM_ACC    = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT)),0),
        --SUM_SUP    = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP)),0),
        --SVAT_SUP   = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * PVAT_SUP),0),
        --SUM_ACC_0  = ISNULL(SUM(CASE WHEN tt.tax_rate= 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
        --SUM_ACC_10 = ISNULL(SUM(CASE WHEN tt.tax_rate=10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
        --SUM_ACC_18 = ISNULL(SUM(CASE WHEN tt.tax_rate=18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * (CH_I.SUMM+CH_I.SUMM_DISCOUNT) ELSE 0 END),0),
        --SUM_SUP_0  = ISNULL(SUM(CASE WHEN tt.tax_rate= 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0),
        --SUM_SUP_10 = ISNULL(SUM(CASE WHEN tt.tax_rate=10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0),
        --SUM_SUP_18 = ISNULL(SUM(CASE WHEN tt.tax_rate=18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.QUANTITY * (PRICE_SUP-PVAT_SUP) ELSE 0 END),0)

        SUM_ACC    = ISNULL(SUM(LM.SIGN_OP1 * (LM.SUM_ACC+LM.DISCOUNT_ACC)),0),
        SUM_SUP    = ISNULL(SUM(LM.SIGN_OP1 * LM.QUANTITY * (L.PRICE_SUP-L.PVAT_SUP)),0),
        SVAT_SUP   = ISNULL(SUM(LM.SIGN_OP1 * LM.QUANTITY * L.PVAT_SUP),0),
        SUM_ACC_0  = ISNULL(SUM(CASE WHEN L.tax_rate= 0 THEN LM.SIGN_OP1 * (LM.SUM_ACC+LM.DISCOUNT_ACC) ELSE 0 END),0),
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN L.tax_rate=10 THEN LM.SIGN_OP1 * (LM.SUM_ACC+LM.DISCOUNT_ACC) ELSE 0 END),0),
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN L.tax_rate=18 THEN LM.SIGN_OP1 * (LM.SUM_ACC+LM.DISCOUNT_ACC) ELSE 0 END),0),
        SUM_SUP_0  = ISNULL(SUM(CASE WHEN L.tax_rate= 0 THEN LM.SIGN_OP1 * LM.QUANTITY * (L.PRICE_SUP-L.PVAT_SUP) ELSE 0 END),0),
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN L.tax_rate=10 THEN LM.SIGN_OP1 * LM.QUANTITY * (L.PRICE_SUP-L.PVAT_SUP) ELSE 0 END),0),
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN L.tax_rate=18 THEN LM.SIGN_OP1 * LM.QUANTITY * (L.PRICE_SUP-L.PVAT_SUP) ELSE 0 END),0)

        FROM 
            CHEQUE CH WITH(NOLOCK)
            INNER JOIN #CHEQUE_PM_CACHE PM WITH(NOLOCK) ON PM.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
            INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
            INNER JOIN MV_LOT_MOVEMENT_SIGN_OP1 LM   ON LM.ID_DOCUMENT_ITEM = CH_I.ID_CHEQUE_ITEM_GLOBAL

            INNER JOIN CASH_SESSION CS WITH(NOLOCK)  ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL 
            INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
            INNER JOIN CONTRACTOR C WITH(NOLOCK)     ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
            
            --INNER JOIN LOT L WITH(NOLOCK)            ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL
            --INNER JOIN GOODS G WITH(NOLOCK)          ON G.ID_GOODS = L.ID_GOODS
            --INNER JOIN TAX_TYPE TT WITH(NOLOCK)      ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
            --RIGHT JOIN #GOODS Fg WITH(NOLOCK)        ON fg.id_goods = G.id_goods
            INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL

        WHERE 
            CH.DATE_CHEQUE < @DATE_FR 
            AND C.ID_CONTRACTOR = @ID_AU 
            AND CH.DOCUMENT_STATE = 'PROC' 
            --AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
            --AND CH.CHEQUE_TYPE in ('SALE','RETURN') -- лишняя проверка у других чеков нет CHEQUE_ITEM
            AND exists(select null from @stores s where s.id_store = l.id_store)
        --GROUP BY  
        --    CH.mnemocode+' от '+ CONVERT(VARCHAR(8),ch.DATE_CHEQUE,4)


---------------------------------------------------------------------------------------------------
-- Из таблицы #VO_ITEM собираю суммы INTO #VO 
---------------------------------------------------------------------------------------------------
SELECT   
  NUM_DOC = CASE WHEN @USE_DIAGN_REPORT=1 and @INV_REMAINDER=1 THEN NUM_DOC ELSE '' END,
  SUM_ACC = SUM(SUM_ACC),  
  SUM_SUP = SUM(SUM_SUP),  
  SVAT_SUP = SUM(CASE WHEN @USE_VAT=1 THEN 0 ELSE SVAT_SUP END),  
  SUM_ACC_0 = SUM(SUM_ACC_0),  
  SUM_ACC_10 = SUM(SUM_ACC_10),  
  SUM_ACC_18 = SUM(SUM_ACC_18),  
  SUM_SUP_0  = SUM(SUM_SUP_0),  
  SUM_SUP_10 = SUM(SUM_SUP_10),  
  SUM_SUP_18 = SUM(SUM_SUP_18)  
  INTO #VO 
  FROM #VO_ITEM WITH(NOLOCK)
  GROUP BY CASE WHEN @USE_DIAGN_REPORT=1 and @INV_REMAINDER=1 THEN NUM_DOC ELSE '' END 

PRINT 'вычит корректировки ТО'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
--select @DATE_FR
--select * from #vo
--return

---------------------------------------------------------------------------------------------------
--- приход
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#MOVE') is not null drop table #MOVE

CREATE TABLE #MOVE 
(
    idRec           INT IDENTITY(1,1) PRIMARY KEY,

    SECTION_NUMBER  BIGINT,  
    ORDER_BY        MONEY,  
    ID_TABLE        BIGINT,  
    TABLE_NAME      NVARCHAR(MAX),  
    CONTRACTOR      NVARCHAR(MAX),  
    SECTION_NAME    NVARCHAR(MAX),   
    DOC_NUM         NVARCHAR(MAX),  
    DOC_NUM_SUP     NVARCHAR(MAX),   
    DATE_DOC        DATETIME, 
    PREFIX          NVARCHAR(MAX),
    SUM_ACC         MONEY,  
    SUM_SUP         MONEY,  
    SVAT_SUP        MONEY,  
    SUM_ACC_0       MONEY,  
    SUM_ACC_10      MONEY,  
    SUM_ACC_18      MONEY,  
    SUM_SUP_0       MONEY,  
    SUM_SUP_10      MONEY,  
    SUM_SUP_18      MONEY,
    CASH            NVARCHAR(MAX),
    KIND            NVARCHAR(MAX),
    RETURN_OR_BUY   NVARCHAR(MAX),
    DATE_GR         DATETIME
)
CREATE INDEX IX_#MOVE$SECTION_NUMBER ON #MOVE(SECTION_NUMBER)   

PRINT 'приход'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()


---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(1)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT  
        SECTION_NUMBER = 1,  
        ORDER_BY=case LM.id_table when 2  then case when c.use_vat=1 then -1 else 0 end
								  when 12 then 1
								  when 8  then 2
								  when 37 then 3
								  when 38 then 4
								  when 39 then 5 end,  
        ID_TABLE = LM.ID_TABLE,  
        TABLE_NAME = CASE LM.ID_TABLE WHEN 2  THEN case when c.use_vat=1 then 'Приход от поставщика (плательщики НДС)' else 'Приход от поставщика (не плательщики НДС)' end
                                      WHEN 8  THEN 'Приход от аптек итого'
                                      WHEN 37 THEN 'Приход от аптек итого'
                                      WHEN 38 THEN 'Приход от аптек итого'
                                      WHEN 39 THEN 'Приход от аптек итого'
                                      WHEN 12 THEN 'Возврат от покупателя итого'
                                      --WHEN 24 THEN 'Излишки по инвентаризации итого' 
                                      END, 
--                                   WHEN 13 THEN 'Переоценка' END,  
        CONTRACTOR = CASE WHEN LM.ID_TABLE  IN (8,37,38,39) THEN C_38.NAME ELSE C.NAME END,  
        SECTION_NAME='',  
        
        DOC_NUM = 
            CASE   
                WHEN LM.ID_TABLE = 2 THEN CONVERT(VARCHAR,CONVERT(INT,RIGHT(I.MNEMOCODE,8)))+' от ' +CONVERT(VARCHAR(8),I.DOCUMENT_DATE,4)--I.INCOMING_NUMBER + ' от ' + CONVERT(VARCHAR, I.INCOMING_DATE, 104) + ' № '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104)  
                WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '++ CONVERT(VARCHAR(8),LM.DATE_OP,4)-- CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,LM.DATE_OP))),104)  
                ELSE m.mnemocode+'/' + AD.DOC_NUM+' от '+CONVERT(VARCHAR(8),LM.DATE_OP,4)-- CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,LM.DATE_OP))),104) 
            END,
              
        DOC_NUM_SUP = CASE WHEN LM.ID_TABLE = 2 THEN '№'+I.INCOMING_NUMBER + CASE WHEN I.INCOMING_DATE IS NOT NULL THEN ' от '+CONVERT(VARCHAR(8),I.INCOMING_DATE,4) ELSE '' END ELSE '' END,  
        DATE_DOC = LM.DATE_OP,  
        PREFIX = '',
        SUM_ACC = ISNULL(SUM(LM.SUM_ACC + case when lm.id_table=12 then lm.discount_acc else 0 end),0),--CASE WHEN LM.ID_TABLE!=12 THEN 0 ELSE ISNULL(SUM(CH_VAT.SUM_DISCOUNT),0) END,  
        SUM_SUP = ISNULL(SUM((LM.SUM_SUP - LM.SVAT_SUP)),0),  
        SVAT_SUP = SUM(CASE WHEN (LM.ID_TABLE=2 AND @USE_VAT=1)OR(@USE_VAT=0) THEN ISNULL(LM.SVAT_SUP,0) ELSE 0 END),  
        SUM_ACC_0  = ISNULL(SUM(CASE WHEN L.tax_rate = 0 THEN SUM_ACC + case when lm.id_table=12 then lm.discount_acc else 0 end ELSE 0 END),0),  
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN L.tax_rate = 10 THEN SUM_ACC + case when lm.id_table=12 then lm.discount_acc else 0 end ELSE 0 END),0),  
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN L.tax_rate = 18 THEN SUM_ACC + case when lm.id_table=12 then lm.discount_acc else 0 end ELSE 0 END),0),   
        SUM_SUP_0  = ISNULL(SUM(CASE WHEN L.tax_rate = 0 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),0),  
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN L.tax_rate = 10 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),0),  
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN L.tax_rate = 18 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),0),
        
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = CASE WHEN LM.ID_TABLE = 38 THEN m.mnemocode ELSE '' end,
		DATE_GR = null  
    
    FROM 
        --LOT_MOVEMENT LM WITH(NOLOCK) 
        MV_LOT_MOVEMENT_SIGN_OP1 LM WITH(NOLOCK)  
        INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT 

        --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL  
        --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS  
        --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE  
        --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods 
        INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL 

        LEFT JOIN INVOICE I WITH(NOLOCK) ON I.ID_INVOICE_GLOBAL = LM.ID_DOCUMENT  
        LEFT JOIN
        (  
            SELECT  
                ID_CHEQUE,  
                T.TAX_RATE,  
                SUM_DISCOUNT = isnull(SUM(case ch.cheque_type when 'sale' then 1 when 'return' then -1 end * CHI.SUMM_DISCOUNT),0)  
            FROM 
                CHEQUE_ITEM CHI WITH(NOLOCK)  
                INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL  
                INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS  
                INNER JOIN TAX_TYPE T WITH(NOLOCK) ON T.ID_TAX_TYPE = G.ID_TAX_TYPE  
                INNER JOIN CHEQUE CH WITH(NOLOCK) ON CH.ID_CHEQUE_GLOBAL = CHI.ID_CHEQUE_GLOBAL  
            GROUP BY 
                ID_CHEQUE, T.TAX_RATE
        ) 
        CH_VAT ON CH_VAT.ID_CHEQUE = AD.ID_DOCUMENT_BASE AND CONVERT(MONEY,CH_VAT.tax_rate) = L.tax_rate  

        LEFT JOIN CONTRACTOR C WITH(NOLOCK) ON (C.ID_CONTRACTOR = AD.ID_CONTRACTOR_FROM AND LM.ID_TABLE not in (8,21,37,38,39))
							      or
							      (LM.ID_TABLE in (8,21,37,38,39) and c.ID_CONTRACTOR = ad.ID_CONTRACTOR_TO)
        left join interfirm_moving_acceptance_act a WITH(NOLOCK) on a.ID_INTERFIRM_MOVING_ACCEPTANCE_ACT_GLOBAL = lm.id_document and lm.id_table = 38
        left join interfirm_moving m WITH(NOLOCK) on m.ID_INTERFIRM_MOVING_GLOBAL = a.ID_INTERFIRM_MOVING_GLOBAL
        left join interfirm_moving_refusal_act r WITH(NOLOCK) on r.id_interfirm_moving_refusal_act_global = lm.id_document and lm.id_table = 39
        left join interfirm_moving m1 WITH(NOLOCK) on m1.ID_INTERFIRM_MOVING_GLOBAL = r.ID_INTERFIRM_MOVING_GLOBAL
        left join store s WITH(NOLOCK) on s.id_store = m.id_store_from_main or s.id_store = m1.id_store_from_main 
        Left JOIN CONTRACTOR C_38 WITH(NOLOCK) ON C_38.ID_CONTRACTOR = AD.ID_CONTRACTOR_FROM
    
    WHERE 
        LM.DATE_OP >= @DATE_FR AND LM.DATE_OP <= @DATE_TO   
        AND EXISTS (SELECT ID_STORE FROM @STORES WHERE (ID_STORE = AD.ID_STORE1 and ad.id_table in (2,12,24))or(ID_STORE = AD.ID_STORE_to and ad.id_table in (8,37,38,39)))  
        AND 
        (
            LM.ID_TABLE IN (12)   
            OR (LM.ID_TABLE = 2 AND NOT EXISTS (SELECT ID_CONTRACTOR FROM CONTRACTOR c1 WITH(NOLOCK) WHERE NAME='СТУ' AND c1.id_contractor = c.id_contractor))
            OR LM.ID_TABLE IN (8,37,38,39)   
               AND (
                        @IS_FILTERED=0 OR (@IS_FILTERED=1 AND EXISTS(SELECT NULL FROM #ID_DOC_MOVEMENT IDM WITH(NOLOCK) WHERE IDM.ID_DOCUMENT = LM.ID_DOCUMENT))
                   )
        )  
        AND LM.OP1 = 'ADD'  
        
    GROUP BY 
        LM.ID_TABLE,  
        LM.ID_DOCUMENT,
        CASE LM.ID_TABLE 
            WHEN 2 THEN case when c.use_vat=1 then 'Приход от поставщика (плательщики НДС)' else 'Приход от поставщика (не плательщики НДС)' end
            WHEN 8 THEN 'Приход от аптек итого'
            WHEN 37 THEN 'Приход от аптек итого1'
            WHEN 38 THEN 'Приход от аптек итого2'
            WHEN 39 THEN 'Приход от аптек итого3'
            WHEN 12 THEN 'Возврат от покупателя итого'
            --WHEN 24 THEN 'Излишки по инвентаризации итого' 
        END,  
        CASE WHEN LM.ID_TABLE IN (8,37,38,39) THEN C_38.NAME ELSE C.NAME END,  
        c.use_vat,
        CASE WHEN LM.ID_TABLE = 38 THEN m.mnemocode ELSE '' end,
        CASE
            WHEN LM.ID_TABLE = 2 THEN CONVERT(VARCHAR,CONVERT(INT,RIGHT(I.MNEMOCODE,8)))+' от ' +CONVERT(VARCHAR(8),I.DOCUMENT_DATE,4)  
            WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ + CONVERT(VARCHAR(8),LM.DATE_OP,4)
            ELSE m.mnemocode+'/' + AD.DOC_NUM+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4)
        END,  
        CASE 
            WHEN LM.ID_TABLE = 2 THEN '№'+I.INCOMING_NUMBER + CASE WHEN I.INCOMING_DATE IS NOT NULL THEN ' от '+CONVERT(VARCHAR(8),I.INCOMING_DATE,4) ELSE '' END 
            ELSE '' 
        END,  
        LM.DATE_OP  

PRINT 'INSERT INTO #MOVE(1)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

-----------------------------------------------------------------------
-- Приход от всех поставщиков
-----------------------------------------------------------------------    
IF (@USE_DIAGN_REPORT=0) BEGIN
  
INSERT INTO #MOVE
	SELECT
		SECTION_NUMBER,
		ORDER_BY = 0,
		ID_TABLE = 1,
		TABLE_NAME = 'Итого приход от всех поставщиков',
		CONTRACTOR = '',
		SECTION_NAME = '!!!',
		DOC_NUM = '',
		DOC_NUM_SUP = '',
		DATE_DOC = '',
		PREFIX = '',
		SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
		SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
		SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
		SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
		SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
		SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
		SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
		SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
		SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0),
		CASH = '',
		KIND = '',
		RETURN_OR_BUY = '',
		DATE_GR = null 
	FROM #MOVE
	WHERE TABLE_NAME = 'Приход от поставщика (плательщики НДС)' OR TABLE_NAME = 'Приход от поставщика (не плательщики НДС)'
	GROUP BY SECTION_NUMBER
	
END

PRINT 'Приход от всех поставщиков'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(2)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
	SELECT
        SECTION_NUMBER = 1,
        ORDER_BY = 6,--0,
        ID_TABLE = 24,
        TABLE_NAME = 'Излишки по инвентаризации итого',
        CONTRACTOR = lm.contractor,
        SECTION_NAME='',
        DOC_NUM = lm.doc_num,--'№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,AD.DOC_DATE))),104),
        DOC_NUM_SUP = '',
        DATE_DOC = lm.doc_date,--AD.DOC_DATE,
        PREFIX = '',
        SUM_ACC = ISNULL(SUM_SUM_ACC,0),--CASE WHEN LM.ID_TABLE!=12 THEN 0 ELSE ISNULL(SUM(CH_VAT.SUM_DISCOUNT),0) END,
        SUM_SUP = ISNULL(SUM_SUM_SUP,0),
        SVAT_SUP = 0,
        SUM_ACC_0 = SUM(ISNULL(CASE WHEN VAT = 0 THEN SUM_ACC ELSE 0 END,0)),        
        SUM_ACC_10 = SUM(ISNULL(CASE WHEN VAT = 10 THEN SUM_ACC ELSE 0 END,0)),
        SUM_ACC_18 = SUM(ISNULL(CASE WHEN VAT = 18 THEN SUM_ACC ELSE 0 END,0)), 
        SUM_SUP_0 = SUM(ISNULL(CASE WHEN VAT = 0 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        SUM_SUP_10 = SUM(ISNULL(CASE WHEN VAT = 10 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        SUM_SUP_18 = SUM(ISNULL(CASE WHEN VAT = 18 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = '',
		DATE_GR = null  
	from 
	    @inventory lm
	    inner join all_document ad WITH(NOLOCK) on ad.id_document_global = lm.id_document	
	where 
	    sign_op=1
	group by 
	    lm.contractor, 
	    lm.doc_num,
	    lm.doc_date,
	    ISNULL(SUM_SUM_ACC,0),ISNULL(SUM_SUM_SUP,0)
 
PRINT 'INSERT INTO #MOVE(2)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

 
---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(3)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT  *
    from #SERVICES WITH(NOLOCK)
   -- Where (SUM_ACC+SUM_SUP+SVAT_SUP+SUM_ACC_0+SUM_ACC_10+SUM_ACC_18+SUM_SUP_0+SUM_SUP_10+SUM_SUP_18<>0)


---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(4)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
	SELECT  *
    from #DISCOUNT WITH(NOLOCK)


---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(5)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
	SELECT  *
    from #SK WITH(NOLOCK)


INSERT INTO #MOVE
	SELECT  *
    from #INDEMNITY WITH(NOLOCK)

    
---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(6)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
	SELECT  *
    from #CHEQUE WITH(NOLOCK)


PRINT 'INSERT INTO #MOVE(3-6)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(7)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT  
        SECTION_NUMBER = 2,  
        ORDER_BY = CASE LM.ID_TABLE WHEN 3 THEN 16  
                                    WHEN 21 THEN 17 END,--0,  
        ID_TABLE = LM.ID_TABLE,  
        TABLE_NAME = MAX('Возврат поставщику:итого'),  
        CONTRACTOR = C.NAME,  
        SECTION_NAME = CASE LM.ID_TABLE WHEN 3 THEN '-по претензии итого'  
                                        WHEN 21 THEN '-обратная реализация' END,  
        DOC_NUM = '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4),  
        DOC_NUM_SUP='',  
        DATE_DOC = LM.DATE_OP,  
        PREFIX ='',                
        SUM_ACC = SUM(LM.SUM_ACC),  
        SUM_SUP = SUM((LM.SUM_SUP - LM.SVAT_SUP)),  
        SVAT_SUP = SUM(CASE WHEN @USE_VAT = 1 THEN 0 ELSE 1 END * LM.SVAT_SUP),  
        SUM_ACC_0 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 0 THEN SUM_ACC ELSE 0 END),  
        SUM_ACC_10 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 10 THEN SUM_ACC ELSE 0 END),  
        SUM_ACC_18 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 18 THEN SUM_ACC ELSE 0 END),  
        SUM_SUP_0 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 0 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),  
        SUM_SUP_10 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 10 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),  
        SUM_SUP_18 = SUM(CASE WHEN convert(bigint,L.tax_rate) = 18 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END),
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = '',
		DATE_GR = null    
    
        FROM 
            --LOT_MOVEMENT LM WITH(NOLOCK) 
            MV_LOT_MOVEMENT_SIGN_OP1 LM WITH(NOLOCK) 
            INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT  
            INNER JOIN CONTRACTOR C WITH(NOLOCK) ON C.ID_CONTRACTOR = AD.ID_CONTRACTOR_TO   

            --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
            --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
            --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
            --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
            INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL   

        WHERE 
            LM.DATE_OP >= @DATE_FR AND LM.DATE_OP <= @DATE_TO   
            AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = AD.ID_STORE1)  
            AND (LM.ID_TABLE = 3 OR (LM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WITH(NOLOCK) WHERE IS_SUPPLIER=1 AND ID_INVOICE_OUT_GLOBAL = LM.ID_DOCUMENT)))  
            --AND DBO.FN_LOT_MOVEMENT_OP(LM.ID_LOT_MOVEMENT) = 'SUB'
            AND LM.OP1 = 'SUB'  
        GROUP BY 
            LM.ID_TABLE,AD.ID_CONTRACTOR_TO,LM.ID_DOCUMENT,  
            CASE LM.ID_TABLE WHEN 3 THEN 'по претензии итого'  
                             WHEN 21 THEN 'обратная реализация' END,  
            C.NAME,  
            '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4),  
            LM.DATE_OP  
  
PRINT 'INSERT INTO #MOVE(7)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(8)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT
        SECTION_NUMBER = 2,  
        ORDER_BY = CASE LM.ID_TABLE WHEN 13 THEN 13/*1*/  
                                    WHEN 6 THEN 14/*2*/ ELSE 12/*0*/ END,  
        ID_TABLE = LM.ID_TABLE,  
        TABLE_NAME = 'Расход(опт) итого',
        CONTRACTOR = C_TO.NAME,  
        SECTION_NAME = 'в т.ч.скидка итого',  
        DOC_NUM = '№ '+SUBSTRING(LM.DOC_NUM,LEN(LM.DOC_NUM)-7,8)+' от '+CONVERT(VARCHAR(8),LM.DATE_OP,4),  
        DOC_NUM_SUP = '',  
        DATE_DOC = LM.DATE_OP, 
        PREFIX ='',        
        SUM_ACC = SUM(ISNULL(LM.DISCOUNT_ACC,0)),  
        SUM_SUP = CONVERT(MONEY,0),  
        SVAT_SUP = CONVERT(MONEY,0),  
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 0 THEN ISNULL(LM.DISCOUNT_ACC,0) ELSE 0 END),0),  
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 10 THEN ISNULL(LM.DISCOUNT_ACC,0) ELSE 0 END),0),  
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 18 THEN ISNULL(LM.DISCOUNT_ACC,0) ELSE 0 END),0),  
        SUM_SUP_0 = CONVERT(MONEY,0),  
        SUM_SUP_10 = CONVERT(MONEY,0),  
        SUM_SUP_18 = CONVERT(MONEY,0),
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = '',
		DATE_GR =  null    
    
        FROM 
            #LM_DISCOUNT LM WITH(NOLOCK)
            INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT  
            LEFT JOIN CONTRACTOR C_TO WITH(NOLOCK) ON C_TO.ID_CONTRACTOR = AD.ID_CONTRACTOR_TO AND LM.ID_TABLE in (8,21)  
            LEFT JOIN ACT_DEDUCTION AD1 WITH(NOLOCK) ON AD1.ID_ACT_DEDUCTION_GLOBAL = LM.ID_DOCUMENT  
            LEFT JOIN ENUMERATION_VALUE EV WITH(NOLOCK) ON EV.VALUE = AD1.DEDUCTION_REASON  
        WHERE
            (LM.DISCOUNT_ACC>0) 
            --AND (DBO.FN_LOT_MOVEMENT_OP(LM.ID_LOT_MOVEMENT)= 'SUB') 
            AND (LM.OP1= 'SUB') 
        GROUP BY 
            LM.ID_TABLE,
            AD.ID_CONTRACTOR_TO,
            LM.ID_DOCUMENT,  
            C_TO.NAME,  
            '№ '+SUBSTRING(LM.DOC_NUM,LEN(LM.DOC_NUM)-7,8)+' от '+CONVERT(VARCHAR(8),LM.DATE_OP,4),  
            LM.DATE_OP  
        HAVING 
            round(SUM(ISNULL(LM.SUM_ACC,0)),2)<>0 or round(SUM(ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0)),2)<>0 

PRINT 'INSERT INTO #MOVE(8)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(9)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT  
        SECTION_NUMBER = 2,  
        ORDER_BY = CASE LM.ID_TABLE WHEN 21 THEN 15
									WHEN 8 THEN 18
									WHEN 37 THEN 19
									WHEN 38 THEN 20
									WHEN 39 THEN 21
									WHEN 13 THEN 24
									WHEN 6 THEN 25
									WHEN 20 THEN 22 END,
        --ORDER_BY = CASE LM.ID_TABLE WHEN 13 THEN 1  
        --                            WHEN 6 THEN 2 ELSE 0 END,  
      ID_TABLE = LM.ID_TABLE,  
        TABLE_NAME = CASE LM.ID_TABLE WHEN 8 THEN 'Перемещение в аптеки итого'                                        
                                      WHEN 37 THEN 'Перемещение в аптеки итого'  
                                      WHEN 38 THEN 'Перемещение в аптеки итого'  
                                      WHEN 39 THEN 'Перемещение в аптеки итого'  
                                      WHEN 20 THEN 'Списание итого'  
                                      WHEN 21 THEN 'Расход(опт) итого'  
--                                      WHEN 24 THEN 'Недостача по инвентаризации итого'   
                                      WHEN 13 THEN 'Переоценка'  
                                      WHEN 6 THEN 'Разборка итого' END,  
          CONTRACTOR = CASE WHEN LM.ID_TABLE not in (8,21,37,38,39) THEN C.NAME ELSE CASE WHEN LM.ID_TABLE = 21 THEN 
				isnull(C_TO21.NAME,'') 
				ELSE 
				CASE WHEN LM.ID_TABLE = 38 THEN ISNULL(C_38.NAME,'')
				else
				C_TO.NAME end END END,  /* EV.DESCRIPTION dr.name*/ 
        SECTION_NAME = CASE WHEN LM.ID_TABLE = 20 THEN
														case when LEN(ISNULL(EV.DESCRIPTION,''))=0 then 
															case when LEN(ISNULL(dr.name,''))=0 then  'Прочие списания' else dr.name end
														 else EV.DESCRIPTION end 
												ELSE  '' END,  
        DOC_NUM = CASE WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4) 
															ELSE 
																CASE 
																WHEN 
																	LM.ID_TABLE = 38
																	THEN
																		m.mnemocode+'/' + AD.DOC_NUM+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4)
																	ELSE
															AD.DOC_NUM+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4) 
															end
															--+case lm.id_table when 38 then '('+m.MNEMOCODE+')'
															--				  when 39 then '('+m1.MNEMOCODE+')' else '' 
															--				  end 
															END,  
															-- m.mnemocode+'/' + AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,LM.DATE_OP))),104),  
        DOC_NUM_SUP = '',  
        DATE_DOC = CASE WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN LM.DATE_OP ELSE convert(datetime,floor(CONVERT(MONEY,LM.DATE_OP))) END,  
        PREFIX = ISNULL(case when LM.ID_TABLE IN (8,37,38,39) then substring(ad.doc_num,0,patindex('%/%',ad.doc_num)) else '' end,''),
        SUM_ACC = SUM(CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * 
                      ISNULL(LM.SUM_ACC,0)),  
        SUM_SUP = SUM(CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * 
                      ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0)),  
        SVAT_SUP = SUM(CASE WHEN @USE_VAT=1 THEN 0 ELSE 1 END * CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB' then 1 when 'ADD' then -1 end END * 
                      ISNULL((LM.SVAT_SUP),0)),  
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 0 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL(LM.SUM_ACC,0) ELSE 0 END),0),  
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 10 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL(LM.SUM_ACC,0) ELSE 0 END),0),  
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 18 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL(LM.SUM_ACC,0) ELSE 0 END),0),  
        SUM_SUP_0 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 0 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0) ELSE 0 END),0),  
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 10 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0) ELSE 0 END),0),  
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 18 THEN CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE case LM.OP1 when 'SUB'then 1 when 'ADD' then -1 end END * ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0) ELSE 0 END),0)  ,
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = CASE WHEN LM.ID_TABLE = 38 THEN m.mnemocode ELSE '' end,
		DATE_GR =  null  

    FROM 
    --LOT_MOVEMENT LM WITH(NOLOCK)
    MV_LOT_MOVEMENT_SIGN_OP1 LM WITH(NOLOCK)
   
    --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL  
    --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS  
    --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE  
    --right join #GOODS Fg on fg.id_goods = G.id_goods
    INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
   
    INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT  
    LEFT JOIN CONTRACTOR C WITH(NOLOCK) ON C.ID_CONTRACTOR = AD.ID_CONTRACTOR_FROM AND LM.ID_TABLE not in (8,21,37,38,39)  
    left join interfirm_moving_acceptance_act a WITH(NOLOCK) on a.ID_INTERFIRM_MOVING_ACCEPTANCE_ACT_GLOBAL = LM.id_document and LM.id_table = 38
    left join interfirm_moving m WITH(NOLOCK) on m.ID_INTERFIRM_MOVING_GLOBAL = a.ID_INTERFIRM_MOVING_GLOBAL
    left join interfirm_moving_refusal_act r WITH(NOLOCK) on r.id_interfirm_moving_refusal_act_global = LM.id_document and LM.id_table = 39
    left join interfirm_moving m1 WITH(NOLOCK) on m1.ID_INTERFIRM_MOVING_GLOBAL = r.ID_INTERFIRM_MOVING_GLOBAL
    left join store s WITH(NOLOCK) on s.id_store = m.id_store_from_main or s.id_store = m1.id_store_from_main
    LEFT JOIN CONTRACTOR C_TO WITH(NOLOCK) ON C_TO.ID_CONTRACTOR = AD.ID_CONTRACTOR_FROM AND LM.ID_TABLE in (8,21,37,38,39)  
    LEFT JOIN CONTRACTOR C_TO21 WITH(NOLOCK) ON C_TO21.ID_CONTRACTOR = AD.ID_CONTRACTOR_TO AND LM.ID_TABLE in (21)
    Left JOIN CONTRACTOR C_38 WITH(NOLOCK) ON C_38.ID_CONTRACTOR = AD.ID_CONTRACTOR_TO
    LEFT JOIN ACT_DEDUCTION AD1 WITH(NOLOCK) ON AD1.ID_ACT_DEDUCTION_GLOBAL = LM.ID_DOCUMENT  
    LEFT JOIN ENUMERATION_VALUE EV WITH(NOLOCK) ON EV.VALUE = AD1.DEDUCTION_REASON  
    left join DEDUCTION_REASON dr WITH(NOLOCK) on dr.ID_DEDUCTION_REASON = ad1.ID_DEDUCTION_REASON
    

   WHERE  (LM.DATE_OP >= @DATE_FR AND LM.DATE_OP <= @DATE_TO)  
          AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = AD.ID_STORE1)  
           AND (  
                ((LM.ID_TABLE IN (20) OR 
                 (LM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WITH(NOLOCK) WHERE IS_SUPPLIER=0 AND ID_INVOICE_OUT_GLOBAL = LM.ID_DOCUMENT))  --РН без "обратная реализация"
                  OR (LM.ID_TABLE in (8,37,38,39) AND (@IS_FILTERED=0 OR (@IS_FILTERED=1 AND   
                                                            EXISTS(SELECT NULL FROM #ID_DOC_MOVEMENT IDM WITH(NOLOCK) WHERE IDM.ID_DOCUMENT = LM.ID_DOCUMENT)))))  
                 --AND LM.OP = 'SUB'
                AND LM.OP1 = 'SUB' 
                )OR  
                (LM.ID_TABLE IN (6,13)))-- AND LM.OP IN ('ADD','SUB')))  
   GROUP BY 
			LM.ID_TABLE,
            AD.ID_CONTRACTOR_FROM,
            AD.ID_CONTRACTOR_TO,--DM.ID_CONTRACTOR,
            LM.ID_DOCUMENT,  
            CASE LM.ID_TABLE WHEN 8 THEN 'Перемещение в аптеки итого'                                        
                              WHEN 37 THEN 'Перемещение в аптеки итого1'  
                              WHEN 38 THEN 'Перемещение в аптеки итого2'  
                              WHEN 39 THEN 'Перемещение в аптеки итого3'  
                              WHEN 20 THEN 'Списание итого'  
                              WHEN 21 THEN 'Расход(опт) итого'  
                              --WHEN 24 THEN 'Недостача по инвентаризации итого'   
                              WHEN 13 THEN 'Переоценка'  
                              WHEN 6 THEN 'Разборка итого' END,  
                              
                              CASE WHEN LM.ID_TABLE not in (8,21,37,38,39) THEN C.NAME ELSE CASE WHEN LM.ID_TABLE = 21 THEN 
				isnull(C_TO21.NAME,'') 
				ELSE 
				CASE WHEN LM.ID_TABLE = 38 THEN ISNULL(C_38.NAME,'')
				else
				C_TO.NAME end END END,
           --  CASE WHEN LM.ID_TABLE not in (8,21,37,38,39) THEN C.NAME ELSE C_TO.NAME END,
             
	            CASE WHEN LM.ID_TABLE = 20 THEN
														case when LEN(ISNULL(EV.DESCRIPTION,''))=0 then 
															case when LEN(ISNULL(dr.name,''))=0 then  'Прочие списания' else dr.name end
														 else EV.DESCRIPTION end 
												ELSE  '' END,  
												
              CASE WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4) 
															ELSE 
																CASE 
																WHEN 
																	LM.ID_TABLE = 38
																	THEN
																		m.mnemocode+'/' + AD.DOC_NUM+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4)
																	ELSE
															AD.DOC_NUM+' от '+ CONVERT(VARCHAR(8),LM.DATE_OP,4) 
															end
															--+case lm.id_table when 38 then '('+m.MNEMOCODE+')'
															--				  when 39 then '('+m1.MNEMOCODE+')' else '' 
															--				  end 
															END,  
             --CASE WHEN LM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,LM.DATE_OP))),104) ELSE AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,LM.DATE_OP))),104) END,  
             LM.DATE_OP,  
             CASE LM.ID_TABLE WHEN 13 THEN 1 WHEN 6 THEN 2 ELSE 0 END,
             --ISNULL(CONVERT(VARCHAR(12),CONVERT(DATETIME, FLOOR(CONVERT(MONEY,ad.DOC_DATE))),104),''),
             isnull(case when LM.ID_TABLE IN (8,37,38,39) then substring(ad.doc_num,0,patindex('%/%',ad.doc_num)) else '' end,''),
             CASE WHEN LM.ID_TABLE = 38 THEN m.mnemocode ELSE '' end
--             ,case when lm.ID_TABLE IN (8,37,38,39) then eb.CODE else '' end  
    HAVING  round(SUM(CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) 
                           THEN 1 
                           ELSE case when LM.OP1='SUB' and LM.id_table = 6 --LM.OP 
                                     then -1 
                                     when LM.OP1='ADD' and LM.id_table = 6
                                     then 1 end 
                           END * ISNULL(LM.SUM_ACC,0)),2)<>0 or    
            sum(case when LM.id_table=13 then 0 end)>=0  or  
            round(SUM(CASE WHEN LM.ID_TABLE IN (8,20,21,24,37,38,39) 
                           THEN 1 
                           ELSE case when LM.OP1='SUB' and LM.id_table = 6 
                           then -1 
                           when  LM.OP1='ADD' and LM.id_table = 6  
                           then 1 end END * ISNULL((LM.SUM_SUP - LM.SVAT_SUP),0)),2)<>0  
 
PRINT 'INSERT INTO #MOVE(9)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
-- INSERT INTO #MOVE(10)
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY = 23,--0,
        ID_TABLE = 24,
        TABLE_NAME = 'Недостача по инвентаризации итого',
        CONTRACTOR = lm.contractor,
        SECTION_NAME='',
        DOC_NUM = lm.doc_num,--'№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,AD.DOC_DATE))),104),				  
        DOC_NUM_SUP = '',
        DATE_DOC = lm.doc_date,--AD.DOC_DATE,
        PREFIX ='',        
        SUM_ACC = ISNULL(SUM_SUM_ACC,0),--CASE WHEN LM.ID_TABLE!=12 THEN 0 ELSE ISNULL(SUM(CH_VAT.SUM_DISCOUNT),0) END,
        SUM_SUP = ISNULL(SUM_SUM_SUP,0),
        SVAT_SUP = 0,
        SUM_ACC_0 = SUM(ISNULL(CASE WHEN VAT = 0 THEN SUM_ACC ELSE 0 END,0)),
        SUM_ACC_10 = SUM(ISNULL(CASE WHEN VAT = 10 THEN SUM_ACC ELSE 0 END,0)),
        SUM_ACC_18 = SUM(ISNULL(CASE WHEN VAT = 18 THEN SUM_ACC ELSE 0 END,0)), 
        SUM_SUP_0 = SUM(ISNULL(CASE WHEN VAT = 0 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        SUM_SUP_10 = SUM(ISNULL(CASE WHEN VAT = 10 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        SUM_SUP_18 = SUM(ISNULL(CASE WHEN VAT = 18 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE 0 END,0)),
        CASH = '',
		KIND = '',
		RETURN_OR_BUY = '',
		DATE_GR =  null
    
    	from @inventory lm
	         --inner join all_document ad on ad.id_document_global = lm.id_document	
	    where sign_op=-1
	    group by 
	        lm.contractor, 
	        lm.doc_num,--'№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,AD.DOC_DATE))),104),
	        lm.doc_date,--AD.DOC_DATE, 
	        ISNULL(SUM_SUM_ACC,0),ISNULL(SUM_SUM_SUP,0)

PRINT 'INSERT INTO #MOVE(10)'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

   
---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
INSERT INTO #VO
    SELECT '',0,0,0,0,0,0,0,0,0
    WHERE  NOT EXISTS (SELECT NULL FROM #VO WITH(NOLOCK))
    

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
SELECT * FROM #VO  



---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
SELECT * 
    FROM #MOVE WITH(NOLOCK)
	Where	             
	    (@USE_DIAGN_REPORT=0)     
		 or 
		    (
				(@INV_CONTR_W_VAT = 1 OR ORDER_BY NOT IN (-1))
				and(@INV_CONTR_NW_VAT  = 1 OR ORDER_BY NOT IN (0))
				and(@RETURN_BUYER  = 1 OR ORDER_BY NOT IN (1))
				and(@INV_FROM_AP  = 1 OR ORDER_BY NOT IN (2,3,4))
				and(@EXCESS_BY_INVENT  = 1 OR ORDER_BY NOT IN (6))
				
				and(@SERVICE  = 1 OR (KIND <> '3 услуги из них'))
				and(@EXPENS_DISCOUNT  = 1 OR (KIND <> '4 скидки из них'))
				and(@SK  = 1 OR (KIND <> '5 операции со СК из них'))
				and(@CASH  = 1 OR (KIND <> '1 наличная из них'))
				and(@CASHLESS  = 1 OR (KIND <> '2 безналичная из них'))
				
				and(@RECIPES_GROSS  = 1 OR ORDER_BY NOT IN (15))
				and(@RECIPES_GROSS_DISCOUNT  = 1 OR ORDER_BY NOT IN (12,15))
				and(@COMPLAINT  = 1 OR ORDER_BY NOT IN (16))
				and(@BACK_SALE  = 1 OR ORDER_BY NOT IN (17))
				and(@MOVE_IN_CONTR = 1 OR ORDER_BY NOT IN (18,19,20))
				
				and(@WRITE_OFF = 1 OR ORDER_BY NOT IN (22))
				and(@SHORTAGE_BY_INV = 1 OR ORDER_BY NOT IN (23))
				and(@REVALUATION = 1 OR ORDER_BY NOT IN (24))
				and(@DISMANTLING = 1 OR ORDER_BY NOT IN (25))
			)
    order by 
        SECTION_NUMBER,ORDER_BY,CONTRACTOR, DATE_DOC,
        PREFIX,
        DATE_GR,cash, kind, RETURN_OR_BUY,   
        DOC_NUM --, PREFIX, DOC_NUM   
      
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE  
    SELECT  1,-22,1,NULL,'',NULL,NULL,NULL,NULL, '',0,0,0,0,0,0,0,0,0,'','','',''  
    WHERE NOT EXISTS (SELECT NULL FROM #MOVE WITH(NOLOCK))


---------------------------------------------------------------------------------------------------
--Исходящий остаток   
---------------------------------------------------------------------------------------------------
if (@USE_DIAGN_REPORT=0)
BEGIN
    SELECT
        SUM_ACC = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.SECTION_NAME='в т.ч. услуга  итого' or M.SECTION_NAME='в т.ч. операции со СК итого' or M.SECTION_NAME='скидка и прочее итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC,0))/*+ISNULL(MAX(V.SUM_ACC),0)*/,  
        SUM_SUP = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP,0))+ISNULL(MAX(V.SUM_SUP),0),  
        SVAT_SUP = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SVAT_SUP,0))+ISNULL(MAX(V.SVAT_SUP),0),  
        SUM_ACC_0 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.SECTION_NAME='в т.ч. услуга  итого' or M.SECTION_NAME='в т.ч. операции со СК итого' or M.SECTION_NAME='скидка и прочее итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_0,0))+ISNULL(MAX(V.SUM_ACC_0),0)/*+ISNULL(MAX(sk.SUM_ACC_0),0)+ISNULL(MAX(s.SUM_ACC_0),0)*/,  
        SUM_ACC_10 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.SECTION_NAME='в т.ч. услуга  итого' or M.SECTION_NAME='в т.ч. операции со СК итого' or M.SECTION_NAME='скидка и прочее итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_10,0))+ISNULL(MAX(V.SUM_ACC_10),0)/*+ISNULL(MAX(sk.SUM_ACC_10),0)+ISNULL(MAX(s.SUM_ACC_10),0)*/,  
        SUM_ACC_18 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.SECTION_NAME='в т.ч. услуга  итого' or M.SECTION_NAME='в т.ч. операции со СК итого' or M.SECTION_NAME='скидка и прочее итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_18,0))+ISNULL(MAX(V.SUM_ACC_18),0)/*+ISNULL(MAX(sk.SUM_ACC_18),0)+ISNULL(MAX(s.SUM_ACC_18),0)*/,  
        SUM_SUP_0 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_0,0))+ISNULL(MAX(V.SUM_SUP_0),0),  
        SUM_SUP_10 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_10,0))+ISNULL(MAX(V.SUM_SUP_10),0),  
        SUM_SUP_18 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_18,0))+ISNULL(MAX(V.SUM_SUP_18),0)  
        FROM 
        (	select
			    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
			    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
			    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
			    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
			    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
			    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
			    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
			    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
			    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
		        from #VO WITH(NOLOCK)
	    )V
        , #MOVE M WITH(NOLOCK)
   
END
ELSE
BEGIN       
    SELECT
        SUM_ACC = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.KIND='3 услуги из них' or M.KIND='5 операции со СК из них' or M.KIND='4 скидки из них'))) THEN -1 ELSE 0 END END * M.SUM_ACC,0))/*+ISNULL(MAX(V.SUM_ACC),0)*/,  
        SUM_SUP = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP,0))+ISNULL(MAX(V.SUM_SUP),0),  
        SVAT_SUP = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SVAT_SUP,0))+ISNULL(MAX(V.SVAT_SUP),0),  
        SUM_ACC_0 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.KIND='3 услуги из них' or M.KIND='5 операции со СК из них' or M.KIND='4 скидки из них'))) THEN -1 ELSE 0 END END * M.SUM_ACC_0,0))+ISNULL(MAX(V.SUM_ACC_0),0)/*+ISNULL(MAX(sk.SUM_ACC_0),0)+ISNULL(MAX(s.SUM_ACC_0),0)*/,  
        SUM_ACC_10 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.KIND='3 услуги из них' or M.KIND='5 операции со СК из них' or M.KIND='4 скидки из них'))) THEN -1 ELSE 0 END END * M.SUM_ACC_10,0))+ISNULL(MAX(V.SUM_ACC_10),0)/*+ISNULL(MAX(sk.SUM_ACC_10),0)+ISNULL(MAX(s.SUM_ACC_10),0)*/,  
        SUM_ACC_18 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 19 AND (M.KIND='3 услуги из них' or M.KIND='5 операции со СК из них' or M.KIND='4 скидки из них'))) THEN -1 ELSE 0 END END * M.SUM_ACC_18,0))+ISNULL(MAX(V.SUM_ACC_18),0)/*+ISNULL(MAX(sk.SUM_ACC_18),0)+ISNULL(MAX(s.SUM_ACC_18),0)*/,  
        SUM_SUP_0 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_0,0))+ISNULL(MAX(V.SUM_SUP_0),0),  
        SUM_SUP_10 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_10,0))+ISNULL(MAX(V.SUM_SUP_10),0),  
        SUM_SUP_18 = SUM(ISNULL(CASE WHEN m.SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_18,0))+ISNULL(MAX(V.SUM_SUP_18),0)  
    FROM 
    (	select
			SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
			SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
			SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
			SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
			SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
			SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
			SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
			SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
			SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
		from #VO WITH(NOLOCK)) V
        , #MOVE  M WITH(NOLOCK) 
END

---------------------------------------------------------------------------------------------------
--является ли плательщиком НДС   
---------------------------------------------------------------------------------------------------
SELECT USE_VAT = @USE_VAT  
  
SELECT FULL_NAME = LEGAL_PERS_SHORT  
    FROM CONTRACTOR WITH(NOLOCK)   
	where id_contractor = @ID_AU   


---------------------------------------------------------------------------------------------------
---продажа услуг 
---------------------------------------------------------------------------------------------------
SELECT          
	TABLE_NAME = 'Выручка итого в т.ч.',  
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),
    SUM_SUP = CONVERT(MONEY,0),  
    SVAT_SUP = CONVERT(MONEY,0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),
    SUM_SUP_0 = CONVERT(MONEY,0),  
    SUM_SUP_10 = CONVERT(MONEY,0),  
    SUM_SUP_18 = CONVERT(MONEY,0)
    from #SERVICES WITH(NOLOCK)

    --суммарные скидки по РН  
    SELECT  
        TABLE_NAME = 'Расход(опт) итого',  
        SUM_ACC = ISNULL(SUM(LM.DISCOUNT_ACC),0),  
        SUM_SUP = CONVERT(MONEY,0),  
        SVAT_SUP = CONVERT(MONEY,0),          
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 0 THEN LM.DISCOUNT_ACC ELSE 0 END),0),  
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 10 THEN LM.DISCOUNT_ACC ELSE 0 END),0),  
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN convert(bigint,LM.tax_rate) = 18 THEN LM.DISCOUNT_ACC ELSE 0 END),0),  
        SUM_SUP_0 = CONVERT(MONEY,0),  
        SUM_SUP_10 = CONVERT(MONEY,0),  
        SUM_SUP_18 = CONVERT(MONEY,0)  
--    FROM DOC_MOVEMENT DM  
    FROM 
        #LM_DISCOUNT LM WITH(NOLOCK)
        INNER JOIN ALL_DOCUMENT AD WITH(NOLOCK) ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT  
        INNER JOIN CONTRACTOR C WITH(NOLOCK) ON C.ID_CONTRACTOR = AD.ID_CONTRACTOR_FROM  
    WHERE  
        LM.DISCOUNT_ACC<>0

---------------------------------------------------------------------------------------------------
--скидка и прочее итого
---------------------------------------------------------------------------------------------------
SELECT  
    TABLE_NAME = 'Выручка итого в т.ч.',    
    --SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT),0), --сумма наличных продаж  
    --SUM_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END *(L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY),0),  
    --SVAT_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PVAT_SUP * CH_I.QUANTITY),0),  
    --SUM_ACC_0  = ISNULL(SUM(CASE WHEN convert(bigint,tt.tax_rate) = 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),  
    --SUM_ACC_10 = ISNULL(SUM(CASE WHEN convert(bigint,tt.tax_rate) = 10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),  
    --SUM_ACC_18 = ISNULL(SUM(CASE WHEN convert(bigint,tt.tax_rate) = 18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),  
    --SUM_SUP_0  = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tt.tax_rate) = 0 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0),  
    --SUM_SUP_10 = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tt.tax_rate) = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0),  
    --SUM_SUP_18 = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN convert(bigint,tt.tax_rate) = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0)  
    
    SUM_ACC = ISNULL(SUM(-LM.SIGN_OP1 * LM.DISCOUNT_ACC),0), --сумма наличных продаж  
    SUM_SUP = ISNULL(SUM(-LM.SIGN_OP1 *(L.PRICE_SUP - L.PVAT_SUP) * LM.QUANTITY),0),  
    SVAT_SUP = ISNULL(SUM(-LM.SIGN_OP1 * L.PVAT_SUP * CH_I.QUANTITY),0),  
    SUM_ACC_0  = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 0  THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
    SUM_ACC_10 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 10 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
    SUM_ACC_18 = ISNULL(SUM(CASE WHEN convert(bigint,l.tax_rate) = 18 THEN -LM.SIGN_OP1 * LM.DISCOUNT_ACC ELSE 0 END),0),  
    SUM_SUP_0  = ISNULL(SUM(-LM.SIGN_OP1 * CASE WHEN convert(bigint,l.tax_rate) =  0 THEN (L.PRICE_SUP - L.PVAT_SUP) * LM.QUANTITY ELSE 0 END),0),  
    SUM_SUP_10 = ISNULL(SUM(-LM.SIGN_OP1 * CASE WHEN convert(bigint,l.tax_rate) = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * LM.QUANTITY ELSE 0 END),0),  
    SUM_SUP_18 = ISNULL(SUM(-LM.SIGN_OP1 * CASE WHEN convert(bigint,l.tax_rate) = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * LM.QUANTITY ELSE 0 END),0)  
    
    FROM 
        --CASH_SESSION CS WITH(NOLOCK)  
        --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
        --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
        --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
        #CHEQUE_CACHE CH WITH(NOLOCK)
        INNER JOIN CHEQUE_ITEM CH_I WITH(NOLOCK) ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL  
        
        INNER JOIN MV_LOT_MOVEMENT_SIGN_OP1 LM   ON LM.ID_DOCUMENT_ITEM = CH_I.ID_CHEQUE_ITEM_GLOBAL

        --INNER JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL  
        --INNER JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
        --INNER JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
        --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
        INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL

    WHERE 
        --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to
        --AND C.ID_CONTRACTOR = @ID_AU 
        --AND CH.DOCUMENT_STATE = 'PROC'  
        --AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
        EXISTS(SELECT NULL FROM @STORES S1 WHERE S1.ID_STORE = L.ID_STORE)


---------------------------------------------------------------------------------------------------
--операции со СК итого
---------------------------------------------------------------------------------------------------
SELECT --операции со СК итого  
    TABLE_NAME = 'Выручка итого в т.ч.',  
    SUM_ACC = sum(ISNULL(dri.sum_sk,0)),  
    SUM_SUP = CONVERT(MONEY,0),  
    SVAT_SUP = CONVERT(MONEY,0),  
    SUM_ACC_0 = sum(case when convert(bigint,l.tax_rate)=0 then ISNULL(dri.sum_sk,0) else 0 end),  
    SUM_ACC_10 = sum(case when convert(bigint,l.tax_rate)=10 then ISNULL(dri.sum_sk,0) else 0 end),  
    SUM_ACC_18 = sum(case when convert(bigint,l.tax_rate)=18 then ISNULL(dri.sum_sk,0) else 0 end),  
    SUM_SUP_0 = CONVERT(MONEY,0),  
    SUM_SUP_10 = CONVERT(MONEY,0),  
    SUM_SUP_18 = CONVERT(MONEY,0)  
    FROM 
        --CASH_SESSION CS WITH(NOLOCK)  
        --INNER JOIN CASH_REGISTER CR WITH(NOLOCK) ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER  
        --INNER JOIN CONTRACTOR C WITH(NOLOCK) ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR  
        --INNER JOIN CHEQUE CH WITH(NOLOCK) ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL  
        #CHEQUE_CACHE CH WITH(NOLOCK)
        LEFT JOIN DLS_RECIPE_ITEM dri WITH(NOLOCK) on dri.ID_RECIPE_GLOBAL = CH.ID_CHEQUE_GLOBAL   
        
        --LEFT JOIN LOT L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL  
        --LEFT JOIN GOODS G WITH(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
        --LEFT JOIN TAX_TYPE TT WITH(NOLOCK) ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
        --right join #GOODS Fg WITH(NOLOCK) on fg.id_goods = G.id_goods
        INNER JOIN #LOT_GOODS_RATE L WITH(NOLOCK) ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL
    WHERE 
        --CH.DATE_CHEQUE BETWEEN @date_fr AND @date_to   
        --AND C.ID_CONTRACTOR = @ID_AU   
        --AND CH.DOCUMENT_STATE = 'PROC'  
        --AND DBO.FN_CHECQUE_TYPE_PAYMENT2(CH.ID_CHEQUE_GLOBAL) IN ('TYPE1','TYPE2','TYPE3')
        ch.cheque_type='sale'  
        AND EXISTS(SELECT NULL FROM @STORES S1 WHERE S1.ID_STORE = L.ID_STORE)
    HAVING 
        sum(ISNULL(dri.sum_sk,0))<>0  

---------------------------------------------------------------------------------------------------
--Список складов через запятую
---------------------------------------------------------------------------------------------------
select STORE = isnull(@STORE,'')


---------------------------------------------------------------------------------------------------
--Список складов через запятую
---------------------------------------------------------------------------------------------------
select
    'Услуги',
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    from #SERVICES WITH(NOLOCK)

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
select
    'Наличная итого',
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    from #CHEQUE WITH(NOLOCK)
    Where KIND = '1 наличная из них'

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
select
    'Безналичная итого',
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    from #CHEQUE WITH(NOLOCK)
    Where KIND = '2 безналичная из них'

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
select
    'скидка и прочее итого',
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    from #DISCOUNT WITH(NOLOCK)

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
select 
    'соперации со СК итого',
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    from #SK WITH(NOLOCK)

---------------------------------------------------------------------------------------------------
-- Приход и Расход
---------------------------------------------------------------------------------------------------
if OBJECT_ID('Tempdb..#MOVE1') is not null drop table #MOVE1

select 
	SECTION_NUMBER,
	NAME = CASE WHEN SECTION_NUMBER = 1 THEN 'ПРИХОД ИТОГО:' ELSE 'РАСХОД ИТОГО:' END,
	SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
	SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
	SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
	SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
	SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
	SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
	SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
	SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
	SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0),
	SHOW = 0
    INTO #MOVE1
    from #MOVE WITH(NOLOCK)
    where ORDER_BY<>-22 and KIND<>'3 услуги из них' and KIND<>'5 операции со СК из них'
	AND TABLE_NAME <> 'Итого приход от всех поставщиков'
    group by
	    CASE WHEN SECTION_NUMBER = 1 THEN 'ПРИХОД ИТОГО:' ELSE 'РАСХОД ИТОГО:' END,
	    SECTION_NUMBER
    ORDER BY 
	    SECTION_NUMBER

			
---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
INSERT INTO #MOVE1  
    SELECT  1,'ПРИХОД ИТОГО:',0,0,0,0,0,0,0,0,0,1
        WHERE NOT EXISTS (SELECT NULL FROM #MOVE1 WITH(NOLOCK) where NAME = 'ПРИХОД ИТОГО:')
    
INSERT INTO #MOVE1  
    SELECT  2,'РАСХОД ИТОГО:',0,0,0,0,0,0,0,0,0,1
        WHERE NOT EXISTS (SELECT NULL FROM #MOVE1 WITH(NOLOCK) where NAME = 'РАСХОД ИТОГО:')
    
select  * 
	from #MOVE1 WITH(NOLOCK) 
	ORDER BY 
		SECTION_NUMBER--where SECTION_NUMBER =1

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
select 
    SUM_ACC = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_ACC),0),  
    SUM_SUP = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_SUP),0),  
    SVAT_SUP = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL((CASE WHEN SECTION_NUMBER = 1 then 1 else -1 end * SUM_SUP_18),0),
    SHOW
    from #MOVE1 WITH(NOLOCK)
    where (@INV = 0 and SECTION_NUMBER in (1)) or (@EXPENS = 0 and SECTION_NUMBER  in (2))

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
if (@USE_DIAGN_REPORT=1)
BEGIN
    SELECT * from #MOVE1 WITH(NOLOCK) where SECTION_NUMBER =2
END
ELSE
BEGIN
    SELECT
		SUM_ACC = ISNULL(m.SUM_ACC,0)-sk.SUM_ACC-se.SUM_ACC,  
		SUM_SUP = ISNULL(m.SUM_SUP,0)-sk.SUM_SUP-se.SUM_SUP,  
		SVAT_SUP = ISNULL(m.SVAT_SUP,0)-sk.SVAT_SUP-se.SVAT_SUP,  
		SUM_ACC_0 = ISNULL(m.SUM_ACC_0,0)-sk.SUM_ACC_0-se.SUM_ACC_0,  
		SUM_ACC_10 = ISNULL(m.SUM_ACC_10,0)-sk.SUM_ACC_10-se.SUM_ACC_10,  
		SUM_ACC_18 = ISNULL(m.SUM_ACC_18,0)-sk.SUM_ACC_18-se.SUM_ACC_18,  
		SUM_SUP_0 = ISNULL(m.SUM_SUP_0,0)-sk.SUM_SUP_0-se.SUM_SUP_0,  
		SUM_SUP_10 = ISNULL(m.SUM_SUP_10,0)-sk.SUM_SUP_10-se.SUM_SUP_10,  
		SUM_SUP_18 = ISNULL(m.SUM_SUP_18,0)-sk.SUM_SUP_18-se.SUM_SUP_18
		from
		    #MOVE1 m WITH(NOLOCK),
		    (
		        select 
			        SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
			        SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
			        SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
			        SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
			        SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
			        SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
			        SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
			        SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
			        SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
		            from #SK WITH(NOLOCK) 
		    )sk,
		    (
		        select
			        SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
			        SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
			        SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
			        SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
			        SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
			        SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
			        SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
			        SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
			        SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
		            from #SERVICES WITH(NOLOCK)
		    )se
		    WHERE
		        SECTION_NUMBER = 2
END


SELECT 		
    SUM_ACC = ISNULL(SUM(SUM_ACC),0),  
    SUM_SUP = ISNULL(SUM(SUM_SUP),0),  
    SVAT_SUP = ISNULL(SUM(SVAT_SUP),0),  
    SUM_ACC_0 = ISNULL(SUM(SUM_ACC_0),0),  
    SUM_ACC_10 = ISNULL(SUM(SUM_ACC_10),0),  
    SUM_ACC_18 = ISNULL(SUM(SUM_ACC_18),0),  
    SUM_SUP_0 = ISNULL(SUM(SUM_SUP_0),0),  
    SUM_SUP_10 = ISNULL(SUM(SUM_SUP_10),0),  
    SUM_SUP_18 = ISNULL(SUM(SUM_SUP_18),0)
    FROM #VO WITH(NOLOCK)
 
PRINT 'СЕЛЕКТЫ...'
PRINT 'DURATION:'+RIGHT(CONVERT(nvarchar(50), GETDATE()-@dStart, 121),12); SET @dStart = GETDATE()

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
RETURN
GO
---------------------------------------------------------------------------------------------------
-- CLEAR CACHE
---------------------------------------------------------------------------------------------------
--DBCC FREEPROCCACHE WITH NO_INFOMSGS
--DBCC DROPCLEANBUFFERS WITH NO_INFOMSGS
--DBCC FREESESSIONCACHE WITH NO_INFOMSGS
--GO
--------------------------------------
--exec REP_EX_TO_MR_RIGLA @xmlParam=N'
--<XML>
--    <DATE_FR>2013-05-01T00:00:00.000</DATE_FR>
--    <DATE_TO>2013-05-31T00:00:00.000</DATE_TO>
    
--    <IS_FILTERED>1</IS_FILTERED><ID_AU>6287</ID_AU><USE_DIAGN_REPORT>0</USE_DIAGN_REPORT><INV_REMAINDER>0</INV_REMAINDER><INV>0</INV><INV_CONTR_W_VAT>0</INV_CONTR_W_VAT><INV_CONTR_NW_VAT>0</INV_CONTR_NW_VAT><RETURN_BUYER>0</RETURN_BUYER><EXCESS_BY_INVENT>0</EXCESS_BY_INVENT><EXPENS>0</EXPENS><RECEIPTS>0</RECEIPTS><SERVICE>0</SERVICE><EXPENS_DISCOUNT>0</EXPENS_DISCOUNT><SK>0</SK><CASH>0</CASH><CASHLESS>0</CASHLESS><RECIPES_GROSS>0</RECIPES_GROSS><RECIPES_GROSS_DISCOUNT>0</RECIPES_GROSS_DISCOUNT><RETURN_TO_CONTRAC>0</RETURN_TO_CONTRAC><COMPLAINT>0</COMPLAINT><BACK_SALE>0</BACK_SALE><MOVE_IN_CONTR>0</MOVE_IN_CONTR><WRITE_OFF>0</WRITE_OFF><SHORTAGE_BY_INV>0</SHORTAGE_BY_INV><REVALUATION>0</REVALUATION><DISMANTLING>0</DISMANTLING>
--</XML>'
--GO
---------------------------------------------------------------------------------------------------
