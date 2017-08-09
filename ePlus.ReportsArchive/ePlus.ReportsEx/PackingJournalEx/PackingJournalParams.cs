using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;

namespace PackingJournalEx
{
	public partial class PackingJournalParams : ExternalReportForm, IExternalReportFormMethods
	{
		public PackingJournalParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			string contractors = String.Empty;
			string stores = String.Empty;

			foreach (DataRowItem contractor in ucContractor.Items)
			{
				Utils.AddNode(root, "ID_CONTRACTOR", contractor.Id);
				contractors = contractors + contractor.Text + ',';
			}

			foreach (DataRowItem store in ucStore.Items)
			{
				Utils.AddNode(root, "ID_STORE", store.Id);
				stores = stores + store.Text + ',';
			}
		
			ucPeriod.AddValues(root);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_PACKING_JOURNAL", doc.InnerXml);
			rep.BindDataSource("PackingJournal_DS_Table", 0);

			rep.AddParameter("CONTRACTOR", contractors);
			rep.AddParameter("store", stores);
			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Фасовочный журнал"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucContractor.Items.Clear();
			ucStore.Items.Clear();
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description;}
        }        
	}
}