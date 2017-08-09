using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;

namespace SumSupAndDiscountForRigla
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
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("SUM_SUP_AND_DISCOUNT_EX", doc.InnerXml);
            rep.BindDataSource("ForRigla_DS_Table1", 0);
            rep.BindDataSource("ForRigla_DS_Table2", 1);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Отчет для Риглы"; }
        }
    }
}