// Type: FCChInvoiceOutInvoice_Rigla.InvoiceOutInvoice
// Assembly: FCChInvoiceOutInvoice_Rigla_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 508D15D3-CFC0-430C-86B9-F207DB5E1844
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCChInvoiceOutInvoice_Rigla_8.dll

using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace FCChInvoiceOutInvoice_Rigla
{
  public class InvoiceOutInvoice : AbstractDocumentReport, IExternalDocumentPrintForm, IExternalReport, IDocumentPrintForm
  {
    private const string CACHE_FOLDER = "Cache";
    private string connectionString;
    private string folderPath;

    public string GroupName
    {
      get
      {
        return string.Empty;
      }
    }

    public string PluginCode
    {
      get
      {
        return "INVOICE_OUT";
      }
    }

    public string ReportName
    {
      get
      {
        return "Счет фактура (Ригла) (по пст. 1137)";
      }
    }

    public InvoiceOutInvoice()
    {
    }

    private void CreateStoredProc(string connectionString)
    {
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCChInvoiceOutInvoice_Rigla.InvoiceOutInvoice.sql"), Encoding.GetEncoding(1251)))
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

    public void Execute(string connectionString, string folderPath)
    {
      this.connectionString = connectionString;
      this.folderPath = folderPath;
      this.CreateStoredProc(this.connectionString);
      this.ExtractReport();
    }

    private void ExtractReport()
    {
      string str = Path.Combine(this.folderPath, "Cache");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCChInvoiceOutInvoice_Rigla.InvoiceOutInvoice.rdlc")))
      {
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(str, "InvoiceOutInvoice.rdlc")))
          streamWriter.Write(streamReader.ReadToEnd());
      }
    }

    public override IReportForm GetReportForm(DataRowItem dataRowItem)
    {
      XmlDocument xmlDocument = new XmlDocument();
      Utils.AddNode(Utils.AddNode((XmlNode) xmlDocument, "XML"), "ID_GLOBAL", dataRowItem.Guid);
      ReportFormNew reportFormNew1 = new ReportFormNew();
      ReportFormNew reportFormNew2 = reportFormNew1;
      string reportName;
      reportFormNew1.ReportFormName = reportName = this.ReportName;
      string str1 = reportName;
      ((Control) reportFormNew2).Text = str1;
      reportFormNew1.ReportPath = Path.Combine(Path.Combine(this.folderPath, "Cache"), "InvoiceOutInvoice.rdlc");
      reportFormNew1.LoadData("DBO.REPEX_INVOICE_OUT_INVOICE_RIGLA", xmlDocument.InnerXml);
      reportFormNew1.BindDataSource("InvoiceOutInvoice_DS_Table0", 0);
      reportFormNew1.BindDataSource("InvoiceOutInvoice_DS_Table1", 1);
      reportFormNew1.BindDataSource("InvoiceOutInvoice_DS_Table2", 2);
      string str2;
      string str3 = str2 = "";
      string str4 = str2;
      string str5 = str2;
      string str6 = str2;
      string str7;
      using (InvoiceForm invoiceForm = new InvoiceForm())
      {
        string @string = Utils.GetString(dataRowItem.Row, "DOC_NUM");
        foreach (DataRow dataRow in (InternalDataCollectionBase) reportFormNew1.DataSource.Tables[2].Rows)
        {
          invoiceForm.DoverBuh = str6 = Utils.GetString(dataRow, "BUH_DOC");
          invoiceForm.DoverDir = str5 = Utils.GetString(dataRow, "DIRECTOR_DOC");
          invoiceForm.FIODir = str4 = Utils.GetString(dataRow, "DIR");
          invoiceForm.FIOBuh = str3 = Utils.GetString(dataRow, "BUH");
        }
        str7 = invoiceForm.Number = !string.IsNullOrEmpty(@string) ? @string : Utils.GetString(dataRowItem.Row, "MNEMOCODE");
        if (invoiceForm.ShowDialog() == DialogResult.OK)
        {
          str7 = invoiceForm.Number;
          str6 = invoiceForm.DoverBuh;
          str5 = invoiceForm.DoverDir;
          str4 = invoiceForm.FIODir;
          str3 = invoiceForm.FIOBuh;
        }
      }
      DateTime date = Utils.GetDate(reportFormNew1.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE");
      string str8 = date.ToString("D");
      List<ReportParameter> list = new List<ReportParameter>();
      list.Add(new ReportParameter("INVOICE_NAME", str7));
      int day = date.Day;
      list.Add(new ReportParameter("INVOICE_DATE_DAY", day.ToString()));
      list.Add(new ReportParameter("INVOICE_DATE", str8.Substring(str8.IndexOf(" "), str8.Length - str8.IndexOf(" "))));
      list.Add(new ReportParameter("VER_DLL", Assembly.GetExecutingAssembly().ManifestModule.ScopeName));
      list.Add(new ReportParameter("BUH_DOC", str6));
      list.Add(new ReportParameter("DIRECTOR_DOC", str5));
      list.Add(new ReportParameter("DIR", str4));
      list.Add(new ReportParameter("BUH", str3));
      reportFormNew1.ReportViewer.LocalReport.SetParameters((IEnumerable<ReportParameter>) list);
      return (IReportForm) reportFormNew1;
    }
  }
}
