SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_STORE_LIFE_CONTROL') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_STORE_LIFE_CONTROL AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_STORE_LIFE_CONTROL
	@XMLPARAM NTEXT AS 

DECLARE @HDOC INT
DECLARE @DAYS INT
DECLARE @ALL_STORES BIT, @ALL_CONTRACTORS BIT, @ALL_GOODS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT @DAYS = DAYS	FROM OPENXML(@HDOC, '/XML') WITH(DAYS INT 'DAYS')

SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_STORES = 1
	
SELECT * INTO #GOODS FROM OPENXML(@HDOC, '//ID_GOODS') WITH(ID_GOODS BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_GOODS = 1
	
SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTORS = 1

			
EXEC SP_XML_REMOVEDOCUMENT @HDOC

DECLARE @DATE_NOW DATETIME
SET @DATE_NOW = getdate()

SELECT
	GOODS_CODE = G.CODE,
	GOODS_NAME = G.NAME,
	STORE_NAME = S.NAME,
	DOC_NUM = L.DOC_NUM,
	DOC_DATE = L.DOC_DATE,
	DATE_TO = L.DOC_DATE + @DAYS,
	DAYS_OFF = DATEDIFF(day, (L.DOC_DATE + @DAYS), @DATE_NOW)
	,QUANTITY_REM = L.QUANTITY_REM
	,QUANTITY_ADD = L.QUANTITY_ADD
	,PRICE_SAL = PRICE_SAL
	,SUM_SAL = L.QUANTITY_REM * PRICE_SAL
	,SUPPLIER = C.NAME
	,IMPORTANT = CASE G.IMPORTANT WHEN 0 THEN 'нет' ELSE 'да' END
	,BEST_BEFORE = SR.BEST_BEFORE
		--select *
FROM LOT L
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = L.ID_SUPPLIER
	LEFT JOIN SERIES SR ON SR.ID_SERIES = L.ID_SERIES
WHERE DATEDIFF(day, (L.DOC_DATE + @DAYS), @DATE_NOW) > 0
	AND L.QUANTITY_REM > 0
	AND (@ALL_STORES = 1 OR (L.ID_STORE IN (SELECT * FROM #STORE)))
	AND (@ALL_GOODS = 1 OR (L.ID_GOODS IN (SELECT * FROM #GOODS)))
	AND (@ALL_CONTRACTORS = 1 OR (L.ID_SUPPLIER IN (SELECT * FROM #CONTRACTORS)))
ORDER BY G.NAME	

RETURN 0
GO
/*

EXEC REPEX_STORE_LIFE_CONTROL N'
<XML>
	<DAYS>14</DAYS>
</XML>'


SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'StoreLifeControl.StoreLifeControlParams'