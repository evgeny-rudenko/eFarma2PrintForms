using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.SqlClient;
using ePlus.MetaData.Server;
using System.Data;
using System.Collections.Generic;

namespace RCBMoveGoodsDK
{
  public partial class MoveGoodsDK : ExternalReportForm, IExternalReportFormMethods
  {
    private Dictionary<int, InOutTable> tablesIn = new Dictionary<int, InOutTable>();
    private Dictionary<int, InOutTable> tablesOut = new Dictionary<int, InOutTable>();
    public MoveGoodsDK()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Заполнение словарей с идентификаторами таблиц данных по TABLE_DATA
    /// </summary>
    private void FillTableData()
    {
      // IN
      // invoice
      tablesIn.Add(0, new InOutTable(2, "INVOICE"));
      //movement
      tablesIn.Add(1, new InOutTable(8, "MOVEMENT"));
      //INTERFIRM_MOVING
      tablesIn.Add(2, new InOutTable(37, "INTERFIRM_MOVING"));
      //ACT_RETURN_TO_BUYER
      tablesIn.Add(3, new InOutTable(12, "ACT_RETURN_TO_BUYER"));
      //INVENTORY_SVED
      tablesIn.Add(4, new InOutTable(24, "INVENTORY_SVED"));
      //ACT_REVALUATION2
      tablesIn.Add(5, new InOutTable(13, "ACT_REVALUATION2"));
      //IMPORT_REMAINS
      tablesIn.Add(6, new InOutTable(30, "IMPORT_REMAINS"));
      //ACT_DISASSEMBLING
      tablesIn.Add(7, new InOutTable(6, "ACT_DISASSEMBLING"));

      //OUT
      //CHEQUE
      tablesOut.Add(0, new InOutTable(7, "CHEQUE"));
      //INVOICE_OUT
      tablesOut.Add(1, new InOutTable(21, "INVOICE_OUT"));
      //movement
      tablesOut.Add(2, new InOutTable(8, "MOVEMENT"));
      //INTERFIRM_MOVING
      tablesOut.Add(3, new InOutTable(37, "INTERFIRM_MOVING"));
      //INVENTORY_SVED
      tablesOut.Add(4, new InOutTable(24, "INVENTORY_SVED"));
      //ACT_REVALUATION2
      tablesOut.Add(5, new InOutTable(13, "ACT_REVALUATION2"));
      //ACT_DISASSEMBLING
      tablesOut.Add(6, new InOutTable(6, "ACT_DISASSEMBLING"));
      //ACT_RETURN_TO_CONTRACTOR
      tablesOut.Add(7, new InOutTable(3, "ACT_RETURN_TO_CONTRACTOR"));
      //ACT_DEDUCTION
      tablesOut.Add(8, new InOutTable(20, "ACT_DEDUCTION"));
    }

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = DateTime.Now;
      ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

      cbAllIn.Checked = false;
      for (int i = 0; i < ucIn.Items.Count; i++)
      {
        ucIn.SetItemChecked(i, false);
      }

      cbAllOut.Checked = false;
      for (int i = 0; i < ucOut.Items.Count; i++)
      {
        ucOut.SetItemChecked(i, false);
      }

      cbAU.Checked = false;
      checkMove.Checked = true;

      ucStores.Items.Clear();
      ucSuppliers.Items.Clear();
      ucGoods.Items.Clear();

      cbReportView.SelectedIndex = 0;
    }

    private bool checkingAllIn;
    private void cbAllIn_Click(object sender, EventArgs e)
    {
      checkingAllIn = true;
      for (int i = 0; i < ucIn.Items.Count; i++)
      {
        ucIn.SetItemChecked(i, cbAllIn.Checked);
      }
      checkingAllIn = false;
    }

    private bool checkingAllOut;
    private void cbAllOut_Click(object sender, EventArgs e)
    {
      checkingAllOut = true;
      for (int i = 0; i < ucOut.Items.Count; i++)
      {
        ucOut.SetItemChecked(i, cbAllOut.Checked);
      }
      checkingAllOut = false;
    }

    private void ucIn_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (!checkingAllIn)
      {
        bool checkAll = true;
        for (int i = 0; i < ucIn.Items.Count; i++)
        {
          if ((!ucIn.GetItemChecked(i) && e.Index != i) || (e.Index == i && e.NewValue != CheckState.Checked))
            checkAll = false;
        }
        cbAllIn.Checked = checkAll;
      }
    }

    private void ucOut_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (!checkingAllOut)
      {
        bool checkAll = true;
        for (int i = 0; i < ucOut.Items.Count; i++)
        {
          if ((!ucOut.GetItemChecked(i) && e.Index != i) || (e.Index == i && e.NewValue != CheckState.Checked))
            checkAll = false;
        }
        cbAllOut.Checked = checkAll;
      }
    }

    private void ucStores_BeforePluginShow(object sender, System.ComponentModel.CancelEventArgs e)
    {
        /*
      ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
        "(STORE.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
         * */
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
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

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
      cbReportView.SelectedIndex = Utils.GetInt(root, "REPORT_VIEW");

      XmlNodeList ins = root.SelectNodes("IN");
      foreach (XmlNode node in ins)
      {
        ucIn.SetItemChecked(Utils.GetInt(node, "INDEX"), Utils.GetBool(node, "CHECKED"));
      }

      XmlNodeList outs = root.SelectNodes("OUT");
      foreach (XmlNode node in outs)
      {
        ucOut.SetItemChecked(Utils.GetInt(node, "INDEX"), Utils.GetBool(node, "CHECKED"));
      }

      cbAU.Checked = Utils.GetBool(root, "GROUPS");
      checkMove.Checked = Utils.GetBool(root, "MOVE");

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucStores.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucSuppliers.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList contractors_c = root.SelectNodes("CONTRACTOR_C");
      foreach (XmlNode node in contractors_c)
      {
          long id = Utils.GetLong(node, "ID");
          string text = Utils.GetString(node, "TEXT");
          Guid guid = Utils.GetGuid(node, "GUID");
          string code = Utils.GetString(node, "CODE");
          ucContractor.AddRowItem(new DataRowItem(id, guid, code, text));
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
      Utils.AddNode(root, "REPORT_VIEW", cbReportView.SelectedIndex);

      for (int i = 0; i < ucIn.Items.Count; i++)
      {
        XmlNode node = Utils.AddNode(root, "IN");
        Utils.AddNode(node, "INDEX", i);
        Utils.AddNode(node, "CHECKED", ucIn.GetItemChecked(i));
      }


      for (int i = 0; i < ucOut.Items.Count; i++)
      {
        XmlNode node = Utils.AddNode(root, "OUT");
        Utils.AddNode(node, "INDEX", i);
        Utils.AddNode(node, "CHECKED", ucOut.GetItemChecked(i));
      }

      Utils.AddNode(root, "GROUPS", cbAU.Checked);
      Utils.AddNode(root, "MOVE", checkMove.Checked);

      foreach (DataRowItem dri in ucStores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucSuppliers.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }
      foreach (DataRowItem dri in ucContractor.Items)
      {
          XmlNode node = Utils.AddNode(root, "CONTRACTOR_C");
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

      doc.Save(SettingsFilePath);
    }

    public string ReportName
    {
      get { return "Движение товаров (ДК)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void MoveGoodsDK_Load(object sender, EventArgs e)
    {
      FillTableData();
      LoadSettings();
    }

    private void MoveGoodsDK_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    public void Print(string[] reportFiles)
    {
      if (cbReportView.SelectedIndex == 1)
      {
        int cnt = 0;
        for (int i = 0; i < ucIn.Items.Count; i++)
        {
          if (ucIn.GetItemChecked(i))
            cnt++;
        }
        for (int i = 0; i < ucOut.Items.Count; i++)
        {
          if (ucOut.GetItemChecked(i))
            cnt++;
        }
        if (cnt == 0)
        {
          MessageBox.Show("Нужно выбрать хотя бы один тип документа", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
      }
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
      Utils.AddNode(root, "GROUP", cbAU.Checked ? "1" : "0");
      Utils.AddNode(root, "MOV", checkMove.Checked ? "1" : "0");

      ucStores.AddItems(root, "ID_STORE");
      ucSuppliers.AddItems(root, "ID_CONTRACTOR");
      ucGoods.AddItems(root, "ID_GOODS");
      ucContractor.AddItems(root, "ID_CONTRACTOR_C");
      ReportFormNew rep = new ReportFormNew();

      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),
          cbReportView.SelectedIndex == 0 ? "MoveGoodsDK.rdlc" : "MoveGoodsDK_Full.rdlc");

      switch (cbReportView.SelectedIndex)
      {
        case 0:
          rep.LoadData("REPEX_MOVE_GOODS_DK", doc.InnerXml);
          rep.BindDataSource("MoveGoodsDK_DS_Table1", 0);
          break;
        case 1:
          // названия таблиц для In и Out
          for (int i = 0; i < ucIn.Items.Count; i++)
          {
            if (ucIn.GetItemChecked(i))
              Utils.AddNode(root, "ID_TABLE_IN", ((InOutTable)tablesIn[i]).ID);
          }
          for (int i = 0; i < ucOut.Items.Count; i++)
          {
            if (ucOut.GetItemChecked(i))
              Utils.AddNode(root, "ID_TABLE_OUT", ((InOutTable)tablesOut[i]).ID);
          }
          rep.LoadData("REPEX_MOVE_GOODS_DK_FULL", doc.InnerXml);

          // здесь достраиваем структуру отчета
          XmlDocument doc1 = new XmlDocument();
          doc1.Load(rep.ReportPath);
          XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc1.NameTable);
          nsmgr.AddNamespace("rp", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
          XmlElement root1 = doc1.DocumentElement;

          // заполняем списки тех таблиц, которые нужно убрать из структуры
          List<InOutTable> tables_in = new List<InOutTable>();
          List<InOutTable> tables_out = new List<InOutTable>();
          for (int i = 0; i < ucIn.Items.Count; i++)
          {
            if (!ucIn.GetItemChecked(i))
              tables_in.Add((InOutTable)tablesIn[i]);
          }
          for (int i = 0; i < ucOut.Items.Count; i++)
          {
            if (!ucOut.GetItemChecked(i))
              tables_out.Add((InOutTable)tablesOut[i]);
          }

          // меняем структуру
          XmlNodeList nodes;
          XmlNode nd;

          // IN
          foreach (InOutTable iot in tables_in)
          {
            //nodes = root1.SelectNodes("/rp:Report/rp:Body/rp:ReportItems/rp:Table[@Name='table1']/rp:Header/rp:TableRows", nsmgr);
            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_H", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_HC", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_HS", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_VC", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_VS", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_FC", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_FS", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            for (int cnt = 1; cnt <= 2; cnt++)
            {
              nodes = root1.SelectNodes("//rp:Table[@Name='table1']/rp:TableColumns", nsmgr);
              foreach (XmlNode n in nodes.Item(0).ChildNodes)
              {
                if (n.ChildNodes[0].InnerXml == "1.6cm")
                {
                  nodes.Item(0).RemoveChild(n);
                }
              }
            }

            //Width of container
            XmlNode containerWidth = root1.SelectSingleNode("/rp:Report/rp:Width", nsmgr);
            string twd = containerWidth.InnerXml;
            twd = twd.Replace('.', ',');
            string znach = twd.Substring(twd.Length - 2, 2);
            decimal dtwd = Convert.ToDecimal(twd.Substring(0, twd.Length - 2));
            //if (table_width + 2 * colWidth > dtwd)
            containerWidth.InnerXml = ((dtwd - 3.2m).ToString()).Replace(',', '.') + znach;
          }

          //OUT
          foreach (InOutTable iot in tables_out)
          {
            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_HO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_HCO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_HSO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_VCO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_VSO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_FCO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            nodes = root1.SelectNodes("//rp:TableCell/rp:ReportItems/rp:Textbox[@Name='" + String.Format("{0}_FSO", iot.Name) + "']", nsmgr);
            nd = nodes.Item(0).ParentNode.ParentNode;
            nodes.Item(0).ParentNode.ParentNode.ParentNode.RemoveChild(nd);

            for (int cnt = 1; cnt <= 2; cnt++)
            {
              nodes = root1.SelectNodes("//rp:Table[@Name='table1']/rp:TableColumns", nsmgr);
              foreach (XmlNode n in nodes.Item(0).ChildNodes)
              {
                if (n.ChildNodes[0].InnerXml == "1.6cm")
                {
                  nodes.Item(0).RemoveChild(n);
                }
              }
            }

            //Width of container
            XmlNode containerWidth = root1.SelectSingleNode("/rp:Report/rp:Width", nsmgr);
            string twd = containerWidth.InnerXml;
            twd = twd.Replace('.', ',');
            string znach = twd.Substring(twd.Length - 2, 2);
            decimal dtwd = Convert.ToDecimal(twd.Substring(0, twd.Length - 2));
            containerWidth.InnerXml = ((dtwd - 3.2m).ToString()).Replace(',', '.') + znach;
          }
          doc1.Save(rep.ReportPath);

          rep.BindDataSource("MoveGoodsDK_DS_Table0", 0);
          break;
      }

      //rep.BindDataSource("MoveGoodsDK_DS_Table0", 0);

      rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
      rep.AddParameter("DATE_TO", ucPeriod.DateToText);
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      //rep.AddParameter("SUPPLIER", ucSuppliers.TextValues());
      //rep.AddParameter("STORE", ucStores.TextValues());
      //rep.AddParameter("GOODS", ucGoods.TextValues());

      rep.ExecuteReport(this);
    }

    private void cbReportView_SelectedIndexChanged(object sender, EventArgs e)
    {
      ucIn.Enabled = ucOut.Enabled = cbAllIn.Enabled = cbAllOut.Enabled = cbReportView.SelectedIndex == 1;
    }


      private void ucContractor_BeforePluginShow(object sender, System.ComponentModel.CancelEventArgs e)
      {
          /*
          if (!SelfIsCenter())
              ucContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(C.ID_CONTRACTOR = (SELECT TOP 1 ID_CONTRACTOR FROM ENTERPRISE_BRANCH WHERE IS_SELF = 1))");
          else
              ucContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "(1=1)");
          */
      }
  }

  public class InOutTable
  {
    public InOutTable(int id, string name)
    {
      _id = id;
      _name = name;
    }

    private int _id;
    private string _name;

    public int ID
    {
      get { return _id; }
      private set { _id = value; }
    }

    public string Name
    {
      get { return _name; }
      private set { _name = value; }
    }
  }
}