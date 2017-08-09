SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_ACT_DEDUCTION_TORG_16_RIGLA') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_ACT_DEDUCTION_TORG_16_RIGLA AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_ACT_DEDUCTION_TORG_16_RIGLA
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT
DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
		
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
		
SELECT @ID_GLOBAL = ID_GLOBAL
FROM OPENXML(@HDOC , '/XML') WITH(ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL')
		
EXEC SP_XML_REMOVEDOCUMENT @HDOC

SELECT
	CONTRACTOR = C.LEGAL_PERS,
	STORE = C.NAME,
	DOC_NUM = AD.MNEMOCODE,
	DOC_DATE = AD.[DATE],
	COMMENT = AD.COMMENT,
	ORDER_NUMBER = isnull(AD.ORDER_NUMBER, ''),
	ORDER_DATE = AD.ORDER_DATE,
	DEDUCTION_REASON = DR.NAME
FROM ACT_DEDUCTION AD
	INNER JOIN STORE S ON S.ID_STORE = AD.ID_STORE
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	LEFT JOIN INVOICE I ON I.ID_INVOICE = AD.ID_BASE_DOCUMENT
	LEFT JOIN DEDUCTION_REASON DR ON DR.ID_DEDUCTION_REASON = AD.ID_DEDUCTION_REASON
WHERE AD.ID_ACT_DEDUCTION_GLOBAL = @ID_GLOBAL

IF (SELECT TOP 1 VALUE FROM SYS_OPTION WHERE CODE = 'INVENTORY_CALC_PRICE') = 'SUP'
BEGIN

SELECT
	GOODS_NAME = G.NAME,
	ID_GOODS = G.CODE,
	UNIT_NAME = U.NAME,
	OKEI_CODE = U.OKEI_CODE,
	QUANTITY = ADI.QUANTITY,
	PRICE_SAL = CASE WHEN CT.USE_VAT = 1 THEN L.PRICE_SUP - L.PVAT_SUP ELSE ADI.PRICE_SUP END,
	SUM_SAL = (CASE WHEN CT.USE_VAT = 1 THEN L.PRICE_SUP - L.PVAT_SUP ELSE ADI.PRICE_SUP END) * ADI.QUANTITY
FROM ACT_DEDUCTION_ITEM ADI
	INNER JOIN GOODS G ON G.ID_GOODS = ADI.ID_GOODS	
	INNER JOIN ACT_DEDUCTION AD ON AD.ID_ACT_DEDUCTION_GLOBAL = ADI.ID_ACT_DEDUCTION_GLOBAL
	INNER JOIN STORE S ON S.ID_STORE = AD.ID_STORE 
	INNER JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR = S.ID_CONTRACTOR
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = ADI.ID_LOT_GLOBAL
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
WHERE ADI.ID_ACT_DEDUCTION_GLOBAL = @ID_GLOBAL

END
ELSE
BEGIN
SELECT
	GOODS_NAME = G.NAME,
	ID_GOODS = G.CODE,
	UNIT_NAME = U.NAME,
	OKEI_CODE = U.OKEI_CODE,
	QUANTITY = ADI.QUANTITY,
	PRICE_SAL = ADI.PRICE_ACC,
	SUM_SAL = ADI.SUM_ACC
FROM ACT_DEDUCTION_ITEM ADI
	INNER JOIN GOODS G ON G.ID_GOODS = ADI.ID_GOODS
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = ADI.ID_LOT_GLOBAL
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
WHERE ADI.ID_ACT_DEDUCTION_GLOBAL = @ID_GLOBAL
ORDER BY G.NAME
END

SELECT DISTINCT
	INCOME_DATE = L.INVOICE_DATE,
	DEDUCTION_DATE = AD.DATE,
	INCOMING_NUMBER = L.INCOMING_NUM,
	INCOMING_DATE = L.INCOMING_DATE
FROM ACT_DEDUCTION AD
	INNER JOIN ACT_DEDUCTION_ITEM ADI ON ADI.ID_ACT_DEDUCTION_GLOBAL = AD.ID_ACT_DEDUCTION_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = ADI.ID_LOT_GLOBAL
WHERE AD.ID_ACT_DEDUCTION_GLOBAL = @ID_GLOBAL
ORDER BY L.INVOICE_DATE
	
-- �������� �������� ����� �������� �����������-������
select
	STORE = s.NAME
from dbo.STORE s
left join dbo.STORE_TYPE st on st.ID_STORE_TYPE_GLOBAL = s.ID_STORE_TYPE_GLOBAL
where ID_CONTRACTOR = (
    select top 1
        ID_CONTRACTOR
    from dbo.CONTRACTOR
    where ID_CONTRACTOR_GLOBAL = dbo.FN_CONTRACTOR_SELF_GLOBAL())
and st.MNEMOCODE = 'MAIN'
and not (s.MNEMOCODE LIKE '%[^0-9]%')

RETURN 0
GO