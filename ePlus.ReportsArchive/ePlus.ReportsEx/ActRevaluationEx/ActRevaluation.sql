SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_ACT_REVALUATION') IS NULL EXEC('CREATE PROCEDURE REPEX_ACT_REVALUATION AS RETURN')
GO
ALTER PROCEDURE REPEX_ACT_REVALUATION
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT , @ID_ACT_REVALUATION_GLOBAL UNIQUEIDENTIFIER

EXEC	SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT	TOP 1 @ID_ACT_REVALUATION_GLOBAL = ID_ACT_REVALUATION_GLOBAL
	FROM	OPENXML(@HDOC, '/XML') WITH (ID_ACT_REVALUATION_GLOBAL UNIQUEIDENTIFIER 'ID_ACT_REVALUATION_GLOBAL')
EXEC	SP_XML_REMOVEDOCUMENT @HDOC

SELECT TOP 1 
	ACT_REVALUATION_NUMBER = AR.MNEMOCODE,
	ACT_REVALUATION_DATE = CONVERT(VARCHAR, AR.DATE , 104),
	DOC = AR.BASE_DOC_TEXT,
	STORE_NAME = S.NAME + ' (' + C.NAME + ')',
	COMPANY = (SELECT case
	                    when C.FULL_NAME is null then C.NAME
	                    when C.FULL_NAME = '' then C.NAME
	                    else C.FULL_NAME
	                  end  
	                     FROM CONTRACTOR C WHERE C.ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF())
FROM ACT_REVALUATION2 AS AR
	INNER JOIN STORE AS S ON AR.ID_STORE = S.ID_STORE
	INNER JOIN CONTRACTOR AS C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
WHERE AR.ID_ACT_REVALUATION2_GLOBAL = @ID_ACT_REVALUATION_GLOBAL

SELECT
	GOODS_NAME = G.NAME + ' (' + P.NAME + ')',
	BEST_BEFORE = SO.BEST_BEFORE,
	BEST_BEFORE_STRING = CONVERT(VARCHAR , SO.BEST_BEFORE , 4),
	SERIES_NUMBER = SO.SERIES_NUMBER,
	QUANTITY = CAST(ARI.QUANTITY AS DECIMAL(18 , 2)),
	SCALINGMULTIPLY = CAST(CAST(SR.NUMERATOR AS DECIMAL(18 , 2)) / SR.DENOMINATOR AS DECIMAL(18 , 2)),
	UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',		
	PRICE_SUPPLIER_VAT_OLD = LO.PRICE_SUP,
	SUMM_PRICE_SUPPLIER_VAT_OLD = LO.PRICE_SUP * ARI.QUANTITY ,
	PRICE_SUPPLIER_VAT_NEW = LN.PRICE_SUP,
	OLD_PRICE_RETAIL = LO.PRICE_SAL,
	NEW_PRICE_RETAIL = ARI.RETAIL_PRICE_VAT,
	KOEF_TAX_RATE = CONVERT(MONEY, LO.VAT_SAL) / 100 + 1
FROM ACT_REVALUATION2_ITEM AS ARI 
	INNER JOIN ACT_REVALUATION2 AS AR ON AR.ID_ACT_REVALUATION2_GLOBAL = ARI.ID_ACT_REVALUATION2_GLOBAL
	LEFT JOIN LOT AS LO ON LO.ID_LOT_GLOBAL = ARI.ID_LOT_GLOBAL
	LEFT JOIN LOT AS LN ON LN.ID_TABLE = 13 AND LN.ID_DOCUMENT_ITEM = ARI.ID_ACT_REVALUATION2_ITEM_GLOBAL and LN.ID_DOCUMENT = AR.ID_ACT_REVALUATION2_GLOBAL
	LEFT JOIN SERIES AS SO ON SO.ID_SERIES = LO.ID_SERIES
	INNER JOIN GOODS AS G ON G.ID_GOODS = LO.ID_GOODS
	INNER JOIN PRODUCER AS P ON P.ID_PRODUCER = G.ID_PRODUCER
	INNER JOIN SCALING_RATIO AS SR ON SR.ID_SCALING_RATIO = LO.ID_SCALING_RATIO
	INNER JOIN UNIT AS U ON U.ID_UNIT = SR.ID_UNIT
WHERE AR.ID_ACT_REVALUATION2_GLOBAL = @ID_ACT_REVALUATION_GLOBAL
ORDER BY GOODS_NAME

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO