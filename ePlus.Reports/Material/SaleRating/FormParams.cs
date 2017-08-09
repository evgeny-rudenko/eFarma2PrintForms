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

namespace SaleRating
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
            if (!cbAllKind.Checked && !cbGoodsMoving.Checked && !cbInvoiceOut.Checked && !cbSaleKKM.Checked)
            { 
                MessageBox.Show("Необходимо выбрать вид расхода.");
                return;
            }
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			ucPeriod.AddValues(root);
			mpsStore.AddItems(root, "ID_STORE");
			//mpsContractor.AddItems(root, "ID_CONTRACTOR");
            mpsGoods.AddItems(root, "ID_GOODS");
            Utils.AddNode(root, "TYPE_REPORT", cbTypeReport.SelectedIndex);
            Utils.AddNode(root, "ALL_KIND", cbAllKind.Checked ? "1": "0");
            Utils.AddNode(root, "GOODS_MOVING", cbGoodsMoving.Checked ? "1" : "0");
            Utils.AddNode(root, "INVOICE_OUT", cbInvoiceOut.Checked ? "1" : "0");
            Utils.AddNode(root, "SALE_KKM", cbSaleKKM.Checked ? "1" : "0");
            Utils.AddNode(root, "GROUP_A", nUpDPersentA.Value);
            Utils.AddNode(root, "GROUP_B", nUpDPersentB.Value);
            Utils.AddNode(root, "GROUP_C", nUpDPersentC.Value);
            Utils.AddNode(root, "NOAU", cbFilterAU.Checked ? "1" : "0");
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];  
            rep.LoadData("DBO.REPEX_SALE_RATING", doc.InnerXml);
            rep.BindDataSource("SALE_RATING_DS_Table", 0);
            decimal SumProfit = 0m, QtyDocSale = 0m, SumSale = 0m;
            string Detail2 = "", ColNamePercent = "";
            bool ShowSumSale = false;

            foreach (DataRow Row in rep.DataSource.Tables[0].Rows)
            {
                SumProfit += Utils.GetDecimal(Row, "SUM_PROFIT");
                QtyDocSale += Utils.GetDecimal(Row, "QTY_DOC_SALE");
                SumSale += Utils.GetDecimal(Row, "SUM_SALE");
            }
            if (cbTypeReport.SelectedIndex == 0)
            {
                Detail2 = string.Format("Суммарная прибыль - {0:c}", SumProfit);
                ColNamePercent = "% от общей прибыли";
            }
            else
                if (cbTypeReport.SelectedIndex == 1)
                {
                    Detail2 = string.Format("Общее количество продаж - {0:f}", QtyDocSale);
                    ColNamePercent = "% от кол-ва продаж";
                }
                else
                {
                    Detail2 = string.Format("Общая сумма реализации - {0:c}", SumSale);
                    ColNamePercent = "% от суммы реализации";
                    ShowSumSale = true;
                }
            rep.AddParameter("DETAIL", " Период с " + ucPeriod.DateFrom.Date.ToString("dd.MM.yyyy") + " по " + ucPeriod.DateTo.Date.ToString("dd.MM.yyyy"));
            rep.AddParameter("REP_CAPTION", cbTypeReport.Text);
            rep.AddParameter("DETAIL2", Detail2);
            rep.AddParameter("STORES", mpsStore.TextValues()=="" ? "Все": mpsStore.TextValues());
            rep.AddParameter("COL_NAME_PERCENT", ColNamePercent);
            rep.AddParameter("GOODS", mpsGoods.TextValues() == "" ? "Все" : mpsGoods.TextValues());
            rep.AddParameter("SHOW_SUM_SALE", ShowSumSale ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

        public  string ReportName
		{
            get { return "Рейтинг"; }
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

            //mpsContractor.Items.Clear();
            cbAllKind.Checked = true;
            cbAllKind.Checked = false;
            mpsStore.Items.Clear();
            mpsGoods.Items.Clear();

            //nUpDPersent.Value = 30;
		}

        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                nUpDPersentA.Value = 20;
                nUpDPersentB.Value = 30;
                nUpDPersentC.Value = 50;
                cbTypeReport.SelectedIndex = 0;
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            cbTypeReport.SelectedIndex = Utils.GetInt(root, "TYPE_REPORT");
            cbAllKind.Checked = Utils.GetBool(root, "ALL_KIND");
            cbGoodsMoving.Checked = Utils.GetBool(root, "GOODS_MOVING");
            cbInvoiceOut.Checked = Utils.GetBool(root, "INVOICE_OUT");
            cbSaleKKM.Checked = Utils.GetBool(root, "SALE_KKM");
            nUpDPersentA.Value = Utils.GetInt(root, "GROUP_A");
            nUpDPersentB.Value = Utils.GetInt(root, "GROUP_B");
            nUpDPersentC.Value = Utils.GetInt(root, "GROUP_C");
            cbFilterAU.Checked = Utils.GetBool(root, "NOAU");
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

            //Utils.AddNode(root, "GOODS_EXISTS_PERCENT", nUpDPersent.Value);
            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "TYPE_REPORT", cbTypeReport.SelectedIndex);
            Utils.AddNode(root, "ALL_KIND", cbAllKind.Checked ? "1" : "0");
            Utils.AddNode(root, "GOODS_MOVING", cbGoodsMoving.Checked ? "1" : "0");
            Utils.AddNode(root, "INVOICE_OUT", cbInvoiceOut.Checked ? "1" : "0");
            Utils.AddNode(root, "SALE_KKM", cbSaleKKM.Checked ? "1" : "0");
            Utils.AddNode(root, "GROUP_A", nUpDPersentA.Value);
            Utils.AddNode(root, "GROUP_B", nUpDPersentB.Value);
            Utils.AddNode(root, "GROUP_C", nUpDPersentC.Value);
            Utils.AddNode(root, "NOAU", cbFilterAU.Checked ? "1" : "0");
/*
            foreach (DataRowItem dri in mpsContractor.Items)
            {
                XmlNode node = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }
            */
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
            if (!SelfIsCenter())
            {
                //storesPluginMultiSelect.PluginContol.ComponentList[2].ToString();
                CheckBox ChB = (CheckBox)(((ePlus.CommonEx.Store.UCStoreFilter)mpsStore.PluginContol.ComponentList[0]).Controls["chkSelf"]);
                ChB.Checked = true;// chkSelf = true;
                ChB.Enabled = false;
            }
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


        private void cbAllKind_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllKind.Checked)
            {
                cbGoodsMoving.Enabled = false;
                cbInvoiceOut.Enabled = false;
                cbSaleKKM.Enabled = false;
                cbGoodsMoving.Checked = false;
                cbInvoiceOut.Checked = false;
                cbSaleKKM.Checked = false;
            }
            else
            {
                cbGoodsMoving.Enabled = true;
                cbInvoiceOut.Enabled = true;
                cbSaleKKM.Enabled = true;
            }
        }
        private void CheckABC()
        { 
            decimal Tmp = 100M - (nUpDPersentA.Value + nUpDPersentB.Value);
            if (Tmp>=0)
            nUpDPersentC.Value = Tmp;
            else
            {
                nUpDPersentC.Value = 0;
                nUpDPersentB.Value += Tmp;
            }
        }
        private void nUpDPersentA_ValueChanged(object sender, EventArgs e)
        {
            CheckABC();
        }

        private void nUpDPersentB_ValueChanged(object sender, EventArgs e)
        {
            CheckABC();
        }
	}
}