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

namespace GoodsReportsTaxGroupsEKB
{
	public partial class TaxGroupsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public TaxGroupsParams()
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
				Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? 1 : 0);
				Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
				Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
				if (rbDocType.Checked)
					Utils.AddNode(root, "SORT_DOC", 1);  //�� ����� ���
				else Utils.AddNode(root, "SORT_DOC", 0);  //�� ����� ���

				foreach (DataRowItem dr in mpsContractor.Items)
					Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

				foreach (DataRowItem dr in mpsStore.Items)
					Utils.AddNode(root, "ID_STORE", dr.Id);

				if (chkRefreshDocMov.Checked)
					Utils.AddNode(root, "REFRESH_DOC_MOV", 1);

				ReportFormNew rep = new ReportFormNew();
				rep.ReportPath = reportFiles[0];
				rep.LoadData("REP_GOODS_REPORTS_TO_EKB_EX", doc.InnerXml);

				//rep.AddParameter("date_fr", ucPeriod.DateFrText);
				//rep.AddParameter("date_to", ucPeriod.DateToText);

				rep.BindDataSource("Goods_Reports_TO_DS_Table", 0);
				rep.BindDataSource("Goods_Reports_TO_DS_Table1", 1);
				rep.BindDataSource("Goods_Reports_TO_DS_Table2", 2);
				rep.BindDataSource("Goods_Reports_TO_DS_Table3", 3);
				rep.BindDataSource("Goods_Reports_TO_DS_Table4", 4);

				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
				rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
				rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
                rep.AddParameter("CONTRACTOR_GROUP", "1");
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("�������� �����������!");
		}

		public string ReportName
		{
			get { return "�������� ����� �� ��������� ������� (���)"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
		}
	}
}