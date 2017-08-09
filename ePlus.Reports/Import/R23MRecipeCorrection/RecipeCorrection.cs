using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;

namespace R23MRecipeCorrection
{
	public partial class R23MRecipeCorrectionParams : ExternalReportForm, IExternalReportFormMethods
	{
        public R23MRecipeCorrectionParams()
		{
			InitializeComponent();

			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
            return;
            /*
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "True") 
                    Utils.AddNode(root, "ID_INVOICE_ITEM", dataGridView1.Rows[i].Cells[1].Value.ToString());
            }*/
		}
        public void RefreshTables()
        {
            StringBuilder queryString;

            #region Table1
            queryString = new StringBuilder("");
            
            queryString.Append(" select ");
            queryString.Append(" 	id = TL.id_trust_letter_global, ");
            queryString.Append(" 	pack_name = UP.number_pack, ");
            queryString.Append(" 	recipe = TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE, ");
            queryString.Append(" 	name = TL.MNEMOCODE,    ");
            queryString.Append(" 	tl_date = CONVERT(VARCHAR, TL.DOC_DATE, 104),    ");
            queryString.Append("    sum_pay = SUM(TLI.PRICE_SAL * TLI.QUANTITY), ");
            queryString.Append(" 	reg_date = convert(varchar,TL.DATE_REG,104), ");
            queryString.Append(" 	pack_date = convert(varchar,up.date_pack,104), ");
            queryString.Append(" 	lpu_name = DMO.name,    ");
            queryString.Append("    polis = TL.NUMBER_POLICY,  ");  
            queryString.Append("    member_name = ISNULL(MEM.LASTNAME + ' ', '') + ISNULL(UPPER(LEFT(MEM.FIRSTNAME,1)) + '. ', '') + ISNULL(UPPER(LEFT(MEM.MIDDLENAME,1))+'.', '') ");
            queryString.Append(" from TRUST_LETTER TL ");
            queryString.Append(" INNER JOIN SGAU_REP_UZO_PACK UP ON (TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR)+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR, UP.date_pack, 104)) ");
            queryString.Append(" inner join trust_letter_item TLI on TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL  ");
            queryString.Append(" left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION    ");
            queryString.Append(" left join DISCOUNT2_MEMBER MEM on TL.ID_DISCOUNT2_MEMBER_GLOBAL = MEM.ID_DISCOUNT2_MEMBER_GLOBAL    ");
            queryString.Append(" WHERE  ");
            queryString.Append(" 	TL.document_state = 'PROC' ");
            queryString.Append(" 	and convert(varchar(7),DATE_REG,120) <> convert(varchar(7),up.date_pack,120) ");
            queryString.Append(" 	and	convert(varchar(7),DOC_DATE,120) = convert(varchar(7),up.date_pack,120) ");
            queryString.Append(" GROUP BY   ");
            queryString.Append(" 	TL.id_trust_letter_global,   ");
            queryString.Append("     TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,   ");
            queryString.Append(" 	TL.MNEMOCODE,    ");
            queryString.Append("     TL.DOC_DATE, TL.DATE_REG, up.date_pack, ");
            queryString.Append("     DMO.name, UP.number_pack, ");
            queryString.Append("     TL.NUMBER_POLICY,    ");
            queryString.Append("     MEM.LASTNAME, MEM.FIRSTNAME, MEM.MIDDLENAME ");
            dataGridViewEx1.Rows.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridViewEx1.Rows.Add("True", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9], reader[10]);
                }
                reader.Close();
            }
            #endregion

            #region Table2
            queryString = new StringBuilder("");

            queryString.Append(" select ");
            queryString.Append(" 	id = TL.id_trust_letter_global, ");
            queryString.Append(" 	pack_name = UP.number_pack, ");
            queryString.Append(" 	recipe = TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE, ");
            queryString.Append(" 	name = TL.MNEMOCODE,    ");
            queryString.Append(" 	tl_date = CONVERT(VARCHAR, TL.DOC_DATE, 104),    ");
            queryString.Append("    sum_pay = SUM(TLI.PRICE_SAL * TLI.QUANTITY), ");
            queryString.Append(" 	reg_date = convert(varchar,TL.DATE_REG,104), ");
            queryString.Append(" 	pack_date = convert(varchar,up.date_pack,104), ");
            queryString.Append(" 	lpu_name = DMO.name,    ");
            queryString.Append("    polis = TL.NUMBER_POLICY,  ");
            queryString.Append("    member_name = ISNULL(MEM.LASTNAME + ' ', '') + ISNULL(UPPER(LEFT(MEM.FIRSTNAME,1)) + '. ', '') + ISNULL(UPPER(LEFT(MEM.MIDDLENAME,1))+'.', '') ");
            queryString.Append(" from TRUST_LETTER TL ");
            queryString.Append(" INNER JOIN SGAU_REP_UZO_PACK UP ON (TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR)+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR, UP.date_pack, 104)) ");
            queryString.Append(" inner join trust_letter_item TLI on TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL  ");
            queryString.Append(" left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION    ");
            queryString.Append(" left join DISCOUNT2_MEMBER MEM on TL.ID_DISCOUNT2_MEMBER_GLOBAL = MEM.ID_DISCOUNT2_MEMBER_GLOBAL    ");
            queryString.Append(" WHERE  ");
            queryString.Append(" 	TL.document_state = 'PROC' ");
            queryString.Append(" 	and convert(varchar(7),DATE_REG,120) <> convert(varchar(7),up.date_pack,120) ");
            queryString.Append(" 	and	convert(varchar(7),DOC_DATE,120) <> convert(varchar(7),up.date_pack,120) ");
            queryString.Append(" GROUP BY   ");
            queryString.Append(" 	TL.id_trust_letter_global,   ");
            queryString.Append("     TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,   ");
            queryString.Append(" 	TL.MNEMOCODE,    ");
            queryString.Append("     TL.DOC_DATE, TL.DATE_REG, up.date_pack, ");
            queryString.Append("     DMO.name, UP.number_pack, ");
            queryString.Append("     TL.NUMBER_POLICY,    ");
            queryString.Append("     MEM.LASTNAME, MEM.FIRSTNAME, MEM.MIDDLENAME ");
            dataGridViewEx2.Rows.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridViewEx2.Rows.Add("True", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9], reader[10]);
                }
                reader.Close();
            }
            #endregion

            #region Table3
            queryString = new StringBuilder("");

            queryString.Append("  select  ");
            queryString.Append("      id = TL.id_trust_letter_global,   ");
            queryString.Append("      recipe = TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,   ");
            queryString.Append("      name = TL.MNEMOCODE,    ");
            queryString.Append("      DATE_TL = CONVERT(VARCHAR, TL.DATE_SHIP, 104),    ");
            queryString.Append("      SUM_PAY = SUM(TLI.PRICE_SAL * TLI.QUANTITY),    ");
            queryString.Append("      DMO.name,    ");
            queryString.Append("      POLIS = TL.NUMBER_POLICY,    ");
            queryString.Append("      MEM_NAME = ISNULL(MEM.LASTNAME + ' ', '') + ISNULL(UPPER(LEFT(MEM.FIRSTNAME,1)) + '. ', '') + ISNULL(UPPER(LEFT(MEM.MIDDLENAME,1))+'.', ''), ");
            queryString.Append("      BALANCE = CASE WHEN STT.MNEMOCODE = 'RLO' THEN 'Забаланс' ELSE 'Баланс' END, ");
			queryString.Append(" 	  PACK_NAME = ISNULL(TL.reestr_number, '') ");
            queryString.Append("  from trust_letter TL    ");
            queryString.Append("  inner join store ST on TL.id_store = ST.id_store  ");
            queryString.Append("  inner join store_type STT on ST.id_store_type_global = STT.id_store_type_global  ");
            queryString.Append("  inner join trust_letter_item TLI on TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL  ");
            queryString.Append("  left join SGAU_REP_UZO_PACK UP on ((TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR)+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR, UP.date_pack, 104)) and TL.DOCUMENT_STATE='PROC')    ");
            queryString.Append("  left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION    ");
            queryString.Append("  left join DISCOUNT2_MEMBER MEM on TL.ID_DISCOUNT2_MEMBER_GLOBAL = MEM.ID_DISCOUNT2_MEMBER_GLOBAL    ");
            queryString.Append("  WHERE   ");
            queryString.Append("      ISNULL(UP.id_pack,'')='' AND TL.document_state = 'PROC' ");
            queryString.Append("  GROUP BY   ");
            queryString.Append(" 	TL.id_trust_letter_global,   ");
            queryString.Append("     TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,   ");
            queryString.Append("     TL.MNEMOCODE, TL.reestr_number, ");
            queryString.Append("     TL.DATE_SHIP,  ");
            queryString.Append("     DMO.name,    ");
            queryString.Append("     TL.NUMBER_POLICY,    ");
            queryString.Append("     MEM.LASTNAME, MEM.FIRSTNAME, MEM.MIDDLENAME, ");
            queryString.Append("     STT.MNEMOCODE ");
            dataGridViewEx3.Rows.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridViewEx3.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]);
                }
                reader.Close();
            }
            #endregion
        }

		public string ReportName
		{
			get { return "Корректировка товарного отчета по льготе (СГАУ)"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
            return;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTables();
        }

        private void R23MRecipeCorrectionParams_Load(object sender, EventArgs e)
        {
            RefreshTables();
        }

        private void btnHelp1_Click(object sender, EventArgs e)
        {
            string msg;
            msg = "=== #1 Проблема повторной отработки ===\n";
            msg += "Снятие товара с остатков производится на дату отработки доверительного письма (рецепта).\n";
            msg += "Таким образом после редактирования доверительного письма, может появиться расхождение в остатках.\n";
            msg += "--- Список документов\n";
            msg += "В эту таблицу попадают такие документы, дата (сравнивается год и месяц) отработки (Дата в ТО) которых\n";
            msg += "не соответствует дате реестра, но дата документа - соответствует.\n";
            msg += "--- Действие пункта \"Исправить\"\n";
            msg += "Для отмеченных документов дата снятия товара с остатков устанавливается равной дате документа.";
            MessageBox.Show(msg);
        }

        private void btnHelp2_Click(object sender, EventArgs e)
        {
            string msg;
            msg = "=== #2 Проблема несоответствия даты рецепта и даты реестра ===\n";
            msg += "Снятие товара с остатков производится на дату отработки доверительного письма (рецепта).\n";
            msg += "Таким образом после редактирования доверительного письма, может появиться расхождение в остатках.\n";
            msg += "--- Список документов\n";
            msg += "В эту таблицу попадают такие документы, дата (сравнивается год и месяц) документа которых не соответствует дате реестра.\n";
            msg += "Эти документы вы можете исправить самостоятельно, отменив отработку документа и установив\n";
            msg += "нужную дату (но в этом случае эти документы скорее всего попадут под условие проблемы #1), либо воспользоваться кнопкой \"Исправить\".\n";
            msg += "--- Действие пункта \"Исправить\"\n";
            msg += "Для отмеченных документов дата документа и дата снятия товара с остатков устанавливается первым числом месяца,";
            msg += "которым выписан был реестр этого письма.";
            MessageBox.Show(msg);
        }

        private void btnHelp3_Click(object sender, EventArgs e)
        {
            string msg;
            msg = "=== #3 Проблема неучтенных в реестрах рецептов ===\n";
            msg += "В товарный отчет по льготе попадают РЕЕСТРЫ, а в обычный товарный отчет РЕЦЕПТЫ.\n";
            msg += "Из-за наличие доверительных писем (рецептов), неучтенных в реестрах, может давать расхождение в товарных отчетах.\n";
            msg += "--- Список документов\n";
            msg += "В эту таблицу попадают такие документы, которые не учтены ни в одном из реестров.\n";
            msg += "Автоматического исправления по данной проблеме не предусмотрено.\n";
            msg += "Для решения данной проблемы вам необходимо или учесть эти рецепты в каком-либо уже существующем реестре,\n";
            msg += "или сформировать новый реестр для этих рецептов.\n";
            MessageBox.Show(msg);
        }

        public void CorrectTL_on_problem1(string guid)
        {
            string q_str = "UPDATE TRUST_LETTER SET DATE_REG = DOC_DATE WHERE ID_TRUST_LETTER_GLOBAL = '" + guid + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(q_str, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void CorrectTL_on_problem2(string guid)
        {
            StringBuilder queryString = new StringBuilder("");

            queryString.Append(" declare @up_date datetime ");
            queryString.Append(" select top 1 ");
            queryString.Append(" 	@up_date = cast((convert(varchar(7),up.date_pack,120)+'-01') as datetime ) ");
            queryString.Append(" from TRUST_LETTER TL ");
            queryString.Append(" INNER JOIN SGAU_REP_UZO_PACK UP ON (TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR)+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR, UP.date_pack, 104)) ");
            queryString.Append(" where ID_TRUST_LETTER_GLOBAL = '" + guid + "' ");
            queryString.Append(" update TRUST_LETTER ");
            queryString.Append(" set DOC_DATE = @up_date, DATE_REG = @up_date ");
            queryString.Append(" where ID_TRUST_LETTER_GLOBAL = '" + guid + "' ");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void btnCorrect1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
            {
                if (dataGridViewEx1.Rows[i].Cells[0].Value.ToString() == "True")
                    CorrectTL_on_problem1(dataGridViewEx1.Rows[i].Cells[1].Value.ToString());
            }
            RefreshTables();
            MessageBox.Show("Ок!");
        }

        private void btnCorrect2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewEx2.Rows.Count; i++)
            {
                if (dataGridViewEx2.Rows[i].Cells[0].Value.ToString() == "True")
                    CorrectTL_on_problem2(dataGridViewEx2.Rows[i].Cells[1].Value.ToString());
            }
            RefreshTables();
            MessageBox.Show("Ок!");
        }



	}
}