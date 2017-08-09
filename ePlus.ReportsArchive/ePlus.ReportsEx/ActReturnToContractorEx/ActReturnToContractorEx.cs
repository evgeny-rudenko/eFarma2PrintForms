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

namespace ActReturnToContractorEx
{
	public class ActReturnToContractorEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActReturnToContractorEx.ActReturnToContractor.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActReturnToContractorEx.ActReturnToContractor.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnToContractor.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_RETURN_TO_CONTRACTOR", dataRowItem.Id);
			Utils.AddNode(root, "ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnToContractor.rdlc");

			rep.LoadData("REPEX_ACT_RETURN_TO_CONTRACTOR", doc.InnerXml);
			rep.BindDataSource("Act_ReturnToContractor_DS_Table", 0);
			rep.BindDataSource("Act_ReturnToContractor_DS_Table1", 1);
			rep.BindDataSource("Act_ReturnToContractor_DS_Table2", 2);

			decimal dPm_SumSupp = 0m, dPm_SumPvatSupp = 0m,
				dPm_SumSal = 0m, dPm_SumPvatSal = 0m,
				dPm_SumDiscount = 0m, dPm_SumTaxSale = 0m;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
			{
				dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"] = AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"]) == 0.0M ? AsDecimal(dataSet.Tables[2].Rows[i]["ACI_PRICESUPP"]) * AsDecimal(dataSet.Tables[2].Rows[i]["ACI_QUANTITY"]) : dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"];
				dataSet.Tables[2].Rows[i]["ACI_SUMSAL"] = AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSAL"]) == 0.0M ? AsDecimal(dataSet.Tables[2].Rows[i]["ACI_PRICESAL"]) * AsDecimal(dataSet.Tables[2].Rows[i]["ACI_QUANTITY"]) : dataSet.Tables[2].Rows[i]["ACI_SUMSAL"];
				dPm_SumSupp += AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"]);
				dPm_SumSal += AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSAL"]);
			}

			ReportParameter[] parameters = new ReportParameter[8] {
				new ReportParameter("Pm_Header", "Возврат поставщику № " + 
				Utils.GetString(dataSet.Tables[1].Rows[0]["AC_NUMBER"]) + " от " + 
				Utils.GetDate(dataSet.Tables[1].Rows[0]["AC_DATE"]).ToShortDateString()),

				new ReportParameter("Pm_SumSupp", dPm_SumSupp.ToString()),
				new ReportParameter("Pm_SumPvatSupp", dPm_SumPvatSupp.ToString()),
				new ReportParameter("Pm_SumSal", dPm_SumSal.ToString()),
				new ReportParameter("Pm_SumSalRusWords", RusCurrency.Str((double) dPm_SumSupp)),
				new ReportParameter("Pm_SumPvatSal", dPm_SumPvatSal.ToString()),
				new ReportParameter("Pm_SumDiscount", dPm_SumDiscount.ToString()),
				new ReportParameter("Pm_SumTaxSale", dPm_SumTaxSale.ToString())
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

		static decimal AsDecimal(object value)
		{
			return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
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
			ExtractReport();
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "Возврат поставщику"; }
		}
	}

}
