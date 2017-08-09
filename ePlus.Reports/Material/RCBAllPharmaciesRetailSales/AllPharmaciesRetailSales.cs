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
using ePlus.MetaData.Server;
using System.Data.SqlClient;

namespace RCBAllPharmaciesRetailSales
{
  public partial class AllPharmaciesRetailSales : ExternalReportForm, IExternalReportFormMethods
  {
    public AllPharmaciesRetailSales()
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

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);
      ucStores.AddItems(root, "ID_STORE");
      ucGoods.AddItems(root, "ID_GOODS");
      ucDrugstores.AddItems(root, "ID_CONTRACTOR");
      foreach (DataRowItem dri in ucGroups.Items)
      {
        Utils.AddNode(root, "GUID_GROUP", dri.Guid);
      }

      Utils.AddNode(root, "BY_GROUPS", cbByGroups.Checked);
      Utils.AddNode(root, "FROM_IO", rbIO.Checked);
      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "AllPharmaciesRetailSales.rdlc");
      rep.LoadData("REPEX_ALL_PHARMACIES_RETAIL_SALES", doc.InnerXml);
      rep.BindDataSource("RCBAllPharmaciesRetailSales_DS_Table", 0);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("drugstores", ucDrugstores.TextValues());

      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      DateTime tm = DateTime.Now;
      ucPeriod.DateTo = tm;
      ucPeriod.DateFrom = tm.AddDays(-tm.Day + 1);
      ucStores.Items.Clear();
      ucGoods.Items.Clear();
      ucDrugstores.Items.Clear();
      ucGroups.Items.Clear();
      cbByGroups.Checked = false;
      rbCheques.Checked = true;
    }

    public string ReportName
    {
      get { return "Розничные продажи по номенклатуре в разрезе всех аптек"; }
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

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList groups = root.SelectNodes("GROUPS");
      foreach (XmlNode node in groups)
      {
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        ucGroups.AddRowItem(new DataRowItem(0, guid, String.Empty, text));
      }

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
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucGoods.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucDrugstores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      cbByGroups.Checked = Utils.GetBool(root, "BY_GROUPS");
      rbIO.Checked = Utils.GetBool(root, "FROM_IO");
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

      foreach (DataRowItem dri in ucGroups.Items)
      {
        XmlNode node = Utils.AddNode(root, "GROUPS");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

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
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucDrugstores.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "BY_GROUPS", cbByGroups.Checked);
      Utils.AddNode(root, "FROM_IO", rbIO.Checked);

      doc.Save(SettingsFilePath);
    }

    private void ucDrugstores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //если МЫ не является центром, то делаем плагин неактивным
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      // Выбираем склады только для МЫ, если МЫ не ЦО
      if (!SelfIsCenter())
      {
        //ucStores.PluginContol.Grid(0).SetParameterValue("@IS_SELF", "1");
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
            String.Format("(STORE.ID_CONTRACTOR IN (SELECT C.ID_CONTRACTOR FROM REPLICATION_CONFIG RC INNER JOIN CONTRACTOR C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1 AND RC.IS_ACTIVE = 1))"));
      }
      // если МЫ = ЦО, отбираем склады только для выбранных аптек или для всех, если список аптек пуст
      else
      {
        if (ucDrugstores.Items.Count > 0)
        {
          string stores = string.Empty;
          foreach (DataRowItem dri in ucDrugstores.Items)
          {
            stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
          }
          if (!String.IsNullOrEmpty(stores))
            ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
              String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));

        }
        else
        {
          ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
              "(1=1)");
        }
      }
    }

    private void cbByGroups_CheckedChanged(object sender, EventArgs e)
    {
      ucGroups.Enabled = cbByGroups.Checked;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void AllPharmaciesRetailSales_Load(object sender, EventArgs e)
    {
      LoadSettings();
      //если МЫ не является центром, то делаем плагин неактивным
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void AllPharmaciesRetailSales_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}