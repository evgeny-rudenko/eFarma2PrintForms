-- USE EPLUS_DEV10
SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
-- DROP PROCEDURE REP_GOODS_REPORTS_TO_EX
---------------------------------------------------------------------------
IF (OBJECT_ID('DBO.REP_GOODS_REPORTS_TO_EKB_EX') IS NULL ) EXEC ('CREATE PROCEDURE DBO.REP_GOODS_REPORTS_TO_EKB_EX AS SELECT NULL')
GO
ALTER PROCEDURE DBO.REP_GOODS_REPORTS_TO_EKB_EX
         @XMLPARAM NTEXT
AS
SET nocount on
DECLARE @NO_DETAIL BIT --, @ALL_STORE BIT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
DECLARE @SORT_DOC BIT, @SHOW_ADD BIT, @SHOW_SUB BIT, @REFRESH_DOC_MOV BIT

DECLARE @HDOC INT, @SQL NVARCHAR(4000), @SQL_FIELDS NVARCHAR(4000)
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
    SELECT TOP 1 
        @DATE_FR = DATE_FR, @DATE_TO = DATE_TO,
        @NO_DETAIL = NO_DETAIL, @SORT_DOC = SORT_DOC,
        @SHOW_ADD = SHOW_ADD, @SHOW_SUB = SHOW_SUB,
        @REFRESH_DOC_MOV = REFRESH_DOC_MOV
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FR DATETIME 'DATE_FR', 
        DATE_TO DATETIME 'DATE_TO', 
        NO_DETAIL BIT 'NO_DETAIL',
        SORT_DOC BIT 'SORT_DOC', 
        SHOW_ADD BIT 'SHOW_ADD', 
        SHOW_SUB BIT 'SHOW_SUB',
        REFRESH_DOC_MOV BIT 'REFRESH_DOC_MOV')

    /* ������� ��������������� ������������ */
    SELECT DISTINCT C.ID_CONTRACTOR, C.NAME INTO #CONTRACTOR
    FROM 
        (SELECT * FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
        WITH(ID_CONTRACTOR BIGINT '.')) TAB
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = TAB.ID_CONTRACTOR
    
    /* ������� ��������������� ������� */
    SELECT DISTINCT S.ID_STORE, S.ID_CONTRACTOR, S.NAME INTO #STORE
    FROM
    	(SELECT * FROM OPENXML(@HDOC, '//ID_STORE') 
        WITH(ID_STORE BIGINT '.')) TAB
    INNER JOIN STORE S ON S.ID_STORE = TAB.ID_STORE
EXEC SP_XML_REMOVEDOCUMENT @HDOC

IF @REFRESH_DOC_MOV = 1 BEGIN
    -- ����������� DOC_MOVEMENT �� ���� ����������
    EXEC UTL_REFRESH_DOC_MOVEMENT 'REPAIR', 0
END

-- ������������� ����
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

-- ���� �� ������ �����, �� ����� ���������� �� ���� �������
IF (SELECT COUNT(*) FROM #STORE) = 0 BEGIN
    INSERT INTO #STORE (ID_STORE, ID_CONTRACTOR, NAME)

	SELECT S.ID_STORE, S.ID_CONTRACTOR, S.NAME
	FROM STORE S
	INNER JOIN #CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
END
DELETE FROM #STORE WHERE ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)

-- ������� �� ������
SET @SQL_FIELDS = '
        SUM_SUM_SUP = SUM(DM.SUM_SUP * #SIGN_OP),
        SUM_SUM_ACC = SUM(SUM_ACC * #SIGN_OP),
        KOF = CASE WHEN MAX(CODE_OP) = ''DIS'' THEN 0 ELSE 1 END,
    
        SUM_SVAT_SUP = SUM(DM.SVAT_SUP * #SIGN_OP),
        SUM_SVAT_ACC = SUM(SVAT_ACC * #SIGN_OP),
    
        SUM_SUP0 = SUM(CASE WHEN DM.VAT_RATE = 0 THEN (DM.SUM_SUP * #SIGN_OP) ELSE 0 END),
        SUM_SUP10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN (DM.SUM_SUP * #SIGN_OP) ELSE 0 END),
        SUM_SUP18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN (DM.SUM_SUP * #SIGN_OP) ELSE 0 END),
        SUM_ACC0 = SUM(CASE WHEN DM.VAT_RATE = 0 THEN SUM_ACC * #SIGN_OP ELSE 0 END),
        SUM_ACC10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN SUM_ACC * #SIGN_OP ELSE 0 END),
        SUM_ACC18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN SUM_ACC * #SIGN_OP ELSE 0 END),
    
        SVAT_SUP10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN (DM.SVAT_SUP * #SIGN_OP) ELSE 0 END),
        SVAT_SUP18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN (DM.SVAT_SUP * #SIGN_OP) ELSE 0 END),
        SVAT_ACC10 = SUM(CASE WHEN DM.VAT_RATE = 10 THEN SVAT_ACC * #SIGN_OP ELSE 0 END),
        SVAT_ACC18 = SUM(CASE WHEN DM.VAT_RATE = 18 THEN SVAT_ACC * #SIGN_OP ELSE 0 END)
    '
SET @SQL = REPLACE(@SQL_FIELDS, '#SIGN_OP', 'SIGN_OP')
SET @SQL = 'SELECT ' + @SQL + '
    FROM DOC_MOVEMENT DM
        INNER JOIN #STORE S ON S.ID_STORE = DM.ID_STORE
    WHERE DM.DATE_OP < @DATE_FR AND DM.CODE_OP <> ''DIS'''
EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME, @DATE_TO DATETIME',
   @DATE_FR = @DATE_FR, @DATE_TO = @DATE_TO


DECLARE @ORDER VARCHAR(128)
IF(@SHOW_ADD = 1) BEGIN
	-- ����� ������ �� ����������
    SET @SQL = REPLACE(@SQL_FIELDS, '#SIGN_OP', '1')
	SET @SQL = 
        'SELECT DM.ID_DOCUMENT, 
                DOC_NAME = (SELECT TOP 1 [DESCRIPTION_EXT] FROM FN_DOC_MOVEMENT_DESCRIPTION() DMD
                            WHERE DM.ID_DOCUMENT = DMD.ID_DOCUMENT AND DM.ID_STORE = DMD.ID_STORE),
                DM.DATE_OP, 
                ALD.DOC_NUM,
        ' + @SQL + '
        FROM DOC_MOVEMENT DM
            INNER JOIN #STORE S ON S.ID_STORE = DM.ID_STORE
            LEFT JOIN ALL_DOCUMENT ALD ON DM.ID_DOCUMENT = ALD.ID_DOCUMENT_GLOBAL
        WHERE DM.CODE_OP = ''ADD''
            AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
        GROUP BY DM.ID_DOCUMENT, DM.DATE_OP, ALD.DOC_NUM, DM.ID_STORE
        ORDER BY #ORDER'

    SET @ORDER = 'DM.DATE_OP ASC, DOC_NAME ASC, ALD.DOC_NUM ASC'
    IF(@SORT_DOC = 1) SET @ORDER = 'DOC_NAME ASC, ALD.DOC_NUM ASC, DM.DATE_OP ASC'

    SET @SQL = REPLACE(@SQL, '#ORDER', @ORDER)
	EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME, @DATE_TO DATETIME',
    @DATE_FR = @DATE_FR, @DATE_TO = @DATE_TO
END
ELSE BEGIN
    SELECT NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
END


IF(@SHOW_SUB = 1) BEGIN
	-- ����� ������ �� ����������
    SET @SQL = REPLACE(@SQL_FIELDS, '#SIGN_OP', '1')
	SET @SQL = '
        SELECT  DM.ID_DOCUMENT, 
                DOC_NAME = (SELECT TOP 1 [DESCRIPTION_EXT] FROM FN_DOC_MOVEMENT_DESCRIPTION() DMD
                            WHERE DM.ID_DOCUMENT = DMD.ID_DOCUMENT AND DM.ID_STORE = DMD.ID_STORE),  
                DM.DATE_OP, 
                ALD.DOC_NUM,
				DM.ID_CONTRACTOR_TO,
				CONTRACTOR_TO = ISNULL(C.NAME,'''')+ISNULL(''(''+S.NAME+'')'',''''),
				DM.ID_TABLE,
         ' + @SQL + '
        FROM DOC_MOVEMENT DM
            INNER JOIN #STORE S ON S.ID_STORE = DM.ID_STORE
            LEFT JOIN ALL_DOCUMENT ALD ON DM.ID_DOCUMENT = ALD.ID_DOCUMENT_GLOBAL
			LEFT JOIN STORE ST ON ST.ID_STORE = DM.ID_STORE_TO
			LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
        WHERE (DM.CODE_OP = ''SUB'')
            AND DM.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
        GROUP BY DM.ID_DOCUMENT, DM.ID_TABLE, DM.DATE_OP, ALD.DOC_NUM, DM.ID_STORE, DM.ID_CONTRACTOR_TO, C.NAME, S.NAME
        ORDER BY #ORDER'

		SET @ORDER = 'DM.ID_TABLE, DM.DATE_OP ASC'
		IF(@SORT_DOC = 0) SET @ORDER = 'DM.DATE_OP ASC, DM.ID_TABLE'
--     SET @ORDER = 'DOC_NAME ASC, ALD.DOC_NUM ASC, DM.DATE_OP ASC'
--     IF(@SORT_DOC = 0) SET @ORDER = 'DM.DATE_OP ASC, DOC_NAME ASC, ALD.DOC_NUM ASC'

    SET @SQL = REPLACE(@SQL, '#ORDER', @ORDER)
	EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME, @DATE_TO DATETIME',
        @DATE_FR = @DATE_FR, @DATE_TO = @DATE_TO
END
ELSE BEGIN
    SELECT NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
END

DECLARE @STORES VARCHAR(1024)
DECLARE @CONTRACTORS VARCHAR(1024)

EXEC DBO.USP_TABLE_NAMES '#STORE', @STORES OUT
EXEC DBO.USP_TABLE_NAMES '#CONTRACTOR', @CONTRACTORS OUT
SELECT @CONTRACTORS, @STORES

-- ������� �� �����
SET @SQL = REPLACE(@SQL_FIELDS, '#SIGN_OP', 'SIGN_OP')
SET @SQL = 'SELECT ' + @SQL + '
    FROM DOC_MOVEMENT DM
        INNER JOIN #STORE S ON S.ID_STORE = DM.ID_STORE
        LEFT JOIN ALL_DOCUMENT ALD ON DM.ID_DOCUMENT = ALD.ID_DOCUMENT_GLOBAL
    WHERE DM.DATE_OP <= @DATE_TO AND DM.CODE_OP <> ''DIS'''
EXEC SP_EXECUTESQL @SQL, N'@DATE_FR DATETIME, @DATE_TO DATETIME, @SHOW_ADD BIT, @SHOW_SUB BIT',
   @DATE_FR = @DATE_FR, @DATE_TO = @DATE_TO, @SHOW_ADD = @SHOW_ADD, @SHOW_SUB = @SHOW_SUB 


RETURN 0
GO
---------------------------------------------------------------------------
--exec REP_GOODS_REPORTS_TO_EX @xmlParam = N'<XML><DATE_FR>2008-01-01T13:06:43.187</DATE_FR><DATE_TO>2008-10-29T13:06:43.187</DATE_TO><ID_CONTRACTOR>5271</ID_CONTRACTOR><SORT_DOC>1</SORT_DOC><SHOW_ADD>1</SHOW_ADD><SHOW_SUB>1</SHOW_SUB><REFRESH_DOC_MOV>0</REFRESH_DOC_MOV></XML>'
--SELECT * FROM DOC_MOVEMENT