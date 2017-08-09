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

namespace PurchaseStatisticsEx
{
    public partial class PurchaseStatisticsParams : ExternalReportForm, IExternalReportFormMethods
    {
        public PurchaseStatisticsParams()
        {
            InitializeComponent();
			ucStores.AllowSaveState = true;
			ClearValues();
        }

        public void Print(string[] reportFiles)
        {
			int orderDays = 0;
			if (!int.TryParse(nbOrderDays.Text, out orderDays) || orderDays <= 0)
			{
				MessageBox.Show("Количество дней заявки должно быть целым положительным числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "IS_ES", chkES.Checked ? "1" : "0");
            Utils.AddNode(root, "ORDER_DAYS", orderDays);

            string rowCount = (string) rbAll.Tag;
            if (rb1000.Checked)
                rowCount = (string) rb1000.Tag;
            if (rb500.Checked)
                rowCount = (string) rb500.Tag;
            if (rb400.Checked)
                rowCount = (string) rb400.Tag;
            if (rb300.Checked)
                rowCount = (string) rb300.Tag;
            if (rb200.Checked)
                rowCount = (string) rb200.Tag;
            if (rb100.Checked)
                rowCount = (string) rb100.Tag;

            Utils.AddNode(root, "ROW_COUNT", rowCount);

            string docs = (string) rbAllDoc.Tag;
            if (rbOut.Checked)
                docs = (string) rbOut.Tag;
            if (rbCheque.Checked)
                docs = (string) rbCheque.Tag;

            Utils.AddNode(root, "DOC_TYPE", docs);
            Utils.AddNode(root, "ORDER_BY", rbGoods.Checked ? "1" : "0");
			ucStores.AddItems(root, "ID_STORE");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_PURCHASES_STATISTICS", doc.InnerXml);

			rep.BindDataSource("PurchaseStatistics_DS_Table0", 0);

			StringBuilder period = new StringBuilder("Анализ продаж составлен за период: с ");
			period.Append(ucPeriod.DateFrText);
			period.Append(" по ");
			period.Append(ucPeriod.DateToText);
			period.Append(" (");
			period.Append(((ucPeriod.DateTo - ucPeriod.DateFrom).Days + 1).ToString());
			period.Append(" дн.)");

            rep.AddParameter("period",  period.ToString());
			rep.AddParameter("params", "Остатки проанализированы на " + ucPeriod.DateToText + ", а заявка с учетом " + nbOrderDays.Text + " дн.");
			rep.AddParameter("stores", "Склад: " + ucStores.TextValues());
			rep.AddParameter("rowCount", "Отображение в отчете: " + (rowCount == "0" ? "Все позиции" : rowCount + " позиций"));
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

            string docType = rbAllDoc.Text;
            if (rbOut.Checked) 
				docType = rbOut.Text;
            if (rbCheque.Checked) 
				docType = rbCheque.Text;

			rep.AddParameter("docs", "Обрабатываемые документы: " + docType);			

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Статистика для закупок"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

			rbGoods.Checked = true;
			rbAllDoc.Checked = true;
			rbAll.Checked = true;
			nbOrderDays.Text = "7";
			chkES.Checked = false;
		}
    }
}