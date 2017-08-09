SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_REJECTED_GOODS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_REJECTED_GOODS AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_REJECTED_GOODS
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT

DECLARE @MNAME INT
DECLARE @MSER INT

DECLARE @ALL_CONTRACTOR BIT
DECLARE @ALL_STORE BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
SELECT
	@MNAME = MNAME,
	@MSER = MSER
FROM OPENXML(@HDOC, '/XML') WITH(
	MNAME INT 'MNAME',
	MSER INT 'MSER'
)

SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1

SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') 
WITH(ID_STORE BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_STORE = 1;

declare @sql nvarchar(4000)

create table #temp_t1
(
	ID_LOT_GLOBAL UNIQUEIDENTIFIER,
	GOODS_NAME VARCHAR(255),
	SERIES_NUMBER VARCHAR(2048),
	PRODUCER_NAME VARCHAR(100),
	SUPPLIER_NAME VARCHAR(100),
	QUANTITY_IN MONEY,
	QUANTITY_REM MONEY,
	DOC_NUM VARCHAR(50),
	DOC_DATE DATETIME,
	[SOURCE] VARCHAR(255)
)

declare @join_filter varchar(100)
set @join_filter = 'LEFT(G.NAME, ' + CAST(@MNAME AS VARCHAR) + ') = LEFT(GD.DRUG_TXT, ' + CAST(@MNAME AS VARCHAR) +') AND LEFT(SER.SERIES_NUMBER, ' + CAST(@MSER AS VARCHAR) + ') = LEFT(GD.SERIES_NUMBER, ' + CAST(@MSER AS VARCHAR) + ')'
--select @join_filter

declare @contr_filter varchar(100)
if (@ALL_CONTRACTOR = 1) set @contr_filter = '' else set @contr_filter = 'AND S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)'
--select @contr_filter

declare @store_filter varchar(100)
if (@ALL_STORE = 1) set @store_filter = '' else set @store_filter = 'AND S.ID_STORE IN (SELECT ID_STORE FROM #STORE)'
--select @store_filter

set @sql = 
'
insert into #temp_t1
select
	ID_LOT_GLOBAL = L.id_lot_global,
	GOODS_NAME = g.NAME,
	SERIES_NUMBER = SER.SERIES_NUMBER,
	PRODUCER_NAME = PR.NAME,
	SUPPLIER_NAME = SUP.NAME,
	QUANTITY_IN = (SELECT QUANTITY_ADD FROM LOT_MOVEMENT WHERE ID_LOT_GLOBAL = L.ID_LOT_GLOBAL AND ID_TABLE IN (2, 30)),
	QUANTITY_REM = L.QUANTITY_REM,
	DOC_NUM = LETTER_NR,
	DOC_DATE = LETTER_DATE,
	[SOURCE] = LAB_NM
from LOT L
	INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN PRODUCER PR ON PR.ID_PRODUCER = G.ID_PRODUCER
	INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = L.ID_SUPPLIER
	INNER JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
	INNER JOIN GOODS_DEFECT GD ON ' + @join_filter +
'where L.QUANTITY_REM > 0 ' + @contr_filter + @store_filter

--select @sql

exec sp_executesql @sql, N'@ALL_CONTRACTOR BIT, @ALL_STORE BIT', @ALL_CONTRACTOR = @ALL_CONTRACTOR, @ALL_STORE = @ALL_STORE

--select * from #temp_t1

select
	GOODS_NAME = t1.GOODS_NAME,
	SERIES_NUMBER = t1.SERIES_NUMBER,
	PRODUCER_NAME = t1.PRODUCER_NAME,
	SUPPLIER_NAME = t1.SUPPLIER_NAME,
	QUANTITY_IN = SUM(ISNULL(t1.QUANTITY_IN, 0)),
	QUANTITY_REM = SUM(ISNULL(t1.QUANTITY_REM, 0)),
	DOC_NUM = t1.DOC_NUM,
	DOC_DATE = t1.DOC_DATE,
	[SOURCE] = t1.[SOURCE]
into #temp_t2
from #temp_t1 t1
group by t1.GOODS_NAME,	t1.SERIES_NUMBER, t1.PRODUCER_NAME,	t1.SUPPLIER_NAME, t1.DOC_NUM,
	t1.DOC_DATE, t1.[SOURCE]
	
--select * from #temp_t2
/*
select
	SERIES_NUMBER = t1.SERIES_NUMBER,
	CNAME = ct.NAME,
	AR_DATE = ar.DATE,
	AR_QUANTITY = ari.QUANTITY
into #temp_t3
from #temp_t1 t1
	inner join ACT_RETURN_TO_CONTRACTOR_ITEM ari on t1.ID_LOT_GLOBAL = ari.ID_LOT_GLOBAL
	inner join ACT_RETURN_TO_CONTRACTOR ar on ar.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = ari.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL
	inner join CONTRACTOR ct on ct.ID_CONTRACTOR = ar.ID_CONTRACTOR_FROM*/

select
	SERIES_NUMBER = t1.SERIES_NUMBER,
	CNAME = ct.NAME,
	AR_DATE = ar.DATE,
	AR_QUANTITY = min(ari.QUANTITY)
into #temp_t3
from #temp_t1 t1
	inner join ACT_RETURN_TO_CONTRACTOR_ITEM ari on t1.ID_LOT_GLOBAL = ari.ID_LOT_GLOBAL
	inner join ACT_RETURN_TO_CONTRACTOR ar on ar.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = ari.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL
	inner join CONTRACTOR ct on ct.ID_CONTRACTOR = ar.ID_CONTRACTOR_FROM
group by t1.SERIES_NUMBER, ct.NAME, ar.DATE

--select * from #temp_t3

select
	GOODS_NAME = t1.GOODS_NAME,
	SERIES_NUMBER = t1.SERIES_NUMBER,
	PRODUCER_NAME = t1.PRODUCER_NAME,
	SUPPLIER_NAME = t1.SUPPLIER_NAME,
	QUANTITY_IN = t1.QUANTITY_IN,
	QUANTITY_REM = t1.QUANTITY_REM,
	DOC_NUM = t1.DOC_NUM,
	DOC_DATE = t1.DOC_DATE,
	[SOURCE] = t1.[SOURCE],
	CNAME = t2.CNAME,
	AR_DATE = t2.AR_DATE,
	AR_QUANTITY = t2.AR_QUANTITY,
	AR_QS = (select SUM(ar_quantity) from #temp_t3 where SERIES_NUMBER = t1.SERIES_NUMBER)
from #temp_t2 t1
	left join #temp_t3 t2 on t1.SERIES_NUMBER = t2.SERIES_NUMBER
order by t1.GOODS_NAME

RETURN
GO

/*
exec REPEX_REJECTED_GOODS N'
<XML>
	<MNAME>4</MNAME>
	<MSER>4</MSER>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<ID_STORE>152</ID_STORE>
</XML>'*/
 