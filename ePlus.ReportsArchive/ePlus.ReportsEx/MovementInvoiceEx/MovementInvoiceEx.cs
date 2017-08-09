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

namespace MovementInvoiceEx
{
	public class MovementInvoiceEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("MovementInvoiceEx.Movement.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("MovementInvoiceEx.MovementInvoice.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "MovementInvoice.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_MOVEMENT", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = ReportName;
			rep.ReportFormName = "Перемещение (накладная)";
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "MovementInvoice.rdlc");

			rep.LoadData("REPEX_MOVEMENT", doc.InnerXml);
			rep.BindDataSource("Movement_DS_Table1", 0);

			decimal summary = 0;
			int i = 0;

			DataSet ds = rep.DataSource;
			foreach (DataRow Row in ds.Tables[0].Rows)
			{
				summary += Utils.GetDecimal(Row, "PRICE_SUMM_SALE");
				ds.Tables[0].Rows[i]["NUMBER"] = i + 1;
				i++;
			}

			ReportParameter[] parameters = new ReportParameter[12] {
				new ReportParameter("COMPANY", Utils.GetString(ds.Tables[2].Rows[0], 0)),
				new ReportParameter("DATE", Utils.GetDate(ds.Tables[1].Rows[0], "date").ToString("dd.MM.yyyy")),
				new ReportParameter("MNEMOCODE", Utils.GetString(ds.Tables[1].Rows[0], "mnemocode")),
				new ReportParameter("STOREFROM", Utils.GetString(ds.Tables[1].Rows[0], "storenamefrom")),
				new ReportParameter("STORETO", Utils.GetString(ds.Tables[1].Rows[0], "storenameto")),
				new ReportParameter("BASEDOC", Utils.GetString(ds.Tables[1].Rows[0], "doc")),
				new ReportParameter("BASEDOCDATE", Utils.GetDate(ds.Tables[1].Rows[0], "date_doc").ToString("dd.MM.yyyy")),
				new ReportParameter("COUNT", RusNumber.Str(i, true, null, null, null)),
				new ReportParameter("SUMMARY", RusCurrency.Str((double) summary)),
				new ReportParameter("MNEMOCODESHORT", Utils.GetString(ds.Tables[1].Rows[0], "mnemocodeshort")),
				new ReportParameter("STOREFROMSHORT", Utils.GetString(ds.Tables[1].Rows[0], "storenamefromshort")),
				new ReportParameter("STORETOSHORT", Utils.GetString(ds.Tables[1].Rows[0], "storenametoshort"))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
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
			ExtractReport();
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "Перемещение (накладная)"; }
		}
	}
}
