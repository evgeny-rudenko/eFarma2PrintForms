SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO
IF OBJECT_ID('DBO.REPEX_CORRECT_PRICE_AU') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REPEX_CORRECT_PRICE_AU AS RETURN')
    GRANT EXECUTE ON DBO.REPEX_CORRECT_PRICE_AU TO PUBLIC
END
GO
ALTER PROCEDURE DBO.REPEX_CORRECT_PRICE_AU
    @XMLPARAM NTEXT
AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @ALL_CONTRACTOR BIT, @ALL_PRODUCER BIT, @ALL_DRUGSTORE BIT, @ALL_GOODS BIT


EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1 @DATE_FR = DATE_FR, @DATE_TO = DATE_TO
    FROM OPENXML(@HDOC, '/XML') WITH(DATE_FR DATETIME 'DATE_FR', DATE_TO DATETIME 'DATE_TO')
    /* ������� ��������������� ����������� */
    SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
    WITH(ID_CONTRACTOR BIGINT '.') WHERE ID_CONTRACTOR <> 0
    IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1 ELSE SET @ALL_CONTRACTOR = 0
    /* ������� ��������������� �������������� */
    SELECT * INTO #PRODUCER    FROM OPENXML(@HDOC, '//ID_PRODUCER') 
    WITH(ID_PRODUCER BIGINT '.') WHERE ID_PRODUCER <> 0
    IF @@ROWCOUNT = 0 SET @ALL_PRODUCER = 1 ELSE SET @ALL_PRODUCER = 0 
    /* ������� ��������������� ����� */
    SELECT * INTO #DRUGSTORE FROM OPENXML(@HDOC, '//ID_DRUGSTORE') 
    WITH(ID_DRUGSTORE BIGINT '.') WHERE ID_DRUGSTORE <> 0
    IF @@ROWCOUNT = 0 SET @ALL_DRUGSTORE = 1 ELSE SET @ALL_DRUGSTORE = 0
    /* ������� ��������������� ������ */
    SELECT * INTO #GOODS FROM OPENXML(@HDOC, '//ID_GOODS') 
    WITH(ID_GOODS BIGINT '.') WHERE ID_GOODS <> 0
    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0 
EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT

SELECT G.ID_GOODS,
    COD_AP = G.CODE,
    CODE_AU = C.ID_CONTRACTOR,
    NAME_AU = C.[NAME],
    ID_GOODS = G.ID_GOODS,
    NAME_GOODS = G.[NAME],
    NAME_PR = PR.[NAME],
    NAME_POST = POST.FULL_NAME,
    PRICE_AP = A.PRICE_SAL,
    [DATE] = (SELECT TOP 1 H.PRICE_DATE FROM ASSORTMENT_PLAN_IMPORT_HISTORY H 
        WHERE H.ID_CONTRACTOR = A.ID_CONTRACTOR AND H.ID_GOODS = A.ID_GOODS
        ORDER BY H.PRICE_DATE DESC),
    PRICE_PR = ROUND(II.SUPPLIER_PRICE_VAT * SR.DENOMINATOR / SR.NUMERATOR, 2),
    PRICE_ROZ = ROUND(II.RETAIL_PRICE_VAT * SR.DENOMINATOR / SR.NUMERATOR, 2),
    DATE_ROZ = I.DOCUMENT_DATE,
    PROCENT = ROUND((II.RETAIL_PRICE_VAT * SR.DENOMINATOR / SR.NUMERATOR) / NULLIF(A.PRICE_SAL, 0) * 100, 2) 
FROM INVOICE I
INNER JOIN INVOICE_ITEM II ON II.ID_INVOICE = I.ID_INVOICE
INNER JOIN GOODS G ON G.ID_GOODS = II.ID_GOODS
INNER JOIN PRODUCER PR ON PR.ID_PRODUCER = G.ID_PRODUCER
INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
INNER JOIN CONTRACTOR POST ON POST.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = II.ID_SCALING_RATIO
LEFT JOIN ASSORTMENT_PLAN A ON A.ID_GOODS = G.ID_GOODS AND A.ID_CONTRACTOR = S.ID_CONTRACTOR
WHERE I.DOCUMENT_STATE = 'PROC'
    AND I.DOCUMENT_DATE BETWEEN @DATE_FR AND @DATE_TO
    AND (@ALL_CONTRACTOR = 1 OR I.ID_CONTRACTOR_SUPPLIER IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
    AND (@ALL_PRODUCER = 1 OR G.ID_PRODUCER IN (SELECT ID_PRODUCER FROM #PRODUCER))
    AND (@ALL_DRUGSTORE = 1 OR S.ID_CONTRACTOR IN (SELECT ID_DRUGSTORE FROM #DRUGSTORE))
    AND (@ALL_GOODS = 1 OR II.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS))

RETURN
GO
--exec REPEX_CORRECT_PRICE_AU @xmlParam=N'<XML><DATE_FR>2009-01-01T00:00:00.000</DATE_FR><DATE_TO>2009-05-13T18:05:04.265</DATE_TO></XML>'
