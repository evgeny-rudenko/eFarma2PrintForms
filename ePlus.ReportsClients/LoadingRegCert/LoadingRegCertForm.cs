using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Controls;
using ePlus.CommonEx.DataAccess;

using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace LoadingRegCert
{
    public partial class LoadingRegCertForm : ExternalReportForm, IExternalReportFormMethods
    {
        List<ITEM> items = new List<ITEM>();
        List<INVOICE> invoiceList = new List<INVOICE>();

        public LoadingRegCertForm()
        {
            InitializeComponent();
        }

        #region IExternalReportFormMethods Members

        public void Print(string[] reportFiles)
        {
            if (ucInvoice.Items.Count==0 || chlbFiles.Items.Count==0)
            {
                MessageBox.Show("Выберите накладные и файлы загрузки", "Предупреждение", MessageBoxButtons.OK);
                return;
            }

            foreach (DataRowItem dr in ucInvoice.Items)
            {
                INVOICE invoice = new INVOICE();
                invoice.ID_INVOICE_GLOBAL = dr.Guid;
                invoiceList.Add(invoice);
            }

            items.ForEach(delegate(ITEM _item) { _item.ORGAN = tbOrgan.Text; });

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            XmlNode invoicesNode = Utils.AddNode(root, "INVOICES");
            List<ITEM> itemCheced = new List<ITEM>();
            for (int i = 0; i < chlbFiles.Items.Count; i++)
                if (chlbFiles.GetItemChecked(i))
                    itemCheced.AddRange(items.FindAll(delegate(ITEM _item)
                                                          {
                                                              return Path.GetFileNameWithoutExtension(_item.FILE_NAME) ==
                                                                     (string) chlbFiles.Items[i];
                                                          }));
            foreach (INVOICE invoice in invoiceList)
            {
                //XmlNode invoiceNode = Utils.AddNode(invoicesNode, "INVOICE");
                invoice.ToXml(invoicesNode);
            }

            XmlNode itemsNode = Utils.AddNode(root, "ITEMS");
            foreach (ITEM item in itemCheced)
            {
                XmlNode itemNode = Utils.AddNode(itemsNode, "ITEM");
                item.ToXml(itemNode);
            }
            if (itemCheced.Count==0)
            {
                MessageBox.Show("Выберите файлы загрузки", "Предупреждение", MessageBoxButtons.OK);
                return;
            }

            List<ITEM> logItems;
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand comm = new SqlCommand("USP_LOADING_REG_CERT", con);
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandTimeout = 120;
                //comm.Parameters.Add(new SqlParameter("@ID_DOCUMENT", SqlDbType.UniqueIdentifier)).Value = id_document_global;
                comm.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                con.Open();
                
                SqlDataReader reader = comm.ExecuteReader();
                if (reader == null) return;
                logItems = new List<ITEM>();
                while (reader.Read())
                {
                    ITEM _item = new ITEM();
                    _item.FromReader(reader);
                    logItems.Add(_item);
                }
            }
            if (logItems!=null)
            {
                string fileNameLog = Path.Combine(Utils.TempDir(), "Загрузка рег.сертификатов.txt");                
                using (StreamWriter sw = new StreamWriter(fileNameLog, true, Encoding.Default))
                {
                    foreach (ITEM _item in logItems)
                    {
                        string NextLine = _item.CODE + ";" + _item.NUMREESTR + ";" + _item.SER + ";" + _item.NUM;// +";" + _item.POST;
                        sw.WriteLine(NextLine);                                              
                    }
                    sw.Close();  
                }
            }
            MessageBox.Show("Загрузка завершена", "Сообщение", MessageBoxButtons.OK);
        }

        public string ReportName
        {
            get { return "Загрузка рег.сертификатов"; }
        }

        #endregion

        private void tbDirectory_ValueChanged(object sender, EventArgs e)
        {
            int i = 0;
            
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "dbf files (*.dbf)|*.dbf"; //|All files (*.*)|*.*
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    string connectionString=string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=dBASE IV;User Id=admin;Password=;", Path.GetDirectoryName(fileName));
                    connectionString = DBFUtils.FormatConnectionString(connectionString);
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        using (OleDbCommand comm = new OleDbCommand(string.Format("SELECT * FROM [{0}]",Path.GetFileNameWithoutExtension(fileName)), conn))
                        {
                            //CODE, SER, NUM, POST, NUMREESTR 
                            OleDbDataReader reader = comm.ExecuteReader();
                            while (reader.Read())
                            {
                                ITEM item = new ITEM();
                                item.FILE_NAME = fileName;                                
                                item.FromReader(reader);
                                items.Add(item);
                            }
                        }
                    }
                    chlbFiles.Items.Add(Path.GetFileNameWithoutExtension(fileName));
                    for(int i=0;i<chlbFiles.Items.Count; i++)
                        chlbFiles.SetItemChecked(i,true);
                    //foreach (ObjectCollection  chlbFiles.Items)
                }                
            }                        
        }

    }
}