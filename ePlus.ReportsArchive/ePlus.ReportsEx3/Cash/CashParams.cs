using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Cash
{
    public partial class CashParams : ExternalReportForm, IExternalReportFormMethods
    {
        public CashParams()
        {
            InitializeComponent();
			ClearValues();
        }

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

			cashierMulti.Items.Clear();
			kkmMulti.Items.Clear();
			producerMulti.Items.Clear();
			contractorMulti.Items.Clear();
			goodsMulti.Items.Clear();

			detailCheckBox.Checked = false;
			serviceCheckBox.Checked = false;
		}

		private DataTable subReportTable;

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "DETAIL", detailCheckBox.Checked ? "1" : "0");

			cashierMulti.AddItems(root, "CASHIER");
			kkmMulti.AddItems(root, "KKM");
			producerMulti.AddItems(root, "PRODUCER");
			contractorMulti.AddItems(root, "CONTRACTOR");
			goodsMulti.AddItems(root, "GOODS");
			
            ReportFormNew rep = new ReportFormNew();

			if (serviceCheckBox.Checked)
			{
				rep.LoadData("REPEX_CASH_SERVICE", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REPEX_CASH", doc.InnerXml);
			}

            rep.BindDataSource("Cash_DS_Table0", 0);
			rep.BindDataSource("Cash_DS_Table2", 2);

            rep.ReportPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]),"Cash.rdlc");
            rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
            subReportTable = rep.DataSource.Tables[1];

            rep.AddParameter("date_fr", ucPeriod.DateFrText);
            rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("detail", detailCheckBox.Checked ? "1" : "0");
			//rep.AddParameter("period", (ucPeriod.DateTo.Subtract(ucPeriod.DateFrom).Days + 1).ToString());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

            rep.ExecuteReport(this);
        }        

        private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
        {
            if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Cash_Sub")
            {
                e.DataSources.Add(new ReportDataSource("Cash_DS_Table1", subReportTable));
            }
        }

        public string ReportName
        {
            get { return "Отчет по кассирам"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
    }
}