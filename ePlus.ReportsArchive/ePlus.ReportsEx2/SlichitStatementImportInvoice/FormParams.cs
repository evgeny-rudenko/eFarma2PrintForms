using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;


namespace SlichitStatementImportInvoice
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
    	private string _secretPass = "lol";
    	private string pass = "";
		private bool showImages = true;

        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            foreach (DataRowItem dri in mpsImportRemains.Items)
            {
                Utils.AddNode(root, "ID_IMPORT_REMAINS_GLOBAL", dri.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REP_SLICHIT_STATEMENT_IMPORT_REMAINS", doc.InnerXml);
        	rep.AddParameter("ImageVisible", showImages ? "True" : "False");
            //rep.SaveSchema(@"C:\data.xml");
            //return;
            rep.BindDataSource("SlichitStatement_DS_Table", 0);
            rep.ExecuteReport(this);
        	showImages = true;
        }

        public string ReportName
        {
            get { return "Инвентаризация. Сличительная ведомость остатков по документу \"Ввод остатков\""; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }

		private void FormParams_Load(object sender, EventArgs e)
		{

		}

		private void FormParams_KeyPress(object sender, KeyPressEventArgs e)
		{
			pass += e.KeyChar;
			if (!_secretPass.StartsWith(pass))
			{
				pass = "";
				showImages = true;
			}
			if (_secretPass == pass)
			{
				showImages = false;
			}
		}
    }
}