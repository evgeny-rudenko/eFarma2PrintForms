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

namespace UPD
{
	public class UPD : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		private const string CacheFolder = "Cache";
		private string connectionString;
		private string folderPath;

		/// <summary>
		/// Код плагина
		/// </summary>
		public string PluginCode
		{
			get { return "INVOICE_OUT"; }
		}

		/// <summary>
		/// Имя группы
		/// </summary>
		public string GroupName
		{
			get { return string.Empty; }
		}

		/// <summary>
		/// Название отчета
		/// </summary>
		public string ReportName
		{
			get { return "Универсальный передаточный документ (УПД)"; }
		}

		/// <summary>
		/// Execute метод
		/// </summary>
		/// <param name="connectionStr"></param>
		/// <param name="path"></param>
		public void Execute(string connectionStr, string path)
		{
			connectionString = connectionStr;
			folderPath = path;
			
			// обновить хранимую процедуру
			CreateStoredProc();

			// сохраняет rdlc файл отчета в папку кеш
			ExtractReport();
		}

		/// <summary>
		/// Вызов формы отчета
		/// </summary>
		/// <param name="dataRowItem">элемент списка</param>
		/// <returns>форма для печати</returns>
		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			var doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			var rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CacheFolder), "UPD.rdlc");

			rep.LoadData("DBO.REPEX_INVOICE_OUT_INVOICE_RIGLA_UPD", doc.InnerXml);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table0", 0);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table1", 1);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table2", 2);

			List<ReportParameter> parameters = null;

			// форма параметров
			using (var paramForm = new InvoiceForm())
			{
				var dataRow = rep.DataSource.Tables["InvoiceOutInvoice_DS_Table0"].Rows[0];
				paramForm.ChiefName = Utils.GetString(dataRow, "DIRECTOR_FIO");
				paramForm.AccountantName = Utils.GetString(dataRow, "BUH_FIO");
                paramForm.DocDetails = "Договор поставки";
				paramForm.DocStatus = "1";

				if (paramForm.ShowDialog() == DialogResult.OK)
				{
					// добавляем параметры
					parameters = new List<ReportParameter> {
						new ReportParameter("INVOICE_NAME", paramForm.Number + " от " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE").ToString("D")),
						new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName),
						new ReportParameter("CHIEF_NAME", paramForm.ChiefName),
						new ReportParameter("ACCOUNTANT_NAME", paramForm.AccountantName),
						new ReportParameter("DOCUMENT_DETAILS", paramForm.DocDetails),
						new ReportParameter("DOCUMENT_STATUS", paramForm.DocStatus == "2" ? "2" : "1")
					};
				}
				else
				{
					return null;
				}
			}

			// добавляем параметры в отчет если есть
			if (parameters != null)
			{
				rep.ReportViewer.LocalReport.SetParameters(parameters);
			}

			return rep;
		}

		/// <summary>
		/// Создает хранимую процедуру
		/// </summary>
		private void CreateStoredProc()
		{
			Stream stream = this.GetType().Assembly.GetManifestResourceStream("UPD.UPD.sql");

			if (stream == null)
			{
				return;
			}

			using (var sr = new StreamReader(stream, Encoding.GetEncoding(1251)))
			{
				string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

				foreach (var statement in batch)
				{
					if (statement == string.Empty)
					{
						continue;
					}

					using (var con = new SqlConnection(connectionString))
					{
						var command = new SqlCommand(statement, con);
						con.Open();
						command.ExecuteNonQuery();
						command.Dispose();
					}
				}
			}

			stream.Dispose();
		}

		/// <summary>
		/// Сохраняет rdlc файл отчет в папку кеш
		/// </summary>
		private void ExtractReport()
		{
			string cachePath = Path.Combine(folderPath, CacheFolder);

			if (!Directory.Exists(cachePath))
			{
				Directory.CreateDirectory(cachePath);
			}

			Stream stream = this.GetType().Assembly.GetManifestResourceStream("UPD.UPD.rdlc");

			if (stream == null)
			{
				return;
			}

			using (var sr = new StreamReader(stream))
			{
				using (var sw = new StreamWriter(Path.Combine(cachePath, "UPD.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}

			stream.Dispose();
		}
	}
}
