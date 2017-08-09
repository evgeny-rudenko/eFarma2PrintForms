SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REPEX_REQUEST') IS NULL) EXEC ('CREATE PROCEDURE REPEX_REQUEST AS RETURN')
GO
ALTER PROCEDURE REPEX_REQUEST
	(@XMLPARAM NTEXT)
AS
    DECLARE @HDOC INT
    DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT 
        @ID_GLOBAL = ID_GLOBAL
    FROM OPENXML(@HDOC, '/XML') WITH (
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL'
    )
    EXEC SP_XML_REMOVEDOCUMENT @HDOC


    SELECT 
        R.DOC_NUM,
        R.DOC_DATE,
        CONTRACTOR_NAME = CASE WHEN ISNULL(C.FULL_NAME, '') = '' THEN C.NAME ELSE C.FULL_NAME END,
        STORE_NAME = S.NAME
    FROM REQUEST R
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = R.ID_CONTRACTOR
    LEFT JOIN STORE S ON S.ID_STORE = R.ID_STORE    
    WHERE ID_REQUEST=@ID_GLOBAL

    SELECT 
        GOODS_NAME = G.NAME, 
        PRODUCER_NAME = P.NAME, 
        GROUP_NAME = GC.NAME, 
        QTY = RI.QTY, 
        ACCESSIBLE = RI.QTY - RIM.QTY, 
        ADDED = RIM.QTY, 
        ON_THE_WAY = OTW.QTY
    FROM REQUEST_ITEM RI
    INNER JOIN REQUEST R ON R.ID_REQUEST = RI.ID_REQUEST
    INNER JOIN GOODS G ON G.ID_GOODS_GLOBAL = RI.ID_GOODS
    INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
    INNER JOIN (SELECT
                    ID_REQUEST_ITEM,
                    QTY = SUM(QTY_SUB), 
                    QTY_ADD = SUM(QTY_ADD)
                FROM REQUEST_ITEM_MOVEMENT
                GROUP BY ID_REQUEST_ITEM) RIM ON RIM.ID_REQUEST_ITEM = RI.ID_REQUEST_ITEM
    LEFT JOIN (SELECT 
                   RI.ID_GOODS,
                   R.ID_CONTRACTOR,
                   R.ID_STORE,
                   QTY = SUM(RIM.QTY_ADD-RIM.QTY_SUB)
               FROM REQUEST_ITEM_MOVEMENT RIM
               INNER JOIN REQUEST_ITEM RI ON RI.ID_REQUEST_ITEM = RIM.ID_REQUEST_ITEM
               INNER JOIN REQUEST R ON R.ID_REQUEST = RI.ID_REQUEST
               GROUP BY RI.ID_GOODS, R.ID_CONTRACTOR, R.ID_STORE) OTW ON OTW.ID_GOODS = RI.ID_GOODS 
                                                                     AND OTW.ID_CONTRACTOR = R.ID_CONTRACTOR 
                                                                     AND OTW.ID_STORE = R.ID_STORE
    LEFT JOIN GOODS_CLASSIFIER_2_GOODS GC2G ON GC2G.ID_GOODS = G.ID_GOODS_GLOBAL
    LEFT JOIN GOODS_CLASSIFIER GC ON GC.ID_GOODS_CLASSIFIER = GC2G.ID_GOODS_CLASSIFIER
    WHERE R.ID_REQUEST=@ID_GLOBAL
    ORDER BY G.NAME
RETURN
GO
