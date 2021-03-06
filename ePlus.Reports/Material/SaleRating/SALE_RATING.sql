SET NOCOUNT 			ON
SET QUOTED_IDENTIFIER 	OFF
GO
IF OBJECT_ID('DBO.REPEX_SALE_RATING') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REPEX_SALE_RATING AS RETURN')
    GRANT EXEC ON DBO.REPEX_SALE_RATING TO PUBLIC
END
GO

ALTER PROCEDURE DBO.REPEX_SALE_RATING
    @XMLPARAM NTEXT
AS
DECLARE @SQL NVARCHAR(4000), @QUERY_WHERE VARCHAR(400), @QUERY NVARCHAR(4000)
DECLARE @SUM_SALE MONEY,
		 @SUM_PROFIT MONEY, 
		 @QUANTITY_DOC_SALE MONEY,
		 @ALL_KIND BIT,
		 @GOODS_MOVING BIT,
		 @INVOICE_OUT BIT, 
		 @SALE_KKM BIT
DECLARE @NAME VARCHAR(512), @PERCENT MONEY
DECLARE	 
		@GROUP_A TINYINT,
		@GROUP_B TINYINT,
		@GROUP_C TINYINT,
		@GROUP MONEY,
		@TYPE_REPORT TINYINT,
		@ALL_GOODS BIT, 
		@ALL_STORE BIT,
		@NOAU BIT, 
		@HDOC INT, 
		@DATE_FR DATETIME, 
		@DATE_TO DATETIME
SET QUOTED_IDENTIFIER ON 

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	
	SELECT TOP 1
        @DATE_FR 	= DATE_FR,
        @DATE_TO 	= DATE_TO,
        @GROUP_A		= GROUPA,
		@GROUP_B		= GROUPB,
		@GROUP_C		= GROUPC,
		@TYPE_REPORT	= TYPE_REPORT,
		@ALL_KIND	= ALL_KIND,
		@GOODS_MOVING	= GOODS_MOVING,
		@INVOICE_OUT	= INVOICE_OUT,
		@SALE_KKM	 = SALE_KKM,
		@NOAU = NOAU
	FROM OPENXML(@HDOC , '/XML') WITH
	(
		DATE_FR	DATETIME 'DATE_FR',
        DATE_TO		DATETIME 'DATE_TO',
		GROUPA		TINYINT 'GROUP_A',
		GROUPB		TINYINT 'GROUP_B',
		GROUPC		TINYINT 'GROUP_C',
		TYPE_REPORT		TINYINT 'TYPE_REPORT',
		ALL_KIND BIT 'ALL_KIND',
		GOODS_MOVING BIT 'GOODS_MOVING',
		INVOICE_OUT BIT 'INVOICE_OUT',
		SALE_KKM BIT 'SALE_KKM',
		NOAU BIT 'NOAU'
	)
SET @NOAU = 1
SET @QUERY_WHERE=''
IF (@ALL_KIND=1) SET @QUERY_WHERE ='19,21,8,37,38,'-- 19 - CHEQUE, 21- INVOICE_OUT, 12 - ACT_R2B, 8,37,38 - MOVE
	ELSE 
		BEGIN
			IF (@GOODS_MOVING=1)
				SET @QUERY_WHERE = @QUERY_WHERE +'8,37,38,'
			IF (@INVOICE_OUT=1)
				SET @QUERY_WHERE = @QUERY_WHERE +'21,'
			IF (@SALE_KKM=1)
				SET @QUERY_WHERE = @QUERY_WHERE +'19,'
		END 
SET @QUERY_WHERE = LEFT(@QUERY_WHERE, LEN(@QUERY_WHERE)-1)

--SELECT @QUERY_WHERE
SET @GROUP = 0
SELECT ID_STORE = ID_STORE INTO #STORE FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(ID_STORE BIGINT '.') 
--INNER JOIN STORE S ON S.ID_STORE = TAB.ID_STORE
IF @@ROWCOUNT = 0 
	SET @ALL_STORE = 1

SELECT * INTO #GOODS FROM OPENXML(@HDOC, '/XML/ID_GOODS') WITH(ID_GOODS INT '.')
IF (@@ROWCOUNT = 0)	SET @ALL_GOODS = 1


--���� id ��� �����������,������� ����� ����������� ��� @IS_FILTERED=1  
--DECLARE @ID_DOC_MOVEMENT TABLE(ID_DOCUMENT UNIQUEIDENTIFIER)  
IF OBJECT_ID('TEMPDB..#ID_DOC_MOVEMENT') IS NOT NULL DROP TABLE #ID_DOC_MOVEMENT
CREATE TABLE #ID_DOC_MOVEMENT
(ID_DOCUMENT UNIQUEIDENTIFIER)
--Select @NOAU
if (@NOAU=1)  
BEGIN      
    insert into #ID_DOC_MOVEMENT
    select distinct  
        f.id_document    
    from (select   
            id_document,  
            id_store_fr=id_store  
          from doc_movement  Dm1
          where code_op='sub' and id_table in (8,37,38,39)) f  
    inner join (select   
                    id_document,  
                    id_store_to=id_store  
                    --select *
                from doc_movement Dm2 
                where code_op='add' and id_table in (8,37,38,39) 
               -- AND Dm2.DATE_OP BETWEEN @DATE_FR AND @DATE_TO
                ) w on w.id_document = f.id_document  
               
    where not exists(select null   
                     from #STORE s1, #STORE s2   
                     where (s1.id_store = f.id_store_fr and s2.id_store = w.id_store_to)or(s1.id_store = w.id_store_to and s2.id_store = f.id_store_fr))  
END  
--  select * from #ID_DOC_MOVEMENT 


EXEC SP_XML_REMOVEDOCUMENT @HDOC
EXEC USP_RANGE_DAYS @DATE_FR OUTPUT , @DATE_TO OUTPUT
--select @DATE_FR, @DATE_TO
---------------------------------------------------------------------------

IF OBJECT_ID('TEMPDB..#TEMP_LM') IS NOT NULL DROP TABLE #TEMP_LM
CREATE TABLE #TEMP_LM
(
	ID_LOT_GLOBAL UNIQUEIDENTIFIER,
	SUM_ACC MONEY, 
	SUM_SUP MONEY,
	QUANTITY_SUB MONEY,
	Q_DOC_SUB  INT,
	Q_DOC_RETURN INT
)

IF OBJECT_ID('TEMPDB..#TEMP') IS NOT NULL DROP TABLE #TEMP
CREATE TABLE #TEMP
(
	NAME VARCHAR(512),
	QTY_SALE MONEY, 
	QTY_REM MONEY,
	QTY_DOC_SALE MONEY,
	SUM_SALE MONEY,
	SUM_PROFIT MONEY,
	PERC MONEY,
	ABCGROUP VARCHAR(10)
)
---------------------------------------------------------------------------
--select @QUERY_WHERE
SET @QUERY = ' INSERT INTO #TEMP_LM SELECT 
					lm.ID_LOT_GLOBAL, 
					SUM_ACC = SUM(CASE WHEN LM.QUANTITY_SUB > 0 THEN 1 ELSE -1 END * SUM_ACC), 
					SUM_SUP = SUM(CASE WHEN LM.QUANTITY_SUB > 0 THEN 1 ELSE -1 END * SUM_SUP), 
					QUANTITY_SUB = SUM(LM.QUANTITY_SUB * SR.NUMERATOR / SR.DENOMINATOR),
					Q_DOC_SUB =SUM(CASE WHEN LM.OP = ''SUB'' THEN 1 ELSE 0 END),
					Q_DOC_RETURN =SUM(CASE WHEN LM.OP = ''RETURN'' THEN 1 ELSE 0 END)
			FROM LOT_MOVEMENT lm
			INNER JOIN LOT L ON L.ID_LOT_GLOBAL = lm.ID_LOT_GLOBAL
			INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
		WHERE  lm.ID_TABLE IN (@QUERY_WHERE) 
			--	AND  LM.QUANTITY_SUB > 0
				AND DATE_OP BETWEEN @DATE_FR AND @DATE_TO
					and (@NOAU =0 or   
                           (@NOAU =1 and   
                           not exists(select id_document from #id_doc_movement idm where idm.id_document = lm.id_document)))  
			GROUP BY LM.ID_LOT_GLOBAL '
set @QUERY =REPLACE(@QUERY,'@QUERY_WHERE',@QUERY_WHERE)
EXEC SP_EXECUTESQL @QUERY, N' @DATE_FR DATETIME, @DATE_TO DATETIME, @NOAU BIT',  @DATE_FR  = @DATE_FR, @DATE_TO = @DATE_TO, @NOAU = @NOAU
--select * from #TEMP_LM 
-------------------------------------------------------------------------		
SELECT 
	G.NAME, 
	SUM_SALE = SUM(LM.SUM_ACC),
	SUM_PROFIT = SUM(LM.SUM_ACC - LM.SUM_SUP),
	PRICE_SALE = SUM(L.PRICE_SAL),
	QTY_DOC_SALE = SUM(LM.Q_DOC_SUB-LM.Q_DOC_RETURN),
	QTY_SALE = SUM(LM.QUANTITY_SUB),
	QTY_REM = SUM(L.QUANTITY_REM * SR.NUMERATOR / SR.DENOMINATOR)
INTO #TEMP1
FROM GOODS G
INNER JOIN LOT L ON G.ID_GOODS = L.ID_GOODS
LEFT JOIN ( 
			select *
			from  #TEMP_LM
			) LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
INNER JOIN SCALING_RATIO SR ON SR.ID_SCALING_RATIO = L.ID_SCALING_RATIO
--INNER JOIN ALL_DOCUMENT AD ON AD.ID_DOCUMENT_GLOBAL = L.ID_DOCUMENT
WHERE G.DATE_EXCLUDED IS NULL 
	AND (@ALL_STORE = 1 OR L.ID_STORE IN(SELECT ID_STORE FROM #STORE))
	AND (@ALL_GOODS = 1 OR G.ID_GOODS IN(SELECT ID_GOODS FROM #GOODS))
	--AND (@NOAU = 1 OR (AD.ID_TABLE NOT IN (8, 37, 39)) OR AD.ID_STORE_TO NOT IN (SELECT ID_STORE FROM #STORES_EX1))

                                            
GROUP BY G.NAME
--select * from #TEMP1
SELECT @SUM_SALE = SUM(SUM_SALE), @SUM_PROFIT = SUM(SUM_PROFIT),@QUANTITY_DOC_SALE=SUM(QTY_DOC_SALE)  
FROM #TEMP1
WHERE QTY_SALE <> 0
--select  @SUM_SALE , @SUM_PROFIT,@QUANTITY_DOC_SALE
--return
SET @SQL =
'INSERT INTO #TEMP
SELECT 
	NAME,
	QTY_SALE,
	QTY_REM,
	QTY_DOC_SALE,
	SUM_SALE,
	SUM_PROFIT,
	/*PERCENT*/,
	''''
FROM #TEMP1
WHERE QTY_SALE <> 0
ORDER BY /*PERCENT*/ DESC'

IF @TYPE_REPORT = 0 BEGIN --�� ������������
SET @SQL = REPLACE(@SQL,'/*PERCENT*/', 'SUM_PROFIT * 100 /@SUM_PROFIT ')
END
ELSE
IF @TYPE_REPORT = 1 BEGIN 
SET @SQL = REPLACE(@SQL,'/*PERCENT*/', 'CAST(QTY_DOC_SALE AS MONEY) * 100 /@QUANTITY_DOC_SALE')
END
ELSE BEGIN 
SET @SQL = REPLACE(@SQL,'/*PERCENT*/', 'SUM_SALE * 100 /@SUM_SALE')
END

EXEC SP_EXECUTESQL @SQL, N'@SUM_SALE MONEY, @SUM_PROFIT MONEY, @QUANTITY_DOC_SALE INT', @SUM_SALE = @SUM_SALE, @SUM_PROFIT = @SUM_PROFIT, @QUANTITY_DOC_SALE = @QUANTITY_DOC_SALE
--return
--select * from #TEMP
--select @GROUP
DECLARE ABC_CUR CURSOR FOR SELECT NAME, PERC FROM #TEMP
OPEN ABC_CUR
FETCH NEXT FROM ABC_CUR INTO @NAME, @PERCENT
WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @GROUP = @GROUP + @PERCENT
		---select @GROUP
		UPDATE #TEMP SET ABCGROUP = 
			CASE WHEN @GROUP < @GROUP_A THEN 'A' WHEN @GROUP BETWEEN @GROUP_A AND @GROUP_A + @GROUP_B THEN 'B' ELSE 'C' END
		WHERE #TEMP.NAME = @NAME
		FETCH NEXT FROM ABC_CUR INTO @NAME, @PERCENT
	END
CLOSE ABC_CUR
DEALLOCATE ABC_CUR

SELECT 	NAME,
	QTY_SALE, 
	--QTY_REM,
	QTY_DOC_SALE,
	SUM_SALE,
	SUM_PROFIT,
	PERC,
	ABCGROUP 
FROM #TEMP
--where SUM_SALE<>0 and SUM_PROFIT<>0
/* 
where name like '%����%'
 order by name
*/
 
RETURN 0
GO
/*
exec REPEX_SALE_RATING N'
<XML>
<DATE_FR>2010-08-18T10:13:30.781</DATE_FR>
<DATE_TO>2010-08-18T10:13:30.781</DATE_TO>
<GROUP_A>20</GROUP_A>
<GROUP_B>35</GROUP_B>
<GROUP_C>45</GROUP_C>
<TYPE_REPORT>1</TYPE_REPORT>
<ALL_KIND>0</ALL_KIND>
<GOODS_MOVING>1</GOODS_MOVING>
<INVOICE_OUT>0</INVOICE_OUT>
<SALE_KKM>1</SALE_KKM>
<NOAU>0</NOAU>
</XML>'
*/
