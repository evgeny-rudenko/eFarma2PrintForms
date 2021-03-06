IF (OBJECT_ID('[REPEX_LIST_71]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_LIST_71] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_LIST_71]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME, @ALL_CONTRACTOR BIT, @ALL_PROGRAM BIT, @ALL_CONTRACTS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_FR = DATE_FR, @DATE_TO = DATE_TO
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO')

    SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
    WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR <> 0
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1 ELSE SET @ALL_CONTRACTOR = 0

    SELECT * INTO #PROGRAM FROM OPENXML(@HDOC, '//ID_TASK_PROGRAM_GLOBAL') 
    WITH(ID_TASK_PROGRAM_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_TASK_PROGRAM_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_PROGRAM = 1 ELSE SET @ALL_PROGRAM = 0

    SELECT * INTO #CONTRACTS FROM OPENXML(@HDOC, '//ID_CONTRACTS_GLOBAL') 
    WITH(ID_CONTRACTS_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_CONTRACTS_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTS = 1 ELSE SET @ALL_CONTRACTS = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT
    CON.ID_CONTRACTOR,
    CONTRACTOR_NAME = CON.[NAME],
    GOODS_NAME = G.[NAME],
    UNIT_NAME = (SELECT TOP 1 U.SHORT_NAME FROM SCALING_RATIO SR
        INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
        WHERE SR.NUMERATOR = 1 AND SR.DATE_DELETED IS NULL),
    CM.QUANTITY,
    PRICE = CM.SUM_SUP / CM.QUANTITY,
    CM.SUM_SUP
FROM CONTRACTS C
INNER JOIN CONTRACTS_MOVEMENT CM ON CM.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = CM.ID_GOODS
INNER JOIN CONTRACTOR CON ON CON.ID_CONTRACTOR = C.ID_CONTRACTOR
WHERE (@ALL_CONTRACTOR = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTOR WHERE #CONTRACTOR.ID_CONTRACTOR = C.ID_CONTRACTOR))
    AND (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
    AND (@ALL_CONTRACTS = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTS WHERE #CONTRACTS.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL))
    AND CM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND C.TYPE = 'PURCHASE'
RETURN 0
GO
--exec REPEX_LIST_71 
--@xmlParam=N'<XML><DATE_FR>2010-03-01T11:14:34.484</DATE_FR><DATE_TO>2010-03-31T11:14:34.484</DATE_TO></XML>'

