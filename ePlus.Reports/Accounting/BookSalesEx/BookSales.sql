SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_BOOK_SALES') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_BOOK_SALES AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_BOOK_SALES
	@XMLPARAM NTEXT AS

    DECLARE @HDOC INT
    
    DECLARE 
        @DATE_FROM DATETIME, 
        @DATE_TO DATETIME, 
        @SEL_STORE BIT, 
        @SEL_CONTRACTOR BIT
	DECLARE @CONTRACTORS TABLE(CONTRACTOR_NAME VARCHAR(128))

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM OUT
    SELECT 
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO

    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO'
    )

    SELECT
        ID_CONTRACTOR
    INTO #CONTRACTORS
    FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(
        ID_CONTRACTOR BIGINT '.'
    )

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
    EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT

	SELECT
		S.ID_STORE,
		S.ID_CONTRACTOR
	INTO #STORES
	FROM #CONTRACTORS C
	LEFT JOIN STORE S ON C.ID_CONTRACTOR = S.ID_CONTRACTOR

IF (SELECT COUNT(*) FROM #STORES) != 0
BEGIN
	SELECT 
	    MNEMOCODE = I.MNEMOCODE,
	    DATE_AND_NUM = ISNULL(CONVERT(VARCHAR, I.DOC_DATE, 104),'')+ISNULL('/'+I.DOC_NUM,''),
	    DOC_NUMBER = I.DOC_NUM,
	    DOC_DATE = I.DOC_DATE,
	    CONTRACTOR_NAME = C.NAME,
	    INN = C.INN,
	    KPP = C.KPP,
	    DATE_PAYMENT = I.DATE_PAYMENT,
	    SUM_SAL = SUM(II.PRICE_SAL*II.QUANTITY),
	    VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN II.PSUM_SAL ELSE 0 END),--L.PVAT_SAL * II.QUANTITY
	    SUM_VAT_RATE_18 = SUM(CASE WHEN L.VAT_SAL = 18 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END),--(L.PRICE_SAL - L.PVAT_SAL) * II.QUANTITY
	    VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN II.PSUM_SAL ELSE 0 END),
	    SUM_VAT_RATE_10 = SUM(CASE WHEN L.VAT_SAL = 10 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END),
	    SUM_VAT_RATE_0 = SUM(CASE WHEN L.VAT_SAL = 0 THEN II.SUM_SAL - II.PSUM_SAL ELSE 0 END)
	FROM INVOICE_OUT_ITEM II(NOLOCK)
	    INNER JOIN INVOICE_OUT I ON I.ID_INVOICE_OUT_GLOBAL = II.ID_INVOICE_OUT_GLOBAL 
	    AND I.STATE='PROC' 
	    AND I.ID_STORE IN (SELECT ID_STORE FROM #STORES) 
	    AND (I.DATE BETWEEN @DATE_FROM AND @DATE_TO)
	    INNER JOIN CONTRACTOR C(NOLOCK) ON C.ID_CONTRACTOR = I.ID_CONTRACTOR_TO 
	    LEFT JOIN LOT L(NOLOCK) ON L.ID_LOT_GLOBAL = II.ID_LOT_GLOBAL
	GROUP BY I.MNEMOCODE,I.DOC_NUM,I.DOC_DATE,C.NAME,C.INN,C.KPP,I.DATE_PAYMENT
	ORDER BY I.DOC_DATE, DOC_NUMBER

	INSERT INTO @CONTRACTORS
	SELECT 
		CONTRACTOR_NAME = CASE WHEN ISNULL(FULL_NAME, '') = '' THEN NAME ELSE FULL_NAME END
	FROM CONTRACTOR 
	WHERE ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS)

    DECLARE @CONTRACTOR_STRING VARCHAR(4000)

    SELECT 
        @CONTRACTOR_STRING = ISNULL(@CONTRACTOR_STRING+' ,'+C.CONTRACTOR_NAME, C.CONTRACTOR_NAME)
    FROM @CONTRACTORS C
	SELECT CONTRACTOR_NAME = @CONTRACTOR_STRING

END

	
SELECT
	DIRECTOR_FIO,
	BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN
GO

/*
exec REPEX_BOOK_SALES N'
<XML>
	<DATE_FROM>2009-01-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2009-08-17T00:00:00.000</DATE_TO>
	<ID_CONTRACTOR>5277</ID_CONTRACTOR>
</XML>'*/

