SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REPEX_GOODS_SUPPLY_REQUEST_SUMMARY') IS NULL) EXEC ('CREATE PROCEDURE REPEX_GOODS_SUPPLY_REQUEST_SUMMARY AS RETURN')
GO
ALTER PROCEDURE REPEX_GOODS_SUPPLY_REQUEST_SUMMARY
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
	DM.ID_DOC_MOVEMENT, 
	II.ID_GOODS, 
	ID_SUPPLIER = DM.ID_CONTRACTOR_FROM, 
	SUPPLIER_NAME = C_SUP.NAME, 
	DM.DATE_OP, 
	II.QUANTITY, 
	II.SUPPLIER_PRICE_VAT, 
	II.ID_INVOICE, II.ID_INVOICE_ITEM
INTO #TEMP	
FROM DOC_MOVEMENT DM
INNER JOIN CONTRACTOR C_SUP ON DM.ID_CONTRACTOR_FROM = C_SUP.ID_CONTRACTOR
INNER JOIN INVOICE I ON DM.ID_DOCUMENT = I.ID_INVOICE_GLOBAL AND I.DOCUMENT_STATE = 'PROC'
INNER JOIN INVOICE_ITEM II ON I.ID_INVOICE_GLOBAL = II.ID_INVOICE_GLOBAL
WHERE DM.ID_TABLE = 2
ORDER BY II.ID_GOODS, DM.DATE_OP

DELETE #TEMP
FROM #TEMP T WHERE 
EXISTS (SELECT NULL
FROM #TEMP T1 WHERE T.ID_GOODS = T1.ID_GOODS AND (T.QUANTITY > T1.QUANTITY OR 
	(T.QUANTITY = T1.QUANTITY AND T.SUPPLIER_PRICE_VAT < T1.SUPPLIER_PRICE_VAT) ))

SELECT 
	ID_AP = R.ID_CONTRACTOR, 
	R.ID_STORE,
	AP_CONTRACTOR_NAME = C_AP.NAME, 
	AP_STORE_NAME = S.NAME, 
	GOODS_NAME = G.NAME, 
	ADV_SUPPLIER_NAME = C_S.NAME, 
	ORDERED_QTY = ISNULL(SUM(RI.QTY), 0), 
	AP.QUANTITY_MIN, 
	A.REMAINS, 
	T.SUPPLIER_NAME, 
	SUPPLY_DATE = T.DATE_OP, 
	SUPPLIED_QTY = T.QUANTITY, 
	T.SUPPLIER_PRICE_VAT
FROM REQUEST_ITEM RI
INNER JOIN REQUEST R ON RI.ID_REQUEST_SUMMARY = R.ID_REQUEST
INNER JOIN STORE S ON R.ID_STORE = S.ID_STORE
INNER JOIN CONTRACTOR C_AP ON R.ID_CONTRACTOR = C_AP.ID_CONTRACTOR
INNER JOIN GOODS G ON G.ID_GOODS_GLOBAL = RI.ID_GOODS
LEFT JOIN ASSORTMENT_PLAN AP ON G.ID_GOODS = AP.ID_GOODS AND AP.DATE_DELETED IS NULL
LEFT JOIN CONTRACTOR C_S ON AP.ID_SUPPLIER = C_S.ID_CONTRACTOR
LEFT JOIN 
(SELECT 
	L.ID_GOODS, 
	REMAINS = SUM(ISNULL((L.QUANTITY_ADD - L.QUANTITY_SUB-L.QUANTITY_RES) 
		* CONVERT(MONEY, SR.NUMERATOR) / SR.DENOMINATOR, 0))
FROM LOT L
INNER JOIN SCALING_RATIO SR ON L.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
GROUP BY L.ID_GOODS
) A 
ON G.ID_GOODS = A.ID_GOODS
LEFT JOIN (
	SELECT T.ID_GOODS, T.SUPPLIER_NAME, T.SUPPLIER_PRICE_VAT, 
	T.DATE_OP, T.QUANTITY FROM #TEMP T
		WHERE 
		T.ID_DOC_MOVEMENT = (SELECT TOP 1 ID_DOC_MOVEMENT FROM #TEMP T1
			WHERE T.ID_GOODS = T1.ID_GOODS AND T1.DATE_OP = (SELECT MAX(T2.DATE_OP) FROM #TEMP T2
				WHERE T1.ID_GOODS = T2.ID_GOODS))
) T ON A.ID_GOODS = T.ID_GOODS
WHERE R.DOC_STATE = 'PROC' AND R.IS_SUMMARY = 1 
AND RI.ID_REQUEST_SUMMARY=@ID_GLOBAL
GROUP BY R.ID_CONTRACTOR, R.ID_STORE, G.ID_GOODS, C_AP.NAME, S.NAME, G.NAME, 
	C_S.NAME, AP.QUANTITY_MIN, A.REMAINS, T.SUPPLIER_NAME, T.DATE_OP, 
	T.QUANTITY, T.SUPPLIER_PRICE_VAT
ORDER BY C_AP.NAME, S.NAME, G.NAME

	SELECT 
        R.DOC_NUM,
        R.DOC_DATE
    FROM REQUEST R
    WHERE ID_REQUEST=@ID_GLOBAL

RETURN
GO