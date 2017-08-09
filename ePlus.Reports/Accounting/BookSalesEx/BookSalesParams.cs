using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;

namespace BookSalesEx
{
	public partial class BookSalesParams : ExternalReportForm, IExternalReportFormMethods
	{
		public BookSalesParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public override void Execute(string connectionString, string folderPath)
		{
			base.Execute(connectionString, folderPath);
		}

		public void Print(string[] reportFiles)
		{
			if (ucContractors.Items.Count == 0)
			{
				MessageBox.Show("Ќе выбран(ы) продавец(цы)!", "е‘арма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (DataRowItem dr in ucContractors.Items)
                Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_BOOK_SALES", doc.InnerXml);
			rep.BindDataSource("BookSale_DS_Table0", 0);
			rep.BindDataSource("BookSale_DS_Table1", 1);
			rep.BindDataSource("BookSale_DS_Table2", 2);

			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("date_fr", ucPeriod.DateFrText),
				new ReportParameter("date_to", ucPeriod.DateToText),
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucContractors.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return " нига продаж"; }
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
        }
	}
}