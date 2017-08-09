using System;
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

namespace F17MInvoiceOutPTORG12
{
	public class InvoiceOutPTORG12Ex : AbstractDocumentPrintForm, IExternalDocumentPrintForm
	{
		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F17MInvoiceOutPTORG12.TORG12.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F17MInvoiceOutPTORG12.InvoiceOutPTORG12Ex.rdlc");
            using (StreamReader sr = new StreamReader(s))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutPTORG12Ex.rdlc")))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE_OUT_GLOBAL", dataRowItem.Guid);

            ReportFormNew rep = new ReportFormNew();

            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutPTORG12Ex.rdlc");

            rep.LoadData("DBO.REPEX_INVOICE_OUT_TORG12", doc.InnerXml);
            rep.BindDataSource("InvoiceOutPTORG12Ex_DS_DataTable0", 0);
            rep.BindDataSource("InvoiceOutPTORG12Ex_DS_DataTable1", 1);
            rep.BindDataSource("InvoiceOutPTORG12Ex_DS_DataTable2", 2);
            decimal sum = 0;
            foreach (DataRow row in rep.DataSource.Tables[0].Rows)
            {
                sum += Utils.GetDecimal(row, "SUM_CONTRACTOR_PRICE_VAT");
            }
            string sumInText = ePlus.CommonEx.Reporting.RusCurrency.Str((double)sum);
            string countInText = ePlus.CommonEx.Reporting.RusCurrency.Str(rep.DataSource.Tables[0].Rows.Count, "NUM");
            ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("PAGE_COUNT", "0"),
                new ReportParameter("STR_SUMMORY", sumInText),
                new ReportParameter("COUNT_ROWS", countInText),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
            return rep;
		}

        public override string PluginCode
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

        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
			get { return "ТОРГ-12 (книжн.с подп.)"; }
		}
	}
}
