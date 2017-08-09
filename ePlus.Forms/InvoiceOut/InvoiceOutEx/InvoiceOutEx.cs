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

namespace FCSInvoiceOut
{
	public class InvoiceOutEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSInvoiceOut.InvoiceOut.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSInvoiceOut.InvoiceOut.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOut.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_INVOICE_OUT", connectionString))
			{
				sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = string.Format("<XML><ID_INVOICE_OUT>{0}</ID_INVOICE_OUT></XML>", dataRowItem.Id);
				sqlda.Fill(ds);

			}

			decimal sum = 0m;
			decimal sumRet = 0m;
			decimal sumRetWithDiscount = 0m;

			foreach (DataRow row in ds.Tables[0].Rows)
			{
				sum += Utils.GetDecimal(row, "RETAIL_SUM") + Utils.GetDecimal(row, "SUM_DISCOUNT");
				sumRet += Utils.GetDecimal(row, "RETAIL_SUM") + Utils.GetDecimal(row, "SUM_DISCOUNT");
				sumRetWithDiscount += Utils.GetDecimal(row, "RETAIL_SUM");
			}

			sum = Utils.Round(sum, 2);
			sumRet = Utils.Round(sumRet, 2);
			string sumInText = RusCurrency.Str((double) sum);
			string sumRetInText = RusCurrency.Str((double) sumRet);
			string sumRetWithDiscountText = RusCurrency.Str((double) sumRetWithDiscount);

			ReportFormNew rep = new ReportFormNew();
			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportViewer.LocalReport.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOut.rdlc");
			rep.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Invoice_Out_DS_Table", ds.Tables[0]));
			rep.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Invoice_Out_DS_Table1", ds.Tables[1]));
			rep.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Invoice_Out_DS_Table2", ds.Tables[2]));
			rep.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Invoice_Out_DS_Table3", ds.Tables[3]));

			ReportParameter p1 = new ReportParameter("SUMMORY", sumInText);
			ReportParameter p2 = new ReportParameter("RETAIL_SUMMORY", sumRetInText);
			ReportParameter p3 = new ReportParameter("RETAIL_SUMMARY_WITH_DISCOUNT", sumRetWithDiscountText);
            ReportParameter p4 = new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
			
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
			get { return "Накладная"; }
		}
	}
}
