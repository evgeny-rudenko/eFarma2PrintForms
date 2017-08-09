using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;
using System.Data.SqlClient;

namespace CheckExchange
{
  public partial class CheckExchangeParams : ExternalReportForm, IExternalReportFormMethods
  {
    public CheckExchangeParams()
    {
      InitializeComponent();
      ClearValues();
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
      if ((radioDetailed.Checked) && (String.IsNullOrEmpty(ucContractor.Code)))
      {
        MessageBox.Show("ÕÂ ÛÍ‡Á‡Ì‡ ‡ÔÚÂÍ‡", "œÂ‰ÛÔÂÊ‰ÂÌËÂ");
        return;
      }
      
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
      Utils.AddNode(root, RTYPE, radioDetailed.Checked);
      
      ReportFormNew rep = new ReportFormNew();
      if (radioWhole.Checked)
      {
        foreach (DataRowItem dr in AUPluginMultiSelect.Items)
        {
          Utils.AddNode(root, "ID_AU", dr.Code);
        }
        //AUPluginMultiSelect.AddItems(root, "ID_AU");
        rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CheckExchangeWhole.rdlc");
        rep.LoadData("REPEX_CHECK_EXCHANGE", doc.InnerXml);
        rep.BindDataSource("CheckExchange_DS_Table0", 0);
      }
      else
      {
        Utils.AddNode(root, "ID_AU", ucContractor.Code);
        rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CheckExchangeDetailed.rdlc");
        rep.LoadData("REPEX_CHECK_EXCHANGE", doc.InnerXml);
        //—œ–¿¬Œ◊Õ» » œ–»Õﬂ“Œ
        rep.BindDataSource("CheckExchange_DS_Table1", 0);
        //—œ–¿¬Œ◊Õ» » Œ“œ–¿¬À≈ÕŒ
        rep.BindDataSource("CheckExchange_DS_Table2", 1);
        //—œ–¿¬Œ◊Õ» » œŒƒ“¬≈–∆ƒ≈ÕŒ
        rep.BindDataSource("CheckExchange_DS_Table3", 2);
        //ƒŒ ”Ã≈Õ“€ œ–»Õﬂ“Œ
        rep.BindDataSource("CheckExchange_DS_Table4", 3);
        //ƒŒ ”Ã≈Õ“€ Œ“œ–¿¬À≈ÕŒ
        rep.BindDataSource("CheckExchange_DS_Table5", 4);
        //ƒŒ ”Ã≈Õ“€ œŒƒ“¬≈–∆ƒ≈ÕŒ
        rep.BindDataSource("CheckExchange_DS_Table6", 5);
      }

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      if (radioDetailed.Checked)
        rep.AddParameter("AU", ucContractor.Text);
      
      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

      ucContractor.Clear();
      AUPluginMultiSelect.Items.Clear();

      radioWhole.Checked = true;
      AUPluginMultiSelect.Enabled = true;
      ucContractor.Enabled = false;
    }

    public string ReportName
    {
      get { return "œÓ‚ÂÍ‡ Ó·ÏÂÌ‡"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }
    private const string RTYPE = "RTYPE";

    private void radioWhole_CheckedChanged(object sender, EventArgs e)
    {
      AUPluginMultiSelect.Enabled = !radioDetailed.Checked;
      ucContractor.Enabled = radioDetailed.Checked;
    }

    private void radioDetailed_CheckedChanged(object sender, EventArgs e)
    {
      AUPluginMultiSelect.Enabled = !radioDetailed.Checked;
      ucContractor.Enabled = radioDetailed.Checked;
    }

    private void AUPluginMultiSelect_BeforePluginShow(object sender, CancelEventArgs e)
    {
      ((GridController)AUPluginMultiSelect.PluginContol.Grid(0)).dataLoaded += GetFilteredTable;
    }

    private void GetFilteredTable(GridController gc)
    {
      gc.DataSource.DefaultView.RowFilter = "IS_ACTIVE = 1";
    }

    private void ucContractor_BeforePluginShow(object sender, EventArgs e)
    {
      ((GridController)ucContractor.PluginContol.Grid(0)).dataLoaded += GetFilteredTable;
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

      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = 0;
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        AUPluginMultiSelect.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNode contractor = root.SelectSingleNode("CONTRACTOR");

      ucContractor.SetValues(new DataRowItem(0, Utils.GetGuid(contractor, "GUID"), 
        Utils.GetString(contractor, "CODE"), Utils.GetString(contractor, "TEXT")));      

      bool type = Utils.GetBool(root, "DETAILED");
      if (type)
      { radioDetailed.Checked = true; }
      else
      { radioWhole.Checked = true; }
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

      foreach (DataRowItem dri in AUPluginMultiSelect.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      XmlNode contractor = Utils.AddNode(root, "CONTRACTOR");
      Utils.AddNode(contractor, "GUID", ucContractor.Guid);
      Utils.AddNode(contractor, "CODE", ucContractor.Code);
      Utils.AddNode(contractor, "TEXT", ucContractor.Text);

      Utils.AddNode(root, "DETAILED", radioDetailed.Checked);

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void CheckExchangeParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void CheckExchangeParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}