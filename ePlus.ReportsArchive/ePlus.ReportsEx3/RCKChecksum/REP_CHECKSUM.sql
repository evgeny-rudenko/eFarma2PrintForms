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
declare @L money
declare @R money
declare @maxR money
declare @delta money

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT ID_STORE = STORE INTO #STORES FROM OPENXML(@HDOC, '/XML/STORE') WITH(STORE BIGINT '.')
IF (@@ROWCOUNT = 0) SET @ALL_STORES = 1  

SELECT 
    @DATE_FR = DATE_FR,
    @DATE_TO = DATE_TO,
    @DELTA = DELTA
FROM OPENXML(@HDOC, '/XML', 2) WITH(
    DATE_FR DATETIME,
    DATE_TO DATETIME,
	DELTA INT
)

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

SELECT summ = sum(chi.SUMM) into #temp_t
from CHEQUE ch
	inner join cheque_item chi on chi.id_cheque_global = ch.id_cheque_global
	left join lot l on l.id_lot_global = chi.id_lot_global
	left JOIN SERVICE_4_SALE AS S ON S.ID_SERVICE_4_SALE = CHI.ID_LOT_GLOBAL
where ch.DATE_CHEQUE between @DATE_FR and @DATE_TO
	and ch.document_state = 'PROC'
	and ch.cheque_type in ('SALE')
	and ((@all_stores = 1) or l.id_store in (select id_store from #stores))
	and not exists (select * from cheque where id_document_base_global = ch.id_cheque_global and date_cheque > @DATE_FR)
group by ch.id_cheque_global
ORDER BY SUMM DESC

--select * from #temp_t

SELECT @v_maxR = (select top 1 summ from #temp_t)

--select @v_maxR

set @L = 0

set @R = @delta

create table #t
(
	number int,
	interval  nvarchar(100),
	gistogramma int,
	total_summ money,
	average_summ money
)

DECLARE abc_cursor CURSOR FOR
select summ from #temp_t

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

select *
from #t
where total_summ > 0
order by number

select DELTA = convert(nvarchar(100), @delta)

RETURN 0
GO

/*
exec repex_checksum N'
<XML>
	<DATE_FR>2009-07-28T11:00:00.000</DATE_FR>
	<DATE_TO>2009-07-28T15:55:00.000</DATE_TO>
	<DELTA>100</DELTA>
</XML>'*/
