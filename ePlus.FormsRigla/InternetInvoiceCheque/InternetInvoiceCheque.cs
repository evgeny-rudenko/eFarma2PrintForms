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
using ePlus.MetaData.Server;

namespace InternetInvoiceCheque
{
    public class InternetInvoiceCheque : AbstractDocumentPrintForm, IExternalDocumentPrintForm
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            DataService_BL bl = new DataService_BL();
            ReportFormNew rep = new ReportFormNew();
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = reportFiles[0];
            rep.DataSource = bl.Execute(string.Format("REPEX_INTERNET_INVOICE_CHEQUE '{0}'", doc.InnerXml));
            rep.BindDataSource("InternetInvoiceCheque_DS_Table0", 0);
            rep.BindDataSource("InternetInvoiceCheque_DS_Table1", 1);
            List<ReportParameter> par = new List<ReportParameter>();
            par.Add(new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName));
            rep.ReportViewer.LocalReport.SetParameters(par);
            return rep;
        }
        public override string PluginCode
		{
            get { return "INTERNET_ORDER"; }
		}

        public override string ReportName
		{
			get { return "Товарный чек"; }
		}

        public override string GroupName
		{
			get { return string.Empty; }
		}
	}
}

