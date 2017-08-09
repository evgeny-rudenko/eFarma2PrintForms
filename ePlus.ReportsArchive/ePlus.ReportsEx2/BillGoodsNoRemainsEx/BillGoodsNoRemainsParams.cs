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

namespace BillGoodsNoRemainsEx
{
    public partial class BillGoodsNoRemainsParams : ExternalReportForm, IExternalReportFormMethods
    {		
        public BillGoodsNoRemainsParams()
        {
            InitializeComponent();

			ucSellers.AllowSaveState = true;
			ucBuyers.AllowSaveState = true;

			ClearValues();
        }

        public void Print(string[] reportFiles)
        {            
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);

			string reportType = string.Empty; ;

			if (radioButton1.Checked)
				reportType = "1";
			if (radioButton2.Checked)
				reportType = "2";
			if (radioButton3.Checked)
				reportType = "3";

			Utils.AddNode(root, "REPORT_TYPE", reportType);

			ucSellers.AddItems(root, "ID_SELLER");
			ucBuyers.AddItems(root, "ID_BUYER");
			foreach (DataRowItem item in ucBills.Items)
			{
				Utils.AddNode(root, "ID_BILL_GLOBAL", item.Guid);
			}

            ReportFormNew rep = new ReportFormNew();

			rep.LoadData("REPEX_BILL_GOODS_NO_REMAINS", doc.InnerXml);

			if (radioButton1.Checked)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "DeficitList.rdlc");
				rep.BindDataSource("DeficitList_DS_Table0", 0);
			}
			if (radioButton2.Checked)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "DeficitListSummary.rdlc");
				rep.BindDataSource("DeficitListSummary_DS_Table0", 0);
			}
			if (radioButton3.Checked)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "DeficitListBySupplier.rdlc");
				rep.BindDataSource("DeficitListBySupplier_DS_Table0", 0);
			}

            rep.AddParameter("period", "с " + period.DateFrText + " по " + period.DateToText);
            rep.AddParameter("seller", ucSellers.TextValues());
			rep.AddParameter("buyer", ucBuyers.TextValues());
			rep.AddParameter("bill", ucBills.TextValues());

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Товар по счёту, отсутствующий на остатках"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			period.SetPeriodMonth();

			ucSellers.Items.Clear();
			ucBuyers.Items.Clear();
			ucBills.Items.Clear();
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
					ucSellers.AddItem(id);
			}
		}

		private void BillGoodsNoRemainsParams_Load(object sender, EventArgs e)
		{
			LoadDefaultContractor();
		}
    }
}