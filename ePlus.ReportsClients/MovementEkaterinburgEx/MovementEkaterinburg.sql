SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.FN_CERT_LIST_4_REPEX_MOVEMENT_EKATERINBURG') IS NULL)	EXEC ('CREATE FUNCTION DBO.FN_CERT_LIST_4_REPEX_MOVEMENT_EKATERINBURG() RETURNS VARCHAR(4000) AS BEGIN RETURN CONVERT(VARCHAR(4000), NULL) END')
GO
ALTER FUNCTION DBO.FN_CERT_LIST_4_REPEX_MOVEMENT_EKATERINBURG(
    @ID_SERIES BIGINT
)
RETURNS VARCHAR(4000)
AS
BEGIN
    DECLARE @RESULT VARCHAR(4000)
    SELECT 
        @RESULT = ISNULL(@RESULT + ' ;' + C.CERT_NUMBER, ISNULL(C.CERT_NUMBER, ''))
    FROM CERTIFICATE C
    WHERE ID_SERIES = @ID_SERIES
    RETURN @RESULT
END
GO

IF OBJECT_ID('REPEX_MOVEMENT_EKATERINBURG') IS NULL EXEC('CREATE PROCEDURE REPEX_MOVEMENT_EKATERINBURG AS RETURN')
GO
ALTER PROCEDURE REPEX_MOVEMENT_EKATERINBURG

	(@XMLPARAM NTEXT) AS 

DECLARE @HDOC INT
DECLARE @ID_MOVEMENT BIGINT
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_MOVEMENT = ID_MOVEMENT
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_MOVEMENT BIGINT 'ID_MOVEMENT')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	M.MNEMOCODE,	
	M.DATE,
	STORE_NAME_FROM = (SELECT TOP 1 C.NAME + ' - ' + S.NAME FROM STORE AS S  
		LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
		WHERE S.ID_STORE = M.ID_STORE_FROM),
	STORE_NAME_TO = (SELECT TOP 1 C.NAME + ' - ' + S.NAME FROM STORE AS S 
		LEFT JOIN CONTRACTOR AS C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR 
		WHERE S.ID_STORE = M.ID_STORE_TO),
	DOC = (SELECT TOP 1 '��������� ��������� ' + INV.MNEMOCODE + ' �� '+ CONVERT(VARCHAR(10),INV.DOCUMENT_DATE,104) FROM INVOICE AS INV WHERE M.ID_DOCUMENT_BASE = INV.ID_INVOICE),
	DOC_DATE = (SELECT TOP 1 DATE FROM INVOICE WHERE M.ID_DOCUMENT_BASE = ID_INVOICE)
FROM MOVEMENT AS M
WHERE ID_MOVEMENT = @ID_MOVEMENT

SELECT
	GOODS_NAME = G.NAME + ' ( ' + (SELECT TOP 1 P.NAME FROM PRODUCER AS P WHERE G.ID_PRODUCER = P.ID_PRODUCER) + ' ) ',
	SERIES_NUMBER = S.SERIES_NUMBER,
	QUANTITY = M.QUANTITY,
	UNIT_NAME = DBO.FN_SCALE_NAME(M.ID_SCALING_RATIO),
	PRICE_SALE,
	PRICE_SUMM_SALE = QUANTITY * PRICE_SALE,
	S.BEST_BEFORE,
	REG_CERT_NAME = RC.[NAME],
	FED_CERT_NAME = DBO.FN_CERT_LIST_4_REPEX_MOVEMENT_EKATERINBURG(S.ID_SERIES)
FROM MOVEMENT_ITEM AS M
	INNER JOIN LOT AS L ON L.ID_LOT = M.ID_LOT_FROM
	LEFT JOIN GOODS AS G ON G.ID_GOODS = M.ID_GOODS
	LEFT JOIN SERIES AS S ON S.ID_SERIES = L.ID_SERIES
	LEFT JOIN SCALING_RATIO AS SR ON SR.ID_SCALING_RATIO = M.ID_SCALING_RATIO
	LEFT JOIN UNIT AS U ON U.ID_UNIT = SR.ID_UNIT
	LEFT JOIN REG_CERT AS RC ON L.ID_REG_CERT_GLOBAL = RC.ID_REG_CERT_GLOBAL
WHERE ID_MOVEMENT = @ID_MOVEMENT
ORDER BY GOODS_NAME ASC

SELECT ISNULL(NAME, '') AS COMPANY
FROM CONTRACTOR WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()

RETURN 0
GO