using System;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;
using ePlus.MetaData.Client;

namespace RCBBestBefore246_Rigla
{
	public partial class BestBefore246Params : ExternalReportForm, IExternalReportFormMethods
	{
		public BestBefore246Params()
		{
			InitializeComponent();
		}

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

		public void Print(string[] reportFiles)
		{
			DateTime date = DateTime.Now.AddMonths(1);
			DateTime firstDayOfTheNextMonth = new DateTime(date.Year, date.Month, 1);

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucStores.AddItems(root, "ID_STORE");

			if (comboBox1.SelectedIndex == 0)
			{
				Utils.AddNode(root, "DATE_N", firstDayOfTheNextMonth);
				//string vtemp = "";

				/*if (comboSort.Text == "Название товара")
				{
					vtemp = "order by GOODS_NAME";
				}
				if (comboSort.Text == "Срок годности")
				{
					vtemp = "order by BEST_BEFORE";
				}*/

				//Utils.AddNode(root, "ORDER_BY", vtemp);
			}
			else
			{
				ucPeriod.AddValues(root);
				Utils.AddNode(root, "USE_DATE", periodCheckBox.Checked ? "0" : "1");
				//Utils.AddNode(root, "SORT", comboSort.SelectedIndex == 0 ? "1" : "0");
			}		
			
			ReportFormNew rep = new ReportFormNew();

			if (comboBox1.SelectedIndex == 0)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "BestBefore246.rdlc");

				rep.LoadData("REPEX_BEST_BEFORE_246", doc.InnerXml);
				rep.BindDataSource("BestBefore246_Table0", 0);
				rep.BindDataSource("BestBefore246_Table1", 1);

				rep.AddParameter("date", firstDayOfTheNextMonth.ToShortDateString());
				rep.AddParameter("contractors", ucContractors.TextValues());
				rep.AddParameter("stores", ucStores.TextValues());
			}
			else
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "BestBefore.rdlc");
				rep.LoadData("REPEX_BEST_BEFORE_RIGLA", doc.InnerXml);
				rep.BindDataSource("BestBefore_DS_Table0", 0);
				rep.BindDataSource("BestBefore_DS_Table1", 1);
			}
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodNow();
			ucContractors.Items.Clear();
			ucStores.Items.Clear();
			periodCheckBox.Checked = false;
			comboBox1.SelectedIndex = 0;
      //comboSort.SelectedIndex = 0;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по срокам годности товаров"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			ucPeriod.Enabled = label2.Enabled = periodCheckBox.Enabled = comboBox1.SelectedIndex == 1;
		}

		private void periodCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ucPeriod.Enabled = label2.Enabled = !periodCheckBox.Checked;
		}

		private void LoadSettings()
		{
      ClearValues();
      if (!File.Exists(SettingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucContractors.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORE");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

			comboBox1.SelectedIndex = Utils.GetInt(root, REPORT_TYPE);
			periodCheckBox.Checked = Utils.GetBool(root, USE_DATE);
//			comboSort.SelectedIndex = Utils.GetInt(root, SORT);
		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root;

      if (File.Exists(SettingsFilePath))
			{
        doc.Load(SettingsFilePath);
				root = doc.SelectSingleNode("//XML");
				root.RemoveAll();
			}
			else
			{
				root = Utils.AddNode(doc, "XML");
			}

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

			Utils.AddNode(root, REPORT_TYPE, comboBox1.SelectedIndex);
			Utils.AddNode(root, USE_DATE, periodCheckBox.Checked);
			//Utils.AddNode(root, SORT, comboSort.SelectedIndex);

      doc.Save(SettingsFilePath);
		}

		//private string settingsFilePath = Path.Combine(Utils.TempDir(), "BestBeforeRiglaSettings.xml");
		private const string REPORT_TYPE = "REPORT_TYPE";
		private const string USE_DATE = "USE_DATE";
		private const string SORT = "SORT";

		private void BestBefore246Params_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void BestBefore246Params_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}