IF (OBJECT_ID('REP_MOVEMENT_TEST') IS NULL) EXEC ('CREATE PROCEDURE REP_MOVEMENT_TEST AS RETURN')
GO
ALTER PROCEDURE REP_MOVEMENT_TEST(
    @ID_GLOBAL UNIQUEIDENTIFIER
)
AS
    SELECT
        DOC_NUM = M.MNEMOCODE,
        DOC_DATE = M.DATE,
        [FROM] = S_FROM.NAME,            
        [TO]= S_TO.NAME
    FROM MOVEMENT M
    INNER JOIN STORE S_TO ON S_TO.ID_STORE = M.ID_STORE_TO
    INNER JOIN STORE S_FROM ON S_FROM.ID_STORE = M.ID_STORE_FROM
    WHERE ID_MOVEMENT_GLOBAL = @ID_GLOBAL

    SELECT
        GOODS = G.NAME,
        QTY = MI.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR,
        SUM_SAL = L.PRICE_SAL * MI.QUANTITY
    FROM MOVEMENT_ITEM MI
    INNER JOIN LOT L ON L.ID_LOT = MI.ID_LOT_FROM
    INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
    INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
    WHERE MI.ID_MOVEMENT_GLOBAL = @ID_GLOBAL
RETURN
GO