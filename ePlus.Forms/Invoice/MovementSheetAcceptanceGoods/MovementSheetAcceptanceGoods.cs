using System.IO;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;

namespace FCChMovementSheetAcceptanceGoods
{
    public class MovementSheetAcceptanceGoods: AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_MOVEMENT", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "MovementSheetAcceptanceGoods.rdlc");
            rep.LoadData("REPEX_MOVEMENT_SHEET_ACCEPTANCE_GOODS ", doc.InnerXml);
            rep.BindDataSource("MOVEMENT_SHEET_ACCEPTANCE_GOODS_DS_Table", 0);
            rep.BindDataSource("MOVEMENT_SHEET_ACCEPTANCE_GOODS_DS_Table1", 1);

			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            string file = Path.Combine(cachePath, "MovementSheetAcceptanceGoods.rdlc");
            ReportParameter p1 = new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1});
			return rep;
		}

        public override string PluginCode
		{
            get { return "Movement"; }
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
