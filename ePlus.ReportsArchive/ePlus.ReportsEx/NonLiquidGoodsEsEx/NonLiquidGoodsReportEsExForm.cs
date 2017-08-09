using System;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;

namespace NonLiquidGoodsEsEx
{
    public partial class NonLiquidGoodsEsExForm : ExternalReportForm, IExternalReportFormMethods
    {
        public NonLiquidGoodsEsExForm()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "SHOW_LOTS", chkShowLots.Checked);

            store.AddItems(root, "STORE");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.AddParameter("DATE_FROM", period.DateFrText);
            rep.AddParameter("DATE_TO", period.DateToText);
            rep.AddParameter("STORES", string.IsNullOrEmpty(store.ToCommaDelimetedStringList()) ? "Все склады" : store.ToCommaDelimetedStringList());
            rep.LoadData("REP_NON_LIQUID_GOODS_ES_EX", doc.InnerXml);
            rep.BindDataSource("NonLiquidGoodsEsEx_DS_Table", 0);
            rep.BindDataSource("NonLiquidGoodsEsEx_DS_Table1", 1);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчет по неликвидным товарам"; }
        }
    }
}