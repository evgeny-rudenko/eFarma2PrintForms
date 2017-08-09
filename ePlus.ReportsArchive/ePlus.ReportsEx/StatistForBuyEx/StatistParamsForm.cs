using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;


namespace StatistForBuyEx
{
    public partial class StatistParamsForm : ExternalReportForm, IExternalReportFormMethods
    {
        public StatistParamsForm()
        {
            InitializeComponent();
        }

        private string _rowCount;
        private string _document;
        private string _orderBy;
        private string _docType;        

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FR", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "IS_ES", chkES.Checked ? "1" : "0");
            Utils.AddNode(root, "DOC_DAYS", (int)nbRemDays.Value);
            Utils.AddNode(root, "MAX_DAYS", (int)nbOrderDays.Value);
            _rowCount = (string)rbAll.Tag;
            if (rb1000.Checked)
                _rowCount = (string)rb1000.Tag;
            if (rb500.Checked)
                _rowCount = (string)rb500.Tag;
            if (rb400.Checked)
                _rowCount = (string)rb400.Tag;
            if (rb300.Checked)
                _rowCount = (string)rb300.Tag;
            if (rb200.Checked)
                _rowCount = (string)rb200.Tag;
            if (rb100.Checked)
                _rowCount = (string)rb100.Tag;

            Utils.AddNode(root, "ROW_COUNT", _rowCount);

            _document = (string)rbAllDoc.Tag;
            if (rbOut.Checked) 
                _document = (string)rbOut.Tag;
            if (rbCheque.Checked) 
                _document = (string)rbCheque.Tag;            

            Utils.AddNode(root, "TYPE_DOC", _document);

            //_orderBy = (string)rbRem.Tag;
            //if (rbGoods.Checked) 
            //    _orderBy = (string)rbGoods.Tag;

            Utils.AddNode(root, "ORDER_BY", rbGoods.Checked ? "0" : "1");

            foreach (DataRowItem dr in stores.Items)
                Utils.AddNode(root, "STORE", dr.Id);            

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REP_STATIST_FORBUY_TOTAL_EX", doc.InnerXml);

            rep.BindDataSource("Statist_Forbuy_DS_Table", 0);

            long nCountDays = (Int64)period.DateTo.ToOADate() - (Int64)period.DateFrom.ToOADate() + 1;
            rep.AddParameter("Pm_StoreName", stores.Items.Count == 0 ? "По всем" : stores.ToCommaDelimetedStringList());
            rep.AddParameter("Pm_DateFrom", period.DateFrText);
            rep.AddParameter("Pm_DateTo", period.DateToText);
            rep.AddParameter("Pm_CountDays", nCountDays.ToString());
            rep.AddParameter("Pm_QtyDays", nbRemDays.Value.ToString());
            rep.AddParameter("Pm_MaxDays", nbOrderDays.Value.ToString());
            rep.AddParameter("Pm_RowCount", _rowCount == "0" ? "Все позиции" : _rowCount);

            _docType = rbAllDoc.Text;
            if (rbOut.Checked) _docType = rbOut.Text;
            if (rbCheque.Checked) _docType = rbCheque.Text;            

            rep.AddParameter("Pm_TypeDoc", _docType);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Статистика для закупок (общая)"; }
        }
    }
}