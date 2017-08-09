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

namespace InvoiceOutEkaterinburgEx
{
	public class InvoiceOutEkaterinburgEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutEkaterinburgEx.InvoiceOutEkaterinburg.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutEkaterinburgEx.InvoiceOutEkaterinburg.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutEkaterinburg.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE_OUT_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = ReportName;
			rep.ReportFormName = "Расходная накладная Екатеринбург";
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutEkaterinburg.rdlc");

			rep.LoadData("REPEX_INVOICE_OUT_EKATERINBURG", doc.InnerXml);
			rep.BindDataSource("Invoice_Out_DS_Table", 0);
			rep.BindDataSource("Invoice_Out_DS_Table1", 1);
			rep.BindDataSource("Invoice_Out_DS_Table2", 2);
			rep.BindDataSource("Invoice_Out_DS_Table3", 3);

			decimal sum = 0m;
			decimal sumRet = 0m;
			decimal sumRetWithDiscount = 0m;
						
			foreach (DataRow row in rep.DataSource.Tables[0].Rows)
			{
				sum += Utils.GetDecimal(row, "RETAIL_SUM") + Utils.GetDecimal(row, "SUM_DISCOUNT");
				sumRet += Utils.GetDecimal(row, "RETAIL_SUM") + Utils.GetDecimal(row, "SUM_DISCOUNT");
				sumRetWithDiscount += Utils.GetDecimal(row, "RETAIL_SUM");
			}

			ReportParameter[] parameters = new ReportParameter[] {
				new ReportParameter("SUMMORY", RusCurrency.Str((double) sum)),
				new ReportParameter("RETAIL_SUMMORY", RusCurrency.Str((double) sumRet)),
				new ReportParameter("RETAIL_SUMMARY_WITH_DISCOUNT", RusCurrency.Str((double) sumRetWithDiscount))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);			
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
			get { return "Расходная накладная Екатеринбург"; }
		}
	}
}
