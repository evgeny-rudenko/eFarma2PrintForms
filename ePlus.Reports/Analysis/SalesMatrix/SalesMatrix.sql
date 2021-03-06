SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID(N'dbo.FN_REPEX_SALES_MATRIX_HELPER', N'FN') IS NOT NULL 
DROP FUNCTION dbo.FN_REPEX_SALES_MATRIX_HELPER
GO

CREATE FUNCTION dbo.FN_REPEX_SALES_MATRIX_HELPER
    (@ID_GOODS BIGINT)
RETURNS VARCHAR(128)
AS
BEGIN

DECLARE @RESULT VARCHAR(128)

DECLARE @COUNT_GROUPS INT
SELECT @COUNT_GROUPS = COUNT(*) FROM GOODS_2_GROUP WHERE ID_GOODS = @ID_GOODS AND DATE_DELETED IS NULL

IF (@COUNT_GROUPS = 0) 
BEGIN
	SET @RESULT = ''
END
ELSE IF (@COUNT_GROUPS = 1)
BEGIN
	SELECT @RESULT = NAME 
	FROM GOODS_GROUP GG
		INNER JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP 
	WHERE G2G.ID_GOODS = @ID_GOODS  AND GG.DATE_DELETED IS NULL
END
ELSE
BEGIN
	SELECT @RESULT = NAME
	FROM GOODS_GROUP GG
		INNER JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP
	WHERE G2G.ID_GOODS = @ID_GOODS  AND GG.DATE_DELETED IS NULL AND GG.ID_PARENT_GROUP IS NOT NULL
		AND GG.ID_GOODS_GROUP = (SELECT MAX(GG.ID_GOODS_GROUP) FROM GOODS_GROUP GG INNER JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP WHERE G2G.ID_GOODS = @ID_GOODS  AND GG.DATE_DELETED IS NULL)
END

IF (@RESULT IS NULL)
BEGIN
	SELECT @RESULT = MAX(NAME)
	FROM GOODS_GROUP GG
		INNER JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP
	WHERE G2G.ID_GOODS = @ID_GOODS  AND GG.DATE_DELETED IS NULL
END

RETURN @RESULT

END
GO

IF OBJECT_ID('DBO.REPEX_SALES_MATRIX') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_SALES_MATRIX AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_SALES_MATRIX
    @XMLPARAM NTEXT AS
        
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ALL_CONTRACTORS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM OUT
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC , '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO' 
)

SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 
	SET @ALL_CONTRACTORS = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_DAYS	@DATE_FR OUTPUT, @DATE_TO OUTPUT
EXEC DBO.USP_RANGE_NORM	@DATE_FR OUTPUT, @DATE_TO OUTPUT

--select @date_fr, @date_to

SELECT
	CONTRACTOR_NAME = CT.NAME,
	GOODS_CODE = G.CODE,
	GOODS_NAME = G.NAME,
	GOODS_GROUP = dbo.FN_REPEX_SALES_MATRIX_HELPER(G.ID_GOODS),
	SR_NAME = CAST(SR.NUMERATOR AS VARCHAR(5)) + '/' + CAST(SR.DENOMINATOR AS VARCHAR(5)) + ' ' + U.NAME,
	VAT_RATE = TT.TAX_RATE,
	PRICE_SUP = L.PRICE_SUP,
	INVOICE_DATE = L.INCOMING_DATE,
	SUPPLIER_NAME = SUP.NAME,
	SELL_DATE = CH.DATE_CHEQUE,
	PRICE_SAL = L.PRICE_SAL - CHI.SUMM_DISCOUNT / CHI.QUANTITY,
	QUANTITY = CHI.QUANTITY,
	SUM_SAL = CHI.SUMM
FROM CHEQUE CH
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR = ST.ID_CONTRACTOR
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
	INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = L.ID_SUPPLIER
	LEFT JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
WHERE CH.CHEQUE_TYPE = 'SALE'
	AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
	AND (@ALL_CONTRACTORS = 1 OR (CT.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS)))
ORDER BY CONTRACTOR_NAME, GOODS_NAME

RETURN 0
GO

/*
EXEC REPEX_SALES_MATRIX N'
<XML>
	<DATE_FR>2010-01-01T17:23:28.031</DATE_FR>
	<DATE_TO>2010-02-25T17:23:28.031</DATE_TO>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<ID_CONTRACTOR>5273</ID_CONTRACTOR>
</XML>'*/


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'SalesMatrix.SalesMatrixParams'