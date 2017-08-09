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

namespace RemainderFrom1CEx
{
	public partial class RemainderFrom1CParams : ExternalReportForm, IExternalReportFormMethods
	{
		public RemainderFrom1CParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "NO_DETAIL", checkBox_Detail.Checked ? "1" : "0" );

            ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_REMAINDER_FROM_1C", doc.InnerXml);
            rep.BindDataSource("RemainderFrom1C_DS_Table", 0);            

            rep.AddParameter("date_fr", ucPeriod.DateFrText );
			rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("nodetail", checkBox_Detail.Checked ? "1" : "0" );

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Сводный о вводе остатков из 1C"; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			checkBox_Detail.Checked = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
        }
	}
}