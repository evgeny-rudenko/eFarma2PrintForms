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
using ePlus.MetaData.ExternReport;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace RRBReplicationCO
{
    public partial class ReplicationCO : Form, IExternalReport, IReportParams
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
        Stream s = this.GetType().Assembly.GetManifestResourceStream("RRBReplicationCO.ReplicationCO.sql");

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
        Stream s = this.GetType().Assembly.GetManifestResourceStream("RRBReplicationCO.ReplicationCO.rdlc");
        StreamReader sr = new StreamReader(s);
        string rep = sr.ReadToEnd();
        string reportPath = Path.Combine(cachePath, "ReplicationCO.rdlc");
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


    public  void Print()
    {
        CreateStoredProc(connectionString);
        ExtractReport();
        string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
        ReportFormNew rep = new ReportFormNew();
        rep.Text = ReportName;
        rep.ReportPath = Path.Combine(cachePath, "ReplicationCO.rdlc");
        rep.LoadData("REPEX_REPLICATION_CO", "");
        rep.BindDataSource("RCBReplication_DS_Table", 0);
        ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
        rep.ReportViewer.LocalReport.SetParameters(parameters);
        rep.ExecuteReport(this);
    }



    public string ReportName
    {
      get { return "Репликация-центр (Ригла)"; }
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


