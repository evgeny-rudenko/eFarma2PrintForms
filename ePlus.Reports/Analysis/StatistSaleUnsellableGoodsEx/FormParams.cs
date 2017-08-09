using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace RCBStatistSaleUnsellableGoods
{
  public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
  {
    public FormParams()
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

    private class ListData
    {
      private string _text = "";
      private string _data = "";

      public ListData(string text, string data)
      {
        _text = text;
        _data = data;
      }

      public string Text
      {
        get { return _text; }
        set { _text = value; }
      }
      public string Data
      {
        get { return _data; }
        set { _data = value; }
      }

      static public List<ListData> GetList(string[] texts, string[] datas)
      {
        List<ListData> list = new List<ListData>();
        int length = texts.Length > datas.Length ? texts.Length : datas.Length;
        for (int i = 0; i < length; i++)
        {
          string text = i < texts.Length ? texts[i] : "";
          string data = i < datas.Length ? datas[i] : "";
          list.Add(new ListData(text, data));
        }
        return list;
      }

      public override bool Equals(object obj)
      {
        ListData ld = obj as ListData;
        if (ld != null)
          return ld.Data.Equals(this.Data);
        return base.Equals(obj);
      }

      public override string ToString()
      {
        return Text;
      }
    }

    public IGridController gridParent = null;
    private List<ListData> listRows = ListData.GetList(new string[] { "Все", "1000", "500", "400", "300", "200", "100" }, new string[] { "0", "1000", "500", "400", "300", "200", "100" });

    private List<ListData> listSort = ListData.GetList(new string[] { "Медикаменты по алфавиту", "Начальный остаток", "Конечный остаток", "Итого расход", "Процент продаж", "Поставщики по алфавиту" }, new string[] { "G_RUSNAME", "G_OST_FROM", "G_OST_TO", "G_QTYOUT", "G_PERCENT", "G_SUPPLIER" });
    //private List<ListData> listSort = new List<ListData>[]
    //{
    //        ListData.GetList(new string[] { "Медикаменты по алфавиту", "Начальный остаток", "Конечный остаток", "Итого расход", "Процент продаж", "Поставщики по алфавиту" }, new string[] { "G_RUSNAME", "G_OST_FROM", "G_OST_TO", "G_QTYOUT", "G_PERCENT", "G_SUPPLIER" }),
    //};

    public void Print(string[] reportFiles)
    {
      if (rbCheckType.Checked && (!chbKKM.Checked && !chbOut.Checked && !chbMovement.Checked))
      {
        MessageBox.Show("Выберите виды расхода", "Предупреждение", MessageBoxButtons.OK);
        return;
      }
      string TypeOutText = string.Empty;
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      Utils.AddNode(root, "ORDER_BY", ((ListData)comboSort.SelectedItem).Data);
      Utils.AddNode(root, "SORT_ORDER", sortOrderComboBox.SelectedIndex == 0 ? "ASC" : "DESC");
      Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
      //Utils.AddNode(root, "ROW_COUNT", ((ListData)comboRows.SelectedItem).Data);
      if (rbPercent.Checked)
      {
        Utils.AddNode(root, "PERCENT", numericPercent.Value);
        Utils.AddNode(root, "QUANTITY", null);
      }
      else
      {
        Utils.AddNode(root, "PERCENT", null);
        Utils.AddNode(root, "QUANTITY", numericPercent.Value);
      }
      //Utils.AddNode(root, "TYPE_OUT", ((ListData)comboType.SelectedItem).Data);            
      XmlNode typeOut = Utils.AddNode(root, "TYPE_OUT");
      if (rbAllType.Checked)
      {
        for (int i = 1; i <= 3; i++)
          Utils.AddNode(typeOut, "TYPE_NUM", i);
        TypeOutText = "Все";
      }
      else
      {
        if (chbKKM.Checked)
        {
          Utils.AddNode(typeOut, "TYPE_NUM", 1);
          TypeOutText += "Чеки";
        }

        if (chbOut.Checked)
        {
          Utils.AddNode(typeOut, "TYPE_NUM", 2);
          if (TypeOutText.Length != 0)
            TypeOutText += ',';
          TypeOutText += "Расходные накладные";
        }

        if (chbMovement.Checked)
        {
          Utils.AddNode(typeOut, "TYPE_NUM", 3);
          if (TypeOutText.Length != 0)
            TypeOutText += ',';
          TypeOutText += "Перемещения";
        }
      }

      Utils.AddNode(root, "USE_GOODS_REPORT_NAME", chbGroupGoods.Checked ? 1 : 0);

      XmlNode store = Utils.AddNode(root, "STORE");
      foreach (DataRowItem row in stores.Items)
      {
        Utils.AddNode(store, "ID_STORE", row.Id);
      }

      XmlNode g = Utils.AddNode(root, "GOODS");
      //foreach (DataRowItem row in goods.Items)
      //{
      //    Utils.AddNode(g, "ID_GOODS", row.Id);
      //}

      XmlNode group = Utils.AddNode(root, "GROUPS");
      foreach (CatalogItem item in selectGoodsGroup.Items)
      {
        Utils.AddNode(group, "ID_GROUP", item.Id);
      }

      XmlNode tradeName = Utils.AddNode(root, "TRADE_NAME");
      foreach (DataRowItem row in ucTradeName.Items)
      {
        Utils.AddNode(tradeName, "ID_TRADE_NAME", row.Id);
      }

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];
      //rep.LoadDataAsync("STATIST_SALE_UNSELLABLE_GOODS_EX", doc.InnerXml,
      rep.LoadData("STATIST_SALE_UNSELLABLE_GOODS_EX", doc.InnerXml);
      rep.BindDataSource("Statist_UnsellableGoods_DS_Table", 1);
      rep.BindDataSource("Statist_UnsellableGoods_DS_Table1", 2);
      rep.BindDataSource("Statist_UnsellableGoods_DS_Table2", 0);
      rep.BindDataSource("Statist_UnsellableGoods_DS_Table21", 3);

      long nCountDays = (Int64)period.DateTo.ToOADate() - (Int64)period.DateFrom.ToOADate() + 1;
      rep.AddParameter("Pm_StoreName", stores.Items.Count == 0 ? "Все склады" : stores.ToCommaDelimetedStringList());
      rep.AddParameter("Pm_DateFrom", period.DateFrText);
      rep.AddParameter("Pm_DateTo", period.DateToText);
      rep.AddParameter("Pm_CountDays", nCountDays.ToString());
      //rep.AddParameter("Pm_TypeOut", comboType.SelectedIndex.ToString());
      rep.AddParameter("Pm_TypeOutText", TypeOutText);
      //rep.AddParameter("Pm_RowCount", comboRows.SelectedIndex == 0 ? "Все позиции" : comboRows.Text);
      rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Анализ продаж (непродаваемые товары)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
    }

    private void rbAllType_CheckedChanged(object sender, EventArgs e)
    {
      if (rbAllType.Checked)
        chbKKM.Enabled = chbOut.Enabled = chbMovement.Enabled = false;
      else
        chbKKM.Enabled = chbOut.Enabled = chbMovement.Enabled = true;
    }

    private void goods_BeforePluginShow(object sender, CancelEventArgs e)
    {
      //goods.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", 
      //    string.Format(" (NOT EXISTS (SELECT NULL FROM CONTRACTS_GOODS WHERE ID_CONTRACTS_GLOBAL='{0}' AND DATE_DELETED IS NULL) OR ID_GOODS IN (SELECT ID_GOODS FROM CONTRACTS_GOODS WHERE ID_CONTRACTS_GLOBAL = '{0}' AND DATE_DELETED IS NULL)) ", Invoice.ID_CONTRACTS_GLOBAL));
    }

    private void FormParam_Load(object sender, EventArgs e)
    {
        if (period != null)
        {
            period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            period.DateFrom = period.DateTo.AddDays(-13);
        }

        sortOrderComboBox.SelectedIndex = 0;
        
        foreach (ListData ld in listSort)
        comboSort.Items.Add(ld);
      foreach (ListData ld in listRows)
        comboRows.Items.Add(ld);

      if (!File.Exists(SettingsFilePath)) return;
      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("/XML");
      period.DateFrom = Utils.GetDate(root, "DATE_FROM");
      period.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList sort = root.SelectNodes("SORT");
      foreach (XmlNode node in sort)
      {
        ListData l = new ListData(Utils.GetString(node, "TEXT"), Utils.GetString(node, "DATA"));
        if (l.Data == null)
        {
          l.Data = listSort[0].Data;
          l.Text = listSort[0].Text;
        }
        comboSort.SelectedItem = l;
      }

      XmlNodeList rows = root.SelectNodes("ROWS");
      foreach (XmlNode node in rows)
      {
        ListData lr = new ListData(Utils.GetString(node, "TEXT"), Utils.GetString(node, "DATA"));
        if (lr.Data == null)
        {
          lr.Data = listRows[0].Data;
          lr.Text = listRows[0].Text;
        }
        comboRows.SelectedItem = lr;
      }
      //comboRows.SelectedIndex = Utils.GetInt(root, "ROWS");
      rbPercent.Checked = Utils.GetBool(root, "PERCENT");
      rbQuantity.Checked = Utils.GetBool(root, "QUANTITY");
      numericPercent.Value = Utils.GetDecimal(root, "NUM_PER");

      if (Utils.GetBool(root, "ALL_OUT_TYPE"))
      {
        rbAllType.Checked = true;
        rbCheckType.Checked = false;
      }
      else
      {
        rbCheckType.Checked = true;
        rbAllType.Checked = false;
      }
      chbKKM.Checked = Utils.GetBool(root, "KKM_TYPE");
      chbOut.Checked = Utils.GetBool(root, "OUT_TYPE");
      chbMovement.Checked = Utils.GetBool(root, "MOVE_TYPE");

      XmlNodeList groups = root.SelectNodes("GROUPS");
      foreach (XmlNode node in groups)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        CatalogItem ci = new CatalogItem();
        ci.Id = id;
        ci.Name = text;
        selectGoodsGroup.AddItem(ci);
      }

      chbGroupGoods.Checked = Utils.GetBool(root, "GOODS_GROUP");
      chbGoodCode.Checked = Utils.GetBool(root, "GOODS_CODE");
      auCheckBox.Checked = Utils.GetBool(root, "NOAU");

      XmlNodeList st = root.SelectNodes("STORES");
      foreach (XmlNode node in st)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        stores.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList tn = root.SelectNodes("TRADE_NAME");
      foreach (XmlNode node in tn)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucTradeName.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }

      XmlNodeList gd = root.SelectNodes("GOODS2");
      foreach (XmlNode node in gd)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        goods.AddRowItem(new DataRowItem(id, Guid.Empty, "", text));
      }
      sortOrderComboBox.SelectedIndex = Utils.GetInt(root, "SORT_ORDER");
    }

    private void FormParam_Shown(object sender, EventArgs e)
    {
      if (comboSort.SelectedIndex == -1)
        comboSort.SelectedIndex = 0;
      if (comboRows.SelectedIndex == -1)
        comboRows.SelectedIndex = 0;
    }

    private void FormParam_FormClosed(object sender, FormClosedEventArgs e)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      XmlNode sort = Utils.AddNode(root, "SORT");
      Utils.AddNode(sort, "DATA", ((ListData)comboSort.SelectedItem).Data);
      Utils.AddNode(sort, "TEXT", ((ListData)comboSort.SelectedItem).Text);
      XmlNode rows = Utils.AddNode(root, "ROWS");
      Utils.AddNode(rows, "DATA", ((ListData)comboRows.SelectedItem).Data);
      Utils.AddNode(rows, "TEXT", ((ListData)comboRows.SelectedItem).Text);
      Utils.AddNode(root, "PERCENT", rbPercent.Checked);
      Utils.AddNode(root, "QUANTITY", rbQuantity.Checked);
      Utils.AddNode(root, "NUM_PER", numericPercent.Value);
      Utils.AddNode(root, "ALL_OUT_TYPE", rbCheckType.Checked ? 0 : 1); //0 - не все виды расхода,1 - все
      Utils.AddNode(root, "SORT_ORDER", sortOrderComboBox.SelectedIndex);

      foreach (DataRowItem dri in stores.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in ucTradeName.Items)
      {
        XmlNode node = Utils.AddNode(root, "TRADE_NAME");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in goods.Items)
      {
        XmlNode node = Utils.AddNode(root, "GOODS2");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "KKM_TYPE", chbKKM.Checked);
      Utils.AddNode(root, "OUT_TYPE", chbOut.Checked);
      Utils.AddNode(root, "MOVE_TYPE", chbMovement.Checked);

      foreach (CatalogItem dr in selectGoodsGroup.Items)
      {
        XmlNode groups = Utils.AddNode(root, "GROUPS");
        Utils.AddNode(groups, "ID", dr.Id);
        Utils.AddNode(groups, "TEXT", dr.Name);
      }

      Utils.AddNode(root, "GOODS_GROUP", chbGroupGoods.Checked);
      Utils.AddNode(root, "NOAU", auCheckBox.Checked);
      Utils.AddNode(root, "GOODS_CODE", chbGoodCode.Checked);
      doc.Save(SettingsFilePath);
    }
  }
}