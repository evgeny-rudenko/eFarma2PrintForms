SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_INVOICE_INVOICE_RIGLA') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_INVOICE_INVOICE_RIGLA AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_INVOICE_RIGLA
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT
DECLARE	@ID_GLOBAL UNIQUEIDENTIFIER
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
SELECT @ID_GLOBAL = ID_GLOBAL FROM OPENXML(@HDOC , '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	[CONTRACTOR_FROM_NAME] = ISNULL(C_FR.FULL_NAME, '') + ISNULL(' (' + C_FR.NAME + ')', ''),  
	[CONTRACTOR_FROM_ADDRESS] = ISNULL(C_FR.ADDRESS, '�� �������'), 
	[CONTRACTOR_FROM_INN] = ISNULL(C_FR.INN, '�� �������') + '/' + ISNULL(C_FR.KPP, '�� �������'), 
	[GCONTRACTOR] = ISNULL(C_FR.NAME, '') + ISNULL(', ' + C_FR.ADDRESS, ''), 
	[GCONTRACTOR_TO_NAME] = ISNULL(ISNULL(C_TO.NAME, C_TO.FULL_NAME),'') + ISNULL(', ' + C_TO.ADDRESS, ''),
	[CONTRACTOR_TO_NAME] = ISNULL(ISNULL(C_P.NAME, C_P.FULL_NAME),''),	
	[CONTRACTOR_TO_ADDRESS] = ISNULL(C_P.ADDRESS, ''),
	[CONTRACTOR_TO_INN] = ISNULL(C_P.INN, '�� �������') + '/' + ISNULL(C_P.KPP, '�� �������'), 
	
	[INVOICE_NAME] = CASE WHEN ISNULL(I.INCOMING_BILL_NUMBER, '') = '' THEN 
		CASE WHEN ISNULL(I.INCOMING_NUMBER, '') = '' THEN I.MNEMOCODE ELSE I.INCOMING_NUMBER END
		ELSE I.INCOMING_BILL_NUMBER END, 
	[INVOICE_DATE] = CASE WHEN ISNULL(I.INCOMING_BILL_DATE, '') = '' THEN 
		CASE WHEN ISNULL(I.INCOMING_DATE, '') = '' THEN I.DOCUMENT_DATE ELSE I.INCOMING_DATE END
		ELSE I.INCOMING_BILL_DATE END, 
	DIR = CASE WHEN ISNULL(C_FR.DIRECTOR_FIO, '') = '' THEN '                                                  ' ELSE C_FR.DIRECTOR_FIO END,
	BUH = CASE WHEN ISNULL(C_FR.BUH_FIO, '') = '' THEN '                                                  ' ELSE C_FR.BUH_FIO END
FROM INVOICE I
INNER JOIN STORE S ON I.ID_STORE = S.ID_STORE
INNER JOIN CONTRACTOR C_TO ON S.ID_CONTRACTOR = C_TO.ID_CONTRACTOR
INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
LEFT JOIN CONTRACTOR C_P ON C_P.ID_CONTRACTOR = I.ID_PAYER
WHERE I.ID_INVOICE_GLOBAL = @ID_GLOBAL

DECLARE @IS_USE_VAT BIGINT
SELECT 
	@IS_USE_VAT = C_FR.USE_VAT
FROM INVOICE I
	INNER JOIN CONTRACTOR C_FR ON C_FR.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
WHERE I.ID_INVOICE_GLOBAL = @ID_GLOBAL

IF (@IS_USE_VAT = 1)
	BEGIN

	SELECT
		[GOODS_NAME] = G.NAME,
		[UNIT_NAME] = U.SHORT_NAME,
		[QUANTITY] = II.QUANTITY,
		[PRICE_SAL] = L.PRICE_SUP - L.PVAT_SUP,
		[SUM_SAL_WITHOUT_VAT] = II.QUANTITY * (L.PRICE_SUP - L.PVAT_SUP),
		[VAT_SAL] = CASE WHEN L.PVAT_SUP = 0 THEN CAST('��� ���' AS VARCHAR(10)) ELSE CAST(L.VAT_SUP AS VARCHAR(9)) + '%' END,
		[PSUM_SAL] = II.QUANTITY * L.PVAT_SUP,
		[SUM_SAL] = II.QUANTITY * L.PRICE_SUP,
		[COUNTRY] = C.NAME,
		GTD_NUMBER = L.GTD_NUMBER
	FROM INVOICE_ITEM II
		INNER JOIN LOT L ON L.ID_DOCUMENT_ITEM = II.ID_INVOICE_ITEM_GLOBAL
		INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
		LEFT JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
		LEFT JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
		INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
		INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	WHERE II.ID_INVOICE_GLOBAL = @ID_GLOBAL
	ORDER BY GOODS_NAME

	END
ELSE
	BEGIN
	SELECT
		[GOODS_NAME] = G.NAME,
		[UNIT_NAME] = U.SHORT_NAME,
		[QUANTITY] = II.QUANTITY,
		[PRICE_SAL] = (L.PRICE_SUP / (100 + ISNULL(TT.TAX_RATE, 0))) * 100,
		[SUM_SAL_WITHOUT_VAT] = (L.PRICE_SUP / (100 + ISNULL(TT.TAX_RATE, 0))) * 100 * II.QUANTITY,
		[VAT_SAL] = CASE WHEN ISNULL(TT.TAX_RATE, 0) = 0 THEN CAST('��� ���' AS VARCHAR(10)) ELSE CAST(ISNULL(TT.TAX_RATE, 0) AS VARCHAR(9)) + '%' END,
		[PSUM_SAL] = II.QUANTITY * ISNULL(TT.TAX_RATE, 0) * L.PRICE_SUP / (100 + ISNULL(TT.TAX_RATE, 0)),
		[SUM_SAL] = II.QUANTITY * L.PRICE_SUP,
		[COUNTRY] = C.NAME,
		GTD_NUMBER = L.GTD_NUMBER
		--select *
	FROM INVOICE_ITEM II
		INNER JOIN LOT L ON L.ID_DOCUMENT_ITEM = II.ID_INVOICE_ITEM_GLOBAL
		INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
		LEFT JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
		LEFT JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
		LEFT JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
		INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
		INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	WHERE II.ID_INVOICE_GLOBAL = @ID_GLOBAL
	ORDER BY GOODS_NAME
	END

RETURN
GO

--EXEC DBO.REPEX_INVOICE_INVOICE_RIGLA N'<XML><ID_GLOBAL>D14FE47D-4E72-4BFE-BEEE-EFBD98F9B1DE</ID_GLOBAL></XML>'





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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InvoiceInvoice.InvoiceInvoiceRigla'