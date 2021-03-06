--------------------------------------------------------------------
IF (OBJECT_ID('[REPEX_INVOICE_LS_76]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_INVOICE_LS_76] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_INVOICE_LS_76]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME, @ALL_CONTRACTOR BIT, @ALL_PROGRAM BIT, @ALL_CONTRACTS BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_FR = DATE_FR, @DATE_TO = DATE_TO
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO')

    SELECT * INTO #PROGRAM FROM OPENXML(@HDOC, '//ID_TASK_PROGRAM_GLOBAL') 
    WITH(ID_TASK_PROGRAM_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_TASK_PROGRAM_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_PROGRAM = 1 ELSE SET @ALL_PROGRAM = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT 
    T.ID_TASK_PROGRAM_GLOBAL,
    T.PROGRAM_NAME,
    COUNT_G_TN = COUNT(DISTINCT GG.ID_TRADE_NAME),
    COUNT_G_MNN = COUNT(DISTINCT GG.ID_SUBSTANCE),
    COUNT_M_TN = COUNT(DISTINCT GM.ID_TRADE_NAME),
    COUNT_M_MNN = COUNT(DISTINCT GM.ID_SUBSTANCE),
    SUMM = SUM(CM.SUM_SUP)
FROM CONTRACTS C 
INNER JOIN TASK_PROGRAM T ON T.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL
INNER JOIN CONTRACTS_GOODS CG ON CG.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN CONTRACTS_MOVEMENT CM ON CM.ID_CONTRACTS_GOODS_GLOBAL = CG.ID_CONTRACTS_GOODS_GLOBAL
INNER JOIN GOODS GG ON GG.ID_GOODS = CG.ID_GOODS
INNER JOIN GOODS GM ON GM.ID_GOODS = CM.ID_GOODS
WHERE (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
    AND CM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
    AND C.TYPE = 'PURCHASE'
GROUP BY T.ID_TASK_PROGRAM_GLOBAL, T.PROGRAM_NAME

RETURN 0
GO
--exec REPEX_INVOICE_LS_76 @xmlParam=N'<XML></XML>'--<ID_TASK_PROGRAM_GLOBAL>06B1B58C-AE54-4F30-B2D6-A6B04EE7FF81</ID_TASK_PROGRAM_GLOBAL></XML>'