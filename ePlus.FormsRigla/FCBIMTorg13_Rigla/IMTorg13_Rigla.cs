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

namespace FCBIMTorg13_Rigla
{
  public class IMTorg13_Rigla : AbstractDocumentReport, IExternalDocumentPrintForm
  {
    const string CACHE_FOLDER = "Cache";
    string connectionString;
    string folderPath;

    void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBIMTorg13_Rigla.IMTorg13_Rigla.sql");

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
      Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBIMTorg13_Rigla.IMTorg13_Rigla.rdlc");
      using (StreamReader sr = new StreamReader(s))
      {
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "IMTorg13_Rigla.rdlc")))
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

      rep.Text = rep.ReportFormName = ReportName;
      rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "IMTorg13_Rigla.rdlc");

      rep.LoadData("REPEX_IM_TORG_13_RIGLA", doc.InnerXml);
      rep.BindDataSource("IMTorg13_Rigla_DS_Table0", 0);
      rep.BindDataSource("IMTorg13_Rigla_DS_Table1", 1);

      decimal sum = 0.0M;

      DataSet dataSet = rep.DataSource;
      for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
      {
        sum += Utils.GetDecimal(dataSet.Tables[1].Rows[i]["SUM_SUP"]);
      }

      ReportParameter[] parameters = new ReportParameter[1] 
			{
				new ReportParameter("sum", RusCurrency.Str((double) sum))
			};

      rep.ReportViewer.LocalReport.SetParameters(parameters);

      return rep;
    }

    public string PluginCode
    {
      get { return "INTERFIRM_MOVING"; }
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
      get { return "����-13 (�����)"; }
    }
  }
}
