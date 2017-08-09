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

namespace InvoiceOutTorg13
{
    public class InvoiceOutTorg13 : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutTorg13.rdlc");

            rep.LoadData("REPEX_INVOICE_OUT_TORG_13", doc.InnerXml);
            rep.BindDataSource("INVOICE_OUT_TORG_13_DS_Table0", 0);
            rep.BindDataSource("INVOICE_OUT_TORG_13_DS_Table1", 1);
						
			decimal sum = 0.0M;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
			{
				sum += Utils.GetDecimal(dataSet.Tables[1].Rows[i]["SUM_SUP"]);
			}

            ReportParameter[] parameters = new ReportParameter[2] 
			{
				new ReportParameter("sum", RusCurrency.Str((double) sum)),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
            };

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

        public override string PluginCode
		{
			get { return "INVOICE_OUT"; }
		}
        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
			get { return "ÒÎÐÃ-13"; }
		}
	}

}
