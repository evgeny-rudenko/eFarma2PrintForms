SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_GOODS_REGISTRY_KURSK') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_GOODS_REGISTRY_KURSK AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_GOODS_REGISTRY_KURSK
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME

DECLARE @ALL_CONTRACTOR BIT
DECLARE @ALL_STORE BIT
DECLARE @ALL_INS BIT
DECLARE @ALL_LGOT BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_CONTRACTOR = 1

SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') 
WITH(ID_STORE BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_STORE = 1;

SELECT * INTO #INS FROM OPENXML(@HDOC, '//ID_INS') 
WITH(ID_INS UNIQUEIDENTIFIER '.')
IF @@ROWCOUNT = 0 SET @ALL_INS = 1;

SELECT * INTO #LGOT FROM OPENXML(@HDOC, '//ID_LGOT') 
WITH(ID_LGOT UNIQUEIDENTIFIER '.')
IF @@ROWCOUNT = 0 SET @ALL_LGOT = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT

SELECT
			A.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL, 
			LETTERS_COUNT = COUNT(DISTINCT A.NUMBER_RECIPE)
			INTO #TEMP
FROM
(
		
	SELECT 
		TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL,
		TL.NUMBER_RECIPE
		FROM TRUST_LETTER TL
		LEFT JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL		
		LEFT JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
		LEFT JOIN GOODS G ON CHI.ID_GOODS = G.ID_GOODS
		LEFT JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
		LEFT JOIN DISCOUNT2_MEMBER DM ON DM.ID_DISCOUNT2_MEMBER_GLOBAL = TL.ID_DISCOUNT2_MEMBER_GLOBAL
		LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
		LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	WHERE CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
		--AND TL.DATE_REG IS NOT NULL
		AND (@ALL_CONTRACTOR = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
		AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
		AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
		AND (@ALL_LGOT = 1 OR TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
		AND (NOT EXISTS (SELECT * FROM CHEQUE rch
			WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
				AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL)	
			OR EXISTS
			(SELECT * FROM CHEQUE rch
				inner join CHEQUE ch2 on ch2.ID_DOCUMENT_BASE_GLOBAL = rch.ID_CHEQUE_GLOBAL		
				inner join CHEQUE_ITEM chi2 on ch2.ID_CHEQUE_GLOBAL = chi2.ID_CHEQUE_GLOBAL
			WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
				AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL and chi2.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL))	
				
	UNION ALL
	
	SELECT 
		TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL,
		TL.NUMBER_RECIPE
		FROM TRUST_LETTER TL
		INNER JOIN TRUST_LETTER_ITEM TLI ON TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
		INNER JOIN LOT L ON L.ID_LOT_GLOBAL = TLI.ID_SOURCE_GLOBAL
		LEFT JOIN GOODS G ON L.ID_GOODS = G.ID_GOODS
		LEFT JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
		LEFT JOIN DISCOUNT2_MEMBER DM ON DM.ID_DISCOUNT2_MEMBER_GLOBAL = TL.ID_DISCOUNT2_MEMBER_GLOBAL
		LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	WHERE TL.DATE_REG BETWEEN @DATE_FR AND @DATE_TO
		--AND TL.DATE_REG IS NOT NULL
		AND (@ALL_CONTRACTOR = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
		AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
		AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
		AND (@ALL_LGOT = 1 OR TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
		AND NOT EXISTS(SELECT NULL FROM CHEQUE CH WHERE CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL)

) A
	GROUP BY A.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL

SELECT * 
FROM
(
SELECT
	NUMBER_RECIPE = TL.NUMBER_RECIPE,
	DATE_RECIPE = TL.DATE_RECIPE,
	DOCTOR = TL.DOCTOR,
	MEMBER_NAME = ISNULL(DM.LASTNAME + ' ', '') + ISNULL(DM.FIRSTNAME + ' ', '') + ISNULL(DM.MIDDLENAME, ''),
	LGOT = DLG.NAME, 
	NUMBER_POLICY = TL.NUMBER_POLICY,
	[ADDRESS] = DM.[ADDRESS],
	BIRTHDAY = DM.BIRTHDAY,	
	GOODS_NAME = G.NAME,
	QUANTITY = CHI.QUANTITY * CONVERT(MONEY,SR.NUMERATOR) / CONVERT(MONEY, SR.DENOMINATOR),
	PRICE = CHI.PRICE * CONVERT(MONEY,SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR),
	SUMM = ISNULL(ISNULL((SELECT TOP 1 SUMM FROM CHEQUE_PAYMENT WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND  TYPE_PAYMENT/*SEPARATE_TYPE*/ = 'TYPE4'),(SELECT TOP 1 SUMM FROM CHEQUE_PAYMENT WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND  /*TYPE_PAYMENT*/SEPARATE_TYPE = 'TYPE4')) * CHI.SUMM / CH.SUMM, 0),
	DATE_CHEQUE = CH.DATE_CHEQUE,
	PERCENT_PAY =FLOOR(ISNULL(ISNULL((SELECT TOP 1 SUMM FROM CHEQUE_PAYMENT WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND  TYPE_PAYMENT/*SEPARATE_TYPE*/ = 'TYPE4'),(SELECT TOP 1 SUMM FROM CHEQUE_PAYMENT WHERE ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL AND  /*TYPE_PAYMENT*/SEPARATE_TYPE = 'TYPE4')) * CHI.SUMM / CH.SUMM, 0) / (CHI.SUMM - CHI.SUMM_DISCOUNT) * 100),
	INSURANCE_COMPANY = IC.NAME, 
	RECIPE_COUNT = CTL.LETTERS_COUNT, 
	ALL_RECIPES = (SELECT SUM(LETTERS_COUNT) FROM #TEMP)
FROM TRUST_LETTER TL
	INNER JOIN /*(
		SELECT T.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL, T.LETTERS_COUNT, ALL_RECIPES = SUM(T.LETTERS_COUNT) FROM #TEMP T
	)*/ #TEMP CTL ON CTL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL		
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
	INNER JOIN GOODS G ON CHI.ID_GOODS = G.ID_GOODS
	INNER JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	LEFT JOIN DISCOUNT2_CAT_LGOT DLG ON TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL = DLG.ID_DISCOUNT2_CAT_LGOT_GLOBAL
	LEFT JOIN DISCOUNT2_MEMBER DM ON DM.ID_DISCOUNT2_MEMBER_GLOBAL = TL.ID_DISCOUNT2_MEMBER_GLOBAL
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
WHERE CH.DATE_CHEQUE BETWEEN @DATE_FR AND @DATE_TO
	--AND TL.DATE_REG IS NOT NULL
	AND (@ALL_CONTRACTOR = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
	AND (@ALL_LGOT = 1 OR TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
	AND (NOT EXISTS (SELECT * FROM CHEQUE rch
		WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
			AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL)	
	OR EXISTS
		(SELECT * FROM CHEQUE rch
			inner join CHEQUE ch2 on ch2.ID_DOCUMENT_BASE_GLOBAL = rch.ID_CHEQUE_GLOBAL		
			inner join CHEQUE_ITEM chi2 on ch2.ID_CHEQUE_GLOBAL = chi2.ID_CHEQUE_GLOBAL
		WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
			AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL and chi2.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL))	
			
UNION ALL

SELECT
	NUMBER_RECIPE = TL.NUMBER_RECIPE,
	DATE_RECIPE = TL.DATE_RECIPE,
	DOCTOR = TL.DOCTOR,
	MEMBER_NAME = ISNULL(DM.LASTNAME + ' ', '') + ISNULL(DM.FIRSTNAME + ' ', '') + ISNULL(DM.MIDDLENAME, ''),
	LGOT = DLG.NAME, 
	NUMBER_POLICY = TL.NUMBER_POLICY,
	[ADDRESS] = DM.[ADDRESS],
	BIRTHDAY = DM.BIRTHDAY,	
	GOODS_NAME = G.NAME,
	QUANTITY = TLI.QUANTITY * CONVERT(MONEY,SR.NUMERATOR) / CONVERT(MONEY, SR.DENOMINATOR),
	PRICE = L.PRICE_SAL * CONVERT(MONEY,SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR),
	SUMM = TLI.QUANTITY * L.PRICE_SAL,
	DATE_CHEQUE = TL.DATE_REG,
	PERCENT_PAY = 100,
	INSURANCE_COMPANY = IC.NAME, 
	RECIPE_COUNT = CTL.LETTERS_COUNT, 
	ALL_RECIPES = (SELECT SUM(LETTERS_COUNT) FROM #TEMP)
FROM TRUST_LETTER TL
	INNER JOIN /*(
		SELECT T.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL, T.LETTERS_COUNT, ALL_RECIPES = SUM(T.LETTERS_COUNT) FROM #TEMP T
	)*/ #TEMP CTL ON CTL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN TRUST_LETTER_ITEM TLI ON TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = TLI.ID_SOURCE_GLOBAL
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	LEFT JOIN GOODS G ON L.ID_GOODS = G.ID_GOODS
	INNER JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	LEFT JOIN DISCOUNT2_CAT_LGOT DLG ON TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL = DLG.ID_DISCOUNT2_CAT_LGOT_GLOBAL
	LEFT JOIN DISCOUNT2_MEMBER DM ON DM.ID_DISCOUNT2_MEMBER_GLOBAL = TL.ID_DISCOUNT2_MEMBER_GLOBAL
	LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
WHERE TL.DATE_REG BETWEEN @DATE_FR AND @DATE_TO
	--AND TL.DATE_REG IS NOT NULL
	AND (@ALL_CONTRACTOR = 1 OR ST.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
	AND (@ALL_LGOT = 1 OR TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
	AND NOT EXISTS(SELECT NULL FROM CHEQUE CH WHERE CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL)
			
) B
ORDER BY B.MEMBER_NAME, B.DATE_CHEQUE

SELECT
	DIRECTOR_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)

RETURN
GO
 
/*
exec REPEX_GOODS_REGISTRY_KURSK N'
<XML>
	<DATE_FR>2010-12-28T00:00:00.000</DATE_FR>
	<DATE_TO>2012-12-28T00:00:00.000</DATE_TO>
	
</XML>'
*/
--<ID_INS>22A3893D-C889-4F50-83D8-4160A0C70164</ID_INS>

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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'TrustLetterGoodsRegistryKursk.GoodsRegistryKurskParams'