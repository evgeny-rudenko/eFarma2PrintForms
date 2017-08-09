using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace StatistSaleEsEx
{
    public partial class StatistEsForm : ExternalReportForm, IExternalReportFormMethods
    {
        public StatistEsForm()
        {
            InitializeComponent();
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

        #region Fields

        public IGridController gridParent = null;
        private List<ListData> listType = ListData.GetList(new string[] { "Все виды расхода товара", "Продажа по ККМ и расходным накладным", "Возвраты поставщику", "Перемещение" }, new string[] { "0", "1", "2", "3" });
        private List<ListData> listRows = ListData.GetList(new string[] { "Все", "1000", "500", "400", "300", "200", "100" }, new string[] { "0", "1000", "500", "400", "300", "200", "100" });
        private List<ListData> listReport = ListData.GetList(new string[] { "Полный", "Прибыльные товары", "Непродаваемые товары", "Упущенная выгода" }, new string[] { "0", "1", "2", "3" });
        private Button buttonForm;
        private CheckBox chkUseGoodsReportName;
        private List<ListData>[] listSort = new List<ListData>[]
        {
            ListData.GetList(new string[] { "Медикаменты по алфавиту", "Сумма оборота", "Итого расход", "Доход", "Процент от общего дохода по продажам", "Процент от суммы реализации" }, new string[] { "G_RUSNAME", "G_SUMOUT", "G_QTYOUT", "G_SUMADD", "G_PERCENTADD", "G_PERCENTSUMOUT" }),
            ListData.GetList(new string[] { "Медикаменты по алфавиту", "Сумма оборота", "Итого расход", "Доход", "Процент от общего дохода по продажам", "Процент от суммы реализации" }, new string[] { "G_RUSNAME", "G_SUMOUT", "G_QTYOUT", "G_SUMADD", "G_PERCENTADD", "G_PERCENTSUMOUT" }),
            ListData.GetList(new string[] { "Начальный остаток", "Конечный остаток", "Итого расход", "Процент от общего дохода по продажам" }, new string[] { "G_STOCKQTYFROM", "G_STOCKQTYTO", "G_QTYOUT", "G_PERCENTADD" }),
            ListData.GetList(new string[] { "Сумма упущенной выгоды", "Количество дней отсутствия", "Средний доход", "Итого расход" }, new string[] { "G_DOCADD", "G_DAYABSENT", "G_AVGSUMADD", "G_QTYOUT" })
        };

        #endregion

        private void SetDefaultValues()
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
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "TYPE_REPORT", ((ListData)comboReport.SelectedItem).Data);
            Utils.AddNode(root, "PERCENT", numericPercent.Value);
            Utils.AddNode(root, "ORDER_BY", ((ListData)comboSort.SelectedItem).Data);
            Utils.AddNode(root, "ROW_COUNT", ((ListData)comboRows.SelectedItem).Data);
            Utils.AddNode(root, "TYPE_OUT", ((ListData)comboType.SelectedItem).Data);
            Utils.AddNode(root, "PARTS", checkParts.Checked ? 1 : 0);
            Utils.AddNode(root, "USE_GOODS_REPORT_NAME", chkGoodsGroup.Checked ? 1 : 0);
            stores.AddItems(root, "ID_STORE");
            goods.AddItems(root, "KOD_ES");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REP_STATIST_SALE_ES_EX", doc.InnerXml);
            rep.BindDataSource("StatistSaleEsEx_DS_Table", 0);
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
            rep.ExecuteReport(this);

        }

        public string ReportName
        {
            get { return "Анализ продаж"; }
        }

        private void comboReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericPercent.Enabled = (comboReport.SelectedIndex == 1) || (comboReport.SelectedIndex == 2);
            if (sender != null) numericPercent.Value = comboReport.SelectedIndex == 1 ? 80 : 10;

            comboRows.Enabled = (comboReport.SelectedIndex == 0);
            if (sender != null) comboRows.SelectedIndex = 0;

            goods.Enabled = (comboReport.SelectedIndex == 0);
            if (!goods.Enabled) goods.Items.Clear();

            int nIndex = sender == null ? comboSort.SelectedIndex : 0;

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
    }
}