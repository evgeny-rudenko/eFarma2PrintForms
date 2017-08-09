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

namespace RCBTrustLetterGoodsRegistryKursk
{
  public partial class GoodsRegistryKurskParams : ExternalReportForm, IExternalReportFormMethods
  {
    private string settingsFilePath;

    public GoodsRegistryKurskParams()
    {
      InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);

      ucContractors.AddItems(root, "ID_CONTRACTOR");
      ucStores.AddItems(root, "ID_STORE");

      foreach (DataRowItem row in ucIns.Items)
      {
        Utils.AddNode(root, "ID_INS", row.Guid);
      }

      foreach (DataRowItem row in ucLgot.Items)
      {
        Utils.AddNode(root, "ID_LGOT", row.Guid);
      }

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "GoodsRegistryKursk.rdlc");


      rep.LoadData("DBO.REPEX_GOODS_REGISTRY_KURSK", doc.InnerXml);
      rep.BindDataSource("GoodsRegistryKursk_DS_Table0", 0);
      rep.BindDataSource("GoodsRegistryKursk_DS_Table1", 1);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("contrs", ucContractors.TextValues());
      rep.AddParameter("stores", ucStores.TextValues());
      rep.AddParameter("lgots", ucLgot.TextValues());
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Реестр лекарственных средств отпущенных за период (Курск)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void ClearValues()
    {
      ucPeriod.SetPeriodMonth();

      ucContractors.Items.Clear();
      ucStores.Items.Clear();
      ucIns.Items.Clear();
      ucLgot.Items.Clear();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(settingsFilePath))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(settingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
        return;

      //аптеки
      XmlNodeList contractors = root.SelectNodes("ID_CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucContractors.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      //склады
      XmlNodeList stores = root.SelectNodes("ID_STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      DataRowItem dri = new DataRowItem();

      //льготники
      XmlNodeList lgots = root.SelectNodes("ID_LGOT");
      foreach (XmlNode node in lgots)
      {
        dri.Guid = Utils.GetGuid(node, "GUID");
        dri.Text = Utils.GetString(node, "TEXT");
        ucLgot.AddRowItem(dri);
      }

      //страховые компании
      XmlNodeList ins = root.SelectNodes("ID_INS");
      foreach (XmlNode node in ins)
      {
        dri.Guid = Utils.GetGuid(node, "GUID");
        dri.Text = Utils.GetString(node, "TEXT");
        ucIns.AddRowItem(dri);
      }

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      //аптеки
      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      //склады
      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "GUID", dri.Guid);
      }

      //страховые компании
      foreach (DataRowItem dri in ucIns.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_INS");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      //льготники
      foreach (DataRowItem dri in ucLgot.Items)
      {
        XmlNode node = Utils.AddNode(root, "ID_LGOT");
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      doc.Save(settingsFilePath);
    }

    private void GoodsRegistryKurskParams_Load(object sender, EventArgs e)
    {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      LoadSettings();
    }

    private void GoodsRegistryKurskParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}