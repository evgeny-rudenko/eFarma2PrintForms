SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('REPEX_AP25') IS NULL) EXEC ('CREATE PROCEDURE REPEX_AP25 AS RETURN')
GO
ALTER PROCEDURE REPEX_AP25(
    @XMLPARAM NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @DATE_FROM DATETIME, @DATE_TO DATETIME, @SORT_BY_DOCTYPE BIT, @REFRESH_DOC_MOV BIT
    DECLARE @STORE TABLE(ID_CONTRACTOR BIGINT NOT NULL, ID_STORE BIGINT NOT NULL)
	DECLARE @NOAU BIT, @CO BIT

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @SORT_BY_DOCTYPE = SORT_BY_DOCTYPE,
        @REFRESH_DOC_MOV = REFRESH_DOC_MOV,
		@NOAU = NOAU,
		@CO = CO
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
        SORT_BY_DOCTYPE BIT 'SORT_BY_DOCTYPE',
        REFRESH_DOC_MOV BIT 'REFRESH_DOC_MOV',
		NOAU BIT 'NOAU', CO BIT 'CO'
    )        

    INSERT INTO @STORE
    SELECT 
        S.ID_CONTRACTOR,
        A.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)

    INSERT INTO @STORE
    SELECT
        A.ID_CONTRACTOR,
        S.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(
        ID_CONTRACTOR BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_CONTRACTOR = A.ID_CONTRACTOR
    AND NOT EXISTS (SELECT NULL FROM @STORE S1 WHERE S1.ID_CONTRACTOR=A.ID_CONTRACTOR)
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
    EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT

    IF (@REFRESH_DOC_MOV=1) BEGIN
        EXEC UTL_REFRESH_DOC_MOVEMENT 'REPAIR', 0
    END

--select * from @store

		-- �� ������

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP < @DATE_FROM
    AND DM.ID_STORE IN (SELECT ID_STORE FROM @STORE)

    -- ������
    select
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        DOC_TYPE = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')',''),
		INCOMING_NUMBER = CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
        ID_CONTRACTOR_FROM = ISNULL(DM.ID_CONTRACTOR_FROM, 0),
        CONTRACTOR_FROM = ISNULL(C.NAME,''),
        STORE_NAME = CASE WHEN @SORT_BY_DOCTYPE = 0 THEN NULL ELSE S.NAME END,
		SUM_SUP = SUM(DM.SUM_SUP),
        SUM_ACC = SUM(DM.SUM_ACC)
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
	LEFT JOIN INVOICE I ON DM.ID_DOCUMENT = I.ID_INVOICE_GLOBAL
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_FROM
    WHERE DM.CODE_OP='ADD' AND DM.ID_TABLE<>19 
    AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
    AND DM.ID_STORE IN (SELECT ID_STORE FROM @STORE)
	AND (@NOAU = 1 OR (DM.ID_TABLE not in (8, 37, 39)) OR dm.id_store_from not in (select id_store from @store where id_contractor = dm.id_contractor_to)) --(dm.id_contractor_from <> dm.id_contractor_to or (dm.id_contractor_from = dm.id_contractor_to and dm.id_store_to not in (select id_store from @store))))
    GROUP BY 
			DM.ID_DOCUMENT, 
			DM.ID_TABLE, 
			TD.DESCRIPTION, 
			DM.DATE_OP, 
			AD.DOC_NUM, 
			ISNULL(TD.DESCRIPTION+': ','')+ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')',''),
			CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
			ISNULL(DM.ID_CONTRACTOR_FROM, 0), 
			ISNULL(C.NAME,''),
			S.NAME
    ORDER BY CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN DM.DATE_OP ELSE NULL END, 
		CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN NULL ELSE TD.DESCRIPTION END, 
		ISNULL(C.NAME,''), DM.DATE_OP
    
    -- ������
    select
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        DOC_TYPE = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
        ID_CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
        CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'') END,
		STORE_NAME = S.NAME,
        SUM_DIS = SUM(CASE WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE<>19 THEN DM.SUM_ACC
 						   WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE=19 THEN DM.SUM_ACC * SIGN_OP
 						   ELSE 0 END),
		SUM_SUP = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_SUP * DM.SIGN_OP * -1
						   END),
        SUM_ACC = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_ACC * DM.SIGN_OP * -1
						   END)
	INTO #T
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_TO
    WHERE (CODE_OP IN ('SUB','DIS') OR DM.ID_TABLE=19)
    AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
    AND DM.ID_STORE IN (SELECT ID_STORE FROM @STORE)
	AND (@NOAU = 1 OR (DM.ID_TABLE not in (8, 37, 39)) OR dm.id_store_to not in (select id_store from @store where id_contractor = dm.id_contractor_from)) --(dm.id_contractor_from <> dm.id_contractor_to or (dm.id_contractor_from = dm.id_contractor_to and dm.id_store_from not in (SELECT ID_STORE FROM @STORE))))
    GROUP BY 
			DM.ID_DOCUMENT, 
			DM.ID_TABLE, 
			TD.DESCRIPTION, 
			DM.DATE_OP, 
			AD.DOC_NUM, 
			ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END,
			CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END, 
			CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'') END,
			S.NAME
    ORDER BY CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN DM.DATE_OP ELSE NULL END, 
		CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN NULL ELSE TD.DESCRIPTION END, 
		CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'') END, DM.DATE_OP

	SELECT * FROM #T
	--������� ����� �� �������
	SELECT SUM_DIS = SUM(SUM_DIS) FROM #T

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP <= @DATE_TO
    AND DM.ID_STORE IN (SELECT ID_STORE FROM @STORE)

-- ����������� � ������ ����� ,
    DECLARE @CONTRACTOR_STRING VARCHAR(4000)
    DECLARE @STORE_STRING VARCHAR(4000)

    SELECT 
        @STORE_STRING = ISNULL(@STORE_STRING+' ,'+S.NAME, S.NAME)
    FROM (SELECT DISTINCT ID_STORE FROM @STORE) T
    INNER JOIN STORE S ON S.ID_STORE = T.ID_STORE

    SELECT 
        @CONTRACTOR_STRING = ISNULL(@CONTRACTOR_STRING+' ,'+C.NAME, C.NAME)
    FROM (SELECT DISTINCT ID_CONTRACTOR FROM @STORE) T
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR

    SELECT CONTRACTOR = @CONTRACTOR_STRING, STORE = @STORE_STRING
    
    SELECT S_SUM_ACC = NULL
    
RETURN
GO

/*
exec REPEX_AP25 N'
<XML>
	<DATE_FROM>2009-12-02T00:00:00.000</DATE_FROM>
	<DATE_TO>2009-12-02T23:00:00.000</DATE_TO>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>	
	<SORT_BY_DOCTYPE>1</SORT_BY_DOCTYPE>
	<REFRESH_DOC_MOV>0</REFRESH_DOC_MOV>	
</XML>'*/

SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_AP25_SERVICE') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_AP25_SERVICE AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_AP25_SERVICE(
    @XMLPARAM NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @DATE_FROM DATETIME, @DATE_TO DATETIME, @SORT_BY_DOCTYPE BIT, @REFRESH_DOC_MOV BIT
    DECLARE @STORE TABLE(ID_CONTRACTOR BIGINT NOT NULL, ID_STORE BIGINT NOT NULL)
	DECLARE @NOAU BIT, @CO BIT

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
    SELECT
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @SORT_BY_DOCTYPE = SORT_BY_DOCTYPE,
        @REFRESH_DOC_MOV = REFRESH_DOC_MOV,
		@NOAU = NOAU, @CO = CO
    FROM OPENXML(@HDOC, '/XML') WITH(
        DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
        SORT_BY_DOCTYPE BIT 'SORT_BY_DOCTYPE',
        REFRESH_DOC_MOV BIT 'REFRESH_DOC_MOV',
		NOAU BIT 'NOAU', CO BIT 'CO'
    )
    
    /* ������� ��������������� ������������ */
    SELECT DISTINCT C.ID_CONTRACTOR, C.NAME INTO #CONTRACTOR
    FROM 
        (SELECT * FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
        WITH(ID_CONTRACTOR BIGINT '.')) TAB
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = TAB.ID_CONTRACTOR
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)
    
    /* ������� ��������������� ������� */
    INSERT INTO @STORE
    SELECT distinct
        S.ID_CONTRACTOR,
        A.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE
    WHERE ((@CO <> 1 OR @CO IS NULL) 
		AND S.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))
		OR (@CO = 1)

    /*INSERT INTO @STORE
    SELECT 
        S.ID_CONTRACTOR,
        A.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE

    INSERT INTO @STORE
    SELECT
        A.ID_CONTRACTOR,
        S.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(
        ID_CONTRACTOR BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_CONTRACTOR = A.ID_CONTRACTOR
    AND NOT EXISTS (SELECT NULL FROM @STORE S1 WHERE S1.ID_CONTRACTOR=A.ID_CONTRACTOR)*/

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
    EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT

    IF (@REFRESH_DOC_MOV=1) BEGIN
        EXEC UTL_REFRESH_DOC_MOVEMENT 'REPAIR', 0
    END
    
    -- ���� �� ������ �����, �� ����� ���������� �� ���� �������
	IF NOT EXISTS(SELECT TOP 1 1 FROM @STORE) BEGIN
		INSERT INTO @STORE (ID_CONTRACTOR, ID_STORE)
		SELECT S.ID_CONTRACTOR, S.ID_STORE
		FROM STORE S(NOLOCK) INNER JOIN #CONTRACTOR C ON C.ID_CONTRACTOR = S.ID_CONTRACTOR
	END
	DELETE FROM @STORE WHERE ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR)

	-- �� ������

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP < @DATE_FROM
    AND (DM.ID_STORE IN (SELECT ID_STORE FROM @STORE))

    -- ������
    select
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = DM.ID_TABLE,
        DOC_TYPE = TD.DESCRIPTION,    
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = ISNULL(TD.DESCRIPTION+': ','')+ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')',''),
		INCOMING_NUMBER = CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
        ID_CONTRACTOR_FROM = ISNULL(DM.ID_CONTRACTOR_FROM, 0),
        CONTRACTOR_FROM = ISNULL(C.NAME,''),
        STORE_NAME = S.NAME,
		SUM_SUP = SUM(DM.SUM_SUP),
        SUM_ACC = SUM(DM.SUM_ACC)
--		DIFF = SUM(DM.SUM_ACC-DM.SUM_SUP)
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
	LEFT JOIN INVOICE I ON DM.ID_DOCUMENT = I.ID_INVOICE_GLOBAL
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_FROM
    WHERE DM.CODE_OP='ADD' AND DM.ID_TABLE<>19 --SIGN_OP=1 AND DM.ID_TABLE <> 19
    AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
    AND DM.ID_STORE IN (SELECT ID_STORE FROM @STORE)
	AND (@NOAU = 1 OR (DM.ID_TABLE not in (8, 37, 39)) OR dm.id_store_from not in (select id_store from @store where id_contractor = dm.id_contractor_to))
    GROUP BY 
			DM.ID_DOCUMENT, 
			DM.ID_TABLE, 
			TD.DESCRIPTION, 
			DM.DATE_OP, 
			AD.DOC_NUM, 
			ISNULL(TD.DESCRIPTION+': ','')+ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')',''),
			CASE WHEN DM.ID_TABLE = 2 THEN ISNULL('� ��:'+ I.INCOMING_NUMBER,'') ELSE '' END,
			ISNULL(DM.ID_CONTRACTOR_FROM, 0), 
			ISNULL(C.NAME,''),
			S.NAME
    ORDER BY CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN DM.DATE_OP ELSE NULL END, 
		CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN NULL ELSE TD.DESCRIPTION END, 
		ISNULL(C.NAME,''), DM.DATE_OP
    
    -- ������
    select
        ID_DOCUMENT = DM.ID_DOCUMENT,
        ID_TABLE = CASE WHEN DM.ID_STORE IS NULL THEN 99 ELSE DM.ID_TABLE END,
        DOC_TYPE = CASE WHEN DM.ID_STORE IS NULL THEN '������' ELSE TD.DESCRIPTION END,
        DOC_DATE = DM.DATE_OP,
        DOC_NUM = AD.DOC_NUM,
        DOC_NAME = CASE WHEN DM.ID_STORE IS NULL THEN '������' ELSE ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END END,
        ID_CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END,
        CONTRACTOR_TO = CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'') END,
		STORE_NAME = S.NAME,
        SUM_DIS = SUM(CASE WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE<>19 THEN DM.SUM_ACC
 						   WHEN DM.CODE_OP='DIS' AND DM.ID_TABLE=19 THEN DM.SUM_ACC * SIGN_OP
 						   ELSE 0 END),
		SUM_SUP = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_SUP * DM.SIGN_OP * -1
						   END),--SUM(DM.SUM_SUP), --         SUM_SUP = SUM(CASE WHEN CODE_OP='DIS' THEN 0 ELSE DM.SUM_SUP*DM.SIGN_OP * -1 END),
        SUM_ACC = SUM(CASE WHEN DM.CODE_OP='DIS' THEN 0
						   ELSE DM.SUM_ACC * DM.SIGN_OP * -1
						   END)
-- 		DIFF = SUM((CASE WHEN DM.CODE_OP='DIS' THEN 0
-- 						   ELSE DM.SUM_ACC * DM.SIGN_OP * -1
-- 						   END)-DM.SUM_SUP)
	INTO #T
    FROM DOC_MOVEMENT DM
    INNER JOIN TABLE_DATA TD ON TD.ID_TABLE_DATA = DM.ID_TABLE
    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = DM.ID_DOCUMENT
    LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
    LEFT JOIN STORE S ON S.ID_STORE = DM.ID_STORE_TO
    WHERE (CODE_OP IN ('SUB','DIS') OR DM.ID_TABLE=19)--(SIGN_OP=-1 OR DM.ID_TABLE=19)
    AND DATE_OP BETWEEN @DATE_FROM AND @DATE_TO
    AND (DM.ID_STORE IS NULL OR DM.ID_STORE IN (SELECT ID_STORE FROM @STORE))
	AND (@NOAU = 1 OR (DM.ID_TABLE not in (8, 37, 39)) OR dm.id_store_to not in (select id_store from @store where id_contractor = dm.id_contractor_from))
    GROUP BY 
			DM.ID_DOCUMENT,			
			CASE WHEN DM.ID_STORE IS NULL THEN 99 ELSE DM.ID_TABLE END,
			CASE WHEN DM.ID_STORE IS NULL THEN '������' ELSE TD.DESCRIPTION END,
			DM.DATE_OP, 
			AD.DOC_NUM, 
			CASE WHEN DM.ID_STORE IS NULL THEN '������' ELSE ISNULL(TD.DESCRIPTION+': ','')+CASE WHEN DM.ID_TABLE=19 THEN '' ELSE ISNULL(C.NAME,'')+ISNULL('('+S.NAME+')','') END END,
			CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(DM.ID_CONTRACTOR_TO, 0) END, 
			CASE WHEN DM.ID_TABLE=19 THEN NULL ELSE ISNULL(C.NAME,'') END,
			S.NAME
	--ORDER BY CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN DM.DATE_OP ELSE ISNULL(DM.ID_TABLE DM.ID_STORE, 99) END, DM.DATE_OP
	ORDER BY CASE WHEN DM.ID_STORE IS NULL THEN 99 ELSE DM.ID_TABLE END, DM.DATE_OP
	
	SELECT * FROM #T
	ORDER BY CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN DOC_DATE ELSE NULL END, 
		CASE WHEN NULLIF(@SORT_BY_DOCTYPE,0) IS NULL THEN NULL ELSE DOC_TYPE END, 
		CONTRACTOR_TO, DOC_DATE
	--������� ����� �� �������
	SELECT SUM_DIS = SUM(SUM_DIS) FROM #T

    -- �� �����

    SELECT
        SUM_SUP = SUM(DM.SUM_SUP * DM.SIGN_OP),
        SUM_ACC = SUM(CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP),
		NAL = SUM((CASE WHEN ID_TABLE=19 AND CODE_OP='DIS' THEN -1 ELSE 1 END * DM.SUM_ACC * DM.SIGN_OP) - DM.SUM_SUP * DM.SIGN_OP)
    FROM DOC_MOVEMENT DM
    WHERE DATE_OP <= @DATE_TO
    AND (DM.ID_STORE IN (SELECT ID_STORE FROM @STORE))

-- ����������� � ������ ����� ,
    DECLARE @CONTRACTOR_STRING VARCHAR(4000)
    DECLARE @STORE_STRING VARCHAR(4000)

    SELECT 
        @STORE_STRING = ISNULL(@STORE_STRING+' ,'+S.NAME, S.NAME)
    FROM (SELECT DISTINCT ID_STORE FROM @STORE) T
    INNER JOIN STORE S ON S.ID_STORE = T.ID_STORE

    SELECT 
        @CONTRACTOR_STRING = ISNULL(@CONTRACTOR_STRING+' ,'+C.NAME, C.NAME)
    FROM (SELECT DISTINCT ID_CONTRACTOR FROM @STORE) T
    INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = T.ID_CONTRACTOR

    SELECT CONTRACTOR = @CONTRACTOR_STRING, STORE = @STORE_STRING

	SELECT S_SUM_ACC = SUM(SUM_ACC) FROM #T WHERE ID_TABLE = 99
	
RETURN
GO

/*
exec REPEX_AP25_SERVICE N'
<XML>
	<DATE_FROM>2010-01-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2010-01-25T23:00:00.000</DATE_TO>
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<SORT_BY_DOCTYPE>0</SORT_BY_DOCTYPE>
	<REFRESH_DOC_MOV>0</REFRESH_DOC_MOV>
</XML>'*/