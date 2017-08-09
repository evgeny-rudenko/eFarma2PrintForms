IF (OBJECT_ID('REPEX_BILL') IS NULL) EXEC ('CREATE PROCEDURE REPEX_BILL AS RETURN')
GO
ALTER PROCEDURE REPEX_BILL
    @XMLPARAM NTEXT 
AS

DECLARE	@HDOC INT  
DECLARE @ID_BILL_GLOBAL UNIQUEIDENTIFIER

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
	SELECT	TOP 1 @ID_BILL_GLOBAL = ID_BILL_GLOBAL 
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_BILL_GLOBAL UNIQUEIDENTIFIER 'ID_BILL_GLOBAL')
EXEC	SP_XML_REMOVEDOCUMENT @HDOC
-- SUP - ��������� (���������� ����), C - ��������, �� ���� ������� ����� ����������� ������, REC - ���������� ������ (��� ����� �������)
SELECT TOP 1 
	BILL_NUMBER = B.DOC_NUM, 
	BILL_DATE   = CONVERT(VARCHAR , B.DOC_DATE , 104) ,
	-- ���������� � ���������� ������
	SUPPLIER_INFO = ISNULL('��� ' + SUP.INN+', ','') + ISNULL('��� ' + SUP.KPP + ', ','') + ISNULL(SUP.[FULL_NAME]+' ',ISNULL(SUP.NAME+' ', '')) + ISNULL(SUP.BANK + ', ','') + ISNULL(SUP.ADDRESS+', ','') + ISNULL('���.: ' + SUP.PHONE,''),
	-- ���������� � ���������� ������
	RECEPIENT_INFO = ISNULL('��� ' + REC.INN+', ','') + ISNULL('��� ' + REC.KPP + ', ','') + ISNULL(REC.[FULL_NAME]+' ',ISNULL(REC.NAME+' ', '')) + ISNULL(REC.BANK + ', ','') + ISNULL(REC.ADDRESS+', ','') + ISNULL('���.: ' + REC.PHONE,''),
	-- ���������� � ���������� �����
	COMPANY_NAME = ISNULL(C.FULL_NAME, C.[NAME]),
	COMPANY_ADDRESS = C.ADDRESS,
	COMPANY_PHONE = C.PHONE,
	COMPANY_INN = C.INN,
	COMPANY_KPP = C.KPP,
	COMPANY_BANK = C.BANK,
	COMPANY_BANK_ADDRESS = C.BANK_ADDRESS,
	COMPANY_ACCOUNT = C.ACCOUNT,
	COMPANY_CORR_BANK = C.CORR_BANK,
	COMPANY_BIK = C.BIK,
	COMPANY_CORR_ACCOUNT = C.CORR_ACCOUNT,
	-- �������
	DIRECTOR_FIO = SUP.DIRECTOR_FIO,
	BUH_FIO = SUP.BUH_FIO
FROM BILL B(NOLOCK) 
INNER JOIN CONTRACTOR SUP(NOLOCK) ON (B.ID_SUPPLIER = SUP.ID_CONTRACTOR)
INNER JOIN CONTRACTOR REC(NOLOCK) ON (B.ID_CONTRACTOR =  REC.ID_CONTRACTOR)
INNER JOIN CONTRACTOR C(NOLOCK) ON (B.ID_CONTRACTOR_PAY =  C.ID_CONTRACTOR)
WHERE B.ID_BILL_GLOBAL = @ID_BILL_GLOBAL

SELECT	
	GOODS_NAME = G.[NAME] + ', �������������: ' + P.[NAME] + '(' +C.NAME+ '), ��� ' + CONVERT(VARCHAR(4), BI.VAT_SAL)+'%',
	QUANTITY = BI.QUANTITY,
	UNIT_NAME = U.SHORT_NAME,		
	RETAIL_PRICEV = BI.PRICE_SAL, 
	RETAIL_SUMV   = BI.PRICE_SAL * BI.QUANTITY,
	VAT = BI.PVAT_SAL * BI.QUANTITY,
    VAT_SUM10 = CASE WHEN BI.VAT_SAL=10 THEN BI.PVAT_SAL * BI.QUANTITY ELSE 0 END,
    VAT_SUM18 = CASE WHEN BI.VAT_SAL=18 THEN BI.PVAT_SAL * BI.QUANTITY ELSE 0 END
FROM BILL_ITEM BI(NOLOCK)
INNER JOIN GOODS G(NOLOCK) ON G.ID_GOODS = BI.ID_GOODS
INNER JOIN PRODUCER P(NOLOCK) ON P.ID_PRODUCER = G.ID_PRODUCER 
INNER JOIN LOT L(NOLOCK) ON L.ID_LOT = BI.ID_LOT
INNER JOIN SCALING_RATIO SR(NOLOCK) ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO 
INNER JOIN UNIT U(NOLOCK) ON U.ID_UNIT = SR.ID_UNIT
INNER JOIN COUNTRY C(NOLOCK) ON P.ID_COUNTRY = C.ID_COUNTRY
WHERE BI.ID_BILL_GLOBAL =  @ID_BILL_GLOBAL
ORDER BY GOODS_NAME
RETURN
GO
--exec REPEX_NEWBILL @xmlParam = N'<XML><ID_BILL_GLOBAL>96f7957b-87fc-4be0-9b30-e064759721d1</ID_BILL_GLOBAL></XML>'