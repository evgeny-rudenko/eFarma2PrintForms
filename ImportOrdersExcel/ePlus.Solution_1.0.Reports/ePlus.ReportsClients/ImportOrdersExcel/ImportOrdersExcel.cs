using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using ePlus.ImportZakaz;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using Aspose.Cells;
using ePlus.PriceList.ImportZakaz;
using ePlus.CommonEx.DataAccess;

namespace ImportOrdersExcel
{
    public partial class ImportOrdersExcel : ExternalReportForm, IExternalReportFormMethods
    {
        //Import_BL bl = new Import_BL();
        AccessPointManager apm;
        ACCESS_POINT ap;
        private CONFIGURATION_IMPORT confImport;
        CONFIGURATION_IMPORT_BL confImportBl = new CONFIGURATION_IMPORT_BL();
        ORDERS_BL orderBl = new ORDERS_BL();

        private Settings setting;       //���������,��������� �������������
        private Dictionary<string, string> mapping = new Dictionary<string, string>(); //������� ������������
        private Dictionary<XmlDocument, string> fileNamesDir = new Dictionary<XmlDocument, string>();
        private List<string> columns; //�������,�� ������� ����� ��������� ������
        private List<FILE_INFO> fileInfoList;
        public List<string> Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        public Settings Setting
        {
            get { return setting; }
            set { setting = value; }
        }
        public Dictionary<string, string> Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }

        public ImportOrdersExcel()
        {
            InitializeComponent();
            Mapping.Add("GX", "ID_GOODS_GLOBAL");
            //Mapping.Add("B", "���-�� ����");
            //Mapping.Add("C", "���-�� �������");
            //Mapping.Add("D", "������� ������");
            Mapping.Add("E", "QUANTITY");   //���-�� ����������
            Mapping.Add("GS", "CODE_BUYER");    //��� ����������
            Mapping.Add("GT", "STORE_MNEMOCODE");   //�����
            Mapping.Add("GU", "ID_CONTRACTS_GLOBAL");   //�������
            Mapping.Add("GW", "ID_CONTRACTS_GOODS_GLOBAL");
        }

        public void Print(string[] reportFiles)
        {
            if (confImport==null) return;
            AccessPointManager apmArchive = confImportBl.Load(confImport.AP_ARCHIVE);
            Dictionary<XmlDocument, string> _fileNamesDir = new Dictionary<XmlDocument, string>();
            
            for (int i = 0; i < chlbFiles.Items.Count;i++)
                if (chlbFiles.GetItemChecked(i))
                {
                    foreach(KeyValuePair<XmlDocument, string> fd in fileNamesDir)
                        if (fd.Value==((FILE_INFO)chlbFiles.Items[i]).FULL_PATH)
                        {
                            if (!_fileNamesDir.ContainsKey(fd.Key))                       
                                _fileNamesDir.Add(fd.Key, fd.Value);                                                                               
                        }
                }

            IMPORT.Import(orderBl, _fileNamesDir, confImport, apmArchive, apm, ucImport.Code);
            foreach (KeyValuePair<XmlDocument, string> fd in _fileNamesDir)
                fileNamesDir.Remove(fd.Key);
            chlbFiles.Items.Clear();
            foreach (KeyValuePair<XmlDocument, string> fd in fileNamesDir)            
                AddItems(fd.Value);            
            //MessageBox.Show("������ ��������", "��������������", MessageBoxButtons.OK);
        }

        private void AddItems(string s)
        {
            FILE_INFO file_info = new FILE_INFO();
            file_info.FILE_NAME = Path.GetFileName(s);
            file_info.FULL_PATH = s;
            chlbFiles.Items.Add(file_info, true);            
        }

        public string ReportName
        {
            get { return "������ ������� ������� Excel"; }
        }

        private void InitConfImport(string key)
        {
            string[] s = new string[]{key};
            confImport = confImportBl.LoadConfiguration(s);
        }
        
        private void ucImport_ValueChanged(object sender, EventArgs e)
        {
            //bl.LoadConfigurationImport(ucImport.Id);            
            if (ucImport.Text == string.Empty)
            {
                tbCatalog.Text = string.Empty;
                return;
            }
            InitConfImport(ucImport.Code);
            //MessageBox.Show("��������� ������������");
            apm = new AccessPointManager(ucImport.Text);//confImport.AP_IMPORT            
            ap = apm.AccessPoint;
            tbCatalog.Text = ap.ToString();
            chlbFiles.Items.Clear();
            //MessageBox.Show("��������� ����� �������");
            LoadFiles();
        }

        private void LoadFiles()
        {
            List<string> directories = apm.GetDirectories();    //� ����������,��������� � ��������� ��������� ������� �������
            //MessageBox.Show("������� ������ ����������");

            List<string> files=new List<string>();
            Dictionary<string,List<string>> filesDir = new Dictionary<string, List<string>>(); //�����-�����
            string[] _files;
            //���� ��� ������������� � �������� �� ��� �����
            foreach (string dir in directories)
            {
                _files = Directory.GetFiles(dir);
                foreach (string _file in _files) //���� ����� ������ ���������� ������ �����������
                {
                    if (Path.GetExtension(_file) == ".xls")
                    {
                        if (!filesDir.ContainsKey(dir)) //���� � �������(�����) �� ����������
                            filesDir.Add(dir, new List<string>()); //�������
                        filesDir[dir].AddRange(_files); //��������� � �������-�� ������� ������
                        files.Add(_file);
                    }                    
                }
            }
            //MessageBox.Show("������� ������ ������");
            //�������� ������ ����� Excel - � ����������� .xls
            fileInfoList = new List<FILE_INFO>();
            foreach (string file in files)
            {
                List<string> tables = GetSheetList(file);
                if (tables.Count == 0)
                    throw new Exception(string.Format("�� ������� ����� � ����� {0}", file));
                string sheetName = tables[0];//.Replace("'", "");

                using (OleDbConnection conn = new OleDbConnection(ConnectionString(file)))
                {
                    List<ITEM> items = new List<ITEM>();
                    conn.Open();
                    //��������� ���������� � ����� �� ��������� ������������� ��������
                    for (int iter = 0; iter < Columns.Count; iter ++)
                    {
                        
                        try
                        {
                            using (OleDbCommand comm = new OleDbCommand(string.Format("SELECT * FROM [{0}{1}{2}:{1}300]", sheetName, Columns[iter], setting.BEGIN_ROW))) //[{0}A1:A300]
                            {
                                //MessageBox.Show("������ ��������");
                                comm.Connection = conn;
                                

                                using (OleDbDataReader reader = comm.ExecuteReader())
                                {
                                    int k = 0;//������� ��� ������������ �� ����� �������
                                    while (reader.Read())
                                    {
                                        ITEM item = new ITEM();
                                        item.FILE_NAME = file;
                                        switch (Columns[iter])
                                        {
                                            case "GX":
                                                item.ID_GOODS_GLOBAL = Utils.GetGuid(reader.GetValue(0));
                                                if (iter > 0)
                                                    items[k].ID_GOODS_GLOBAL = item.ID_GOODS_GLOBAL;
                                                break;
                                            case "GU":
                                                item.ID_CONTRACTS_GLOBAL = Utils.GetGuid(reader.GetValue(0));
                                                if (iter > 0)
                                                    items[k].ID_CONTRACTS_GLOBAL = item.ID_CONTRACTS_GLOBAL;
                                                break;
                                            case "E":
                                                item.QUANTITY = Utils.GetDecimal(reader.GetValue(0));
                                                if (iter > 0)
                                                    items[k].QUANTITY = item.QUANTITY;
                                                break;
                                            case "GT":
                                                item.STORE_MNEMOCODE = Utils.GetString(reader.GetValue(0));
                                                if (iter > 0)
                                                    items[k].STORE_MNEMOCODE = item.STORE_MNEMOCODE;
                                                break;
                                            case "GS":
                                                item.CODE_BUYER = Utils.GetLong(reader.GetValue(0));
                                                if (iter > 0)
                                                    items[k].CODE_BUYER = item.CODE_BUYER;
                                                break;
                                            default:
                                                break;
                                        }
                                        if (iter == 0)
                                            items.Add(item);
                                        k++;
                                    }
                                    //MessageBox.Show("������ �������");
                                }
                                //conn.Close();
                            }                                 
                        }
                        catch(OleDbException ex)
                        {
                            throw new Exception("�������� ������ ����������,�������� ���������.", ex);
                        }
                   
                    }
                    XmlDocument doc = new XmlDocument();
                    XmlNode root = Utils.AddNode(doc, "XML");
                    foreach (ITEM _item in items)
                    {
                        XmlNode xls = Utils.AddNode(root, "XLS");
                        Utils.AddNode(xls, "ID_ORDERS_GLOBAL", Guid.NewGuid());
                        _item.ToXml(xls);
                    }
                    
                    if (!fileNamesDir.ContainsKey(doc))
                        fileNamesDir.Add(doc, file);   
                    
                }

                FILE_INFO fileInfo = new FILE_INFO();
                fileInfo.FILE_NAME = Path.GetFileName(file);
                fileInfo.FULL_PATH = file;
                fileInfoList.Add(fileInfo);
                chlbFiles.Items.Add(fileInfo, true);
                //MessageBox.Show("������ �������� � ���� �����������");
            }
            chbSelectAll.Checked = true;
        }

        public static string ConnectionString(string fileName)
        {
            const string connectionStringTemplate = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended properties=\"Excel 8.0;HDR=Yes;IMEX=0\";"; //MAXSCANROWS=10;
            return DBFUtils.FormatConnectionString(string.Format(connectionStringTemplate, fileName));
        }

        public static List<string> GetSheetList(string uri)
        {
            List<string> tables = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(ConnectionString(uri)))
            {
                conn.Open();
                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (!dt.Columns.Contains("TABLE_NAME"))
                    throw new Exception(string.Format("���� TABLE_NAME �� ������� � ����� {0}", uri));
                foreach (DataRow dr in dt.Rows)
                {
                    string sheet = dr["TABLE_NAME"] as string;
                    if (!string.IsNullOrEmpty(sheet) && sheet.Replace("'", "") == "�������$")
                        tables.Add(sheet.Replace("'", ""));
                }
            }
            return tables;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm setForm = new SettingForm(mapping, Setting);
            setForm.ShowDialog();
            if (setForm.Setting == null) return;
            Setting = setForm.Setting;
            SetColumnList();
        }

        private void SetColumnList()
        {
            Columns = new List<string>();
            if (!Columns.Exists(delegate(string _set) { return _set == Setting.STORE_COL; }))
                Columns.Add(Setting.STORE_COL);
            if (!Columns.Exists(delegate(string _set) { return _set == Setting.QUANTITY_COL; }))
                Columns.Add(Setting.QUANTITY_COL);
            if (!Columns.Exists(delegate(string _set) { return _set == Setting.CONTRACT_COL; }))
                Columns.Add(Setting.CONTRACT_COL);
            if (!Columns.Exists(delegate(string _set) { return _set == Setting.BUYER_COL; }))
                Columns.Add(Setting.BUYER_COL);
        }

        private void ImportOrdersExcel_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileName)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            if (Setting==null)
                Setting = new Settings();
            XmlNode root = doc.SelectSingleNode("/XML");
            Setting.BEGIN_ROW = Utils.GetInt(root, "BEGIN_ROW");
            Setting.BUYER_COL = Utils.GetString(root, "BUYER_COL");
            Setting.CONTRACT_COL = Utils.GetString(root, "CONTRACT_COL");
            Setting.QUANTITY_COL = Utils.GetString(root, "QUANTITY_COL");
            Setting.STORE_COL = Utils.GetString(root, "STORE_COL");
            SetColumnList();
        }

        private string fileName = Path.Combine(Utils.TempDir(), "ImportOrdersExcelSettings.xml");
        private void ImportOrdersExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Setting == null) return;
            XmlDocument docSave = new XmlDocument();
            XmlNode root = Utils.AddNode(docSave, "XML");            
            Utils.AddNode(root,"BEGIN_ROW", Setting.BEGIN_ROW);
            Utils.AddNode(root, "BUYER_COL", Setting.BUYER_COL);
            Utils.AddNode(root, "CONTRACT_COL", Setting.CONTRACT_COL);
            Utils.AddNode(root, "QUANTITY_COL", Setting.QUANTITY_COL);
            Utils.AddNode(root, "STORE_COL", Setting.STORE_COL);   
            docSave.Save(fileName);
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chlbFiles.Items.Count; i++)
                chlbFiles.SetItemChecked(i, chbSelectAll.Checked);
        }
    }
}
