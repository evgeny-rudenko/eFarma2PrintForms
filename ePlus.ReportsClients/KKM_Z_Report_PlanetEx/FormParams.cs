using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace RCRKKM_Z_Report_PlanetEx
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
            ucPeriod1.AddValues(root);
            if (ucMetaPluginSelect1.Id != 0)
                Utils.AddNode(root, "ID_CASH_REGISTER", ucMetaPluginSelect1.Id);
            if (ucMetaPluginSelect2.Id != 0)
                Utils.AddNode(root, "ID_CONTRACTOR", ucMetaPluginSelect2.Id);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];//Utils.AppDir("KKM_Z_Report.rdlc");
            rep.LoadData("REP_KKMZREPORT_PLANET", doc.InnerXml);
            rep.BindDataSource("KKM_Z_ReportDS_Table", 0);

            rep.AddParameter("date_fr", ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("detail", checkBox_detail.Checked ? "0" : "1");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ReportFormName = "Отчёт ККМ: 'Z-отчет за период'";
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Z-отчет (ООО Здоровая планета)"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.CashReports).Description;
            }
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            ucMetaPluginSelect1.SetId(0);
            ucMetaPluginSelect2.SetId(0);
            if (ucPeriod1 != null)
            {
                ucPeriod1.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod1.DateFrom = ucPeriod1.DateTo.AddDays(-13);
            }
        }
    }
}