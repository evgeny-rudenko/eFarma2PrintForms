using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.IO;

namespace RCChGoodsDefectUnion
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{

    private string settingsFilePath;
		public FormParams()
		{
        	InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
            if ((ucPeriod.DateTo < Convert.ToDateTime("01.01.1753")) || (ucPeriod.DateFrom < Convert.ToDateTime("01.01.1753")))
            {
                MessageBox.Show("Ошибка в периоде, минимальное значение даты  01.01.1753");
                return;
            }
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			string spath = SecurityContextEx.Context.User.Xml_data;
			int mname = 4;
			int mser = 4;

			if (!string.IsNullOrEmpty(spath))
			{
				XmlDocument settings = new XmlDocument();
				settings.LoadXml(spath);
				XmlNode mnameNode = settings.SelectSingleNode(settings.DocumentElement.Name + "/DEFECT_SETTINGS/SETTINGS/LENGTH_DRUGTXT");
				XmlNode mserNode = settings.SelectSingleNode(settings.DocumentElement.Name + "/DEFECT_SETTINGS/SETTINGS/LENGTH_SERIES");
				if (mnameNode != null && !int.TryParse(mnameNode.InnerText, out mname)) mname = 4;
				if (mserNode != null && !int.TryParse(mserNode.InnerText, out mser)) mser = 4;
			}

			Utils.AddNode(root, "MNAME", mname);
			Utils.AddNode(root, "MSER", mser);
			Utils.AddNode(root, "TYPE", typeComboBox.SelectedIndex);
			Utils.AddNode(root, "ORDER", rbSortName.Checked ? "0" : "1");

			ucPeriod.AddValues(root);

			ucPluginMulti_Store.AddItems(root, "ID_STORE");
			ucPluginMulti_Contractor.AddItems(root, "ID_CONTRACTOR");
			ucPluginMulti_Invoice.AddItems(root, "ID_INVOICE");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.AddParameter("Pm_ViewGoodsCode", showCodeCheckBox.Checked ? "1" : "0");
			rep.AddParameter("stores", ucPluginMulti_Store.TextValues());
			rep.AddParameter("contr", ucPluginMulti_Contractor.TextValues());
			rep.AddParameter("hide", typeComboBox.SelectedIndex != 1 ? "0" : "1");

			string detail = null;
			if (typeComboBox.SelectedIndex == 0)
				detail = "по остаткам товара за период с " + ucPeriod.DateFrText + " по " + ucPeriod.DateToText;
			else if (typeComboBox.SelectedIndex == 1)
				detail = "по приходным накладным: " + ucPluginMulti_Invoice.TextValues();
			else if (typeComboBox.SelectedIndex == 2)
				detail = "по приходным накладным за период с " + ucPeriod.DateFrText + " по " + ucPeriod.DateToText;

			rep.AddParameter("detail", detail);

			rep.LoadData("DBO.REPEX_GOODS_DEFECT_UNION", doc.InnerXml);
			rep.BindDataSource("GOODS_DEFECT_DS_Table", 0);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Фальсификаты на остатках"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

			ucPluginMulti_Contractor.Items.Clear();
			ucPluginMulti_Invoice.Items.Clear();
			ucPluginMulti_Store.Items.Clear();

			typeComboBox.SelectedIndex = 0;
			rbSortName.Checked = true;
			showCodeCheckBox.Checked = false; 
		}

		
		private const string TYPE = "TYPE";
		private const string SHOW_CODE = "SHOW_CODE";
		private const string SORT_NAME = "SORT_NAME";
		private const string SORT_SER = "SORT_SER";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			typeComboBox.SelectedIndex = Utils.GetInt(root, TYPE);
			showCodeCheckBox.Checked = Utils.GetBool(root, SHOW_CODE);
			rbSortName.Checked = Utils.GetBool(root, SORT_NAME);
			rbSortSeries.Checked = Utils.GetBool(root, SORT_SER);

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucPluginMulti_Contractor.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucPluginMulti_Store.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList invoices = root.SelectNodes("INVOICES");
      foreach (XmlNode node in invoices)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucPluginMulti_Invoice.AddRowItem(new DataRowItem(id, guid, code, text));
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

			Utils.AddNode(root, TYPE, typeComboBox.SelectedIndex);
			Utils.AddNode(root, SHOW_CODE, showCodeCheckBox.Checked);
			Utils.AddNode(root, SORT_NAME, rbSortName.Checked);
			Utils.AddNode(root, SORT_SER, rbSortSeries.Checked);

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      foreach (DataRowItem dri in ucPluginMulti_Contractor.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucPluginMulti_Store.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucPluginMulti_Invoice.Items)
      {
        XmlNode node = Utils.AddNode(root, "INVOICES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

			doc.Save(settingsFilePath);
		}

		private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ucPeriod.Enabled = typeComboBox.SelectedIndex == 2;
			ucPluginMulti_Contractor.Enabled = typeComboBox.SelectedIndex == 0;
			ucPluginMulti_Store.Enabled = typeComboBox.SelectedIndex == 0;
			ucPluginMulti_Invoice.Enabled = typeComboBox.SelectedIndex == 1;			
		}

        private void FormParams_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            ClearValues();
        }
	}
}