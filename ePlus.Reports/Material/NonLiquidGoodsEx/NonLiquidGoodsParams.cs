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

namespace RCSNonLiquidGoods
{
	public partial class NonLiquidGoodsParams : ExternalReportForm, IExternalReportFormMethods
	{	
        public NonLiquidGoodsParams()
		{
			InitializeComponent();
		}

		private class Doc
		{
			private string code;
			private string name;

			public string Code
			{
				get { return code; }
				set { code = value; }
			}

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public Doc(string code, string name)
			{
				this.code = code;
				this.name = name;
			}
			public override string ToString()
			{
				return name;
			}
		}



		public void Print(string[] reportFiles)
		{
			int quantity = 0;
			int percent = 0;

			if (reportTypeComboBox.SelectedIndex > 0)
			{
				if (!int.TryParse(timeTextBox.Text, out quantity) || quantity < 0)
				{
					MessageBox.Show("Значение в поле Время жизни поставки должно быть целым положительным числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (!int.TryParse(percentTextBox.Text, out percent) || percent < 0)
				{
					MessageBox.Show("Значение в поле Уровень текущего остатка должно быть целым положительным числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", periodPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
			Utils.AddNode(root, "SHOW_LOTS", showLotsCheckBox.Checked);
			storesPluginMultiSelect.AddItems(root, "STORE");

			if (reportTypeComboBox.SelectedIndex > 0)
			{
				Utils.AddNode(root, "TIME", timeTextBox.Text);
				Utils.AddNode(root, "PERCENT", percentTextBox.Text);
				Utils.AddNode(root, "GROUPS", groupCheckBox.Checked);

				foreach (Doc d in docsCheckedListBox.CheckedItems)
				{
					Utils.AddNode(root, "DOC", d.Code);
				}
			}

			ReportFormNew rep = new ReportFormNew();

			if (reportTypeComboBox.SelectedIndex == 0)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "NonLiquidGoods.rdlc");
				rep.LoadData("REPEX_NON_LIQUID_GOODS", doc.InnerXml);
				rep.BindDataSource("NonLiquidGoods_DS_Table1", 0);
				rep.BindDataSource("NonLiquidGoods_DS_Table2", 1);
			}
			else if (reportTypeComboBox.SelectedIndex == 1)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "NonLiquidGoods_new.rdlc");
				rep.LoadData("REPEX_NON_LIQUID_GOODS_PERIOD", doc.InnerXml);
				rep.BindDataSource("NonLiquidGoods_DS_Table3", 0);
			}
			else if (reportTypeComboBox.SelectedIndex == 2)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "NonLiquidGoods_new.rdlc");
				rep.LoadData("REPEX_NON_LIQUID_GOODS_NO_PERIOD", doc.InnerXml);
				rep.BindDataSource("NonLiquidGoods_DS_Table3", 0);
			}

			rep.AddParameter("DATE_FROM", periodPeriod.DateFrText);
			rep.AddParameter("DATE_TO", periodPeriod.DateToText);
			rep.AddParameter("STORES", storesPluginMultiSelect.TextValues());

			if (reportTypeComboBox.SelectedIndex > 0)
			{
				rep.AddParameter("time", timeTextBox.Text);
				rep.AddParameter("percent", percentTextBox.Text);
				rep.AddParameter("period", reportTypeComboBox.SelectedIndex == 1 ? "1" : "0");
			}
			else
			{
				rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
			}
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			periodPeriod.DateTo = DateTime.Now;
			periodPeriod.DateFrom = DateTime.Now.AddDays(-13);

			reportTypeComboBox.SelectedIndex = 0;

			chbGoodCode.Checked = false;
			showLotsCheckBox.Checked = false;
			groupCheckBox.Checked = false;
			storesPluginMultiSelect.Items.Clear();

			timeTextBox.Text = "75";
			percentTextBox.Text = "50";

			for (int i = 0; i < docsCheckedListBox.Items.Count; i++)
			{
				docsCheckedListBox.SetItemChecked(i, true);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по неликвидным товарам"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			timeLabel.Enabled = reportTypeComboBox.SelectedIndex > 0;
			timeTextBox.Enabled = reportTypeComboBox.SelectedIndex > 0;

			percentLabel.Enabled = reportTypeComboBox.SelectedIndex > 0;
			percentTextBox.Enabled = reportTypeComboBox.SelectedIndex > 0;

			groupCheckBox.Enabled = reportTypeComboBox.SelectedIndex > 0;
			showLotsCheckBox.Enabled = reportTypeComboBox.SelectedIndex == 0;
			chbGoodCode.Enabled = reportTypeComboBox.SelectedIndex == 0;

			chargeLabel.Enabled = reportTypeComboBox.SelectedIndex > 0;
			docsCheckedListBox.Enabled = reportTypeComboBox.SelectedIndex == 1;
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "NonLiquidGoodsReportSettings.xml");
		private const string REPORT = "REPORT";
		private const string SHOW_LOTS = "SHOW_LOTS";
		private const string SHOW_CODE = "SHOW_CODE";
		private const string GROUPS = "GROUPS";
		private const string TIME = "TIME";
		private const string PERCENT = "PERCENT";
		private const string ITEM0 = "ITEM0";
		private const string ITEM1 = "ITEM1";
		private const string ITEM2 = "ITEM2";
		private const string ITEM3 = "ITEM3";
		private const string ITEM4 = "ITEM4";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			reportTypeComboBox.SelectedIndex = Utils.GetInt(root, REPORT);
			timeTextBox.Text = Utils.GetString(root, TIME);
			percentTextBox.Text = Utils.GetString(root, PERCENT);
			showLotsCheckBox.Checked = Utils.GetBool(root, SHOW_LOTS);
			chbGoodCode.Checked = Utils.GetBool(root, SHOW_CODE);
			groupCheckBox.Checked = Utils.GetBool(root, GROUPS);

			docsCheckedListBox.SetItemChecked(0, Utils.GetBool(root, ITEM0));
			docsCheckedListBox.SetItemChecked(1, Utils.GetBool(root, ITEM1));
			docsCheckedListBox.SetItemChecked(2, Utils.GetBool(root, ITEM2));
			docsCheckedListBox.SetItemChecked(3, Utils.GetBool(root, ITEM3));
			docsCheckedListBox.SetItemChecked(4, Utils.GetBool(root, ITEM4));
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

			Utils.AddNode(root, REPORT, reportTypeComboBox.SelectedIndex);
			Utils.AddNode(root, TIME, timeTextBox.Text);
			Utils.AddNode(root, PERCENT, percentTextBox.Text);
			Utils.AddNode(root, SHOW_LOTS, showLotsCheckBox.Checked);
			Utils.AddNode(root, SHOW_CODE, chbGoodCode.Checked);
			Utils.AddNode(root, GROUPS, groupCheckBox.Checked);

			Utils.AddNode(root, ITEM0, docsCheckedListBox.GetItemChecked(0));
			Utils.AddNode(root, ITEM1, docsCheckedListBox.GetItemChecked(1));
			Utils.AddNode(root, ITEM2, docsCheckedListBox.GetItemChecked(2));
			Utils.AddNode(root, ITEM3, docsCheckedListBox.GetItemChecked(3));
			Utils.AddNode(root, ITEM4, docsCheckedListBox.GetItemChecked(4));

			doc.Save(settingsFilePath);
		}

		private void NonLiquidGoodsParams_Load(object sender, EventArgs e)
		{

            Doc[] docs = new Doc[] {
				new Doc("CHEQUE", "Чеки"),
				new Doc("INVOICE_OUT", "Расходные накладные"),
				new Doc("MOVE", "Перемещение"),
				new Doc("DED", "Акты списания"),
				new Doc("DIS", "Акты разукомплектации")
			};

            docsCheckedListBox.Items.AddRange(docs);

            storesPluginMultiSelect.AllowSaveState = true;

			ClearValues();
			LoadSettings();
		}

		private void NonLiquidGoodsParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}