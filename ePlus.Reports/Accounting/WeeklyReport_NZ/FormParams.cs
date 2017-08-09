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
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace R32PWeeklyReport_NZ
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        private string settingsFilePath;

        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);

            ucPeriod1.AddValues(root);
            ucContractor.AddItems(root, "ID_CONTRACTOR");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "WeeklyReport_NZ.rdlc");

            //doc.Save(@"e:\Temp\input.xml");
            rep.LoadData("DBO.REPEX_WEEKLY_REPORT_NZ", doc.InnerXml);
            //rep.SaveSchema(@"e:\Temp\data.xml");

            rep.BindDataSource("WeeklyReport_NZ_DS_Table", 0);

            rep.AddParameter("date_fr", ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("contractor", ucContractor.TextValues() == "" ? "по всем" : ucContractor.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Еженедельный отчёт (На здоровье)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
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

            ucPeriod1.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod1.DateTo = Utils.GetDate(root, "DATE_TO");
        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", ucPeriod1.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod1.DateTo);

            doc.Save(settingsFilePath);
        }

        private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            ucPeriod1.DateTo = DateTime.Now.AddDays(-1);
            ucPeriod1.DateFrom = DateTime.Now.AddDays(-7);

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");

            LoadSettings();
        }
    }
}