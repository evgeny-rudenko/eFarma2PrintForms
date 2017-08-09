using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
//using ePlus.Client.Core;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Client.Component;
using System.Data.SqlClient;

namespace RCBMoveGoods_Rigla
{
	public partial class MoveGoodsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public MoveGoodsParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "MOV", checkMove.Checked ? "1" : "0");
			Utils.AddNode(root, "ONLY_INVOICE", checkInvoice.Checked ? "1" : "0");

			multiProducer.AddItems(root, "ID_PRODUCER");
			multiContractor.AddItems(root, "ID_CONTRACTOR");
			multiStore.AddItems(root, "ID_STORE");
			multiKind.AddItems(root, "ID_GOODS_KIND");
			multiGoods.AddItems(root, "ID_GOODS");

			if (chkUseLotDate.Checked)
			{
				Utils.AddNode(root, "USE_LOT_DATE", 1);
				Utils.AddNode(root, "LOT_DATE_FROM", periodLot.DateFrom);
				Utils.AddNode(root, "LOT_DATE_TO", periodLot.DateTo);
				if (cbSort.SelectedIndex != 0)
					Utils.AddNode(root, "SORT_LOT_DATE_ORDER", cbSort.SelectedIndex == 1 ? 0 : 1);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

      rep.LoadData("REPEX_MOVE_GOODS_RIGLA", doc.InnerXml);
			rep.BindDataSource("MoveGoods_DS_Table", 0);
			rep.BindDataSource("MoveGoods_DS_Table1", 1);

			rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("PRODUCER", multiProducer.TextValues());
			rep.AddParameter("CONTRACTOR", multiContractor.TextValues());
			rep.AddParameter("STORE", multiStore.TextValues());
			rep.AddParameter("GOODS_KIND", multiKind.TextValues());
			rep.AddParameter("GOODS", multiGoods.TextValues());
			rep.AddParameter("MOVE", checkMove.Checked.ToString());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Движение товаров (Ригла)"; }
		}

		private void chkUseLotDate_CheckedChanged(object sender, EventArgs e)
		{
			cbSort.Enabled = periodLot.Enabled = chkUseLotDate.Checked;
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

			periodLot.DateTo = DateTime.Now;
			periodLot.DateFrom = DateTime.Now.AddDays(-13);

			checkMove.Checked = true;
			chkUseLotDate.Checked = false;
			checkInvoice.Checked = false;

			cbSort.SelectedIndex = 0;

			multiContractor.Items.Clear();
			multiGoods.Items.Clear();
			multiKind.Items.Clear();
			multiProducer.Items.Clear();
			multiStore.Items.Clear();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        private void multiContractor_BeforePluginShow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            multiContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", @"(C.ID_CONTRACTOR in(select ID_CONTRACTOR from dbo.CONTRACTOR_2_CONTRACTOR_GROUP where ID_CONTRACTOR_GROUP = (select TOP 1 ID_CONTRACTOR_GROUP from dbo.CONTRACTOR_GROUP where MNEMOCODE='DISTRIBUTOR')))");
            multiContractor.PluginContol.Caption = "Поставщики";
        }

        private void MoveGoodsParams_Load(object sender, EventArgs e)
        {

            multiContractor.AllowSaveState = true;
            multiGoods.AllowSaveState = true;
            multiKind.AllowSaveState = true;
            multiProducer.AllowSaveState = true;
            multiStore.AllowSaveState = true;

            ClearValues();
        }
	}
}