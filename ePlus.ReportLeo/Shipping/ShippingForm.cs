using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace Shipping
{
    public partial class ShippingForm : ExternalReportForm, IExternalReportFormMethods
    {
        public ShippingForm()
        {
            InitializeComponent();
            Text = "Отгрузка";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Отгрузка"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucSupplier.AddItems(root, "ID_SUPPLIER");
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_SHIPPING", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("SUPPLIER", ucSupplier.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("Shipping_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}