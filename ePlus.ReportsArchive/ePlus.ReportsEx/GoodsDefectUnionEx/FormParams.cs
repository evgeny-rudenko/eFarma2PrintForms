using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;

namespace GoodsDefectUnionEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
			if (period != null)
			{
				period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				period.DateFrom = period.DateTo.AddDays(-13);
			}

        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "DOC", 0);
            Utils.AddNode(root, "ORDER_BY", rbSortName.Checked ? 0 : 1);
			Utils.AddNode(root, "LABELSTORE", ucPluginMulti_Store.TextValues());
			Utils.AddNode(root, "LABELCONTRACTOR", ucPluginMulti_Contractor.TextValues());
			Utils.AddNode(root, "DATE_FR", period.DateFrom);
			Utils.AddNode(root, "DATE_TO", period.DateTo);
            ucPluginMulti_Store.AddItems(root, "ID_STORE");
            ucPluginMulti_Contractor.AddItems(root, "ID_CONTRACTOR");
			ucPluginMulti_Invoice.AddItems(root, "ID_INVOICE");

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
			doc.SelectSingleNode(doc.DocumentElement.Name + "/DEFECT_SETTINGS/SETTINGS/ID_STORE").InnerText = "";
			if (checkBoxRemains.Checked)
			{
				doc.SelectSingleNode(doc.DocumentElement.Name + "/DEFECT_SETTINGS/SETTINGS/CHECKREMAINS").InnerXml = "Да";
			}
			else
			{
				doc.SelectSingleNode(doc.DocumentElement.Name + "/DEFECT_SETTINGS/SETTINGS/CHECKREMAINS").InnerXml = "Нет";
			};

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            //rep.Text = "Отчет \'Фальсификаты на остатках\'";
            //rep.ReportPath = Utils.AppDir("GOODS_DEFECT_EX.rdlc");

			rep.AddParameter("DATE_FR", period.DateFrText);
			rep.AddParameter("DATE_TO", period.DateToText);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

			rep.LoadData("GOODS_DEFECT_UNION_EX", doc.InnerXml);
            rep.BindDataSource("GOODS_DEFECT_DS_Table", 0);
			rep.BindDataSource("GOODS_DEFECT_DS_Table1", 1);
			rep.BindDataSource("GOODS_DEFECT_DS_Table2", 2);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Фальсификаты на остатках"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }        
    }
}