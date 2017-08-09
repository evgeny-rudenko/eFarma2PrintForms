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
		private string settingsFilePath = Path.Combine(Utils.TempDir(), "MonthReportSettings.xml");
		private const string CONTRACTOR = "CONTRACTOR";
		private const string MONTH = "MONTH";
		private const string PLAN = "PLAN";
		private const string DAYS = "DAYS";
		private const string EMPLOYEES = "EMPLOYEES";
		private const string AREA = "AREA";
		private const string FULL_AREA = "FULL_AREA";

		public FormParams()
		{
			InitializeComponent();
			СlearValues();
		}

		public void Print(string[] reportFiles)
		{
			int days = 0;
			int employees = 0;
			int area = 0;
			int fullArea = 0;

			if (ucContractor.Id == 0)
			{
				MessageBox.Show("Не задан контрагент", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (planNumericBox.Value <= 0m)
			{
				MessageBox.Show("Выручка от реализации должна быть больше нуля", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(daysTextBox.Text, out days) || days <= 0)
			{
				MessageBox.Show("Количество рабочих дней должно быть целым положительным числом", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(employeesTextBox.Text, out employees) || employees <= 0)
			{
				MessageBox.Show("Количество сотрудников должно быть целым положительным числом", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(areaTextBox.Text, out area) || area <= 0)
			{
				MessageBox.Show("Площадь торгового зала должна быть целым положительным числом", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(fullAreaTextBox.Text, out fullArea) || fullArea <= 0)
			{
				MessageBox.Show("Общая площадь аптеки должна быть целым положительным числом", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE", monthDateTimePicker.Value);
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MonthReport.rdlc");

			rep.LoadData("DBO.REPEX_MONTH_REPORT", doc.InnerXml);
			rep.BindDataSource("MonthReport_DS_Table0", 0);

			rep.AddParameter(CONTRACTOR, ucContractor.Text);
			rep.AddParameter(MONTH, monthDateTimePicker.Text);
			rep.AddParameter(PLAN, planNumericBox.Value.ToString());
			rep.AddParameter(DAYS, daysTextBox.Text);
			rep.AddParameter(EMPLOYEES, employeesTextBox.Text);
			rep.AddParameter(AREA, areaTextBox.Text);
			rep.AddParameter(FULL_AREA, fullAreaTextBox.Text);			

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Отчёт аптеки за месяц"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void FormParams_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			СlearValues();
		}

		private void СlearValues()
		{
			ucContractor.SetId(0);
			monthDateTimePicker.Value = monthDateTimePicker.Value.AddMonths(-1);

			planNumericBox.Value = 0m;
			daysTextBox.Text = "20";
			employeesTextBox.Text = "2";
			areaTextBox.Text = "40";
			fullAreaTextBox.Text = "100";
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

			ucContractor.SetId(Utils.GetInt(root, CONTRACTOR));
			planNumericBox.Value = Utils.GetDecimal(root, PLAN);
			daysTextBox.Text = Utils.GetString(root, DAYS);
			employeesTextBox.Text = Utils.GetString(root, EMPLOYEES);
			areaTextBox.Text = Utils.GetString(root, AREA);
			fullAreaTextBox.Text = Utils.GetString(root, FULL_AREA);
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

			Utils.AddNode(root, CONTRACTOR, ucContractor.Id);
			Utils.AddNode(root, PLAN, planNumericBox.Value);
			Utils.AddNode(root, DAYS, daysTextBox.Text);
			Utils.AddNode(root, EMPLOYEES, employeesTextBox.Text);
			Utils.AddNode(root, AREA, areaTextBox.Text);
			Utils.AddNode(root, FULL_AREA, fullAreaTextBox.Text);

			doc.Save(settingsFilePath);
		}
	}
}
