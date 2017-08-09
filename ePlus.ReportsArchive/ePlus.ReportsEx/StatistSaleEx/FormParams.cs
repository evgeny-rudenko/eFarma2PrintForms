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
using ePlus.MetaData.ExternReport;

namespace StatistSaleEx
{
  public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
  {
    public FormParams()
    {
      InitializeComponent();
      if (period != null)
      {
        period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
        period.DateFrom = period.DateTo.AddDays(-13);
      }
      Timer timer = new Timer();
      timer.Interval = 100;
      timer.Tick += new EventHandler(FormParams_Shown);
      timer.Start();

      cbZNVLS.Items.Clear();
      cbPKKN.Items.Clear();
      Array a = Enum.GetValues(typeof(TriStateFilter));
      foreach (TriStateFilter f in a)
      {
        cbZNVLS.Items.Add(new TriStateFilterDescription(f));
        cbPKKN.Items.Add(new TriStateFilterDescription(f));        
      }

      SetDefaultValues();
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
    }

    public IGridController gridParent = null;
    private List<ListData> listType = ListData.GetList(new string[] { "Все виды расхода товара", "Продажа по ККМ и расходным накладным", "Перемещение" }, new string[] { "0", "1", "2" });
    private List<ListData> listRows = ListData.GetList(new string[] { "Все", "1000", "500" , "400" , "300" , "200" , "100" }, new string[] { "0", "1000", "500" , "400" , "300" , "200" , "100" });
    private List<ListData> listReport = ListData.GetList(new string[] { "Полный", "Прибыльные товары", "Непродаваемые товары" , "Упущенная выгода" }, new string[] { "0", "1", "2" , "3" });

    private List<ListData>[] listSort = new List<ListData>[]
        {
            ListData.GetList(new string[] { "Код", "Медикаменты по алфавиту", "Сумма оборота", "Итого расход", "Доход", "Процент от общего дохода по продажам", "Процент от суммы реализации", "Поставщики по алфавиту" }, new string[] { "G_CODE", "G_RUSNAME", "G_SUMOUT", "G_QTYOUT", "G_SUMADD", "G_PERCENTADD", "G_PERCENTSUMOUT", "G_SUPPLIER" }),
            ListData.GetList(new string[] { "Код", "Медикаменты по алфавиту", "Сумма оборота", "Итого расход", "Доход", "Процент от общего дохода по продажам", "Процент от суммы реализации", "Поставщики по алфавиту" }, new string[] { "G_CODE", "G_RUSNAME", "G_SUMOUT", "G_QTYOUT", "G_SUMADD", "G_PERCENTADD", "G_PERCENTSUMOUT", "G_SUPPLIER" }),
            ListData.GetList(new string[] { "Начальный остаток", "Конечный остаток", "Итого расход", "Процент от общего дохода по продажам" }, new string[] { "G_STOCKQTYFROM", "G_STOCKQTYTO", "G_QTYOUT", "G_PERCENTADD" }),
            ListData.GetList(new string[] { "Сумма упущенной выгоды", "Количество дней отсутствия", "Средний доход", "Итого расход" }, new string[] { "G_DOCADD", "G_DAYABSENT", "G_AVGSUMADD", "G_QTYOUT" })
        };

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      Utils.AddNode(root, "TYPE_REPORT", ((ListData)comboReport.SelectedItem).Data);
      Utils.AddNode(root, "PERCENT", numericPercent.Value);
      Utils.AddNode(root, "ORDER_BY", ((ListData)comboSort.SelectedItem).Data);
      Utils.AddNode(root, "ROW_COUNT", ((ListData)comboRows.SelectedItem).Data);
      Utils.AddNode(root, "TYPE_OUT", ((ListData)comboType.SelectedItem).Data);
      Utils.AddNode(root, "PARTS", checkParts.Checked ? 1 : 0);
      Utils.AddNode(root, "USE_GOODS_REPORT_NAME", chbGroupGoods.Checked ? 1 : 0);
      
      TriStateFilterDescription zv = cbZNVLS.SelectedItem as TriStateFilterDescription;
      if (zv != null && zv.TriStateFilter != TriStateFilter.Unused)
        Utils.AddNode(root, "ZNVLS", zv.TriStateFilter == TriStateFilter.Yes);

      TriStateFilterDescription pkkn = cbPKKN.SelectedItem as TriStateFilterDescription;
      if (pkkn != null && pkkn.TriStateFilter != TriStateFilter.Unused)
        Utils.AddNode(root, "PKKN", pkkn.TriStateFilter == TriStateFilter.Yes);

      foreach (DataRowItem row in stores.Items)
      {
        XmlNode store = Utils.AddNode(root, "STORE");
        Utils.AddNode(store, "ID_STORE", row.Id);
      }
      foreach (DataRowItem row in goods.Items)
      {
        XmlNode store = Utils.AddNode(root, "GOODS");
        Utils.AddNode(store, "ID_GOODS", row.Id);
      }
      foreach (DataRowItem row in goodsKind.Items)
      {
        XmlNode store = Utils.AddNode(root, "GOODS_KIND");
        Utils.AddNode(store, "ID_GOODS_KIND", row.Id);
      }
      

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];
      //if (((ListData)comboReport.SelectedItem).Data == "0")
      //    rep.LoadData("STATIST_SALE_ALL_EX", doc.InnerXml);
      //else if (((ListData)comboReport.SelectedItem).Data == "1")
      //    rep.LoadData("STATIST_SALE_PROFITABLE_GOODS_EX", doc.InnerXml);
      //else if (((ListData)comboReport.SelectedItem).Data == "2")
      //    rep.LoadData("STATIST_SALE_UNSELLABLE_GOODS_EX", doc.InnerXml);
      //else rep.LoadData("STATIST_SALE_LOST_PROFIT_EX", doc.InnerXml);
      rep.LoadData("STATIST_SALE_EX", doc.InnerXml);
      rep.BindDataSource("Statist_Sale_DS_Table", 0);
      //rep.BindDataSource("Statist_Sale_DS_Table1", 1);

      long nCountDays = (Int64)period.DateTo.ToOADate() - (Int64)period.DateFrom.ToOADate() + 1;
      rep.AddParameter("Pm_StoreName", stores.Items.Count == 0 ? "Все склады" : stores.ToCommaDelimetedStringList());
      rep.AddParameter("Pm_GoodsName", goods.Items.Count == 0 ? "Все товары" : goods.ToCommaDelimetedStringList());
      rep.AddParameter("Pm_DateFrom", period.DateFrText);
      rep.AddParameter("Pm_DateTo", period.DateToText);
      rep.AddParameter("Pm_CountDays", nCountDays.ToString());
      rep.AddParameter("Pm_TypeOut", comboType.SelectedIndex.ToString());
      rep.AddParameter("Pm_TypeOutText", comboType.Text);
      rep.AddParameter("Pm_Percent", numericPercent.Value.ToString());
      rep.AddParameter("Pm_RowCount", comboRows.SelectedIndex == 0 ? "Все позиции" : comboRows.Text);
      rep.AddParameter("Pm_TypeReport", comboReport.SelectedIndex.ToString());
      rep.AddParameter("Pm_TypeReportText", comboReport.SelectedIndex == 0 ? "" : ": " + comboReport.Text.ToLower());
      rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Анализ продаж"; }
    }

    private void FormParams_Shown(object sender, EventArgs e)
    {
      comboReport.SelectedIndexChanged += new EventHandler(ComboReport_SelectedIndexChanged);
      ComboReport_SelectedIndexChanged(null, null);
      Timer timer = sender as Timer;
      if (timer != null) timer.Dispose();
    }

    private void ComboReport_SelectedIndexChanged(object o, EventArgs e)
    {
      numericPercent.Enabled = (comboReport.SelectedIndex == 1) || (comboReport.SelectedIndex == 2);
      if (o != null) numericPercent.Value = comboReport.SelectedIndex == 1 ? 80 : 10;

      comboRows.Enabled = (comboReport.SelectedIndex == 0);
      if (o != null) comboRows.SelectedIndex = 0;

      goods.Enabled = (comboReport.SelectedIndex == 0);
      if (!goods.Enabled) goods.Items.Clear();

      int nIndex = o == null ? comboSort.SelectedIndex : 0;

      ListData listData = comboReport.SelectedItem as ListData;
      if (listData != null)
      {
        int num = Utils.GetInt(listData.Data);
        if (num >= 0 && num < listSort.Length)
        {
          comboSort.DataSource = listSort[num];
          comboSort.DisplayMember = "Text";
          comboSort.SelectedIndex = nIndex;
        }
      }
    }

    protected void SetDefaultValues()
    {
      if (checkParts != null) checkParts.Checked = false;
      if (numericPercent != null) numericPercent.Value = 80;
      if (period != null)
      {
        period.DateTo = DateTime.Now;
        period.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
      }
      if (comboRows != null)
      {
        comboRows.DataSource = listRows;
        comboRows.DisplayMember = "Text";
        comboRows.SelectedIndex = 0;
      }
      if (comboSort != null)
      {
        comboSort.DataSource = listSort[0];
        comboSort.DisplayMember = "Text";
        comboSort.SelectedIndex = 0;
      }
      if (comboType != null)
      {
        comboType.DataSource = listType;
        comboType.DisplayMember = "Text";
        comboType.SelectedIndex = 0;
      }
      if (comboReport != null)
      {
        comboReport.DataSource = listReport;
        comboReport.DisplayMember = "Text";
        comboReport.SelectedIndex = 0;
      }

      cbZNVLS.SelectedItem = new TriStateFilterDescription(TriStateFilter.Unused);
      cbPKKN.SelectedItem = new TriStateFilterDescription(TriStateFilter.Unused);
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
    }
  }


  internal enum TriStateFilter { Unused, Yes, No }

  internal class TriStateFilterDescription
  {
    private TriStateFilter triStateFilter = TriStateFilter.Unused;

    public TriStateFilter TriStateFilter
    {
      get { return triStateFilter; }
    }

    private string description;

    public string Description
    {
      get { return description; }
    }

    public TriStateFilterDescription(TriStateFilter triStateFilter)
    {
      this.triStateFilter = triStateFilter;
      switch (triStateFilter)
      {
        case TriStateFilter.Unused:
          description = "<Нет фильтра>";
          break;
        case TriStateFilter.Yes:
          description = "Да";
          break;
        case TriStateFilter.No:
          description = "Нет";
          break;
      }
    }

    public override string ToString()
    {
      return description;
    }

    public override bool Equals(object obj)
    {
      TriStateFilterDescription d = obj as TriStateFilterDescription;
      if (d != null)
        return d.triStateFilter == this.triStateFilter;
      return base.Equals(obj);
    }
  }
}