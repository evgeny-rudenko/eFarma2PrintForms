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

namespace MovementsRegistry
{
  public partial class MovementsRegistryParams : ExternalReportForm, IExternalReportFormMethods
  {
    private string fileName;

    public MovementsRegistryParams()
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
      ucCGroupFrom.AddItems(root, "ID_CGROUPS_FROM");
      ucCGroupTo.AddItems(root, "ID_CGROUPS_TO");
      ucContractorsFrom.AddItems(root, "ID_CONTRACTORS_FROM");
      ucContractorsTo.AddItems(root, "ID_CONTRACTORS_TO");
      ucStoresFrom.AddItems(root, "ID_STORE_FROM");
      ucStoresTo.AddItems(root, "ID_STORE_TO");
      Utils.AddNode(root, ADD_SUB, cbReturn.Checked);
      Utils.AddNode(root, TYPE_REM, rbUsual.Checked ? 0 : rbUnits.Checked ? 1 : 2);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MovementRegistry.rdlc");
      rep.LoadData("REPEX_MOVEMENT_REGISTRY", doc.InnerXml);
      rep.BindDataSource("MovementRegistry_DS_Table0", 0);

      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("stores_fr", ucStoresFrom.Items.Count > 0 ? ucStoresFrom.TextValues() : "Все");
      rep.AddParameter("stores_to", ucStoresTo.Items.Count > 0 ? ucStoresTo.TextValues() : "Все");
      rep.AddParameter("contractors_fr", ucContractorsFrom.Items.Count > 0 ? ucContractorsFrom.TextValues() : "Все");
      rep.AddParameter("contractors_to", ucContractorsTo.Items.Count > 0 ? ucContractorsTo.TextValues() : "Все");
      rep.AddParameter("add_sub", cbReturn.Checked ? "Возвраты " : "Перемещения ");
      rep.AddParameter("type_rem", rbUsual.Checked ? "обычные" : rbUnits.Checked ? "между подразделениями" : "");
      rep.AddParameter("cgroup_fr", ucCGroupFrom.Items.Count > 0 ? ucCGroupFrom.TextValues() : "Все");
      rep.AddParameter("cgroup_to", ucCGroupTo.Items.Count > 0 ? ucCGroupTo.TextValues() : "Все");
      
      rep.ExecuteReport(this);
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

      ucCGroupFrom.Items.Clear();
      ucCGroupTo.Items.Clear();
      ucStoresFrom.Items.Clear();
      ucStoresTo.Items.Clear();
      ucContractorsFrom.Items.Clear();
      ucContractorsTo.Items.Clear();

      cbReturn.Checked = false;
      rbUsual.Checked = true;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void groupFROM_Enter(object sender, EventArgs e)
    {

    }

    private void MovementsRegistryParams_Load(object sender, EventArgs e)
    {
      ClearValues();
      if (!File.Exists(fileName)) return;
      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      XmlNode root = doc.SelectSingleNode("/XML");

      XmlNodeList cgroupTo = root.SelectNodes("CGROUP_TO");
      foreach (XmlNode node in cgroupTo)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucCGroupTo.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList cgroupFrom = root.SelectNodes("CGROUP_FROM");
      foreach (XmlNode node in cgroupFrom)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucCGroupFrom.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList contractorsTo = root.SelectNodes("CONTRACTORS_TO");
      foreach (XmlNode node in contractorsTo)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucContractorsTo.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList contractorsFrom = root.SelectNodes("CONTRACTORS_FROM");
      foreach (XmlNode node in contractorsFrom)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucContractorsFrom.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList storesTo = root.SelectNodes("STORES_TO");
      foreach (XmlNode node in storesTo)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucStoresTo.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList storesFrom = root.SelectNodes("STORES_FROM");
      foreach (XmlNode node in storesFrom)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucStoresFrom.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
      if (Utils.GetBool(root, "ADD_SUB"))
        cbReturn.Checked = true;
      switch (Utils.GetInt(root, "TYPE_REM"))
      {
        case 0:
          rbUsual.Checked = true;
          break;
        case 1:
          rbUnits.Checked = true;
          break;
        case 2:
          rbBoth.Checked = true;
          break;
      }
    }

    private void MovementsRegistryParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      foreach (DataRowItem dri in ucContractorsTo.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS_TO");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucContractorsFrom.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS_FROM");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucStoresTo.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES_TO");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucStoresFrom.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES_FROM");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucCGroupTo.Items)
      {
        XmlNode node = Utils.AddNode(root, "CGROUP_TO");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucCGroupFrom.Items)
      {
        XmlNode node = Utils.AddNode(root, "CGROUP_FROM");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
      Utils.AddNode(root, "ADD_SUB", cbReturn.Checked);
      Utils.AddNode(root, "TYPE_REM", rbUsual.Checked ? 0 : rbUnits.Checked ? 1 : 2);
      doc.Save(fileName);
    }

    public string ReportName
    {
      get { return "Реестр перемещений"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private const string ADD_SUB = "ADD_SUB";
    private const string TYPE_REM = "TYPE_REM";

    private void ucStoresFrom_BeforePluginShow(object sender, CancelEventArgs e)
    {
      string qry = "";
      if (ucCGroupFrom.Items.Count > 0)
      {
        string groups = "";
        foreach (DataRowItem item in ucCGroupFrom.Items)
        {
          groups = String.IsNullOrEmpty(groups) ? item.Id.ToString() : groups + "," + item.Id.ToString();
        }
        qry = String.Format("(STORE.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP	WHERE DATE_DELETED IS NULL AND ID_CONTRACTOR_GROUP IN ({0}))", 
          groups);
      }

      if (ucContractorsFrom.Items.Count > 0)
      {
        string contrs = "";
        foreach (DataRowItem item in ucContractorsFrom.Items)
        {
          contrs = String.IsNullOrEmpty(contrs) ? item.Id.ToString() : contrs + "," + item.Id.ToString();
        }
        qry = String.IsNullOrEmpty(qry) ? String.Format("STORE.ID_CONTRACTOR IN ({0})",
          contrs) : qry + " OR " + String.Format("STORE.ID_CONTRACTOR IN ({0}))", contrs);
      }
      else
        qry = !String.IsNullOrEmpty(qry) ? qry + ")" : String.Empty;
      if (!string.IsNullOrEmpty(qry))
        ucStoresFrom.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }

    private void ucStoresTo_BeforePluginShow(object sender, CancelEventArgs e)
    {
      string qry = "";
      if (ucCGroupTo.Items.Count > 0)
      {
        string groups = "";
        foreach (DataRowItem item in ucCGroupTo.Items)
        {
          groups = String.IsNullOrEmpty(groups) ? item.Id.ToString() : groups + "," + item.Id.ToString();
        }
        qry = String.Format("(STORE.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM CONTRACTOR_2_CONTRACTOR_GROUP	WHERE DATE_DELETED IS NULL AND ID_CONTRACTOR_GROUP IN ({0}))",
          groups);
      }

      if (ucContractorsTo.Items.Count > 0)
      {
        string contrs = "";
        foreach (DataRowItem item in ucContractorsTo.Items)
        {
          contrs = String.IsNullOrEmpty(contrs) ? item.Id.ToString() : contrs + "," + item.Id.ToString();
        }
        qry = String.IsNullOrEmpty(qry) ? String.Format("STORE.ID_CONTRACTOR IN ({0})",
          contrs) : qry + " OR " + String.Format("STORE.ID_CONTRACTOR IN ({0}))", contrs);
      }
      else
        qry = !String.IsNullOrEmpty(qry) ? qry + ")" : String.Empty;
      if (!string.IsNullOrEmpty(qry))
        ucStoresTo.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", qry);
    }
  }
}