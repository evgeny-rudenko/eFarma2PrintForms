using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using System.IO;
using System.Data.SqlClient;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;

namespace RCBStockPrognosis
{
  public partial class StockPrognosis : ExternalReportForm, IExternalReportFormMethods
  {
    public StockPrognosis()
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
      // проверяем значения периода
      if (ucPeriod.DateFrText.Length < 10)
      {
        MessageBox.Show("Заполните начальное значение периода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      TimeSpan span = ucPeriod.DateTo - ucPeriod.DateFrom;
      if (span.Days > 200)
      {
        if (MessageBox.Show("Указан очень длинный период. Выполнение запроса займет некоторое время. Продолжить?", "Внимание", 
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            return;
      }

      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);
      ucStores.AddItems(root, "ID_STORE");
      ucGoods.AddItems(root, "ID_GOODS");
      ucDrugstores.AddItems(root, "ID_CONTRACTOR");

      Utils.AddNode(root, "ZERO_PROGNOSIS_GOODS", cbZeroPrognosisGoods.Checked);
      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "StockPrognosis.rdlc");
      rep.LoadData("REPEX_STOCK_PROGNOSIS", doc.InnerXml);
      rep.BindDataSource("RCBStockPrognosis_DS_Table0", 0);
      //rep.BindDataSource("RCBStockPrognosis_DS_Table1", 1);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);

      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
      ucStores.Items.Clear();
      ucGoods.Items.Clear();
      ucDrugstores.Items.Clear();
      cbZeroPrognosisGoods.Checked = false;
    }

    public string ReportName
    {
      get { return "Прогноз по остаткам товаров"; }
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

      cbZeroPrognosisGoods.Checked = Utils.GetBool(root, "ZERO_PROGNOSIS_GOODS");
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
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
      }

      foreach (DataRowItem dri in ucDrugstores.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "ZERO_PROGNOSIS_GOODS", cbZeroPrognosisGoods.Checked);

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void StockPrognosis_Load(object sender, EventArgs e)
    {
      LoadSettings();
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void StockPrognosis_FormClosed(object sender, FormClosedEventArgs e)
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

    private void ucDrugstores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //если МЫ не является центром, то открываем только те склады, которые относятся к МЫ
      string qry = "";
      if (!SelfIsCenter())
      {
        qry = String.Format("(STORE.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
      }
      else
      {
        if (ucDrugstores.Items.Count > 0)
        {
          StringBuilder Contractoritems = new StringBuilder();
          foreach (DataRowItem dri in ucDrugstores.Items)
          {
            if (Contractoritems.Length > 0)
            { Contractoritems.AppendFormat(",{0}", dri.Id); }
            else
            { Contractoritems.AppendFormat("{0}", dri.Id); }
          }
          qry = String.Format("STORE.ID_CONTRACTOR IN ({0})", Contractoritems.ToString());
        }
        else
        {
          qry = "(1=1)";
        }
      }
      if (!string.IsNullOrEmpty(qry))
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }
  }
}