using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FCSMovement_Rigla
{
  public class Movement : AbstractDocumentReport, IExternalDocumentPrintForm, IExternalReport, IDocumentPrintForm
  {
    private const string CACHE_FOLDER = "Cache";
    private string connectionString;
    private string folderPath;

    public string PluginCode
    {
      get
      {
        return "Movement";
      }
    }

    public string ReportName
    {
      get
      {
        return "Требование-накладная";
      }
    }

    public string GroupName
    {
      get
      {
        return string.Empty;
      }
    }

    public Movement()
    {
    }

    private void CreateStoredProc(string connectionString)
    {
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCSMovement_Rigla.Movement_Rigla.sql"), Encoding.GetEncoding(1251)))
      {
        string[] strArray = Regex.Split(streamReader.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);
        foreach (string cmdText in strArray)
        {
          if (!(cmdText == string.Empty))
          {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
              connection.Open();
              sqlCommand.ExecuteNonQuery();
            }
          }
        }
      }
    }

    private void ExtractReport()
    {
      string str = Path.Combine(this.folderPath, "Cache");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCSMovement_Rigla.Movement_Rigla.rdlc")))
      {
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(str, "Movement_Rigla.rdlc")))
          streamWriter.Write(streamReader.ReadToEnd());
      }
    }

    public override IReportForm GetReportForm(DataRowItem dataRowItem)
    {
      XmlDocument xmlDocument = new XmlDocument();
      Utils.AddNode(Utils.AddNode((XmlNode) xmlDocument, "XML"), "ID_GLOBAL", dataRowItem.Guid);
      ReportFormNew reportForm = new ReportFormNew();
      reportForm.ReportFormName = this.ReportName;
      reportForm.Text = this.ReportName;
      reportForm.ReportPath = Path.Combine(Path.Combine(this.folderPath, "Cache"), "Movement_Rigla.rdlc");
      reportForm.LoadData("REPEX_MOVEMENT_RIGLA", xmlDocument.InnerXml);
      reportForm.BindDataSource("Movement_DS_Table0", 0);
      reportForm.BindDataSource("Movement_DS_Table1", 1);
      reportForm.BindDataSource("Movement_DS_Table2", 2);
      reportForm.BindDataSource("Movement_DS_Table3", 3);
      Decimal num = new Decimal(0);
      foreach (DataRow dataRow in (InternalDataCollectionBase) reportForm.DataSource.Tables[1].Rows)
        num += Utils.GetDecimal(dataRow, "SUM_SAL");
      string str2 = string.Format("{0:N2}", (object) num);
      string str3 = str2.Remove(str2.Length - 3, 3);
      string str4 = string.Format("{0:N2}", (object) num).Remove(0, str3.Length + 1);
      ReportParameter[] reportParameterArray = new ReportParameter[1]
      {
        new ReportParameter("sum", string.Format("{0} руб {1} коп ({2})", (object) str3, (object) str4, (object) RusCurrency.Str((double) num)))
      };
      reportForm.ReportViewer.LocalReport.SetParameters((IEnumerable<ReportParameter>) reportParameterArray);
      return (IReportForm) reportForm;
    }

    public void Execute(string connectionString, string folderPath)
    {
      this.connectionString = connectionString;
      this.folderPath = folderPath;
      this.CreateStoredProc(this.connectionString);
      this.ExtractReport();
    }
  }
}
