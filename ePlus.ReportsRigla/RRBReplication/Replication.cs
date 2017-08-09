using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.IO;
using ePlus.MetaData.Server;
using System.Data.SqlClient;

namespace RRBReplication
{
    public partial class Replication : Form, IExternalReport, IReportParams
    {
        private string connectionString;
        private string folderPath;
        private const string CACHE_FOLDER = "Cache";
        public bool IsShowPreview
        {
            get { return true; }
        }

        public string HeaderText
        {
            get { return ReportName; }
        }
        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("RRBReplication.Replication.sql");

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
        private void ExtractReport()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            Stream s = this.GetType().Assembly.GetManifestResourceStream("RRBReplication.Replication.rdlc");
            StreamReader sr = new StreamReader(s);
            string rep = sr.ReadToEnd();
            string reportPath = Path.Combine(cachePath, "Replication.rdlc");
            using (StreamWriter sw = new StreamWriter(reportPath))
            {
                sw.Write(rep);
                sw.Flush();
                sw.Close();
            }
        }

        private void ClearCache()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (Directory.Exists(cachePath))
            {
                try
                {
                    Utils.ClearFolder(cachePath);
                    Directory.Delete(cachePath);
                }
                catch
                {

                }
            }
        }


        public void Print()
        {
            CreateStoredProc(connectionString);
            ExtractReport();
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            ReportFormNew rep = new ReportFormNew();
            rep.Text = "Репликация (Ригла)";
            rep.ReportPath = Path.Combine(cachePath, "Replication.rdlc");
            rep.LoadData("REPEX_REPLICATION", "");
            rep.BindDataSource("RCBReplication_DS_Table", 0);
            ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

            rep.ReportViewer.LocalReport.SetParameters(parameters);
            rep.ExecuteReport(this);
        }



        public string ReportName
        {
            get { return "Репликация (Ригла)"; }
        }


        public string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        public void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
            Print();
        }
    }
}