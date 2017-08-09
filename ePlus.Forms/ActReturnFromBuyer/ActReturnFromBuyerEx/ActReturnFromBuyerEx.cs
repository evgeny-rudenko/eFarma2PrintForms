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

namespace FCSActReturnFromBuyer
{
	public class ActReturnFromBuyerEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSActReturnFromBuyer.ActReturnFromBuyer.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSActReturnFromBuyer.ActReturnFromBuyer.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnFromBuyer.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_RETURN_TO_BUYER_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;			
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnFromBuyer.rdlc");

			rep.LoadData("REPEX_ACT_RETURN_FROM_BUYER", doc.InnerXml);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table", 0);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table1", 1);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table2", 2);
						
			decimal sum = 0.0m;
			decimal disc = 0.0m;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
			{
				sum += AsDecimal(dataSet.Tables[1].Rows[i]["ABI_RETAILPRICEPERUNIT"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ABI_QUANTITY"]);
				disc += AsDecimal(dataSet.Tables[1].Rows[i]["DISCOUNT"]);
			}

			DataRow row = dataSet.Tables[0].Rows[0];

			ReportParameter[] parameters = new ReportParameter[10] {
				new ReportParameter("Chemistry", row["AB_CHEMISTRY"].ToString()),
				new ReportParameter("Store", row["AB_STORE"].ToString()),
				new ReportParameter("DocumentNumber", row["AB_NUMBER"].ToString()),
				new ReportParameter("DocumentDate", row["AB_DATE"].ToString()),
				new ReportParameter("ContractorNameFrom", row["AB_CONTRACTORFROM"].ToString()),
				new ReportParameter("ContractorNameTo", row["AB_CONTRACTORTO"].ToString()),
				new ReportParameter("BaseDocumentNumber", row["AB_NUMBERBASE"].ToString()),
				new ReportParameter("WritableSum", RusCurrency.Str((double) sum)),
				new ReportParameter("disc", RusCurrency.Str((double) (sum - disc))),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
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
			get { return "ActReturnBuyer"; }
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
			get { return "Акт возврата от покупателя"; }
		}
	}

}
