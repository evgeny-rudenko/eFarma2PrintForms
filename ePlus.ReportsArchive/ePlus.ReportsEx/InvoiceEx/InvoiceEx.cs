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

namespace InvoiceEx
{
	public class InvoiceEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceEx.Invoice.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceEx.Invoice.rdlc");
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
			rep.LoadData("REPEX_INVOICE", doc.InnerXml);
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
			string strSummory = RusCurrency.Str((double) dblSummory);
			string strRetailSummory = RusCurrency.Str((double) retailSummory);

			string file = Path.Combine(cachePath, "Invoice.rdlc");
			rep.ReportViewer.LocalReport.ReportPath = file;

			// параметры
			ReportParameter p1 = new ReportParameter("SUMMORY", strSummory);
			ReportParameter p2 = new ReportParameter("RETAIL_SUMMORY", strRetailSummory);

			rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
			rep.ReportFormName = "Приходная накладная";
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
			get { return "Приходная накладная"; }
		}
	}
}
