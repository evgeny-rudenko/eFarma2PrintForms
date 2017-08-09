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

namespace ListReturnToContractorEx
{
	public class ListReturnToContractor : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ListReturnToContractorEx.ListReturnToContractor.sql");

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

		void AddParameter(DataSet ds, Type type, string paramName, object val)
		{
			string tname = "parameters";
			DataTable ptab = ds.Tables[tname];
			if (ptab == null)
			{
				ptab = new DataTable(tname);
				ds.Tables.Add(ptab);
			}

			// Вставка параметра - столбца
			DataColumn col = ptab.Columns[paramName];
			if (col == null)
			{
				col = new DataColumn(paramName, type);
				ptab.Columns.Add(col);
			};
			DataRow dr = ptab.Rows.Count == 0 ? ptab.NewRow() : ptab.Rows[0];
			dr[paramName] = val;
			if (ptab.Rows.Count == 0)
				ptab.Rows.Add(dr);
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");			
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_LIST_RETURN_TO_CONTRACTOR", connectionString))
			{
				sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
				sqlda.Fill(ds);
			}

			decimal sum = 0m;
			foreach (DataRow row in ds.Tables[0].Rows)
			{
				sum += Utils.GetDecimal(row, "SUM_CONTRACTOR_PRICE_VAT");
			}

			AddParameter(ds, typeof(double), "summory", sum);
			AddParameter(ds, typeof(string), "str_summory", RusCurrency.Str((double) sum));
			AddParameter(ds, typeof(string), "count_rows", RusCurrency.Str(ds.Tables[0].Rows.Count, "NUM"));

			InvoiceTORG12 report = new InvoiceTORG12();
			ReportFormCrystal reportForm = new ReportFormCrystal();
			reportForm.SetDataSource(ReportName, ds, report);
			return reportForm;
		}

		public string PluginCode
		{
			get { return "ActReturnToContractor"; }
		}

		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "Акты возврата поставщику: Товарная накладная"; }
		}
	}
}
