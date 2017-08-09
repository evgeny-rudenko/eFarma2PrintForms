using System;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace F77MImun
{
	public partial class ImmunobiologicalTrafficLogParams : ExternalReportForm, IExternalReportFormMethods
	{
		public ImmunobiologicalTrafficLogParams()
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

            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "ImmunobiologicalTrafficLog.rdlc");

            rep.LoadData("DBO.IMMUNOBIOLOGICAL_TRAFFIC_LOG", doc.InnerXml);

            rep.BindDataSource("IMMUNOBIOLOGICAL_DS_Table0", 0);
            rep.BindDataSource("IMMUNOBIOLOGICAL_DS_Table1", 1);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);			

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
            get { return "Журнал регистрации движения МИБП"; }
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