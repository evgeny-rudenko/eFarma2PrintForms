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

namespace KKMCashier_Ex
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "GOODS", checkGoods.Checked ? "1" : "0");

			cashierMulti.AddItems(root, "CASHIER");
			kkmMulti.AddItems(root, "KKM");
			producerMulti.AddItems(root, "PRODUCER");
			contractorMulti.AddItems(root, "CONTRACTOR");
			goodsMulti.AddItems(root, "GOODS");
			
            ReportFormNew rep = new ReportFormNew();

			if (serviceCheckBox.Checked)
			{
				rep.LoadData("REP_KKMCASHIER_EX_SERVICE", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REP_KKMCASHIER_EX", doc.InnerXml);
			}

            rep.BindDataSource("KKM_Cashier_DS_Table2", 0, false);
            rep.BindDataSource("KKM_Cashier_DS_Table3", 1, false);
            rep.BindDataSource("KKM_Cashier_DS_Table1", 2);

            rep.ReportPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]),"KKM_Cashier_goods.rdlc");
            rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
            subReportTable = rep.DataSource.Tables[1];

            //DataRow Row = rep.DataSource.Tables[1].Rows[0];
            rep.AddParameter("date_fr1", period.DateFrText);
            rep.AddParameter("date_to1", period.DateToText);

            rep.AddParameter("id_detail", comboGroup.SelectedIndex.ToString());
            rep.AddParameter("goods", checkGoods.Checked.ToString());
            rep.AddParameter("date_fr", period.DateFrText);
            rep.AddParameter("date_to", period.DateToText);
            rep.AddParameter("detail", checkGoods.Checked ? "1" : "0");

            rep.ExecuteReport(this);            
        }

        DataTable subReportTable;
        private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
        {
            if (Path.GetFileNameWithoutExtension(e.ReportPath) == "KKMCashier_Sub")
            {
                e.DataSources.Add(new ReportDataSource("KKM_Cashier_DS_Table3", subReportTable));
            }
        }

        public string ReportName
        {
            get { return "Отчет по кассирам"; }
        }

        private void comboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboGroup.SelectedIndex == 4)
            {
                labelDays.Enabled = true;
                numericDays.Enabled = true;
            }
            else
            {
                labelDays.Enabled = false;
                numericDays.Enabled = false;
            }
            if (comboGroup.SelectedIndex == 0) checkGoods.Checked = false;
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }
    }
}