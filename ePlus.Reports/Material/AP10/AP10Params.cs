using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace AP10
{
	public partial class AP10Params : ExternalReportForm, IExternalReportFormMethods
	{
		public AP10Params()
		{
			InitializeComponent();

			multiGoods.AllowSaveState = true;
			multiStore.AllowSaveState = true;
			 
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			multiStore.AddItems(root, "ID_STORE");
			multiGoods.AddItems(root, "ID_GOODS");

			ReportFormNew rep = new ReportFormNew();

			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
				shortCheckBox.Checked ? "AP10_short.rdlc" : "AP10.rdlc");

			rep.LoadData("DBO.REPEX_AP_10", doc.InnerXml);

			rep.BindDataSource("AP10_DS_Table0", 0);
			rep.BindDataSource("AP10_DS_Table1", 1);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);			

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Журнал учета ядовитых, наркотических, других медикаментов и этилового спирта"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);	
		
			multiGoods.Items.Clear();
			multiStore.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
	}
}