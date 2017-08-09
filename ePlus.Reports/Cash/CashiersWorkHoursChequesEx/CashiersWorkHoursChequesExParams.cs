using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace CashiersWorkHoursChequesEx
{
  public partial class CashiersWorkHoursChequesExParams : ExternalReportForm, IExternalReportFormMethods
  {
    public CashiersWorkHoursChequesExParams()
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

    private DataTable subReportTable;
    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);

      //ucDrugstores.AddItems(root, "GUID_CONTRACTOR");
      foreach (DataRowItem row in ucDrugstores.Items)
      {
        Utils.AddNode(root, "GUID_CONTRACTOR", row.Guid);
      }

      foreach (DataRowItem row in ucCashiers.Items)
      {
        Utils.AddNode(root, "USER_CODE", row.Code);
      }

      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath =
        Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursCheques.rdlc");
      rep.LoadData("REPEX_CASHIERS_WORK_HOURS_CHEQUES", doc.InnerXml);
      rep.BindDataSource("CashiersWorkHoursCheques_DS_Table0", 0);
      rep.BindDataSource("CashiersWorkHoursCheques_DS_Table1", 1);

      rep.ReportPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursCheques.rdlc");
      rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
      subReportTable = rep.DataSource.Tables[0];

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);

      rep.ExecuteReport(this);
    }

    private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
    {
      if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Subreport")
      {
        e.DataSources.Add(new ReportDataSource("CashiersWorkHoursCheques_DS_Table0", subReportTable));
      }
    }

    public string ReportName
    {
      get { return "Количество чеков по кассирам по часам"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
      ucDrugstores.Items.Clear();
      ucCashiers.Items.Clear();
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

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
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
        return;

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      // Аптеки
      XmlNodeList stores = root.SelectNodes("DRUGSTORES");
      foreach (XmlNode node in stores)
      {
        long id = 0;
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucDrugstores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      // Сотрудники АУ
      XmlNodeList users = root.SelectNodes("USERS");
      foreach (XmlNode node in users)
      {
        long id = 0;
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Guid.Empty;
        string code = Utils.GetString(node, "CODE");
        ucCashiers.AddRowItem(new DataRowItem(id, guid, code, text));
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

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      // Аптеки
      foreach (DataRowItem dri in ucDrugstores.Items)
      {
        XmlNode node = Utils.AddNode(root, "DRUGSTORES");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      // Сотрудники АУ
      foreach (DataRowItem dri in ucCashiers.Items)
      {
        XmlNode node = Utils.AddNode(root, "USERS");
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(SettingsFilePath);
    }

    private void CashiersWorkHoursChequesExParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void CashiersWorkHoursChequesExParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}