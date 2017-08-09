using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace MovementEx
{
	public class MovementEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("MovementEx.Movement.sql");

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
			Utils.AddNode(root, "ID_MOVEMENT", dataRowItem.Id);

			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_MOVEMENT", connectionString))
			{
				sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
				sqlda.Fill(ds);
			}

			decimal summary1 = 0;
			decimal summary2 = 0;
			int i = 0;

			foreach (DataRow Row in ds.Tables[0].Rows)
			{
				summary1 += Utils.GetDecimal(Row, "PRICE_SUMMA");
				summary2 += Utils.GetDecimal(Row, "PRICE_SUMMA_VAT");
				ds.Tables[0].Rows[i]["NUMBER"] = i + 1;
				i++;
			}

			string summary1InText = RusCurrency.Str((double) summary1);
			string summary2InText = RusCurrency.Str((double) summary2);
			string rowsCountInText = string.Format(@"( {0} )", RusNumber.Str(i, true, "", "", ""));

			AddParameter(ds, typeof(string), "summory1", summary1InText);
			AddParameter(ds, typeof(string), "summory2", summary2InText);
			AddParameter(ds, typeof(string), "count_rows", rowsCountInText);

			Movement report = new Movement();
			ReportFormCrystal reportForm = new ReportFormCrystal();
			reportForm.SetDataSource("Перемещение", ds, report);
			return reportForm;
		}

		public string PluginCode
		{
			get { return "Movement"; }
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
			get { return "Перемещение"; }
		}
	}
}
