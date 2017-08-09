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

namespace ServicesSalesAnalysisEx
{
	public partial class ServicesSalesAnalysisParams : ExternalReportForm, IExternalReportFormMethods
	{
		public ServicesSalesAnalysisParams()
		{
			InitializeComponent();
			ClearValues();
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

			rep.LoadData("REPEX_SERVICES_SALES_ANALYSIS", doc.InnerXml);
			rep.BindDataSource("ServicesSalesAnalysis_Table0", 0);

			rep.AddParameter("date_from", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("services", ucServices.TextValues());

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
			get { return "Анализ продаж по дополнительным услугам"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}
	}
}