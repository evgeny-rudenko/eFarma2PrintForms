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
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace KKMQuantityEx
{
	public partial class KKMQuantityParams : ExternalReportForm, IExternalReportFormMethods
	{
		public KKMQuantityParams()
		{
			InitializeComponent();

			cashRegistersPluginMultiSelect.AllowSaveState = true;
			storesPluginMultiSelect.AllowSaveState = true;

			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			if (contractorsPluginMultiSelect.Items.Count == 0)
			{
				MessageBox.Show("Не заполнен контрагент!", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);
			periodPeriod.AddValues(root);

			if (timeCheckBox.Checked)
			{
				Utils.AddNode(root, "TIME_FR", fromTimeDateTimePicker.Value.ToShortTimeString());
				Utils.AddNode(root, "TIME_TO", toTimeDateTimePicker.Value.ToShortTimeString());
			}
			
			Utils.AddNode(root, "ID_DETAIL", detailCheckBox.Checked ? "1" : "0");

			if (closeSessionCheckBox.Checked)
				Utils.AddNode(root, "IS_CLOSED", "1");

			cashRegistersPluginMultiSelect.AddItems(root, "ID_CASH_REGISTER");
			contractorsPluginMultiSelect.AddItems(root, "ID_CONTRACTOR");
			storesPluginMultiSelect.AddItems(root, "ID_STORE");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			if (serviceCheckBox.Checked)
			{
				rep.LoadData("REPEX_KKM_QUANTITY_SERVICE", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REPEX_KKM_QUANTITY", doc.InnerXml);
			}


			rep.BindDataSource("KKM_QTY_Buying_DS_Table", 0);
			rep.BindDataSource("KKM_QTY_Buying_DS_Table1", 1);

			ReportParameter[] parameters = new ReportParameter[5] {
				new ReportParameter("date_fr1", Utils.GetDate(rep.DataSource.Tables[0].Rows[0]["DATE_FR"]).ToString()),
				new ReportParameter("date_fr", periodPeriod.DateFrText),
				new ReportParameter("date_to", periodPeriod.DateToText),
				new ReportParameter("detail", detailCheckBox.Checked.ToString()),
				new ReportParameter("cashes", cashRegistersPluginMultiSelect.TextValues())
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			rep.ExecuteReport(this);
		}


		void ClearValues()
		{
			detailCheckBox.Checked = true;
			closeSessionCheckBox.Checked = false;
			timeCheckBox.Checked = false;

			periodPeriod.SetPeriodMonth();
			toTimeDateTimePicker.Value = DateTime.Now;
			fromTimeDateTimePicker.Value = fromTimeDateTimePicker.Value.AddHours(-1);

			cashRegistersPluginMultiSelect.Items.Clear();
			contractorsPluginMultiSelect.Items.Clear();
			storesPluginMultiSelect.Items.Clear();
		}

		private void LoadSelf()
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1", con);
				con.Open();
				contractorsPluginMultiSelect.AddItem((long) command.ExecuteScalar());
			}
		}

		void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
			LoadSelf();
		}

		public string ReportName
		{
			get { return "Отчет по количеству покупок"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}

		private void KKMQuantityParams_Load(object sender, EventArgs e)
		{
			LoadSelf();
		}

		private void timeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			fromTimeDateTimePicker.Enabled = timeCheckBox.Checked;
			toTimeDateTimePicker.Enabled = timeCheckBox.Checked;
		}

		private void toTimeDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (toTimeDateTimePicker.Value < fromTimeDateTimePicker.Value)
				toTimeDateTimePicker.Value = fromTimeDateTimePicker.Value;
		}
	}
}