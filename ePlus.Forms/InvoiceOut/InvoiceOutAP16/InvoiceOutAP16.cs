using System;
using System.Collections.Generic;
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

namespace InvoiceOutAP16
{
	public class InvoiceOutAP16 : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutAP16.InvoiceOutAP16.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutAP16.InvoiceOutAP16.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutAP16.rdlc")))
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
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutAP16.rdlc");

			rep.LoadData("REPEX_INVOICE_OUT_AP16", doc.InnerXml);
			rep.BindDataSource("InvoiceOut_DS_Table0", 0);
			rep.BindDataSource("InvoiceOut_DS_Table1", 1);
			rep.BindDataSource("InvoiceOut_DS_Table2", 2);
			rep.BindDataSource("InvoiceOut_DS_Table3", 3);

			decimal sum = 0;

			foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
			{
				sum += Utils.GetDecimal(Row, "SUM_SAL");
			}

			ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("sum", RusCurrency.Str((double) sum))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}


		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
			ExtractReport();
		}

		public string PluginCode
		{
			get { return "INVOICE_OUT"; }
		}

		public string ReportName
		{
      get { return "Требование-накладная"; }
		}

		public string GroupName
		{
			get { return string.Empty; }
		}
	}
}

