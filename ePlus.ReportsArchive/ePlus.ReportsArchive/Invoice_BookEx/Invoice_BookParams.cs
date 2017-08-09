using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;

namespace Invoice_BookEx
{
	public partial class Invoice_BookParams : ExternalReportForm, IExternalReportFormMethods
	{
		public Invoice_BookParams()
		{
			InitializeComponent();
			SetDefaultValues();			
		}

		public void Print(string[] reportFiles)
		{
			if (ucStore.Text != string.Empty)
			{
				XmlDocument doc = new XmlDocument();
				XmlNode root = Utils.AddNode(doc, "XML");
				Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				Utils.AddNode(root, "ID_STORE", ucStore.Id);
				Utils.AddNode(root, "DETAIL", chbDetail.Checked ? 1 : 0);

				ReportFormNew rep = new ReportFormNew();
				rep.ReportPath = reportFiles[0];
				rep.LoadData("REP_INVOICE_BOOK_EX", doc.InnerXml);

				rep.AddParameter("contractor", "");
				rep.AddParameter("store", ucStore.Text);
				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				if (chbDetail.Checked)
					rep.AddParameter("detail", "детализация");
				else rep.AddParameter("detail", "без детализации");

				rep.BindDataSource("Invoice_Book_DS_Table", 0);
				rep.BindDataSource("Invoice_Book_DS_Table1", 1);
				rep.BindDataSource("Invoice_Book_DS_Table2", 2);
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("Выберите склад");
		}

		public string ReportName
		{
			get { return "Завозная книга общая"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = DateTime.Now;
				ucPeriod.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
			}
			chbDetail.Checked = false;
		}

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
            }
        }
	}
}