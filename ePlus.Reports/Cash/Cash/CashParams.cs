using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace RCSCash
{
    public partial class CashParams : ExternalReportForm, IExternalReportFormMethods
    {
        private string settingsFilePath;
        public CashParams()
        {
            InitializeComponent();
        }

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

			cashierMulti.Items.Clear();
			kkmMulti.Items.Clear();
			producerMulti.Items.Clear();
			contractorMulti.Items.Clear();
			goodsMulti.Items.Clear();

			detailCheckBox.Checked = false;
			serviceCheckBox.Checked = false;
		}

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "DETAIL", detailCheckBox.Checked ? "1" : "0");

			cashierMulti.AddItems(root, "CASHIER");
			kkmMulti.AddItems(root, "KKM");
			producerMulti.AddItems(root, "PRODUCER");
			contractorMulti.AddItems(root, "CONTRACTOR");
			goodsMulti.AddItems(root, "GOODS");
			
            ReportFormNew rep = new ReportFormNew();

			if (serviceCheckBox.Checked)
			{
				rep.LoadData("REPEX_CASH_SERVICE", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REPEX_CASH", doc.InnerXml);
			}

            rep.BindDataSource("Cash_DS_Table0", 0);
            rep.BindDataSource("Cash_DS_Table1", 1);
			rep.BindDataSource("Cash_DS_Table2", 2);

            rep.ReportPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]),"Cash.rdlc");
            rep.AddParameter("date_fr", ucPeriod.DateFrText);
            rep.AddParameter("date_to", ucPeriod.DateToText);
            rep.AddParameter("detail", detailCheckBox.Checked ? "1" : "0");
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ExecuteReport(this);
        }        

        public string ReportName
        {
            get { return "Отчет по кассирам"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            detailCheckBox.Checked = Utils.GetBool(root, "DETAIL");
            serviceCheckBox.Checked = Utils.GetBool(root, "WITH_SERVICE");
            chbGoodCode.Checked = Utils.GetBool(root, "SHOW_GOODS_CODE");

            XmlNodeList Cashier = root.SelectNodes("CASHIER");
            foreach (XmlNode node in Cashier)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                cashierMulti.AddRowItem(new DataRowItem(id, guid, code, text));
            }
            XmlNodeList KKM = root.SelectNodes("KKM");
            foreach (XmlNode node in KKM)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                kkmMulti.AddRowItem(new DataRowItem(id, guid, code, text));
            }
            XmlNodeList goods = root.SelectNodes("GOODS");
            foreach (XmlNode node in goods)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                goodsMulti.AddRowItem(new DataRowItem(id, guid, code, text));
            }
            XmlNodeList Producer = root.SelectNodes("PRODUCER");
            foreach (XmlNode node in Producer)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                producerMulti.AddRowItem(new DataRowItem(id, guid, code, text));
            }
            XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                contractorMulti.AddRowItem(new DataRowItem(id, guid, code, text));
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

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root,"DETAIL",detailCheckBox.Checked?"1":"0");
            Utils.AddNode(root,"WITH_SERVICE",serviceCheckBox.Checked?"1":"0");
            Utils.AddNode(root,"SHOW_GOODS_CODE",chbGoodCode.Checked?"1":"0");

            foreach (DataRowItem dri in cashierMulti.Items)
            {
                XmlNode node = Utils.AddNode(root, "CASHIER");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in kkmMulti.Items)
            {
                XmlNode node = Utils.AddNode(root, "KKM");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in goodsMulti.Items)
            {
                XmlNode node = Utils.AddNode(root, "GOODS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }
            foreach (DataRowItem dri in producerMulti.Items)
            {
                XmlNode node = Utils.AddNode(root, "PRODUCER");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }
            foreach (DataRowItem dri in contractorMulti.Items)
            {
                XmlNode node = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }
            doc.Save(settingsFilePath);
        }

        private void CashParams_Load(object sender, EventArgs e)
        {
            settingsFilePath = Path.Combine(Utils.TempDir(), System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName + ".xml");
            ClearValues();
            LoadSettings();
        }

        private void CashParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

    }
}