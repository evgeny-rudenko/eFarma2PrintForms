-- Еженедельный отчет для аптечной сети "На Здоровье"
/*
 Колонки:
  
1) Запасы товара на начало недели
2) Приход (без переоценки) - сумма (3) - (6)
3) Приход от поставщика (медикаменты)
4) Приход со склада (медикаменты)
5) Приход со склада (ортопедия)
6) Излишки по ревизии 
7) Переоценка (новая сумма)
8) Выручка (чистая, т.е. с учетом скидки)
9) Возврат (без переоценки)
10) Возврат на склад (брак)
11) Возврат в другую аптеку
12) Недостача по ревизии
13) Переоценка (старая сумма)
14) Скидка
15) Средний чек
16) Проходимость
17) Итог разукомплектации (разукомплектация Приход - разукомплектация Расход)
18) Запасы товара на конец недели
19) Название аптеки (если по всем аптекам, то = "По всем аптекам")
20) Название показателя
21) Приход от поставщика (ортопедия)
22) Средняя выручка в сутки за период
23) Средний запас на конец периода
24) Время обращения запасов в днях
25) Ожидаемая выручка 


A - выручка (нал + безнал)
B - выручка (только наличка)
C - выручка (только безнал)
D - ночная выручка (в ночное время)
E - ортопедия 
Z - общий итог по всем аптекам (без разбивки по аптекам)

*/
set nocount on
IF OBJECT_ID('DBO.FN_GOODS_IS_ORTO') IS NOT NULL
	DROP FUNCTION DBO.FN_GOODS_IS_ORTO
GO
CREATE FUNCTION DBO.FN_GOODS_IS_ORTO (@ID_GOODS BIGINT)
-- Функция определяет, принадлежит ли товар к ортопедии
RETURNS BIT
AS
BEGIN
	DECLARE @RES BIT
	SET @RES = 0
	
	IF EXISTS (SELECT *
			   FROM GOODS_2_GROUP GG
			   WHERE GG.ID_GOODS = @ID_GOODS AND GG.ID_GOODS_GROUP IN (324, 326)) BEGIN
		SET @RES = 1
	END
    
    RETURN @RES
END
GO
GRANT EXEC ON DBO.FN_GOODS_IS_ORTO TO PUBLIC
GO
----------------------------------------------------------------------

IF OBJECT_ID('DBO.FN_APTEKA_IS_NIGHT') IS NOT NULL
	DROP FUNCTION DBO.FN_APTEKA_IS_NIGHT
GO
CREATE FUNCTION DBO.FN_APTEKA_IS_NIGHT (@ID_CONTRACTOR BIGINT)
-- Функция определяет, является ли аптека круглосуточной
RETURNS BIT
AS
BEGIN
	DECLARE @RES BIT
	SET @RES = 0
	
	IF EXISTS (select *
			   from ATTRIBUTE_VALUES av
			      join ATTRIBUTE a on av.ID_ATTRIBUTE = a.ID_ATTRIBUTE and a.NAME = 'IS_NIGHT' -- атрибут IS_NIGHT
				  join CONTRACTOR c on av.ID_OBJECT = c.ID_CONTRACTOR_GLOBAL
			   where C.ID_CONTRACTOR = @ID_CONTRACTOR AND av.VALUE = 1) BEGIN

		SET @RES = 1
	END
    
    RETURN @RES
END
GO
GRANT EXEC ON DBO.FN_APTEKA_IS_NIGHT TO PUBLIC
GO
-----------------------------------------------------------------------
IF OBJECT_ID('DBO.FN_TIME_IS_NIGHT') IS NOT NULL
	DROP FUNCTION DBO.FN_TIME_IS_NIGHT
GO
CREATE FUNCTION DBO.FN_TIME_IS_NIGHT (@DATE DATETIME)
-- Функция определяет, является ли момент времени, заданный во входном параметре, ночным временем
-- (ночное время - это время с 20:00 до 07:59:59)
RETURNS BIT
AS
BEGIN
	DECLARE @RES BIT
	SET @RES = 0
	
	IF (DATEPART(HOUR, @DATE) >= 20) OR (DATEPART(HOUR, @DATE) < 8) BEGIN
		SET @RES = 1
	END
    
    RETURN @RES
END
GO
GRANT EXEC ON DBO.FN_TIME_IS_NIGHT TO PUBLIC
GO

IF (OBJECT_ID('DBO.REPEX_WEEKLY_REPORT_NZ') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_WEEKLY_REPORT_NZ AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_WEEKLY_REPORT_NZ
	@XMLPARAM NTEXT
AS

	DECLARE @HDOC INT

	DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME
	DECLARE @ALL_CONTRACTOR BIT

	EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM OUT
	SELECT 
		@DATE_FR = DATE_FR
		, @DATE_TO = DATE_TO
	FROM OPENXML(@HDOC, '/XML') WITH(
		DATE_FR DATETIME 'DATE_FR'
		, DATE_TO DATETIME 'DATE_FR'
	)

	SELECT ID_CONTRACTOR, NAME = CAST(NULL AS VARCHAR(150)) INTO #CONTRACTOR FROM OPENXML(@HDOC, '/XML/ID_CONTRACTOR') WITH(ID_CONTRACTOR BIGINT '.')
	SET @ALL_CONTRACTOR = 1 - SIGN(@@ROWCOUNT)
	
	/*
	UPDATE #CONTRACTOR
	SET NAME = (SELECT TOP 1 C.NAME FROM CONTRACTOR C WHERE C.ID_CONTRACTOR = #CONTRACTOR.ID_CONTRACTOR)
	*/
	EXEC SP_XML_REMOVEDOCUMENT @HDOC

	EXEC USP_RANGE_DAYS @DATE_FR OUT, @DATE_TO OUT
	EXEC USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT

	DECLARE @COUNT_DAY_PERIOD INT -- КОЛИЧЕСТВО ЦЕЛЫХ ДНЕЙ В ПЕРИОДЕ C @DATE_FR ПО (@DATE_TO + 1 СЕКУНДА)
	SET @COUNT_DAY_PERIOD = DATEDIFF(DAY, @DATE_FR, @DATE_TO) + 1
	--SELECT @COUNT_DAY_PERIOD

	DECLARE @MES1 INT
	DECLARE @MES2 INT
	DECLARE @PRINT_PROGNOZ BIT

	DECLARE @COUNT_DAY_OF_MONTH INT

	SET @MES1 = MONTH(@DATE_FR)
	SET @MES2 = MONTH(@DATE_TO)
	IF @MES1 = @MES2 BEGIN 
	   SET @PRINT_PROGNOZ = 1
	   SET @COUNT_DAY_OF_MONTH = DAY(DATEADD(MONTH, 1, @DATE_FR) - DAY(DATEADD(MONTH, 1, @DATE_FR)))
	END
	ELSE BEGIN
	   SET @PRINT_PROGNOZ = 0    
	END   
	--SELECT @PRINT_PROGNOZ
	--SELECT @COUNT_DAY_OF_MONTH

	---------------------------------------------------------------------
	-- ОБЩИЕ ТАБЛИЦЫ

	---- #ORTO - ХРАНЯТСЯ ID_GOODS ТОВАРОВ, ОТНОСЯЩИХСЯ К ОРТОПЕДИИ

	--IF OBJECT_ID('TEMPDB..#ORTO') IS NOT NULL DROP TABLE #ORTO
	--CREATE TABLE #ORTO (ID_GOODS BIGINT)

	--INSERT INTO #ORTO (ID_GOODS)
	--SELECT DISTINCT GG.ID_GOODS
	--FROM GOODS_2_GROUP GG
	--WHERE GG.ID_GOODS_GROUP IN (324, 326)
	--   AND ISNULL(GG.DATE_DELETED, '') = ''
	--order by gg.ID_GOODS   
	----select * from #orto


	-- таблица, в которую будем вставлять данные (только в разрезе аптек)
	IF OBJECT_ID('TEMPDB..#T') IS NOT NULL DROP TABLE #T
	CREATE TABLE #T (
	F1 MONEY,
	F2 MONEY,
	F3 MONEY,
	F4 MONEY,
	F5 MONEY,
	F6 MONEY,
	F7 MONEY,
	F8 MONEY,
	F9 MONEY,
	F10 MONEY,
	F11 MONEY,
	F12 MONEY,
	F13 MONEY,
	F14 MONEY,
	F15 MONEY,
	F16 MONEY,
	F17 MONEY,
	F18 MONEY,
	F19 VARCHAR(100),
	F20 VARCHAR(50),
	F21 MONEY,
	F22 MONEY,
	F23 MONEY,
	F24 MONEY,
	F25 MONEY
	)
	--SELECT * FROM #T

	-- таблица, в которую будем вставлять данные (только общие итоги суммарно по всем аптекам)
	IF OBJECT_ID('TEMPDB..#T_ALL') IS NOT NULL DROP TABLE #T_ALL
	CREATE TABLE #T_ALL (
	F1 MONEY,
	F2 MONEY,
	F3 MONEY,
	F4 MONEY,
	F5 MONEY,
	F6 MONEY,
	F7 MONEY,
	F8 MONEY,
	F9 MONEY,
	F10 MONEY,
	F11 MONEY,
	F12 MONEY,
	F13 MONEY,
	F14 MONEY,
	F15 MONEY,
	F16 MONEY,
	F17 MONEY,
	F18 MONEY,
	F19 VARCHAR(100),
	F20 VARCHAR(50),
	F21 MONEY,
	F22 MONEY,
	F23 MONEY,
	F24 MONEY,
	F25 MONEY
	)

	DECLARE	@Z1	MONEY
	DECLARE	@Z2	MONEY
	DECLARE	@Z3	MONEY
	DECLARE	@Z4	MONEY
	DECLARE	@Z5	MONEY
	DECLARE	@Z6	MONEY
	DECLARE	@Z7	MONEY
	DECLARE	@Z8	MONEY
	DECLARE	@Z9	MONEY
	DECLARE	@Z10 MONEY
	DECLARE	@Z11 MONEY
	DECLARE	@Z12 MONEY
	DECLARE	@Z13 MONEY
	DECLARE	@Z14 MONEY
	DECLARE	@Z15 MONEY
	DECLARE	@Z16 MONEY
	DECLARE	@Z17 MONEY
	DECLARE	@Z18 MONEY
	DECLARE	@Z21 MONEY
	DECLARE	@Z22 MONEY
	DECLARE	@Z23 MONEY
	DECLARE	@Z24 MONEY
	DECLARE	@Z25 MONEY

	DECLARE	@A1	MONEY
	DECLARE	@A2	MONEY
	DECLARE	@A3	MONEY
	DECLARE	@A4	MONEY
	DECLARE	@A5	MONEY
	DECLARE	@A6	MONEY
	DECLARE	@A7	MONEY
	DECLARE	@A8	MONEY
	DECLARE	@A9	MONEY
	DECLARE	@A10 MONEY
	DECLARE	@A11 MONEY
	DECLARE	@A12 MONEY
	DECLARE	@A13 MONEY
	DECLARE	@A14 MONEY
	DECLARE	@A15 MONEY
	DECLARE	@A16 MONEY
	DECLARE	@A17 MONEY
	DECLARE	@A18 MONEY
	DECLARE	@A21 MONEY
	DECLARE	@A22 MONEY
	DECLARE	@A23 MONEY
	DECLARE	@A24 MONEY
	DECLARE	@A25 MONEY

	DECLARE	@B1	MONEY
	DECLARE	@B2	MONEY
	DECLARE	@B3	MONEY
	DECLARE	@B4	MONEY
	DECLARE	@B5	MONEY
	DECLARE	@B6	MONEY
	DECLARE	@B7	MONEY
	DECLARE	@B8	MONEY
	DECLARE	@B9	MONEY
	DECLARE	@B10 MONEY
	DECLARE	@B11 MONEY
	DECLARE	@B12 MONEY
	DECLARE	@B13 MONEY
	DECLARE	@B14 MONEY
	DECLARE	@B15 MONEY
	DECLARE	@B16 MONEY
	DECLARE	@B17 MONEY
	DECLARE	@B18 MONEY
	DECLARE	@B21 MONEY
	DECLARE	@B22 MONEY
	DECLARE	@B23 MONEY
	DECLARE	@B24 MONEY
	DECLARE	@B25 MONEY

	DECLARE	@C1	MONEY
	DECLARE	@C2	MONEY
	DECLARE	@C3	MONEY
	DECLARE	@C4	MONEY
	DECLARE	@C5	MONEY
	DECLARE	@C6	MONEY
	DECLARE	@C7	MONEY
	DECLARE	@C8	MONEY
	DECLARE	@C9	MONEY
	DECLARE	@C10 MONEY
	DECLARE	@C11 MONEY
	DECLARE	@C12 MONEY
	DECLARE	@C13 MONEY
	DECLARE	@C14 MONEY
	DECLARE	@C15 MONEY
	DECLARE	@C16 MONEY
	DECLARE	@C17 MONEY
	DECLARE	@C18 MONEY
	DECLARE	@C21 MONEY
	DECLARE	@C22 MONEY
	DECLARE	@C23 MONEY
	DECLARE	@C24 MONEY
	DECLARE	@C25 MONEY

	DECLARE	@D1	MONEY
	DECLARE	@D2	MONEY
	DECLARE	@D3	MONEY
	DECLARE	@D4	MONEY
	DECLARE	@D5	MONEY
	DECLARE	@D6	MONEY
	DECLARE	@D7	MONEY
	DECLARE	@D8	MONEY
	DECLARE	@D9	MONEY
	DECLARE	@D10 MONEY
	DECLARE	@D11 MONEY
	DECLARE	@D12 MONEY
	DECLARE	@D13 MONEY
	DECLARE	@D14 MONEY
	DECLARE	@D15 MONEY
	DECLARE	@D16 MONEY
	DECLARE	@D17 MONEY
	DECLARE	@D18 MONEY
	DECLARE	@D21 MONEY
	DECLARE	@D22 MONEY
	DECLARE	@D23 MONEY
	DECLARE	@D24 MONEY
	DECLARE	@D25 MONEY

	DECLARE	@E1	MONEY
	DECLARE	@E2	MONEY
	DECLARE	@E3	MONEY
	DECLARE	@E4	MONEY
	DECLARE	@E5	MONEY
	DECLARE	@E6	MONEY
	DECLARE	@E7	MONEY
	DECLARE	@E8	MONEY
	DECLARE	@E9	MONEY
	DECLARE	@E10 MONEY
	DECLARE	@E11 MONEY
	DECLARE	@E12 MONEY
	DECLARE	@E13 MONEY
	DECLARE	@E14 MONEY
	DECLARE	@E15 MONEY
	DECLARE	@E16 MONEY
	DECLARE	@E17 MONEY
	DECLARE	@E18 MONEY
	DECLARE	@E21 MONEY
	DECLARE	@E22 MONEY
	DECLARE	@E23 MONEY
	DECLARE	@E24 MONEY
	DECLARE	@E25 MONEY

	DECLARE @APT_ALL VARCHAR(50)
	DECLARE @A VARCHAR(50)
	DECLARE @B VARCHAR(50)
	DECLARE @C VARCHAR(50)
	DECLARE @D VARCHAR(50)
	DECLARE @E VARCHAR(50)
	DECLARE @Z VARCHAR(50)

	set @APT_ALL = '-- По всем аптекам'
	set @A = 'Выручка (нал. + безнал.)'
	set @B = 'Наличные'
	set @C = 'Безнал'
	set @D = 'Ночная выручка'
	set @E = 'Ортопедия'
	set @Z = 'Общая сумма'

	--*******************************************************************************
	-- 1. Запасы товара на начало недели:
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#OST_NACH') IS NOT NULL DROP TABLE #OST_NACH
	SELECT 
		C.NAME,
		L.PRICE_SAL,
		AMOUNT_OST = SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES),
		AMOUNT_OST_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.id_goods) = 1 then (LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES) else 0 end)
	INTO #OST_NACH		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
	WHERE LM.DATE_OP <= @DATE_FR
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME, L.PRICE_SAL
	--SELECT * FROM #OST_NACH

	-- Z: Общая сумма
	SELECT @Z1 = SUM(O.AMOUNT_OST * O.PRICE_SAL)
	FROM #OST_NACH O
	
	--SELECT @Z1
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(@Z1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- E: Ортопедия
	SELECT @E1 = SUM(O.AMOUNT_OST_ORTO * O.PRICE_SAL)
	FROM #OST_NACH O
	
	--select @E1
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(@E1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)

	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F1, F19, F20)
	SELECT SUM(O.AMOUNT_OST * O.PRICE_SAL), O.NAME, @Z
	FROM #OST_NACH O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	-- E: Ортопедия 
	INSERT INTO #T (F1, F19, F20)
	SELECT SUM(O.AMOUNT_OST_ORTO * O.PRICE_SAL), O.NAME, @E
	FROM #OST_NACH O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME
	--*******************************************************************************
	-- 18. Запасы товара на конец недели:

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#OST_KON') IS NOT NULL DROP TABLE #OST_KON
	SELECT 
		C.name,
		L.PRICE_SAL,
		AMOUNT_OST = SUM(LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES),
		AMOUNT_OST_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.id_goods) = 1 then (LM.QUANTITY_ADD - LM.QUANTITY_SUB - LM.QUANTITY_RES) else 0 end)
	INTO #OST_KON		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP <= @DATE_TO
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name, L.PRICE_SAL
	--select * from #OST_KON

	-- Z: Общая сумма
	SELECT @Z18 = SUM(O.AMOUNT_OST * O.PRICE_SAL)
	FROM #OST_KON O
	
	--select @Z18
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z18, @APT_ALL, @Z, 0)
	
	-- E: Ортопедия
	SELECT @E18 = SUM(O.AMOUNT_OST_ORTO * O.PRICE_SAL)
	FROM #OST_KON O
	
	--select @E18
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E18, @APT_ALL, @E, 0)

	-- С РАЗБИВКОЙ ПО АПТЕКАМ:
	-- Z: Общая сумма 
	insert into #T (F18, F19, F20)
	SELECT SUM(O.AMOUNT_OST * O.PRICE_SAL), o.name, @Z
	FROM #OST_KON O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	-- E: Ортопедия 
	insert into #T (F18, F19, F20)
	SELECT SUM(O.AMOUNT_OST_ORTO * O.PRICE_SAL), o.name, @E
	FROM #OST_KON O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 3. Приход от поставщика (медикаменты)
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#PRIHOD') IS NOT NULL DROP TABLE #PRIHOD
	SELECT 
		C.name,
		QUANTITY_ADD = SUM(case when dbo.FN_GOODS_IS_ORTO(l.id_goods) = 0 then LM.QUANTITY_ADD else 0 end),
		QUANTITY_ADD_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.id_goods) = 1 then LM.QUANTITY_ADD else 0 end),
		L.PRICE_SAL
	INTO #PRIHOD		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE in (2, 30) -- Приходные накладные и ввод остатков
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name, L.PRICE_SAL
	--select * from #PRIHOD

	-- Z: Общая сумма
	SELECT @Z3 = SUM(O.QUANTITY_ADD * O.PRICE_SAL)
	FROM #PRIHOD O
	
	--select @Z3
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, @Z3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F3, F19, F20)
	SELECT SUM(O.QUANTITY_ADD * O.PRICE_SAL), o.name, @Z
	FROM #PRIHOD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 21. Приход от поставщика (ортопедия)
	-- Используем таблицу #PRIHOD (см. п. 3)

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	SELECT @Z21 = SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL)
	FROM #PRIHOD O
	
	--select @Z21
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, @Z21)

	-- С РАЗБИВКОЙ ПО АПТЕКАМ:
	-- Z: Общая сумма 
	insert into #T (F21, F19, F20)
	SELECT SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL), o.name, @Z
	FROM #PRIHOD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 4. Перемещение со склада в аптеки (медикаменты)
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	IF OBJECT_ID('TEMPDB..#PEREM') IS NOT NULL DROP TABLE #PEREM
	SELECT -- Перемещение между подразделениями (отправитель - ЦО, получатель - не ЦО)
		CC_to.name,
		QUANTITY_SUB = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 0 then LM.QUANTITY_SUB else 0 end),
		QUANTITY_SUB_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	INTO #PEREM		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join INTERFIRM_MOVING m on lm.ID_DOCUMENT = m.ID_INTERFIRM_MOVING_GLOBAL and lm.ID_TABLE = 37 -- Перемещения между подразделениями
		join STORE ss_to on m.ID_STORE_TO_MAIN = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM_MAIN = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_from.ID_CONTRACTOR_GLOBAL = '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and m.ID_STORE_FROM_MAIN != m.ID_STORE_TO_MAIN
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY CC_to.name, L.PRICE_SAL

	union all

	SELECT -- Перемещение между подразделениями (отправитель - не ЦО, получатель - не ЦО)
		CC_to.name,
		QUANTITY_SUB = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 0 then LM.QUANTITY_SUB else 0 end),
		QUANTITY_SUB_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join INTERFIRM_MOVING m on lm.ID_DOCUMENT = m.ID_INTERFIRM_MOVING_GLOBAL and lm.ID_TABLE = 37 -- Перемещения между подразделениями
		join STORE ss_to on m.ID_STORE_TO_MAIN = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM_MAIN = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_from.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and cc_to.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and m.ID_STORE_FROM_MAIN != m.ID_STORE_TO_MAIN
		AND (@ALL_CONTRACTOR = 1 OR cc_from.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR) OR cc_to.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY CC_to.name, L.PRICE_SAL

	union all

	SELECT -- Перемещение внутреннее (отправитель - ЦО, получатель - не ЦО)
		CC_to.name,
		QUANTITY_SUB = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 0 then LM.QUANTITY_SUB else 0 end),
		QUANTITY_SUB_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join MOVEMENT m on lm.ID_DOCUMENT = m.ID_MOVEMENT_GLOBAL and lm.ID_TABLE = 8 -- Перемещение внутреннее
		join STORE ss_to on m.ID_STORE_TO = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_from.ID_CONTRACTOR_GLOBAL = '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and cc_to.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and m.ID_STORE_FROM != m.ID_STORE_TO
		AND (@ALL_CONTRACTOR = 1 OR cc_from.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR) OR cc_to.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY CC_to.name, L.PRICE_SAL

	--select * from #PEREM
	
	SELECT @Z4 = SUM(O.QUANTITY_SUB * O.PRICE_SAL)
	FROM #PEREM O
	
	--select @Z4
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, @Z4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F4, F19, F20)
	SELECT SUM(O.QUANTITY_SUB * O.PRICE_SAL), o.name, @Z
	FROM #PEREM O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 5. Перемещение со склада в аптеки (ортопедия)
	-- Используем таблицу #PEREM (см. п.4)

	
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	SELECT @Z5 = SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL)
	FROM #PEREM O
	
	--select @Z5
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, @Z5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F5, F19, F20)
	SELECT SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL), o.name, @Z
	FROM #PEREM O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 6. Излишки по ревизии:

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#INV_ADD') IS NOT NULL DROP TABLE #INV_ADD
	SELECT 
		C.name,
		QUANTITY_ADD = SUM(LM.QUANTITY_ADD),
		QUANTITY_ADD_ORTO = SUM(case when dbo.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_ADD else 0 end),
		L.PRICE_SAL
	INTO #INV_ADD		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 24
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name , L.PRICE_SAL
	--select * from #INV_ADD

	-- Z: Общая сумма
	SELECT @Z6 = SUM(O.QUANTITY_ADD * O.PRICE_SAL)
	FROM #INV_ADD O
	
	--select @Z6
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, @Z6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	-- E: Ортопедия
	SELECT @E6 = SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL)
	FROM #INV_ADD O
	
	--select @E1
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, @E6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:
	-- Z: Общая сумма 
	insert into #T (F6, F19, F20)
	SELECT SUM(O.QUANTITY_ADD * O.PRICE_SAL), o.name, @Z
	FROM #INV_ADD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	-- E: Ортопедия 
	insert into #T (F6, F19, F20)
	SELECT SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL), o.name, @E
	FROM #INV_ADD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 12. Недостача по ревизии:

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#INV_SUB') IS NOT NULL DROP TABLE #INV_SUB

	SELECT 
		C.name,
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		QUANTITY_SUB_ORTO = SUM(CASE WHEN DBO.FN_GOODS_IS_ORTO(L.ID_GOODS) = 1 THEN LM.QUANTITY_SUB ELSE 0 END),
		L.PRICE_SAL
	INTO #INV_SUB		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 24
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name , L.PRICE_SAL
	--select * from #INV_SUB

	-- Z: Общая сумма
	SELECT @Z12 = SUM(O.QUANTITY_SUB * O.PRICE_SAL)
	FROM #INV_SUB O
	
	--select @Z12
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z12, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)

	-- E: Ортопедия
	SELECT @E12 = SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL)
	FROM #INV_SUB O
	
	--select @E12
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E12, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F12, F19, F20)
	SELECT SUM(O.QUANTITY_SUB * O.PRICE_SAL), o.name, @Z
	FROM #INV_SUB O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	-- E: Ортопедия 
	insert into #T (F12, F19, F20)
	SELECT SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL), o.name, @E
	FROM #INV_SUB O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name
	--*******************************************************************************
	-- 7. Переоценка (новая сумма):

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#REV_ADD') IS NOT NULL DROP TABLE #REV_ADD

	SELECT 
		C.name,
		QUANTITY_ADD = SUM(LM.QUANTITY_ADD),
		QUANTITY_ADD_ORTO = SUM(CASE WHEN DBO.FN_GOODS_IS_ORTO(L.ID_GOODS) = 1 THEN LM.QUANTITY_ADD ELSE 0 END),
		L.PRICE_SAL
	INTO #REV_ADD		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 13
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name, L.PRICE_SAL
	--select * from #REV_ADD

	-- Z: Общая сумма
	SELECT @Z7 = SUM(O.QUANTITY_ADD * O.PRICE_SAL)
	FROM #REV_ADD O
	
	--select @Z7
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, @Z7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)

	-- E: Ортопедия
	SELECT @E7 = SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL)
	FROM #REV_ADD O
	
	--select @E7
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, @E7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)

	-- С РАЗБИВКОЙ ПО АПТЕКАМ:
	-- Z: Общая сумма 
	insert into #T (F7, F19, F20)
	SELECT SUM(O.QUANTITY_ADD * O.PRICE_SAL), o.name, @Z
	FROM #REV_ADD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	-- E: Ортопедия 
	insert into #T (F7, F19, F20)
	SELECT SUM(O.QUANTITY_ADD_ORTO * O.PRICE_SAL), o.name, @E
	FROM #REV_ADD O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 13. Переоценка (старая сумма):

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	IF OBJECT_ID('TEMPDB..#REV_SUB') IS NOT NULL DROP TABLE #REV_SUB

	SELECT 
		C.name,
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		QUANTITY_SUB_ORTO = SUM(CASE WHEN DBO.FN_GOODS_IS_ORTO(L.ID_GOODS) = 1 THEN LM.QUANTITY_SUB ELSE 0 END),
		L.PRICE_SAL
	INTO #REV_SUB		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 13
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name , L.PRICE_SAL
	--select * from #REV_SUB

	SELECT @Z13 = SUM(O.QUANTITY_SUB * O.PRICE_SAL)
	FROM #REV_SUB O
	
	--select @Z13
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z13, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	-- E: Ортопедия

	SELECT @E13 = SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL)
	FROM #REV_SUB O
	
	--select @E13
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E13, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)

	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:
	-- Z: Общая сумма 
	insert into #T (F13, F19, F20)
	SELECT SUM(O.QUANTITY_SUB * O.PRICE_SAL), o.name, @Z
	FROM #REV_SUB O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	-- E: Ортопедия 
	insert into #T (F13, F19, F20)
	SELECT SUM(O.QUANTITY_SUB_ORTO * O.PRICE_SAL), o.name, @E
	FROM #REV_SUB O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 17. Итог разукомплектации
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	IF OBJECT_ID('TEMPDB..#DISASM') IS NOT NULL DROP TABLE #DISASM

	SELECT 
		C.name,
		QUANTITY_ADD = SUM(LM.QUANTITY_ADD),
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		L.PRICE_SAL
	INTO #DISASM		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 6 -- Акты разукомплектации
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.name, L.PRICE_SAL
	--select * from #DISASM

	SELECT @Z17 = SUM(O.QUANTITY_ADD * O.PRICE_SAL - O.QUANTITY_SUB * O.PRICE_SAL)
	FROM #DISASM O
	
	--select @Z17
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z17, 0, @APT_ALL, @Z, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F17, F19, F20)
	SELECT SUM(O.QUANTITY_ADD * O.PRICE_SAL - O.QUANTITY_SUB * O.PRICE_SAL), o.name, @Z
	FROM #DISASM O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--*******************************************************************************
	-- 8) Выручка (чистая, т.е. с учетом скидки)

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#CHEQUE') IS NOT NULL DROP TABLE #CHEQUE
	SELECT 
		C.NAME,
		SUM_ACC = SUM(lm.SUM_ACC),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then lm.SUM_ACC else 0 end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then lm.SUM_ACC else 0 end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then lm.SUM_ACC else 0 end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then lm.SUM_ACC else 0 end)
	INTO #CHEQUE		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'SALE'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME

	union all

	SELECT 
		C.NAME,
		SUM_ACC = SUM(-lm.SUM_ACC),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then -lm.SUM_ACC else 0 end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then -lm.SUM_ACC else 0 end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then -lm.SUM_ACC else 0 end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then -lm.SUM_ACC else 0 end)
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'RETURN'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME
	--SELECT * FROM #CHEQUE

	-- Z: Общая сумма
	SELECT @Z8 = SUM(o.SUM_ACC)
	FROM #CHEQUE O
	
	--SELECT @Z8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @Z8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- A - выручка (нал + безнал)
	SELECT @A8 = SUM(o.SUM_ACC)
	FROM #CHEQUE O
	
	--SELECT @A8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @A8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @A, 0)
	
	
	-- B - выручка (только наличка)
	SELECT @B8 = SUM(o.SUM_NAL)
	FROM #CHEQUE O
	
	--SELECT B8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @B8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @B, 0)
	
	
	-- C - выручка (только безнал)
	SELECT @C8 = SUM(o.SUM_BEZNAL)
	FROM #CHEQUE O
	
	--SELECT C8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @C8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @C, 0)
	
	
	-- D - ночная выручка (в ночное время)
	SELECT @D8 = SUM(o.SUM_NIGHT)
	FROM #CHEQUE O
	
	--SELECT D8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @D8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @D, 0)
	
	
	-- E - ортопедия 
	SELECT @E8 = SUM(o.SUM_ORTO)
	FROM #CHEQUE O
	
	--SELECT E8
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, @E8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_ACC), O.NAME, @Z
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--A - выручка (нал + безнал)
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_ACC), O.NAME, @A
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME


	--B - выручка (только наличка)
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_NAL), O.NAME, @B
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--C - выручка (только безнал)
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_BEZNAL), O.NAME, @C
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--D - ночная выручка (в ночное время)
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_NIGHT), O.NAME, @D
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--E - ортопедия 
	INSERT INTO #T (F8, F19, F20)
	SELECT SUM(o.SUM_ORTO), O.NAME, @E
	FROM #CHEQUE O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--*******************************************************************************
	-- 14) Скидка

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#DISCOUNT') IS NOT NULL DROP TABLE #DISCOUNT
	SELECT 
		C.NAME,
		DISCOUNT_ACC = SUM(lm.DISCOUNT_ACC),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then lm.DISCOUNT_ACC else 0 end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then lm.DISCOUNT_ACC else 0 end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then lm.DISCOUNT_ACC else 0 end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then lm.DISCOUNT_ACC else 0 end)
	INTO #DISCOUNT		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'SALE'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME

	union all

	SELECT 
		C.NAME,
		DISCOUNT_ACC = SUM(-lm.DISCOUNT_ACC),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then -lm.DISCOUNT_ACC else 0 end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then -lm.DISCOUNT_ACC else 0 end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then -lm.DISCOUNT_ACC else 0 end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then -lm.DISCOUNT_ACC else 0 end)
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'RETURN'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME
	--SELECT * FROM #DISCOUNT

	-- Z: Общая сумма
	SELECT @Z14 = SUM(o.DISCOUNT_ACC)
	FROM #DISCOUNT O
	
	--SELECT @Z14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z14, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- A - выручка (нал + безнал)
	SELECT @A14 = SUM(o.DISCOUNT_ACC)
	FROM #DISCOUNT O
	
	--SELECT @A14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @A14, 0, 0, 0, 0, @APT_ALL, @A, 0)
	
	
	-- B - выручка (только наличка)
	SELECT @B14 = SUM(o.SUM_NAL)
	FROM #DISCOUNT O
	
	--SELECT @B14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @B14, 0, 0, 0, 0, @APT_ALL, @B, 0)
	
	
	-- C - выручка (только безнал)
	SELECT @C14 = SUM(o.SUM_BEZNAL)
	FROM #DISCOUNT O
	
	--SELECT C14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @C14, 0, 0, 0, 0, @APT_ALL, @C, 0)
	
	
	-- D - ночная выручка (в ночное время)
	SELECT @D14 = SUM(o.SUM_NIGHT)
	FROM #DISCOUNT O
	
	--SELECT D14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @D14, 0, 0, 0, 0, @APT_ALL, @D, 0)
	
	
	-- E - ортопедия 
	SELECT @E14 = SUM(o.SUM_ORTO)
	FROM #DISCOUNT O
	
	--SELECT E14
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E14, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.DISCOUNT_ACC), O.NAME, @Z
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--A - выручка (нал + безнал)
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.DISCOUNT_ACC), O.NAME, @A
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME


	--B - выручка (только наличка)
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.SUM_NAL), O.NAME, @B
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--C - выручка (только безнал)
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.SUM_BEZNAL), O.NAME, @C
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--D - ночная выручка (в ночное время)
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.SUM_NIGHT), O.NAME, @D
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--E - ортопедия 
	INSERT INTO #T (F14, F19, F20)
	SELECT SUM(o.SUM_ORTO), O.NAME, @E
	FROM #DISCOUNT O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--*******************************************************************************
	-- 11. Перемещение из аптек на склад + перемещение между аптеками
	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	IF OBJECT_ID('TEMPDB..#PEREM_BACK') IS NOT NULL DROP TABLE #PEREM_BACK
	SELECT -- Перемещение между подразделениями (отправитель - не ЦО, получатель - ЦО)
		CC_from.name,
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	INTO #PEREM_BACK		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join INTERFIRM_MOVING m on lm.ID_DOCUMENT = m.ID_INTERFIRM_MOVING_GLOBAL and lm.ID_TABLE = 37 -- Перемещения между подразделениями
		join STORE ss_to on m.ID_STORE_TO_MAIN = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM_MAIN = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_to.ID_CONTRACTOR_GLOBAL = '1967AA8A-ED8E-4D6E-9384-758B0B5A376C' -- Центральный офис
	   and cc_from.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C' -- не центральный офис
	   and m.ID_STORE_FROM_MAIN != m.ID_STORE_TO_MAIN
		AND (@ALL_CONTRACTOR = 1 OR cc_from.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR) OR cc_to.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY CC_from.name, L.PRICE_SAL

	union all

	SELECT -- Перемещение между подразделениями (отправитель - не ЦО, получатель - не ЦО)
		cc_from.name,
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join INTERFIRM_MOVING m on lm.ID_DOCUMENT = m.ID_INTERFIRM_MOVING_GLOBAL and lm.ID_TABLE = 37 -- Перемещения между подразделениями
		join STORE ss_to on m.ID_STORE_TO_MAIN = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM_MAIN = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_from.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and cc_to.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and m.ID_STORE_FROM_MAIN != m.ID_STORE_TO_MAIN
		AND (@ALL_CONTRACTOR = 1 OR cc_from.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR) OR cc_to.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY cc_from.name, L.PRICE_SAL

	union all

	SELECT -- Перемещение внутреннее (отправитель - не ЦО, получатель - ЦО)
		cc_from.name,
		QUANTITY_SUB = SUM(LM.QUANTITY_SUB),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then LM.QUANTITY_SUB else 0 end),
		L.PRICE_SAL
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		join STORE s on l.ID_STORE = s.ID_STORE
		join CONTRACTOR c on s.ID_CONTRACTOR = c.ID_CONTRACTOR
		join MOVEMENT m on lm.ID_DOCUMENT = m.ID_MOVEMENT_GLOBAL and lm.ID_TABLE = 8 -- Перемещение внутреннее
		join STORE ss_to on m.ID_STORE_TO = ss_to.ID_STORE
		join CONTRACTOR cc_to on ss_to.ID_CONTRACTOR = cc_to.ID_CONTRACTOR
		join STORE ss_from on m.ID_STORE_FROM = ss_from.ID_STORE
		join CONTRACTOR cc_from on ss_from.ID_CONTRACTOR = cc_from.ID_CONTRACTOR
	where LM.DATE_OP between @DATE_FR and @DATE_TO
	   and cc_from.ID_CONTRACTOR_GLOBAL != '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and cc_to.ID_CONTRACTOR_GLOBAL = '1967AA8A-ED8E-4D6E-9384-758B0B5A376C'
	   and m.ID_STORE_FROM != m.ID_STORE_TO
		AND (@ALL_CONTRACTOR = 1 OR cc_from.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR) OR cc_to.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY CC_from.name, L.PRICE_SAL


	--select * from #PEREM

	-- СУММАРНО ПО АПТЕКАМ
	-- Z: Общая сумма
	SELECT @Z11 = SUM(O.QUANTITY_SUB * O.PRICE_SAL)
	FROM #PEREM_BACK O
	
	--select @Z11
	insert into #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	values(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z11, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- E - ортопедия 
	SELECT @E11 = SUM(o.SUM_ORTO * O.PRICE_SAL)
	FROM #PEREM_BACK O
	
	--SELECT @E11
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E11, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	insert into #T (F11, F19, F20)
	SELECT SUM(O.QUANTITY_SUB * O.PRICE_SAL), o.name, @Z
	FROM #PEREM_BACK O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	group by o.name

	--E - ортопедия 
	INSERT INTO #T (F11, F19, F20)
	SELECT SUM(o.SUM_ORTO * O.PRICE_SAL), O.NAME, @E
	FROM #PEREM_BACK O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--*******************************************************************************
	-- 10) Возврат поставщику

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#ACT_RETURN_TO_CONTRACTOR') IS NOT NULL DROP TABLE #ACT_RETURN_TO_CONTRACTOR
	SELECT 
		C.NAME,
		SUM_ACC = SUM(lm.SUM_ACC),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then lm.SUM_ACC else 0 end)
	INTO #ACT_RETURN_TO_CONTRACTOR		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 3 -- Акт возврата поставщику
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME


	--SELECT * FROM #ACT_RETURN_TO_CONTRACTOR

	-- Z: Общая сумма
	SELECT @Z10 = SUM(o.SUM_ACC)
	FROM #ACT_RETURN_TO_CONTRACTOR O
	
	--SELECT @Z10
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, @Z10, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- E - ортопедия 
	SELECT @E10 = SUM(o.SUM_ORTO)
	FROM #ACT_RETURN_TO_CONTRACTOR O
	
	--SELECT E10
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, @E10, 0, 0, 0, 0, 0, 0, 0, 0, @APT_ALL, @E, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F10, F19, F20)
	SELECT SUM(o.SUM_ACC), O.NAME, @Z
	FROM #ACT_RETURN_TO_CONTRACTOR O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--E - ортопедия 
	INSERT INTO #T (F10, F19, F20)
	SELECT SUM(o.SUM_ORTO), O.NAME, @E
	FROM #ACT_RETURN_TO_CONTRACTOR O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--*******************************************************************************
	-- 15) Средний чек

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:

	IF OBJECT_ID('TEMPDB..#CHEQUE_AVG') IS NOT NULL DROP TABLE #CHEQUE_AVG
	SELECT 
		C.NAME,
		SUM_ACC = SUM(lm.SUM_ACC),
		COUNT_ACC = COUNT(distinct cq.id_cheque_global),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then lm.SUM_ACC else 0 end),
		COUNT_NAL = count(distinct case when cq.PAY_TYPE_NAME = 'Оплата наличными' then cq.id_cheque_global else null end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then lm.SUM_ACC else 0 end),
		COUNT_BEZNAL = count(distinct case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then cq.id_cheque_global else null end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then lm.SUM_ACC else 0 end),
		COUNT_NIGHT = COUNT(distinct case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then cq.id_cheque_global else null end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then lm.SUM_ACC else 0 end),
		COUNT_ORTO = COUNT(distinct case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then cq.id_cheque_global else null end)
	INTO #CHEQUE_AVG		
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'SALE'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME

	union all

	SELECT 
		C.NAME,
		SUM_ACC = SUM(-lm.SUM_ACC),
		COUNT_ACC = -COUNT(distinct cq.id_cheque_global),
		SUM_NAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата наличными' then -lm.SUM_ACC else 0 end),
		COUNT_NAL = -count(distinct case when cq.PAY_TYPE_NAME = 'Оплата наличными' then cq.id_cheque_global else null end),
		SUM_BEZNAL = sum(case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then -lm.SUM_ACC else 0 end),
		COUNT_BEZNAL = -COUNT(distinct case when cq.PAY_TYPE_NAME = 'Оплата платежной картой' then cq.id_cheque_global else null end),
		SUM_NIGHT = SUM(case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then -lm.SUM_ACC else 0 end),
		COUNT_NIGHT = -COUNT(distinct case when dbo.fn_time_is_night(lm.DATE_OP) = 1 and dbo.fn_apteka_is_night(c.ID_CONTRACTOR) = 1 then cq.id_cheque_global else null end),
		SUM_ORTO = SUM(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then -lm.SUM_ACC else 0 end),
		COUNT_ORTO = -COUNT(case when DBO.FN_GOODS_IS_ORTO(l.ID_GOODS) = 1 then cq.id_cheque_global else null end)
	FROM LOT L
		JOIN LOT_MOVEMENT LM ON L.ID_LOT_GLOBAL = LM.ID_LOT_GLOBAL
		JOIN STORE S ON L.ID_STORE = S.ID_STORE
		JOIN CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
		join CHEQUE_ITEM ci on lm.ID_DOCUMENT_ITEM = ci.ID_CHEQUE_ITEM_GLOBAL
		join CHEQUE cq on ci.ID_CHEQUE_GLOBAL = cq.ID_CHEQUE_GLOBAL and cq.CHEQUE_TYPE = 'RETURN'
	WHERE LM.DATE_OP BETWEEN @DATE_FR and @DATE_TO
	   and lm.ID_TABLE = 19
		AND (@ALL_CONTRACTOR = 1 OR C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM #CONTRACTOR))
	GROUP BY C.NAME
	--SELECT * FROM #CHEQUE_AVG

	-- Z: Общая сумма
	SELECT @Z15 = SUM(o.SUM_ACC) / (case when SUM(o.COUNT_ACC) != 0 then SUM(o.COUNT_ACC) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT @Z15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z15, 0, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- A - выручка (нал + безнал)
	SELECT @A15 = SUM(o.SUM_ACC) / (case when SUM(o.COUNT_ACC) != 0 then SUM(o.COUNT_ACC) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT @A15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @A15, 0, 0, 0, @APT_ALL, @A, 0)
	
	
	-- B - выручка (только наличка)
	SELECT @B15 = SUM(o.SUM_NAL) / (case when SUM(o.COUNT_NAL) != 0 then SUM(o.COUNT_NAL) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT B15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @B15, 0, 0, 0, @APT_ALL, @B, 0)
	
	
	-- C - выручка (только безнал)
	SELECT @C15 = SUM(o.SUM_BEZNAL) / (case when SUM(o.COUNT_BEZNAL) != 0 then SUM(o.COUNT_BEZNAL) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT C15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @C15, 0, 0, 0, @APT_ALL, @C, 0)
	
	
	-- D - ночная выручка (в ночное время)
	SELECT @D15 = SUM(o.SUM_NIGHT) / (case when SUM(o.COUNT_NIGHT) != 0 then SUM(o.COUNT_NIGHT) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT @D15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @D15, 0, 0, 0, @APT_ALL, @D, 0)
	
	
	-- E - ортопедия 
	SELECT @E15 = SUM(o.SUM_ORTO) / (case when SUM(o.COUNT_ORTO) != 0 then SUM(o.COUNT_ORTO) else 1 end)
	FROM #CHEQUE_AVG O
	
	--SELECT E15
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @E15, 0, 0, 0, @APT_ALL, @E, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_ACC) / (case when SUM(o.COUNT_ACC) != 0 then SUM(o.COUNT_ACC) else 1 end), O.NAME, @Z
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--A - выручка (нал + безнал)
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_ACC) / (case when SUM(o.COUNT_ACC) != 0 then SUM(o.COUNT_ACC) else 1 end), O.NAME, @A
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--B - выручка (только наличка)
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_NAL) / (case when SUM(o.COUNT_NAL) != 0 then SUM(o.COUNT_NAL) else 1 end), O.NAME, @B
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--C - выручка (только безнал)
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_BEZNAL) / (case when SUM(o.COUNT_BEZNAL) != 0 then SUM(o.COUNT_BEZNAL) else 1 end), O.NAME, @C
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--D - ночная выручка (в ночное время)
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_NIGHT) / (case when SUM(o.COUNT_NIGHT) != 0 then SUM(o.COUNT_NIGHT) else 1 end), O.NAME, @D
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--E - ортопедия 
	INSERT INTO #T (F15, F19, F20)
	SELECT SUM(o.SUM_ORTO) / (case when SUM(o.COUNT_ORTO) != 0 then SUM(o.COUNT_ORTO) else 1 end), O.NAME, @E
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	------------------------------------------------------------------------------------
	-- 16. Проходимость.
	-- Используем данные из таблицы #CHEQUE_AVG

	-- СУММАРНО ПО ВСЕМ АПТЕКАМ:
	-- Z: Общая сумма
	SELECT @Z16 = SUM(o.COUNT_ACC) / @COUNT_DAY_PERIOD
	FROM #CHEQUE_AVG O
	
	--SELECT @Z16
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @Z16, 0, 0, @APT_ALL, @Z, 0)
	
	
	-- D - ночная выручка (в ночное время)
	SELECT @D16 = SUM(o.COUNT_NIGHT) / @COUNT_DAY_PERIOD
	FROM #CHEQUE_AVG O
	
	--SELECT @D16
	INSERT INTO #T_ALL (F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21)
	VALUES(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @D16, 0, 0, @APT_ALL, @D, 0)
	
	
	-- С РАЗБИВКОЙ ПО АПТЕКАМ:

	-- Z: Общая сумма 
	INSERT INTO #T (F16, F19, F20)
	SELECT SUM(o.COUNT_ACC) / @COUNT_DAY_PERIOD, O.NAME, @Z
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--D - ночная выручка (в ночное время)
	INSERT INTO #T (F16, F19, F20)
	SELECT SUM(o.COUNT_NIGHT) / @COUNT_DAY_PERIOD, O.NAME, @D
	FROM #CHEQUE_AVG O
	--WHERE (@ALL_CONTRACTOR = 1 OR O.NAME IN (SELECT NAME FROM #CONTRACTOR))
	GROUP BY O.NAME

	--******** ВЫВОД КОНЕЧНЫХ РЕЗУЛЬТАТОВ **********************************************

	-- в разрезе аптек:
	select 
		t.F19 as [Аптека], 
		t.F20 as [Показатель], 
		isnull(SUM(t.F1), 0) as [Запасы_товара_на_начало_недели],
		isnull(SUM(t.F3), 0) + isnull(SUM(t.F4), 0) + isnull(SUM(t.F5), 0) + isnull(SUM(t.F6), 0) + isnull(SUM(t.F21), 0) as [Приход_без_переоценки],
		isnull(SUM(t.F3), 0) as [Приход_от_поставщика_медикаменты],
		isnull(SUM(t.F21), 0) as [Приход_от_поставщика_ортопедия],
		isnull(SUM(t.F4), 0) as [Приход_со_склада_медикаменты],
		isnull(SUM(t.F5), 0) as [Приход_со_склада_ортопедия],
		isnull(SUM(t.F6), 0) as [Излишки_по_ревизии],
		isnull(SUM(t.F7), 0) as [Переоценка_новая_сумма],
		isnull(SUM(t.F8), 0) as [Выручка],
		isnull(SUM(t.F10), 0) + isnull(SUM(t.F11), 0) + isnull(SUM(t.F12), 0) as [Возврат_без_переоценки],
		isnull(SUM(t.F10), 0) as [Возврат_на_склад_брак],
		isnull(SUM(t.F11), 0) as [Возврат_в_другую_аптеку],
		isnull(SUM(t.F12), 0) as [Недостача_по_ревизии],
		isnull(SUM(t.F13), 0) as [Переоценка_старая_сумма)],
		isnull(SUM(t.F15), 0) as [Средний_чек],
		isnull(SUM(t.F16), 0) as [Проходимость],
		isnull(SUM(t.F17), 0) as [Итог_разукомплектации],
		isnull(SUM(t.F18), 0) as [Запасы_товара_на_конец_недели],
		isnull(SUM(t.F14), 0) as [Скидка],
		round(isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD, 2) as [Средняя_выручка_в_сутки],
		round((isnull(SUM(t.F1), 0) + isnull(SUM(t.F18), 0)) / 2, 2) as [Средний_запас_на_конец_периода],
		round(((isnull(SUM(t.F1), 0) + isnull(SUM(t.F18), 0)) / 2) / (case when isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD != 0 then isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD else 1 end), 2) as [Время_обращения_запаса_в_днях],
		case 
		   when @print_prognoz = 1 then round(isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD, 2) * @COUNT_DAY_OF_MONTH
		   else 0
		end as [Ожидаемая_выручка]
	from #T t
	group by t.F19, t.F20
	
	union all
	
	-- общая сумма по аптекам:
	select 
		case when @ALL_CONTRACTOR = 1 then t.F19 else '-- По выбранным аптекам' end as [Аптека], 
		t.F20 as [Показатель], 
		isnull(SUM(t.F1), 0) as [Запасы_товара_на_начало_недели],
		isnull(SUM(t.F3), 0) + isnull(SUM(t.F4), 0) + isnull(SUM(t.F5), 0) + isnull(SUM(t.F6), 0) + isnull(SUM(t.F21), 0) as [Приход_без_переоценки],
		isnull(SUM(t.F3), 0) as [Приход_от_поставщика_медикаменты],
		isnull(SUM(t.F21), 0) as [Приход_от_поставщика_ортопедия],
		isnull(SUM(t.F4), 0) as [Приход_со_склада_медикаменты],
		isnull(SUM(t.F5), 0) as [Приход_со_склада_ортопедия],
		isnull(SUM(t.F6), 0) as [Излишки_по_ревизии],
		isnull(SUM(t.F7), 0) as [Переоценка_новая_сумма],
		isnull(SUM(t.F8), 0) as [Выручка],
		isnull(SUM(t.F10), 0) + isnull(SUM(t.F11), 0) + isnull(SUM(t.F12), 0) as [Возврат_без_переоценки],
		isnull(SUM(t.F10), 0) as [Возврат_на_склад_брак],
		isnull(SUM(t.F11), 0) as [Возврат_в_другую_аптеку],
		isnull(SUM(t.F12), 0) as [Недостача_по_ревизии],
		isnull(SUM(t.F13), 0) as [Переоценка_старая_сумма)],
		isnull(SUM(t.F15), 0) as [Средний_чек],
		isnull(SUM(t.F16), 0) as [Проходимость],
		isnull(SUM(t.F17), 0) as [Итог_разукомплектации],
		isnull(SUM(t.F18), 0) as [Запасы_товара_на_конец_недели],
		isnull(SUM(t.F14), 0) as [Скидка],
		round(isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD, 2) as [Средняя_выручка_в_сутки],
		round((isnull(SUM(t.F1), 0) + isnull(SUM(t.F18), 0)) / 2, 2) as [Средний_запас_на_конец_периода],
		round(((isnull(SUM(t.F1), 0) + isnull(SUM(t.F18), 0)) / 2) / (case when isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD != 0 then isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD else 1 end), 2) as [Время_обращения_запаса_в_днях],
		case 
		   when @print_prognoz = 1 then round(isnull(SUM(t.F8), 0) / @COUNT_DAY_PERIOD, 2) * @COUNT_DAY_OF_MONTH
		   else 0
		end as [Ожидаемая_выручка]
	from #T_ALL t
	--where (@ALL_CONTRACTOR = 1 or t.F19 in (select name from #CONTRACTOR))
	group by case when @ALL_CONTRACTOR = 1 then t.F19 else '-- По выбранным аптекам' end, t.F20
	order by [Аптека], [Показатель]
		

RETURN 0
GO

--exec REPEX_WEEKLY_REPORT_NZ '<XML><DATE_FR>2012-05-01</DATE_FR><DATE_TO>2012-06-01</DATE_TO><ID_CONTRACTOR>5315</ID_CONTRACTOR></XML>'
--exec REPEX_WEEKLY_REPORT_NZ '<XML><DATE_FR>2012-05-01</DATE_FR><DATE_TO>2012-06-01</DATE_TO></XML>'

