using System;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WinForms;
using ePlus.MetaData.Client;

namespace RCBIndividualSales_Rigla
{
	public partial class IndividualSalesParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string settingsFilePath;
		private const string CASHIER = "Cashier";
		private const string GOODS = "Goods";
		private const string CODE = "Code";
		private const string CODE_ENABLED = "CodeEnabled";

		public IndividualSalesParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			string contractorName = string.Empty;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(@"DBO.REPEX_INDIVIDUAL_SALES_HELPER", con);
				command.CommandType = CommandType.StoredProcedure;
				con.Open();
				contractorName = (string) command.ExecuteScalar();
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);
			Utils.AddNode(root, "CASH", cashierCheckBox.Checked ? "1" : "0");
			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucCashier.AddItems(root, "ID_CASHIER");
			ucGoods.AddItems(root, "ID_GOODS");
			foreach (CatalogItem item in ucGoodsGroup.Items)
			{
				Utils.AddNode(root, "ID_GOODS_GROUP", item.Id);
			}

			ReportFormNew rep = new ReportFormNew();

			rep.ReportPath = Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]), "IndividualSales.rdlc");

			rep.LoadData("DBO.REPEX_INDIVIDUAL_SALES", doc.InnerXml);
			rep.BindDataSource("IndividualSales_DS_Table1", 0);
			rep.BindDataSource("IndividualSales_DS_Table2", 1);
			rep.BindDataSource("IndividualSales_DS_Table3", 2);

			rep.AddParameter("date_from", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("rk", contractorName);
			rep.AddParameter("contractor", ucContractors.TextValues());
			rep.AddParameter("cashier", ucCashier.TextValues());
			rep.AddParameter("goods", ucGoods.TextValues());
			rep.AddParameter("show_code", codeCheckBox.Checked ? "1" : "0");
			rep.AddParameter("show_goods", goodsCheckBox.Checked ? "1" : "0");
			rep.AddParameter("show_cashier", cashierCheckBox.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();

			ucContractors.Items.Clear();
			ucGoods.Items.Clear();
			ucGoodsGroup.Clear();
			ucCashier.Items.Clear();

			cashierCheckBox.Checked = false;
			goodsCheckBox.Checked = false;
			codeCheckBox.Checked = false;
			codeCheckBox.Enabled = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по индивидуальным продажам"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}

		private void LoadSettings()
		{
      ClearValues();
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

      // группы товаров
      XmlNodeList group = root.SelectNodes("ID_GOODS_GROUP");
      foreach (XmlNode node in group)
      {
        CatalogItem ci = new CatalogItem();
        ci.Id = Utils.GetLong(node, "ID");
        ci.Name = Utils.GetString(node, "TEXT");
        ci.ParentId = Utils.GetLong(node, "PARENT_ID");
        ucGoodsGroup.AddItem(ci);
      }

      // товары
      XmlNodeList goods = root.SelectNodes("ID_GOODS");
      foreach (XmlNode node in goods)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucGoods.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      // кассиры
      XmlNodeList cashiers = root.SelectNodes("ID_CASHIER");
      foreach (XmlNode node in cashiers)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucCashier.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      // контрагенты
      XmlNodeList contractors = root.SelectNodes("ID_CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucContractors.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

			cashierCheckBox.Checked = Utils.GetBool(root, CASHIER);
			goodsCheckBox.Checked = Utils.GetBool(root, GOODS);
			codeCheckBox.Checked = Utils.GetBool(root, CODE);
			codeCheckBox.Enabled = Utils.GetBool(root, CODE_ENABLED);
		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      //группы товаров
      foreach (CatalogItem dri in ucGoodsGroup.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_GOODS_GROUP");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Name);
        Utils.AddNode(node, "PARENT_ID", dri.ParentId);
      }

      //товары
      foreach (DataRowItem dri in ucGoods.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_GOODS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      //кассиры
      foreach (DataRowItem dri in ucCashier.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_CASHIER");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      //контрагенты
      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			Utils.AddNode(root, CASHIER, cashierCheckBox.Checked);
			Utils.AddNode(root, GOODS, goodsCheckBox.Checked);
			Utils.AddNode(root, CODE, codeCheckBox.Checked);
			Utils.AddNode(root, CODE_ENABLED, codeCheckBox.Enabled);

			doc.Save(settingsFilePath);
		}

		private void IndividualSalesParams_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
			LoadSettings();
		}

		private void IndividualSalesParams_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
		}

		private void goodsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			codeCheckBox.Enabled = goodsCheckBox.Checked;
			if (goodsCheckBox.Checked == false)
				codeCheckBox.Checked = false;
		}
	}
}