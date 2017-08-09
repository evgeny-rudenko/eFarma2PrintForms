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

namespace RejectedGoods
{
	public partial class RejectedGoodsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public RejectedGoodsParams()
		{
			InitializeComponent();
			ClearValues();
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "RejectedGoodsSettings.xml");
		private const string MNAME = "MNAME";
		private const string MSER = "MSER";

		public void Print(string[] reportFiles)
		{
			int name = 0;
			int series = 0;

			if (!int.TryParse(nameTextBox.Text, out name) || name < 0)
			{
				MessageBox.Show("Значение в поле Количество символов наименования должно быть неотрицательным целым числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(seriesTextBox.Text, out series) || series < 0)
			{
				MessageBox.Show("Значение в поле Количество символов серии должно быть неотрицательным целым числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, MNAME, nameTextBox.Text);
			Utils.AddNode(root, MSER, seriesTextBox.Text);
						
			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucStores.AddItems(root, "ID_STORE");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_REJECTED_GOODS", doc.InnerXml);
			rep.BindDataSource("RejectedGoods_Table0", 0);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Отчет по браку"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			nameTextBox.Text = "4";
			seriesTextBox.Text = "4";

			ucContractors.Items.Clear();
			ucStores.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void RejectedGoodsParams_Load(object sender, EventArgs e)
		{
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

			nameTextBox.Text = Utils.GetString(root, MNAME);
			seriesTextBox.Text = Utils.GetString(root, MSER);
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

			Utils.AddNode(root, MNAME, nameTextBox.Text);
			Utils.AddNode(root, MSER, seriesTextBox.Text);

			doc.Save(settingsFilePath);
		}

		private void RejectedGoodsParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}