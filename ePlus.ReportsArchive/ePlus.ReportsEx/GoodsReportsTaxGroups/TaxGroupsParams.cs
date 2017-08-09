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

namespace GoodsReportsTaxGroups
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

				if (supVatRadioButton.Checked)
					Utils.AddNode(root, "SUP_VAT", 1);

				ReportFormNew rep = new ReportFormNew();
				rep.ReportPath = reportFiles[0];

				if (serviceCheckBox.Checked)
				{
					if (supVatRadioButton.Checked)
					{
						rep.LoadData("REP_GOODS_REPORTS_TO_EX_SERVICE", doc.InnerXml);
					}
					else
					{
						rep.LoadData("REP_GOODS_REPORTS_TO_EX_SERVICE_SAL", doc.InnerXml);
					}
				}
				else
				{
					if (supVatRadioButton.Checked)
					{
						rep.LoadData("REP_GOODS_REPORTS_TO_EX", doc.InnerXml);
					}
					else
					{
						rep.LoadData("REP_GOODS_REPORTS_TO_EX_SAL", doc.InnerXml);
					}
				}

				rep.BindDataSource("Goods_Reports_TO_DS_Table", 0);
				rep.BindDataSource("Goods_Reports_TO_DS_Table1", 1);
				rep.BindDataSource("Goods_Reports_TO_DS_Table2", 2);
				rep.BindDataSource("Goods_Reports_TO_DS_Table3", 4);
				rep.BindDataSource("Goods_Reports_TO_DS_Table4", 5);
				rep.BindDataSource("Goods_Reports_TO_DS_Table5", 3);

				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
				rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
				rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
				rep.AddParameter("show_nal", chbShowNal.Checked ? "1" : "0");
				rep.ExecuteReport(this);
			}
			else MessageBox.Show("�������� �����������!");
		}

		public string ReportName
		{
			get { return "�������� ����� �� ��������� �������"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
		}

		public override string GroupName 
		{
			get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
		}
	}
}