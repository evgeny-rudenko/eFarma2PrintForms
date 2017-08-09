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

namespace CashRefusals
{
    public partial class CashRefusalsParams : ExternalReportForm, IExternalReportFormMethods
    {
        public CashRefusalsParams()
        {
            InitializeComponent();
            FillSortTypes();
            ucSort.Refresh();
        }

        private string SettingsFilePath
        {
            get
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            }
        }

        private void FillSortTypes()
        {
            ucSort.Items.Add((object)new SortTypes("Дата отказа", "DOCUMENT_DATE"));
            ucSort.Items.Add((object)new SortTypes("Название товара", "GOODS_NAME"));
            ucSort.Items.Add((object)new SortTypes("Количество отказов", "REFUSES_COUNT"));
            ucSort.Items.Add((object)new SortTypes("Количество отказанных единиц", "QUANTITY"));
            ucSort.Items.Add((object)new SortTypes("Сотрудник АУ", "USER_NAME"));
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            ucPeriod.AddValues(root);

            ucStores.AddItems(root, "ID_STORE");
            ucGoods.AddItems(root, "ID_GOODS");

            foreach (DataRowItem row in ucUsers.Items)
            {
                Utils.AddNode(root, "USER_CODE", row.Code);
            }

            // тип сортировки
            SortTypes st = (SortTypes)ucSort.SelectedItem;
            Utils.AddNode(root, "SORT", st.Value);

            // центр или нет
            bool selfIsCenter = SelfIsCenter();
            Utils.AddNode(root, "CO", selfIsCenter);

            // если ЦО то с группировкой, иначе - без
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = selfIsCenter ?
              Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashRefusals_CO.rdlc") :
              Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashRefusals.rdlc");
            rep.LoadData("REPEX_CASH_REFUSALS", doc.InnerXml);
            rep.BindDataSource("CashRefusals_DS_Table0", 0);
            rep.BindDataSource("CashRefusals_DS_Table1", 1);

            rep.AddParameter("date_fr", ucPeriod.DateFrText);
            rep.AddParameter("date_to", ucPeriod.DateToText);

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отказы на кассе"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

        private void ClearValues()
        {
            ucPeriod.DateTo = DateTime.Now;
            ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
            ucStores.Items.Clear();
            ucGoods.Items.Clear();
            ucUsers.Items.Clear();
            ucSort.SelectedItem = 0;
        }

        /// <summary>
        /// Метод для получения подтверждения или опровержения того, что МЫ является центром
        /// </summary>
        /// <returns>true - ЦЕНТР, false - не ЦЕНТР</returns>
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void LoadSettings()
        {
            ClearValues();
            if (!File.Exists(SettingsFilePath))
            {
                ucSort.SelectedIndex = 0;
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            // Аптеки
            XmlNodeList stores = root.SelectNodes("STORES");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            // Сотрудники АУ
            XmlNodeList users = root.SelectNodes("USERS");
            foreach (XmlNode node in users)
            {
                long id = 0;
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Guid.Empty;
                string code = Utils.GetString(node, "CODE");
                ucUsers.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            // Товары
            XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucGoods.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            string sort = Utils.GetString(root, "SORT");
            if (!string.IsNullOrEmpty(sort))
                ucSort.SelectedIndex = Utils.GetInt(root, "SORT");
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

            // Аптеки
            foreach (DataRowItem dri in ucStores.Items)
            {
                XmlNode node = Utils.AddNode(root, "STORES");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            // Сотрудники АУ
            foreach (DataRowItem dri in ucUsers.Items)
            {
                XmlNode node = Utils.AddNode(root, "USERS");
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            // Товары
            foreach (DataRowItem dri in ucGoods.Items)
            {
                XmlNode node = Utils.AddNode(root, "GOODS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            Utils.AddNode(root, "SORT", ucSort.SelectedIndex);

            doc.Save(SettingsFilePath);
        }

        private void CashRefusalsParams_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void CashRefusalsParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
        {
            //если МЫ не является центром, то открываем только те склады, которые относятся к МЫ
            string qry = "";
            string dop = "";
            if (SelfIsCenter())
                dop = " OR (1 = 1)";
            qry = String.Format("((STORE.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)){0})", dop);
            if (!string.IsNullOrEmpty(qry))
                ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
        }
    }

    public class SortTypes
    {
        private string name;
        private string value;

        public SortTypes(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
        }

        public string Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return name;
        }
    }
}