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

namespace RCBTrustLetter_AP25
{
    public partial class TrustLetter_AP25_Params : ExternalReportForm, IExternalReportFormMethods
    {
        private string settingsFilePath;

        public TrustLetter_AP25_Params()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            InitializeComponent();

            //ucContractors.MultiSelect = true;
            //ucGoods.MultiSelect = true;
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            Utils.AddNode(root, "DATE_SHIP_FROM", ucPeriodShip.DateFrom);
            Utils.AddNode(root, "DATE_SHIP_TO", ucPeriodShip.DateTo);

            foreach (DataRowItem row in ucLgot.Items)
            {
                Utils.AddNode(root, "ID_LGOT", row.Guid);
            }

            ucStores.AddItems(root, "ID_STORE");

            foreach (DataRowItem row in ucIns.Items)
            {
                Utils.AddNode(root, "ID_INS", row.Guid);
            }

            foreach (DataRowItem dr in ucContractors.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);
            }

            foreach (DataRowItem dr in ucGoods.Items)
            {
                Utils.AddNode(root, "ID_GOODS", dr.Id);
            }

            foreach (DataRowItem dr in ucLPU.Items)
            {
                Utils.AddNode(root, "ID_LPU", dr.Id);
            }

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TrustLetter_AP25.rdlc");

            rep.LoadData("DBO.REPEX_TRUST_LETTER_AP25", doc.InnerXml);
            rep.BindDataSource("TrustLetter_AP25_DS_Table0", 0);

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчет по доверительным письмам (Форма АП25)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        private void ClearValues()
        {
            ucPeriod.SetPeriodMonth();
            ucPeriodShip.SetPeriodMonth();

            ucContractors.Clear();
            ucGoods.Clear();
            ucLPU.Clear();

            ucIns.Items.Clear();
            ucStores.Items.Clear();
            ucLgot.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void LoadSettings()
        {
            ClearValues();
            if (!File.Exists(settingsFilePath))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            DataRowItem dri = new DataRowItem();

            //льготники
            XmlNodeList lgots = root.SelectNodes("ID_LGOT");
            foreach (XmlNode node in lgots)
            {
                dri.Guid = Utils.GetGuid(node, "GUID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucLgot.AddRowItem(dri);
            }

            //склады
            XmlNodeList stores = root.SelectNodes("ID_STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            //страховые компании
            XmlNodeList ins = root.SelectNodes("ID_INS");
            foreach (XmlNode node in ins)
            {
                dri.Guid = Utils.GetGuid(node, "GUID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucIns.AddRowItem(dri);
            }

            //контрагенты
            XmlNodeList contractors = root.SelectNodes("ID_CONTRACTOR");
            foreach (XmlNode node in contractors)
            {
                dri.Guid = Utils.GetGuid(node, "ID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucContractors.AddRowItem(dri);
            }

            //товары
            XmlNodeList goods = root.SelectNodes("ID_GOODS");
            foreach (XmlNode node in goods)
            {
                dri.Guid = Utils.GetGuid(node, "ID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucGoods.AddRowItem(dri);
            }

            //товары
            XmlNodeList lpus = root.SelectNodes("ID_LPU");
            foreach (XmlNode node in lpus)
            {
                dri.Guid = Utils.GetGuid(node, "ID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucLPU.AddRowItem(dri);
            }

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            ucPeriodShip.DateFrom = Utils.GetDate(root, "DATE_SH_FROM");
            ucPeriodShip.DateTo = Utils.GetDate(root, "DATE_SH_TO");
        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            //льготники
            foreach (DataRowItem dri in ucLgot.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_LGOT");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //склады
            foreach (DataRowItem dri in ucStores.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_STORES");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "GUID", dri.Guid);
            }

            //страховые компании
            foreach (DataRowItem dri in ucIns.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_INS");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //контрагенты
            foreach (DataRowItem dri in ucContractors.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_CONTRACTOR");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //товары
            foreach (DataRowItem dri in ucGoods.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_GOODS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //ЛПУ
            foreach (DataRowItem dri in ucLPU.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_LPU");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            Utils.AddNode(root, "DATE_SH_FROM", ucPeriodShip.DateFrom);
            Utils.AddNode(root, "DATE_SH_TO", ucPeriodShip.DateTo);

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            doc.Save(settingsFilePath);
        }

        private void TrustLetter_AP25_Params_Load(object sender, EventArgs e)
        {
            LoadSettings();

            //ucContractors.MultiSelect = true;
            //ucGoods.MultiSelect = true;
        }

        private void TrustLetter_AP25_Params_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }
    }
}