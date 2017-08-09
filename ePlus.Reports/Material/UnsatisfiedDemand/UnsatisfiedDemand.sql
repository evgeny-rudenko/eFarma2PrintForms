SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_UNSATISFIED_DEMAND') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_UNSATISFIED_DEMAND AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_UNSATISFIED_DEMAND
    @XMLPARAM NTEXT AS
    
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME

DECLARE @ALL_USER BIT
DECLARE @ALL_CONTRACTOR BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO	
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #ID_STORES FROM OPENXML(@HDOC, '/XML/ID_USERS') WITH(ID_USER UNIQUEIDENTIFIER '.')
IF (@@ROWCOUNT = 0)	SET @ALL_USER = 1

SELECT * INTO #ID_CONTRACTORS FROM OPENXML(@HDOC, '/XML/ID_CONTRACTORS') WITH(ID_CONTRACTOR UNIQUEIDENTIFIER '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CONTRACTOR = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT 
	AU = CASE WHEN C.FULL_NAME IS NULL OR C.FULL_NAME = '' THEN C.NAME
		ELSE C.FULL_NAME
		END,  
	ID, 
	CANCEL_DATE = [DATE], 
	GOODS_NAME, 
	PRODUCER_NAME, 
	QUANTITY, 
	PRICE, 
	BUY_FREQUENCY = CASE ISNULL(UD.BUY_FREQUENCY, 0)
		WHEN 0 THEN 'Не задано'
		WHEN 1 THEN 'Еженедельно'
		WHEN 2 THEN 'Раз в месяц'
		WHEN 3 THEN 'Раз в квартал'
		WHEN 4 THEN 'Раз в полгода'
		WHEN 5 THEN 'Раз в год'
		ELSE 'Не задано'
		END, 
	EMPLOYEE = MU.FULL_NAME
FROM UNSATISFIED_DEMAND UD
INNER JOIN META_USER MU WITH(NOLOCK) ON MU.ID_USER = UD.ID_USER_GLOBAL
INNER JOIN CONTRACTOR C WITH(NOLOCK) ON UD.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL
WHERE 
	UD.[DATE] BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_CONTRACTOR = 1 OR UD.ID_CONTRACTOR_GLOBAL IN (SELECT ID_CONTRACTOR FROM #ID_CONTRACTORS))
	AND (@ALL_USER = 1 OR UD.ID_USER_GLOBAL IN (SELECT ID_USER FROM #ID_STORES))
ORDER BY AU

RETURN 
GO

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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'UnsatisfiedDemand.UnsatisfiedDemandParams'