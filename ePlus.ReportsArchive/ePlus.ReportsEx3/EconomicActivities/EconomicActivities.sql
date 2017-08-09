SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF (OBJECT_ID('DBO.REPEX_ECONOMIC_ACTIVITIES') IS NULL) EXEC ('CREATE PROCEDURE DBO.REPEX_ECONOMIC_ACTIVITIES AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_ECONOMIC_ACTIVITIES
    @XMLPARAM NTEXT
AS
    DECLARE @HDOC INT,
            @DATE_FROM DATETIME,
            @DATE_TO DATETIME,
            @ID_CONTRACTOR BIGINT

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLPARAM
        SELECT
            @DATE_FROM = DATE_FROM,
            @DATE_TO = DATE_TO,
            @ID_CONTRACTOR  = ID_CONTRACTOR
        FROM OPENXML(@HDOC, '/XML', 2) WITH(
            DATE_FROM DATETIME,
            DATE_TO DATETIME,
            ID_CONTRACTOR BIGINT
        )
    EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC USP_RANGE_DAYS @DATE_FROM OUT, @DATE_TO OUT
EXEC USP_RANGE_NORM @DATE_FROM OUT, @DATE_TO OUT
SET @DATE_TO = CONVERT(DATETIME, CONVERT(MONEY, @DATE_TO) - 0.0001)

-- ����������� ��� � ������ �� ��� � �������� �������
DECLARE @CAL_DAY TABLE(
    CAL_DAY DATETIME,
    SUM_SUP MONEY,
    CHEQUE_SUM_SUP MONEY,
    CHEQUE_SUM_SAL MONEY,
    COUNT_CHEQUE_SALE INT,
    COUNT_CHEQUE_RETURN INT,
    COUNT_CHEQUE INT
    )
DECLARE @DATE_COUNTER DATETIME
SET @DATE_COUNTER = CONVERT(DATETIME, FLOOR(CONVERT(MONEY, @DATE_FROM)))
WHILE @DATE_COUNTER <= CONVERT(DATETIME, FLOOR(CONVERT(MONEY, @DATE_TO)))
BEGIN
    INSERT INTO @CAL_DAY(CAL_DAY)
    SELECT @DATE_COUNTER

    SET @DATE_COUNTER = DATEADD(dd, 1, @DATE_COUNTER)
END
-- declare @T datetime
-- set @T = getdate()
-- select DATEDIFF(ss, @T, getdate())

-- ������ �� ����� �� ������ �� ��������:
DECLARE @CHEQUE_DATA TABLE (
    ID_ROW INT IDENTITY PRIMARY KEY,
    DOC_DAY DATETIME,
    CHEQUE_SUM_SUP MONEY,
    CHEQUE_SUM_SAL MONEY,
    COUNT_CHEQUE INT,   
    CHEQUE_TYPE VARCHAR(10)
    )

INSERT @CHEQUE_DATA (
    DOC_DAY,
    CHEQUE_SUM_SUP, 
    CHEQUE_SUM_SAL, 
    COUNT_CHEQUE, 
    CHEQUE_TYPE)
SELECT
    CHEQUE_DAY,
    CHEQUE_SUM_SUP = SUM(SUM_SUP),
    CHEQUE_SUM_SAL = SUM(SUM_SAL),
    COUNT_CHEQUE = SUM(CHEQUE_COUNT),
    CHEQUE_TYPE
FROM CASH_SESSION_CHEQUE_SUM
WHERE   ID_STORE IN (SELECT ID_STORE FROM STORE WHERE ID_CONTRACTOR = @ID_CONTRACTOR)
    AND CHEQUE_DAY BETWEEN @DATE_FROM AND @DATE_TO
    AND CHEQUE_TYPE IN ('SALE', 'RETURN')
    AND CHEQUE_STATE = 'PROC'
GROUP BY CHEQUE_DAY, CHEQUE_TYPE

-- ��� ������
UPDATE CD SET
    CD.SUM_SUP = INV.SUM_SUP
FROM @CAL_DAY CD
    INNER JOIN (
        SELECT 
            INV_DAY = CONVERT(DATETIME, FLOOR(CONVERT(MONEY, I.DOCUMENT_DATE))),
            SUM_SUP = SUM(SUM_SUPPLIER)
        FROM INVOICE I 
            INNER JOIN STORE S ON S.ID_STORE = I.ID_STORE
        WHERE I.DOCUMENT_STATE = 'PROC'
          AND S.ID_CONTRACTOR = @ID_CONTRACTOR
        GROUP BY CONVERT(DATETIME, FLOOR(CONVERT(MONEY, I.DOCUMENT_DATE))))
    INV
    ON CD.CAL_DAY = INV.INV_DAY
-- ������� ����� �� �����
UPDATE CD SET
    CD.CHEQUE_SUM_SUP = (SELECT SUM(CASE WHEN CHEQUE_TYPE = 'RETURN' THEN CHEQUE_SUM_SUP * -1 ELSE CHEQUE_SUM_SUP END)
                         -- SUM(CHD.CHEQUE_SUM_SUP)
                         FROM @CHEQUE_DATA CHD 
                         WHERE  CD.CAL_DAY = CHD.DOC_DAY /*AND CHEQUE_TYPE = 'SALE'*/)
FROM @CAL_DAY CD
-- ��������� ����� �� �����
UPDATE CD SET
    CD.CHEQUE_SUM_SAL = (SELECT SUM(CASE WHEN CHEQUE_TYPE = 'RETURN' THEN CHEQUE_SUM_SAL * -1 ELSE CHEQUE_SUM_SAL END)
                         -- SELECT SUM(CHD.CHEQUE_SUM_SAL)
                         FROM @CHEQUE_DATA CHD 
                         WHERE  CD.CAL_DAY = CHD.DOC_DAY /*AND CHEQUE_TYPE = 'SALE'*/)
FROM @CAL_DAY CD
-- ���-�� �����: �������
UPDATE CD SET
    CD.COUNT_CHEQUE_SALE = (SELECT SUM(CHD.COUNT_CHEQUE)
                            FROM @CHEQUE_DATA CHD 
                            WHERE  CD.CAL_DAY = CHD.DOC_DAY AND CHD.CHEQUE_TYPE = 'SALE')
FROM @CAL_DAY CD
-- ���-�� �����: �������
UPDATE CD SET
    CD.COUNT_CHEQUE_RETURN = (SELECT SUM(CHD.COUNT_CHEQUE)
                              FROM @CHEQUE_DATA CHD 
                              WHERE  CD.CAL_DAY = CHD.DOC_DAY AND CHD.CHEQUE_TYPE = 'RETURN')
FROM @CAL_DAY CD
-- ����� ���-�� �����
UPDATE @CAL_DAY
SET COUNT_CHEQUE = ISNULL(COUNT_CHEQUE_SALE, 0) -- + ISNULL(COUNT_CHEQUE_RETURN, 0)

-- �������������� SELECT-�
-- ������ �� �����:
SELECT
    [DATE] = CAL_DAY,
    SUM_SUP,
    CHEQUE_SUM_SUP,
    CHEQUE_SUM_SAL,
    [PERCENT] = CASE WHEN CHEQUE_SUM_SUP = 0 THEN 0 ELSE (CHEQUE_SUM_SAL - CHEQUE_SUM_SUP) / CHEQUE_SUM_SUP * 100 END ,
    DELTA = CHEQUE_SUM_SAL - CHEQUE_SUM_SUP,
    COUNT_CHEQUE,
    AVG_CHEQUE_PRICE = CASE WHEN COUNT_CHEQUE = 0 THEN 0 ELSE CHEQUE_SUM_SAL / COUNT_CHEQUE END
FROM @CAL_DAY 

-- �������� ���� � ������
DECLARE @DATE_TO_DAY DATETIME
SET @DATE_TO_DAY = CONVERT(DATETIME, FLOOR(CONVERT(MONEY, @DATE_TO)))

-- ������ �� �������� �� ������� ��������� �������:
SELECT 
    [DATE] = CAL_DAY,
    SUM_SUP,
    SUM_SAL,
    DELTA = SUM_SAL - SUM_SUP,
    [PERCENT] = CASE WHEN SUM_SAL = 0 THEN 0 ELSE (SUM_SAL - SUM_SUP) / SUM_SAL * 100 END
FROM(
    SELECT
        CAL_DAY = @DATE_FROM,
        SUM_SUP = SUM(SUM_SUP * SIGN_OP),
        -- SUM_SAL = SUM(SUM_ACC * SIGN_OP)
        SUM_SAL = SUM(CASE WHEN DM.CODE_OP='DIS' THEN SUM_ACC * -1 ELSE SUM_ACC * SIGN_OP END)
    FROM DOC_MOVEMENT DM
        INNER JOIN STORE S ON S.ID_STORE = DM.ID_STORE
    WHERE DATE_OP < @DATE_FROM
      AND S.ID_CONTRACTOR = @ID_CONTRACTOR
    
    UNION ALL
    
    SELECT
        CAL_DAY = @DATE_TO_DAY,
        SUM_SUP = SUM(SUM_SUP * SIGN_OP),
        -- SUM_SAL = SUM(SUM_ACC * SIGN_OP)
       SUM_SAL = SUM(CASE WHEN DM.CODE_OP='DIS' THEN SUM_ACC * -1 ELSE SUM_ACC * SIGN_OP END)
    FROM DOC_MOVEMENT DM
        INNER JOIN STORE S ON S.ID_STORE = DM.ID_STORE
    WHERE DATE_OP <= @DATE_TO
      AND S.ID_CONTRACTOR = @ID_CONTRACTOR
    ) REM
ORDER BY CAL_DAY ASC

RETURN 
GO  



