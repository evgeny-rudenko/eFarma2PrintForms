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

namespace MoveGoodsEsEx
{
	public partial class MoveGoodsEsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public MoveGoodsEsParams()
		{
			InitializeComponent();
			cbSort.SelectedIndex = 0;
		    ucPeriod.SetPeriodMonth();
		}

	    public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");			
			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "MOV", checkMove.Checked ? "1" : "0");
			Utils.AddNode(root, "ONLY_INVOICE", checkInvoice.Checked ? "1" : "0");
            multiProducer.AddItems(root, "KOD_PRODUCER");
			multiContractor.AddItems(root, "ID_CONTRACTOR");
			multiStore.AddItems(root, "ID_STORE");
			multiKind.AddItems(root, "ID_GOODS_KIND");
            multiGoods.AddItems(root, "KOD_ES");
			if (chkUseLotDate.Checked)
			{
				Utils.AddNode(root, "USE_LOT_DATE", 1);
				Utils.AddNode(root, "LOT_DATE_FROM", periodLot.DateFrom);
				Utils.AddNode(root, "LOT_DATE_TO", periodLot.DateTo);
				Utils.AddNode(root, "SORT_LOT_DATE_ORDER", 1);
				//if (cbSort.SelectedIndex > 0)
				//    Utils.AddNode(root, "SORT_LOT_DATE_ORDER", cbSort.SelectedIndex == 1 ? 0 : 1);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];			
			rep.LoadData("REP_MOVEGOODS_ES_EX", doc.InnerXml);
			rep.BindDataSource("MoveGoodsEs_DS_Table", 0);
			rep.BindDataSource("MoveGoodsEs_DS_Table1", 1);

			rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("PRODUCER", multiProducer.TextValues());
			rep.AddParameter("CONTRACTOR", multiContractor.TextValues());
			rep.AddParameter("STORE", multiStore.TextValues());
			rep.AddParameter("GOODS_KIND", multiKind.TextValues());
			rep.AddParameter("GOODS", multiGoods.TextValues());
			rep.AddParameter("MOVE", checkMove.Checked.ToString());
			rep.ExecuteReport(this);
			//rep.ReportFormName = "Движение товаров";
		}

		public string ReportName
		{
			get { return "Движение товаров"; }
		}

		private void chkUseLotDate_CheckedChanged(object sender, EventArgs e)
		{
			cbSort.Enabled = periodLot.Enabled = chkUseLotDate.Checked;
		}
	}
}