using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace List71
{
    public partial class List71Form : ExternalReportForm, IExternalReportFormMethods
    {
        public List71Form()
        {
            InitializeComponent();
            Text = "Перечень, объем и цены";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Перечень, объем и цены"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucSupplier.AddItems(root, "ID_SUPPLIER");
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
            rep.LoadData("REPEX_LIST_71", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("CONTRACTOR", ucSupplier.TextValues());
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("List71_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}