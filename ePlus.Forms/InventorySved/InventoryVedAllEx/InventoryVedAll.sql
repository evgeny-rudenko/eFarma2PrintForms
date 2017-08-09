SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVENTORY_VED_ALL') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVENTORY_VED_ALL AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVENTORY_VED_ALL
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT, @ID_INVENTORY_GLOBAL UNIQUEIDENTIFIER, @ID_STORE BIGINT, @DOC_DATE DATETIME, @FULL BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INVENTORY_GLOBAL = ID_INVENTORY_GLOBAL FROM OPENXML(@HDOC , '/XML') WITH(
        ID_INVENTORY_GLOBAL UNIQUEIDENTIFIER 'ID_INVENTORY_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT 
	@ID_STORE = ID_STORE,
	@FULL = [FULL],
	@DOC_DATE = DOC_DATE
FROM INVENTORY_SVED I
WHERE ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

SELECT
    DOC_NAME = '������������������ ��������� � ' + RTRIM(I.DOC_NUM) + ' �� ' + CONVERT(VARCHAR , I.DOC_DATE , 104),
    STORE_NAME = S.NAME,
    CONTRACTOR_NAME = C.NAME
FROM INVENTORY_SVED I
    INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

DECLARE @TEMP_T TABLE
(
	GOODS_NAME VARCHAR(255),
	GOODS_CODE VARCHAR(40),
	SERIAL_NUMBER VARCHAR(2061),
	PRICE_SUP MONEY,
	PRICE_SAL MONEY,
	QUANTITY_REM MONEY,
	QUANTITY MONEY,
	SCALING_RATIO_NAME VARCHAR(162),
	PVAT_SAL MONEY
)

INSERT INTO @TEMP_T
SELECT
	GOODS_NAME = GOODS_NAME,
	GOODS_CODE = GOODS_MNEMOCODE,
	SERIAL_NUMBER = SERIES,
	PRICE_SUP = PRICE_SUP,
	PRICE_SAL = PRICE_SAL,
	QUANTITY_REM = QUANTITY_REM,
	QUANTITY = QUANTITY,
	SCALING_RATIO_NAME = SCALING_RATIO_NAME,
	PVAT_SAL = PVAT_SAL
FROM MV_INVENTORY_VED_ITEM IVI
	INNER JOIN INVENTORY_VED IV ON IV.ID_INVENTORY_VED_GLOBAL = IVI.ID_INVENTORY_VED_GLOBAL
	INNER JOIN INVENTORY_SVED I ON I.ID_INVENTORY_GLOBAL = IV.ID_INVENTORY_SVED_GLOBAL
WHERE I.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

IF (@FULL = 1)
BEGIN
INSERT INTO @TEMP_T
SELECT
	GOODS_NAME = G.NAME,
	GOODS_CODE = G.MNEMOCODE,
	SERIAL_NUMBER = ISNULL(S.SERIES_NUMBER + ' / ', ' /') + ISNULL(CONVERT(VARCHAR(10), S.BEST_BEFORE,104), ''),
	PRICE_SUP = L.PRICE_SUP,
	PRICE_SAL = L.PRICE_SAL,
	QUANTITY_REM = (SELECT SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB) FROM LOT_MOVEMENT LM WHERE LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL AND LM.DATE_OP < @DOC_DATE),
	QUANTITY = 0,
	SCALING_RATIO_NAME = CONVERT(VARCHAR, NUMERATOR) + '/' + CONVERT(VARCHAR, DENOMINATOR) + ' ' + U.NAME,
	PVAT_SAL = L.PVAT_SAL
FROM LOT L
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
WHERE L.ID_STORE = @ID_STORE AND
	NOT EXISTS(
        SELECT TOP 1 1
        FROM INVENTORY_VED_ITEM IVI
            INNER JOIN INVENTORY_VED IV ON IV.ID_INVENTORY_VED_GLOBAL = IVI.ID_INVENTORY_VED_GLOBAL
            INNER JOIN INVENTORY_SVED I ON I.ID_INVENTORY_GLOBAL = IV.ID_INVENTORY_SVED_GLOBAL
        WHERE I.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL AND IVI.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
    ) AND (SELECT SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB) FROM LOT_MOVEMENT LM WHERE LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL AND LM.DATE_OP < @DOC_DATE) <> 0
END

SELECT T.*
		, SUM_SUP=(PRICE_SUP*(QUANTITY-QUANTITY_REM))
		, SUM_SAL=(PRICE_SAL*(QUANTITY-QUANTITY_REM))
		, SVAT_SUP=(PVAT_SAL*(QUANTITY-QUANTITY_REM))
 FROM @TEMP_T T
ORDER BY GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

--exec REPEX_INVENTORY_VED_ALL N'<XML><ID_INVENTORY_GLOBAL>6CF24A4A-0283-4A6E-9797-A7DB2FB406CD</ID_INVENTORY_GLOBAL></XML>'


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InventoryVedAllEx.InventoryVedAllEx'
