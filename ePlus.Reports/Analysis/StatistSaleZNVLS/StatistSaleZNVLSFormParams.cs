using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;
using System.Data.SqlClient;
using ePlus.MetaData.Server;

namespace RCChStatistSaleZNVLS
{
  public partial class StatistSaleZNVLSFormParams : ExternalReportForm, IExternalReportFormMethods
  {
    public StatistSaleZNVLSFormParams()
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

    public string ReportName
    {
      get { return "Данные об объемах реализации ЖНВЛС"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
    }

    public void Print(string[] reportFiles)
    {
        if (contractors.Items.Count==0)
      {
        MessageBox.Show("Выберите аптеку", "Предупреждение", MessageBoxButtons.OK);
        return;
      }
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      foreach (DataRowItem row in contractors.Items)
      {
          Utils.AddNode(root, "ID_CONTRACTOR", row.Id);
      }
      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "StatistSaleZNVLS.rdlc");
      rep.LoadData("DBO.STATIST_SALE_ZNVLS", doc.InnerXml);
      rep.BindDataSource("STATIST_SALE_ZNVLS_DS_Table", 0);

      rep.AddParameter("Pm_ContractorName", contractors.ToCommaDelimetedStringList());
      rep.AddParameter("Pm_DateFrom", period.DateFrText);
      rep.AddParameter("Pm_DateTo", period.DateToText);
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.ExecuteReport(this);
    }

    private void FormParams_Shown(object sender, EventArgs e)
    {
      //comboSort.DataSource = listSort[0];
      //comboSort.DisplayMember = "Text";
      //comboSort.SelectedIndex = 0;
      //comboRows.DataSource = listRows;
      //comboRows.DisplayMember = "Text";
      //comboRows.SelectedIndex = 0;
    }




      private void FormParams_Load(object sender, EventArgs e)
      {
          if (period != null)
          {
              period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
              period.DateFrom = period.DateTo.AddDays(-13);
          }
          contractors.Clear();
          if (!File.Exists(SettingsFilePath)) return;
          XmlDocument doc = new XmlDocument();
          doc.Load(SettingsFilePath);
          XmlNode root = doc.SelectSingleNode("/XML");
          period.DateFrom = Utils.GetDate(root, "DATE_FROM");
          period.DateTo = Utils.GetDate(root, "DATE_TO");
          XmlNodeList contr = root.SelectNodes("CONTRACTOR");
          foreach (XmlNode node in contr)
          {
              long id = Utils.GetLong(node, "ID");
              string text = Utils.GetString(node, "TEXT");
              Guid guid = Utils.GetGuid(node, "GUID");
              string code = Utils.GetString(node, "CODE");
              contractors.AddRowItem(new DataRowItem(id, guid, code, text));
          }
      }

    private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
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
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      foreach (DataRowItem dri in contractors.Items)
      {
          XmlNode node = Utils.AddNode(root, "CONTRACTOR");
          Utils.AddNode(node, "ID", dri.Id);
          Utils.AddNode(node, "GUID", dri.Guid);
          Utils.AddNode(node, "CODE", dri.Code);
          Utils.AddNode(node, "TEXT", dri.Text);
      }
      doc.Save(SettingsFilePath);
    }

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

      private void contractors_BeforePluginShow(object sender, CancelEventArgs e)
      {
          if (!SelfIsCenter())
              contractors.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
          else
              contractors.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(1=1)");
      }
  }
}