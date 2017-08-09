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
using System.Windows.Forms;

namespace InvoiceInvoice
{
  public class InvoiceInvoiceRigla : AbstractDocumentReport, IExternalDocumentPrintForm
  {
    const string CACHE_FOLDER = "Cache";
    string connectionString;
    string folderPath;

    void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceInvoice.InvoiceInvoiceRigla.sql");

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
      Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceInvoice.InvoiceInvoiceRigla.rdlc");
      using (StreamReader sr = new StreamReader(s))
      {
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceInvoiceRigla.rdlc")))
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
      rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceInvoiceRigla.rdlc");

      rep.LoadData("DBO.REPEX_INVOICE_INVOICE_RIGLA", doc.InnerXml);
      rep.BindDataSource("InvoiceInvoice_DS_Table0", 0);
      rep.BindDataSource("InvoiceInvoice_DS_Table1", 1);

      string number;
      using (InvoiceForm paramForm = new InvoiceForm())
      {
        string num = Utils.GetString(rep.DataSource.Tables[0].Rows[0], "INVOICE_NAME");

        number = paramForm.Number = !string.IsNullOrEmpty(num) ? num : Utils.GetString(dataRowItem.Row, "MNEMOCODE");

        if (paramForm.ShowDialog() == DialogResult.OK)
          number = paramForm.Number;
      }

      ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("INVOICE_NAME", number + " от " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_DATE").ToString("D")),
			};

      rep.ReportViewer.LocalReport.SetParameters(parameters);

      return rep;
    }

    public string PluginCode
    {
      get { return "INVOICE"; }
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
      get { return "Счет-фактура от получателя (Ригла)"; }
    }
  }
}
