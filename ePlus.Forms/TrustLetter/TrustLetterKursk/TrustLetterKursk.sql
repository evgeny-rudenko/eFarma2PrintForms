IF (OBJECT_ID('REP_TRUST_LETTER_KURSK') IS NULL) EXEC ('CREATE PROCEDURE REP_TRUST_LETTER_KURSK AS RETURN')
GO
ALTER PROCEDURE REP_TRUST_LETTER_KURSK
AS

    DECLARE @ALL_CAT_LGOT UNIQUEIDENTIFIER
    SET @ALL_CAT_LGOT = NEWID()

    IF OBJECT_ID('tempdb..#CL') IS NOT NULL DROP TABLE #CL
    SELECT  COLUMN_CODE = @ALL_CAT_LGOT, COLUMN_NAME = 'ВСЕГО, в т.ч.:'
    INTO #CL
    UNION ALL
    SELECT 
        ID_DISCOUNT2_CAT_LGOT_GLOBAL,
        NAME 
    FROM DISCOUNT2_CAT_LGOT 
    WHERE DATE_DELETED IS NULL   

    SELECT COLUMN_NAME FROM #CL

    IF OBJECT_ID('tempdb..#RW') IS NOT NULL DROP TABLE #RW
    SELECT ROW_NAME = 'Обслужено рецептов (шт.)'
    INTO #RW
    UNION ALL
    SELECT 'Отпущено ЛП на сумму (тыс.руб.)'
    UNION ALL
    SELECT 'Кол-во рецептов на отсроченном обеспечении (шт.)'
    UNION ALL
    SELECT 'Кол-во рецептов, срок действия которых истек в период нахождения на остроченном обеспечении (шт.)'

    SELECT * FROM #RW

    IF OBJECT_ID('tempdb..#RES') IS NOT NULL DROP TABLE #RES
    SELECT COLUMN_NAME = CL.COLUMN_NAME, ROW_NAME = RW.ROW_NAME, VALUE = COUNT(*)
    INTO #RES
    FROM TRUST_LETTER AS TL
    INNER JOIN TRUST_LETTER_ITEM AS TLI ON TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
    INNER JOIN #CL CL ON CL.COLUMN_CODE = TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL
    INNER JOIN #RW RW ON RW.ROW_NAME = 'Обслужено рецептов (шт.)'
    WHERE TL.DOCUMENT_STATE = 'PROC'
    GROUP BY CL.COLUMN_NAME, RW.ROW_NAME
    UNION ALL
    SELECT CL.COLUMN_NAME, RW.ROW_NAME, VALUE = SUM(TL.SUMM_PAY_CREDIT)/1000
    FROM TRUST_LETTER AS TL
    INNER JOIN #CL CL ON CL.COLUMN_CODE = TL.ID_DISCOUNT2_CAT_LGOT_GLOBAL
    INNER JOIN #RW RW ON RW.ROW_NAME = 'Отпущено ЛП на сумму (тыс.руб.)'
    WHERE TL.DOCUMENT_STATE = 'PROC'
    GROUP BY CL.COLUMN_NAME, RW.ROW_NAME

    
    SELECT COLUMN_NAME = 'ВСЕГО, в т.ч.:',R.ROW_NAME, SUM_VALUE = SUM(VALUE)
    FROM #RES R
    GROUP BY R.ROW_NAME
    UNION ALL
    SELECT COLUMN_NAME, ROW_NAME, VALUE
    FROM #RES
    UNION ALL
    SELECT COLUMN_NAME, T.ROW_NAME, NULL
    FROM #CL
    CROSS JOIN
    (
        SELECT ROW_NAME = 'Кол-во рецептов на отсроченном обеспечении (шт.)'
        UNION ALL
        SELECT 'Кол-во рецептов, срок действия которых истек в период нахождения на остроченном обеспечении (шт.)'
    ) AS T
    

GO

