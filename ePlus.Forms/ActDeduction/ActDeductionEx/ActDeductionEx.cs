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

namespace ActDeductionEx
{
	public class ActDeductionEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActDeductionEx.ActDeduction.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActDeductionEx.ActDeduction.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActDeduction.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_DEDUCTION", dataRowItem.Id);
			Utils.AddNode(root, "ID_ACT_DEDUCTION_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActDeduction.rdlc");

			rep.LoadData("REPEX_ACT_DEDUCTION", doc.InnerXml);
			rep.BindDataSource("Act_Deduction_DS_Table", 0);
			rep.BindDataSource("Act_Deduction_DS_Table1", 1);
			rep.BindDataSource("Act_Deduction_DS_Table2", 2);
						
			decimal fPm_SumAll = 0.0M;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
			{
				dataSet.Tables[1].Rows[i]["ADI_SUMSUPPLIER"] = AsDecimal(dataSet.Tables[1].Rows[i]["ADI_SUMSUPPLIER"]) == 0.0M ? AsDecimal(dataSet.Tables[1].Rows[i]["ADI_PRICESUPPLIER"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ADI_QUANTITY"]) : dataSet.Tables[1].Rows[i]["ADI_SUMSUPPLIER"];
				dataSet.Tables[1].Rows[i]["ADI_SUMRETAIL"] = AsDecimal(dataSet.Tables[1].Rows[i]["ADI_SUMRETAIL"]) == 0.0M ? AsDecimal(dataSet.Tables[1].Rows[i]["ADI_PRICERETAIL"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ADI_QUANTITY"]) : dataSet.Tables[1].Rows[i]["ADI_SUMRETAIL"];
				fPm_SumAll += AsDecimal(dataSet.Tables[1].Rows[i]["ADI_PRICERETAIL"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ADI_QUANTITY"]);
			}

			ReportParameter[] parameters = new ReportParameter[3] {
				new ReportParameter("Pm_SumAll", fPm_SumAll.ToString()),
				new ReportParameter("Pm_ActNumber", AsString(dataSet.Tables[0].Rows[0]["AD_NUMBER"])),
				new ReportParameter("Pm_RusWordsSumAll", RusCurrency.Str((double) fPm_SumAll)),
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

		static decimal AsDecimal(object value)
		{
			return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
		}
		
		static string AsString(object value)
		{
			return value != DBNull.Value ? Convert.ToString(value) : "";
		}
		
		public string PluginCode
		{
			get { return "ACT_DEDUCTION"; }
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
			get { return "Акт списания"; }
		}
	}

}
