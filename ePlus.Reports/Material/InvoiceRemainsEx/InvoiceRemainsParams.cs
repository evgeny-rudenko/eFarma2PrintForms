using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace InvoiceRemainsEx
{
	public partial class InvoiceRemainsParams : ExternalReportForm, IExternalReportFormMethods
	{
		private Dictionary<string, List<DataRowItem>> lists = new Dictionary<string, List<DataRowItem>>();

		public InvoiceRemainsParams()
		{
			InitializeComponent();	
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			ucPeriod1.AddValues(root);

			if (ucPluginMultiSelect1.Enabled)
			{
				switch (ucPluginMultiSelect1.Mnemocode)
				{
					case "":
						break;
					case "INVOICE":
						ucPluginMultiSelect1.AddItems(root, "ID_INVOICE");
						break;
					case "CONTRACTOR":
						ucPluginMultiSelect1.AddItems(root, "ID_SUPPLIER");
						break;
				}
			}
						
			ReportFormNew rep = new ReportFormNew();						
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_INVOICE_REMAINS", doc.InnerXml);
			rep.BindDataSource("Invoice_rem_DS_Table", 0);

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("date_fr", ucPeriod1.DateFrText),
				new ReportParameter("date_to", ucPeriod1.DateToText),
				new ReportParameter("all_goods", "0")
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod1.SetPeriodMonth();
			filterComboBox.SelectedIndex = 0;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Остатки по приходу"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}		

		private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ucPluginMultiSelect1.Enabled)
			{
				if (!lists.ContainsKey(ucPluginMultiSelect1.Mnemocode))
					lists.Add(ucPluginMultiSelect1.Mnemocode, new List<DataRowItem>());
				else
					lists[ucPluginMultiSelect1.Mnemocode].Clear();
				foreach (DataRowItem item in ucPluginMultiSelect1.Items)
				{
					lists[ucPluginMultiSelect1.Mnemocode].Add(item);
				}
			}

			List<DataRowItem> currentList = null;
			switch (filterComboBox.SelectedIndex)
			{
				case 0:
					ucPluginMultiSelect1.Enabled = true;
					ucPluginMultiSelect1.SetPluginControl("INVOICE");
					ucPluginMultiSelect1.Caption = "Приходные документы";
					ucPluginMultiSelect1.Clear();
					if (!lists.ContainsKey("INVOICE"))
						lists.Add("INVOICE", new List<DataRowItem>());
					currentList = lists["INVOICE"];
					break;
				case 1:
					ucPluginMultiSelect1.Enabled = true;
					ucPluginMultiSelect1.SetPluginControl("CONTRACTOR");
					ucPluginMultiSelect1.Caption = "Поставщики";
					ucPluginMultiSelect1.Clear();
					if (!lists.ContainsKey("CONTRACTOR"))
						lists.Add("CONTRACTOR", new List<DataRowItem>());
					currentList = lists["CONTRACTOR"];
					break;
				case 2:
					ucPluginMultiSelect1.Enabled = false;
					ucPluginMultiSelect1.Mnemocode = string.Empty;
					ucPluginMultiSelect1.Caption = string.Empty;
					ucPluginMultiSelect1.Clear();
					currentList = null;
					break;
			}

			ucPeriod1.Enabled = currentList != lists["INVOICE"];

			if (currentList == null) 
				return;

			foreach (DataRowItem item in currentList)
			{
				ucPluginMultiSelect1.AddRowItem(item);
			}
		}
	}
}