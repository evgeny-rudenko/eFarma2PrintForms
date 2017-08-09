using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace ExportInvoiceOutToXml
{
    public class ExportForm : AbstractDocumentPrintForm,IExternalDocumentPrintFormParams  
    {
        //private string accessPointMnemocode;
        private bool isPriceChange = false;
        private AccessPointManager apm;
        private string fileName = Path.Combine(Utils.TempDir(), "ExportInvoiceOutRiglaSettings.xml");

        public string AccessPointMnemocode
        {
            get
            {
                if (apm != null)
                    return apm.AccessPoint.MNEMOCODE;
                return null;                        
            }
            set
            {                
                try
                {                    
                   apm = new AccessPointManager(value);
                }
                catch
                {
                    apm = null;
                }
            }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            //загружаем данные в объекты
            DataService_BL bl = new DataService_BL();
            using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
            {
                conn.Open();
                //Махинации с документом
                if (isPriceChange)
                    DocAction(dataRowItem.Guid, conn);

                //Выгрзука
                using (SqlCommandEx comm = new SqlCommandEx("USP_EXPORT_INVOICE_OUT_2_XML_REP", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@ID_GLOBAL", SqlDbType.UniqueIdentifier)).Value = dataRowItem.Guid;
                    SqlDataReader dr = comm.SqlCommand.ExecuteReader();
                    DOC doc = new DOC();                    
                    doc.Header = new HEADER();
                    while (dr.Read())
                    {                        
                        doc.FromReader(dr);
                    }
                    if (dr.NextResult())
                        while (dr.Read())
                        {
                            doc.Header.FromReader(dr);
                        }

                    List<ITEMS> items = new List<ITEMS>();
                    if (dr.NextResult())
                        while (dr.Read())
                        {
                            ITEMS item = new ITEMS();
                            item.FromReader(dr);
                            items.Add(item);
                        }

                    List<CERT> certificates = new List<CERT>();
                    if (dr.NextResult())
                        while (dr.Read())
                        {
                            CERT cert = new CERT();
                            cert.FromReader(dr);
                            certificates.Add(cert);
                        }
                    foreach (ITEMS _item in items)
                    {
                        _item.CERT.AddRange(certificates.FindAll(delegate(CERT _cert) { return _cert.TID == _item.TID; }));
                    }
                    doc.Header.ITEMS.AddRange(items);
                    //выгружаем файл на указанную точку доступа или в папку по умолчанию(temp)
                    ToXml(doc);
                    dr.Close();

                }
            }
                                              
            return null;            
        }

        private void DocAction(Guid guid, SqlConnection conn)
        {
            SqlCommandEx comm =
                new SqlCommandEx(
                    string.Format("select state from invoice_out where id_invoice_out_global = '{0}'", guid), conn);
            comm.CommandType = CommandType.Text;
            string state = (string)comm.ExecuteScalar();    
        
            if (state.Trim()=="PROC")
            {
                comm = new SqlCommandEx("USP_INVOICE_OUT_UNPROC",conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@ID_USER_MODIFIED", SqlDbType.UniqueIdentifier)).Value =
                    SecurityContextEx.Context.User.Id_user_global;
                comm.Parameters.Add(new SqlParameter("@ID_INVOICE_OUT_GLOBAL", SqlDbType.UniqueIdentifier)).Value = guid;
                comm.ExecuteNonQuery();
            }

            comm = new SqlCommandEx("USP_INVOICE_OUT_SAVE_4_RIGLA_LOADER", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID_USER_MODIFIED", SqlDbType.UniqueIdentifier)).Value =
                SecurityContextEx.Context.User.Id_user_global;
            comm.Parameters.Add(new SqlParameter("@ID_INVOICE_OUT_GLOBAL", SqlDbType.UniqueIdentifier)).Value = guid;
            comm.ExecuteNonQuery();

            if (state.Trim()=="PROC")
            {
                comm = new SqlCommandEx("USP_INVOICE_OUT_PROC", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@ID_USER_MODIFIED", SqlDbType.UniqueIdentifier)).Value =
                    SecurityContextEx.Context.User.Id_user_global;
                comm.Parameters.Add(new SqlParameter("@ID_INVOICE_OUT_GLOBAL", SqlDbType.UniqueIdentifier)).Value = guid;
                comm.ExecuteNonQuery();                
            }
        }

        /// <summary>
        /// Выгружаем данные в xml файл
        /// </summary>
        /// <param name="_doc">Выгружаемая структура</param>
        private void ToXml(DOC _doc)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = Utils.AddNode(doc, "XML");
            XmlNode invoice = Utils.AddNode(node, "INVOICE");

            PropertyInfo[] propInfo = typeof (HEADER).GetProperties();
            foreach (PropertyInfo info in propInfo)
            {
                Attribute a = Attribute.GetCustomAttribute(info, typeof (ToXml)); 
                if (a!=null)
                    Utils.AddNode(invoice, info.Name, Utils.GetString(info.GetValue(_doc.Header, null)));
            }

            XmlNode itemsXml = Utils.AddNode(invoice, "ITEMS");
            propInfo = typeof(ITEMS).GetProperties();
            foreach (ITEMS item in _doc.Header.ITEMS)
            {
                XmlNode itemXml = Utils.AddNode(itemsXml, "ITEM");
                foreach (PropertyInfo info in propInfo)
                {
                    Attribute a = Attribute.GetCustomAttribute(info, typeof(ToXml));
                    if (a != null)
                        Utils.AddNode(itemXml, info.Name, Utils.GetString(info.GetValue(item, null)));
                }

                XmlNode certsXml = Utils.AddNode(itemXml, "CERTIFICATES");
                PropertyInfo[] propInfo1 = typeof(CERT).GetProperties();
                foreach (CERT cert in item.CERT)
                {
                    XmlNode certXml = Utils.AddNode(certsXml, "CERTIFICATE");
                    foreach (PropertyInfo info in propInfo1)
                    {
                        Attribute a = Attribute.GetCustomAttribute(info, typeof(ToXml));
                        if (a != null)
                            Utils.AddNode(certXml, info.Name, Utils.GetString(info.GetValue(cert, null)));
                    }
                }
            }

            string fileName = string.Format("{0}.xml", _doc.DOC_NAME.Replace('/', '_'));
            string fullName = Path.Combine(Utils.TempDir(), fileName);
            doc.Save(fullName);
            apm.Send(fullName, Path.GetFileName(fullName));
        }

        public override string PluginCode
        {
            get { return "INVOICE_OUT"; }
        }

        public override string ReportName
        {
            get { return "Экспорт РН в формате xml"; }
        }

        public override string GroupName
        {
            get { return string.Empty; }
        }

        public bool Validate()
        {
            if (MessageBox.Show(@"Внимание, сейчас розничные цены накладных будут приравнены оптовым. Продолжить?",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                isPriceChange = true;               
            return true;
        }

        public void AfterPrint()
        {
            //Сохранение параметров
            XmlDocument _doc = new XmlDocument();
            XmlNode node = Utils.AddNode(_doc, "XML");
            Utils.AddNode(node, "APM", apm.AccessPoint.MNEMOCODE);
            _doc.Save(fileName);

            //сообщение об окончании и куда вывалили)));
            if (string.IsNullOrEmpty(AccessPointMnemocode))
                MessageBoxEx.Show(string.Format("Выбранные документы были выгружены в {0}", Utils.TempDir()));
            else MessageBoxEx.Show(string.Format("Выбранные документы были выгружены в {0}", apm.AccessPoint));                
        }

      public void Prepare()
      {
        if (!File.Exists(fileName)) return;
        XmlDocument doc = new XmlDocument();
        doc.Load(fileName);

        XmlNode root = doc.SelectSingleNode("/XML");
        AccessPointMnemocode = Utils.GetString(root, "APM");
      }


      public IExternalDocumentPrintFormParamsControl Control
        {            
            get { return new AccessPointUserControl(); }
        }

        public bool HasPrintForm
        {
            get { return false; }
        }
    }
}
