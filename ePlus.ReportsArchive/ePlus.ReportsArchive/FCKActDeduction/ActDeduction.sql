SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REP_FCK_ACT_DEDUCTION') IS NULL EXEC('CREATE PROCEDURE REP_FCK_ACT_DEDUCTION AS RETURN')
GO

ALTER PROCEDURE [dbo].[REP_FCK_ACT_DEDUCTION]
    @XMLPARAM NTEXT AS
    
    DECLARE @HDOC INT
    DECLARE @DATE_FROM DATETIME, @DATE_TO DATETIME, @ID_ACT_DEDUCTION BIGINT , @ID_ACT_DEDUCTION_GLOBAL UNIQUEIDENTIFIER
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    
  --  SELECT 
  --      @DATE_FROM = DATE_FROM,
  --      @DATE_TO = DATE_TO
  --  FROM OPENXML(@HDOC, 'XML', 2) WITH (
  --      DATE_FROM DATETIME,
  --      DATE_TO DATETIME
  --  )
    
    
  	SELECT	TOP 1 @ID_ACT_DEDUCTION = ID_ACT_DEDUCTION,
  	              @ID_ACT_DEDUCTION_GLOBAL = ID_ACT_DEDUCTION_GLOBAL
	FROM	OPENXML(@HDOC , '/XML') 
	WITH(ID_ACT_DEDUCTION BIGINT 'ID_ACT_DEDUCTION',
	     ID_ACT_DEDUCTION_GLOBAL UNIQUEIDENTIFIER 'ID_ACT_DEDUCTION_GLOBAL')

        
    EXEC SP_XML_REMOVEDOCUMENT @HDOC


--EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT
--EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT

SELECT
	AD_NUMBER = AD.MNEMOCODE ,
	AD_DATE = CONVERT(VARCHAR , AD.DATE , 104) ,
	AD_DATE1 = AD.DATE ,
	AD_COMMENT = AD.COMMENT ,
	AD_COMPANY = (SELECT ISNULL(MAX(C.NAME) , '') FROM CONTRACTOR C(NOLOCK) WHERE C.ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()),
	AD_STORE = st.NAME,
	AD_MNEMOCODE = AD.MNEMOCODE,
	AD_BASE_DOCUMENT_NAME = AD.BASE_DOCUMENT_NAME	
FROM	ACT_DEDUCTION AD(NOLOCK)
left join store st on (st.id_store = AD.id_store)
WHERE	AD.ID_ACT_DEDUCTION = @ID_ACT_DEDUCTION OR AD.ID_ACT_DEDUCTION_GLOBAL = @ID_ACT_DEDUCTION_GLOBAL



SELECT 
    D.ID_ACT_DEDUCTION_GLOBAL as ID_ACT_DEDUCTION_GLOBAL, 
    D.MNEMOCODE, 
    D.DOCUMENT_STATE, 
    D.DATE, 
    D.DATE_MODIFIED, 
    D.DATE_MODIFIED_STATE, 
    D.COMMENT, 
    --ID_FOLDER, 
--    D.ID_STORE,
    D.STORE_NAME,
--    ID_BASE_DOCUMENT_TYPE,
--    BASE_DOCUMENT_TYPE_NAME,
--    ID_BASE_DOCUMENT, 
    D.RETAIL_SUM, 
    D.RETAIL_SUM_VAT, 
--    DEDUCTION_REASON,
    D.DEDUCTION_REASON_NAME,
    D.BASE_DOCUMENT_NAME,
--    REPL_STATUS = (SELECT TOP 1 STATUS FROM REPLICATION_LOG_ADD RLA WHERE RLA.ID_ROW_GLOBAL = D.ID_ACT_DEDUCTION_GLOBAL),
--
    IT_D.QUANTITY as IT_D_QUANTITY, 
    IT_D.ID_LOT_GLOBAL as IT_D_ID_LOT_GLOBAL, 
--    PRICE_VAT, 
    IT_D.PRICE_SUP as IT_D_PRICE_SUP,
--    RETAIL_PRICE, 
    IT_D.PVAT_ACC as IT_D_PVAT_ACC,
--    RETAIL_PRICE_VAT, 
    IT_D.PRICE_ACC as IT_D_PRICE_ACC,
--    RETAIL_SUM, 
    IT_D.SVAT_ACC as IT_D_SVAT_ACC,
--    RETAIL_SUM_VAT, 
    IT_D.SUM_ACC as IT_D_SUM_ACC,
    IT_D.ID_GOODS as IT_D_ID_GOODS, 
    IT_D.LOT_MNEMOCODE as IT_D_LOT_MNEMOCODE, 
    IT_D.GOODS_NAME as IT_D_GOODS_NAME, 
    IT_D.SCALING_RATIO_NAME as IT_D_SCALING_RATIO_NAME, 
    IT_D.SERIES_NUMBER as IT_D_SERIES_NUMBER, 
    IT_D.BEST_BEFORE as IT_D_BEST_BEFORE,
    L.INCOMING_NUM as L_INCOMING_NUM,
    L.INCOMING_DATE as L_INCOMING_DATE,
    ctr.NAME as CTR_NAME
FROM MV_ACT_DEDUCTION D
inner join  MV_ACT_DEDUCTION_ITEM IT_D on (IT_D.ID_ACT_DEDUCTION_GLOBAL = D.ID_ACT_DEDUCTION_GLOBAL)
inner join LOT L on (L.ID_LOT_GLOBAL = IT_D.ID_LOT_GLOBAL)
left join contractor ctr on (ctr.ID_CONTRACTOR = L.ID_SUPPLIER)
--WHERE DATE BETWEEN @DATE_FROM AND @DATE_TO


RETURN 0
GO
