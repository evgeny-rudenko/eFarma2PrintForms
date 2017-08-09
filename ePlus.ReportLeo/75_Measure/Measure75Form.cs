using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace Measure75
{
    public partial class Measure75Form : ExternalReportForm, IExternalReportFormMethods
    {
        public Measure75Form()
        {
            InitializeComponent();
            Text = "Мероприятия по предоставлению мер";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Мероприятия по предоставлению мер государственной социальной поддержки отдельным категориям граждан"; }
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
            rep.LoadData("REPEX_MEASURE_75", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("PROGRAM", ucProgram.TextValues());
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());

            rep.BindDataSource("Measure75_DS_Table", 0);
            rep.ExecuteReport(this);
        }
    }
}