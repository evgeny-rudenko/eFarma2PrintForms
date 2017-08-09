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

namespace GoodsMovementDocsEx
{
    public partial class GoodsMovementParams : ExternalReportForm, IExternalReportFormMethods
    {
        public GoodsMovementParams()
        {
            InitializeComponent();

			ucContractor.AllowSaveState = true;
			ucStore.AllowSaveState = true;
			ucGoods.AllowSaveState = true;
			ucSupplier.AllowSaveState = true;
			ucProducer.AllowSaveState = true;

			ClearValues();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);
			Utils.AddNode(root, "SORT", sortComboBox.SelectedIndex == 0 ? "1" : "0");
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");

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

            rep.LoadData("GOODS_MOVEMENT_DOCS", doc.InnerXml);
            rep.BindDataSource("GoodsMovementDocs_DS_Table1", 0);
            rep.BindDataSource("GoodsMovementDocs_DS_Table2", 1);
            rep.BindDataSource("GoodsMovementDocs_DS_Table3", 2);            

            rep.AddParameter("date_fr",ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("rep_type",rbFullRep.Checked? "1" : "0");

			rep.AddParameter("contractors", ucContractor.TextValues());
			rep.AddParameter("stores", ucStore.TextValues());
			rep.AddParameter("goods", ucGoods.TextValues());
			rep.AddParameter("suppliers", ucSupplier.TextValues());
			rep.AddParameter("producers", ucProducer.TextValues());

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Движение товара по документам"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			ucPeriod1.DateTo = DateTime.Now;
			ucPeriod1.DateFrom = DateTime.Now.AddDays(-13);

			ucContractor.Items.Clear();
			ucStore.Items.Clear();
			ucGoods.Items.Clear();
			ucSupplier.Items.Clear();
			ucProducer.Items.Clear();

			rbFullRep.Checked = true;
			sortComboBox.SelectedIndex = 0;
		}
    }
}