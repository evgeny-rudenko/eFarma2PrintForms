using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace SaleAnalysisKaliningrad
{
	public partial class RemainderFrom1CParams : ExternalReportForm, IExternalReportFormMethods
	{
		public RemainderFrom1CParams()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		public void Print(string[] reportFiles)
		{
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode( doc, "XML", null );
			ucPeriod.AddValues(root);
			Utils.AddNode(root, "REMAINS_ONLY", chkRemainsOnly.Checked ? 1 : 0);
            ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
			rep.LoadData("REP_SALE_ANALYSIS_KALININGRAD", doc.InnerXml);
			//rep.SaveSchema(@"C:\data.xml");
			//return;
			rep.BindDataSource("SaleAnalysisKaliningrad_DS_Table", 0);            
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText );
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Анализ продаж (Калининград)"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
		}

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
            }
        }

	}
}