using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;
using System.Data.SqlClient;

namespace MatVetEx
{
    public partial class MatVedParams : ExternalReportForm, IExternalReportFormMethods
    {
        public MatVedParams()
        {
            InitializeComponent();

			ucProducers.AllowSaveState = true;
			ucStores.AllowSaveState = true;
			ucGoodsKind.AllowSaveState = true;
			ucGoods.AllowSaveState = true;			

			ClearValues();

        }

        public void Print(string[] reportFiles)
        {            
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "SHOW_SERIES", showSeriesCheckBox.Checked ? 1 : 0);
            Utils.AddNode(root, "IMPORTANT_ONLY", importantOnlyCheckBox.Checked ? 1 : 0);

			ucSuppliers.AddItems(root, "ID_SUPPLIER");
			ucProducers.AddItems(root, "ID_PRODUCER");
			ucStores.AddItems(root, "ID_STORE");
			ucGoodsKind.AddItems(root, "ID_GOODS_KIND");
			ucGoods.AddItems(root, "ID_GOODS");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_MAT_VED", doc.InnerXml);
            rep.BindDataSource("MatVed_DS_Table0", 0);
            rep.BindDataSource("MatVed_DS_Table1", 1);

            rep.AddParameter("DATE_FROM", period.DateFrText);
            rep.AddParameter("DATE_TO", period.DateToText);
            rep.AddParameter("ALL_STORE", ucStores.Items.Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_SUPPLIER", ucSuppliers.Items.Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_PRODUCER", ucProducers.Items.Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_GOODS_KIND", ucGoodsKind.Items.Count > 0 ? "0" : "1");

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Материальная ведомость"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
        }

		private void ClearValues()
		{
			period.DateTo = DateTime.Now;
			period.DateFrom = DateTime.Now.AddDays(-13);

			showSeriesCheckBox.Checked = false;
			importantOnlyCheckBox.Checked = false;

			ucSuppliers.Items.Clear();
			ucProducers.Items.Clear();
			ucStores.Items.Clear();
			ucGoodsKind.Items.Clear();
			ucGoods.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
			LoadDefaultContractor();
		}

		private void LoadDefaultContractor()
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT CAST(VALUE AS BIGINT) FROM SYS_OPTION WHERE CODE = 'CONTRACTOR_SUPPLIER_LBO'", con);
				con.Open();
				long id = (long) command.ExecuteScalar();

				if (id != 0)
					ucSuppliers.AddItem(id);
			}
		}

		private void MatVedParams_Load(object sender, EventArgs e)
		{
			LoadDefaultContractor();
		}
    }
}