namespace ePlus.PriceList.ImportZakaz
{
    using ePlus.CommonEx.AccessPoint;
    using ePlus.CommonEx.DataAccess;
    using ePlus.MetaData.Core;
    using ePlus.MetaData.Server;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class ORDERS_BL
    {
        private DataService_BL bl = new DataService_BL();
        private CONFIGURATION_IMPORT confImport = new CONFIGURATION_IMPORT();
        private List<XmlDocument> docs = new List<XmlDocument>();
        private string fileDir = Utils.TempDir();

        public void CreateBill(XmlDocument doc)
        {
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                SqlCommandEx ex = new SqlCommandEx("USP_BILL_SAVE_FROM_ORDER", connection);
                ex.CommandType = CommandType.StoredProcedure;
                ex.Parameters.Add(new SqlParameter("@ID_MODIFIER", SqlDbType.UniqueIdentifier)).Value = Guid.Empty;
                ex.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                connection.Open();
                ex.ExecuteNonQuery();
            }
        }

        public Dictionary<XmlDocument, string> Load(AccessPointManager apm)
        {
            List<string> files = apm.GetFiles("*.xml");
            if (files.Count == 0)
            {
                return null;
            }
            List<string> folderPaths = new List<string>();
            files.ForEach(delegate (string _file) {
                folderPaths.Add(Path.GetFileName(_file));
            });
            Dictionary<XmlDocument, string> dictionary = new Dictionary<XmlDocument, string>();
            foreach (string str in folderPaths)
            {
                apm.Receive(str, Path.Combine(this.fileDir, str));
                XmlDocument key = new XmlDocument();
                key.Load(Path.Combine(this.fileDir, str));
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, str);
                }
            }
            return dictionary;
        }

        public Dictionary<XmlDocument, string> LoadCtlFile(AccessPointManager apm)
        {
            List<string> files = apm.GetFiles("*.rar");
            List<string> rarFiles = new List<string>();
            files.ForEach(delegate (string _file) {
                rarFiles.Add(Path.GetFileName(_file));
            });
            string path = Path.Combine(this.fileDir, "CTL");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string str2 in rarFiles)
            {
                apm.Receive(str2, Path.Combine(this.fileDir, str2));
                string str3 = Path.Combine(path, Path.GetFileName(str2));
                if (File.Exists(str3))
                {
                    File.Delete(str3);
                }
                RarUtils.Extract(Path.Combine(this.fileDir, str2), Path.GetDirectoryName(str3), string.Empty);
                File.Delete(Path.Combine(this.fileDir, str2));
            }
            List<string> list2 = new List<string>(Directory.GetFiles(path));
            list2.RemoveAll(delegate (string _file) {
                return Path.GetExtension(_file) != ".ctl";
            });
            if (list2.Count == 0)
            {
                return null;
            }
            List<string> folderPaths = new List<string>();
            list2.ForEach(delegate (string _file) {
                folderPaths.Add(Path.GetFileName(_file));
            });
            Dictionary<XmlDocument, string> dictionary = new Dictionary<XmlDocument, string>();
            foreach (string str4 in folderPaths)
            {
                XmlDocument root = new XmlDocument();
                XmlNode node = Utils.AddNode(root, "XML", (string) null);
                using (StreamReader reader = new StreamReader(Path.Combine(path, str4), Encoding.GetEncoding(0x4e3)))
                {
                    CtlTable table = new CtlTable();
                    table.ReadTableStruct(reader);
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        table.LoadRow(line);
                        XmlNode node2 = Utils.AddNode(node, "ITEM", (string) null);
                        foreach (CtlField field in table.FieldsList)
                        {
                            if (!string.IsNullOrEmpty(field.Value))
                            {
                                Utils.AddNodeEx(node2, field.FieldName, field.Value);
                            }
                        }
                    }
                }
                if (!dictionary.ContainsKey(root))
                {
                    dictionary.Add(root, str4);
                }
            }
            return dictionary;
        }

        public void OrderMoveToArchiv(AccessPointManager _apm, AccessPointManager apmImport, string fileName)
        {
            Predicate<string> match = null;
            string fileDir = this.fileDir;
            if (Path.GetExtension(fileName) == ".ctl")
            {
                fileDir = Path.Combine(this.fileDir, "CTL");
                List<string> files = apmImport.GetFiles("*.rar");
                if (match == null)
                {
                    match = delegate (string s) {
                        return Path.GetFileNameWithoutExtension(s) == Path.GetFileNameWithoutExtension(fileName);
                    };
                }
                foreach (string str2 in files.FindAll(match))
                {
                    apmImport.Delete(Path.GetFileName(str2));
                }
            }
            if (Path.GetExtension(fileName) == ".xls")
            {
                fileDir = Path.GetDirectoryName(fileName);
                fileName = Path.GetFileName(fileName);
            }
            _apm.Send(Path.Combine(fileDir, fileName), fileName);
            if (Path.GetExtension(fileName) == ".rar")
            {
                apmImport.Delete(fileName);
            }
            File.Delete(Path.Combine(fileDir, fileName));
        }

        public void Save(string save_state, XmlDocument doc, long id_configuration_import)
        {
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                SqlCommandEx ex = new SqlCommandEx("USP_ORDERS_SAVE", connection);
                ex.CommandType = CommandType.StoredProcedure;
                ex.Parameters.Add(new SqlParameter("@STATE_DOC", SqlDbType.VarChar)).Value = save_state;
                ex.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                ex.Parameters.Add(new SqlParameter("@ID_CONFIGURATION_IMPORT", SqlDbType.BigInt)).Value = id_configuration_import;
                connection.Open();
                ex.ExecuteNonQuery();
            }
        }

        public string ValidateOrder(XmlDocument doc, string import_key)
        {
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                SqlCommandEx ex = new SqlCommandEx("USP_ORDERS_LOAD_VALIDATE", connection);
                ex.CommandType = CommandType.StoredProcedure;
                ex.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
                ex.Parameters.Add(new SqlParameter("@KEY", SqlDbType.VarChar)).Value = import_key;
                connection.Open();
                return (string) ex.ExecuteScalar();
            }
        }
    }
}

