using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;

namespace FCBActDeductionTorg16_Rigla
{
  public class ActDeductionTorg16_Rigla : AbstractDocumentReport, IExternalDocumentPrintForm
  {
    private const string CACHE_FOLDER = "Cache";
    string connectionString;
    string folderPath;

    void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBActDeductionTorg16_Rigla.ActDeductionTorg16_Rigla.sql");

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
      Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBActDeductionTorg16_Rigla.ActDeductionTorg16_Rigla.rdlc");
      using (StreamReader sr = new StreamReader(s))
      {
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActDeductionTorg16_Rigla.rdlc")))
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
      rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActDeductionTorg16_Rigla.rdlc");

      rep.LoadData("REPEX_ACT_DEDUCTION_TORG_16_RIGLA", doc.InnerXml);
      rep.BindDataSource("ActDeductionTorg16_Rigla_DS_Table0", 0);
      rep.BindDataSource("ActDeductionTorg16_Rigla_DS_Table1", 1);
      rep.BindDataSource("ActDeductionTorg16_Rigla_DS_Table2", 2);
      rep.BindDataSource("ActDeductionTorg16_Rigla_DS_Table3", 3);

      decimal sum = 0m;
      string xml = rep.DataSource.GetXml();

      foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
      {
        sum += Utils.GetDecimal(Row, "SUM_SAL");
      }

      ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("sumInText", RusCurrency.Str((double) sum)),
                new ReportParameter("VER_DLL",System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
      
      rep.ReportViewer.LocalReport.SetParameters(parameters);

      return rep;
    }


    public void Execute(string connectionString, string folderPath)
    {
      this.connectionString = connectionString;
      this.folderPath = folderPath;
      CreateStoredProc(this.connectionString);
      ExtractReport();
    }

    public string PluginCode
    {
      get { return "ACT_DEDUCTION"; }
    }

    public string ReportName
    {
      get { return "����-16 (�����)"; }
    }

    public string GroupName
    {
      get { return string.Empty; }
    }
  }
}
