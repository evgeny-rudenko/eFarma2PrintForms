using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;

namespace AP25
{
  public partial class AP25Params : Form, IReportParams, IExternalReport
  {
    public AP25Params()
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

    public bool IsShowPreview
    {
      get { return true; }
    }

    public string HeaderText
    {
      get { return "Товарный отчет УФ АП-25"; }
    }

    public string ReportName
    {
      get { return "УФ АП-25"; }
    }

    public string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
    }

    private void ExtractReport(string repName)
    {
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
      if (!Directory.Exists(cachePath))
        Directory.CreateDirectory(cachePath);
      Stream s = this.GetType().Assembly.GetManifestResourceStream("AP25." + repName + ".rdlc");
      StreamReader sr = new StreamReader(s);
      string rep = sr.ReadToEnd();
      string reportPath = Path.Combine(cachePath, repName + ".rdlc");
      using (StreamWriter sw = new StreamWriter(reportPath))
      {
        sw.Write(rep);
        sw.Flush();
        sw.Close();
      }
    }

    private void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("AP25.REPEX_AP25.sql");
      StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
      string procScript = sr.ReadToEnd();
      string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);

      SqlCommand comm = null;
      foreach (string statement in batch)
      {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
          comm = new SqlCommand(statement, con);
          con.Open();
          comm.ExecuteNonQuery();
        }
      }
    }

    private string connectionString;
    private string folderPath;
    private const string CACHE_FOLDER = "Cache";
    public void Execute(string connectionString, string folderPath)
    {
      this.connectionString = connectionString;
      this.folderPath = folderPath;
      this.MdiParent = AppManager.ClientMainForm;
      AppManager.RegisterForm(this);
      this.Show();
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
      //chkContractorGroup.Checked = false;
      chkShortReport.Checked = false;
      chkRefreshDocMov.Checked = false;
      serviceCheckBox.Checked = true;
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
      //chkContractorGroup.Checked = Utils.GetBool(root, "ContractorGroup");
      chkShortReport.Checked = Utils.GetBool(root, "ShortReport");
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
      //Utils.AddNode(root, "ContractorGroup", chkContractorGroup.Checked);
      Utils.AddNode(root, "ShortReport", chkShortReport.Checked);
      Utils.AddNode(root, "RefreshDocMov", chkRefreshDocMov.Checked);
      Utils.AddNode(root, "Service", serviceCheckBox.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);

      doc.Save(SettingsFilePath);
    }

    private void AP25Params_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void AP25Params_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void bClose_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void bPrint_Click(object sender, EventArgs e)
    {
      if (mpsContractor.Items.Count == 0)
      {
        MessageBox.Show("Выберите контрагента!");
        return;
      }
      CreateStoredProc(connectionString);
      ExtractReport("AP25");
      ExtractReport("AP25_date");
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);

      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML", null);
      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
      foreach (DataRowItem dri in mpsContractor.Items)
        Utils.AddNode(root, "ID_CONTRACTOR", dri.Id);
      foreach (DataRowItem dri in mpsStore.Items)
        Utils.AddNode(root, "ID_STORE", dri.Id);
      Utils.AddNode(root, "SORT_BY_DOCTYPE", this.Sort_By_DocType);
      Utils.AddNode(root, "REFRESH_DOC_MOV", this.chkRefreshDocMov.Checked);
      Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
      Utils.AddNode(root, "CO", SelfIsCenter());

      ReportFormNew rep = new ReportFormNew();

      if (Sort_By_DocType)
      {
        rep.ReportPath = Path.Combine(cachePath, "AP25.rdlc");
      }
      else
      {
        rep.ReportPath = Path.Combine(cachePath, "AP25_date.rdlc");
      }

      if (serviceCheckBox.Checked)
      {
        rep.LoadData("REPEX_AP25_SERVICE", doc.InnerXml);
      }
      else
      {
        rep.LoadData("REPEX_AP25", doc.InnerXml);
      }

      rep.BindDataSource("AP25_DS_Table0", 0);
      rep.BindDataSource("AP25_DS_Table1", 1);
      rep.BindDataSource("AP25_DS_Table2", 2);
      rep.BindDataSource("AP25_DS_Table3", 4);
      rep.BindDataSource("AP25_DS_Table4", 5);
      rep.BindDataSource("AP25_DS_Table5", 3);
      rep.BindDataSource("AP25_DS_Table7", 6);

      rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
      rep.AddParameter("DATE_TO", ucPeriod.DateToText);
      rep.AddParameter("SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
      rep.AddParameter("SHOW_SUB", chkShowSub.Checked ? "1" : "0");
      //rep.AddParameter("CONTRACTOR_GROUP", chkContractorGroup.Checked ? "1" : "0");
      rep.AddParameter("DOCTYPE_GROUP", this.Sort_By_DocType ? "1" : "0");
      rep.AddParameter("SHORT_REPORT", this.chkShortReport.Checked ? "1" : "0");
      rep.AddParameter("SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked ? "1" : "0");
      rep.AddParameter("service", serviceCheckBox.Checked ? "1" : "0");

      rep.ReportFormName = ReportName;
      rep.ExecuteReport(this);
    }

    private void rbDocType_CheckedChanged(object sender, EventArgs e)
    {
      chkShowSumByDocType.Visible = rbDocType.Checked;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
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

    private void mpsContractor_BeforePluginShow(object sender, CancelEventArgs e)
    {
      if (!SelfIsCenter())
        mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
      else
        mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(1=1)");
    }
  }
}