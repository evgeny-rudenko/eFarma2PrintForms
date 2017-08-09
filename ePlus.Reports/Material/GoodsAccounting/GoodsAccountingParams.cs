using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using System.IO;

namespace RCSGoodsAccounting
{
	public partial class GoodsAccountingParams : ExternalReportForm, IExternalReportFormMethods
	{
		public GoodsAccountingParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			if (ucStore.Id == 0)
			{
				MessageBox.Show("Не выбран склад", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (ucGoods.Id == 0)
			{
				MessageBox.Show("Не выбран товар", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);

			Utils.AddNode(root, ID_STORE, ucStore.Id);
			Utils.AddNode(root, ID_GOODS, ucGoods.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_GOODS_ACCOUNTING", doc.InnerXml);
			rep.BindDataSource("GoodsAccounting_DS_Table0", 0);
			rep.BindDataSource("GoodsAccounting_DS_Table1", 1);
			rep.BindDataSource("GoodsAccounting_DS_Table2", 2);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);			
			rep.AddParameter("stores", ucStore.Text);
			rep.AddParameter("goods", ucGoods.Text);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Складской учет товара"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucStore.Clear();
			ucGoods.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "GoodsAccountingSettings.xml");
		private const string ID_STORE = "ID_STORE";
		private const string ID_GOODS = "ID_GOODS";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			ucStore.SetId(Utils.GetInt(root, ID_STORE));
			ucGoods.SetId(Utils.GetInt(root, ID_GOODS));
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

			Utils.AddNode(root, ID_STORE, ucStore.Id);
			Utils.AddNode(root, ID_GOODS, ucGoods.Id);

			doc.Save(settingsFilePath);
		}

		private void GoodsAccountingParams_Load(object sender, EventArgs e)
		{
            ClearValues();
			LoadSettings();
		}

		private void GoodsAccountingParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}