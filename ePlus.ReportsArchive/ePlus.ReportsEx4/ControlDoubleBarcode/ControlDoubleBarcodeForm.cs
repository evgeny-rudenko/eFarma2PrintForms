using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace ControlDoubleBarcode
{
    public partial class ControlDoubleBarcodeForm : ExternalReportForm, IExternalReportFormMethods
    {
        public ControlDoubleBarcodeForm()
        {
            InitializeComponent();
            Text = "Отчет контроль дублей чеков";
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "CH_STOCK", chbStock.Checked);
            Utils.AddNode(root, "CH_CODE", chbVisibleCode.Checked);
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_CONTROL_DOUBLE_BARCODE", doc.InnerXml);
            rep.AddParameter("CODE_VIS", chbVisibleCode.Checked ? "1" : "0");
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.BindDataSource("ControlDoubleBarcode_DS_Table", 0);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчет контроль дублей чеков"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
        }
    }
}