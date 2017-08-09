using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using ePlus.CommonEx.Reporting;

namespace EnterBalancesEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
            CreateSortControl();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkedListBox1.Enabled = false;
            cbSort.Enabled = false;
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "TYPE_REPORT", rbEnterBal.Checked ? "1" : "0");
            Utils.AddNode(root, "SORT_FIELD", ((ListData)cbSort.SelectedItem).Data);
            foreach (DataRowItem docs in ucRemains.Items)
                Utils.AddNode(root, "ID_DOC", docs.Id);

            ReportFormNew rep = new ReportFormNew();            
            rep.LoadData("IMPORT_REMAINS_REP_EX", doc.InnerXml);
            if (rbEnterBal.Checked)
            {
                rep.BindDataSource("EnterBalancesEx_DS_Table1", 0);
                rep.BindDataSource("EnterBalancesEx_DS_Table3", 1);
                rep.BindDataSource("EnterBalancesEx_DS_Table4", 2);
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "Enter_Balances_Ex.rdlc");
            }
            else
            {
                rep.BindDataSource("EnterBalancesEx_DS_Table2", 0);
                rep.BindDataSource("EnterBalancesEx_DS_Table3", 1);
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "Summary_Statment_Ex.rdlc");
            }
            string p_ch = "";
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
            }
            rep.AddParameter("p_ch", p_ch);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Ввод остатков"; }
        }

        public void CreateSortControl()
        {
        }

        private List<ListData> listType = ListData.GetList(new string[] { "Товар", "Поставщик", "Ставка НДС", "Место хранения" }, new string[] { "NAME_GOODS", "SUPPLIER", "RETAIL_VAT", "STORE_PLACE" });

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

        private void FormParams_Shown(object sender, EventArgs e)
        {
            cbSort.DataSource = listType;
            cbSort.DisplayMember = "Text";
            cbSort.SelectedIndex = 0;
        }

        private void rbEnterBal_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Enabled = rbEnterBal.Checked? true : false;
            cbSort.Enabled = rbEnterBal.Checked ? true : false;
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }
    }
}