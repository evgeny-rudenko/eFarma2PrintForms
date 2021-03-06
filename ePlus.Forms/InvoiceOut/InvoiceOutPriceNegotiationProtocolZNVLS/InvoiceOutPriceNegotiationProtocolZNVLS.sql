SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_OUT_PRICE_NEGOTIATION_PROTOCOL_ZNVLS') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_OUT_PRICE_NEGOTIATION_PROTOCOL_ZNVLS AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_OUT_PRICE_NEGOTIATION_PROTOCOL_ZNVLS
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	FR_NAME = CASE WHEN ISNULL(C_FR.FULL_NAME, '') = '' THEN C_FR.NAME ELSE C_FR.FULL_NAME END,
	TO_NAME = CASE WHEN ISNULL(C_TO.FULL_NAME, '') = '' THEN C_TO.NAME ELSE C_TO.FULL_NAME END,
	DOC_NUM = I.DOC_NUM,
	DOC_DATE = CONVERT(VARCHAR, I.DOC_DATE, 104)
FROM INVOICE_OUT I
	INNER JOIN STORE S ON I.ID_STORE = S.ID_STORE
	INNER JOIN CONTRACTOR C_TO ON C_TO.ID_CONTRACTOR = I.ID_CONTRACTOR_TO
	INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE i.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
	
SELECT DISTINCT
	GOODS_NAME = G.NAME, 
	SERIES_NAME = S.SERIES_NUMBER,
	PRODUCER_NAME = P.NAME,
	REGISTER_PRICE = L.REGISTER_PRICE,
	--6
	--7
	PRICE_PROD = L.PRICE_PROD,
	SUP_ADPRICE = L.ADPRICE_SUP,
	SUM_ADPRICE_SUP = L.PRICE_PROD * L.ADPRICE_SUP / 100,
	PRICE_SUP = L.PRICE_SUP - L.PVAT_SUP, 
	L.VAT_SUP, 
	PRICE_SUP_NDS = L.PRICE_SUP, 
	II.ADPRICE_SAL, 
	SUM_ADPRICE_SAL = II.PRICE_SAL - L.PRICE_SUP, 
	PRICE_SAL = II.PRICE_SAL - II.PVAT_SAL, 
	PRICE_SAL_NDS = II.PRICE_SAL
FROM INVOICE_OUT_ITEM II
	INNER JOIN INVOICE_OUT I ON II.ID_INVOICE_OUT_GLOBAL = I.ID_INVOICE_OUT_GLOBAL
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	LEFT JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	LEFT JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES	
WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL AND 
	I.STATE IN ('PROC', 'SAVE') AND G.IMPORTANT = 1
ORDER BY G.NAME

RETURN 0
GO

--exec DBO.REPEX_INVOICE_OUT_PRICE_NEGOTIATION_PROTOCOL_ZNVLS N'<XML><ID_GLOBAL>59367F51-CCB4-4F01-87CE-566DFD4A3BD5</ID_GLOBAL></XML>'


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InvoiceOutPriceNegotiationProtocolZNVLS.InvoiceOutPriceNegotiationProtocolZNVLS'