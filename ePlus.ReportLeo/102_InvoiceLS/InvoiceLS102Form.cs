using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceLS102
{
    public partial class InvoiceLS102Form : ExternalReportForm, IExternalReportFormMethods
    {
        public InvoiceLS102Form()
        {
            InitializeComponent();
            Text = "Данные о поставках ЛС (Приложение 2)";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Данные о поставках ЛС (Приложение 2)"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucSupplier.AddItems(root, "ID_CONTRACTOR");
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
            rep.LoadData("REPEX_INVOICE_LS_102", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());
            rep.AddParameter("CONTRACTOR", ucSupplier.TextValues());

            rep.BindDataSource("InvoiceLS102_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}