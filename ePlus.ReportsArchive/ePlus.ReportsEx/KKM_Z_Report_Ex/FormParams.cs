using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace RCRKKM_Z_Report
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
		private DataTable subReportTable;

        public FormParams()
        {
            InitializeComponent();
        }		

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);

            ucPeriod1.AddValues(root);
			Utils.AddNode(root, "DETAIL", groupByTypeCheckBox.Checked ? "1" : "0");

            if (ucMetaPluginSelect1.Id != 0)
                Utils.AddNode(root, "ID_CASH_REGISTER", ucMetaPluginSelect1.Id);
            if (ucMetaPluginSelect2.Id != 0)
                Utils.AddNode(root, "ID_CONTRACTOR", ucMetaPluginSelect2.Id);

            ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
				reportTypeComboBox.SelectedIndex == 0 ? "KKM_Z_Report_Ex.rdlc" : "KKM_Z_Report_payment_types.rdlc");

			rep.LoadData("REPEX_Z_REPORT", doc.InnerXml);
            rep.BindDataSource("KKM_Z_ReportDS_Table", 0);

			if (groupByTypeCheckBox.Checked)
			{
				rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
				subReportTable = rep.DataSource.Tables[1];				
			}

            rep.AddParameter("date_fr", ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("detail", checkBox_detail.Checked ? "0" : "1");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			if (reportTypeComboBox.SelectedIndex != 0)
			{
				rep.AddParameter("sub", groupByTypeCheckBox.Checked ? "1" : "0");
			}
            
            rep.ExecuteReport(this);
        }

		private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
		{
			if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Z_Sub")
			{
				e.DataSources.Add(new ReportDataSource("KKM_Z_ReportDS_Table1", subReportTable));
			}
		}

        public string ReportName
        {
            get { return "Z-îò÷åò çà ïåðèîä"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ÑlearValues();
		}

		private void ÑlearValues()
		{
			ucPeriod1.DateTo = DateTime.Now;
			ucPeriod1.DateFrom = ucPeriod1.DateTo.AddDays(-13);

			ucMetaPluginSelect1.SetId(0);
			ucMetaPluginSelect2.SetId(0);

			checkBox_detail.Checked = false;
			groupByTypeCheckBox.Checked = false;
			reportTypeComboBox.SelectedIndex = 0;
		}

		private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			groupByTypeCheckBox.Enabled = reportTypeComboBox.SelectedIndex == 1;
		}

        private void FormParams_Load(object sender, EventArgs e)
        {
            ÑlearValues();
        }
    }
}