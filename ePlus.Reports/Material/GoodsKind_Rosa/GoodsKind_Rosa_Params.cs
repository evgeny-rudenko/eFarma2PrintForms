using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace R32PGoodsKind_Rosa
{
	public partial class R32PGoodsKind_Rosa_Params : ExternalReportForm, IExternalReportFormMethods
	{
        public R32PGoodsKind_Rosa_Params()
		{
			InitializeComponent();
		}

        private string SettingsFilePath
        {
            get
            {
                //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                //return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
                return "GoodsKind_Rosa.xml";
            }
        }
        
        public void Print(string[] reportFiles)
		{
            ReportFormNew rep = new ReportFormNew();

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FR", fPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", fPeriod.DateTo);

            fStore.AddItems(root, "ID_STORE");
            fGoodsKind.AddItems(root, "ID_GOODS_KIND");
            fGoods.AddItems(root, "ID_GOODS");

            Utils.AddNode(root, "GOODS_DETAIL", chGoods.Checked ? "1" : "0");

            rep.ReportPath = reportFiles[0];

            //doc.Save(@"e:\Temp\input.xml");
            rep.LoadData("REPEX_GOODS_KIND_ROSA", doc.InnerXml);
            rep.BindDataSource("GoodsKind_Rosa_DS_Table", 0);
            //rep.SaveSchema(@"e:\Temp\data.xml");

            rep.AddParameter("DATE_FR", fPeriod.DateFrText);
            rep.AddParameter("DATE_TO", fPeriod.DateToText);
            rep.AddParameter("STORE", fStore.TextValues() == "" ? "(по всем)" : fStore.TextValues());
            rep.AddParameter("GOODS_DETAIL", chGoods.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Отчёт по видам номенклатуры (Роса)"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

        private void LoadSettings()
        {
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


            fPeriod.DateFrom = Utils.GetDate(root, "DATE_FR");
            fPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

            chGoods.Checked = Utils.GetBool(root, "GOODS_DETAIL");
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

            Utils.AddNode(root, "DATE_FR", fPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", fPeriod.DateTo);

            Utils.AddNode(root, "GOODS_DETAIL", chGoods.Checked);

            doc.Save(SettingsFilePath);
        }

        private void R32PGoodsKind_Rosa_Params_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void R32PGoodsKind_Rosa_Params_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }
    }
}