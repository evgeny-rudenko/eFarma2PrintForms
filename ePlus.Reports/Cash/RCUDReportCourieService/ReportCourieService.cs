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

namespace RCUDReportCourieService
{
    public partial class ReportCourieService : ExternalReportForm, IExternalReportFormMethods
    {
    
        public ReportCourieService()
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


            DateTime variable = txtDateReport.Value.Date;            
            Utils.AddNode(root, "DATE_REP", Utils.SqlDate(variable));             
            Utils.AddNode(root, "RTYPE", selFixReport.Checked ? "1" : "0");
            Utils.AddNode(root, "DELIVERY_SERVICE", selDeliveryService.SelectedIndex);

            ucContractors.AddItems(root, "ID_CONTRACTOR");


            ReportFormNew rep = new ReportFormNew();
            rep.Text = ReportName;
            rep.ReportFormName = "ДС от Курьерской службы";
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ReportCourieService.rdlc");          
            
            rep.LoadData("UDP_REPEX_COURIER_SERVICE_SAV", doc.InnerXml);            

            rep.BindDataSource("ReportCourieService_DS_Table", 0);

            rep.AddParameter("DATE_REP", string.Format("на {0}", txtDateReport.Value.ToString("dd.MM.yyyy")));
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ReportFormName = ReportName;
            rep.ExecuteReport(this);

        }

        private void ClearValues()
        {
            txtDateReport.Value = DateTime.Now;                      
        }

        public string ReportName
        {
            get { return "ДС от Курьерской службы"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
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


            //txtDateReport.Value = Utils.GetDate(root, "DATE");

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

            //txtDateReport.Value = Utils.GetDate(root, "DATE");

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

        private void ucPeriod_Load(object sender, EventArgs e)
        {

        }

        private void selFixReport_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}