SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REP_CHECKSUM') IS NULL) EXEC ('CREATE PROCEDURE REP_CHECKSUM AS RETURN')
GO

ALTER PROCEDURE [dbo].[REP_CHECKSUM]
    @XMLPARAM NTEXT
AS
    DECLARE @DATE_TO DATETIME
    DECLARE @DATE_FROM DATETIME
    DECLARE @temp nvarchar(100)    
    DECLARE @HDOC INT
    declare @ALL_STORES bit
    declare @L money
    declare @R money
    declare @maxR money
    declare @delta money
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT 
        ID_STORE = STORE
    INTO #STORES
    FROM OPENXML(@HDOC, '/XML/STORE') WITH(
        STORE BIGINT '.'
    )
    IF (@@ROWCOUNT=0)
        SET @ALL_STORES = 1
    else    
        set @ALL_STORES = 0
      

    SELECT @DATE_TO = convert(datetime, DATE_TO, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_TO nvarchar(100) 'DATE_TO'
    )
    IF (@@ROWCOUNT=0)
        set @DATE_TO = getdate()

    SELECT @DATE_FROM = convert(datetime, DATE_FROM, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DATE_FROM nvarchar(100) 'DATE_FROM'
    )

    SELECT @delta = convert(money, DELTA, 104)
    FROM OPENXML(@HDOC, '//XML') WITH(
        DELTA nvarchar(100) 'DELTA'
    )

    IF (@@ROWCOUNT=0)
        set @delta = 100


    EXEC SP_XML_REMOVEDOCUMENT @HDOC

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

SELECT @v_maxR = max(SUMM)
from CHEQUE ch
where ch.DATE_CHEQUE >= @DATE_FROM and ch.DATE_CHEQUE <= @DATE_TO

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
SELECT SUMM
from CHEQUE ch
where ch.DATE_CHEQUE >= @DATE_FROM and ch.DATE_CHEQUE <= @DATE_TO
--and (@ALL_STORES=1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
ORDER BY ch.SUMM DESC

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
order by number

select DELTA = convert(nvarchar(100), @delta)

RETURN 0
GO
