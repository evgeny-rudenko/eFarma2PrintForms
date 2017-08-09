using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.SqlClient;
using ePlus.MetaData.Server;

namespace RemainsWithInsuranceReserve
{
  public partial class RemainsWithInsuranceReserveParams : ExternalReportForm, IExternalReportFormMethods
  {
    public RemainsWithInsuranceReserveParams()
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

      bool selfIsCenter = SelfIsCenter();
      ucStores.AddItems(root, "ID_STORE");
      ucGoods.AddItems(root, "ID_GOODS");
      Utils.AddNode(root, "TYPE", rbWith.Checked ? "WITH" : "LESS");
      Utils.AddNode(root, "CO", selfIsCenter);
      Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);

      // если ЦО то с группировкой, иначе - без
      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = selfIsCenter ?
        Path.Combine(Path.GetDirectoryName(reportFiles[0]), "RemainsWithInsuranceReserve_CO.rdlc") :
        Path.Combine(Path.GetDirectoryName(reportFiles[0]), "RemainsWithInsuranceReserve.rdlc");
      rep.LoadData("REPEX_REMAINS_WITH_INS_RESERVE", doc.InnerXml);
      rep.BindDataSource("RemainsWithInsuranceReserve_DS_Table0", 0);
      rep.BindDataSource("RemainsWithInsuranceReserve_DS_Table1", 1);

      rep.AddParameter("type", rbWith.Checked ? "WITH" : "LESS");

      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      rbWith.Checked = true;
      ucStores.Items.Clear();
      ucGoods.Items.Clear();
      cbRestsOnly.Checked = false;
    }

    public string ReportName
    {
      get { return "Остатки со страховым запасом"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
        return;

      string _type = Utils.GetString(root, "TYPE");
      if (_type == "LESS") rbLess.Checked = true;

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList goods = root.SelectNodes("GOODS");
      foreach (XmlNode node in goods)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucGoods.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      cbRestsOnly.Checked = Utils.GetBool(root, "RESTS_ONLY");
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

      Utils.AddNode(root, "TYPE", rbWith.Checked ? "WITH" : "LESS");

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucGoods.Items)
      {
        XmlNode node = Utils.AddNode(root, "GOODS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void RemainsWithInsuranceReserveParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void RemainsWithInsuranceReserveParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
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

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //если МЫ не является центром, то открываем только те склады, которые относятся к МЫ
      string qry = "";
      string dop = "";
      if (SelfIsCenter())
        dop = " OR (1 = 1)";
      qry = String.Format("((STORE.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)){0})", dop);
      if (!string.IsNullOrEmpty(qry))
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }

    private void rbLess_CheckedChanged(object sender, EventArgs e)
    {
      cbRestsOnly.Enabled = rbLess.Checked;
      ucGoods.Enabled = (((!cbRestsOnly.Checked) && (rbLess.Checked)) || rbWith.Checked);
    }

    private void cbRestsOnly_CheckedChanged(object sender, EventArgs e)
    {
      ucGoods.Enabled = (((!cbRestsOnly.Checked) && (rbLess.Checked)) || rbWith.Checked);
    }
  }
}