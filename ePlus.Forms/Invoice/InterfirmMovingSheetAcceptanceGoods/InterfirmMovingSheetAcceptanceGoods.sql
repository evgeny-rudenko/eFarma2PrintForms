SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('[REPEX_INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS]') IS NULL EXEC('CREATE PROCEDURE [REPEX_INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS] AS RETURN')
GO
ALTER PROCEDURE REPEX_INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS
	(@XMLPARAM NTEXT) AS
	
DECLARE @HDOC INT
DECLARE @ID_INTERFIRM_MOVING BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 @ID_INTERFIRM_MOVING = ID_INTERFIRM_MOVING
	FROM OPENXML(@HDOC, '/XML') 
	WITH(ID_INTERFIRM_MOVING BIGINT 'ID_INTERFIRM_MOVING')
EXEC SP_XML_REMOVEDOCUMENT @HDOC

-- ШАПКА И СПИСОК ТОВАРОВ В ПРИХОДНОЙ НАКЛОДНОЙ
SELECT
    ITEM.ID_INTERFIRM_MOVING_ITEM_GLOBAL,
    
    -- НОМЕР ПРИХОДНОЙ НАКЛОДНОЙ
    IM.MNEMOCODE,
    -- ДАТА ПРИХОДНОЙ НАКЛАДНОЙ
    INV0ICE_DATE = CONVERT(VARCHAR, IM.[DATE], 104),
    -- ВХОДЯЩИЙ ДОКУМЕНТ
    INCOMING_NUMBER = '' /* COALESCE(M.INCOMING_NUMBER, '')+
        COALESCE(' от '+CONVERT(VARCHAR, M.INCOMING_DATE, 104), '') */,
    -- ПОСТАВЩИК 
    SUPPLIER_NAME = case 
                      when CNR_SUP.[FULL_NAME] is null then CNR_SUP.[NAME]
                      else CNR_SUP.[FULL_NAME]
                      end,
    -- ПОКУПАТЕЛЬ
    CUSTOMER_NAME = (SELECT TOP 1 case 
                                   when C.[FULL_NAME] is null then C.[NAME]
                                   else C.[FULL_NAME]
                                   end 
                     FROM DBO.INTERFIRM_MOVING IM
					INNER JOIN DBO.STORE S ON S.ID_STORE = IM.ID_STORE_TO_MAIN 
					INNER JOIN DBO.CONTRACTOR C ON S.ID_CONTRACTOR = C.ID_CONTRACTOR
					WHERE IM.ID_INTERFIRM_MOVING = @ID_INTERFIRM_MOVING),
    --КОД товара
    CODE = G.CODE,
    --Наименование
    GOODS = G.[NAME] + ' [' + PROD.NAME + ', ' + CNR.NAME + ']',
    --Срок годности
    BEST_BEFORE = ISNULL(CONVERT(VARCHAR,SER.BEST_BEFORE,104),''),
    --Срок годности скорректированный
    BEST_BEFORE_CORRECT = NULL,
    --Серия
    SERIES_NUMBER = ISNULL(SER.SERIES_NUMBER,''),
    --№ короба
    BOX = L_FROM.BOX,
    --внутренний штрих код
    INTERNAL_BARCODE = L_TO.INTERNAL_BARCODE,
    --количество по документу 
    QUANTITY = CAST(ROUND(ITEM.QUANTITY, 0) AS VARCHAR),
	--фактическое количество 
	QUANTITY_FACT = NULL,
	--Место хранения  данные из справочника номенклатуры
	STORE_PLACE = SP.DESCRIPTION
    --select * from GOODS
FROM DBO.INTERFIRM_MOVING IM --M
  LEFT JOIN STORE S_SUP ON IM.ID_STORE_FROM_MAIN = S_SUP.ID_STORE
  INNER JOIN DBO.CONTRACTOR CNR_SUP ON CNR_SUP.ID_CONTRACTOR = S_SUP.ID_CONTRACTOR
  INNER JOIN DBO.INTERFIRM_MOVING_ITEM ITEM ON ITEM.ID_INTERFIRM_MOVING_GLOBAL = IM.ID_INTERFIRM_MOVING_GLOBAL
  INNER JOIN LOT L_TO ON L_TO.ID_LOT = ITEM.ID_LOT_TO
  LEFT JOIN LOT L_FROM ON L_FROM.ID_LOT = ITEM.ID_LOT_FROM
  INNER JOIN DBO.GOODS G ON G.ID_GOODS = L_TO.ID_GOODS
  INNER JOIN DBO.PRODUCER PROD ON G.ID_PRODUCER = PROD.ID_PRODUCER
  INNER JOIN DBO.COUNTRY CNR ON PROD.ID_COUNTRY = CNR.ID_COUNTRY

  LEFT JOIN DBO.SERIES SER ON L_FROM.ID_SERIES = SER.ID_SERIES 
  LEFT JOIN STORE_PLACE SP ON G.ID_STORE_PLACE = SP.ID_STORE_PLACE
WHERE (IM.ID_INTERFIRM_MOVING = @ID_INTERFIRM_MOVING)
ORDER BY GOODS


SELECT
	DIR = DIRECTOR_FIO,
	BUH = BUH_FIO
FROM CONTRACTOR
WHERE ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)


RETURN 0
GO


--exec REPEX_INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS N'<XML><ID_INTERFIRM_MOVING>57</ID_INTERFIRM_MOVING></XML>'
--select newid()
--select * from INVOICE_ITEM



SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
GO 

IF OBJECT_ID('DBO.REMOVE_REPORT_BY_TYPE_NAME') IS NULL EXEC('CREATE PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME AS RETURN')
GO
ALTER PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME 
	@REPORT_TYPE_NAME VARCHAR(200) AS
	
DECLARE @id_meta_report BIGINT

	select 
		@id_meta_report = id_meta_report
	from meta_report
	where type_name = @REPORT_TYPE_NAME
	--select @id_meta_report
		
	DECLARE @SQL NVARCHAR(200)
	SET @SQL = N'delete from META_REPORT_2_REPORT_GROUPS
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('META_REPORT_2_REPORT_GROUPS') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_csv_export
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_csv_export') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_visible
		where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_visible') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
		

	SET @SQL = N'delete from meta_report_settings_managed
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_managed') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	SET @SQL = N'delete from meta_report_settings_archive
				where id_meta_report = @id_meta_report'
	IF (OBJECT_ID('meta_report_settings_archive') IS NOT NULL)
		EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	delete from meta_report
	where id_meta_report = @id_meta_report

RETURN 0
GO

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'InterfirmMovingSheetAcceptanceGoods.InterfirmMovingSheetAcceptanceGoods'