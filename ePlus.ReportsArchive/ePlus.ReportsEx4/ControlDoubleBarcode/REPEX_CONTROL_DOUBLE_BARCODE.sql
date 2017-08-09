SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO
IF OBJECT_ID('DBO.REPEX_CONTROL_DOUBLE_BARCODE') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REPEX_CONTROL_DOUBLE_BARCODE AS RETURN')
    GRANT EXECUTE ON DBO.REPEX_CONTROL_DOUBLE_BARCODE TO PUBLIC
END
GO
ALTER PROCEDURE DBO.REPEX_CONTROL_DOUBLE_BARCODE
    @XMLPARAM NTEXT
AS

DECLARE @HDOC INT
DECLARE @CH_STOCK BIT, @CH_CODE BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
SELECT TOP 1 @CH_STOCK = CH_STOCK, @CH_CODE = CH_CODE
FROM OPENXML(@HDOC, '/XML') WITH(CH_STOCK BIT 'CH_STOCK', CH_CODE BIT 'CH_CODE')

EXEC SP_XML_REMOVEDOCUMENT @HDOC

DECLARE @CONTRACTOR_SELF BIGINT
SET @CONTRACTOR_SELF = [DBO].FN_CONST_CONTRACTOR_SELF()

SELECT [ID] = IDENTITY(INT, 1, 1),
    ID_LOT = L.ID_LOT,
    ID_LOT_D = LD.ID_LOT,
    INTERNAL_BARCODE = L.INTERNAL_BARCODE, 
    CODE = G.CODE, 
    CODE_D = GD.CODE,
    GOODS_NAME = G.[NAME],
    GOODS_NAME_D = GD.[NAME], 
    LOT_NAME = L.LOT_NAME, 
    LOT_NAME_D = LD.LOT_NAME, 
    UNIT_NAME = U.[NAME] + '(' + CAST(SR.NUMERATOR AS VARCHAR) + '/' + CAST(SR.DENOMINATOR AS VARCHAR) + ')',
    QUANTITY_REM = L.QUANTITY_REM,
    UNIT_NAME_D = UD.[NAME] + '(' + CAST(SRD.NUMERATOR AS VARCHAR) + '/' + CAST(SRD.DENOMINATOR AS VARCHAR) + ')',
    QUANTITY_REM_D = LD.QUANTITY_REM,
    CODE_GROUP = CASE WHEN L.ID_LOT > LD.ID_LOT THEN CAST(L.ID_LOT AS VARCHAR(8)) + CAST(LD.ID_LOT AS VARCHAR(8))
        ELSE CAST(LD.ID_LOT AS VARCHAR(8)) + CAST(L.ID_LOT AS VARCHAR(8)) END
INTO #TEMP FROM LOT L
INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
INNER JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
INNER JOIN GOODS G ON L.ID_GOODS = G.ID_GOODS
INNER JOIN LOT LD ON LD.INTERNAL_BARCODE = L.INTERNAL_BARCODE
INNER JOIN STORE SD ON SD.ID_STORE = LD.ID_STORE
INNER JOIN SCALING_RATIO SRD ON SRD.ID_SCALING_RATIO = LD.ID_SCALING_RATIO
INNER JOIN UNIT UD ON SRD.ID_UNIT = UD.ID_UNIT
INNER JOIN GOODS GD ON LD.ID_GOODS = GD.ID_GOODS
WHERE (@CH_STOCK = 0 OR (L.QUANTITY_REM != 0 AND LD.QUANTITY_REM != 0))
    AND L.ID_GOODS != LD.ID_GOODS 
    AND S.ID_CONTRACTOR = @CONTRACTOR_SELF AND SD.ID_CONTRACTOR = @CONTRACTOR_SELF

ORDER BY ID_LOT

SELECT [ID] = MIN([ID]) INTO #ID FROM #TEMP GROUP BY CODE_GROUP

SELECT * FROM #TEMP T
WHERE EXISTS(SELECT TOP 1 1 FROM #ID I WHERE I.[ID] = T.[ID])
ORDER BY INTERNAL_BARCODE, CODE

RETURN
GO

--EXEC REPEX_CONTROL_DOUBLE_BARCODE N'<XML></XML>'