using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using Microsoft.Reporting.WinForms;

namespace CashiersWorkHoursDataEx
{
  public partial class CashiersWorkHoursRevenueExParams : ExternalReportForm, IExternalReportFormMethods
  {
     private DataTable subReportTable;
     private string SettingsFilePath
    {
      get
      {
        return Path.Combine(Utils.TempDir(), ((object) Assembly.GetExecutingAssembly().GetName().Name).ToString() + ".xml");
      }
    }

    public string ReportName
    {
      get
      {
        return "Данные по кассирам по часам";
      }
    }

    public override string GroupName
    {
      get
      {
        return new ReportGroupDescription((ReportGroup) 3).Description;
      }
    }

    public CashiersWorkHoursRevenueExParams()
    {
      this.InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode root = Utils.AddNode(xmlDocument, "XML");
      this.ucPeriod.AddValues(root);
      foreach (DataRowItem current in this.ucDrugstores.Items)
      {
        Utils.AddNode(root, "GUID_CONTRACTOR", current.Guid);
      }
      foreach (DataRowItem current in this.ucCashiers.Items)
      {
        Utils.AddNode(root, "USER_CODE", current.Code);
      }
      bool nodeValue = this.SelfIsCenter();
      Utils.AddNode(root, "CO", nodeValue);
      Utils.AddNode(root, "TYPE_CALC", (long)this.ucTypeCalculation.SelectedIndex);
      ReportFormNew reportFormNew = new ReportFormNew();
      if (this.ucType.SelectedIndex == 0)
      {
        reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursRevenue.rdlc");
        reportFormNew.LoadData("REPEX_CASHIERS_WORK_HOURS_REVENUE", xmlDocument.InnerXml);
        reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table0", 0);
        reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table1", 1);
        reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursRevenue.rdlc");
      }
      else
      {
        if (this.ucType.SelectedIndex == 1)
        {
          reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursCheques.rdlc");
          reportFormNew.LoadData("REPEX_CASHIERS_WORK_HOURS_CHEQUES", xmlDocument.InnerXml);
          reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table0", 0);
          reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table1", 1);
          reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursCheques.rdlc");
        }
        else
        {
          reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursGoodsCount.rdlc");
          reportFormNew.LoadData("REPEX_CASHIERS_WORK_HOURS_GOODS_COUNT", xmlDocument.InnerXml);
          reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table0", 0);
          reportFormNew.BindDataSource("CashiersWorkHoursRevenue_DS_Table1", 1);
          reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CashiersWorkHoursGoodsCount.rdlc");
        }
      }
      reportFormNew.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(this.OnSubReportProc);
      this.subReportTable = reportFormNew.DataSource.Tables[0];
      reportFormNew.AddParameter("date_fr", this.ucPeriod.DateFrText);
      reportFormNew.AddParameter("date_to", this.ucPeriod.DateToText);
      reportFormNew.AddParameter("type_calc", this.ucTypeCalculation.Text);
      reportFormNew.ExecuteReport(this);
    }

    private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
    {
      if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Subreport_1" 
          || Path.GetFileNameWithoutExtension(e.ReportPath) == "Subreport_2" 
          || Path.GetFileNameWithoutExtension(e.ReportPath) == "Subreport_3")
      {
        e.DataSources.Add(new ReportDataSource("CashiersWorkHoursRevenue_DS_Table0", this.subReportTable));
      }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13.0);
      ucDrugstores.Items.Clear();
      ucCashiers.Items.Clear();
      ucType.SelectedIndex = 0;
      ucTypeCalculation.SelectedIndex = 0;
      ucType.Refresh();
    }

    private bool SelfIsCenter()
    {
      bool result = false;
      DataService_BL dataService_BL = new DataService_BL();
      using (SqlConnection sqlConnection = new SqlConnection(dataService_BL.ConnectionString))
      {
        SqlCommand sqlCommand = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", sqlConnection);
        sqlCommand.CommandType = CommandType.Text;
        sqlConnection.Open();
        result = (bool)sqlCommand.ExecuteScalar();
      }
      return result;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      this.ClearValues();
    }

    private void LoadSettings()
    {
      this.ClearValues();
      if (File.Exists(this.SettingsFilePath))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(this.SettingsFilePath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//XML");
        if (xmlNode != null)
        {
          this.ucPeriod.DateFrom = Utils.GetDate(xmlNode, "DATE_FROM");
          this.ucPeriod.DateTo = Utils.GetDate(xmlNode, "DATE_TO");
          XmlNodeList xmlNodeList = xmlNode.SelectNodes("DRUGSTORES");
          foreach (XmlNode node in xmlNodeList)
          {
            long itemId = 0L;
            string @string = Utils.GetString(node, "TEXT");
            Guid itemGuid = Utils.GetGuid(node, "GUID");
            string string2 = Utils.GetString(node, "CODE");
            this.ucDrugstores.AddRowItem(new DataRowItem(itemId, itemGuid, string2, @string));
          }
          XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("USERS");
          foreach (XmlNode node in xmlNodeList2)
          {
            long itemId = 0L;
            string @string = Utils.GetString(node, "TEXT");
            Guid itemGuid = Guid.Empty;
            string string2 = Utils.GetString(node, "CODE");
            this.ucCashiers.AddRowItem(new DataRowItem(itemId, itemGuid, string2, @string));
          }
          this.ucType.SelectedIndex = Utils.GetInt(xmlNode, "TYPE");
          this.ucTypeCalculation.SelectedIndex = Utils.GetInt(xmlNode, "TYPE_CALC");
        }
      }
    }

    private void SaveSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode xmlNode;
      if (File.Exists(this.SettingsFilePath))
      {
        xmlDocument.Load(this.SettingsFilePath);
        xmlNode = xmlDocument.SelectSingleNode("//XML");
        xmlNode.RemoveAll();
      }
      else
      {
        xmlNode = Utils.AddNode(xmlDocument, "XML");
      }
      Utils.AddNode(xmlNode, "DATE_FROM", this.ucPeriod.DateFrom);
      Utils.AddNode(xmlNode, "DATE_TO", this.ucPeriod.DateTo);
      foreach (DataRowItem current in this.ucDrugstores.Items)
      {
        XmlNode root = Utils.AddNode(xmlNode, "DRUGSTORES");
        Utils.AddNode(root, "GUID", current.Guid);
        Utils.AddNode(root, "CODE", current.Code);
        Utils.AddNode(root, "TEXT", current.Text);
      }
      foreach (DataRowItem current in this.ucCashiers.Items)
      {
        XmlNode root = Utils.AddNode(xmlNode, "USERS");
        Utils.AddNode(root, "CODE", current.Code);
        Utils.AddNode(root, "TEXT", current.Text);
      }
      Utils.AddNode(xmlNode, "TYPE", (long)this.ucType.SelectedIndex);
      Utils.AddNode(xmlNode, "TYPE_CALC", (long)this.ucTypeCalculation.SelectedIndex);
      xmlDocument.Save(this.SettingsFilePath);
    }

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
    }

    private void CashiersWorkHoursDataExParams_Load(object sender, EventArgs e)
    {
      this.LoadSettings();
      //если МЫ не является центром, то не позволяем выбирать аптеки
      this.ucDrugstores.Enabled = this.SelfIsCenter();
    }

    private void CashiersWorkHoursDataExParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.SaveSettings();
    }
  }
}
