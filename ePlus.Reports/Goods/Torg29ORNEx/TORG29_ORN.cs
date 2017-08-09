using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;
using System.Data.SqlClient;


namespace Torg29ORNEx
{
  public partial class TORG29_ORN : ExternalReportForm, IExternalReportFormMethods
  {
    public TORG29_ORN()
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

    /*private bool Sort_By_DocType
    {
      get { return rbDocType.Checked && !rbDocDate.Checked; }
      set
      {
        rbDocType.Checked = value;
        rbDocDate.Checked = !value;
      }
    }*/

    public void Print(string[] reportFiles)
    {
      if (mpsContractor.Items.Count != 0)
      {
        XmlDocument doc = new XmlDocument();
        XmlNode root = Utils.AddNode(doc, "XML");
        Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
        Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
        //Utils.AddNode(root, "NO_DETAIL", chShortReport.Checked ? "1" : "0");
        Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
        Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
        Utils.AddNode(root, "SORT_DOC", rbDocType.Checked ? "1" : "0");
        Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
        Utils.AddNode(root, "CO", SelfIsCenter());

        foreach (DataRowItem dr in mpsContractor.Items)
          Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

        foreach (DataRowItem dr in mpsStore.Items)
          Utils.AddNode(root, "ID_STORE", dr.Id);
        Utils.AddNode(root, "REFRESH_DOC_MOV", chkRefreshDocMov.Checked ? "1" : "0");

        ReportFormNew rep = new ReportFormNew();

        //rep.ReportPath = rbDocDate.Checked ? reportFiles[1] : reportFiles[0];

        rep.ReportPath = rbDocDate.Checked ? Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TORG29_NAL_BY_DATE.rdlc") : Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TORG29_NAL.rdlc");

        if (serviceCheckBox.Checked)
        {
          rep.LoadData("REP_GOODS_REPORTS_NAL_EX_SERVICE", doc.InnerXml);
        }
        else
        {
          rep.LoadData("REP_GOODS_REPORTS_NAL_EX", doc.InnerXml);
        }

        rep.BindDataSource("GoodsReportsNal_DS_dtBegin", 0);
        rep.BindDataSource("GoodsReportsNal_DS_dtAdd", 1);
        rep.BindDataSource("GoodsReportsNal_DS_dtSub", 2);
        rep.BindDataSource("GoodsReportsNal_DS_dtEnd", 4);
        rep.BindDataSource("GoodsReportsNal_DS_dtContractor", 5);
        rep.BindDataSource("GoodsReportsNal_DS_dtSumDis", 3);
        rep.BindDataSource("GoodsReportsNal_DS_Table0", 6);

        rep.AddParameter("date_fr", ucPeriod.DateFrText);
        rep.AddParameter("date_to", ucPeriod.DateToText);
        rep.AddParameter("no_detail", chShortReport.Checked ? "1" : "0");
        rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
        rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
        rep.AddParameter("service", serviceCheckBox.Checked ? "1" : "0");
        //rep.AddParameter("DOCTYPE_GROUP", this.Sort_By_DocType ? "1" : "0");
        rep.AddParameter("SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked ? "1" : "0");
        rep.ExecuteReport(this);
      }
      else MessageBox.Show("Выберите контрагента!");
    }

    public string ReportName
    {
      get { return "Торг29 Опт-розница-наложение"; }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
      ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
      mpsContractor.Items.Clear();
      mpsStore.Items.Clear();
      rbDocType.Checked = true;
      chkShowSumByDocType.Checked = false;
      chkShowAdd.Checked = true;
      chkShowSub.Checked = true;
      chShortReport.Checked = false;
      chkRefreshDocMov.Checked = false;
      serviceCheckBox.Checked = false;
      auCheckBox.Checked = false;
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

      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
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

      if (Utils.GetBool(root, "DocType"))
        rbDocType.Checked = true;
      else
        rbDocDate.Checked = true;

      chkShowSumByDocType.Checked = Utils.GetBool(root, "ShowSumByDocType");
      chkShowAdd.Checked = Utils.GetBool(root, "ShowAdd");
      chkShowSub.Checked = Utils.GetBool(root, "ShowSub");
      chShortReport.Checked = Utils.GetBool(root, "ShortReport");
      chkRefreshDocMov.Checked = Utils.GetBool(root, "RefreshDocMov");
      serviceCheckBox.Checked = Utils.GetBool(root, "Service");
      auCheckBox.Checked = Utils.GetBool(root, "AU");
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
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
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

      Utils.AddNode(root, "DocType", rbDocType.Checked);
      Utils.AddNode(root, "ShowSumByDocType", chkShowSumByDocType.Checked);
      Utils.AddNode(root, "ShowAdd", chkShowAdd.Checked);
      Utils.AddNode(root, "ShowSub", chkShowSub.Checked);
      Utils.AddNode(root, "ShortReport", chShortReport.Checked);
      Utils.AddNode(root, "RefreshDocMov", chkRefreshDocMov.Checked);
      Utils.AddNode(root, "Service", serviceCheckBox.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);

      doc.Save(SettingsFilePath);
    }

    private void TORG29_ORN_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void TORG29_ORN_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    public override string GroupName
    {
      get
      {
        return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
      }
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void rbDocType_CheckedChanged(object sender, EventArgs e)
    {
      chkShowSumByDocType.Enabled = rbDocType.Checked;
      if (!chkShowSumByDocType.Enabled)
        chkShowSumByDocType.Checked = false;
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
  }
}