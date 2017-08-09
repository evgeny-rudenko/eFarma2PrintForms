using System;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceOutGoods
{
    public partial class InvoiceOutGoodsForm : ExternalReportForm, IExternalReportFormMethods
    {
        private const string name = "Отпуск по группам товаров";

        public InvoiceOutGoodsForm()
        {
            InitializeComponent();
            Text = name;
            ucDate.Value = DateTime.Now;
        }

        public string ReportName
        {
            get { return name; }
        }

        public void Print(string[] reportFiles)
        {
            if (ucDate.Value == DateTime.MinValue)
            {
                MessageBoxEx.Show("Не указана дата!");
                return;
            }
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            DateTime date = new DateTime(ucDate.Value.Year, ucDate.Value.Month, 1);
            DateTime date_begin = new DateTime(ucDate.Value.Year, 1, 1);
            Utils.AddNode(root, "DATE_BEGIN", date_begin);
            Utils.AddNode(root, "DATE_END", ucDate.Value);
            Utils.AddNode(root, "DATE", date);
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucGoodsGroup.AddItems(root, "ID_GOODS_GROUP");
            

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_OUT_GOODS", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE", ucDate.Value.ToShortDateString());

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("GOODS_GROUP", ucGoodsGroup.TextValues());

            rep.BindDataSource("InvoiceOutGoods_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}