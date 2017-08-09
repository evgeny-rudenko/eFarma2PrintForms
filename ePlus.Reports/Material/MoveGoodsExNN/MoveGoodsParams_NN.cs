using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace R18MMoveGoodsNN
{
	public partial class MoveGoodsParams_NN : ExternalReportForm, IExternalReportFormMethods
	{
		public MoveGoodsParams_NN()
		{
			InitializeComponent();

		}

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "MOV", checkMove.Checked ? "1" : "0");
			Utils.AddNode(root, "ONLY_INVOICE", checkInvoice.Checked ? "1" : "0");
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
			if (repTypeComboBox.SelectedIndex == 0)
			{
				Utils.AddNode(root, "GG", ggCheckBox.Checked ? "1" : "0");
			}

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
					cbForm.SelectedIndex == 1 ? "MoveGoods_NN.rdlc" : "MoveGoods_Albom_NN.rdlc");
			}
			else
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
					cbForm.SelectedIndex == 1 ? "MoveGoodsVat_NN.rdlc" : "MoveGoodsVat_Albom_NN.rdlc");
			}

			switch (repTypeComboBox.SelectedIndex)
			{
				case 0:
					rep.LoadData("REPEX_MOVE_GOODS_NN", doc.InnerXml);
					break;
				case 1:
					rep.LoadData("REPEX_MOVE_GOODS_NN_VAT_SUP", doc.InnerXml);
					break;
				case 2:
					rep.LoadData("REPEX_MOVE_GOODS_NN_VAT_SAL", doc.InnerXml);
					break;
			}

			rep.BindDataSource("MoveGoods_NN_DS_Table", 0);
			rep.BindDataSource("MoveGoods_NN_DS_Table1", 1);
      rep.BindDataSource("MoveGoods_NN_DS_Table3", 2);

			if (repTypeComboBox.SelectedIndex != 0)
			{
				rep.BindDataSource("MoveGoods_NN_DS_Table2", 3);
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
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Движение товаров НН"; }
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
			ggCheckBox.Checked = false;
      chbGoodCode.Checked = false;
      auCheckBox.Checked = false;

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

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
      {
        return;
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
      {
        return;
      }

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      periodLot.DateFrom = Utils.GetDate(root, "DATE_LOT_FROM");
      periodLot.DateTo = Utils.GetDate(root, "DATE_LOT_TO");
      
      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        multiContractor.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        multiStore.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList goods = root.SelectNodes("GOODS");
      foreach (XmlNode node in goods)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        multiGoods.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList producers = root.SelectNodes("PRODUCER");
      foreach (XmlNode node in producers)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = string.Empty;
        multiProducer.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList goodsKind = root.SelectNodes("GOODS_KIND");
      foreach (XmlNode node in goodsKind)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Guid.Empty;
        string code = string.Empty;
        multiKind.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      checkMove.Checked = Utils.GetBool(root, "Move");
      chkUseLotDate.Checked = Utils.GetBool(root, "UseLotDate");
      checkInvoice.Checked = Utils.GetBool(root, "Invoice");
      ggCheckBox.Checked = Utils.GetBool(root, "GG");
      chbGoodCode.Checked = Utils.GetBool(root, "GoodCode");
      auCheckBox.Checked = Utils.GetBool(root, "AU");

      cbSort.SelectedIndex = Utils.GetInt(root, "Sort");
      cbForm.SelectedIndex = Utils.GetInt(root, "Form");
      repTypeComboBox.SelectedIndex = Utils.GetInt(root, "repType");

      vatCheckedListBox.SetItemChecked(0, Utils.GetBool(root, "vat_0"));
      vatCheckedListBox.SetItemChecked(1, Utils.GetBool(root, "vat_10"));
      vatCheckedListBox.SetItemChecked(2, Utils.GetBool(root, "vat_18"));
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root;

      if (File.Exists(SettingsFilePath))
      {
        doc.Load(SettingsFilePath);
        root = doc.SelectSingleNode("//XML");
        root.RemoveAll();
      }
      else
      {
        root = Utils.AddNode(doc, "XML");
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      Utils.AddNode(root, "DATE_LOT_FROM", periodLot.DateFrom);
      Utils.AddNode(root, "DATE_LOT_TO", periodLot.DateTo);

      foreach (DataRowItem dri in multiContractor.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in multiStore.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in multiGoods.Items)
      {
        XmlNode node = Utils.AddNode(root, "GOODS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
      }

      foreach (DataRowItem dri in multiProducer.Items)
      {
        XmlNode node = Utils.AddNode(root, "PRODUCER");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
        Utils.AddNode(node, "GUID", dri.Guid);
      }

      foreach (DataRowItem dri in multiKind.Items)
      {
        XmlNode node = Utils.AddNode(root, "GOODS_KIND");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "Move", checkMove.Checked);
      Utils.AddNode(root, "UseLotDate", chkUseLotDate.Checked);
      Utils.AddNode(root, "Invoice", checkInvoice.Checked);
      Utils.AddNode(root, "GG", ggCheckBox.Checked);
      Utils.AddNode(root, "GoodCode", chbGoodCode.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);

      Utils.AddNode(root, "Sort", cbSort.SelectedIndex);
      Utils.AddNode(root, "Form", cbForm.SelectedIndex);
      Utils.AddNode(root, "repType", repTypeComboBox.SelectedIndex);

      Utils.AddNode(root, "vat_0", vatCheckedListBox.GetItemChecked(0));
      Utils.AddNode(root, "vat_10", vatCheckedListBox.GetItemChecked(1));
      Utils.AddNode(root, "vat_18", vatCheckedListBox.GetItemChecked(2));

      doc.Save(SettingsFilePath);
    }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void repTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			label3.Enabled = repTypeComboBox.SelectedIndex != 0;
			vatCheckedListBox.Enabled = repTypeComboBox.SelectedIndex != 0;
			ggCheckBox.Enabled = repTypeComboBox.SelectedIndex == 0;
		}

        private void MoveGoodsParams_NN_Load(object sender, EventArgs e)
        {

            multiContractor.AllowSaveState = true;
            multiGoods.AllowSaveState = true;
            multiKind.AllowSaveState = true;
            multiProducer.AllowSaveState = true;
            multiStore.AllowSaveState = true;

            ClearValues();
            cbForm.SelectedIndex = 1;

            LoadSettings();
        }

    private void MoveGoodsParams_NN_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
	}
}