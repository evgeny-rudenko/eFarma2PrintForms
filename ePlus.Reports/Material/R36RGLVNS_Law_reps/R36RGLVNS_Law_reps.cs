using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace R36RGLVNS_Law_reps
{
    public partial class R36RGLVNS_Law_reps : ExternalReportForm, IExternalReportFormMethods
    {
        public R36RGLVNS_Law_reps()
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
            XmlNode root = Utils.AddNode(doc, "XML", null);

            ucStore.AddItems(root, "ID_STORE");
            Utils.AddNode(root, "DATE_OST", Utils.SqlDate(dateDateTimePicker.Value));
            Utils.AddNode(root, "IS_20", rb20pos.Checked ? 1 : 0);

            ReportFormNew rep = new ReportFormNew();

            //    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "PriceList2Ex.rdlc");
            rep.ReportPath = reportFiles[0];
            rep.ReportPath = rb20pos.Checked ?
              Path.Combine(Path.GetDirectoryName(reportFiles[0]), "R36RGLVNS_Law_reps_1.rdlc") :
              Path.Combine(Path.GetDirectoryName(reportFiles[0]), "R36RGLVNS_Law_reps.rdlc");

            rep.LoadData("REPEX_R36RGLVNS_Law_reps", doc.InnerXml);
            rep.BindDataSource("R36RGLVNS_Law_reps_DS_Table0", 0);
            //rep.BindDataSource("R36RGoodsRest_DS_Table1", 1);

            rep.AddParameter("STORES", ucStore.TextValues());
            rep.AddParameter("DATE_OST", string.Format("{0}", dateDateTimePicker.Value.ToString("dd.MM.yyyy")));
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчеты для прокуратуры (Оренбург 2012)"; }
        }

        private void ClearValues()
        {
            dateDateTimePicker.Value = DateTime.Now;
            //cbGroups.Checked = false;
            //ucGoods.Items.Clear();
            ucStore.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearValues();
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

            dateDateTimePicker.Value = Utils.GetDate(root, "DATE");
            if (Utils.GetBool(root, "IS_20"))
            {
                rb20pos.Checked = true;
                rbAllpos.Checked = false;
            }
            else
            {
                rbAllpos.Checked = true;
                rb20pos.Checked = false;
            }

            //cbGroups.Checked = Utils.GetBool(root, "GROUPS");

            /*XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucGoods.AddRowItem(new DataRowItem(id, guid, code, text));
            }*/

            XmlNodeList stores = root.SelectNodes("STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucStore.AddRowItem(new DataRowItem(id, guid, code, text));
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

            Utils.AddNode(root, "DATE", dateDateTimePicker.Value);
            Utils.AddNode(root, "IS_20", rb20pos.Checked);
            //Utils.AddNode(root, "GROUPS", cbGroups.Checked);

            /*foreach (DataRowItem dri in ucGoods.Items)
            {
                XmlNode node = Utils.AddNode(root, "GOODS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
            }*/

            foreach (DataRowItem dri in ucStore.Items)
            {
                XmlNode node = Utils.AddNode(root, "STORES");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            doc.Save(SettingsFilePath);
        }

        private void PriceListParams_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void PriceListParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ClearValues();
        }
    }
}
