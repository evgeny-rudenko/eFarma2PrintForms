using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace FCSSupplyProductsRyazan
{
    public class FCSSupplyProducts : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "SupplyProducts.rdlc");

            rep.LoadData("REPEX_SUPPLY_PRODUCTS_RYAZAN", doc.InnerXml);
            rep.BindDataSource("SupplyProducts_DS_Table0", 0);
            rep.BindDataSource("SupplyProducts_DS_Table1", 1);

			ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL",System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
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
            get { return "Договор на поставку продукции"; }
		}
	}
}
