using System;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ePlus.MetaData.Client;
using System.IO;
using ePlus.MetaData.Server;

namespace RCChCalculationGroupsABC
{
    public partial class ABCAnalizParams : ExternalReportForm, IExternalReportFormMethods//, IExternalReportWithTags
	{
        public ABCAnalizParams()
		{
			InitializeComponent();
		}

        public void Parametrs()
        {
            ucPeriod.DateTo = DateTime.Now;
            ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
            //this.GROUPA.Value = 20M;
            //this.GROUPB.Value = 70M;
            //this.GROUPC.Value = 10M;
            //comboBox1.SelectedIndex = 0;
            LoadSettings();
        }
		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            Utils.AddNode(root, "GROUPA", GROUPA.Value.ToString());
            Utils.AddNode(root, "GROUPB", GROUPB.Value.ToString());
            Utils.AddNode(root, "GROUPC", GROUPC.Value.ToString());
            Utils.AddNode(root, "GROUPD", GROUPD.Value.ToString());

            Utils.AddNode(root, "GROUPA_P", GROUPA_P.Value.ToString());
            Utils.AddNode(root, "GROUPB_P", GROUPB_P.Value.ToString());
            Utils.AddNode(root, "GROUPC_P", GROUPC_P.Value.ToString());
            Utils.AddNode(root, "GROUPD_P", GROUPD_P.Value.ToString());

            Utils.AddNode(root, "ID_CONTRACTOR", Contractor.Id);

            Utils.AddNode(root, "CHEQUE", chCheque.Checked);
            Utils.AddNode(root, "INVOICE_OUT", chInvoiceOut.Checked);
            Utils.AddNode(root, "MOVEMENT", chMovement.Checked);

            Utils.AddNode(root, "RELOAD_GROUP", MessageBox.Show("Переформировать группы A, B, C в справочнике номенклатуры?","Переформирование групп",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes? 1: 0);

			foreach (DataRowItem dr in ucStore.Items)
				Utils.AddNode(root, "ID_STORE", dr.Id);

            foreach (DataRowItem dr in ucGroupGoods.Items)
                Utils.AddNode(root, "ID_GOODS_GROUP", dr.Id);

            foreach (ABCAnaliz.Table1Row row in this.aBCAnaliz1.Table1.Rows)
            {
                XmlNode node = Utils.AddNode(root, "GROUPS_TABLE");
                Utils.AddNode(node, "GROUP", row.GROUP);
                Utils.AddNode(node, "NORM", row.NORM);
                Utils.AddNode(node, "NORM_MIN", row.NORM_MIN);
                Utils.AddNode(node, "MAIN_GROUP", row.MAIN_GROUP.ToString().ToUpper());
            }


            Utils.AddNode(root, "ID_USER", SecurityContextEx.ID_USER);

            Utils.AddNode(root, "DESCRIPT_PARAM", GetDescriptOfPropertice());

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

            rep.LoadData("REP_ABC_ANALIZ_CHANGE_GROUP_OF_GOODS", doc.InnerXml);
            rep.BindDataSource("ABCAnaliz_Table0", 0);

            //rep.AddParameter("date_from", ucPeriod.DateFrText);
            rep.AddParameter("CONTRACTOR", Contractor.Text);
            rep.AddParameter("Pm_DateFrom", ucPeriod.DateFrText);
            rep.AddParameter("Pm_DateTo", ucPeriod.DateToText);
            rep.AddParameter("Pm_StoreName", (this.ucStore.Items.Count == 0) ? "Все склады" : this.ucStore.ToCommaDelimetedStringList());
            rep.AddParameter("Pm_GroupsName", (this.ucGroupGoods.Items.Count == 0) ? "Все группы товаров" : this.ucGroupGoods.ToCommaDelimetedStringList());
            rep.AddParameter("PROP", GetDescriptOfPropertice());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			ucStore.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
            get { return "Расчет групп АВС"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}
        /*
       public override string[] Tags
        {
            get
            {
                return new string[2] { "ABC", "маржинальность"};
            }
        }
         * */
        private void GROUPA_ValueChanged(object sender, EventArgs e)
        {
            decimal per = GROUPA.Value + GROUPC.Value + GROUPD.Value;
            if (per > 100)
            {
                GROUPB.Value = 0;
                GROUPA.Value = 100M - GROUPC.Value - GROUPD.Value;
            }
            else
            {
                GROUPB.Value = 100M - per;
            }
        }

        private void GROUPB_ValueChanged(object sender, EventArgs e)
        {
            decimal per = GROUPB.Value + GROUPA.Value + GROUPD.Value;
            if (per > 100)
            {
                GROUPC.Value = 0;
                GROUPB.Value = 100M - GROUPA.Value - GROUPD.Value;
            }
            else
            {
                GROUPC.Value = 100M - per;
            }
        }

        private void GROUPC_ValueChanged(object sender, EventArgs e)
        {
            decimal per = GROUPA.Value + GROUPC.Value + GROUPB.Value;
            if (per > 100)
            {
                GROUPD.Value = 0;
                GROUPC.Value = 100M - GROUPA.Value - GROUPB.Value;
            }
            else
            {
                GROUPD.Value = 100M - per;
            }
        }

        private void GROUPD_ValueChanged(object sender, EventArgs e)
        {
            decimal per = GROUPA.Value + GROUPB.Value + GROUPD.Value;
            if (per > 100)
            {
                GROUPC.Value = 0;
                GROUPD.Value = 100M - GROUPA.Value - GROUPB.Value;
            }
            else
            {
                GROUPC.Value = 100M - per;
            }
        }

        private string settingsFilePath
        {
            get
            {
                string user = SecurityContextEx.ID_USER.ToString();
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() +"("+user+ ").xml");
            }
        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root;

            if (File.Exists(settingsFilePath))
            {
                doc.Load(settingsFilePath);
                root = doc.SelectSingleNode("//XML");
                root.RemoveAll();
            }
            else
            {
                root = Utils.AddNode(doc, "XML");
            }
            Utils.AddNode(root, "ID_CONTRACTOR", Contractor.Id);
            //группы товаров
            foreach (DataRowItem dri in ucGroupGoods.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_GOODS_GROUP");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "GUID", dri.Guid);
            }
            //Склады
            foreach (DataRowItem dri in ucStore.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_STORE");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "GUID", dri.Guid);
            }

            Utils.AddNode(root, "GROUPA", GROUPA.Value.ToString());
            Utils.AddNode(root, "GROUPB", GROUPB.Value.ToString());
            Utils.AddNode(root, "GROUPC", GROUPC.Value.ToString());
            Utils.AddNode(root, "GROUPD", GROUPD.Value.ToString());

            Utils.AddNode(root, "GROUPA_P", GROUPA_P.Value.ToString());
            Utils.AddNode(root, "GROUPB_P", GROUPB_P.Value.ToString());
            Utils.AddNode(root, "GROUPC_P", GROUPC_P.Value.ToString());
            Utils.AddNode(root, "GROUPD_P", GROUPD_P.Value.ToString());

            Utils.AddNode(root, "CHEQUE", chCheque.Checked);
            Utils.AddNode(root, "INVOICE_OUT", chInvoiceOut.Checked);
            Utils.AddNode(root, "MOVEMENT", chMovement.Checked);

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (ABCAnaliz.Table1Row row in this.aBCAnaliz1.Table1.Rows)
            {
                XmlNode node = Utils.AddNode(root, "GROUPS_TABLE");
                Utils.AddNode(node, "GROUP", row.GROUP);
                Utils.AddNode(node, "NORM", row.NORM);
                Utils.AddNode(node, "NORM_MIN", row.NORM_MIN);
                Utils.AddNode(node, "MAIN_GROUP", row.MAIN_GROUP.ToString().ToUpper()[0]);
            }

            doc.Save(settingsFilePath);
        }

        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                GROUPA.Value = 79;
                GROUPB.Value = 15;
                GROUPC.Value = 5; 
                GROUPC.Value = 1;

                ucPeriod.DateTo = DateTime.Now;
                ucPeriod.DateFrom = DateTime.Now.AddDays(-180);

                long IdContr = GetContractorWe();
                if (IdContr != 0)
                {
                    Contractor.Id = IdContr;
                    GetStoresContrWe(IdContr);
                }

                chCheque.Checked = true;
                chInvoiceOut.Checked = true;
                chMovement.Checked = true;

                /////////////////////////////////////////////////
                ABCAnaliz.Table1Row row = this.aBCAnaliz1.Table1.NewTable1Row();
                row.GROUP = "AA";
                row.NORM = 14;
                row.NORM_MIN = 7;
                row.MAIN_GROUP = 'A';
                aBCAnaliz1.Table1.Rows.Add(row);

                ABCAnaliz.Table1Row row1 = this.aBCAnaliz1.Table1.NewTable1Row();
                row1.GROUP = "AB";
                row1.NORM = 10;
                row1.NORM_MIN = 7;
                row1.MAIN_GROUP = 'A';
                aBCAnaliz1.Table1.Rows.Add(row1);

                ABCAnaliz.Table1Row row2 = this.aBCAnaliz1.Table1.NewTable1Row();
                row2.GROUP = "BA";
                row2.NORM = 12;
                row2.NORM_MIN = 7;
                row2.MAIN_GROUP = 'A';
                aBCAnaliz1.Table1.Rows.Add(row2);

                ABCAnaliz.Table1Row row3 = this.aBCAnaliz1.Table1.NewTable1Row();
                row3.GROUP = "BB";
                row3.NORM = 7;
                row3.NORM_MIN = 3;
                row3.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row3);

                ABCAnaliz.Table1Row row4 = this.aBCAnaliz1.Table1.NewTable1Row();
                row4.GROUP = "CB";
                row4.NORM = 7;
                row4.NORM_MIN = 3;
                row4.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row4);

                ABCAnaliz.Table1Row row5 = this.aBCAnaliz1.Table1.NewTable1Row();
                row5.GROUP = "BC";
                row5.NORM = 7;
                row5.NORM_MIN = 3;
                row5.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row5);

                ABCAnaliz.Table1Row row6 = this.aBCAnaliz1.Table1.NewTable1Row();
                row6.GROUP = "CA";
                row6.NORM = 7;
                row6.NORM_MIN = 3;
                row6.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row6);

                ABCAnaliz.Table1Row row7 = this.aBCAnaliz1.Table1.NewTable1Row();
                row7.GROUP = "AC";
                row7.NORM = 10;
                row7.NORM_MIN = 7;
                row7.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row7);
                //////
                ABCAnaliz.Table1Row row8 = this.aBCAnaliz1.Table1.NewTable1Row();
                row8.GROUP = "AD";
                row8.NORM = 10;
                row8.NORM_MIN = 7;
                row8.MAIN_GROUP = 'B';
                aBCAnaliz1.Table1.Rows.Add(row8);

                ABCAnaliz.Table1Row row9 = this.aBCAnaliz1.Table1.NewTable1Row();
                row9.GROUP = "CC";
                row9.NORM = 7;
                row9.NORM_MIN = 3;
                row9.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row9);

                ABCAnaliz.Table1Row row10 = this.aBCAnaliz1.Table1.NewTable1Row();
                row10.GROUP = "CD";
                row10.NORM = 7;
                row10.NORM_MIN = 3;
                row10.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row10);

                ABCAnaliz.Table1Row row11 = this.aBCAnaliz1.Table1.NewTable1Row();
                row11.GROUP = "DC";
                row11.NORM = 7;
                row11.NORM_MIN = 3;
                row11.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row11);

                ABCAnaliz.Table1Row row12 = this.aBCAnaliz1.Table1.NewTable1Row();
                row12.GROUP = "BD";
                row12.NORM = 7;
                row12.NORM_MIN = 3;
                row12.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row12);

                ABCAnaliz.Table1Row row13 = this.aBCAnaliz1.Table1.NewTable1Row();
                row13.GROUP = "DA";
                row13.NORM = 7;
                row13.NORM_MIN = 3;
                row13.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row13);

                ABCAnaliz.Table1Row row14 = this.aBCAnaliz1.Table1.NewTable1Row();
                row14.GROUP = "DB";
                row14.NORM = 7;
                row14.NORM_MIN = 3;
                row14.MAIN_GROUP = 'C';
                aBCAnaliz1.Table1.Rows.Add(row14);

                ABCAnaliz.Table1Row row15 = this.aBCAnaliz1.Table1.NewTable1Row();
                row15.GROUP = "DD";
                row15.NORM = 7;
                row15.NORM_MIN = 3;
                row15.MAIN_GROUP = 'D';
                aBCAnaliz1.Table1.Rows.Add(row15);
                //////////////////////////////////////////////////

                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;
            Contractor.Id = Utils.GetLong(root, "ID_CONTRACTOR");
            // группы товаров
            XmlNodeList group = root.SelectNodes("ID_GOODS_GROUP");
            foreach (XmlNode node in group)
            {
                DataRowItem ci = new DataRowItem();
                ci.Id = Utils.GetLong(node, "ID");
                ci.Text = Utils.GetString(node, "TEXT");
                ci.Guid = Utils.GetGuid(node, "GUID");
                ucGroupGoods.AddRowItem(ci);
            }
            // Склады
            ucStore.Clear();
            XmlNodeList store = root.SelectNodes("ID_STORE");
            foreach (XmlNode node in store)
            {
                DataRowItem ci = new DataRowItem();
                ci.Id = Utils.GetLong(node, "ID");
                ci.Text = Utils.GetString(node, "TEXT");
                ci.Guid = Utils.GetGuid(node, "GUID");
                ucStore.AddRowItem(ci);
            }
            GROUPA.Value = Utils.GetInt(root, "GROUPA");
            GROUPB.Value = Utils.GetInt(root, "GROUPB");
            GROUPC.Value = Utils.GetInt(root, "GROUPC");
            GROUPD.Value = Utils.GetInt(root, "GROUPD");

            GROUPA_P.Value = Utils.GetInt(root, "GROUPA_P");
            GROUPB_P.Value = Utils.GetInt(root, "GROUPB_P");
            GROUPC_P.Value = Utils.GetInt(root, "GROUPC_P");
            GROUPD_P.Value = Utils.GetInt(root, "GROUPD_P");

            chCheque.Checked = Utils.GetBool(root, "CHEQUE");
            chInvoiceOut.Checked = Utils.GetBool(root, "INVOICE_OUT");
            chMovement.Checked = Utils.GetBool(root, "MOVEMENT");

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");


            XmlNodeList list = root.SelectNodes("GROUPS_TABLE");
            foreach (XmlNode node2 in list)
            {
                ABCAnaliz.Table1Row row = this.aBCAnaliz1.Table1.NewTable1Row();
                row.GROUP = Utils.GetString(node2, "GROUP");
                row.NORM = Utils.GetDecimal(node2, "NORM");
                row.NORM_MIN = Utils.GetDecimal(node2, "NORM_MIN");
                row.MAIN_GROUP = Utils.GetChar(node2, "MAIN_GROUP",' ');
                aBCAnaliz1.Table1.Rows.Add(row);
            }

        }
        private string GetDescriptOfPropertice()
        {
            string res = string.Empty;
            res += string.Format("Контрагент: {0} \r\n", Contractor.Text);

            res += string.Format("Период: с {0} по {1} \r\n", ucPeriod.DateFrText, ucPeriod.DateToText);

            res += string.Format("{0}:\r\n", groupBox3.Text);
            res += string.Format("A:{0}\r\n", GROUPA.Value);
            res += string.Format("B:{0}\r\n", GROUPB.Value);
            res += string.Format("C:{0}\r\n", GROUPC.Value);
            res += string.Format("D:{0}\r\n", GROUPD.Value);

            res += string.Format("{0}:\r\n", groupBox4.Text);
            res += string.Format("A:{0}\r\n", GROUPA_P.Value);
            res += string.Format("B:{0}\r\n", GROUPB_P.Value);
            res += string.Format("C:{0}\r\n", GROUPC_P.Value);
            res += string.Format("D:{0}\r\n", GROUPD_P.Value);

            res += string.Format("{0}:\r\n", groupBox2.Text);
            res += string.Format("{0}:{1}\r\n", chCheque.Text, chCheque.Checked ? "Да" : "Нет");
            res += string.Format("{0}:{1}\r\n", chInvoiceOut.Text, chInvoiceOut.Checked ? "Да" : "Нет");
            res += string.Format("{0}:{1}\r\n", chMovement.Text, chMovement.Checked ? "Да" : "Нет");

            res += string.Format("{0}:\r\n", ucStore.Caption);
            res += string.Format("{0}\r\n", (this.ucStore.Items.Count == 0) ? "Все склады" : this.ucStore.ToCommaDelimetedStringList());

            res += string.Format("{0}:\r\n", ucGroupGoods.Caption);
            res += string.Format("{0}\r\n", (this.ucGroupGoods.Items.Count == 0) ? "Все склады" : this.ucGroupGoods.ToCommaDelimetedStringList());

            res += string.Format("Норматив товарного запаса:\r\n");
            res += string.Format("Сочетание | Норматив Товарного запаса (дн) для Qp |  Порог заказа (дн) для Qmin | Группа \r\n");
            foreach (ABCAnaliz.Table1Row row in this.aBCAnaliz1.Table1.Rows)
            {
                res += string.Format("    {0}                                         {1}                                      {2}                          {3}\r\n",row.GROUP, row.NORM, row.NORM_MIN, row.MAIN_GROUP.ToString().ToUpper());
            }
            return res;
        }
        private long GetContractorWe()
        {
            long result = 0;
            DataService_BL bl = new DataService_BL();

            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand(@"SELECT TOP 1
                                                    [ID] = C.ID_CONTRACTOR
                                                FROM CONTRACTOR C(NOLOCK)
                                                WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()", connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                result = (long)command.ExecuteScalar();
            }
            return result;
        }

        private void GetStoresContrWe(long id_contractor)
        {
            DataService_BL bl = new DataService_BL();

            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand(string.Format(@"select id = s.id_store, name = s.name, [guid] = id_store_global from store s
                                                                    inner join store_type st on st.id_store_type_global = s.id_store_type_global
                                                                    where id_contractor = {0} and st.mnemocode = 'MAIN'", id_contractor), connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ucStore.Clear();
                while (reader.Read())
                {
                    DataRowItem ci = new DataRowItem();
                    ci.Id = reader.GetInt64(0);
                    ci.Text = reader.GetString(1);
                    ci.Guid = reader.GetGuid(2);
                    ucStore.AddRowItem(ci);
                }
            }
        }

        private void ABCAnalizParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void Contractor_ValueChanged(object sender, EventArgs e)
        {
            GetStoresContrWe(Contractor.Id);
        }

        private void ABCAnalizParams_Load(object sender, EventArgs e)
        {
            ClearValues();
            Parametrs();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void filtersDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filtersDataGridView_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            

            
        }

        private void filtersDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                decimal res =0;
                if (!(decimal.TryParse(e.FormattedValue.ToString(), out res)))
                {
                    MessageBox.Show("В редактируемой ячейке должно быть число", "Ошибка при заполнении", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            if (e.ColumnIndex == 3)
            {
                char res =' ';
                if (!((char.TryParse(e.FormattedValue.ToString(), out res)) && ("ABCD".Contains(e.FormattedValue.ToString().ToUpper()))))
                {
                    MessageBox.Show("В редактируемой ячейке должна быть одна из латинских букв A,B,C,D", "Ошибка при заполнении", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void filtersDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper()[0];
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


	}
}