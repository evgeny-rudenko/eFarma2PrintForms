SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_INVOICE_FULL') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_INVOICE_FULL AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_INVOICE_FULL
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT, @ID_GLOBAL UNIQUEIDENTIFIER
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
		
SELECT TOP 1 
	@ID_GLOBAL = ID_GLOBAL
FROM OPENXML(@HDOC , '/XML')
WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
		
EXEC SP_XML_REMOVEDOCUMENT @HDOC
 
SELECT 
	DOC_NUM = I.MNEMOCODE,
	DOC_DATE = I.DOCUMENT_DATE

FROM INVOICE AS I	
WHERE I.ID_INVOICE_GLOBAL = @ID_GLOBAL

DECLARE @ROWS TABLE
(
	[ID] BIGINT NOT NULL IDENTITY,
	ID_LOT_GLOBAL UNIQUEIDENTIFIER,
	GOODS_NAME VARCHAR(512),
	DOCUMENT VARCHAR(50),
	DOC_NUMBER VARCHAR(30),
	DOC_DATE DATETIME,
	UNIT_NAME VARCHAR(60),
	INITIAL_BALANCE MONEY,
	QUANTITY_ADD MONEY,
	QUANTITY_SUB MONEY,
	FINAL_BALANCE MONEY,
	QUANTITY_RES MONEY,
	SUPPLIER_PRICE MONEY,
	RETAIL_PRICE MONEY,
	ADPRICE MONEY
)

DECLARE INVOICE_ITEMS_CURSOR CURSOR FOR
SELECT 
	ID_INVOICE_ITEM_GLOBAL
FROM INVOICE_ITEM
WHERE ID_INVOICE_GLOBAL = @ID_GLOBAL

DECLARE @ID UNIQUEIDENTIFIER

OPEN INVOICE_ITEMS_CURSOR
FETCH NEXT FROM INVOICE_ITEMS_CURSOR INTO @ID
WHILE @@FETCH_STATUS = 0
BEGIN
	IF ((SELECT COUNT(*)
		FROM LOT_MOVEMENT AS LM
			INNER JOIN LOT AS L ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
			WHERE L.ID_DOCUMENT_ITEM = @ID AND CODE_OP <> 'INVOICE') = 0)
	BEGIN
		INSERT INTO @ROWS
			(
				ID_LOT_GLOBAL,
				GOODS_NAME,
				DOCUMENT,
				INITIAL_BALANCE,
				FINAL_BALANCE,
				SUPPLIER_PRICE,
				RETAIL_PRICE,
				ADPRICE
			)
		SELECT
			ID_LOT_GLOBAL = L.ID_LOT_GLOBAL,
			GOODS_NAME = G.NAME + ', ' + U.NAME + CASE WHEN S.SERIES_NUMBER IS NULL OR S.SERIES_NUMBER = '' THEN '' ELSE ', ' + S.SERIES_NUMBER END + CASE WHEN II.BAR_CODE IS NULL OR II.BAR_CODE = '' THEN '' ELSE ', ' + II.BAR_CODE END,
			DOCUMENT = 'inv',
			INITIAL_BALANCE = II.QUANTITY,
			FINAL_BALANCE = II.QUANTITY,
			SUPPLIER_PRICE = L.PRICE_SUP,
			RETAIL_PRICE = L.PRICE_SAL,
			ADPRICE = II.RETAIL_ADPRICE
		FROM INVOICE_ITEM AS II
			INNER JOIN LOT AS L ON II.ID_INVOICE_ITEM_GLOBAL = L.ID_DOCUMENT_ITEM
			INNER JOIN GOODS AS G ON G.ID_GOODS = L.ID_GOODS			
			INNER JOIN SCALING_RATIO AS SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
			INNER JOIN UNIT AS U ON U.ID_UNIT = SR.ID_UNIT
			LEFT JOIN SERIES AS S ON S.ID_SERIES = L.ID_SERIES
		WHERE II.ID_INVOICE_ITEM_GLOBAL = @ID
	END
	ELSE
	BEGIN
		DECLARE @LOTS TABLE
		(
			[ID] BIGINT NOT NULL IDENTITY,
			ID_LOT_GLOBAL UNIQUEIDENTIFIER,
			ID_DOCUMENT UNIQUEIDENTIFIER,
			ID_DOCUMENT_ITEM UNIQUEIDENTIFIER,
			CODE_OP VARCHAR(16),
			QUANTITY_ADD MONEY,
			QUANTITY_SUB MONEY,
			QUANTITY_RES MONEY
		)

		INSERT INTO @LOTS
		(
			ID_LOT_GLOBAL,
			ID_DOCUMENT,
			ID_DOCUMENT_ITEM,
			CODE_OP,
			QUANTITY_ADD,
			QUANTITY_SUB,
			QUANTITY_RES
		)
		SELECT 
			L.ID_LOT_GLOBAL,
			LM.ID_DOCUMENT,
			LM.ID_DOCUMENT_ITEM,
			LM.CODE_OP,
			LM.QUANTITY_ADD,
			LM.QUANTITY_SUB,
			LM.QUANTITY_RES	
		FROM INVOICE_ITEM II
			INNER JOIN LOT L ON L.ID_DOCUMENT_ITEM = II.ID_INVOICE_ITEM_GLOBAL
			INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
		WHERE II.ID_INVOICE_ITEM_GLOBAL  = @ID AND LM.CODE_OP <> 'INVOICE'

		INSERT INTO @ROWS
		(
			ID_LOT_GLOBAL,
			GOODS_NAME,
			DOCUMENT,
			DOC_NUMBER,
			DOC_DATE,
			UNIT_NAME,
			INITIAL_BALANCE,
			QUANTITY_ADD,
			QUANTITY_SUB,
			FINAL_BALANCE,
			QUANTITY_RES,
			SUPPLIER_PRICE,
			RETAIL_PRICE,
			ADPRICE
		)
		SELECT
			ID_LOT_GLOBAL = LT.ID_LOT_GLOBAL,
			GOODS_NAME = G.NAME + ', ' + U.NAME + CASE WHEN S.SERIES_NUMBER IS NULL OR S.SERIES_NUMBER = '' THEN '' ELSE ', ' + S.SERIES_NUMBER END + CASE WHEN II.BAR_CODE IS NULL OR II.BAR_CODE = '' THEN '' ELSE ', ' + II.BAR_CODE END,
			DOCUMENT = CASE WHEN LT.CODE_OP = 'MOVE' THEN '�����������'
							WHEN LT.CODE_OP = 'CHEQUE' THEN '���'
							WHEN LT.CODE_OP = 'DED' THEN '��� ��������'
							WHEN LT.CODE_OP = 'DIS' THEN '����������������'
							WHEN LT.CODE_OP = 'ACT_R2C'THEN '������� ����������'
							WHEN LT.CODE_OP = 'ACT_REV'THEN '����������'                                    
							WHEN LT.CODE_OP = 'INVENTORY_SVED' THEN '������� ���. ���������'
							WHEN LT.CODE_OP = 'INVOICE_OUT' THEN '��������� ���������'
							WHEN LT.CODE_OP = 'ACT_R2B' THEN '������� �� ����������'
							WHEN LT.CODE_OP = 'INVENTORY_VED' THEN '���. ���������'
							WHEN LT.CODE_OP = 'BILL' THEN '����'
							WHEN LT.CODE_OP = 'PROD' THEN '������������'
						END,
			DOC_NUMBER = CASE WHEN LT.CODE_OP = 'MOVE' THEN M.MNEMOCODE
							WHEN LT.CODE_OP = 'ACT_R2C' THEN AR2C.MNEMOCODE
							WHEN LT.CODE_OP = 'DED' THEN AD.MNEMOCODE
							WHEN LT.CODE_OP = 'CHEQUE' THEN CONVERT(VARCHAR, CH.ID_CHEQUE)
							WHEN LT.CODE_OP = 'DIS' THEN ADIS.MNEMOCODE
							WHEN LT.CODE_OP = 'ACT_REV'THEN AR.MNEMOCODE
							WHEN LT.CODE_OP = 'INVENTORY_SVED' THEN ISV.DOC_NUM
							WHEN LT.CODE_OP = 'INVOICE_OUT' THEN INVO.MNEMOCODE
							WHEN LT.CODE_OP = 'ACT_R2B' THEN ARB.MNEMOCODE
							WHEN LT.CODE_OP = 'INVENTORY_VED' THEN IVED.DOC_NUM
							WHEN LT.CODE_OP = 'BILL' THEN BILL.DOC_NUM
							WHEN LT.CODE_OP = 'PROD' THEN PROD.MNEMOCODE
						END,
			DOC_DATE = CASE	WHEN LT.CODE_OP = 'MOVE' THEN M.DATE
							WHEN LT.CODE_OP = 'ACT_R2C' THEN AR2C.DATE
							WHEN LT.CODE_OP = 'DED' THEN AD.DATE
							WHEN LT.CODE_OP = 'CHEQUE' THEN CH.DATE_CHEQUE
							WHEN LT.CODE_OP = 'DIS' THEN ADIS.DATE
							WHEN LT.CODE_OP = 'ACT_REV'THEN AR.DATE
							WHEN LT.CODE_OP = 'INVENTORY_SVED' THEN ISV.DOC_DATE
							WHEN LT.CODE_OP = 'INVOICE_OUT' THEN INVO.DATE
							WHEN LT.CODE_OP = 'ACT_R2B' THEN ARB.DATE
							WHEN LT.CODE_OP = 'INVENTORY_VED' THEN IVED.DOC_DATE
							WHEN LT.CODE_OP = 'BILL' THEN BILL.DOC_DATE
							WHEN LT.CODE_OP = 'PROD' THEN PROD.DATE
						END,
			UNIT_NAME = CONVERT(VARCHAR, SR.NUMERATOR) + '/' + CONVERT(VARCHAR, SR.DENOMINATOR) + ' ' + U.SHORT_NAME,
			INITIAL_BALANCE = II.QUANTITY,
			QUANTITY_ADD = LT.QUANTITY_ADD,
			QUANTITY_SUB = LT.QUANTITY_SUB,
			FINAL_BALANCE = II.QUANTITY + (
											SELECT SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB) 
											FROM LOT_MOVEMENT AS LM
											WHERE LM.ID_LOT_GLOBAL = LT.ID_LOT_GLOBAL AND LM.CODE_OP <> 'INVOICE'
											GROUP BY LM.ID_LOT_GLOBAL
											),
			QUANTITY_RES = LT.QUANTITY_RES,
			SUPPLIER_PRICE = L.PRICE_SUP,
			RETAIL_PRICE = L.PRICE_SAL,
			ADPRICE = L.ADPRICE_SAL			

		FROM @LOTS AS LT
			INNER JOIN LOT AS L ON LT.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
			INNER JOIN GOODS AS G ON G.ID_GOODS = L.ID_GOODS
			INNER JOIN SCALING_RATIO AS SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
			INNER JOIN UNIT AS U ON U.ID_UNIT = SR.ID_UNIT
			LEFT JOIN SERIES AS S ON S.ID_SERIES = L.ID_SERIES
			INNER JOIN INVOICE_ITEM AS II ON II.ID_INVOICE_ITEM_GLOBAL = L.ID_DOCUMENT_ITEM

			LEFT JOIN MOVEMENT M ON M.ID_MOVEMENT_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN ACT_RETURN_TO_CONTRACTOR AR2C ON AR2C.ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN ACT_DEDUCTION AD ON AD.ID_ACT_DEDUCTION_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN CHEQUE CH ON (CH.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL AND EXISTS (SELECT NULL FROM CHEQUE_ITEM CHI WHERE CHI.ID_CHEQUE_ITEM_GLOBAL = LT.ID_DOCUMENT_ITEM AND CH.ID_CHEQUE_GLOBAL = CHI.ID_CHEQUE_GLOBAL))
			LEFT JOIN ACT_DISASSEMBLING ADIS ON ADIS.ID_ACT_DISASSEMBLING_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN DBO.ACT_REVALUATION2 AR ON AR.ID_ACT_REVALUATION2_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN DBO.INVENTORY_SVED ISV ON ISV.ID_INVENTORY_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN INVOICE_OUT INVO ON INVO.ID_INVOICE_OUT_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN DBO.ACT_RETURN_TO_BUYER ARB ON ARB.ID_ACT_RETURN_TO_BUYER_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN DBO.INVENTORY_VED IVED ON IVED.ID_INVENTORY_VED_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN BILL ON BILL.ID_BILL_GLOBAL = LT.ID_DOCUMENT
			LEFT JOIN PRODUCTION PROD ON PROD.ID_PRODUCTION_GLOBAL = LT.ID_DOCUMENT
	END
	FETCH NEXT FROM INVOICE_ITEMS_CURSOR INTO @ID
	delete from @lots
END

CLOSE INVOICE_ITEMS_CURSOR
DEALLOCATE INVOICE_ITEMS_CURSOR

SELECT 
	ID_LOT_GLOBAL,
	GOODS_NAME,
	DOCUMENT,
	DOC_NUMBER,
	DOC_DATE,
	UNIT_NAME,
	INITIAL_BALANCE,
	QUANTITY_ADD,
	QUANTITY_SUB,
	FINAL_BALANCE,
	QUANTITY_RES = CASE WHEN QUANTITY_RES = 0 THEN NULL ELSE QUANTITY_RES END,
	SUPPLIER_PRICE,
	RETAIL_PRICE,
	ADPRICE
FROM @ROWS
ORDER BY GOODS_NAME

SELECT 
	LOTS_COUNT = COUNT(*), 
	GOODS_COUNT = SUM(QUANTITY),
	VAT_0 = SUM(CASE WHEN L.VAT_SUP = 0 THEN L.PVAT_SUP * II.QUANTITY ELSE 0 END),
	VAT_10 = SUM(CASE WHEN L.VAT_SUP = 10 THEN L.PVAT_SUP * II.QUANTITY ELSE 0 END),
	VAT_18 = SUM(CASE WHEN L.VAT_SUP = 18 THEN L.PVAT_SUP * II.QUANTITY ELSE 0 END),
	ADPRICE_SUM = SUM(II.RETAIL_ADPRICE),
	SUPPLIER_NOVAT_SUM = SUM((L.PRICE_SUP - L.PVAT_SUP) * II.QUANTITY),
	RETAIL_NOVAT_SUM = SUM((L.PRICE_SAL - L.PVAT_SAL) * II.QUANTITY),
	SUPPLIER_VAT_SUM = SUM(L.PVAT_SUP * II.QUANTITY),
	RETAIL_VAT_SUM = SUM(L.PVAT_SAL * II.QUANTITY),
	SUPPLIER_SUM = SUM(L.PRICE_SUP * II.QUANTITY),
	RETAIL_SUM = SUM(L.PRICE_SAL * II.QUANTITY)
FROM INVOICE_ITEM AS II
	INNER JOIN LOT AS L ON II.ID_INVOICE_ITEM_GLOBAL = L.ID_DOCUMENT_ITEM
WHERE II.ID_INVOICE_GLOBAL = @ID_GLOBAL

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO

--EXEC REPEX_INVOICE_FULL '<XML><ID_GLOBAL>D627AE31-05A9-4A01-AB88-CB57DE77C647</ID_GLOBAL></XML>'

