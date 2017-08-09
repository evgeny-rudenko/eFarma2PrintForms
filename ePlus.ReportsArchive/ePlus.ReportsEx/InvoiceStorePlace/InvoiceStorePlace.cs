using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
//using ePlus.MetaData.Client.Reports;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using Microsoft.Reporting.WinForms;

namespace InvoiceStorePlace
{
    /// <summary>
    /// Summary description for Invoce.
    /// </summary>
    public class RepInvoice : AbstractDocumentReport, IExternalDocumentPrintForm
    {
        private const string CACHE_FOLDER = "Cache";
        string connectionString;
        string folderPath;

        private void CreateStoredProc(string connectionString)
        {
          Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceStorePlace.InvoiceStorePlace.sql");
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
            string procScript = sr.ReadToEnd();
            string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);

            SqlCommand comm = null;
            foreach (string statement in batch)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    comm = new SqlCommand(statement, con);
                    con.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        private void ExtractReport()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
              Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceStorePlace.InvoiceStorePlace.rdlc");
            StreamReader sr = new StreamReader(s);
            string rep = sr.ReadToEnd();
            string reportPath = Path.Combine(cachePath, "InvoiceStorePlace.rdlc");
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

        public override IReportForm GetReportForm(DataRowItem dataRowItem)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVOICE", dataRowItem.Id);

            ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;

            rep.LoadData("REPEX_INVOICE_STORE_PLACE", doc.InnerXml);
            rep.BindDataSource("Invoice_Table", 0);
            rep.BindDataSource("Invoice_Table1", 1);
            rep.BindDataSource("Invoice_Table2", 2);

            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            // подсчет общей суммы
            decimal dblSummory = 0;
            decimal retailSummory = 0;
            foreach (DataRow Row in rep.DataSource.Tables[2].Rows)
            {
                dblSummory += Utils.GetDecimal(Row, "CONTRACTOR_SUM_VAT");
                retailSummory += Utils.GetDecimal(Row, "SUM_RETAIL_SUM");
            }
            dblSummory = Utils.Round(dblSummory, 2);
            retailSummory = Utils.Round(retailSummory, 2);
            // преобразование суммы в строку
            string strSummory = RusCurrency.Str((double)dblSummory);
            string strRetailSummory = RusCurrency.Str((double)retailSummory);

            string file = Path.Combine(cachePath, "InvoiceStorePlace.rdlc");
            rep.ReportViewer.LocalReport.ReportPath = file;

            ReportParameter p1 = new ReportParameter("SUMMORY", strSummory);
            ReportParameter p2 = new ReportParameter("RETAIL_SUMMORY", strRetailSummory);

            rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

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

        public string ReportName
        {
            get { return "Приходные документы: Накладная с местами хранения"; }
        }

        public string GroupName
        {
            get { return string.Empty; }
        }
    }
}
