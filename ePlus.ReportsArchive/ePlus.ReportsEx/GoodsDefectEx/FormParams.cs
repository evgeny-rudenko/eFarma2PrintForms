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

namespace GoodsDefectEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "DOC", 0);
            Utils.AddNode(root, "ORDER_BY", rbSortName.Checked ? 0 : 1);
            ucPluginMulti_Store.AddItems(root, "ID_STORE");
            ucPluginMulti_Contractor.AddItems(root, "ID_CONTRACTOR");

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


            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            //rep.Text = "Отчет \'Фальсификаты на остатках\'";
            //rep.ReportPath = Utils.AppDir("GOODS_DEFECT_EX.rdlc");
            rep.LoadData("GOODS_DEFECT_EX", doc.InnerXml);
            rep.BindDataSource("GOODS_DEFECT_DS_Table", 0);
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