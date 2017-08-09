using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.SqlClient;
using ePlus.MetaData.Server;
using System.Data;

namespace RCBSupRestsSummNDS
{
  public partial class SupRestsSummNDS : ExternalReportForm, IExternalReportFormMethods
  {
    public SupRestsSummNDS()
    {
      InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE", ucDate.Value);
      ucStores.AddItems(root, "ID_STORE");
      ucDrugstores.AddItems(root, "ID_CONTRACTOR");
      // центр или нет
      //bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", /*selfIsCenter*/true);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "SupRestsSummNDS.rdlc");
      rep.LoadData("REPEX_SUP_RESTS_SUMM_NDS", doc.InnerXml);
      rep.BindDataSource("RCBSupRestsSummNDS_DS_Table", 0);

      rep.AddParameter("date", String.Format("{0:dd.MM.yyyy}", ucDate.Value));
      rep.AddParameter("drugstores", ucDrugstores.TextValues());
      rep.AddParameter("stores", ucStores.TextValues());

      rep.ExecuteReport(this);
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

    private void ClearValues()
    {
      ucDate.Value = DateTime.Now;
      ucStores.Items.Clear();
      ucDrugstores.Items.Clear();
    }

    public string ReportName
    {
      get { return "Оптовые суммы остатков (НДС)"; }
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

      ucDate.Value = Utils.GetDate(root, "DATE");

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
        ucDrugstores.AddRowItem(new DataRowItem(id, guid, code, text));
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

      Utils.AddNode(root, "DATE", ucDate.Value);

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
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

      doc.Save(SettingsFilePath);
    }

    private void ucDrugstores_BeforePluginShow(object sender, System.ComponentModel.CancelEventArgs e)
    {
      //если МЫ не является центром, то делаем плагин неактивным
      //ucDrugstores.Enabled = SelfIsCenter();
    }

    private void ucStores_BeforePluginShow(object sender, System.ComponentModel.CancelEventArgs e)
    {
      /* убираем учет ЦО 
      // Выбираем склады только для МЫ, если МЫ не ЦО
      if (!SelfIsCenter())
      {
        //ucStores.PluginContol.Grid(0).SetParameterValue("@IS_SELF", "1");
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
            String.Format("(STORE.ID_CONTRACTOR IN (SELECT C.ID_CONTRACTOR FROM REPLICATION_CONFIG RC INNER JOIN CONTRACTOR C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1 AND RC.IS_ACTIVE = 1))"));
      }
      // если МЫ = ЦО, отбираем склады только для выбранных аптек или для всех, если список аптек пуст
      else
      {*/
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
      //}
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void SupRestsSummNDS_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void SupRestsSummNDS_Load(object sender, EventArgs e)
    {
      LoadSettings();
      //если МЫ не является центром, то делаем плагин неактивным
      //ucDrugstores.Enabled = SelfIsCenter();
    }
  }
}