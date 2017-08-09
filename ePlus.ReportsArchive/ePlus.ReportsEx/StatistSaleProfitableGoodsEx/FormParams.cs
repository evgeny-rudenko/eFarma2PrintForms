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

namespace StatistSaleProfitableGoodsEx
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
        private List<ListData> listType = ListData.GetList(new string[] { "Все виды расхода товара", "Продажа по ККМ и расходным накладным"}, new string[] { "0", "1"});
        private List<ListData> listRows = ListData.GetList(new string[] { "Все", "1000", "500", "400", "300", "200", "100" }, new string[] { "0", "1000", "500", "400", "300", "200", "100" });
        private List<ListData>[] listSort = new List<ListData>[]
        {
            ListData.GetList(new string[] { "Медикаменты по алфавиту", "Сумма оборота", "Итого расход", "Доход", "Процент от общего дохода по продажам", "Процент от суммы реализации", "Поставщики по алфавиту" }, new string[] { "G_RUSNAME", "G_SUMOUT", "G_QTYOUT", "G_SUMADD", "G_PERCENTADD", "G_PERCENTSUMOUT", "G_SUPPLIER" }),
        };

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            //Utils.AddNode(root, "TYPE_REPORT", ((ListData)comboReport.SelectedItem).Data);
            Utils.AddNode(root, "PERCENT", numericPercent.Value);
            Utils.AddNode(root, "ORDER_BY", ((ListData)comboSort.SelectedItem).Data);
            //Utils.AddNode(root, "ROW_COUNT", ((ListData)comboRows.SelectedItem).Data);
            Utils.AddNode(root, "TYPE_OUT", ((ListData)comboType.SelectedItem).Data);
            //Utils.AddNode(root, "PARTS", checkParts.Checked ? 1 : 0);
            Utils.AddNode(root, "USE_GOODS_REPORT_NAME", chbGroupGoods.Checked ? 1 : 0);
            foreach (DataRowItem row in stores.Items)
            {
                XmlNode store = Utils.AddNode(root, "STORE");
                Utils.AddNode(store, "ID_STORE", row.Id);
            }

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("STATIST_SALE_PROFITABLE_GOODS_EX", doc.InnerXml);

            rep.BindDataSource("Statist_Sale_DS_Table2", 0);
            rep.BindDataSource("Statist_Sale_DS_Table", 1);
            rep.BindDataSource("Statist_Sale_DS_Table1", 2);
            
            long nCountDays = (Int64)period.DateTo.ToOADate() - (Int64)period.DateFrom.ToOADate() + 1;
            rep.AddParameter("Pm_StoreName", stores.Items.Count == 0 ? "Все склады" : stores.ToCommaDelimetedStringList());
            //rep.AddParameter("Pm_GoodsName", goods.Items.Count == 0 ? "Все товары" : goods.ToCommaDelimetedStringList());
            rep.AddParameter("Pm_DateFrom", period.DateFrText);
            rep.AddParameter("Pm_DateTo", period.DateToText);
            rep.AddParameter("Pm_CountDays", nCountDays.ToString());
            rep.AddParameter("Pm_TypeOut", comboType.SelectedIndex.ToString());
            rep.AddParameter("Pm_TypeOutText", comboType.Text);
            rep.AddParameter("Pm_Percent", numericPercent.Value.ToString());
            //rep.AddParameter("Pm_RowCount", comboRows.SelectedIndex == 0 ? "Все позиции" : comboRows.Text);
            //rep.AddParameter("Pm_TypeReport", comboReport.SelectedIndex.ToString());
            rep.AddParameter("Pm_TypeOutText", comboType.Text);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Анализ продаж (прибыльные товары)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
        }

        private void FormParams_Shown(object sender, EventArgs e)
        {
            if (numericPercent != null) numericPercent.Value = 80;
            comboSort.DataSource = listSort[0];
            comboSort.DisplayMember = "Text";
            comboSort.SelectedIndex = 0;
            comboType.DataSource = listType;
            comboType.DisplayMember = "Text";
            comboType.SelectedIndex = 0;
        }
    }
}