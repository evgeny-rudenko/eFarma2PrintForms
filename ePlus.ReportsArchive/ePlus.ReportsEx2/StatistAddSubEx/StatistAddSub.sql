SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_STATIST_ADD_SUB') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_STATIST_ADD_SUB AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_STATIST_ADD_SUB
    @XMLPARAM NTEXT AS

DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME, @ID_CONTRACTOR BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
SELECT TOP 1
	@DATE_FROM = DATE_FROM,
	@DATE_TO = DATE_TO,
	@ID_CONTRACTOR = ID_CONTRACTOR
FROM OPENXML(@HDOC , '/XML') WITH(
	DATE_FROM DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO',
	ID_CONTRACTOR BIGINT 'ID_CONTRACTOR'
)

DECLARE @INVOICE_EXP BIT
DECLARE @ACT_RETURN_TO_CONTRACTOR_INV BIT
DECLARE @ACT_RETURN_TO_BUYER_EXP BIT
DECLARE @ACT_REVALUATION_INV BIT
DECLARE @ACT_REVALUATION_EXP BIT
DECLARE @CASH_SESION_INV BIT
DECLARE @ACT_DEDUCTION_EXP BIT
DECLARE @INVOICE_OUT_INV BIT
DECLARE @BILL_INV BIT
DECLARE @VAT_CORRECT_INV BIT
DECLARE @INVENTORY_SVED_INV BIT
DECLARE @INVENTORY_SVED_EXP BIT
DECLARE @IMPORT_REMAINS_EXP BIT

SELECT @INVOICE_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/INVOICE') WITH(IS_EXP BIT 'IS_EXP')
SELECT @ACT_RETURN_TO_CONTRACTOR_INV = IS_INV FROM OPENXML (@HDOC , '/XML/ACT_RETURN_TO_CONTRACTOR') WITH(IS_INV BIT 'IS_INV')
SELECT @ACT_RETURN_TO_BUYER_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/ACT_RETURN_TO_BUYER') WITH(IS_EXP BIT 'IS_EXP')
SELECT @ACT_REVALUATION_INV = IS_INV FROM OPENXML (@HDOC , '/XML/ACT_REVALUATION') WITH(IS_INV BIT 'IS_INV')
SELECT @ACT_REVALUATION_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/ACT_REVALUATION') WITH(IS_EXP BIT 'IS_EXP')
SELECT @CASH_SESION_INV = IS_INV FROM OPENXML (@HDOC , '/XML/CASH_SESSION') WITH(IS_INV BIT 'IS_INV')
SELECT @ACT_DEDUCTION_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/ACT_DEDUCTION') WITH(IS_EXP BIT 'IS_EXP')
SELECT @INVOICE_OUT_INV = IS_INV FROM OPENXML (@HDOC , '/XML/INVOICE_OUT') WITH(IS_INV BIT 'IS_INV')
SELECT @BILL_INV = IS_INV FROM OPENXML (@HDOC , '/XML/BILL') WITH(IS_INV BIT 'IS_INV')
SELECT @VAT_CORRECT_INV = IS_INV FROM OPENXML (@HDOC , '/XML/VAT_CORRECT') WITH(IS_INV BIT 'IS_INV')
SELECT @INVENTORY_SVED_INV = IS_INV FROM OPENXML (@HDOC , '/XML/INVENTORY_SVED') WITH(IS_INV BIT 'IS_INV')
SELECT @INVENTORY_SVED_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/INVENTORY_SVED') WITH(IS_EXP BIT 'IS_EXP')
SELECT @IMPORT_REMAINS_EXP = IS_EXP FROM OPENXML (@HDOC , '/XML/IMPORT_REMAINS') WITH(IS_EXP BIT 'IS_EXP')

EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT

DECLARE @DOCUMENTS TABLE
(
	DOC_NUM VARCHAR(50),
	DOC_DATE DATETIME,
	DOC_TYPE_NAME VARCHAR(50),
	INV_SUM MONEY,
	EXP_SUM MONEY
)

IF (@INVOICE_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DOCUMENT_DATE,
		DOC_TYPE_NAME = '��������� ���������',
		INV_SUM = NULL,
		EXP_SUM = D.SUM_SUPPLIER
	FROM INVOICE D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DOCUMENT_DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

IF (@ACT_RETURN_TO_CONTRACTOR_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE,
		DOC_TYPE_NAME = '��� �������� ����������',
		INV_SUM = TOTAL,
		EXP_SUM = NULL
	FROM ACT_RETURN_TO_CONTRACTOR D
	WHERE D.DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		D.ID_CONTRACTOR_FROM = @ID_CONTRACTOR
END

IF (@ACT_RETURN_TO_BUYER_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE,
		DOC_TYPE_NAME = '��� �������� �� ����������',
		INV_SUM = NULL,
		EXP_SUM = D.TOTAL
	FROM ACT_RETURN_TO_BUYER D
	WHERE D.DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		D.ID_CONTRACTOR_TO = @ID_CONTRACTOR
END

IF (@ACT_REVALUATION_INV = 1 OR @ACT_REVALUATION_EXP = 1)
BEGIN
	DECLARE @ACTS TABLE
	(
		DOC_NUM VARCHAR(50),
		DOC_DATE DATETIME,
		DOC_TYPE_NAME VARCHAR(50),
		INV_SUM MONEY,
		EXP_SUM MONEY
	)

	INSERT INTO @ACTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE,
		DOC_TYPE_NAME = '��� ����������',
		INV_SUM = (SELECT SUM(CASE WHEN L_NEW.PRICE_SAL - L.PRICE_SAL > 0 THEN (L_NEW.PRICE_SAL - L.PRICE_SAL) * ARI.QUANTITY ELSE 0 END)
					FROM ACT_REVALUATION2_ITEM ARI
						INNER JOIN LOT L ON L.ID_LOT_GLOBAL = ARI.ID_LOT_GLOBAL
						INNER JOIN LOT L_NEW ON L_NEW.ID_DOCUMENT = ARI.ID_ACT_REVALUATION2_GLOBAL AND L_NEW.ID_DOCUMENT_ITEM = ID_ACT_REVALUATION2_ITEM_GLOBAL
					WHERE ARI.ID_ACT_REVALUATION2_GLOBAL = D.ID_ACT_REVALUATION2_GLOBAL),
		EXP_SUM = (SELECT SUM(CASE WHEN L_NEW.PRICE_SAL - L.PRICE_SAL < 0 THEN (L_NEW.PRICE_SAL - L.PRICE_SAL) * ARI.QUANTITY ELSE 0 END)
					FROM ACT_REVALUATION2_ITEM ARI
						INNER JOIN LOT L ON L.ID_LOT_GLOBAL = ARI.ID_LOT_GLOBAL
						INNER JOIN LOT L_NEW ON L_NEW.ID_DOCUMENT = ARI.ID_ACT_REVALUATION2_GLOBAL AND L_NEW.ID_DOCUMENT_ITEM = ID_ACT_REVALUATION2_ITEM_GLOBAL
					WHERE ARI.ID_ACT_REVALUATION2_GLOBAL = D.ID_ACT_REVALUATION2_GLOBAL)
	FROM ACT_REVALUATION2 D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR

IF(@ACT_REVALUATION_INV = 1 AND @ACT_REVALUATION_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM = CASE WHEN INV_SUM = 0 THEN NULL ELSE INV_SUM END,
		EXP_SUM = CASE WHEN EXP_SUM = 0 THEN NULL ELSE ABS(EXP_SUM) END
	FROM @ACTS
END ELSE IF (@ACT_REVALUATION_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM = INV_SUM,
		EXP_SUM = CASE WHEN EXP_SUM <> 0 THEN ABS(EXP_SUM) ELSE NULL END
	FROM @ACTS
	WHERE INV_SUM <> 0
END ELSE IF (@ACT_REVALUATION_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM = CASE WHEN INV_SUM <> 0 THEN INV_SUM ELSE NULL END,
		EXP_SUM = EXP_SUM
	FROM @ACTS
	WHERE EXP_SUM <> 0
END
END

IF (@CASH_SESION_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE_CLOSE,
		DOC_TYPE_NAME = '�������� �����',
		INV_SUM = MAX(D.SUM_SALES - D.SUM_SALES_RETURNS),
		EXP_SUM = NULL
	FROM CASH_SESSION D
		INNER JOIN CASH_REGISTER CR ON CR.ID_CASH_REGISTER = D.ID_CASH_REGISTER
		INNER JOIN CASH_REGISTER_2_STORE CRTS ON CRTS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
		INNER JOIN STORE S ON S.ID_STORE = CRTS.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DATE_CLOSE BETWEEN @DATE_FROM AND @DATE_TO AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
	GROUP BY D.MNEMOCODE, D.DATE_CLOSE
END

IF (@ACT_DEDUCTION_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE,
		DOC_TYPE_NAME = '��� ��������',
		INV_SUM = NULL,
		EXP_SUM = RETAIL_SUM_VAT
	FROM ACT_DEDUCTION D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

IF (@INVOICE_OUT_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DATE,
		DOC_TYPE_NAME = '��������� ���������',
		INV_SUM = D.SUM_SAL,
		EXP_SUM = NULL
	FROM INVOICE_OUT D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

IF (@BILL_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.DOC_NUM,
		DOC_DATE = D.DOC_DATE,
		DOC_TYPE_NAME = '����',
		INV_SUM = (SELECT SUM(I.QUANTITY * L.PRICE_SAL) FROM BILL_ITEM I INNER JOIN LOT L ON L.ID_LOT = I.ID_LOT WHERE I.ID_BILL_GLOBAL = D.ID_BILL_GLOBAL),
		EXP_SUM = NULL
	FROM BILL D
	WHERE D.DOC_DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOC_STATE = 'PROC' AND
		D.ID_SUPPLIER = @ID_CONTRACTOR
END

IF (@VAT_CORRECT_INV = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.DOC_NUM,
		DOC_DATE = D.DOC_DATE,
		DOC_TYPE_NAME = '������������� ��������� ������',
		INV_SUM = D.SUM_SAL,
		EXP_SUM = NULL
	FROM VAT_CORRECT D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DOC_DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOC_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

IF (@INVENTORY_SVED_INV = 1 OR @INVENTORY_SVED_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.DOC_NUM,
		DOC_DATE = D.DOC_DATE,
		DOC_TYPE_NAME = '�������������� �������',
		INV_SUM = D.SUM_SAL,
		EXP_SUM = NULL
	FROM INVENTORY_SVED D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DOC_DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOC_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

IF (@IMPORT_REMAINS_EXP = 1)
BEGIN
	INSERT INTO @DOCUMENTS
	(
		DOC_NUM,
		DOC_DATE,
		DOC_TYPE_NAME,
		INV_SUM,
		EXP_SUM
	)
	SELECT 
		DOC_NUM = D.MNEMOCODE,
		DOC_DATE = D.DOCUMENT_DATE,
		DOC_TYPE_NAME = '���� ��������',
		INV_SUM = NULL,
		EXP_SUM = D.SUM_SUPPLIER
	FROM IMPORT_REMAINS D
		INNER JOIN STORE S ON S.ID_STORE = D.ID_STORE
		INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	WHERE D.DOCUMENT_DATE BETWEEN @DATE_FROM AND @DATE_TO AND
		D.DOCUMENT_STATE = 'PROC' AND
		C.ID_CONTRACTOR = @ID_CONTRACTOR
END

SELECT * FROM @DOCUMENTS
ORDER BY DOC_TYPE_NAME, DOC_DATE

RETURN 0
GO

/*
EXEC REPEX_STATIST_ADD_SUB N'
<XML>
	<DATE_FROM>2009-01-01T10:34:25.484</DATE_FROM>
	<DATE_TO>2009-06-02T10:34:25.484</DATE_TO>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
<IMPORT_REMAINS>
	<IS_INV>1</IS_INV>
	<IS_EXP>1</IS_EXP>
</IMPORT_REMAINS>
</XML>'*/