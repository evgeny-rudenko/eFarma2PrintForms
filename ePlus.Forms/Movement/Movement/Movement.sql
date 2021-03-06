SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_MOVEMENT') IS NULL EXEC('CREATE PROCEDURE REPEX_MOVEMENT AS RETURN')
GO
ALTER PROCEDURE REPEX_MOVEMENT
	(@XMLPARAM NTEXT) AS 

DECLARE @HDOC INT, @ID_MOVEMENT BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_MOVEMENT = ID_MOVEMENT
	FROM OPENXML(@HDOC, '/XML') WITH(ID_MOVEMENT BIGINT 'ID_MOVEMENT')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	ISNULL(Full_NAME,NAME) AS COMPANY
FROM CONTRACTOR WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()

SELECT
	MNEMOCODE = ISNULL(MNEMOCODE + ' ','') + ISNULL(COMMENT,''),
	DATE,
	STORENAMEFROM = (SELECT TOP 1 ISNULL(C.FULL_NAME+ ' - ',C.NAME+ ' - ') + S.NAME 
					FROM STORE AS S  
					LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
					WHERE S.ID_STORE = M.ID_STORE_FROM),
	STORENAMETO = (SELECT TOP 1 ISNULL(C.FULL_NAME+ ' - ', C.NAME+ ' - ') + S.NAME 
					FROM STORE AS S 
					LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR 
					WHERE S.ID_STORE = M.ID_STORE_TO),
	DOC = ISNULL( CASE WHEN ISNULL(M.BASE_DOCUMENT_NAME,'')='' THEN NULL ELSE M.BASE_DOCUMENT_NAME END,(
										SELECT (SELECT TOP 1 DESCRIPTION FROM TABLE_DATA TD WHERE TD.ID_TABLE_DATA = AD.ID_TABLE)+' '+ISNULL(DOC_NUM,'') + ISNULL(' �� '+ CONVERT(VARCHAR(10),DOC_DATE,104),'')
										FROM ALL_DOCUMENT AD 
										WHERE AD.ID_DOCUMENT_GLOBAL =M.ID_DOC_BASE_GLOBAL
										)
				)
FROM MOVEMENT AS M WHERE ID_MOVEMENT = @ID_MOVEMENT

SELECT
	GOODSNAME = ISNULL(G.NAME,'') + ISNULL(' ( ' + (SELECT TOP 1 P.NAME FROM PRODUCER AS P WHERE G.ID_PRODUCER = P.ID_PRODUCER) + ' ) ',''),
	SERIESNUMBER = SER.SERIES_NUMBER,
	QUANTITY = M.QUANTITY,
	UNITNAME = DBO.FN_SCALE_NAME(M.ID_SCALING_RATIO),
	PRICE_SALE,
	PRICE_SUMM_SALE = QUANTITY * PRICE_SALE,
	PRICE_SUPPLIER,
	PRICE_SUMMA = QUANTITY * PRICE_SUPPLIER,
	PRICE_SUPPLIER_VAT,
	PRICE_SUMMA_VAT = QUANTITY * PRICE_SUPPLIER_VAT
	--select * 
FROM MOVEMENT_ITEM AS M
	INNER JOIN LOT L ON L.ID_LOT = M.ID_LOT_FROM
	LEFT JOIN GOODS AS G ON G.ID_GOODS = M.ID_GOODS
	LEFT JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = M.ID_SCALING_RATIO
	LEFT JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT WHERE ID_MOVEMENT = @ID_MOVEMENT
ORDER BY GOODSNAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO

--exec repex_movement '<XML><ID_MOVEMENT>1960</ID_MOVEMENT></XML>'

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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'Movement.Movement'

