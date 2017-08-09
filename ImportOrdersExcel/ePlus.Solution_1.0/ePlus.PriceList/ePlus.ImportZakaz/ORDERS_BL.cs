using System;
using System.Collections.Generic;
using System.Text;
//using System.Windows;
using ePlus.CommonEx.DataAccess;
using ePlus.MetaData.Server;
using System.Data;
//using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Core;
using System.Data.SqlClient;
using ePlus.CommonEx.AccessPoint;
using System.IO;
using ePlus.MetaData.Client;

namespace ePlus.PriceList.ImportZakaz
{
    public class ORDERS_BL
    {
        DataService_BL bl = new DataService_BL();
        CONFIGURATION_IMPORT confImport = new CONFIGURATION_IMPORT();
        List<XmlDocument> docs = new List<XmlDocument>();
        string fileDir = Utils.TempDir();

        //Сохранение заказа
        public void Save(string save_state,XmlDocument doc, long id_configuration_import)
        {
            using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
            {
                SqlCommandEx comm = new SqlCommandEx("USP_ORDERS_SAVE", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@STATE_DOC", SqlDbType.VarChar)).Value = save_state;
                comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                comm.Parameters.Add(new SqlParameter("@ID_CONFIGURATION_IMPORT", SqlDbType.BigInt)).Value = id_configuration_import;
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Загрузка заказа из файла формата .xml
        /// </summary>
        /// <param name="apm">Точка доступа</param>
        /// <returns>Список файлов формата xml</returns>
        public Dictionary<XmlDocument,string> Load(AccessPointManager apm)
        {
            List<string> list = apm.GetFiles("*.xml");
            if (list.Count == 0) return null;
            List<string> folderPaths = new List<string>();
            
            list.ForEach(delegate(string _file) { folderPaths.Add(Path.GetFileName(_file)); });
            XmlDocument doc;
            Dictionary<XmlDocument, string> dictDocs = new Dictionary<XmlDocument, string>();
            foreach (string folderPath in folderPaths)
            {
                apm.Receive(folderPath, Path.Combine(fileDir, folderPath));                 
                doc = new XmlDocument();
                doc.Load(Path.Combine(fileDir, folderPath));                
                if (!dictDocs.ContainsKey(doc))
                    dictDocs.Add(doc, folderPath);
            }
            return dictDocs;
        }

        /// <summary>
        /// загрузка файла-льготы формата .ctl
        /// </summary>
        /// <param name="apm">Точка доступа</param>
        /// <returns>Список файлов формата ctl</returns>
        public Dictionary<XmlDocument, string> LoadCtlFile(AccessPointManager apm) //string localFile
        {            
            List<string> listRars = apm.GetFiles("*.rar");
            List<string> rarFiles = new List<string>();
            listRars.ForEach(delegate(string _file) { rarFiles.Add(Path.GetFileName(_file)); });

            string ctlDirectory = Path.Combine(fileDir, "CTL");
            if (!Directory.Exists(ctlDirectory))
                Directory.CreateDirectory(ctlDirectory);

            foreach (string rarFile in rarFiles)
            {
                apm.Receive(rarFile, Path.Combine(fileDir, rarFile));
                string newFile = Path.Combine(ctlDirectory, Path.GetFileName(rarFile));
                if (File.Exists(newFile))
                    File.Delete(newFile);
                RarUtils.Extract(Path.Combine(fileDir, rarFile), Path.GetDirectoryName(newFile), string.Empty);
                File.Delete(Path.Combine(fileDir, rarFile)); //удаляем фай из темпа    
            }

            //foreach (string listRar in listRars)
            //{
            //    //File.Exists();
            //    string newFile = Path.Combine(ctlDirectory, Path.GetFileName(listRar));
            //    if (File.Exists(newFile))
            //        File.Delete(newFile);
            //    File.Copy(listRar, newFile);
                
            //    RarUtils.Extract(newFile, Path.GetDirectoryName(newFile), string.Empty);
            //    File.Delete(newFile); //Удаляем архив в темпе                
            //}
            
            //берем все файлы из папки ctl
            List<string> list = new List<string>(Directory.GetFiles(ctlDirectory));//apm.GetFiles("*.ctl");
            //отсеиваем с левым расширением
            list.RemoveAll(delegate(string _file) {return Path.GetExtension(_file) != ".ctl";});
            if (list.Count == 0) return null;

            List<string>  folderPaths = new List<string>();

            list.ForEach(delegate(string _file) { folderPaths.Add(Path.GetFileName(_file)); });
            XmlDocument doc;
            Dictionary<XmlDocument, string> dictDocs = new Dictionary<XmlDocument, string>();
            foreach (string folderPath in folderPaths)
            {
                //apm.Receive(folderPath, Path.Combine(ctlDirectory, folderPath));
                doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML", null);
                //
                using (StreamReader sr = new StreamReader(Path.Combine(ctlDirectory, folderPath), Encoding.GetEncoding(1251)))
                {
                    CtlTable table = new CtlTable();
                    table.ReadTableStruct(sr);
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        table.LoadRow(line);
                        XmlNode node = Utils.AddNode(root, "ITEM", null);

                        foreach (CtlField field in table.FieldsList)
                        {
                            if (string.IsNullOrEmpty(field.Value)) continue;
                            Utils.AddNodeEx(node, field.FieldName, field.Value);
                        }
                    }
                }
                //
                //doc.Load(Path.Combine(fileDir, folderPath));
                if (!dictDocs.ContainsKey(doc))
                    dictDocs.Add(doc, folderPath);
            }
            //list.ForEach(delegate (string _file) { File.Delete(_file);});
            return dictDocs;
        }

        //проверка валидности заказа(по описанным в ТЗ критериям) и установка его статуса
        public string ValidateOrder(XmlDocument doc, string import_key)
        {
            using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
            {
                SqlCommandEx comm = new SqlCommandEx("USP_ORDERS_LOAD_VALIDATE", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                comm.Parameters.Add(new SqlParameter("@KEY", SqlDbType.VarChar)).Value = import_key;
                conn.Open();
                return (string)comm.ExecuteScalar();
            }
        }

        //Перемещение заказа в архив
        public void OrderMoveToArchiv(AccessPointManager _apm, AccessPointManager apmImport, string fileName)
        {
            string directory = fileDir;

            if (Path.GetExtension(fileName) == ".ctl")
            {
                directory = Path.Combine(fileDir, "CTL"); //раб папка
                //удаляю все архивы в исходнике
                List<string> listRars = apmImport.GetFiles("*.rar");
                foreach (string listRar in listRars)                
                    apmImport.Delete(listRar);                
            }

            if (Path.GetExtension(fileName) == ".xls")
            {
                directory = Path.GetDirectoryName(fileName);
                fileName = Path.GetFileName(fileName);
            }
                
            //if (!File.Exists(Path.Combine(fileDir, fileName)))
            _apm.Send(Path.Combine(directory, fileName), fileName); //пересылаем файл в папку "архив"
            if (Path.GetExtension(fileName) == ".rar")
                apmImport.Delete(fileName);                         //удаляем в исходнике
            File.Delete(Path.Combine(directory, fileName));   //удаляем в раб папке
        }

        /// <summary>
        /// Создание документа "Счет"
        /// </summary>
        /// <param name="doc">Документ-основание "Заказ"</param>
        public void CreateBill(XmlDocument doc)
        {            
            using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
            {
                SqlCommandEx comm = new SqlCommandEx("USP_BILL_SAVE_FROM_ORDER", conn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@ID_MODIFIER", SqlDbType.UniqueIdentifier)).Value = Guid.Empty;//SecurityContextEx.USER_GUID;
                comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}
