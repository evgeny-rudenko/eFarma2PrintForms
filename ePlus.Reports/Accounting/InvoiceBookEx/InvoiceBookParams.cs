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
using System.IO;

namespace RCBInvoiceBook
{
  public partial class InvoiceBookParams : ExternalReportForm, IExternalReportFormMethods
  {
    public InvoiceBookParams()
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
      int ParentNode = 6;
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
      Utils.AddNode(root, "DETAIL", chbDetail.Checked ? 1 : 0);
      Utils.AddNode(root, "SHOW_REMAIN", remainCheckBox.Checked ? 1 : 0);
      Utils.AddNode(root, "ROUND_DIGIT", nupdRoundDigit.Value);

      storesPluginMultiSelect.AddItems(root, "STORE");

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];

      // изменяем структуру файла-отчета для добавления колонок с разными НДС и типами НДС
      XmlDocument doc1 = new XmlDocument();
      doc1.Load(reportFiles[0]);
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
      nsmgr.AddNamespace("rp", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
      XmlElement root1 = doc1.DocumentElement;

      rep.LoadData("REPEX_INVOICE_BOOK", doc.InnerXml);
      rep.BindDataSource("Invoice_Book_DS_TableFields", 0);
      if (rep.DataSource.Tables[0].Rows.Count > 0 && (!String.IsNullOrEmpty(rep.DataSource.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())))
      foreach (DataRow dr in rep.DataSource.Tables[0].Rows)
      {
        //DataSet
        XmlNodeList nodes = root1.SelectNodes("/rp:Report/rp:DataSets/rp:DataSet[@Name='Invoice_Book_DS_Table']/rp:Fields", nsmgr);
        XmlNode newDS = nodes.Item(0).ChildNodes[nodes.Item(0).ChildNodes.Count - ParentNode].CloneNode(true);
        string nm = nodes.Item(0).ChildNodes[nodes.Item(0).ChildNodes.Count - ParentNode].Attributes["Name"].Value;
        nodes.Item(0).AppendChild(newDS);
        // меняем значения
        nodes = root1.SelectNodes("//rp:DataSets/rp:DataSet[@Name='Invoice_Book_DS_Table']/rp:Fields/rp:Field[@Name='" + nm + "']", nsmgr);
        nodes.Item(1).Attributes["Name"].Value = dr.ItemArray.GetValue(0).ToString();
        nodes.Item(1).ChildNodes[0].InnerXml = dr.ItemArray.GetValue(0).ToString();

        //Details
        nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Details/rp:TableRows/rp:TableRow/rp:TableCells", nsmgr);
        XmlNode newCell = nodes.Item(0).ChildNodes[nodes.Item(0).ChildNodes.Count - ParentNode].CloneNode(true);
        nm = newCell.ChildNodes[0].ChildNodes[0].Attributes["Name"].Value;
        nodes.Item(0).AppendChild(newCell);
        // меняем значения
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + nm + "']", nsmgr);
        nodes.Item(1).Attributes["Name"].Value = dr.ItemArray.GetValue(0).ToString();
        //nodes.Item(1).ChildNodes[
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + dr.ItemArray.GetValue(0).ToString() + "']/rp:Value", nsmgr);
        nodes.Item(0).InnerXml = "=Fields!" + dr.ItemArray.GetValue(0).ToString() + ".Value";

        //Header
        nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Header/rp:TableRows", nsmgr);
        int ito = nodes.Item(0).ChildNodes.Count;
        for (int i = 0; i < ito; i++)
        {
          nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Header/rp:TableRows", nsmgr);
          XmlNode nd = nodes.Item(0).ChildNodes[i].ChildNodes[0].ChildNodes[nodes.Item(0).ChildNodes[i].ChildNodes[0].ChildNodes.Count - ParentNode];
          nm = nd.ChildNodes[0].ChildNodes[0].Attributes["Name"].Value;
          XmlNode newHeader = nd.CloneNode(true);
          nodes.Item(0).ChildNodes[i].ChildNodes[0].AppendChild(newHeader);
          // меняем значения
          nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + nm + "']", nsmgr);
          nodes.Item(1).Attributes["Name"].Value = dr.ItemArray.GetValue(0).ToString() + "_header" + i.ToString();
          nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + dr.ItemArray.GetValue(0).ToString() + "_header" + i.ToString() + "']/rp:Value", nsmgr);
          if (!String.IsNullOrEmpty(nodes.Item(0).InnerXml))
            nodes.Item(0).InnerXml = "" + dr.ItemArray.GetValue(1).ToString();
        }

        //Footer
        nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Footer/rp:TableRows/rp:TableRow/rp:TableCells", nsmgr);
        XmlNode newFooter = nodes.Item(0).ChildNodes[nodes.Item(0).ChildNodes.Count - ParentNode].CloneNode(true);
        nm = newFooter.ChildNodes[0].ChildNodes[0].Attributes["Name"].Value;
        nodes.Item(0).AppendChild(newFooter);
        // меняем значения
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + nm + "']", nsmgr);
        nodes.Item(1).Attributes["Name"].Value = dr.ItemArray.GetValue(0).ToString() + "_footer";
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + dr.ItemArray.GetValue(0).ToString() + "_footer']/rp:Value", nsmgr);
        nodes.Item(0).InnerXml = "=SUM(Fields!" + dr.ItemArray.GetValue(0).ToString() + ".Value)";

        //Group
        nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:TableGroups/rp:TableGroup/rp:Header/rp:TableRows/rp:TableRow/rp:TableCells", nsmgr);
        XmlNode newGroup = nodes.Item(0).ChildNodes[nodes.Item(0).ChildNodes.Count - ParentNode].CloneNode(true);
        nm = newGroup.ChildNodes[0].ChildNodes[0].Attributes["Name"].Value;
        nodes.Item(0).AppendChild(newGroup);
        // меняем значения
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + nm + "']", nsmgr);
        nodes.Item(1).Attributes["Name"].Value = dr.ItemArray.GetValue(0).ToString() + "_gr";
        nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + dr.ItemArray.GetValue(0).ToString() + "_gr']/rp:Value", nsmgr);
        nodes.Item(0).InnerXml = "=SUM(Fields!" + dr.ItemArray.GetValue(0).ToString() + ".Value)";

        //Columns
        XmlNode columns = root1.SelectSingleNode("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:TableColumns", nsmgr);
        XmlNode newColumn = columns.ChildNodes[columns.ChildNodes.Count - ParentNode].CloneNode(true);
        columns.AppendChild(newColumn);
        string wd = newColumn.ChildNodes[newColumn.ChildNodes.Count-1].InnerXml;// было 1 
        wd = wd.Replace('.', ',');
        decimal colWidth = Convert.ToDecimal(wd.Substring(0, wd.Length - 2));

        //tableWidth
        XmlNode tableNode = root1.SelectSingleNode("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Width", nsmgr);
        string twidth_str = tableNode.InnerXml;
        twidth_str = twidth_str.Replace('.', ',');
        decimal table_width = Convert.ToDecimal(twidth_str.Substring(0, twidth_str.Length - 2));

        //Width of container
        XmlNode containerWidth = root1.SelectSingleNode("/rp:Report/rp:Width", nsmgr);
        string twd = containerWidth.InnerXml;
        twd = twd.Replace('.', ',');
        string znach = twd.Substring(twd.Length - 2, 2);
        decimal dtwd = Convert.ToDecimal(twd.Substring(0, twd.Length - 2));
        if (table_width + 2 * colWidth > dtwd)
          containerWidth.InnerXml = ((dtwd + colWidth).ToString()).Replace(',', '.') + znach;
      }
      doc1.Save(reportFiles[0]);

      rep.AddParameter("store", storesPluginMultiSelect.TextValues());
      rep.AddParameter("date_fr", ucPeriod.DateFrText);
      rep.AddParameter("date_to", ucPeriod.DateToText);
      rep.AddParameter("detail", chbDetail.Checked ? "1" : "0");

      rep.AddParameter("one", checkedListBox1.GetItemChecked(0) ? "1" : "0");
      rep.AddParameter("two", checkedListBox1.GetItemChecked(1) ? "1" : "0");
      rep.AddParameter("three", checkedListBox1.GetItemChecked(2) ? "1" : "0");
      rep.AddParameter("half", checkedListBox1.GetItemChecked(3) ? "1" : "0");
      rep.AddParameter("full", checkedListBox1.GetItemChecked(4) ? "1" : "0");
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.AddParameter("ROUND_DIGIT", nupdRoundDigit.Value.ToString());

      rep.BindDataSource("Invoice_Book_DS_Table3", 1);
      rep.BindDataSource("Invoice_Book_DS_Table", 2);
      rep.BindDataSource("Invoice_Book_DS_Table4", 3);
      rep.BindDataSource("Invoice_Book_DS_Table1", 4);

      rep.BindDataSource("Invoice_Book_DS_T1", 5);
      rep.BindDataSource("Invoice_Book_DS_T2", 6);
      rep.BindDataSource("Invoice_Book_DS_T3", 7);
      rep.BindDataSource("Invoice_Book_DS_T4", 8);
      rep.BindDataSource("Invoice_Book_DS_T5", 9);

      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Завозная книга"; }
    }

    private void ClearValues()
    {
      ucPeriod.SetPeriodMonth();
      storesPluginMultiSelect.Clear();
      chbDetail.Checked = false;
      remainCheckBox.Checked = false;

      for (int i = 0; i < checkedListBox1.Items.Count; i++)
      {
        checkedListBox1.SetItemChecked(i, true);
      }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
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
        nupdRoundDigit.Value = 2;
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
      nupdRoundDigit.Value = Utils.GetDecimal(root, "ROUND_DIGIT");
      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        storesPluginMultiSelect.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      string str = Utils.GetString(root, "SHOW_SUM");
      for (int i = 0; i < checkedListBox1.Items.Count; i++)
      {
        checkedListBox1.SetItemChecked(i, (str[i] == (char)'1'));
      }

      chbDetail.Checked = Utils.GetBool(root, "DETAIL");
      remainCheckBox.Checked = Utils.GetBool(root, "REMAIN");
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
      Utils.AddNode(root, "ROUND_DIGIT", nupdRoundDigit.Value);
      foreach (DataRowItem dri in storesPluginMultiSelect.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      string str = "";
      for (int i = 0; i < checkedListBox1.Items.Count; i++)
      {
        str = str + (checkedListBox1.GetItemChecked(i) ? "1" : "0");
      }
      Utils.AddNode(root, "SHOW_SUM", str);

      Utils.AddNode(root, "DETAIL", chbDetail.Checked);
      Utils.AddNode(root, "REMAIN", remainCheckBox.Checked);

      doc.Save(SettingsFilePath);
    }

    private void InvoiceBookParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void InvoiceBookParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}