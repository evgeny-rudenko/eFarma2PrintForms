using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace R42BContracts
{
    public partial class Contracts : ExternalReportForm, IExternalReportFormMethods
    {
        public Contracts()
        {
            InitializeComponent();
        }

        private string SettingsFilePath
        {
            get
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            }
        }

        public void Print(string[] reportFiles)
        {

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            ucContractors.AddItems(root, "ID_CONTRACTOR");


            ReportFormNew rep = new ReportFormNew();
            rep.Text = ReportName;
            rep.ReportFormName = "Сведения о заключенных договорах";
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "Contracts.rdlc");
            rep.LoadData("R42B_CONTRACTS", doc.InnerXml);
            rep.BindDataSource("Contracts_DS_Table", 0);
            rep.BindDataSource("Details_DS_Table", 1);
            
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ReportFormName = ReportName;
            rep.ExecuteReport(this);

        }

        private void ClearValues()
        {
            ucPeriod.DateTo = DateTime.Now;
            ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
            ucContractors.Items.Clear();
        }

        public string ReportName
        {
            get { return "Сведения о заключенных договорах"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        private void LoadSettings()
        {
            ClearValues();
            if (!File.Exists(SettingsFilePath))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
            {
                return;
            }


            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            XmlNodeList stores = root.SelectNodes("CONTRACTORS");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucContractors.AddRowItem(new DataRowItem(id, guid, code, text));
            }

        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root;

            if (File.Exists(SettingsFilePath))
            {
                doc.Load(SettingsFilePath);
                root = doc.SelectSingleNode("//XML");
                root.RemoveAll();
            }
            else
            {
                root = Utils.AddNode(doc, "XML");
            }

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (DataRowItem dri in ucContractors.Items)
            {
                XmlNode node = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            doc.Save(SettingsFilePath);
        }

        private void bOK_Click_1(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void bClose_Click_1(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}