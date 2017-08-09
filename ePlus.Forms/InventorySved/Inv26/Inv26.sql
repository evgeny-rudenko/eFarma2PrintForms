SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INV26') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INV26 AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INV26
	@XMLPARAM NTEXT AS

DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT @ID_GLOBAL = ID_GLOBAL
FROM OPENXML(@HDOC , '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	CONTRACTOR = CASE WHEN ISNULL(C.FULL_NAME, '') = '' THEN C.NAME ELSE C.FULL_NAME END,
	STORE = S.NAME,
	DOC_NUM = I.DOC_NUM,
	DOC_DATE = CONVERT(VARCHAR(10), I.DOC_DATE, 104)
FROM INVENTORY_SVED I
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_GLOBAL = @ID_GLOBAL

SELECT 
    GOODSNAME = G.NAME --+ ' ('+P.NAME+')'
    ,SUM_ADD = CASE WHEN ISNULL(SUM_SAL, 0) > 0 THEN SUM_SAL ELSE 0 END
    ,SUM_SUB = CASE WHEN ISNULL(SUM_SAL, 0) < 0 THEN ABS(SUM_SAL) ELSE 0 END
FROM 
    (
    SELECT ID_GOODS    
        ,SUM_SAL = SUM(SIGN(LM.QUANTITY_ADD-LM.QUANTITY_SUB)*SUM_ACC)
    FROM LOT_MOVEMENT LM(NOLOCK)
    INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
    WHERE LM.ID_DOCUMENT = @ID_GLOBAL
    GROUP BY ID_GOODS) t
INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = t.ID_GOODS
INNER JOIN PRODUCER P(NOLOCK)ON P.ID_PRODUCER = G.ID_PRODUCER
WHERE SUM_SAL <> 0

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

/*
exec REPEX_INV26 N'<XML><ID_GLOBAL>F4D118DA-2163-4752-B675-E26854F158F8</ID_GLOBAL></XML>'

exec REPEX_INV26 N'<XML><ID_GLOBAL>A2E341E0-8254-4C1B-8677-FB4CC9991EFF</ID_GLOBAL></XML>'

exec REPEX_INV26 N'<XML><ID_GLOBAL>01B51E8F-C38E-4B12-AEEE-87F2DD932CA9</ID_GLOBAL></XML>'
*/
--exec REPEX_INV26 N'<XML><ID_GLOBAL>01B51E8F-C38E-4B12-AEEE-87F2DD932CA9</ID_GLOBAL></XML>'

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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'Inv26.Inv26'