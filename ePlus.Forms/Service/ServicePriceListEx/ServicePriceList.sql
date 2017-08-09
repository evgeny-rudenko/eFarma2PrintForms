SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_SERVICE_PRICE_LIST') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_SERVICE_PRICE_LIST AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_SERVICE_PRICE_LIST
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT, @ALL_ITEMS BIT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT		
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC , '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')		

SELECT SPLI
INTO #ITEMS 
    FROM OPENXML(@HDOC, '//SPLI') WITH(SPLI UNIQUEIDENTIFIER '.') TAB
--select * from #ITEMS
IF(@@ROWCOUNT=0)  
	SET @ALL_ITEMS = 1
--select @ALL_ITEMS
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT 
	CONTRACTOR = C.[NAME],
	STORE = NULL
	--select *
FROM SERVICE_PRICE_LIST AS PL
	INNER JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = PL.ID_CONTRACTOR
WHERE PL.ID_SERVICE_PRICE_LIST = @ID_GLOBAL

SELECT
	SERVICE_NAME = S.NAME,
	QUANTITY = PLIC.QUANTITY,
	PRICE_SAL = PLIC.PRICE_SAL,
	SUM_SAL = PLIC.QUANTITY * PLIC.PRICE_SAL,
	LIST_NAME = PLI.NAME
	,INTERNAL_BARCODE = S4S.INTERNAL_BARCODE
	,DC_IMAGE = Convert(BINARY,null)
	--select *
FROM SERVICE_PRICE_LIST_ITEM AS PLI
	left JOIN SERVICE_PRICE_LIST_ITEM_COMPLEX AS PLIC ON PLIC.ID_SERVICE_PRICE_LIST_ITEM = PLI.ID_SERVICE_PRICE_LIST_ITEM
	inner JOIN SERVICE AS S ON S.ID_SERVICE = PLIC.ID_SERVICE
	inner JOIN SERVICE_4_SALE S4S ON S4S.ID_DOCUMENT_ITEM = PLI.id_Service_price_list_item
WHERE PLI.ID_SERVICE_PRICE_LIST = @ID_GLOBAL  AND S4S.CAN_SALE = 1
and S4S.DATE_DELETED is null
and (@ALL_ITEMS=1 OR PLI.ID_SERVICE_PRICE_LIST_ITEM in (select SPLI from #ITEMS))
ORDER BY LIST_NAME, SERVICE_NAME
	
RETURN 0
GO
/*
exec REPEX_SERVICE_PRICE_LIST N'
<XML>
<ID_GLOBAL>1D349DB2-7E51-4534-9515-85CB56DAB1E7</ID_GLOBAL>
<SPLI>411844C4-4E77-4EAE-BBF9-D5F2433F96A0</SPLI>
<SPLI>411844C4-4E77-4EAE-BBF9-D5F2433F96A0</SPLI>
<SPLI>411844C4-4E77-4EAE-BBF9-D5F2433F96A0</SPLI>
<SPLI>411844C4-4E77-4EAE-BBF9-D5F2433F96A0</SPLI>
</XML>'
*/
/*
select  * from SERVICE_PRICE_LIST_ITEM_COMPLEX
select  * from SERVICE
select  * from SERVICE_4_SALE
*/
/*
select 
	ID_SERVICE_PRICE_LIST_ITEM,
	NAME,
	NAME_FULL,
	PRICE_SAL
FROM SERVICE_PRICE_LIST_ITEM
WHERE 
	ID_SERVICE_PRICE_LIST =
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'ServicePriceListEx.ServicePriceListEx'
