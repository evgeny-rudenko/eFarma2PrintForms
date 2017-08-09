SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_CHECKSUM') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_CHECKSUM AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_CHECKSUM
	@XMLPARAM NTEXT AS

DECLARE @DATE_TO DATETIME
DECLARE @DATE_FR DATETIME
DECLARE @temp nvarchar(100)
DECLARE @HDOC INT
declare @ALL_STORES bit
declare @ALL_CASH bit
declare @L money
declare @R money
declare @maxR money
declare @delta money

DECLARE @LM MONEY
DECLARE @CARD BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT ID_STORE = STORE INTO #STORES FROM OPENXML(@HDOC, '/XML/STORE') WITH(STORE BIGINT '.')
IF (@@ROWCOUNT = 0) SET @ALL_STORES = 1  

SELECT ID_CASH_REGISTER = CASH INTO #CASHES FROM OPENXML(@HDOC, '/XML/CASH') WITH(CASH BIGINT '.')
IF (@@ROWCOUNT = 0) SET @ALL_CASH = 1  
--select * from #CASHES
SELECT 
    @DATE_FR = DATE_FR,
    @DATE_TO = DATE_TO,
    @DELTA = DELTA,
	@LM = LM,
	@CARD = CARD
FROM OPENXML(@HDOC, '/XML', 2) WITH(
    DATE_FR DATETIME,
    DATE_TO DATETIME,
	DELTA INT,
	LM MONEY,
	CARD BIT
)

--select @lm, @card

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

DECLARE @v_total_summ money
DECLARE @v_average_summ money
DECLARE @v_maxR money
DECLARE @v_gistogramma int
set @v_maxR = 0
set @v_average_summ = 0
set @v_total_summ = 0
set @v_gistogramma = 0

-- Declare the variables to store the values returned by FETCH.
DECLARE @r_summ money
set @r_summ = 0

DECLARE @i int
set @i = 0

declare @temp_t table (summ money)

if (@card = 1)
begin
insert into @temp_t
SELECT summ = sum(chi.SUMM)
from CHEQUE ch
	inner join cheque_item chi on chi.id_cheque_global = ch.id_cheque_global
	left join lot l on l.id_lot_global = chi.id_lot_global
	left JOIN SERVICE_4_SALE AS S ON S.ID_SERVICE_4_SALE = CHI.ID_LOT_GLOBAL
	LEFT JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL	
where ch.DATE_CHEQUE between @DATE_FR and @DATE_TO
	and ch.id_discount_card_global is null
	and ch.document_state = 'PROC'
	and ch.cheque_type in ('SALE')
	and ((@all_stores = 1) or l.id_store in (select id_store from #stores))
	and ((@ALL_CASH = 1) or CS.ID_CASH_REGISTER in (select ID_CASH_REGISTER from #CASHES))
	and not exists (select * from cheque where id_document_base_global = ch.id_cheque_global and date_cheque > @DATE_FR)
group by ch.id_cheque_global
ORDER BY SUMM DESC
end
else
begin
insert into @temp_t
SELECT summ = sum(chi.SUMM)
from CHEQUE ch
	inner join cheque_item chi on chi.id_cheque_global = ch.id_cheque_global
	left join lot l on l.id_lot_global = chi.id_lot_global
	left JOIN SERVICE_4_SALE AS S ON S.ID_SERVICE_4_SALE = CHI.ID_LOT_GLOBAL	
	LEFT JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL
where ch.DATE_CHEQUE between @DATE_FR and @DATE_TO
	and ch.document_state = 'PROC'
	and ch.cheque_type in ('SALE')
	and ((@all_stores = 1) or l.id_store in (select id_store from #stores))
	and ((@ALL_CASH = 1) or CS.ID_CASH_REGISTER in (select ID_CASH_REGISTER from #CASHES))
	and not exists (select * from cheque where id_document_base_global = ch.id_cheque_global and date_cheque > @DATE_FR)
group by ch.id_cheque_global
ORDER BY SUMM DESC
end
--select * from cash_session
--select * from @temp_t

SELECT @v_maxR = (select top 1 summ from @temp_t)

--select @v_maxR

set @L = @LM

set @R = @LM + @delta

create table #t
(
	number int,
	interval  nvarchar(100),
	gistogramma int,
	total_summ money,
	average_summ money
)

DECLARE abc_cursor CURSOR FOR
select summ from @temp_t

while  (@R <= @v_maxR) or (@R >= @v_maxR and @L < @v_maxR)
BEGIN

OPEN abc_cursor

-- Perform the first fetch and store the values in variables.
-- Note: The variables are in the same order as the columns
-- in the SELECT statement. 

FETCH NEXT FROM abc_cursor
INTO @r_summ
-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN
if (@r_summ >= @L) and (@r_summ <= @R)
begin
  set @v_gistogramma = @v_gistogramma + 1
  set @v_total_summ = @v_total_summ + @r_summ 
end
-- This is executed as long as the previous fetch succeeds.
FETCH NEXT FROM abc_cursor
INTO @r_summ
END

CLOSE abc_cursor

set @i = @i + 1

if (@v_gistogramma <> 0)
set @v_average_summ = @v_total_summ / @v_gistogramma
else
set @v_average_summ = 0


insert into #t values (@i, convert(nvarchar(100), @L) + ' - ' + convert(nvarchar(100), @R), @v_gistogramma, @v_total_summ, @v_average_summ)

if @i = 1
set @L = @L + @delta + 0.01
else
set @L = @L + @delta
set @R = @R + @delta
set @v_total_summ = 0
set @v_gistogramma = 0
set @r_summ = 0

END -- while

DEALLOCATE abc_cursor
--select * from #t
select *
from #t
where total_summ > 0
order by number

select DELTA = convert(nvarchar(100), @delta)

RETURN 0
GO


/*
exec REPEX_CHECKSUM N'
<XML>
	<DATE_FR>2012-03-25T11:00:00.000</DATE_FR>
	<DATE_TO>2012-04-25T15:55:00.000</DATE_TO>
	<DELTA>1000</DELTA>
	<LM>1000.01</LM>
	<CARD>0</CARD>
	<CASH>47</CASH>
	<CASH>48</CASH>
</XML>'
*/



SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO 

IF OBJECT_ID('DBO.REMOVE_REPORT_BY_TYPE_NAME') IS NULL EXEC('CREATE PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME AS RETURN')
GO
ALTER PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME 
	@REPORT_TYPE_NAME VARCHAR(200) AS
	
DECLARE @id_meta_report BIGINT

	select 
		@id_meta_report = id_meta_report
	from meta_report
	where type_name = @REPORT_TYPE_NAME
	--select @id_meta_report
		
	DECLARE @SQL NVARCHAR(200)
	SET @SQL = N'delete from META_REPORT_2_REPORT_GROUPS
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('META_REPORT_2_REPORT_GROUPS') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_csv_export
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_csv_export') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_visible
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_visible') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_managed
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_managed') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	SET @SQL = N'delete from meta_report_settings_archive
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_archive') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	delete from meta_report
	where id_meta_report = @id_meta_report

RETURN 0
GO

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'RCKChecksum.RCKChecksumParams'