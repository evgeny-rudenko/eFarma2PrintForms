SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_BONUSREMADD') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_BONUSREMADD AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_BONUSREMADD
	@XMLPARAM NTEXT AS
		
DECLARE	@HDOC INT

DECLARE	@DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @ALL_CONTRACTOR BIT
DECLARE @ALL_DISCOUNT2_CARD BIT
DECLARE @CARD_ONLY_WITH_MOVE BIT

EXEC DISCOUNT2_CARD_BONUS_APPLY
   
EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
		
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@CARD_ONLY_WITH_MOVE = CARD_ONLY_WITH_MOVE
FROM OPENXML(@HDOC , '/XML') 
	WITH(DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO',
	CARD_ONLY_WITH_MOVE BIT 'CARD_ONLY_WITH_MOVE')

SELECT ID_CONTRACTOR = ID_CONTRACTOR INTO #CONTRACTOR
FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTOR = 1
	ELSE 
	SET @ALL_CONTRACTOR = 0
--SELECT * FROM #CONTRACTOR
SELECT ID_DISCOUNT2_CARD_GLOBAL = ID_DISCOUNT2_CARD_GLOBAL INTO #DISCOUNT2_CARD
FROM OPENXML(@HDOC, '/XML/ID_DISCOUNT2_CARD_GLOBAL') WITH(ID_DISCOUNT2_CARD_GLOBAL UNIQUEIDENTIFIER '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_DISCOUNT2_CARD = 1	

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUTPUT, @DATE_TO OUTPUT
EXEC USP_RANGE_DAYS @DATE_FR OUTPUT, @DATE_TO OUTPUT

SELECT  CARD_TYPE_NAME = DCT.NAME
		,CARD_NUMBER = DC.NUMBER
		,DC.ID_DISCOUNT2_CARD_GLOBAL
		,BONUS_ADD_BEGIN = ISNULL(DCBH.BONUS_ADD_BEGIN, 0)
		,BONUS_ADD_ALL = ISNULL(DCBH.BONUS_ADD_ALL, 0) 
		,BONUS_REMOVE_ALL = ISNULL(DCBH.BONUS_REMOVE_ALL, 0) 
		,BONUS_ADD_END = ISNULL(DCBH.BONUS_ADD_ALL, 0) - ISNULL(DCBH.BONUS_REMOVE_ALL, 0) + ISNULL(DCBH.BONUS_ADD_BEGIN, 0)
	--,DATE_START
		--SELECT *
FROM DISCOUNT2_CARD DC
LEFT JOIN DISCOUNT2_CARD_TYPE DCT ON DCT.ID_DISCOUNT2_CARD_TYPE_GLOBAL = DC.ID_DISCOUNT2_CARD_TYPE_GLOBAL
LEFT JOIN (
	SELECT TMP.ID_DISCOUNT2_CARD_GLOBAL
			,BONUS_ADD_BEGIN=SUM(ISNULL(TMP.BONUS_ADD_BEGIN, 0))
			,BONUS_ADD_ALL = SUM(ISNULL(TMP.BONUS_ADD_ALL, 0))
			,BONUS_REMOVE_ALL = SUM(ISNULL(TMP.BONUS_REMOVE_ALL, 0))
			,BONUS_ADD_END = SUM(ISNULL(TMP.BONUS_ADD_END, 0))
	FROM (		
	SELECT	ID_DISCOUNT2_CARD_GLOBAL
			,BONUS_ADD_BEGIN=SUM(ISNULL(BONUS, 0))
			,BONUS_ADD_ALL = 0
			,BONUS_REMOVE_ALL = 0
			,BONUS_ADD_END = 0
	FROM DISCOUNT2_CARD_BONUS_HISTORY DCBH
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = DCBH.ID_CHEQUE_GLOBAL
	INNER JOIN CASH_SESSION CS ON CH.ID_CASH_SESSION_GLOBAL=CS.ID_CASH_SESSION_GLOBAL
	INNER JOIN CASH_REGISTER CR ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	WHERE DATE_APPLIED<@DATE_FR
	AND (@ALL_CONTRACTOR = 1 OR(CR.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR)))
	GROUP BY ID_DISCOUNT2_CARD_GLOBAL
	---------------------------------------------
	UNION ALL
	SELECT	ID_DISCOUNT2_CARD_GLOBAL
			,BONUS_ADD_BEGIN = 0
			,BONUS_ADD_ALL = SUM(CASE WHEN BONUS > 0 THEN BONUS ELSE 0 END )
			,BONUS_REMOVE_ALL = SUM(CASE WHEN BONUS < 0 THEN -BONUS ELSE 0 END )
			,BONUS_ADD_END = 0
	FROM DISCOUNT2_CARD_BONUS_HISTORY DCBH
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = DCBH.ID_CHEQUE_GLOBAL
	INNER JOIN CASH_SESSION CS ON CH.ID_CASH_SESSION_GLOBAL=CS.ID_CASH_SESSION_GLOBAL
	INNER JOIN CASH_REGISTER CR ON CR.ID_CASH_REGISTER = CS.ID_CASH_REGISTER
	WHERE DATE_APPLIED BETWEEN @DATE_FR AND @DATE_TO
			AND (@ALL_CONTRACTOR = 1 OR(CR.ID_CONTRACTOR IN (SELECT * FROM #CONTRACTOR)))
	GROUP BY ID_DISCOUNT2_CARD_GLOBAL
	) TMP
	GROUP BY ID_DISCOUNT2_CARD_GLOBAL
	---------------------------------------------
) DCBH ON DCBH.ID_DISCOUNT2_CARD_GLOBAL = DC.ID_DISCOUNT2_CARD_GLOBAL
WHERE ISNULL(DCT.IS_BONUS,0) = 1
	AND (@ALL_DISCOUNT2_CARD = 1 OR (DCBH.ID_DISCOUNT2_CARD_GLOBAL IN (SELECT ID_DISCOUNT2_CARD_GLOBAL FROM #DISCOUNT2_CARD)))
	AND DC.DATE_START < @DATE_TO
	AND ISNULL(DC.DATE_END, GETDATE()) > @DATE_FR
	AND (@CARD_ONLY_WITH_MOVE = 0 OR ((BONUS_ADD_BEGIN<>0)OR(BONUS_ADD_ALL<>0)OR(BONUS_REMOVE_ALL<>0)OR(BONUS_ADD_END<>0)))
	AND (@ALL_CONTRACTOR = 1 OR ((BONUS_ADD_BEGIN<>0)OR(BONUS_ADD_ALL<>0)OR(BONUS_REMOVE_ALL<>0)OR(BONUS_ADD_END<>0)))
ORDER BY  DC.DATE_MODIFIED DESC

RETURN 0
GO  

/*
EXEC REPEX_BONUSREMADD '
<XML>
	<DATE_FR>2001-09-15</DATE_FR>
	<DATE_TO>2022-09-18T16:35:00.000</DATE_TO>
	<CARD_ONLY_WITH_MOVE>1</CARD_ONLY_WITH_MOVE>
	
</XML>'

*/



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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'BonusRemAdd.DefecturaParams'