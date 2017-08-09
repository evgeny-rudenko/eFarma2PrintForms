SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
-----------------------------------------------------------------------------------------
IF OBJECT_ID('STATIST_SALE_PROFITABLE_GOODS_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE STATIST_SALE_PROFITABLE_GOODS_EX AS RETURN')
    GRANT EXEC ON [STATIST_SALE_PROFITABLE_GOODS_EX] TO [PUBLIC]
END
GO
ALTER PROCEDURE STATIST_SALE_PROFITABLE_GOODS_EX
    @XMLPARAM NTEXT
AS

DECLARE	@SQL NVARCHAR(4000), @TOP NVARCHAR(4000), @G_ROWCOUNTALL NVARCHAR(256), @DATE DATETIME, @DATE_FROM2 DATETIME
DECLARE	@ALL_GOODS BIT, @ALL_STORE BIT, @ALL_TRADES BIT, @ALL_GROUPS BIT, @G_SUMOUTALL DECIMAL(18, 2), @G_SUMADDALL DECIMAL(18, 2)
DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME, @TYPE_REPORT TINYINT, @PERCENT TINYINT
DECLARE	@ORDER_BY NVARCHAR(16), @ROW_COUNT SMALLINT, @TYPE_OUT TINYINT, @PARTS BIT, @DAY_COUNT INT
DECLARE @USE_GOODS_REPORT_NAME BIT
DECLARE @ORDER NVARCHAR(4000)
DECLARE @DATA TABLE(TABLES_DATA VARCHAR(16))
DECLARE @GOODS TABLE(ID_GOODS BIGINT)
DECLARE @SORT_ORDER NVARCHAR(5)

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
        @ORDER_BY = ORDER_BY,
        @PERCENT = [PERCENT],
        @USE_GOODS_REPORT_NAME = USE_GOODS_REPORT_NAME,
		@SORT_ORDER = SORT_ORDER
	FROM OPENXML(@HDOC , '/XML') WITH(
		DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
        [PERCENT] TINYINT 'PERCENT',
        ORDER_BY NVARCHAR(4000) 'ORDER_BY',
        USE_GOODS_REPORT_NAME BIT 'USE_GOODS_REPORT_NAME',
		SORT_ORDER VARCHAR(5) 'SORT_ORDER'
	)

    SELECT TYPE_NUM INTO #TYPE_OUT FROM OPENXML(@HDOC, '/XML/TYPE_OUT/TYPE_NUM') WITH(
        TYPE_NUM INT '.'
    )
    
	SELECT ID_STORE INTO #STORE FROM OPENXML(@HDOC , '/XML/STORE/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    )
    WHERE ID_STORE != 0

    IF @@ROWCOUNT = 0 SET @ALL_STORE = 1 ELSE SET @ALL_STORE = 0

    SELECT ID_GROUP INTO #GROUPS FROM OPENXML(@HDOC, '/XML/GROUPS/ID_GROUP') WITH(
        ID_GROUP BIGINT '.'
    )
    WHERE ID_GROUP !=0
    IF @@ROWCOUNT = 0 SET @ALL_GROUPS = 1 ELSE SET @ALL_GROUPS = 0    

    SELECT ID_TRADE_NAME INTO #TRADE_NAME FROM OPENXML(@HDOC, '/XML/TRADE_NAME/ID_TRADE_NAME') WITH(
        ID_TRADE_NAME BIGINT '.'
    )
    IF @@ROWCOUNT = 0 SET @ALL_TRADES = 1 ELSE SET @ALL_TRADES = 0
    
	SELECT ID_GOODS INTO #GOODS FROM OPENXML(@HDOC, '/XML/GOODS/ID_GOODS') WITH(
        ID_GOODS BIGINT '.'
    )
    WHERE ID_GOODS != 0
    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0

    INSERT INTO @GOODS(ID_GOODS)
    SELECT 
        ID_GOODS = T.ID_GOODS
    FROM(
        SELECT DISTINCT ID_GOODS
        FROM GOODS G
        WHERE @ALL_GOODS = 0 AND EXISTS(SELECT NULL FROM #GOODS GS WHERE GS.ID_GOODS = G.ID_GOODS)
--        AND EXISTS (SELECT NULL FROM #TRADE_NAME TN WHERE TN.ID_TRADE_NAME = G.ID_TRADE_NAME) 
        AND NOT(@ALL_GOODS = 1 AND @ALL_GROUPS = 0)
    
        UNION        

        SELECT DISTINCT ID_GOODS
        FROM GOODS_2_GROUP G2G
        WHERE EXISTS (SELECT NULL FROM #GROUPS G WHERE G.ID_GROUP = G2G.ID_GOODS_GROUP)
        AND NOT EXISTS(SELECT NULL FROM #GOODS G WHERE G.ID_GOODS = G2G.ID_GOODS)
        AND NOT(@ALL_GOODS = 0 AND @ALL_GROUPS = 1)
--        UNION
        
--         SELECT DISTINCT G.ID_GOODS
--         FROM GOODS G
--         WHERE EXISTS(SELECT NULL FROM #TRADE_NAME TN WHERE TN.ID_TRADE_NAME = G.ID_TRADE_NAME)
--         AND NOT EXISTS(SELECT NULL FROM @GOODS G2G WHERE G.ID_GOODS = G2G.ID_GOODS)

    )T			

    IF @@ROWCOUNT = 0 SET @ALL_GOODS = 1 ELSE SET @ALL_GOODS = 0
	EXEC SP_XML_REMOVEDOCUMENT @HDOC
			
	EXEC USP_RANGE_DAYS @DATE_FROM OUTPUT , @DATE_TO OUTPUT
	SET @DAY_COUNT = DATEDIFF(DAY , @DATE_FROM , @DATE_TO) + 1
	SET @DAY_COUNT = CASE WHEN @DAY_COUNT < 1 THEN 1 ELSE @DAY_COUNT END

    INSERT INTO @DATA
    SELECT
        TABLES_DATA = CASE TYPE_NUM 
                            WHEN 1 THEN 'CHEQUE'
                            WHEN 2 THEN 'INVOICE_OUT'
                            WHEN 3 THEN 'MOVE' END
    FROM #TYPE_OUT

	CREATE TABLE #TABLE_DATA(
		G_ID BIGINT NULL,
--		ID_LOT_MOVEMENT BIGINT NULL,
		G_RUSNAME NVARCHAR(256) NULL,
		G_CODE NVARCHAR(16) NULL,
		G_SUPPLIER NVARCHAR(256) NULL,
		G_QTYSALE DECIMAL(18, 4) NULL,
		G_QTYRETURN DECIMAL(18, 4) NULL,
		G_QTYOUT DECIMAL(18, 4) NULL,
		PRICE_SAL DECIMAL(18, 4) NULL,
		G_SUMOUT DECIMAL(18, 4) NULL,
		G_SUMDISCOUNT DECIMAL(18, 4) NULL,
		G_SUMADD DECIMAL(18, 4) NULL,
		G_PERCENTADDSUM DECIMAL(18, 4) NULL,
		G_PERCENTADD DECIMAL(18, 4) NULL,
		G_PERCENTSUMOUT DECIMAL(18, 4) NULL
--		code_op  NVARCHAR(256) NULL
	)


	CREATE TABLE #TABLE_DATA_SORT(
        id_num bigint identity(1,1) not null,
		G_ID BIGINT NULL,
--		ID_LOT_MOVEMENT BIGINT NULL,
		G_RUSNAME NVARCHAR(256) NULL,
		G_CODE NVARCHAR(16) NULL,
		G_SUPPLIER NVARCHAR(256) NULL,
		G_QTYSALE DECIMAL(18, 4) NULL,
		G_QTYRETURN DECIMAL(18, 4) NULL,
		G_QTYOUT DECIMAL(18, 4) NULL,
		PRICE_SAL DECIMAL(18, 4) NULL,
		G_SUMOUT DECIMAL(18, 4) NULL,
		G_SUMDISCOUNT DECIMAL(18, 4) NULL,
		G_SUMADD DECIMAL(18, 4) NULL,
		G_PERCENTADDSUM DECIMAL(18, 4) NULL,
		G_PERCENTADD DECIMAL(18, 4) NULL,
		G_PERCENTSUMOUT DECIMAL(18, 4) NULL,
        G_PERCENT DECIMAL(18,4) NULL
	)

	INSERT INTO #TABLE_DATA
	SELECT 
		G_ID = G.ID_GOODS,
		G_RUSNAME = G.NAME,					 --������������ ������		
		G_CODE = max(G.CODE),
		G_SUPPLIER = C.NAME,			--�������� ����������
        --���-�� ������:
		G_QTYSALE = SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)* SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)), 	
        --���-�� ������ ��������:
		G_QTYRETURN = SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)), 
        --����� ������ ��.
 		G_QTYOUT = SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)) - 
                        SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),
		PRICE_SAL = MAX(B.PRICE_SAL), --������� ���� ������ �� ������
        --����� �������:
        G_SUMOUT = SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB * L.PRICE_SAL ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)) - 
                        SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD * L.PRICE_SAL ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)),  
        --������
 		G_SUMDISCOUNT = SUM(CASE WHEN LM.QUANTITY_SUB<0 THEN -1 ELSE 1 END * LM.DISCOUNT_ACC),	
        --�����
        G_SUMADD = SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB * L.PRICE_SAL ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)) - 
                        SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD * L.PRICE_SAL ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR))-
                   (SUM((CASE WHEN LM.QUANTITY_SUB>0 THEN LM.QUANTITY_SUB * L.PRICE_SUP ELSE 0 END)*SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)) - 
                        SUM((CASE WHEN LM.CODE_OP='ACT_R2B' AND LM.QUANTITY_ADD>0 THEN LM.QUANTITY_ADD * L.PRICE_SUP ELSE 0 END) * SR.NUMERATOR / CONVERT(MONEY, SR.DENOMINATOR)))-
                   SUM(LM.DISCOUNT_ACC),        
        G_PERCENTADDSUM = NULL,
 		G_PERCENTADD = NULL,		--%�� ������ ������ �� ��������
 		G_PERCENTSUMOUT = NULL 	--%�� ����� ����������
	FROM LOT_MOVEMENT LM  
	INNER JOIN LOT L ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
--    INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = LM.ID_DOCUMENT
    INNER JOIN (SELECT ID_DOCUMENT,ID_STORE_FROM = MAX(ID_STORE_FROM),ID_STORE_TO=MAX(ID_STORE_TO) FROM DOC_MOVEMENT GROUP BY ID_DOCUMENT) DM ON DM.ID_DOCUMENT = LM.ID_DOCUMENT
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = L.ID_SUPPLIER
	INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
	INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
	LEFT JOIN (SELECT -- ���� ���������� �������
                L.ID_GOODS,
                L.ID_LOT_GLOBAL,
                LM.DATE_OP,
                L.PRICE_SAL,
				L.PRICE_SUP
            FROM LOT L
            INNER JOIN LOT_MOVEMENT LM ON LM.ID_LOT_GLOBAL = L.ID_LOT_GLOBAL
                                      AND LM.ID_DOCUMENT = L.ID_DOCUMENT
                                      AND LM.ID_DOCUMENT_ITEM = L.ID_DOCUMENT_ITEM

            WHERE LM.CODE_OP IN ('INVOICE','IMPORT_REMAINS')--NOT IN ('ACT_REV', 'ACT_DIS')
			AND (SELECT 
				     COUNT(*)
				 FROM LOT L1
				 INNER JOIN LOT_MOVEMENT LM1 ON LM1.ID_LOT_GLOBAL = L1.ID_LOT_GLOBAL
				       	                    AND LM1.ID_DOCUMENT = L1.ID_DOCUMENT
						                    AND LM1.ID_DOCUMENT_ITEM = L1.ID_DOCUMENT_ITEM
			     WHERE L1.ID_GOODS = L.ID_GOODS
	   			     AND (LM1.DATE_OP>LM.DATE_OP
				     OR (LM1.DATE_OP=LM.DATE_OP AND LM1.ID_LOT_MOVEMENT >= LM.ID_LOT_MOVEMENT))
					 AND LM1.DATE_OP < = @DATE_TO AND LM1.CODE_OP IN ('INVOICE','IMPORT_REMAINS')
				 GROUP BY L1.ID_GOODS)=1
        ) B ON B.ID_GOODS = G.ID_GOODS
	WHERE (LM.DATE_OP BETWEEN @DATE_FROM AND @DATE_TO)
--    AND (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = AD.ID_STORE1 OR S.ID_STORE = AD.ID_STORE2))
--    AND (@ALL_STORE = 1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = DM.ID_STORE_FROM OR S.ID_STORE = DM.ID_STORE_TO))
    AND (@all_store=1 OR EXISTS (SELECT NULL FROM #STORE S WHERE S.ID_STORE = L.ID_STORE))
	AND (@ALL_GOODS = 1 OR EXISTS (SELECT NULL FROM @GOODS where G.ID_GOODS = ID_GOODS))    
    AND (@ALL_TRADES = 1 OR EXISTS (SELECT NULL FROM #TRADE_NAME WHERE ID_TRADE_NAME = G.ID_TRADE_NAME))
	AND (EXISTS (SELECT NULL FROM @DATA WHERE TABLES_DATA = LM.CODE_OP)OR LM.CODE_OP IN ('ACT_R2B','invoice','IMPORT_REMAINS','INVENTORY_SVED')) --LM.CODE_OP IN (SELECT TABLES_DATA FROM @DATA)--('INVOICE_OUT','CHEQUE','ACT_R2B')
	GROUP BY G.ID_GOODS, G.NAME, C.NAME

	DECLARE @ALLROWCOUNT INT
	SET @ALLROWCOUNT = @@ROWCOUNT

	SELECT G_ROWCOUNTALL = COUNT(*) FROM #TABLE_DATA  --���.������������������ �������

 	UPDATE #TABLE_DATA 
	SET
		G_PERCENTADD = CASE WHEN SUMM.G_SUMADDALL!=0 THEN G_SUMADD * 100 / SUMM.G_SUMADDALL ELSE 0 END,
		G_PERCENTSUMOUT = CASE WHEN G_QTYSALE!=0 THEN G_SUMOUT * 100 / SUMM.G_SUMOUTALL ELSE 0 END
	FROM (SELECT G_SUMADDALL = SUM(G_SUMADD),G_SUMOUTALL = SUM(G_SUMOUT) FROM #TABLE_DATA WHERE G_QTYSALE!=0) SUMM
    

   	SET @SQL = 'INSERT INTO #TABLE_DATA_SORT(G_ID,G_RUSNAME,G_CODE,G_SUPPLIER,G_QTYSALE,G_QTYRETURN,G_QTYOUT,PRICE_SAL,G_SUMOUT,G_SUMDISCOUNT,G_SUMADD,G_PERCENTADDSUM,G_PERCENTADD,G_PERCENTSUMOUT) SELECT G_ID,G_RUSNAME,G_CODE,G_SUPPLIER,G_QTYSALE,G_QTYRETURN,G_QTYOUT,PRICE_SAL,G_SUMOUT,G_SUMDISCOUNT,G_SUMADD,G_PERCENTADDSUM,G_PERCENTADD,G_PERCENTSUMOUT FROM #TABLE_DATA ORDER BY '+ @ORDER_BY + ' DESC'--@SORT_ORDER
   	EXEC(@SQL)

IF (@ORDER_BY!='G_RUSNAME' AND @ORDER_BY!='G_SUPPLIER')
BEGIN
--��� ������� � ����������:���������� �������� ��� ����,�� �������� ����� �������������� ������
    IF (@ORDER_BY!='G_PERCENTADD' AND @ORDER_BY!='G_PERCENTSUMOUT' ) 
    BEGIN
        SET @SQL ='UPDATE #TABLE_DATA_SORT
            SET
                G_PERCENT = CASE WHEN G_SUMPERCENT!=0 THEN CONVERT(DECIMAL(18,4),'+CAST(@ORDER_BY AS VARCHAR)+')*100/ SUMM.G_SUMPERCENT ELSE 0 END
            FROM (SELECT G_SUMPERCENT = SUM(CONVERT(DECIMAL(18,4),'+CAST(@ORDER_BY AS VARCHAR)+')) FROM #TABLE_DATA_SORT) SUMM'
        EXEC (@SQL)
    END
    ELSE BEGIN
        UPDATE #TABLE_DATA_SORT
        SET 
            G_PERCENT = CASE @ORDER_BY WHEN 'G_PERCENTADD' THEN G_PERCENTADD ELSE G_PERCENTSUMOUT END
        FROM #TABLE_DATA_SORT
    END


--������
	DECLARE @PR_PERCENT DECIMAL(18, 4),
			@CUR_PERCENT DECIMAL(18, 4),
			@ROWCOUNT INT
	SET @PR_PERCENT = 0
	SET @CUR_PERCENT = 0
	SET @ROWCOUNT = 0

	DECLARE ALL_GOODS CURSOR FOR 
	SELECT G_PERCENT FROM #TABLE_DATA_SORT order by id_num
	
	OPEN ALL_GOODS
	FETCH NEXT FROM ALL_GOODS INTO @CUR_PERCENT
	WHILE (@@FETCH_STATUS=0 AND @PR_PERCENT<@PERCENT)
	BEGIN
		SET @PR_PERCENT = @PR_PERCENT + @CUR_PERCENT
		SET @ROWCOUNT = @ROWCOUNT + 1
		FETCH NEXT FROM ALL_GOODS INTO @CUR_PERCENT		
	END
	CLOSE ALL_GOODS
	DEALLOCATE ALL_GOODS
-- 
--select * FROM #TABLE_DATA_SORT TD ORDER BY id_num DESC

    SET @ALLROWCOUNT = @ALLROWCOUNT-@ROWCOUNT 

    SET @SQL = 'DELETE FROM #TABLE_DATA_SORT WHERE ID_NUM IN (SELECT TOP '+ CAST(@ALLROWCOUNT AS VARCHAR) +' ID_NUM FROM #TABLE_DATA_SORT TD ORDER BY '+ @ORDER_BY+ ' ASC)'
 	EXEC(@SQL)
END

--����������� ������ �� �������
	IF (@USE_GOODS_REPORT_NAME = 1)
	BEGIN

	    UPDATE #TABLE_DATA_SORT SET
	        G_RUSNAME = ISNULL(GC.NAME, G_RUSNAME),
	        G_CODE = G.CODE
	    FROM GOODS G 
 	    LEFT JOIN GOODS_CLASSIFIER_2_GOODS GC2G ON GC2G.ID_GOODS = G.ID_GOODS_GLOBAL
	    LEFT JOIN GOODS_CLASSIFIER GC ON GC.ID_GOODS_CLASSIFIER = GC2G.ID_GOODS_CLASSIFIER
	    WHERE G.ID_GOODS = #TABLE_DATA_SORT.G_ID

		SELECT 
			G_ID = MAX(TD.G_ID),
			G_RUSNAME,
			G_CODE = max(G_CODE),
			G_SUPPLIER,
			G_QTYSALE = SUM(TD.G_QTYSALE),
			G_QTYRETURN = SUM(TD.G_QTYRETURN),
			G_QTYOUT = SUM(TD.G_QTYOUT),
			PRICE_SAL = SUM(TD.PRICE_SAL),
			G_SUMOUT = SUM(TD.G_SUMOUT),
			G_SUMDISCOUNT = SUM(TD.G_SUMDISCOUNT),
			G_SUMADD = SUM(TD.G_SUMADD),
			G_PERCENTADD = SUM(TD.G_PERCENTADD),
			G_PERCENTSUMOUT = SUM(TD.G_PERCENTSUMOUT)
		INTO #TABLE_DATA_GROUP
		FROM #TABLE_DATA_SORT TD 
		GROUP BY G_RUSNAME, G_SUPPLIER 
	END

 	SET @SQL = 'SELECT '+ CASE WHEN @USE_GOODS_REPORT_NAME = 0 THEN ' * FROM #TABLE_DATA_SORT ORDER BY ' 
 												  ELSE ' * FROM #TABLE_DATA_GROUP ORDER BY ' END + @ORDER_BY + ' ' + @SORT_ORDER

 	EXEC(@SQL)
--     
	SELECT 
		G_SUMADDALL = SUM(G_SUMADD),
		G_SUMOUTALL = SUM(G_SUMOUT)
	FROM #TABLE_DATA_SORT

RETURN 0
GO

/*
exec STATIST_SALE_PROFITABLE_GOODS_EX @xmlParam = N'
<XML>
	<DATE_FROM>2009-08-01T00:00:00.000</DATE_FROM>
	<DATE_TO>2009-08-21T00:00:00.000</DATE_TO>
	<ORDER_BY>G_SUMOUT</ORDER_BY>
	<PERCENT>80</PERCENT>
	<TYPE_OUT>
		<TYPE_NUM>1</TYPE_NUM>
		<TYPE_NUM>2</TYPE_NUM>
		<TYPE_NUM>3</TYPE_NUM>
	</TYPE_OUT>
	<USE_GOODS_REPORT_NAME>0</USE_GOODS_REPORT_NAME>
	<SORT_ORDER>DESC</SORT_ORDER>
</XML>'*/