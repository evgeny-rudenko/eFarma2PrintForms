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

namespace FCSActReturnToContractorInvoice
{
	public class ActReturnToContractorInvoice : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSActReturnToContractorInvoice.ActReturnToContractorInvoice.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSActReturnToContractorInvoice.ActReturnToContractorInvoice.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnToContractorInvoice.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
            if (!Directory.Exists(cachePath))
				Directory.CreateDirectory(cachePath);
            s = this.GetType().Assembly.GetManifestResourceStream("FCSActReturnToContractorInvoice.ActReturnToContractorInvoiceAfter240112.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnToContractorInvoiceAfter240112.rdlc")))
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
            if (GetDateDocument(dataRowItem.Guid) < Convert.ToDateTime("24.01.2012"))
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnToContractorInvoice.rdlc");
            else
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnToContractorInvoiceAfter240112.rdlc");

			rep.LoadData("DBO.REPEX_ACT_RETURN_TO_CONTRACTOR_INVOICE", doc.InnerXml);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table0", 0);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table1", 1);
			rep.BindDataSource("InvoiceOutInvoice_DS_Table2", 2);

			string number;

			using (InvoiceForm paramForm = new InvoiceForm())
			{
				number = paramForm.Number = Utils.GetString(dataRowItem.Row, "MNEMOCODE");

				if (paramForm.ShowDialog() == DialogResult.OK)
					number = paramForm.Number;
			}

			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("INVOICE_NAME", number + " от " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE").ToString("D")),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}
        private DateTime GetDateDocument(Guid guid)
        {
            DateTime Result = DateTime.Now;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT DATE FROM ACT_RETURN_TO_CONTRACTOR WHERE ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL = '{0}'", guid), con);
                command.CommandTimeout = 60 * 60;
                con.Open();
                try
                {
                    Result = Convert.ToDateTime(command.ExecuteScalar());
                }
                catch
                { }
            }
            return Result;
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
			get { return "Счет-фактура"; }
		}
	}

}
