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

namespace AccompanimentGoods
{
    public partial class AccompanimentGoodsParams : ExternalReportForm, IExternalReportFormMethods
    {
		public AccompanimentGoodsParams()
        {
            InitializeComponent();
			ClearValues();
        }

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucContractor.Items.Clear();
			ucStore.Items.Clear();
			ucGoods.Clear();
		}

        public void Print(string[] reportFiles)
        {
			if (ucGoods.Id == 0)
			{
				MessageBox.Show("Не выбран товар!", "еФарма2", MessageBoxButtons.OK);
				return;
			}

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			Utils.AddNode(root, "ID_GOODS", ucGoods.Id);
			ucContractor.AddItems(root, "ID_CONTRACTOR");
			ucStore.AddItems(root, "ID_STORE");
			
            ReportFormNew rep = new ReportFormNew();
			rep.LoadData("REPEX_ACCOMPANIMENT_GOODS", doc.InnerXml);
			rep.BindDataSource("AccompanimentGoods_DS_Table0", 0);
			rep.BindDataSource("AccompanimentGoods_DS_Table1", 1);
			rep.BindDataSource("AccompanimentGoods_DS_Table2", 2);

			rep.ReportPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]), "AccompanimentGoods.rdlc");

            rep.ExecuteReport(this);
        }        

        public string ReportName
        {
            get { return "Сопутствующие товары"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
    }
}