using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;

namespace GoodsReportsDiscountGroup
{
	public partial class DiscountGroupParams : ExternalReportForm, IExternalReportFormMethods
	{
		public DiscountGroupParams()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		DataTable subReportTable;

		public void Print(string[] reportFiles)
		{
			if (mpsContractor.Items.Count != 0)
			{
				XmlDocument doc = new XmlDocument();
				XmlNode root = Utils.AddNode(doc, "XML");
				Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? 1 : 0);
				if (rbDocType.Checked)
					Utils.AddNode(root, "SORT_DOC", 1);  //по видам док
				else 
					Utils.AddNode(root, "SORT_DOC", 0);  //по датам док
				Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? 1 : 0);
				Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? 1 : 0);
				if (chkRefreshDocMov.Checked)
					Utils.AddNode(root, "REFRESH_DOC_MOV", 1);

				foreach (DataRowItem dr in mpsContractor.Items)
					Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

				foreach (DataRowItem dr in mpsStore.Items)
					Utils.AddNode(root, "ID_STORE", dr.Id);

				ReportFormNew rep = new ReportFormNew();
				if (reportFiles[1].Contains("GOODS_REPORTS_NAL_DIS_GROUP_EX.rdlc"))
					rep.ReportPath = reportFiles[1];
				if (chkGroupDiscount.Checked && rbDocType.Checked)
				{
					//    GOODS_REPORTS_NAL_DIS_GROUP_EX.rdlc
					if (reportFiles[1].Contains("GOODS_REPORTS_NAL_DIS_GROUP_EX.rdlc"))
					{
						rep.ReportPath = reportFiles[1];
						rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
						//subReportTable = 
						rep.LoadData("REP_GOODS_REPORTS_DISCOUNT_GROUP", doc.InnerXml);
						//rep.BindDataSource("DiscountGroupSub_DS_Table0", 0);											 
						subReportTable = rep.DataSource.Tables[0];
					}
				}
				else
					rep.ReportPath = rbDocType.Checked ? reportFiles[2] : reportFiles[0];

				rep.LoadData("GOODS_REPORTS_NAL_EX", doc.InnerXml);
				
				rep.BindDataSource("GoodsReportsNal_DS_dtBegin", 0);
				rep.BindDataSource("GoodsReportsNal_DS_dtAdd", 1);
				rep.BindDataSource("GoodsReportsNal_DS_dtSub", 2);
				rep.BindDataSource("GoodsReportsNal_DS_dtEnd", 3);
				rep.BindDataSource("GoodsReportsNal_DS_dtContractor", 4);
								
				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("Выберите контрагента!");
		}

		private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
		{
			e.DataSources.Add(new ReportDataSource("DiscountGroupSub_DS_Table0", subReportTable));
		}

		public string ReportName
		{
			get { return "Товарный отчет опт-розница-наложение"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
		}			
	}
}