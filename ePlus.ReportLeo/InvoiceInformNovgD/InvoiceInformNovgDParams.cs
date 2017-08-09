using System;
using System.Text;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;

namespace InvoiceInformNovgD
{
	public partial class InvoiceInformNosologParams : ExternalReportForm, IExternalReportFormMethods
	{
	

		public InvoiceInformNosologParams()
		{
			InitializeComponent();

			ucContractorTo.AllowSaveState = true;
            sortComboBox.SelectedIndex = 0;
            ucContracts.AllowSaveState = true;
            
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            XmlNode contractsList = Utils.AddNode(root, "CONTRACTS");
            foreach (DataRowItem ctr in ucContracts.Items)
                Utils.AddNode(contractsList, "ID_CONTRACTS", ctr.Guid);
			
			XmlNode contractorsFromNode = Utils.AddNode(root, "CONTRACTORS_TO");
			ucContractorTo.AddItems(contractorsFromNode, "ID_CONTRACTOR");
			
			XmlNode storesFromNode = Utils.AddNode(root, "STORES");
		
            
            //XmlNode goodsNode = Utils.AddNode(root, "GOODS");
          // ucGoods.AddItems(goodsNode, "ID_GOOD");
			
			Utils.AddNode(root, "SORT_DOC", sortComboBox.SelectedIndex);							
						
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INVOICE_INFORMNOVG_D", doc.InnerXml);
            
		    //rep.SaveSchema(@"c:\data.xml");
            //return; 
		    
		    rep.BindDataSource("INVOICEINFORMNOSOLOG_DS_Table", 0);
            rep.BindDataSource("INVOICEINFORMNOSOLOG_DS_Table1", 1);
            rep.BindDataSource("INVOICEINFORMNOSOLOG_DS_Table2", 2);
			StringBuilder period = new StringBuilder("с ");
			period.Append(ucPeriod.DateFrText);
			period.Append(" по ");
			period.Append(ucPeriod.DateToText);
			period.Append(" (");
			period.Append(ucPeriod.DateTo.Subtract(ucPeriod.DateFrom).Days + 1);
			period.Append(" дней)");

			rep.AddParameter("PERIOD", period.ToString());			
			rep.AddParameter("ROW_COUNT", rep.DataSource.Tables[0].Rows.Count.ToString());
			//rep.AddParameter("showSupSum", showCheckedListBox.GetItemChecked(0).ToString());
			//rep.AddParameter("showSupVat", showCheckedListBox.GetItemChecked(1).ToString());
          
            rep.AddParameter("CONTRACTORS", ucContractorTo.TextValues());
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);
            rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
			rep.ExecuteReport(this);
		}


		public string ReportName
		{
            get { return "Отчет о поставках ЛС(243-Д)-Д"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			ucContractorTo.Items.Clear();
            sortComboBox.SelectedIndex = 0;


        
		}
	}



}