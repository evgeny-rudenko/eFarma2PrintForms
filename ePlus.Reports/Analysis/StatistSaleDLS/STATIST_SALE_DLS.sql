SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
-------------------------------------------------------------------------------------------
IF OBJECT_ID('DBO.REP_STATIST_SALE_DLS') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.REP_STATIST_SALE_DLS AS RETURN')
END
GO

ALTER PROCEDURE DBO.REP_STATIST_SALE_DLS 
	@XMLPARAM NTEXT 
AS

/* PARAMETERS */
DECLARE @HDOC INT
DECLARE @DATE_FR DATETIME, @DATE_TO DATETIME

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT, @XMLPARAM OUTPUT
	SELECT TOP 1 
		@DATE_FR = DATE_FR, 
		@DATE_TO = DATE_TO
	FROM OPENXML(@HDOC, '/XML') WITH(
		DATE_FR DATETIME 'DATE_FR', 
		DATE_TO DATETIME 'DATE_TO'
	)


EXEC SP_XML_REMOVEDOCUMENT @HDOC

EXEC DBO.USP_RANGE_NORM @DATE_FR OUT, @DATE_TO OUT
EXEC DBO.REP_RANGEDAY @DATE_FR OUT, @DATE_TO OUT


            SELECT 
				DATE_CHEQUE = CH.DATE_CHEQUE,
				A_COD = C.NAME,
				NUMBER_POLICY = DR.PPL_NUM,--NUMBER_POLICY,
				PERSON = ISNULL(DR.PPL_NAME,''),
				GOODS_NAME = G.NAME,
				QUANTITY = DRI.QUANTITY,
				SUMM_INSURER = ISNULL(DRI.SUM_SK,0),
				SUMM_CASH = ISNULL(DRI.SUM_PAY,0),
				SUMM_ALL = ISNULL(DRI.SUM_SK,0)+ISNULL(DRI.SUM_PAY,0)
				--select *
            FROM DLS_RECIPE_ITEM DRI
            INNER JOIN DLS_RECIPE DR ON DR.ID_RECIPE_GLOBAL = DRI.ID_RECIPE_GLOBAL
            INNER JOIN CHEQUE_ITEM CH_I on CH_I.id_CHEQUE_ITEM_global = DRI.id_RECIPE_ITEM_global
            INNER JOIN CHEQUE CH ON CH.ID_CHEQUE_GLOBAL = CH_I.ID_CHEQUE_GLOBAL
            INNER JOIN CASH_SESSION CS ON CS.ID_CASH_SESSION_GLOBAL = CH.ID_CASH_SESSION_GLOBAL 
            INNER JOIN CASH_REGISTER CR ON CS.ID_CASH_REGISTER = CR.ID_CASH_REGISTER
            INNER JOIN CONTRACTOR C ON CR.ID_CONTRACTOR = C.ID_CONTRACTOR
            INNER JOIN LOT L ON  L.ID_LOT_GLOBAL = CH_I.ID_LOT_GLOBAL
            INNER JOIN GOODS G ON G.ID_GOODS = L.ID_GOODS
            INNER JOIN TAX_TYPE TT ON TT.ID_TAX_TYPE = G.ID_TAX_TYPE
            --left JOIN TRUST_LETTER TL ON CH.ID_CHEQUE_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL
			--LEFT JOIN DISCOUNT2_MEMBER DM ON DM.ID_DISCOUNT2_MEMBER_GLOBAL = TL.ID_DISCOUNT2_MEMBER_GLOBAL

           -- right join #GOODS Fg on fg.id_goods = G.id_goods
            WHERE 
            CH.DATE_CHEQUE >= @DATE_FR AND CH.DATE_CHEQUE <= @DATE_TO
            and CH.CHEQUE_TYPE='SALE' AND DR.DATE_DELETED IS NULL
            ORDER  BY 
				CH.DATE_CHEQUE
            -- C.ID_CONTRACTOR = @ID_AU AND CH.DOCUMENT_STATE = 'PROC' AND CH.CHEQUE_TYPE in ('SALE','RETURN')
           -- AND exists(select null from @stores s where s.id_store = l.id_store)
RETURN 0
GO
/*
exec REP_STATIST_SALE_DLS @xmlParam = N'
<XML>
<DATE_FR>2010-10-22T13:18:27.750</DATE_FR>
<DATE_TO>2011-12-22T13:18:27.750</DATE_TO>
</XML>'
*/
/*
--exec REP_KKMZREPORT_PLANET @xmlParam = N'<XML><DATE_FR>2008-01-01T13:18:27.750</DATE_FR><DATE_TO>2008-12-31T13:18:27.750</DATE_TO></XML>'
select * from CHEQUE
select * from CHEQUE_PAYMENT where type_payment ='TYPE4'
select * from dbo.CHEQUE_ITEM
select * from dbo.DISCOUNT2_MEMBER
--update dbo.CASH_REGISTER set ext_setting = '<EnableGoodsPackingInfo Enable="True" DataType="Bool" Description="Печатать фасовочный вкладыш">False</EnableGoodsPackingInfo><EnableDiscountControl Enable="True" DataType="Bool" Description="Контроль применения ДК">False</EnableDiscountControl><ServicesColorRGB Enable="True" DataType="ColorRGB" Description="Цвет товара акселератора">100 100 100</ServicesColorRGB><AcceleratorRecordColorRGB Enable="True" DataType="ColorRGB" Description="Цвет товара акселератора">255 128 0</AcceleratorRecordColorRGB><PrismaServerPort Enable="True" DataType="Int" Description="Порт событийного сервера Призма">21845</PrismaServerPort><PrismaServerIP Enable="True" DataType="IPAddress" Description="IP адресс событийного сервера Призма">127.0.0.1</PrismaServerIP><EnablePrisma Enable="True" DataType="Bool" Description="Передавать события АРМ в систему Призма">False</EnablePrisma><PrintDiscountPercent Enable="True" DataType="Bool" Description="Печать процента скидки на чеке">True</PrintDiscountPercent><DisableOfflineWork Enable="True" DataType="Bool" Description="Запрет работы в офлайн режиме">False</DisableOfflineWork><PrintVatTaxValues Enable="True" DataType="Bool" Description="Печать сумм с разбивкой по НДС">False</PrintVatTaxValues><SelectedRecordColorRGB Enable="True" DataType="ColorRGB" Description="Цвет выделенной строки в талбице">0 64 128</SelectedRecordColorRGB><EnableAutoUseDiscountCard Enable="True" DataType="Bool" Description="Включить функционал автоматического применения ДК">True</EnableAutoUseDiscountCard><EnableLotoCard Enable="True" DataType="Bool" Description="Включить карты Лото">True</EnableLotoCard><EnableProvisorInterface Enable="True" DataType="Bool" Description="Включить интерфейс провизора">False</EnableProvisorInterface><SearchInternalBarcodeOnly Enable="True" DataType="Bool" Description="Подбирать товар в чек только по внутреннему ШК">True</SearchInternalBarcodeOnly><EnableARMLocking Enable="True" DataType="Bool" Description="Включить блокировку АРМа в случае возникновения ошибок синхронизации с еФарама2">False</EnableARMLocking><VerifyTimeDifferenceChequePrint Enable="True" DataType="Bool" Description="Включить проверку расхождения времени фискального регистратора и компьютера до печати чека">False</VerifyTimeDifferenceChequePrint><PrintGoodsCheque Enable="True" DataType="Bool" Description="Автоматическая печать товарного чека">False</PrintGoodsCheque><EnableBaloonDescription Enable="True" DataType="Bool" Description="Включить всплывающее описание товара">True</EnableBaloonDescription><EnableAP Enable="True" DataType="Bool" Description="Использовать альтернативный поиск для подбора предложений покупателям">True</EnableAP><HeaderText Enable="True" DataType="StringMultiline" Description="Текст в заголовке чека">******ЗАГОЛОВОК******</HeaderText><AnalogColorRGB Enable="True" DataType="ColorRGB" Description="Цвет товаров аналогов (Формирование предложений покупателю)">255 255 0</AnalogColorRGB><EnableMixPayment Enable="True" DataType="Bool" Description="Разрешить смешанные платежи">True</EnableMixPayment><EnableAcquiring Enable="True" DataType="Bool" Description="Разрешить использование эквайринга">False</EnableAcquiring><DisassmRecordColorRGB Enable="True" DataType="ColorRGB" Description="Цвет разукомплектованных позиций">0 255 128</DisassmRecordColorRGB><ExpireSeriesRecordColorRGB Enable="True" DataType="ColorRGB" Description="Цвет позиций с истекающим сроком годности">255 0 0</ExpireSeriesRecordColorRGB><ChangeRemainsLocaly Enable="False" DataType="Bool" Description="Локальное изменение остатков">True</ChangeRemainsLocaly><PrintSectionReport Enable="True" DataType="Bool" Description="Печать отчета по секциям">False</PrintSectionReport><ExpireSeriesPeriod Enable="True" DataType="Int" Description="Период начала подсветки товаров с истекающим сроком годности">30</ExpireSeriesPeriod><UseFIFO Enable="True" DataType="Bool" Description="Использовать FIFO при подборе товара по внеш. ШК">False</UseFIFO><UseEAN128 Enable="True" DataType="Bool" Description="Использовать символьные ШК">False</UseEAN128><PILOT_NT_PATH Enable="True" DataType="String" Description="Полный путь к библиотеке Pilot_nt.dll (Необходимо для работы эквайринга)">c:\sbbank\Pilot_nt.dll</PILOT_NT_PATH><AskBeforeChequePrint Enable="True" DataType="Bool" Description="Требовать подтверждения перед печатью чека">True</AskBeforeChequePrint>'
*/


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

EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME 'StatistSaleDLS.FormParams'