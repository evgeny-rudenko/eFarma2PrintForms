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
using System.Data.SqlClient;

namespace R23MCardOfGoodEx
{
    public partial class CardOfGoodParams : ExternalReportForm, IExternalReportFormMethods
    {
        private class DocState
        {
            private string state;
            private string name;

            public string State
            {
                get { return state; }
                set { state = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public DocState(string state, string name)
            {
                this.state = state;
                this.name = name;
            }
            public override string ToString()
            {
                return name;
            }
        }

        public CardOfGoodParams()
        {
            InitializeComponent();

            ClearValues();
        }

        public void Print(string[] reportFiles)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбрана серия!");
                return;
            }

            ReportFormNew rep = new ReportFormNew();

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);

            Utils.AddNode(root, "SORT_DOC", sortComboBox.SelectedIndex);
            Utils.AddNode(root, "ID_GOODS", zaprosView.SelectedRows[0].Cells[0].Value.ToString());
            Utils.AddNode(root, "SERIES_NUMBER", listBox1.SelectedItem.ToString());

            Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
            if (auCheckBox.Checked)
                rep.AddParameter("AU", "Только внутри АУ");
            else
                rep.AddParameter("AU", "");

            if (typeComboBox.SelectedIndex == 0)
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CardOfGood.rdlc");
            else if (typeComboBox.SelectedIndex == 1)
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CardOfGood_Brak.rdlc");

            rep.LoadData("REPEX_CARD_OF_GOOD", doc.InnerXml);

            rep.BindDataSource("DocsRegistry_DS_Table", 0);
            rep.BindDataSource("DocsRegistry_DS_Table1", 1);
            rep.AddParameter("ROW_COUNT", rep.DataSource.Tables[0].Rows.Count.ToString());

            rep.ExecuteReport(this);
        }


        public string ReportName
        {
            get { return "Карточка товара (КФ)"; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        private void ClearValues()
        {
            sortComboBox.SelectedIndex = 0;
            typeComboBox.SelectedIndex = 0;

            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------
            using (SqlConnection cnnDB = new SqlConnection(connectionString))
            {
                string zapros;
                zapros = "select distinct ID_GOODS=G.id_goods, GOOD=G.name, PRODUCER=PROD.name, COUNTRY=CNT.name from lot L ";
                zapros = zapros + " inner join goods G on L.id_goods = G.id_goods ";
                zapros = zapros + " inner join producer PROD on PROD.id_producer = G.id_producer ";
                zapros = zapros + " inner join country CNT on CNT.id_country = PROD.id_country ";
                zapros = zapros + " where G.name like '%" + textBox1.Text.ToString() + "%' ";
                zapros = zapros + " order by G.name, PROD.name, CNT.name";

                SqlDataAdapter da = new SqlDataAdapter(zapros, cnnDB);
                DataSet ds = new DataSet();
                da.Fill(ds, "zaprosView");
                zaprosView.DataSource = ds.Tables["zaprosView"].DefaultView;
                zaprosView.Columns["ID_GOODS"].Visible = false;
                zaprosView.Columns["GOOD"].HeaderText = "Наименование";
                zaprosView.Columns["PRODUCER"].HeaderText = "Производитель";
                zaprosView.Columns["COUNTRY"].HeaderText = "Страна";

            }
            //-----------------------------------------------------
        }

        private void zaprosView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Clear();

            if (zaprosView.SelectedRows.Count == 0)
                return;
            if (zaprosView.SelectedRows[0].Cells[0].Value.ToString() == "")
                return;



            StringBuilder queryString = new StringBuilder("");
            queryString.Append("Select distinct series_number from series where id_goods=");
            queryString.Append(zaprosView.SelectedRows[0].Cells[0].Value.ToString());
            queryString.Append(" order by series_number");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listBox1.Items.Add(reader[0]);
                }
                reader.Close();
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
        }

    }



}