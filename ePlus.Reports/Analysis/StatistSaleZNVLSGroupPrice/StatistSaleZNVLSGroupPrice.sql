SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO
IF OBJECT_ID('DBO.STATIST_SALE_ZNVLS_GROUP_PRICE') IS NULL EXEC('CREATE PROCEDURE DBO.STATIST_SALE_ZNVLS_GROUP_PRICE AS RETURN')
GO
ALTER PROCEDURE DBO.STATIST_SALE_ZNVLS_GROUP_PRICE
    @XMLPARAM NTEXT
AS

DECLARE	@ALL_CONTRACTOR BIT
DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME


EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT 
SELECT TOP 1
    @DATE_FROM = DATE_FROM,
    @DATE_TO = DATE_TO

FROM OPENXML(@HDOC , '/XML') WITH(
	DATE_FROM DATETIME 'DATE_FROM',
    DATE_TO DATETIME 'DATE_TO')


SELECT S.ID_STORE, S.ID_CONTRACTOR INTO #STORE
FROM STORE S WHERE  S.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM OPENXML(@HDOC, '//XML/ID_CONTRACTOR') 
    WITH(ID_CONTRACTOR BIGINT '.'))


IF @@ROWCOUNT = 0 
	SET @ALL_CONTRACTOR = 1 
	ELSE SET @ALL_CONTRACTOR = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC USP_RANGE_DAYS @DATE_FROM OUTPUT , @DATE_TO OUTPUT


SELECT 
	QUANTITY_SUB = (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) - 
                    (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB) ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR),
    SUM_PROD = L.PRICE_PROD * (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) - 
                    (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB) ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR),
    SUM_SUP =   L.PRICE_SUP * (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) - 
                    (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB) ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR),            
	SUM_SAL = L.PRICE_SAL * (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) - 
                    (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB) ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR),                    
	SUM_PROFIT = (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB * L.PRICE_SAL ELSE 0 END)/**SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)*/ - 
                    (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD * L.PRICE_SAL 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB * L.PRICE_SAL) ELSE 0 END)/* * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)*/-
               ((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB * L.PRICE_SUP ELSE 0 END)/**SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)*/ - 
                   (CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD * L.PRICE_SUP 
                    WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB * L.PRICE_SUP) ELSE 0 END)/* * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)*/)-
               LM.DISCOUNT_ACC
into #TEMP
FROM LOT_MOVEMENT LM
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
	INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = L.ID_SUPPLIER
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT
WHERE (LM.DATE_OP BETWEEN @DATE_FROM AND @DATE_TO)
    AND (@ALL_CONTRACTOR=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
	AND ( G.IMPORTANT = 1)
	AND (CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) - 
	(CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD 
	WHEN LM.CODE_OP='CHEQUE' AND LM.QUANTITY_SUB<0 THEN (-1 * LM.QUANTITY_SUB) ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR) > 0


--select * from #TEMP


select 
GROUP_NAME = CASE WHEN (SUM_SUP/QUANTITY_SUB)<=50. THEN '�� 50 ���. ������������' 
		ELSE 
			CASE WHEN ((SUM_SUP/QUANTITY_SUB)>50. AND (SUM_SUP/QUANTITY_SUB)<=500.) THEN '����� 50 ���. �� 500 ���. ������������' 
				ELSE '����� 500 ���.' END END,
QUANTITY_SUB = SUM(QUANTITY_SUB),
SUM_PROD = SUM(SUM_PROD),
SUM_SUP = SUM(SUM_SUP),
SUM_SAL = SUM(SUM_SAL),
AVG_PRICE_PROD = SUM(SUM_PROD) / SUM(QUANTITY_SUB),
AVG_PRICE_SUP = SUM(SUM_SUP) / SUM(QUANTITY_SUB),
AVG_PRICE_SAL = SUM(SUM_SAL) / SUM(QUANTITY_SUB),
SUM_PROFIT = SUM(SUM_PROFIT),
AVG_PROFIT = (SUM(SUM_SAL) / SUM(QUANTITY_SUB))-(SUM(SUM_SUP) / SUM(QUANTITY_SUB))
from #TEMP
group by
CASE WHEN (SUM_SUP/QUANTITY_SUB)<=50. THEN '�� 50 ���. ������������' 
		ELSE 
			CASE WHEN ((SUM_SUP/QUANTITY_SUB)>50. AND (SUM_SUP/QUANTITY_SUB)<=500.) THEN '����� 50 ���. �� 500 ���. ������������' 
				ELSE '����� 500 ���.' END END

RETURN 0
GO

/*
EXEC STATIST_SALE_ZNVLS_GROUP_PRICE @XMLPARAM = N'
<XML>
	<DATE_FROM>2011-01-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2011-01-01T00:00:00.000</DATE_TO>
	<ID_CONTRACTOR>5276</ID_CONTRACTOR>
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'StatistSaleZNVLSGroupPrice.StatistSaleZNVLSGroupPriceFormParams'