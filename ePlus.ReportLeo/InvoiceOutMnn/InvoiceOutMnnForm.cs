using System;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceOutMnn
{
    public partial class InvoiceOutMnnForm : ExternalReportForm, IExternalReportFormMethods
    {
        private const string name = "Отпуск товаров за период";

        public InvoiceOutMnnForm()
        {
            InitializeComponent();
            Text = name;
            ucPeriod.SetPeriodMonth();
            }

        public string ReportName
        {
            get { return name; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucGoodsGroup.AddItems(root, "ID_GOODS_GROUP");
            ucMNN.AddItems(root, "ID_MNN");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_OUT_MNN", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("GOODS_GROUP", ucGoodsGroup.TextValues());
            rep.AddParameter("MNN", ucGoodsGroup.TextValues());

            rep.BindDataSource("InvoiceOutMnn_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}