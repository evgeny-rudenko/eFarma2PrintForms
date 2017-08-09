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
using System.Windows.Forms;

namespace RKOEx
{
	public class RKOEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("RKOEx.RKO.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("RKOEx.RKO.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "RKO.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_CASH_ORDER", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "RKO.rdlc");

			rep.LoadData("REPEX_RKO", doc.InnerXml);
			rep.BindDataSource("RKO_DS_Table", 0);
			rep.BindDataSource("RKO_DS_Table2", 2);

			DataRow dr = rep.DataSource.Tables[1].Rows[0];
			string s = string.Empty;

			if (Convert.ToDecimal(dr[2]) > 0m)
				s = "НДС - " + Convert.ToDecimal(dr[2]).ToString("N2");
			else if (Convert.ToDecimal(dr[0]) == 0m && Convert.ToDecimal(dr[1]) == 0m)
				s = "Без НДС";
			else
			{
				if (Convert.ToDecimal(dr[0]) != 0m)
					s = "НДС (10%) - " + Convert.ToDecimal(dr[0]).ToString("N2") + " руб.";
				if (Convert.ToDecimal(dr[1]) != 0m)
				{
					if (s != "")
						s += ", ";
					s += "НДС (18%) - " + Convert.ToDecimal(dr[1]).ToString("N2") + " руб.";
				}
			}

            string numberRKO = rep.DataSource.Tables[0].Rows[0]["MNEMOCODE"].ToString();
            using (RKO_Form paramForm = new RKO_Form())
            {
                numberRKO = paramForm.NumberRKO = !string.IsNullOrEmpty(numberRKO) && numberRKO.Length > 4 ?
                    numberRKO.Substring(numberRKO.Length - 4) : rep.DataSource.Tables[0].Rows[0]["NUMBER"].ToString();

				if (paramForm.ShowDialog() == DialogResult.OK)
					numberRKO = paramForm.NumberRKO;
				else
					return null;
            }

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("SUMM", RusCurrency.Str((double) Utils.GetDecimal(rep.DataSource.Tables[0].Rows[0], "SUM"))),
                new ReportParameter("NUMBER_RKO", numberRKO),
				new ReportParameter("NDS", s)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			return rep;
		}

		public string PluginCode
		{
			get { return "EXPENCE_CASH_ORDER"; }
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
			get { return "Расходный кассовый ордер"; }
		}
	}
}
