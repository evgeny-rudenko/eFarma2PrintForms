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
using ePlus.CommonEx.DataAccess;

namespace ImportNovgorod
{
    public partial class ParamsForm : ExternalReportForm, IExternalReportFormMethods
    {
        public string ReportName
        {
            get { return "Импорт данных Новгород"; }
        }

        #region Loading and saving
        private string settingsFilePath;
        public ParamsForm()
        {
            InitializeComponent();
            settingsFilePath = Path.Combine(Utils.TempDir(), this.Name + "Settings.xml");
            FillFormats();
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

        private bool DoImportRemains
        {
            get
            {
                return GetDoImportRemains();
            }
        }

        private delegate bool GetDoImportRemainsDelegate();
        private bool GetDoImportRemains()
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new GetDoImportRemainsDelegate(this.getDoImportRemains));
            return getDoImportRemains();
        }

        private bool getDoImportRemains()
        {
            return checkBoxImportRemains.Checked;
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            if (Utils.IsDesignMode(this)) return;
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
                tbImportFileRemains.Text = Utils.GetString(root, "DATA_FILE_REMAINS_PATH");
                tbImportFileContracts.Text = Utils.GetString(root, "DATA_FILE_CONTRACTS_PATH");
                tbImportLogFolder.Text = Utils.GetString(root, "LOG_FOLDER");
                checkBoxImportRemains.Checked = Utils.GetBool(root, "IMPORT_REMAINS");
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
            Utils.AddNode(root, "DATA_FILE_REMAINS_PATH", tbImportFileRemains.Text);
            Utils.AddNode(root, "DATA_FILE_CONTRACTS_PATH", tbImportFileContracts.Text);
            Utils.AddNode(root, "LOG_FOLDER", tbImportLogFolder.Text);
            Utils.AddNode(root, "IMPORT_REMAINS", DoImportRemains);
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

        #region Common routine
        private DataSet GetFileData(string filePath)
        {
            string tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), "dbf");
            File.Copy(filePath, tempFilePath, true);
            string tableName = Path.GetFileNameWithoutExtension(tempFilePath);
            string oleDbConnectionString = DBFUtils.FormatConnectionString(
              string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=dBASE IV;", Path.GetDirectoryName(tempFilePath)));
           
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
            SortedList<string, PropertyInfo> sortedProps = new SortedList<string, PropertyInfo>();
            foreach (PropertyInfo pi in properties)
                sortedProps.Add(pi.Name, pi);

            sb.Append("<TR>");
            sb.Append("<TH>Тип ошибки</TH>");
            sb.Append("<TH>Описание ошибки</TH>");
            foreach (PropertyInfo pi in sortedProps.Values)
            {
                FormatAttribute attr = (FormatAttribute)Attribute.GetCustomAttribute(pi, typeof(FormatAttribute));
                if (attr != null)
                    sb.AppendFormat("<TH>{0}</TH>", pi.Name);
            }
            sb.Append("</TR>"); 
            return sb.ToString();
        }

        private string GetFieldsString<T>(T item)
        where T : IObject
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();
            SortedList<string, PropertyInfo> sortedProps = new SortedList<string, PropertyInfo>();
            foreach (PropertyInfo pi in properties)
                sortedProps.Add(pi.Name, pi);

            foreach (PropertyInfo pi in sortedProps.Values)
            {
                FormatAttribute attr = (FormatAttribute)Attribute.GetCustomAttribute(pi, typeof(FormatAttribute));
                if (attr != null)
                    sb.AppendFormat("<TD>{0}</TD>", attr.Format(pi.GetValue(item, null)));
            }
            return sb.ToString();
        }

        #endregion

        #region Rest
        private XmlDocument GetXmlDocument(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "IMPORT_FORMAT", SelectedFormat.ImportFormat.ToString().ToUpper());
            if (list != null)
                foreach (IMPORT_REMAINS_ITEM item in list)
                {
                    XmlNode node = Utils.AddNode(root, "ITEM");
                    item.ToXml(node);
                }
            if (list2 != null)
                foreach (CONTRACTS_ITEM item in list2)
                {
                    XmlNode node = Utils.AddNode(root, "CONTRACTS_ITEM");
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

        private void Save(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2)
        {
            XmlDocument doc = GetXmlDocument(list, list2);
//#if DEBUG
//            doc.Save("c:\\temp\\OSTATKI.xml");
//#endif
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_IMPORT_NOVGOROD_IMPORT", conn))
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
            if (string.IsNullOrEmpty(tbImportFileRemains.Text) || !File.Exists(tbImportFileRemains.Text))
            {
                ShowError("Не задан путь к файлу остатков или файл не существует");
                return false;
            }
            if (string.IsNullOrEmpty(tbImportFileContracts.Text) || !File.Exists(tbImportFileContracts.Text))
            {
                ShowError("Не задан путь к файлу договоров или файл не существует");
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

        private void ShowError(string s)
        {
            Logger.ShowMessage(s, 0, MessageBoxIcon.Error);
        }

        private void ShowResult(bool import, string logFilePath, bool isValid, bool showLog)
        {
            string message = isValid ? string.Format("успешно{0}", showLog ? string.Empty : " и в полном объеме") : "с ошибками";
            MessageBoxIcon icon = isValid ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            Logger.ShowMessage(string.Format("{0}{1}", import ? "Импорт данных завершился " : "Проверка данных завершилась ", message), 0, icon);

//            if (showLog && !string.IsNullOrEmpty(logFilePath) && File.Exists(logFilePath))
//                Process.Start(logFilePath);
            if (import)
            {
                if (DoImportRemains)
                {
                    PluginFormView pfv = AppManager.GetPluginView("IMPORT_REMAINS");
                    AppManager.RegisterForm(pfv);
                    pfv.Show();
                }
                if (SelectedFormat.ImportFormat != ImportFormat.None)
                {
                    PluginFormView pfv2 = AppManager.GetPluginView("CONTRACTS");
                    AppManager.RegisterForm(pfv2);
                    pfv2.Show();
                }
            }
        }

        private delegate void ImportProc(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2);

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
                                         List<CONTRACTS_ITEM> list2 = null;
                                         if (SelectedFormat.ImportFormat != ImportFormat.None)
                                         {
                                             worker.ReportProgress(0, "Загрузка договоров");
                                             list2 = LoadData<CONTRACTS_ITEM>(tbImportFileContracts.Text);
                                         }
                                         List<IMPORT_REMAINS_ITEM> list = null;
                                         if (DoImportRemains)
                                         {
                                             worker.ReportProgress(0, "Загрузка остатков");
                                             list = LoadData<IMPORT_REMAINS_ITEM>(tbImportFileRemains.Text);
                                         }
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

        private void ImportData(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2)
        {
            if (list != null)
            {
                list.RemoveAll(delegate(IMPORT_REMAINS_ITEM item)
                    { return errorLog.ContainsKey(item.ID_GLOBAL); });

                list.RemoveAll(delegate(IMPORT_REMAINS_ITEM item)
                                 {
                                     return item.Errors.Count > 0 && item.Errors.Exists(delegate(RowError err)
                                                                                        {
                                                                                            return err.RowErrorLevel == RowErrorLevel.Critical;
                                                                                        });
                                 });
            }
            if (list2 != null)
            {
                list2.RemoveAll(delegate(CONTRACTS_ITEM item)
                    { return errorLog.ContainsKey(item.ID_GLOBAL); });

                list2.RemoveAll(delegate(CONTRACTS_ITEM item)
                                 {
                                     return item.Errors.Count > 0 && item.Errors.Exists(delegate(RowError err)
                                                                                        {
                                                                                            return err.RowErrorLevel == RowErrorLevel.Critical;
                                                                                        });
                                 });
            }

            Save(list, list2);
        }

        public override void Execute(string connectionString, string folderPath)
        {
            base.Execute(connectionString, folderPath);
        }

        private bool WriteErrorLog(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2, out string logFilePath)
        {
            string logFileName = string.Format("OSTATKI_LOG_{0:ddMMyyyy}.htm", DateTime.Now);
            logFilePath = Path.Combine(tbImportLogFolder.Text, logFileName);

            bool header1 = false;
            bool header2 = false;

            bool hasErrors1 = (list == null)? false : list.Exists(delegate(IMPORT_REMAINS_ITEM i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });

            bool hasErrors2 = (list2 == null)? false : list2.Exists(delegate(CONTRACTS_ITEM i)
                                          {
                                              return i.Errors.Count > 0 || errorLog.ContainsKey(i.ID_GLOBAL);
                                          });

            if (!hasErrors1 && !hasErrors2) return false;

            using (StreamWriter sw = new StreamWriter(logFilePath, false, Encoding.GetEncoding(1251)))
            {
                sw.WriteLine("<HTML><BODY>");
                if (hasErrors1)
                {
                    foreach (IMPORT_REMAINS_ITEM item in list)
                    {
                        if (!header1)
                        {
                            sw.WriteLine("{0}", tbImportFileRemains.Text);
                            sw.WriteLine("<TABLE border=\"1\">");
                            sw.WriteLine(GetHeaderString<IMPORT_REMAINS_ITEM>());
                            header1 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("<TR><TD>ОШИБКА</TD><TD>{0}</TD>{1}", s, GetFieldsString<IMPORT_REMAINS_ITEM>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("<TR><TD>{0}</TD><TD>{1}</TD>{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<IMPORT_REMAINS_ITEM>(item));
                    }
                    if (header1)
                        sw.WriteLine("</TABLE>");
                }
                if (hasErrors2)
                {
                    foreach (CONTRACTS_ITEM item in list2)
                    {
                        if (!header2)
                        {
                            sw.WriteLine("{0}", tbImportFileContracts.Text);
                            sw.WriteLine("<TABLE border=\"1\">");
                            sw.WriteLine(GetHeaderString<CONTRACTS_ITEM>());
                            header2 = true;
                        }
                        if (errorLog.ContainsKey(item.ID_GLOBAL))
                            foreach (string s in errorLog[item.ID_GLOBAL])
                                sw.WriteLine("<TR><TD>ОШИБКА</TD><TD>{0}</TD>{1}", s, GetFieldsString<CONTRACTS_ITEM>(item));
                        if (item.Errors.Count > 0)
                            foreach (RowError err in item.Errors)
                                sw.WriteLine("<TR><TD>{0}</TD><TD>{1}</TD>{2}", new RowErrorLevelDescription(err.RowErrorLevel), err.ErrorText, GetFieldsString<CONTRACTS_ITEM>(item));
                    }
                    if (header2)
                        sw.WriteLine("</TABLE>");
                }
                sw.WriteLine("</BODY></HTML>");
            }
            return true;
        }

        private bool CheckData(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2, BackgroundWorker worker)
        {
            errorLog.Clear();
            bool remainsValid = true;
            if (DoImportRemains)
            {
                worker.ReportProgress(1, "проверка обязательных полей остатков");
                remainsValid = CheckRequiredFields<IMPORT_REMAINS_ITEM>(list);
            }
            bool contractsValid =true;
            if (SelectedFormat.ImportFormat != ImportFormat.None)
            {
                worker.ReportProgress(1, "проверка обязательных полей договоров");
                contractsValid = CheckRequiredFields<CONTRACTS_ITEM>(list2);
            }

            // проверка возможности импорта в БД
            worker.ReportProgress(1, "проверка корректности данных");
            bool dataValid = CheckDataDb(list, list2);
            worker.ReportProgress(1, "конвертация кодов товаров остатков");
            bool amountsConverted = ConvertRemains(list);

            return remainsValid && contractsValid && amountsConverted && dataValid;
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
            XmlDocument doc = GetXmlDocument(list, null);
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_IMPORT_NOVGOROD_CONVERT", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                    SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
                    sqlda.Fill(ds);
                }
            }
            if (ds.Tables.Count > 0)
            {
                //позиции в лог
                List<LOG> logList = GetList<LOG>(ds.Tables[0]);
                Dictionary<Guid, string> dictLogList = new Dictionary<Guid,string>();
                foreach (LOG log in logList)
                {
                    if (!dictLogList.ContainsKey(log.ID_GLOBAL))
                        dictLogList.Add(log.ID_GLOBAL, log.REASON);
                    else dictLogList[log.ID_GLOBAL] = log.REASON;
                }
                string errorText = string.Empty;
                foreach (KeyValuePair<Guid,string> key in dictLogList)
                {                    
                    if (key.Value=="CREDIT")
                        errorText = "Код товара в льготе не найден";
                    else errorText = "Код товара однозначно не определен";
                    AddError(key.Key, errorText);

                }
                //нормальные позиции
                List<IMPORT_REMAINS_ITEM> normal = GetList<IMPORT_REMAINS_ITEM>(ds.Tables[1]);
                //"конвертнутые" позиции
                List<IMPORT_REMAINS_ITEM> converted = GetList<IMPORT_REMAINS_ITEM>(ds.Tables[2]);
                foreach (IMPORT_REMAINS_ITEM convertedItem in converted)
                {
                    // удаляем из нормальных позиций, где нет ни одной единицы товара,  конвертированых
                    normal.RemoveAll(delegate(IMPORT_REMAINS_ITEM item) {return (item.ID_GLOBAL == convertedItem.ID_GLOBAL) && (item.OST < 1.0); });
                }

                foreach (IMPORT_REMAINS_ITEM item in normal)
                {
                    IMPORT_REMAINS_ITEM normalItem = list.Find(delegate(IMPORT_REMAINS_ITEM i)
                                                               {
                                                                   return i.ID_GLOBAL == item.ID_GLOBAL;
                                                               });
                    normalItem.DENOMINATOR = item.DENOMINATOR;
                    normalItem.ID_GOODS = item.ID_GOODS;
                    normalItem.CONTROL_TYPE = item.CONTROL_TYPE;
                    normalItem.OST2 = item.OST2;
                    normalItem.P_SUP = item.P_SUP;
                    normalItem.P_SUP2 = item.P_SUP2;
                    if (normalItem.OST != item.OST)
                    {
                        normalItem.Errors.Add(RowError.Warning(string.Format("Не удалось подобрать коэффициент пересчета. Количество было заменено c {0} на {1}, чтобы можно было продолжать", normalItem.OST, item.OST)));
                        normalItem.OST = item.OST;
                    }
                    if (normalItem.OST == 0.0)
                        normalItem.Errors.Add(RowError.Critical(string.Format("Не удалось подобрать коэффициент пересчета. Количество не может быть нулевым при загрузке.")));
                }

                foreach (IMPORT_REMAINS_ITEM item in converted)
                {
                    IMPORT_REMAINS_ITEM convertedItem = list.Find(delegate(IMPORT_REMAINS_ITEM i)
                                                               {
                                                                   return i.ID_GLOBAL == item.ID_GLOBAL;
                                                               });
                    list.RemoveAll(delegate(IMPORT_REMAINS_ITEM _item) { return _item.ID_GLOBAL == item.ID_GLOBAL; });
                    
                    IMPORT_REMAINS_ITEM copy = convertedItem.Copy();
                    list.Add(copy);
                    double integerKol = Math.Floor(copy.OST);
                    if (convertedItem == null && integerKol == 0)
                    {
                        copy.Errors.Add(RowError.Critical(string.Format("Не удалось подобрать коэффициент пересчета.")));
                        isValid = false;
                        continue;
                    }
                    else if (convertedItem == null && integerKol > 0)
                    {
                        copy.Errors.Add(RowError.Warning(string.Format("Не удалось подобрать коэффициент пересчета. Количество было заменено c {0} на {1}, чтобы можно было продолжать", copy.OST, integerKol)));
                        copy.OST = integerKol;
                    }
                    else if (convertedItem != null)
                    {
                        IMPORT_REMAINS_ITEM newItem = copy.Copy();
                        newItem.ID_GOODS = item.ID_GOODS;
                        newItem.CONTROL_TYPE = item.CONTROL_TYPE;
                        newItem.OST = item.OST;
                        newItem.OST2 = item.OST2;
                        newItem.CO = item.CO;
                        newItem.DENOMINATOR = item.DENOMINATOR;
                        newItem.P_SUP = item.P_SUP;
                        newItem.P_SUP2 = item.P_SUP2;
                        newItem.P_APT = item.P_APT;
                        list.Add(newItem);

                        if (integerKol > 0)
                            copy.OST = integerKol;
                        else
                            list.Remove(copy);
                    }
                }
            }
            return isValid;
        }

        private bool CheckDataDb(List<IMPORT_REMAINS_ITEM> list, List<CONTRACTS_ITEM> list2)
        {
            bool isValid = true;
            XmlDocument doc = GetXmlDocument(list, list2);
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (SqlCommandEx comm = new SqlCommandEx("REPEX_IMPORT_NOVGOROD_CHECK", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                    SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
                    sqlda.Fill(ds);
                }
            }
            if (ds.Tables.Count > 0)
            {
                // ошибки остатков
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Guid g = Utils.GetGuid(dr, "ID_GLOBAL");
                    string code = Utils.GetString(dr, "CODE");
                    string text = Utils.GetString(dr, "TEXT");
                    AddError(g, text);
                    isValid = false;
                }
                // ошибки договоров
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Guid g = Utils.GetGuid(dr, "ID_GLOBAL");
                    string code = Utils.GetString(dr, "CODE");
                    string text = Utils.GetString(dr, "TEXT");

                    //все ошибки считаем некритическими, чтобы правильно посчитать сумму по договору
                    CONTRACTS_ITEM badItem = list2.Find(delegate(CONTRACTS_ITEM i) { return i.ID_GLOBAL == g; });
                    if (badItem != null)
                        badItem.SetError(text, RowErrorLevel.Warning);
                    isValid = false;
                }
            }
            return isValid;
        }
        #endregion

        #region GUI events
        private void bSelectFile_Click(object sender, EventArgs e)
        {
            SelectFile(tbImportFileRemains, "Остатки (*.dbf)|*.dbf");
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
            SelectFile(tbImportFileContracts, string.Format("{0} (*.dbf)|*.dbf", SelectedFormat == null ? string.Empty : SelectedFormat.ToString()));
        }

        private void bSelectFolder_Click(object sender, EventArgs e)
        {
            SelectFolder(tbImportLogFolder, "Выберите каталог для сохранения файла отчета об импорте");
        }

        private void cbFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelContracts.Enabled = (getSelectedFormat().ImportFormat != ImportFormat.None);
        }
        private void checkBoxImportRemains_CheckedChanged(object sender, EventArgs e)
        {
            panelRemains.Enabled = DoImportRemains;
        }

        #endregion
    }
  
  public enum LoadAction{OnlyCheck, CheckAndLoad}  
}