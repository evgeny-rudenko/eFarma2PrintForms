using System;
using System.Net.Mime;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WinForms;
using ePlus.MetaData.Client;

namespace RCLReestrPayNoLS
{
    public partial class ReestrPayNoLSForm : ExternalReportForm, IExternalReportFormMethods
    {
        private string settingsFilePath;
        public ReestrPayNoLSForm()
        {
            InitializeComponent();
        }

        public string ReportName
        {
            get { return "Реестр по закупке нелекарственных средств"; }
        }

        private void ClearValues()
        {
            ucPeriod.SetPeriodMonth();
            ucContractor.Items.Clear();
            ucStore.Items.Clear();
            ucGoodsGroup.Clear();
            checkShowGroup.Checked = true;
        }

        public void Print(string[] reportFiles)
        {
            if (ucGoodsGroup.Items.Count == 0)
            {
                MessageBox.Show("Не заполнена группа ЛС", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveControl = ucGoodsGroup;
                return;
            }
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucGoodsGroup.AddItems(root, "ID_GOODS_GROUP");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]), "ReestrPayNoLS.rdlc"); ;
            rep.LoadData("REPEX_REESTR_PAY_NO_LS", doc.InnerXml);
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("GOODS_GROUP", ucGoodsGroup.TextValues());
            rep.AddParameter("SHOW_GROUP", checkShowGroup.Checked? "1":"0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.BindDataSource("ReestrPayNoLS_DS_Table", 0);
            rep.ExecuteReport(this);
            
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

            XmlNodeList store = root.SelectNodes("ID_STORE");
            foreach (XmlNode node in store)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucStore.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }

            XmlNodeList contractors = root.SelectNodes("ID_CONTRACTOR");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucContractor.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }

            XmlNodeList group = root.SelectNodes("ID_GOODS_GROUP");
            foreach (XmlNode node in group)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucGoodsGroup.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }
            checkShowGroup.Checked = Utils.GetBool(root, "SHOW_GROUP");


            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            foreach (DataRowItem dri in ucStore.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_STORE");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in ucContractor.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_CONTRACTOR");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in ucGoodsGroup.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_GOODS_GROUP");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            Utils.AddNode(root, "SHOW_GROUP", checkShowGroup.Checked);

            doc.Save(settingsFilePath);
        }

        private void ReestrPayNoLSForm_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            ClearValues();
            LoadSettings();
        }

        private void ReestrPayNoLSForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}