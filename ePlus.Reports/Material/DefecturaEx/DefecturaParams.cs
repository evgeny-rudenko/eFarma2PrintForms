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
using System.Data.SqlClient;
using System.IO;

namespace DefecturaEx
{
	public partial class DefecturaParams : ExternalReportForm, IExternalReportFormMethods
	{
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

		public DefecturaParams()
		{
			InitializeComponent();

			Doc[] docs = new Doc[] {
				new Doc("CHEQUE", "Чеки"),
				new Doc("INVOICE_OUT", "Расходные накладные"),
				new Doc("MOVE", "Перемещение")
			};

			docsCheckedListBox.Items.AddRange(docs);
		}

		public void Print(string[] reportFiles)
		{
			int min = 0;

			if (!int.TryParse(minValueTextBox.Text, out min))
			{
				MessageBox.Show("Значение в поле Минимальный остаток должно быть целым числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument param = new XmlDocument();
			XmlNode root = Utils.AddNode(param, "XML");

			Utils.AddNode(root, "DATE_FROM", fromDateTimePicker.Value);
			Utils.AddNode(root, "DATE_TO", toDateTimePicker.Value);

			Utils.AddNode(root, "MIN_VALUE", minValueTextBox.Text);
			Utils.AddNode(root, "SORT", sortComboBox.SelectedIndex);
						
			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucStores.AddItems(root, "ID_STORE");
			ucGoods.AddItems(root, "ID_GOODS");

			Utils.AddNode(root, "IS_OA", oaCheckBox.Checked);
			Utils.AddNode(root, "IS_GROUPS", groupCheckBox.Checked);
			Utils.AddNode(root, "IS_RESERVE", reserveCheckBox.Checked);			
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "1" : "0");

			Utils.AddNode(root, "IS_ES", esCheckBox.Enabled ? esCheckBox.Checked : false);

			foreach (Doc doc in docsCheckedListBox.CheckedItems)
			{
				Utils.AddNode(root, "DOC", doc.Code);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("DBO.REPEX_DEFECTURA", param.InnerXml);
			rep.BindDataSource("Defectura_Table0", 0);

			rep.AddParameter("date_from", fromDateTimePicker.Value.ToString("g"));
			rep.AddParameter("date_to", toDateTimePicker.Value.ToString("g"));
			rep.AddParameter("stores", ucStores.TextValues());
			rep.AddParameter("goods", ucGoods.TextValues());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
			rep.AddParameter("contr", ucContractors.TextValues());

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Дефектура"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			toDateTimePicker.Value = DateTime.Now;
			fromDateTimePicker.Value = new DateTime(toDateTimePicker.Value.Year, toDateTimePicker.Value.Month, toDateTimePicker.Value.Day);

			minValueTextBox.Text = "0";
			sortComboBox.SelectedIndex = 0;

			ucGoods.Items.Clear();
			ucContractors.Items.Clear();
			ucStores.Items.Clear();

			oaCheckBox.Checked = false;
			groupCheckBox.Checked = false;
			reserveCheckBox.Checked = false;
			esCheckBox.Checked = false;
			chbGoodCode.Checked = false;

			for (int i = 0; i < docsCheckedListBox.Items.Count; i++)
			{
				docsCheckedListBox.SetItemChecked(i, true);
			}
			/*
			long? storeId = null;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT TOP 1 ID_STORE FROM STORE WHERE ID_CONTRACTOR = (SELECT C.ID_CONTRACTOR FROM REPLICATION_CONFIG AS RC INNER JOIN CONTRACTOR AS C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1) ORDER BY MNEMOCODE", con);
				con.Open();
				storeId = (long?) command.ExecuteScalar();
			}

			if (storeId.HasValue)
				ucStore.SetId(storeId.Value);
			*/
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (toDateTimePicker.Value < fromDateTimePicker.Value)
			{
				fromDateTimePicker.Value = toDateTimePicker.Value.AddHours(-1);
			}
		}

		private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (fromDateTimePicker.Value > toDateTimePicker.Value)
			{
				toDateTimePicker.Value = fromDateTimePicker.Value.AddHours(1);
			}
		}

		private void DefecturaParams_Load(object sender, EventArgs e)
		{
			ClearValues();
			LoadSettings();
		}

		private void groupCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (groupCheckBox.Checked)
			{
				minValueLabel.Enabled = false;
				minValueTextBox.Enabled = false;
				sortLabel.Enabled = false;
				sortComboBox.Enabled = false;
				esCheckBox.Enabled = false;
			}
			else
			{
				minValueLabel.Enabled = true;
				minValueTextBox.Enabled = true;
				sortLabel.Enabled = true;
				sortComboBox.Enabled = true;
				esCheckBox.Enabled = true;
			}
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "DefecturaReportExSettings.xml");
		private const string MIN_VALUE = "MIN_VALUE";
		private const string SORT = "SORT";
		private const string OA = "OA";
		private const string GROUP = "GROUP";
		private const string RESERVE = "RESERVE";
		private const string ES = "ES";
		private const string SHOW_CODE = "SHOW_CODE";
		private const string ITEM0 = "ITEM0";
		private const string ITEM1 = "ITEM1";
		private const string ITEM2 = "ITEM2";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			minValueTextBox.Text = Utils.GetString(root, MIN_VALUE);
			sortComboBox.SelectedIndex = Utils.GetInt(root, SORT);
			oaCheckBox.Checked = Utils.GetBool(root, OA);
			groupCheckBox.Checked = Utils.GetBool(root, GROUP);
			reserveCheckBox.Checked = Utils.GetBool(root, RESERVE);
			esCheckBox.Checked = Utils.GetBool(root, ES);
			chbGoodCode.Checked = Utils.GetBool(root, SHOW_CODE);
			docsCheckedListBox.SetItemChecked(0, Utils.GetBool(root, ITEM0));
			docsCheckedListBox.SetItemChecked(1, Utils.GetBool(root, ITEM1));
			docsCheckedListBox.SetItemChecked(2, Utils.GetBool(root, ITEM2));
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

			Utils.AddNode(root, MIN_VALUE, minValueTextBox.Text);
			Utils.AddNode(root, SORT, sortComboBox.SelectedIndex);
			Utils.AddNode(root, OA, oaCheckBox.Checked);
			Utils.AddNode(root, GROUP, groupCheckBox.Checked);
			Utils.AddNode(root, RESERVE, reserveCheckBox.Checked);
			Utils.AddNode(root, ES, esCheckBox.Checked);
			Utils.AddNode(root, SHOW_CODE, chbGoodCode.Checked);			
			Utils.AddNode(root, ITEM0, docsCheckedListBox.GetItemChecked(0));
			Utils.AddNode(root, ITEM1, docsCheckedListBox.GetItemChecked(1));
			Utils.AddNode(root, ITEM2, docsCheckedListBox.GetItemChecked(2));

			doc.Save(settingsFilePath);
		}

		private void DefecturaParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}