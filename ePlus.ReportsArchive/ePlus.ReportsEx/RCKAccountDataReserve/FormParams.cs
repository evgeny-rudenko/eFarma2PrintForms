using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;

namespace GoodsDefectUnionEx
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string filter;
		private string resDate;

		public FormParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);
			Utils.AddNode(root, "DATAFILTER", filter);
			Utils.AddNode(root, "DATE_FR", period.DateFrom);
			Utils.AddNode(root, "DATE_TO", period.DateTo);
			//Utils.AddNode(root, "DATE_S", dateTimePicker1.Value);
			//Utils.AddNode(root, "DATE_E", dateTimePicker2.Value);
			ucPluginMulti_Store.AddItems(root, "ID_STORE");
			ucPluginMulti_Contractor.AddItems(root, "ID_CONTRACTOR");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.AddParameter("DATE_FR", period.DateFrText);
			rep.AddParameter("DATE_TO", period.DateToText);
			rep.AddParameter("date_res", resDate);
			rep.AddParameter("contractor", ucPluginMulti_Contractor.TextValues());
			rep.AddParameter("store", ucPluginMulti_Store.TextValues());

			rep.LoadData("REPEX_ACCOUNT_DATARESERVE", doc.InnerXml);
			rep.BindDataSource("AccountDataReserve_DS_Table", 0);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Отчет по счетам и срокам резервирования"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
		{

			switch (comboBox1.SelectedIndex)
			{
				case 0:
					dateTimePicker1.Enabled = true;
					dateTimePicker2.Enabled = false;
					filter = " > " + "convert(datetime, \'" + dateTimePicker1.Value.ToShortDateString() + "\', 104)";
					resDate = "Больше даты " + dateTimePicker1.Value.ToShortDateString();
					break;
				case 1:
					dateTimePicker1.Enabled = true;
					dateTimePicker2.Enabled = true;
					filter = " BETWEEN " + "convert(datetime, \'" + dateTimePicker1.Value.ToShortDateString() + "\', 104)"
												+ " AND " + "convert(datetime, \'" + dateTimePicker2.Value.AddDays(1).ToShortDateString() + "\', 104)";
					resDate = "Входит в интервал с " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();
					break;
				case 2:
					dateTimePicker1.Enabled = false;
					dateTimePicker2.Enabled = false;
					filter = "";
					resDate = "Любая дата ";
					break;
				case 3:
					dateTimePicker1.Enabled = true;
					dateTimePicker2.Enabled = false;
					filter = " < " + "convert(datetime, \'" + dateTimePicker1.Value.ToShortDateString() + "\', 104)";
					resDate = "Меньше даты " + dateTimePicker1.Value.ToShortDateString();
					break;
				case 4:
					dateTimePicker1.Enabled = true;
					dateTimePicker2.Enabled = false;
					filter = " = " + "convert(datetime, \'" + dateTimePicker1.Value.ToShortDateString() + "\', 104)";
					resDate = "Равна дате " + dateTimePicker1.Value.ToShortDateString();
					break;
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void ClearValues()
		{
			period.DateTo = DateTime.Now;
			period.DateFrom = DateTime.Now.AddDays(-13);
			comboBox1.SelectedIndex = 2;

			ucPluginMulti_Contractor.Items.Clear();
			ucPluginMulti_Store.Items.Clear();
		}
	}
}