---------------------------------------------------------------------------------------
IF OBJECT_ID('FN_INFO_SECURING_LS_M') IS NOT NULL EXEC('DROP FUNCTION DBO.FN_INFO_SECURING_LS_M')
GO
CREATE FUNCTION DBO.FN_INFO_SECURING_LS_M(@BEST_BEFORE DATETIME)
RETURNS VARCHAR(20)
AS
BEGIN
    RETURN CAST(ROUND(ABS((CAST((CAST(@BEST_BEFORE AS BIGINT) - CAST(GETDATE() AS BIGINT)) AS MONEY)) / 30), 1) AS VARCHAR(20))
END
GO
IF OBJECT_ID('FN_INFO_SECURING_LS_P') IS NOT NULL EXEC('DROP FUNCTION DBO.FN_INFO_SECURING_LS_P')
GO
CREATE FUNCTION DBO.FN_INFO_SECURING_LS_P(@BEST_BEFORE DATETIME, @DATE_PRODUCTION DATETIME)
RETURNS VARCHAR(30)
AS
BEGIN
    RETURN ABS((ROUND(CAST((CAST(@BEST_BEFORE AS BIGINT) - CAST(GETDATE() AS BIGINT)) AS MONEY) / 30, 1) /
        ((CAST((CAST(@BEST_BEFORE AS BIGINT) - CAST(@DATE_PRODUCTION AS BIGINT)) AS MONEY)) / 30))) * 100
END
GO
--------------------------------------------------------------------
IF (OBJECT_ID('[REPEX_CRF_70]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_CRF_70] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_CRF_70]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_BEGIN DATETIME, @DATE_1P DATETIME, @DATE_2P DATETIME,
    @ALL_PROGRAM BIT, @ALL_CONTRACTS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_BEGIN = DATE_BEGIN, 
        @DATE_1P = DATE_1P, @DATE_2P = DATE_2P
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_BEGIN DATETIME 'DATE_BEGIN',
        DATE_1P DATETIME 'DATE_1P', DATE_2P DATETIME 'DATE_2P')

    SELECT * INTO #PROGRAM FROM OPENXML(@HDOC, '//ID_TASK_PROGRAM_GLOBAL') 
    WITH(ID_TASK_PROGRAM_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_TASK_PROGRAM_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_PROGRAM = 1 ELSE SET @ALL_PROGRAM = 0

    SELECT * INTO #CONTRACTS FROM OPENXML(@HDOC, '//ID_CONTRACTS_GLOBAL') 
    WITH(ID_CONTRACTS_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_CONTRACTS_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTS = 1 ELSE SET @ALL_CONTRACTS = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT C.ID_CONTRACTS_GLOBAL,
    ID_GROUP = CAST(G.ID_SUBSTANCE AS VARCHAR) + '_' + CAST(G.ID_GOODS AS VARCHAR),
    NAME_MNN = S.[NAME],
    NAME_TN =  G.[NAME] + ' (' + P.[NAME] + ') ' + COU.[NAME],
    NAME_GOODS =  G.[NAME],
    NAME_CONTRACTOR = CON.[NAME],
    QUANTITY_LOT = CAST(CM.QUANTITY AS VARCHAR) + '('
        + CASE WHEN DBO.FN_INFO_SECURING_LS_P(SER.BEST_BEFORE, SER.DATE_PRODUCTION) < 70 THEN DBO.FN_INFO_SECURING_LS_M(SER.BEST_BEFORE)
            ELSE '0' END + ')' + CHAR(13)
        + CONVERT(VARCHAR, CM.DATE_OP, 104),
    SERIES = CAST(CASE WHEN DBO.FN_INFO_SECURING_LS_P(SER.BEST_BEFORE, SER.DATE_PRODUCTION) < 50 THEN DBO.FN_INFO_SECURING_LS_M(SER.BEST_BEFORE) ELSE '0' END AS NVARCHAR(2000))
INTO #TEMP
FROM CONTRACTS C 
INNER JOIN CONTRACTS_GOODS CG ON CG.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN CONTRACTS_MOVEMENT CM ON CM.ID_CONTRACTS_GOODS_GLOBAL = CG.ID_CONTRACTS_GOODS_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = CG.ID_GOODS
INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
INNER JOIN COUNTRY COU ON COU.ID_COUNTRY = P.ID_COUNTRY
INNER JOIN SUBSTANCE S ON S.ID_SUBSTANCE = G.ID_SUBSTANCE
LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CM.ID_LOT_GLOBAL
LEFT JOIN CONTRACTOR CON ON CON.ID_CONTRACTOR = L.ID_SUPPLIER
LEFT JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
WHERE (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
    AND (@ALL_CONTRACTS = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTS WHERE #CONTRACTS.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL))
    AND CM.DATE_OP <= @DATE_2P
    AND C.TYPE = 'PURCHASE'

SELECT NAME_GOODS = G.[NAME], ID_LOT_MOVEMENT,
    ID_PERIOD = CASE WHEN LM.DATE_OP < @DATE_BEGIN THEN 1
        WHEN LM.DATE_OP >= @DATE_BEGIN AND LM.DATE_OP < @DATE_1P THEN 2
        WHEN LM.DATE_OP >= @DATE_1P AND LM.DATE_OP <= @DATE_2P THEN 3 ELSE 0 END,
    PERIOD = CASE WHEN LM.DATE_OP < @DATE_BEGIN THEN CONVERT(VARCHAR, @DATE_BEGIN, 104)
        WHEN LM.DATE_OP >= @DATE_BEGIN AND LM.DATE_OP < @DATE_1P THEN CONVERT(VARCHAR, @DATE_1P, 104)
        WHEN LM.DATE_OP >= @DATE_1P AND LM.DATE_OP <= @DATE_2P THEN CONVERT(VARCHAR, @DATE_2P, 104)
        ELSE '' END,
    QUANTITY = LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES
INTO #LOT
FROM LOT L
INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
WHERE EXISTS(SELECT TOP 1 1 FROM #TEMP T WHERE T.ID_CONTRACTS_GLOBAL = L.ID_CONTRACTS_GLOBAL)

SELECT
    NAME_GOODS, ID_PERIOD,
    SERIES = PERIOD + '-(' + CAST(CAST(QUANTITY AS NUMERIC(13,0)) AS VARCHAR) + ')' 
        + ISNULL(' ' + (SELECT TOP 1 SERIES FROM #TEMP TT WHERE TT.NAME_GOODS = T.NAME_GOODS), '')
INTO #TOTAL_LOT
FROM (SELECT NAME_GOODS, ID_PERIOD, PERIOD, QUANTITY = SUM(QUANTITY)
    FROM #LOT
    GROUP BY NAME_GOODS, ID_PERIOD, PERIOD) T


SELECT DISTINCT NAME_GOODS,
    SERIES = CAST('' AS NVARCHAR(2000))
INTO #TOTAL
FROM #TOTAL_LOT

DECLARE @NAME_GOODS VARCHAR(1000), @SERIES VARCHAR(1000)

DECLARE CUR CURSOR LOCAL READ_ONLY
FOR SELECT NAME_GOODS, SERIES FROM #TOTAL_LOT ORDER BY NAME_GOODS, ID_PERIOD  

OPEN CUR
WHILE 1 = 1
BEGIN
    FETCH NEXT FROM CUR INTO @NAME_GOODS, @SERIES
    IF @@FETCH_STATUS <> 0 BREAK
    
    UPDATE #TOTAL
    SET SERIES = SERIES + CASE WHEN SERIES != '' THEN CHAR(13) ELSE '' END + @SERIES
    WHERE NAME_GOODS = @NAME_GOODS
    
END
CLOSE CUR
DEALLOCATE CUR

UPDATE T SET SERIES = TT.SERIES
FROM #TEMP T
INNER JOIN #TOTAL TT ON TT.NAME_GOODS = T.NAME_GOODS

SELECT * FROM #TEMP WHERE NAME_GOODS LIKE '%�������%'

RETURN 0
GO
--exec REPEX_CRF_70 
--@xmlParam=N'<XML><DATE_BEGIN>2010-01-01T00:00:00.000</DATE_BEGIN><DATE_1P>2010-07-01T00:00:00.000</DATE_1P><DATE_2P>2010-12-31T00:00:00.000</DATE_2P></XML>'