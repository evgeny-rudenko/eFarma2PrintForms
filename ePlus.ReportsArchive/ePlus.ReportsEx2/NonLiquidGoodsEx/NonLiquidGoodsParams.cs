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
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace NonLiquidGoodsEx
{
	public partial class NonLiquidGoodsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public NonLiquidGoodsParams()
		{
			InitializeComponent();
			storesPluginMultiSelect.AllowSaveState = true;
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "DATE_FROM", periodPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
			Utils.AddNode(root, "SHOW_LOTS", showLotsCheckBox.Checked);

			storesPluginMultiSelect.AddItems(root, "STORE");

			ReportFormNew rep = new ReportFormNew();						
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_NON_LIQUID_GOODS", doc.InnerXml);
			rep.BindDataSource("NonLiquidGoods_DS_Table1", 0);
			rep.BindDataSource("NonLiquidGoods_DS_Table2", 1);

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("DATE_FROM", periodPeriod.DateFrText),
				new ReportParameter("DATE_TO", periodPeriod.DateToText),
				new ReportParameter("STORES", storesPluginMultiSelect.TextValues())
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			periodPeriod.DateTo = DateTime.Now;
			periodPeriod.DateFrom = DateTime.Now.AddDays(-13);

			showLotsCheckBox.Checked = false;
			storesPluginMultiSelect.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по неликвидным товарам"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}
	}
}