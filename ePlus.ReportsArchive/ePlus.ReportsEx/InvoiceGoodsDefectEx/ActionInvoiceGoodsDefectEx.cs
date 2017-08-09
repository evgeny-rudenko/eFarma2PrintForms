using System;
using System.Collections.Generic;
using ePlus.CommonEx.Reporting;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.MetaData;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace InvoiceGoodsDefectEx
{
	public class ActionInvoiceGoodsDefectEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
  
	  const string CACHE_FOLDER = "Cache";
	  string connectionString;
	  string folderPath;
    	private XmlDocument m_doc;
//		private DataSet ds;

	  void CreateStoredProc(string connectionString)
	  {
		  Stream s = this.GetType().Assembly.GetManifestResourceStream("FKCInvoiceGoodsDefectEx_1.REP_INVOICE_GOODS_DEFECT.sql");

		  using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
		  {
			  string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

			  SqlCommand comm = null;
			  foreach (string statement in batch)
			  {
				  if (statement == string.Empty)
					  continue;

				  using (SqlConnection con = new SqlConnection(connectionString))
				  {
					  comm = new SqlCommand(statement, con);
					  con.Open();
					  comm.ExecuteNonQuery();
				  }
			  }
		  }
	  }

	  void ExtractReport()
	  {
		  string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
		  if (!Directory.Exists(cachePath))
			  Directory.CreateDirectory(cachePath);
		  Stream s = this.GetType().Assembly.GetManifestResourceStream("FKCInvoiceGoodsDefectEx_1.FKCInvoiceGoodsDefectMain.rdlc");
//		  Stream ss = this.GetType().Assembly.GetManifestResourceStream("FKCInvoiceGoodsDefectEx_1.InvoiceGoodsDefectSub.rdlc");

		  using (StreamReader sr = new StreamReader(s))
		  {
			  using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "FKCInvoiceGoodsDefectMain.rdlc")))
			  {
				  sw.Write(sr.ReadToEnd());
			  }
		  }
//		  using (StreamReader sr = new StreamReader(ss))
//		  {
//			  using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "InvoiceGoodsDefectSub.rdlc")))
//			  {
//				  sw.Write(sr.ReadToEnd());
//			  }
//		  }
	  }
		
	//	private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
	//	{
	//		e.DataSources.Add(new ReportDataSource("InvoiceGoodsDefect_DS_Table1", ds.Tables[1]));
	//	}

	  public override IReportForm GetReportForm(DataRowItem dataRowItem)
	  {

		  //===============
		  LicenseChecker.IsModuleLicensed(LicensedModules.GoodsDefect);

		  XmlDocument doc = new XmlDocument();
		  XmlNode root = Utils.AddNode(doc, "XML");

		  XmlNode invoiceNode = Utils.AddNode(root, "INVOICE");
		  Guid g = dataRowItem.Guid;
		  Utils.AddNode(invoiceNode, "ID_INVOICE_GLOBAL", g);

		  XmlDocument v_XMLdoc = new XmlDocument();
		  try  // Если документа нет или он не парсится
		  {
			  v_XMLdoc.LoadXml(SecurityContextEx.Context.User.Xml_data);
			  v_XMLdoc.SelectNodes(v_XMLdoc.DocumentElement.Name + "/DEFECT_SETTINGS");
		  }
		  catch (Exception)
		  {
			  string v_XMLstr = "<XML>" +
						   "<DEFECT_SETTINGS>" +
							 "<IS_ACTIVE>" +
							 "1" +
							 "</IS_ACTIVE>" +
							  "<SETTINGS>" +
								"<ID_STORE>" +
								"" +
								"</ID_STORE>" +
								"<LENGTH_DRUGTXT>" +
								"4" +
								"</LENGTH_DRUGTXT>" +
								"<LENGTH_SERIES>" +
								"4" +
								"</LENGTH_SERIES>" +
								"<DATA_SERVICEABLE>" +
								"Нет" +
								"</DATA_SERVICEABLE>" +
								"<CHECKREMAINS>" +
								"Нет" +
								"</CHECKREMAINS>" +
							  "</SETTINGS>" +
							"</DEFECT_SETTINGS>" +
							"</XML>";
			  v_XMLdoc.LoadXml(v_XMLstr);
		  }; // catch (Exception e)

		  XmlNode vXMLdocNode = doc.CreateElement("DEFECT_SETTINGS");
		  vXMLdocNode.InnerXml = v_XMLdoc.SelectSingleNode(v_XMLdoc.DocumentElement.Name + "/DEFECT_SETTINGS").InnerXml;
		  XmlNode vroot = doc.DocumentElement;
		  vroot.AppendChild(vXMLdocNode);

		  //===============


		  ReportFormNew rep = new ReportFormNew();
		  //ds = rep.DataSource;
		  //rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
		  rep.Text = ReportName;
		  rep.LoadData("REP_INVOICE_GOODS_DEFECT_EX", doc.InnerXml);
		  rep.BindDataSource("FKCInvoiceGoodsDefect_DS_Table0", 0);
//		  rep.BindDataSource("InvoiceGoodsDefect_DS_Table1", 1);		  

		  string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
		  string file = Path.Combine(cachePath, "FKCInvoiceGoodsDefectMain.rdlc");
		  rep.ReportViewer.LocalReport.ReportPath = file;

//		  rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
		  rep.ReportFormName = "Фальсификаты";
		  return rep;
	  }

	  public string PluginCode
	  {
		  get { return "INVOICE"; }
	  }

	  public void Execute(string connectionString, string folderPath)
	  {
		  this.connectionString = connectionString;
		  this.folderPath = folderPath;
		  CreateStoredProc(this.connectionString);
		  ExtractReport();
	  }

	  public string GroupName
	  {
		  get { return string.Empty; }
	  }

	  public string ReportName
	  {
		  get { return "Проверка на брак"; }
	  }
  }
}
