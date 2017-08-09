IF (OBJECT_ID('DBO.FN_REPEX_RCY_CONVERT_RIGLA_BARCODE_GEN') IS NULL) EXEC ('CREATE FUNCTION DBO.FN_REPEX_RCY_CONVERT_RIGLA_BARCODE_GEN(@BARCODE VARCHAR(12)) RETURNS VARCHAR(13) AS BEGIN RETURN NULL END')
GO

ALTER FUNCTION DBO.FN_REPEX_RCY_CONVERT_RIGLA_BARCODE_GEN(
    @BARCODE VARCHAR(12)
)
RETURNS VARCHAR(13)
AS
BEGIN
    DECLARE @BARCODELEN INT
    SET @BARCODELEN = LEN(@BARCODE)
    IF (@BARCODELEN<>12)
        GOTO ERR
    
     -- ���������� ����������� ����� ��� �����-����  
    DECLARE @BARCODECRC AS INT  
    DECLARE @SYMBCOUNTER AS INT  
    DECLARE @PARITYSUMM AS INT  
    DECLARE @NONPARITYSUMM AS INT  
       
    SET @SYMBCOUNTER = 1  
    SET @PARITYSUMM = 0  
    SET @NONPARITYSUMM = 0  

    DECLARE @CASTEDINT INT
    SET @CASTEDINT = NULL 
    DECLARE @TOCAST CHAR
    SET @TOCAST = NULL

     -- ��������� ����� ���� �������� ���������  
    WHILE @SYMBCOUNTER <= @BARCODELEN  
    BEGIN  
        SET @TOCAST = SUBSTRING(@BARCODE, @SYMBCOUNTER, 1)
        SET @CASTEDINT = CASE WHEN ISNUMERIC(@TOCAST)=1 THEN CAST(SUBSTRING(@BARCODE, @SYMBCOUNTER, 1) AS INT) ELSE NULL END
        IF (@CASTEDINT IS NULL)
            GOTO ERR
        SET @NONPARITYSUMM = @NONPARITYSUMM + @CASTEDINT  
        SET @SYMBCOUNTER = @SYMBCOUNTER + 2
    END  
    
    SET @SYMBCOUNTER = 2
    
    -- ��������� ����� ���� ������ ���������  
    WHILE @SYMBCOUNTER <= @BARCODELEN 
    BEGIN  
        SET @TOCAST = SUBSTRING(@BARCODE, @SYMBCOUNTER, 1)
        SET @CASTEDINT = CASE WHEN ISNUMERIC(@TOCAST)=1 THEN CAST(SUBSTRING(@BARCODE, @SYMBCOUNTER, 1) AS INT) ELSE NULL END
        IF (@CASTEDINT IS NULL)
            GOTO ERR

        SET @PARITYSUMM = @PARITYSUMM + @CASTEDINT  
        SET @SYMBCOUNTER = @SYMBCOUNTER + 2  
    END  

    SET @BARCODECRC = @PARITYSUMM * 3 + @NONPARITYSUMM  
    IF @BARCODECRC % 10 = 0  
        SET @BARCODECRC = 0  
    ELSE  
        SET @BARCODECRC = 10 - (@BARCODECRC % 10)  
    
    RETURN @BARCODE + CAST(@BARCODECRC AS VARCHAR)  
ERR:
    RETURN NULL
END
GO


IF (OBJECT_ID('REPEX_RCY_CONVERT_RIGLA_DATA_CONVERT') IS NULL) EXEC ('CREATE PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_CONVERT AS RETURN')
GO
ALTER PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_CONVERT(
    @XMLDATA NTEXT
)
AS
    DECLARE @HDOC INT
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
    DECLARE @T TABLE(
        ID_GLOBAL UNIQUEIDENTIFIER,
        CODE_STU VARCHAR(20),
        KOL MONEY,
        MAN_PRICE MONEY,
        COST_PRICE MONEY,
        SALE_PRICE MONEY
    )

    INSERT INTO @T(
        ID_GLOBAL,
        CODE_STU,
        KOL,
        MAN_PRICE,
        COST_PRICE,
        SALE_PRICE
    )
    SELECT
        ID_GLOBAL,
        CODE_STU,
        KOL,
        MAN_PRICE, 
        COST_PRICE,
        SALE_PRICE
    FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        CODE_STU VARCHAR(20) 'CODE_STU',
        KOL MONEY 'KOL',
        MAN_PRICE MONEY 'MAN_PRICE',
        COST_PRICE MONEY 'COST_PRICE',
        SALE_PRICE MONEY 'SALE_PRICE'
    )
    WHERE (KOL - FLOOR(KOL))>0

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    DECLARE @ID_CONTRACTOR BIGINT
    SELECT TOP 1 @ID_CONTRACTOR = ID_CONTRACTOR
    FROM CONTRACTOR
    WHERE DATE_DELETED IS NULL
    AND NAME = '���'

    SELECT
        T.ID_GLOBAL,
        KOL = ROUND(((T.KOL - FLOOR(T.KOL)) * A.DENOMINATOR),0),
        Q_FIRST = A.DENOMINATOR,
        MAN_PRICE = T.MAN_PRICE / A.DENOMINATOR,
        SALE_PRICE = T.SALE_PRICE / A.DENOMINATOR,
        COST_PRICE = T.COST_PRICE / A.DENOMINATOR
    FROM (SELECT
              T.ID_GLOBAL,
              DENOMINATOR = MIN(SR.DENOMINATOR)
          FROM @T T
          INNER JOIN (SELECT 
                          ID_GOODS = MIN(ID_GOODS),
                          GC.CODE
                      FROM GOODS_CODE GC
                      WHERE GC.ID_CONTRACTOR = @ID_CONTRACTOR
                      AND GC.DATE_DELETED IS NULL
                      GROUP BY CODE) GC ON GC.CODE = T.CODE_STU 
          INNER JOIN GOODS G ON G.ID_GOODS = GC.ID_GOODS 
--                            AND G.DATE_EXCLUDED IS NULL
          INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = G.ID_GOODS
                                     AND SR.DATE_DELETED IS NULL
                                     AND SR.DENOMINATOR > 1
          WHERE ABS(((T.KOL - FLOOR(T.KOL)) * SR.DENOMINATOR) - ROUND(((T.KOL - FLOOR(T.KOL)) * SR.DENOMINATOR), 0))<=0.1
          GROUP BY T.ID_GLOBAL) A
    INNER JOIN @T T ON T.ID_GLOBAL = A.ID_GLOBAL
RETURN
GO

-- EXEC REPEX_RCY_CONVERT_RIGLA_DATA_CONVERT NULL

IF (OBJECT_ID('REPEX_RCY_CONVERT_RIGLA_DATA_CHECK') IS NULL) EXEC('CREATE PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_CHECK AS RETURN')
GO
ALTER PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_CHECK(
    @XMLDATA NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @T TABLE(
      ID_GLOBAL UNIQUEIDENTIFIER,
      CODE_STU VARCHAR(40)
    )

    DECLARE @CS TABLE(
        ID_GLOBAL UNIQUEIDENTIFIER,
        SUPPLIERID INT,
        SUPIDAP INT
    )

    DECLARE @RESULT TABLE(
        ID_GLOBAL UNIQUEIDENTIFIER,
        TEXT VARCHAR(4000)    
    )

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
    INSERT INTO @T(
      ID_GLOBAL,
      CODE_STU
    )
    SELECT
      ID_GLOBAL,
      CODE_STU
    FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        CODE_STU VARCHAR(40) 'CODE_STU'
    )

    INSERT INTO @CS(
        ID_GLOBAL,
        SUPPLIERID,
        SUPIDAP
    )
    SELECT
        ID_GLOBAL,
        SUPPLIERID,
        SUPIDAP
    FROM OPENXML(@HDOC, '/XML/CROSS_SUP') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        SUPPLIERID INT 'SUPPLIERID',
        SUPIDAP INT 'SUPIDAP'
    )

    EXEC SP_XML_REMOVEDOCUMENT @HDOC

    DECLARE @ID_CONTRACTOR BIGINT
    SELECT TOP 1 @ID_CONTRACTOR = ID_CONTRACTOR
    FROM CONTRACTOR
    WHERE DATE_DELETED IS NULL
    AND NAME = '���'

    DECLARE @ID_CONTRACTOR_GROUP BIGINT
    SELECT TOP 1 @ID_CONTRACTOR_GROUP = ID_CONTRACTOR_GROUP
    FROM CONTRACTOR_GROUP
    WHERE DATE_DELETED IS NULL
    AND ID_PARENT_GROUP IS NULL
    AND MNEMOCODE = 'DISTRIBUTOR'

    INSERT INTO @RESULT(
        ID_GLOBAL,
        TEXT
    )
    SELECT
        T.ID_GLOBAL,
        '� ��2 ����������� ��� ���'
    FROM @T T
    WHERE (NOT EXISTS (SELECT NULL
                      FROM GOODS_CODE GC
                      WHERE GC.DATE_DELETED IS NULL
                      AND GC.CODE = T.CODE_STU
                      AND GC.ID_CONTRACTOR = @ID_CONTRACTOR)
        AND @ID_CONTRACTOR IS NOT NULL)
    OR @ID_CONTRACTOR IS NULL
    UNION
    SELECT
        T.ID_GLOBAL,
        '���� ��� ������������ ��������� ����� ��'
    FROM @T T
    WHERE EXISTS (SELECT NULL
                  FROM GOODS_CODE GC
                  WHERE GC.DATE_DELETED IS NULL
                  AND GC.CODE = T.CODE_STU
                  AND GC.ID_CONTRACTOR = @ID_CONTRACTOR
                  GROUP BY GC.CODE
                  HAVING COUNT(*)>1)
    AND @ID_CONTRACTOR IS NOT NULL
    UNION
    SELECT
        T.ID_GLOBAL,
        '� ��2 �� ���� �� �� ������ ��������� ������'
    FROM @CS T
    WHERE (NOT EXISTS(SELECT NULL
                      FROM CONTRACTOR C
                      WHERE 1=1
                      AND C.A_COD = T.SUPIDAP)
    AND T.SUPIDAP IS NOT NULL)
    OR T.SUPIDAP IS NULL
    UNION
    SELECT
        T.ID_GLOBAL,
        '��������� ������� �� ��������'
    FROM @CS T
    WHERE EXISTS(SELECT NULL
                 FROM CONTRACTOR C
                 WHERE 1=1
                 AND C.A_COD = T.SUPIDAP
                 AND C.DATE_DELETED IS NOT NULL)
    AND T.SUPIDAP IS NOT NULL
    
    SELECT ID_GLOBAL, TEXT FROM @RESULT
RETURN
GO

IF (OBJECT_ID('REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT') IS NULL) EXEC ('CREATE PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT AS RETURN')
GO
ALTER PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT(
    @XMLDATA NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @ID_STORE BIGINT
    DECLARE @ENVD BIT
    DECLARE @USE_VAT_FROM_EFARMA BIT
    DECLARE @IMPORT_FORMAT VARCHAR(40)

    DECLARE @T TABLE(
        ID_GOODS BIGINT,
        ID_CONTRACTOR BIGINT,
        ID_SCALING_RATIO BIGINT,
        ID_SERIES UNIQUEIDENTIFIER, 

        ID_GLOBAL UNIQUEIDENTIFIER,
        NAME VARCHAR(300),
        KOL MONEY,
        BARCODE VARCHAR(300),
        SERIY VARCHAR(300),
        SROK_GODN DATETIME,
        NUM_SERT VARCHAR(300),
        GTD VARCHAR(300),
        Q_FIRST INT,
        MAN_PRICE MONEY,
        COST_PRICE MONEY,
        SALE_PRICE MONEY,
        NDS MONEY,
        SUPPLIERID INT,
        DOC_DATE DATETIME,
        DOC_NUM VARCHAR(300),
        CODE_STU VARCHAR(40),
        NUM_SF VARCHAR(50),
        DATA_SF DATETIME    
    )

    DECLARE @CS TABLE(
        ID_CONTRACTOR BIGINT,

        ID_GLOBAL UNIQUEIDENTIFIER,
        SUPPLIERID INT,
        SUPIDAP INT
    )

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
        
    SELECT 
        @ID_STORE = S.ID_STORE
    FROM OPENXML(@HDOC, '/XML/ID_STORE') WITH(
        ID_STORE BIGINT '.'
    ) A
    INNER JOIN STORE S ON S.ID_STORE = A.ID_STORE

    SELECT 
        @ENVD = ENVD,
        @IMPORT_FORMAT = IMPORT_FORMAT,
        @USE_VAT_FROM_EFARMA = USE_VAT_FROM_EFARMA
    FROM OPENXML(@HDOC, '/XML') WITH(
        ENVD BIGINT 'ENVD',
        IMPORT_FORMAT VARCHAR(40) 'IMPORT_FORMAT',
        USE_VAT_FROM_EFARMA BIGINT 'USE_VAT_FROM_EFARMA'
    ) A

    INSERT INTO @T(
        ID_GLOBAL,
        NAME,
        KOL,
        BARCODE,
        SERIY,
        SROK_GODN,
        NUM_SERT,
        GTD,
        Q_FIRST,
        MAN_PRICE,
        COST_PRICE,
        SALE_PRICE,
        NDS,
        SUPPLIERID,
        DOC_DATE,
        DOC_NUM,
        CODE_STU,
        NUM_SF,
        DATA_SF
    )    
    SELECT 
        ID_GLOBAL,
        NAME,
        KOL,
        BARCODE,
        SERIY,
        SROK_GODN,
        NUM_SERT,
        GTD,
        Q_FIRST,
        MAN_PRICE,
        COST_PRICE,
        SALE_PRICE,
        ISNULL(NDS,0),
        SUPPLIERID,
        DOC_DATE,
        DOC_NUM,
        CODE_STU,
        NUM_SF,
        DATA_SF
    FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        NAME VARCHAR(300) 'NAME',
        KOL MONEY 'KOL',
        BARCODE VARCHAR(300) 'BARCODE',
        SERIY VARCHAR(300) 'SERIY',
        SROK_GODN DATETIME 'SROK_GODN',
        NUM_SERT VARCHAR(300) 'NUM_SERT',
        GTD VARCHAR(300) 'GTD',
        Q_FIRST INT 'Q_FIRST',
        MAN_PRICE MONEY 'MAN_PRICE',
        COST_PRICE MONEY 'COST_PRICE',
        SALE_PRICE MONEY 'SALE_PRICE',
        NDS MONEY 'NDS',
        SUPPLIERID INT 'SUPPLIERID',
        DOC_DATE DATETIME 'DOC_DATE',
        DOC_NUM VARCHAR(300) 'DOC_NUM',
        CODE_STU VARCHAR(40) 'CODE_STU',
        NUM_SF VARCHAR(50) 'NUM_SF',
        DATA_SF DATETIME 'DATA_SF'   
    )

    INSERT INTO @CS(
        ID_GLOBAL,
        SUPPLIERID,
        SUPIDAP
    )
    SELECT
        ID_GLOBAL,
        SUPPLIERID,
        SUPIDAP
    FROM OPENXML(@HDOC, '/XML/CROSS_SUP') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
        SUPPLIERID INT 'SUPPLIERID',
        SUPIDAP INT 'SUPIDAP'
    )
   
    EXEC SP_XML_REMOVEDOCUMENT @HDOC

DECLARE @ID_CONTRACTOR_GROUP BIGINT
SELECT @ID_CONTRACTOR_GROUP = ID_CONTRACTOR_GROUP
FROM CONTRACTOR_GROUP
WHERE DATE_DELETED IS NULL
AND MNEMOCODE = 'DISTRIBUTOR'
AND ID_PARENT_GROUP IS NULL

DECLARE @ID_CONTRACTOR BIGINT
SELECT TOP 1 @ID_CONTRACTOR = ID_CONTRACTOR
FROM CONTRACTOR 
WHERE NAME = '���'
AND DATE_DELETED IS NULL

UPDATE CS SET
    ID_CONTRACTOR = C.ID_CONTRACTOR
FROM @CS CS
INNER JOIN (SELECT
                ID_CONTRACTOR,
                A_COD
            FROM CONTRACTOR C
--             WHERE EXISTS (SELECT NULL
--                           FROM dbo.FN_CONTRACTOR_GROUP_CHILD_GROUPS(@ID_CONTRACTOR_GROUP) A
--                           INNER JOIN CONTRACTOR_GROUP CG ON CG.ID_CONTRACTOR_GROUP = A.ID_CONTRACTOR_GROUP
--                                                         AND CG.DATE_DELETED IS NULL
--                           INNER JOIN CONTRACTOR_2_CONTRACTOR_GROUP C2CG ON C2CG.ID_CONTRACTOR_GROUP = CG.ID_CONTRACTOR_GROUP
--                                                                        AND C2CG.DATE_DELETED IS NULL
--                           WHERE C2CG.ID_CONTRACTOR = C.ID_CONTRACTOR)
            WHERE 1=1
            AND C.DATE_DELETED IS NULL) C ON C.A_COD = CS.SUPIDAP
    
UPDATE T SET
    ID_GOODS = G.ID_GOODS
FROM @T T
INNER JOIN (SELECT
                ID_GOODS = MIN(CG.ID_GOODS),
                CG.CODE    
            FROM GOODS_CODE CG
            INNER JOIN GOODS G ON G.ID_GOODS = CG.ID_GOODS
            WHERE CG.DATE_DELETED IS NULL
            AND CG.ID_CONTRACTOR = @ID_CONTRACTOR
--            AND G.DATE_EXCLUDED IS NULL
            GROUP BY CG.CODE) G ON G.CODE = T.CODE_STU

UPDATE T SET
    ID_CONTRACTOR = C.ID_CONTRACTOR
FROM @T T
INNER JOIN (SELECT
                ID_CONTRACTOR = MIN(ID_CONTRACTOR),
                SUPPLIERID
            FROM @CS CS
            GROUP BY SUPPLIERID) C ON C.SUPPLIERID = T.SUPPLIERID

UPDATE T SET
    ID_SCALING_RATIO = SR.ID_SCALING_RATIO
FROM @T T
INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = T.ID_GOODS
INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
WHERE T.ID_SCALING_RATIO IS NULL
AND T.ID_GOODS IS NOT NULL
AND T.ID_CONTRACTOR IS NOT NULL
AND SR.DATE_DELETED IS NULL
AND SR.NUMERATOR = 1
AND ((SR.DENOMINATOR = 1 AND ISNULL(NULLIF(T.Q_FIRST,0),1)=1 AND U.MNEMOCODE = 'BOX')
     OR
     (SR.DENOMINATOR <> 1 AND T.Q_FIRST > 1 AND SR.DENOMINATOR = T.Q_FIRST AND U.MNEMOCODE = 'PBOX'))

DECLARE @ERROR INT, @ROWCOUNT INT

BEGIN TRAN    

------------- BOX
UPDATE U SET
    DATE_DELETED = NULL
FROM UNIT U
WHERE U.MNEMOCODE = 'BOX'
AND U.DATE_DELETED IS NOT NULL

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
-- 
IF (@ERROR<>0)
    GOTO ERR

INSERT INTO UNIT(
    NAME,
    DESCRIPTION,
    MNEMOCODE,
    SHORT_NAME,
    DATE_MODIFIED
)
SELECT
    '��������',
    '��������',
    'BOX',
    '��.',
    GETDATE()
WHERE NOT EXISTS (SELECT NULL
                  FROM UNIT U
                  WHERE U.MNEMOCODE = 'BOX')
    
SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
-- 
IF (@ERROR<>0)
    GOTO ERR

UPDATE SR SET
    DATE_DELETED = NULL
FROM SCALING_RATIO SR
INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
INNER JOIN @T T ON T.ID_GOODS = SR.ID_GOODS
WHERE T.ID_GOODS IS NOT NULL
AND T.ID_CONTRACTOR IS NOT NULL
AND SR.DATE_DELETED IS NOT NULL
AND SR.NUMERATOR = 1
AND U.MNEMOCODE = 'BOX' AND ISNULL(NULLIF(T.Q_FIRST,0),1) = 1 AND SR.DENOMINATOR = 1

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
-- 
IF (@ERROR<>0)
    GOTO ERR

INSERT INTO SCALING_RATIO(
    NUMERATOR,
    DENOMINATOR,
    ID_UNIT,
    ID_GOODS,
    DATE_MODIFIED    
)
SELECT DISTINCT
    1,
    1,
    U.ID_UNIT,
    T.ID_GOODS,
    GETDATE()
FROM @T T,
UNIT U
WHERE U.MNEMOCODE = 'BOX'
AND ISNULL(NULLIF(T.Q_FIRST,0),1) = 1
AND T.ID_GOODS IS NOT NULL
AND T.ID_CONTRACTOR IS NOT NULL
AND NOT EXISTS (SELECT NULL
                FROM SCALING_RATIO SR
                WHERE SR.ID_UNIT = U.ID_UNIT
                AND SR.ID_GOODS = T.ID_GOODS
                AND SR.NUMERATOR = 1
                AND SR.DENOMINATOR = 1)

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
-- 
IF (@ERROR<>0)
    GOTO ERR
-------------------
------------- PBOX-
IF (ISNULL(@IMPORT_FORMAT,'A2000') = 'A2000') BEGIN
    UPDATE U SET
        DATE_DELETED = NULL
    FROM UNIT U
    WHERE U.MNEMOCODE = 'PBOX'
    AND U.DATE_DELETED IS NOT NULL
    
    SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
    -- 
    IF (@ERROR<>0)
        GOTO ERR
    
    INSERT INTO UNIT(
        NAME,
        DESCRIPTION,
        MNEMOCODE,
        SHORT_NAME,
        DATE_MODIFIED
    )
    SELECT
        '��������� ��������',
        '��������� ��������',
        'PBOX',
        '����. ��.',
        GETDATE()
    WHERE NOT EXISTS (SELECT NULL
                      FROM UNIT U
                      WHERE U.MNEMOCODE = 'PBOX')
        
    SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
    -- 
    IF (@ERROR<>0)
        GOTO ERR


    UPDATE SR SET
        DATE_DELETED = NULL
    FROM SCALING_RATIO SR
    INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
    INNER JOIN @T T ON T.ID_GOODS = SR.ID_GOODS
    WHERE T.ID_GOODS IS NOT NULL
    AND T.ID_CONTRACTOR IS NOT NULL
    AND SR.DATE_DELETED IS NOT NULL
    AND SR.NUMERATOR = 1
    AND U.MNEMOCODE = 'PBOX' 
    AND T.Q_FIRST > 1
    AND SR.DENOMINATOR = T.Q_FIRST
    
    SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
    -- 
    IF (@ERROR<>0)
        GOTO ERR
    
    INSERT INTO SCALING_RATIO(
        NUMERATOR,
        DENOMINATOR,
        ID_UNIT,
        ID_GOODS,
        DATE_MODIFIED    
    )
    SELECT DISTINCT
        1,
        T.Q_FIRST,
        U.ID_UNIT,
        T.ID_GOODS,
        GETDATE()
    FROM @T T,
    UNIT U
    WHERE U.MNEMOCODE = 'PBOX'
    AND T.Q_FIRST > 1
    AND T.ID_GOODS IS NOT NULL
    AND T.ID_CONTRACTOR IS NOT NULL
    AND NOT EXISTS (SELECT NULL
                    FROM SCALING_RATIO SR
                    WHERE SR.ID_UNIT = U.ID_UNIT
                    AND SR.ID_GOODS = T.ID_GOODS
                    AND SR.NUMERATOR = 1
                    AND SR.DENOMINATOR = T.Q_FIRST)
    
    SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
    -- 
    IF (@ERROR<>0)
        GOTO ERR
END     
-------------------

UPDATE T SET
    ID_SCALING_RATIO = SR.ID_SCALING_RATIO
FROM @T T
INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = T.ID_GOODS
INNER JOIN UNIT U ON U.ID_UNIT = SR.ID_UNIT
WHERE T.ID_SCALING_RATIO IS NULL
AND T.ID_GOODS IS NOT NULL
AND T.ID_CONTRACTOR IS NOT NULL
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL
AND SR.DATE_DELETED IS NULL
AND SR.NUMERATOR = 1
AND ((SR.DENOMINATOR = 1 AND ISNULL(NULLIF(T.Q_FIRST,0),1)=1 AND U.MNEMOCODE = 'BOX')
     OR
     (SR.DENOMINATOR <> 1 AND T.Q_FIRST > 1 AND SR.DENOMINATOR = T.Q_FIRST AND U.MNEMOCODE = 'PBOX'))

UPDATE T SET
    ID_SERIES = NEWID()
FROM @T T
WHERE T.ID_GOODS IS NOT NULL 
AND T.ID_CONTRACTOR IS NOT NULL
AND T.ID_SCALING_RATIO IS NOT NULL
AND (T.SERIY IS NOT NULL OR T.SROK_GODN IS NOT NULL OR T.NUM_SERT IS NOT NULL)
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL

INSERT INTO SERIES(
    MNEMOCODE,
    BEST_BEFORE,
    ID_GOODS,
    SERIES_NUMBER,
    DATE_MODIFIED,
    ID_SERIES_GLOBAL
)
SELECT
    CONVERT(VARCHAR(36), T.ID_SERIES), 
    T.SROK_GODN,
    T.ID_GOODS,
    T.SERIY,
    GETDATE(),
    T.ID_SERIES   
FROM @T T
WHERE T.ID_SERIES IS NOT NULL
    
SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0)
    GOTO ERR

INSERT INTO CERTIFICATE(
    DATE_MODIFIED,
   CERT_NUMBER,
    ID_SERIES,
    ID_SERIES_GLOBAL
)
SELECT
    GETDATE(),
    T.NUM_SERT,
    S.ID_SERIES,
    S.ID_SERIES_GLOBAL
FROM @T T
INNER JOIN SERIES S ON S.ID_SERIES_GLOBAL = T.ID_SERIES
WHERE T.NUM_SERT IS NOT NULL AND T.ID_SERIES IS NOT NULL

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0)
    GOTO ERR

DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
SET @ID_GLOBAL = NEWID()
DECLARE @MNEMOCODE VARCHAR(50)
EXEC USP_MNEMOCODE_GEN @MNEMOCODE OUT, 'IMPORT_REMAINS'

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0)
    GOTO ERR

INSERT INTO IMPORT_REMAINS(
    ID_IMPORT_REMAINS_GLOBAL,
    MNEMOCODE,
    ID_STORE,
    ID_PRICING_PRICING_MODEL,
    ID_STORE_PLACE,
    SUM_RETAIL,
    SVAT_RETAIL,
    SUM_SUPPLIER,
    SVAT_SUPPLIER,
    ID_USER,
    DOCUMENT_STATE,
    DOCUMENT_DATE,
    DATE_MODIFIED,
    COMMENT,
    ID_MOL
)
SELECT
    @ID_GLOBAL,
    @MNEMOCODE,
    @ID_STORE,
    (SELECT TOP 1 ID_PRICING_PRICING_MODEL FROM STORE WHERE ID_STORE = @ID_STORE),
    NULL,
    SUM(T.KOL * T.SALE_PRICE),
    SUM(T.KOL * (T.SALE_PRICE - (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD=1 THEN 0 ELSE TT.TAX_RATE END + 100.0000)))),
    SUM(T.KOL * T.COST_PRICE * (1+(CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000))),
    SUM(T.KOL * T.COST_PRICE * (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000)),
    NULL,
    'SAVE',
    GETDATE(),
    GETDATE(),
    '������������ �������������� � ���������� ����������� ������ �� '+ISNULL(@IMPORT_FORMAT,'A2000'),
    NULL
FROM @T T
INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
WHERE T.ID_CONTRACTOR IS NOT NULL 
AND T.ID_GOODS IS NOT NULL
AND T.ID_SCALING_RATIO IS NOT NULL
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL
AND ISNULL(@IMPORT_FORMAT, 'A2000')<>'A2000'
HAVING COUNT(*)>0
UNION
SELECT
    @ID_GLOBAL,
    @MNEMOCODE,
    @ID_STORE,
    (SELECT TOP 1 ID_PRICING_PRICING_MODEL FROM STORE WHERE ID_STORE = @ID_STORE),
    NULL,
    SUM(T.KOL * T.SALE_PRICE),
    SUM(T.KOL * (T.SALE_PRICE - (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD=1 THEN 0 ELSE TT.TAX_RATE END  + 100.0000)))),
    SUM(T.KOL * T.COST_PRICE * (1+(CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000))),
    SUM(T.KOL * T.COST_PRICE * (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000)),
    NULL,
    'SAVE',
    GETDATE(),
    GETDATE(),
    '������������ �������������� � ���������� ����������� ������ �� '+ISNULL(@IMPORT_FORMAT,'A2000'),
    NULL
FROM @T T
INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
WHERE T.ID_CONTRACTOR IS NOT NULL 
AND T.ID_GOODS IS NOT NULL
AND T.ID_SCALING_RATIO IS NOT NULL
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL
AND ISNULL(@IMPORT_FORMAT, 'A2000')='A2000'
HAVING COUNT(*)>0

    
SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0 OR @ROWCOUNT=0)
    GOTO ERR

INSERT INTO IMPORT_REMAINS_ITEM(
    ID_IMPORT_REMAINS_ITEM_GLOBAL,
    ID_IMPORT_REMAINS_GLOBAL,
    ID_GOODS,
    ID_SCALING_RATIO,
    QUANTITY,
    ID_SUPPLIER,
    PRODUCER_PRICE,
    SUPPLIER_VAT,
    SUPPLIER_ADPRICE,
    SUPPLIER_PRICE,
    SUPPLIER_PRICE_VAT,
    SUPPLIER_VAT_SUM,
    SUPPLIER_SUM,
    SUPPLIER_SUM_VAT,
    RETAIL_VAT,
    RETAIL_ADPRICE,
    RETAIL_PRICE,
    RETAIL_PRICE_VAT,
    RETAIL_VAT_SUM,
    RETAIL_SUM,
    RETAIL_SUM_VAT,
    INCOMING_BILL_NUMBER,
    INCOMING_BILL_DATE,
    ID_STORE_PLACE,
    GTD_NUMBER,
    ID_REG_CERT_GLOBAL,
    ID_SERIES_GLOBAL,
    BARCODE,
    INCOMING_NUMBER,
    INCOMING_DATE,
    VAT,
    CODE_STU
)
SELECT
    NEWID(),
    @ID_GLOBAL,
    T.ID_GOODS,
    T.ID_SCALING_RATIO,
    T.KOL,
    T.ID_CONTRACTOR,
    PRODUCER_PRICE = T.MAN_PRICE,
    SUPPLIER_VAT = TT.TAX_RATE,
    SUPPLIER_ADPRICE = CASE WHEN ISNULL(T.MAN_PRICE,0) =0 THEN 0 ELSE (T.COST_PRICE - T.MAN_PRICE) * 100.0000 / T.MAN_PRICE END, -- ( �������� - �� ) * 100 / ��
    SUPPLIER_PRICE = T.COST_PRICE,
    SUPPLIER_PRICE_VAT = T.COST_PRICE * (1 + (T.NDS / 100.0000)),
    SUPPLIER_VAT_SUM = T.COST_PRICE * (T.NDS / 100.0000) * T.KOL,
    SUPPLIER_SUM = T.COST_PRICE * T.KOL,
    SUPPLIER_SUM_VAT = T.COST_PRICE * (1 + (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000)) * T.KOL,
    RETAIL_VAT = CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END,
    RETAIL_ADPRICE = (T.SALE_PRICE / (T.COST_PRICE * (1+(CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END / 100.0000)))) * 100.0000 - 100,
    RETAIL_PRICE = (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000)),
    RETAIL_PRICE_VAT = T.SALE_PRICE,
    RETAIL_VAT_SUM = (T.SALE_PRICE - (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000))) * T.KOL,
    RETAIL_SUM = (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000)) * T.KOL,
    RETAIL_SUM_VAT = T.SALE_PRICE * T.KOL,
    INCOMING_BILL_NUMBER = T.NUM_SF,
    INCOMING_BILL_DATE = T.DATA_SF,
    ID_STORE_PLACE = NULL,
    GTD_NUMBER = T.GTD,
    ID_REG_CERT_GLOBAL = NULL,
    ID_SERIES_GLOBAL = T.ID_SERIES,
    BARCODE = CASE WHEN LEN(T.BARCODE)=12 THEN ISNULL(DBO.FN_REPEX_RCY_CONVERT_RIGLA_BARCODE_GEN(T.BARCODE), T.BARCODE) ELSE T.BARCODE END,
    INCOMING_NUMBER = T.DOC_NUM,
    INCOMING_DATE = T.DOC_DATE,
    VAT = TT.TAX_RATE,
    CODE_STU = T.CODE_STU
FROM @T T
INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
WHERE T.ID_CONTRACTOR IS NOT NULL 
AND T.ID_GOODS IS NOT NULL
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL
AND T.ID_SCALING_RATIO IS NOT NULL
AND ISNULL(@IMPORT_FORMAT,'A2000')<>'A2000'
UNION ALL
SELECT
    NEWID(),
    @ID_GLOBAL,
    T.ID_GOODS,
    T.ID_SCALING_RATIO,
    T.KOL,
    T.ID_CONTRACTOR,
    PRODUCER_PRICE = T.MAN_PRICE,
    SUPPLIER_VAT = CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END,
    SUPPLIER_ADPRICE = CASE WHEN ISNULL(T.MAN_PRICE,0) =0 THEN 0 ELSE (T.COST_PRICE - T.MAN_PRICE) * 100.0000 / T.MAN_PRICE END, -- ( �������� - �� ) * 100 / ��
    SUPPLIER_PRICE = T.COST_PRICE,
    SUPPLIER_PRICE_VAT = T.COST_PRICE * (1 + (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000)),
    SUPPLIER_VAT_SUM = T.COST_PRICE * (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000) * T.KOL,
    SUPPLIER_SUM = T.COST_PRICE * T.KOL,
    SUPPLIER_SUM_VAT = T.COST_PRICE * (1 + (CASE WHEN @USE_VAT_FROM_EFARMA = 1 THEN TT.TAX_RATE ELSE T.NDS END / 100.0000)) * T.KOL,
    RETAIL_VAT = (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END),
    RETAIL_ADPRICE = (T.SALE_PRICE / (T.COST_PRICE * (1 + (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END/100.0000)))) * 100.0000 - 100,
    RETAIL_PRICE = (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000)),
    RETAIL_PRICE_VAT = T.SALE_PRICE,
    RETAIL_VAT_SUM = (T.SALE_PRICE - (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000))) * T.KOL,
    RETAIL_SUM = (T.SALE_PRICE *  100.0000 / (CASE WHEN @ENVD = 1 THEN 0 ELSE TT.TAX_RATE END + 100.0000)) * T.KOL,
    RETAIL_SUM_VAT = T.SALE_PRICE * T.KOL,
    INCOMING_BILL_NUMBER = T.NUM_SF,
    INCOMING_BILL_DATE = T.DATA_SF,
    ID_STORE_PLACE = NULL,
    GTD_NUMBER = T.GTD,
    ID_REG_CERT_GLOBAL = NULL,
    ID_SERIES_GLOBAL = T.ID_SERIES,
    BARCODE = CASE WHEN LEN(T.BARCODE)=12 THEN ISNULL(DBO.FN_REPEX_RCY_CONVERT_RIGLA_BARCODE_GEN(T.BARCODE), T.BARCODE) ELSE T.BARCODE END,
    INCOMING_NUMBER = T.DOC_NUM,
    INCOMING_DATE = T.DOC_DATE,
    VAT = TT.TAX_RATE,
    CODE_STU = T.CODE_STU
FROM @T T
INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
WHERE T.ID_CONTRACTOR IS NOT NULL 
AND T.ID_GOODS IS NOT NULL
AND NULLIF(T.COST_PRICE,0) IS NOT NULL 
AND NULLIF(T.SALE_PRICE,0) IS NOT NULL
AND T.ID_SCALING_RATIO IS NOT NULL
AND ISNULL(@IMPORT_FORMAT,'A2000')='A2000'

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0 OR @ROWCOUNT=0)
    GOTO ERR

EXEC USP_IMPORT_REMAINS_LOT_GEN @ID_GLOBAL

SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
IF (@ERROR<>0)
    GOTO ERR
COMMIT
ERR:
    IF (@@TRANCOUNT<>0)
        ROLLBACK    
RETURN
GO


IF (OBJECT_ID('REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT_DCARD') IS NULL) EXEC ('CREATE PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT_DCARD AS RETURN')
GO
ALTER PROCEDURE REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT_DCARD(
    @XMLDATA NTEXT
)
AS
    DECLARE @HDOC INT
    DECLARE @IMPORT_FORMAT VARCHAR(40)

    DECLARE @DC TABLE(
		NN VARCHAR(20),
		SHCOD VARCHAR(13),
		TIPDK INT,
		[NAME] VARCHAR(300),
		FAM  VARCHAR(300),
		OTCH VARCHAR(300),
		MOBPHONE VARCHAR(40),
		DTBTH DATETIME,
		CITY VARCHAR(40),
		CITYD VARCHAR(40),
		STREET VARCHAR(40),
		HOUSE VARCHAR(40),
		FLAT VARCHAR(40),
		EMAIL VARCHAR(40),
		SUMMA_OPL MONEY,
		ISNEW BIT,
		ISNEWMEMBER BIT,
		ID_MEMBER UNIQUEIDENTIFIER,
        MALE VARCHAR(1),
        DISTRICT VARCHAR(40)
    )

    DECLARE @DCT TABLE(
        TIPDKSTU INT,
        TIPDKEF UNIQUEIDENTIFIER
    )

    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
        
    SELECT 
        @IMPORT_FORMAT = IMPORT_FORMAT
    FROM OPENXML(@HDOC, '/XML') WITH(
        IMPORT_FORMAT VARCHAR(40) 'IMPORT_FORMAT'
    ) A

    INSERT INTO @DC(
		NN,
		SHCOD,
		TIPDK,
		[NAME],
		FAM,
		OTCH,
		MOBPHONE,
		DTBTH,
		CITY,
		CITYD,
		STREET,
		HOUSE,
		FLAT,
		EMAIL,
		SUMMA_OPL,
        MALE,
        DISTRICT
    )    
    SELECT 
		NN,
		SHCOD,
		TIPDK,
		[NAME],
		FAM,
		OTCH,
		MOBPHONE,
		DTBTH,
		CITY,
		CITYD,
		STREET,
		HOUSE,
		FLAT,
		EMAIL,
		SUMMA_OPL,
        MALE,
        DISTRICT
    FROM OPENXML(@HDOC, '/XML/DCARD') WITH(
		NN VARCHAR(20) 'NN',
		SHCOD VARCHAR(13) 'SHCOD',
		TIPDK INT 'TIPDK',
		[NAME] VARCHAR(300) 'NAME',
		FAM  VARCHAR(300) 'FAM',
		OTCH VARCHAR(300) 'OTCH',
		MOBPHONE VARCHAR(40) 'MOBPHONE',
		DTBTH DATETIME 'DTBTH',
		CITY VARCHAR(40) 'CITY',
		CITYD VARCHAR(40) 'CITYD',
		STREET VARCHAR(40) 'STREET',
		HOUSE VARCHAR(40) 'HOUSE',
		FLAT VARCHAR(40) 'FLAT',
		EMAIL VARCHAR(40) 'EMAIL',
		SUMMA_OPL MONEY 'SUMMA_OPL',
        MALE VARCHAR(1) 'MALE',
        DISTRICT VARCHAR(40) 'DISTRICT'	
    )

    INSERT INTO @DCT(
        TIPDKSTU,
        TIPDKEF
    )
    SELECT
        TIPDKSTU,
        TIPDKEF
    FROM OPENXML(@HDOC, '/XML/CROS_DCT') WITH(
        TIPDKSTU INT 'TIPDKSTU',
        TIPDKEF UNIQUEIDENTIFIER 'TIPDKEF'
    )
   
    EXEC SP_XML_REMOVEDOCUMENT @HDOC

	-- ����������� ��� ���� ��������, ��� �� �������
	UPDATE DC
	SET
		DC.ID_MEMBER = d2c.ID_DISCOUNT2_MEMBER_GLOBAL,
		DC.ISNEWMEMBER = 0
	FROM @DC DC INNER JOIN dbo.DISCOUNT2_CARD d2c ON (DC.SHCOD = d2c.BARCODE)

	-- ����������� ��� ���� ����� ��������, ��� ���� ���� �� 1 �������
	UPDATE @DC
	SET
		ID_MEMBER = NEWID(),
		ISNEWMEMBER = 1
	WHERE (ID_MEMBER IS NULL)
		AND ((FAM IS NOT NULL AND FAM <> '')
			OR ([NAME] IS NOT NULL AND [NAME] <> '')
			OR (OTCH IS NOT NULL AND OTCH <> ''))

	-- ��������� ����� �������� � �������
	INSERT INTO DISCOUNT2_MEMBER
	(
		ID_DISCOUNT2_MEMBER_GLOBAL,
		LASTNAME,
		FIRSTNAME,
		MIDDLENAME,
		BIRTHDAY,
		[ADDRESS],
        GENDER
	)
	SELECT
		ID_MEMBER,
		FAM,
		[NAME],
		OTCH,
		DTBTH,
		LEFT(
        ISNULL(DC.MOBPHONE,'') + ';' +
		ISNULL(DC.CITY,'') + ';' +
        ISNULL(DC.DISTRICT,'')+';'+
		ISNULL(DC.CITYD,'') + ';' +
		ISNULL(DC.STREET,'') + ';' +
		ISNULL(DC.HOUSE,'') + ';' +
		ISNULL(DC.FLAT,'') + ';' +
		ISNULL(DC.EMAIL,''), 1024) AS [ADDRESS],
        CASE WHEN MALE='F' THEN '�' 
             WHEN MALE='M' THEN '�'
             ELSE NULL
        END
	FROM @DC DC
	WHERE (DC.ID_MEMBER IS NOT NULL) AND (DC.ISNEWMEMBER = 1)

	-- ��������� ��������, ��� �� �����
	UPDATE d2m
	SET
		LASTNAME = DC.FAM,
		FIRSTNAME = DC.[NAME],
		MIDDLENAME = DC.OTCH,
		BIRTHDAY = DC.DTBTH,
		ADDRESS = LEFT(
        ISNULL(DC.MOBPHONE,'') + ';' +
		ISNULL(DC.CITY,'') + ';' +
        ISNULL(DC.DISTRICT,'')+';'+
		ISNULL(DC.CITYD,'') + ';' +
		ISNULL(DC.STREET,'') + ';' +
		ISNULL(DC.HOUSE,'') + ';' +
		ISNULL(DC.FLAT,'') + ';' +
		ISNULL(DC.EMAIL,''), 1024),
        GENDER = CASE WHEN MALE='F' THEN '�' 
                      WHEN MALE='M' THEN '�'
                      ELSE NULL
                 END
	FROM @DC DC INNER JOIN dbo.DISCOUNT2_MEMBER d2m ON (DC.ID_MEMBER = d2m.ID_DISCOUNT2_MEMBER_GLOBAL)
	WHERE (DC.ID_MEMBER IS NOT NULL) AND (DC.ISNEWMEMBER = 0)

	-- � ��������� ���������, ��������� ��� ���� ����, ����� ����� ��� ���
	UPDATE DC
	SET
		ISNEW = 0
	FROM @DC DC INNER JOIN dbo.DISCOUNT2_CARD d2c ON (DC.SHCOD = d2c.BARCODE)
	UPDATE @DC
	SET
		ISNEW = 1
	WHERE ISNEW IS NULL

	-- ������� ����� �����
	INSERT INTO DISCOUNT2_CARD
	(
		ID_DISCOUNT2_CARD_GLOBAL,
		NUMBER,
		BARCODE,
		ID_DISCOUNT2_CARD_TYPE_GLOBAL,
		DATE_START,
		ID_DISCOUNT2_MEMBER_GLOBAL
	)
	SELECT
		NEWID(),
		DC.NN,
		DC.SHCOD,
		DCT.TIPDKEF,
		GETDATE(),
		DC.ID_MEMBER
	FROM @DC DC JOIN @DCT DCT ON (DC.TIPDK = DCT.TIPDKSTU)
	WHERE DC.ISNEW = 1

	-- ������� ������������ �����
	UPDATE d2c
	SET
		NUMBER = DC.NN,
		ID_DISCOUNT2_CARD_TYPE_GLOBAL = DCT.TIPDKEF,
		ID_DISCOUNT2_MEMBER_GLOBAL = DC.ID_MEMBER
	FROM DISCOUNT2_CARD d2c
		INNER JOIN @DC DC ON (DC.SHCOD = d2c.BARCODE)
		INNER JOIN @DCT DCT ON (DC.TIPDK = DCT.TIPDKSTU)
	WHERE DC.ISNEW = 0

	-- ���� ������������� ������ ��������� ������ ���� ������ QWERTY
	IF @IMPORT_FORMAT = 'QWERTY'
	BEGIN
		UPDATE d2c
		SET
			ACCUMULATE_SUM = DC.SUMMA_OPL
		FROM DISCOUNT2_CARD d2c
			INNER JOIN @DC DC ON (DC.SHCOD = d2c.BARCODE)
	END

RETURN
GO

IF OBJECT_ID('DBO.REPEX_DISCOUNT2_CARD_GET_USED') IS NULL EXEC('CREATE PROCEDURE DBO.REPEX_DISCOUNT2_CARD_GET_USED AS RETURN')
GO
ALTER PROCEDURE DBO.REPEX_DISCOUNT2_CARD_GET_USED 
AS
BEGIN	
	SELECT C.BARCODE
	FROM DISCOUNT2_CARD C
	WHERE EXISTS 
	(
		SELECT * FROM DISCOUNT2_MAKE_ITEM MI
		WHERE C.ID_DISCOUNT2_CARD_GLOBAL = MI.ID_DISCOUNT2_CARD_GLOBAL
	)
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM SYSINDEXES WHERE NAME = 'IX_DISCOUNT2_CARD$BARCODE')
 	CREATE INDEX IX_DISCOUNT2_CARD$BARCODE ON DISCOUNT2_CARD ([BARCODE] ASC)
GO
