SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_INVENTORY_OPIS_EX') IS NULL EXEC('CREATE PROCEDURE REPEX_INVENTORY_OPIS_EX AS RETURN')
GO
ALTER PROCEDURE REPEX_INVENTORY_OPIS_EX
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT
DECLARE @ID_INVENTORY_GLOBAL UNIQUEIDENTIFIER
DECLARE @ID_STORE BIGINT
DECLARE @DOC_DATE DATETIME
DECLARE @FULL BIT
DECLARE @C_NAME VARCHAR(120), @S_NAME VARCHAR(120)
DECLARE @USE_VAT BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT TOP 1 
        @ID_INVENTORY_GLOBAL = ID_INVENTORY_GLOBAL 
    FROM OPENXML(@HDOC , '/XML') WITH(
        ID_INVENTORY_GLOBAL UNIQUEIDENTIFIER 'ID_INVENTORY_GLOBAL')

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT TOP 1 
    @ID_STORE = I.ID_STORE, 
    @FULL = [FULL], 
    @DOC_DATE = DOC_DATE,
    @C_NAME = C.NAME,
    @S_NAME = S.NAME
FROM INVENTORY_SVED I(NOLOCK)
INNER JOIN STORE S(NOLOCK) ON I.ID_STORE = S.ID_STORE
INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

SELECT 
    C_NAME = @C_NAME,
    S_NAME = @S_NAME,
    DOC_DATE = I.DOC_DATE,
    DOC_NUM = I.DOC_NUM
FROM INVENTORY_SVED I(NOLOCK)
WHERE I.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

declare @PUC VARCHAR(20)
select @PUC = Value from sys_option(NOLOCK) where Code= 'INVENTORY_CALC_PRICE'

DECLARE @CONST VARCHAR(10)
SELECT TOP 1 @CONST = VALUE FROM SYS_OPTION WHERE CODE = 'INVENTORY_CALC_PRICE'

--select * FROM INVENTORY_SVED

select
--ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL,
L.ID_GOODS,
PRICE = L.PRICE_SUP - L.PVAT_SUP
,VAT_SUP = CASE WHEN @USE_VAT = 1 THEN L.VAT_SUP ELSE 0 END
					,l_m.ID_LOT_GLOBAL
					, L.INTERNAL_BARCODE, L.ID_STORE
				   ,OKEI_CODE = U.OKEI_CODE
				   ,UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')'
				   ,QUANTITY_DOC = SUM((L_M.QUANTITY_ADD - L_M.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR)
				   ,RET_SUM_DOC = SUM((l_m.QUANTITY_ADD - l_m.QUANTITY_SUB) * L.PRICE_SUP)--(CASE WHEN @PUC = 'SAL' THEN L.PRICE_SAL ELSE L.PRICE_SUP END))--chsa
				   ,RET_SUM_DOC_SUB_NDS = SUM((l_m.QUANTITY_ADD - l_m.QUANTITY_SUB) * (L.PRICE_SUP-L.PVAT_SUP))--(CASE WHEN @PUC = 'SAL' THEN L.PRICE_SAL-L.PVAT_SAL ELSE L.PRICE_SUP-L.PVAT_SUP END))--chsa
				INTO #LOT
				from LOT_MOVEMENT l_m(NOLOCK)
				INNER JOIN LOT L(NOLOCK) ON L.ID_lot_global= l_m.ID_LOT_GLOBAL --and l.ID_STORE = @ID_STORE--chsa
				INNER JOIN SCALING_RATIO SR(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
				INNER JOIN UNIT U(NOLOCK) ON SR.ID_UNIT = U.ID_UNIT
				WHERE (@FULL = 1 OR (@FULL = 0 
    AND EXISTS(SELECT TOP 1 1 FROM INVENTORY_VED IV
        INNER JOIN INVENTORY_VED_ITEM II ON II.ID_INVENTORY_VED_GLOBAL = IV.ID_INVENTORY_VED_GLOBAL
        WHERE IV.ID_INVENTORY_SVED_GLOBAL = @ID_INVENTORY_GLOBAL
            AND L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
		AND L.ID_STORE = IV.ID_STORE)))
AND DATE_OP < @DOC_DATE
AND EXISTS(SELECT TOP 1 1 FROM INVENTORY_VED II WHERE II.ID_STORE = L.ID_STORE 
	AND II.ID_INVENTORY_SVED_GLOBAL = @ID_INVENTORY_GLOBAL)
				group by L.ID_GOODS
				, L.PRICE_SUP - L.PVAT_SUP
				, CASE WHEN @USE_VAT = 1 THEN L.VAT_SUP ELSE 0 END
				, l_m.ID_LOT_GLOBAL
				, L.INTERNAL_BARCODE, L.ID_STORE
				, U.OKEI_CODE
				, U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')'
				HAVING (SUM(L_M.QUANTITY_ADD) - SUM(L_M.QUANTITY_SUB)) > 0

select
ISNULL(ID_LOT_GLOBAL, NEWID()) AS ID_LOT_GLOBAL,
ID_GOODS
    ,G_NAME 
	,G_CODE
	--,UNIT_NAME 
	--,OKEI_CODE 
	--,INTERNAL_BARCODE
    ,PRICE_SAL = SUM(PRICE_SAL)
    ,QUANTITY_FACT = SUM(QUANTITY_FACT)
    ,SUM_FACT = SUM(SUM_FACT)
    ,SUM_FACT_SUB_NDS = SUM(SUM_FACT_SUB_NDS)
    --,QUANTITY_DOC = SUM(QUANTITY_DOC)
    --,RET_SUM_DOC = SUM(RET_SUM_DOC)
    --,RET_SUM_DOC_SUB_NDS = SUM(RET_SUM_DOC_SUB_NDS)

into #ref
from 
	(
		select
		G.ID_GOODS, 
		INV_VI.ID_LOT_GLOBAL,
		   G_NAME = G.NAME
		   ,G_CODE = G.CODE
		   --,UNIT_NAME
		   --,OKEI_CODE
			,PRICE_SAL = INV_VI.PRICE_SUP-INV_VI.PVAT_SUP--(case @PUC when 'SUP' then INV_VI.PRICE_SUP else INV_VI.PRICE_SAL end)
			,QUANTITY_FACT = INV_VI.QUANTITY
			,SUM_FACT = INV_VI.SUM_SUP --(case @PUC when 'SUP' then INV_VI.SUM_SUP else INV_VI.SUM_SAL end)
			,SUM_FACT_SUB_NDS =INV_VI.SUM_SUP-INV_VI.SVAT_SUP -- (case @PUC when 'SUP' then INV_VI.SUM_SUP-INV_VI.SVAT_SUP else INV_VI.SUM_SAL-INV_VI.SVAT_SAL end)
			--,lm.INTERNAL_BARCODE
			--,ISNULL(QUANTITY_DOC, 0) as QUANTITY_DOC
			--,RET_SUM_DOC
			--,RET_SUM_DOC_SUB_NDS
		from INVENTORY_VED iv(NOLOCK)
		inner join INVENTORY_VED_ITEM INV_VI(NOLOCK) on INV_VI.ID_INVENTORY_VED_GLOBAL = iv.ID_INVENTORY_VED_GLOBAL 
		
		INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = INV_VI.ID_GOODS
		--LEFT JOIN #LOT lm on lm.ID_LOT_GLOBAL = INV_VI.ID_LOT_GLOBAL
		--	AND lm.ID_GOODS = INV_VI.ID_GOODS
		--	AND IV.ID_STORE = LM.ID_STORE
		--	AND lm.PRICE = INV_VI.PRICE_SUP-INV_VI.PVAT_SUP
		--	AND lm.VAT_SUP = INV_VI.VAT_SUP
		where ID_INVENTORY_SVED_GLOBAL = @ID_INVENTORY_GLOBAL
			
	) t
group by 
ID_LOT_GLOBAL,
ID_GOODS,
G_NAME, G_CODE 
--,UNIT_NAME
--,OKEI_CODE
--,INTERNAL_BARCODE
--HAVING (SUM(QUANTITY_FACT)) > 0
ORDER BY G_NAME

--select SUM(QUANTITY_FACT), SUM(SUM_FACT)
----,SUM(QUANTITY_DOC)
----,SUM(RET_SUM_DOC) 
--from #ref

DECLARE @OKEI_CODE VARCHAR(40), @NAME VARCHAR(100)
SELECT TOP 1 @OKEI_CODE = OKEI_CODE, @NAME = NAME FROM UNIT WHERE MNEMOCODE = 'BOX' 

select 
G_NAME 
,G_CODE
,ISNULL(UNIT_NAME, @NAME+'(1/1)') AS UNIT_NAME
,ISNULL(OKEI_CODE, @OKEI_CODE) AS OKEI_CODE
,ISNULL(INTERNAL_BARCODE,'') AS INTERNAL_BARCODE
,PRICE_SAL
,QUANTITY_FACT
,SUM_FACT
,SUM_FACT_SUB_NDS
,ISNULL(QUANTITY_DOC, 0) AS QUANTITY_DOC
,ISNULL(RET_SUM_DOC, 0) AS RET_SUM_DOC
,ISNULL(RET_SUM_DOC_SUB_NDS, 0) AS RET_SUM_DOC_SUB_NDS
from #ref r
FULL OUTER JOIN #LOT l
ON r.ID_LOT_GLOBAL = l.ID_LOT_GLOBAL AND r.ID_GOODS = l.ID_GOODS



RETURN
GO

--exec REPEX_INVENTORY_OPIS_EX @XMLPARAM = N'<XML><ID_INVENTORY_GLOBAL>f1dddad5-443d-43c9-a9f8-d345f5d66669</ID_INVENTORY_GLOBAL></XML>'

--exec REPEX_INVENTORY_OPIS_EX @XMLPARAM = N'<XML><ID_INVENTORY_GLOBAL>348EC6CF-8E5C-44AA-9473-CEEC06D9806F</ID_INVENTORY_GLOBAL></XML>'
/*
select * from inventory_sved
   G_NAME = G.NAME,
   G_CODE = G.CODE,
   OKEI_CODE = U.OKEI_CODE,
   UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
   PRICE_SAL = (case @PUC when 'SUP' then INV_VI.PRICE_SUP else INV_VI.PRICE_SAL end),
   QUANTITY_FACT = ROUND(INV_VI.QUANTITY, 2),
   SUM_FACT = ROUND((case @PUC when 'SUP' then INV_VI.PRICE_SUP else INV_VI.PRICE_SAL end) * INV_VI.QUANTITY, 2),
   SUM_FACT_SUB_NDS = ROUND((case @PUC when 'SUP' then INV_VI.PRICE_SUP-INV_VI.PVAT_SUP else INV_VI.PRICE_SAL-INV_VI.PVAT_SAL end) * INV_VI.QUANTITY, 2),
   INTERNAL_BARCODE = L.INTERNAL_BARCODE,
   QUANTITY_DOC = ROUND(SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR), 2),--chsa
   RET_SUM_DOC = ROUND(SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * (CASE WHEN @PUC = 'SAL' THEN L.PRICE_SAL ELSE L.PRICE_SUP END)), 2),--chsa
   RET_SUM_DOC_SUB_NDS = ROUND(SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * (CASE WHEN @PUC = 'SAL' THEN L.PRICE_SAL-L.PVAT_SAL ELSE L.PRICE_SUP-L.PVAT_SUP END)), 2)--chsa

FROM INVENTORY_SVED INV_S
INNER JOIN INVENTORY_VED INV_V ON INV_V.ID_INVENTORY_SVED_GLOBAL = INV_S.ID_INVENTORY_GLOBAL
INNER JOIN INVENTORY_VED_ITEM INV_VI ON INV_VI.ID_INVENTORY_VED_GLOBAL = INV_V.ID_INVENTORY_VED_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = INV_VI.ID_GOODS
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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InventoryOpisEx.InventoryOpisEx'