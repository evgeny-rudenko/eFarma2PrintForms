IF (OBJECT_ID('REPEX_IMPORT_NOVGOROD_CONVERT') IS NULL) EXEC ('CREATE PROCEDURE REPEX_IMPORT_NOVGOROD_CONVERT AS RETURN')
GO

ALTER PROCEDURE REPEX_IMPORT_NOVGOROD_CONVERT(
    @XMLDATA NTEXT
)
AS
    DECLARE @HDOC INT
    EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
    DECLARE @T TABLE(
        ID_GLOBAL UNIQUEIDENTIFIER,
        ANL VARCHAR(125),
        NNOM VARCHAR(7),
        IZG VARCHAR(7),
        OST MONEY,
        CO MONEY,
        P_SUP MONEY,
        P_APT MONEY,
		BARCOD VARCHAR(20)
    )

    INSERT INTO @T(
        ID_GLOBAL,
         ANL,
         NNOM,
		 IZG,
         OST,
         CO,
         P_SUP,
         P_APT,
		 BARCOD
    )
    SELECT
         ID_GLOBAL,
         ANL,
         NNOM,
		 IZG,
         OST,
         CO,
         P_SUP,
         P_APT,
		 BARCOD
    FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
        ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
         ANL VARCHAR(125) 'ANL',
         NNOM VARCHAR(7) 'NNOM',
         IZG VARCHAR(7) 'IZG',
         OST MONEY 'OST',
         CO MONEY 'CO',
         P_SUP MONEY 'P_SUP',
         P_APT MONEY 'P_APT',
		 BARCOD VARCHAR(20) 'BARCOD'
    )

DECLARE @IN_LOG TABLE(ID_GLOBAL UNIQUEIDENTIFIER, REASON VARCHAR(10))    --��� ������ � ���:id ������ � ������� ������ � ���

INSERT INTO @IN_LOG(ID_GLOBAL, REASON)
SELECT 
    T.ID_GLOBAL, 'CREDIT'
FROM @T T
LEFT JOIN GOODS_CODE GC on GC.CODE = T.ANL
LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC.ID_CONTRACTOR
WHERE ((T.ANL <> '' and (T.ANL is not null)) 
        AND C.NAME='������'
        AND (SELECT COUNT(*)
             FROM GOODS_CODE GC1
             INNER JOIN CONTRACTOR C1 ON C1.ID_CONTRACTOR = GC1.ID_CONTRACTOR 
             WHERE GC.CODE = GC1.CODE AND C1.NAME = '������' AND GC.DATE_DELETED IS NULL
             GROUP BY GC1.CODE)<>1
      )
OR (T.ANL is null OR T.ANL = '') OR NOT EXISTS(SELECT NULL FROM GOODS_CODE CG2 WHERE CG2.CODE = T.ANL)
GROUP BY ID_GLOBAL

INSERT INTO @IN_LOG(ID_GLOBAL, REASON)
SELECT 
    T.ID_GLOBAL, 'TRADING'
FROM @T T
LEFT JOIN GOODS_CODE GC ON GC.CODE = T.NNOM + ' ' + T.IZG
LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC.ID_CONTRACTOR
WHERE (((T.NNOM + ' ' + T.IZG <>' ' and ((T.NNOM + ' ' + T.IZG) is not null)) 
        AND C.NAME='���'
        AND (SELECT COUNT(*)
             FROM GOODS_CODE GC1
             INNER JOIN CONTRACTOR C1 ON C1.ID_CONTRACTOR = GC1.ID_CONTRACTOR 
             WHERE GC.CODE = GC1.CODE AND C1.NAME = '���' AND GC.DATE_DELETED IS NULL
             GROUP BY GC1.CODE)<>1
        ) 
OR ((T.NNOM + ' ' + T.IZG)=' ' OR (T.NNOM + ' ' + T.IZG) is null) OR NOT EXISTS(select null from GOODS_CODE cg2 where cg2.code = T.NNOM + ' ' + T.IZG))
AND EXISTS(SELECT NULL FROM @IN_LOG L WHERE L.ID_GLOBAL = T.ID_GLOBAL)
GROUP BY ID_GLOBAL

--SELECT * FROM @IN_LOG

DECLARE @IDS TABLE(ID_GLOBAL UNIQUEIDENTIFIER, ID_GOODS BIGINT, CONTROL_TYPE VARCHAR(10), OST money)
-- ������ �� ANL
INSERT INTO @IDS(ID_GLOBAL, ID_GOODS, CONTROL_TYPE, OST)
SELECT
    T.ID_GLOBAL,
    GC.ID_GOODS,
	'LS',
    T.OST
FROM @T T
INNER JOIN (SELECT
                ID_GOODS = MIN(ID_GOODS),
                GC1.CODE
            FROM GOODS_CODE GC1
				INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
            WHERE (C.NAME='������')
                  AND GC1.DATE_DELETED IS NULL
            GROUP BY GC1.CODE) GC ON (GC.CODE = T.ANL)
-- ������ �� NNOM+IZG
INSERT INTO @IDS(ID_GLOBAL, ID_GOODS, CONTROL_TYPE, OST)
SELECT
    T.ID_GLOBAL,
    GC.ID_GOODS,
	'LS',
    T.OST
FROM @T T
INNER JOIN (SELECT
                ID_GOODS = MIN(ID_GOODS),
                GC1.CODE
            FROM GOODS_CODE GC1
				INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
            WHERE (C.NAME='���')
                  AND GC1.DATE_DELETED IS NULL
            GROUP BY GC1.CODE) GC ON (GC.CODE = (T.NNOM + ' ' + T.IZG))
WHERE T.ID_GLOBAL NOT IN (SELECT DISTINCT ID_GLOBAL FROM @IDS)
-- ������ �� BARCOD
INSERT INTO @IDS(ID_GLOBAL, ID_GOODS, CONTROL_TYPE, OST)
SELECT
    T.ID_GLOBAL,
    BC.ID_GOODS,
	'LS',
    T.OST
FROM @T T
INNER JOIN (SELECT
                ID_GOODS = MIN(ID_GOODS),
                BC1.CODE
            FROM BAR_CODE BC1
            WHERE BC1.DATE_DELETED IS NULL
            GROUP BY BC1.CODE) BC ON (BC.CODE = T.BARCOD)
WHERE T.ID_GLOBAL NOT IN (SELECT DISTINCT ID_GLOBAL FROM @IDS)

-- ������ �� NNOM
DECLARE @IDS2 TABLE(ID_GLOBAL UNIQUEIDENTIFIER, ID_GOODS BIGINT, CONTROL_TYPE VARCHAR(10), OST money)
INSERT INTO @IDS2(ID_GLOBAL, ID_GOODS, CONTROL_TYPE, OST)
SELECT
    T.ID_GLOBAL,
    GC.ID_GOODS,
	'TN',
    T.OST
FROM @T T
INNER JOIN (SELECT
                ID_GOODS = MIN(ID_GOODS),
                GC1.CODE
            FROM GOODS_CODE GC1
				INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
            WHERE (C.NAME='���')
                  AND GC1.DATE_DELETED IS NULL
            GROUP BY GC1.CODE) GC ON (GC.CODE LIKE T.NNOM + '%')
WHERE T.ID_GLOBAL NOT IN (SELECT DISTINCT ID_GLOBAL FROM @IDS)

-- �������� ������ ������� �� NNOM, ���� ����� ��� ����� ������� ���� ��������� ����������
INSERT INTO @IDS(ID_GLOBAL, ID_GOODS, CONTROL_TYPE, OST)
SELECT
	T.ID_GLOBAL,
	T1.ID_GOODS,
	T1.CONTROL_TYPE,
	T1.OST
FROM @IDS2 T
INNER JOIN @IDS2 T1 ON (T1.ID_GLOBAL = T.ID_GLOBAL AND T1.ID_GOODS = (SELECT MIN(ID_GOODS) FROM @IDS2 WHERE ID_GLOBAL = T.ID_GLOBAL))

DELETE FROM @IN_LOG
WHERE ID_GLOBAL IN (SELECT DISTINCT ID_GLOBAL FROM @IDS)

SELECT ID_GLOBAL, REASON FROM @IN_LOG

SELECT
    IDS.ID_GLOBAL,
	IDS.ID_GOODS,
    OST = FLOOR(T.OST),
	OST2 = CEILING(T.OST),
	DENOMINATOR = 1,
    T.CO,
    T.P_SUP,
	P_SUP2 = T.P_SUP,
    T.P_APT,
	IDS.CONTROL_TYPE
FROM @IDS IDS
    INNER JOIN GOODS G ON G.ID_GOODS = IDS.ID_GOODS 
    INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = G.ID_GOODS
                                   AND SR.DATE_DELETED IS NULL
                                   AND SR.DENOMINATOR = 1	
	INNER JOIN @T T ON T.ID_GLOBAL = IDS.ID_GLOBAL

SELECT
    T.ID_GLOBAL,
	A.ID_GOODS,
    OST = ROUND(((T.OST - FLOOR(T.OST)) * A.DENOMINATOR),0),
    OST2 = ROUND(((T.OST - FLOOR(T.OST)) * A.DENOMINATOR),0),
    DENOMINATOR = A.DENOMINATOR,
    CO = T.CO/A.DENOMINATOR,
    T.P_SUP/A.DENOMINATOR as P_SUP,
	P_SUP2 = T.P_SUP,
    P_APT = T.P_APT/A.DENOMINATOR,
	A.CONTROL_TYPE
FROM (SELECT 
          IDS.ID_GLOBAL,
	      IDS.ID_GOODS,
          DENOMINATOR = MIN(SR.DENOMINATOR),
		  IDS.CONTROL_TYPE
      FROM @IDS IDS
      INNER JOIN GOODS G ON G.ID_GOODS = IDS.ID_GOODS 
      INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = G.ID_GOODS
                                   AND SR.DATE_DELETED IS NULL
                                   AND SR.DENOMINATOR > 1
      WHERE ABS(((IDS.OST - FLOOR(IDS.OST)) * SR.DENOMINATOR) - ROUND(((IDS.OST - FLOOR(IDS.OST)) * SR.DENOMINATOR), 0))<=0.1
      GROUP BY IDS.ID_GLOBAL, IDS.ID_GOODS, IDS.CONTROL_TYPE) A
INNER JOIN @T T ON T.ID_GLOBAL = A.ID_GLOBAL
WHERE (T.OST - FLOOR(T.OST))>0

RETURN 
GO

-- EXEC REPEX_IMPORT_NOVGOROD_CONVERT NULL

IF (OBJECT_ID('REPEX_IMPORT_NOVGOROD_CHECK') IS NULL) EXEC('CREATE PROCEDURE REPEX_IMPORT_NOVGOROD_CHECK AS RETURN')
GO

ALTER PROCEDURE REPEX_IMPORT_NOVGOROD_CHECK(
     @XMLDATA NTEXT
 )
 AS
     DECLARE @HDOC INT
     DECLARE @T TABLE(
       ID_GLOBAL UNIQUEIDENTIFIER,
       NOTD VARCHAR(20),
		POST VARCHAR(20)
     )
 
     DECLARE @CT TABLE(
         ID_GLOBAL UNIQUEIDENTIFIER,
       NOTD VARCHAR(20),
		POST VARCHAR(20),
		K39_1 VARCHAR(20),
         CODE_L VARCHAR(20),
         CODE_K VARCHAR(20),
		NNOM VARCHAR(20),
		FIN MONEY,
		ID_GOODS BIGINT
	)
 
     DECLARE @RESULT TABLE(
         ID_GLOBAL UNIQUEIDENTIFIER,
		CODE VARCHAR(250),
         TEXT VARCHAR(4000)    
     )
 
     DECLARE @CRESULT TABLE(
         ID_GLOBAL UNIQUEIDENTIFIER,
		CODE VARCHAR(250),
         TEXT VARCHAR(4000)    
     )

     EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
     INSERT INTO @T(
       ID_GLOBAL,
       NOTD,
		POST
     )
     SELECT
       ID_GLOBAL,
       NOTD,
		POST
     FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
         ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
         NOTD VARCHAR(20) 'NOTD',
		POST VARCHAR(20) 'POST'
     )
 
     INSERT INTO @CT(
         ID_GLOBAL,
       NOTD,
		POST,
		K39_1,
		CODE_L,
         CODE_K,
		NNOM,
		FIN
     )
     SELECT
         ID_GLOBAL,
       NOTD,
		POST,
		K39_1,
         CODE_L = CO9_1,
         CODE_K = NNOM+' '+IZG,
		NNOM,
		FIN
     FROM OPENXML(@HDOC, '/XML/CONTRACTS_ITEM') WITH(
         ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
         NOTD VARCHAR(20) 'NOTD',
		POST VARCHAR(20) 'POST',
		K39_1 VARCHAR(20) 'K39_1',
         CO9_1 VARCHAR(20) 'CO9_1',
         NNOM VARCHAR(20) 'NNOM',
         IZG VARCHAR(20) 'IZG',
		FIN MONEY 'FIN'
     )
 
     EXEC SP_XML_REMOVEDOCUMENT @HDOC
 
     INSERT INTO @RESULT(
         ID_GLOBAL,
		CODE,
         TEXT
     )
     SELECT
         T.ID_GLOBAL,
		'BADSTORE',
         '� ��2 ����������� ����� � �������� �����'
     FROM @T T
     WHERE (NOT EXISTS (SELECT NULL
                       FROM STORE S
                       WHERE S.DATE_DELETED IS NULL
                       AND S.MNEMOCODE = T.NOTD))
     UNION
     SELECT
         T.ID_GLOBAL,
		'BADCONTRACTOR',
         '� ��2 ����������� ���������� � �������� �����'
     FROM @T T
     WHERE (NOT EXISTS (SELECT NULL
                   FROM CONTRACTOR C
                   WHERE C.DATE_DELETED IS NULL
                   AND C.A_COD = CONVERT(BIGINT, T.POST)))

     INSERT INTO @CRESULT(
         ID_GLOBAL,
		CODE,
         TEXT
     )
     SELECT
         T.ID_GLOBAL,
		'BADSTORE',
         '� ��2 ����������� ����� � �������� �����'
     FROM @CT T
     WHERE (NOT EXISTS (SELECT NULL
                       FROM STORE S
                       WHERE S.DATE_DELETED IS NULL
                       AND S.MNEMOCODE = T.NOTD))
     UNION
     SELECT
         T.ID_GLOBAL,
		'BADCONTRACTOR',
         '� ��2 ����������� ���������� � �������� �����'
     FROM @CT T
     WHERE (NOT EXISTS (SELECT NULL
                   FROM CONTRACTOR C
                   WHERE C.DATE_DELETED IS NULL
                   AND C.A_COD = CONVERT(BIGINT, T.POST)))
     UNION
     SELECT
         T.ID_GLOBAL,
		'FUZZYCONTRACT',
         '�������� � ����� ������������� ����� 1 ����������� ��� ��������� ��������������'
     FROM @CT T
	 WHERE T.K39_1 IN
		(SELECT C1.K39_1 FROM @CT C1
		GROUP BY C1.K39_1
		HAVING
			(SELECT COUNT(DISTINCT POST) FROM @CT C2 WHERE C1.K39_1 = C2.K39_1) > 1 OR		
			(SELECT COUNT(DISTINCT FIN) FROM @CT C3 WHERE C1.K39_1 = C3.K39_1) > 1)

	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS
	 FROM @CT CT
	 INNER JOIN (
		SELECT
            ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='���')
              AND GC1.DATE_DELETED IS NULL			  
        GROUP BY GC1.CODE) GC ON (GC.CODE LIKE CT.NNOM + ' %')

	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS
	 FROM @CT CT
	 INNER JOIN (
		SELECT
            ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='���')
              AND GC1.DATE_DELETED IS NULL			  
        GROUP BY GC1.CODE) GC ON (GC.CODE = CT.CODE_K)

	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS
	 FROM @CT CT
	 INNER JOIN (
		SELECT
			ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='������')
              AND GC1.DATE_DELETED IS NULL
        GROUP BY GC1.CODE) GC ON (GC.CODE = CT.CODE_L)

	DECLARE @IN_LOG TABLE(ID_GLOBAL UNIQUEIDENTIFIER, REASON VARCHAR(10))

	INSERT INTO @IN_LOG(ID_GLOBAL, REASON)
	SELECT 
		T.ID_GLOBAL, 'CREDIT'
	FROM @CT T
	LEFT JOIN GOODS_CODE GC on GC.CODE = T.CODE_L
	LEFT JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC.ID_CONTRACTOR
	WHERE ((T.CODE_L <> '' and (T.CODE_L is not null)) 
			AND C.NAME='������'
			AND (SELECT COUNT(*)
				 FROM GOODS_CODE GC1
				 INNER JOIN CONTRACTOR C1 ON C1.ID_CONTRACTOR = GC1.ID_CONTRACTOR 
				 WHERE GC.CODE = GC1.CODE AND C1.NAME = '������' AND GC.DATE_DELETED IS NULL
				 GROUP BY GC1.CODE)<>1
		  )
	OR (T.CODE_L is null OR T.CODE_L = '')OR NOT EXISTS(SELECT NULL FROM GOODS_CODE CG2 WHERE CG2.CODE = T.CODE_L)
	GROUP BY ID_GLOBAL

	INSERT INTO @CRESULT(
         ID_GLOBAL,
		CODE,
         TEXT
     )
     SELECT
         T.ID_GLOBAL,
		'BADGOODSCODE',
         '�� ������� ����������� ����� �� ����� ��������� �� �� ������ �������� �����.'
     FROM @CT T
	 WHERE T.ID_GOODS IS NULL

	DELETE FROM @CT
	WHERE ID_GOODS IS NULL

-- 	INSERT INTO @CRESULT(
--          ID_GLOBAL,
-- 		CODE,
--          TEXT
--      )
-- 	SELECT T.ID_GLOBAL,
-- 		'SAMEIDGOODS',
--          '� �������� ���� � ��� �� ����� ����������� 2 ����.'
--      FROM @CT T
-- 	WHERE EXISTS
-- 		(
-- 			SELECT NULL
-- 			FROM @CT T2
-- 			WHERE T.K39_1 = T2.K39_1
-- 				AND T.ID_GOODS = T2.ID_GOODS
-- 				AND T.ID_GLOBAL <> T2.ID_GLOBAL
-- 		)

     SELECT ID_GLOBAL, CODE, TEXT FROM @RESULT
     SELECT ID_GLOBAL, CODE, TEXT FROM @CRESULT
 RETURN
GO

-- EXEC REPEX_IMPORT_NOVGOROD_CHECK NULL
 
IF (OBJECT_ID('REPEX_IMPORT_NOVGOROD_IMPORT') IS NULL) EXEC ('CREATE PROCEDURE REPEX_IMPORT_NOVGOROD_IMPORT AS RETURN')
GO
 
ALTER PROCEDURE REPEX_IMPORT_NOVGOROD_IMPORT(
     @XMLDATA NTEXT
 )
 AS
     DECLARE @HDOC INT
     DECLARE @IMPORT_FORMAT VARCHAR(40)
 


     DECLARE @T TABLE(
		ID_GLOBAL UNIQUEIDENTIFIER,
        ID_GOODS BIGINT,
        ID_CONTRACTOR BIGINT,
        ID_SCALING_RATIO BIGINT,
		ID_STORE BIGINT,

		NOTD VARCHAR(20),
		POST VARCHAR(20),
		DSC DATETIME,
		NSC VARCHAR(20),
		KON7 VARCHAR(10),
		OST MONEY,
		OST2 MONEY,
		CO MONEY,
		NACP MONEY,
		SER VARCHAR(20),
		TMT MONEY,
		SERT VARCHAR(20),
		KEM VARCHAR(20),
		S DATETIME,
		PO DATETIME,
		TAM VARCHAR(40),
		P_SUP MONEY,
		P_SUP2 MONEY,
		P_APT MONEY,
		DENOMINATOR INT,
		CONTROL_TYPE VARCHAR(10),
        NDS_SUP MONEY,
        NDS_APT MONEY
     )
 
    DECLARE @CT TABLE(
        ID BIGINT NOT NULL IDENTITY,
        [NAME] VARCHAR(120),
        NOTD VARCHAR(20),
        POST VARCHAR(20),
        K39_1 VARCHAR(20),
        KOL39_1 MONEY,
        Z9C_1 MONEY,
        FIN MONEY,
        CO9_1 VARCHAR(20),
        NNOM VARCHAR(20),
        IZG VARCHAR(20),

		ID_CONTRACTOR BIGINT,
		ID_GOODS BIGINT,
		CONTROL_TYPE VARCHAR(20)
    )
 
     EXEC SP_XML_PREPAREDOCUMENT @HDOC OUT, @XMLDATA
 
     SELECT 
         @IMPORT_FORMAT = IMPORT_FORMAT
     FROM OPENXML(@HDOC, '/XML') WITH(
         IMPORT_FORMAT VARCHAR(40) 'IMPORT_FORMAT'
     ) A
 
	 INSERT INTO @T(
		ID_GLOBAL,
		ID_GOODS,
		NOTD,
		POST,
		DSC,
		NSC,
		KON7,
		OST,
		OST2,
		CO,
		NACP,
		SER,
		TMT,
		SERT,
		KEM,
		S,
		PO,
		TAM,
		P_SUP,
		P_SUP2,
		P_APT,
		DENOMINATOR,
		CONTROL_TYPE,
        NDS_SUP,
        NDS_APT)
	 SELECT
		ID_GLOBAL,
		ID_GOODS,
		NOTD,
		POST,
		DSC,
		NSC,
		KON7,
		OST,
		OST2,
		CO,
		NACP,
		SER,
		TMT,
		SERT,
		KEM,
		S,
		PO,
		TAM,
		P_SUP,
		P_SUP2,
		P_APT,
		DENOMINATOR,
		CONTROL_TYPE,
        NDS_SUP,
        NDS_APT
	 FROM OPENXML(@HDOC, '/XML/ITEM') WITH(
		ID_GLOBAL UNIQUEIDENTIFIER 'ID_GLOBAL',
		ID_GOODS BIGINT 'ID_GOODS',
		NOTD VARCHAR(20) 'NOTD',
		POST VARCHAR(20) 'POST',
		DSC DATETIME 'DSC',
		NSC VARCHAR(20) 'NSC',
		KON7 VARCHAR(10) 'KON7',
		OST MONEY 'OST',
		OST2 MONEY 'OST2',
		CO MONEY 'CO',
		NACP MONEY 'NACP',
		SER VARCHAR(20) 'SER',
		TMT MONEY 'TMT',
		SERT VARCHAR(20) 'SERT',
		KEM VARCHAR(20) 'KEM',
		S DATETIME 'S',
		PO DATETIME 'PO',
		TAM VARCHAR(40) 'TAM',
		P_SUP MONEY 'P_SUP',
		P_SUP2 MONEY 'P_SUP2',
		P_APT MONEY 'P_APT',
		DENOMINATOR INT 'DENOMINATOR',
		CONTROL_TYPE VARCHAR(10) 'CONTROL_TYPE',
        NDS_SUP MONEY 'NDS_SUP',
        NDS_APT MONEY 'NDS_APT'
	 )

    INSERT INTO @CT(
        [NAME],
		NOTD,
		POST,
        K39_1,
		KOL39_1,
		Z9C_1,
        FIN,
		CO9_1,
		NNOM,
		IZG
    )
    SELECT
        [NAME],
		NOTD,
		POST,
        K39_1,
		KOL39_1,
		Z9C_1,
        FIN,
		CO9_1,
		NNOM,
		IZG
    FROM OPENXML(@HDOC, '/XML/CONTRACTS_ITEM') WITH(
        [NAME] VARCHAR(120) 'NAME',
        NOTD VARCHAR(2) 'NOTD',
        POST VARCHAR(7) 'POST',
        K39_1 VARCHAR(10) 'K39_1',
        KOL39_1 MONEY 'KOL39_1',
        Z9C_1 MONEY 'Z9C_1',
        FIN MONEY 'FIN',
        CO9_1 CHAR(8) 'CO9_1',
        NNOM VARCHAR(7) 'NNOM',
        IZG VARCHAR(7) 'IZG')
    
     EXEC SP_XML_REMOVEDOCUMENT @HDOC

 DECLARE @ERROR INT, @ROWCOUNT INT
 BEGIN TRAN    

-- ��������
	 UPDATE CT SET
		 ID_CONTRACTOR = C.ID_CONTRACTOR
	 FROM @CT CT
	 INNER JOIN (SELECT
					 ID_CONTRACTOR,
					 A_COD
				 FROM CONTRACTOR C
				 WHERE C.DATE_DELETED IS NULL) C ON C.A_COD = CONVERT(BIGINT, CT.POST)

	-- ������ �� NNOM
	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS,
		CONTROL_TYPE = 'TN'
	 FROM @CT CT
	 INNER JOIN (
		SELECT
            ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='���')
              AND GC1.DATE_DELETED IS NULL			  
        GROUP BY GC1.CODE) GC ON (GC.CODE LIKE CT.NNOM + ' %')
	-- ������ �� NNOM+IZG
	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS,
		CONTROL_TYPE = 'LS'
	 FROM @CT CT
	 INNER JOIN (
		SELECT
            ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='���')
              AND GC1.DATE_DELETED IS NULL			  
        GROUP BY GC1.CODE) GC ON (GC.CODE = CT.NNOM + ' ' + CT.IZG)
	-- ������ �� CO9_1
	 UPDATE CT SET
		 ID_GOODS = GC.ID_GOODS,
		CONTROL_TYPE = 'LS'
	 FROM @CT CT
	 INNER JOIN (
		SELECT
			ID_GOODS = MIN(ID_GOODS),
            GC1.CODE
        FROM GOODS_CODE GC1
        INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
        WHERE (C.NAME='������')
              AND GC1.DATE_DELETED IS NULL
        GROUP BY GC1.CODE) GC ON (GC.CODE = CT.CO9_1)

	-- �������� ������ �� ID_GOODS
    UPDATE T SET
        KOL39_1 = T2.KOL39_1
    FROM @CT T
    INNER JOIN (SELECT 
                    K39_1,
                    ID_GOODS,
                    Z9C_1 = ISNULL(Z9C_1,0),
                    KOL39_1 = SUM(ISNULL(KOL39_1,0))
                FROM @CT T2
                GROUP BY K39_1, ID_GOODS, ISNULL(Z9C_1,0)) T2 ON T2.K39_1 = T.K39_1 
                                                             AND T2.ID_GOODS = T.ID_GOODS 
                                                             AND T2.Z9C_1 = ISNULL(T.Z9C_1,0)
        
	DELETE T FROM @CT T
	WHERE EXISTS(	
			SELECT NULL
			FROM @CT T2
			WHERE T.K39_1 = T2.K39_1
    		AND T.ID_GOODS = T2.ID_GOODS
            AND ISNULL(T.Z9C_1,0) = ISNULL(T2.Z9C_1,0)
			GROUP BY T.K39_1, T.ID_GOODS, ISNULL(T.Z9C_1,0)
			HAVING COUNT(*) > 1
            AND MIN(T2.id) <> T.ID
	)

	DECLARE @CONTRACT_TYPE VARCHAR(20)
	IF @IMPORT_FORMAT = 'GK'
		SET @CONTRACT_TYPE = 'PURCHASE'
	ELSE
		SET @CONTRACT_TYPE = 'SUPPLY'

    INSERT INTO CONTRACTS(
        NAME,
        ID_CONTRACTOR,
        TYPE,
        START_DATE,
        END_DATE,
        DAYS_OF_DELAY,
        TYPE_OF_DELAY,
        ID_TASK_PROGRAM_GLOBAL,
        ID_FEE_SCALE_TYPE_GLOBAL,
        ENTRY,
        DATE_DELETED,
        REQ_AMOUNT,
        CHECK_SUM,
        ID_CONTRACTS_PARENT,
        CONTRACTOR_CODE,
        FUNDING_SOURCE
    )
    SELECT
        NAME = LEFT(CASE WHEN @IMPORT_FORMAT = 'GK' THEN '������� 'ELSE '�������� 'END + T.K39_1, 150),
        MIN(ID_CONTRACTOR),
        TYPE = left(@CONTRACT_TYPE,50),
        START_DATE = getdate(),
        END_DATE = NULL,
        DAYS_OF_DELAY = 0,
        TYPE_OF_DELAY = 0,
        ID_TASK_PROGRAM_GLOBAL = NULL,
        ID_FEE_SCALE_TYPE_GLOBAL = NULL,
        ENTRY = 0,
        DATE_DELETED = NULL,
        REQ_AMOUNT = NULL,
        CHECK_SUM = 0,
        ID_CONTRACTS_PARENT = NULL,
        CONTRACTOR_CODE = LEFT(K39_1,10),
        FUNDING_SOURCE = MIN(LEFT(EV.MNEMOCODE,10))
    FROM @CT T
		LEFT JOIN ENUMERATION_VALUE EV ON (EV.VALUE = CONVERT(VARCHAR(20), CONVERT(INT, COALESCE(T.FIN, 0)))) AND (EV.ID_ENUMERATION IN (SELECT ID_ENUMERATION FROM ENUMERATION WHERE MNEMOCODE = 'FUNDING_SOURCE'))
	WHERE K39_1 NOT IN
	(SELECT CONTRACTOR_CODE FROM CONTRACTS WHERE [TYPE] = @CONTRACT_TYPE) AND ID_CONTRACTOR IS NOT NULL
	GROUP BY K39_1

	SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
	IF (@ERROR<>0)
	GOTO ERR

	INSERT INTO CONTRACTS_GOODS
		(ID_CONTRACTS_GLOBAL,
		ID_GOODS,
		CONTROL_TYPE,
		QUANTITY,
		PRICE,
		DATE_DELETED)
	SELECT
		C.ID_CONTRACTS_GLOBAL,
		CT.ID_GOODS,
		LEFT(CT.CONTROL_TYPE,20),
		CT.KOL39_1,
		CT.Z9C_1,
		NULL
	FROM CONTRACTS C
		INNER JOIN @CT CT ON (C.CONTRACTOR_CODE = CT.K39_1 AND C.TYPE = @CONTRACT_TYPE)
	WHERE CT.ID_GOODS IS NOT NULL AND CT.ID_GOODS NOT IN
	(SELECT CG.ID_GOODS FROM CONTRACTS_GOODS CG WHERE CG.ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL)

	SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
	IF (@ERROR<>0)
	GOTO ERR

	UPDATE C SET REQ_AMOUNT = CSUMS.AMOUNT
	FROM
		CONTRACTS C
		INNER JOIN (
			SELECT CT.K39_1, SUM(COALESCE(CT.KOL39_1 * CT.Z9C_1, 0)) AS AMOUNT
			FROM @CT CT
			GROUP BY CT.K39_1
		) CSUMS ON (C.CONTRACTOR_CODE = CSUMS.K39_1 AND C.TYPE = @CONTRACT_TYPE)

	SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
	IF (@ERROR<>0)
	GOTO ERR

-- �������

	 UPDATE T SET
		 ID_CONTRACTOR = C.ID_CONTRACTOR
	 FROM @T T
	 INNER JOIN (SELECT
					 ID_CONTRACTOR,
					 A_COD
				 FROM CONTRACTOR C
				 WHERE C.DATE_DELETED IS NULL) C ON C.A_COD = CONVERT(BIGINT, T.POST)


	 UPDATE T SET
		 ID_STORE = S.ID_STORE
	 FROM @T T
	 INNER JOIN (SELECT
					 ID_STORE,
					 MNEMOCODE
				 FROM STORE S
				 WHERE S.DATE_DELETED IS NULL) S ON S.MNEMOCODE = T.NOTD

	UPDATE T SET
		ID_SCALING_RATIO = SR.ID_SCALING_RATIO
	FROM @T T
		INNER JOIN SCALING_RATIO SR ON SR.ID_GOODS = T.ID_GOODS AND SR.DENOMINATOR = T.DENOMINATOR

	DELETE FROM @T
	WHERE ID_STORE IS NULL OR ID_CONTRACTOR IS NULL OR ID_SCALING_RATIO IS NULL

	DECLARE curStores CURSOR FOR
	SELECT DISTINCT ID_STORE FROM @T T

	DECLARE @ID_STORE BIGINT
	DECLARE @ID_IMPORT_REMAINS_GLOBAL UNIQUEIDENTIFIER

	OPEN curStores
	FETCH NEXT FROM curStores INTO @ID_STORE
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @ID_IMPORT_REMAINS_GLOBAL = NEWID()

		DECLARE @MNEMOCODE VARCHAR(50)
		EXEC USP_MNEMOCODE_GEN @MNEMOCODE OUT, 'IMPORT_REMAINS'		

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
			@ID_IMPORT_REMAINS_GLOBAL,
			LEFT(@MNEMOCODE,40),
			@ID_STORE,
			(SELECT TOP 1 ID_PRICING_PRICING_MODEL FROM STORE WHERE ID_STORE = @ID_STORE),
			NULL,
			0,
			0,
			0,
			0,
			NULL,
			'SAVE',
			GETDATE(),
			GETDATE(),
			'������������ ������������� � ���������� ����������� ������',
			NULL
		FROM @T T
			INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
			WHERE T.ID_CONTRACTOR IS NOT NULL 
			AND T.ID_GOODS IS NOT NULL
			AND T.ID_SCALING_RATIO IS NOT NULL
			HAVING COUNT(*)>0

		SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
		IF (@ERROR<>0)
		GOTO ERR

		DECLARE @ID_GLOBAL UNIQUEIDENTIFIER
        SET @ID_GLOBAL = NULL    

		DECLARE curItems CURSOR FOR
		SELECT ID_GLOBAL FROM @T
		WHERE ID_STORE = @ID_STORE
	
		OPEN curItems
		FETCH NEXT FROM curItems INTO @ID_GLOBAL
		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @ID_SERIES UNIQUEIDENTIFIER
			SET @ID_SERIES = NEWID()

			DECLARE @TMT INT
			SELECT @TMT = CONVERT(INT, T.TMT)
			FROM @T T
			WHERE T.ID_GLOBAL = @ID_GLOBAL
			
			-- ������ �����
			INSERT INTO SERIES(
				MNEMOCODE,
				BEST_BEFORE,
				ID_GOODS,
				SERIES_NUMBER,
				DATE_MODIFIED,
				ID_SERIES_GLOBAL
			)
			SELECT
				CONVERT(VARCHAR(36), LEFT(@ID_SERIES,36)), 
				CONVERT(DATETIME, '1/' + CONVERT(VARCHAR(2), @TMT / 100) + '/'+ CONVERT(VARCHAR(2), @TMT % 100), 3),
				T.ID_GOODS,
				T.SER,
				GETDATE(),
				@ID_SERIES
			FROM @T T
			WHERE T.ID_GLOBAL = @ID_GLOBAL

			SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
			IF (@ERROR<>0)
			GOTO ERR

			-- ������ ����������			
			INSERT INTO CERTIFICATE(
				DATE_MODIFIED,
				CERT_NUMBER,
				ISSUED_BY,
				CERT_DATE,
				CERT_END_DATE,
				ID_SERIES,
				ID_SERIES_GLOBAL
			)
			SELECT
				GETDATE(),
				LEFT(T.SERT,4000),
				LEFT(ISSUER.NAME,1024),
				T.S,
				T.PO,
				S.ID_SERIES,
				S.ID_SERIES_GLOBAL
			FROM @T T
				INNER JOIN SERIES S ON S.ID_SERIES_GLOBAL = @ID_SERIES
				LEFT JOIN CONTRACTOR ISSUER ON T.KEM = ISSUER.A_COD
			WHERE T.ID_GLOBAL = @ID_GLOBAL

			SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
			IF (@ERROR<>0)
			GOTO ERR
	
			DECLARE @CONTRACTOR_CODE VARCHAR(20)
			SELECT @CONTRACTOR_CODE = T.KON7 FROM @T T
			WHERE T.ID_GLOBAL = @ID_GLOBAL

			-- ���� ��� ��������
			IF ((@CONTRACTOR_CODE IS NOT NULL) AND (@CONTRACTOR_CODE <> '') AND (NOT EXISTS (SELECT NULL FROM CONTRACTS WHERE @CONTRACTOR_CODE = CONTRACTOR_CODE AND [TYPE] = 'PURCHASE')))
			BEGIN
				-- ������ ����� �������
				INSERT INTO CONTRACTS(
					NAME,
					ID_CONTRACTOR,
					TYPE,
					START_DATE,
					END_DATE,
					DAYS_OF_DELAY,
					TYPE_OF_DELAY,
					ID_TASK_PROGRAM_GLOBAL,
					ID_FEE_SCALE_TYPE_GLOBAL,
					ENTRY,
					DATE_DELETED,
					REQ_AMOUNT,
					CHECK_SUM,
					ID_CONTRACTS_PARENT,
					CONTRACTOR_CODE,
					FUNDING_SOURCE
				)
				SELECT
					NAME = LEFT('������� ' + T.KON7, 150),
					T.ID_CONTRACTOR,
					TYPE = 'PURCHASE',
					START_DATE = getdate(),
					END_DATE = NULL,
					DAYS_OF_DELAY = 0,
					TYPE_OF_DELAY = 0,
					ID_TASK_PROGRAM_GLOBAL = NULL,
					ID_FEE_SCALE_TYPE_GLOBAL = NULL,
					ENTRY = 0,
					DATE_DELETED = NULL,
					REQ_AMOUNT = NULL,
					CHECK_SUM = 0,
					ID_CONTRACTS_PARENT = NULL,
					CONTRACTOR_CODE = LEFT(T.KON7,10),
					FUNDING_SOURCE = NULL
				FROM @T T
				WHERE T.ID_GLOBAL = @ID_GLOBAL

				SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
				IF (@ERROR<>0)
				GOTO ERR
			END
	
			DECLARE @ID_CONTRACTS_GLOBAL UNIQUEIDENTIFIER
            SET @ID_CONTRACTS_GLOBAL = NULL
			SELECT @ID_CONTRACTS_GLOBAL = C.ID_CONTRACTS_GLOBAL
			FROM CONTRACTS C
				INNER JOIN @T T ON C.CONTRACTOR_CODE = T.KON7 AND C.TYPE = 'PURCHASE'
			WHERE T.ID_GLOBAL = @ID_GLOBAL

			DECLARE @ID_CONTRACTS_GOODS_GLOBAL UNIQUEIDENTIFIER
            SET @ID_CONTRACTS_GOODS_GLOBAL = NULL
			SELECT @ID_CONTRACTS_GOODS_GLOBAL = CG.ID_CONTRACTS_GOODS_GLOBAL
			FROM CONTRACTS_GOODS CG
				INNER JOIN @T T ON CG.ID_GOODS = T.ID_GOODS
			WHERE CG.ID_CONTRACTS_GLOBAL = @ID_CONTRACTS_GLOBAL AND T.ID_GLOBAL = @ID_GLOBAL

			-- ���� ������� ���� � ��� ������ ������ � ��������
			IF((@ID_CONTRACTS_GLOBAL IS NOT NULL) AND (@ID_CONTRACTS_GOODS_GLOBAL IS NULL))
			BEGIN
				SET @ID_CONTRACTS_GOODS_GLOBAL = NEWID()
		
				-- ������� ����� ����� � �������
				INSERT INTO CONTRACTS_GOODS
					(ID_CONTRACTS_GOODS_GLOBAL,
					ID_CONTRACTS_GLOBAL,
					ID_GOODS,
					QUANTITY,
					PRICE,
					DATE_DELETED,
					CONTROL_TYPE)
				SELECT
					@ID_CONTRACTS_GOODS_GLOBAL,
					@ID_CONTRACTS_GLOBAL,
					T.ID_GOODS,
					T.OST2,
					T.P_SUP2,
					NULL,
					LEFT(T.CONTROL_TYPE,20)
				FROM @T T
				WHERE T.ID_GLOBAL = @ID_GLOBAL

				SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
				IF (@ERROR<>0)
				GOTO ERR

				UPDATE C SET REQ_AMOUNT = CSUMS.AMOUNT
				FROM
					CONTRACTS C
					INNER JOIN (
						SELECT CG.ID_CONTRACTS_GLOBAL, SUM(COALESCE(CG.PRICE * CG.QUANTITY, 0)) AS AMOUNT
						FROM CONTRACTS_GOODS CG
						GROUP BY CG.ID_CONTRACTS_GLOBAL
					) CSUMS ON (C.ID_CONTRACTS_GLOBAL = CSUMS.ID_CONTRACTS_GLOBAL)
				WHERE C.ID_CONTRACTS_GLOBAL = @ID_CONTRACTS_GLOBAL

				SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
				IF (@ERROR<>0)
				GOTO ERR
			END

			DECLARE @ID_IMPORT_REMAINS_ITEM_GLOBAL UNIQUEIDENTIFIER
			SET @ID_IMPORT_REMAINS_ITEM_GLOBAL = NEWID()

			-- ��������� ������ � ��������
			INSERT INTO IMPORT_REMAINS_ITEM(
				ID_IMPORT_REMAINS_ITEM_GLOBAL,
				ID_IMPORT_REMAINS_GLOBAL,
				ID_GOODS,
				ID_SCALING_RATIO,
				QUANTITY,
				ID_SUPPLIER,
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
				PRODUCER_PRICE,
				VAT,
				CODE_STU,
				ID_CONTRACTS_GLOBAL,
				ID_CONTRACTS_GOODS_GLOBAL
			)
			SELECT
				@ID_IMPORT_REMAINS_ITEM_GLOBAL,
				@ID_IMPORT_REMAINS_GLOBAL,
				T.ID_GOODS,
				T.ID_SCALING_RATIO,
				T.OST,
				T.ID_CONTRACTOR,
				SUPPLIER_VAT = NDS_SUP,
				SUPPLIER_ADPRICE =  (CASE WHEN T.NACP IS NOT NULL THEN T.NACP ELSE 0 END),
				SUPPLIER_PRICE = 0,
				SUPPLIER_PRICE_VAT = (CASE WHEN T.P_SUP IS NOT NULL THEN T.P_SUP ELSE 0 END),
				SUPPLIER_VAT_SUM = 0,
				SUPPLIER_SUM = 0,
				SUPPLIER_SUM_VAT = 0,
				RETAIL_VAT = NDS_APT,
				RETAIL_ADPRICE = 0,
				RETAIL_PRICE = 0,
				RETAIL_PRICE_VAT = (CASE WHEN T.P_APT IS NOT NULL THEN T.P_APT ELSE 0 END),
				RETAIL_VAT_SUM = 0,
				RETAIL_SUM = 0,
				RETAIL_SUM_VAT = 0,
				INCOMING_BILL_NUMBER = NULL,
				INCOMING_BILL_DATE = NULL,
				ID_STORE_PLACE = NULL,
				GTD_NUMBER = LEFT(T.TAM,100),
				ID_REG_CERT_GLOBAL = NULL,
				ID_SERIES_GLOBAL = @ID_SERIES,
				BARCODE = NULL,
				INCOMING_NUMBER = LEFT(T.NSC,50),
				INCOMING_DATE = T.DSC,
				PRODUCER_PRICE = T.CO,
				VAT = COALESCE(TT.TAX_RATE, 0),
				CODE_STU = NULL,
				ID_CONTRACTS_GLOBAL = CONTRACTS.ID_CONTRACTS_GLOBAL,
				ID_CONTRACTS_GOODS_GLOBAL = @ID_CONTRACTS_GOODS_GLOBAL
			FROM @T T
				INNER JOIN GOODS G ON G.ID_GOODS = T.ID_GOODS
				INNER JOIN CONTRACTOR SUP ON SUP.ID_CONTRACTOR = T.ID_CONTRACTOR
				INNER JOIN STORE S ON S.ID_STORE = @ID_STORE
				INNER JOIN CONTRACTOR APT ON APT.ID_CONTRACTOR = S.ID_CONTRACTOR
				LEFT JOIN TAX_TYPE TT ON G.ID_TAX_TYPE = TT.ID_TAX_TYPE
				LEFT JOIN CONTRACTS ON T.KON7 = CONTRACTS.CONTRACTOR_CODE AND CONTRACTS.TYPE = 'PURCHASE'
			WHERE
				T.ID_GLOBAL = @ID_GLOBAL

			SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
			IF (@ERROR<>0)
			GOTO ERR

			-- �������� ���, ���� �� ������
			UPDATE IMPORT_REMAINS_ITEM
			SET				
				SUPPLIER_PRICE = (SUPPLIER_PRICE_VAT * 100) / (100 + SUPPLIER_VAT),
				RETAIL_PRICE = (RETAIL_PRICE_VAT * 100) / (100 + RETAIL_VAT)
			WHERE 
				ID_IMPORT_REMAINS_ITEM_GLOBAL = @ID_IMPORT_REMAINS_ITEM_GLOBAL
			UPDATE IMPORT_REMAINS_ITEM
			SET				
				SUPPLIER_SUM = SUPPLIER_PRICE * QUANTITY,
				SUPPLIER_SUM_VAT = SUPPLIER_PRICE_VAT * QUANTITY,				
				RETAIL_SUM = RETAIL_PRICE * QUANTITY,
				RETAIL_SUM_VAT = RETAIL_PRICE_VAT * QUANTITY
			WHERE 
				ID_IMPORT_REMAINS_ITEM_GLOBAL = @ID_IMPORT_REMAINS_ITEM_GLOBAL

			UPDATE IMPORT_REMAINS_ITEM
			SET
				SUPPLIER_VAT_SUM = SUPPLIER_SUM_VAT - SUPPLIER_SUM,
				RETAIL_VAT_SUM = RETAIL_SUM_VAT - RETAIL_SUM
			WHERE 
				ID_IMPORT_REMAINS_ITEM_GLOBAL = @ID_IMPORT_REMAINS_ITEM_GLOBAL			


			FETCH NEXT FROM curItems INTO @ID_GLOBAL
		END	
		CLOSE curItems
		DEALLOCATE curItems

		-- �������� ���� �� ���������
		UPDATE IR
		SET
			IR.SUM_SUPPLIER = IRSUMS.SUM_SUPPLIER,
			IR.SUM_RETAIL = IRSUMS.SUM_RETAIL,
			IR.SVAT_SUPPLIER = IRSUMS.SVAT_SUPPLIER,
			IR.SVAT_RETAIL = IRSUMS.SVAT_RETAIL
		FROM IMPORT_REMAINS IR INNER JOIN
		(
			SELECT
				IRI.ID_IMPORT_REMAINS_GLOBAL,
				SUM(IRI.SUPPLIER_SUM_VAT) AS SUM_SUPPLIER,
				SUM(IRI.RETAIL_SUM_VAT) AS SUM_RETAIL,
				SUM(IRI.SUPPLIER_VAT_SUM) AS SVAT_SUPPLIER,
				SUM(IRI.RETAIL_VAT_SUM) AS SVAT_RETAIL
			FROM IMPORT_REMAINS_ITEM IRI
			GROUP BY IRI.ID_IMPORT_REMAINS_GLOBAL
		) IRSUMS ON (IR.ID_IMPORT_REMAINS_GLOBAL = IRSUMS.ID_IMPORT_REMAINS_GLOBAL)
		WHERE IR.ID_IMPORT_REMAINS_GLOBAL = @ID_IMPORT_REMAINS_GLOBAL

		-- ������ ������ �� ���������
		EXEC USP_IMPORT_REMAINS_LOT_GEN @ID_IMPORT_REMAINS_GLOBAL
 
		SELECT @ERROR = @@ERROR, @ROWCOUNT = @@ROWCOUNT
		IF (@ERROR<>0)
		GOTO ERR

		-- � ��������� � �������� ��������� ��� ���������� ������
		FETCH NEXT FROM curStores INTO @ID_STORE
	END	
	
	CLOSE curStores
	DEALLOCATE curStores

 COMMIT
 ERR:
     IF (@@TRANCOUNT<>0)
         ROLLBACK    
 RETURN
GO

-- EXEC REPEX_IMPORT_NOVGOROD_IMPORT NULL

IF (OBJECT_ID('REPEX_IMPORT_NOVGOROD_FIND_GOODS') IS NULL) EXEC ('CREATE PROCEDURE REPEX_IMPORT_NOVGOROD_FIND_GOODS AS RETURN')
GO

ALTER PROCEDURE REPEX_IMPORT_NOVGOROD_FIND_GOODS(
    @ANL VARCHAR(20),
	@NNOM VARCHAR(20),
	@IZG VARCHAR(20),
	@BARCOD VARCHAR(20),
	@ID_GOODS BIGINT OUTPUT,
	@CONTROL_TYPE VARCHAR(10) OUTPUT
)
AS
BEGIN
	IF (@ANL IS NOT NULL)
	BEGIN
		PRINT 'searching by anl...'

		SELECT
			@ID_GOODS = GC.ID_GOODS,
			@CONTROL_TYPE = 'LS'
		FROM (
				SELECT
					GC1.CODE,
					ID_GOODS = MIN(ID_GOODS),
					GOODS_FOUND = COUNT(DISTINCT ID_GOODS)
				FROM GOODS_CODE GC1
					INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
				WHERE (C.NAME='������')
					  AND GC1.DATE_DELETED IS NULL
				GROUP BY GC1.CODE) GC
		WHERE GC.CODE = @ANL

		IF (@@ROWCOUNT >= 1)
		BEGIN			
			PRINT 'goods found'
			RETURN
		END
	END
	IF (@NNOM IS NOT NULL) AND (@IZG IS NOT NULL)
	BEGIN
		PRINT 'searching by nnom+izg...'

		SELECT
			@ID_GOODS = GC.ID_GOODS,
			@CONTROL_TYPE = 'LS'
		FROM (
				SELECT
					GC1.CODE,
					ID_GOODS = MIN(ID_GOODS),
					GOODS_FOUND = COUNT(DISTINCT ID_GOODS)
				FROM GOODS_CODE GC1
					INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
				WHERE (C.NAME='���')
					  AND GC1.DATE_DELETED IS NULL
				GROUP BY GC1.CODE) GC
		WHERE GC.CODE = @NNOM + ' ' + @IZG

		IF (@@ROWCOUNT >= 1)
		BEGIN			
			PRINT 'goods found'
			RETURN
		END
	END
	IF (@BARCOD IS NOT NULL)
	BEGIN
		PRINT 'searching by barcod...'

		SELECT
			@ID_GOODS = BC.ID_GOODS,
			@CONTROL_TYPE = 'LS'
		FROM (
				SELECT
		            BC1.CODE,
			        ID_GOODS = MIN(ID_GOODS),
					GOODS_FOUND = COUNT(DISTINCT ID_GOODS)
	            FROM BAR_CODE BC1
		        WHERE BC1.DATE_DELETED IS NULL
			    GROUP BY BC1.CODE) BC
		WHERE BC.CODE = @BARCOD

		IF (@@ROWCOUNT >= 1)
		BEGIN			
			PRINT 'goods found'
			RETURN
		END
	END

	IF (@NNOM IS NOT NULL)
	BEGIN
		PRINT 'searching by nnom...'

		SELECT
			@ID_GOODS = GC.ID_GOODS,
			@CONTROL_TYPE = 'TN'
		FROM (
				SELECT
					GC1.CODE,
					ID_GOODS = MIN(ID_GOODS),
					GOODS_FOUND = COUNT(DISTINCT ID_GOODS)
				FROM GOODS_CODE GC1
					INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = GC1.ID_CONTRACTOR
				WHERE (C.NAME='���')
					  AND GC1.DATE_DELETED IS NULL
				GROUP BY GC1.CODE) GC
		WHERE GC.CODE LIKE @NNOM + '%'

		IF (@@ROWCOUNT >= 1)
		BEGIN			
			PRINT 'goods found'
			RETURN
		END
	END
END
GO

-- EXEC REPEX_IMPORT_NOVGOROD_FIND_GOODS NULL