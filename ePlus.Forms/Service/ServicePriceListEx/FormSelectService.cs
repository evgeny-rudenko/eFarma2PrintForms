using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Server;
using System.Data.SqlClient;

namespace FCSServicePriceList
{
    public partial class FormSelectService : Form
    {
        private ServicePriceListEx MainClass;
        BindingSource binding = new BindingSource();
        Guid GUPriceList;
        public FormSelectService(ServicePriceListEx mainClass, Guid gUPriceList)
        {
            InitializeComponent();
            MainClass = mainClass;
            GUPriceList = gUPriceList;
        }
        public void SelectRows()
        {
            List<Guid> GUPriceList = new List<Guid>();
            for(int i=0;i<dataGridView1.SelectedRows.Count;i++)
            {
               DataGridViewRow Row = dataGridView1.SelectedRows[i];
               try
               {
                   GUPriceList.Add((Guid)Row.Cells["ID_SERVICE_PRICE_LIST_ITEM"].Value);
               }
               catch (Exception e)
               {
               }
            }

            ServicePriceListEx.AddGUPriceList(GUPriceList);
            this.Close();
        }
      
        
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            SelectRows();
        }
         
        private void FillGrig()
        {
            DataService_BL bl = new DataService_BL();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand(string.Format(@"	
                                                        select 
	                                                        ID_SERVICE_PRICE_LIST_ITEM,
	                                                        NAME,
	                                                        NAME_FULL,
	                                                        PRICE_SAL
                                                        FROM SERVICE_PRICE_LIST_ITEM
                                                        WHERE 
	                                                        ID_SERVICE_PRICE_LIST ='{0}'", GUPriceList), connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataAdapter Adapt = new SqlDataAdapter(command);
                Adapt.Fill(ds, "Services");
                binding.DataSource = ds.Tables["Services"].DefaultView;
                dataGridView1.DataSource = binding;
                dataGridView1.Columns["ID_SERVICE_PRICE_LIST_ITEM"].Visible = false;
                dataGridView1.Columns["NAME"].HeaderText = "Имя";
                dataGridView1.Columns["NAME_FULL"].HeaderText = "Полное имя";
                dataGridView1.Columns["PRICE_SAL"].HeaderText = "Цена";
                dataGridView1.Columns["PRICE_SAL"].DefaultCellStyle.Format = "N2";
            }
        }

        private void FormCodeAP_Load(object sender, EventArgs e)
        {
            FillGrig();
            dataGridView1.Focus();
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.SelectAll();
                //MessageBox.Show("dd");
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SelectRows();
        }
        /*
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            CodeAPFind();
        }
         * */
        /*
        public void CodeAPFind()
        {
            try
            {
                if (maskedTextBox1.Text.Length==0)
                    binding.Filter = String.Format("1 = 1");
                else
                    binding.Filter = String.Format("NAME_FULL = '{0}'", maskedTextBox1.Text.Replace("?", "").Replace("*", ""));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        */
        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dataGridView1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows[0].Selected = true;
            dataGridView1.SelectAll();
            dataGridView1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectRows();
        }
    }
}