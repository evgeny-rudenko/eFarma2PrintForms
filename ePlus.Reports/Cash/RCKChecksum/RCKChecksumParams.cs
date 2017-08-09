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
using System.IO;

namespace RCSChecksum
{
	public partial class RCKChecksumParams : ExternalReportForm, IExternalReportFormMethods
	{
        private string settingsFilePath;
		public RCKChecksumParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			int delta = 0;
			if (!int.TryParse(txtBxDelta.Text, out delta) || delta <= 0)
			{
				MessageBox.Show("ƒельта - целое число больше нул€", "е‘арма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			decimal lm;
			if (!decimal.TryParse(lmTextBox.Text, out lm) || lm < 0.0m || lm > 1000000.0m)
			{
				MessageBox.Show("Ќижн€€ граница суммы чека - число в дипазоне 0,00 - 1000000,00", "е‘арма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", periodPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
			Utils.AddNode(root, "DELTA", txtBxDelta.Text.Trim());
			Utils.AddNode(root, "LM", lm);
			Utils.AddNode(root, "CARD", cardCheckBox.Checked ? "1" : "0");

			storesPluginMultiSelect.AddItems(root, "STORE");
            cashPluginMultiSelect.AddItems(root, "CASH");

			ReportFormNew rep = new ReportFormNew();						
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_CHECKSUM", doc.InnerXml);
			rep.BindDataSource("RCKChecksum_DS_Table1", 0);
			rep.BindDataSource("RCKChecksum_DS_Table2", 1);

			ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("DATE_FROM", periodPeriod.DateFrom.ToShortDateString()),
				new ReportParameter("DATE_TO", periodPeriod.DateTo.ToShortDateString()),
				new ReportParameter("STORES", string.IsNullOrEmpty(storesPluginMultiSelect.ToCommaDelimetedStringList()) ? "¬се склады" : storesPluginMultiSelect.ToCommaDelimetedStringList()),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			periodPeriod.DateTo = DateTime.Now;
			periodPeriod.DateFrom = DateTime.Now.AddDays(-10);

			lmTextBox.Text = "0,01";
			txtBxDelta.Text = "100";
			cardCheckBox.Checked = false;

			storesPluginMultiSelect.Items.Clear();
            cashPluginMultiSelect.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "—правка о продаже по сумме чека"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}

        private void RCKChecksumParams_Load(object sender, EventArgs e)
        {
            //storesPluginMultiSelect.AllowSaveState = true;
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            ClearValues();
            LoadSettings();
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

            periodPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            periodPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            XmlNodeList store = root.SelectNodes("STORE");
            foreach (XmlNode node in store)
            {
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                storesPluginMultiSelect.AddRowItem(new DataRowItem(0, guid, String.Empty, text));
            }

            XmlNodeList cash = root.SelectNodes("CASH");
            foreach (XmlNode node in cash)
            {
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                cashPluginMultiSelect.AddRowItem(new DataRowItem(0, guid, String.Empty, text));
            }

            txtBxDelta.Text = Utils.GetString(root, "DELTA");
            lmTextBox.Text = Utils.GetString(root, "LM");
            cardCheckBox.Checked = Utils.GetBool(root, "CARD");
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

            Utils.AddNode(root, "DATE_FROM", periodPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);

            foreach (DataRowItem dri in storesPluginMultiSelect.Items)
            {
                XmlNode node = Utils.AddNode(root, "STORE");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in cashPluginMultiSelect.Items)
            {
                XmlNode node = Utils.AddNode(root, "CASH");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "TEXT", dri.Text);
            }


            Utils.AddNode(root, "DELTA", txtBxDelta.Text);
            Utils.AddNode(root, "LM", lmTextBox.Text);
            Utils.AddNode(root, "CARD", cardCheckBox.Checked);

            doc.Save(settingsFilePath);
        }

        private void RCKChecksumParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

	}
}