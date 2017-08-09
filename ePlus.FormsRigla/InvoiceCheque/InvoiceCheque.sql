SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_INVOICE_CHEQUE_RIGLA') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_INVOICE_CHEQUE_RIGLA AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_CHEQUE_RIGLA
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT 
	DOC = I.MNEMOCODE + ' от ' + CONVERT(VARCHAR, I.DATE, 104),
	CONTRACTOR_FROM = FR.NAME,	
	ADDRESS_FROM = FR.ADDRESS,
	FULL_FROM = FR.FULL_NAME,
	CONTRACTOR_TO = CT.NAME,
	PHONE_TO = CT.PHONE,
	ADDRESS_TO = CT.ADDRESS 
FROM INVOICE_OUT I
	INNER JOIN CONTRACTOR CT ON I.ID_CONTRACTOR_TO = CT.ID_CONTRACTOR
	INNER JOIN STORE ST ON ST.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR FR ON FR.ID_CONTRACTOR = ST.ID_CONTRACTOR
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL

SELECT
	GOODS_NAME = G.NAME,
	BARCODE = L.INTERNAL_BARCODE,
	QUANTITY = II.QUANTITY,
	PRICE_SAL = II.PRICE_SAL
FROM INVOICE_OUT_ITEM II
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

RETURN
GO

--exec DBO.REPEX_INVOICE_CHEQUE_RIGLA '<XML><ID_GLOBAL>0564BB00-61F8-42EC-AB19-E4DA3B72F3B0</ID_GLOBAL></XML>'


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InvoiceOutCheque_Rigla.InvoiceOutAP16'