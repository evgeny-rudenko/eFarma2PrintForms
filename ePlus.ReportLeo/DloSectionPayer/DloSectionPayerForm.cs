using System;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace DloSectionPayer
{
    public partial class DloSectionPayerForm : ExternalReportForm, IExternalReportFormMethods
    {
    	private const string text = "Реализация по ДЛО в разрезе покупателей";

		public DloSectionPayerForm()
        {
            InitializeComponent();
			Text = text;
			ucDate.Value = DateTime.Now;
        }

        public string ReportName
        {
			get { return text; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
			Utils.AddNode(root, "DATE", Utils.SqlDate(ucDate.Value));
            ucSupplier.AddItems(root, "ID_CONTRACTOR");
			ucStore.AddItems(root, "ID_STORE");
			foreach (DataRowItem row in ucProgram.Items)
			{
				Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
			}
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
			rep.LoadData("REPEX_DLO_SECTION_PAYER", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
			rep.AddParameter("DATE", ucDate.Value.ToShortDateString());
            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucSupplier.TextValues());
			rep.AddParameter("PROGRAM", ucProgram.TextValues());

			rep.BindDataSource("DloSectionPayer_DS_Table", 0);
			rep.BindDataSource("DloSectionPayer_DS_Table1", 1);
			rep.ExecuteReport(this);
        }
    }
}