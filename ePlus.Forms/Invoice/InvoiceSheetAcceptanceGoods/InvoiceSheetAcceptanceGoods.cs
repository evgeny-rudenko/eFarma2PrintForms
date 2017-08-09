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

namespace InvoiceSheetAcceptanceGood
{
    public class InvoiceSheetAcceptanceGoods: AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        /*

        	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            /////////////////////
            ReportFormNew rep = new ReportFormNew();

            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceTORG12.rdlc");

            rep.LoadData("DBO.REPEX_LIST_RETURN_TO_CONTRACTOR", doc.InnerXml);
            rep.BindDataSource("ListReturnToContractor_DS_DataTable0", 0);
            rep.BindDataSource("ListReturnToContractor_DS_DataTable1", 1);

            decimal sum = 0m;
            foreach (DataRow row in rep.DataSource.Tables[0].Rows)
            {
                sum += Utils.GetDecimal(row, "SUM_CONTRACTOR_PRICE_VAT");
            }
            ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("COUNT_ROWS", RusCurrency.Str(rep.DataSource.Tables[0].Rows.Count, "NUM")),
                new ReportParameter("STR_SUMMORY", RusCurrency.Str((double) sum)),
                new ReportParameter("PAGE_COUNT", "0"),
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};

            rep.ReportViewer.LocalReport.SetParameters(parameters);

            return rep;
        }


	}
        */




        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_INVOICE", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceSheetAcceptanceGoods.rdlc");
            rep.LoadData("REPEX_INVOICE_SHEET_ACCEPTANCE_GOODS ", doc.InnerXml);
            rep.BindDataSource("INVOICE_SHEET_ACCEPTANCE_GOODS_DS_Table", 0);
            rep.BindDataSource("INVOICE_SHEET_ACCEPTANCE_GOODS_DS_Table1", 1);
            rep.BindDataSource("INVOICE_SHEET_ACCEPTANCE_GOODS_DS_Table2", 2);

			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            string file = Path.Combine(cachePath, "InvoiceSheetAcceptanceGoods.rdlc");
			rep.ReportViewer.LocalReport.ReportPath = file;
            /*
			decimal dblSummory = 0;
			decimal intCountRows = 0;
			foreach (DataRow row in rep.DataSource.Tables[1].Rows)
			{
				dblSummory += ePlus.MetaData.Core.Utils.GetDecimal(row, "RETAIL_SUM_VAT");
				intCountRows++;
			}
			// преобразование суммы в строку
			string strSummory = ePlus.CommonEx.Reporting.RusCurrency.Str((double) dblSummory);*/
            ReportParameter p1 = new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1});
			return rep;
		}

        public override string PluginCode
		{
			get { return "INVOICE"; }
		}

        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
			get { return "Ведомость приемки товара"; }
		}
	}
}
