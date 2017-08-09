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
using ePlus.MetaData.Server;

namespace UnloadRegCert
{
    public partial class UnloadRegCertForm : ExternalReportForm, IExternalReportFormMethods
    {
        private Guid id_document_global;        //гуид ПН
        private string pathDirectory;           //путь выгрузки
        private List<ITEM> items;               //список строк выбранной ПН
        private string fileName = string.Format("{0:yyyyMMdd}", DateTime.Now);//= string.Empty; //Название файла

        public UnloadRegCertForm()
        {
            InitializeComponent();            
        }
        public override void Execute(string connectionString, string folderPath)
        {
            base.Execute(connectionString, folderPath);
            CreateStoredProc("UnloadRegCert.UNLOAD_REG_CERT.sql");
        }


        #region IExternalReportFormMethods Members

        public void Print(string[] reportFiles)
        {
            pathDirectory = tbDirectory.Text;
            if (items == null || !Directory.Exists(pathDirectory) || items.FindAll(delegate(ITEM _item) { return _item.IS_SELECTED; }).Count < 1) return;

            string filePath = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "dbf");
            DbfTable dbf = createDbfTable(filePath);

            doExport(dbf);
            File.Copy(filePath, Path.Combine(pathDirectory, Path.ChangeExtension(fileName, "DBF")), true);
            File.Delete(filePath);
            MessageBox.Show("Выгрузка завершена", "Сообщение", MessageBoxButtons.OK);
        }


        private void doExport(DbfTable dbf)
        {
            List<ITEM> list = items.FindAll(delegate(ITEM _item) { return _item.IS_SELECTED; });
            DataTable dt = ExportInvoice(id_document_global, list);
            using (OleDbConnection conn = dbf.Open(dbf.FileName))
            {
                foreach (DataRow drow in dt.Rows)
                    dbf.Insert(conn, drow);  
            }
        }

        /// <summary>
        /// Задание структуры .dbf файла
        /// </summary>
        /// <returns></returns>
        private DbfTable createDbfTable(string filePath)
        {
            DbfTable result = new DbfTable(filePath);
            result.Columns.Add(new DbfColumnChar("CODE", 10));
            result.Columns.Add(new DbfColumnChar("NAME", 100));
            result.Columns.Add(new DbfColumnChar("SER",20));
            result.Columns.Add(new DbfColumnChar("PROD", 100));
            result.Columns.Add(new DbfColumnNum("NUM", 10, 0));
            result.Columns.Add(new DbfColumnChar("POST", 100));
            result.Columns.Add(new DbfColumnChar("NSERT", 35));
            result.Columns.Add(new DbfColumnDate("DATASERT"));
            result.Columns.Add(new DbfColumnChar("CENTRSERT", 100));
            result.Create(result.FileName);
            return result;
        }

        public string ReportName
        {
            get { return "Выгрузка для рег. серт. органа"; }
        }

        public override string GroupName
        {
            get { return string.Empty; }
            //new ReportGroupDescription(ReportGroup.GoodsReports).Description
        }
        #endregion

        private void ucInvoiceSelect_ValueChanged(object sender, EventArgs e)
        {
            //id_document_global = ucInvoiceSelect.Guid;
            //if (id_document_global == Guid.Empty)
            //{
            //    tecItems.DataSource = null;
            //    return;
            //}                           
            //fileName = ucInvoiceSelect.Text.Substring(ucInvoiceSelect.Text.Length - 8, 8);

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            foreach (DataRowItem item in ucInvoice.Items)
                Utils.AddNode(root, "ID_ITEM_GLOBAL", item.Guid);


            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand comm = new SqlCommand("USP_GET_ITEMS_BY_ID_DOCUMENT", con);
                comm.CommandType = CommandType.StoredProcedure;
                //comm.Parameters.Add(new SqlParameter("@ID_DOCUMENT", SqlDbType.UniqueIdentifier)).Value = id_document_global;
                comm.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                con.Open();                

                SqlDataReader reader = comm.ExecuteReader();
                if (reader==null) return;                
                items = new List<ITEM>();
                while (reader.Read())
                {
                    ITEM item = new ITEM();
                    item.FromDataReader(reader);
                    items.Add(item);
                }
            }
            tecItems.DataSource = items;
        }

        public DataTable ExportInvoice(Guid idInvoiceOutGlobal, List<ITEM> list)
        {
            XmlDocument doc = new XmlDocument();            
            XmlNode root = Utils.AddNode(doc, "XML");            
            foreach (ITEM item in list)
            {
                XmlNode itemNode = Utils.AddNode(root, "ITEM");
                item.ToXml(itemNode);
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {

                SqlCommandEx comm = new SqlCommandEx("USP_GET_ITEMS_BY_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;                
                comm.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                comm.Parameters.Add(new SqlParameter("@ID_DOCUMENT", SqlDbType.UniqueIdentifier)).Value = idInvoiceOutGlobal;
                SqlDataAdapter da = new SqlDataAdapter(comm.SqlCommand);
                da.Fill(ds);
            }
            return ds.Tables[0];
        }

        //private void ucInvoiceSelect_BeforePluginShow(object sender, CancelEventArgs e)
        //{
        //    ucInvoiceSelect.PluginForm.Grid(0).SetParameterValue("@ADV_FILTER", " DOCUMENT_STATE IN ('SAVE', 'PROC')");
        //}

        private void ucContractor_ValuesListChanged()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            foreach (DataRowItem item in ucInvoice.Items)
                Utils.AddNode(root, "ID_ITEM_GLOBAL", item.Guid);


            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand comm = new SqlCommand("USP_GET_ITEMS_BY_ID_DOCUMENT", con);
                comm.CommandType = CommandType.StoredProcedure;
                //comm.Parameters.Add(new SqlParameter("@ID_DOCUMENT", SqlDbType.UniqueIdentifier)).Value = id_document_global;
                comm.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                con.Open();

                SqlDataReader reader = comm.ExecuteReader();
                if (reader == null) return;
                items = new List<ITEM>();
                while (reader.Read())
                {
                    ITEM item = new ITEM();
                    item.FromDataReader(reader);
                    items.Add(item);
                }
            }
            tecItems.DataSource = items;
        }

        private void ucInvoice_BeforePluginShow(object sender, CancelEventArgs e)
        {
            ucInvoice.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", " DOCUMENT_STATE IN ('SAVE', 'PROC')");
        }
    }

    public class DirectorySelectControl : MetaPluginDictionarySelectControl
    {
        public DirectorySelectControl()
        {
            this.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
            this.ClearTextOnValidatingIfValueIsEmpty = false;
            this.UseSpaceToOpenPlugin = false;
        }

        public override bool UseEnterToOpenPlugin
        {
            get { return string.IsNullOrEmpty(this.Text); }
        }

        public override string PluginMnemocode
        {
            get
            {
                return string.Empty;
            }
        }

        protected override void SelectEntity()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                Value = new DataRowItem(1,Guid.Empty,string.Empty,dialog.SelectedPath);                
            }                        
        }

        protected override void SetFont()
        {
            return;
        }
    }
}