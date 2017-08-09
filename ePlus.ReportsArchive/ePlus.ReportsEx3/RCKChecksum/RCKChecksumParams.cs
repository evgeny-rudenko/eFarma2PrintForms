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

namespace RCKChecksum
{
	public partial class RCKChecksumParams : ExternalReportForm, IExternalReportFormMethods
	{
		public RCKChecksumParams()
		{
			InitializeComponent();
			storesPluginMultiSelect.AllowSaveState = true;
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			int delta = 0;
			if (!int.TryParse(txtBxDelta.Text, out delta) || delta <= 0)
			{
				MessageBox.Show("Дельта - целое число больше нуля", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", periodPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
			Utils.AddNode(root, "DELTA", txtBxDelta.Text.Trim());

			storesPluginMultiSelect.AddItems(root, "STORE");

			ReportFormNew rep = new ReportFormNew();						
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_CHECKSUM", doc.InnerXml);
			rep.BindDataSource("RCKChecksum_DS_Table1", 0);
			rep.BindDataSource("RCKChecksum_DS_Table2", 1);

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("DATE_FROM", periodPeriod.DateFrom.ToShortDateString()),
				new ReportParameter("DATE_TO", periodPeriod.DateTo.ToShortDateString()),
				new ReportParameter("STORES", string.IsNullOrEmpty(storesPluginMultiSelect.ToCommaDelimetedStringList()) ? "Все склады" : storesPluginMultiSelect.ToCommaDelimetedStringList())
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			periodPeriod.DateTo = DateTime.Now;
			periodPeriod.DateFrom = DateTime.Now.AddDays(-10);

			storesPluginMultiSelect.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Справка о продаже по сумме чека"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}
	}
}