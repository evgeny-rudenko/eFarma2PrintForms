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
INNER JOIN STORE S ON I.ID_STORE = S.ID_STORE
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

SELECT 
    C_NAME = @C_NAME,
    S_NAME = @S_NAME,
    DOC_DATE = I.DOC_DATE,
    DOC_NUM = I.DOC_NUM
FROM INVENTORY_SVED I
WHERE I.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL

SELECT 
   G_NAME = G.NAME,
   G_CODE = G.CODE,
   OKEI_CODE = U.OKEI_CODE,
   UNIT_NAME = U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
   PRICE_SAL = INV_VI.PRICE_SAL,
   QUANTITY_FACT = INV_VI.QUANTITY,
   SUM_FACT = INV_VI.PRICE_SAL * INV_VI.QUANTITY
FROM INVENTORY_SVED INV_S
INNER JOIN INVENTORY_VED INV_V ON INV_V.ID_INVENTORY_SVED_GLOBAL = INV_S.ID_INVENTORY_GLOBAL
INNER JOIN INVENTORY_VED_ITEM INV_VI ON INV_VI.ID_INVENTORY_VED_GLOBAL = INV_V.ID_INVENTORY_VED_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = INV_VI.ID_GOODS
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = INV_VI.ID_SCALING_RATIO
INNER JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
where INV_S.ID_INVENTORY_GLOBAL = @ID_INVENTORY_GLOBAL
--GROUP BY G.NAME,G.CODE,U.OKEI_CODE,U.NAME + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')'
ORDER BY G.NAME

RETURN
GO

--exec REPEX_INVENTORY_OPIS_EX @XMLPARAM = N'<XML><ID_INVENTORY_GLOBAL>f1dddad5-443d-43c9-a9f8-d345f5d66669</ID_INVENTORY_GLOBAL></XML>'