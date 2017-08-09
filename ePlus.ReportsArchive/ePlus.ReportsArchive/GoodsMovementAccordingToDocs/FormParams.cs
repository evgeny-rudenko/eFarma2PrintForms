using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;

namespace GoodsMovementAccordingToDocs
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        private string fileName = Path.Combine(Utils.TempDir(), "GoodsMovementAccordingToDocs.xml");
        public FormParams()
        {
            InitializeComponent();
            if (ucPeriod1 != null)
            {
                ucPeriod1.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod1.DateFrom = ucPeriod1.DateTo.AddDays(-13);
            }
        }

        public void Print(string[] reportFiles)
        {
            if (ucPeriod1.DateFrom > ucPeriod1.DateTo)
                return;
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);
            foreach (DataRowItem store in ucStore.Items)
                Utils.AddNode(root, "ID_STORE", store.Id);
            foreach (DataRowItem contractor in ucContractor.Items)
                Utils.AddNode(root, "ID_CONTRACTOR", contractor.Id);
            foreach (DataRowItem good in ucGoods.Items)
                Utils.AddNode(root, "ID_GOODS", good.Id);
            foreach (DataRowItem supplier in ucSupplier.Items)
                Utils.AddNode(root, "ID_SUPPLIER", supplier.Id);
            foreach (DataRowItem producer in ucProducer.Items)
                Utils.AddNode(root, "ID_PRODUCER", producer.Id);
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("GOODS_MOVEMENT_DOCS_EX", doc.InnerXml);
            rep.BindDataSource("GoodsMovementDocs_DS_Table1", 0);
            rep.BindDataSource("GoodsMovementDocs_DS_Table2", 1);
            rep.BindDataSource("GoodsMovementDocs_DS_Table3", 2);
            rep.BindDataSource("GoodsMovementDocs_DS_Table4", 3);
            rep.AddParameter("date_fr",ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("rep_type",rbFullRep.Checked? "1" : "0");
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Движение товара по документам"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }

        private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            //Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
            //Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);
            foreach (DataRowItem dri in ucContractor.Items)
            {
                XmlNode contractor = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(contractor, "ID_CONTRACTOR", dri.Id);
                Utils.AddNode(contractor, "TEXT_CONTRACTOR", dri.Text);               
            }
            foreach (DataRowItem dri in ucStore.Items)
            {
                XmlNode store = Utils.AddNode(root, "STORES");
                Utils.AddNode(store, "ID_STORE", dri.Id);
                Utils.AddNode(store, "TEXT_STORE", dri.Text);
            }
            foreach (DataRowItem dri in ucGoods.Items)
            {
                XmlNode good = Utils.AddNode(root, "GOODS");
                Utils.AddNode(good, "ID_GOODS", dri.Id);
                Utils.AddNode(good, "TEXT_GOODS", dri.Text);
            }
            foreach (DataRowItem dri in ucSupplier.Items)
            {
                XmlNode supplier = Utils.AddNode(root, "SUPPLIERS");
                Utils.AddNode(supplier, "ID_SUPPLIER", dri.Id);
                Utils.AddNode(supplier, "TEXT_SUPPLIER", dri.Text);
            }
            foreach (DataRowItem dri in ucProducer.Items)
            {
                XmlNode producer = Utils.AddNode(root, "PRODUCERS");
                Utils.AddNode(producer, "ID_PRODUCER", dri.Id);
                Utils.AddNode(producer, "TEXT_PRODUCER", dri.Text);
            }
            Utils.AddNode(root, "repFullType", rbFullRep.Checked ? "1" : "0");
            doc.Save(fileName);
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileName)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.SelectSingleNode("/XML");
            XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID_CONTRACTOR");
                string text = Utils.GetString(node, "TEXT_CONTRACTOR");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucContractor.Items.Add(dri);
            }

            XmlNodeList stores = root.SelectNodes("STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID_STORE");
                string text = Utils.GetString(node, "TEXT_STORE");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucStore.Items.Add(dri);
            }

            XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID_GOODS");
                string text = Utils.GetString(node, "TEXT_GOODS");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucGoods.Items.Add(dri);
            }

            XmlNodeList suppliers = root.SelectNodes("SUPPLIERS");
            foreach (XmlNode node in suppliers)
            {
                long id = Utils.GetLong(node, "ID_SUPPLIER");
                string text = Utils.GetString(node, "TEXT_SUPPLIER");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucSupplier.Items.Add(dri);
            }

            XmlNodeList producers = root.SelectNodes("PRODUCERS");
            foreach (XmlNode node in producers)
            {
                long id = Utils.GetLong(node, "ID_PRODUCER");
                string text = Utils.GetString(node, "TEXT_PRODUCER");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucProducer.Items.Add(dri);
            }

            rbFullRep.Checked = Utils.GetBool(root, "repFullType");
            rbShortRep.Checked = !rbFullRep.Checked;
        }
    }
}