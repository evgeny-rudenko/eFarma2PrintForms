using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace MoveGoodsEx
{
	public partial class MoveGoodsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public MoveGoodsParams()
		{
			InitializeComponent();

			multiContractor.AllowSaveState = true;
			multiGoods.AllowSaveState = true;
			multiKind.AllowSaveState = true;
			multiProducer.AllowSaveState = true;
			multiStore.AllowSaveState = true;

			ClearValues();
			cbForm.SelectedIndex = 1;
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

			if (repTypeComboBox.SelectedIndex != 0)
			{
				if (vatCheckedListBox.GetItemChecked(0))
					Utils.AddNode(root, "VAT_RATE", 0);
				if (vatCheckedListBox.GetItemChecked(1))
					Utils.AddNode(root, "VAT_RATE", 10);
				if (vatCheckedListBox.GetItemChecked(2))
					Utils.AddNode(root, "VAT_RATE", 18);
			}

			if (repTypeComboBox.SelectedIndex == 1)
			{
				Utils.AddNode(root, "SUP", 1);
			}

			ReportFormNew rep = new ReportFormNew();

			if (repTypeComboBox.SelectedIndex == 0)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
					cbForm.SelectedIndex == 1 ? "MoveGoods.rdlc" : "MoveGoods_Albom.rdlc");
			}
			else
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
					cbForm.SelectedIndex == 1 ? "MoveGoodsVat.rdlc" : "MoveGoodsVat_Albom.rdlc");
			}

			switch (repTypeComboBox.SelectedIndex)
			{
				case 0:
					rep.LoadData("REPEX_MOVE_GOODS", doc.InnerXml);
					break;
				case 1:
					rep.LoadData("REPEX_MOVE_GOODS_VAT_SUP", doc.InnerXml);
					break;
				case 2:
					rep.LoadData("REPEX_MOVE_GOODS_VAT_SAL", doc.InnerXml);
					break;
			}

			rep.BindDataSource("MoveGoods_DS_Table", 0);
			rep.BindDataSource("MoveGoods_DS_Table1", 1);

			if (repTypeComboBox.SelectedIndex != 0)
			{
				rep.BindDataSource("MoveGoods_DS_Table2", 2);
			}

			rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("PRODUCER", multiProducer.TextValues());
			rep.AddParameter("CONTRACTOR", multiContractor.TextValues());
			rep.AddParameter("STORE", multiStore.TextValues());
			rep.AddParameter("GOODS_KIND", multiKind.TextValues());
			rep.AddParameter("GOODS", multiGoods.TextValues());
			rep.AddParameter("MOVE", checkMove.Checked.ToString());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

			if (repTypeComboBox.SelectedIndex != 0)
			{
				rep.AddParameter("vat_0", vatCheckedListBox.GetItemChecked(0).ToString());
				rep.AddParameter("vat_10", vatCheckedListBox.GetItemChecked(1).ToString());
				rep.AddParameter("vat_18", vatCheckedListBox.GetItemChecked(2).ToString());
			}

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Движение товаров"; }
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
			cbForm.SelectedIndex = 1;

			multiContractor.Items.Clear();
			multiGoods.Items.Clear();
			multiKind.Items.Clear();
			multiProducer.Items.Clear();
			multiStore.Items.Clear();

			repTypeComboBox.SelectedIndex = 0;

			for (int i = 0; i < 3; i++)
			{
				vatCheckedListBox.SetItemChecked(i, true);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void repTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			label3.Enabled = repTypeComboBox.SelectedIndex != 0;
			vatCheckedListBox.Enabled = repTypeComboBox.SelectedIndex != 0;
		}
	}
}