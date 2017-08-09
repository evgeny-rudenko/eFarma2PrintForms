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

namespace RCChSaleGoodsByGroup
{
    public partial class SaleGoodsByGroupParams : ExternalReportForm, IExternalReportFormMethods
    {
        private string settingsFilePath;
		public SaleGoodsByGroupParams()
        {
            InitializeComponent();
        }

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucContractor.Items.Clear();
			ucGoodsGroup.Items.Clear();
			//ucGoods.Clear();
		}

        public void Print(string[] reportFiles)
        {
            if (ucGoodsGroup.Items.Count == 0)
			{
				MessageBox.Show("Не выбрана группа товаров!", "еФарма2", MessageBoxButtons.OK);
				return;
			}

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            //Utils.AddNode(root, "ID_GOODS_GROUP", ucGoodsGroup.Id);
			ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucGoodsGroup.AddItems(root, "ID_GOODS_GROUP");
			
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_SALE_GOODS_BY_GROUP", doc.InnerXml);
            rep.BindDataSource("SALE_GOODS_BY_GROUP_DS_Table0", 0);
            rep.BindDataSource("SALE_GOODS_BY_GROUP_DS_Table1", 1);
            rep.BindDataSource("SALE_GOODS_BY_GROUP_DS_Table2", 2);
            rep.BindDataSource("SALE_GOODS_BY_GROUP_DS_Table3", 3);
            rep.BindDataSource("SALE_GOODS_BY_GROUP_DS_Table4", 4);

            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("CONTRACTOR", ucContractor.Items.Count == 0 ? "Все" : ucContractor.TextValues());
            rep.AddParameter("GOODS_GROUP", ucGoodsGroup.Items.Count == 0 ? "Все" : ucGoodsGroup.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ExecuteReport(this);
        }        

        public string ReportName
        {
            get { return "Объём продаж по группам товаров"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        private void AccompanyingGoodsParams_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            ClearValues();
            LoadSettings();
        }

        private void AccompanyingGoodsParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
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

            foreach (DataRowItem dri in ucContractor.Items)
            {
                XmlNode node = Utils.AddNode(root, "CONTRACTORS");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }

            foreach (DataRowItem dri in ucGoodsGroup.Items)
            {
                XmlNode node = Utils.AddNode(root, "GOODS_GROUP");
                Utils.AddNode(node, "ID", dri.Id);
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
            }
            doc.Save(settingsFilePath);
        }
        private void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
               // nUpDPersent.Value = 30;
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
                return;

           // nUpDPersent.Value = Utils.GetInt(root, "GOODS_EXISTS_PERCENT");
            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucContractor.AddRowItem(new DataRowItem(id, guid, code, text));
            }

            XmlNodeList GoodsGroup = root.SelectNodes("GOODS_GROUP");
            foreach (XmlNode node in GoodsGroup)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucGoodsGroup.AddRowItem(new DataRowItem(id, guid, code, text));
            }
        }
    }
}