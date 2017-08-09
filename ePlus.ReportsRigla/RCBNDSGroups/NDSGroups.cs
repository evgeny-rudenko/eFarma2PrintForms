using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using System.Xml;
using ePlus.MetaData.Core;
using System.IO;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;

namespace RCBNDSGroups_Rigla
{
  public partial class NDSGroups : ExternalReportForm, IExternalReportFormMethods
  {
    public NDSGroups()
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

      Utils.AddNode(root, "DATE", ucDate.Value);
      Utils.AddNode(root, "RESTS_ONLY", true);
      //Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);
      Utils.AddNode(root, "IS_NDS_SAL", true);
      //Utils.AddNode(root, "IS_NDS_SAL", rbSal.Checked);
      Utils.AddNode(root, "NO_GROUPS", true);
      //Utils.AddNode(root, "NO_GROUPS", cbNoGroups.Checked);

      ucStores.AddItems(root, "ID_STORE");
      ucContractors.AddItems(root, "ID_CONTRACTOR");
      ucGoodsGroups.AddItems(root, "ID_GOODS_GROUP");
      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      ReportFormNew rep = new ReportFormNew();
      //rep.ReportPath = cbNoGroups.Checked ?
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "NDSGroups_NoGroups.rdlc");// :
        //Path.Combine(Path.GetDirectoryName(reportFiles[0]), "NDSGroups.rdlc");
      rep.LoadData("REPEX_NDS_GROUPS", doc.InnerXml);
      rep.BindDataSource("RCBNDSGroups_DS_Table0", 0);
      rep.BindDataSource("RCBNDSGroups_DS_Table1", 1);

      rep.AddParameter("date", String.Format("{0:dd.MM.yyyy}", ucDate.Value));
      rep.AddParameter("contractors", ucContractors.TextValues());
      rep.AddParameter("stores", ucStores.TextValues());
      rep.AddParameter("goods_groups", string.Empty); //ucGoodsGroups.TextValues()
      rep.AddParameter("user_name", SecurityContextEx.USER_NAME);
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.ExecuteReport(this);
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
    
    private void ClearValues()
    {
      ucDate.Value = DateTime.Now;
      ucStores.Items.Clear();
      ucContractors.Items.Clear();
      ucGoodsGroups.Items.Clear();
      cbRestsOnly.Checked = true;
      rbSup.Checked = true;
      cbNoGroups.Checked = false;
    }

    public string ReportName
    {
      get { return "Отчет по группам НДС (Ригла)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
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

      ucDate.Value = Utils.GetDate(root, "DATE");
      //cbRestsOnly.Checked = Utils.GetBool(root, "RESTS_ONLY");
      //bool ndsSal = Utils.GetBool(root, "NDS_SAL");
      //if (ndsSal)
      //{ rbSal.Checked = true; }
      //else
      //{ rbSup.Checked = true; }
      //cbNoGroups.Checked = Utils.GetBool(root, "NO_GROUPS");

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucContractors.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      //XmlNodeList goods_groups = root.SelectNodes("GOODS_GROUP");
      //foreach (XmlNode node in goods_groups)
      //{
      //  long id = Utils.GetLong(node, "ID");
      //  string text = Utils.GetString(node, "TEXT");
      //  Guid guid = Utils.GetGuid(node, "GUID");
      //  string code = String.Empty;
      //  ucGoodsGroups.AddRowItem(new DataRowItem(id, guid, code, text));
      //}
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

      Utils.AddNode(root, "DATE", ucDate.Value);
      //Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);
      //Utils.AddNode(root, "NDS_SAL", rbSal.Checked);
      //Utils.AddNode(root, "NO_GROUPS", cbNoGroups.Checked);

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      //foreach (DataRowItem dri in ucGoodsGroups.Items)
      //{
      //  XmlNode node = Utils.AddNode(root, "GOODS_GROUP");
      //  Utils.AddNode(node, "ID", dri.Id);
      //  Utils.AddNode(node, "GUID", dri.Guid);
      //  Utils.AddNode(node, "TEXT", dri.Text);
      //}

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void ucContractors_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //если МЫ не является центром, то делаем плагин неактивным
      //ucContractors.Enabled = SelfIsCenter();
    }

    private void NDSGroups_Load(object sender, EventArgs e)
    {
      LoadSettings();
      //если МЫ не является центром, то делаем плагин неактивным
      //ucContractors.Enabled = SelfIsCenter();
    }

    private void NDSGroups_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
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
        if (ucContractors.Items.Count > 0)
        {
          string stores = string.Empty;
          foreach (DataRowItem dri in ucContractors.Items)
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
  }
}