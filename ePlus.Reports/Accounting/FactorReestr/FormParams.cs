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

namespace FactorReestr
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public FormParams()
		{
			InitializeComponent();
			—learValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);

			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucStores.AddItems(root, "ID_STORE");
			Utils.AddNode(root, "ZNVLS", znvlsCheckBox.Checked);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("DBO.REPEX_FACTOR_REESTR", doc.InnerXml);
			rep.BindDataSource("FactorReestr_DS_Table0", 0);

			rep.AddParameter("contr", ucContractors.TextValues());
			rep.AddParameter("stores", ucStores.TextValues());
			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "œÓÍ‡Á‡ÚÂÎË Ì‡ˆÂÌÍË"; }
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

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "RCSFactorReeestr_1.xml");
		private const string ZNVLS = "ZNVLS";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			znvlsCheckBox.Checked = Utils.GetBool(root, ZNVLS);
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

			Utils.AddNode(root, ZNVLS, znvlsCheckBox.Checked);

			doc.Save(settingsFilePath);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			—learValues();
		}

		private void —learValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

			ucContractors.Items.Clear();
			ucStores.Items.Clear();

			znvlsCheckBox.Checked = false;
		}
	}
}
