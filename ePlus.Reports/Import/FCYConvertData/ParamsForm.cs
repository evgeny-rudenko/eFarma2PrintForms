using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using RCYConvertData.DiscountCards;

namespace RCYConvertData
{
    public partial class ParamsForm : ExternalReportForm, IExternalReportFormMethods
    {
        public string ReportName
        {
            get { return "Конвертация данных"; }
        }

        #region Loading and saving
        private string settingsFilePath;
        public ParamsForm()
        {
            InitializeComponent();

        }

        private void FillFormats()
        {
            cbFormats.Items.Clear();
            Array a = Enum.GetValues(typeof(ImportFormat));
            foreach (ImportFormat f in a)
                cbFormats.Items.Add(new ImportFormatDescription(f));
            if (cbFormats.Items.Count > 0)
                cbFormats.SelectedIndex = 0;
        }

        private ImportFormatDescription SelectedFormat
        {
            get
            {
                return GetSelectedFormat();
            }
        }

        private ImportFormatDescription getSelectedFormat()
        {
            return cbFormats.SelectedItem as ImportFormatDescription;
        }

        private delegate ImportFormatDescription GetSelectedFormatDelegate();
        private ImportFormatDescription GetSelectedFormat()
        {
            if (this.InvokeRequired)
                return this.Invoke(new GetSelectedFormatDelegate(this.getSelectedFormat)) as ImportFormatDescription;
            return getSelectedFormat();
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////
            this.ucStore = new ePlus.CommonEx.Controls.StoreContractor();
            this.ucStore.ClosePlugin = false;
            this.ucStore.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucStore.Location = new System.Drawing.Point(3, 16);
            this.ucStore.Name = "ucStore";
            this.ucStore.Size = new System.Drawing.Size(451, 51);
            this.ucStore.TabIndex = 3;
            this.ucStore.ValueChanged += new System.EventHandler(this.ucStore_ValueChanged);
            this.groupBox1.Controls.Add(this.ucStore);
            /////////////////////////////////////////////////////////
            settingsFilePath = Path.Combine(Utils.TempDir(), this.Name + "Settings.xml");
            FillFormats();
            if (Utils.IsDesignMode(this)) return;
            ucStore.LoadByStoreId(this.IdStoreDefault);
            if (File.Exists(settingsFilePath))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(settingsFilePath);
                }
                catch
                {
                    doc = null;
                }
                if (doc == null) return;
                XmlNode root = doc.SelectSingleNode("//XML");
                if (root == null) return;
                tbImportFile.Text = Utils.GetString(root, "DATA_FILE_PATH");
                tbImportFile2.Text = Utils.GetString(root, "DATA_FILE2_PATH");
                textBoxDiscountCard.Text = Utils.GetString(root, "DATA_FILE_DCARD_PATH");
                textBoxDiscountCardType.Text = Utils.GetString(root, "DATA_FILE_CROS_TDC_PATH");
                tbImportLogFolder.Text = Utils.GetString(root, "LOG_FOLDER");
                chkUseVATFromEfarma.Checked = Utils.GetBool(root, "USE_VAT");
                long idStore = Utils.GetLong(root, "ID_STORE");
                bool envd = Utils.GetBool(root, "NOT_ENVD");
                if (idStore > 0)
                    ucStore.LoadByStoreId(idStore);
                chkENVD.Checked = !envd;

                string format = Utils.GetString(root, "FORMAT");
                SetFormat(format);
            }
        }

        private void SetFormat(string format)
        {
            if (cbFormats.Items.Count == 0) return;
            if (string.IsNullOrEmpty(format) ||
               !Enum.IsDefined(typeof(ImportFormat), format))
            {
                cbFormats.SelectedIndex = 0;
                return;
            }
            ImportFormat f = (ImportFormat)Enum.Parse(typeof(ImportFormat), format);
            cbFormats.SelectedItem = new ImportFormatDescription(f);
        }

        private void ParamsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_STORE", ucStore.Store.Id);
            Utils.AddNode(root, "NOT_ENVD", chkENVD.Checked ? 0 : 1);
            Utils.AddNode(root, "DATA_FILE_PATH", tbImportFile.Text);
            Utils.AddNode(root, "DATA_FILE2_PATH", tbImportFile2.Text);
            Utils.AddNode(root, "DATA_FILE_DCARD_PATH", textBoxDiscountCard.Text);
            Utils.AddNode(root, "DATA_FILE_CROS_TDC_PATH", textBoxDiscountCardType.Text);
            Utils.AddNode(root, "LOG_FOLDER", tbImportLogFolder.Text);
            Utils.AddNode(root, "USE_VAT", chkUseVATFromEfarma.Checked);
            ImportFormatDescription d = cbFormats.SelectedItem as ImportFormatDescription;
            if (d != null)
                Utils.AddNode(root, "FORMAT", d.ImportFormat.ToString());
            doc.Save(settingsFilePath);

        }
        #endregion

        public void Print(string[] reportFiles)
        {
            LoadAction currentAction;
            using (ActionDialogForm form = new ActionDialogForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                currentAction = form.CurrentAction;
            }
            if (tabControl1.SelectedTab == tabPageRest)
            {
                switch (currentAction)
                {
                    case LoadAction.OnlyCheck:
                        CheckAndImport(null);
                        break;
                    case LoadAction.CheckAndLoad:
                        CheckAndImport(ImportData);
                        break;
                }
            }
            else if (tabControl1.SelectedTab == tabPageCards)
            {
                switch (currentAction)
                {
                    case LoadAction.OnlyCheck:
                        CheckAndImportDiscountCards(null);
                        break;
                    case LoadAction.CheckAndLoad:
                        CheckAndImportDiscountCards(ImportDiscountCardsData);
                        break;
                }
            }
        }


        #region Common routine
        private DataSet GetFileData(string filePath)
        {
            string tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), "dbf");
            File.Copy(filePath, tempFilePath, true);
            string tableName = Path.GetFileNameWithoutExtension(tempFilePath);
            string oleDbConnectionString =
              string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=dBASE IV;", Path.GetDirectoryName(tempFilePath));

            using (OleDbDataAdapter oledbda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]",
                                                                                 tableName),
                                                                   oleDbConnectionString))
            {
                DataSet ds = new DataSet();
                oledbda.Fill(ds);
                return ds;
            }
        }

        private List<T> LoadData<T>(string file) where T : new()
        {
            DataSet ds = GetFileData(file);
            if (ds.Tables.Count == 0) return new List<T>();
            List<T> list = GetList<T>(ds.Tables[0]);
            return list;
        }

        private List<T> GetList<T>(DataTable table) where T : new()
        {
            List<T> result = new List<T>();
            foreach (DataRow dr in table.Rows)
            {
                T t = new T();
                foreach (DataColumn col in table.Columns)
                {
                    object obj = dr.IsNull(col) ? null : dr[col];
                    PropertyInfo pi = t.GetType().GetProperty(col.ColumnName);
                    if (pi == null)
                    {
                        if (t is ISupportsErrorState)
                            ((ISupportsErrorState)t).Errors.Add(RowError.Warning(string.Format("В файле найдено поле [{0}], которому не соответствует ни одно свойство", col.ColumnName)));
                        continue;
                    }
                    if (!pi.CanWrite) continue;

                    object converted;
                    RowError err = ConvertObject(pi.PropertyType, obj, col.ColumnName, out converted);
                    pi.SetValue(t, converted, null);
                    if (err.RowErrorLevel == RowErrorLevel.None)
                        continue;
                    if (t is ISupportsErrorState)
                        ((ISupportsErrorState)t).Errors.Add(err);
                    else if (err.RowErrorLevel == RowErrorLevel.Critical)
                        throw new Exception(err.ErrorText);
                }
                result.Add(t);
            }
            return result;
        }

        private RowError ConvertObject(Type type, object value, string columnName, out object obj)
        {
            if (value == null)
            {
                obj = null;
                return RowError.None();
            }
            Type valueType = value.GetType();
            if (valueType == type)
            {
                obj = value;
                return RowError.None();
            }
            try
            {
                if (value is IConvertible)
                {
                    if (type == typeof(double))
                    {
                        obj = Convert.ToDouble(value);
                        return RowError.None();
                    }
                    if (type == typeof(decimal))
                    {
                        obj = Convert.ToDecimal(value);
                        return RowError.None();
                    }
                }
                throw new InvalidCastException(string.Format("Поле [{0}]: нельзя преобразовать тип данных [{1}] к типу данных [{2}]", columnName, valueType.ToString(), type.ToString()));
            }
            catch (InvalidCastException ex)
            {
                obj = null;
                return RowError.Critical(ex.Message);
            }
        }

        private string GetHeaderString<T>()
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();
            sb.Append("Тип ошибки;");
            sb.Append("Описание ошибки;");
            foreach (PropertyInfo pi in properties)
            {
                FormatAttribute attr = (FormatAttribute)Attribute.GetCustomAttribute(pi, typeof(FormatAttribute));
                if (attr != null)
                    sb.AppendFormat("{0};", pi.Name);
            }
            return sb.ToString();

        }

        private string GetFieldsString<T>(T item)
        where T : IObject
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                FormatAttribute attr = (FormatAttribute)Attribute.GetCustomAttribute(pi, typeof(FormatAttribute));
                if (attr != null)
                    sb.AppendFormat("{0};", attr.Format(pi.GetValue(item, null)));
            }
            return sb.ToString();
        }

        #endregion

        #region Rest
        private XmlDocument GetXmlDocument(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            if (ucStore.Store.Id > 0)
                Utils.AddNode(root, "ID_STORE", ucStore.Store.Id);
            Utils.AddNode(root, "ENVD", chkENVD.Checked ? 1 : 0);
            Utils.AddNode(root, "USE_VAT_FROM_EFARMA", chkUseVATFromEfarma.Checked ? 1 : 0);
            Utils.AddNode(root, "IMPORT_FORMAT", SelectedFormat.ImportFormat.ToString().ToUpper());
            foreach (IMPORT_REMAINS_ITEM item in list)
            {
                XmlNode node = Utils.AddNode(root, "ITEM");
                item.ToXml(node);
            }
            foreach (CROSS_SUP_ITEM item in list2)
            {
                XmlNode node = Utils.AddNode(root, "CROSS_SUP");
                item.ToXml(node);
            }
            return doc;
        }

        Dictionary<Guid, List<string>> errorLog = new Dictionary<Guid, List<string>>();

        private void AddError(Guid global, string text)
        {
            if (!errorLog.ContainsKey(global))
                errorLog.Add(global, new List<string>());
            errorLog[global].Add(text);
        }

        private void Save(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2)
        {
            XmlDocument doc = GetXmlDocument(list, list2);
            //doc.Save("c:\\OSTATKI.xml");
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        private bool ValidateParams()
        {
            if (string.IsNullOrEmpty(tbImportFile.Text) || !File.Exists(tbImportFile.Text))
            {
                ShowError("Не задан путь к файлу остатков А2000 или файл не существует");
                return false;
            }
            if (string.IsNullOrEmpty(tbImportFile2.Text) || !File.Exists(tbImportFile2.Text))
            {
                ShowError("Не задан путь к файлу соответствий кодов поставщиков или файл не существует");
                return false;
            }

            if (string.IsNullOrEmpty(tbImportLogFolder.Text) || !Directory.Exists(tbImportLogFolder.Text))
            {
                ShowError("Не задан путь к каталогу для сохранения результатов импорта или каталог не существует");
                return false;
            }
            if (ucStore.Store.Id == 0)
            {
                ShowError("Не заданы аптека и склад, на который переносятся остатки");
                return false;
            }

            if (SelectedFormat == null)
            {
                ShowError("Не задан формат импорта");
                return false;
            }
            return true;
        }

        private void ShowError(string s)
        {
            Logger.ShowMessage(s, 0, MessageBoxIcon.Error);
        }

        private void ShowResult(bool import, string logFilePath, bool isValid, bool showLog)
        {
            string message = isValid ? string.Format("успешно{0}", showLog ? string.Empty : " и в полном объеме") : "с ошибками";
            MessageBoxIcon icon = isValid ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            Logger.ShowMessage(string.Format("{0}{1}", import ? "Импорт данных завершился " : "Проверка данных завершилась ", message), 0, icon);

            if (showLog && !string.IsNullOrEmpty(logFilePath) && File.Exists(logFilePath))
                Process.Start(logFilePath);
            if (import && isValid)
            {
                PluginFormView pfv = AppManager.GetPluginView("IMPORT_REMAINS");
                AppManager.RegisterForm(pfv);
                pfv.Show();
            }
        }

        private delegate void ImportProc(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2);

        private void SelectFolder(Control sender, string description)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                sender.Text = fbd.SelectedPath;
            }
        }

        private void CheckAndImport(ImportProc importProc)
        {
            string logFilePath = string.Empty;
            Exception ex = null;
            bool isValid = true;
            bool showLog = false;
            try
            {
                this.Enabled = false;
                if (!ValidateParams()) return;
                using (ProgressStatusForm form = new ProgressStatusForm())
                {
                    form.DoWork += delegate(object sender, DoWorkEventArgs e)
                                     {
                                         BackgroundWorker worker = (BackgroundWorker)sender;
                                         worker.ReportProgress(0, "Загрузка остатков");
                                         List<IMPORT_REMAINS_ITEM> list = LoadData<IMPORT_REMAINS_ITEM>(tbImportFile.Text);
                                         worker.ReportProgress(0, "Загрузка кодов поставщиков");
                                         List<CROSS_SUP_ITEM> list2 = LoadData<CROSS_SUP_ITEM>(tbImportFile2.Text);
                                         worker.ReportProgress(0, "Проверка данных");
                                         isValid = CheckData(list, list2, worker);
                                         showLog = WriteErrorLog(list, list2, out logFilePath);

                                         worker.ReportProgress(0, "Загрузка данных");
                                         if (importProc != null)
                                             importProc.Invoke(list, list2);
                                     };
                    form.ShowDialog();
                    ex = form.Error;
                }
            }
            finally
            {
                if (ex != null)
                {
                    ShowError("Импорт данных был прерван из-за ошибки");
                }
                else
                {
                    ShowResult(importProc != null, logFilePath, isValid, showLog);
                }
                this.Enabled = true;
            }
        }

        private void ImportData(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2)
        {
            list.RemoveAll(delegate(IMPORT_REMAINS_ITEM item) { return errorLog.ContainsKey(item.ID_GLOBAL); });
            list.RemoveAll(delegate(IMPORT_REMAINS_ITEM item)
                            {
                                CROSS_SUP_ITEM sup = list2.Find(delegate(CROSS_SUP_ITEM s)
                                                                  {
                                                                      return s.SUPPLIERID == item.SUPPLIERID;
                                                                  });
                                // не найден поставщик
                                return sup != null && errorLog.ContainsKey(sup.ID_GLOBAL);
                            });
            list.RemoveAll(delegate(IMPORT_REMAINS_ITEM item)
                             {
                                 return item.Errors.Count > 0 && item.Errors.Exists(delegate(RowError err)
                                                                                    {
                                                                                        return err.RowErrorLevel == RowErrorLevel.Critical;
                                                                                    });
                             });
            if (list.Count > 0)
                Save(list, list2);
        }

        public override void Execute(string connectionString, string folderPath)
        {
            base.Execute(connectionString, folderPath);
        }

        private bool WriteErrorLog(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2, out string logFilePath)
        {
            string logFileName = string.Format("OSTATKI_LOG_{0:ddMMyyyy}.txt", DateTime.Now);
            logFilePath = Path.Combine(tbImportLogFolder.Text, logFileName);

            bool header1 = false;
            bool header2 = false;

            bool hasErrors1 = list.Exists(delegate(IMPORT_REMAINS_ITEM i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });
            bool hasErrors2 = list2.Exists(delegate(CROSS_SUP_ITEM i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });

            if (!hasErrors1 && !hasErrors2) return false;

            using (StreamWriter sw = new StreamWriter(logFilePath, false, Encoding.GetEncoding(1251)))
            {
                if (hasErrors1)
                {
                    foreach (IMPORT_REMAINS_ITEM item in list)
                    {
                        if (!header1)
                        {
                            sw.WriteLine("{0}", tbImportFile.Text);
                            sw.WriteLine(GetHeaderString<IMPORT_REMAINS_ITEM>());
                            header1 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("ОШИБКА;{0};{1}", s, GetFieldsString<IMPORT_REMAINS_ITEM>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("{0};{1};{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<IMPORT_REMAINS_ITEM>(item));
                    }
                }
                if (hasErrors2)
                {
                    foreach (CROSS_SUP_ITEM item in list2)
                    {
                        if (!header2)
                        {
                            sw.WriteLine("{0}", tbImportFile2.Text);
                            sw.WriteLine(GetHeaderString<CROSS_SUP_ITEM>());
                            header2 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("ОШИБКА;{0};{1}", s, GetFieldsString<CROSS_SUP_ITEM>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("{0};{1};{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<CROSS_SUP_ITEM>(item));
                    }
                }
            }
            return true;
        }

        private bool CheckData(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2, BackgroundWorker worker)
        {
            errorLog.Clear();
            worker.ReportProgress(1, "проверка формата");
            bool validFormat = CheckFormat(list);
            worker.ReportProgress(1, "проверка обязательных полей");
            // проверка обязательных полей 
            bool remainsValid = CheckRequiredFields<IMPORT_REMAINS_ITEM>(list);
            bool crossSupValid = CheckRequiredFields<CROSS_SUP_ITEM>(list2);
            // проверка согласованности данных
            worker.ReportProgress(1, "проверка согласованности данных");
            bool suppliersValid = CheckSuppliersLocal(list, list2);
            // проверка возможности импорта в БД
            worker.ReportProgress(1, "проверка возможности импорта в БД");
            bool dataValid = CheckDataDb(list, list2);
            bool convertValid = true;
            if (SelectedFormat.ImportFormat == ImportFormat.QWERTY)
            {
                worker.ReportProgress(1, "подбор коэффициентов пересчета");
                convertValid = ConvertRemains(list);
            }
            return validFormat && remainsValid && crossSupValid && suppliersValid && dataValid && convertValid;
        }

        private bool CheckFormat(List<IMPORT_REMAINS_ITEM> list)
        {
            bool isValid = true;
            foreach (IMPORT_REMAINS_ITEM item in list)
            {
                if ((item.KOL - Math.Floor(item.KOL)) > 0 &&
                    SelectedFormat.ImportFormat == ImportFormat.A2000)
                {
                    AddError(item.ID_GLOBAL, "Файл остатков из А2000 не может содержать дробное количество");
                    isValid = false;
                }

                if (item.Q_FIRST != 1 && SelectedFormat.ImportFormat == ImportFormat.QWERTY)
                {
                    AddError(item.ID_GLOBAL, "Файл остатков из Qwerty не может содержать Q_FIRST != 1");
                    isValid = false;
                }

            }
            return isValid;
        }

        private bool CheckSuppliersLocal(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2)
        {
            bool isValid = true;
            foreach (IMPORT_REMAINS_ITEM item in list)
            {
                List<CROSS_SUP_ITEM> sups = list2.FindAll(delegate(CROSS_SUP_ITEM s)
                                                     {
                                                         return s.SUPPLIERID == item.SUPPLIERID && s.SUPIDAP > 0;
                                                     });
                if (sups.Count > 1)
                {
                    AddError(item.ID_GLOBAL, "Поставщику соотвествует несколько записей в кросс-файле");
                    isValid = false;
                }

                if (sups.Count == 0)
                {
                    AddError(item.ID_GLOBAL, "Поставщик отсутствует в кросс-файле");
                    isValid = false;
                }
            };
            return isValid;
        }

        private bool CheckRequiredFields<T>(List<T> list)
          where T : IObject
        {
            bool isValid = true;
            foreach (T item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    RequiredAttribute attr = (RequiredAttribute)Attribute.GetCustomAttribute(pi, typeof(RequiredAttribute));
                    if (attr != null && attr.Required)
                    {
                        object value = pi.GetValue(item, null);
                        if (RequiredAttribute.IsEmpty(value))
                        {
                            AddError(item.ID_GLOBAL, string.Format("Не заполнено обязательное поле {0}", pi.Name));
                            isValid = false;
                        }

                    }
                }
            }
            return isValid;
        }

        private bool ConvertRemains(List<IMPORT_REMAINS_ITEM> list)
        {
            bool isValid = true;
            List<IMPORT_REMAINS_ITEM> filtered = list.FindAll(delegate(IMPORT_REMAINS_ITEM item)
                                                                {
                                                                    return item.Q_FIRST == 1 && (item.KOL - Math.Floor(item.KOL)) > 0;
                                                                });
            XmlDocument doc = GetXmlDocument(filtered, new List<CROSS_SUP_ITEM>());
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_RCY_CONVERT_RIGLA_DATA_CONVERT", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                    SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
                    sqlda.Fill(ds);
                }
            }
            if (ds.Tables.Count > 0)
            {
                List<IMPORT_REMAINS_ITEM> converted = GetList<IMPORT_REMAINS_ITEM>(ds.Tables[0]);
                foreach (IMPORT_REMAINS_ITEM item in filtered)
                {
                    IMPORT_REMAINS_ITEM convertedItem = converted.Find(delegate(IMPORT_REMAINS_ITEM i)
                                                               {
                                                                   return i.ID_GLOBAL == item.ID_GLOBAL;
                                                               });
                    list.Remove(item);

                    IMPORT_REMAINS_ITEM copy = item.Copy();
                    list.Add(copy);
                    double integerKol = Math.Floor(copy.KOL);
                    if (convertedItem == null && integerKol == 0)
                    {
                        copy.Errors.Add(RowError.Critical(string.Format("Не удалось подобрать коэффициент пересчета.")));
                        isValid = false;
                        continue;
                    }
                    else if (convertedItem == null && integerKol > 0)
                    {
                        copy.Errors.Add(RowError.Warning(string.Format("Не удалось подобрать коэффициент пересчета. Количество было заменено c {0} на {1}, чтобы можно было продолжать", copy.KOL, integerKol)));
                        copy.KOL = integerKol;
                    }
                    else if (convertedItem != null)
                    {
                        IMPORT_REMAINS_ITEM newItem = copy.Copy();
                        newItem.KOL = convertedItem.KOL;
                        newItem.Q_FIRST = convertedItem.Q_FIRST;
                        newItem.MAN_PRICE = convertedItem.MAN_PRICE;
                        newItem.SALE_PRICE = convertedItem.SALE_PRICE;
                        newItem.COST_PRICE = convertedItem.COST_PRICE;
                        list.Add(newItem);

                        if (integerKol > 0)
                            copy.KOL = integerKol;
                        else
                            list.Remove(copy);
                    }
                }
            }
            return isValid;
        }

        private bool CheckDataDb(List<IMPORT_REMAINS_ITEM> list, List<CROSS_SUP_ITEM> list2)
        {
            bool isValid = true;
            XmlDocument doc = GetXmlDocument(list, list2);
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_RCY_CONVERT_RIGLA_DATA_CHECK", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                    SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
                    sqlda.Fill(ds);
                }
            }
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Guid g = Utils.GetGuid(dr, "ID_GLOBAL");
                    string text = Utils.GetString(dr, "TEXT");
                    AddError(g, text);
                    isValid = false;
                }
            }
            return isValid;
        }
        #endregion

        #region Discount Cards
        private delegate void ImportCardsProc(List<DCARD> listCards, List<CROS_TDC> listCardTypes);

        private void CheckAndImportDiscountCards(ImportCardsProc importProc)
        {
            string logFilePath = string.Empty;
            Exception ex = null;
            bool isValid = true;
            bool showLog = false;
            try
            {
                this.Enabled = false;
                if (!ValidateParamsDiscountCards()) return;
                using (ProgressStatusForm form = new ProgressStatusForm())
                {
                    form.DoWork += delegate(object sender, DoWorkEventArgs e)
                                     {
                                         BackgroundWorker worker = (BackgroundWorker)sender;
                                         worker.ReportProgress(0, "Загрузка дисконтных карт");
                                         List<DCARD> listCards = LoadData<DCARD>(textBoxDiscountCard.Text);
                                         worker.ReportProgress(0, "Загрузка соответствия типов дисконтных карт ");
                                         List<CROS_TDC> listCardTypes = LoadData<CROS_TDC>(textBoxDiscountCardType.Text);
                                         worker.ReportProgress(0, "Проверка данных");
                                         isValid = CheckDataDiscountCards(listCards, listCardTypes, worker);
                                         showLog = WriteErrorLogDiscountCards(listCards, listCardTypes, out logFilePath);

                                         worker.ReportProgress(0, "Загрузка данных");
                                         if (importProc != null)
                                             importProc.Invoke(listCards, listCardTypes);
                                     };
                    form.ShowDialog();
                    ex = form.Error;
                }
            }
            finally
            {
                if (ex != null)
                {
                    ShowError("Импорт данных был прерван из-за ошибки");
                }
                else
                {
                    ShowResult(importProc != null, logFilePath, isValid, showLog);
                }
                this.Enabled = true;
            }
        }

        private bool WriteErrorLogDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes, out string logFilePath)
        {
            string logFileName = string.Format("DCARD_LOG_{0:ddMMyyyy}.txt", DateTime.Now);
            logFilePath = Path.Combine(tbImportLogFolder.Text, logFileName);

            bool header1 = false;
            bool header2 = false;

            bool hasErrors1 = listCards.Exists(delegate(DCARD i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });
            bool hasErrors2 = listCardTypes.Exists(delegate(CROS_TDC i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });

            if (!hasErrors1 && !hasErrors2) return false;

            using (StreamWriter sw = new StreamWriter(logFilePath, false, Encoding.GetEncoding(1251)))
            {
                if (hasErrors1)
                {
                    foreach (DCARD item in listCards)
                    {
                        if (!header1)
                        {
                            sw.WriteLine("{0}", tbImportFile.Text);
                            sw.WriteLine(GetHeaderString<DCARD>());
                            header1 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("ОШИБКА;{0};{1}", s, GetFieldsString<DCARD>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("{0};{1};{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<DCARD>(item));
                    }
                }
                if (hasErrors2)
                {
                    foreach (CROS_TDC item in listCardTypes)
                    {
                        if (!header2)
                        {
                            sw.WriteLine("{0}", tbImportFile2.Text);
                            sw.WriteLine(GetHeaderString<CROS_TDC>());
                            header2 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("ОШИБКА;{0};{1}", s, GetFieldsString<CROS_TDC>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("{0};{1};{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<CROS_TDC>(item));
                    }
                }
            }
            return true;
        }

        private bool CheckDataDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes, BackgroundWorker worker)
        {
            errorLog.Clear();
            worker.ReportProgress(1, "проверка целостности данных");
            // проверка целостности данных 
            bool dataConsistencyValid = CheckConsistencyDiscountCards(listCards, listCardTypes);
            worker.ReportProgress(1, "проверка обязательных полей");
            // проверка обязательных полей 
            // ОШИБКА1: в файле DCARD.dbf не заполнены поля SHCOD, TIPDK
            bool discountCardValid = CheckRequiredFields<DCARD>(listCards);
            // ОШИБКА8: в файле CROS_TDC.dbf не заполнены поля TIPDKSTU, TIPDKEF
            bool discountCardTypesValid = CheckRequiredFields<CROS_TDC>(listCardTypes);
            // проверка согласованности данных
            worker.ReportProgress(1, "проверка согласованности данных");
            bool crossTableValid = CheckCrossTableDiscountCards(listCards, listCardTypes);
            // проверка возможности импорта в БД
            worker.ReportProgress(1, "проверка возможности импорта в БД");
            bool dataValid = CheckDataDbDiscountCards(listCards, listCardTypes);

            return dataConsistencyValid && discountCardValid && discountCardTypesValid && crossTableValid && dataValid;
        }

        private bool CheckConsistencyDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes)
        {
            bool result = true;
            // ОШИБКА6: проверка поля SHCOD в файле DCARD.dbf на уникальность
            Dictionary<string, List<Guid>> codes = new Dictionary<string, List<Guid>>();
            listCards.ForEach(delegate(DCARD dcard)
            {
                List<Guid> targetList = null;
                string errorMes = null;
                if (codes.ContainsKey(dcard.SHCOD))
                {
                    targetList = codes[dcard.SHCOD];
                    errorMes = string.Format("ШК дисконтной карты \"{0}\" не уникален", dcard.SHCOD);
                    result = false;
                }
                else
                {
                    targetList = new List<Guid>();
                    codes.Add(dcard.SHCOD, targetList);
                }

                targetList.Add(dcard.ID_GLOBAL);

                if (targetList.Count == 2)
                    AddError(targetList[0], errorMes);
                if (targetList.Count >= 2)
                    AddError(targetList[targetList.Count - 1], errorMes);
            });

            // ОШИБКА9: проверка если в файле CROS_TDC.dbf одному типу ДК еФ2 соответствует более одного  типа ДК старой СТУ
            // + проверка если в файле CROS_TDC.dbf одному типу ДК старой СТУ соответствует более одного  типа ДК еФ2
            Dictionary<string, List<Guid>> typesEf = new Dictionary<string, List<Guid>>();
            Dictionary<double, List<Guid>> typesStu = new Dictionary<double, List<Guid>>();
            listCardTypes.ForEach(delegate(CROS_TDC type)
            {
                // прроверка кодов ЕФ на уникальность
                List<Guid> targetListEf = null;
                string errorMesEf = null;
                if (typesEf.ContainsKey(type.TIPDKEF))
                {
                    targetListEf = typesEf[type.TIPDKEF];
                    errorMesEf = string.Format("В кросс-файле для одного кода типа ДК еФ2 \"{0}\" найдено более одного типа ДК СТУ", type.TIPDKEF);
                    result = false;
                }
                else
                    targetListEf = new List<Guid>();

                targetListEf.Add(type.ID_GLOBAL);

                if (targetListEf.Count == 2)
                    AddError(targetListEf[0], errorMesEf);
                if (targetListEf.Count >= 2)
                    AddError(targetListEf[targetListEf.Count - 1], errorMesEf);

                // прроверка кодов СТУ на уникальность
                List<Guid> targetListStu = null;
                string errorMesStu = null;
                if (typesStu.ContainsKey(type.TIPDKSTU))
                {
                    targetListStu = typesStu[type.TIPDKSTU];
                    errorMesStu = string.Format("В кросс-файле для одного кода типа ДК СТУ \"{0}\" найдено более одного типа ДК еФ2", type.TIPDKSTU);
                    result = false;
                }
                else
                    targetListStu = new List<Guid>();

                targetListStu.Add(type.ID_GLOBAL);

                if (targetListStu.Count == 2)
                    AddError(targetListStu[0], errorMesStu);
                if (targetListStu.Count >= 2)
                    AddError(targetListStu[targetListStu.Count - 1], errorMesStu);            
            });

            return result;
        }

        private bool CheckDataDbDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes)
        {
            bool result = true;

            // ОШИБКА5: В еФ2 не найдено типа ДК с ID_DISCOUNT2_CARD_TYPE_GLOBAL = TIPDKEF, указанным в файле CROS_TDC.dbf
            string dctIdsQuery = "select ID_DISCOUNT2_CARD_TYPE_GLOBAL from dbo.DISCOUNT2_CARD_TYPE";
            List<Guid> dctEfIds = new List<Guid>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(dctIdsQuery, conn);
                DataTable dtDct = new DataTable();
                da.Fill(dtDct);
                foreach (DataRow drow in dtDct.Rows)
                    dctEfIds.Add((Guid)drow["ID_DISCOUNT2_CARD_TYPE_GLOBAL"]);
            }

            foreach (CROS_TDC ctdc in listCardTypes)
            {
                try
                {
                    Guid guidFromFile = new Guid(ctdc.TIPDKEF);
                    if (!dctEfIds.Contains(guidFromFile))
                    {
                        AddError(ctdc.ID_GLOBAL, string.Format("В еФ2 не найдено типа ДК \"{0}\"", guidFromFile));
                        result = false;
                    }
                }
                catch(Exception ex)
                {
                    AddError(ctdc.ID_GLOBAL, string.Format("Неожиданная ошибка!{0}{1}", Environment.NewLine, ex.Message));
                    result = false;
                }
            }

            // ОШИБКА7: Проверка есть ли дисконтная карта, по которой уже были продажи из файла DCARD.dbf в еФарма2
            string queryUsedCards = "EXEC REPEX_DISCOUNT2_CARD_GET_USED";
            List<string> usedEfBarcodes = new List<string>();
            using (SqlConnection conn2 = new SqlConnection(connectionString))
            {
                SqlDataAdapter da2 = new SqlDataAdapter(queryUsedCards, conn2);
                DataTable dtUsed = new DataTable();
                da2.Fill(dtUsed);
                foreach (DataRow drow in dtUsed.Rows)
                    usedEfBarcodes.Add((string)drow["BARCODE"]);
            }

            foreach (DCARD dcard in listCards)
            {
                if (usedEfBarcodes.Contains(dcard.SHCOD))
                {
                    AddError(dcard.ID_GLOBAL, "По данной дисконтной карте уже предоставлены скидки в еФарма2");
                    result = false;
                }
            }

            return result;
        }

        private bool CheckCrossTableDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes)
        {
            bool result = true;
            foreach (DCARD dcard in listCards)
            {
                int linkedCount = listCardTypes.FindAll(delegate(CROS_TDC s)
                                                                  {
                                                                      return s.TIPDKSTU == dcard.TIPDK;
                                                                  }).Count;
                if (linkedCount == 0)
                {
                    // ОШИБКА4: нет типа карты связанного с картой
                    AddError(dcard.ID_GLOBAL, string.Format("Типу ДК \"{0}\" из файла DCARD.dbf не найдено соответствий в файле CROS_TDC.dbf", dcard.TIPDK));
                    result = false;
                }
                else if (linkedCount >= 2)
                {
                    // ОШИБКА2: более 1 типа карты связано с картой
                    AddError(dcard.ID_GLOBAL, string.Format("Типу ДК \"{0}\" из файла DCARD.dbf соответствует более одного типа ДК в файле CROS_TDC.dbf", dcard.TIPDK));
                    result = false;
                }
            }
            return result;
        }

        private bool ValidateParamsDiscountCards()
        {
            if (string.IsNullOrEmpty(textBoxDiscountCard.Text) || !File.Exists(textBoxDiscountCard.Text))
            {
                ShowError("Не задан путь к файлу дисконтных карт или файл не существует");
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDiscountCardType.Text) || !File.Exists(textBoxDiscountCardType.Text))
            {
                ShowError("Не задан путь к файлу соответствия типов дисконтных карт или файл не существует");
                return false;
            }

            if (string.IsNullOrEmpty(tbImportLogFolder.Text) || !Directory.Exists(tbImportLogFolder.Text))
            {
                ShowError("Не задан путь к каталогу для сохранения результатов импорта или каталог не существует");
                return false;
            }

            if (SelectedFormat == null)
            {
                ShowError("Не задан формат импорта");
                return false;
            }
            return true;
        }

        private void ImportDiscountCardsData(List<DCARD> listCards, List<CROS_TDC> listCardTypes)
        {
            listCards.RemoveAll(delegate(DCARD item) { return errorLog.ContainsKey(item.ID_GLOBAL); });
            listCards.RemoveAll(delegate(DCARD item)
                            {
                                int cardTypesCountByCard = listCardTypes.FindAll(delegate(CROS_TDC s)
                                                                  {
                                                                      return s.TIPDKSTU == item.TIPDK;
                                                                  }).Count;
                                // ОШИБКА2: более 1 типа карты связано с картой
                                // ОШИБКА4: нет типа карты связанного с картой
                                return cardTypesCountByCard != 1;
                            });
            listCards.RemoveAll(delegate(DCARD item)
                             {
                                 return item.Errors.Count > 0 && item.Errors.Exists(delegate(RowError err)
                                                                                    {
                                                                                        return err.RowErrorLevel == RowErrorLevel.Critical;
                                                                                    });
                             });
            if (listCards.Count > 0)
                SaveDiscountCards(listCards, listCardTypes);
        }

        private void SaveDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes)
        {
            List<string> docList = GetXmlDocumentsDiscountCards(listCards, listCardTypes, 0, 1000);
            //doc.Save("c:\\debug\\DCARD.xml");
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();
                    for (int i = 0; i <= docList.Count - 1; i++)
                    {
                        string doc1000 = docList[i];
                        using (SqlCommandEx comm1000 = new SqlCommandEx("REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT_DCARD", conn))
                        {
                            try
                            {
                                tryLoadPacket(conn, comm1000, doc1000);
                            }
                            catch (SqlException)
                            {
                                // если неожиданная ошибка в пакете, грузим пакет по 1 записи пока не найдём, которая в самом деле плохая
                                List<string> docListByOne = GetXmlDocumentsDiscountCards(listCards, listCardTypes, i * 1000, 1);
                                for (int j = 0; j <= 1000 - 1; j++)
                                {
                                    int cardIndex = i * 1000 + j;
                                    string doc1 = docListByOne[i];
                                    using (SqlCommandEx comm1 = new SqlCommandEx("REPEX_RCY_CONVERT_RIGLA_DATA_IMPORT_DCARD", conn))
                                    {
                                        try
                                        {
                                            tryLoadPacket(conn, comm1, doc1);
                                        }
                                        catch (SqlException ex1)
                                        {
                                            MessageBox.Show("Исключительная ситуация при загрузке строки: " + Environment.NewLine +
                                                listCards[cardIndex].ToString() + Environment.NewLine +
                                                "Сообщение об ошибке: " + Environment.NewLine +
                                                ex1.Message);
                                        }
                                    }
                                }
                            }
                        }
                        docList[i] = null;
                    }
                }
                finally
                {
                    conn.Close();
                }                
            }
        }

        private void tryLoadPacket(SqlConnection conn, SqlCommandEx comm, string doc)
        {
            SqlTransaction tran = conn.BeginTransaction();
            comm.SqlCommand.Transaction = tran;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc;
            comm.ExecuteNonQuery();
            tran.Commit();
        }

        private List<string> GetXmlDocumentsDiscountCards(List<DCARD> listCards, List<CROS_TDC> listCardTypes, int startIndex, int packetSize)
        {
            List<string> docList = new List<string>();

            int lastLoadedIndex = -1;

            nextDoc:
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "IMPORT_FORMAT", SelectedFormat.ImportFormat.ToString().ToUpper());
            for (int i = startIndex; (i <= listCards.Count-1) && (i <= startIndex + packetSize - 1);i++)
            {
                DCARD item = listCards[i];
                XmlNode node = Utils.AddNode(root, "DCARD");
                item.ToXml(node);
                lastLoadedIndex = i;
            }
            foreach (CROS_TDC item in listCardTypes)
            {
                XmlNode node = Utils.AddNode(root, "CROS_DCT");
                item.ToXml(node);
            }

            docList.Add(doc.InnerXml);
            if (lastLoadedIndex <= listCards.Count - 2)
            {
                startIndex = lastLoadedIndex + 1;
                goto nextDoc;
            }


            return docList;
        }

        #endregion

        #region GUI events
        private void bSelectFile_Click(object sender, EventArgs e)
        {
            SelectFile(tbImportFile, string.Format("Остатки {0} (OSTATKI.dbf)|OSTATKI.dbf", SelectedFormat == null ? string.Empty : SelectedFormat.ToString()));
        }

        private void SelectFile(Control sender, string filter)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                sender.Text = ofd.FileName;
            }
        }

        private void bSelectFile2_Click(object sender, EventArgs e)
        {
            SelectFile(tbImportFile2, "Соответствия кодов поставщиков (CROS_SUPP.dbf)|CROS_SUPP.dbf");
        }

        private void bSelectFolder_Click(object sender, EventArgs e)
        {
            SelectFolder(tbImportLogFolder, "Выберите каталог для сохранения файла отчета об импорте");
        }

        private void ucStore_ValueChanged(object sender, EventArgs e)
        {
            chkENVD.Checked = true;
        }

        private void buttonDiscountCard_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxDiscountCard, "Файл дисконтных карт (DCARD.dbf)|DCARD.dbf");
        }

        private void buttonDiscountCardType_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxDiscountCardType, "Файл соответствия типов дисконтных карт (CROS_TDC.dbf)|CROS_TDC.dbf");
        }
        #endregion
    }
  
  public enum LoadAction{OnlyCheck, CheckAndLoad}
  
  enum ImportFormat{A2000, QWERTY}
  
  class ImportFormatDescription
  {
    private ImportFormat importFormat;
    private string description;

    public ImportFormat ImportFormat
    {
      get { return importFormat; }
    }

    public string Description
    {
      get { return description; }
    }

    public ImportFormatDescription(ImportFormat importFormat)
    {
      this.importFormat = importFormat;
      switch (importFormat)
      {
        case ImportFormat.A2000:
          description = "Аптека 2000";
          break;
        case ImportFormat.QWERTY:
          description = "Qwerty";
          break;
      }
    }

    public override bool Equals(object obj)
    {
      ImportFormatDescription d = obj as ImportFormatDescription;
      if (d != null)
        return d.importFormat == this.importFormat;
      return base.Equals(obj);
    }

    public override string ToString()
    {
      return description;
    }
  }
}