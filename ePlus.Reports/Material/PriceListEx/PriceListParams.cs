using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace RCBPriceList
{
	public partial class PriceListParams : ExternalReportForm, IExternalReportFormMethods
	{
		public PriceListParams()
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
			XmlNode root = Utils.AddNode(doc, "XML", null);

			ucGoods.AddItems(root, "ID_GOODS");
			ucStore.AddItems(root, "ID_STORE");

			Utils.AddNode(root, "DATE_OST", Utils.SqlDate(dateDateTimePicker.Value));
			Utils.AddNode(root, "SORT", cbSort.SelectedIndex);

      Utils.AddNode(root, "SHOW_RESERVE", (checkedListBox1.GetItemChecked(16) && cbForm.SelectedIndex == 1));

			ReportFormNew rep = new ReportFormNew();

            if (cbForm.SelectedIndex == 0)
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "PriceList1Ex.rdlc");
			if (cbForm.SelectedIndex == 1)
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "PriceList2Ex.rdlc");

			rep.LoadData("REPEX_PRICE_LIST", doc.InnerXml);
			rep.BindDataSource("PriceListDS_Table", 0);
			rep.BindDataSource("PriceListDS_Table1", 1);

			string p_ch = "";
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
			    if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
			}

			rep.AddParameter("p_ch", p_ch);
			rep.AddParameter("STORE", ucStore.TextValues());
			rep.AddParameter("DATE_OST", string.Format("Прайс-лист на {0}", dateDateTimePicker.Value.ToString("dd.MM.yyyy")));
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Прайс-лист"; }
		}

		private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkedListBox1.Enabled = cbForm.SelectedIndex == 1;
		}

		private void ClearValues()
		{
			cbSort.SelectedIndex = 0;
			cbForm.SelectedIndex = 0;

			dateDateTimePicker.Value = DateTime.Now;

			ucGoods.Items.Clear();
			ucStore.Items.Clear();

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkedListBox1.Enabled = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
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

      dateDateTimePicker.Value = Utils.GetDate(root, "DATE");
      cbForm.SelectedIndex = Utils.GetInt(root, "FORM");
      cbSort.SelectedIndex = Utils.GetInt(root, "SORT");

      string p_ch = Utils.GetString(root, "FIELDS");
      for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
      {
        checkedListBox1.SetItemChecked(i, p_ch[i] == '1');
      }

      XmlNodeList goods = root.SelectNodes("GOODS");
      foreach (XmlNode node in goods)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucGoods.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStore.AddRowItem(new DataRowItem(id, guid, code, text));
      }
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

      Utils.AddNode(root, "DATE", dateDateTimePicker.Value);
      Utils.AddNode(root, "FORM", cbForm.SelectedIndex);
      Utils.AddNode(root, "SORT", cbSort.SelectedIndex);

      string p_ch = "";
      for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
      {
        if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
      }
      Utils.AddNode(root, "FIELDS", p_ch);

      foreach (DataRowItem dri in ucGoods.Items)
      {
        XmlNode node = Utils.AddNode(root, "GOODS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
      }

      foreach (DataRowItem dri in ucStore.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(SettingsFilePath);
    }

    private void PriceListParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void PriceListParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
	}
}