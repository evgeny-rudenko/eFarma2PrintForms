using System;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace FCBMarginControlZNVLS
{
	public partial class MarginControlParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string settingsFilePath = Path.Combine(Utils.TempDir(), "MarginControl.xml");
		private const string MARGIN = "Margin";
		private const string IMPORTANT = "Important";
		private const string INDEX = "Index";
		private const string EMPTY = "Empty";

		public MarginControlParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			foreach (MarginControl_DS.Table1Row row in this.marginControl_DS.Table1.Rows)
			{
				if (((row.LOW < 0.0M) || (row.HIGH < 0.0M)) || (row.VALUE < 0.0M))
				{
					MessageBox.Show("«начени€ должны быть положительными числами", "е‘арма", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				if (row.LOW > row.HIGH)
				{
					MessageBox.Show("Ќижн€€ граница должна быть меньше верхней", "е‘арма", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucContractors.AddItems(root, "ID_CONTRACTOR");			
			foreach (CatalogItem item in ucGoodsGroup.Items)
			{
				Utils.AddNode(root, "ID_GOODS_GROUP", item.Id);
			}

			foreach (MarginControl_DS.Table1Row row in this.marginControl_DS.Table1.Rows)
			{
				XmlNode node = Utils.AddNode(root, "ADPRICE");
				Utils.AddNode(node, "LOW", row.LOW);
				Utils.AddNode(node, "HIGH", row.HIGH);
				Utils.AddNode(node, "VALUE", row.VALUE);
			}

			Utils.AddNode(root, "IMPORTANT", importantCheckBox.Checked ? "1" : "0");
			Utils.AddNode(root, "ST", filterComboBox.SelectedIndex);
			Utils.AddNode(root, "EMPTY", emptyCheckBox.Checked ? "1" : "0");
			
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("DBO.REPEX_MARGIN_CONTROL_ZNVLS", doc.InnerXml);
			rep.BindDataSource("MarginControl_DS_Table0", 0);

			rep.AddParameter("show_code", codeCheckBox.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucContractors.Items.Clear();
			ucGoodsGroup.Clear();
			importantCheckBox.Checked = true;
			filterComboBox.SelectedIndex = 1;
			emptyCheckBox.Checked = true;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "ќтчет по контролю наценки по ∆Ќ¬Ћ—"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
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

			XmlNodeList list = doc.SelectNodes("//XML/ADPRICE");
			foreach (XmlNode node2 in list)
			{
				MarginControl_DS.Table1Row row = this.marginControl_DS.Table1.NewTable1Row();
				row.LOW = Utils.GetDecimal(node2, "LOW");
				row.HIGH = Utils.GetDecimal(node2, "HIGH");
				row.VALUE = Utils.GetDecimal(node2, "VALUE");
				this.marginControl_DS.Table1.Rows.Add(row);
			}

			importantCheckBox.Checked = Utils.GetBool(root, IMPORTANT);
			filterComboBox.SelectedIndex = Utils.GetInt(root, INDEX);
			emptyCheckBox.Checked = Utils.GetBool(root, EMPTY);
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

			foreach (MarginControl_DS.Table1Row row in this.marginControl_DS.Table1.Rows)
			{
				XmlNode node = Utils.AddNode(root, "ADPRICE");
				Utils.AddNode(node, "LOW", row.LOW);
				Utils.AddNode(node, "HIGH", row.HIGH);
				Utils.AddNode(node, "VALUE", row.VALUE);
			}

			Utils.AddNode(root, IMPORTANT, importantCheckBox.Checked);
			Utils.AddNode(root, INDEX, filterComboBox.SelectedIndex);
			Utils.AddNode(root, EMPTY, emptyCheckBox.Checked);

			doc.Save(settingsFilePath);
		}

		private void MarginControlParams_Load(object sender, EventArgs e)
		{
            ClearValues();
			LoadSettings();
		}

		private void MarginControlParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

		private void filtersDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
		{
			e.Row.Cells[0].Value = 0.0M;
			e.Row.Cells[1].Value = 0.0M;
			e.Row.Cells[2].Value = 0.0M;
		}

		private void filtersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			this.filtersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0M;
			e.Cancel = true;
		}
	}
}