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

namespace RCSLotHistory
{
	public partial class LotHistoryRep : ExternalReportForm, IExternalReportFormMethods
	{
        private string settingsFilePath;
		public LotHistoryRep()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);			
			Utils.AddNode(root, "ID_DEFECT", ucLot.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
			
			rep.LoadData("REPEX_LOT_HISTORY", doc.InnerXml);
			rep.BindDataSource("LotHistory_DS_Table0", 0);
			rep.BindDataSource("LotHistory_DS_Table1", 1);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "История партии"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            long IdDefect = Utils.GetLong(root, "ID_DEFECT");
            ucLot.SetValues(new DataRowItem(IdDefect, Guid.Empty, "", IdDefect.ToString()));
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

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "ID_DEFECT", ucLot.Id);

            doc.Save(settingsFilePath);
        }

        private void LotHistoryRep_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////
            this.ucLot = new UCMetaPluginSelect();
            this.ucLot.ButtonStyle = EButtonStyle.SelectClear;
            this.ucLot.Location = new System.Drawing.Point(66, 63);
            this.ucLot.Mnemocode = "DEFECT_JOURNAL";
            this.ucLot.Name = "ucLot";
            this.ucLot.Size = new System.Drawing.Size(229, 21);
            this.ucLot.TabIndex = 125;
            this.Controls.Add(this.ucLot);
            this.Controls.SetChildIndex(this.ucLot, 0);
            ///////////////////////////////////////////////////////
            settingsFilePath = Path.Combine(Utils.TempDir(), System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName + ".xml");
            ClearValues();
            LoadSettings();
        }

        private void LotHistoryRep_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

	}
}