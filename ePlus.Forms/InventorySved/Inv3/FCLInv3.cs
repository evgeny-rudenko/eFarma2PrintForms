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
using ePlus.MetaData.Server;
using Microsoft.Reporting.WinForms;

namespace FCLInv3
{
    public class Inv3Form : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
		void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCLInv3.Inv3.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCLInv3.FCLInv3.rdlc");
            using (StreamReader sr = new StreamReader(s))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "FCLInv3.rdlc")))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            ReportFormNew rep = new ReportFormNew();
            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "FCLInv3.rdlc");
            rep.LoadData("REPEX_INV3", doc.InnerXml);
            rep.BindDataSource("Inv3_DS_Table0", 0);
            rep.BindDataSource("Inv3_DS_Table1", 1);
            ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
            return rep;
		}

		private void AddParameter(DataSet ds, Type type, string paramName, object val)
		{
			string tname = "parameters";
			DataTable ptab = ds.Tables[tname];
			if (ptab == null)
			{
				ptab = new DataTable(tname);
				ds.Tables.Add(ptab);
			}

			DataColumn col = ptab.Columns[paramName];
			if (col == null)
			{
				col = new DataColumn(paramName, type);
				ptab.Columns.Add(col);
			};
			DataRow dr = ptab.Rows.Count == 0 ? ptab.NewRow() : ptab.Rows[0];
			dr[paramName] = val;
			if (ptab.Rows.Count == 0)
				ptab.Rows.Add(dr);
		}

        public override string PluginCode
		{
			get { return "INVENTORY_SVED"; }
		}

		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
            ExtractReport();
		}

        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
			get { return "ИНВ-3 Инвентаризационная опись товарно-материальных ценностей"; }
		}
	}
}
