using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.Common;
using ePlus.Dictionary.BusinessObjects;
using ePlus.Dictionary.Client;
using ePlus.Dictionary.Server;
using ePlus.Export.PharmReferenceExport.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;

namespace RCChProtekExport
{
	public partial class ProtekExportParams : ExternalReportForm, IExternalReportFormMethods 
	{
        private StringBuilder sb = new StringBuilder();
        protected string _exportDirectory;
        protected BackgroundWorker _exportBw;
        protected ExporterBase _exporter;
        protected long[] _store;
        protected List<long> _selfStore;
        protected long _drugStoreId;
        protected CONTRACTOR _itemContractor;
        protected long IdContractor = 0;
        protected string ContractorName = string.Empty;

        public ProtekExportParams()
		{
			InitializeComponent();
		}

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

    /// <summary>
    /// Метод для получения подтверждения или опровержения того, что МЫ является центром
    /// </summary>
    /// <returns>true - ЦЕНТР, false - не ЦЕНТР</returns>
    private bool SelfIsCenter()
    {
      bool result = false;
      DataService_BL bl = new DataService_BL();

      using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
      {
        SqlCommand command = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", connection);
        command.CommandType = CommandType.Text;
        connection.Open();
        result = (bool)command.ExecuteScalar();
      }
      return result;
    }
		public void Print(string[] reportFiles)
		{
         
		}

		public string ReportName
		{
            get { return "Выгрузка справки в формат Протек"; }
		}
       
		private void SetDefaultValues()
		{ 
		}
       
		public override string GroupName 
		{
			get { return string.Empty; }
		}
        
    public void LoadSettings()
    {
      SetDefaultValues();
      if (!File.Exists(SettingsFilePath))
      {
        return;
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
      {
        return;
      }
    txtExportDirectory.Text = Utils.GetString(root, "EXP_DIR");
    checkBoxSelfStores.Checked = Utils.GetBool(root, "SELF_STORES");
    }

        private void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root;

            if (File.Exists(SettingsFilePath))
            {
                doc.Load(SettingsFilePath);
                root = doc.SelectSingleNode("//XML");
                root.RemoveAll();
            }
            else
            {
                root = Utils.AddNode(doc, "XML");
            }

            Utils.AddNode(root, "EXP_DIR", txtExportDirectory.Text);
            Utils.AddNode(root, "SELF_STORES", checkBoxSelfStores.Checked ? "1" : "0");
            doc.Save(SettingsFilePath);
        }
        
    private void TaxGroupsParams_Load(object sender, EventArgs e)
    {
        _exportDirectory = string.Empty;
        _exportBw = new BackgroundWorker();
        _exportBw.ProgressChanged +=
            new ProgressChangedEventHandler(_exportBw_ProgressChanged);
        _exportBw.DoWork +=
            new DoWorkEventHandler(_exportBw_DoWork);
        _exportBw.RunWorkerCompleted +=
            new RunWorkerCompletedEventHandler(_exportBw_RunWorkerCompleted);
        _exportBw.WorkerReportsProgress = true;
        _exportBw.WorkerSupportsCancellation = true;

        _exporter = new ProtekExporter(_exportBw);
        ((ProtekExporter)_exporter).StoredProcedure = "USP_EXPORT_SPRAVKA_SST";
      LoadSettings();
    }

    private void TaxGroupsParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
      
        private void bOK_Click_1(object sender, EventArgs e)
        {
            btnExport_Click(sender, e);
        }

        private void btnExportDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtExportDirectory.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK &&
                folderBrowserDialog1.SelectedPath != string.Empty)
            {
                //_exportDirectory = folderBrowserDialog1.SelectedPath;
                //txtExportDirectory.Text = _exportDirectory;
                txtExportDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnContractor_Click(object sender, EventArgs e)
        {
            ContractorSelectWrapper csw = new ContractorSelectWrapper();
            if (csw.Show() == EChooseResult.OK)
            {
                _itemContractor = csw.Contractor;
                LoadSelfStore(_itemContractor.ID_CONTRACTOR);
                IdContractor = csw.Contractor.ID_CONTRACTOR;
                ContractorName = csw.Contractor.NAME;
                SetContractor(csw.Contractor.ID_CONTRACTOR, csw.Contractor.NAME);
            }
        }
  
       private void LoadSelfStore(long id_contractor)
        {
           _selfStore = new List<long>();
            DataService_BL bl = new DataService_BL();
            SqlDataReader result = null;
            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT ID_STORE  FROM STORE WHERE ID_CONTRACTOR = {0}",id_contractor), connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                result = command.ExecuteReader();
                
                while (result.Read())
                {
                   _selfStore.Add((long)result.GetValue(0));
                }
                result.Close();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_exportDirectory))
            {
                MessageBox.Show("Каталога экспорта не существует." +
                                " Задайте существующий каталог.");
                return;
            }
            if (clstStore.CheckedItems.Count != 0)
            {
                _store = new long[clstStore.CheckedItems.Count];
                for (int i = 0; i < clstStore.CheckedItems.Count; i++)
                {
                    _store[i] = ((STORE)clstStore.CheckedItems[i]).ID_STORE;
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одного склада");
                return;
            }

            btnExport.Enabled = false;
            _exportBw.RunWorkerAsync();
        }


        public StringBuilder Messages
        {
            get { return sb; }
        }

        private void _exportBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TraceController.Clear();
            System.Diagnostics.Trace.WriteLine(sb);
            sb.Length = 0;
            if (e.Error != null)
            {
                if (e.Error.Message == "NoRecords")
                {
                    MessageBox.Show("Было экспортировано 0 записей", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(e.Error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                SuccessExport();
            }
            btnExport.Enabled = true;
        }

        protected virtual void SuccessExport()
        {
            //MessageBox.Show("Экспорт данных завершен успешно !", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Экспорт данных завершен успешно !" + Environment.NewLine + "Отправить отчет ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes) return;

            _itemContractor.LoadDetail();

            string dir = _itemContractor.IMPORT_EXPORT.PATH_ORDER == null ? "" : _itemContractor.IMPORT_EXPORT.PATH_ORDER.Trim();
            dir = !dir.EndsWith("\\") ? dir + "\\" : dir;
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Каталог расположения е-Заказа \"" + dir + "\" не существует !");
                return;
            }

            string file = _itemContractor.IMPORT_EXPORT.FILE_ORDER == null ? "" : _itemContractor.IMPORT_EXPORT.FILE_ORDER.Trim();
            file = !file.EndsWith(".exe") ? file + ".exe" : file;
            if (!File.Exists(dir + file))
            {
                 MessageBox.Show("В каталоге \"" + dir + "\" не найден файл запуска е-Заказа \"" + file + "\"");
                return;
            }

            try
            {
                System.Diagnostics.Process prc = System.Diagnostics.Process.Start(dir + file, _itemContractor.IMPORT_EXPORT.KEY_EXPORT_REPORT);
                prc.CloseMainWindow();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void _exportBw_DoWork(object sender, DoWorkEventArgs e)
        {
            _exporter.Export(_exportDirectory, _store, _drugStoreId);
        }

        private void _exportBw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void txtExportDirectory_TextChanged(object sender, EventArgs e)
        {
            _exportDirectory = txtExportDirectory.Text; // folderBrowserDialog1.SelectedPath;
        }
        protected void SetContractor(long idContractor, string szContractor)
        {
            _drugStoreId = idContractor;
            txtContractor.Text = szContractor;
            SetContractor(_drugStoreId);
        }

        private void SetContractor(long idContractor)
        {
            clstStore.Items.Clear();
            STORE_BL storeBl = (STORE_BL)BLProvider.Instance.GetBL(typeof(STORE_BL));
            System.Collections.IList storeList = storeBl.GetList(null/*new ParamSet("ID_CONTRACTOR", idContractor)*/);
            foreach (STORE store in storeList)
            {
                if (store.DATE_DELETED == DateTime.MinValue)
                {
                    if (checkBoxSelfStores.Checked)
                    {
                        if (_selfStore.Contains(store.ID_STORE))
                            clstStore.Items.Add(store, true);
                    /*    else
                            clstStore.Items.Add(store, false);*/
                    }
                    else
                        clstStore.Items.Add(store, true);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void checkBoxSelfStores_CheckedChanged(object sender, EventArgs e)
        {
            if (IdContractor!=0)
                SetContractor(IdContractor, ContractorName);
        }
	}
    class ProtekExporter : ExporterBase
    {
        protected string _dbDirectoryPath = "";

        private string procSource = "";

        public ProtekExporter() : base() { }

        public ProtekExporter(BackgroundWorker worker) : base(worker) { }

        public string StoredProcedure
        {
            get { return procSource; }
            set { procSource = value; }
        }

        public override void Export(string dbDirectoryPath, long[] storeId, long drugStoreId)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "ID_CONTRACTOR", drugStoreId);
            foreach (long id_store in storeId)
            {
                XmlNode row = Utils.AddNode(root, "STORE", null);
                Utils.AddNode(row, "ID_STORE", id_store);
            }
            string fileTarget = Path.Combine(dbDirectoryPath, "spravka.sst");
            string[] prefix = new string[] { "[Header]", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ";;;;", "[Body]" };
            string[] postfix = new string[] { };
            Export2Txt(procSource, doc.InnerXml, fileTarget, prefix, postfix);
        }
    }
}