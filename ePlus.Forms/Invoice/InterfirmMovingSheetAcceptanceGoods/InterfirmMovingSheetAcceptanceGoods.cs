﻿using System;
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

namespace FCChInterfirmMovingSheetAcceptanceGoods
{
    public class InterfirmMovingSheetAcceptanceGoods : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INTERFIRM_MOVING", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InterfirmMovingSheetAcceptanceGoods.rdlc");
            rep.LoadData("REPEX_INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS ", doc.InnerXml);
            rep.BindDataSource("INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS_DS_Table", 0);
            rep.BindDataSource("INTERFIRM_MOVING_SHEET_ACCEPTANCE_GOODS_DS_Table1", 1);

			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            string file = Path.Combine(cachePath, "InterfirmMovingSheetAcceptanceGoods.rdlc");
            ReportParameter p1 = new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1});
			return rep;
		}

        public override string PluginCode
		{
            get { return "INTERFIRM_MOVING"; }
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
