using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InfoMonitoring37
{
    public partial class InfoMonitoring37Form : ExternalReportForm, IExternalReportFormMethods
    {
        public InfoMonitoring37Form()
        {
            InitializeComponent();
            Text = @"Сведения для мониторинга";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return @"Сведения для мониторинга"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucPayment.AddItems(root, "ID_CONTRACTOR_GROUP");
            ucGroupGoods.AddItems(root, "ID_GOODS_GROUP");
            
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INFO_MONITORING_37", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("DATE_TO", ucPeriod.DateFrText);
            rep.AddParameter("DATE_FR", ucPeriod.DateToText);
            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR_GROUP", ucGroupGoods.TextValues());
            rep.AddParameter("GOODS_GROUP", ucPayment.TextValues());
            rep.BindDataSource("InfoMonitoring37_DS_Table", 0);
            rep.BindDataSource("InfoMonitoring37_DS_Table1", 1);
            rep.ExecuteReport(this);

        }
    }
}