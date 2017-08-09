using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;

namespace FCChStockGoods
{
    public class StockGoods : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache"; 
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCChStockGoods.StockGoods.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCChStockGoods.StockGoods.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "StockGoods.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GOODS", dataRowItem.Id);
            Utils.AddNode(root, "ID_LOT_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "StockGoods.rdlc");

			rep.LoadData("DBO.REPEX_STOCK_GOODS_EXT", doc.InnerXml);
            rep.BindDataSource("StockGoods_DS_Table0", 0);
            ReportParameter[] parameters = new ReportParameter[1] {
new ReportParameter("VER_DLL",System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
			return rep;
		}


		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
			ExtractReport();
		}

		public string PluginCode
		{
			get { return "STOCK"; }
		}

		public string ReportName
		{
            get { return "Резервирование по документам"; }
		}

		public string GroupName
		{
			get { return string.Empty; }
		}
	}
}

