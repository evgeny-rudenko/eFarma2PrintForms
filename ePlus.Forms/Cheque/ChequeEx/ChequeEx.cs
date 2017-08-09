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

using ePlus.InternalBarcoding;
using System.Drawing;
using System.Drawing.Imaging;

namespace FCSCheque
{
    public class ChequeEx : AbstractDocumentPrintForm, IExternalDocumentPrintForm 
	{
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_CHEQUE_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;			
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "Cheque.rdlc");

			rep.LoadData("REPEX_CHEQUE", doc.InnerXml);
			rep.BindDataSource("GoodsCheque_DS_Table", 0);
			rep.BindDataSource("GoodsCheque_DS_Table1", 1);

			decimal summary = 0m;
			foreach (DataRow Row in rep.DataSource.Tables[0].Rows)
			{
				summary = Utils.GetDecimal(Row, "ITOGO");
			}
            if ((rep.DataSource.Tables.Count > 0))
            {
                string barcode = Convert.ToString((rep.DataSource.Tables[0].Rows[0]["BARCODE"]));
                byte[] bmpBin = null;
                if (!string.IsNullOrEmpty(barcode))
                {
                    EAN13Encoder encoder = new EAN13Encoder();
                    Image img = encoder.GenImage(barcode, 1, 50, 50, 125);
                    MemoryStream stream = new MemoryStream();
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(stream, ImageFormat.Bmp);
                    bmpBin = stream.ToArray();
                }
                DataTable tableParams = new DataTable();
                tableParams.Columns.Add("bar_code_2", typeof(byte[]));
                tableParams.Rows.Add(bmpBin);

                rep.ReportViewer.LocalReport.DataSources.Add(
                    new ReportDataSource("GoodsCheque_DS_parameters", tableParams));
            }
			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("SUMMA", RusCurrency.Str((double) summary)),
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
			get { return "Товарный чек"; }
		}
	}
}
