SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('REPEX_CHEQUE') IS NULL EXEC('CREATE PROCEDURE REPEX_CHEQUE AS RETURN')
GO

ALTER PROCEDURE REPEX_CHEQUE
	(@XMLPARAM NTEXT) AS

/* PARAMETERS */
DECLARE @HDOC INT
DECLARE @STORE VARCHAR(500)
DECLARE @LEN INT
DECLARE @ID_CHEQUE_GLOBAL UNIQUEIDENTIFIER



EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_CHEQUE_GLOBAL = ID_CHEQUE_GLOBAL
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_CHEQUE_GLOBAL UNIQUEIDENTIFIER 'ID_CHEQUE_GLOBAL')
EXEC SP_XML_REMOVEDOCUMENT @HDOC


SET @STORE = ''
SET @LEN = 0
SELECT @STORE = @STORE + ST.[NAME] + ', ' FROM
	(SELECT DISTINCT S.ID_STORE, S.[NAME] 
		FROM CHEQUE CH
			INNER JOIN CHEQUE_ITEM CH_I ON CH_I.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
			INNER JOIN LOT L ON CH_I.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
			INNER JOIN STORE S ON L.ID_STORE = S.ID_STORE
		WHERE CH.ID_CHEQUE_GLOBAL = @ID_CHEQUE_GLOBAL) ST

SET @LEN = LEN(@STORE)
IF (@LEN >= 1)
BEGIN
  SET @STORE = LEFT(@STORE, @LEN - 1)
END



SELECT
	CAST(CH.ID_CHEQUE AS VARCHAR) AS N_CHEQUE,CH.DATE_CHEQUE,
 	CONTRACTOR_NAME = case 
 	                      when C.FULL_NAME is null then C.NAME
 	                      when C.FULL_NAME = '' then C.NAME
 	                      else C.FULL_NAME
 	                  end,    
 	C.ADDRESS, C.INN,
	@STORE AS STORE,
	(SELECT SUM(SUMM) FROM CHEQUE_ITEM WHERE ID_CHEQUE_GLOBAL = @ID_CHEQUE_GLOBAL) AS ITOGO
	,USER_FULL_NAME = mu.NAME--mu.FULL_NAME
	,USER_ROLE_NAME = mr.NAME
	,CUSTOMER = ASR.CUSTOMER_NAME
	,RESERVE_DATE = CONVERT(VARCHAR(10), ASR.[DATE], 104)
	,BARCODE = CH.BARCODE
FROM CHEQUE CH
	INNER JOIN CASH_SESSION CS ON CH.ID_CASH_SESSION_GLOBAL = CS.ID_CASH_SESSION_GLOBAL
	LEFT JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
	LEFT JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
	inner join meta_user mu on mu.USER_NUM = ch.ID_USER_DATA
	left join META_USER2ROLE mu2r on mu2r.ID_USER = mu.ID_USER
	left join META_ROLE mr on mr.ID_ROLE = mu2r.ID_ROLE
	LEFT JOIN ARM_STOCK_RESERVE ASR ON ASR.ID_CHEQUE_GLOBAL = CH.ID_CHEQUE_GLOBAL
WHERE CH.ID_CHEQUE_GLOBAL = @ID_CHEQUE_GLOBAL 

SELECT 
	CASE WHEN CH_I.ID_GOODS=0 THEN S4S.NAME ELSE G.NAME END AS GOODS_NAME,
	P.[NAME] AS PRODUCER_NAME,
	CH_I.QUANTITY,
	U.SHORT_NAME AS UNIT_NAME,
	CH_I.PRICE,
	CH_I.SUMM_DISCOUNT,
	(CH_I.QUANTITY * CH_I.PRICE) AS SUMM,
	CH_I.SUMM AS SUMM_ITOGO 
	--select  *	
FROM CHEQUE CH
	INNER JOIN CHEQUE_ITEM CH_I ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
	LEFT JOIN GOODS G ON CH_I.ID_GOODS = G.ID_GOODS
	LEFT JOIN SCALING_RATIO SR ON CH_I.ID_SCALING_RATIO = SR.ID_SCALING_RATIO
	LEFT JOIN PRODUCER P ON G.ID_PRODUCER = P.ID_PRODUCER
	LEFT JOIN UNIT U ON SR.ID_UNIT = U.ID_UNIT
	LEFT JOIN SERVICE_4_SALE S4S ON S4S.ID_SERVICE_4_SALE = CH_I.ID_LOT_GLOBAL
	--LEFT JOIN SERVICE S ON S.ID_SERVICE = S4S.ID_SERVICE
WHERE CH.ID_CHEQUE_GLOBAL = @ID_CHEQUE_GLOBAL
ORDER BY GOODS_NAME

RETURN 0
GO
/*

exec REPEX_CHEQUE @xmlParam=N'<XML>
          <ID_CHEQUE_GLOBAL>E27E4054-45DA-4AD3-A48C-0EFF562CDF70</ID_CHEQUE_GLOBAL>
          </XML>'
          
          */
--select * from CHEQUE_ITEM


SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO 

IF OBJECT_ID('DBO.REMOVE_REPORT_BY_TYPE_NAME') IS NULL EXEC('CREATE PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME AS RETURN')
GO
ALTER PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME 
	@REPORT_TYPE_NAME VARCHAR(200) AS
	
DECLARE @ID_META_REPORT BIGINT

	SELECT 
		@ID_META_REPORT = ID_META_REPORT
	FROM META_REPORT
	WHERE TYPE_NAME = @REPORT_TYPE_NAME
	--SELECT @ID_META_REPORT
		
	DECLARE @SQL NVARCHAR(200)
	SET @SQL = N'DELETE FROM META_REPORT_2_REPORT_GROUPS
				WHERE ID_META_REPORT = @ID_META_REPORT'
	IF (OBJECT_ID('META_REPORT_2_REPORT_GROUPS') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@ID_META_REPORT BIGINT', @ID_META_REPORT=@ID_META_REPORT
		

	SET @SQL = N'DELETE FROM META_REPORT_SETTINGS_CSV_EXPORT
		WHERE ID_META_REPORT = @ID_META_REPORT'
	IF (OBJECT_ID('META_REPORT_SETTINGS_CSV_EXPORT') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@ID_META_REPORT BIGINT', @ID_META_REPORT=@ID_META_REPORT
		

	SET @SQL = N'DELETE FROM META_REPORT_SETTINGS_VISIBLE
		WHERE ID_META_REPORT = @ID_META_REPORT'
	IF (OBJECT_ID('META_REPORT_SETTINGS_VISIBLE') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@ID_META_REPORT BIGINT', @ID_META_REPORT=@ID_META_REPORT
		

	SET @SQL = N'DELETE FROM META_REPORT_SETTINGS_MANAGED
				WHERE ID_META_REPORT = @ID_META_REPORT'
	IF (OBJECT_ID('META_REPORT_SETTINGS_MANAGED') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@ID_META_REPORT BIGINT', @ID_META_REPORT=@ID_META_REPORT


	SET @SQL = N'DELETE FROM META_REPORT_SETTINGS_ARCHIVE
				WHERE ID_META_REPORT = @ID_META_REPORT'
	IF (OBJECT_ID('META_REPORT_SETTINGS_ARCHIVE') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@ID_META_REPORT BIGINT', @ID_META_REPORT=@ID_META_REPORT


	DELETE FROM META_REPORT
	WHERE ID_META_REPORT = @ID_META_REPORT

RETURN 0
GO


EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'ChequeEx.ChequeEx'
GO
