using System;
using System.Collections.Generic;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace FCSInventoryOpisEx_Rigla
{
    public class InventoryOpisEx : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
    {
        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSInventoryOpisEx_Rigla.INVENTORY_OPIS_EX.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSInventoryOpisEx_Rigla.InventoryOpisEx.rdlc");
            using (StreamReader sr = new StreamReader(s))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InventoryOpisEx.rdlc")))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVENTORY_GLOBAL", dataRowItem.Guid);
            ReportFormNew rep = new ReportFormNew();
            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InventoryOpisEx.rdlc");
            rep.LoadData("REPEX_INVENTORY_OPIS_EX", doc.InnerXml);
            rep.BindDataSource("InventoryOpis_DS_Table0", 0);
            rep.BindDataSource("InventoryOpis_DS_Table1", 1);
            ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
            return rep;
        }

        public override string PluginCode
        {
            get { return "INVENTORY_SVED"; }
        }

        public void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
            ExtractReport();
        }

        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string ReportName
        {
            get { return "»Õ¬-3 (–Ë„Î‡)"; }
        }
      }
}
