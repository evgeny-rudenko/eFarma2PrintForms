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

namespace PPVatEx
{
	public class PPVatEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("PPVatEx.PPVat.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("PPVatEx.PPVat.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "PPVat.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_PAYMENT_ORDER", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "PPVat.rdlc");

			rep.LoadData("REPEX_PAYMENT_VAT", doc.InnerXml);
			rep.BindDataSource("Payment_DS_Table", 0);

			string numberPP = null;

			using (PP_Form paramForm = new PP_Form())
			{
				numberPP = paramForm.NumberPP = Utils.GetString(rep.DataSource.Tables[0].Rows[0], "MNEMOCODE");

				if (paramForm.ShowDialog() == DialogResult.OK)
					numberPP = paramForm.NumberPP;
			}

			double pvatSum = (double) Utils.GetDecimal(rep.DataSource.Tables[0].Rows[0], "PVAT_SUM");

			string sum_f = pvatSum.ToString();
			sum_f = (sum_f.IndexOf(',') == -1) ? sum_f += "=" : sum_f.Replace(',', '-');
			string vat_sum = ((double) Utils.GetDecimal(rep.DataSource.Tables[0].Rows[0], "VAT_SUM")).ToString();
			vat_sum = (vat_sum.IndexOf(',') == -1) ? vat_sum += "=" : vat_sum.Replace(',', '-');

			ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("SUMM", RusCurrency.Str(pvatSum)),
				new ReportParameter("NUMBER_PP", numberPP),
				new ReportParameter("SUMM_F", sum_f),
				new ReportParameter("vat", "НДС(" + Utils.GetInt(rep.DataSource.Tables[0].Rows[0], "VAT").ToString() + "%) " + vat_sum)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			return rep;
		}

		public string PluginCode
		{
			get { return "PAYMENT_ORDER"; }
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
			get { return "Банковские документы: Платежное поручение"; }
		}
	}
}
