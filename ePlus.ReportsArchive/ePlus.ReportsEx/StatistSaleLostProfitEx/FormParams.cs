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

namespace StatistSaleLostProfitEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        private string fileName = Path.Combine(Utils.TempDir(), "StatistLostProfit.xml");
        public FormParams()
        {
            InitializeComponent();
            if (period != null)
            {
                period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                period.DateFrom = period.DateTo.AddDays(-13);
            }
			sortOrderComboBox.SelectedIndex = 0;
        }

        ExpenceType eType = 0;

        private List<ListData> listSort = ListData.GetList(new string[] { "Медикаменты по алфавиту", "Сумма упущенной выгоды", "Количество дней отсутствия", "Средняя скорость продаж", "Итого расход" }, new string[] { "G_NAME", "LOST_PROFIT", "QDAYS_ZERO", "AVG_SALE", "G_QTY_EXP" });

        //private List<ListData>[] listSort = new List<ListData>[]
        //    {
        //        ListData.GetList(new string[] { "Медикаменты по алфавиту", "Сумма упущенной выгоды", "Количество дней отсутствия", "Средняя скорость продаж", "Итого расход", "Поставищики по алфавиту"}, new string[] { "G_RUSNAME", "G_SUMPROFIT", "G_DAYSOUT", "G_AVG", "G_QTY_EXP", "G_SUPPLIER" }),
        //    };
        
        private void FormParams_Shown(object sender, EventArgs e)
        {
            if (comboSort.SelectedIndex == -1)
                comboSort.SelectedIndex = 1;
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

        public string ReportName
        {
            get { return "Анализ продаж(упущенная выгода)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
        }

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
            //Utils.AddNode(root, "ROW_COUNT", ((ListData)comboRows.SelectedItem).Data);
            //Utils.AddNode(root, "TYPE_OUT", ((ListData)comboType.SelectedItem).Data);            
            XmlNode typeOut = Utils.AddNode(root, "TYPE_OUT");
            if ((eType & ExpenceType.All) == ExpenceType.All)
            {
                for (int i = 1; i <= 3; i++)
                    Utils.AddNode(typeOut, "TYPE_NUM", i);
                TypeOutText = "Все";
            }
            else
            {
                if ((eType & ExpenceType.Cheque) == ExpenceType.Cheque)
                {
                    Utils.AddNode(typeOut, "TYPE_NUM", 1);
                    TypeOutText += "Чеки";
                }

                if ((eType & ExpenceType.InvoiceOut) == ExpenceType.InvoiceOut)
                {
                    Utils.AddNode(typeOut, "TYPE_NUM", 2);
                    if (TypeOutText.Length != 0)
                        TypeOutText += ',';
                    TypeOutText += "Расходные накладные";
                }

                if ((eType & ExpenceType.Movement) == ExpenceType.Movement)
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
            foreach (DataRowItem row in goods.Items)
            {
                Utils.AddNode(g, "ID_GOODS", row.Id);
            }

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
            rep.LoadData("STATIST_SALE_LOST_PROFIT_EX", doc.InnerXml);
            rep.BindDataSource("STATIST_SALE_LOST_DS_Table", 0);           

            long nCountDays = (Int64)period.DateTo.ToOADate() - (Int64)period.DateFrom.ToOADate() + 1;
            rep.AddParameter("Pm_StoreName", stores.Items.Count == 0 ? "Все склады" : stores.ToCommaDelimetedStringList());
            //rep.AddParameter("Pm_GoodsName", goods.Items.Count == 0 ? "Все товары" : goods.ToCommaDelimetedStringList());
            rep.AddParameter("Pm_DateFrom", period.DateFrText);
            rep.AddParameter("Pm_DateTo", period.DateToText);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
            //rep.AddParameter("Pm_CountDays", nCountDays.ToString());
            //rep.AddParameter("Pm_TypeOut", comboType.SelectedIndex.ToString());
            //rep.AddParameter("Pm_TypeOutText", TypeOutText);
            //rep.AddParameter("Pm_RowCount", comboRows.SelectedIndex == 0 ? "Все позиции" : comboRows.Text);
            rep.ExecuteReport(this);
        }
        private void chbKKM_CheckedChanged(object sender, EventArgs e)
        {
            if (chbKKM.Checked)
                eType = (ExpenceType)((int)eType | (int)ExpenceType.Cheque);
            else
                eType = (ExpenceType)((int)eType ^ (int)ExpenceType.Cheque);
        }

        private void chbOut_CheckedChanged(object sender, EventArgs e)
        {
            if (chbOut.Checked)
                eType = (ExpenceType)((int)eType | (int)ExpenceType.InvoiceOut);
            else
                eType = (ExpenceType)((int)eType ^ (int)ExpenceType.InvoiceOut);
        }

        private void chbMovement_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMovement.Checked)
                eType = (ExpenceType)((int)eType | (int)ExpenceType.Movement);
            else
                eType = (ExpenceType)((int)eType ^ (int)ExpenceType.Movement);
        }

        private void rbAllType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllType.Checked)
                eType = (ExpenceType)((int)ExpenceType.All);
            else
                eType = (ExpenceType)((int)ExpenceType.None);

            if (rbAllType.Checked)
                chbKKM.Enabled = chbOut.Enabled = chbMovement.Enabled = false;
            else
                chbKKM.Enabled = chbOut.Enabled = chbMovement.Enabled = true;
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            foreach (ListData ld in listSort)
                comboSort.Items.Add(ld);

            if (!File.Exists(fileName)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
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
            //comboRows.SelectedIndex = Utils.GetInt(root, "ROWS");
            if (Utils.GetBool(root, "TYPE"))
            {
                rbAllType.Checked = true;
                rbCheckType.Checked = false;
            }
            else
            {
                rbCheckType.Checked = true;
                rbAllType.Checked = false;
                chbKKM.Checked = Utils.GetBool(root, "KKM_TYPE");
                chbOut.Checked = Utils.GetBool(root, "OUT_TYPE");
                chbMovement.Checked = Utils.GetBool(root, "MOVE_TYPE");
            }
            XmlNodeList store = root.SelectNodes("STORE");
            foreach (XmlNode node in store)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                stores.Items.Add(dri);
            }
            XmlNodeList trade_name = root.SelectNodes("TRADE_NAME");
            foreach (XmlNode node in trade_name)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                ucTradeName.Items.Add(dri);
            }
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
            XmlNodeList good = root.SelectNodes("GOODS");
            foreach (XmlNode node in good)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
                goods.Items.Add(dri);
            }
        }

        private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            XmlNode sort = Utils.AddNode(root, "SORT");
            Utils.AddNode(sort, "DATA", ((ListData)comboSort.SelectedItem).Data);
            Utils.AddNode(sort, "TEXT", ((ListData)comboSort.SelectedItem).Text);
            //Utils.AddNode(root, "SORT", comboSort.SelectedIndex);
            //Utils.AddNode(root, "ROWS", comboRows.SelectedIndex);
            Utils.AddNode(root, "TYPE", rbCheckType.Checked ? 0 : 1); //0 - не все виды расхода,1 - все
            if (rbCheckType.Checked)
            {
                Utils.AddNode(root, "KKM_TYPE", chbKKM.Checked);
                Utils.AddNode(root, "OUT_TYPE", chbOut.Checked);
                Utils.AddNode(root, "MOVE_TYPE", chbMovement.Checked);
            }
            foreach (DataRowItem dr in stores.Items)
            {
                XmlNode store = Utils.AddNode(root, "STORE");
                Utils.AddNode(store, "ID", dr.Id);
                Utils.AddNode(store, "TEXT", dr.Text);
            }

            foreach (DataRowItem dr in ucTradeName.Items)
            {
                XmlNode tradeName = Utils.AddNode(root, "TRADE_NAME");
                Utils.AddNode(tradeName, "ID", dr.Id);
                Utils.AddNode(tradeName, "TEXT", dr.Text);
            }

            foreach (CatalogItem dr in selectGoodsGroup.Items)
            {
                XmlNode groups = Utils.AddNode(root, "GROUPS");
                Utils.AddNode(groups, "ID", dr.Id);
                Utils.AddNode(groups, "TEXT", dr.Name);
            }

            foreach (DataRowItem dr in goods.Items)
            {
                XmlNode good = Utils.AddNode(root, "GOODS");
                Utils.AddNode(good, "ID", dr.Id);
                Utils.AddNode(good, "TEXT", dr.Text);
            }
            Utils.AddNode(root, "GOODS_GROUP", chbGroupGoods.Checked);
            doc.Save(fileName);
        }
    }
    [Flags]
    public enum ExpenceType : int { None = 0, Cheque = 1, InvoiceOut = 2, Movement = 4, All = 7 }
}