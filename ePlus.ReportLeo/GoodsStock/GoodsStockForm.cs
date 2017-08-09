using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace GoodsStock
{
    public partial class GoodsStockForm : ExternalReportForm, IExternalReportFormMethods
    {
        public GoodsStockForm()
        {
            InitializeComponent();
            Text = "Товарные запасы";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return "Товарные запасы"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucFin.AddItems(root, "ID_ENUMERATION_VALUE");
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_GOODS_STOCK", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("FIN", ucFin.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("GoodsStock_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}