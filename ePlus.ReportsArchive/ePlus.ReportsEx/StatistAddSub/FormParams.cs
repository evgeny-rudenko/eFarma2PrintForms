using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
//using Microsoft.Reporting.WinForms;
using System.IO;

namespace StatistAddSub
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
            XmlNode doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
            Utils.AddNode(root, "DOCUMENTS", 0);
            
            foreach (DOC item in data_docs)
            {
                if (item.IsAdd || item.IsSub)
                {
                    XmlNode exp = Utils.AddNode(root, "EXP");
                    Utils.AddNode(exp, "CODE_OP", item.Code_op);
                    Utils.AddNode(exp, "IS_INV", item.IsAdd);
                    Utils.AddNode(exp, "IS_EXP", item.IsSub);
                }
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("STATIST_ADD_SUB_REP_EX", doc.InnerXml);
            rep.BindDataSource("StatistAddSub_DS_Table1", 0);
            rep.AddParameter("Date_From", period.DateFrText);
            rep.AddParameter("Date_To", period.DateToText);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Справка о доходах и расходах"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
            }
        }
        
        private void FormParams_Load(object sender, EventArgs e)
        {
            CreateStoredProc("StatistAddSub.StatistAddSubRep.sql");
            SetDataGridView();
        }

        private List<DOC> data_docs = new List<DOC>();
        private void SetDataGridView()
        {
            DataService_BL bl = new DataService_BL();
            DataSet ds = bl.Execute("EXEC USP_TABLE_DATAS");
            SqlLoader<DOC> tdLoader = new SqlLoader<DOC>();
            data_docs = tdLoader.GetList(ds.Tables[0]);                      

            documents.ColumnHeadersVisible  =true;
            documents.AllowUserToAddRows = false;
            documents.AllowUserToResizeRows = false;
            documents.RowHeadersVisible = false;
            documents.MultiSelect = false;
            documents.ReadOnly = false;
            documents.DataSource = data_docs;
            documents.Columns["Description"].Resizable = DataGridViewTriState.True;
            documents.Columns["Description"].HeaderText = "Описание";
            documents.Columns["Description"].Width = 200;
            documents.Columns["IsAdd"].Resizable = DataGridViewTriState.False;
            documents.Columns["IsAdd"].Width = 60;
            documents.Columns["IsAdd"].HeaderText = "Доход";
            documents.Columns["IsSub"].Resizable = DataGridViewTriState.False;
            documents.Columns["IsSub"].Width = 60;
            documents.Columns["IsSub"].HeaderText = "Расход";
            documents.Columns["Code_op"].Visible = false;
        }
    }

    internal class DOC
    {
        private string code_op;
        private string description;
        private bool isAdd;
        private bool isSub;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsAdd
        {
            get { return isAdd; }
            set { isAdd = value; }
        }

        public bool IsSub
        {
            get { return isSub; }
            set { isSub = value; }
        }

        public string Code_op
        {
            get { return code_op; }
            set { code_op = value; }
        }

    }
}