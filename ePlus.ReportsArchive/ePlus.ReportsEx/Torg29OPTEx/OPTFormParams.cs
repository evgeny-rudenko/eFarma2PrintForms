using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;


namespace Torg29OPTEx
{
	public partial class OPTFormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public OPTFormParams()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		public void Print(string[] reportFiles)
		{
			if (mpsContractor.Items.Count != 0)
			{
				XmlDocument doc = new XmlDocument();
				XmlNode root = Utils.AddNode(doc, "XML");
				Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? "1" : "0");
				Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
				Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
				Utils.AddNode(root, "SORT_DOC", rbDocType.Checked ? "1" : "0");

				foreach (DataRowItem dr in mpsContractor.Items)
					Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

				foreach (DataRowItem dr in mpsStore.Items)
					Utils.AddNode(root, "ID_STORE", dr.Id);
				Utils.AddNode(root, "REFRESH_DOC_MOV", chkRefreshDocMov.Checked ? "1" : "0");

				ReportFormNew rep = new ReportFormNew();
				rep.ReportPath = reportFiles[0];
				rep.LoadData("REP_TORG29_OPT_EX", doc.InnerXml);

				rep.BindDataSource("Goods_ReportsDS_Table", 0);
				rep.BindDataSource("Goods_ReportsDS_Table1", 1);
				rep.BindDataSource("Goods_ReportsDS_Table2", 2);
				rep.BindDataSource("Goods_ReportsDS_Table3", 3);
				rep.BindDataSource("Goods_ReportsDS_Table4", 4);

				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
				rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
				rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("Выберите контрагента!");
		}

		public string ReportName
		{
			get { return "Торг 29 (суммы поставщика)"; }
		}
	}
}