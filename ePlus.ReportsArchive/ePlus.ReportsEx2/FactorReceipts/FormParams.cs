using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using System.IO;

namespace FactorReceipts
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string fileName = Path.Combine(Utils.TempDir(), "FactorReceiprsSettings.xml");

		public FormParams()
		{
			InitializeComponent();
			ÑlearValues();
		}

		public void Print(string[] reportFiles)
		{
			if (((cbType.SelectedIndex == 1) && (ucPeriod.DateFrom > ucPeriod.DateTo)) || ((cbType.SelectedIndex == 2) && (dtpDateFr.Value > dtpDateTo.Value)))
			{
				MessageBox.Show("Äàòû âûáðàíû íåêîððåêòíî", "åÔàðìà", MessageBoxButtons.OK);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			if (cbType.SelectedIndex != 3)
			{
				Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			}

			ReportFormNew rep = new ReportFormNew();
			if (cbType.SelectedIndex == 0)
			{
				//Utils.AddNode(root, "DATE_CHEQUE", dtpDay.Value);    
				Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "HoursReport.rdlc");
				rep.LoadData("REP_FACTOR_RECEIPTS_HOUR", doc.InnerXml);
				rep.BindDataSource("FactorReceipts_DS_Table1", 0);
				rep.BindDataSource("FactorReceipts_DS_Table2", 1);
				rep.BindDataSource("FactorReceipts_DS_Table3", 2);
				rep.BindDataSource("FactorReceipts_DS_Table4", 3);
				rep.AddParameter("date", ucPeriod.DateFrText);
			}
			if (cbType.SelectedIndex == 1)
			{
				Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "DaysReport.rdlc");
				rep.LoadData("REP_FACTOR_RECEIPTS_DAYS", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsDays_DS_Table1", 0);
				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("period", (ucPeriod.DateTo.Subtract(ucPeriod.DateFrom).Days + 1).ToString());
			}
			if (cbType.SelectedIndex == 2)
			{
				Utils.AddNode(root, "DATE_FROM", dtpDateFr.Value);
				Utils.AddNode(root, "DATE_TO", dtpDateTo.Value);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MonthsReport.rdlc");
				rep.LoadData("REP_FACTOR_RECEIPTS_MONTHS", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsMonths_DS_Table1", 0);
				rep.AddParameter("date_fr", dtpDateFr.Text);
				rep.AddParameter("date_to", dtpDateTo.Text);
			}
			if (cbType.SelectedIndex == 3)
			{				
				Utils.AddNode(root, "DATE_FROM", new DateTime(yearDateTimePicker.Value.Year, 1, 1));
				Utils.AddNode(root, "DATE_TO", new DateTime(yearDateTimePicker.Value.Year, 12, 31));
				ucApt.AddItems(root, "ID_CONTRACTOR");
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MonthsReportIncrement.rdlc");
				rep.LoadData("REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsMonthsIncrement_DS_Table1", 0);
				rep.BindDataSource("FactorReceiptsMonthsIncrement_DS_Table2", 1);
				rep.AddParameter("user", ucApt.TextValues());
				rep.AddParameter("year", yearDateTimePicker.Value.Year.ToString());
			}

			if (cbType.SelectedIndex != 3)
			{
				rep.AddParameter("user", ucContractor.Text);
			}
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Ïîêàçàòåëè âûðó÷êè àïòåêè"; }
		}

		public override string GroupName
		{
			get
			{
				return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
			}
		}

		private void cbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			pDay.Visible = false;
			pPeriod.Visible = cbType.SelectedIndex == 1 || cbType.SelectedIndex == 0;
			pMonths.Visible = cbType.SelectedIndex == 2;
			yearPanel.Visible = cbType.SelectedIndex == 3;

			ucContractor.Enabled = cbType.SelectedIndex != 3;
			label6.Enabled = cbType.SelectedIndex != 3;
			ucApt.Enabled = cbType.SelectedIndex == 3;
		}

		private void FormParams_Load(object sender, EventArgs e)
		{
			this.ucContractor.Id = this.IdContractorDefault;
			if (!File.Exists(fileName)) return;
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			XmlNode root = doc.SelectSingleNode("/XML");
			cbType.SelectedIndex = Utils.GetInt(root, "TYPE");
			XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
			foreach (XmlNode node in contractors)
			{
				long id = Utils.GetLong(node, "ID");
				string text = Utils.GetString(node, "TEXT");
				ucContractor.Id = id;
			}
			dtpDay.Value = Utils.GetDate(root, "DTP_DAY");
			ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
			ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
			dtpDateFr.Value = Utils.GetDate(root, "DTP_DATE_FR");
			dtpDateTo.Value = Utils.GetDate(root, "DTP_DATE_TO");
		}

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "TYPE", cbType.SelectedIndex);
			Utils.AddNode(root, "DTP_DAY", dtpDay.Value);
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "DTP_DATE_FR", dtpDateFr.Value);
			Utils.AddNode(root, "DTP_DATE_TO", dtpDateTo.Value);
			doc.Save(fileName);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ÑlearValues();
		}

		private void ÑlearValues()
		{
			dtpDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);

			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

			dtpDateFr.Value = DateTime.Now;
			dtpDateFr.Value = dtpDateFr.Value.AddMonths(-1);

			yearDateTimePicker.Value = DateTime.Now;

			cbType.SelectedIndex = 0;

			ucApt.Items.Clear();
		}
	}
}
