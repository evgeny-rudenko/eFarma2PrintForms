SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO


--IF TYPE_ID('GOODS_GROUPS') IS NULL
--CREATE TYPE GOODS_GROUPS AS TABLE
--( ID_GOODS_GROUP BIGINT )

IF OBJECT_ID('DBO.REPEX_INDIVIDUAL_SALES_HELPER') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INDIVIDUAL_SALES_HELPER AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_INDIVIDUAL_SALES_HELPER
AS

IF (EXISTS (SELECT * FROM REPLICATION_CONFIG WHERE EXPORT_MODEL = 'CENTER'))
BEGIN
	SELECT TOP 1 NAME
	FROM REPLICATION_CONFIG RC
		INNER JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR_GLOBAL = RC.ID_CONTRACTOR_GLOBAL
	WHERE RC.EXPORT_MODEL = 'CENTER'
END
ELSE
BEGIN
	SELECT TOP 1 NAME
	FROM REPLICATION_CONFIG RC
		INNER JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR_GLOBAL = RC.ID_CONTRACTOR_GLOBAL
	WHERE RC.IS_SELF = 1
END

RETURN 0
GO

IF (OBJECT_ID('DBO.FN_INDIVIDUAL_SALES_GET_NAME_GROUP') IS NULL)	EXEC ('CREATE FUNCTION DBO.FN_INDIVIDUAL_SALES_GET_NAME_GROUP() RETURNS VARCHAR(4000) AS BEGIN RETURN CONVERT(VARCHAR(4000), NULL) END')
GO
ALTER FUNCTION DBO.FN_INDIVIDUAL_SALES_GET_NAME_GROUP(
    @ID_GOODS BIGINT, @HDOC INT
)
RETURNS VARCHAR(4000)
AS
BEGIN
    DECLARE @GROUP_NAMES VARCHAR(4000)
    DECLARE @CNT INT
    
    
    DECLARE @GOODS_GROUPS TABLE(ID_GOODS_GROUP BIGINT)
    
    INSERT INTO @GOODS_GROUPS
    SELECT * FROM OPENXML(@HDOC, '//ID_GOODS_GROUP') WITH(ID_GOODS_GROUP BIGINT '.')
       
    SELECT @CNT=COUNT(*) FROM @GOODS_GROUPS
    SET @GROUP_NAMES =''
    
    IF (@CNT<>0)
		
		SELECT @GROUP_NAMES=@GROUP_NAMES+ISNULL(GG.NAME+',','') FROM GOODS_2_GROUP G2G
		INNER JOIN @GOODS_GROUPS GGS ON GGS.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP
		LEFT JOIN GOODS_GROUP GG ON GG.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP
		 WHERE G2G.ID_GOODS = @ID_GOODS AND G2G.DATE_DELETED IS NULL
	ELSE
		SELECT @GROUP_NAMES=@GROUP_NAMES+ISNULL(GG.NAME+',','') FROM GOODS_2_GROUP G2G
		LEFT JOIN GOODS_GROUP GG ON GG.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP
		 WHERE G2G.ID_GOODS = @ID_GOODS AND G2G.DATE_DELETED IS NULL
	
	IF (@GROUP_NAMES = '')
		RETURN '����� ��� ������'
	
	IF (@@ROWCOUNT <> 0)
		RETURN SUBSTRING(@GROUP_NAMES,1,LEN(@GROUP_NAMES)-1)
	RETURN @GROUP_NAMES
END
GO

IF OBJECT_ID('DBO.REPEX_INDIVIDUAL_SALES') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INDIVIDUAL_SALES AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INDIVIDUAL_SALES
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME

DECLARE @ALL_CONTRACTORS BIT
DECLARE @ALL_CASHIERS BIT
DECLARE @ALL_GOODS BIT
DECLARE @ALL_GOODS_GROUPS BIT
DECLARE @CASH BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@CASH = CASH
FROM OPENXML(@HDOC, '/XML')
WITH (DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO', CASH BIT 'CASH')

SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTORS = 1

SELECT * INTO #CASHIERS FROM OPENXML(@HDOC, '//ID_CASHIER') WITH(ID_CASHIER BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CASHIERS = 1

SELECT * INTO #GOODS FROM OPENXML(@HDOC, '//ID_GOODS') WITH(ID_GOODS BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_GOODS = 1

SELECT * INTO #GOODS_GROUPS FROM OPENXML(@HDOC, '//ID_GOODS_GROUP') WITH(ID_GOODS_GROUP BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_GOODS_GROUPS = 1


EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT


SELECT DISTINCT
	G2G.ID_GOODS, 
	G2G.ID_GOODS_GROUP, 
	GG.NAME AS NAME, 
	GG.ID_PARENT_GROUP AS ID_PARENT_GROUP
INTO #GGROUPS	
FROM GOODS_2_GROUP G2G
INNER JOIN GOODS G ON G.ID_GOODS = G2G.ID_GOODS
LEFT JOIN GOODS_GROUP GG ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP
WHERE GG.DATE_DELETED IS NULL
--select * from #GGROUPS
--select @ALL_GOODS_GROUPS
--return

IF @ALL_GOODS_GROUPS IS NULL
BEGIN
	DELETE FROM #GGROUPS
	FROM #GGROUPS G1
	WHERE NOT EXISTS (SELECT ID_GOODS_GROUP FROM #GOODS_GROUPS GG
	WHERE G1.ID_GOODS_GROUP = GG.ID_GOODS_GROUP)
END

DELETE FROM #GGROUPS
FROM #GGROUPS G1
WHERE G1.ID_PARENT_GROUP IS NULL 
AND EXISTS (SELECT ID_PARENT_GROUP FROM #GGROUPS G2
WHERE G1.ID_GOODS = G2.ID_GOODS AND G2.ID_PARENT_GROUP IS NOT NULL)

DELETE FROM #GGROUPS	
FROM #GGROUPS G1
WHERE EXISTS (SELECT ID_PARENT_GROUP FROM #GGROUPS G2
WHERE G1.ID_GOODS = G2.ID_GOODS AND G2.ID_GOODS_GROUP > G1.ID_GOODS_GROUP)

CREATE INDEX I_TTT ON #GGROUPS(ID_GOODS)


SELECT
	C.ID_CONTRACTOR,	
	CONTRACTOR_NAME = C.NAME + CASE WHEN C.A_COD IS NOT NULL THEN ', ' + CAST(C.A_COD AS VARCHAR(10)) ELSE '' END,
	CH.ID_USER_DATA,
	CASHIER_NAME = U.FULL_NAME,	
	GOODS_CODE = G.CODE,
	GOODS_NAME = G.NAME,
	--GOODS_GROUP_NAME = ISNULL(DBO.FN_INDIVIDUAL_SALES_GET_NAME_GROUP(G.ID_GOODS, @GOODS_GROUPS), '����� ��� ������'),--ISNULL(GGR.NAME, '����� ��� ������'),
	GOODS_GROUP_NAME = ISNULL(DBO.FN_INDIVIDUAL_SALES_GET_NAME_GROUP(G.ID_GOODS, @HDOC), '����� ��� ������'),--ISNULL(GGR.NAME, '����� ��� ������'),
	COUNT_CHEQUE = SUM(CASE WHEN CH.CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END),	
	COUNT_QUANTITY = SUM(CASE CH.CHEQUE_TYPE
		WHEN 'SALE' THEN CHI.QUANTITY * CASE WHEN SR.ID_SCALING_RATIO IS NULL THEN 1 ELSE CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR END
		WHEN 'RETURN' THEN -CHI.QUANTITY * CASE WHEN SR.ID_SCALING_RATIO IS NULL THEN 1 ELSE CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR END
	END),
	SUM_SUP = SUM(CASE WHEN CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.QUANTITY * L.PRICE_SUP),
	SUM_SAL = SUM(CASE WHEN CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.QUANTITY * CHI.PRICE),	
	SUM_DIS = SUM(CASE WHEN CHEQUE_TYPE = 'SALE' THEN 1 ELSE -1 END * CHI.SUMM_DISCOUNT),
	SUMM = SUM(case when ch.cheque_type = 'SALE' then 1 else -1 end * CHI.SUMM),
	CH.ID_CHEQUE_GLOBAL
INTO #temp_t1
FROM CHEQUE CH
	INNER JOIN CHEQUE_ITEM CHI ON CH.ID_CHEQUE_GLOBAL = CHI.ID_CHEQUE_GLOBAL
	INNER JOIN LOT L ON CHI.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
	INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	LEFT JOIN [META_USER] U ON CH.ID_USER_DATA = U.USER_NUM
	LEFT JOIN SCALING_RATIO SR ON CHI.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
	LEFT JOIN GOODS G ON CHI.ID_GOODS = G.ID_GOODS
/*	LEFT JOIN GOODS_2_GROUP G2G ON G2G.ID_GOODS = G.ID_GOODS
	LEFT JOIN GOODS_GROUP GG ON GG.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP
	LEFT JOIN #GGROUPS GGR ON G2G.ID_GOODS = GGR.ID_GOODS */
WHERE CH.CHEQUE_TYPE IN ('SALE', 'RETURN') AND CH.DOCUMENT_STATE = 'PROC'
	AND CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
	--AND GG.DATE_DELETED IS NULL
	AND (@ALL_CASHIERS = 1 OR CH.ID_USER_DATA IN (SELECT * FROM #CASHIERS))
	
	AND (@ALL_CONTRACTORS = 1 OR (C.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTORS)))
	AND (@ALL_GOODS = 1 OR (G.ID_GOODS IS NULL OR G.ID_GOODS IN (SELECT * FROM #GOODS)))
	--AND (@ALL_GOODS_GROUPS = 1 OR (G2G.ID_GOODS_GROUP IN (SELECT * FROM #GOODS_GROUPS)))
	AND (@ALL_GOODS_GROUPS = 1 OR (G.ID_GOODS IN (SELECT G2G.ID_GOODS FROM #GOODS_GROUPS GG inner join GOODS_2_GROUP G2G ON G2G.ID_GOODS_GROUP = GG.ID_GOODS_GROUP)))
GROUP BY C.NAME + CASE WHEN C.A_COD IS NOT NULL THEN ', ' + CAST(C.A_COD AS VARCHAR(10)) ELSE '' END,/* GGR.NAME, */
	U.FULL_NAME, G.ID_GOODS, /* G2G.ID_GOODS_GROUP, */ G.CODE, G.NAME, CH.ID_CHEQUE_GLOBAL, C.ID_CONTRACTOR, CH.ID_USER_DATA



--SELECT * FROM #TEMP_T1
DROP TABLE #GGROUPS
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	ID_CONTRACTOR,
	ID_USER_DATA,
	CONTRACTOR_NAME,
	CASHIER_NAME,
	GOODS_CODE,
	GOODS_NAME,
	GOODS_GROUP_NAME,
	COUNT_CHEQUE = COUNT_CHEQUE,
	COUNT_QUANTITY = COUNT_QUANTITY,
	SUM_SUP = SUM_SUP,
	SUM_SAL = SUM_SAL,
	SUM_DIS = SUM_DIS,
	SUMM = SUMM	
INTO #TEMP_TX
FROM #TEMP_T1
GROUP BY ID_CONTRACTOR,
	ID_USER_DATA,
	CONTRACTOR_NAME,
	CASHIER_NAME,
	GOODS_CODE,
	GOODS_NAME,
	GOODS_GROUP_NAME,
	COUNT_CHEQUE,
	COUNT_QUANTITY,
	SUM_SUP,
	SUM_SAL,
	SUM_DIS,
	SUMM,
	ID_CHEQUE_GLOBAL	

--SELECT ID_CONTRACTOR, GOODS_GROUP_NAME, COUNT_CHEQUE= sum(COUNT_CHEQUE) FROM #temp_t1 group by ID_CONTRACTOR, GOODS_GROUP_NAME
/*

*/
IF OBJECT_ID('TEMPDB..#TEMP_T2') IS NOT NULL DROP TABLE #TEMP_T2
create table #TEMP_T2
(
	ID_CONTRACTOR bigint
	,ID_USER_DATA bigint
	,GOODS_GROUP_NAME VARCHAR(2000)
	,COUNT_CHEQUE_GR MONEY
)

IF (@CASH = 1)
BEGIN
--select 1
insert #TEMP_T2
(
	ID_CONTRACTOR 
	,ID_USER_DATA 
	,GOODS_GROUP_NAME 
	,COUNT_CHEQUE_GR 
)
SELECT
	ID_CONTRACTOR,
	ID_USER_DATA,
	GOODS_GROUP_NAME,
	COUNT_CHEQUE_GR = COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
FROM (SELECT DISTINCT ID_CONTRACTOR, ID_USER_DATA, GOODS_GROUP_NAME, ID_CHEQUE_GLOBAL, COUNT_CHEQUE FROM #temp_t1) TMP
GROUP BY ID_CONTRACTOR, ID_USER_DATA, GOODS_GROUP_NAME

END
ELSE
BEGIN
--select 2
insert #TEMP_T2
(
	ID_CONTRACTOR 
	,ID_USER_DATA 
	,GOODS_GROUP_NAME 
	,COUNT_CHEQUE_GR 
)
--SELECT 
SELECT
	TMP.ID_CONTRACTOR,
	ID_USER_DATA,
	TMP.GOODS_GROUP_NAME,
	COUNT_CHEQUE_GR =MAX(tt.COUNT_CHEQUE_GR)-- COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
FROM (SELECT DISTINCT ID_CONTRACTOR, ID_USER_DATA, GOODS_GROUP_NAME, ID_CHEQUE_GLOBAL, COUNT_CHEQUE FROM #temp_t1) TMP
INNER JOIN (
SELECT
	ID_CONTRACTOR,
	--ID_USER_DATA=0,
	GOODS_GROUP_NAME,
	COUNT_CHEQUE_GR = COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
FROM (SELECT DISTINCT ID_CONTRACTOR, ID_USER_DATA, GOODS_GROUP_NAME, ID_CHEQUE_GLOBAL, COUNT_CHEQUE FROM #temp_t1) TMP
GROUP BY ID_CONTRACTOR, GOODS_GROUP_NAME
) tt on tt.ID_CONTRACTOR = TMP.ID_CONTRACTOR AND tt.GOODS_GROUP_NAME = TMP.GOODS_GROUP_NAME
GROUP BY TMP.ID_CONTRACTOR, ID_USER_DATA, TMP.GOODS_GROUP_NAME


END 
--select sum(COUNT_CHEQUE_GR) from  #TEMP_T2
--select * from  #TEMP_T2

SELECT
	ID_CONTRACTOR,
	ID_USER_DATA,
	COUNT_CHEQUE_U = COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
INTO #TEMP_T3
FROM (SELECT DISTINCT ID_CONTRACTOR, ID_USER_DATA, ID_CHEQUE_GLOBAL, COUNT_CHEQUE= MAX(COUNT_CHEQUE) FROM #temp_t1 group by ID_CONTRACTOR, ID_USER_DATA, ID_CHEQUE_GLOBAL) TMP
GROUP BY ID_CONTRACTOR, ID_USER_DATA

--select sum(COUNT_CHEQUE_U) from  #TEMP_T3
--select * from  #TEMP_T3

SELECT
	ID_CONTRACTOR,
	COUNT_CHEQUE_CT = COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
INTO #TEMP_T4
FROM (SELECT DISTINCT ID_CONTRACTOR, ID_CHEQUE_GLOBAL, COUNT_CHEQUE= MAX(COUNT_CHEQUE) FROM #temp_t1 group by ID_CONTRACTOR, ID_CHEQUE_GLOBAL) TMP
GROUP BY ID_CONTRACTOR

--select * from  #TEMP_T4
--select sum(COUNT_CHEQUE) from  #TEMP_T1 

IF (@CASH = 1)
BEGIN
	SELECT
		CONTRACTOR_NAME = t1.CONTRACTOR_NAME,
		CASHIER_NAME = t1.CASHIER_NAME,
		GOODS_CODE = GOODS_CODE,
		GOODS_NAME = GOODS_NAME,
		GOODS_GROUP_NAME = t1.GOODS_GROUP_NAME,
		COUNT_CHEQUE = SUM(t1.COUNT_CHEQUE),
		COUNT_QUANTITY = SUM(t1.COUNT_QUANTITY),
		SUM_SUP = SUM(t1.SUM_SUP),
		SUM_SAL = SUM(t1.SUM_SAL),
		SUM_DIS = SUM(t1.SUM_DIS),
		SUMM = SUM(t1.SUMM),
		ADPRICE = CASE WHEN ISNULL(SUM(t1.SUM_SUP), 0) = 0 THEN NULL ELSE (SUM(t1.SUM_SAL) / SUM(t1.SUM_SUP) - 1) * 100 END,
		COUNT_CHEQUE_GR = MAX(T2.COUNT_CHEQUE_GR),
		COUNT_CHEQUE_U = MAX(T3.COUNT_CHEQUE_U),
		COUNT_CHEQUE_CT = MAX(T4.COUNT_CHEQUE_CT)
	FROM #temp_tx t1
		inner join #temp_t2 t2 on t1.id_contractor = t2.id_contractor and t1.id_user_data = t2.id_user_data and t1.GOODS_GROUP_NAME = t2.GOODS_GROUP_NAME
		inner join #temp_t3 t3 on t1.id_contractor = t3.id_contractor and t1.id_user_data = t3.id_user_data
		inner join #temp_t4 t4 on t1.id_contractor = t4.id_contractor
	GROUP BY CONTRACTOR_NAME, CASHIER_NAME, t1.GOODS_GROUP_NAME, GOODS_CODE, GOODS_NAME
	HAVING SUM(t1.COUNT_CHEQUE) > 0 --and(GOODS_CODE= 66076)
	ORDER BY CONTRACTOR_NAME, CASHIER_NAME, t1.GOODS_GROUP_NAME, GOODS_NAME
END
ELSE
BEGIN
--select s=sum(s) from (
SELECT
--s=sum(COUNT_CHEQUE)/*
	CONTRACTOR_NAME = t1.CONTRACTOR_NAME,
	GOODS_CODE = GOODS_CODE,
	GOODS_NAME = GOODS_NAME,
	GOODS_GROUP_NAME = t1.GOODS_GROUP_NAME, 
	COUNT_CHEQUE = SUM(t1.COUNT_CHEQUE),
	COUNT_QUANTITY = SUM(t1.COUNT_QUANTITY),
	SUM_SUP = SUM(t1.SUM_SUP),
	SUM_SAL = SUM(t1.SUM_SAL),
	SUM_DIS = SUM(t1.SUM_DIS),
	SUMM = SUM(t1.SUMM),
	ADPRICE = CASE WHEN ISNULL(SUM(t1.SUM_SUP), 0) = 0 THEN NULL ELSE (SUM(t1.SUM_SAL) / SUM(t1.SUM_SUP) - 1) * 100 END,
	COUNT_CHEQUE_GR = MAX(T2.COUNT_CHEQUE_GR),
	COUNT_CHEQUE_CT = MAX(T4.COUNT_CHEQUE_CT)
	--*/
FROM #temp_tx t1
	left join #temp_t2 t2 on t1.id_contractor = t2.id_contractor and t1.id_user_data = t2.id_user_data and t1.GOODS_GROUP_NAME = t2.GOODS_GROUP_NAME
	left join #temp_t4 t4 on t1.id_contractor = t4.id_contractor		
GROUP BY CONTRACTOR_NAME, t1.GOODS_GROUP_NAME, GOODS_CODE, GOODS_NAME
HAVING SUM(t1.COUNT_CHEQUE) > 0 
--)g
ORDER BY CONTRACTOR_NAME, t1.GOODS_GROUP_NAME, GOODS_NAME
END
DECLARE @GROUPS VARCHAR(4000)

IF (@ALL_GOODS_GROUPS = 1)
BEGIN
SELECT GOODS_GROUPS = '(�� ����)'
END
ELSE
BEGIN
SELECT 
	@GROUPS = ISNULL(@GROUPS + ', ' + G.NAME, G.NAME)
FROM (SELECT DISTINCT ID_GOODS_GROUP FROM #GOODS_GROUPS) T
	INNER JOIN GOODS_GROUP G ON G.ID_GOODS_GROUP = T.ID_GOODS_GROUP

SELECT GOODS_GROUPS = @GROUPS
END

SELECT DISTINCT
	COUNT_CHEQUE_ALL = COUNT(*) - 2 * SUM(CASE WHEN COUNT_CHEQUE = -1 THEN 1 ELSE 0 END)
FROM
(SELECT ID_CHEQUE_GLOBAL,COUNT_CHEQUE= MAX(COUNT_CHEQUE) FROM #TEMP_T1 group by ID_CHEQUE_GLOBAL) TMP
--SELECT * FROM #TEMP_T1
--SELECT ID_CHEQUE_GLOBAL, MAX(COUNT_CHEQUE) FROM #TEMP_T1 group by ID_CHEQUE_GLOBAL

RETURN 0
GO
/*
EXEC DBO.REPEX_INDIVIDUAL_SALES N'
<XML>
	<DATE_FR>2011-09-01T11:24:51.062</DATE_FR>
	<DATE_TO>2011-09-01T11:24:51.062</DATE_TO>
<CASH>1</CASH>
</XML>'
	--
*/
--select * from sys_option
--update m set version = 0 from meta_report m



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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'IndividualSalesParams.IndividualSalesParams'
--select type_name from  meta_report --RCBIndividualSales_Rigla