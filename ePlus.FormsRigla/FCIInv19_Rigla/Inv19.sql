SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INV19_RIGLA_Alter201104') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INV19_RIGLA_Alter201104 AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INV19_RIGLA_Alter201104
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
-- IS_SAL = 0 - ”◊»“€¬¿ﬁ“—ﬂ —»—“≈ÃÕ€≈  ŒÕ—“¿Õ“€ (œŒ ”ÃŒÀ◊. –Œ«Õ. ÷≈Õ€)
-- IS_SAL = 1 - –Œ«Õ»◊Õ€≈ ÷≈Õ€
-- IS_SAL = 2 - Œœ“Œ¬€≈ ÷≈Õ€
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

IF @DOC_DATE < CONVERT(datetime, '2011/04/01')
Begin
	--exec REPEX_INV19_RIGLA_BEFORE201104 @XMLPARAM
	select 
		CONTRACTOR = NULL,
		STORE = NULL,
		DOC_NUM = NULL,
		DOC_DATE = NULL
	select 
		GOODS_NAME = NULL,
		GOODS_CODE = NULL,
		OKEI_CODE = NULL,
		UNIT_NAME = NULL,
		Q_Add = 0,
		SumPrice_Add = 0,
		SumVat_Add = 0,
		
		Q_Sub = 0,
		SumPrice_Sub = 0,
		SumVat_Sub	= 0,
		
		Q_Rev = 0,
		SumPrice_Rev = 0,
		SumVat_Rev = 0
	SELECT
		DIR = NULL,
		BUH = NULL
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
	l.ID_GOODS,
	QUANTITY = SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR),
	SUM_PRICE = ABS(
		CASE WHEN ((@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1) THEN ISNULL(sum(SUM_ACC), 0)
		ELSE ISNULL(sum(SUM_SUP), 0) END),
	SUM_SVAT =  ABS(
		CASE WHEN ((@CONST = 'SAL' AND @IS_SAL = 0) OR @IS_SAL = 1) THEN ISNULL(sum(SUM_ACC-SVAT_ACC), 0)
        ELSE ISNULL(sum(SUM_SUP-SVAT_SUP), 0) END),
	[TYPE] = case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'IZL' else 'NEDOS' end,
	[Sign] = SIGN(lm.QUANTITY_ADD - lm.QUANTITY_SUB),
	ID_STORE
into #Tmp
from lot_movement lm (NOLOCK)
inner join LOT l (NOLOCK) on l.ID_LOT_GLOBAL = lm.ID_LOT_GLOBAL
INNER JOIN SCALING_RATIO SR (NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
where lm.ID_DOCUMENT = @ID_GLOBAL
	and lm.OP <> 'SUB_RES'
group by l.ID_GOODS,
	case when (lm.QUANTITY_ADD - lm.QUANTITY_SUB) > 0 then 'IZL' else 'NEDOS' end,
	CASE WHEN @USE_VAT = 1 THEN CASE WHEN @CONST_VAT = 'SUP' THEN L.VAT_SUP ELSE L.VAT_SAL END ELSE 0 END,
	ID_STORE,
	SIGN(lm.QUANTITY_ADD - lm.QUANTITY_SUB)
having SUM((LM.QUANTITY_ADD - LM.QUANTITY_SUB) * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR) <> 0

select
	GOODS_NAME = g.NAME,
	GOODS_CODE = g.CODE,
	OKEI_CODE = @OKEI_CODE,
	UNIT_NAME = '1/1' + ' ' + @NAME,

	Q_Add,
	SumPrice_Add,
	SumVat_Add,

	ABS(Q_Sub) AS Q_Sub,
	SumPrice_Sub,
	SumVat_Sub,

	Q_Rev = (Q_Add + Q_Sub),
	SumPrice_Rev = ISNULL(SumPrice_Add - SumPrice_Sub, 0),
	SumVat_Rev = ISNULL(SumVat_Add - SumVat_Sub, 0)
into #Res
from (
	select 
		Q_Add = (select ISNULL(sum(QUANTITY),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL'),
		SumPrice_Add = (select ISNULL(sum(SUM_PRICE),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL'),
		SumVat_Add = (select ISNULL(sum(SUM_SVAT),0) from #Tmp t_a where t_a.id_goods = t.id_goods and [TYPE] = 'IZL'),

		Q_Sub = (select ISNULL(sum(QUANTITY),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS'),
		SumPrice_Sub = (select ISNULL(sum(SUM_PRICE),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS'),
		SumVat_Sub = (select ISNULL(sum(SUM_SVAT),0) from #Tmp t_s where t_s.id_goods = t.id_goods and [TYPE] = 'NEDOS'),
		t.id_goods
	from #Tmp t
	group by t.ID_GOODS
	) t
inner join goods g on g.ID_GOODS = t.ID_GOODS

select
	r.GOODS_NAME,
	r.GOODS_CODE,
	r.OKEI_CODE,
	r.UNIT_NAME,

	-- ÓÚÓ·‡Ê‡ÂÏ ÒÓ Ò‚ÂÚÍÓÈ
	[Q_Add] = case when r.Q_Add - r.Q_Sub >= 0 then r.Q_Add - r.Q_Sub else 0 end,
	[SumPrice_Add] = case when r.SumPrice_Add - r.SumPrice_Sub > 0 then r.SumPrice_Add - r.SumPrice_Sub else 0 end,
	[SumVat_Add] = case when r.SumVat_Add - r.SumVat_Sub > 0 then r.SumVat_Add - r.SumVat_Sub else 0 end,

	-- ÓÚÓ·‡Ê‡ÂÏ ÒÓ Ò‚ÂÚÍÓÈ
	[Q_Sub] = case when r.Q_Sub - r.Q_Add >= 0 then r.Q_Sub - r.Q_Add else 0 end,
	[SumPrice_Sub] = case when r.SumPrice_Sub - r.SumPrice_Add > 0 then r.SumPrice_Sub - r.SumPrice_Add else 0 end,
	[SumVat_Sub] = case when r.SumVat_Sub - r.SumVat_Add > 0 then r.SumVat_Sub - r.SumVat_Add else 0 end,

	r.Q_Rev,
	r.SumPrice_Rev,
	r.SumVat_Rev
from #Res r
where r.Q_Rev <> 0 or r.SumPrice_Rev <> 0 or r.SumVat_Rev <> 0
order by r.GOODS_NAME

SELECT
	DIR = c.DIRECTOR_FIO,
	BUH = c.BUH_FIO
FROM dbo.CONTRACTOR c (nolock)
WHERE c.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO
/*
exec REPEX_INV19_RIGLA_Alter201104 @xmlParam = N'
<XML>
<ID_GLOBAL>D65DBA9B-9083-4EF1-962F-F8110DA67A31</ID_GLOBAL>
<IS_SAL>0</IS_SAL></XML>'
*/
--select * from INVENTORY_SVED where ID_INVENTORY_GLOBAL = '821AEB3C-7903-44AC-94CC-E49F35D7D4C3'
--select * from lot_movement where QUANTITY_RES<0 and CODE_OP='INVENTORY_SVED' and OP='SUB_RES'