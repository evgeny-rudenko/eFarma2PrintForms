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

namespace F17MMovementP
{
	public class Movement : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("F17MMovementP.Movement.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F17MMovementP.Movement.rdlc"); 
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "Movement.rdlc")))
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

			rep.Text = rep.ReportFormName = ReportName;			
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "Movement.rdlc");

			rep.LoadData("REPEX_MOVEMENT", doc.InnerXml);
			rep.BindDataSource("MovementDS_Table0", 0);
			rep.BindDataSource("MovementDS_Table1", 1);
			rep.BindDataSource("MovementDS_Table2", 2);
			rep.BindDataSource("MovementDS_Table3", 3);
			
			int i = 0;
			decimal summary = 0m;			

			DataSet ds = rep.DataSource;
			foreach (DataRow Row in ds.Tables[2].Rows)
			{
				summary += Utils.GetDecimal(Row, "PRICE_SUMM_SALE");				
				i++;
			}

			ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("Count", i.ToString()),
				new ReportParameter("CountInText", RusNumber.Str(i, true, "", "", "")),
				new ReportParameter("SummaryInText", RusCurrency.Str((double) summary)),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
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
			get { return "Перемещение(книж.)"; }
		}
	}
}
