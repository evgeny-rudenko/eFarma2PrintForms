SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_PAYMENT') IS NULL EXEC('CREATE PROCEDURE REPEX_PAYMENT AS RETURN')
GO
ALTER  PROCEDURE REPEX_PAYMENT
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @ID_PAYMENT_ORDER BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT @ID_PAYMENT_ORDER = ID_PAYMENT_ORDER
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_PAYMENT_ORDER BIGINT 'ID_PAYMENT_ORDER')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

DECLARE @NUM BIGINT
SET @NUM = (SELECT COUNT_BIG(*) FROM PAYMENT_ORDER_ITEM WHERE ID_PAYMENT_ORDER_GLOBAL = (SELECT ID_PAYMENT_ORDER_GLOBAL FROM PAYMENT_ORDER WHERE ID_PAYMENT_ORDER = @ID_PAYMENT_ORDER))

SELECT 
	MNEMOCODE = P.MNEMOCODE,
	P_DATE = P.[DATE],
	TYPE_P = P.PAYMENT_TYPE,
	PVAT_SUM = CASE WHEN P.INCLUDE_VAT = 0 AND @NUM = 0 THEN P.SUM_VAT + P.PVAT_SUM ELSE P.SUM_VAT END,
	INN = C.INN,
	KPP = C.KPP,
	NAME_ADD = ISNULL(NULLIF(LTRIM(C.FULL_NAME), ''), C.[NAME]),
	
	ACCOUNT = COALESCE(AC.ACCOUNT_NAME, C.ACCOUNT, ''),
	NAME_BANK = COALESCE(AC.BANK+' '+AC.ADDRESS_BANK , C.BANK+' '+C.BANK_ADDRESS,''),
	BIK = COALESCE(AC.BIK, C.BIK, ''),
	CORR_ACCOUNT = COALESCE(AC.CORR_ACCOUNT, C.CORR_ACCOUNT,''),
	
	ACCOUNT_P = COALESCE(AC_P.ACCOUNT_NAME, C_P.ACCOUNT, ''),
	NAME_BANK_P = COALESCE(AC_P.BANK+' '+AC_P.ADDRESS_BANK , C_P.BANK+' '+C_P.BANK_ADDRESS,''),
	BIK_P = COALESCE(AC_P.BIK, C_P.BIK, ''),
	CORR_ACCOUNT_P = COALESCE(AC_P.CORR_ACCOUNT, C_P.CORR_ACCOUNT,''),
	
	INN_P = C_P.INN,
	KPP_P = C_P.KPP,
	NAME_ADD_P = ISNULL(NULLIF(LTRIM(C_P.[FULL_NAME]), ''), C_P.[NAME]),
	TYPE_OP = '01',
	PAYMENT_PERIOD = P.PAYMENT_PERIOD,
	PAYMENT_QUEUE = P.PAYMENT_QUEUE,
	PAYMENT_ASSINGMENT = P.PAYMENT_ASSINGMENT,
    SUM_DOC = CASE WHEN P.INCLUDE_VAT = 0 AND @NUM = 0 THEN P.SUM_VAT + P.PVAT_SUM ELSE P.SUM_VAT END
FROM PAYMENT_ORDER P
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = P.ID_PAYER
	LEFT JOIN CONTRACTOR C_P ON C_P.ID_CONTRACTOR = P.ID_RECIPIENT
	LEFT JOIN ACCOUNT AC ON AC.ID_ACCOUNT_GLOBAL = P.PAYER_ACCOUNT
	LEFT JOIN ACCOUNT AC_P ON AC_P.ID_ACCOUNT_GLOBAL = P.RECIPIENT_ACCOUNT
WHERE P.ID_PAYMENT_ORDER = @ID_PAYMENT_ORDER


DECLARE @NDS VARCHAR(100)
DECLARE @ID_TAX_TYPE BIGINT
DECLARE @TAX_RATE MONEY

IF (@NUM <> 0)
BEGIN

SELECT
	SUM_VAT_10 = ISNULL(SUM(CASE WHEN II.SUPPLIER_VAT = 10 THEN II.SUPPLIER_VAT_SUM ELSE 0 END), 0),
	SUM_VAT_18 = ISNULL(SUM(CASE WHEN II.SUPPLIER_VAT = 18 THEN II.SUPPLIER_VAT_SUM ELSE 0 END), 0),
	SUM_NO = 0
	--select * 
FROM PAYMENT_ORDER PO
	INNER JOIN PAYMENT_ORDER_ITEM POI ON POI.ID_PAYMENT_ORDER_GLOBAL = PO.ID_PAYMENT_ORDER_GLOBAL
	INNER JOIN INVOICE I ON I.ID_INVOICE = POI.ID_DOCUMENT
	INNER JOIN INVOICE_ITEM II ON II.ID_INVOICE_GLOBAL = I.ID_INVOICE_GLOBAL
WHERE PO.ID_PAYMENT_ORDER = @ID_PAYMENT_ORDER

END
ELSE
BEGIN
	-- �������� XML-����� �� �������� ���
	DECLARE @PAR VARCHAR(8000)
	SELECT TOP 1 @PAR = TAX_PARAMS
	FROM PAYMENT_ORDER
	WHERE ID_PAYMENT_ORDER = @ID_PAYMENT_ORDER
	
	-- �������� ��������� � �������
	DECLARE @HTAX INT
	EXEC SP_XML_PREPAREDOCUMENT @HTAX OUTPUT, @PAR OUTPUT
		SELECT T.ID_TAX, T.SUM_VAT, TT.TAX_RATE, T.IS_INCLUDE
		INTO #TAXES
		FROM OPENXML(@HTAX, '/TAX_PARAMS/TAX')
		WITH(ID_TAX BIGINT 'ID_TAX', SUM_VAT MONEY 'SUM_VAT', IS_INCLUDE BIT 'IS_INCLUDE')  T
		INNER JOIN TAX_TYPE TT ON T.ID_TAX = TT.ID_TAX_TYPE
		WHERE TT.TAX_RATE IN (0, 10, 18)
	EXEC SP_XML_REMOVEDOCUMENT @HTAX 
	
	-- �������� ��������� ������
	DECLARE @SUM_NO MONEY
	DECLARE @SUM_VAT_10 MONEY
	DECLARE @SUM_VAT_18 MONEY
	
	SELECT TOP 1 @SUM_VAT_10 = CASE WHEN T.IS_INCLUDE = 1 THEN T.SUM_VAT / 11
		ELSE 0.1 * T.SUM_VAT END
	FROM #TAXES T
	WHERE T.TAX_RATE = 10
	
	SELECT TOP 1 @SUM_VAT_18 = CASE WHEN T.IS_INCLUDE = 1 THEN T.SUM_VAT * 18 / 118
		ELSE 0.18 * T.SUM_VAT END
	FROM #TAXES T
	WHERE T.TAX_RATE = 18
	
	SELECT TOP 1 @SUM_NO = 0
	FROM #TAXES T
	WHERE T.TAX_RATE = 0
	
	DROP TABLE #TAXES
	
	SELECT SUM_VAT_10 = ISNULL(@SUM_VAT_10, 0), SUM_VAT_18 = ISNULL(@SUM_VAT_18, 0), SUM_NO = ISNULL(@SUM_NO, 0)	
END

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO

--exec REPEX_PAYMENT '<XML><ID_PAYMENT_ORDER>47</ID_PAYMENT_ORDER></XML>'


SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO 

IF OBJECT_ID('DBO.REMOVE_REPORT_BY_TYPE_NAME') IS NULL EXEC('CREATE PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME AS RETURN')
GO
ALTER PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME 
	@REPORT_TYPE_NAME VARCHAR(200) AS
	
DECLARE @id_meta_report BIGINT

	select 
		@id_meta_report = id_meta_report
	from meta_report
	where type_name = @REPORT_TYPE_NAME
	--select @id_meta_report
		
	DECLARE @SQL NVARCHAR(200)
	SET @SQL = N'delete from META_REPORT_2_REPORT_GROUPS
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('META_REPORT_2_REPORT_GROUPS') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_csv_export
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_csv_export') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_visible
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_visible') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_managed
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_managed') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	SET @SQL = N'delete from meta_report_settings_archive
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_archive') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	delete from meta_report
	where id_meta_report = @id_meta_report

RETURN 0
GO

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'PPEx.PPEx'