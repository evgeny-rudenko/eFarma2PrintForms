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

namespace RCSTurnoverPercent
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string fileName = Path.Combine(Utils.TempDir(), "TurnoverPercentSettings.xml");

		public FormParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			Utils.AddNode(root, "ORDER", sortComboBox.SelectedIndex);
			Utils.AddNode(root, "IS_SAL", vatComboBox.SelectedIndex == 1);

			ReportFormNew rep = new ReportFormNew();

			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TurnoverPercent.rdlc");
			rep.LoadData("REPEX_TURNOVER_PERCENT", doc.InnerXml);

			rep.BindDataSource("TurnoverPercent_DS_Table1", 0);
			rep.BindDataSource("TurnoverPercent_DS_Table2", 1);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("contractor", ucContractor.Text);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Ïðîöåíò îáîðîòà"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void FormParams_Load(object sender, EventArgs e)
		{
            ÑlearValues();

			this.ucContractor.Id = this.IdContractorDefault;

			if (!File.Exists(fileName)) return;
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			XmlNode root = doc.SelectSingleNode("/XML");

			vatComboBox.SelectedIndex = Utils.GetInt("VAT_VAL");
			sortComboBox.SelectedIndex = Utils.GetInt("SORT_VAL");
		}

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "VAT_VAL", vatComboBox.SelectedIndex);
			Utils.AddNode(root, "SORT_VAL", sortComboBox.SelectedIndex);
			doc.Save(fileName);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ÑlearValues();
		}

		private void ÑlearValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

			vatComboBox.SelectedIndex = 0;
			sortComboBox.SelectedIndex = 0;
		}
	}
}
