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

namespace InvoiceRating
{
	public class InvoiceRating : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceRating.InvoiceRating.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceRating.InvoiceRating.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceRating.rdlc")))
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
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceRating.rdlc");

			rep.LoadData("REPEX_INVOICE_RATING", doc.InnerXml);
			rep.BindDataSource("InvoiceRating_DS_Table0", 0);
			rep.BindDataSource("InvoiceRating_DS_Table1", 1);
			rep.BindDataSource("InvoiceRating_DS_Table2", 2); 
						
			decimal vat = 0.0m;
			decimal ad = 0.0m;
			decimal sal = 0.0m;

			DataTable table = rep.DataSource.Tables[2];
			for (int i = 0; i < table.Rows.Count; i++)
			{
				vat += Utils.GetDecimal(table.Rows[i]["VSUM_SUP"]);
				ad += Utils.GetDecimal(table.Rows[i]["SAL_ADPRICE_SUM"]);
				sal += Utils.GetDecimal(table.Rows[i]["VPRICE_SUM"]);

			}

			ReportParameter[] parameters = new ReportParameter[1]
			{
				new ReportParameter("sum", new StringBuilder("Наименований ")
				.Append(table.Rows.Count.ToString())
				.Append(" шт., сумма закупочная (с НДС) ")
				.Append(RusCurrency.Str((double) vat).ToLower())
				.Append(", сумма розничной наценки ")
				.Append(RusCurrency.Str((double) ad).ToLower())
				.Append(", сумма розничная (с НДС) ")
				.Append(RusCurrency.Str((double) sal).ToLower()).ToString())
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
			get { return "Расценочная накладная"; }
		}
	}
}
