IF (OBJECT_ID('[REPEX_REESTR_PAY_NO_LS]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_REESTR_PAY_NO_LS] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_REESTR_PAY_NO_LS]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME,
    @ALL_CONTRACTOR BIT, @ALL_STORE BIT, @ALL_GOODS_GROUP BIT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_FR = DATE_FR, @DATE_TO = DATE_TO
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO')

    SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
    WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1 ELSE SET @ALL_CONTRACTOR = 0

    SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') 
    WITH(ID_STORE BIGINT '.') WHERE ID_STORE IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_STORE = 1 ELSE SET @ALL_STORE = 0

    SELECT * INTO #GOODS_GROUP FROM OPENXML(@HDOC, '//ID_GOODS_GROUP') 
    WITH(ID_GOODS_GROUP BIGINT '.') WHERE ID_GOODS_GROUP IS NOT NULL
    

    IF @@ROWCOUNT = 0 begin SET @ALL_GOODS_GROUP = 1 end
    ELSE begin
            insert into #GOODS_GROUP
            select distinct t.id_goods_group 
            from FN_GOODS_GROUP_CHILD_GROUPS(null) t
            inner join goods_group gg on gg.id_goods_group = t.id_goods_group_parent and gg.DATE_DELETED IS NULL
            where exists(select null from #GOODS_GROUP t1 where t1.ID_GOODS_GROUP = gg.id_goods_group)
            and not exists(select null from #GOODS_GROUP g where g.ID_GOODS_GROUP = t.ID_GOODS_GROUP)
        SET @ALL_GOODS_GROUP = 0
    end

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT 
	GOODS_NAME = G.[NAME],
	PRODUCER_NAME = P.[NAME],
	PRICE = ROUND(II.SUPPLIER_PRICE_VAT * CONVERT(MONEY, SR.DENOMINATOR)/SR.NUMERATOR,2),
	QUANTITY = SUM(ROUND(II.QUANTITY * CONVERT(MONEY, SR.NUMERATOR)/SR.DENOMINATOR,2)),
	SUPPLYER_NAME = C.[NAME] 
	,STORE_NAME = st.NAME
	,CONTRACTOR_NAME = ct_to.NAME
	--select *
FROM INVOICE I
INNER JOIN INVOICE_ITEM II ON II.ID_INVOICE = I.ID_INVOICE
INNER JOIN GOODS G ON G.ID_GOODS = II.ID_GOODS
INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = II.ID_SCALING_RATIO
inner join STORE st on st.ID_STORE = i.ID_STORE
inner join CONTRACTOR ct_to on ct_to.ID_CONTRACTOR = st.ID_CONTRACTOR
LEFT JOIN (SELECT DISTINCT ID_GOODS FROM GOODS_2_GROUP G2G
        INNER JOIN #GOODS_GROUP GG ON GG.ID_GOODS_GROUP = G2G.ID_GOODS_GROUP
        WHERE G2G.DATE_DELETED IS NULL) T ON T.ID_GOODS = II.ID_GOODS
WHERE (@ALL_CONTRACTOR = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTOR WHERE #CONTRACTOR.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER))
    AND (@ALL_STORE = 1 OR EXISTS(SELECT TOP 1 1 FROM #STORE WHERE #STORE.ID_STORE = I.ID_STORE))
    AND I.DOCUMENT_DATE BETWEEN @DATE_FR AND @DATE_TO
	AND I.DOCUMENT_STATE = 'PROC'
    AND T.ID_GOODS IS NULL
GROUP BY G.[NAME], P.[NAME], ROUND(II.SUPPLIER_PRICE_VAT * CONVERT(MONEY, SR.DENOMINATOR)/SR.NUMERATOR,2), C.[NAME]
,st.NAME,ct_to.NAME
ORDER BY ct_to.NAME,st.NAME,G.[NAME], P.[NAME], C.[NAME], PRICE


RETURN 0
GO
/*
exec REPEX_REESTR_PAY_NO_LS @xmlParam=N'<XML>
<DATE_FR>2010-09-01T10:13:30.781</DATE_FR>
<DATE_TO>2011-09-04T10:13:30.781</DATE_TO>
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'ReestrPayNoLS.ReestrPayNoLSForm'



