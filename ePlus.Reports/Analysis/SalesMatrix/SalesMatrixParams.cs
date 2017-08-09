using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace RCSSalesMatrix
{
    public partial class SalesMatrixParams : ExternalReportForm, IExternalReportFormMethods
    {
		public SalesMatrixParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
			
			ucPeriod.AddValues(root);
			ucContractors.AddItems(root, "ID_CONTRACTOR");			

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_SALES_MATRIX", doc.InnerXml);

			rep.BindDataSource("SalesMatrix_DS_Table0", 0);

            rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("code", codeCheckBox.Checked ? "1" : "0");
			rep.AddParameter("contractors", ucContractors.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
			get { return "Матрица продаж"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			codeCheckBox.Checked = true;
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "SalesMatrixSettings.xml");
		private const string CODE = "CODE";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			codeCheckBox.Checked = Utils.GetBool(root, CODE);
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

			Utils.AddNode(root, CODE, codeCheckBox.Checked);

			doc.Save(settingsFilePath);
		}

		private void SalesMatrixParams_Load(object sender, EventArgs e)
		{
            ClearValues();
			LoadSettings();
		}

		private void SalesMatrixParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

    }
}