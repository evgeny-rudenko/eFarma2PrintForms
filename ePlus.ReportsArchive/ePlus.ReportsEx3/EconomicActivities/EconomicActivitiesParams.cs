using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace EconomicActivities
{
	public partial class EconomicActivitiesParams : ExternalReportForm, IExternalReportFormMethods
	{
		public EconomicActivitiesParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			if (ucContractor.Id == 0)
			{
				MessageBox.Show("Не выбран контрагент!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_ECONOMIC_ACTIVITIES", doc.InnerXml);
			rep.BindDataSource("EconomicActivities_DS_Table0", 0);
			rep.BindDataSource("EconomicActivities_DS_Table1", 1);

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("CONTRACTOR_NAME", ucContractor.Text),
				new ReportParameter("DATE_FROM", ucPeriod.DateFrText),
				new ReportParameter("DATE_TO", ucPeriod.DateToText)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);

		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
			LoadSelf();
		}

		private void LoadSelf()
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1", con);
				con.Open();
				ucContractor.SetId((long) command.ExecuteScalar());
			}
		}

		public string ReportName
		{
			get { return "Основные экономические показатели АП"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void EconomicActivitiesParams_Load(object sender, EventArgs e)
		{
			LoadSelf();
		}
	}
}

