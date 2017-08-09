using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace FCSInvoiceOutCertListRyazan
{
  public class InvoiceOutCertListExRyazan : AbstractDocumentReport, IExternalDocumentPrintForm, IExternalReport, IDocumentPrintForm
  {
    private const string CACHE_FOLDER = "Cache";
    private string connectionString;
    private string folderPath;

    public string PluginCode
    {
      get
      {
        return "INVOICE_OUT";
      }
    }

    public string GroupName
    {
      get
      {
        return string.Empty;
      }
    }

    public string ReportName
    {
      get
      {
        return "Реестр сертификатов (Рязань)";
      }
    }

    public InvoiceOutCertListExRyazan()
    {
    }

    private void CreateStoredProc(string connectionString)
    {
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCSInvoiceOutCertListRyazan.InvoiceOutCertList.sql"), Encoding.GetEncoding(1251)))
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
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("FCSInvoiceOutCertListRyazan.InvoiceOutCertListRyazan.rdlc")))
      {
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(str, "InvoiceOutCertListRyazan.rdlc")))
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
      string str = reportName;
      ((Control) reportFormNew2).Text = str;
      reportFormNew1.ReportPath = Path.Combine(Path.Combine(this.folderPath, "Cache"), "InvoiceOutCertListRyazan.rdlc");
      reportFormNew1.LoadData("REPEX_INVOICE_OUT_CERT_LIST", xmlDocument.InnerXml);
      reportFormNew1.BindDataSource("CertList_DS_Table0", 0);
      reportFormNew1.BindDataSource("CertList_DS_Table1", 1);
      reportFormNew1.BindDataSource("CertList_DS_Table2", 2);
      ReportParameter[] reportParameterArray = new ReportParameter[1]
      {
        new ReportParameter("VER_DLL", Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
      };
      reportFormNew1.ReportViewer.LocalReport.SetParameters((IEnumerable<ReportParameter>) reportParameterArray);
      return (IReportForm) reportFormNew1;
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
