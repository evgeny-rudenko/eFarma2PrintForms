SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INV19_RIGLA_300111_020211') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INV19_RIGLA_300111_020211 AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INV19_RIGLA_300111_020211
	@XMLPARAM NTEXT AS
	
SET LANGUAGE 'us_english'
	
DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
-- IS_SAL = 0 - Ó×ÈÒÛÂÀÞÒÑß ÑÈÑÒÅÌÍÛÅ ÊÎÍÑÒÀÍÒÛ (ÏÎ ÓÌÎË×. ÐÎÇÍ. ÖÅÍÛ)
-- IS_SAL = 1 - ÐÎÇÍÈ×ÍÛÅ ÖÅÍÛ
-- IS_SAL = 2 - ÎÏÒÎÂÛÅ ÖÅÍÛ
DECLARE @IS_SAL SMALLINT
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
		
SELECT @ID_GLOBAL = ID_GLOBAL, @IS_SAL = IS_SAL
FROM OPENXML(@HDOC , '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL', IS_SAL SMALLINT 'IS_SAL')

		
EXEC SP_XML_REMOVEDOCUMENT @HDOC

DECLARE @DOC_DATE DATETIME, @USE_VAT BIT, @FULL BIT
SELECT TOP 1 @DOC_DATE = I.DOC_DATE, @USE_VAT = C.USE_VAT, @FULL = I.[FULL]
FROM INVENTORY_SVED I 
INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_GLOBAL = @ID_GLOBAL


DECLARE @FR_DATE DATETIME, @TO_DATE DATETIME

SET @FR_DATE = CONVERT(datetime, '2011/01/30')
SET @TO_DATE = CONVERT(datetime, '2011/02/02')

IF @DOC_DATE not between @FR_DATE and @TO_DATE
Begin
	--exec REPEX_INV19_RIGLA_BEFORE201104 @XMLPARAM
	select 
		CONTRACTOR = NULL
		,STORE = NULL
		,DOC_NUM = NULL
		,DOC_DATE = NULL
	select 
	
		GOODS_NAME = NULL
		,GOODS_CODE = NULL
		,OKEI_CODE = NULL
		,UNIT_NAME = NULL
		,Q_Add = 0
		,SumPrice_Add = 0
		,SumVat_Add = 0
	
		,Q_Sub = 0
		,SumPrice_Sub = 0
		,SumVat_Sub	= 0
	
		,Q_Rev = 0
		,SumPrice_Rev = 0
		,SumVat_Rev = 0
	SELECT
		DIR = NULL
		,BUH = NULL
	return
End

SELECT 
	CONTRACTOR = CASE WHEN ISNULL(C.FULL_NAME, '') = '' THEN C.NAME ELSE C.FULL_NAME END,
	STORE = S.NAME,
	DOC_NUM = I.DOC_NUM,
	DOC_DATE = CONVERT(VARCHAR(10), I.DOC_DATE, 104)
FROM INVENTORY_SVED I
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_GLOBAL = @ID_GLOBAL


DECLARE @CONST_VAT VARCHAR(10)
IF @IS_SAL = 0
	SELECT TOP 1 @CONST_VAT = VALUE FROM SYS_OPTION WHERE CODE = 'INVENTORY_CALC_VAT'

DECLARE @CONST VARCHAR(10)
IF @IS_SAL = 0
BEGIN
	SELECT TOP 1 @CONST = VALUE FROM SYS_OPTION WHERE CODE = 'INVENTORY_CALC_PRICE'
	IF ISNULL(@CONST, '') = '' SET @CONST = 'SAL'
END

DECLARE @OKEI_CODE VARCHAR(40), @NAME VARCHAR(100)
SELECT TOP 1 @OKEI_CODE = OKEI_CODE, @NAME = NAME FROM UNIT WHERE MNEMOCODE = 'BOX' 

--if OBJECT_ID('Tempdb..#Tmp') is not null drop table #Tmp
select 
	l.ID_GOODS
	,QUANTITY = SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR)
	,SUM_PRICE =  
		ABS(CASE 
				WHEN ((@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1) 
					THEN ISNULL(sum(SUM_ACC), 0)
				ELSE 
						ISNULL(sum(SUM_SUP), 0) END)
	,SUM_SVAT =  ABS(CASE WHEN ((@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1) 
		THEN ISNULL(sum(SUM_ACC-SVAT_ACC), 0)
        ELSE ISNULL(sum(SUM_SUP-SVAT_SUP), 0) END)
	,[TYPE] = case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'IZL' else 'NEDOS' end	
	,[Sign] = case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 1 else -1 end
	,ID_STORE
into #Tmp
from lot_movement lm (NOLOCK)
inner join LOT l (NOLOCK) on l.ID_LOT_GLOBAL = lm.ID_LOT_GLOBAL
INNER JOIN SCALING_RATIO SR (NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
where lm.ID_DOCUMENT = @ID_GLOBAL
group by l.ID_GOODS,
	case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'IZL' else 'NEDOS' end
	,CASE WHEN @USE_VAT = 1 THEN CASE WHEN @CONST_VAT = 'SUP' THEN L.VAT_SUP ELSE L.VAT_SAL END ELSE 0 END
	,ID_STORE, case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 1 else -1 end

select
	GOODS_NAME = g.NAME
	,GOODS_CODE = g.CODE
	,OKEI_CODE = @OKEI_CODE
	,UNIT_NAME = '1/1' + ' ' + @NAME	
	,Q_Add = (case when (Q_Add + Q_Sub)>0 then (Q_Add + Q_Sub) else 0 end)
	,SumPrice_Add = case when SumPrice_Add - SumPrice_Sub>0 then SumPrice_Add - SumPrice_Sub else 0 end
	,SumVat_Add = case when SumVat_Add - SumVat_Sub>0 then SumVat_Add - SumVat_Sub else 0 end
	
	,Q_Sub = (case when (Q_Add + Q_Sub)<0 then ABS(Q_Add + Q_Sub) else 0 end)
	,SumPrice_Sub = case when SumPrice_Add - SumPrice_Sub<0 then ABS(SumPrice_Add - SumPrice_Sub) else 0 end
	,SumVat_Sub	= case when SumVat_Add - SumVat_Sub<0 then  ABS(SumVat_Add - SumVat_Sub) else 0 end
	
	,Q_Rev = (Q_Add + Q_Sub)
	,SumPrice_Rev = SumPrice_Add - SumPrice_Sub
	,SumVat_Rev = SumVat_Add - SumVat_Sub
into #Res
from (
	select 
		Q_Add = (select ISNULL(sum(QUANTITY),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL')
		,SumPrice_Add = (select ISNULL(sum(SUM_PRICE),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL')
		,SumVat_Add = (select ISNULL(sum(SUM_SVAT),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL')
		,Q_Sub = (select ISNULL(sum(QUANTITY),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS')
		,SumPrice_Sub = (select ISNULL(sum(SUM_PRICE),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS')
		,SumVat_Sub = (select ISNULL(sum(SUM_SVAT),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS')
		,t.id_goods
	from #Tmp t
	group by t.ID_GOODS
	) t
inner join goods--(select ID_GOODS, Name, CODE from GOODS (nolock)) 
	g on g.ID_GOODS = t.ID_GOODS
	
select * from #Res
where Q_Rev<>0 or SumPrice_Rev<>0 or SumVat_Rev<>0
order by GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR (nolock)
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

/*
exec DBO.REPEX_INV19_RIGLA_300111_020211 @xmlParam = N'
<XML>
<ID_GLOBAL>BD5C1968-3176-45C0-B615-F249118A6C90</ID_GLOBAL>
<IS_SAL>0</IS_SAL></XML>'
*/
--select * from INVENTORY_SVED where ID_INVENTORY_GLOBAL = '821AEB3C-7903-44AC-94CC-E49F35D7D4C3'




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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'Inv19_Rigla_300111_020211.Inv19_Rigla_300111_020211'