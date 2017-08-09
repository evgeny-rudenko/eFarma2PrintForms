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

namespace RCBUnsatisfiedDemand
{
  public partial class UnsatisfiedDemandParams : ExternalReportForm, IExternalReportFormMethods
  {
    private string fileName;
    public UnsatisfiedDemandParams()
    {
      InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);
      //ucUsers.AddItems(root, "ID_USERS");
      foreach (DataRowItem dri in ucUsers.Items)
      {
        Utils.AddNode(root, "ID_USERS", dri.Code);
      }
      //ucContractors.AddItems(root, "ID_CONTRACTORS");
      foreach (DataRowItem dri in ucContractors.Items)
      {
        Utils.AddNode(root, "ID_CONTRACTORS", dri.Guid);
      }

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "UnsatisfiedDemand.rdlc");
      rep.LoadData("REPEX_UNSATISFIED_DEMAND", doc.InnerXml);
      rep.BindDataSource("UnsatisfiedDemand_DS_Table0", 0);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("contractors", ucContractors.TextValues());
      rep.AddParameter("show_au", ucContractors.Items.Count == 1 ? "1" : "0");
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.ExecuteReport(this);
    }

    private void SetDefaultValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

      ucContractors.Items.Clear();
      ucUsers.Items.Clear();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      SetDefaultValues();
    }

    public string ReportName
    {
      get { return "Неудовлетворенный спрос"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void LoadSettings()
    {
      SetDefaultValues();
      if (!File.Exists(fileName))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      XmlNode root = doc.SelectSingleNode("/XML");
      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucContractors.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList users = root.SelectNodes("USERS");
      DataRowItem row = new DataRowItem();
      foreach (XmlNode node in users)
      {
        row.Code = Utils.GetString(node, "CODE");
        row.Text = Utils.GetString(node, "TEXT");

        ucUsers.AddRowItem(row);
      }
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root;

      if (File.Exists(fileName))
      {
        doc.Load(fileName);
        root = doc.SelectSingleNode("//XML");
        root.RemoveAll();
      }
      else
      {
        root = Utils.AddNode(doc, "XML");
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      foreach (DataRowItem dri in ucUsers.Items)
      {
        XmlNode node = Utils.AddNode(root, "USERS");
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucContractors.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(fileName);
    }

    private void UnsatisfiedDemandParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void UnsatisfiedDemandParams_FormClosed(object sender, FormClosedEventArgs e)
    {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        fileName = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
        SetDefaultValues();
      SaveSettings();
    }
  }
}