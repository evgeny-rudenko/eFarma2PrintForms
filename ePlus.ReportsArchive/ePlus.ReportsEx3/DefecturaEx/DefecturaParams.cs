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

			Utils.AddNode(root, "ID_STORE", ucStore.Id);
			ucGoods.AddItems(root, "ID_GOODS");

			Utils.AddNode(root, "IS_OA", oaCheckBox.Checked);
			Utils.AddNode(root, "IS_GROUPS", groupCheckBox.Checked);
			Utils.AddNode(root, "IS_RESERVE", reserveCheckBox.Checked);
			Utils.AddNode(root, "IS_ES", esCheckBox.Checked);

			foreach (Doc doc in docsCheckedListBox.CheckedItems)
			{
				Utils.AddNode(root, "DOC", doc.Code);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			PostLoadDataCompleteAsyncArgs e = new PostLoadDataCompleteAsyncArgs();
			e.ThrowException = true;
			e.Completed = false;
			rep.LoadDataAsync("REPEX_DEFECTURA", param.InnerXml, delegate(object state)
																   {
																	   rep.BindDataSource("Defectura_Table0", 0);

																	   rep.AddParameter("date_from", fromDateTimePicker.Value.ToString("g"));
																	   rep.AddParameter("date_to", toDateTimePicker.Value.ToString("g"));
																	   rep.AddParameter("stores", ucStore.Text);
																	   rep.AddParameter("goods", ucGoods.TextValues());
																	   rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

																	   rep.ExecuteReport(this);
																   }, e);
			//rep.LoadData("REPEX_DEFECTURA", param.InnerXml);
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
			ucStore.Clear();

			oaCheckBox.Checked = false;
			groupCheckBox.Checked = false;
			reserveCheckBox.Checked = false;
			esCheckBox.Checked = false;
			chbGoodCode.Checked = false;

			for (int i = 0; i < docsCheckedListBox.Items.Count; i++)
			{
				docsCheckedListBox.SetItemChecked(i, true);
			}

			long? storeId = null;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT TOP 1 ID_STORE FROM STORE WHERE ID_CONTRACTOR = (SELECT C.ID_CONTRACTOR FROM REPLICATION_CONFIG AS RC INNER JOIN CONTRACTOR AS C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1) ORDER BY MNEMOCODE", con);
				con.Open();
				storeId = (long?) command.ExecuteScalar();
			}

			if (storeId.HasValue)
				ucStore.SetId(storeId.Value);

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
	}
}