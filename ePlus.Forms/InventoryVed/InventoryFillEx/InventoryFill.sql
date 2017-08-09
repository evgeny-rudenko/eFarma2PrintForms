SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_INVENTORY_FILL') IS NULL EXEC('CREATE PROCEDURE REPEX_INVENTORY_FILL AS RETURN')
GO
ALTER PROCEDURE REPEX_INVENTORY_FILL(
    @XMLPARAM NTEXT
) AS

DECLARE @HDOC INT, @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
FROM OPENXML(@HDOC, '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC


SELECT
	I.DOC_NUM,
	S.[NAME] AS STORE,
	C.[NAME] AS CONTRACTOR
FROM INVENTORY_VED I
    INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.ID_INVENTORY_VED_GLOBAL = @ID_GLOBAL

SELECT
	GOODS_NAME,
    PRODUCER_NAME,
    SCALING_RATIO_NAME,	
	PRICE_SUP,
	PRICE_SAL,
	SERIES
FROM MV_INVENTORY_VED_ITEM
WHERE ID_INVENTORY_VED_GLOBAL = @ID_GLOBAL
ORDER BY GOODS_NAME

RETURN 0
GO