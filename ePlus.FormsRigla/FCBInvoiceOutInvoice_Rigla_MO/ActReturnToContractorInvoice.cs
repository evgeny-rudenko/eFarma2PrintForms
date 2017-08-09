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

namespace FCBInvoiceOutInvoice_Rigla
{
  public class ActReturnToContractorInvoice : AbstractDocumentReport, IExternalDocumentPrintForm
  {
    const string CACHE_FOLDER = "Cache";
    string connectionString;
    string folderPath;

    void CreateStoredProc(string connectionString)
    {
        Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBInvoiceOutInvoice_Rigla.ActReturnToContractorInvoice.sql");

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
    Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBInvoiceOutInvoice_Rigla.ActReturnToContractorInvoice.rdlc");
      using (StreamReader sr = new StreamReader(s))
      {
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnToContractorInvoice.rdlc")))
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
      rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnToContractorInvoice.rdlc");

      rep.LoadData("DBO.REPEX_INVOICE_OUT_INVOICE_RIGLA", doc.InnerXml);
      rep.BindDataSource("InvoiceOutInvoice_DS_Table0", 0);
      rep.BindDataSource("InvoiceOutInvoice_DS_Table1", 1);
      rep.BindDataSource("InvoiceOutInvoice_DS_Table2", 2);

      string number, DoverBuh, DoverDir, FIODir, FIOBuh;
        DoverBuh = DoverDir = FIODir = FIOBuh = "";
      /*
      using (InvoiceForm paramForm = new InvoiceForm())
      {
        number = paramForm.Number = Utils.GetString(dataRowItem.Row, "MNEMOCODE");

        if (paramForm.ShowDialog() == DialogResult.OK)
          number = paramForm.Number;
      }*/
      using (InvoiceForm paramForm = new InvoiceForm())
      {
        string num = Utils.GetString(dataRowItem.Row, "DOC_NUM");

        foreach (DataRow row in rep.DataSource.Tables[2].Rows)
        {
            paramForm.DoverBuh = DoverBuh = paramForm.DoverBuhContractor = string.IsNullOrEmpty(Utils.GetString(row, "BUH_DOC")) ? "" : Utils.GetString(row, "BUH_DOC");
            paramForm.DoverDir = DoverDir = paramForm.DoverDirContractor = string.IsNullOrEmpty(Utils.GetString(row, "DIRECTOR_DOC")) ? "" : Utils.GetString(row, "DIRECTOR_DOC");
            paramForm.FIODir = FIODir = paramForm.FIODirContractor = string.IsNullOrEmpty(Utils.GetString(row, "DIR")) ? "" : Utils.GetString(row, "DIR");
            paramForm.FIOBuh = FIOBuh = paramForm.FIOBuhContractor = string.IsNullOrEmpty(Utils.GetString(row, "BUH")) ? "" : Utils.GetString(row, "BUH");
        }

        number = paramForm.Number = !string.IsNullOrEmpty(num) ? num : Utils.GetString(dataRowItem.Row, "MNEMOCODE");

        if (paramForm.ShowDialog() == DialogResult.OK)
        {
            number = paramForm.Number;
            DoverBuh = paramForm.DoverBuh;
            DoverDir = paramForm.DoverDir;
            FIODir = paramForm.FIODir;
            FIOBuh = paramForm.FIOBuh;
        }
      }
        /*
      ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("INVOICE_NAME", number + " �� " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE").ToString("D")),
			};
      */
      List<ReportParameter> par = new List<ReportParameter>();
      par.Add(new ReportParameter("INVOICE_NAME", number + " �� " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE").ToString("D")));
      par.Add(new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName));

      par.Add(new ReportParameter("BUH_DOC", DoverBuh));
      par.Add(new ReportParameter("DIRECTOR_DOC", DoverDir));
      par.Add(new ReportParameter("DIR", FIODir));
      par.Add(new ReportParameter("BUH", FIOBuh));
      rep.ReportViewer.LocalReport.SetParameters(par);

      return rep;
    }

    public string PluginCode
    {
      get { return "INVOICE_OUT"; }
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
      get { return "����-������� (�����)"; }
    }
  }
}
