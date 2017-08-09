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

namespace Defectura
{
  public partial class DefecturaParams : ExternalReportForm, IExternalReportFormMethods
  {
    public DefecturaParams()
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
      // Проверяем введенные параметры
      if (!String.IsNullOrEmpty(InvalidParametersMessage()))
      {
        MessageBox.Show(InvalidParametersMessage(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE_FROM", ucPeriodFrom.Value);
      Utils.AddNode(root, "DATE_TO", ucPeriodTo.Value);

      ucStores.AddItems(root, "ID_STORE");
      ucGoods.AddItems(root, "ID_GOODS");
      Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);

      if (!String.IsNullOrEmpty(ucInsReserve.Text))
        Utils.AddNode(root, "INS_RESERVE", ucInsReserve.Text);
      Utils.AddNode(root, "DAYS", ucDays.Value);

      // тип сортировки
      string st = (ucSort.SelectedIndex == 0) ? GOODS_NAME : SUPPLIER_NAME;
      Utils.AddNode(root, "SORT", st);

      Utils.AddNode(root, "OA", cbOA.Checked);
      Utils.AddNode(root, "GROUPS", cbGroups.Checked);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "Defectura.rdlc");
      rep.LoadData("REPEX_DEFECTURA", doc.InnerXml);
      rep.BindDataSource("Defectura_DS_Table0", 0);
      //rep.BindDataSource("Defectura_DS_Table1", 1);

      rep.AddParameter("date_from", ucPeriodFrom.Value.ToString("g"));
      rep.AddParameter("date_to", ucPeriodTo.Value.ToString("g"));
      rep.AddParameter("stores", ucStores.TextValues());
      rep.AddParameter("days", ucDays.Value.ToString());

      rep.ExecuteReport(this);
    }

    private string InvalidParametersMessage()
    {
      string result = String.Empty;

      // Аптека
      if (ucContractor.Id == 0)
        result = String.Format("Не указана аптека.");

      // Период
      if (ucPeriodFrom.Value > ucPeriodTo.Value)
        result = String.IsNullOrEmpty(result) ?
          String.Format("Значение начальной даты не может быть позже значения конечной даты.") :
          String.Format("{0}{1}Значение начальной даты не может быть позже значения конечной даты.",
              result, Environment.NewLine);

      // Страховой запас
      if (!String.IsNullOrEmpty(ucInsReserve.Text))
      {
        try
        {
          int i = Convert.ToInt32(ucInsReserve.Text);
        }
        catch
        {
          result = String.IsNullOrEmpty(result) ?
          String.Format("Задано слишком большое либо неверно значение Страхового запаса.") :
          String.Format("{0}{1}Задано слишком большое либо неверно значение Страхового запаса.",
              result, Environment.NewLine);
        }
      }
      return result;
    }

    private void ClearValues()
    {
      DateTime now = DateTime.Now;
      ucPeriodFrom.Value = (now.AddHours(-now.Hour)).AddMinutes(-now.Minute);
      ucPeriodTo.Value = now;

      ucInsReserve.Text = String.Empty;
      ucDays.Value = 1;
      ucSort.SelectedIndex = 0;
      ucSort.Refresh();

      ucContractor.Clear();
      ucStores.Items.Clear();
      ucGoods.Items.Clear();

      cbGroups.Checked = false;
      cbOA.Checked = false;
    }

    public string ReportName
    {
      get { return "Дефектура"; }
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

      ucPeriodFrom.Value = Utils.GetDate(root, "DATE_FROM");
      ucPeriodTo.Value = Utils.GetDate(root, "DATE_TO");

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

      XmlNode contractor = root.SelectSingleNode("CONTRACTOR");
      ucContractor.SetValues(new DataRowItem(Utils.GetLong(contractor, "ID"), Utils.GetGuid(contractor, "GUID"),
        Utils.GetString(contractor, "CODE"), Utils.GetString(contractor, "TEXT")));

      ucInsReserve.Text = Utils.GetString(root, "INS_RESERVE");
      ucDays.Value = Utils.GetInt(root, "DAYS");
      ucSort.SelectedIndex = Utils.GetInt(root, "SORT");

      cbOA.Checked = Utils.GetBool(root, "OA");
      cbGroups.Checked = Utils.GetBool(root, "GROUPS");
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

      Utils.AddNode(root, "DATE_FROM", ucPeriodFrom.Value);
      Utils.AddNode(root, "DATE_TO", ucPeriodTo.Value);

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

      XmlNode contractor = Utils.AddNode(root, "CONTRACTOR");
      Utils.AddNode(contractor, "ID", ucContractor.Id);
      Utils.AddNode(contractor, "GUID", ucContractor.Guid);
      Utils.AddNode(contractor, "CODE", ucContractor.Code);
      Utils.AddNode(contractor, "TEXT", ucContractor.Text);

      Utils.AddNode(root, "INS_RESERVE", ucInsReserve.Text);
      Utils.AddNode(root, "DAYS", ucDays.Value);
      Utils.AddNode(root, "SORT", ucSort.SelectedIndex);

      Utils.AddNode(root, "OA", cbOA.Checked);
      Utils.AddNode(root, "GROUPS", cbGroups.Checked);

      doc.Save(SettingsFilePath);
    }

    private bool nonNumberEntered = false;
    private void ucInsReserve_KeyDown(object sender, KeyEventArgs e)
    {
      nonNumberEntered = false;

      if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
      {
        if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
        {
          if (e.KeyCode != Keys.Back)
          {
            nonNumberEntered = true;
          }
        }
      }
    }

    private void ucInsReserve_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (nonNumberEntered == true)
      {
        e.Handled = true;
      }
    }

    private void cbGroups_CheckedChanged(object sender, EventArgs e)
    {
      ucInsReserve.Enabled = !cbGroups.Checked;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private readonly string GOODS_NAME = "GOODS_NAME";
    private readonly string SUPPLIER_NAME = "LAST_SUPPLIER";

    private void DefecturaParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void DefecturaParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void ucStores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      string qry = "";
      qry = String.Format("STORE.ID_CONTRACTOR = ({0})", ucContractor.Id);
      ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }

    private void ucContractor_ValueChanged()
    {
      ucStores.Enabled = (ucContractor.Id != 0);
    }
  }
}