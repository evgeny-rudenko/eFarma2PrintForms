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

namespace Inv19_Rigla_Alter201104
{
    public class Inv19_Alter201104 : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		private const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("Inv19_Rigla_Alter201104.Inv19.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("Inv19_Rigla_Alter201104.Inv19_Rigla.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "Inv19_Rigla.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
      //по умолчанию по розничным ценам
      Int16 isSal = 0;
      using (Inv19_Alter201104Form paramForm = new Inv19_Alter201104Form())
      {
        if (paramForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          isSal = paramForm.IsSal;
      }
      
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
      Utils.AddNode(root, "IS_SAL", isSal);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
      rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "Inv19_Rigla.rdlc");

      rep.LoadData("REPEX_INV19_RIGLA_Alter201104", doc.InnerXml);
			rep.BindDataSource("Inv19_DS_Table0", 0);
			rep.BindDataSource("Inv19_DS_Table1", 1);
			rep.BindDataSource("Inv19_DS_Table2", 2);
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
			get { return "INVENTORY_SVED"; }
		}

		public string ReportName
		{
			get { return "ИНВ-19 (Ригла) c 01.04.2011: Сличительная ведомость результатов инвентаризации товарно-материальных ценностей"; }
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

	}
}
