SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
--------------------------------------------------------------------------
GO
IF OBJECT_ID('DBO.REP_NON_LIQUID_GOODS_ES_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REP_NON_LIQUID_GOODS_ES_EX AS RETURN')
    GRANT EXEC ON DBO.REP_NON_LIQUID_GOODS_ES_EX TO PUBLIC
END
GO
ALTER  PROCEDURE REP_NON_LIQUID_GOODS_ES_EX
    @XMLPARAM NTEXT
AS
    DECLARE @HDOC INT
    DECLARE @DATE_FROM DATETIME, @DATE_TO DATETIME, @ALL_STORES BIT, @SHOW_LOTS BIT
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT 
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @SHOW_LOTS = SHOW_LOTS
    FROM OPENXML(@HDOC, 'XML', 2) WITH (
        DATE_FROM DATETIME,
        DATE_TO DATETIME,
        SHOW_LOTS BIT
    )
    
    SELECT 
        ID_STORE = STORE
    INTO #STORES
    FROM OPENXML(@HDOC, '/XML/STORE') WITH(
        STORE BIGINT '.'
    )
    IF (@@ROWCOUNT=0)
        SET @ALL_STORES = 1
    EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT
    IF (@SHOW_LOTS=1) BEGIN
        SELECT
            G.ID_GOODS_GLOBAL,
            LOT_NAME = CHAR(10)+CHAR(13)+ L.LOT_NAME,
            STORE = S.NAME,
            QTY_REMAIN = SUM((LM.QUANTITY_ADD-LM.QUANTITY_SUB-LM.QUANTITY_RES)*CONVERT(MONEY, SR.NUMERATOR)/CONVERT(MONEY,SR.DENOMINATOR)),
            PRICE_SUP = (L.PRICE_SUP - L.PVAT_SUP) * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR),
            PRICE_SAL = L.PRICE_SAL  * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR)
        INTO #LOT
        FROM LOT L
        INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
        INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
        INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
        INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
        WHERE NOT EXISTS (SELECT DISTINCT
                              LM.ID_LOT_GLOBAL
                          FROM LOT_MOVEMENT LM
                          WHERE CODE_OP IN ('CHEQUE', 'INVOICE_OUT')
                          AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
                          AND LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL)
        AND (@ALL_STORES=1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
        AND LM.DATE_OP <= @DATE_TO
        and L.ID_LOT_GLOBAL IN (SELECT DISTINCT 
                                    LP.ID_LOT_GLOBAL
                                FROM LOT LP
                                INNER JOIN LOT_MOVEMENT LMP ON LMP.ID_LOT_GLOBAL = LP.ID_LOT_GLOBAL 
                                                           AND LMP.ID_DOCUMENT=LP.ID_DOCUMENT 
                                                           AND LMP.ID_DOCUMENT_ITEM=LP.ID_DOCUMENT_ITEM
                                WHERE LMP.DATE_OP<=@DATE_FROM)
        GROUP BY G.ID_GOODS_GLOBAL, L.LOT_NAME, S.NAME, (L.PRICE_SUP - L.PVAT_SUP) * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR), L.PRICE_SAL  * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR)
        HAVING SUM(LM.QUANTITY_ADD-LM.QUANTITY_SUB-LM.QUANTITY_RES)>0

        SELECT
            GOODS = F2.[NAME] + T.LOT_NAME,
            PRODUCER = PROD.PRODUCER_NAME,
            T.STORE,
            QTY_REMAIN = SUM(T.QTY_REMAIN),
            PRICE_SUP = SUM(T.PRICE_SUP),
            PRICE_SAL = SUM(T.PRICE_SAL)
        FROM #LOT T(NOLOCK)
        INNER JOIN ES_ES_2_GOODS G2(NOLOCK) ON G2.ID_GOODS_GLOBAL = T.ID_GOODS_GLOBAL
        INNER JOIN ES_EF2 F2(NOLOCK) ON F2.GUID_ES = G2.C_ES
        LEFT JOIN ES_PRODUCER PROD(NOLOCK) ON PROD.KOD_PRODUCER = F2.PRODUCER_COD
        GROUP BY F2.[NAME], PROD.PRODUCER_NAME, T.STORE, T.LOT_NAME
        ORDER BY F2.[NAME], T.STORE
    END
    ELSE BEGIN
        DECLARE @TAB TABLE(
            ID_LOT_GLOBAL UNIQUEIDENTIFIER,
            ID_GOODS BIGINT,
            ID_STORE BIGINT,
            DATE_OP DATETIME
        )
        INSERT INTO @TAB
        SELECT
            LM1.ID_LOT_GLOBAL,
            MLD.ID_GOODS,
            MLD.ID_STORE,
            MLD.DATE_OP
        FROM 
        LOT_MOVEMENT LM1
        INNER JOIN (
                    SELECT -- дата партии = минимальная дата движения по партии
                        L.ID_LOT_GLOBAL,
                        L.ID_GOODS,
                        L.ID_STORE,
                        DATE_OP = LM.DATE_OP
                    FROM LOT L
                    INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL =L.ID_LOT_GLOBAL
                    WHERE LM.DATE_OP = (SELECT 
                                             MIN(DATE_OP) 
                                         FROM LOT_MOVEMENT
                                         WHERE LOT_MOVEMENT.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
                                         AND DATE_OP<=@DATE_TO)
                    ) MLD ON MLD.ID_LOT_GLOBAL = LM1.ID_LOT_GLOBAL AND MLD.DATE_OP = LM1.DATE_OP
        
        DECLARE @TAB1 TABLE(
            ID_LOT_GLOBAL UNIQUEIDENTIFIER
        )
        
        INSERT INTO @TAB1
        SELECT -- соединяем с самим собой чтобы найти идентификатор партии по дате
            T.ID_LOT_GLOBAL
        FROM @TAB T
        INNER JOIN (
                    SELECT -- максимальная дата партии по складу и товару
                        ID_GOODS,
                        ID_STORE,
                        DATE_OP = MAX(DATE_OP)
                    FROM @TAB
                    GROUP BY ID_GOODS, ID_STORE
                    ) G ON G.ID_GOODS = T.ID_GOODS AND G.ID_STORE = T.ID_STORE AND G.DATE_OP = T.DATE_OP
        SELECT
            G.ID_GOODS_GLOBAL,
            STORE = S.NAME,
            A.QTY_REMAIN,
            B.PRICE_SUP,
            B.PRICE_SAL
        INTO #TAB
        FROM (
                SELECT -- выбираем товары и остатки
                    L.ID_GOODS,
                    L.ID_STORE,
                    QTY_REMAIN = SUM((LM.QUANTITY_ADD-LM.QUANTITY_SUB-LM.QUANTITY_RES)*CONVERT(MONEY, SR.NUMERATOR)/CONVERT(MONEY,SR.DENOMINATOR))
                FROM LOT L
                INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
                INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
                INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
                INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE
                INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
                WHERE NOT EXISTS (SELECT DISTINCT -- партии по которым были продажи
                                    LM.ID_LOT_GLOBAL
                                FROM LOT_MOVEMENT LM
                                WHERE CODE_OP IN ('CHEQUE', 'INVOICE_OUT')
                                AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
                                AND LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL)
                AND (@ALL_STORES=1 OR (L.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
                AND LM.DATE_OP <= @DATE_TO
                AND L.ID_LOT_GLOBAL IN (SELECT DISTINCT 
                                            LP.ID_LOT_GLOBAL
                                        FROM LOT LP
                                        INNER JOIN LOT_MOVEMENT LMP ON LMP.ID_LOT_GLOBAL = LP.ID_LOT_GLOBAL 
                                                                   AND LMP.ID_DOCUMENT=LP.ID_DOCUMENT 
                                                                   AND LMP.ID_DOCUMENT_ITEM=LP.ID_DOCUMENT_ITEM
                                        WHERE LMP.DATE_OP<=@DATE_FROM)

                GROUP BY L.ID_GOODS, L.ID_STORE
                HAVING SUM(LM.QUANTITY_ADD-LM.QUANTITY_SUB-LM.QUANTITY_RES)>0
            ) A
        INNER JOIN (
                SELECT -- выбираем товары и цены партии
                    L.ID_GOODS,
                    L.ID_STORE,
                    PRICE_SUP = (L.PRICE_SUP - L.PVAT_SUP) * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR),
                    PRICE_SAL = L.PRICE_SAL  * CONVERT(MONEY, SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR)
                FROM LOT L
                INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
                INNER JOIN @TAB1 T1 ON T1.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
            ) B ON A.ID_GOODS = B.ID_GOODS AND A.ID_STORE = B.ID_STORE
        INNER JOIN GOODS G ON G.ID_GOODS = A.ID_GOODS
        INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
        INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE

        SELECT
            GOODS = F2.[NAME],
            PRODUCER = PROD.PRODUCER_NAME,
            T.STORE,
            QTY_REMAIN = SUM(T.QTY_REMAIN),
            PRICE_SUP = SUM(T.PRICE_SUP),
            PRICE_SAL = SUM(T.PRICE_SAL)
        FROM #TAB T(NOLOCK)
        INNER JOIN ES_ES_2_GOODS G2(NOLOCK) ON G2.ID_GOODS_GLOBAL = T.ID_GOODS_GLOBAL
        INNER JOIN ES_EF2 F2(NOLOCK) ON F2.GUID_ES = G2.C_ES
        LEFT JOIN ES_PRODUCER PROD(NOLOCK) ON PROD.KOD_PRODUCER = F2.PRODUCER_COD
        GROUP BY F2.[NAME], PROD.PRODUCER_NAME, T.STORE
        ORDER BY F2.[NAME], T.STORE 

    END   

    DECLARE C CURSOR FOR
    SELECT DISTINCT
        C.NAME
    FROM CONTRACTOR C
    INNER JOIN STORE S ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
    WHERE (@ALL_STORES =1 OR (S.ID_STORE IN (SELECT ID_STORE FROM #STORES)))
    DECLARE @NAME VARCHAR(300)
    DECLARE @CONTRACTORS VARCHAR(4000)
    OPEN C
    WHILE 1=1
    BEGIN
        FETCH NEXT FROM C INTO @NAME
        IF (@@FETCH_STATUS<>0) BREAK
        SET @CONTRACTORS = ISNULL(@CONTRACTORS+', '+@NAME, @NAME)
    END
    CLOSE C
    DEALLOCATE C    
    
    SELECT CONTRACTORS = @CONTRACTORS   
RETURN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
exec REP_NON_LIQUID_GOODS_ES_EX @xmlParam = N'<XML><DATE_FROM>2008-01-01T00:00:00.000</DATE_FROM><DATE_TO>2008-10-22T12:54:52.578</DATE_TO><SHOW_LOTS>0</SHOW_LOTS></XML>'
