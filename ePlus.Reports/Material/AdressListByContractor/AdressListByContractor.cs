using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;

namespace AdressListByContractor
{
    public partial class AdressListByContractorParams : ExternalReportForm, IExternalReportFormMethods
    {
        public AdressListByContractorParams()
        {
            InitializeComponent();

			ClearValues();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "ID_CONTRACTOR", ucMetaPluginSelect1.Id);							


            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "AdressListByContractor.rdlc");

            rep.LoadData("REPEX_ADRESSLIST_BY_CONTRACTOR", doc.InnerXml);

            //rep.SaveSchema("c:\\schema.xml");
            //doc.Save("c:\\test.xml");
            rep.BindDataSource("InvoiceOut_AdressList_DS_Table1", 0);

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Адреса контрагента (КФ)"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
 		}

    }
}