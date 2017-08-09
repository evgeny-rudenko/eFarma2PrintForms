// Type: InvoiceOutBillExRyazan.InvoiceOutBillExRyazan
// Assembly: FCSInvoiceOutBillRyazan_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E955682C-D1E0-4105-9376-CCAC0C8967E2
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCSInvoiceOutBillRyazan_8.dll

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
using System.Windows.Forms;
using System.Xml;

namespace InvoiceOutBillExRyazan
{
  public class InvoiceOutBillExRyazan : AbstractDocumentReport, IExternalDocumentPrintForm, IExternalReport, IDocumentPrintForm
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

    public string ReportName
    {
      get
      {
        return "Счет (Рязань)";
      }
    }

    public string GroupName
    {
      get
      {
        return string.Empty;
      }
    }

    public InvoiceOutBillExRyazan()
    {
    }

    private void CreateStoredProc(string connectionString)
    {
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("InvoiceOutBillExRyazan.InvoiceOutBill.sql"), Encoding.GetEncoding(1251)))
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
      using (StreamReader streamReader = new StreamReader(((object) this).GetType().Assembly.GetManifestResourceStream("InvoiceOutBillExRyazan.InvoiceOutBillRyazan.rdlc")))
      {
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(str, "InvoiceOutBillRyazan.rdlc")))
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
      reportFormNew1.ReportFormName = (reportName = this.ReportName);
      string str1 = reportName;
      ((Control) reportFormNew2).Text = str1;
      reportFormNew1.ReportPath = (Path.Combine(Path.Combine(this.folderPath, "Cache"), "InvoiceOutBillRyazan.rdlc"));
      reportFormNew1.LoadData("REPEX_INVOICE_OUT_BILL", xmlDocument.InnerXml);
      reportFormNew1.BindDataSource("BillReportData_Table", 0);
      reportFormNew1.BindDataSource("BillReportData_Table1", 1);
      Decimal num1 = new Decimal(0);
      Decimal num2 = new Decimal(0);
      Decimal num3 = new Decimal(0);
      Decimal num4 = new Decimal(0);
      foreach (DataRow dataRow in (InternalDataCollectionBase) reportFormNew1.DataSource.Tables[1].Rows)
      {
        num1 += Utils.GetDecimal(dataRow, "RETAIL_SUMV");
        num2 += Utils.GetDecimal(dataRow, "VAT");
        num3 += Utils.GetDecimal(dataRow, "VAT_SUM10");
        num4 += Utils.GetDecimal(dataRow, "VAT_SUM18");
      }
      string str2 = " ";
      using (InvoiceForm invoiceForm = new InvoiceForm())
      {
        if (invoiceForm.ShowDialog() == DialogResult.OK)
          str2 = invoiceForm.Contract;
      }
      ReportParameter[] reportParameterArray = new ReportParameter[6]
      {
        new ReportParameter("SUMM", num1.ToString("N2")),
        new ReportParameter("VAT_SUMM", num2.ToString("N2")),
        new ReportParameter("SUMM_RUS", RusCurrency.Str((double) num1)),
        new ReportParameter("VAT_SUM10", num3.ToString("N2")),
        new ReportParameter("VAT_SUM18", num4.ToString("N2")),
        new ReportParameter("CONTRACT", str2)
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
