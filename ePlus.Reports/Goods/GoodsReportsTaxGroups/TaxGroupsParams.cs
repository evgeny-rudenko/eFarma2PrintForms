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
using System.IO;
using ePlus.MetaData.Server;
using System.Data.SqlClient;

namespace RCBGoodsReportsTaxGroups
{
	public partial class TaxGroupsParams : ExternalReportForm, IExternalReportFormMethods
	{
		public TaxGroupsParams()
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

    /// <summary>
    /// Метод для получения подтверждения или опровержения того, что МЫ является центром
    /// </summary>
    /// <returns>true - ЦЕНТР, false - не ЦЕНТР</returns>
    private bool SelfIsCenter()
    {
      bool result = false;
      DataService_BL bl = new DataService_BL();

      using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
      {
        SqlCommand command = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", connection);
        command.CommandType = CommandType.Text;
        connection.Open();
        result = (bool)command.ExecuteScalar();
      }
      return result;
    }

    private bool Sort_By_DocType
    {
      get { return rbDocType.Checked && !rbDocDate.Checked; }
      set
      {
        rbDocType.Checked = value;
        rbDocDate.Checked = !value;
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
                Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
                Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
                Utils.AddNode(root, "DIS", cbNDS.Checked ? "1" : "0");
                Utils.AddNode(root, "CO", SelfIsCenter());

                if (rbDocType.Checked)
                    Utils.AddNode(root, "SORT_DOC", 1);  //по видам док
                else Utils.AddNode(root, "SORT_DOC", 0);  //по датам док

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
                rep.BindDataSource("Goods_Reports_TO_DS_Table6", 6);

                rep.AddParameter("date_fr", ucPeriod.DateFrText);
                rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
                rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
                rep.AddParameter("show_nal", chbShowNal.Checked ? "1" : "0");
                rep.AddParameter("service", serviceCheckBox.Checked ? "1" : "0");
                rep.AddParameter("disNDS", cbNDS.Checked ? "1" : "0");
                rep.AddParameter("DOCTYPE_GROUP", this.Sort_By_DocType ? "1" : "0");
                rep.AddParameter("SHORT_REPORT", this.chkShortReport.Checked ? "1" : "0");
                rep.AddParameter("SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked ? "1" : "0");
                rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
                rep.ExecuteReport(this);
            }
            else MessageBox.Show("Выберите контрагента!");
        }

		public string ReportName
		{
			get { return "Товарный отчет по налоговым группам"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
      mpsContractor.Items.Clear();
      mpsStore.Items.Clear();
      chbShowNal.Checked = false;
      chkRefreshDocMov.Checked = false;
      serviceCheckBox.Checked = true;
      chkShortReport.Checked = false;
      auCheckBox.Checked = false;
      cbNDS.Checked = false;
      rbDocType.Checked = true;
      chkShowSumByDocType.Checked = false;
      chkShowAdd.Checked = true;
      chkShowSub.Checked = true;
      supVatRadioButton.Checked = true;
		}

		public override string GroupName 
		{
			get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
		}

    private void LoadSettings()
    {
      SetDefaultValues();
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

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        mpsContractor.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        mpsStore.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      chbShowNal.Checked = Utils.GetBool(root, "ShowNal");
      chkRefreshDocMov.Checked = Utils.GetBool(root, "RefreshDocMov");
      serviceCheckBox.Checked = Utils.GetBool(root, "service");
      chkShortReport.Checked = Utils.GetBool(root, "ShortReport");
      auCheckBox.Checked = Utils.GetBool(root, "AU");
      cbNDS.Checked = Utils.GetBool(root, "NDS");
      if (Utils.GetBool(root, "DocType"))
      { rbDocType.Checked = true; }
      else
      { rbDocDate.Checked = true; }
      chkShowSumByDocType.Checked = Utils.GetBool(root, "ShowSumByDocType");
      chkShowAdd.Checked = Utils.GetBool(root, "ShowAdd");
      chkShowSub.Checked = Utils.GetBool(root, "ShowSub");
      if (Utils.GetBool(root, "supVat"))
      { supVatRadioButton.Checked = true; }
      else
      { retVatRadioButton.Checked = true; }
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

      foreach (DataRowItem dri in mpsContractor.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in mpsStore.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "ShowNal", chbShowNal.Checked);
      Utils.AddNode(root, "RefreshDocMov", chkRefreshDocMov.Checked);
      Utils.AddNode(root, "service", serviceCheckBox.Checked);
      Utils.AddNode(root, "ShortReport", chkShortReport.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);
      Utils.AddNode(root, "NDS", cbNDS.Checked);
      Utils.AddNode(root, "DocType", rbDocType.Checked);
      Utils.AddNode(root, "ShowSumByDocType", chkShowSumByDocType.Checked);
      Utils.AddNode(root, "ShowAdd", chkShowAdd.Checked);
      Utils.AddNode(root, "ShowSub", chkShowSub.Checked);
      Utils.AddNode(root, "supVat", supVatRadioButton.Checked);

      doc.Save(SettingsFilePath);
    }

    private void TaxGroupsParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void TaxGroupsParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      SetDefaultValues();
    }

    private void mpsContractor_BeforePluginShow(object sender, CancelEventArgs e)
    {
      if (!SelfIsCenter())
        mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
      else
        mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(1=1)");
    }

    private void mpsStore_BeforePluginShow(object sender, CancelEventArgs e)
    {
      if (mpsContractor.Items.Count > 0)
      {
        string stores = string.Empty;
        foreach (DataRowItem dri in mpsContractor.Items)
        {
          stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
        }
        if (!String.IsNullOrEmpty(stores))
          mpsStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
            String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));
      }
      else
      {
        e.Cancel = true;
        MessageBox.Show("Выберите контрагента!");
      }
    }

    private void rbDocType_CheckedChanged(object sender, EventArgs e)
    {
      chkShowSumByDocType.Enabled = rbDocType.Checked;
      if (!chkShowSumByDocType.Enabled)
        chkShowSumByDocType.Checked = false;
    }
	}
}