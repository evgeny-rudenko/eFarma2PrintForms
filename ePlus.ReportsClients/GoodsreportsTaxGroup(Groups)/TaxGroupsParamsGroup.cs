using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ePlus.CommonEx.Reporting;
using ePlus.CommonEx.Controls;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;

namespace GoodsReportsTaxGroup_Groups
{
    public partial class TaxGroupsParamsGroup : ExternalReportForm, IExternalReportFormMethods
    {
        public TaxGroupsParamsGroup()
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
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? 1 : 0);
            Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
            Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
            if (rbDocType.Checked)
                Utils.AddNode(root, "SORT_DOC", 1);  //по видам док
            else Utils.AddNode(root, "SORT_DOC", 0);  //по датам док

            foreach (long store in ucSelectContractorStores.SelectedStores)
                Utils.AddNode(root, "ID_STORE", store);

            if (chkRefreshDocMov.Checked)
                Utils.AddNode(root, "REFRESH_DOC_MOV", 1);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];

            if (serviceCheckBox.Checked)
            {
                rep.LoadData("REP_GOODS_REPORTS_TO_EX_SERVICE", doc.InnerXml);
            }
            else
            {
                rep.LoadData("REP_GOODS_REPORTS_GROUPS_TG_EX", doc.InnerXml);
            }

            rep.BindDataSource("Goods_Reports_TO_DS_Table", 0);
            rep.BindDataSource("Goods_Reports_TO_DS_Table1", 1);
            rep.BindDataSource("Goods_Reports_TO_DS_Table2", 2);
            rep.BindDataSource("Goods_Reports_TO_DS_Table3", 4);
            rep.BindDataSource("Goods_Reports_TO_DS_Table4", 5);
            rep.BindDataSource("Goods_Reports_TO_DS_Table5", 3);

            rep.AddParameter("date_fr", ucPeriod.DateFrText);
            rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
            rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
            rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
            rep.AddParameter("show_nal", chbShowNal.Checked ? "1" : "0");
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Товарный отчет по налоговым группам (по группам)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
        }
    }
}