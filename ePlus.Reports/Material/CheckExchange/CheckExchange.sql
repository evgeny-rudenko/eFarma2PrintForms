SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_CHECK_EXCHANGE') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_CHECK_EXCHANGE AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_CHECK_EXCHANGE
    @XMLPARAM NTEXT AS
    
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @RTYPE BIT
DECLARE @ALL_AU BIT
DECLARE @AU_GUID UNIQUEIDENTIFIER
DECLARE @ID_AU VARCHAR(40)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@RTYPE = RTYPE
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	RTYPE BIT 'RTYPE'
)

SELECT * INTO #AU FROM OPENXML(@HDOC, '/XML/ID_AU') WITH(ID_AU VARCHAR(40) '.')
IF (@@ROWCOUNT = 0)	SET @ALL_AU = 1
    
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT


CREATE TABLE #TO_ALL (ID_REPLICATION_SESSION UNIQUEIDENTIFIER, ID_CONTRACTOR_TO_GLOBAL UNIQUEIDENTIFIER)

DECLARE @ID_REPLICATION_SESSION UNIQUEIDENTIFIER
DECLARE CR CURSOR FOR SELECT RS.ID_REPLICATION_SESSION FROM REPLICATION_SESSION RS
INNER JOIN REPLICATION_CONFIG RC ON RC.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
WHERE ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000'
	AND RS.SESSION_STATE IN ('SENT_OK', 'SENT_ERR', 'RESP_OK', 'RESP_ERR') AND RS.DIRECTION = 'EXP' 
	AND RS.REPLICATION_MODEL IN ('DCT_FOR_ALL', 'DCT', 'DOC')
	AND RS.CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
	--AND (@ALL_AU = 1 OR RC.GLOBAL_CODE IN (SELECT ID_AU FROM #AU))

OPEN CR
FETCH NEXT FROM CR INTO @ID_REPLICATION_SESSION
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO #TO_ALL
	        ( ID_REPLICATION_SESSION ,
	          ID_CONTRACTOR_TO_GLOBAL
	        )
	SELECT @ID_REPLICATION_SESSION, RC.ID_CONTRACTOR_GLOBAL FROM REPLICATION_CONFIG RC WHERE /*RC.IS_SELF <> 1 AND */RC.IS_ACTIVE = 1
	AND NOT EXISTS (SELECT NULL FROM #TO_ALL T WHERE T.ID_REPLICATION_SESSION = @ID_REPLICATION_SESSION 
		AND T.ID_CONTRACTOR_TO_GLOBAL = RC.ID_CONTRACTOR_GLOBAL)
	AND NOT EXISTS (SELECT NULL FROM REPLICATION_SESSION RS WHERE RS.ID_REPLICATION_SESSION = @ID_REPLICATION_SESSION 
		AND RS.ID_CONTRACTOR_FROM_GLOBAL = RC.ID_CONTRACTOR_GLOBAL)
	FETCH NEXT FROM CR INTO @ID_REPLICATION_SESSION
END
CLOSE CR
DEALLOCATE CR

SELECT A.*
INTO #TEMP
FROM (

		SELECT  
			CASE
			WHEN (RS.DIRECTION = 'IMP') 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				THEN 'SRP_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (SESSION_STATE <> 'RESP_OK' AND SESSION_STATE <> 'RESP_ERR')
				THEN 'SPR_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (SESSION_STATE = 'RESP_OK' OR SESSION_STATE = 'RESP_ERR')
				THEN 'SPR_RESP'
			WHEN DIRECTION = 'IMP' 
				AND (REPLICATION_MODEL = 'DOC')
				THEN 'DOC_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (SESSION_STATE <> 'RESP_OK' AND SESSION_STATE <> 'RESP_ERR')
				THEN 'DOC_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (SESSION_STATE = 'RESP_OK' OR SESSION_STATE = 'RESP_ERR')
				THEN 'DOC_RESP'
			END AS REPL_TYPE, 
        	RS.DIRECTION,
        	RS.SESSION_STATE,
        	RS.SESSION_NUMBER,
        	CREATED_DATE = RS.CREATED_DATE,
			C_FROM = CFR.ID_CONTRACTOR_GLOBAL,
        	NAME_FROM = CFR.NAME,
        	C_TO = isnull(CTO.ID_CONTRACTOR_GLOBAL, '00000000-0000-0000-0000-000000000000'), 
        	NAME_TO  = CASE WHEN RS.ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000' THEN '��� ����'
                            ELSE CTO.NAME
                       END,
        	STATE = CONVERT(VARCHAR, RS.ERRORS_COUNT) + '/' + CONVERT(VARCHAR, RS.ITEMS_COUNT),
            CASE RS.REPLICATION_MODEL 
					               WHEN 'DCT' THEN 'SPR'
                                   WHEN 'DCT_FOR_ALL' THEN 'SPR'                                  
                                   ELSE RS.REPLICATION_MODEL
            END AS REPLICATION_MODEL
        FROM REPLICATION_SESSION RS

		INNER JOIN REPLICATION_CONFIG RCF ON RCF.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL AND RCF.IS_ACTIVE = 1
		LEFT JOIN REPLICATION_CONFIG RCT ON RCT.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_TO_GLOBAL AND RCT.IS_ACTIVE = 1

        LEFT JOIN CONTRACTOR CFR ON CFR.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
        LEFT JOIN CONTRACTOR CTO ON CTO.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_TO_GLOBAL
        WHERE  RS.CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO 
        AND RS.SESSION_STATE <> 'NEW'
		AND RS.ID_CONTRACTOR_TO_GLOBAL <> '00000000-0000-0000-0000-000000000000'
		AND RS.REPLICATION_MODEL IN ('DCT_FOR_ALL', 'DCT', 'DOC')

UNION ALL
		SELECT  
			CASE
			WHEN (RS.DIRECTION = 'IMP') 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				THEN 'SRP_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (RS.SESSION_STATE <> 'RESP_OK' AND RS.SESSION_STATE <> 'RESP_ERR')
				THEN 'SPR_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (RS.SESSION_STATE = 'RESP_OK' OR RS.SESSION_STATE = 'RESP_ERR')
				THEN 'SPR_RESP'
			WHEN DIRECTION = 'IMP' 
				AND (REPLICATION_MODEL = 'DOC')
				THEN 'DOC_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (RS.SESSION_STATE <> 'RESP_OK' AND RS.SESSION_STATE <> 'RESP_ERR')
				THEN 'DOC_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (RS.SESSION_STATE = 'RESP_OK' OR RS.SESSION_STATE = 'RESP_ERR')
				THEN 'DOC_RESP'
			END AS REPL_TYPE, 
        	RS.DIRECTION,
        	RS.SESSION_STATE,
        	RS.SESSION_NUMBER,
        	CREATED_DATE = RS.CREATED_DATE,
			C_FROM = CFR.ID_CONTRACTOR_GLOBAL,
        	NAME_FROM = CFR.NAME,
        	C_TO = #TO_ALL.ID_CONTRACTOR_TO_GLOBAL, --isnull(CTO.ID_CONTRACTOR_GLOBAL, '00000000-0000-0000-0000-000000000000'), 
        	NAME_TO  = /*(SELECT [NAME] FROM CONTRACTOR WHERE ID_CONTRACTOR_GLOBAL = #TO_ALL.ID_CONTRACTOR_TO_GLOBAL)*/
						CASE WHEN RS.ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000' THEN '��� ����'
                            ELSE CTO.NAME
                        END,
        	STATE = CONVERT(VARCHAR, RS.ERRORS_COUNT) + '/' + CONVERT(VARCHAR, RS.ITEMS_COUNT),
            CASE RS.REPLICATION_MODEL 
					               WHEN 'DCT' THEN 'SPR'
                                   WHEN 'DCT_FOR_ALL' THEN 'SPR'                                  
                                   ELSE RS.REPLICATION_MODEL
            END AS REPLICATION_MODEL
        FROM REPLICATION_SESSION RS
		INNER JOIN #TO_ALL ON RS.ID_REPLICATION_SESSION = #TO_ALL.ID_REPLICATION_SESSION

		INNER JOIN REPLICATION_CONFIG RCF ON RCF.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL AND RCF.IS_ACTIVE = 1
		INNER JOIN REPLICATION_CONFIG RCT ON RCT.ID_CONTRACTOR_GLOBAL = #TO_ALL.ID_CONTRACTOR_TO_GLOBAL AND RCT.IS_ACTIVE = 1
		LEFT JOIN REPLICATION_SESSION_RESPONCE RSR ON RS.ID_REPLICATION_SESSION = RSR.ID_REPLICATION_SESSION AND RSR.SESSION_STATE LIKE 'RESP_%'
			AND RSR.ID_REPLICATION_CONFIG = RCT.ID_REPLICATION_CONFIG

        LEFT JOIN CONTRACTOR CFR ON CFR.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
        LEFT JOIN CONTRACTOR CTO ON CTO.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_TO_GLOBAL
        WHERE  RS.CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO 
		AND RS.ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000'
		AND RS.SESSION_STATE IN ('SENT_OK', 'SENT_ERR', 'RESP_OK', 'RESP_ERR') AND RS.DIRECTION = 'EXP' 
		AND RS.REPLICATION_MODEL IN ('DCT_FOR_ALL', 'DCT', 'DOC')
/*������ ����������� ������ ��� ����*/
UNION ALL
		SELECT  
			CASE
			WHEN (RS.DIRECTION = 'IMP') 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				THEN 'SRP_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (SESSION_STATE <> 'RESP_OK' AND SESSION_STATE <> 'RESP_ERR')
				THEN 'SPR_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DCT_FOR_ALL' OR REPLICATION_MODEL = 'DCT')
				AND (SESSION_STATE = 'RESP_OK' OR SESSION_STATE = 'RESP_ERR')
				THEN 'SPR_RESP'
			WHEN DIRECTION = 'IMP' 
				AND (REPLICATION_MODEL = 'DOC')
				THEN 'DOC_IMP'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (SESSION_STATE <> 'RESP_OK' AND SESSION_STATE <> 'RESP_ERR')
				THEN 'DOC_SENT'
			WHEN DIRECTION = 'EXP' 
				AND (REPLICATION_MODEL = 'DOC')
				AND (SESSION_STATE = 'RESP_OK' OR SESSION_STATE = 'RESP_ERR')
				THEN 'DOC_RESP'
			END AS REPL_TYPE, 
        	RS.DIRECTION,
        	RS.SESSION_STATE,
        	RS.SESSION_NUMBER,
        	CREATED_DATE = RS.CREATED_DATE,
			C_FROM = CFR.ID_CONTRACTOR_GLOBAL,
        	NAME_FROM = CFR.NAME,
        	C_TO = (SELECT TOP 1 ID_CONTRACTOR_GLOBAL FROM REPLICATION_CONFIG WHERE IS_SELF = 1), 
        	NAME_TO  = /*(SELECT TOP 1 C.NAME FROM REPLICATION_CONFIG RC
						INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR_GLOBAL = RC.ID_CONTRACTOR_GLOBAL
						WHERE RC.IS_SELF = 1)*/
						CASE WHEN RS.ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000' THEN '��� ����'
                            ELSE CTO.NAME
                        END,
        	STATE = CONVERT(VARCHAR, RS.ERRORS_COUNT) + '/' + CONVERT(VARCHAR, RS.ITEMS_COUNT),
            CASE RS.REPLICATION_MODEL 
					               WHEN 'DCT' THEN 'SPR'
                                   WHEN 'DCT_FOR_ALL' THEN 'SPR'                                  
                                   ELSE RS.REPLICATION_MODEL
            END AS REPLICATION_MODEL
        --INTO #TEMP
        FROM REPLICATION_SESSION RS

		INNER JOIN REPLICATION_CONFIG RCF ON RCF.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL AND RCF.IS_ACTIVE = 1
		
        LEFT JOIN CONTRACTOR CFR ON CFR.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
        LEFT JOIN CONTRACTOR CTO ON CTO.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_TO_GLOBAL
        WHERE  RS.CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO 
		AND /*(*/(RS.ID_CONTRACTOR_TO_GLOBAL = '00000000-0000-0000-0000-000000000000'
		AND RS.SESSION_STATE IN ('IMP_OK', 'IMP_ERR') AND RS.DIRECTION = 'IMP')
		AND RS.REPLICATION_MODEL IN ('DCT_FOR_ALL', 'DCT', 'DOC')
) A

--select * from #TEMP

IF @RTYPE = 0
BEGIN
	SELECT DISTINCT C.ID_CONTRACTOR, C.NAME, 
		CNT_DCT_IMP_ERR = ISNULL(IMP.CNT_DCT_IMP_ERR, 0), CNT_DOC_IMP_ERR = ISNULL(IMP.CNT_DOC_IMP_ERR, 0), 
		CNT_DCT_IMP_OK = ISNULL(IMP.CNT_DCT_IMP_OK, 0), CNT_DOC_IMP_OK = ISNULL(IMP.CNT_DOC_IMP_OK, 0), 
		CNT_DCT_SENT_ERR = ISNULL([SENT].CNT_DCT_SENT_ERR, 0), CNT_DOC_SENT_ERR = ISNULL([SENT].CNT_DOC_SENT_ERR, 0), 
		CNT_DCT_SENT_OK = ISNULL([SENT].CNT_DCT_SENT_OK, 0), CNT_DOC_SENT_OK = ISNULL([SENT].CNT_DOC_SENT_OK, 0), 
		CNT_DCT_RESP_ERR = ISNULL(RESP.CNT_DCT_RESP_ERR, 0), CNT_DOC_RESP_ERR = ISNULL(RESP.CNT_DOC_RESP_ERR, 0), 
		CNT_DCT_RESP_OK = ISNULL(RESP.CNT_DCT_RESP_OK, 0), CNT_DOC_RESP_OK = ISNULL(RESP.CNT_DOC_RESP_OK, 0)
	FROM REPLICATION_CONFIG RC
	INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR_GLOBAL = RC.ID_CONTRACTOR_GLOBAL-- AND RC.IS_ACTIVE = '1'
	LEFT JOIN 
	(
-- IMP
		SELECT CTO.ID_CONTRACTOR, 
		ISNULL(RS1.CNT, 0) AS CNT_DCT_IMP_ERR, 
		ISNULL(RS2.CNT, 0) AS CNT_DOC_IMP_ERR, 
		ISNULL(RS3.CNT, 0) AS CNT_DCT_IMP_OK, 
		ISNULL(RS4.CNT, 0) AS CNT_DOC_IMP_OK
		FROM (SELECT DISTINCT ID_CONTRACTOR_TO_GLOBAL FROM REPLICATION_SESSION) RS
		INNER JOIN CONTRACTOR CTO ON CTO.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_TO_GLOBAL
		LEFT JOIN 
-- �����, �������, � ��������
		(SELECT C_TO, 
			COUNT(C_TO) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_ERR')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_ERR'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_TO) RS1
			ON RS1.C_TO = RS.ID_CONTRACTOR_TO_GLOBAL
		LEFT JOIN 
-- ���, �������, � ��������
		(SELECT C_TO, 
			COUNT(C_TO) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_ERR')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_ERR'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_TO) RS2
			ON RS2.C_TO = RS.ID_CONTRACTOR_TO_GLOBAL
		LEFT JOIN 
-- �����, �������, ��� ������
		(SELECT C_TO, 
			COUNT(C_TO) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_OK')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_OK'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_TO) RS3
			ON RS3.C_TO = RS.ID_CONTRACTOR_TO_GLOBAL
		LEFT JOIN 
-- ���, �������, ��� ������
		(SELECT	C_TO, 
			COUNT(C_TO) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_OK')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_OK'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_TO) RS4
			ON RS4.C_TO = RS.ID_CONTRACTOR_TO_GLOBAL	
		) IMP ON IMP.ID_CONTRACTOR = C.ID_CONTRACTOR

	LEFT JOIN
	(
	
-- SENT
	SELECT CFR.ID_CONTRACTOR, 
		ISNULL(RS1.CNT, 0) AS CNT_DCT_SENT_ERR, 
		ISNULL(RS2.CNT, 0) AS CNT_DOC_SENT_ERR, 
		ISNULL(RS3.CNT, 0) AS CNT_DCT_SENT_OK, 
		ISNULL(RS4.CNT, 0) AS CNT_DOC_SENT_OK
	FROM (SELECT DISTINCT ID_CONTRACTOR_FROM_GLOBAL FROM REPLICATION_SESSION) RS
	INNER JOIN CONTRACTOR CFR ON CFR.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
	LEFT JOIN 
-- �����, ����������, � ��������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND DIRECTION = 'EXP'
			AND SESSION_STATE = 'SENT_ERR'
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS1
			ON RS1.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- ���, ����������, � ��������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND DIRECTION = 'EXP'
			AND SESSION_STATE = 'SENT_ERR'
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS2
			ON RS2.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- �����, ����������, ��� ������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND DIRECTION = 'EXP'
			AND SESSION_STATE = 'SENT_OK'
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS3
			ON RS3.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- ���, ����������, ��� ������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND DIRECTION = 'EXP'
			AND SESSION_STATE = 'SENT_OK'
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS4
			ON RS4.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL	
	) [SENT] ON [SENT].ID_CONTRACTOR = C.ID_CONTRACTOR

	LEFT JOIN
	(
-- RESP
	SELECT CFR.ID_CONTRACTOR, 
		ISNULL(RS1.CNT, 0) AS CNT_DCT_RESP_ERR, 
		ISNULL(RS2.CNT, 0) AS CNT_DOC_RESP_ERR, 
		ISNULL(RS3.CNT, 0) AS CNT_DCT_RESP_OK, 
		ISNULL(RS4.CNT, 0) AS CNT_DOC_RESP_OK
	FROM (SELECT DISTINCT ID_CONTRACTOR_FROM_GLOBAL FROM REPLICATION_SESSION) RS
	INNER JOIN CONTRACTOR CFR ON CFR.ID_CONTRACTOR_GLOBAL = RS.ID_CONTRACTOR_FROM_GLOBAL
	LEFT JOIN 
-- �����, ������������, � ��������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_ERR')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_ERR'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS1
			ON RS1.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- ���, ������������, � ��������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_ERR')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_ERR'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS2
			ON RS2.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- �����, ������������, ��� ������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'SPR')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_OK')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_OK'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS3
			ON RS3.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL
		LEFT JOIN 
-- ���, ������������, ��� ������
		(SELECT C_FROM, 
			COUNT(C_FROM) AS CNT
			FROM #TEMP
			WHERE (REPLICATION_MODEL = 'DOC')
			AND ((DIRECTION = 'IMP' AND SESSION_STATE = 'IMP_OK')
			OR (DIRECTION = 'EXP' AND SESSION_STATE = 'RESP_OK'))
			AND CREATED_DATE BETWEEN @DATE_FR AND @DATE_TO
			GROUP BY C_FROM) RS4
			ON RS4.C_FROM = RS.ID_CONTRACTOR_FROM_GLOBAL				
	) RESP ON RESP.ID_CONTRACTOR = C.ID_CONTRACTOR
	WHERE (@ALL_AU = 1 OR RC.GLOBAL_CODE IN (SELECT ID_AU FROM #AU))
	AND IS_ACTIVE = 1
	ORDER BY C.ID_CONTRACTOR

	DROP TABLE #TEMP
END
ELSE
BEGIN
	SELECT TOP 1 @ID_AU = ID_AU FROM #AU
	
	SELECT TOP 1 @AU_GUID = RC.ID_CONTRACTOR_GLOBAL FROM REPLICATION_CONFIG RC
	WHERE RC.GLOBAL_CODE = @ID_AU

--select * from #TEMP

-- ����������� �������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE 
	REPLICATION_MODEL = 'SPR' AND
	(
		(DIRECTION = 'IMP' AND SESSION_STATE IN ('IMP_OK', 'IMP_ERR')) 
		OR 
		(DIRECTION = 'EXP' AND SESSION_STATE IN ('RESP_OK', 'RESP_ERR'))
	)
	AND ((C_TO = @AU_GUID) OR (C_TO = '00000000-0000-0000-0000-000000000000' AND 
		@AU_GUID = (SELECT TOP 1 ID_CONTRACTOR_GLOBAL FROM REPLICATION_CONFIG WHERE IS_SELF = 1)))
	ORDER BY CREATED_DATE
	
-- ����������� ����������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE DIRECTION = 'EXP' 
	AND (REPLICATION_MODEL = 'SPR')
	AND (SESSION_STATE IN ('SENT_OK', 'SENT_ERR'))
	AND C_FROM = @AU_GUID
	ORDER BY CREATED_DATE

-- ����������� ������������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE 
	REPLICATION_MODEL = 'SPR'
	AND 
	(
		(DIRECTION = 'EXP' AND SESSION_STATE IN ('RESP_OK', 'RESP_ERR'))
		OR
		(DIRECTION = 'IMP' AND SESSION_STATE IN ('IMP_OK', 'IMP_ERR'))
	)
	AND C_FROM = @AU_GUID
	ORDER BY CREATED_DATE

-- ��������� �������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE
	REPLICATION_MODEL = 'DOC'
	AND
	(
		(DIRECTION = 'IMP' AND SESSION_STATE IN ('IMP_OK', 'IMP_ERR')) 
		OR 
		(DIRECTION = 'EXP' AND SESSION_STATE IN ('RESP_OK', 'RESP_ERR'))
	)
	AND ((C_TO = @AU_GUID) OR (C_TO = '00000000-0000-0000-0000-000000000000' AND 
		@AU_GUID = (SELECT TOP 1 ID_CONTRACTOR_GLOBAL FROM REPLICATION_CONFIG WHERE IS_SELF = 1)))
	ORDER BY CREATED_DATE
	
-- ��������� ����������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE DIRECTION = 'EXP' 
	AND (REPLICATION_MODEL = 'DOC')
	AND (SESSION_STATE IN ('SENT_OK', 'SENT_ERR'))
	AND C_FROM = @AU_GUID
	ORDER BY CREATED_DATE
	
-- ��������� ������������
SELECT CREATED_DATE, SESSION_NUMBER, NAME_FROM, NAME_TO, STATE FROM #TEMP WHERE 
	REPLICATION_MODEL = 'DOC'
	AND 
	(
		(DIRECTION = 'EXP' AND SESSION_STATE IN ('RESP_OK', 'RESP_ERR'))
		OR
		(DIRECTION = 'IMP' AND SESSION_STATE IN ('IMP_OK', 'IMP_ERR'))
	)
	AND C_FROM = @AU_GUID
	ORDER BY CREATED_DATE
	
DROP TABLE #TEMP
        		
END
RETURN
GO

/*exec REPEX_CHECK_EXCHANGE 
@xmlParam=N'<XML><DATE_FR>2011-01-12T15:12:14.640</DATE_FR><DATE_TO>2011-01-12T15:12:14.640</DATE_TO><RTYPE>0</RTYPE></XML>'*/

/*
exec REPEX_CHECK_EXCHANGE 
@xmlParam=N'<XML><DATE_FR>2011-01-12T14:05:11.093</DATE_FR><DATE_TO>2011-01-12T14:05:11.093</DATE_TO><RTYPE>1</RTYPE><ID_AU>2</ID_AU></XML>'
*/