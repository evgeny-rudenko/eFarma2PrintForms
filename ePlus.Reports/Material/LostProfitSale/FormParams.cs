using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.IO;
using System.Data.SqlClient;
using ePlus.MetaData.Server;

namespace RCChLostProfitSale
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{

    private string settingsFilePath;
        public FormParams()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            InitializeComponent();
            ClearValues();
        }

		public void Print(string[] reportFiles)
		{
            if ((ucPeriod.DateTo < Convert.ToDateTime("01.01.1753")) || (ucPeriod.DateFrom < Convert.ToDateTime("01.01.1753")))
            {
                MessageBox.Show("Ошибка в периоде, минимальное значение даты  01.01.1753");
                return;
            }
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			ucPeriod.AddValues(root);
			mpsStore.AddItems(root, "ID_STORE");
			mpsContractor.AddItems(root, "ID_CONTRACTOR");
            mpsGoods.AddItems(root, "ID_GOODS");
            Utils.AddNode(root, "GOODS_EXISTS_PERCENT", nUpDPersent.Value);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

            rep.AddParameter("DETAIL", " Период с " + ucPeriod.DateFrom.Date.ToString("dd.MM.yyyy") + " по " + ucPeriod.DateTo.Date.ToString("dd.MM.yyyy"));
            rep.AddParameter("DateFrom", ucPeriod.DateFrom.Date.ToString("dd.MM.yyyy"));
            rep.AddParameter("DateTo", ucPeriod.DateTo.Date.ToString("dd.MM.yyyy"));
            rep.AddParameter("STORES", mpsStore.TextValues()=="" ? "Все": mpsStore.TextValues());
            rep.AddParameter("CONTRACTORS", mpsContractor.TextValues() == "" ? "Все" : mpsContractor.TextValues());
            rep.AddParameter("GOODS", mpsGoods.TextValues() == "" ? "Все" : mpsGoods.TextValues());
            //rep.AddParameter("GOODS_EXISTS_PERCENT", nUpDPersent.Value.ToString());


            rep.LoadData("DBO.REPEX_LOST_PROFIT_SALE", doc.InnerXml);
            rep.BindDataSource("LOST_PROFIT_SALE_DS_Table", 0);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
            get { return "Упущенная выгода"; }
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

            mpsContractor.Items.Clear();
            mpsStore.Items.Clear();
            mpsGoods.Items.Clear();

            //nUpDPersent.Value = 30;
		}

        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                nUpDPersent.Value = 30;
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            nUpDPersent.Value = Utils.GetInt(root, "GOODS_EXISTS_PERCENT");
            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                mpsContractor.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            XmlNodeList stores = root.SelectNodes("STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                mpsStore.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                mpsGoods.AddRowItem(new DataRowItem(id, guid, code, text));
            }
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

            Utils.AddNode(root, "GOODS_EXISTS_PERCENT", nUpDPersent.Value);
            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (DataRowItem dri in mpsContractor.Items)
            {
                XmlNode node = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in mpsStore.Items)
            {
                XmlNode node = Utils.AddNode(root, "STORES");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in mpsGoods.Items)
            {
                XmlNode node = Utils.AddNode(root, "GOODS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            doc.Save(settingsFilePath);
        }


        private bool SelfIsCenter()
        {
            bool result = false;
            DataService_BL bl = new DataService_BL();

            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                result = (bool)command.ExecuteScalar();
            }
            return result;
        }

        private void mpsStore_BeforePluginShow(object sender, CancelEventArgs e)
        {
            if (mpsContractor.Items.Count > 0)
            {
                string stores = string.Empty;
                foreach (DataRowItem dri in mpsContractor.Items)
                {
                    stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
                }
                if (!String.IsNullOrEmpty(stores))
                    mpsStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
                      String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("Выберите контрагента!");
            }


        }

        private void mpsContractor_BeforePluginShow(object sender, CancelEventArgs e)
        {
            if (!SelfIsCenter())
                mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
            else
                mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(1=1)");
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            ClearValues();
            LoadSettings();
        }

        private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }
	}
}