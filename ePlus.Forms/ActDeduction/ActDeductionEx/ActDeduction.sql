SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_ACT_DEDUCTION') IS NULL EXEC('CREATE PROCEDURE REPEX_ACT_DEDUCTION AS RETURN')
GO
ALTER PROCEDURE REPEX_ACT_DEDUCTION
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT , @ID_ACT_DEDUCTION BIGINT , @ID_ACT_DEDUCTION_GLOBAL UNIQUEIDENTIFIER
		
EXEC	SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
		
	SELECT	TOP 1 @ID_ACT_DEDUCTION = ID_ACT_DEDUCTION , @ID_ACT_DEDUCTION_GLOBAL = ID_ACT_DEDUCTION_GLOBAL
	FROM	OPENXML(@HDOC , '/XML') WITH(ID_ACT_DEDUCTION BIGINT 'ID_ACT_DEDUCTION' , ID_ACT_DEDUCTION_GLOBAL UNIQUEIDENTIFIER 'ID_ACT_DEDUCTION_GLOBAL')
		
EXEC	SP_XML_REMOVEDOCUMENT @HDOC
		
SELECT	AD_NUMBER = AD.MNEMOCODE ,
	AD_DATE = CONVERT(VARCHAR , AD.DATE , 104) ,
	AD_DATE1 = AD.DATE ,
	AD_COMMENT = AD.COMMENT ,
	AD_COMPANY = (SELECT case 
	                       when C.FULL_NAME is null then C.NAME
	                       when C.FULL_NAME = '' then C.NAME
	                       else C.FULL_NAME
	                     end 
	                FROM CONTRACTOR C(NOLOCK) WHERE C.ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF())
FROM	ACT_DEDUCTION AD(NOLOCK)
WHERE	AD.ID_ACT_DEDUCTION = @ID_ACT_DEDUCTION OR AD.ID_ACT_DEDUCTION_GLOBAL = @ID_ACT_DEDUCTION_GLOBAL
		
SELECT	G_ID = G.ID_GOODS,
	G_MODELCODE = G.MNEMOCODE,
	G_RUSNAME = G.NAME,
	G_SERIALNUMBER = SN.SERIES_NUMBER,
	G_BESTBEFORE = CONVERT(VARCHAR , SN.BEST_BEFORE , 104),
	G_SCALINGNAME = DBO.FN_SCALE_NAME(L.ID_SCALING_RATIO) ,
	ADI_QUANTITY = ADI.QUANTITY,
	ADI_SCALINGMULTIPLY = SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR),
	ADI_PRICESUPPLIER = ADI.PRICE_SUP,
	ADI_PRICERETAIL = ADI.PRICE_ACC,
	ADI_PRICERETVAT = ADI.PVAT_ACC,
	ADI_SUMSUPPLIER = 0,
	ADI_SUMRETAIL = ADI.SUM_ACC,
	ADI_SUMRETVAT = ADI.SVAT_ACC,
	ADI_COMMENT = (SELECT ISNULL(MAX(EV.DESCRIPTION) , '') FROM ENUMERATION_VALUE EV(NOLOCK) WHERE AD.DEDUCTION_REASON = EV.VALUE)
FROM ACT_DEDUCTION_ITEM ADI(NOLOCK) 
LEFT JOIN ACT_DEDUCTION AD(NOLOCK) ON AD.ID_ACT_DEDUCTION_GLOBAL = ADI.ID_ACT_DEDUCTION_GLOBAL 
LEFT JOIN GOODS G(NOLOCK) ON G.ID_GOODS = ADI.ID_GOODS 
LEFT JOIN LOT L(NOLOCK) ON L.ID_LOT_GLOBAL = ADI.ID_LOT_GLOBAL
LEFT JOIN SERIES SN(NOLOCK) ON SN.ID_SERIES = L.ID_SERIES
LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
WHERE	AD.ID_ACT_DEDUCTION = @ID_ACT_DEDUCTION OR AD.ID_ACT_DEDUCTION_GLOBAL = @ID_ACT_DEDUCTION_GLOBAL
ORDER	BY G.NAME
		
SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)
		
		
RETURN 0
GO
