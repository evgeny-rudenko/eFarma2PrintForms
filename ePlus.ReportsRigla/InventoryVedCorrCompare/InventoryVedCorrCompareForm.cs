using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InventoryVedCorrCompare
{
    public partial class InventoryVedCorrCompareForm : ExternalReportForm, IExternalReportFormMethods
    {
        public InventoryVedCorrCompareForm()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            foreach (DataRowItem dri in ucInventoryVed.Items)
                Utils.AddNode(root, "ID_INVENTORY_VED_GLOBAL", dri.Guid);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVENTORY_VED_CORR_COMPARE", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.BindDataSource("InventoryVedCorrCompare_DS_Table", 0);
            rep.AddParameter("INV", ucInventoryVed.TextValues());
            rep.ExecuteReport(this);
        }


        public string ReportName
        {
            get { return "Корректность ввода данных пересчета"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }
    }
}