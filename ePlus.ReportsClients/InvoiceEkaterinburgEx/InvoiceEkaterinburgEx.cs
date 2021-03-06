﻿using System;
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

namespace InvoiceEkaterinburgEx
{
	public class InvoiceEkaterinburgEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceEkaterinburgEx.InvoiceEkaterinburg.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("InvoiceEkaterinburgEx.InvoiceEkaterinburg.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceEkaterinburg.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = ReportName;
			rep.ReportFormName = "Накладная Екатеринбург";
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceEkaterinburg.rdlc");

			rep.LoadData("REPEX_INVOICE_EKATERINBURG", doc.InnerXml);
			rep.BindDataSource("Invoice_Table", 0);
			rep.BindDataSource("Invoice_Table1", 1);
			rep.BindDataSource("Invoice_Table2", 2);

			decimal dblSummory = 0m;
			decimal retailSummory = 0m;
			foreach (DataRow Row in rep.DataSource.Tables[2].Rows)
			{
				dblSummory += Utils.GetDecimal(Row, "CONTRACTOR_SUM_VAT");
				retailSummory += Utils.GetDecimal(Row, "SUM_RETAIL_SUM");
			}

			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("SUMMORY", RusCurrency.Str((double) dblSummory)),
				new ReportParameter("RETAIL_SUMMORY", RusCurrency.Str((double) retailSummory))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);			
			return rep;
		}

		public string PluginCode
		{
			get { return "INVOICE"; }
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
			get { return "Накладная Екатеринбург"; }
		}
	}
}
