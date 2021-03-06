using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;

namespace TORG29ORNDisEKB
{
    public partial class ORNDisFormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public ORNDisFormParams()
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
            if (mpsContractor.Items.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML");
                Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
                Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
                Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
                Utils.AddNode(root, "SORT_DOC", rbDocType.Checked ? "1" : "0");

                foreach (DataRowItem dr in mpsContractor.Items)
                    Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

                foreach (DataRowItem dr in mpsStore.Items)
                    Utils.AddNode(root, "ID_STORE", dr.Id);
                Utils.AddNode(root, "REFRESH_DOC_MOV", chkRefreshDocMov.Checked ? "1" : "0");

                ReportFormNew rep = new ReportFormNew();
                if (rbDocType.Checked)
                    rep.ReportPath = reportFiles[0];
                else rep.ReportPath = reportFiles[1];
                rep.LoadData("REP_GOODS_REPORTS_NAL_WITH_DIS_EKB_EX", doc.InnerXml);

                rep.BindDataSource("GoodsReportsNal_DS_dtBegin", 0);
                rep.BindDataSource("GoodsReportsNal_DS_dtAdd", 1);
                rep.BindDataSource("GoodsReportsNal_DS_dtSub", 2);
                rep.BindDataSource("GoodsReportsNal_DS_dtEnd", 3);
                rep.BindDataSource("GoodsReportsNal_DS_dtContractor", 4);

                rep.AddParameter("date_fr", ucPeriod.DateFrText);
                rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
                rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
                rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
                rep.AddParameter("show_date", chkDateReport.Checked ? "1" : "0");
                rep.AddParameter("show_discount", chkColumnSale.Checked ? "1" : "0");
                rep.ExecuteReport(this);
            }
            else MessageBox.Show("�������� �����������!");
        }

        public string ReportName
        {
            get { return "���� 29 ���-�������-��������� �� ������� (���)"; }
        }
    }
}