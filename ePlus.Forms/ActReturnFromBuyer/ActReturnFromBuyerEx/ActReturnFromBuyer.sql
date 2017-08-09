SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_ACT_RETURN_FROM_BUYER') IS NULL EXEC('CREATE PROCEDURE REPEX_ACT_RETURN_FROM_BUYER AS RETURN')
GO		
ALTER PROCEDURE REPEX_ACT_RETURN_FROM_BUYER
	@XMLPARAM NTEXT AS
		
DECLARE @HDOC INT
DECLARE @ID_ACT_RETURN_TO_BUYER_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT @ID_ACT_RETURN_TO_BUYER_GLOBAL = ID_ACT_RETURN_TO_BUYER_GLOBAL
	FROM OPENXML(@HDOC , '/XML') WITH(
		ID_ACT_RETURN_TO_BUYER_GLOBAL UNIQUEIDENTIFIER 'ID_ACT_RETURN_TO_BUYER_GLOBAL' 
	)
EXEC SP_XML_REMOVEDOCUMENT @HDOC



DECLARE @ID_DOCUMENT_BASE_GLOBAL uniqueidentifier,
		@SQL NVARCHAR(200)
SET @ID_DOCUMENT_BASE_GLOBAL = '00000000-0000-0000-0000-000000000000'
SET @SQL = N'SELECT @ID_DOCUMENT_BASE_GLOBAL = ID_DOCUMENT_BASE_GLOBAL
			FROM ACT_RETURN_TO_BUYER
			WHERE ID_ACT_RETURN_TO_BUYER_GLOBAL = @ID_ACT_RETURN_TO_BUYER_GLOBAL'
IF (COL_LENGTH('ACT_RETURN_TO_BUYER', 'ID_DOCUMENT_BASE_GLOBAL') IS NOT NULL)
EXEC SP_EXECUTESQL @SQL, N'@ID_ACT_RETURN_TO_BUYER_GLOBAL uniqueidentifier, @ID_DOCUMENT_BASE_GLOBAL uniqueidentifier OUT', @ID_ACT_RETURN_TO_BUYER_GLOBAL=@ID_ACT_RETURN_TO_BUYER_GLOBAL, @ID_DOCUMENT_BASE_GLOBAL=@ID_DOCUMENT_BASE_GLOBAL OUT


SELECT TOP 1 
	AB_NUMBER = AB.MNEMOCODE,
	AB_NUMBERBASE = DOCUMENT_DESCRIPTION,
	AB_DATE = CONVERT(VARCHAR , AB.DATE , 104),
	AB_STORE = (SELECT TOP 1 S.NAME FROM STORE S WHERE S.ID_STORE = AB.ID_STORE_TO),
	AB_CONTRACTORTO = (SELECT TOP 1 case
	                                  when C.FULL_NAME is null then C.NAME
	                                  when C.FULL_NAME = '' then C.NAME
	                                  else C.FULL_NAME
	                                end
	                                   FROM CONTRACTOR C WHERE C.ID_CONTRACTOR = AB.ID_CONTRACTOR_TO),
	AB_CONTRACTORFROM = (SELECT TOP 1 case
	                                    when C.FULL_NAME is Null then C.NAME
	                                    when C.FULL_NAME = '' then C.NAME
	                                    else C.FULL_NAME
	                                  end
	                                    FROM CONTRACTOR C WHERE C.ID_CONTRACTOR = AB.ID_CONTRACTOR_FROM),
	AB_CHEMISTRY = (SELECT TOP 1 C.NAME FROM CONTRACTOR C WHERE C.ID_CONTRACTOR = (SELECT ISNULL(MAX(S.ID_CONTRACTOR) , -1) FROM STORE S WHERE S.ID_STORE = AB.ID_STORE_TO))
FROM MV_ACT_RETURN_TO_BUYER AB
WHERE AB.ID_ACT_RETURN_TO_BUYER_GLOBAL = @ID_ACT_RETURN_TO_BUYER_GLOBAL
		
SELECT
--CHI.*,
--ABI.*,f="tttttttttt",
	G_RUSNAME =  ISNULL(CASE WHEN ABI.HAS_LOT = 0 THEN (SELECT TOP 1 G.NAME FROM GOODS G WHERE G.ID_GOODS = ABI.ID_GOODS) ELSE (SELECT TOP 1 G.NAME FROM GOODS G , LOT L WHERE L.ID_GOODS = G.ID_GOODS AND L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL) END,S4S.NAME) ,
	G_PRODUCER = CASE WHEN ABI.HAS_LOT = 0 THEN (SELECT TOP 1 P.NAME FROM PRODUCER P , GOODS G WHERE P.ID_PRODUCER = G.ID_PRODUCER AND G.ID_GOODS = ABI.ID_GOODS) ELSE (SELECT TOP 1 P.NAME FROM PRODUCER P , LOT L , GOODS G WHERE L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL AND P.ID_PRODUCER = G.ID_PRODUCER AND G.ID_GOODS = L.ID_GOODS) END ,
	G_SERIALNUMBER =
	CASE
	WHEN ABI.HAS_LOT = 0 AND ABI.HAS_SERIES = 0 THEN ABI.SERIES_NUMBER
	WHEN ABI.HAS_LOT = 0 AND ABI.HAS_SERIES = 1 THEN (SELECT TOP 1 S.SERIES_NUMBER FROM SERIES S WHERE S.ID_SERIES = ABI.ID_SERIES)
	ELSE (SELECT TOP 1 S.SERIES_NUMBER FROM LOT L , SERIES S WHERE L.ID_SERIES = S.ID_SERIES AND L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL)
	END ,
	G_BESTBEFORE =
	CASE
	WHEN ABI.HAS_LOT = 0 AND ABI.HAS_SERIES = 0 THEN CONVERT(VARCHAR , ABI.BEST_BEFORE , 104)
	WHEN ABI.HAS_LOT = 0 AND ABI.HAS_SERIES = 1 THEN (SELECT TOP 1 CONVERT(VARCHAR , S.BEST_BEFORE , 104) FROM SERIES S WHERE S.ID_SERIES = ABI.ID_SERIES)
	ELSE (SELECT TOP 1 CONVERT(VARCHAR , S.BEST_BEFORE , 104) FROM LOT L , SERIES S WHERE L.ID_SERIES = S.ID_SERIES AND L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL)
	END ,
	ABI_SUPPLIERPRICEPERUNIT = ISNULL(CASE WHEN ABI.HAS_LOT = 0 THEN ISNULL(CAST(ABI.SUPPLIER_PRICE_PER_UNIT AS DECIMAL(18 , 2)) , 0) ELSE (SELECT ISNULL(CAST(L.PRICE_SUP AS DECIMAL(18 , 2)) , 0) FROM LOT L WHERE L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL) END,ABI.SUPPLIER_PRICE_PER_UNIT - ABI.PVAT_SUP) ,
	ABI_SUPPLIERTAXRATE = CASE WHEN ABI.HAS_LOT = 0 THEN (SELECT ISNULL(MAX(CAST(TT.TAX_RATE AS DECIMAL(18 , 2))) , 0) FROM TAX_TYPE TT WHERE TT.ID_TAX_TYPE = ABI.ID_SUPPLIER_TAX_TYPE) ELSE (SELECT ISNULL(MAX(CAST(L.VAT_SUP AS DECIMAL(18 , 2))), 0) FROM LOT L WHERE L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL) END ,
	ABI_RETAILPRICEPERUNIT = ISNULL(CASE 
										WHEN ABI.HAS_LOT = 0 THEN ISNULL(CAST(ABI.PRICE_PER_UNIT AS DECIMAL(18 , 2)) , 0) 
										ELSE (SELECT ISNULL(CAST(L.PRICE_SAL AS DECIMAL(18 , 2)) , 0) FROM LOT L WHERE L.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL) 
									END, ABI.PRICE_PER_UNIT),
	ABI_QUANTITY = CAST(ABI.QUANTITY AS DECIMAL(18 , 2)) ,
	ABI_SCALINGMULTIPLY = ISNULL((SELECT TOP 1 CAST(CAST(SR.NUMERATOR AS DECIMAL(18 , 2)) / SR.DENOMINATOR AS DECIMAL(18 , 2)) FROM SCALING_RATIO SR WHERE SR.ID_SCALING_RATIO = ABI.ID_SCALING_RATIO) , 1.00) ,
	ABI_NAMEUNIT = DBO.FN_SCALE_NAME(ABI.ID_SCALING_RATIO)
	,DISCOUNT = ISNULL(CHI.SUMM_DISCOUNT,0)* CASE  WHEN ISNULL(CHI.QUANTITY,0) = 0 THEN 1 ELSE CONVERT(MONEY,ABI.QUANTITY) / CONVERT(MONEY, CHI.QUANTITY)  END
	--select *--ID_DOCUMENT_BASE_GLOBAL,id_base_document_item, * 
FROM	ACT_RETURN_TO_BUYER_ITEM ABI
inner join ACT_RETURN_TO_BUYER ab ON AB.ID_ACT_RETURN_TO_BUYER_GLOBAL = ABI.ID_ACT_RETURN_TO_BUYER_GLOBAL
LEFT JOIN CHEQUE_ITEM CHI ON (CHI.ID_CHEQUE_GLOBAL = @ID_DOCUMENT_BASE_GLOBAL /* OR CHI.ID_CHEQUE_ITEM = ABI.id_base_document_item*/) and (CHI.ID_LOT_GLOBAL = ABI.ID_LOT_GLOBAL)
LEFT JOIN SERVICE_4_SALE S4S ON S4S.ID_SERVICE_4_SALE = ABI.ID_LOT_GLOBAL 
WHERE ABI.ID_ACT_RETURN_TO_BUYER_GLOBAL = @ID_ACT_RETURN_TO_BUYER_GLOBAL
ORDER BY G_RUSNAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO

/*
exec REPEX_ACT_RETURN_FROM_BUYER N'<XML>
<ID_ACT_RETURN_TO_BUYER_GLOBAL>3BF5AC38-0DAD-409D-B9CF-D53B79524458</ID_ACT_RETURN_TO_BUYER_GLOBAL></XML>'

exec REPEX_ACT_RETURN_FROM_BUYER N'<XML>
<ID_ACT_RETURN_TO_BUYER_GLOBAL>19D1EBAA-328F-4E54-934E-C5480136BA9D</ID_ACT_RETURN_TO_BUYER_GLOBAL></XML>'
*/
--select * from ACT_RETURN_TO_BUYER
--select *from CHEQUE_ITEM where ID_CHEQUE_ITEM_GLOBAL in ('291403D3-C46B-4C7F-9596-88F69E3C176D','538C2663-7E9D-4819-AF37-B3F2AAC46757')
--select * from lot where ID_LOT_GLOBAL = 'A4C8C6FA-5AD4-40DC-AD41-47CC74DB71E7'


--A55E83B2-4036-475C-B944-37FEB958C95A


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'ActReturnFromBuyerEx.ActReturnFromBuyerEx'



