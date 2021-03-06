SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_MOVEMENT_REGISTRY') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_MOVEMENT_REGISTRY AS RETURN')
GO
ALTER  PROCEDURE DBO.REPEX_MOVEMENT_REGISTRY
    @XMLPARAM NTEXT AS
    
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ADD_SUB BIT
DECLARE @TYPE_REM SMALLINT

DECLARE @ALL_STORE_FR BIT
DECLARE @ALL_CONTRACTOR_FR BIT
DECLARE @ALL_CGROUP_FR BIT
DECLARE @ALL_STORE_TO BIT
DECLARE @ALL_CONTRACTOR_TO BIT
DECLARE @ALL_CGROUP_TO BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM

SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@ADD_SUB = ADD_SUB, 
	@TYPE_REM = TYPE_REM	
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	ADD_SUB BIT 'ADD_SUB', 
	TYPE_REM SMALLINT 'TYPE_REM'
)
    
SELECT * INTO #STORES_FR FROM OPENXML(@HDOC, '/XML/ID_STORE_FROM') WITH(ID_STORE BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_STORE_FR = 1

SELECT * INTO #STORES_TO FROM OPENXML(@HDOC, '/XML/ID_STORE_TO') WITH(ID_STORE BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_STORE_TO = 1

SELECT * INTO #CONTRACTORS_FR FROM OPENXML(@HDOC, '/XML/ID_CONTRACTORS_FROM') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CONTRACTOR_FR = 1

SELECT * INTO #CONTRACTORS_TO FROM OPENXML(@HDOC, '/XML/ID_CONTRACTORS_TO') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CONTRACTOR_TO = 1

SELECT * INTO #CGROUPS_FR FROM OPENXML(@HDOC, '/XML/ID_CGROUPS_FROM') WITH(ID_CGROUPS BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CGROUP_FR = 1

SELECT * INTO #CGROUPS_TO FROM OPENXML(@HDOC, '/XML/ID_CGROUPS_TO') WITH(ID_CGROUPS BIGINT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_CGROUP_TO = 1

CREATE TABLE #CONTRACTOR_FROM (ID_CONTRACTOR BIGINT)

IF @ALL_CONTRACTOR_FR IS NULL
BEGIN
	INSERT INTO #CONTRACTOR_FROM
	        ( ID_CONTRACTOR )
	SELECT ID_CONTRACTOR FROM #CONTRACTORS_FR
END/*
ELSE IF @ALL_CONTRACTOR_FR = 1
BEGIN
	INSERT INTO #CONTRACTOR_FROM
	        ( ID_CONTRACTOR )
	SELECT ID_CONTRACTOR FROM CONTRACTOR
END*/
IF @ALL_CGROUP_FR IS NULL
BEGIN
	IF @ALL_CONTRACTOR_FR IS NULL
	BEGIN
		DELETE #CONTRACTOR_FROM FROM #CONTRACTOR_FROM CF
		WHERE CF.ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP CC 
			WHERE CC.ID_CONTRACTOR_GROUP IN (SELECT ID_CGROUPS FROM #CGROUPS_FR))
	END
	ELSE
	BEGIN
		INSERT INTO #CONTRACTOR_FROM (ID_CONTRACTOR)
		SELECT DISTINCT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP
			WHERE ID_CONTRACTOR_GROUP IN (SELECT ID_CGROUPS FROM #CGROUPS_FR)
			AND ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS_FR)
	END
END
/*ELSE IF @ALL_CGROUP_FR = 1
BEGIN
	INSERT INTO #CONTRACTOR_FROM (ID_CONTRACTOR)
	SELECT DISTINCT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP
		WHERE ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS_FR)
END*/

CREATE TABLE #CONTRACTOR_TO(ID_CONTRACTOR BIGINT)

IF @ALL_CONTRACTOR_TO IS NULL
BEGIN
	INSERT INTO #CONTRACTOR_TO
	        ( ID_CONTRACTOR )
	SELECT ID_CONTRACTOR FROM #CONTRACTORS_TO	
END/*
ELSE IF @ALL_CONTRACTOR_TO = 1
BEGIN
	INSERT INTO #CONTRACTOR_TO
	        ( ID_CONTRACTOR )
	SELECT ID_CONTRACTOR FROM CONTRACTOR
END*/
IF @ALL_CGROUP_TO IS NULL
BEGIN
	IF @ALL_CONTRACTOR_TO IS NULL
	BEGIN
		DELETE #CONTRACTOR_TO FROM #CONTRACTOR_TO CT
		WHERE CT.ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP CC 
			WHERE CC.ID_CONTRACTOR_GROUP IN (SELECT ID_CGROUPS FROM #CGROUPS_TO))
	END
	ELSE
	BEGIN
		INSERT INTO #CONTRACTOR_TO (ID_CONTRACTOR)
		SELECT DISTINCT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP
			WHERE ID_CONTRACTOR_GROUP IN (SELECT ID_CGROUPS FROM #CGROUPS_TO)
			AND ID_CONTRACTOR NOT IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS_TO)
	END
END

DROP TABLE #CGROUPS_FR
DROP TABLE #CGROUPS_TO
DROP TABLE #CONTRACTORS_FR
DROP TABLE #CONTRACTORS_TO

--select * from #stores
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

/* ��� ��������*/
/*
SET @ADD_SUB = 'SUB'
SET @TYPE_REM = 1
SET @DATE_FR = DATEADD(D, 0, '2010-01-01')
SET @DATE_TO = GETDATE()
*/
/*����� ���� ��������*/

SELECT DISTINCT
	CFROMNAME = CASE WHEN IM.ID_STORE_FROM_MAIN IS NULL THEN CF.NAME ELSE CFM.NAME END, 
	CFROMID = CASE WHEN IM.ID_STORE_FROM_MAIN IS NULL THEN CF.ID_CONTRACTOR ELSE CFM.ID_CONTRACTOR END, 
	SFROM = CASE WHEN IM.ID_STORE_FROM_MAIN IS NULL THEN SF.NAME ELSE SFM.NAME END, 
	CTONAME = CASE WHEN IM.ID_STORE_TO_MAIN IS NULL THEN CT.NAME ELSE CTM.NAME END, 
	CTOID = CASE WHEN IM.ID_STORE_TO_MAIN IS NULL THEN CT.ID_CONTRACTOR ELSE CTM.ID_CONTRACTOR END, 
	STO = CASE WHEN IM.ID_STORE_TO_MAIN IS NULL THEN ST.NAME ELSE STM.NAME END, 
	AD.DOC_NUM, 
	AD.DOC_DATE, 
	AD.SUM_SUP, 
	AD.SUM_SAL, 
	DM.CODE_OP
  INTO #TEMP
  FROM DOC_MOVEMENT DM 
  INNER JOIN ALL_DOCUMENT AD ON DM.ID_DOCUMENT = AD.ID_DOCUMENT_GLOBAL
  LEFT JOIN CONTRACTOR CF ON CF.ID_CONTRACTOR = DM.ID_CONTRACTOR_FROM
  LEFT JOIN CONTRACTOR CT ON CT.ID_CONTRACTOR = DM.ID_CONTRACTOR_TO
  LEFT JOIN STORE SF ON DM.ID_STORE_FROM = SF.ID_STORE
  LEFT JOIN STORE ST ON DM.ID_STORE_TO = ST.ID_STORE
  
  LEFT JOIN INTERFIRM_MOVING IM ON IM.ID_INTERFIRM_MOVING_GLOBAL = DM.ID_DOCUMENT
  LEFT JOIN STORE SFM ON IM.ID_STORE_FROM_MAIN = SFM.ID_STORE
  LEFT JOIN STORE STM ON IM.ID_STORE_TO_MAIN = STM.ID_STORE
  LEFT JOIN CONTRACTOR CFM ON SFM.ID_CONTRACTOR = CFM.ID_CONTRACTOR
  LEFT JOIN CONTRACTOR CTM ON STM.ID_CONTRACTOR = CTM.ID_CONTRACTOR
  
  WHERE 
  ((DM.ID_TABLE = 8 AND @TYPE_REM = 0) OR 
	(DM.ID_TABLE = 37 AND @TYPE_REM = 1) OR
	((DM.ID_TABLE = 8 OR DM.ID_TABLE = 37) AND @TYPE_REM = 2)
  )
  AND AD.DOC_STATE = 'PROC'
  AND (DM.CODE_OP = CASE WHEN @ADD_SUB = 0 THEN 'ADD' ELSE 'SUB' END)
  AND AD.DOC_DATE BETWEEN @DATE_FR AND @DATE_TO
  AND 
	((@ALL_CGROUP_FR = 1 AND @ALL_CONTRACTOR_FR = 1) OR 
	CASE WHEN IM.ID_STORE_FROM_MAIN IS NULL THEN CF.ID_CONTRACTOR ELSE CFM.ID_CONTRACTOR END IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR_FROM))
  AND
	((@ALL_CGROUP_TO = 1 AND @ALL_CONTRACTOR_TO = 1) OR
	CASE WHEN IM.ID_STORE_TO_MAIN IS NULL THEN CT.ID_CONTRACTOR ELSE CTM.ID_CONTRACTOR END IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR_TO))
  AND
	(@ALL_STORE_FR = 1 OR CASE WHEN IM.ID_STORE_FROM_MAIN IS NULL THEN SF.ID_STORE ELSE SFM.ID_STORE END IN (SELECT ID_STORE FROM #STORES_FR))
  AND
	(@ALL_STORE_TO = 1 OR CASE WHEN IM.ID_STORE_TO_MAIN IS NULL THEN ST.ID_STORE ELSE STM.ID_STORE END IN (SELECT ID_STORE FROM #STORES_TO))

SELECT 
	T.DOC_NUM, 
	CTOID = CASE WHEN @ADD_SUB = 0 THEN T.CTOID ELSE T.CFROMID END, 
	CTONAME = CASE WHEN @ADD_SUB = 0 THEN T.CTONAME ELSE T.CFROMNAME END, 
	STO = CASE WHEN @ADD_SUB = 0 THEN T.STO ELSE T.SFROM END, 
	SUM(T.SUM_SUP) AS SUM_SUP, 
	SUM(T.SUM_SAL) AS SUM_SAL
FROM #TEMP T
GROUP BY T.DOC_NUM, CASE WHEN @ADD_SUB = 0 THEN T.CTOID ELSE T.CFROMID END, 
	CASE WHEN @ADD_SUB = 0 THEN T.CTONAME ELSE T.CFROMNAME END, 
	CASE WHEN @ADD_SUB = 0 THEN T.STO ELSE T.SFROM END

DROP TABLE #TEMP

SELECT ID_STORE FROM #stores_to
SELECT * FROM #contractor_to

RETURN 
GO

/*
EXEC REPEX_MOVEMENT_REGISTRY N'
<XML>
  <CONTRACTORS_FROM>5294</CONTRACTORS_FROM>
  <CONTRACTORS_FROM>5275</CONTRACTORS_FROM>
  <CONTRACTORS_FROM>5271</CONTRACTORS_FROM>
  <STORES_FROM>179</STORES_FROM>
  <STORES_FROM>174</STORES_FROM>
  <STORES_FROM>156</STORES_FROM>
  <STORES_FROM>155</STORES_FROM>
  <CGROUP_FROM>116</CGROUP_FROM>
  <CGROUP_FROM>117</CGROUP_FROM>
  <DATE_FROM>2010-01-01T00:00:00.000</DATE_FROM> 
  <DATE_TO>2010-08-16T16:56:50.609</DATE_TO> 
  <ADD_SUB>1</ADD_SUB> 
  <TYPE_REM>2</TYPE_REM> 
</XML>'*/