SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_IM_NDS') IS NULL EXEC('CREATE PROCEDURE REPEX_IM_NDS AS RETURN')
GO
ALTER PROCEDURE REPEX_IM_NDS
	(@XMLPARAM NTEXT) AS 

DECLARE @HDOC INT, @ID_INTERFIRM_MOVING BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INTERFIRM_MOVING = ID_INTERFIRM_MOVING
	FROM OPENXML(@HDOC, '/XML') WITH(ID_INTERFIRM_MOVING BIGINT 'ID_INTERFIRM_MOVING')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	case
	  when Full_NAME is null then NAME 
	  when Full_NAME = '' then NAME 
	  else Full_NAME
	 end AS COMPANY
FROM CONTRACTOR WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()

SELECT
	MNEMOCODE = MNEMOCODE + ' ' + COMMENT,
	DATE,
	STORENAMEFROM = (SELECT TOP 1 case 
	                                  when C.FULL_NAME is null then C.NAME
	                                  when C.FULL_NAME = '' then C.NAME
	                                  else  C.FULL_NAME
	                                end  + ' - ' + S.NAME FROM STORE AS S  
		LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
		WHERE S.ID_STORE = M.ID_STORE_FROM_MAIN),
	STORENAMETO = (SELECT TOP 1 case 
	                                  when C.FULL_NAME is null then C.NAME
	                                  when C.FULL_NAME = '' then C.NAME
	                                  else  C.FULL_NAME
	                                end + ' - ' + S.NAME FROM STORE AS S 
		LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR 
		WHERE S.ID_STORE = M.ID_STORE_TO_MAIN),
	DOC = M.BASE_DOC_TEXT
FROM INTERFIRM_MOVING AS M WHERE ID_INTERFIRM_MOVING = @ID_INTERFIRM_MOVING

SELECT
	GOODSNAME = G.NAME + ' ( ' + (SELECT TOP 1 P.NAME FROM PRODUCER AS P WHERE G.ID_PRODUCER = P.ID_PRODUCER) + ' ) ',
	SERIESNUMBER = SER.SERIES_NUMBER,
	QUANTITY = M.QUANTITY,
	UNITNAME = DBO.FN_SCALE_NAME(L.ID_SCALING_RATIO),
	PRICE_SUPPLIER = L.PRICE_SUP - L.PVAT_SUP,
	L.VAT_SUP,
	L.PVAT_SUP,
	PRICE_SUMMA = QUANTITY * (L.PRICE_SUP - L.PVAT_SUP),
	PRICE_SUMMA_VAT = M.SUM_SUPPLIER,
	PRICE_SAL = L.PRICE_SAL - L.PVAT_SAL,
	L.VAT_SAL,
	L.PVAT_SAL,
	PRICE_SAL_VAT = L.PRICE_SAL,
	PRICE_SUMM_SALE = PVAT_RETAIL
INTO #TEMP
FROM INTERFIRM_MOVING_ACCEPTANCE_ACT_ITEM AS M
	INNER JOIN INTERFIRM_MOVING_ACCEPTANCE_ACT IMA ON IMA.ID_INTERFIRM_MOVING_ACCEPTANCE_ACT_GLOBAL = M.ID_INTERFIRM_MOVING_ACCEPTANCE_ACT_GLOBAL
	INNER JOIN INTERFIRM_MOVING IM ON IM.ID_INTERFIRM_MOVING_GLOBAL = IMA.ID_INTERFIRM_MOVING_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT = M.ID_LOT_FROM
	LEFT JOIN GOODS AS G ON G.ID_GOODS = L.ID_GOODS
	LEFT JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	LEFT JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT WHERE ID_INTERFIRM_MOVING = @ID_INTERFIRM_MOVING
AND M.QUANTITY > 0
ORDER BY GOODSNAME

SELECT * FROM #TEMP

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

SELECT 
	VAT_SUP10 = ISNULL((SELECT SUM(PVAT_SUP) FROM #TEMP WHERE VAT_SUP = 10 GROUP BY VAT_SUP), 0),
	VAT_SUP18 = ISNULL((SELECT SUM(PVAT_SUP) FROM #TEMP WHERE VAT_SUP = 18 GROUP BY VAT_SUP), 0),
	VAT_SAL10 = ISNULL((SELECT SUM(PVAT_SAL) FROM #TEMP WHERE VAT_SAL = 10 GROUP BY VAT_SAL), 0),
	VAT_SAL18 = ISNULL((SELECT SUM(PVAT_SAL) FROM #TEMP WHERE VAT_SAL = 18 GROUP BY VAT_SAL), 0)
RETURN 0
GO

--exec REPEX_IM_NDS '<XML><ID_INTERFIRM_MOVING>212</ID_INTERFIRM_MOVING></XML>'