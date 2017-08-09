using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Xml;
using System.IO;

namespace ReturnToContractor
{
    public partial class ControlReturnToContractorForm : ExternalReportForm, IExternalReportFormMethods
    {
        public ControlReturnToContractorForm()
        {
            InitializeComponent();
            ClearValues();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (DataRowItem dr in mpsContractor.Items)
                Utils.AddNode(root, "ID_CONTRACTOR_FROM", dr.Id);

            foreach (DataRowItem dr in mpsStore.Items)
                Utils.AddNode(root, "ID_STORE", dr.Id);
            
            ReportFormNew rep = new ReportFormNew();

            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "ReturnToContractor.rdlc");
            rep.LoadData("REPEXT_RETURN_TO_CONTRACTOR", doc.InnerXml);
            rep.BindDataSource("ReturnToContractor_DS_RETURN_TO_CONTRACTOR_TABLE", 0);

            rep.AddParameter("timeSpan", "c " + ucPeriod.DateFrText + " по " + ucPeriod.DateToText);
            rep.AddParameter("contractors", mpsContractor.TextValues());
            rep.AddParameter("store", mpsStore.TextValues());

            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Возвраты поставщикам"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void ClearValues()
        {
            DateTime now = DateTime.Now;
            ucPeriod.DateFrom = now.AddDays(-13);
            ucPeriod.DateTo = now;

            mpsContractor.Items.Clear();
            mpsStore.Items.Clear();
        }
    }
}