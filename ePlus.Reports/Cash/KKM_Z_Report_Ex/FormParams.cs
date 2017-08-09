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
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace KKM_Z_Report_Ex
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
    private string settingsFilePath;

		private DataTable subReportTable;

		public FormParams()
		{
      System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
      settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			ucPeriod1.AddValues(root);
			Utils.AddNode(root, "DETAIL", groupByTypeCheckBox.Checked ? "1" : "0");

			if (ucMetaPluginSelect1.Id != 0)
				Utils.AddNode(root, "ID_CASH_REGISTER", ucMetaPluginSelect1.Id);
			if (ucMetaPluginSelect2.Id != 0)
				Utils.AddNode(root, "ID_CONTRACTOR", ucMetaPluginSelect2.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
				reportTypeComboBox.SelectedIndex == 0 ? "KKM_Z_Report_Ex.rdlc" : "KKM_Z_Report_payment_types.rdlc");
			
			rep.LoadData("DBO.REPEX_Z_REPORT", doc.InnerXml);

			rep.BindDataSource("KKM_Z_ReportDS_Table", 0);

			if (groupByTypeCheckBox.Checked)
			{
				rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
				subReportTable = rep.DataSource.Tables[1];
			}

			rep.AddParameter("date_fr", ucPeriod1.DateFrText);
			rep.AddParameter("date_to", ucPeriod1.DateToText);
			rep.AddParameter("detail", checkBox_detail.Checked ? "0" : "1");

			if (reportTypeComboBox.SelectedIndex != 0)
			{
				rep.AddParameter("sub", groupByTypeCheckBox.Checked ? "1" : "0");
			}

			rep.ExecuteReport(this);
		}

		private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
		{
			if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Z_Sub")
			{
				e.DataSources.Add(new ReportDataSource("KKM_Z_ReportDS_Table1", subReportTable));
			}
		}

		public string ReportName
		{
      get { return "Z-îò÷¸ò çà ïåðèîä"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ÑlearValues();
		}

		private void ÑlearValues()
		{
			ucPeriod1.DateTo = DateTime.Now;
			ucPeriod1.DateFrom = ucPeriod1.DateTo.AddDays(-13);

			ucMetaPluginSelect1.SetId(0);
			ucMetaPluginSelect2.SetId(0);

			checkBox_detail.Checked = false;
			groupByTypeCheckBox.Checked = false;
			reportTypeComboBox.SelectedIndex = 0;
		}

		private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			groupByTypeCheckBox.Enabled = reportTypeComboBox.SelectedIndex == 1;
		}

    private void LoadSettings()
    {
      ÑlearValues();
      if (!File.Exists(settingsFilePath))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(settingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
        return;

      ucPeriod1.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod1.DateTo = Utils.GetDate(root, "DATE_TO");

      reportTypeComboBox.SelectedIndex = Utils.GetInt(root, "REPORT_RYPE");
      
      XmlNodeList cash_r = root.SelectNodes("CASH_REGISTER");
      foreach (XmlNode node in cash_r)
      {
        ucMetaPluginSelect1.SetId(Utils.GetLong(node, "ID"));
        ucMetaPluginSelect1.Text = Utils.GetString(node, "TEXT");
      }

      XmlNodeList contractor = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractor)
      {
        ucMetaPluginSelect2.SetId(Utils.GetLong(node, "ID"));
        ucMetaPluginSelect2.Text = Utils.GetString(node, "TEXT");
      }

      checkBox_detail.Checked = Utils.GetBool(root, "SHORT");
      groupByTypeCheckBox.Checked = Utils.GetBool(root, "GROUP_BY_TYPE");
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);

      Utils.AddNode(root, "REPORT_RYPE", reportTypeComboBox.SelectedIndex);

      if (ucMetaPluginSelect1.Id != null)
      {
        XmlNode cash_r = Utils.AddNode(root, "CASH_REGISTER");
        Utils.AddNode(cash_r, "ID", ucMetaPluginSelect1.Id);
        Utils.AddNode(cash_r, "TEXT", ucMetaPluginSelect1.Text);
      }

      if (ucMetaPluginSelect2.Id != null)
      {
        XmlNode cash_r = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(cash_r, "ID", ucMetaPluginSelect2.Id);
        Utils.AddNode(cash_r, "TEXT", ucMetaPluginSelect2.Text);
      }

      Utils.AddNode(root, "SHORT", checkBox_detail.Checked);
      Utils.AddNode(root, "GROUP_BY_TYPE", groupByTypeCheckBox.Checked);

      doc.Save(settingsFilePath);
    }

    private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void FormParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }
	}
}