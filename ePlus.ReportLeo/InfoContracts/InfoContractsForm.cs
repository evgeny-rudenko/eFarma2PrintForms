﻿using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InfoContracts
{
    public partial class InfoContractsForm : ExternalReportForm, IExternalReportFormMethods
    {
        private const string text = "Сведения по контрактам";

        public InfoContractsForm()
        {
            InitializeComponent();
            Text = text;
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            get { return text; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            foreach (DataRowItem row in ucContracts.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTS_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INFO_CONTRACTS", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());

            rep.BindDataSource("InfoContracts_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}