using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace AnalizeAction
{
    public partial class AnalizeActionForm : ExternalReportForm, IExternalReportFormMethods
    {
        public AnalizeActionForm()
        {
            InitializeComponent();
            Text = "Реестр счетов-фактур (без даты)";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Реестр счетов-фактур (без даты)"; }
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
            rep.LoadData("REPEX_ANALIZE_ACTION", doc.InnerXml);
            rep.SaveSchema(@"c:\data.xml");
            return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("AnalizeAction_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}