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

namespace FCChChequeCertificate
{
    public class ChequeCertificate : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_CHEQUE_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ChequeCertificate.rdlc");

            rep.LoadData("REPEX_CHEQUE_CERTIFICATE", doc.InnerXml);
            rep.BindDataSource("CHEQUE_CERTIFICATE_DS_Table", 0);
            rep.BindDataSource("CHEQUE_CERTIFICATE_DS_Table1", 1);

			ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL",System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

        public override string PluginCode
		{
			get { return "CHEQUE"; }
		}
        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
            get { return "Сертификаты чеков"; }
		}
	}
}
