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
using Microsoft.Reporting.WinForms;

namespace F14SInvoiceOutTorg12Saliks
{
    public class InvoiceOutTorg12NV : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE_OUT_GLOBAL", dataRowItem.Guid);
            ReportFormNew rep = new ReportFormNew();

            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutTORG12.rdlc");
            
            rep.LoadData("DBO.REPEX_INVOICE_OUT_TORG12", doc.InnerXml);
            rep.BindDataSource("InvoiceOutTorg12NV_DS_DataTable0", 0);
            rep.BindDataSource("InvoiceOutTorg12NV_DS_DataTable1", 1);
			decimal sum = 0;
			foreach (DataRow row in rep.DataSource.Tables[0].Rows)
			{
				sum += Utils.GetDecimal(row, "SUM_CONTRACTOR_PRICE_VAT");
			}
			
			string sumInText = ePlus.CommonEx.Reporting.RusCurrency.Str((double) sum);
			string countInText = ePlus.CommonEx.Reporting.RusCurrency.Str(rep.DataSource.Tables[0].Rows.Count, "NUM");
            ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("PAGE_COUNT", "0"),
                new ReportParameter("STR_SUMMORY", sumInText),
                new ReportParameter("COUNT_ROWS", countInText),
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
			get { return "“Œ–√-12(‡Î¸·ÓÏ.)"; }
		}
	}
}
