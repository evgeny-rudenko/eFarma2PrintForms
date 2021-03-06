SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_IMPORT_REMAINS_OPIS_EX') IS NULL EXEC('CREATE PROCEDURE REPEX_IMPORT_REMAINS_OPIS_EX AS RETURN')
GO
ALTER PROCEDURE REPEX_IMPORT_REMAINS_OPIS_EX
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT
DECLARE @ID_IMPORT_REMAINS_GLOBAL UNIQUEIDENTIFIER
DECLARE @ID_STORE BIGINT
DECLARE @DOC_DATE DATETIME
DECLARE @FULL BIT
DECLARE @C_NAME VARCHAR(120), @S_NAME VARCHAR(120)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT TOP 1 
        @ID_IMPORT_REMAINS_GLOBAL = ID_IMPORT_REMAINS_GLOBAL 
    FROM OPENXML(@HDOC , '/XML') WITH(
        ID_IMPORT_REMAINS_GLOBAL UNIQUEIDENTIFIER 'ID_IMPORT_REMAINS_GLOBAL')

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT TOP 1 
    @ID_STORE = IR.ID_STORE, 
    @DOC_DATE = DOCUMENT_DATE,
    @C_NAME = C.NAME,
    @S_NAME = S.NAME
FROM IMPORT_REMAINS IR(NOLOCK)
INNER JOIN STORE S ON IR.ID_STORE = S.ID_STORE
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE ID_IMPORT_REMAINS_GLOBAL = @ID_IMPORT_REMAINS_GLOBAL

SELECT 
    C_NAME = @C_NAME,
    S_NAME = @S_NAME,
    DOC_DATE = I.DOCUMENT_DATE,
    DOC_NUM = I.MNEMOCODE
FROM IMPORT_REMAINS I
WHERE I.ID_IMPORT_REMAINS_GLOBAL = @ID_IMPORT_REMAINS_GLOBAL

SELECT 
   G_NAME = G.NAME,
   G_CODE = G.CODE,
   OKEI_CODE = U.OKEI_CODE,
   UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
   PRICE_SAL = IRI.RETAIL_PRICE_VAT,
   QUANTITY_FACT = IRI.QUANTITY,--LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES,
   SUM_FACT = IRI.RETAIL_PRICE_VAT * IRI.QUANTITY--(LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES) * L.PRICE_SAL
FROM IMPORT_REMAINS IR
INNER JOIN IMPORT_REMAINS_ITEM IRI ON IR.ID_IMPORT_REMAINS_GLOBAL = IRI.ID_IMPORT_REMAINS_GLOBAL
LEFT JOIN LOT_MOVEMENT LM ON LM.ID_DOCUMENT_ITEM = IRI.ID_IMPORT_REMAINS_GLOBAL
LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = IRI.ID_GOODS
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = IRI.ID_SCALING_RATIO
INNER JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
WHERE IR.ID_IMPORT_REMAINS_GLOBAL = @ID_IMPORT_REMAINS_GLOBAL
--GROUP BY G.NAME,G.CODE,U.OKEI_CODE,U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')'
ORDER BY G.NAME

RETURN
GO

--exec REPEX_IMPORT_REMAINS_OPIS_EX @XMLPARAM = N'<XML><ID_INVENTORY_GLOBAL>f1dddad5-443d-43c9-a9f8-d345f5d66669</ID_INVENTORY_GLOBAL></XML>'
--exec REPEX_IMPORT_REMAINS_OPIS_EX @XMLPARAM = N'<XML><ID_IMPORT_REMAINS_GLOBAL>bf54726a-5f50-4002-bb10-f6c4333ca1fd</ID_IMPORT_REMAINS_GLOBAL></XML>'