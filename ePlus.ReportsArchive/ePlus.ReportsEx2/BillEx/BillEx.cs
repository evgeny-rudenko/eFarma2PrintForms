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

namespace BillEx
{
	public class BillEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		private const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("BillEx.Bill.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("BillEx.Bill.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "Bill.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_BILL_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "Bill.rdlc");

			rep.LoadData("REPEX_BILL", doc.InnerXml);
			rep.BindDataSource("BillReportData_Table", 0);
			rep.BindDataSource("BillReportData_Table1", 1);

			decimal dblSummory = 0m;
			decimal dblVatSummory = 0m;
			decimal vatSum10 = 0m;
			decimal vatSum18 = 0m;

			foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
			{
				dblSummory += Utils.GetDecimal(Row, "RETAIL_SUMV");
				dblVatSummory += Utils.GetDecimal(Row, "VAT");
				vatSum10 += Utils.GetDecimal(Row, "VAT_SUM10");
				vatSum18 += Utils.GetDecimal(Row, "VAT_SUM18");
			}

			ReportParameter[] parameters = new ReportParameter[5] {
				new ReportParameter("SUMM", dblSummory.ToString("N2")),
				new ReportParameter("VAT_SUMM", dblVatSummory.ToString("N2")),
				new ReportParameter("SUMM_RUS", RusCurrency.Str((double) dblSummory)),
				new ReportParameter("VAT_SUM10", vatSum10.ToString("N2")),
				new ReportParameter("VAT_SUM18", vatSum18.ToString("N2"))
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
			get { return "BILL"; }
		}

		public string ReportName
		{
			get { return "Выписка счетов: Счет"; }
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

	}
}
