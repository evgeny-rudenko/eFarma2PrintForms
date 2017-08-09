using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace ContractMov
{
    public partial class ContractMovForm : ExternalReportForm, IExternalReportFormMethods
    {
        public ContractMovForm()
        {
            InitializeComponent();
            Text = "Договора-контракты";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Договора-контракты"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucSupplier.AddItems(root, "ID_SUPPLIER");
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            foreach (DataRowItem row in ucContracts.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTS_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_CONTRACT_MOV", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("SUPPLIER", ucSupplier.TextValues());
            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());

            rep.BindDataSource("ContractMov_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}