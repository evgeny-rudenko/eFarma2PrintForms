SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_MONTH_REPORT') IS NULL EXEC ('CREATE PROCEDURE DBO.REPEX_MONTH_REPORT AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_MONTH_REPORT
    @XMLPARAM NTEXT AS

DECLARE @DATE DATETIME
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ID_CONTRACTOR BIGINT
DECLARE @HDOC INT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT
	@DATE = [DATE],
    @ID_CONTRACTOR = ID_CONTRACTOR
FROM OPENXML(@HDOC, '/XML') WITH(
    [DATE] DATETIME 'DATE',
    ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
)

SET @DATE_FR = DATEADD(DAY,-DATEPART(DAY,@DATE)+1,@DATE)
SET @DATE_TO = DATEADD(DAY,-1,DATEADD(MONTH,1,DATEADD(DAY,-DATEPART(DAY,@DATE)+1,@DATE)))

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

--select @DATE_FR, @DATE_TO, @ID_CONTRACTOR
declare @use_vat bit

select @use_vat = use_vat from CONTRACTOR where ID_CONTRACTOR = @ID_CONTRACTOR
--select @use_vat
declare @count_ch int
declare @summ money
declare @avg_cost money

SELECT
    @count_ch = SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),
    @SUMM = ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * SUMM),0),
    @AVG_COST = CASE WHEN ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),0)!=0 THEN ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * SUMM),0)/ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),0) ELSE 0 END
FROM CASH_REGISTER CR
	LEFT JOIN CASH_SESSION CS ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CHEQUE C ON C.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
WHERE DOCUMENT_STATE = 'PROC'
	AND CHEQUE_TYPE IN ('SALE','RETURN')
	AND CS.DATE_CLOSE BETWEEN @DATE_FR AND @DATE_TO
	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR

declare @cost_credit money

SELECT
	@COST_CREDIT = SUM(CASE WHEN @USE_VAT = 1 THEN LM.SUM_SUP - LM.SVAT_SUP ELSE LM.SUM_SUP END)
FROM LOT_MOVEMENT LM
	inner join LOT l on l.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
	inner join STORE st on st.ID_STORE = l.ID_STORE
	inner join CONTRACTOR ct on ct.ID_CONTRACTOR = st.ID_CONTRACTOR
WHERE lm.CODE_OP IN ('INVOICE_OUT', 'CHEQUE') 
	AND lm.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND lm.QUANTITY_SUB > 0
	AND ct.ID_CONTRACTOR = @ID_CONTRACTOR
	
--select @count_ch, @summ, @avg_cost, @cost_credit

--Из ТО Ригла

DECLARE @STORES TABLE(ID_STORE BIGINT)    
    INSERT INTO @STORES
    SELECT ID_STORE 
    FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.') TAB

    IF(@@ROWCOUNT=0)
    BEGIN
        INSERT INTO @STORES
        SELECT
            ID_STORE
        FROM STORE S
        WHERE S.ID_CONTRACTOR = @ID_CONTRACTOR
    END
    
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT 
    SUM_ACC = SUM(SUM_ACC),
    SUM_SUP = SUM(SUM_SUP),
    SVAT_SUP = SUM(CASE WHEN @USE_VAT=1 THEN 0 ELSE SVAT_SUP END),
    SUM_ACC_0 = SUM(SUM_ACC_0),
    SUM_ACC_10 = SUM(SUM_ACC_10),
    SUM_ACC_18 = SUM(SUM_ACC_18),
    SUM_SUP_0 = SUM(SUM_SUP_0),
    SUM_SUP_10 = SUM(SUM_SUP_10),
    SUM_SUP_18 = SUM(SUM_SUP_18)
INTO #VO
FROM (
 SELECT 
    SUM_ACC = ISNULL(SUM(CASE WHEN (DM.ID_TABLE = 21 AND CODE_OP='DIS') THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),0),
    SUM_SUP = ISNULL(SUM((DM.SUM_SUP - DM.SVAT_SUP)*DM.SIGN_OP),0),
    SVAT_SUP = ISNULL(SUM(DM.SVAT_SUP * DM.SIGN_OP),0),
    SUM_ACC_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN CASE WHEN DM.ID_TABLE = 21 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP ELSE 0 END),0),
    SUM_ACC_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN CASE WHEN DM.ID_TABLE = 21 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP ELSE 0 END),0),
    SUM_ACC_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN CASE WHEN DM.ID_TABLE = 21 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP ELSE 0 END),0),
    SUM_SUP_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN (DM.SUM_SUP - DM.SVAT_SUP)* DM.SIGN_OP ELSE 0 END),0),
    SUM_SUP_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN (DM.SUM_SUP - DM.SVAT_SUP)* DM.SIGN_OP ELSE 0 END),0),
    SUM_SUP_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN (DM.SUM_SUP - DM.SVAT_SUP)* DM.SIGN_OP ELSE 0 END),0)
FROM DOC_MOVEMENT DM
INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM AND DM.ID_TABLE=2
WHERE EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = DM.ID_STORE AND (ID_STORE IS NOT NULL))
      AND (DM.ID_TABLE = 30
           OR (DM.ID_TABLE = 2 AND exists(select null from contractor where name='СТУ' AND id_contractor = dm.id_contractor_from))
           OR (DM.ID_TABLE = 2 AND exists(select null from contractor where name<>'СТУ'AND id_contractor = dm.id_contractor_from)and dm.date_op<@date_fr)
           OR (DM.ID_TABLE IN (3,6,12,13,19,20,21,24) AND dm.date_op < @date_fr)
           OR (DM.ID_TABLE IN (8,37,38,39) AND dm.date_op < @date_fr 
          ) )
      AND DM.CODE_OP<>'DIS'

UNION ALL
 SELECT 
    SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.SUMM),0),
    SUM_SUP = 0,
    SVAT_SUP = 0,
    SUM_ACC_0 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TT.TAX_RATE)=0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_ACC_10 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TT.TAX_RATE)=10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_ACC_18 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TT.TAX_RATE)=18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN -1 ELSE 1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_SUP_0 = 0,
    SUM_SUP_10 = 0,
    SUM_SUP_18 = 0
 FROM CHEQUE_ITEM CH_I
 INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
 INNER JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL 
 INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
 INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
 INNER JOIN SERVICE_4_SALE_ITEM S4S ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL
 INNER JOIN SERVICE S ON S.ID_SERVICE = S4S.ID_SERVICE
 INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE            
 WHERE CS.DATE_CLOSE< @DATE_FR AND C.ID_CONTRACTOR = @ID_CONTRACTOR AND CH_I.ID_GOODS = 0 AND CH.DOCUMENT_STATE = 'PROC') VO 

--ПРИХОД

    SELECT
        SECTION_NUMBER,
        ORDER_BY,
        ID_TABLE,
        TABLE_NAME,
        CONTRACTOR,
        SECTION_NAME,
        DOC_NUM,
        DATE_DOC,
        SUM_ACC,
        SUM_SUP,
        SVAT_SUP,
        SUM_ACC_0,
        SUM_ACC_10,
        SUM_ACC_18,
        SUM_SUP_0,
        SUM_SUP_10,
        SUM_SUP_18
    INTO #MOVE
    FROM(
    SELECT
        SECTION_NUMBER = 1,
        ORDER_BY=0,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = CASE DM.ID_TABLE WHEN 2 THEN 'Приход от поставщика (организаций) итого'
                                   WHEN 8 THEN 'Приход от аптек итого'
                                   WHEN 37 THEN 'Приход от аптек итого'
                                   WHEN 38 THEN 'Приход от аптек итого'
                                   WHEN 39 THEN 'Приход от аптек итого'
                                   WHEN 12 THEN 'Возврат от покупателя итого'
                                   WHEN 24 THEN 'Излишки по инвентаризации итого' END,
        CONTRACTOR = C.NAME,
        SECTION_NAME='',
        DOC_NUM = CASE 
				WHEN DM.ID_TABLE = 2 THEN I.INCOMING_NUMBER + ' от ' + CONVERT(VARCHAR, I.INCOMING_DATE, 104) + ' № '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104)
				WHEN DM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104)
				ELSE AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) END,
        DATE_DOC = DM.DATE_OP,
        SUM_ACC = ISNULL(SUM(DM.SUM_ACC),0),
        SUM_SUP = ISNULL(SUM((DM.SUM_SUP - DM.SVAT_SUP)),0),
        SVAT_SUP = SUM(CASE WHEN (DM.ID_TABLE=2 AND @USE_VAT=1)OR(@USE_VAT=0) THEN ISNULL(DM.SVAT_SUP,0) ELSE 0 END),
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN SUM_ACC ELSE 0 END),0),
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN SUM_ACC ELSE 0 END),0),
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN SUM_ACC ELSE 0 END),0),
        SUM_SUP_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END),0),
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END),0),
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END),0)
    FROM DOC_MOVEMENT DM
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
	LEFT JOIN INVOICE I ON I.ID_INVOICE_GLOBAL = AD.ID_DOCUMENT_GLOBAL
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
    WHERE DM.DATE_OP >= @DATE_FR AND DM.DATE_OP <= @DATE_TO 
          AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = DM.ID_STORE)
          AND (DM.ID_TABLE IN (12,24) 
                OR (DM.ID_TABLE = 2 AND NOT EXISTS (SELECT ID_CONTRACTOR FROM CONTRACTOR WHERE NAME='СТУ' AND ID_CONTRACTOR=DM.ID_CONTRACTOR))
                OR DM.ID_TABLE IN (8,37,38,39) /*AND (@IS_FILTERED=0 OR (@IS_FILTERED=1 AND 
                                                            EXISTS(SELECT NULL FROM @ID_DOC_MOVEMENT IDM WHERE IDM.ID_DOCUMENT = DM.ID_DOCUMENT)))*/)
          AND DM.CODE_OP = 'ADD'
     GROUP BY DM.ID_TABLE,DM.ID_CONTRACTOR_FROM,DM.ID_DOCUMENT,
              CASE DM.ID_TABLE WHEN 2 THEN 'Приход от поставщика (организаций) итого'
                                   WHEN 8 THEN 'Приход от аптек итого'
                                   WHEN 37 THEN 'Приход от аптек итого1'
                                   WHEN 38 THEN 'Приход от аптек итого2'
                                   WHEN 39 THEN 'Приход от аптек итого3'
                                   WHEN 12 THEN 'Возврат от покупателя итого'
                                   WHEN 24 THEN 'Излишки по инвентаризации итого' END,
             C.NAME,
			CASE 
				WHEN DM.ID_TABLE = 2 THEN I.INCOMING_NUMBER + ' от ' + CONVERT(VARCHAR, I.INCOMING_DATE, 104) + ' № '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104)
				WHEN DM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104)
				ELSE AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) END,
             DM.DATE_OP

    UNION ALL

--услуги
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY=0,
        ID_TABLE = 19,
        TABLE_NAME = 'Выручка итого в т.ч.',
        CONTRACTOR = '',
        SECTION_NAME = 'услуга итого',
        DOC_NUM = '',--'№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104),
        DATE_DOC = getdate(),--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104),
      	SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM),0),	--сумма наличных продаж
        SUM_SUP = 0,
        SVAT_SUP = 0,
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
        SUM_SUP_0 = 0,
        SUM_SUP_10 = 0,
        SUM_SUP_18 = 0
    FROM CASH_SESSION CS
    INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
    INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
    INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
    INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
    INNER JOIN SERVICE_4_SALE_ITEM S4S ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL
    inner JOIN SERVICE S ON S.ID_SERVICE = S4S.ID_SERVICE
    inner JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE
    WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) AND C.ID_CONTRACTOR = @ID_CONTRACTOR AND CH.DOCUMENT_STATE = 'PROC'
    GROUP BY CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)
    having ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM),0)>0
--     GROUP BY CS.ID_CASH_SESSION,CP.TYPE_PAYMENT,CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)+' №'+CS.MNEMOCODE, 
--              CS.DATE_CLOSE 

    UNION ALL
--скидки
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY = 0,
        ID_TABLE = 19,
        TABLE_NAME = 'Выручка итого в т.ч.',
        CONTRACTOR = '',
        SECTION_NAME = 'скидка итого',
        DOC_NUM = '',--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)+ ' №'+CS.MNEMOCODE,
        DATE_DOC = null,--CS.DATE_CLOSE,
--         SUM_ACC = ISNULL(SUM(case ch.cheque_type when 'sale' THEN (ch.sum_discount - isnull(di_s.sum_sk,0))
--                                       when 'return' then -1*(ch.sum_discount - isnull(di_r.sum_sk,0)) end),0),
        SUM_ACC = ISNULL(SUM(case ch.cheque_type when 'sale' THEN (ch_i.summ_discount - isnull(di_s.sum_sk,0))
                                      when 'return' then -1*ch_i.summ_discount end),0),
        SUM_SUP = 0,
        SVAT_SUP = 0,
        SUM_ACC_0 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 0 THEN case ch.cheque_type when 'sale' THEN (ch_i.summ_discount - isnull(di_s.sum_sk,0))
                                      when 'return' then -1*ch_i.summ_discount end ELSE 0 END),0),
        SUM_ACC_10 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 10 THEN case ch.cheque_type when 'sale' THEN (ch_i.summ_discount - isnull(di_s.sum_sk,0))
                                      when 'return' then -1*ch_i.summ_discount end ELSE 0 END),0),
        SUM_ACC_18 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 18 THEN case ch.cheque_type when 'sale' THEN (ch_i.summ_discount - isnull(di_s.sum_sk,0))
                                      when 'return' then -1*ch_i.summ_discount end ELSE 0 END),0),
        SUM_SUP_0 = 0,
        SUM_SUP_10 = 0,
        SUM_SUP_18 = 0
    FROM CASH_SESSION CS
    INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
    INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
    INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
    INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
    LEFT JOIN DLS_RECIPE_ITEM DI_S ON DI_S.ID_RECIPE_ITEM_GLOBAL = CH_I.ID_CHEQUE_ITEM_GLOBAL 
--    LEFT JOIN DLS_RECIPE DI_R ON DI_R.ID_RECIPE_GLOBAL = CH.ID_DOCUMENT_BASE_GLOBAL 
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL
    WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) 
           AND C.ID_CONTRACTOR = @ID_CONTRACTOR 
--     GROUP BY CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)+ ' №'+CS.MNEMOCODE,
--              CS.DATE_CLOSE

    UNION ALL
--операции со СК
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY=0,
        ID_TABLE = 19,
        TABLE_NAME = 'Выручка итого в т.ч.',
        CONTRACTOR = '',
        SECTION_NAME = 'операции со СК итого',
        DOC_NUM = CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104),--+ ' №'+CS.MNEMOCODE,
        DATE_DOC = getdate(),--CONVERT(DATETIME,CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)),--CS.DATE_CLOSE,
        SUM_ACC = sum(ISNULL(dri.sum_sk,0)),
--         SUM_ACC = ISNULL(SUM(case ch.cheque_type when 'sale' THEN isnull(di_s.sum_sk,0)
--                                       when 'return' then -1*isnull(di_r.sum_sk,0) end),0),
        SUM_SUP = 0,
        SVAT_SUP = 0,
        SUM_ACC_0 = sum(case when l.vat_sal=0 then ISNULL(dri.sum_sk,0) else 0 end),
        SUM_ACC_10 = sum(case when l.vat_sal=10 then ISNULL(dri.sum_sk,0) else 0 end),
        SUM_ACC_18 = sum(case when l.vat_sal=18 then ISNULL(dri.sum_sk,0) else 0 end),
--         SUM_ACC_0 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 0 THEN case ch.cheque_type when 'sale' THEN isnull(di_s.sum_sk,0)
--                                       when 'return' then -1 * isnull(di_r.sum_sk,0) end ELSE 0 END),0),
--         SUM_ACC_10 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 10 THEN case ch.cheque_type when 'sale' THEN isnull(di_s.sum_sk,0)
--                                       when 'return' then -1 * isnull(di_r.sum_sk,0) end ELSE 0 END),0),
--         SUM_ACC_18 =  ISNULL(SUM(CASE WHEN L.VAT_SAL = 18 THEN case ch.cheque_type when 'sale' THEN isnull(di_s.sum_sk,0)
--                                       when 'return' then -1 * isnull(di_r.sum_sk,0) end ELSE 0 END),0),
        SUM_SUP_0 = 0,
        SUM_SUP_10 = 0,
        SUM_SUP_18 = 0
    FROM CASH_SESSION CS
    INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
    INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
    INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
    LEFT JOIN DLS_RECIPE_ITEM dri on dri.ID_RECIPE_GLOBAL = CH.ID_CHEQUE_GLOBAL 
--    INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
--     LEFT JOIN DLS_RECIPE DI_S ON DI_S.ID_RECIPE_GLOBAL = CH.ID_CHEQUE_GLOBAL 
--     LEFT JOIN DLS_RECIPE DI_R ON DI_R.ID_RECIPE_GLOBAL = CH.ID_DOCUMENT_BASE_GLOBAL 
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = dri.ID_LOT_GLOBAL
    WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) 
           AND C.ID_CONTRACTOR = @ID_CONTRACTOR 
           AND ch.cheque_type='sale'
    GROUP BY CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)--+ ' №'+CS.MNEMOCODE,
    having sum(ISNULL(dri.sum_sk,0))>0

    UNION ALL

-- чеки
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY = 0,
        ID_TABLE = 19,
        TABLE_NAME = 'Выручка итого в т.ч.',
        CONTRACTOR = '',
        SECTION_NAME = CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN 'наличная итого'
                                            WHEN 'TYPE2' THEN 'безналичная итого' END,
        DOC_NUM = CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104),--+ ' №'+CS.MNEMOCODE,
        DATE_DOC = null,--CS.DATE_CLOSE,
      	SUM_ACC = ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                   CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                        WHEN 'TYPE2' THEN
                                   CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END),0)-
                   ISNULL(SUM(CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                   CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                        WHEN 'TYPE2' THEN
                                   CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END),0),-- SUM(CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END)-SUM(CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END),	--сумма наличных продаж
        SUM_SUP = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY),0),
        SVAT_SUP = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PVAT_SUP * CP.QUANTITY * CASE WHEN @USE_VAT=1 THEN 0 ELSE 1 END),0),
        SUM_ACC_0 = ISNULL(SUM(--CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * 
                                        CASE WHEN CP.ID_GOODS = 0 THEN --услуга
                                            (CASE WHEN convert(bigint,tt.tax_rate) = 0 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END) ELSE 0 END)
                                             ELSE CASE WHEN CP.ID_GOODS <> 0 and VAT_SAL = 0 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END)
 --                                            (CP.SUM_NAL - CP.SUM_RET_NAL) 
                                             ELSE 0 END          --товар
                                             END),0),
--        SUM_ACC_10 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SAL = 10 THEN L.PRICE_SAL * CP.QUANTITY ELSE 0 END),0),
        SUM_ACC_10 = ISNULL(SUM(--CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * 
                                        CASE WHEN CP.ID_GOODS = 0 THEN --услуга
                                            (CASE WHEN convert(bigint,tt.tax_rate) = 10 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END) ELSE 0 END)
                                             ELSE CASE WHEN CP.ID_GOODS <> 0 and VAT_SAL = 10 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END)
--                                                 (CP.SUM_NAL - CP.SUM_RET_NAL) 
                                             ELSE 0 END          --товар
                                             END),0),

--        SUM_ACC_18 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SAL = 18 THEN L.PRICE_SAL * CP.QUANTITY ELSE 0 END),0),
        SUM_ACC_18 = ISNULL(SUM(--CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * 
                                        CASE WHEN CP.ID_GOODS = 0 THEN --услуга
                                            (CASE WHEN convert(bigint,tt.tax_rate) = 18 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END) ELSE 0 END)
                                             ELSE CASE WHEN CP.ID_GOODS <> 0 and VAT_SAL = 18 THEN 
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN
                                                     CASE WHEN CP.SUM_NAL IS NULL THEN 0 ELSE CP.SUM_NAL END 
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_CREDIT IS NULL THEN 0 ELSE CP.SUM_CREDIT END END)-
                                                (CASE CP.TYPE_PAYMENT WHEN 'TYPE1' THEN    
                                                     CASE WHEN CP.SUM_RET_NAL IS NULL THEN 0 ELSE CP.SUM_RET_NAL END
                                                                      WHEN 'TYPE2' THEN
                                                     CASE WHEN CP.SUM_RET_CREDIT IS NULL THEN 0 ELSE CP.SUM_RET_CREDIT END END)
--                                             (CP.SUM_NAL - CP.SUM_RET_NAL) 
                                                  ELSE 0 END          --товар
                                             END),0),
        SUM_SUP_0 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 0 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0),
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN CP.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * CP.QUANTITY ELSE 0 END),0)
    FROM CASH_SESSION CS
    INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
    INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
    INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
    INNER JOIN (SELECT
                    CP.ID_CHEQUE_GLOBAL,
                    TYPE_PAYMENT = CP.TYPE_PAYMENT,
                    CHEQUE_TYPE = CH.CHEQUE_TYPE,
                    QUANTITY = CH_I.QUANTITY,
                    ID_GOODS = CH_I.ID_GOODS,
                    ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL,
                    SUM_NAL = (CASE WHEN CP.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE='SALE' THEN CH_I.SUMM ELSE 0 END),  --продажа по налу
                    SUM_RET_NAL = (CASE WHEN CP.TYPE_PAYMENT = 'TYPE1' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END), --возврат по налу
                    SUM_CREDIT = (CASE WHEN CP.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE='SALE' THEN CH_I.SUMM ELSE 0 END), --продажа по карте
                    SUM_RET_CREDIT = (CASE WHEN CP.TYPE_PAYMENT = 'TYPE2' AND CH.CHEQUE_TYPE = 'RETURN' THEN CH_I.SUMM ELSE 0 END) --возврат по карте
               FROM CHEQUE_PAYMENT CP
               INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = CP.ID_CHEQUE_GLOBAL
               INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
               WHERE CP.TYPE_PAYMENT <> 'TYPE3'
               and id_cheque_payment = (select max(id_cheque_payment) from CHEQUE_PAYMENT c where c.id_cheque_global = ch.id_cheque_global)
--               GROUP BY CP.ID_CHEQUE_GLOBAL,CP.TYPE_PAYMENT,CH.CHEQUE_TYPE
                ) CP ON CP.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
    LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CP.ID_LOT_GLOBAL
    LEFT JOIN SERVICE_4_SALE_ITEM S4S ON S4S.ID_SERVICE_4_SALE = CP.ID_LOT_GLOBAL
    LEFT JOIN SERVICE S ON S.ID_SERVICE = S4S.ID_SERVICE
    LEFT JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE
    WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) AND C.ID_CONTRACTOR = @ID_CONTRACTOR AND CH.DOCUMENT_STATE = 'PROC'
    GROUP BY --CS.ID_CASH_SESSION,
             CP.TYPE_PAYMENT,CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CS.DATE_CLOSE))),104)--+' №'+CS.MNEMOCODE, 
             --CS.DATE_CLOSE --CS.MNEMOCODE,CS.DATE_OPEN,CS.ID_CASH_SESSION,

    UNION ALL 

--ОСТАЛЬНОЙ РАСХОД
    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY=0,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = MAX('Возврат поставщику:итого'),
        CONTRACTOR = C.NAME,
        SECTION_NAME = CASE DM.ID_TABLE WHEN 3 THEN '-по претензии итого'
                                     WHEN 21 THEN '-обратная реализация' END,
        DOC_NUM = '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104),
        DATE_DOC = DM.DATE_OP,
        SUM_ACC = SUM(DM.SUM_ACC),
        SUM_SUP = SUM((DM.SUM_SUP - DM.SVAT_SUP)),
        SVAT_SUP = SUM(CASE WHEN @USE_VAT = 1 THEN 0 ELSE 1 END * DM.SVAT_SUP),
        SUM_ACC_0 = SUM(CASE WHEN VAT_RATE = 0 THEN SUM_ACC ELSE 0 END),
        SUM_ACC_10 = SUM(CASE WHEN VAT_RATE = 10 THEN SUM_ACC ELSE 0 END),
        SUM_ACC_18 = SUM(CASE WHEN VAT_RATE = 18 THEN SUM_ACC ELSE 0 END),
        SUM_SUP_0 = SUM(CASE WHEN VAT_RATE = 0 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END),
        SUM_SUP_10 = SUM(CASE WHEN VAT_RATE = 10 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END),
        SUM_SUP_18 = SUM(CASE WHEN VAT_RATE = 18 THEN DM.SUM_SUP - DM.SVAT_SUP ELSE 0 END)
    FROM DOC_MOVEMENT DM
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO 
    WHERE DM.DATE_OP >= @DATE_FR AND DM.DATE_OP <= @DATE_TO 
          AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = DM.ID_STORE)
          AND (DM.ID_TABLE = 3 OR (DM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WHERE IS_SUPPLIER=1 AND ID_INVOICE_OUT_GLOBAL = DM.ID_DOCUMENT)))
          AND DM.CODE_OP = 'SUB'
    GROUP BY DM.ID_TABLE,DM.ID_CONTRACTOR,DM.ID_DOCUMENT,
             CASE DM.ID_TABLE WHEN 3 THEN 'по претензии итого'
                           WHEN 21 THEN 'обратная реализация' END,
             C.NAME,
            '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104),
            DM.DATE_OP

    UNION ALL

    SELECT
        SECTION_NUMBER = 2,
        ORDER_BY = CASE DM.ID_TABLE WHEN 13 THEN 1
                                    WHEN 6 THEN 2 ELSE 0 END,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = CASE DM.ID_TABLE WHEN 8 THEN 'Перемещение в аптеки итого'                                      
                                      WHEN 37 THEN 'Перемещение в аптеки итого'
                                      WHEN 38 THEN 'Перемещение в аптеки итого'
                                      WHEN 39 THEN 'Перемещение в аптеки итого'
                                      WHEN 20 THEN 'Списание итого'
                                      WHEN 21 THEN 'Расход(опт) итого'
                                      WHEN 24 THEN 'Недостача по инвентаризации итого' 
                                      WHEN 13 THEN 'Переоценка'
                                      WHEN 6 THEN 'Разборка итого' END,
        CONTRACTOR = CASE WHEN DM.ID_TABLE <>8 THEN C.NAME ELSE C_TO.NAME END,
        SECTION_NAME = CASE WHEN DM.ID_TABLE = 21 AND DM.CODE_OP = 'DIS' THEN 'в т.ч.скидка итого' ELSE '' END,
        DOC_NUM = CASE WHEN DM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) ELSE AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) END,
        DATE_DOC = DM.DATE_OP,
        SUM_ACC = ROUND(SUM(CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL(DM.SUM_ACC,0)),2),
        SUM_SUP = ROUND(SUM(CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL((DM.SUM_SUP - DM.SVAT_SUP),0)),2),
        SVAT_SUP = SUM(CASE WHEN @USE_VAT=1 THEN 0 ELSE 1 END * CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL(DM.SVAT_SUP,0)),
        SUM_ACC_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL(DM.SUM_ACC,0) ELSE 0 END),0),
        SUM_ACC_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL(DM.SUM_ACC,0) ELSE 0 END),0),
        SUM_ACC_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL(DM.SUM_ACC,0) ELSE 0 END),0),
        SUM_SUP_0 = ISNULL(SUM(CASE WHEN VAT_RATE = 0 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL((DM.SUM_SUP - DM.SVAT_SUP),0) ELSE 0 END),0),
        SUM_SUP_10 = ISNULL(SUM(CASE WHEN VAT_RATE = 10 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL((DM.SUM_SUP - DM.SVAT_SUP),0) ELSE 0 END),0),
        SUM_SUP_18 = ISNULL(SUM(CASE WHEN VAT_RATE = 18 THEN CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE -1*DM.SIGN_OP END * ISNULL((DM.SUM_SUP - DM.SVAT_SUP),0) ELSE 0 END),0)
    FROM DOC_MOVEMENT DM
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM AND DM.ID_TABLE <> 8
    LEFT JOIN CONTRACTOR C_TO ON C_TO.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO AND DM.ID_TABLE = 8
    WHERE (DM.DATE_OP >= @DATE_FR AND DM.DATE_OP <= @DATE_TO)
          AND EXISTS (SELECT ID_STORE FROM @STORES WHERE ID_STORE = DM.ID_STORE)
          AND (
                ((DM.ID_TABLE IN (20,24) OR 
                 (DM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WHERE IS_SUPPLIER=0 AND ID_INVOICE_OUT_GLOBAL = DM.ID_DOCUMENT))
                  OR (DM.ID_TABLE in (8,37,38,39)/*AND (@IS_FILTERED=0 OR (@IS_FILTERED=1 AND 
                                                            EXISTS(SELECT NULL FROM @ID_DOC_MOVEMENT IDM WHERE IDM.ID_DOCUMENT = DM.ID_DOCUMENT))))*/))
                 AND (DM.CODE_OP = 'SUB' or DM.CODE_OP = 'DIS')
                )OR
                (DM.ID_TABLE IN (6,13) AND DM.CODE_OP IN ('ADD','SUB')))

--           AND (DM.ID_TABLE IN (8,13,20,24) OR (DM.ID_TABLE = 21 AND EXISTS (SELECT NULL FROM INVOICE_OUT WHERE IS_SUPPLIER=0 AND ID_INVOICE_OUT_GLOBAL = DM.ID_DOCUMENT)))
--           AND (DM.CODE_OP = 'SUB' or DM.CODE_OP = 'DIS')
    GROUP BY DM.ID_TABLE,DM.ID_CONTRACTOR,DM.ID_DOCUMENT,
             CASE DM.ID_TABLE WHEN 8 THEN 'Перемещение в аптеки итого'                                      
                  WHEN 37 THEN 'Перемещение в аптеки итого1'
                  WHEN 38 THEN 'Перемещение в аптеки итого2'
                  WHEN 39 THEN 'Перемещение в аптеки итого3'
                  WHEN 20 THEN 'Списание итого'
                  WHEN 21 THEN 'Расход(опт) итого'
                  WHEN 24 THEN 'Недостача по инвентаризации итого' 
                  WHEN 13 THEN 'Переоценка'
                  WHEN 6 THEN 'Разборка итого' END,
             CASE WHEN DM.ID_TABLE <>8 THEN C.NAME ELSE C_TO.NAME END,
             CASE WHEN DM.ID_TABLE = 21 AND DM.CODE_OP = 'DIS' THEN 'в т.ч.скидка итого' ELSE '' END,
             CASE WHEN DM.ID_TABLE NOT IN (8,37,38,39) THEN '№ '+SUBSTRING(AD.DOC_NUM,LEN(AD.DOC_NUM)-7,8)+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) ELSE AD.DOC_NUM+' от '+ CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,DM.DATE_OP))),104) END,
             DM.DATE_OP,
             CASE DM.ID_TABLE WHEN 13 THEN 1 WHEN 6 THEN 2 ELSE 0 END
    HAVING round(SUM(CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE DM.SIGN_OP END * ISNULL(DM.SUM_ACC,0)),2)<>0 or
            round(SUM(CASE WHEN DM.ID_TABLE IN (8,20,21,24,37,38,39) THEN 1 ELSE DM.SIGN_OP END * ISNULL((DM.SUM_SUP - DM.SVAT_SUP),0)),2)<>0

    )MO

    --SELECT * FROM #VO
    --SELECT * FROM #MOVE ORDER BY order_by

    INSERT INTO #MOVE 
    SELECT
        1,0,1,NULL,'',NULL,NULL,NULL,0,0,0,0,0,0,0,0,0
    WHERE NOT EXISTS (SELECT NULL FROM #MOVE)
              
    SELECT
        SUM_ACC = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 21 and M.SECTION_NAME='в т.ч.скидка итого')or(M.ID_TABLE = 19 AND (M.SECTION_NAME='услуга итого' or M.SECTION_NAME='скидка итого' or M.SECTION_NAME='операции со СК итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC)+MAX(V.SUM_ACC),
        SUM_SUP = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP)+MAX(V.SUM_SUP),
        SVAT_SUP = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SVAT_SUP)+MAX(V.SVAT_SUP),
        SUM_ACC_0 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 21 and M.SECTION_NAME='в т.ч.скидка итого')or(M.ID_TABLE = 19 AND (M.SECTION_NAME='услуга итого' or M.SECTION_NAME='скидка итого' or M.SECTION_NAME='операции со СК итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_0)+MAX(V.SUM_ACC_0),
        SUM_ACC_10 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 21 and M.SECTION_NAME='в т.ч.скидка итого')or(M.ID_TABLE = 19 AND (M.SECTION_NAME='услуга итого' or M.SECTION_NAME='скидка итого' or M.SECTION_NAME='операции со СК итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_10)+MAX(V.SUM_ACC_10),
        SUM_ACC_18 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE CASE WHEN NOT((M.ID_TABLE = 21 and M.SECTION_NAME='в т.ч.скидка итого')or(M.ID_TABLE = 19 AND (M.SECTION_NAME='услуга итого' or M.SECTION_NAME='скидка итого' or M.SECTION_NAME='операции со СК итого'))) THEN -1 ELSE 0 END END * M.SUM_ACC_18)+MAX(V.SUM_ACC_18),
        SUM_SUP_0 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_0)+MAX(V.SUM_SUP_0),
        SUM_SUP_10 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_10)+MAX(V.SUM_SUP_10),
        SUM_SUP_18 = SUM(CASE WHEN SECTION_NUMBER=1 THEN 1 ELSE -1 END * M.SUM_SUP_18)+MAX(V.SUM_SUP_18)
        into #rem
    FROM #VO V, #MOVE M
    --select * from #rem

SELECT 
    TABLE_NAME = 'Выручка итого в т.ч.',
  	SUM_ACC = ISNULL(SUM(RES.SUM_ACC),0),	--сумма наличных продаж
    SUM_SUP = ISNULL(SUM(RES.SUM_SUP),0),
    SVAT_SUP = ISNULL(SUM(RES.SVAT_SUP),0),        
    SUM_ACC_0 = ISNULL(SUM(RES.SUM_ACC_0),0),
    SUM_ACC_10 = ISNULL(SUM(RES.SUM_ACC_10),0),
    SUM_ACC_18 = ISNULL(SUM(RES.SUM_ACC_18),0),
    SUM_SUP_0 = ISNULL(SUM(RES.SUM_SUP_0),0),
    SUM_SUP_10 = ISNULL(SUM(RES.SUM_SUP_10),0),
    SUM_SUP_18 = ISNULL(SUM(RES.SUM_SUP_18),0)
into #di
FROM (
SELECT
    TABLE_NAME = 'Выручка итого в т.ч.',   --скидка по товарам
  	SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT),0),	--сумма наличных продаж
    SUM_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END *(L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY),0),
    SVAT_SUP = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PVAT_SUP * CH_I.QUANTITY),0),
    SUM_ACC_0 = ISNULL(SUM(CASE WHEN VAT_SAL = 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),
    SUM_ACC_10 = ISNULL(SUM(CASE WHEN VAT_SAL = 10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),
    SUM_ACC_18 = ISNULL(SUM(CASE WHEN VAT_SAL = 18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM_DISCOUNT ELSE 0 END),0),
    SUM_SUP_0 = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 0 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0),
    SUM_SUP_10 = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 10 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0),
    SUM_SUP_18 = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CASE WHEN VAT_SUP = 18 THEN (L.PRICE_SUP - L.PVAT_SUP) * CH_I.QUANTITY ELSE 0 END),0)
FROM CASH_SESSION CS
INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
INNER JOIN LOT L ON L.ID_LOT_GLOBAL = Ch_I.ID_LOT_GLOBAL
WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) AND C.ID_CONTRACTOR = @ID_CONTRACTOR AND CH.DOCUMENT_STATE = 'PROC'
UNION ALL
SELECT
    TABLE_NAME = 'Выручка итого в т.ч.',
  	SUM_ACC = ISNULL(SUM(CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM),0),	--сумма наличных продаж
    SUM_SUP = 0,
    SVAT_SUP = 0,
    SUM_ACC_0 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 0 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_ACC_10 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 10 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_ACC_18 = ISNULL(SUM(CASE WHEN CONVERT(BIGINT,TAX_RATE) = 18 THEN CASE WHEN CH.CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * CH_I.SUMM ELSE 0 END),0),
    SUM_SUP_0 = 0,
    SUM_SUP_10 = 0,
    SUM_SUP_18 = 0
FROM CASH_SESSION CS
INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
INNER JOIN CHEQUE CH ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
INNER JOIN SERVICE_4_SALE_ITEM S4S ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL
inner JOIN SERVICE S ON S.ID_SERVICE = S4S.ID_SERVICE
inner JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = S.ID_TAX_TYPE
WHERE (@DATE_FR<=CS.DATE_CLOSE and CS.DATE_CLOSE<= @DATE_TO) AND C.ID_CONTRACTOR = @ID_CONTRACTOR AND CH.DOCUMENT_STATE = 'PROC') RES 

--select * from #di

declare @s_0 money
declare @s_10 money
declare @s_18 money

declare @s_to money

select
	@s_0 = SUM(sum_acc_0),	
	@s_10 = SUM(sum_acc_10),
	@s_18 = SUM(sum_acc_18)
from #move where table_name = 'Выручка итого в т.ч.'--) - (select sum(sum_acc) from #di)

set @s_0 = @s_0 - (select sum(sum_acc_0) from #di)
set @s_10 = @s_10 - (select sum(sum_acc_10) from #di)
set @s_18 = @s_18 - (select sum(sum_acc_18) from #di)

set @s_to = @s_0 + @s_10 / 1.1 + @s_18 / 1.18
--select @s_to
declare @avg_adprice money

IF (@use_vat <> 1)
BEGIN	
	set @avg_adprice = case when isnull(@cost_credit, 0) = 0 then null else (@summ - @cost_credit) / @cost_credit * 100 end
END ELSE
BEGIN
	set @avg_adprice = case when isnull(@cost_credit, 0) = 0 then null else (@s_to - @cost_credit) / @cost_credit * 100 end
END

SELECT
	SUMM = @summ,
	COUNT_CH = @count_ch,
	AVG_COST = @avg_cost,
	COST_CREDIT = @cost_credit,
	AVG_ADPRICE = @avg_adprice,
	REMAIN = (select sum_acc from #rem)

RETURN
GO

/*
EXEC DBO.REPEX_MONTH_REPORT N'
<XML>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<DATE>2010-05-21T15:00:00.000</DATE>
</XML>'*/