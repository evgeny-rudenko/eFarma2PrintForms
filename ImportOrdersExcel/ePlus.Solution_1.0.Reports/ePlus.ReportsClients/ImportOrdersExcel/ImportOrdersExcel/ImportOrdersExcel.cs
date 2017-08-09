namespace RCRImportOrdersExcel
{
    using ePlus.CommonEx.AccessPoint;
    using ePlus.CommonEx.Controls;
    using ePlus.ImportZakaz;
    using ePlus.MetaData.Core;
    using ePlus.MetaData.ExternReport;
    using ePlus.PriceList.ImportZakaz;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using ePlus.MetaData.Server;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ImportOrdersExcel : ExternalReportForm, IExternalReportFormMethods
    {
        private ACCESS_POINT ap;
        private AccessPointManager apm;
        private Button btnSetting;
        private CheckBox chbSelectAll;
        private CheckedListBox chlbFiles;
        private List<string> columns;
        private IContainer components = null;
        private CONFIGURATION_IMPORT confImport;
        private CONFIGURATION_IMPORT_BL confImportBl = new CONFIGURATION_IMPORT_BL();
        private List<FILE_INFO> fileInfoList;
        private string fileName = Path.Combine(Utils.TempDir(), "ImportOrdersExcelSettings.xml");
        private Dictionary<XmlDocument, string> fileNamesDir = new Dictionary<XmlDocument, string>();
        private Label label1;
        private Label label2;
        private Dictionary<string, string> mapping = new Dictionary<string, string>();
        private ORDERS_BL orderBl = new ORDERS_BL();
        private Panel panel2;
        private Settings setting;
        private TextBox tbCatalog;
        private MetaPluginDictionarySelectControl ucImport;

        public ImportOrdersExcel()
        {
            this.InitializeComponent();
            this.Mapping.Add("GV", "ID_GOODS_GLOBAL");
            this.Mapping.Add("G", "QUANTITY");
            this.Mapping.Add("GS", "CODE_BUYER");
            this.Mapping.Add("GT", "STORE_MNEMOCODE");
            this.Mapping.Add("GU", "ID_CONTRACTS_GLOBAL");
            this.Mapping.Add("GW", "ID_CONTRACTS_GOODS_GLOBAL");
        }

        private void AddItems(string s)
        {
            FILE_INFO file_info = new FILE_INFO();
            file_info.FILE_NAME = Path.GetFileName(s);
            file_info.FULL_PATH = s;
            this.chlbFiles.Items.Add(file_info, true);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm(this.mapping, this.Setting);
            form.ShowDialog();
            if (form.Setting != null)
            {
                this.Setting = form.Setting;
                this.SetColumnList();
            }
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chlbFiles.Items.Count; i++)
            {
                this.chlbFiles.SetItemChecked(i, this.chbSelectAll.Checked);
            }
        }

        public static string ConnectionString(string fileName)
        {
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended properties=\"Excel 8.0;HDR=Yes;IMEX=0\";", fileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static List<string> GetSheetList(string uri)
        {
            List<string> list = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(ConnectionString(uri)))
            {
                connection.Open();
                DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (!oleDbSchemaTable.Columns.Contains("TABLE_NAME"))
                {
                    throw new Exception(string.Format("Поле TABLE_NAME не найдено в файле {0}", uri));
                }
                foreach (DataRow row in oleDbSchemaTable.Rows)
                {
                    string str = row["TABLE_NAME"] as string;
                    if (!(string.IsNullOrEmpty(str) || !(str.Replace("'", "") == "Таблица$")))
                    {
                        list.Add(str.Replace("'", ""));
                    }
                }
            }
            return list;
        }

        private void ImportOrdersExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Setting != null)
            {
                XmlDocument root = new XmlDocument();
                XmlNode node = Utils.AddNode(root, "XML");
                Utils.AddNode(node, "BEGIN_ROW", (long) this.Setting.BEGIN_ROW);
                Utils.AddNode(node, "BUYER_COL", this.Setting.BUYER_COL);
                Utils.AddNode(node, "CONTRACT_COL", this.Setting.CONTRACT_COL);
                Utils.AddNode(node, "QUANTITY_COL", this.Setting.QUANTITY_COL);
                Utils.AddNode(node, "STORE_COL", this.Setting.STORE_COL);
                root.Save(this.fileName);
            }
        }
        #region удаление старого отчета
        private DataService_BL bl = new DataService_BL();
        private void ClearOldReport()
        {
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(string.Format(@"
                        IF OBJECT_ID('DBO.REMOVE_REPORT_BY_TYPE_NAME') IS NULL EXEC('CREATE PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME AS RETURN')--GO "), connection))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(string.Format(@"
                        ALTER PROCEDURE DBO.REMOVE_REPORT_BY_TYPE_NAME 
	                        @REPORT_TYPE_NAME VARCHAR(200) AS
                        	
                        DECLARE @id_meta_report BIGINT

	                        select 
		                        @id_meta_report = id_meta_report
	                        from meta_report
	                        where type_name = @REPORT_TYPE_NAME
	                        --select @id_meta_report
                        		
	                        DECLARE @SQL NVARCHAR(200)
	                        SET @SQL = N'delete from META_REPORT_2_REPORT_GROUPS
				                        where id_meta_report = @id_meta_report'
	                        IF (OBJECT_ID('META_REPORT_2_REPORT_GROUPS') IS NOT NULL)
		                        EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
                        		

	                        SET @SQL = N'delete from meta_report_settings_csv_export
		                        where id_meta_report = @id_meta_report'
	                        IF (OBJECT_ID('meta_report_settings_csv_export') IS NOT NULL)
		                        EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
                        		

	                        SET @SQL = N'delete from meta_report_settings_visible
		                        where id_meta_report = @id_meta_report'
	                        IF (OBJECT_ID('meta_report_settings_visible') IS NOT NULL)
		                        EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report
                        		

	                        SET @SQL = N'delete from meta_report_settings_managed
				                        where id_meta_report = @id_meta_report'
	                        IF (OBJECT_ID('meta_report_settings_managed') IS NOT NULL)
		                        EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	                        SET @SQL = N'delete from meta_report_settings_archive
				                        where id_meta_report = @id_meta_report'
	                        IF (OBJECT_ID('meta_report_settings_archive') IS NOT NULL)
		                        EXEC SP_EXECUTESQL @SQL, N'@id_meta_report BIGINT', @id_meta_report=@id_meta_report


	                        delete from meta_report
	                        where id_meta_report = @id_meta_report

                        RETURN 0
                        --GO
                        "), connection))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(string.Format(@"
                        EXEC DBO.REMOVE_REPORT_BY_TYPE_NAME '{0}'
                        ", "ImportOrdersExcel.ImportOrdersExcel"), connection))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion удаление старого отчета
        private void ImportOrdersExcel_Load(object sender, EventArgs e)
        {
            ClearOldReport();
            if (File.Exists(this.fileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.fileName);
                if (this.Setting == null)
                {
                    this.Setting = new Settings();
                }
                XmlNode node = document.SelectSingleNode("/XML");
                this.Setting.BEGIN_ROW = Utils.GetInt(node, "BEGIN_ROW");
                this.Setting.BUYER_COL = Utils.GetString(node, "BUYER_COL");
                this.Setting.CONTRACT_COL = Utils.GetString(node, "CONTRACT_COL");
                this.Setting.QUANTITY_COL = Utils.GetString(node, "QUANTITY_COL");
                this.Setting.STORE_COL = Utils.GetString(node, "STORE_COL");
                this.SetColumnList();
            }
        }

        private void InitConfImport(string key)
        {
            string[] args = new string[] { key };
            this.confImport = this.confImportBl.LoadConfiguration(args);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.ucImport = new MetaPluginDictionarySelectControl();
            this.label2 = new Label();
            this.tbCatalog = new TextBox();
            this.panel2 = new Panel();
            this.chbSelectAll = new CheckBox();
            this.btnSetting = new Button();
            this.chlbFiles = new CheckedListBox();
            base.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            base.bOK.Location = new Point(0xec, 3);
            base.bClose.Location = new Point(0x137, 3);
            base.panel1.Controls.Add(this.btnSetting);
            base.panel1.Location = new Point(0, 0x110);
            base.panel1.Size = new Size(0x185, 0x1d);
            base.panel1.Controls.SetChildIndex(this.btnSetting, 0);
            base.panel1.Controls.SetChildIndex(base.bClose, 0);
            base.panel1.Controls.SetChildIndex(base.bOK, 0);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x9c, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Настройки импорта заказов:";
            this.ucImport.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.ucImport.ClearTextOnValidatingIfValueIsEmpty = true;
            this.ucImport.ELikeTextOption = ELikeTextOption.None;
            this.ucImport.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
            this.ucImport.Location = new Point(4, 0x12);
            this.ucImport.Name = "ucImport";
            this.ucImport.PluginMnemocode = "CONFIGURATION_IMPORT";
            this.ucImport.SelectNextControlAfterSelectEntity = false;
            this.ucImport.Size = new Size(0x17e, 20);
            this.ucImport.TabIndex = 4;
            this.ucImport.UseEnterToOpenPlugin = true;
            this.ucImport.UseSpaceToOpenPlugin = true;
            this.ucImport.ValueChanged += new EventHandler(this.ucImport_ValueChanged);
            this.label2.AutoSize =true;
            this.label2.Location = new Point(1, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Каталог:";
            this.tbCatalog.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbCatalog.Enabled = false;
            this.tbCatalog.Location = new Point(0x3a, 0x2f);
            this.tbCatalog.Name = "tbCatalog";
            this.tbCatalog.Size = new Size(0x148, 20);
            this.tbCatalog.TabIndex = 6;
            this.panel2.Controls.Add(this.tbCatalog);
            this.panel2.Controls.Add(this.ucImport);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x185, 0x49);
            this.panel2.TabIndex = 7;
            this.chbSelectAll.AutoSize =true;
            this.chbSelectAll.Location = new Point(4, 0x4d);
            this.chbSelectAll.Name = "chbSelectAll";
            this.chbSelectAll.Size = new Size(0x61, 0x11);
            this.chbSelectAll.TabIndex = 8;
            this.chbSelectAll.Text = "Выделить все";
            this.chbSelectAll.UseVisualStyleBackColor = true;
            this.chbSelectAll.CheckedChanged+=(new EventHandler(this.chbSelectAll_CheckedChanged));
            this.btnSetting.Location = new Point(4, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new Size(0x4b, 0x17);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "Настройка";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new EventHandler(this.btnSetting_Click);
            this.chlbFiles.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.chlbFiles.FormattingEnabled = true;
            this.chlbFiles.Location = new Point(2, 0x63);
            this.chlbFiles.Name = "chlbFiles";
            this.chlbFiles.Size = new Size(0x180, 0xa9);
            this.chlbFiles.TabIndex = 9;
            this.chlbFiles.ThreeDCheckBoxes = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            base.ClientSize = new Size(0x185, 0x12d);
            base.Controls.Add(this.chlbFiles);
            base.Controls.Add(this.chbSelectAll);
            base.Controls.Add(this.panel2);
            this.MinimumSize = new Size(0x18d, 0x14e);
            base.Name = "ImportOrdersExcel";
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Импорт заказов покупателя";
            base.Load += new EventHandler(this.ImportOrdersExcel_Load);
            base.FormClosed+=(new FormClosedEventHandler(this.ImportOrdersExcel_FormClosed));
            base.Controls.SetChildIndex(base.panel1, 0);
            base.Controls.SetChildIndex(this.panel2, 0);
            base.Controls.SetChildIndex(this.chbSelectAll, 0);
            base.Controls.SetChildIndex(this.chlbFiles, 0);
            base.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadFiles()
        {
            List<string> directories = this.apm.GetDirectories();
            List<string> list2 = new List<string>();
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (string str in directories)
            {
                string[] files = Directory.GetFiles(str);
                foreach (string str2 in files)
                {
                    if (Path.GetExtension(str2) == ".xls")
                    {
                        if (!dictionary.ContainsKey(str))
                        {
                            dictionary.Add(str, new List<string>());
                        }
                        dictionary[str].AddRange(files);
                        list2.Add(str2);
                    }
                }
            }
            this.fileInfoList = new List<FILE_INFO>();
            foreach (string str3 in list2)
            {
                List<string> sheetList = GetSheetList(str3);
                if (sheetList.Count == 0)
                {
                    throw new Exception(string.Format("Не найдены листы в файле {0}", str3));
                }
                string str4 = sheetList[0];
                using (OleDbConnection connection = new OleDbConnection(ConnectionString(str3)))
                {
                    List<ITEM> list4 = new List<ITEM>();
                    connection.Open();
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        try
                        {
                            using (OleDbCommand command = new OleDbCommand(string.Format("SELECT * FROM [{0}{1}{2}:{1}300]", str4, this.Columns[i], this.setting.BEGIN_ROW - 1)))
                            {
                                command.Connection = connection;
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                    for (int j = 0; reader.Read(); j++)
                                    {
                                        ITEM item = new ITEM();
                                        item.FILE_NAME = str3;
                                        string str5 = this.Columns[i];
                                        if (str5 != null)
                                        {
                                            if (!(str5 == "GV"))
                                            {
                                                if (str5 == "GU")
                                                {
                                                    goto Label_025E;
                                                }
                                                if (str5 == "G")
                                                {
                                                    goto Label_029C;
                                                }
                                                if (str5 == "GT")
                                                {
                                                    goto Label_02D7;
                                                }
                                                if (str5 == "GS")
                                                {
                                                    goto Label_0312;
                                                }
                                            }
                                            else
                                            {
                                                item.ID_GOODS_GLOBAL = Utils.GetGuid(reader.GetValue(0));
                                                if (i > 0)
                                                {
                                                    list4[j].ID_GOODS_GLOBAL = item.ID_GOODS_GLOBAL;
                                                }
                                            }
                                        }
                                        goto Label_034F;
                                    Label_025E:
                                        item.ID_CONTRACTS_GLOBAL = Utils.GetGuid(reader.GetValue(0));
                                        if (i > 0)
                                        {
                                            list4[j].ID_CONTRACTS_GLOBAL = item.ID_CONTRACTS_GLOBAL;
                                        }
                                        goto Label_034F;
                                    Label_029C:
                                        item.QUANTITY = Utils.GetDecimal(reader.GetValue(0));
                                        if (i > 0)
                                        {
                                            list4[j].QUANTITY = item.QUANTITY;
                                        }
                                        goto Label_034F;
                                    Label_02D7:
                                        item.STORE_MNEMOCODE = Utils.GetString(reader.GetValue(0));
                                        if (i > 0)
                                        {
                                            list4[j].STORE_MNEMOCODE = item.STORE_MNEMOCODE;
                                        }
                                        goto Label_034F;
                                    Label_0312:
                                        item.CODE_BUYER = Utils.GetLong(reader.GetValue(0));
                                        if (i > 0)
                                        {
                                            list4[j].CODE_BUYER = item.CODE_BUYER;
                                        }
                                    Label_034F:
                                        if (i == 0)
                                        {
                                            list4.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        catch (OleDbException exception)
                        {
                            throw new Exception("Загрузка данных невозможна,измените настройки.", exception);
                        }
                    }
                    XmlDocument root = new XmlDocument();
                    XmlNode node = Utils.AddNode(root, "XML");
                    Guid guid = Guid.NewGuid();
                    foreach (ITEM item2 in list4)
                    {
                        XmlNode node2 = Utils.AddNode(node, "XLS");
                        Utils.AddNode(node2, "ID_ORDERS_GLOBAL", guid);
                        item2.ToXml(node2);
                    }
                    if (!this.fileNamesDir.ContainsKey(root))
                    {
                        this.fileNamesDir.Add(root, str3);
                    }
                }
                FILE_INFO file_info = new FILE_INFO();
                file_info.FILE_NAME = Path.GetFileName(str3);
                file_info.FULL_PATH = str3;
                this.fileInfoList.Add(file_info);
                this.chlbFiles.Items.Add(file_info, true);
            }
            this.chbSelectAll.Checked = true;
        }

        public void Print(string[] reportFiles)
        {
            if (this.confImport != null)
            {
                AccessPointManager apmArchiv = this.confImportBl.Load(this.confImport.AP_ARCHIVE);
                Dictionary<XmlDocument, string> fileNamesDir = new Dictionary<XmlDocument, string>();
                for (int i = 0; i < this.chlbFiles.Items.Count; i++)
                {
                    if (this.chlbFiles.GetItemChecked(i))
                    {
                        foreach (KeyValuePair<XmlDocument, string> pair in this.fileNamesDir)
                        {
                            if ((pair.Value == ((FILE_INFO) this.chlbFiles.Items[i]).FULL_PATH) && !fileNamesDir.ContainsKey(pair.Key))
                            {
                                fileNamesDir.Add(pair.Key, pair.Value);
                            }
                        }
                    }
                }
                IMPORT.Import(this.orderBl, fileNamesDir, this.confImport, apmArchiv, this.apm, this.ucImport.Code);
                foreach (KeyValuePair<XmlDocument, string> pair in fileNamesDir)
                {
                    this.fileNamesDir.Remove(pair.Key);
                }
                this.chlbFiles.Items.Clear();
                foreach (KeyValuePair<XmlDocument, string> pair in this.fileNamesDir)
                {
                    this.AddItems(pair.Value);
                }
            }
        }

        private void SetColumnList()
        {
            this.Columns = new List<string>();
            if (!this.Columns.Exists(delegate (string _set) {
                return _set == this.Setting.STORE_COL;
            }))
            {
                this.Columns.Add(this.Setting.STORE_COL);
            }
            if (!this.Columns.Exists(delegate (string _set) {
                return _set == this.Setting.QUANTITY_COL;
            }))
            {
                this.Columns.Add(this.Setting.QUANTITY_COL);
            }
            if (!this.Columns.Exists(delegate (string _set) {
                return _set == this.Setting.CONTRACT_COL;
            }))
            {
                this.Columns.Add(this.Setting.CONTRACT_COL);
            }
            if (!this.Columns.Exists(delegate (string _set) {
                return _set == this.Setting.BUYER_COL;
            }))
            {
                this.Columns.Add(this.Setting.BUYER_COL);
            }
        }

        private void ucImport_ValueChanged(object sender, EventArgs e)
        {
            if (this.ucImport.Text == string.Empty)
            {
                this.tbCatalog.Text = string.Empty;
            }
            else
            {
                this.InitConfImport(this.ucImport.Code);
                this.apm = new AccessPointManager(this.ucImport.Text);
                this.ap = this.apm.AccessPoint;
                this.tbCatalog.Text = this.ap.ToString();
                this.chlbFiles.Items.Clear();
                this.LoadFiles();
            }
        }

        public List<string> Columns
        {
            get
            {
                return this.columns;
            }
            set
            {
                this.columns = value;
            }
        }

        public Dictionary<string, string> Mapping
        {
            get
            {
                return this.mapping;
            }
            set
            {
                this.mapping = value;
            }
        }

        public string ReportName
        {
            get
            {
                return "Импорт заказов формата Excel";
            }
        }

        public Settings Setting
        {
            get
            {
                return this.setting;
            }
            set
            {
                this.setting = value;
            }
        }
    }
}

