using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using System.Xml;
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using ePlus.CommonEx.Reporting;
using System.IO;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.MetaData.Client.PluginFilter;

namespace RCBCheques
{
  public partial class Cheques : ExternalReportForm, IExternalReportFormMethods
  {
    public Cheques()
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
      if (timeCheckBox.Checked)
      {
        Utils.AddNode(root, "TIME_FR", fromTimeDateTimePicker.Value.ToShortTimeString());
				Utils.AddNode(root, "TIME_TO", toTimeDateTimePicker.Value.ToShortTimeString());
      }

      ucContractors.AddItems(root, "ID_CONTRACTOR");
      ucStores.AddItems(root, "ID_STORE");
      ucCheques.AddItems(root, "ID_CHEQUE");
      ucKKM.AddItems(root, "ID_CASH_REGISTER");
      ucKKMUsers.AddItems(root, "ID_USER");

      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "RCBCheques.rdlc");
      rep.LoadData("REPEX_CHEQUES", doc.InnerXml);
      rep.BindDataSource("RCBCheques_DS_Table0", 0);

      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Отчет по чекам"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
      timeCheckBox.Checked = false;
      toTimeDateTimePicker.Value = DateTime.Now;
      fromTimeDateTimePicker.Value = fromTimeDateTimePicker.Value.AddHours(-1);

      ucCheques.Items.Clear();
      ucContractors.Items.Clear();
      ucKKM.Items.Clear();
      ucKKMUsers.Items.Clear();
      ucStores.Items.Clear();

      ucContractors.Enabled = SelfIsCenter();
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

      timeCheckBox.Checked = Utils.GetBool(root, "TIME_USED");
      if (timeCheckBox.Checked)
      {
        fromTimeDateTimePicker.Value = Utils.GetDate(root, "TIME_FROM");
        toTimeDateTimePicker.Value = Utils.GetDate(root, "TIME_TO");
      }

      // Аптеки
      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucContractors.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      // Склады
      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      // Кассы
      XmlNodeList kkm = root.SelectNodes("KKM");
      foreach (XmlNode node in kkm)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Guid.Empty;
        string code = String.Empty;
        ucKKM.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      // Кассиры
      XmlNodeList users = root.SelectNodes("USERS");
      foreach (XmlNode node in users)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Guid.Empty;
        string code = String.Empty;
        ucKKMUsers.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      // Чеки
      XmlNodeList goods = root.SelectNodes("CHEQUES");
      foreach (XmlNode node in goods)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = String.Empty;
        ucCheques.AddRowItem(new DataRowItem(id, guid, code, text));
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

      Utils.AddNode(root, "TIME_USED", timeCheckBox.Checked);
      if (timeCheckBox.Checked)
      {
        Utils.AddNode(root, "TIME_FROM", fromTimeDateTimePicker.Value);
        Utils.AddNode(root, "TIME_TO", toTimeDateTimePicker.Value);
      }

      // Аптеки
      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      // Склады
      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      // Кассы
      foreach (DataRowItem dri in ucKKM.Items)
      {
        XmlNode node = Utils.AddNode(root, "KKM");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      // Кассиры
      foreach (DataRowItem dri in ucKKMUsers.Items)
      {
        XmlNode node = Utils.AddNode(root, "USERS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      // Чеки
      foreach (DataRowItem dri in ucCheques.Items)
      {
        XmlNode node = Utils.AddNode(root, "CHEQUES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(SettingsFilePath);
    }

    private void toTimeDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
      if (toTimeDateTimePicker.Value < fromTimeDateTimePicker.Value)
        toTimeDateTimePicker.Value = fromTimeDateTimePicker.Value;
    }

    private void fromTimeDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
      if (toTimeDateTimePicker.Value < fromTimeDateTimePicker.Value)
        fromTimeDateTimePicker.Value = toTimeDateTimePicker.Value;
    }

    private void timeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      fromTimeDateTimePicker.Enabled = timeCheckBox.Checked;
      toTimeDateTimePicker.Enabled = timeCheckBox.Checked;
    }

    private void Cheques_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void Cheques_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void ucCheques_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //открываем чеки по указанным фильтрам
      string qry = "";

      // ККМ
      StringBuilder KKMitems = new StringBuilder();
      if (ucKKM.Items.Count > 0)
      {
        foreach (DataRowItem dri in ucKKM.Items)
        {
          if (KKMitems.Length > 0)
            {KKMitems.AppendFormat(",{0}", dri.Id);}
          else 
            {KKMitems.AppendFormat("{0}", dri.Id);}
        }
        qry = String.Format("CS.ID_CASH_REGISTER IN ({0})", KKMitems.ToString());
      }

      // кассиры
      StringBuilder KKMUseritems = new StringBuilder();
      if (ucKKMUsers.Items.Count > 0)
      {
        foreach (DataRowItem dri in ucKKMUsers.Items)
        {
          if (KKMUseritems.Length > 0)
          { KKMUseritems.AppendFormat(",{0}", dri.Id); }
          else
          { KKMUseritems.AppendFormat("{0}", dri.Id); }
        }
        qry = String.IsNullOrEmpty(qry) ? String.Format("CS.ID_USER_DATA IN ({0})", KKMUseritems.ToString()) :
          String.Format("{0} AND CS.ID_USER_DATA IN ({1})", qry, KKMUseritems.ToString());
      }

      string stores = String.Empty;
      string contractors = String.Empty;
      if (ucContractors.Items.Count > 0 || ucStores.Items.Count > 0)
      {
        // склады
        StringBuilder Storeitems = new StringBuilder();
        if (ucStores.Items.Count > 0)
        {
          foreach (DataRowItem dri in ucStores.Items)
          {
            if (Storeitems.Length > 0)
            { Storeitems.AppendFormat(",{0}", dri.Id); }
            else
            { Storeitems.AppendFormat("{0}", dri.Id); }
          }
          stores = String.Format("L.ID_STORE IN ({0})", Storeitems.ToString());
        }
        if (String.IsNullOrEmpty(stores))
          stores = "(1=1)";

        // аптеки
        StringBuilder Contractoritems = new StringBuilder();
        if (ucContractors.Items.Count > 0)
        {
          foreach (DataRowItem dri in ucContractors.Items)
          {
            if (Contractoritems.Length > 0)
            { Contractoritems.AppendFormat(",{0}", dri.Id); }
            else
            { Contractoritems.AppendFormat("{0}", dri.Id); }
          }
          contractors = String.Format("S.ID_CONTRACTOR IN ({0})", Contractoritems.ToString());
        }
        if (String.IsNullOrEmpty(contractors))
          contractors = "(1=1)";
        
        // добавляем фильтры
        qry = String.IsNullOrEmpty(qry) ? String.Format("EXISTS (SELECT NULL FROM CHEQUE_ITEM CI INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CI.ID_LOT_GLOBAL INNER JOIN STORE S ON L.ID_STORE = S.ID_STORE WHERE CI.ID_CHEQUE_GLOBAL = C.ID_CHEQUE_GLOBAL AND {0} AND {1})", stores, contractors) :
          String.Format("{0} AND EXISTS (SELECT NULL FROM CHEQUE_ITEM CI INNER JOIN LOT L ON L.ID_LOT_GLOBAL = CI.ID_LOT_GLOBAL INNER JOIN STORE S ON L.ID_STORE = S.ID_STORE WHERE CI.ID_CHEQUE_GLOBAL = C.ID_CHEQUE_GLOBAL AND {1} AND {2})", qry, stores, contractors);
      }

      // время
      if (timeCheckBox.Checked)
      {
        int time_fr, time_to;
        time_fr = fromTimeDateTimePicker.Value.Hour * 60 + ucPeriod.DateFrom.Minute;
        time_to = toTimeDateTimePicker.Value.Hour * 60 + ucPeriod.DateTo.Minute;
        qry = String.IsNullOrEmpty(qry) ? String.Format("(DATEPART(HOUR, C.DATE_CHEQUE)*60+DATEPART(MINUTE, C.DATE_CHEQUE)) BETWEEN {0} AND {1}", time_fr, time_to) :
          String.Format("{0} AND (DATEPART(HOUR, C.DATE_CHEQUE)*60+DATEPART(MINUTE, C.DATE_CHEQUE)) BETWEEN {1} AND {2}", qry, time_fr, time_to);
      }

      if (!string.IsNullOrEmpty(qry))
        ucCheques.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
      
      // период дат
      DateTime date_from = ucPeriod.DateFrom;
      DateTime date_to = ucPeriod.DateTo;
      //RangeDate(ref date_from, ref date_to);
      SimpleFilter fff = (SimpleFilter)ucCheques.PluginContol.FindControl(typeof(SimpleFilter));
      foreach (IFilterItem fi in fff.ItemList)
      {
        if (fi is FilterItemPeriod)
        {
          XmlDocument doc = new XmlDocument();
          XmlNode root = Utils.AddNode(doc, "XML");
          Utils.AddNode(root, "DateFr", date_from);
          Utils.AddNode(root, "DateTo", date_to);

          fi.LoadSessings(root);
        }
      }
    }

    private void RangeDate(ref DateTime _date_from, ref DateTime _date_to)
    {
      _date_from = _date_from.AddHours(-_date_from.Hour);
      _date_from = _date_from.AddMinutes(-_date_from.Minute);
      _date_from = _date_from.AddMilliseconds(-_date_from.Millisecond);

      _date_to = _date_to.AddHours(-_date_to.Hour);
      _date_to = _date_to.AddMinutes(-_date_to.Minute);
      _date_to = _date_to.AddMilliseconds(-_date_to.Millisecond);
      _date_to = (_date_to.AddDays(1)).AddMilliseconds(-1);
    }

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //если МЫ не является центром, то открываем только те склады, которые относятся к МЫ
      string qry = "";
      string dop = "";
      if (SelfIsCenter())
        dop = " OR (1 = 1)";
      qry = String.Format("((STORE.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1)){0})", dop);
      if (ucContractors.Items.Count > 0)
      {
        StringBuilder Contractoritems = new StringBuilder();
        foreach (DataRowItem dri in ucContractors.Items)
        {
          if (Contractoritems.Length > 0)
          { Contractoritems.AppendFormat(",{0}", dri.Id); }
          else
          { Contractoritems.AppendFormat("{0}", dri.Id); }
        }
        qry = qry + String.Format(" AND STORE.ID_CONTRACTOR IN ({0})", Contractoritems.ToString());
      }
      if (!string.IsNullOrEmpty(qry))
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }

    private void ucContractors_BeforePluginShow(object sender, CancelEventArgs e)
    {
      ucContractors.Enabled = SelfIsCenter();
    }
  }
}