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

namespace GoodsRegistry
{
    public partial class GoodsRegistryParams : ExternalReportForm, IExternalReportFormMethods
    {

        private string settingsFilePath;
        private const string PAGE = "PAGE";
        private const string SHORT = "SHORT";

        public GoodsRegistryParams()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            Utils.AddNode(root, "DATE_SH_FR", ucPeriodShip.DateFrom);
            Utils.AddNode(root, "DATE_SH_TO", ucPeriodShip.DateTo);

            ucContractors.AddItems(root, "ID_CONTRACTOR");
            ucStores.AddItems(root, "ID_STORE");

            foreach (DataRowItem row in ucIns.Items)
            {
                Utils.AddNode(root, "ID_INS", row.Guid);
            }

            foreach (DataRowItem dr in ucGoods.Items)
            {
                Utils.AddNode(root, "ID_GOODS", dr.Id);
            }

            foreach (DataRowItem dr in ucLPU.Items)
            {
                Utils.AddNode(root, "ID_LPU", dr.Id);
            }

            foreach (DataRowItem row in ucLgot.Items)
            {
                Utils.AddNode(root, "ID_LGOT", row.Guid);
            }

            ReportFormNew rep = new ReportFormNew();
            if (pageComboBox.SelectedIndex == 0)
            {
                if (shortCheckBox.Checked)
                {
                    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "GoodsRegistry_port_short.rdlc");
                }
                else
                {
                    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "GoodsRegistry_port.rdlc");
                }
            }
            else
            {
                if (shortCheckBox.Checked)
                {
                    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "GoodsRegistry_land_short.rdlc");
                }
                else
                {
                    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "GoodsRegistry_land.rdlc");
                }
            }

            if (pageComboBox.SelectedIndex == 0)
            {
                Utils.AddNode(root, "SHORT", shortCheckBox.Checked);
                rep.LoadData("DBO.REPEX_GOODS_REGISTRY", doc.InnerXml);
                rep.BindDataSource("GoodsRegistry_Table0", 0);
                rep.BindDataSource("GoodsRegistry_Table1", 1);
            }
            else
            {
                rep.LoadData("DBO.REPEX_GOODS_REGISTRY_LAND", doc.InnerXml);
                rep.BindDataSource("GoodsRegistry_land_Table0", 0);
                rep.BindDataSource("GoodsRegistry_land_Table1", 1);
            }

            //rep.AddParameter("date_fr", ucPeriod.DateFrText);
            //rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("date_fr", ucPeriodShip.DateFrText);
            rep.AddParameter("date_to", ucPeriodShip.DateToText);

            rep.AddParameter("contrs", ucContractors.TextValues());
            rep.AddParameter("stores", ucStores.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Реестр лекарственных средств отпущенных за период"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        private void ClearValues()
        {
            ucPeriod.SetPeriodMonth();
            ucPeriodShip.SetPeriodMonth();

            ucContractors.Items.Clear();
            ucStores.Items.Clear();
            ucIns.Items.Clear();
            ucGoods.Items.Clear();
            ucLPU.Items.Clear();
            ucLgot.Items.Clear();

            pageComboBox.SelectedIndex = 0;
            shortCheckBox.Checked = false;
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

            //аптеки
            XmlNodeList contractors = root.SelectNodes("ID_CONTRACTORS");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucContractors.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }

            //склады
            XmlNodeList stores = root.SelectNodes("ID_STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucStores.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }

            //страховые компании
            XmlNodeList ins = root.SelectNodes("ID_INS");
            foreach (XmlNode node in ins)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                ucIns.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
            }

            //товары
            XmlNodeList goods = root.SelectNodes("ID_GOODS");
            foreach (XmlNode node in goods)
            {
                dri.Guid = Utils.GetGuid(node, "ID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucGoods.AddRowItem(dri);
            }

            //ЛПУ
            XmlNodeList lpus = root.SelectNodes("ID_LPU");
            foreach (XmlNode node in lpus)
            {
                dri.Guid = Utils.GetGuid(node, "ID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucLPU.AddRowItem(dri);
            }

            //льготники
            XmlNodeList lgots = root.SelectNodes("ID_LGOT");
            foreach (XmlNode node in lgots)
            {
                dri.Guid = Utils.GetGuid(node, "GUID");
                dri.Text = Utils.GetString(node, "TEXT");
                ucLgot.AddRowItem(dri);
            }

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            ucPeriodShip.DateFrom = Utils.GetDate(root, "DATE_SH_FROM");
            ucPeriodShip.DateTo = Utils.GetDate(root, "DATE_SH_TO");

            pageComboBox.SelectedIndex = Utils.GetInt(root, PAGE);
            shortCheckBox.Checked = Utils.GetBool(root, SHORT);
        }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            //аптеки
            foreach (DataRowItem dri in ucContractors.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //склады
            foreach (DataRowItem dri in ucStores.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_STORES");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            //страховые компании
            foreach (DataRowItem dri in ucIns.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_INS");
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

            //льготники
            foreach (DataRowItem dri in ucLgot.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_LGOT");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            Utils.AddNode(root, "DATE_SH_FROM", ucPeriodShip.DateFrom);
            Utils.AddNode(root, "DATE_SH_TO", ucPeriodShip.DateTo);

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            Utils.AddNode(root, PAGE, pageComboBox.SelectedIndex);
            Utils.AddNode(root, SHORT, shortCheckBox.Checked);

            doc.Save(settingsFilePath);
        }

        private void GoodsRegistryParams_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void GoodsRegistryParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }
    }
}