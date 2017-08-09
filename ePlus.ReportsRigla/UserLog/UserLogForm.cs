using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.IO;
using ePlus.MetaData.Client;
using System;

namespace RCSUserLog_Rigla
{
    public partial class UserLogForm : ExternalReportForm, IExternalReportFormMethods
    {
        public UserLogForm()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucDrugStore.AddItems(root, "ID_DRUGSTORE");
            ucUser.AddItems(root, "ID_USER");
            ucGoods.AddItems(root, "ID_GOODS");
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_USER_LOG", doc.InnerXml);
            rep.BindDataSource("UserLog_DS_Table", 0);
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("DRUGSTORE", ucDrugStore.TextValues());
            rep.AddParameter("GOODS", ucGoods.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчет по сброшенным чекам"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
        }
        private string fileName = Path.Combine(Utils.TempDir(), (System.Reflection.Assembly.GetExecutingAssembly()).GetName().Name.ToString() + ".xml");  
        //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        //return 

        private void UserLogForm_Load(object sender, System.EventArgs e)
        {
            Text = "Отчет по сброшенным чекам";
            ucPeriod.SetPeriodMonth();


            ucDrugStore.Items.Clear();
            ucStore.Items.Clear();
            ucUser.Items.Clear();
            ucGoods.Items.Clear();


            if (!File.Exists(fileName)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNode root = doc.SelectSingleNode("/XML");

            XmlNodeList drug_stores = root.SelectNodes("DRUG_STORE");
            foreach (XmlNode node in drug_stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucDrugStore.Items.Add(dri);
            }

            XmlNodeList stores = root.SelectNodes("STORE");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucStore.Items.Add(dri);
            }


            XmlNodeList user = root.SelectNodes("USER");
            foreach (XmlNode node in user)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucUser.Items.Add(dri);
            }

            XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucGoods.Items.Add(dri);
            }

        }

        private void UserLogForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            XmlDocument docSave = new XmlDocument();
            XmlNode root = Utils.AddNode(docSave, "XML");
            

            foreach (DataRowItem dri in ucDrugStore.Items)
            {
                XmlNode drug_stores = Utils.AddNode(root, "DRUG_STORE");
                Utils.AddNode(drug_stores, "ID", dri.Id);
                Utils.AddNode(drug_stores, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in ucStore.Items)
            {
                XmlNode store = Utils.AddNode(root, "STORE");
                Utils.AddNode(store, "ID", dri.Id);
                Utils.AddNode(store, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in ucUser.Items)
            {
                XmlNode user = Utils.AddNode(root, "USER");
                Utils.AddNode(user, "ID", dri.Id);
                Utils.AddNode(user, "TEXT", dri.Text);
            }


            foreach (DataRowItem dri in ucGoods.Items)
            {
                XmlNode goods = Utils.AddNode(root, "GOODS");
                Utils.AddNode(goods, "ID", dri.Id);
                Utils.AddNode(goods, "TEXT", dri.Text);
            }

            docSave.Save(fileName);
        }
    }
}