using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InventoryVedCompare
{
  public partial class InventoryVedCompareParams : ExternalReportForm, IExternalReportFormMethods
  {
    public InventoryVedCompareParams()
    {
      InitializeComponent();
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML", null);
      XmlNode list1Node = Utils.AddNode(root, "LIST1");
      XmlNode list2Node = Utils.AddNode(root, "LIST2");      
      foreach (DataRowItem dri in mpsList1.Items)
        Utils.AddNode(list1Node, "ID_INVENTORY_VED_GLOBAL", dri.Guid);
      foreach (DataRowItem dri in mpsList2.Items)
        Utils.AddNode(list2Node, "ID_INVENTORY_VED_GLOBAL", dri.Guid);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];
      rep.LoadData("REPEX_INVENTORY_VED_COMPARE", doc.InnerXml);
      rep.BindDataSource("InventoryVedCompare_Table0", 0);
      rep.ExecuteReport(this);

    }

    public string ReportName
    {
      get { return "Инвентаризация: Расхождения между 1-ым и 2-ым счетом"; }
    }

    private void mpsList1_BeforePluginShow(object sender, CancelEventArgs e)
    {
      UCPluginMultiSelect ucSelect = (UCPluginMultiSelect)sender;
      StringBuilder sb = new StringBuilder(string.Empty);
      foreach (DataRowItem dri in mpsList1.Items)
        sb.Append(string.Format("{0}'{1}'", sb.ToString()==string.Empty? string.Empty:", ", dri.Guid));
      foreach (DataRowItem dri in mpsList2.Items)
        sb.Append(string.Format("{0}'{1}'", sb.ToString()==string.Empty? string.Empty:", ", dri.Guid));
      if (sb.ToString()!=string.Empty)
        ucSelect.PluginContol.Grid(0).SetParameterValue("@FILTER", string.Format("ID_INVENTORY_VED_GLOBAL NOT IN ({0})", sb.ToString()));
    }

    private string fileName = System.IO.Path.Combine(Utils.TempDir(), "InventoryVedCompareSettings.xml");

    private void InventoryVedCompareParams_Load(object sender, EventArgs e)
    {
      if (!System.IO.File.Exists(fileName)) return; 
      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      XmlNode root = doc.SelectSingleNode("XML");
      XmlNodeList list1Nodes = root.SelectNodes("LIST1/INVENTORY_VED");
      XmlNodeList list2Nodes = root.SelectNodes("LIST2/INVENTORY_VED");      
      foreach (XmlNode node in list1Nodes)
      {
        DataRowItem dri = new DataRowItem(0, Utils.GetGuid(node, "ID_INVENTORY_VED_GLOBAL"), string.Empty, Utils.GetString(node, "DOC_NAME"));
        mpsList1.Items.Add(dri);
      }
      foreach (XmlNode node in list2Nodes)
      {
        DataRowItem dri = new DataRowItem(0, Utils.GetGuid(node, "ID_INVENTORY_VED_GLOBAL"), string.Empty, Utils.GetString(node, "DOC_NAME"));
        mpsList2.Items.Add(dri);
      }
    }

    private void InventoryVedCompareParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      XmlNode list1Node = Utils.AddNode(root, "LIST1");
      XmlNode list2Node = Utils.AddNode(root, "LIST2");
      foreach (DataRowItem dri in mpsList1.Items)
      {
        XmlNode driNode = Utils.AddNode(list1Node, "INVENTORY_VED");
        Utils.AddNode(driNode, "ID_INVENTORY_VED_GLOBAL", dri.Guid);
        Utils.AddNode(driNode, "DOC_NAME", dri.Text);      
      }

      foreach (DataRowItem dri in mpsList2.Items)
      {
        XmlNode driNode = Utils.AddNode(list2Node, "INVENTORY_VED");
        Utils.AddNode(driNode, "ID_INVENTORY_VED_GLOBAL", dri.Guid);
        Utils.AddNode(driNode, "DOC_NAME", dri.Text);
      }
      doc.Save(fileName);
    }
  }
}