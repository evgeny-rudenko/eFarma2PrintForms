using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace BookPurchasesEx
{
	public partial class BookPurchasesParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string settingsFilePath = Path.Combine(Utils.TempDir(), "BookPurchasesSettings.xml");
		
		private const string OPERATOR = "OPERATOR";

		public BookPurchasesParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			ucPharmacies.AddItems(root, "ID_CONTRACTOR");
			if (supplierOperatorComboBox.SelectedIndex != 0)
			{
				ucSuppliers.AddItems(root, "ID_SUPPLIER");
			}

			if (supplierOperatorComboBox.SelectedIndex != 2)
			{
				Utils.AddNode(root, "INC", true);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_BOOK_PURCHASES", doc.InnerXml);
			rep.BindDataSource("BookPurchases_DS_Table0", 0);
			rep.BindDataSource("BookPurchases_DS_Table1", 1);
			rep.BindDataSource("BookPurchases_DS_Table2", 2);

			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("date_fr", ucPeriod.DateFrText),
				new ReportParameter("date_to", ucPeriod.DateToText)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			supplierOperatorComboBox.SelectedIndex = 0;
			ucPharmacies.Items.Clear();
			ucSuppliers.Items.Clear();			
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Книга покупок"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void supplierOperatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ucSuppliers.Enabled = supplierOperatorComboBox.SelectedIndex != 0;
		}

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			supplierOperatorComboBox.SelectedIndex = Utils.GetInt(root, OPERATOR);
		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root;			

			if (File.Exists(settingsFilePath))
			{
				doc.Load(settingsFilePath);
				root = doc.SelectSingleNode("//XML");
				root.RemoveAll();
			}
			else
			{
				root = Utils.AddNode(doc, "XML");
			}			

			Utils.AddNode(root, OPERATOR, supplierOperatorComboBox.SelectedIndex);

			doc.Save(settingsFilePath);
		}

		private void BookPurchasesParams_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void BookPurchasesParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}



	}
}

