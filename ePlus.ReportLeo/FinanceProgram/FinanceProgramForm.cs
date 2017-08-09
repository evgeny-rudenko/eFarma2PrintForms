using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace FinanceProgram
{
    public partial class FinanceProgramForm : ExternalReportForm, IExternalReportFormMethods
    {
        public FinanceProgramForm()
        {
            InitializeComponent();
            Text = "Сведения по программам финансирования";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Сведения по программам финансирования"; }
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
            rep.LoadData("REPEX_FINANCE_PROGRAM", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_TO", ucPeriod.DateFrText);
            rep.AddParameter("DATE_FR", ucPeriod.DateToText);
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("FinanceProgram_DS_Table", 0);
            rep.ExecuteReport(this);
        }
    }
}