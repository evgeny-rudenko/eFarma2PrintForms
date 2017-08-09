/****** Object:  StoredProcedure [dbo].[USP_EXPORT_SPRAVKA_SST]    Script Date: 10/17/2011 15:53:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('[USP_EXPORT_SPRAVKA_SST]') IS NULL EXEC('CREATE PROCEDURE [USP_EXPORT_SPRAVKA_SST] AS RETURN')
GO
ALTER PROCEDURE [dbo].[USP_EXPORT_SPRAVKA_SST](
    @XMLPARAM NTEXT
) AS

DECLARE @HDOC INT, @ID_CONTRACTOR BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1 @ID_CONTRACTOR = ID_CONTRACTOR FROM OPENXML(@HDOC, '/XML') WITH(ID_CONTRACTOR BIGINT 'ID_CONTRACTOR')	
	SELECT ID_STORE INTO #STORE FROM OPENXML(@HDOC , '/XML/STORE') WITH(ID_STORE BIGINT 'ID_STORE') WHERE ID_STORE != 0
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
    [Код ЦВ ПРОТЕК] = ';',
    [Код ФФИ] = ';',
    [Код ЕГК] = ';',
    [Код Атик] = ';',
    [Код СИА] = ';',
    [Код Инвакорп] = ';',
    [Код Шрея] = ';',
    [Резерв 1] = ';',
    [Резерв 2] = ';',
    [Резерв 3] = ';',
    [Резерв 4] = ';',
    [Наименование препарата] = UPPER(RTRIM(LTRIM(G.NAME))) + ';',
    [Название производителя препарата] = UPPER(RTRIM(LTRIM(MAX(P.NAME)))) + ';',
    [Название страны производителя препарата] = UPPER(RTRIM(LTRIM(MAX(CON.NAME)))) + ';',
    [Цена продажи] = REPLACE(CAST(ROUND(L.PRICE_SAL* SR.DENOMINATOR / convert(money, sr.NUMERATOR), 2) AS VARCHAR(256)), ',', '.') + ';',
    [Резерв 5] = ';',
    [Остаток в аптеке, количество] = CAST(CAST(ROUND(L.QUANTITY_REM * SR.NUMERATOR / convert(money, sr.DENOMINATOR), 0) AS BIGINT) AS VARCHAR(256)) + ';',
    [Количество дней запаса] = ';',
    [Дата выпуска препарата] = ';',
    [Дата истечения срока годности] = ';'
FROM LOT L(NOLOCK)
    INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = L.ID_GOODS
    INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
    LEFT JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER
    LEFT JOIN COUNTRY CON(NOLOCK) ON CON.ID_COUNTRY = P.ID_COUNTRY
WHERE L.QUANTITY_REM > 0
    AND L.ID_STORE IN (SELECT ID_STORE FROM #STORE)
GROUP BY L.ID_LOT, G.ID_GOODS, G.NAME, L.QUANTITY_REM, L.PRICE_SAL, L.PRICE_PROD, SR.NUMERATOR, SR.DENOMINATOR
ORDER BY G.NAME

RETURN 0
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'ProtekExport.ProtekExportParams'