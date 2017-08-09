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

namespace FCChActRevaluation21
{
    public class ActRevaluation21 : AbstractDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_REVALUATION_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActRevaluation21.rdlc");

            rep.LoadData("REPEX_ACT_REVALUATION_21", doc.InnerXml);
            rep.BindDataSource("ACT_REVALUATION_21_DS_Table", 0);
            rep.BindDataSource("ACT_REVALUATION_21_DS_Table1", 1);
            //rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL",System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
			return rep;
		}

		static decimal AsDecimal(object value)
		{
			return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
		}

        public override string PluginCode
		{
			get { return "REVALUATION"; }
		}


        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string ReportName
		{
			get { return "Акт переоценки АП-21"; }
		}
	}

}
