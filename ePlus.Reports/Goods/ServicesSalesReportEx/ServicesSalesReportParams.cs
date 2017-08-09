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

namespace ServicesSalesAnalysisEx
{
	public partial class ServicesSalesReportParams : ExternalReportForm, IExternalReportFormMethods
	{
		public ServicesSalesReportParams()
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
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			foreach (DataRowItem dr in ucServices.Items)
			{
				Utils.AddNode(root, "ID_SERVICE", dr.Guid);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_SERVICES_SALES_REPORT", doc.InnerXml);
			rep.BindDataSource("ServicesSalesReport_Table0", 0);
			rep.BindDataSource("ServicesSalesReport_Table1", 1);

			rep.AddParameter("date_from", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			ucServices.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Акт реализации услуг"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
		}

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
      {
        return;
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
      {
        return;
      }

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList services = root.SelectNodes("SERVICES");
      foreach (XmlNode node in services)
      {
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        ucServices.AddRowItem(new DataRowItem(0, guid, String.Empty, text));
      }
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

      foreach (DataRowItem dri in ucServices.Items)
      {
        XmlNode node = Utils.AddNode(root, "SERVICES");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(SettingsFilePath);
    }

    private void ServicesSalesReportParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void ServicesSalesReportParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
	}
}