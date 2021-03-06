SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
SET DATEFORMAT DMY 
GO
--------------------------
IF OBJECT_ID('REP_FACTOR_RECEIPTS_HOUR') IS NULL BEGIN
    EXEC ('CREATE PROCEDURE DBO.REP_FACTOR_RECEIPTS_HOUR AS RETURN') 
END
GO
ALTER PROCEDURE REP_FACTOR_RECEIPTS_HOUR
    @XMLPARAM NTEXT
AS

DECLARE @DATE_FR DATETIME,@DATE_TO DATETIME,@ID_CONTRACTOR BIGINT,@HDOC INT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT
    @ID_CONTRACTOR = ID_CONTRACTOR,
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO	
FROM OPENXML(@HDOC, '/XML') WITH(
	ID_CONTRACTOR BIGINT 'ID_CONTRACTOR',
	DATE_FR DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO'
)

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT

declare @calc table(date_cheque datetime)
declare @date_cheque datetime
set @date_cheque = @date_fr

--���������� ���� � ��������������� �������
DECLARE @COUNT_DAYS INT
SET @COUNT_DAYS = DATEDIFF(DD, @DATE_FR, @DATE_TO)+1
PRINT @COUNT_DAYS

CREATE TABLE #MAIN(
--    date_cheque DATETIME,
    DATE_CH VARCHAR(16), 
    US_NAME VARCHAR(16),
    COUNT_CH MONEY,
    SUM_CH_1 MONEY,
    SUM_CH_2 MONEY,
    SUM_CH_3 MONEY,
    SUM_CH_4 MONEY,
    SUM_CH_5 MONEY,
    SUM_CH_6 MONEY,
    SUM_CH_7 MONEY,
    SUM_CH_8 MONEY,
    SUM_CH_9 MONEY,
    SUM_CH_10 MONEY,
    SUM_CH_11 MONEY,
    SUM_CH_12 MONEY,
    SUM_CH_13 MONEY,
    SUM_CH_14 MONEY,
    SUM_CH_15 MONEY,
    SUM_CH_16 MONEY,
    SUM_CH_17 MONEY,
    SUM_CH_18 MONEY,
    SUM_CH_19 MONEY,
    SUM_CH_20 MONEY,
    SUM_CH_21 MONEY,
    SUM_CH_22 MONEY,
    SUM_CH_23 MONEY,
    SUM_CH_24 MONEY
)

create table #CHEQUE_INFO(
    ID_USER VARCHAR(36),
    US_NAME VARCHAR(50),
    CS MONEY,
    date_cheque DATETIME,
    summ MONEY,
    id_user_data bigint
)

while (@date_cheque<=@date_to)
begin
    INSERT INTO #CHEQUE_INFO
select
     ID_USER = U.ID_USER,
     US_NAME = u.name,
     CS = CH.CS,
     date_cheque = CH.date_cheque,
     summ = CH.summ,
     id_user_data = CH.id_user_data
from(
    SELECT
         CS = CASE WHEN CHEQUE_TYPE='SALE' THEN 1 
                   ELSE -1
              END,
    	date_cheque = c.date_cheque,
    	summ = c.summ,
    	id_user_data = c.id_user_data
    FROM cash_register cr
    	left join cash_session cs on cr.id_cash_register = cs.id_cash_register
    	inner join cheque c on c.id_cash_session_global = cs.id_cash_session_global
    WHERE c.DOCUMENT_STATE = 'PROC'
     	AND c.CHEQUE_TYPE IN ('SALE','RETURN')
     	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR
     	AND c.DATE_CHEQUE BETWEEN CONVERT(DATETIME,FLOOR(CONVERT(MONEY,@DATE_CHEQUE))) AND CONVERT(DATETIME,FLOOR(CONVERT(MONEY,@DATE_CHEQUE)+1))
    ) CH
    LEFT JOIN [META_USER] U ON CH.ID_USER_DATA = U.USER_NUM
set @date_cheque = dateadd(d,1,@date_cheque)
end

--select * from #CHEQUE_INFO order by date_cheque

insert into #main
SELECT 
    DATE_CH = CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104), 
    US_NAME = CH.US_NAME,
    COUNT_CH = SUM(CH.CS),
    SUM_CH_1 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=1 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_2 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=2 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_3 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=3 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_4 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=4 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_5 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=5 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_6 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=6 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_7 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=7 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_8 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=8 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_9 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=9 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_10 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=10 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_11 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=11 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_12 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=12 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_13 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=13 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_14 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=14 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_15 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=15 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_16 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=16 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_17 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=17 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_18 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=18 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_19 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=19 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_20 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=20 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_21 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=21 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_22 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=22 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_23 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=23 THEN CH.CS ELSE NULL END * CH.SUMM),0),
    SUM_CH_24 = ISNULL(SUM(CASE WHEN DATEPART(hh,CH.DATE_CHEQUE)=0 THEN CH.CS ELSE NULL END * CH.SUMM),0)
--INTO #MAIN
FROM #CHEQUE_INFO CH
--     (SELECT
--          CS = CASE WHEN CHEQUE_TYPE='SALE' THEN 1 
--                    ELSE -1
--               END,
--     	c.date_cheque,
--     	c.summ,
--     	c.id_user_data
--     FROM cash_register cr
--     	left join cash_session cs on cr.id_cash_register = cs.id_cash_register
--     	inner join cheque c on c.id_cash_session_global = cs.id_cash_session_global
--     WHERE c.DOCUMENT_STATE = 'PROC'
--     	AND c.CHEQUE_TYPE IN ('SALE','RETURN')
--     	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR
--     	AND c.DATE_CHEQUE BETWEEN CONVERT(DATETIME,FLOOR(CONVERT(MONEY,@DATE_CHEQUE))) AND CONVERT(DATETIME,FLOOR(CONVERT(MONEY,@DATE_CHEQUE)+1))
--     ) CH
--     LEFT JOIN [META_USER] U ON CH.ID_USER_DATA = U.USER_NUM
GROUP BY CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),104),CH.US_NAME
-- 
-- SELECT * FROM #MAIN order by date_ch
-- --������� ����� ���� �� ������ �� ������� �����������
CREATE TABLE #AVG_TABLE(
    US_NAME VARCHAR(16),
    COUNT_CH MONEY,
    SUM_CH_1 MONEY,
    SUM_CH_2 MONEY,
    SUM_CH_3 MONEY,
    SUM_CH_4 MONEY,
    SUM_CH_5 MONEY,
    SUM_CH_6 MONEY,
    SUM_CH_7 MONEY,
    SUM_CH_8 MONEY,
    SUM_CH_9 MONEY,
    SUM_CH_10 MONEY,
    SUM_CH_11 MONEY,
    SUM_CH_12 MONEY,
    SUM_CH_13 MONEY,
    SUM_CH_14 MONEY,
    SUM_CH_15 MONEY,
    SUM_CH_16 MONEY,
    SUM_CH_17 MONEY,
    SUM_CH_18 MONEY,
    SUM_CH_19 MONEY,
    SUM_CH_20 MONEY,
    SUM_CH_21 MONEY,
    SUM_CH_22 MONEY,
    SUM_CH_23 MONEY,
    SUM_CH_24 MONEY
)
insert into #AVG_TABLE
SELECT
    US_NAME,
    COUNT_CH = SUM(COUNT_CH)/@count_days,
    SUM_CH_1 = SUM(SUM_CH_1)/@count_days,
    SUM_CH_2 = SUM(SUM_CH_2)/@count_days,
    SUM_CH_3 = SUM(SUM_CH_3)/@count_days,
    SUM_CH_4 = SUM(SUM_CH_4)/@count_days,
    SUM_CH_5 = SUM(SUM_CH_5)/@count_days,
    SUM_CH_6 = SUM(SUM_CH_6)/@count_days,
    SUM_CH_7 = SUM(SUM_CH_7)/@count_days,
    SUM_CH_8 = SUM(SUM_CH_8)/@count_days,
    SUM_CH_9 = SUM(SUM_CH_9)/@count_days,
    SUM_CH_10 = SUM(SUM_CH_10)/@count_days,
    SUM_CH_11 = SUM(SUM_CH_11)/@count_days,
    SUM_CH_12 = SUM(SUM_CH_12)/@count_days,
    SUM_CH_13 = SUM(SUM_CH_13)/@count_days,
    SUM_CH_14 = SUM(SUM_CH_14)/@count_days,
    SUM_CH_15 = SUM(SUM_CH_15)/@count_days,
    SUM_CH_16 = SUM(SUM_CH_16)/@count_days,
    SUM_CH_17 = SUM(SUM_CH_17)/@count_days,
    SUM_CH_18 = SUM(SUM_CH_18)/@count_days,
    SUM_CH_19 = SUM(SUM_CH_19)/@count_days,
    SUM_CH_20 = SUM(SUM_CH_20)/@count_days,
    SUM_CH_21 = SUM(SUM_CH_21)/@count_days,
    SUM_CH_22 = SUM(SUM_CH_22)/@count_days,
    SUM_CH_23 = SUM(SUM_CH_23)/@count_days,
    SUM_CH_24 = SUM(SUM_CH_24)/@count_days
FROM #MAIN
GROUP BY US_NAME

select * from #AVG_TABLE

SELECT
    COUNT_CH = SUM(COUNT_CH),
    SUM_CH_1 = SUM(SUM_CH_1),
    SUM_CH_2 = SUM(SUM_CH_2),
    SUM_CH_3 = SUM(SUM_CH_3),
    SUM_CH_4 = SUM(SUM_CH_4),
    SUM_CH_5 = SUM(SUM_CH_5),
    SUM_CH_6 = SUM(SUM_CH_6),
    SUM_CH_7 = SUM(SUM_CH_7),
    SUM_CH_8 = SUM(SUM_CH_8),
    SUM_CH_9 = SUM(SUM_CH_9),
    SUM_CH_10 = SUM(SUM_CH_10),
    SUM_CH_11 = SUM(SUM_CH_11),
    SUM_CH_12 = SUM(SUM_CH_12),
    SUM_CH_13 = SUM(SUM_CH_13),
    SUM_CH_14 = SUM(SUM_CH_14),
    SUM_CH_15 = SUM(SUM_CH_15),
    SUM_CH_16 = SUM(SUM_CH_16),
    SUM_CH_17 = SUM(SUM_CH_17),
    SUM_CH_18 = SUM(SUM_CH_18),
    SUM_CH_19 = SUM(SUM_CH_19),
    SUM_CH_20 = SUM(SUM_CH_20),
    SUM_CH_21 = SUM(SUM_CH_21),
    SUM_CH_22 = SUM(SUM_CH_22),
    SUM_CH_23 = SUM(SUM_CH_23),
    SUM_CH_24 = SUM(SUM_CH_24)
FROM #AVG_TABLE

----��� �������
SELECT
    DATE_CH = DATEPART(hh,CH.DATE_CHEQUE),--CONVERT(VARCHAR,CONVERT(DATETIME, FLOOR(CONVERT(MONEY,CH.DATE_CHEQUE))),100),
    NAME_C = US_NAME,
    SUMM = SUM(CH.SUMM*CH.CS)/@count_days
INTO #GRAPHIC
FROM #CHEQUE_INFO CH
GROUP BY DATEPART(hh,CH.DATE_CHEQUE),US_NAME

select * from #GRAPHIC

SELECT SUMM_T = SUM(SUMM) FROM #GRAPHIC

RETURN
GO

--exec REP_FACTOR_RECEIPTS_HOUR @xmlParam = N'<XML><ID_CONTRACTOR>5271</ID_CONTRACTOR><DATE_FROM>2008-11-01T00:00:00.000</DATE_FROM><DATE_TO>2008-11-10T00:00:00.000</DATE_TO></XML>'
--------------------------------------------------------------

SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REP_FACTOR_RECEIPTS_DAYS') IS NULL EXEC ('CREATE PROCEDURE REP_FACTOR_RECEIPTS_DAYS AS RETURN')
GO
ALTER PROCEDURE REP_FACTOR_RECEIPTS_DAYS
    @XMLPARAM NTEXT
AS

DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ID_CONTRACTOR BIGINT
DECLARE @HDOC INT


EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@ID_CONTRACTOR = ID_CONTRACTOR
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO',
	ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
)

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM	@DATE_FR OUTPUT, @DATE_TO OUTPUT
EXEC DBO.USP_RANGE_DAYS	@DATE_FR OUTPUT, @DATE_TO OUTPUT

--SELECT @DATE_FR, @DATE_TO, @id_contractor
--select * from #store

DECLARE @TEMP_T TABLE(
	DATE_OP DATETIME,
	SUM_SUP MONEY,
	SUM_SAL MONEY
)
DECLARE @temp_date DATETIME
DECLARE @SUM_SUP MONEY
DECLARE @SUM_SAL MONEY

SET @temp_date = @date_fr
SET @SUM_SUP = 0
SET @SUM_SAL = 0

SELECT
	@SUM_SUP = ISNULL(SUM(CASE WHEN DM.ID_TABLE = 19 AND DM.CODE_OP = 'DIS' THEN -1 ELSE 1 END * DM.SUM_SUP * DM.SIGN_OP), 0),
	@SUM_SAL = ISNULL(SUM(CASE WHEN DM.ID_TABLE = 19 AND DM.CODE_OP = 'DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP), 0)
FROM DOC_MOVEMENT DM
WHERE DM.DATE_OP < @temp_date and (id_store is null or id_store in (select id_store from store where id_contractor = @id_contractor))

INSERT INTO @TEMP_T (DATE_OP, SUM_SUP, SUM_SAL)
SELECT DATE_OP = @temp_date, SUM_SUP = @SUM_SUP, SUM_SAL = @SUM_SAL

SET @temp_date = @temp_date

WHILE @temp_date <= CAST(CONVERT(VARCHAR(8), @DATE_TO, 112) AS DATETIME)
BEGIN

SELECT
	@SUM_SUP = ISNULL(SUM(CASE WHEN DM.ID_TABLE = 19 AND DM.CODE_OP = 'DIS' THEN -1 ELSE 1 END * DM.SUM_SUP * DM.SIGN_OP), 0) + @SUM_SUP,
	@SUM_SAL = ISNULL(SUM(CASE WHEN DM.ID_TABLE = 19 AND DM.CODE_OP = 'DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP), 0) + @SUM_SAL
FROM DOC_MOVEMENT DM
WHERE DM.DATE_OP BETWEEN @temp_date AND @temp_date + 1 and (id_store is null or id_store in (select id_store from store where id_contractor = @id_contractor))

INSERT INTO @TEMP_T (DATE_OP, SUM_SUP, SUM_SAL)
SELECT DATE_OP = @temp_date + 1, SUM_SUP = @SUM_SUP, SUM_SAL = @SUM_SAL

SET @temp_date = @temp_date + 1

END

--select * from @temp_t

SELECT
	DATE_OP = T3.DATE_OP,
	SUM_SUP = T1.SUM_SUP,
	SUM_SAL = T1.SUM_SAL,
	BEGIN_SUM_SUP = T3.SUM_SUP,
	BEGIN_SUM_SAL = T3.SUM_SAL,
	CHEQUE_COUNT = T4.CS,
	CHEQUE_SUM_SUP = ISNULL(T2.SUM_SUP, 0),
	CHEQUE_SUM = T2.SUM_SAL,
	TORG_N = CASE WHEN T3.SUM_SAL = 0 OR T2.SUM_SUP = 0 THEN NULL ELSE T3.SUM_SAL / T3.SUM_SUP * 100 - 100 END,
	SUM_DISCOUNT = T2.SUM_DISCOUNT,
	TORG_N2 = CASE WHEN T2.SUM_SAL = 0 OR T2.SUM_SUP = 0 THEN NULL ELSE T2.SUM_SAL / T2.SUM_SUP * 100 - 100 END,
	DAYS_REM = CASE WHEN T2.SUM_SUP = 0 THEN NULL ELSE T3.SUM_SUP / T2.SUM_SUP END
FROM
(SELECT
	DATE_OP = CAST(CONVERT(VARCHAR(8), DATE_OP, 112) AS DATETIME),
	SUM_SUP = SUM(SUM_SUP),
	SUM_SAL = SUM(SUM_ACC)
FROM DOC_MOVEMENT
WHERE (ID_TABLE = 2 OR ID_TABLE = 30) AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO AND ID_CONTRACTOR_TO = @ID_CONTRACTOR
GROUP BY CAST(CONVERT(VARCHAR(8), DATE_OP, 112) AS DATETIME)) AS T1

FULL OUTER JOIN

(SELECT
	DATE_OP = CAST(CONVERT(VARCHAR(8), C.DATE_CHEQUE, 112) AS DATETIME),         
	SUM_SUP = SUM(CASE WHEN C.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * L.PRICE_SUP * CHI.QUANTITY),
	SUM_SAL = SUM(CASE WHEN C.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM),
	SUM_DISCOUNT = SUM(CASE WHEN C.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM_DISCOUNT)
FROM CASH_REGISTER CR
	LEFT JOIN CASH_SESSION CS ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CHEQUE C ON C.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = C.ID_CHEQUE_GLOBAL
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
WHERE DOCUMENT_STATE = 'PROC'
	AND CHEQUE_TYPE IN ('SALE','RETURN')
	AND DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR
GROUP BY CAST(CONVERT(VARCHAR(8), C.DATE_CHEQUE, 112) AS DATETIME)) AS T2 ON T1.DATE_OP = T2.DATE_OP

LEFT JOIN

(SELECT
	DATE_OP = CAST(CONVERT(VARCHAR(8), C.DATE_CHEQUE, 112) AS DATETIME),         
	CS = SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END)
FROM CASH_REGISTER CR
	LEFT JOIN CASH_SESSION CS ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CHEQUE C ON C.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
WHERE DOCUMENT_STATE = 'PROC'
	AND CHEQUE_TYPE IN ('SALE','RETURN')
	AND DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR
GROUP BY CAST(CONVERT(VARCHAR(8), C.DATE_CHEQUE, 112) AS DATETIME)) AS T4 ON T4.DATE_OP = T2.DATE_OP

INNER JOIN @TEMP_T AS T3 ON T3.DATE_OP = T1.DATE_OP OR T3.DATE_OP = T2.DATE_OP

ORDER BY t3.date_op

RETURN
GO

/*
exec REP_FACTOR_RECEIPTS_DAYS N'
<XML>
	<ID_CONTRACTOR>5287</ID_CONTRACTOR>
	<DATE_FROM>2009-02-01T16:13:14.453</DATE_FROM>
	<DATE_TO>2009-02-28T16:13:14.453</DATE_TO>
</XML>'*/

SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REP_FACTOR_RECEIPTS_MONTHS') IS NULL EXEC ('CREATE PROCEDURE REP_FACTOR_RECEIPTS_MONTHS AS RETURN')
GO

ALTER PROCEDURE REP_FACTOR_RECEIPTS_MONTHS
    @XMLPARAM NTEXT
AS

DECLARE @DATE_FROM DATETIME,@DATE_TO DATETIME,@ID_CONTRACTOR BIGINT,@HDOC INT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @ID_CONTRACTOR = ID_CONTRACTOR
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
        ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
    )

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SET @DATE_FROM = DATEADD(day,-DATEPART(day,@DATE_FROM)+1,@DATE_FROM)
SET @DATE_TO = DATEADD(day,-1,DATEADD(month,1,DATEADD(day,-DATEPART(day,@DATE_TO)+1,@DATE_TO)))

EXEC DBO.USP_RANGE_NORM	@DATE_FROM OUTPUT,	@DATE_TO OUTPUT
EXEC DBO.USP_RANGE_DAYS	@DATE_FROM OUTPUT,	@DATE_TO OUTPUT

SELECT
    DATE_OP = CONVERT(VARCHAR(7), CS.DATE_CLOSE, 120),
    COUNT_CH = SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),
    SUMM = ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * SUMM),0),
    AVG_COST = CASE WHEN ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),0)!=0 THEN ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END * SUMM),0)/ISNULL(SUM(CASE WHEN CHEQUE_TYPE='SALE' THEN 1 ELSE -1 END),0) ELSE 0 END
FROM CASH_REGISTER CR
	LEFT JOIN CASH_SESSION CS ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CHEQUE C ON C.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
WHERE DOCUMENT_STATE = 'PROC'
	AND CHEQUE_TYPE IN ('SALE','RETURN')
	AND CS.DATE_CLOSE BETWEEN @DATE_FROM AND @DATE_TO
	AND CR.ID_CONTRACTOR = @ID_CONTRACTOR
GROUP BY CONVERT(VARCHAR(7), CS.DATE_CLOSE, 120)
ORDER BY CONVERT(VARCHAR(7), CS.DATE_CLOSE, 120)

RETURN
GO

/*
exec REP_FACTOR_RECEIPTS_MONTHS N'
<XML>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<DATE_FROM>2009-01-01T15:00:00.000</DATE_FROM>
	<DATE_TO>2009-12-01T17:00:00.000</DATE_TO>
</XML>'*/

SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT') IS NULL EXEC ('CREATE PROCEDURE DBO.REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT
    @XMLPARAM NTEXT
AS

DECLARE @DATE_FROM DATETIME,
		@DATE_TO DATETIME,
		@HDOC INT,
		@ALL_CONTRACTORS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT TOP 1
	@DATE_FROM = DATE_FROM,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FROM DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #CONTRACTORS
FROM OPENXML(@HDOC, '//XML/ID_CONTRACTOR', 2) WITH(ID_CONTRACTOR VARCHAR(4) '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CONTRACTORS = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

--SELECT * FROM #CONTRACTORS
--SELECT @ALL_CONTRACTORS

SET @DATE_FROM = DATEADD(DAY,-DATEPART(DAY,@DATE_FROM)+1,@DATE_FROM)
SET @DATE_TO = DATEADD(DAY,-1,DATEADD(MONTH,1,DATEADD(DAY,-DATEPART(DAY,@DATE_TO)+1,@DATE_TO)))

EXEC DBO.USP_RANGE_NORM	@DATE_FROM OUTPUT,	@DATE_TO OUTPUT
EXEC DBO.USP_RANGE_DAYS	@DATE_FROM OUTPUT,	@DATE_TO OUTPUT

--SELECT @DATE_FROM, @DATE_TO

SELECT
	ID_CONTRACTOR = CR.ID_CONTRACTOR,
	CONTRACTOR_NAME = CT.NAME,
	MTH = DATEPART(MONTH, CS.DATE_CLOSE),
	CHEQUE_COUNT = COUNT(DISTINCT CH.ID_CHEQUE),
	SUM_SUP = SUM(CHI.QUANTITY * L.PRICE_SUP),
	SUM_SAL = SUM(CHI.SUMM),
	cheque_type = ch.cheque_type
INTO #TEMP_TX
FROM CASH_SESSION CS
	INNER JOIN CHEQUE CH ON CH.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	INNER JOIN CASH_REGISTER CR ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	INNER JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR = CR.ID_CONTRACTOR
WHERE CS.DATE_CLOSE IS NOT NULL AND
	(@ALL_CONTRACTORS = 1 OR CR.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
	AND CS.DATE_CLOSE BETWEEN @DATE_FROM AND @DATE_TO
GROUP BY CR.ID_CONTRACTOR, CT.NAME, DATEPART(MONTH, CS.DATE_CLOSE), ch.cheque_type

--SELECT * FROM #TEMP_TX

SELECT
	ID_CONTRACTOR = ID_CONTRACTOR,
	CONTRACTOR_NAME = CONTRACTOR_NAME,
	MTH = MTH,
	CHEQUE_COUNT = SUM(CASE WHEN cheque_type = 'SALE' THEN 1 ELSE -1 END * CHEQUE_COUNT),
	SUM_SUP = SUM(CASE WHEN cheque_type = 'SALE' THEN 1 ELSE -1 END * SUM_SUP),
	SUM_SAL = SUM(CASE WHEN cheque_type = 'SALE' THEN 1 ELSE -1 END * SUM_SAL)
INTO #TEMP_T1
FROM #TEMP_TX
GROUP BY ID_CONTRACTOR, CONTRACTOR_NAME, MTH

--select * from #temp_t1

SELECT
	ID_OP = 2,
	OP = CAST('������� ����� �������' AS NVARCHAR(50)),
	END_OP = CAST('�������� ������� ����� �������:' AS NVARCHAR(50)),
	CONTRACTOR = CONTRACTOR_NAME,
	JAN = SUM(CASE WHEN MTH = 1 THEN SUM_SUP ELSE 0 END),
	FEB = SUM(CASE WHEN MTH = 2 THEN SUM_SUP ELSE 0 END),
	MAR = SUM(CASE WHEN MTH = 3 THEN SUM_SUP ELSE 0 END),
	APR = SUM(CASE WHEN MTH = 4 THEN SUM_SUP ELSE 0 END),
	MAY = SUM(CASE WHEN MTH = 5 THEN SUM_SUP ELSE 0 END),
	JUN = SUM(CASE WHEN MTH = 6 THEN SUM_SUP ELSE 0 END),
	JUL = SUM(CASE WHEN MTH = 7 THEN SUM_SUP ELSE 0 END),
	AUG = SUM(CASE WHEN MTH = 8 THEN SUM_SUP ELSE 0 END),
	SEP = SUM(CASE WHEN MTH = 9 THEN SUM_SUP ELSE 0 END),
	OCT = SUM(CASE WHEN MTH = 10 THEN SUM_SUP ELSE 0 END),
	NOV = SUM(CASE WHEN MTH = 11 THEN SUM_SUP ELSE 0 END),
	DEC = SUM(CASE WHEN MTH = 12 THEN SUM_SUP ELSE 0 END)
INTO #TEMP_T2
FROM #TEMP_T1
GROUP BY ID_CONTRACTOR, CONTRACTOR_NAME

INSERT INTO #TEMP_T2
SELECT
	ID_OP = 1,
	OP = '���������� �����',
	END_OP = CAST('����� ���������� �����:' AS NVARCHAR(50)),
	CONTRACTOR = CONTRACTOR_NAME,
	JAN = SUM(CASE WHEN MTH = 1 THEN CHEQUE_COUNT ELSE 0 END),
	FEB = SUM(CASE WHEN MTH = 2 THEN CHEQUE_COUNT ELSE 0 END),
	MAR = SUM(CASE WHEN MTH = 3 THEN CHEQUE_COUNT ELSE 0 END),
	APR = SUM(CASE WHEN MTH = 4 THEN CHEQUE_COUNT ELSE 0 END),
	MAY = SUM(CASE WHEN MTH = 5 THEN CHEQUE_COUNT ELSE 0 END),
	JUN = SUM(CASE WHEN MTH = 6 THEN CHEQUE_COUNT ELSE 0 END),
	JUL = SUM(CASE WHEN MTH = 7 THEN CHEQUE_COUNT ELSE 0 END),
	AUG = SUM(CASE WHEN MTH = 8 THEN CHEQUE_COUNT ELSE 0 END),
	SEP = SUM(CASE WHEN MTH = 9 THEN CHEQUE_COUNT ELSE 0 END),
	OCT = SUM(CASE WHEN MTH = 10 THEN CHEQUE_COUNT ELSE 0 END),
	NOV = SUM(CASE WHEN MTH = 11 THEN CHEQUE_COUNT ELSE 0 END),
	DEC = SUM(CASE WHEN MTH = 12 THEN CHEQUE_COUNT ELSE 0 END)
FROM #TEMP_T1
GROUP BY CONTRACTOR_NAME

INSERT INTO #TEMP_T2
SELECT
	ID_OP = 3,
	OP = '��������� ����� �������',
	END_OP = CAST('�������� ��������� ����� �������:' AS NVARCHAR(50)),
	CONTRACTOR = CONTRACTOR_NAME,
	JAN = SUM(CASE WHEN MTH = 1 THEN SUM_SAL ELSE 0 END),
	FEB = SUM(CASE WHEN MTH = 2 THEN SUM_SAL ELSE 0 END),
	MAR = SUM(CASE WHEN MTH = 3 THEN SUM_SAL ELSE 0 END),
	APR = SUM(CASE WHEN MTH = 4 THEN SUM_SAL ELSE 0 END),
	MAY = SUM(CASE WHEN MTH = 5 THEN SUM_SAL ELSE 0 END),
	JUN = SUM(CASE WHEN MTH = 6 THEN SUM_SAL ELSE 0 END),
	JUL = SUM(CASE WHEN MTH = 7 THEN SUM_SAL ELSE 0 END),
	AUG = SUM(CASE WHEN MTH = 8 THEN SUM_SAL ELSE 0 END),
	SEP = SUM(CASE WHEN MTH = 9 THEN SUM_SAL ELSE 0 END),
	OCT = SUM(CASE WHEN MTH = 10 THEN SUM_SAL ELSE 0 END),
	NOV = SUM(CASE WHEN MTH = 11 THEN SUM_SAL ELSE 0 END),
	DEC = SUM(CASE WHEN MTH = 12 THEN SUM_SAL ELSE 0 END)
FROM #TEMP_T1
GROUP BY CONTRACTOR_NAME

INSERT INTO #TEMP_T2
SELECT
	ID_OP = 4,
	OP = '������� ��������� �������',
	END_OP = CAST('�������� ������� ��������� �������:' AS NVARCHAR(50)),
	CONTRACTOR = CONTRACTOR_NAME,
	JAN = SUM(CASE WHEN MTH = 1 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	FEB = SUM(CASE WHEN MTH = 2 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	MAR = SUM(CASE WHEN MTH = 3 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	APR = SUM(CASE WHEN MTH = 4 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	MAY = SUM(CASE WHEN MTH = 5 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	JUN = SUM(CASE WHEN MTH = 6 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	JUL = SUM(CASE WHEN MTH = 7 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	AUG = SUM(CASE WHEN MTH = 8 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	SEP = SUM(CASE WHEN MTH = 9 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	OCT = SUM(CASE WHEN MTH = 10 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	NOV = SUM(CASE WHEN MTH = 11 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END),
	DEC = SUM(CASE WHEN MTH = 12 THEN SUM_SAL / CHEQUE_COUNT ELSE 0 END)
FROM #TEMP_T1
GROUP BY CONTRACTOR_NAME

SELECT 
	ID_OP,
	OP,
	CONTRACTOR = CONTRACTOR,
	END_OP,
	JAN,
	FEB,
	MAR,
	APR,
	MAY,
	JUN,
	JUL,
	AUG,
	SEP,
	OCT,
	NOV,
	DEC,
	IJAN = NULL,
	IFEB = CASE WHEN JAN = 0 OR FEB = 0 THEN NULL ELSE FEB / JAN * 100 - 100 END,
	IMAR = CASE WHEN FEB = 0 OR MAR = 0 THEN NULL ELSE MAR / FEB * 100 - 100 END,
	IAPR = CASE WHEN MAR = 0 OR APR = 0 THEN NULL ELSE APR / MAR * 100 - 100 END,
	IMAY = CASE WHEN APR = 0 OR MAY = 0 THEN NULL ELSE MAY / APR * 100 - 100 END,
	IJUN = CASE WHEN MAY = 0 OR JUN = 0 THEN NULL ELSE JUN / MAY * 100 - 100 END,
	IJUL = CASE WHEN JUN = 0 OR JUL = 0 THEN NULL ELSE JUL / JUN * 100 - 100 END,
	IAUG = CASE WHEN JUL = 0 OR AUG = 0 THEN NULL ELSE AUG / JUL * 100 - 100 END,
	ISEP = CASE WHEN AUG = 0 OR SEP = 0 THEN NULL ELSE SEP / AUG * 100 - 100 END,
	IOCT = CASE WHEN SEP = 0 OR OCT = 0 THEN NULL ELSE OCT / SEP * 100 - 100 END,
	INOV = CASE WHEN OCT = 0 OR NOV = 0 THEN NULL ELSE NOV / OCT * 100 - 100 END,
	IDEC = CASE WHEN NOV = 0 OR DEC = 0 THEN NULL ELSE DEC / NOV * 100 - 100 END
FROM #TEMP_T2 ORDER BY ID_OP

SELECT
	CONTRACTOR_NAME,
	MTH = CASE 
			WHEN MTH = 1 THEN '������'
			WHEN MTH = 2 THEN '�������'
			WHEN MTH = 3 THEN '����'
			WHEN MTH = 4 THEN '������'
			WHEN MTH = 5 THEN '���'
			WHEN MTH = 6 THEN '����'
			WHEN MTH = 7 THEN '����'
			WHEN MTH = 8 THEN '������'
			WHEN MTH = 9 THEN '��������'
			WHEN MTH = 10 THEN '�������'
			WHEN MTH = 11 THEN '������'
			WHEN MTH = 12 THEN '�������'
		END,
	SUM_SAL
FROM #TEMP_T1

RETURN
GO

/*
EXEC REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT N'
<XML>	
	<DATE_FROM>2009-01-01T16:13:14.453</DATE_FROM>
	<DATE_TO>2009-09-12T16:13:14.453</DATE_TO>
</XML>'*/