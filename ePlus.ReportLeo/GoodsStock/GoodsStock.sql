IF (OBJECT_ID('[REPEX_GOODS_STOCK]') IS NULL) EXEC ('CREATE PROCEDURE [REPEX_GOODS_STOCK] AS RETURN')
GO
ALTER PROCEDURE [dbo].[REPEX_GOODS_STOCK]
    @XMLPARAM NTEXT
AS
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME,
    @ALL_FIN BIT, @ALL_PROGRAM BIT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

    SELECT TOP 1 @DATE_FR = DATE_FR, @DATE_TO = DATE_TO
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO')

    SELECT * INTO #FIN FROM OPENXML(@HDOC, '//ID_ENUMERATION_VALUE') 
    WITH(ID_ENUMERATION_VALUE BIGINT '.') WHERE ID_ENUMERATION_VALUE <> 0
    IF @@ROWCOUNT = 0 SET @ALL_FIN = 1 ELSE SET @ALL_FIN = 0

    SELECT * INTO #PROGRAM FROM OPENXML(@HDOC, '//ID_TASK_PROGRAM_GLOBAL') 
    WITH(ID_TASK_PROGRAM_GLOBAL UNIQUEIDENTIFIER '.') WHERE ID_TASK_PROGRAM_GLOBAL IS NOT NULL
    IF @@ROWCOUNT = 0 SET @ALL_PROGRAM = 1 ELSE SET @ALL_PROGRAM = 0

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

SELECT
    F.ID_ENUMERATION_VALUE,
    F.[DESCRIPTION],
    T.ID_TASK_PROGRAM_GLOBAL,
    T.PROGRAM_NAME,
    CON.ID_CONTRACTOR,
    CONTRACTOR_NAME = C.[NAME],
	SUM_SUP = (CASE 
		WHEN LM.QUANTITY_ADD <> 0 THEN LM.SUM_SUP
		WHEN LM.QUANTITY_SUB <> 0 THEN -1 * LM.SUM_SUP * SIGN(LM.QUANTITY_SUB) ELSE 0 END),
    SUM_PLACE = CASE WHEN I.ID_INVOICE IS NOT NULL THEN II.SUPPLIER_SUM_VAT ELSE 0 END
FROM CONTRACTS C 
INNER JOIN ENUMERATION_VALUE F ON F.MNEMOCODE = C.FUNDING_SOURCE
INNER JOIN TASK_PROGRAM T ON T.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL
INNER JOIN LOT L ON L.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
INNER JOIN CONTRACTOR CON ON CON.ID_CONTRACTOR = C.ID_CONTRACTOR
LEFT JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
LEFT JOIN INVOICE_ITEM II ON II.ID_INVOICE_ITEM_GLOBAL = L.ID_DOCUMENT_ITEM AND L.ID_TABLE = 2
LEFT JOIN INVOICE I ON I.ID_INVOICE = II.ID_INVOICE AND I.DOCUMENT_STATE = 'SAVE'
WHERE (@ALL_FIN = 1 OR EXISTS(SELECT TOP 1 1 FROM #FIN WHERE #FIN.ID_ENUMERATION_VALUE = F.ID_ENUMERATION_VALUE))
    AND (@ALL_PROGRAM = 1 OR EXISTS(SELECT TOP 1 1 FROM #PROGRAM WHERE #PROGRAM.ID_TASK_PROGRAM_GLOBAL = C.ID_TASK_PROGRAM_GLOBAL))
	AND C.TYPE = 'PURCHASE'	
    AND ((LM.DATE_OP is not null and LM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO)
		OR (LM.DATE_OP IS NULL AND I.DOCUMENT_DATE BETWEEN @DATE_FR AND @DATE_TO))
ORDER BY F.[DESCRIPTION], T.PROGRAM_NAME, C.[NAME]

RETURN 0
GO
--exec REPEX_GOODS_STOCK @xmlParam=N'<XML><DATE_FR>2000-01-30T15:02:20.437</DATE_FR><DATE_TO>2010-03-30T15:02:20.437</DATE_TO></XML>'


