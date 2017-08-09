SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_SALE_GOODS_BY_GROUP') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_SALE_GOODS_BY_GROUP AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_SALE_GOODS_BY_GROUP
    @XMLPARAM NTEXT AS

DECLARE @ALL_CONTRACTORS BIT
DECLARE @ALL_GOODS_GROUP BIT

DECLARE @HDOC INT

DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @DATE_FR_BEFORE DATETIME
DECLARE @DATE_TO_BEFORE DATETIME


EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_CONTRACTORS = 1

SELECT * INTO #GOODS_GROUP FROM OPENXML(@HDOC, '//ID_GOODS_GROUP') WITH(ID_GOODS_GROUP BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_GOODS_GROUP = 1
--SELECT * FROM #CONTRACTORS
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

SET @DATE_FR_BEFORE = DATEADD(M, -1, @DATE_FR)
SET @DATE_TO_BEFORE = @DATE_FR
EXEC DBO.USP_RANGE_NORM @DATE_FR_BEFORE OUT, @DATE_TO_BEFORE OUT
EXEC DBO.REP_RANGEDAY @DATE_FR_BEFORE OUT, @DATE_TO_BEFORE OUT

SELECT
	DISTINCT ID_GOODS = G2G.ID_GOODS
INTO #TEMP_GOODS
FROM  GOODS_GROUP GG
INNER JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP
--INNER JOIN GOODS G ON G.ID_GOODS = G2G.ID_GOODS
WHERE GG.ID_GOODS_GROUP IN (SELECT ID_GOODS_GROUP FROM #GOODS_GROUP)

--------------------------------------------------------------------------
SELECT
	CH.ID_CHEQUE_GLOBAL,
	CH.CHEQUE_TYPE
INTO #TEMP_T
FROM CHEQUE CH
WHERE CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO 
	AND CH.CHEQUE_TYPE IN ('SALE', 'RETURN')
	AND EXISTS (SELECT * FROM CHEQUE_ITEM WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND ID_GOODS IN (SELECT ID_GOODS FROM #TEMP_GOODS))

SELECT
	CH.ID_CHEQUE_GLOBAL,
	CH.CHEQUE_TYPE
INTO #TEMP_T_BEFORE
FROM CHEQUE CH
WHERE CH.DATE_CHEQUE BETWEEN @DATE_FR_BEFORE AND @DATE_TO_BEFORE 
	AND CH.CHEQUE_TYPE IN ('SALE', 'RETURN')
	AND EXISTS (SELECT * FROM CHEQUE_ITEM WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND ID_GOODS IN (SELECT ID_GOODS FROM #TEMP_GOODS))
	
SELECT
	CH.ID_CHEQUE_GLOBAL,
	CH.CHEQUE_TYPE
INTO #TEMP_T_ALL
FROM CHEQUE CH
WHERE  CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO  
	AND CH.CHEQUE_TYPE IN ('SALE', 'RETURN')
	AND EXISTS (SELECT * FROM CHEQUE_ITEM WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL /* AND ID_GOODS IN (SELECT ID_GOODS FROM #TEMP_GOODS) */)
--------------------------------------------------------------------------
--select * from #TEMP_T_ALL
--select * from #TEMP_T
SELECT
	CONTRACTOR_NAME = CT.NAME,
	STORE_NAME = ST.NAME,
	USER_FULL_NAME = MU.FULL_NAME,
	--COUNT_CHEQUE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END),
	QUANTITY = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.QUANTITY),
	PRICE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM),
	ST.ID_STORE
	--ID_CONTRACTOR = ST.ID_CONTRACTOR
INTO #TEMP_T2
FROM #TEMP_T T
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = CHI.ID_GOODS
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR CT ON ST.ID_CONTRACTOR = CT.ID_CONTRACTOR
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	INNER JOIN META_USER MU ON CH.ID_USER_DATA = MU.USER_NUM
WHERE CHI.ID_GOODS IN (SELECT ID_GOODS FROM #TEMP_GOODS)
	--AND (@ALL_STORES = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORES))
	AND (@ALL_CONTRACTORS = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
GROUP BY MU.FULL_NAME,ST.NAME,ST.ID_STORE, CT.ID_CONTRACTOR, CT.NAME


SELECT
	CONTRACTOR_NAME = CT.NAME,
	STORE_NAME = ST.NAME,
	--MU.FULL_NAME,
	--COUNT_CHEQUE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END),
	--QUANTITY = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.QUANTITY),
	PRICE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM),
	ST.ID_STORE
	--ID_CONTRACTOR = ST.ID_CONTRACTOR
INTO #TEMP_T2_BEFORE
FROM #TEMP_T_BEFORE T
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = CHI.ID_GOODS
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR CT ON ST.ID_CONTRACTOR = CT.ID_CONTRACTOR
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	--INNER JOIN META_USER MU ON CH.ID_USER_DATA = MU.USER_NUM
WHERE CHI.ID_GOODS IN (SELECT ID_GOODS FROM #TEMP_GOODS)
	--AND (@ALL_STORES = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORES))
	AND (@ALL_CONTRACTORS = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
GROUP BY /*MU.FULL_NAME,*/ST.NAME,ST.ID_STORE, CT.ID_CONTRACTOR, CT.NAME





SELECT
	CONTRACTOR_NAME = CT.NAME,
	STORE_NAME = ST.NAME,
	--MU.FULL_NAME,
	--COUNT_CHEQUE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END),
	--QUANTITY = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.QUANTITY),
	PRICE = SUM(CASE WHEN T.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM),
	ST.ID_STORE
	--ID_CONTRACTOR = ST.ID_CONTRACTOR
INTO #TEMP_T2_ALL
FROM #TEMP_T_ALL T
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = CHI.ID_GOODS
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR CT ON ST.ID_CONTRACTOR = CT.ID_CONTRACTOR
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = T.ID_CHEQUE_GLOBAL
	--INNER JOIN META_USER MU ON CH.ID_USER_DATA = MU.USER_NUM
WHERE (@ALL_CONTRACTORS = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
GROUP BY /*MU.FULL_NAME,*/ST.NAME,ST.ID_STORE, CT.ID_CONTRACTOR, CT.NAME

--SELECT * FROM #TEMP_T2_ALL

SELECT	* FROM #TEMP_T2 ORDER BY CONTRACTOR_NAME

--SELECT	* FROM #TEMP_T2_BEFORE 

SELECT CONTRACTOR_NAME=ISNULL(T2B.CONTRACTOR_NAME, T2.CONTRACTOR_NAME)
		,STORE_NAME=ISNULL(T2B.STORE_NAME, T2.STORE_NAME)
		,PRICE_BEFORE  = ISNULL(T2B.PRICE,0)
		,PRICE = ISNULL(T2.PRICE,0)
		,PERC = CASE WHEN ISNULL(T2.PRICE,0)<>0 AND ISNULL(T2B.PRICE,0)<>0 THEN ((T2.PRICE / T2B.PRICE)-1)*100. ELSE NULL END
FROM #TEMP_T2_BEFORE T2B
RIGHT JOIN (
			SELECT
				 CONTRACTOR_NAME
				 ,STORE_NAME
				 ,PRICE = SUM(PRICE)
				 ,ID_STORE = ID_STORE--+1
			 FROM
			  #TEMP_T2
			  GROUP BY ID_STORE
						,CONTRACTOR_NAME
						,STORE_NAME
			  )  T2 ON T2.ID_STORE = T2B.ID_STORE
			  
SELECT
		CONTRACTOR_NAME
		,STORE_NAME
		,QUANTITY = SUM(QUANTITY)
FROM #TEMP_T2
GROUP BY  CONTRACTOR_NAME
			,STORE_NAME
			  
SELECT
		CONTRACTOR_NAME
		,STORE_NAME
		,PRICE = SUM(PRICE)
		,[PLAN] =''
		,PERC = ''
FROM #TEMP_T2
GROUP BY  CONTRACTOR_NAME
			,STORE_NAME


SELECT CONTRACTOR_NAME=ISNULL(T2A.CONTRACTOR_NAME, T2.CONTRACTOR_NAME)
		,STORE_NAME=ISNULL(T2A.STORE_NAME, T2.STORE_NAME)
		,PRICE_ALL  = ISNULL(T2A.PRICE,0)
		,PRICE = ISNULL(T2.PRICE,0)
		,PERC = CASE WHEN ISNULL(T2.PRICE,0)<>0 AND ISNULL(T2A.PRICE,0)<>0 THEN (T2.PRICE / T2A.PRICE)*100. ELSE NULL END
FROM #TEMP_T2_ALL T2A
RIGHT JOIN (SELECT
					CONTRACTOR_NAME
					,STORE_NAME
					,PRICE = SUM(PRICE)
					,ID_STORE = ID_STORE--+1
			 FROM #TEMP_T2
			  GROUP BY ID_STORE
						,CONTRACTOR_NAME
						,STORE_NAME
			 )  T2 ON T2.ID_STORE = T2A.ID_STORE
RETURN 0
GO
/*
EXEC REPEX_SALE_GOODS_BY_GROUP N'
<XML>
	<DATE_FR>2010-08-01T00:00:00.000</DATE_FR>
	<DATE_TO>2011-08-30T00:00:00.000</DATE_TO>
	

	<ID_GOODS_GROUP>71</ID_GOODS_GROUP>

</XML>'
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'SaleGoodsByGroup.SaleGoodsByGroupParams'