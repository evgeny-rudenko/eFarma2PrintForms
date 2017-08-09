using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceLS76
{
    public partial class InvoiceLS76Form : ExternalReportForm, IExternalReportFormMethods
    {
        public InvoiceLS76Form()
        {
            InitializeComponent();
            Text = "Заявка и поступление препаратов ДЛО";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Заявка и поступление препаратов ДЛО на аптечный склад"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_LS_76", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("InvoiceLS76_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}