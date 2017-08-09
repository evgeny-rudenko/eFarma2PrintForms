using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace ActReturnTORG2Ex
{
	public class ActReturnTORG2Ex : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActReturnTORG2Ex.ActReturnTORG2.sql");

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
		
		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_DOCUMENT", dataRowItem.Id);

			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_ACT_RETURN_TORG2", connectionString))
			{
				sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
				sqlda.Fill(ds);
			}

			ActReturnTORG2 report = new ActReturnTORG2();
			ReportFormCrystal reportForm = new ReportFormCrystal();
			reportForm.SetDataSource(ReportName, ds, report);
			return reportForm;
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
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "Акты возврата поставщику: ТОРГ-2"; }
		}
	}
}
