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

namespace CalcMinGoods
{
  public class CalcMinGoods : AbstractDocumentReport, IExternalDocumentPrintForm
  {
    public const string CACHE_FOLDER = "Cache";
    string connectionString;
    public string folderPath;

    void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("CalcMinGoods.CalcMinGoods.sql");

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
      Stream s = this.GetType().Assembly.GetManifestResourceStream("CalcMinGoods.CalcMinGoods.rdlc");
      using (StreamReader sr = new StreamReader(s))
      {
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "CalcMinGoods.rdlc")))
        {
          sw.Write(sr.ReadToEnd());
        }
      }
    }

    public override IReportForm GetReportForm(DataRowItem dataRowItem)
    {/*
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "CalcMinGoods.rdlc");

			rep.LoadData("CALC_G", doc.InnerXml);
			rep.BindDataSource("CalcMinGoods_DS_Table1", 0);
			*/

      using (CalcMinGoodsQtyForm form = new CalcMinGoodsQtyForm(this))
      {
        form.ShowDialog();
      }

      return null;
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
      get { return "DEFECTURA"; }
    }

    public string ReportName
    {
      get { return "Расчёт минимального остатка"; }
    }

    public string GroupName
    {
      get { return string.Empty; }
    }

  }
}
