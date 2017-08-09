SET NOCOUNT ON
SET QUOTED_IDENTIFIER OFF
-----------------------------------------------------------------------------------------
IF (OBJECT_ID('USP_TABLE_DATAS') IS NULL) EXEC ('CREATE PROCEDURE USP_TABLE_DATAS AS RETURN')
GO
ALTER PROCEDURE USP_TABLE_DATAS
AS
    SELECT CODE_OP = 'INVOICE', DESCRIPTION='Приходная накладная' UNION 
    SELECT 'ACT_RETURN_TO_CONTRACTOR', 'Акт возврата поставщику' UNION 
    SELECT 'ACT_RETURN_TO_BUYER', 'Акт возврата от покупателя' UNION 
    SELECT 'ACT_REVALUATION', 'Акт переоценки' UNION 
    SELECT 'IMPORT_REMAINS', 'Ввод остатков' UNION
    SELECT 'CASH_SESSION', 'Кассовая смена' UNION 
    SELECT 'ACT_DEDUCTION', 'Акт списания' UNION 
    SELECT 'INVOICE_OUT', 'Расходная накладная' UNION 
    SELECT 'BILL', 'Счет' UNION 
    SELECT 'INVENTORY_SVED', 'Инвентаризация сводная' UNION 
    SELECT 'VAT_CORRECT', 'Корректировка товарного отчета'
RETURN
GO
-----------------------------------------------------------------------------------------
IF OBJECT_ID('DBO.STATIST_ADD_SUB_REP_EX') IS NULL BEGIN
    EXEC('CREATE PROCEDURE DBO.STATIST_ADD_SUB_REP_EX AS RETURN')
    GRANT EXEC ON [DBO].[STATIST_ADD_SUB_REP_EX] TO [PUBLIC]
END
GO
ALTER PROCEDURE DBO.STATIST_ADD_SUB_REP_EX
    @XMLPARAM NTEXT
AS

DECLARE	@HDOC INT, @DATE_FROM DATETIME, @DATE_TO DATETIME, @ID_CONTRACTOR BIGINT

EXEC SP_XML_PREPAREDOCUMENT @HDOC OUTPUT , @XMLPARAM OUTPUT
    SELECT TOP 1
        @DATE_FROM = DATE_FROM,
        @DATE_TO = DATE_TO,
		@ID_CONTRACTOR = ID_CONTRACTOR
	FROM OPENXML(@HDOC , '/XML') WITH(
		DATE_FROM DATETIME 'DATE_FROM',
        DATE_TO DATETIME 'DATE_TO',
		ID_CONTRACTOR BIGINT 'ID_CONTRACTOR' 
	)

	SELECT CODE_OP,IS_INV,IS_EXP INTO #INV_EXP FROM OPENXML(@HDOC , '/XML/EXP') WITH(
        CODE_OP varchar(30) 'CODE_OP', IS_INV BIT 'IS_INV', IS_EXP BIT 'IS_EXP')

SELECT
	DOC_INFO = VL.DOC_NUM + 'от '+ VL.DOC_DATE,
	DOC_TYPE = VL.DOC_TYPE,
	INV_SUM = SUM(Vl.INV_SUM),
	EXP_SUM = SUM(Vl.EXP_SUM)
FROM (select 
	    doc_num = (case when i.id_invoice_global is not null then i.mnemocode
						when tb.id_act_return_to_buyer_global is not null then tb.mnemocode
	                    when tc.id_act_return_to_contractor_global is not null then tc.mnemocode
	                    when ar.id_act_revaluation2_global is not null then ar.mnemocode	
                        when ir.id_import_remains_global is not null then ir.mnemocode  								
	                    when cs.id_cash_session_global is not null then  cs.mnemocode
	                    when ad.id_act_deduction_global is not null then ad.mnemocode
						when invo.id_invoice_out_global is not null then invo.mnemocode
						when b.id_bill_global is not null then b.doc_num
						when invs.id_inventory_global is not null then invs.doc_num
						when vc.id_vat_correct_global is not null then vc.doc_num
			            else null
	                end),		
	    doc_type = (case when i.id_invoice_global is not null then 'Приходная накладная'
						when tb.id_act_return_to_buyer_global is not null then 'Акт возврата от покупателя'
	                    when tc.id_act_return_to_contractor_global is not null then 'Акт возврата поставщику'
	                    when ar.id_act_revaluation2_global is not null then 'Акт переоценки'
                        when ir.id_import_remains_global is not null then 'Ввод остатков'
	                    when cs.id_cash_session_global is not null then 'Кассовая смена'
	                    when ad.id_act_deduction_global is not null then 'Акт списания'
						when invo.id_invoice_out_global is not null then 'Раходная накладная'
						when b.id_bill_global is not null then 'Счет'
						when invs.id_inventory_global is not null then 'Инвентаризация сводная'
						when vc.id_vat_correct_global is not null then 'Корректировка товарного отчета'
			            else 'Неизвестный документ'
	                end),	
		doc_date = (case when i.id_invoice_global is not null then substring(convert(varchar, i.document_date,4),1,8)
						when tb.id_act_return_to_buyer_global is not null then substring(convert(varchar, tb.date,4),1,8)
	                    when tc.id_act_return_to_contractor_global is not null then substring(convert(varchar, tc.date,4),1,8)
	                    when ar.id_act_revaluation2_global is not null then substring(convert(varchar, ar.date,4),1,8)
                        when ir.id_import_remains_global is not null then substring(convert(varchar, ir.document_date,4),1,8)
	                    when cs.id_cash_session_global is not null then substring(convert(varchar, cs.date_open,4),1,8)
	                    when ad.id_act_deduction_global is not null then substring(convert(varchar, ad.date,4),1,8)
						when invo.id_invoice_out_global is not null then substring(convert(varchar, invo.date,4),1,8)
						when b.id_bill_global is not null then substring(convert(varchar, b.doc_date,4),1,8)
						when invs.id_inventory_global is not null then substring(convert(varchar, invs.doc_date,4),1,8)
						when vc.id_vat_correct_global is not null then substring(convert(varchar, vc.doc_date,4),1,8)
			            else null
					end),
		inv_sum = case when ie.is_inv = 1 then lm.sum_acc * case when lm.quantity_sub!=0 then lm.quantity_sub else lm.quantity_add end else 0 end,
		exp_sum = case when ie.is_exp = 1 then lm.sum_acc * case when lm.quantity_add!=0 then lm.quantity_add else lm.quantity_sub end else 0 end
	from lot_movement lm
	inner join lot l on l.id_lot_global = lm.id_lot_global
	inner join store s on s.id_store = l.id_store
	inner join #inv_exp ie on ie.code_op = lm.code_op
	left join invoice i on lm.id_document = i.id_invoice_global
	left join act_return_to_buyer tb on lm.id_document = tb.id_act_return_to_buyer_global
	left join act_return_to_contractor tc on lm.id_document = tc.id_act_return_to_contractor_global
	left join act_revaluation2 ar on lm.id_document = ar.id_act_revaluation2_global
    left join import_remains ir on ir.id_import_remains_global = lm.id_document
	left join cash_session cs on lm.id_document = cs.id_cash_session_global
	left join act_deduction  ad on lm.id_document = ad.id_act_deduction_global
	left join invoice_out invo on lm.id_document = invo.id_invoice_out_global
	left join bill b on lm.id_document = b.id_bill_global
	left join inventory_sved invs on lm.id_document = invs.id_inventory_global
	left join vat_correct vc on lm.id_document = vc.id_vat_correct_global
	where s.id_contractor = @id_contractor ) vl
GROUP BY VL.DOC_TYPE, VL.DOC_NUM, VL.DOC_DATE
ORDER BY DOC_TYPE,DOC_DATE

RETURN 0
GO

--EXEC STATIST_ADD_SUB_REP_EX '<XML><DATE_FROM>2006-01-01</DATE_FROM><DATE_TO>2009-12-31</DATE_TO><ID_CONTRACTOR>5271</ID_CONTRACTOR><EXP><CODE_OP>invoice_out</CODE_OP><IS_INV>1</IS_INV><IS_EXP>0</IS_EXP></EXP></XML>'
--exec STATIST_ADD_SUB_REP_EX @xmlParam = N'<XML><DATE_FROM>2006-12-25T12:31:21.187</DATE_FROM><DATE_TO>2009-02-03T12:31:21.187</DATE_TO><ID_CONTRACTOR>5271</ID_CONTRACTOR><DOCUMENTS>0</DOCUMENTS><EXP><CODE_OP>invoice</CODE_OP><IS_INV>1</IS_INV><IS_EXP>0</IS_EXP></EXP><EXP><CODE_OP>BILL</CODE_OP><IS_INV>0</IS_INV><IS_EXP>1</IS_EXP></EXP><EXP><CODE_OP>invoice_out</CODE_OP><IS_INV>1</IS_INV><IS_EXP>0</IS_EXP></EXP></XML>'
--exec STATIST_ADD_SUB_REP_EX @xmlParam = N'<XML><DATE_FROM>2009-03-03T00:00:00.000</DATE_FROM><DATE_TO>2009-03-16T00:00:00.000</DATE_TO><ID_CONTRACTOR>5271</ID_CONTRACTOR><DOCUMENTS>0</DOCUMENTS><EXP><CODE_OP>IMPORT_REMAINS</CODE_OP><IS_INV>1</IS_INV><IS_EXP>0</IS_EXP></EXP></XML>'