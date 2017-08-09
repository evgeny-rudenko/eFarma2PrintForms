using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceOut
{
    public partial class InvoiceOutForm : ExternalReportForm, IExternalReportFormMethods
    {
        private readonly string name = "Отпуск товара по расходным накладным";

        public InvoiceOutForm()
        {
            InitializeComponent();
            Text = name;
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return name; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_OUT", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());

            rep.BindDataSource("InvoiceOut_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}