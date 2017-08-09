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

namespace InvoiceBookEx
{
	public partial class InvoiceBookParams : ExternalReportForm, IExternalReportFormMethods
	{
		public InvoiceBookParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "DETAIL", chbDetail.Checked ? 1 : 0);
			Utils.AddNode(root, "SHOW_REMAIN", remainCheckBox.Checked ? 1 : 0);

			storesPluginMultiSelect.AddItems(root, "STORE");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_INVOICE_BOOK", doc.InnerXml);

			rep.AddParameter("store", storesPluginMultiSelect.TextValues());
			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("detail", chbDetail.Checked ? "1" : "0");

			rep.BindDataSource("Invoice_Book_DS_Table3", 0);
			rep.BindDataSource("Invoice_Book_DS_Table", 1);			
			rep.BindDataSource("Invoice_Book_DS_Table1", 2);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Завозная книга"; }
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			storesPluginMultiSelect.Clear();
			chbDetail.Checked = false;
			remainCheckBox.Checked = false;
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
	}
}