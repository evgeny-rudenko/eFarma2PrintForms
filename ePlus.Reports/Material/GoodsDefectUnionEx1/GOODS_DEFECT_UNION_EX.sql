SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID(N'DBO.REPEX_GOODS_DEFECT_UNION') IS NULL EXEC(N'CREATE PROCEDURE DBO.REPEX_GOODS_DEFECT_UNION AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_GOODS_DEFECT_UNION
	@XMLPARAM NTEXT AS

DECLARE @HDOC INT

DECLARE @DATE_FR DATETIME
DECLARE @DATE_TO DATETIME
DECLARE @MNAME INT
DECLARE @MSER INT
DECLARE @TYPE INT
DECLARE @ORDER INT

DECLARE @ALL_CONTRACTORS BIT
DECLARE @ALL_STORES BIT
DECLARE @ALL_INVOICES BIT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT

SELECT 
	@DATE_FR = DATE_FR,
	@DATE_TO = DATE_TO,
	@MNAME = MNAME, 
	@MSER = MSER,
	@TYPE = [TYPE],
	@ORDER = [ORDER]
FROM OPENXML(@HDOC, N'/XML')	WITH (
	DATE_FR DATETIME N'DATE_FR',
	DATE_TO DATETIME N'DATE_TO',
	MNAME INT N'MNAME',
	MSER INT N'MSER',
	[TYPE] INT N'TYPE',
	[ORDER] INT N'ORDER'
)		

SELECT * INTO #CONTRACTORS FROM OPENXML(@HDOC, N'//ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT N'.')
	IF @@ROWCOUNT = 0 SET @ALL_CONTRACTORS = 1
--SELECT * FROM #CONTRACTORS
SELECT * INTO #STORES FROM OPENXML(@HDOC, N'//ID_STORE') WITH(ID_STORE BIGINT N'.')
	IF @@ROWCOUNT = 0 SET @ALL_STORES = 1
--SELECT * FROM #STORES
SELECT * INTO #INVOICES FROM OPENXML(@HDOC, N'//ID_INVOICE') WITH(ID_INVOICE BIGINT N'.')
	IF @@ROWCOUNT = 0 SET @ALL_INVOICES = 1
--SELECT * FROM #INVOICES
EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT

--select @DATE_FR, @DATE_TO, @type, @ORDER

DECLARE @JOIN_FILTER NVARCHAR(255)
DECLARE @ORDER_FILTER NVARCHAR(255)
DECLARE @QUERY NVARCHAR(4000)

IF (@ORDER = 0)
	BEGIN
		SET @ORDER_FILTER = ' ORDER BY NAME_G'
	END 
		ELSE
			BEGIN
				SET @ORDER_FILTER = ' ORDER BY SERIES_GD'
			END


IF (@TYPE = 0)
	BEGIN
		SET @JOIN_FILTER = CASE WHEN @MNAME = 0 THEN N'' ELSE N'LEFT(G.GOODS_NAME, ' + CAST(@MNAME AS NVARCHAR) + N') = LEFT(GD.DRUG_TXT, ' + CAST(@MNAME AS NVARCHAR) + N') AND ' END + CASE WHEN @MSER = 0 THEN N'G.SERIES_NUMBER = GD.SERIES_NR' ELSE N'LEFT(G.SERIES_NUMBER, ' + CAST(@MSER AS NVARCHAR) + N') = LEFT(GD.SERIES_NR, ' + CAST(@MSER AS NVARCHAR) + N')' END
		--SELECT @JOIN_FILTER

		DECLARE @TODAY DATETIME
		SET @TODAY = GETDATE()

		DECLARE @CONTR_FILTER NVARCHAR(4000)
		SET @CONTR_FILTER = N''
		IF (ISNULL(@ALL_CONTRACTORS, 0) <> 1)
			BEGIN
				SELECT @CONTR_FILTER = @CONTR_FILTER + N', ' + CAST(ID_CONTRACTOR AS NVARCHAR) FROM #CONTRACTORS
				SET @CONTR_FILTER = N'AND ST.ID_CONTRACTOR IN (' + STUFF(@CONTR_FILTER, 1,2,'') + N') '
			END
		--select @CONTR_FILTER
		
		DECLARE @STORE_FILTER NVARCHAR(4000)
		SET @STORE_FILTER = N''
		IF (ISNULL(@ALL_STORES, 0) <> 1)
			BEGIN
				SELECT @STORE_FILTER = @STORE_FILTER + N', ' + CAST(ID_STORE AS NVARCHAR) FROM #STORES
				SET @STORE_FILTER = N'AND ST.ID_STORE IN (' + STUFF(@STORE_FILTER, 1,2,'') + N') '
			END
		--SELECT @STORE_FILTER

		DECLARE @WHERE_FILTER NVARCHAR(4000)
		SET @WHERE_FILTER = N'LM.DATE_OP < @TODAY ' --CHSA
		SET @WHERE_FILTER = @WHERE_FILTER + @CONTR_FILTER + @STORE_FILTER



		SET @QUERY = N'
		SELECT
			GOODS_CODE = G.GOODS_CODE,
			NAME_G = G.GOODS_NAME + '' ('' + G.PRODUCER_NAME + '')'',
			LOT_NAME = G.LOT_NAME,
			--QUANTITY = G.QUANTITY,
			SERIES_G = G.SERIES_NUMBER,
			SERIES_GD = GD.SERIES_NR,
			NAME_GD = GD.DRUG_TXT,
			PRODUCER_GD = GD.MNF_NM,
			ARGUMENT = GD.SPEC_NM,
			LETTER = CASE WHEN GD.LETTER_DATE IS NULL THEN GD.LETTER_NR ELSE ISNULL(GD.LETTER_NR, '''') + '' �� '' + CONVERT(VARCHAR, GD.LETTER_DATE, 104) END
		FROM	
		(SELECT
			GOODS_CODE = G.CODE,
			GOODS_NAME = G.NAME,
			PRODUCER_NAME = PR.NAME,
			LOT_NAME = ST.NAME + ''/'' + L.LOT_NAME,
			--QUANTITY = SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB),
			SERIES_NUMBER = SER.SERIES_NUMBER	
		FROM LOT L
			INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
			INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
			INNER JOIN PRODUCER PR ON PR.ID_PRODUCER = G.ID_PRODUCER
			INNER JOIN STORE ST ON ST.ID_STORE = L.ID_STORE
			INNER JOIN SERIES SER ON SER.ID_SERIES = L.ID_SERIES
		WHERE ' + @WHERE_FILTER +
		'GROUP BY L.ID_LOT_GLOBAL, G.CODE, G.NAME, PR.NAME, ST.NAME + ''/'' + L.LOT_NAME, SER.SERIES_NUMBER
		HAVING SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB) > 0) G
			INNER JOIN GOODS_DEFECT GD ON ' + @JOIN_FILTER + @ORDER_FILTER

		--select @QUERY
	END
		ELSE
			BEGIN
				SET @JOIN_FILTER = CASE WHEN @MNAME = 0 THEN N'' ELSE N'LEFT(G.NAME, ' + CAST(@MNAME AS NVARCHAR) + N') = LEFT(GD.DRUG_TXT, ' + CAST(@MNAME AS NVARCHAR) + N') AND ' END + CASE WHEN @MSER = 0 THEN N'SER.SERIES_NUMBER = GD.SERIES_NR' ELSE N'LEFT(SER.SERIES_NUMBER, ' + CAST(@MSER AS NVARCHAR) + N') = LEFT(GD.SERIES_NR, ' + CAST(@MSER AS NVARCHAR) + N')' END
				--SELECT @JOIN_FILTER

				DECLARE @INVOICE_FILTER NVARCHAR(4000)
				SET @INVOICE_FILTER = N''
				IF (ISNULL(@ALL_INVOICES, 0) <> 1)
				BEGIN
				SELECT @INVOICE_FILTER = @INVOICE_FILTER + N', ' + CAST(ID_INVOICE AS NVARCHAR) FROM #INVOICES
				SET @INVOICE_FILTER = N' AND I.ID_INVOICE IN (' + STUFF(@INVOICE_FILTER, 1,2,'') + N') '
				END
				--SELECT @INVOICE_FILTER

				DECLARE @date_filter NVARCHAR(100)
				set @date_filter = N' AND I.DOCUMENT_DATE BETWEEN  @DATE_FR AND @DATE_TO '
				--select @date_filter



				set @QUERY = N'
				SELECT
					GOODS_CODE = G.CODE,
					NAME_G = G.NAME + '' ('' + PR.NAME + '')'',
					LOT_NAME = ST.NAME + ''/'' + (select lot_name from lot l where l.id_document = i.id_invoice_global and l.id_document_item = ii.id_invoice_item_global),
					SERIES_G = SER.SERIES_NUMBER,
					SERIES_GD = GD.SERIES_NR,
					NAME_GD = GD.DRUG_TXT,
					PRODUCER_GD = GD.MNF_NM,
					ARGUMENT = GD.SPEC_NM,
					LETTER = CASE WHEN GD.LETTER_DATE IS NULL THEN GD.LETTER_NR ELSE ISNULL(GD.LETTER_NR, '''') + '' �� '' + CONVERT(VARCHAR, GD.LETTER_DATE, 104) END
				FROM INVOICE I
					INNER JOIN STORE ST ON ST.ID_STORE = I.ID_STORE
					INNER JOIN INVOICE_ITEM II ON II.ID_INVOICE_GLOBAL = I.ID_INVOICE_GLOBAL	
					INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = I.ID_CONTRACTOR_SUPPLIER
					INNER JOIN GOODS G ON G.ID_GOODS = II.ID_GOODS
					INNER JOIN PRODUCER PR ON PR.ID_PRODUCER = G.ID_PRODUCER
					INNER JOIN SERIES SER ON SER.ID_SERIES = II.ID_SERIES
					INNER JOIN GOODS_DEFECT GD ON ' + @join_filter + 
				' WHERE I.DOCUMENT_STATE = ''PROC''' + CASE WHEN @TYPE = 1 THEN @invoice_filter ELSE @date_filter END + @ORDER_FILTER
				--select @QUERY

			END
IF (@TYPE = 0)
		EXEC SP_EXECUTESQL @QUERY,  N'@TODAY DATETIME', @TODAY=@TODAY
	ELSE
		EXEC SP_EXECUTESQL @QUERY,  N'@DATE_FR DATETIME, @DATE_TO DATETIME', @DATE_FR=@DATE_FR,@DATE_TO=@DATE_TO

RETURN 0
GO


/*
EXEC DBO.REPEX_GOODS_DEFECT_UNION N'
<XML><MNAME>4</MNAME>
<MSER>4</MSER>
<TYPE>2</TYPE>
<ORDER>0</ORDER>
<DATE_FR>2010-04-02T10:42:07.952</DATE_FR>
<DATE_TO>2011-04-15T10:42:07.952</DATE_TO></XML>'
*/

/*
	<ID_CONTRACTOR>5271</ID_CONTRACTOR>
	<ID_CONTRACTOR>5273</ID_CONTRACTOR>
	<ID_STORE>345</ID_STORE>
	<ID_STORE>343</ID_STORE>
	<ID_INVOICE>3498</ID_INVOICE>
	<ID_INVOICE>3492</ID_INVOICE>	
*/