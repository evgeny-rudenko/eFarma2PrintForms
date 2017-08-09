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

namespace InvoiceRegistry
{
  public partial class InvoiceRegistryParams : ExternalReportForm, IExternalReportFormMethods
  {
    private string fileName;

    public InvoiceRegistryParams()
    {
      System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
      fileName = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      ucPeriod.AddValues(root);
      ucStores.AddItems(root, "ID_STORES");
      ucSuppliers.AddItems(root, "ID_CONTRACTORS");

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "InvoiceRegistry.rdlc");
      rep.LoadData("REPEX_INVOICE_REGISTRY", doc.InnerXml);
      rep.BindDataSource("InvoiceRegistry_DS_Table0", 0);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("stores", ucStores.TextValues());

      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

      ucSuppliers.Items.Clear();
      ucStores.Items.Clear();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    public string ReportName
    {
      get { return "Реестр приходных накладных"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(fileName)) return;
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
        ucSuppliers.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucStores.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      foreach (DataRowItem dri in ucSuppliers.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(fileName);
    }

    private void InvoiceRegistryParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void InvoiceRegistryParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void ucSuppliers_BeforePluginShow(object sender, CancelEventArgs e)
    {
      /*string qry = "";
      qry = String.Format("ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP CCG INNER JOIN CONTRACTOR_GROUP CG ON CCG.ID_CONTRACTOR_GROUP = CG.ID_CONTRACTOR_GROUP AND CG.MNEMOCODE = 'DISTRIBUTOR')");
      ucSuppliers.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
      ucSuppliers.PluginContol.Caption = "Поставщики";*/
    }
  }
}