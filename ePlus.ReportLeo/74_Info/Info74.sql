---------------------------------------------------------------------------------------
IF (OBJECT_ID('[REPEX_INFO_74]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_INFO_74] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_INFO_74]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_END DATETIME, @DATE_1P DATETIME, @DATE_2P DATETIME,
    @DATE_3P DATETIME, @DATE_4P DATETIME,
    @DATE_5P DATETIME, @DATE_6P DATETIME,
    @ALL_PROGRAM BIT, @ALL_CONTRACTS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_END = DATE_END,
        @DATE_1P = DATE_1P, @DATE_2P = DATE_2P,
        @DATE_3P = DATE_3P, @DATE_4P = DATE_4P,
        @DATE_5P = DATE_5P, @DATE_6P = DATE_6P
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_END DATETIME 'DATE_END',
        DATE_1P DATETIME 'DATE_1P', DATE_2P DATETIME 'DATE_2P',
        DATE_3P DATETIME 'DATE_3P', DATE_4P DATETIME 'DATE_4P',
        DATE_5P DATETIME 'DATE_5P', DATE_6P DATETIME 'DATE_6P')

    SELECT * INTO #PROGRAM FROM OPENXML(@HDOC, '//ID_TASK_PROGRAM_GLOBAL') 
    WITH(ID_TASK_PROGRAM_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_TASK_PROGRAM_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_PROGRAM = 1 ELSE SET @ALL_PROGRAM = 0

    SELECT * INTO #CONTRACTS FROM OPENXML(@HDOC, '//ID_CONTRACTS_GLOBAL') 
    WITH(ID_CONTRACTS_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_CONTRACTS_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTS = 1 ELSE SET @ALL_CONTRACTS = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT G.ID_GOODS,
    NAME_TN =  G.[NAME],
    PRICE = CG.PRICE,
    QUANTITY_G = SUM(CG.QUANTITY),
    QUANTITY_CM = SUM(CM.QUANTITY),
    CONTRACTS_NAME = C.[NAME],
    PERIOD_G_1 = SUM(CASE WHEN CM.DATE_OP <= @DATE_2P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_1 = SUM(CASE WHEN CM.DATE_OP <= @DATE_2P THEN CM.QUANTITY ELSE 0 END),
    PERIOD_G_2 = SUM(CASE WHEN CM.DATE_OP >= @DATE_2P AND CM.DATE_OP <= @DATE_3P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_2 = SUM(CASE WHEN CM.DATE_OP >= @DATE_2P AND CM.DATE_OP <= @DATE_3P THEN CM.QUANTITY ELSE 0 END),
    PERIOD_G_3 = SUM(CASE WHEN CM.DATE_OP >= @DATE_3P AND CM.DATE_OP <= @DATE_4P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_3 = SUM(CASE WHEN CM.DATE_OP >= @DATE_3P AND CM.DATE_OP <= @DATE_4P THEN CM.QUANTITY ELSE 0 END),
    PERIOD_G_4 = SUM(CASE WHEN CM.DATE_OP >= @DATE_4P AND CM.DATE_OP <= @DATE_5P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_4 = SUM(CASE WHEN CM.DATE_OP >= @DATE_4P AND CM.DATE_OP <= @DATE_5P THEN CM.QUANTITY ELSE 0 END),
    PERIOD_G_5 = SUM(CASE WHEN CM.DATE_OP >= @DATE_5P AND CM.DATE_OP <= @DATE_6P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_5 = SUM(CASE WHEN CM.DATE_OP >= @DATE_5P AND CM.DATE_OP <= @DATE_6P THEN CM.QUANTITY ELSE 0 END),
    PERIOD_G_6 = SUM(CASE WHEN CM.DATE_OP >= @DATE_6P THEN CG.QUANTITY ELSE 0 END),
    PERIOD_M_6 = SUM(CASE WHEN CM.DATE_OP >= @DATE_6P THEN CM.QUANTITY ELSE 0 END)
FROM CONTRACTS C 
INNER JOIN CONTRACTS_GOODS CG ON CG.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN CONTRACTS_MOVEMENT CM ON CM.ID_CONTRACTS_GOODS_GLOBAL = CG.ID_CONTRACTS_GOODS_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = CG.ID_GOODS
WHERE (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
    AND (@ALL_CONTRACTS = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTS WHERE #CONTRACTS.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL))
    AND CM.DATE_OP BETWEEN @DATE_1P AND @DATE_END
    AND C.TYPE = 'SUPPLY'
GROUP BY G.ID_GOODS, CG.PRICE, G.[NAME], C.[NAME]

RETURN 0
GO
--exec REPEX_INFO_74 @xmlParam=N'<XML><DATE_1P>2010-01-01T00:00:00.000</DATE_1P><DATE_2P>2010-02-01T00:00:00.000</DATE_2P><DATE_3P>2010-03-01T00:00:00.000</DATE_3P><DATE_4P>2010-04-01T00:00:00.000</DATE_4P><DATE_5P>2010-05-01T00:00:00.000</DATE_5P><DATE_6P>2010-06-01T00:00:00.000</DATE_6P></XML>'