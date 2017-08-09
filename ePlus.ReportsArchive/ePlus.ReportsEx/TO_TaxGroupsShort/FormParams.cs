using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;

namespace TO_TaxGroupsShort
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
            if (ucPeriod != null)
            {
                ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
            }
        }
     
        public void Print(string[] reportFiles)
        {
            if (mpsContractor.Items.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML");
                Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
                Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
                Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? 1 : 0);
                Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
                if (rbDocType.Checked)
                    Utils.AddNode(root, "SORT_DOC", 1);  //по видам док
                else Utils.AddNode(root, "SORT_DOC", 0);  //по датам док

                foreach (DataRowItem dr in mpsContractor.Items)
                    Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

                foreach (DataRowItem dr in mpsStore.Items)
                    Utils.AddNode(root, "ID_STORE", dr.Id);

                if (chkRefreshDocMov.Checked)
                    Utils.AddNode(root, "REFRESH_DOC_MOV", 1);

                ReportFormNew rep = new ReportFormNew();
                rep.ReportPath = reportFiles[0];
                rep.LoadData("REP_TAX_GROUPS_SHORT_REP_EX", doc.InnerXml);

                //rep.AddParameter("date_fr", ucPeriod.DateFrText);
                //rep.AddParameter("date_to", ucPeriod.DateToText);

                rep.BindDataSource("TaxGroup_DS_Table", 0);
                rep.BindDataSource("TaxGroup_DS_Table1", 1);
                rep.BindDataSource("TaxGroup_DS_Table2", 2);
                rep.BindDataSource("TaxGroup_DS_Table3", 4);
                rep.BindDataSource("TaxGroup_DS_Table4", 5);
                rep.BindDataSource("TaxGroup_DS_Table5", 3);

                rep.AddParameter("date_fr", ucPeriod.DateFrText);
                rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
                rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
                rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
                rep.AddParameter("show_nal", chbShowNal.Checked ? "1" : "0");
                rep.ExecuteReport(this);
            }
            else MessageBox.Show("Выберите контрагента!");            
        }

        public string ReportName
        {
            get { return "Товарный отчет по налоговым группам(краткий)"; }
        }

        private void SetDefaultValues()
        {
            ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
            }
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            long i = this.IdContractorDefault;
            mpsContractor.AddItem(i);
        }
    }
}