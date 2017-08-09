SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('DBO.REPEX_MEDICINE_REESTR_SOGAZ') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_MEDICINE_REESTR_SOGAZ AS RETURN')
GO

ALTER PROCEDURE DBO.REPEX_MEDICINE_REESTR_SOGAZ
    @XMLPARAM NTEXT AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @DATE_SH_FR DATETIME
DECLARE @DATE_SH_TO DATETIME
DECLARE @ALL_LGOT BIT
DECLARE @ALL_STORE BIT
DECLARE @ALL_INS BIT
DECLARE @ALL_CONTRACTORS BIT
DECLARE @ALL_GOODS BIT
DECLARE @ALL_LPU BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
SELECT
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC, '/XML') WITH(
	DATE_FR DATETIME 'DATE_FROM',
	DATE_TO DATETIME 'DATE_TO'
)

SELECT * INTO #LGOT FROM OPENXML(@HDOC, '//ID_LGOT') 
WITH(ID_LGOT UNIQUEIDENTIFIER '.')
IF @@ROWCOUNT = 0 SET @ALL_LGOT = 1

SELECT * INTO #STORE FROM OPENXML(@HDOC, '//ID_STORE') 
WITH(ID_STORE BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_STORE = 1;

SELECT * INTO #INS FROM OPENXML(@HDOC, '//ID_INS') 
WITH(ID_INS UNIQUEIDENTIFIER '.')
IF @@ROWCOUNT = 0 SET @ALL_INS = 1;

SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, '//ID_CONTRACTOR') 
WITH(ID_CONTRACTOR BIGINT '.')
IF @@ROWCOUNT = 0 SET @ALL_CONTRACTORS = 1;

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT


SELECT * FROM 
(
SELECT
	INSURANCE_COMPANY = IC.NAME, 
	LPU_CODE = ISNULL(MO.CODE, ''),
	NUMBER_POLICY = TL.NUMBER_POLICY,
	NUMBER_RECIPE = ISNULL(TL.SERIES_RECIPE + ' ', '') + TL.NUMBER_RECIPE,
	DATE_RECIPE = TL.DATE_RECIPE,
	MEMBER_FIO = DM.LASTNAME + ' ' + DM.FIRSTNAME + ' ' +DM.MIDDLENAME,
	GOODS_CODE = G.MNEMOCODE,
	CONTRACTOR_NAME = C.NAME,
	GOODS_NAME = G.NAME, 
	QUANTITY = CHI.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / CONVERT(MONEY,SR.DENOMINATOR), 
	PRICE = L.PRICE_SAL * CONVERT(MONEY,SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR), 
	GOODS_SUMM = L.PRICE_SAL * CHI.QUANTITY, 
	FREE_SUMM = CASE 
		WHEN (TL.PERCENT_PAY_CREDIT = 100 OR CH.SUMM = TL.SUMM_PAY_CREDIT)
			THEN L.PRICE_SAL * CHI.QUANTITY
		ELSE 0
		END, 
	PAYED_SUMM = CASE 
		WHEN TL.PERCENT_PAY_CREDIT > 0 THEN (1 - (TL.PERCENT_PAY_CREDIT / 100)) * L.PRICE_SAL * CHI.QUANTITY
		ELSE L.PRICE_SAL * CHI.QUANTITY * (1 - TL.SUMM_PAY_CREDIT / CH.SUMM)
		END, 
	REPAYMENT = CASE 
		WHEN TL.PERCENT_PAY_CREDIT > 0 THEN (TL.PERCENT_PAY_CREDIT / 100) * L.PRICE_SAL * CHI.QUANTITY
		ELSE L.PRICE_SAL * CHI.QUANTITY * (TL.SUMM_PAY_CREDIT / CH.SUMM)
		END, 
	DATE_CHEQUE = COALESCE (TL.DATE_SHIP, TL.DATE_REG, DATE_CHEQUE)
FROM TRUST_LETTER TL
	INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL		
	INNER JOIN CHEQUE_ITEM CHI ON CHI.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
	INNER JOIN GOODS G ON CHI.ID_GOODS = G.ID_GOODS
	INNER JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN DISCOUNT2_INSURANCE_POLICY IP ON IP.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN DISCOUNT2_MEMBER DM ON IP.ID_DISCOUNT2_MEMBER_GLOBAL = DM.ID_DISCOUNT2_MEMBER_GLOBAL
	LEFT JOIN DISCOUNT2_MEDICAL_ORGANIZATION MO ON MO.ID_DISCOUNT2_MEDICAL_ORGANIZATION = TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION
	LEFT JOIN LOT L ON L.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL
	LEFT JOIN STORE ON STORE.ID_STORE = L.ID_STORE
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = STORE.ID_CONTRACTOR
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
	
WHERE /**/ --TL.DATE_REG IS NOT NULL
	TL.DOCUMENT_STATE = 'PROC'
	AND(NOT EXISTS (SELECT * FROM CHEQUE rch
		WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
			AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL)	
	OR EXISTS
		(SELECT * FROM CHEQUE rch
			inner join CHEQUE ch2 on ch2.ID_DOCUMENT_BASE_GLOBAL = rch.ID_CHEQUE_GLOBAL		
			inner join CHEQUE_ITEM chi2 on ch2.ID_CHEQUE_GLOBAL = chi2.ID_CHEQUE_GLOBAL
		WHERE rch.CHEQUE_TYPE = 'RETURN' AND rch.DOCUMENT_STATE IN ('PRINTED', 'PROC')
			AND rch.ID_DOCUMENT_BASE_GLOBAL = CH.ID_CHEQUE_GLOBAL and chi2.ID_LOT_GLOBAL = CHI.ID_LOT_GLOBAL))	
	AND ISNULL(TL.DATE_REG, DATE_CHEQUE) BETWEEN @DATE_FR AND @DATE_TO
	--AND COALESCE (TL.DATE_SHIP, TL.DATE_REG, DATE_CHEQUE) BETWEEN @DATE_SH_FR AND @DATE_SH_TO
	AND (@ALL_LGOT = 1 OR DM.ID_DISCOUNT2_MEMBER_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
	AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
	--AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS))
	AND (@ALL_CONTRACTORS = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
	--AND (@ALL_LPU = 1 OR TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION IN (SELECT ID_LPU FROM #LPU))

UNION ALL

SELECT
	INSURANCE_COMPANY = IC.NAME, 
	LPU_CODE = ISNULL(MO.CODE, ''),
	NUMBER_POLICY = TL.NUMBER_POLICY,
	NUMBER_RECIPE = isnull(TL.SERIES_RECIPE + ' ', '') + TL.NUMBER_RECIPE,
	DATE_RECIPE = TL.DATE_RECIPE,
	MEMBER_FIO = DM.LASTNAME + ' ' + DM.FIRSTNAME + ' ' +DM.MIDDLENAME,
	GOODS_CODE = G.MNEMOCODE,
	CONTRACTOR_NAME = C.NAME,
	GOODS_NAME = G.NAME, 
	QUANTITY = TLI.QUANTITY * CONVERT(MONEY, SR.NUMERATOR) / CONVERT(MONEY,SR.DENOMINATOR), 
	PRICE = L.PRICE_SAL * CONVERT(MONEY,SR.DENOMINATOR) / CONVERT(MONEY, SR.NUMERATOR), 
	GOODS_SUMM = L.PRICE_SAL * TLI.QUANTITY, 
	FREE_SUMM = CASE 
		WHEN (TL.PERCENT_PAY_CREDIT = 100 OR TL.SUMM_PAY_CREDIT > 0)
			THEN L.PRICE_SAL * TLI.QUANTITY
		ELSE 0
		END, 
	PAYED_SUMM = CASE 
		WHEN (TL.PERCENT_PAY_CREDIT = 100 OR TL.SUMM_PAY_CREDIT > 0) THEN 0
		ELSE L.PRICE_SAL * TLI.QUANTITY
		END, 
	REPAYMENT = CASE 
		WHEN (TL.PERCENT_PAY_CREDIT = 100 OR TL.SUMM_PAY_CREDIT > 0) 
			THEN L.PRICE_SAL * TLI.QUANTITY
		ELSE 0
		END, 
	DATE_CHEQUE = TL.DATE_REG
FROM TRUST_LETTER TL
	INNER JOIN TRUST_LETTER_ITEM TLI ON TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = TLI.ID_SOURCE_GLOBAL
	LEFT JOIN GOODS G ON L.ID_GOODS = G.ID_GOODS
	LEFT JOIN STORE ON STORE.ID_STORE = L.ID_STORE
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = STORE.ID_CONTRACTOR
	INNER JOIN DISCOUNT2_INSURANCE_COMPANY IC ON IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = TL.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN DISCOUNT2_INSURANCE_POLICY IP ON IP.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL = IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL
	INNER JOIN DISCOUNT2_MEMBER DM ON IP.ID_DISCOUNT2_MEMBER_GLOBAL = DM.ID_DISCOUNT2_MEMBER_GLOBAL
	LEFT JOIN DISCOUNT2_MEDICAL_ORGANIZATION MO ON MO.ID_DISCOUNT2_MEDICAL_ORGANIZATION = TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION
	LEFT JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	LEFT JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
WHERE 
	TL.DOCUMENT_STATE = 'PROC'
	AND TL.DATE_REG BETWEEN @DATE_FR AND @DATE_TO
	--AND ISNULL(TL.DATE_SHIP,TL.DATE_REG) BETWEEN @DATE_SH_FR AND @DATE_SH_TO
	AND (@ALL_LGOT = 1 OR DM.ID_DISCOUNT2_MEMBER_GLOBAL IN (SELECT ID_LGOT FROM #LGOT))
	AND (@ALL_STORE = 1 OR ST.ID_STORE IN (SELECT ID_STORE FROM #STORE))
	AND (@ALL_INS = 1 OR IC.ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL IN (SELECT ID_INS FROM #INS))
	AND NOT EXISTS(SELECT NULL FROM CHEQUE CH WHERE CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL)
	--AND (@ALL_GOODS = 1 OR G.ID_GOODS IN (SELECT ID_GOODS FROM #GOODS))
	AND (@ALL_CONTRACTORS = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTORS))
	--AND (@ALL_LPU = 1 OR TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION IN (SELECT ID_LPU FROM #LPU))
) A
ORDER BY A.DATE_CHEQUE

RETURN
GO
 
/*
exec REPEX_TRUST_LETTER_AP25 N'
<XML>
	<DATE_FROM>2014-06-01T14:01:27.223</DATE_FROM>
	<DATE_TO>2014-07-01T14:01:27.223</DATE_TO>
	<DATE_SHIP_FROM>2014-06-19T14:05:55.875</DATE_SHIP_FROM>
	<DATE_SHIP_TO>2014-06-20T14:05:55.875</DATE_SHIP_TO>
</XML>
'

<ID_INS>22A3893D-C889-4F50-83D8-4160A0C70164</ID_INS>
*/