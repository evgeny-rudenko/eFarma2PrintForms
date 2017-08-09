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

namespace InvoiceOutEx
{
	public class InvoiceOutEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutKirov.InvoiceOutKirov.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceOutKirov.InvoiceOutKirov.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutKirov.rdlc")))
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
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutKirov.rdlc");

			rep.LoadData("REPEX_INVOICE_OUT_KIROV", doc.InnerXml);
			rep.BindDataSource("InvoiceOutKirov_Table0", 0);
			rep.BindDataSource("InvoiceOutKirov_Table1", 1);

			decimal sum = 0m;
			int count = 0;

			foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
			{
				sum += Utils.GetDecimal(Row, "PRICE_SUM");
				count++;
			}

			//rep.AddParameter("user", SecurityContextEx.USER_NAME);
			//rep.AddParameter("sum", sum.ToString());
			//rep.AddParameter("sumInText", RusCurrency.Str((double) sum));
			//rep.AddParameter("count", count.ToString());

			ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("sum", sum.ToString("N2")),
				new ReportParameter("sumInText", RusCurrency.Str((double) sum)),
				new ReportParameter("count", count.ToString()),
				new ReportParameter("user", SecurityContextEx.USER_NAME)
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
			get { return "Расходные документы: Накладная Киров"; }
		}
	}
}
