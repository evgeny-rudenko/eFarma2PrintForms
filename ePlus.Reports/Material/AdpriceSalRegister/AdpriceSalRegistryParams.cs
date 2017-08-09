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

namespace RCBAdpriceSalRegister
{
  public partial class AdpriceSalRegistryParams : ExternalReportForm, IExternalReportFormMethods
  {
    public AdpriceSalRegistryParams()
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

      ucPeriod.AddValues(root);
      ucStores.AddItems(root, "ID_STORE");
      ucGoods.AddItems(root, "ID_GOODS");
      ucSuppliers.AddItems(root, "ID_SUPPLIER");
      
      // тип сортировки
      SortTypes st = (SortTypes)ucSort.SelectedItem;
      Utils.AddNode(root, "SORT", st.Value);

      Utils.AddNode(root, "ZNVLS", cbZNVLS.Checked);
      Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);
      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      // если ЦО то с группировкой, иначе - без
      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = selfIsCenter ?
        Path.Combine(Path.GetDirectoryName(reportFiles[0]), "AdpriceSalRegister_CO.rdlc") :
        Path.Combine(Path.GetDirectoryName(reportFiles[0]), "AdpriceSalRegister.rdlc");
      rep.LoadData("REPEX_ADPRICE_SAL_REGISTER", doc.InnerXml);
      rep.BindDataSource("AdpriceSalRegister_DS_Table0", 0);
      rep.BindDataSource("AdpriceSalRegister_DS_Table1", 1);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
      ucStores.Items.Clear();
      ucGoods.Items.Clear();
      ucSuppliers.Items.Clear();
      cbRestsOnly.Checked = false;
      cbZNVLS.Checked = false;
      ucSort.SelectedItem = 0;//(object)new SortTypes("Дата", "DOCUMENT_DATE");
    }

    private void FillSortTypes()
    {
      ucSort.Items.Add((object)new SortTypes("Дата", "DOCUMENT_DATE"));
      ucSort.Items.Add((object)new SortTypes("Название товара", "GOODS_NAME"));
      ucSort.Items.Add((object)new SortTypes("Поставщик", "SUPPLIER_NAME"));
      ucSort.Items.Add((object)new SortTypes("Цена оптовая", "PRICE_SUP"));
      ucSort.Items.Add((object)new SortTypes("% наценки", "ADPRICE_SAL"));
      ucSort.Items.Add((object)new SortTypes("Цена розничная", "PRICE_SAL"));
    }

    public string ReportName
    {
      get { return "Реестр розничной наценки"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
      {
        if (ucSort.SelectedIndex < 0) ucSort.SelectedIndex = 0;
        return;
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
      {
        if (ucSort.SelectedIndex < 0) ucSort.SelectedIndex = 0;
        return;
      }


      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

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

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucSuppliers.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      cbZNVLS.Checked = Utils.GetBool(root, "ZNVLS");
      cbRestsOnly.Checked = Utils.GetBool(root, "RESTS_ONLY");
      ucSort.SelectedIndex = Utils.GetInt(root, "SORT");
      if (ucSort.SelectedIndex < 0) ucSort.SelectedIndex = 0;
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

      foreach (DataRowItem dri in ucSuppliers.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "ZNVLS", cbZNVLS.Checked);
      Utils.AddNode(root, "RESTS_ONLY", cbRestsOnly.Checked);
      Utils.AddNode(root, "SORT", ucSort.SelectedIndex);

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void AdpriceSalRegistryParams_Load(object sender, EventArgs e)
    {
      FillSortTypes();
      LoadSettings();
    }

    private void AdpriceSalRegistryParams_FormClosed(object sender, FormClosedEventArgs e)
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
  }

  public class SortTypes
  {
    private string name;
    private string value;

    public SortTypes(string name, string value)
    {
      this.name = name;
      this.value = value;
    }

    public string Name
    {
      get { return name; }
    }

    public string Value
    {
      get { return value; }
    }

    public override string ToString()
    {
      return name;
    }
  }
}