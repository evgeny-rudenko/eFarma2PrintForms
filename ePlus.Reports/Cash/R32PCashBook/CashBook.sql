SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF COL_LENGTH('CASH_SESSION', 'ID_USER_CLOSE_GLOBAL') IS NULL
    ALTER TABLE CASH_SESSION ADD ID_USER_CLOSE_GLOBAL UNIQUEIDENTIFIER NULL

IF (OBJECT_ID('DBO.REPEX_CASH_BOOK') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_CASH_BOOK AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_CASH_BOOK
	(@XMLPARAM NTEXT) AS

DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME,
	@DATE_TO DATETIME,
	@ALL_CONTRACTOR BIT,
	@ALL_CASH_REGISTER BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT TOP 1
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO
FROM OPENXML(@HDOC, '/XML') WITH
	(DATE_FR DATETIME 'DATE_FR',
	DATE_TO DATETIME 'DATE_TO')

SELECT * INTO #CONTRACTOR FROM OPENXML(@HDOC, '//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CONTRACTOR = 1
SELECT * INTO #CASH_REGISTER FROM OPENXML(@HDOC, '//ID_CASH_REGISTER') WITH(ID_CASH_REGISTER BIGINT '.')
IF (@@ROWCOUNT = 0)
	SET @ALL_CASH_REGISTER = 1

EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

select 
	cs.MNEMOCODE
	, cs.DATE_OPEN
	, cr.NAME_CASH_REGISTER
	, cr.NUMBER_CASH_REGISTER
	, cn.NAME
	--, c.SUMM
	--, c.SUM_DISCOUNT
	--, c.CHEQUE_TYPE
	--, c.PAY_TYPE
	--, c.PAY_TYPE_NAME
	--, cp.PAYMENT_TYPE_NAME
	--, cp.SEPARATE_TYPE
	--, cp.TYPE_PAYMENT
	--, cp.SUMM
	--, l.VAT_SAL
	, SALE_NAL_NDS_0		= case when c.CHEQUE_TYPE = 'SALE' and l.VAT_SAL = 0  and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, SALE_NAL_NDS_10		= case when c.CHEQUE_TYPE = 'SALE' and l.VAT_SAL = 10 and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, SALE_NAL_NDS_18		= case when c.CHEQUE_TYPE = 'SALE' and l.VAT_SAL = 18 and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, SALE_BEZNAL			= case when c.CHEQUE_TYPE = 'SALE' and cp.TYPE_PAYMENT = 'TYPE2' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, RETURN_NAL_NDS_0		= case when c.CHEQUE_TYPE = 'RETURN' and l.VAT_SAL = 0  and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, RETURN_NAL_NDS_10		= case when c.CHEQUE_TYPE = 'RETURN' and l.VAT_SAL = 10 and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, RETURN_NAL_NDS_18		= case when c.CHEQUE_TYPE = 'RETURN' and l.VAT_SAL = 18 and cp.TYPE_PAYMENT = 'TYPE1' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
	, RETURN_BEZNAL			= case when c.CHEQUE_TYPE = 'RETURN' and cp.TYPE_PAYMENT = 'TYPE2' then ci.SUMM - ci.SUMM_DISCOUNT else 0 end
into #t	
from CASH_SESSION cs
	join CASH_REGISTER cr on cr.ID_CASH_REGISTER = cs.ID_CASH_REGISTER
	join CONTRACTOR cn on cn.ID_CONTRACTOR = cr.ID_CONTRACTOR
	join CHEQUE c on c.ID_CASH_SESSION_GLOBAL = cs.ID_CASH_SESSION_GLOBAL
	join CHEQUE_PAYMENT cp on cp.ID_CHEQUE_GLOBAL = c.ID_CHEQUE_GLOBAL
	join CHEQUE_ITEM ci on ci.ID_CHEQUE_GLOBAL = c.ID_CHEQUE_GLOBAL
	join LOT l on l.ID_LOT_GLOBAL = ci.ID_LOT_GLOBAL
where 1=1
	and c.DOCUMENT_STATE = 'PROC'
	and c.CHEQUE_TYPE in ('SALE', 'RETURN')
	and cs.DATE_OPEN between @DATE_FR and @DATE_TO
	and (@ALL_CONTRACTOR = 1 or cn.ID_CONTRACTOR in (select ID_CONTRACTOR from #CONTRACTOR))
	and (@ALL_CASH_REGISTER = 1 or cr.ID_CASH_REGISTER in (select ID_CASH_REGISTER from #CASH_REGISTER))
order by cs.DATE_OPEN

select
	NAME
	, NAME_CASH_REGISTER
	, MNEMOCODE
	, DATE_OPEN = CONVERT(VARCHAR(10), DATE_OPEN, 104)
	, SALE_NAL_NDS_0 = SUM(SALE_NAL_NDS_0)
	, SALE_NAL_NDS_10 = SUM(SALE_NAL_NDS_10)
	, SALE_NAL_NDS_18 = SUM(SALE_NAL_NDS_18)
	, SALE_BEZNAL = SUM(SALE_BEZNAL)
	, RETURN_NAL_NDS_0 = SUM(RETURN_NAL_NDS_0)
	, RETURN_NAL_NDS_10 = SUM(RETURN_NAL_NDS_10)
	, RETURN_NAL_NDS_18 = SUM(RETURN_NAL_NDS_18)
	, RETURN_BEZNAL = SUM(RETURN_BEZNAL)
from #t
group by
	NAME
	, NAME_CASH_REGISTER
	, MNEMOCODE
	, DATE_OPEN

RETURN 0
GO

/*
EXEC DBO.REPEX_CASH_BOOK N'
<XML>
  <DATE_FR>2012-03-01T15:13:32.784</DATE_FR>
  <DATE_TO>2012-03-27T15:13:32.784</DATE_TO>
</XML>'
*/