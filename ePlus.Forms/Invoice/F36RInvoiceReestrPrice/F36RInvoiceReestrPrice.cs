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

namespace F36RInvoiceReestrPrice
{
	public class F36RInvoiceReestrPrice : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F36RInvoiceReestrPrice.F36RInvoiceReestrPrice.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F36RInvoiceReestrPrice.F36RInvoiceReestrPrice.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "F36RInvoiceReestrPrice.rdlc")))
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

			rep.Text = rep.ReportFormName = ReportName;

            rep.LoadData("REPEX_F36RInvoiceReestrPrice", doc.InnerXml);
            rep.BindDataSource("F36RInvoiceReestrPrice_DS_Table", 0);
            rep.BindDataSource("F36RInvoiceReestrPrice_DS_Table1", 1);
            rep.BindDataSource("F36RInvoiceReestrPrice_DS_Table2", 2);

			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            string file = Path.Combine(cachePath, "F36RInvoiceReestrPrice.rdlc");
			rep.ReportViewer.LocalReport.ReportPath = file;

			decimal dblSummory = 0;
			decimal intCountRows = 0;
			foreach (DataRow row in rep.DataSource.Tables[1].Rows)
			{
				dblSummory += ePlus.MetaData.Core.Utils.GetDecimal(row, "RETAIL_SUM_VAT");
				intCountRows++;
			}
			// преобразование суммы в строку
			string strSummory = ePlus.CommonEx.Reporting.RusCurrency.Str((double) dblSummory);
/*			ReportParameter p1 = new ReportParameter("str_summory", strSummory);
			ReportParameter p2 = new ReportParameter("count", dblSummory.ToString("#0.00"));

			rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });*/
            
            ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("str_summory", strSummory),
				new ReportParameter("count", dblSummory.ToString("#0.00")),
				new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
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
            get { return "Протокол согласования розничных цен (Оренбург)"; }
		}
	}
}
