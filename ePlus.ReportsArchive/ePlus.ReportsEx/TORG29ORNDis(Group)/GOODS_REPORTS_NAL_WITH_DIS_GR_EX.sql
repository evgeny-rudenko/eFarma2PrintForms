SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX') IS NULL EXEC('CREATE PROCEDURE DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX AS RETURN')
GO
ALTER PROCEDURE DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX
    @XMLPARAM NTEXT
AS
DECLARE @STORE VARCHAR(256)
DECLARE @LEN INT
DECLARE @PRICE_BEGIN MONEY
DECLARE @HDOC INT, @SQL NVARCHAR(4000)
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @NO_DETAIL BIT, @SORT_DOC BIT, @SHOW_ADD BIT, @SHOW_SUB BIT, @REFRESH_DOC_MOV BIT
DECLARE @SHOW_RETURN BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @NO_DETAIL = NO_DETAIL,
        @SORT_DOC = SORT_DOC,
        @SHOW_ADD = SHOW_ADD,
        @SHOW_SUB = SHOW_SUB,
        @REFRESH_DOC_MOV = REFRESH_DOC_MOV,
		@SHOW_RETURN = SHOW_RETURN
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        NO_DETAIL BIT 'NO_DETAIL',
        SORT_DOC BIT 'SORT_DOC',
        SHOW_ADD BIT 'SHOW_ADD',
        SHOW_SUB BIT 'SHOW_SUB',
        REFRESH_DOC_MOV BIT 'REFRESH_DOC_MOV',
		SHOW_RETURN BIT 'SHOW_RETURN'
    )

	--������
    SELECT DISTINCT S.ID_STORE, S.ID_CONTRACTOR, S.NAME INTO #STORE
    FROM STORE S(NOLOCK)
    INNER JOIN (SELECT * FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.')) TAB ON TAB.ID_STORE = S.ID_STORE

EXEC SP_XML_REMOVEDOCUMENT @HDOC

IF @REFRESH_DOC_MOV = 1 BEGIN
    -- ����������� DOC_MOVEMENT �� ���� ����������
    EXEC UTL_REFRESH_DOC_MOVEMENT 'REPAIR', 0
END

-- ������������� ����
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

	SELECT 
		C.ID_CONTRACTOR, 
		C.NAME
	INTO #CONTRACTOR
	FROM CONTRACTOR C
	WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #STORE)

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP < @DATE_FR
    AND DM.ID_STORE IN (SELECT ID_STORE FROM #STORE)

DECLARE @ORDER VARCHAR(256)

IF @SHOW_ADD = 1 BEGIN
    -- ����� ������ �� ����������
    SELECT
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
		DATE_OP = DM.DATE_OP,
		INCOMING_NUMBER = CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
        ID_CONTRACTOR_FROM = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_FROM, 0) END,
        CONTRACTOR_FROM = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
        AMOUNT_ADD = SUM(DM.QUANTITY),
		SUM_DIS = SUM(CASE WHEN CODE_OP='DIS' THEN DM.SUM_ACC ELSE 0 END),
		SUM_SUP = SUM(DM.SUM_SUP),
		SUM_ACC = SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_ACC ELSE 0 END),--SUM(DM.SUM_ACC),
		DIFF = SUM(DM.SUM_ACC-DM.SUM_SUP) --SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_ACC ELSE 0 END - CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_SUP ELSE 0 END)
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
	LEFT JOIN INVOICE I ON DM.ID_DOCUMENT = I.ID_INVOICE_GLOBAL
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_FROM
    WHERE (@SHOW_RETURN=1 AND (CODE_OP='ADD' OR (CODE_OP='DIS' AND DM.ID_TABLE=19 AND SIGN_OP=-1)) --��������� ������� � �������
		  OR (@SHOW_RETURN=0 AND DM.CODE_OP='ADD' AND DM.ID_TABLE<>19))  		--������ �� ������� � �������
	AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND DM.ID_STORE IN (SELECT ID_STORE FROM #STORE)	 
    GROUP BY DM.ID_DOCUMENT, 
			 DM.ID_TABLE, 
			 TD.DESCRIPTION, 
			 DM.DATE_OP, 
			 AD.DOC_NUM, 
			 ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
			 CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_FROM, 0) END,
             CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END
    ORDER BY CASE WHEN NULLIF(@SORT_DOC,0) IS NULL THEN DM.DATE_OP ELSE DM.ID_TABLE END, DM.DATE_OP

END ELSE BEGIN
    SELECT
        ID_DOCUMENT = CAST(NULL AS UNIQUEIDENTIFIER),
        ID_TABLE = CAST(NULL AS BIGINT),
        TABLE_NAME = CAST(NULL AS NVARCHAR(128)),   
        DOC_DATE = CAST(NULL AS DATETIME),
        DOC_NUM = CAST(NULL AS NVARCHAR(128)),
        DOC_NAME = CAST(NULL AS NVARCHAR(128)),
		DATE_OP = CAST(NULL AS DATETIME),
		INCOMING_NUMBER = CAST(NULL AS NVARCHAR(128)),
        ID_CONTRACTOR_TO = CAST(NULL AS BIGINT),
        CONTRACTOR_TO = CAST(NULL AS NVARCHAR(128)),
        AMOUNT_ADD = CAST(NULL AS MONEY),
		SUM_DIS = CAST(NULL AS MONEY),
        SUM_SUP = CAST(NULL AS MONEY),
        SUM_ACC = CAST(NULL AS MONEY),
		DIFF = CAST(NULL AS MONEY)
END

IF @SHOW_SUB = 1 BEGIN
    -- ����� ������ �� ����������
	SELECT 
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = CASE WHEN DM.ID_TABLE = 26 THEN MAX(VC.DOC_NUM) ELSE AD.DOC_NUM END,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
		DATE_OP = DM.DATE_OP,
        ID_CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
        CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
        AMOUNT_ADD = SUM(DM.QUANTITY),
        SUM_DIS = SUM(CASE WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE=19 THEN DM.SUM_ACC * DM.SIGN_OP
						   WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE<>19 THEN DM.SUM_ACC
						   ELSE 0 END),
-- SUM(CASE WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE <> 19 THEN DM.SUM_ACC 
-- 						   WHEN	DM.CODE_OP='DIS' AND DM.ID_TABLE = 19 AND SIGN_OP=1 THEN DM.SUM_ACC 
-- 						   WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE = 19 AND SIGN_OP=-1 THEN DM.SUM_ACC * -1 ELSE 0 END),		--SUM(CASE WHEN CODE_OP='DIS' THEN SUM_ACC * SIGN_OP ELSE 0 END),
		SUM_SUP = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_SUP * DM.SIGN_OP * -1
						   END),--SUM(DM.SUM_SUP),													--SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_SUP ELSE 0 END),
        SUM_ACC = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_ACC * DM.SIGN_OP * -1
						   END),													--SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_ACC ELSE 0 END), 
		DIFF = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0 ELSE DM.SUM_ACC * DM.SIGN_OP * -1 END - 
				   CASE WHEN DM.CODE_OP='DIS' THEN 0 ELSE DM.SUM_SUP * DM.SIGN_OP * -1 END)											
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    LEFT JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_TO
	LEFT JOIN VAT_CORRECT VC ON VC.ID_VAT_CORRECT_GLOBAL = DM.ID_DOCUMENT
    WHERE ((@SHOW_RETURN=0 AND (CODE_OP IN ('SUB', 'DIS') OR DM.ID_TABLE=19))	--� ���������
			OR (@SHOW_RETURN=1 AND (CODE_OP IN ('SUB', 'DIS') OR (DM.ID_TABLE=19 AND CODE_OP<>'ADD'))))  --��� ����������� ��������
		AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO
		AND DM.ID_STORE IN (SELECT ID_STORE FROM #STORE)	 
    GROUP BY DM.ID_DOCUMENT, 
		     DM.ID_TABLE, 
             TD.DESCRIPTION, 
             DM.DATE_OP, 
             AD.DOC_NUM, 
			 ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END			
    ORDER BY CASE WHEN NULLIF(@SORT_DOC,0) IS NULL THEN DM.DATE_OP ELSE DM.ID_TABLE END, DM.DATE_OP

END ELSE BEGIN
    SELECT
        ID_DOCUMENT = CAST(NULL AS UNIQUEIDENTIFIER),
        ID_TABLE = CAST(NULL AS BIGINT),
        TABLE_NAME = CAST(NULL AS NVARCHAR(128)),   
        DOC_DATE = CAST(NULL AS DATETIME),
        DOC_NUM = CAST(NULL AS NVARCHAR(128)),
        DOC_NAME = CAST(NULL AS NVARCHAR(128)),
		DATE_OP = CAST(NULL AS DATETIME),
        ID_CONTRACTOR_TO = CAST(NULL AS BIGINT),
        CONTRACTOR_TO = CAST(NULL AS NVARCHAR(128)),
        AMOUNT_ADD = CAST(NULL AS MONEY),
		SUM_DIS = CAST(NULL AS MONEY),
        SUM_SUP = CAST(NULL AS MONEY),
        SUM_ACC = CAST(NULL AS MONEY),
		DIFF = CAST(NULL AS MONEY)
END

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP <= @DATE_TO
    AND DM.ID_STORE IN (SELECT ID_STORE FROM #STORE)


DECLARE @STORES VARCHAR(1024), @CONTRACTORS VARCHAR(1024)
EXEC DBO.USP_TABLE_NAMES '#STORE', @STORES OUT
EXEC DBO.USP_TABLE_NAMES '#CONTRACTOR', @CONTRACTORS OUT
SELECT CONTRACTORS = @CONTRACTORS, STORES = @STORES

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX_SERVICE') IS NULL EXEC('CREATE PROCEDURE DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX_SERVICE AS RETURN')

GO
ALTER PROCEDURE DBO.REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX_SERVICE
    @XMLPARAM NTEXT AS

DECLARE @STORE VARCHAR(256)
DECLARE @LEN INT
DECLARE @PRICE_BEGIN MONEY
DECLARE @HDOC INT, @SQL NVARCHAR(4000)
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @NO_DETAIL BIT, @SORT_DOC BIT, @SHOW_ADD BIT, @SHOW_SUB BIT, @REFRESH_DOC_MOV BIT
DECLARE @SHOW_RETURN BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FR = DATE_FR,
        @DATE_TO = DATE_TO,
        @NO_DETAIL = NO_DETAIL,
        @SORT_DOC = SORT_DOC,
        @SHOW_ADD = SHOW_ADD,
        @SHOW_SUB = SHOW_SUB,
        @REFRESH_DOC_MOV = REFRESH_DOC_MOV,
		@SHOW_RETURN = SHOW_RETURN
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR',
        DATE_TO DATETIME 'DATE_TO',
        NO_DETAIL BIT 'NO_DETAIL',
        SORT_DOC BIT 'SORT_DOC',
        SHOW_ADD BIT 'SHOW_ADD',
        SHOW_SUB BIT 'SHOW_SUB',
        REFRESH_DOC_MOV BIT 'REFRESH_DOC_MOV',
		SHOW_RETURN BIT 'SHOW_RETURN'
    )

    SELECT DISTINCT S.ID_STORE, S.ID_CONTRACTOR, S.NAME INTO #STORE
    FROM STORE S(NOLOCK)
    INNER JOIN (SELECT * FROM OPENXML(@HDOC, '//ID_STORE') WITH(ID_STORE BIGINT '.')) TAB ON TAB.ID_STORE = S.ID_STORE

EXEC SP_XML_REMOVEDOCUMENT @HDOC

IF @REFRESH_DOC_MOV = 1 BEGIN
    EXEC UTL_REFRESH_DOC_MOVEMENT 'REPAIR', 0
END

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

	SELECT 
		C.ID_CONTRACTOR, 
		C.NAME
	INTO #CONTRACTOR
	FROM CONTRACTOR C
	WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #STORE)
	

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP < @DATE_FR
    AND (DM.ID_STORE IS NULL OR DM.ID_STORE IN (SELECT ID_STORE FROM #STORE))

DECLARE @ORDER VARCHAR(256)

IF @SHOW_ADD = 1 BEGIN
    -- ����� ������ �� ����������
    SELECT
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
		DATE_OP = DM.DATE_OP,
		INCOMING_NUMBER = CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
        ID_CONTRACTOR_FROM = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_FROM, 0) END,
        CONTRACTOR_FROM = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
        AMOUNT_ADD = SUM(DM.QUANTITY),
		SUM_DIS = SUM(CASE WHEN CODE_OP='DIS' THEN DM.SUM_ACC ELSE 0 END),
		SUM_SUP = SUM(DM.SUM_SUP),
		SUM_ACC = SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_ACC ELSE 0 END),--SUM(DM.SUM_ACC),
		DIFF = SUM(DM.SUM_ACC-DM.SUM_SUP) --SUM(CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_ACC ELSE 0 END - CASE WHEN CODE_OP<>'DIS' THEN DM.SUM_SUP ELSE 0 END)
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
	LEFT JOIN INVOICE I ON DM.ID_DOCUMENT = I.ID_INVOICE_GLOBAL
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_FROM
    WHERE (@SHOW_RETURN=1 AND (CODE_OP='ADD' OR (CODE_OP='DIS' AND DM.ID_TABLE=19 AND SIGN_OP=-1)) --��������� ������� � �������
		  OR (@SHOW_RETURN=0 AND DM.CODE_OP='ADD' AND DM.ID_TABLE<>19))  		--������ �� ������� � �������
	AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO
	AND DM.ID_STORE IN (SELECT ID_STORE FROM #STORE)	 
    GROUP BY DM.ID_DOCUMENT, 
			 DM.ID_TABLE, 
			 TD.DESCRIPTION, 
			 DM.DATE_OP, 
			 AD.DOC_NUM, 
			 ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
			 CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_FROM, 0) END,
             CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END
    ORDER BY CASE WHEN NULLIF(@SORT_DOC,0) IS NULL THEN DM.DATE_OP ELSE DM.ID_TABLE END, DM.DATE_OP

END ELSE BEGIN
    SELECT
        ID_DOCUMENT = CAST(NULL AS UNIQUEIDENTIFIER),
        ID_TABLE = CAST(NULL AS BIGINT),
        TABLE_NAME = CAST(NULL AS NVARCHAR(128)),   
        DOC_DATE = CAST(NULL AS DATETIME),
        DOC_NUM = CAST(NULL AS NVARCHAR(128)),
        DOC_NAME = CAST(NULL AS NVARCHAR(128)),
		DATE_OP = CAST(NULL AS DATETIME),
		INCOMING_NUMBER = CAST(NULL AS NVARCHAR(128)),
        ID_CONTRACTOR_TO = CAST(NULL AS BIGINT),
        CONTRACTOR_TO = CAST(NULL AS NVARCHAR(128)),
        AMOUNT_ADD = CAST(NULL AS MONEY),
		SUM_DIS = CAST(NULL AS MONEY),
        SUM_SUP = CAST(NULL AS MONEY),
        SUM_ACC = CAST(NULL AS MONEY),
		DIFF = CAST(NULL AS MONEY)
END

IF @SHOW_SUB = 1 BEGIN
    -- ����� ������ �� ����������
	SELECT 
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        TABLE_NAME = TD.DESCRIPTION,
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = CASE WHEN DM.ID_TABLE = 26 THEN MAX(VC.DOC_NUM) ELSE AD.DOC_NUM END,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
		DATE_OP = DM.DATE_OP,
        ID_CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
        CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
        AMOUNT_ADD = SUM(DM.QUANTITY),
        SUM_DIS = SUM(CASE WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE=19 THEN DM.SUM_ACC * DM.SIGN_OP
						   WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE<>19 THEN DM.SUM_ACC
						   ELSE 0 END),
		SUM_SUP = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_SUP * DM.SIGN_OP * -1
						   END),
        SUM_ACC = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_ACC * DM.SIGN_OP * -1
						   END),
		DIFF = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0 ELSE DM.SUM_ACC * DM.SIGN_OP * -1 END - 
				   CASE WHEN DM.CODE_OP='DIS' THEN 0 ELSE DM.SUM_SUP * DM.SIGN_OP * -1 END)											
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    LEFT JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_TO
	LEFT JOIN VAT_CORRECT VC ON VC.ID_VAT_CORRECT_GLOBAL = DM.ID_DOCUMENT
    WHERE ((@SHOW_RETURN=0 AND (CODE_OP IN ('SUB', 'DIS') OR DM.ID_TABLE=19))
			OR (@SHOW_RETURN=1 AND (CODE_OP IN ('SUB', 'DIS') OR (DM.ID_TABLE=19 AND CODE_OP<>'ADD'))))
		AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO
		AND (DM.ID_STORE IS NULL OR DM.ID_STORE IN (SELECT ID_STORE FROM #STORE))
    GROUP BY DM.ID_DOCUMENT, 
		     DM.ID_TABLE, 
             TD.DESCRIPTION, 
             DM.DATE_OP, 
             AD.DOC_NUM, 
			 ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
			 CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END
    ORDER BY CASE WHEN NULLIF(@SORT_DOC,0) IS NULL THEN DM.DATE_OP ELSE DM.ID_TABLE END, DM.DATE_OP

END ELSE BEGIN
    SELECT
        ID_DOCUMENT = CAST(NULL AS UNIQUEIDENTIFIER),
        ID_TABLE = CAST(NULL AS BIGINT),
        TABLE_NAME = CAST(NULL AS NVARCHAR(128)),   
        DOC_DATE = CAST(NULL AS DATETIME),
        DOC_NUM = CAST(NULL AS NVARCHAR(128)),
        DOC_NAME = CAST(NULL AS NVARCHAR(128)),
		DATE_OP = CAST(NULL AS DATETIME),
        ID_CONTRACTOR_TO = CAST(NULL AS BIGINT),
        CONTRACTOR_TO = CAST(NULL AS NVARCHAR(128)),
        AMOUNT_ADD = CAST(NULL AS MONEY),
		SUM_DIS = CAST(NULL AS MONEY),
        SUM_SUP = CAST(NULL AS MONEY),
        SUM_ACC = CAST(NULL AS MONEY),
		DIFF = CAST(NULL AS MONEY)
END


    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP <= @DATE_TO
    AND (DM.ID_STORE IS NULL OR DM.ID_STORE IN (SELECT ID_STORE FROM #STORE))


DECLARE @STORES VARCHAR(1024), @CONTRACTORS VARCHAR(1024)
EXEC DBO.USP_TABLE_NAMES '#STORE', @STORES OUT
EXEC DBO.USP_TABLE_NAMES '#CONTRACTOR', @CONTRACTORS OUT
SELECT CONTRACTORS = @CONTRACTORS, STORES = @STORES

SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN 0
GO

/*
exec REP_GOODS_REPORTS_NAL_WITH_DIS_GR_EX_SERVICE N'
<XML>
	<DATE_FR>2009-05-19T00:00:00.000</DATE_FR>
	<DATE_TO>2009-05-19T00:00:00.000</DATE_TO>
	<NO_DETAIL>1</NO_DETAIL>
	<SHOW_ADD>1</SHOW_ADD>
	<SHOW_SUB>1</SHOW_SUB>
	<SORT_DOC>1</SORT_DOC>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<REFRESH_DOC_MOV>0</REFRESH_DOC_MOV>
	<ID_STORE>152</ID_STORE>
	<ID_STORE>157</ID_STORE>
	<SHOW_RETURN>1</SHOW_RETURN>
</XML>'*/