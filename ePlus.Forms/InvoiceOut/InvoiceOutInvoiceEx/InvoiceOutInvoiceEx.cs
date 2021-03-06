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
using System.Windows.Forms;

namespace FCBInvoiceOutInvoice
{
	public class InvoiceOutInvoiceEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBInvoiceOutInvoice.InvoiceOutInvoice.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCBInvoiceOutInvoice.InvoiceOutInvoice.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceOutInvoice.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutInvoice.rdlc");

			rep.LoadData("REPEX_INVOICE_OUT_INVOICE", doc.InnerXml);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table0", 0);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table1", 1);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table2", 2);

			string number;

			using (InvoiceForm paramForm = new InvoiceForm())
			{
				string num = Utils.GetString(dataRowItem.Row, "DOC_NUM");

				number = paramForm.Number = !string.IsNullOrEmpty(num) ? num : Utils.GetString(dataRowItem.Row, "MNEMOCODE");

				if (paramForm.ShowDialog() == DialogResult.OK)
					number = paramForm.Number;
			}

			DataTable dt = rep.DataSource.Tables[0];
			ReportParameter[] parameters = new ReportParameter[5] {
				new ReportParameter("INVOICE_NAME", number + " от " + Utils.GetDate(dt.Rows[0], "INVOICE_OUT_DATE").ToString("dd.MM.yyyy")),
				new ReportParameter("SUMM_WITH_TAX_STRING",	RusCurrency.Str((double) Utils.GetDecimal(dt.Rows[0], "SUMM_WITH_TAX"))),
				new ReportParameter("CONTRACTOR_TO_NAME_ADDRESS", Utils.GetString(dt.Rows[0], "GCONTRACTOR_TO_NAME") + ", Адрес: " + Utils.GetString(dt.Rows[0], "GCONTRACTOR_TO_ADDRESS")),
				new ReportParameter("INVOICE_OUT_NAME_DATE", "Расходная " + Utils.GetString(dt.Rows[0], "INVOICE_OUT_NAME") + " от " + Utils.GetDate(dt.Rows[0], "INVOICE_OUT_DATE").ToString("d")),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			return rep;
		}

		public string PluginCode
		{
			get { return "INVOICE_OUT"; }
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
			get { return "Счет-фактура"; }
		}
	}
}
