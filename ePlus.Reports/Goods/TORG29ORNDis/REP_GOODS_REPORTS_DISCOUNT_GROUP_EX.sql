IF (OBJECT_ID('REP_GOODS_REPORTS_DISCOUNT_GROUP_EX') IS NULL) EXEC ('CREATE PROCEDURE REP_GOODS_REPORTS_DISCOUNT_GROUP_EX AS RETURN')
GO
ALTER PROCEDURE REP_GOODS_REPORTS_DISCOUNT_GROUP_EX
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT, @DATE_FR DATETIME, @DATE_TO DATETIME, @CO BIT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @CO = CO
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        CO BIT 'CO'
    )
    -- �����������
    SELECT DISTINCT C.ID_CONTRACTOR, C.NAME INTO #CONTRACTOR
    FROM CONTRACTOR C(NOLOCK)
    INNER JOIN (SELECT * FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')) TAB ON TAB.ID_CONTRACTOR = C.ID_CONTRACTOR
    /*
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)
		*/
    -- ������
    SELECT DISTINCT S.ID_STORE, S.ID_CONTRACTOR, S.NAME INTO #STORE
    FROM STORE S(NOLOCK)
    INNER JOIN #CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
    INNER JOIN (SELECT * FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.')) TAB ON TAB.ID_STORE = S.ID_STORE
    /*
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)
		*/
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

IF NOT EXISTS(SELECT TOP 1 1 FROM #STORE) BEGIN
    INSERT INTO #STORE (ID_STORE, ID_CONTRACTOR, NAME)
    SELECT S.ID_STORE, S.ID_CONTRACTOR, S.NAME
    FROM STORE S(NOLOCK) INNER JOIN #CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
END
DELETE FROM #STORE WHERE ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)

DECLARE @MAX_MONEY MONEY
SET @MAX_MONEY = 99999999999999.999999999999999999999999

SELECT
    DISCOUNT_PERCENT = ISNULL(DICS.DISCOUNT_VALUE, ISNULL(DAS.DISCOUNT_VALUE, ISNULL(CASE DP.DISCOUNT_TYPE WHEN 'PERCENT' THEN DP.DISCOUNT_VALUE ELSE NULL END,@MAX_MONEY))),
    DOC_NAME =  ISNULL('������ '+CONVERT(VARCHAR, ISNULL(DICS.DISCOUNT_VALUE, ISNULL(DAS.DISCOUNT_VALUE, ISNULL(CASE DP.DISCOUNT_TYPE WHEN 'PERCENT' THEN DP.DISCOUNT_VALUE ELSE NULL END, NULL))))+'%', '������ ������� ������'),
    RETAIL_PRICE = SUM(CASE WHEN C.CHEQUE_TYPE='SALE' THEN CI.SUMM_DISCOUNT ELSE -CI.SUMM_DISCOUNT END)
FROM CHEQUE C
INNER JOIN CHEQUE_ITEM CI ON CI.ID_CHEQUE_GLOBAL = C.ID_CHEQUE_GLOBAL
INNER JOIN DISCOUNT2_MAKE_ITEM DMI ON DMI.ID_CHEQUE_ITEM_GLOBAL = CI.ID_CHEQUE_ITEM_GLOBAL
INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CI.ID_LOT_GLOBAL
LEFT JOIN DISCOUNT2_CARD DC ON DC.ID_DISCOUNT2_CARD_GLOBAL = DMI.ID_DISCOUNT2_CARD_GLOBAL
LEFT JOIN DISCOUNT2_ACCUMULATION_SCHEMA DAS ON DAS.ID_DISCOUNT2_CARD_TYPE_GLOBAL = DC.ID_DISCOUNT2_CARD_TYPE_GLOBAL
LEFT JOIN DISCOUNT2_PROGRAM DP ON DP.ID_DISCOUNT2_PROGRAM_GLOBAL = DMI.ID_DISCOUNT2_PROGRAM_GLOBAL
LEFT JOIN DISCOUNT2_INSURANCE_POLICY DIP ON DIP.ID_DISCOUNT2_INSURANCE_POLICY_GLOBAL = DMI.ID_DISCOUNT2_INSURANCE_POLICY_GLOBAL
LEFT JOIN DISCOUNT2_INSURANCE_CALC_SCHEMA DICS ON DICS.ID_DISCOUNT2_CAT_LGOT_GLOBAL = DIP.ID_DISCOUNT2_CAT_LGOT_GLOBAL
WHERE L.ID_STORE IN (SELECT ID_STORE FROM #STORE)
AND C.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
AND C.CHEQUE_TYPE='SALE' --������ �������
AND (
(DMI.ID_DISCOUNT2_INSURANCE_POLICY_GLOBAL IS NOT NULL AND ISNULL(DICS.CHEQUE_SUM_FROM,0) < C.SUMM+C.SUM_DISCOUNT AND (ISNULL(DICS.CHEQUE_SUM_TO,0)>=C.SUMM+C.SUM_DISCOUNT OR ISNULL(DICS.CHEQUE_SUM_TO,0)=0))
OR (DMI.ID_DISCOUNT2_CARD_GLOBAL IS NOT NULL AND ISNULL(DAS.ACCUMULATION_SUM_FROM,0) < CI.SUMM+CI.SUMM_DISCOUNT AND (ISNULL(DAS.ACCUMULATION_SUM_TO,0)>=CI.SUMM+CI.SUMM_DISCOUNT OR ISNULL(DAS.ACCUMULATION_SUM_TO,0)=0))
OR (DMI.ID_DISCOUNT2_PROGRAM_GLOBAL IS NOT NULL)
)
GROUP BY ISNULL('������ '+CONVERT(VARCHAR, ISNULL(DICS.DISCOUNT_VALUE, ISNULL(DAS.DISCOUNT_VALUE, ISNULL(CASE DP.DISCOUNT_TYPE WHEN 'PERCENT' THEN DP.DISCOUNT_VALUE ELSE NULL END, NULL))))+'%', '������ ������� ������'),
ISNULL(DICS.DISCOUNT_VALUE, ISNULL(DAS.DISCOUNT_VALUE, ISNULL(CASE DP.DISCOUNT_TYPE WHEN 'PERCENT' THEN DP.DISCOUNT_VALUE ELSE NULL END, @MAX_MONEY)))
ORDER BY 1 ASC 
RETURN
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'TORG29ORNDis.ORNDisFormParams'
