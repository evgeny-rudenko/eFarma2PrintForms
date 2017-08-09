
SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.CANCELLATION_OF_SHELF_LIFE_RIGLA') IS NULL EXEC('CREATE PROCEDURE DBO.CANCELLATION_OF_SHELF_LIFE_RIGLA AS RETURN')
GO
ALTER  PROCEDURE DBO.CANCELLATION_OF_SHELF_LIFE_RIGLA
    @XMLPARAM NTEXT AS
    
    DECLARE @HDOC INT
	DECLARE @DATE_FR DATETIME
	DECLARE @DATE_TO DATETIME	
	DECLARE @MONTH VARCHAR(2)
	DECLARE @YEAR VARCHAR(4)
	DECLARE @ID_CONTRACTOR BIGINT
	DECLARE	@RESULT VARCHAR(4000)
	DECLARE	@SALES VARCHAR(4000)
	DECLARE	@DOC_NUMS VARCHAR(4000)
	DECLARE	@DOC_NUM VARCHAR(300)
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT
	@MONTH = [MONTH],
	@YEAR = [YEAR],
	@ID_CONTRACTOR = ID_CONTRACTOR
FROM OPENXML(@HDOC, '/XML') WITH(
	[MONTH] VARCHAR(2) 'MONTH',
	[YEAR] VARCHAR(4) 'YEAR',
	ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
)

EXEC SP_XML_REMOVEDOCUMENT @HDOC
SET LANGUAGE 'us_english'
SET @DATE_FR = DATEADD(D, 0, cast(@YEAR + '.' + @MONTH + '.' + '01' as datetime))
SET @DATE_TO = DATEADD(MONTH, 1, DATEADD(DAY, 1-day(@DATE_FR),@DATE_FR))-1

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

--INN 

--IF OBJECT_ID('Tempdb..#LOT') is not null drop table #LOT
select 
	l.ID_GOODS
	,l.ID_LOT_GLOBAL
	,GOODSCODE = G.CODE
	,GOODSNAME = G.Name
	,UNIT_NAME = U.SHORT_NAME
	,SUPPLIER_NAME = C.NAME
	,INVOICE_DATE =CONVERT(VARCHAR(10), INCOMING_DATE, 104) /* CASE WHEN ID_TABLE IN(30,37) THEN
										CASE WHEN (INCOMING_DATE IS NULL) THEN 
																		 CONVERT(VARCHAR(10), INVOICE_DATE, 104) 
										ELSE CONVERT(VARCHAR(10), INCOMING_DATE, 104) END
					ELSE CONVERT(VARCHAR(10), INVOICE_DATE, 104) END */
	,REMAINS = L.QUANTITY_REM
	,PRICE_SUP = L.PRICE_SUP
	,SUM_SUP = L.QUANTITY_REM * L.PRICE_SUP
	,PRICE_SAL = L.PRICE_SAL
	,BEST_BEFORE = CONVERT(VARCHAR(10), BEST_BEFORE, 104)
	,REV_DATES = convert(varchar(1000), '')
	,SALES = NULL
	,DOC_NUMS = L.INCOMING_NUM
	,ID_STORE
	,INVOICE_SOURCE = l.invoice_num
	,L.ID_SERIES
INTO #LOT
--select *
FROM LOT L WITH(NOLOCK)
INNER JOIN (SELECT ID_GOODS, NAME, CODE from GOODS WITH(NOLOCK)) G on G.ID_GOODS = l.ID_GOODS
INNER JOIN (SELECT ID_CONTRACTOR, NAME FROM CONTRACTOR WITH(NOLOCK)) C on C.ID_CONTRACTOR = L.ID_SUPPLIER
LEFT JOIN (select ID_SERIES, BEST_BEFORE from series WITH(NOLOCK)) SER on SER.ID_SERIES = L.ID_SERIES
INNER JOIN (SELECT ID_SCALING_RATIO, ID_UNIT FROM SCALING_RATIO WITH(NOLOCK)) SR on SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
LEFT JOIN (SELECT ID_UNIT, NAME, SHORT_NAME FROM UNIT) U on U.ID_UNIT = SR.ID_UNIT
--LEFT JOIN (select id_lot_global, PRICE_SUP, PRICE_SAL from LOT WITH(NOLOCK)) l_par on l_par.id_lot_global = dbo.GetParentLot(l.ID_LOT_GLOBAL)
where 1=1
	AND L.ID_STORE in (select ID_STORE FROM STORE where ID_CONTRACTOR = @ID_CONTRACTOR)
	AND L.QUANTITY_REM > 0
	AND BEST_BEFORE between @DATE_FR and @DATE_TO
	
	--and L.ID_LOT_GLOBAL = '73C7C893-F721-4BE2-97B8-758684EF218F'
ORDER BY GOODSNAME, SUPPLIER_NAME


/*
if object_id('Tempdb..#Tmp') is not null drop table #Tmp
select ID_LOT_GLOBAL, ID_PARENT, ID_GOODS 
into #Tmp
from LOT
where id_goods in 
(select ID_GOODS from #LOT)
*/

--if object_id('TmpLots') is not null drop table TmpLots
create table #TmpLots
(	IDLot uniqueidentifier )
DECLARE @Par varchar(255)
	,@IDG bigint
	,@IDL uniqueidentifier
	,@IDSer bigint

DECLARE TmpCur cursor for
	select invoice_source 
		,id_goods
		,ID_LOT_GLOBAL
		,ID_SERIES
	from #LOT
OPEN TmpCur
WHILE (1=1)
BEGIN
	FETCH FROM TmpCur INTO @Par, @IDG, @IDL, @IDSer
	IF @@FETCH_STATUS <> 0 BREAK
	DECLARE @Res varchar(1000), @ID uniqueidentifier
	delete from #TmpLots
	insert into #TmpLots(IDLot) --values (@Buf)
	select id_lot_global from LOT 
	where INVOICE_NUM = @Par 
		and ID_GOODS = @IDG 
		and ID_TABLE = 13
		and ID_SERIES = @IDSer
	union 
    select id_parent 
    from LOT 
    where INVOICE_NUM = @Par 
        and ID_GOODS = @IDG 
        and ID_TABLE = 13
        and ID_SERIES = @IDSer
	--select * from TmpLots
	/*
	WHILE @Par is not null 
	BEGIN
		PRINT @ID PRINT ': 'PRINT @Par-- PRINT' ' PRINT @Buf 
		--insert into TmpLots(IDLot) values (@Buf)
		SELECT @Par = ID_PARENT, @Buf = ID_LOT_GLOBAL
		FROM #Tmp WITH(NOLOCK)
		WHERE ID_LOT_GLOBAL = @Par
		insert into TmpLots(IDLot) values (@Buf)
	END
	*/
	
	set @Res = ''
	select @Res = @Res + CONVERT(varchar(10), [DATE], 104)+'; '
	from ACT_REVALUATION2 a WITH(NOLOCK)
	where ID_ACT_REVALUATION2_GLOBAL in 
		(select ID_ACT_REVALUATION2_GLOBAL
		from ACT_REVALUATION2_ITEM WITH(NOLOCK)
		where ID_LOT_GLOBAL in (select IDLot from #TmpLots)
		)
		and DOCUMENT_STATE = 'PROC'
	order by [DATE]
	IF Len(@Res)>0	
		update #LOT set REV_DATES = @Res
		WHERE ID_LOT_GLOBAL = @IDL
END
CLOSE TmpCur
DEALLOCATE TmpCur

--if object_id('TmpLots') is not null drop table TmpLots

SELECT 
	--Q = T.REV_DATES,
	--id_lot_global,
	ID_GOODS = T.ID_GOODS,  
	GOODSCODE = T.GOODSCODE, 
	GOODSNAME = T.GOODSNAME, 
	UNIT_NAME = T.UNIT_NAME, 
	SUPPLIER_NAME = T.SUPPLIER_NAME, 
	INVOICE_DATE = T.INVOICE_DATE, 
	REMAINS = T.REMAINS, 
	PRICE_SUP = T.PRICE_SUP,
	SUM_SUP = T.REMAINS * T.PRICE_SUP, 
	PRICE_SAL = T.PRICE_SAL, 
	BEST_BEFORE = T.BEST_BEFORE, 
	REV_DATES = T.REV_DATES,
	SALES = T.SALES, 
	DOC_NUM = T.DOC_NUMS
	--,len(REV_DATES)
FROM #LOT T


--IF OBJECT_ID('Tempdb..#LOT') is not null drop table #LOT

GO
--select * from CONTRACTOR where name like '%35%'
--exec CANCELLATION_OF_SHELF_LIFE_RIGLA @xmlParam=N'<XML><MONTH>08</MONTH><YEAR>2011</YEAR><ID_CONTRACTOR>6016</ID_CONTRACTOR></XML>'


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'CancellationOfShelfLife_Rigla.CancellationOfShelfLifeParams'