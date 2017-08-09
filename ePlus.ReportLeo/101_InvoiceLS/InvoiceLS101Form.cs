using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceLS101
{
    public partial class InvoiceLS101Form : ExternalReportForm, IExternalReportFormMethods
    {
        public InvoiceLS101Form()
        {
            InitializeComponent();
            Text = "Данные о поставках товаров по ГК";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Данные о поставках товаров по ГК"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            foreach (DataRowItem row in ucContracts.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTS_GLOBAL", row.Guid);
            }
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_LS_101", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("InvoiceLS101_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}