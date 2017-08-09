SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_DEFECT_OPERATIONS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_DEFECT_OPERATIONS AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_DEFECT_OPERATIONS
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @MNAME INT
DECLARE @MSER INT

DECLARE @ALL_CONTRACTOR BIT
DECLARE @ALL_STORE BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@MNAME = MNAME, 
	@MSER = MSER
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	MNAME INT 'MNAME',
	MSER INT 'MSER'
)

SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1

SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') 
WITH(ID_STORE BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_STORE = 1;

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT

declare @sql nvarchar(4000)

declare @join_filter varchar(150)
set @join_filter = 'LEFT(t1.GOODS_NAME, ' + CAST(@MNAME AS VARCHAR) + ') = LEFT(GD.DRUG_TXT, ' + CAST(@MNAME AS VARCHAR) +') AND LEFT(t1.SERIES_NUMBER, ' + CAST(@MSER AS VARCHAR) + ') = LEFT(GD.SERIES_NUMBER, ' + CAST(@MSER AS VARCHAR) + ')'
--select @join_filter

declare @contr_filter varchar(100)
if (@ALL_CONTRACTOR = 1) set @contr_filter = '' else set @contr_filter = ' AND S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)'
--select @contr_filter

declare @store_filter varchar(100)
if (@ALL_STORE = 1) set @store_filter = '' else set @store_filter = ' AND S.ID_STORE IN (SELECT ID_STORE FROM #STORE)'

--select @join_filter, @contr_filter, @store_filter
--return

set @sql = 
'
select
	DOC_NUM = LETTER_NR + CASE WHEN LETTER_DATE IS NULL THEN '''' ELSE '' от '' + CONVERT(VARCHAR, LETTER_DATE, 104) END,
	GOODS_NAME = gd.drug_txt,
	PRODUCER_NAME = mnf_nm,
	SERIES_NUMBER = gd.series_number,
	QUANTITY_REM = isnull(t1.quantity_rem, 0)
from GOODS_DEFECT GD
	left JOIN (select GOODS_NAME = g.name,
		 PRODUCER_NAME = pr.name,
		 SERIES_NUMBER = SER.SERIES_NUMBER,
		 QUANTITY_REM = SUM(lm.QUANTITY_ADD - lm.QUANTITY_SUB)
		 from lot l 
	inner join LOT_MOVEMENT LM ON LM.id_LOT_GLOBAL = L.Id_lot_global	
	inner JOIN STORE S ON S.ID_STORE = L.ID_STORE
	inner JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	inner JOIN PRODUCER PR ON PR.ID_PRODUCER = G.ID_PRODUCER
	inner JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
where L.QUANTITY_REM > 0 ' + @contr_filter + @store_filter + ' group by g.name, pr.name, SER.SERIES_NUMBER ) as t1 on ' + @join_filter + ' where LETTER_DATE BETWEEN ''' + CONVERT(VARCHAR, @DATE_FR, 112) + ''' AND ''' + CONVERT(VARCHAR, @DATE_TO, 112) + '''' + ' order by LETTER_DATE'

--select @sql
--return 

exec sp_executesql @sql

RETURN
GO

/*
exec REPEX_DEFECT_OPERATIONS N'
<XML>
	<DATE_FR>2010-04-01T00:00:00.000</DATE_FR>
	<DATE_TO>2010-04-21T00:00:00.000</DATE_TO>	
	<MNAME>4</MNAME>
	<MSER>4</MSER>
</XML>'*/




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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'DefectOperations.DefectOperationsParams'