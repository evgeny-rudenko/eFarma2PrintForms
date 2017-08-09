IF OBJECT_ID('DBO.IMPORT_REMAINS_REP_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.IMPORT_REMAINS_REP_EX AS RETURN')
    GRANT EXEC ON [DBO].[IMPORT_REMAINS_REP_EX] TO [PUBLIC]
END
GO
ALTER PROCEDURE DBO.IMPORT_REMAINS_REP_EX
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME, @TYPE_REPORT BIGINT
DECLARE @SORT_FIELD VARCHAR(125)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
    SELECT TOP 1
--         @DATE_FROM = DATE_FROM,
--         @DATE_TO = DATE_TO,
		@TYPE_REPORT = TYPE_REPORT, --1 - ������, 0 - �������
		@SORT_FIELD = SORT_FIELD		
	FROM OPENXML(@HDOC , '/XML') WITH(
-- 		DATE_FROM DATETIME 'DATE_FROM',
--         DATE_TO DATETIME 'DATE_TO',
		TYPE_REPORT BIGINT 'TYPE_REPORT',
		SORT_FIELD VARCHAR(125) 'SORT_FIELD'
	)

	SELECT ID_DOC INTO #DOCS FROM OPENXML(@HDOC , '/XML/ID_DOC') WITH(
        ID_DOC INT '.')

	EXEC USP_RANGE_DAYS @DATE_FROM OUTPUT , @DATE_TO OUTPUT

--������ �����
IF (@TYPE_REPORT = 1)
BEGIN
	SELECT 		
		DOC_NUM = IR.MNEMOCODE,
		ID_IMPORT_REMAINS,
		NAME_GOODS = G.NAME,
		QUANTITY = IRI.QUANTITY,
		ID_SERIES = S.ID_SERIES,
		SERIES = ISNULL(S.SERIES_NUMBER,''),
		CERT_NUMBER = CONVERT(VARCHAR(4000), NULL),
		CERT_END_DATE = CONVERT(VARCHAR(4000), NULL),
		SUPPLIER = C.NAME,
		SUPPLIER_PRICE_VAT = IRI.SUPPLIER_PRICE_VAT, --���� ���������� � ���
		RETAIL_PRICE_VAT = IRI.RETAIL_PRICE_VAT, --���� ��������� � ���
		RETAIL_VAT, --������ ��� ���������
		RETAIL_VAT_SUM, --����� ��� ����������
		RETAIL_ADPRICE, --������� ��������� �������
		SUPPLIER_SUM_VAT, --����� �������
		RETAIL_SUM_VAT, --����� ���������
		IMPORTANT = ISNULL(IMPORTANT,0),      --�����
		GTD_NUMBER = ISNULL(GTD_NUMBER,''),  --����� ���
		STORE_PLACE = ISNULL(SP.NAME,''), --����� ��������
		REG_CERT = ISNULL(RC.[NAME],'')  -- ��� ����
	INTO #RES_FULL
	FROM IMPORT_REMAINS IR  
	INNER JOIN IMPORT_REMAINS_ITEM IRI ON IR.ID_IMPORT_REMAINS_GLOBAL = IRI.ID_IMPORT_REMAINS_GLOBAL
	INNER JOIN GOODS G ON IRI.ID_GOODS = G.ID_GOODS
	INNER JOIN LOT L ON L.ID_DOCUMENT_ITEM = IRI.ID_IMPORT_REMAINS_ITEM_GLOBAL
	LEFT JOIN SERIES S ON L.ID_SERIES = S.ID_SERIES
	INNER JOIN CONTRACTOR C ON IRI.ID_SUPPLIER = C.ID_CONTRACTOR
	LEFT JOIN REG_CERT RC ON RC.ID_REG_CERT_GLOBAL = L.ID_REG_CERT_GLOBAL--II.ID_REG_CERT_GLOBAL
	LEFT JOIN STORE_PLACE SP ON SP.ID_STORE_PLACE = IRI.ID_STORE_PLACE
	WHERE IR.ID_IMPORT_REMAINS IN (SELECT ID_DOC FROM #DOCS) AND IR.DOCUMENT_STATE = 'PROC'

    DECLARE C CURSOR FOR
    SELECT 
        ID_SERIES,    
        CERT_NUMBER,
        CERT_END_DATE
    FROM CERTIFICATE C
    WHERE EXISTS (SELECT NULL FROM #RES_FULL I WHERE I.ID_SERIES = C.ID_SERIES)    

    DECLARE @ID_SERIES BIGINT
    DECLARE @CERT_NUMBER VARCHAR(40)
    DECLARE @CERT_END_DATE DATETIME
    OPEN C 
    WHILE 1=1 BEGIN
        FETCH NEXT FROM C INTO @ID_SERIES, @CERT_NUMBER, @CERT_END_DATE
        IF (@@FETCH_STATUS<>0) BREAK
        UPDATE #RES_FULL SET
            CERT_NUMBER = ISNULL(CERT_NUMBER+ISNULL(', '+@CERT_NUMBER,''), @CERT_NUMBER),
            CERT_END_DATE = ISNULL(CERT_END_DATE+ISNULL(', '+dbo.FN_DATE_2_VARCHAR(@CERT_END_DATE),''), dbo.FN_DATE_2_VARCHAR(@CERT_END_DATE))
        WHERE #RES_FULL.ID_SERIES = @ID_SERIES
    END
    CLOSE C
    DEALLOCATE C

  	DECLARE @SQL NVARCHAR(4000)
 	SET @SQL = 	'SELECT
			NAME_GOODS,
			QUANTITY,
			ID_SERIES,
			SERIES,
			CERT_NUMBER = ISNULL(CERT_NUMBER,''''),
			CERT_END_DATE = ISNULL(CERT_END_DATE,''''),
			SUPPLIER,
			SUPPLIER_PRICE_VAT, 
			RETAIL_PRICE_VAT, 
			RETAIL_VAT, 
			RETAIL_VAT_SUM, 
			RETAIL_ADPRICE, 
			SUPPLIER_SUM_VAT,
			RETAIL_SUM_VAT, 
			IMPORTANT,      
			GTD_NUMBER,  
			STORE_PLACE, 
			REG_CERT
		FROM #RES_FULL ORDER BY ' + CONVERT(VARCHAR(125),@SORT_FIELD)
	exec (@SQL)
	
	SELECT COUNT_ITEM = COUNT(*) FROM #RES_FULL  --���������� ����� � �������
	
    DECLARE @DOCUMENTS VARCHAR(4000)

    SELECT 
        @DOCUMENTS = ISNULL(@DOCUMENTS+' ,'+IR.MNEMOCODE, IR.MNEMOCODE)
    FROM (SELECT DISTINCT ID_IMPORT_REMAINS FROM #RES_FULL) RF
    INNER JOIN IMPORT_REMAINS IR ON IR.ID_IMPORT_REMAINS = RF.ID_IMPORT_REMAINS	
	
	SELECT DOCUMENTS = @DOCUMENTS

END

--�������
IF (@TYPE_REPORT = 0)
BEGIN
	SELECT 
	 	NUM_DOC = MAX(IR.MNEMOCODE),
	 	DOC_DATE = MAX(IR.DOCUMENT_DATE),
		COUNT_ITEM =  COUNT(DISTINCT(CONVERT(VARCHAR(200),IRI.ID_IMPORT_REMAINS_ITEM_GLOBAL))),
		QTY_GOODS = SUM(IRI.QUANTITY),
		RETAIL_ADPRICE = SUM(IRI.RETAIL_ADPRICE)/COUNT(DISTINCT(CONVERT(VARCHAR(200),IRI.ID_IMPORT_REMAINS_ITEM_GLOBAL))),
		SUM_SUP = MAX(IR.SUM_SUPPLIER + IR.SVAT_SUPPLIER),
		SUM_RETAIL = MAX(IR.SUM_RETAIL)
	INTO #RES_SHORT
	FROM IMPORT_REMAINS IR
	INNER JOIN IMPORT_REMAINS_ITEM IRI ON IR.ID_IMPORT_REMAINS_GLOBAL = IRI.ID_IMPORT_REMAINS_GLOBAL
	WHERE IR.ID_IMPORT_REMAINS IN (SELECT ID_DOC FROM #DOCS) AND IR.DOCUMENT_STATE = 'PROC'
	GROUP BY IRI.ID_IMPORT_REMAINS_GLOBAL
	ORDER BY MAX(IR.DOCUMENT_DATE)

	SELECT * FROM #RES_SHORT

	SELECT COUNT_ITEM = COUNT(*) FROM #RES_SHORT
END
----------------------------------------

RETURN 0
GO
--exec IMPORT_REMAINS_REP_EX @xmlParam = N'<XML><TYPE_REPORT>0</TYPE_REPORT><SORT_FIELD>NAME_GOODS</SORT_FIELD><ID_DOC>1</ID_DOC><ID_DOC>3</ID_DOC><ID_DOC>2</ID_DOC></XML>'
--exec IMPORT_REMAINS_REP_EX @xmlParam = N'<XML><TYPE_REPORT>1</TYPE_REPORT><SORT_FIELD>NAME_GOODS</SORT_FIELD><ID_DOC>3</ID_DOC><ID_DOC>1</ID_DOC></XML>'
--exec IMPORT_REMAINS_REP_EX @xmlParam = N'<XML><TYPE_REPORT>1</TYPE_REPORT><SORT_FIELD>SUPPLIER</SORT_FIELD><ID_DOC>1</ID_DOC></XML>'
--exec IMPORT_REMAINS_REP_EX @xmlParam = N'<XML><TYPE_REPORT>1</TYPE_REPORT><SORT_FIELD>NAME_GOODS</SORT_FIELD><ID_DOC>1</ID_DOC><ID_DOC>3</ID_DOC><ID_DOC>2</ID_DOC></XML>'
