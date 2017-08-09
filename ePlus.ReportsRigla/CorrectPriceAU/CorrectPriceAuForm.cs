using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace CorrectPriceAU
{
    public partial class CorrectPriceAuForm : ExternalReportForm, IExternalReportFormMethods
    {
        private string fileXml = Path.Combine(Utils.TempDir(), "CorrectPriceAuForm.xml");

        public CorrectPriceAuForm()
        {
            InitializeComponent();
            Text = "Корректировка цен АУ";
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucProducer.AddItems(root, "ID_PRODUCER");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucDrugStore.AddItems(root, "ID_DRUGSTORE");
            ucGoods.AddItems(root, "ID_GOODS");
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_CORRECT_PRICE_AU", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.BindDataSource("CorrectPriceAu_DS_Table", 0);
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("PRODUCER", ucProducer.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("DRUGSTORE", ucDrugStore.TextValues());
            rep.AddParameter("GOODS", ucGoods.TextValues());
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Корректировка цен АУ"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AnalisysReports).Description;
            }
        }

        private void CorrectPriceAuForm_Load(object sender, EventArgs e)
        {
            //if (!File.Exists(fileXml)) return;
            //XmlDocument doc = new XmlDocument();
            //doc.Load(fileXml);
            //XmlNode root = doc.SelectSingleNode("XML");
            //textFileName.Text = Utils.GetString(root, "DIR_FILE");
            //XmlNodeList listNodes = root.SelectNodes("LIST/INVENTORY_VED");
            //foreach (XmlNode node in listNodes)
            //{
            //    DataRowItem dri = new DataRowItem(0, Utils.GetGuid(node, "ID_INVENTORY_VED_GLOBAL"), string.Empty, Utils.GetString(node, "DOC_NAME"));
            //    ucInventoryVed.Items.Add(dri);
            //}
        }

        private void CorrectPriceAuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            //XmlNode root = Utils.AddNode(doc, "XML");
            //Utils.AddNode(root, "DIR_FILE", textFileName.Text);
            //XmlNode listNode = Utils.AddNode(root, "LIST");
            //foreach (DataRowItem dri in ucInventoryVed.Items)
            //{
            //    XmlNode driNode = Utils.AddNode(listNode, "INVENTORY_VED");
            //    Utils.AddNode(driNode, "ID_INVENTORY_VED_GLOBAL", dri.Guid);
            //    Utils.AddNode(driNode, "DOC_NAME", dri.Text);
            //}
            //doc.Save(fileXml);
        }
    }
}