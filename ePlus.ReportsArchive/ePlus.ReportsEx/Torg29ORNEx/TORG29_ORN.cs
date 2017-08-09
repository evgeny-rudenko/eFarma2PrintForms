using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;


namespace Torg29ORNEx
{
	public partial class TORG29_ORN : ExternalReportForm, IExternalReportFormMethods
	{
		public TORG29_ORN()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

        private string fileName = Path.Combine(Utils.TempDir(), "GoodsReportsORNSettings.xml");

		public void Print(string[] reportFiles)
		{
			if (mpsContractor.Items.Count != 0)
			{
				XmlDocument doc = new XmlDocument();
				XmlNode root = Utils.AddNode(doc, "XML");
				Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				Utils.AddNode(root, "NO_DETAIL", chShortReport.Checked ? "1" : "0");
				Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
				Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
				Utils.AddNode(root, "SORT_DOC", rbDocType.Checked ? "1" : "0");

				foreach (DataRowItem dr in mpsContractor.Items)
					Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

				foreach (DataRowItem dr in mpsStore.Items)
					Utils.AddNode(root, "ID_STORE", dr.Id);
				Utils.AddNode(root, "REFRESH_DOC_MOV", chkRefreshDocMov.Checked ? "1" : "0");
				
				ReportFormNew rep = new ReportFormNew();
			
				//rep.ReportPath = rbDocDate.Checked ? reportFiles[1] : reportFiles[0];
                
                rep.ReportPath = rbDocDate.Checked ? Path.Combine(Path.GetDirectoryName(reportFiles[0]),"TORG29_NAL_BY_DATE.rdlc") : Path.Combine(Path.GetDirectoryName(reportFiles[0]),"TORG29_NAL.rdlc");

				if (serviceCheckBox.Checked)
				{
					rep.LoadData("REP_GOODS_REPORTS_NAL_EX_SERVICE", doc.InnerXml);
				}
				else
				{
					rep.LoadData("REP_GOODS_REPORTS_NAL_EX", doc.InnerXml);
				}

				rep.BindDataSource("GoodsReportsNal_DS_dtBegin", 0);
				rep.BindDataSource("GoodsReportsNal_DS_dtAdd", 1);
				rep.BindDataSource("GoodsReportsNal_DS_dtSub", 2);
				rep.BindDataSource("GoodsReportsNal_DS_dtEnd", 4);
				rep.BindDataSource("GoodsReportsNal_DS_dtContractor", 5);
                rep.BindDataSource("GoodsReportsNal_DS_dtSumDis", 3);

				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("no_detail", chShortReport.Checked ? "1" : "0");
				rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
				rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("Выберите контрагента!");
		}

		public string ReportName
		{
			get { return "Торг29 Опт-розница-наложение"; }
		}

        private void TORG29_ORN_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileName)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.SelectSingleNode("/XML");
            XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
            foreach (XmlNode node in contractors)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                mpsContractor.Items.Add(dri);
            }
            XmlNodeList stores = root.SelectNodes("STORE");
            foreach (XmlNode node in stores)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                mpsStore.Items.Add(dri);
            }
        }

        private void TORG29_ORN_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearCache();            
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            foreach (DataRowItem dri in mpsContractor.Items)
            {
                XmlNode contractor = Utils.AddNode(root, "CONTRACTOR");
                Utils.AddNode(contractor, "ID", dri.Id);
                Utils.AddNode(contractor, "TEXT", dri.Text);
            }
            foreach (DataRowItem dri in mpsStore.Items)
            {
                XmlNode store = Utils.AddNode(root, "STORE");
                Utils.AddNode(store, "ID", dri.Id);
                Utils.AddNode(store, "TEXT", dri.Text);
            }
            doc.Save(fileName);
        }

        private void ClearCache()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (Directory.Exists(cachePath))
            {
                try
                {
                    Utils.ClearFolder(cachePath);
                    Directory.Delete(cachePath);
                }
                catch
                {

                }
            }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
            }
        }
	}
}