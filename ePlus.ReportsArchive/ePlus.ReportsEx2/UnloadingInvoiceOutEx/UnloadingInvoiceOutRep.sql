SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_UNLOADING_INVOICE_OUT') IS NULL EXEC('CREATE PROCEDURE REPEX_UNLOADING_INVOICE_OUT AS RETURN')
GO
ALTER PROCEDURE REPEX_UNLOADING_INVOICE_OUT
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @ID_INVOICE_OUT_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INVOICE_OUT_GLOBAL = ID_INVOICE_OUT_GLOBAL
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INVOICE_OUT_GLOBAL UNIQUEIDENTIFIER 'ID_INVOICE_OUT_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
    DOC_NUM = I_O.DOC_NUM,
    ID_GOODS = G.ID_GOODS,
    GOODS_NAME = G.NAME,
    QUANTITY = IOI.QUANTITY,
    PRICE_SAL = IOI.PRICE_SAL - IOI.PVAT_SAL,
    VAT_SAL = L.VAT_SAL,
    SUM_SAL = IOI.SUM_SAL,
    GTD_NUMBER = L.GTD_NUMBER,
    COUNTRY_NAME = C.NAME
FROM INVOICE_OUT I_O
INNER JOIN INVOICE_OUT_ITEM IOI ON IOI.ID_INVOICE_OUT_GLOBAL = I_O.ID_INVOICE_OUT_GLOBAL
INNER JOIN LOT L ON L.ID_LOT_GLOBAL = IOI.ID_LOT_GLOBAL
INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
INNER JOIN PRODUCER P ON P.ID_PRODUCER = G.ID_PRODUCER
INNER JOIN COUNTRY C ON C.ID_COUNTRY = P.ID_COUNTRY
WHERE I_O.ID_INVOICE_OUT_GLOBAL = @ID_INVOICE_OUT_GLOBAL


RETURN 0
GO

--exec REPEX_UNLOADING_INVOICE_OUT N'<XML><ID_INVOICE_OUT_GLOBAL>ed181a5a-18b3-404b-81f8-9ecb2037a165</ID_INVOICE_OUT_GLOBAL></XML>'
