--------------------------------------------------------------------
IF (OBJECT_ID('[REPEX_INVOICE_LS_101]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_INVOICE_LS_101] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_INVOICE_LS_101]
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

    SELECT * INTO #CONTRACTS FROM OPENXML(@HDOC, '//ID_CONTRACTS_GLOBAL') 
    WITH(ID_CONTRACTS_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_CONTRACTS_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTS = 1 ELSE SET @ALL_CONTRACTS = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT 
    G.[NAME],
    QUANTITY_G = SUM(CG.QUANTITY),
    PRICE_G = CG.PRICE,
    SUM_G = SUM(CG.QUANTITY * CG.PRICE),
    QUANTITY_M = SUM(CM.QUANTITY),
    SUM_M = SUM(CM.SUM_SUP),
    NUM = CONVERT(VARCHAR, CM.DATE_OP, 104) + ' ' + CM.DOC_NUM,
    QUANTITY_P_M = SUM(CMP.QUANTITY_P_M),
    SUM_P_M = SUM(CMP.SUM_P_M)
FROM CONTRACTS C 
INNER JOIN CONTRACTS_GOODS CG ON CG.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN CONTRACTS_MOVEMENT CM ON CM.ID_CONTRACTS_GOODS_GLOBAL = CG.ID_CONTRACTS_GOODS_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = CG.ID_GOODS
LEFT JOIN (SELECT CMM.ID_CONTRACTS_MOVEMENT, 
        QUANTITY_P_M = CMM.QUANTITY, 
        SUM_P_M = CMM.SUM_SUP 
    FROM CONTRACTS_MOVEMENT CMM
    WHERE CMM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO) CMP ON CMP.ID_CONTRACTS_MOVEMENT = CM.ID_CONTRACTS_MOVEMENT
WHERE (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
    AND (@ALL_CONTRACTS = 1 OR EXISTS(SELECT TOP 1 1 FROM #CONTRACTS WHERE #CONTRACTS.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL))
    AND CM.CODE_OP IN ('INVOICE', 'AR_2C')
GROUP BY G.[NAME], CG.PRICE, CM.DATE_OP, CM.DOC_NUM

RETURN 0
GO
--exec REPEX_INVOICE_LS_101 @xmlParam=N'<XML><DATE_BEGIN>2010-01-01T00:00:00.000</DATE_BEGIN><DATE_1P>2010-07-01T00:00:00.000</DATE_1P><DATE_2P>2010-12-31T00:00:00.000</DATE_2P></XML>'