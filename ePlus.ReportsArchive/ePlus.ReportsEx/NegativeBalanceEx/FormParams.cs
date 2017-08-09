using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;
using ePlus.MetaData.Client;
using System.Xml;

namespace NegativeBalanceEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
        }

        #region IExternalReportFormMethods Members

        protected override void CreateStoredProc(string resourceName)
        {
            base.CreateStoredProc(resourceName);
        }
        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc,"XML");
            foreach (DataRowItem store in ucStores.Items)
                Utils.AddNode(root,"ID_STORE", store.Id);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("NEGATIVE_BALANCE", doc.InnerXml);
            rep.BindDataSource("NegativeBalance_DS_Table1", 0);
            rep.BindDataSource("NegativeBalance_DS_Table2", 1);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return " онтроль наличи€ отрицательных остатков"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

        #endregion
    }
}