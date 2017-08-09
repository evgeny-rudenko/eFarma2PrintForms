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

namespace FCSRequestCalculation
{
	public class RequestCalculation : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString; 
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSRequestCalculation.RequestCalculation.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSRequestCalculation.RequestCalculation.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "RequestCalculation.rdlc")))
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
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "RequestCalculation.rdlc");

			rep.LoadData("REPEX_REQUEST_CALCULATION", doc.InnerXml);
			rep.BindDataSource("RequestCalculation_DS_Table0", 0);
			rep.BindDataSource("RequestCalculation_DS_Table1", 1);
			rep.BindDataSource("RequestCalculation_DS_Table2", 2);

			string p_ch = "";

			using (Popup paramForm = new Popup())
			{
				if (paramForm.ShowDialog() == DialogResult.OK)
				{
					for (int i = 0; i < paramForm.checkedListBox1.Items.Count; i++)
					{
						if (paramForm.checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
					}
				}
				else return null;
			}

			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("p_ch", p_ch),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}
		
		public string PluginCode
		{
			get { return "REQUEST_CALCULATION"; }
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
			get { return "Расчет потребности"; }
		}
	}
}
