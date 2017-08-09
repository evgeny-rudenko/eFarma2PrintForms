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
using System.Reflection;

namespace F22MInvoiceOL
{
	public class InvoiceEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F22MInvoiceOL.Invoice.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F22MInvoiceOL.Invoice.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "Invoice.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE", dataRowItem.Id);
			ReportFormNew rep = new ReportFormNew();
			rep.Text = ReportName;
			rep.LoadData("REPEX_INVOICE_OL", doc.InnerXml);
			rep.BindDataSource("Invoice_Table0", 0);
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
			string strSummory = RusCurrency.Str((double) dblSummory);
			string strRetailSummory = RusCurrency.Str((double) retailSummory);

			string file = Path.Combine(cachePath, "Invoice.rdlc");
			rep.ReportViewer.LocalReport.ReportPath = file;

            AddParameterReportName(rep);
			rep.ReportFormName = "Приходная накладная (Омское лекарство)";
			return rep;
		}

        private void AddParameterReportName(ReportFormNew rep)
        {
            string Ver = Assembly.GetExecutingAssembly().FullName;

            int Pos = Ver.IndexOf(',');
            if (Pos > 0)
                Ver = Ver.Substring(0, Pos);

//            rep.AddParameter("VER_DLL", Ver);
            ReportParameter par = new ReportParameter("VER_DLL", Ver);
            rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[1] { par });
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
			get { return "Приходная накладная (Омское лекарство)"; }
		}
	}
}
