using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace InvoiceOutProtocolKONSUL_ROZ
{
    public class InvoiceOutProtocolKONSUL_ROZ : AbstractDocumentReport, IExternalDocumentPrintForm
    {
        const string CACHE_FOLDER = "Cache";
        string connectionString;
        string folderPath;

        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutProtocolKONSUL_ROZ.InvoiceOutProtocolKONS_ROZ.sql");

            using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
            {
                string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

                SqlCommand comm = null;
                foreach (string statement in batch)
                {
                    if (statement == string.Empty)
                        continue;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        comm = new SqlCommand(statement, con);
                        con.Open();
                        comm.ExecuteNonQuery();
                    }
                }
            }
        }

        void ExtractReport()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutProtocolKONSUL_ROZ.InvoiceOutProtocolKONSUL_ROZ.rdlc");
            using (StreamReader sr = new StreamReader(s))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutProtocolKONSUL_ROZ.rdlc")))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }

        public override IReportForm GetReportForm(DataRowItem dataRowItem)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            ReportFormNew rep = new ReportFormNew();

            rep.LoadData("INVOICE_OUT_PROTOCOL_KONS_ROZ", doc.InnerXml);
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutProtocolKONSUL_ROZ.rdlc");
            rep.BindDataSource("InvoiceOutProtocolKR_DS_Table0", 0);
            rep.BindDataSource("InvoiceOutProtocolKR_DS_Table1", 1);
            rep.ReportFormName = "Расходная накладная: Розница.Протокол согласования цен";
            return rep;
        }

        public string PluginCode
        {
            get { return "INVOICE_OUT"; }
        }

        public void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
            ExtractReport();
        }

        public string GroupName
        {
            get { return string.Empty; }
        }

        public string ReportName
        {
            get { return "Расходная накладная: Розница.Протокол согласования цен"; }
        }
    }
}
