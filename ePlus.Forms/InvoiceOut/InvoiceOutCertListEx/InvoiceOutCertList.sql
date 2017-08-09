SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REPEX_INVOICE_OUT_CERT_LIST') IS NULL) EXEC('CREATE PROCEDURE REPEX_INVOICE_OUT_CERT_LIST AS RETURN')
GO

ALTER PROCEDURE REPEX_INVOICE_OUT_CERT_LIST
    @XMLPARAM NTEXT
AS

DECLARE @HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_GLOBAL = ID_GLOBAL
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT 
	DOC_NUMBER = I.MNEMOCODE,
	DOC_DATE = DBO.FN_DATE_2_VARCHAR(I.DATE),
	SUP_NAME = case
	             when SUP.FULL_NAME is null then SUP.NAME
	             when SUP.FULL_NAME = '' then SUP.NAME
	             else SUP.FULL_NAME
	           end, 
	SUP_INN = SUP.INN,
	SUP_ADDRESS = SUP.ADDRESS,
	CONT_NAME = case
	             when CONT.FULL_NAME is null then CONT.NAME
	             when CONT.FULL_NAME = '' then CONT.NAME
	             else CONT.FULL_NAME
	           end,
	CONT_INN = CONT.INN
FROM INVOICE_OUT I
	INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
	INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = S.ID_CONTRACTOR
	INNER JOIN CONTRACTOR CONT ON CONT.ID_CONTRACTOR = I.ID_CONTRACTOR_TO
WHERE I.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
        
SELECT 
	NN = CONVERT(BIGINT, NULL),
	GOODS_NAME = G.NAME,
	PRODUCER = P.NAME,
	SERIES_NUMBER = S.SERIES_NUMBER,
	CERT_NUMBER = C.CERT_NUMBER,
	CERT_DATE = C.CERT_DATE,
	ISSUED_BY = C.ISSUED_BY,
	BEST_BEFORE = S.BEST_BEFORE,
	REG_CERT_NAME = RC.NAME,
	REG_CERT_DATE = RC.DATE,
	COMMENT = CONVERT(VARCHAR(300), NULL)
FROM INVOICE_OUT_ITEM II
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
	LEFT JOIN SERIES S ON S.ID_SERIES = L.ID_SERIES
	LEFT JOIN CERTIFICATE C ON C.ID_SERIES = S.ID_SERIES
	LEFT JOIN REG_CERT RC ON RC.ID_REG_CERT_GLOBAL = L.ID_REG_CERT_GLOBAL        
WHERE II.ID_INVOICE_OUT_GLOBAL = @ID_GLOBAL
ORDER BY 2 ASC

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN
GO
