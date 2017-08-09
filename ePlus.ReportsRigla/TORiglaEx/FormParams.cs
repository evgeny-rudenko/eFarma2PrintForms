using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using ePlus.MetaData.Server;



namespace RCSTO_Rigla
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public FormParams()
		{
			InitializeComponent();
		}
		private long idContractor;
		private long idGood;

		//string connectionString;
		//string folderPath;

		protected override void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("RCSTO_Rigla.TO_MR_RIGLA.sql");
			DataService_BL dataService = new DataService_BL();

			using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
			{
				string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

				SqlCommand comm = null;
				foreach (string statement in batch)
				{
					if (statement == string.Empty)
						continue;

					using (SqlConnection con = new SqlConnection(dataService.ConnectionString))
					{
						comm = new SqlCommand(statement, con);
						comm.CommandTimeout = 360000000;
						con.Open();
						comm.ExecuteNonQuery();
					}
				}
			}
		}

		//public void AddCodesAP(List<long> codesAP)
		//{
		//    foreach (long Code in codesAP)
		//    {
		//        if (!listBoxCodesAP.Items.Contains(Code))
		//            listBoxCodesAP.Items.Add(Code);
		//    }
		//} 
		public void Print(string[] reportFiles)
		{
			string IdGoods = "";
			if (ucContractor.Id == 0)
			{
				MessageBox.Show("Задайте АУ", "Предупреждение");
				return;
			}
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			///////////////////////////////
			Utils.AddNode(root, "DATE_FR", ucPeriod1.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);
			foreach (DataRowItem dr in ucStore.Items)
				Utils.AddNode(root, "ID_STORE", dr.Id);
			//foreach (DataRowItem dr in ucGoods.Items)
			//{
			//    Utils.AddNode(root, "ID_GOODS", dr.Id);
			//    IdGoods += dr.Id.ToString() + " ";
			//}
			if (ucGoods.Id != 0)
			{
				//MessageBox.Show("Задайте АУ", "Предупреждение");
				//return;
				IdGoods += ucGoods.Id.ToString() + " ";
			}
			//if (listBoxCodesAP.Items.Count != 0)
			//{
			//    XmlNode CodesAP = Utils.AddNode(root, "CODES_AP");
			//    for (int i = 0; i < listBoxCodesAP.Items.Count;i++)
			//    {
			//        Utils.AddNode(CodesAP, "CODE_AP", (long)listBoxCodesAP.Items[i]);
			//    }
			//}

			Utils.AddNode(root, "IS_FILTERED", chbFilteredMove.Checked);
			Utils.AddNode(root, "ID_AU", ucContractor.Id);
			// код АП
			if (ucGoods.Id != 0)
				Utils.AddNode(root, "ID_GOODS", ucGoods.Id);
			//--------------------------------- параметры диагностического отчета 
			Utils.AddNode(root, "USE_DIAGN_REPORT", checkBoxUseDiagnReport.Checked);
			Utils.AddNode(root, "INV_REMAINDER", checkBoxUseDiagnReport.Checked ? checkBoxInvRemainder.Checked : false);
			Utils.AddNode(root, "INV", checkBoxUseDiagnReport.Checked ? checkBoxInv.Checked : false);
			Utils.AddNode(root, "INV_CONTR_W_VAT", checkBoxUseDiagnReport.Checked ? checkBoxInvContrWVat.Checked : false);
			Utils.AddNode(root, "INV_CONTR_NW_VAT", checkBoxUseDiagnReport.Checked ? checkBoxInvContrNWVat.Checked : false);
			Utils.AddNode(root, "RETURN_BUYER", checkBoxUseDiagnReport.Checked ? checkBoxReturnBuyer.Checked : false);
			//Utils.AddNode(root, "INV_FROM_AP", checkBoxUseDiagnReport.Checked ? checkBoxInvFromAP.Checked : false);
			Utils.AddNode(root, "EXCESS_BY_INVENT", checkBoxUseDiagnReport.Checked ? checkBoxExcessByInvent.Checked : false);
			Utils.AddNode(root, "EXPENS", checkBoxUseDiagnReport.Checked ? checkBoxExpens.Checked : false);
			Utils.AddNode(root, "RECEIPTS", checkBoxUseDiagnReport.Checked ? checkBoxReceipts.Checked : false);
			Utils.AddNode(root, "SERVICE", checkBoxUseDiagnReport.Checked ? checkBoxService.Checked : false);
			Utils.AddNode(root, "EXPENS_DISCOUNT", checkBoxUseDiagnReport.Checked ? checkBoxExpensDiscount.Checked : false);
			Utils.AddNode(root, "SK", checkBoxUseDiagnReport.Checked ? checkBoxSK.Checked : false);
			Utils.AddNode(root, "CASH", checkBoxUseDiagnReport.Checked ? checkBoxCash.Checked : false);
			Utils.AddNode(root, "CASHLESS", checkBoxUseDiagnReport.Checked ? checkBoxCashless.Checked : false);
			Utils.AddNode(root, "RECIPES_GROSS", checkBoxUseDiagnReport.Checked ? checkBoxRecipesGross.Checked : false);
			Utils.AddNode(root, "RECIPES_GROSS_DISCOUNT", checkBoxUseDiagnReport.Checked ? checkBoxRecipesGrossDiscount.Checked : false);
			Utils.AddNode(root, "RETURN_TO_CONTRAC", checkBoxUseDiagnReport.Checked ? checkBoxReturnToContrac.Checked : false);
			Utils.AddNode(root, "COMPLAINT", checkBoxUseDiagnReport.Checked ? checkBoxComplaint.Checked : false);
			Utils.AddNode(root, "BACK_SALE", checkBoxUseDiagnReport.Checked ? checkBoxBackSale.Checked : false);
			Utils.AddNode(root, "MOVE_IN_CONTR", checkBoxUseDiagnReport.Checked ? checkBoxMoveInContr.Checked : false);
			Utils.AddNode(root, "WRITE_OFF", checkBoxUseDiagnReport.Checked ? checkBoxWriteOff.Checked : false);
			Utils.AddNode(root, "SHORTAGE_BY_INV", checkBoxUseDiagnReport.Checked ? checkBoxShortageByInv.Checked : false);
			Utils.AddNode(root, "REVALUATION", checkBoxUseDiagnReport.Checked ? checkBoxRevaluation.Checked : false);
			Utils.AddNode(root, "DISMANTLING", checkBoxUseDiagnReport.Checked ? checkBoxDismantling.Checked : false);

			//--------------------------------- параметры диагностического отчета
			//MessageBox.Show(doc.OuterXml);
			//////////////////
			/*
			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter("TO_RIGLA_REP_EX", connectionString))
			{
				sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
				sqlda.SelectCommand.CommandTimeout = 360000000;
				sqlda.Fill(ds);
			}
			*/
			///////////////////////////////
			ReportFormNew rep = new ReportFormNew();
			rep.LoadData("REP_EX_TO_MR_RIGLA", doc.InnerXml);
			//rep.DataSource = ds; 
			rep.BindDataSource("TO_MR_RIGLA_DS_Table1", 0);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table6", 1);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table2", 2);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table3", 3);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table4", 4);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table5", 5);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table7", 6);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table8", 7);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table9", 8);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table10", 9);

			rep.BindDataSource("TO_MR_RIGLA_DS_Table11", 10);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table12", 11);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table13", 12);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table14", 13);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table15", 14);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table16", 15);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table17", 16);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table18", 17);
			rep.BindDataSource("TO_MR_RIGLA_DS_Table19", 18);

			bool b = Convert.ToBoolean(rep.DataSource.Tables[3].Rows[0][0]);
			rep.ReportPath = b ? Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TO_USE_VAT.rdlc") : Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TO_DONT_USE_VAT.rdlc");
			//rep.ReportPath = reportFiles[0];
			rep.AddParameter("Date_fr", ucPeriod1.DateFrom.ToString("dd.MM.yy"));
			rep.AddParameter("Date_to", ucPeriod1.DateTo.ToString("dd.MM.yy"));
			rep.AddParameter("AU_NAME", ucContractor.Text);

			string docNumber = ucPeriod1.DateFrom.Month.ToString();
			if ((ucPeriod1.DateTo.Date.Subtract(ucPeriod1.DateFrom.Date).Days + 1) != new GregorianCalendar().GetDaysInMonth(ucPeriod1.DateFrom.Year, ucPeriod1.DateFrom.Month))
				docNumber += "/";
			rep.AddParameter("DOC_NUM", docNumber);
			rep.AddParameter("USER_NAME", SecurityContextEx.Context.User.Name);
			rep.AddParameter("USE_DIAGN_REPORT", checkBoxUseDiagnReport.Checked ? "1" : "0");
			rep.AddParameter("INV_REMAINDER", checkBoxInvRemainder.Checked ? "1" : "0");
			rep.AddParameter("GOODS_FILTER", IdGoods);
			rep.AddParameter("EXPENS", checkBoxExpens.Checked ? "1" : "0");
			rep.AddParameter("INV", checkBoxInv.Checked ? "1" : "0");
			//MessageBox.Show(IdGoods);
			rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

			/*SaveFileDialog sf = new SaveFileDialog();
			 if (sf.ShowDialog() == DialogResult.OK)
			 {
				 MessageBox.Show(sf.FileName);
				 rep.Export("Excel",Path.Combine(sf.FileName,".xls"));
			 }*/
			//rep.Export("Excel", @"D:\tt.xls");
			//((IExternalReport)rep).Execute(this.connectionString, rep.ReportPath);

			// rep.ReportViewer.LocalReport.Render(
			/*   Warning[] warnings;
			   string[] streamids;
			   string mimeType;
			   string encoding;
			   string extension;
			   string exportFormat = "Excel";
			   byte[] bytes = rep.ReportViewer.LocalReport.Render(
				   exportFormat, null, out mimeType, out encoding, out extension,
				   out streamids, out warnings);

			   FileStream fs = new FileStream(@"D:\1\222222.xls", FileMode.Create);
			   fs.Write(bytes, 0, bytes.Length);
			   fs.Close();
			 * */
			//((ReportFormNew)rep).Export("Excel", @"D:\1\0035_ПН-00001521_Ведомость приемки товара.xls");
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Товарный отчет для Риглы"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
		}

		public override bool SyncStoredProcOnPrint
		{
			get { return false; }
		}


		private void FormParams_Load(object sender, EventArgs e)
		{
			ucPeriod1.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1, 0, 0, 0, 0);// DateTime.Now.AddMonths(-1).Date;
			ucPeriod1.DateTo = ucPeriod1.DateFrom.AddMonths(1).AddDays(-1);//DateTime.Now.AddDays(-1).Date;

			//ucGoods.Items.Clear();
			ucContractor.Id = this.IdContractorDefault;
			idContractor = ucContractor.Id;
			chbFilteredMove.Checked = true;
			ucStore.MultiSelect = true;

			if (!File.Exists(fileName)) return;
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);

			XmlNode root = doc.SelectSingleNode("/XML");
			ucContractor.Id = Utils.GetLong(root, "ID_CONTRACTOR");
			if (ucContractor.Id == 0)
				ucContractor.Id = this.IdContractorDefault;

			ucGoods.Id = Utils.GetLong(root, "ID_GOODS");
			//if (ucGoods.Id == 0)
			//    ucGoods.Id = this.IdGoodsDefault; 
			XmlNodeList stores = root.SelectNodes("STORE");
			foreach (XmlNode node in stores)
			{
				long id = Utils.GetLong(node, "ID");
				string text = Utils.GetString(node, "TEXT");
				DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
				ucStore.Items.Add(dri);
			}
			//XmlNodeList goods = root.SelectNodes("GOODS");
			//foreach (XmlNode node in goods)
			//{
			//    long id = Utils.GetLong(node, "ID");
			//    string text = Utils.GetString(node, "TEXT");
			//    DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
			//    ucGoods.Items.Add(dri);
			//}

			checkBoxUseDiagnReport.Checked = Utils.GetBool(root, "USE_DIAGN_REPORT");
			checkBoxInvRemainder.Checked = Utils.GetBool(root, "INV_REMAINDER");
			checkBoxInv.Checked = Utils.GetBool(root, "INV");
			checkBoxInvContrWVat.Checked = Utils.GetBool(root, "INV_CONTR_W_VAT");
			checkBoxInvContrNWVat.Checked = Utils.GetBool(root, "INV_CONTR_NW_VAT");
			checkBoxReturnBuyer.Checked = Utils.GetBool(root, "RETURN_BUYER");
			//checkBoxInvFromAP.Checked = Utils.GetBool(root, "INV_FROM_AP");
			checkBoxExcessByInvent.Checked = Utils.GetBool(root, "EXCESS_BY_INVENT");
			checkBoxExpens.Checked = Utils.GetBool(root, "EXPENS");
			checkBoxReceipts.Checked = Utils.GetBool(root, "RECEIPTS");
			checkBoxService.Checked = Utils.GetBool(root, "SERVICE");
			checkBoxExpensDiscount.Checked = Utils.GetBool(root, "EXPENS_DISCOUNT");
			checkBoxSK.Checked = Utils.GetBool(root, "SK");
			checkBoxCash.Checked = Utils.GetBool(root, "CASH");
			checkBoxCashless.Checked = Utils.GetBool(root, "CASHLESS");
			checkBoxRecipesGross.Checked = Utils.GetBool(root, "RECIPES_GROSS");
			checkBoxRecipesGrossDiscount.Checked = Utils.GetBool(root, "RECIPES_GROSS_DISCOUNT");
			checkBoxReturnToContrac.Checked = Utils.GetBool(root, "RETURN_TO_CONTRAC");
			checkBoxComplaint.Checked = Utils.GetBool(root, "COMPLAINT");
			checkBoxBackSale.Checked = Utils.GetBool(root, "BACK_SALE");
			checkBoxMoveInContr.Checked = Utils.GetBool(root, "MOVE_IN_CONTR");
			checkBoxWriteOff.Checked = Utils.GetBool(root, "WRITE_OFF");
			checkBoxShortageByInv.Checked = Utils.GetBool(root, "SHORTAGE_BY_INV");
			checkBoxRevaluation.Checked = Utils.GetBool(root, "REVALUATION");
			checkBoxDismantling.Checked = Utils.GetBool(root, "DISMANTLING");
		}

		private string fileName = Path.Combine(Utils.TempDir(), "ToRiglaSettings.xml");

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			XmlDocument docSave = new XmlDocument();
			XmlNode root = Utils.AddNode(docSave, "XML");
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			Utils.AddNode(root, "ID_GOODS", ucGoods.Id);

			//foreach (DataRowItem dri in ucGoods.Items)
			//{
			//    XmlNode goods = Utils.AddNode(root, "GOODS");
			//    Utils.AddNode(goods, "ID", dri.Id);
			//    Utils.AddNode(goods, "TEXT", dri.Text);
			//}
			foreach (DataRowItem dri in ucStore.Items)
			{
				XmlNode store = Utils.AddNode(root, "STORE");
				Utils.AddNode(store, "ID", dri.Id);
				Utils.AddNode(store, "TEXT", dri.Text);
			}

			Utils.AddNode(root, "USE_DIAGN_REPORT", checkBoxUseDiagnReport.Checked);
			Utils.AddNode(root, "INV_REMAINDER", checkBoxInvRemainder.Checked);
			Utils.AddNode(root, "INV", checkBoxInv.Checked);
			Utils.AddNode(root, "INV_CONTR_W_VAT", checkBoxInvContrWVat.Checked);
			Utils.AddNode(root, "INV_CONTR_NW_VAT", checkBoxInvContrNWVat.Checked);
			Utils.AddNode(root, "RETURN_BUYER", checkBoxReturnBuyer.Checked);
			//Utils.AddNode(root, "INV_FROM_AP", checkBoxInvFromAP.Checked);
			Utils.AddNode(root, "EXCESS_BY_INVENT", checkBoxExcessByInvent.Checked);
			Utils.AddNode(root, "EXPENS", checkBoxExpens.Checked);
			Utils.AddNode(root, "RECEIPTS", checkBoxReceipts.Checked);
			Utils.AddNode(root, "SERVICE", checkBoxService.Checked);
			Utils.AddNode(root, "EXPENS_DISCOUNT", checkBoxExpensDiscount.Checked);
			Utils.AddNode(root, "SK", checkBoxSK.Checked);
			Utils.AddNode(root, "CASH", checkBoxCash.Checked);
			Utils.AddNode(root, "CASHLESS", checkBoxCashless.Checked);
			Utils.AddNode(root, "RECIPES_GROSS", checkBoxRecipesGross.Checked);
			Utils.AddNode(root, "RECIPES_GROSS_DISCOUNT", checkBoxRecipesGrossDiscount.Checked);
			Utils.AddNode(root, "RETURN_TO_CONTRAC", checkBoxReturnToContrac.Checked);
			Utils.AddNode(root, "COMPLAINT", checkBoxComplaint.Checked);
			Utils.AddNode(root, "BACK_SALE", checkBoxBackSale.Checked);
			Utils.AddNode(root, "MOVE_IN_CONTR", checkBoxMoveInContr.Checked);
			Utils.AddNode(root, "WRITE_OFF", checkBoxWriteOff.Checked);
			Utils.AddNode(root, "SHORTAGE_BY_INV", checkBoxShortageByInv.Checked);
			Utils.AddNode(root, "REVALUATION", checkBoxRevaluation.Checked);
			Utils.AddNode(root, "DISMANTLING", checkBoxDismantling.Checked);

			Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);

			docSave.Save(fileName);
		}

		private void ucStore_BeforePluginShow(object sender, CancelEventArgs e)
		{
			ucStore.PluginContol.Grid(0).SetParameterValue("@ID_CONTRACTOR", ucContractor.Id != null ? ucContractor.Id : 0);
		}

		private void ucContractor_ValueChanged(object sender, EventArgs e)
		{
			if (idContractor != ucContractor.Id)
			{
				ucStore.Items.Clear();
				idContractor = ucContractor.Id;
			}
			if (ucContractor.Id != 0 && ContractorUseVat(ucContractor.Id))
			{
				//checkBoxUseDiagnReport.Checked = false;
				checkBoxUseDiagnReport.Enabled = true;
				//groupBox1.Enabled = true;
			}
			else
			{
				checkBoxUseDiagnReport.Checked = false;
				checkBoxUseDiagnReport.Enabled = false;
				//groupBox1.Enabled = false;
			}
		}
		private bool ContractorUseVat(long Id_Cintractor)
		{
			bool result = false;
			DataService_BL bl = new DataService_BL();

			using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
			{
				SqlCommand command = new SqlCommand(string.Format("SELECT USE_VAT FROM CONTRACTOR WHERE ID_CONTRACTOR = {0}", Id_Cintractor), connection);
				command.CommandType = CommandType.Text;
				connection.Open();
				result = (bool)command.ExecuteScalar();
			}
			return result;
		}

		private void checkBoxInv_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxInv.CheckState == CheckState.Indeterminate) return;
			checkBoxInvContrWVat.Checked = checkBoxExcessByInvent.Checked = checkBoxReturnBuyer.Checked = checkBoxInvContrNWVat.Checked /*= checkBoxInvFromAP.Checked */= checkBoxInv.Checked;
		}
		private void checkBoxInvGroupChange(object sender, EventArgs e)
		{
			//MessageBox.Show(((CheckBox)(sender)).Name);
			//if (((CheckBox)(sender)).Name == "checkBoxInvContrWVat") return;
			if (checkBoxInvContrWVat.Checked && checkBoxExcessByInvent.Checked && checkBoxReturnBuyer.Checked && checkBoxInvContrNWVat.Checked /*&& checkBoxInvFromAP.Checked*/)
			{
				checkBoxInv.Checked = true;
				checkBoxInv.CheckState = CheckState.Checked;
				return;
			}
			if (checkBoxInvContrWVat.Checked || checkBoxExcessByInvent.Checked || checkBoxReturnBuyer.Checked || checkBoxInvContrNWVat.Checked /*|| checkBoxInvFromAP.Checked*/)
			{
				checkBoxInv.CheckState = CheckState.Indeterminate;
				checkBoxInv.Checked = true;
			}
			else
				checkBoxInv.Checked = false;
		}
		private void checkBoxExpensGroupChange(object sender, EventArgs e)
		{
			checkBoxReceiptsGroupChange(sender, e);
			checkBoxReturnToContracGroupChange(sender, e);
			if (checkBoxReceipts.Checked && checkBoxService.Checked
				&& checkBoxExpensDiscount.Checked
				&& checkBoxSK.Checked && checkBoxCash.Checked
				&& checkBoxCashless.Checked && checkBoxRecipesGross.Checked
				&& checkBoxRecipesGrossDiscount.Checked && checkBoxReturnToContrac.Checked
				&& checkBoxComplaint.Checked && checkBoxBackSale.Checked
				&& checkBoxMoveInContr.Checked && checkBoxWriteOff.Checked
				&& checkBoxShortageByInv.Checked && checkBoxRevaluation.Checked
				&& checkBoxDismantling.Checked)
			{
				checkBoxExpens.Checked = true;
				checkBoxExpens.CheckState = CheckState.Checked;
				return;
			}
			if (checkBoxReceipts.Checked || checkBoxService.Checked
				|| checkBoxExpensDiscount.Checked
				|| checkBoxSK.Checked || checkBoxCash.Checked
				|| checkBoxCashless.Checked || checkBoxRecipesGross.Checked
				|| checkBoxRecipesGrossDiscount.Checked || checkBoxReturnToContrac.Checked
				|| checkBoxComplaint.Checked || checkBoxBackSale.Checked
				|| checkBoxMoveInContr.Checked || checkBoxWriteOff.Checked
				|| checkBoxShortageByInv.Checked || checkBoxRevaluation.Checked
				|| checkBoxDismantling.Checked)
			{
				checkBoxExpens.CheckState = CheckState.Indeterminate;
				checkBoxExpens.Checked = true;
			}
			else
				checkBoxExpens.Checked = false;
		}
		private void checkBoxReturnToContracGroupChange(object sender, EventArgs e)
		{
			if (checkBoxComplaint.Checked && checkBoxBackSale.Checked)
			{
				checkBoxReturnToContrac.Checked = true;
				checkBoxReturnToContrac.CheckState = CheckState.Checked;
				return;
			}
			if (checkBoxComplaint.Checked || checkBoxBackSale.Checked)
			{
				checkBoxReturnToContrac.CheckState = CheckState.Indeterminate;
				checkBoxReturnToContrac.Checked = true;
			}
			else
				checkBoxReturnToContrac.Checked = false;
		}
		private void checkBoxReceiptsGroupChange(object sender, EventArgs e)
		{
			if (checkBoxService.Checked && checkBoxExpensDiscount.Checked && checkBoxCash.Checked && checkBoxCashless.Checked)
			{
				checkBoxReceipts.Checked = true;
				checkBoxReceipts.CheckState = CheckState.Checked;
				return;
			}
			if (checkBoxService.Checked || checkBoxExpensDiscount.Checked || checkBoxCash.Checked || checkBoxCashless.Checked)
			{
				checkBoxReceipts.CheckState = CheckState.Indeterminate;
				checkBoxReceipts.Checked = true;
			}
			else
				checkBoxReceipts.Checked = false;
		}
		private void checkBoxExpens_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxExpens.CheckState == CheckState.Indeterminate) return;
			checkBoxReceipts.Checked = checkBoxService.Checked = checkBoxExpensDiscount.Checked
				= checkBoxSK.Checked = checkBoxCash.Checked
				= checkBoxCashless.Checked = checkBoxRecipesGross.Checked
				= checkBoxRecipesGrossDiscount.Checked = checkBoxReturnToContrac.Checked
				= checkBoxComplaint.Checked = checkBoxBackSale.Checked
				= checkBoxMoveInContr.Checked = checkBoxWriteOff.Checked
				= checkBoxShortageByInv.Checked = checkBoxRevaluation.Checked
				= checkBoxDismantling.Checked = checkBoxExpens.Checked;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			groupBox1.Enabled = checkBoxUseDiagnReport.Checked;
			checkBoxInvRemainder.Checked = checkBoxInv.Checked = checkBoxExpens.Checked = checkBoxUseDiagnReport.Checked;
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			/*
			FormCodeAP FrCAP = new FormCodeAP(this);
			FrCAP.ShowDialog();
			 * */
		}

		private void buttonSub_Click(object sender, EventArgs e)
		{
			// listBoxCodesAP.Items.Remove(listBoxCodesAP.SelectedItem);
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			/*  if (MessageBox.Show("Очистить список", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				  listBoxCodesAP.Items.Clear();*/
		}

		private void checkBoxReturnToContrac_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReturnToContrac.CheckState == CheckState.Indeterminate) return;
			checkBoxComplaint.Checked = checkBoxBackSale.Checked = checkBoxReturnToContrac.Checked;
			checkBoxExpensGroupChange(sender, e);
		}

		private void checkBoxRecipesGross_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxRecipesGrossDiscount.Checked = checkBoxRecipesGross.Checked;
			checkBoxExpensGroupChange(sender, e);
		}

		private void checkBoxExpensDiscount_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxSK.Checked = checkBoxExpensDiscount.Checked;
			checkBoxExpensGroupChange(sender, e);
			checkBoxReceiptsGroupChange(sender, e);
		}

		private void checkBoxReceipts_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReceipts.CheckState == CheckState.Indeterminate) return;
			checkBoxService.Checked = checkBoxExpensDiscount.Checked = checkBoxCash.Checked = checkBoxCashless.Checked = checkBoxReceipts.Checked;
			checkBoxExpensGroupChange(sender, e);
		}

		private void checkBoxSK_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxExpensDiscount.Checked = checkBoxSK.Checked;
			checkBoxExpensGroupChange(sender, e);
		}

		private void checkBoxRecipesGrossDiscount_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxRecipesGross.Checked = checkBoxRecipesGrossDiscount.Checked;
			checkBoxExpensGroupChange(sender, e);
		}

		private void ucContractor_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

	}
}