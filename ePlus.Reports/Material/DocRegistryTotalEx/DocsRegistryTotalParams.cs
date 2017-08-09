using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using ePlus.CommonEx.Reporting;

namespace RCSDocRegistry
{
	public partial class DocsRegistryTotalParams : ExternalReportForm, IExternalReportFormMethods
	{		
        public DocsRegistryTotalParams()
		{
			InitializeComponent();
		}
		private class DocState
		{
			private string state;
			private string name;

			public string State
			{
				get { return state; }
				set { state = value; }
			}

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public DocState(string state, string name)
			{
				this.state = state;
				this.name = name;
			}
			public override string ToString()
			{
				return name;
			}
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			XmlNode docTypeNode = Utils.AddNode(root, "DOC_TYPES");
			foreach (DataRowItem td in ucTypesDocument.Items)
				Utils.AddNode(docTypeNode, "DOC_TYPE", td.Code);

			if (docStateCheckedListBox.CheckedItems.Count != 0)
			{
				XmlNode docStateNode = Utils.AddNode(root, "DOC_STATES");
				foreach (DocState state in docStateCheckedListBox.CheckedItems)
				{
					Utils.AddNode(docStateNode, "DOC_STATE", state.State);
				}
			}

			XmlNode contractorsFromNode = Utils.AddNode(root, "CONTRACTORS_FROM");
			ucContractorFrom.AddItems(contractorsFromNode, "ID_CONTRACTOR");

			XmlNode contractorsToNode = Utils.AddNode(root, "CONTRACTORS_TO");
			ucContractorTo.AddItems(contractorsToNode, "ID_CONTRACTOR");

			XmlNode storesFromNode = Utils.AddNode(root, "STORES_FROM"); 
			ucStoreFrom.AddItems(storesFromNode, "ID_STORE");

			XmlNode storesToNode = Utils.AddNode(root, "STORES_TO");
			ucStoreTo.AddItems(storesToNode, "ID_STORE");

			XmlNode goodsNode = Utils.AddNode(root, "GOODS");
			ucGoods.AddItems(goodsNode, "ID_GOOD");

			Utils.AddNode(root, "SORT_DOC", sortComboBox.SelectedIndex);							
						
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			if (repTypeComboBox.SelectedIndex == 0)
			{
				rep.LoadData("REPEX_DOCS_REGISTRY", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REPEX_DOCS_REGISTRY_SAL", doc.InnerXml);
			}
			
			rep.BindDataSource("DocsRegistry_DS_Table", 0);

			StringBuilder period = new StringBuilder("с ");
			period.Append(ucPeriod.DateFrText);
			period.Append(" по ");
			period.Append(ucPeriod.DateToText);
			period.Append(" (");
			period.Append(ucPeriod.DateTo.Subtract(ucPeriod.DateFrom).Days + 1);
			period.Append(" дней)");

			rep.AddParameter("PERIOD", period.ToString());			
			rep.AddParameter("GOODS", ucGoods.TextValues());
			rep.AddParameter("ROW_COUNT", rep.DataSource.Tables[0].Rows.Count.ToString());
			rep.AddParameter("showSupSum", showCheckedListBox.GetItemChecked(0).ToString());
			rep.AddParameter("showSupVat", showCheckedListBox.GetItemChecked(1).ToString());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}


		public string ReportName
		{
			get { return "Реестр документов"; }
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

			docStateCheckedListBox.SetItemChecked(1, true);
			sortComboBox.SelectedIndex = 0;

			showCheckedListBox.SetItemChecked(0, false);
			showCheckedListBox.SetItemChecked(1, false);

			ucTypesDocument.Items.Clear();
			ucContractorFrom.Items.Clear();
			ucContractorTo.Items.Clear();
			ucStoreFrom.Items.Clear();
			ucStoreTo.Items.Clear();
			ucGoods.Items.Clear();

			repTypeComboBox.SelectedIndex = 0;
		}

        private void DocsRegistryTotalParams_Load(object sender, EventArgs e)
        {
            ucTypesDocument.AllowSaveState = true;
            ucContractorFrom.AllowSaveState = true;
            ucContractorTo.AllowSaveState = true;
            ucStoreFrom.AllowSaveState = true;
            ucStoreTo.AllowSaveState = true;
            ucGoods.AllowSaveState = true;

            DocState[] docStates = new DocState[] {
				new DocState("SAVE", "Сохранен"),
				new DocState("PROC", "Отработан"),
				new DocState("DEL", "Удален")
			};

            docStateCheckedListBox.Items.AddRange(docStates);

            ClearValues();
        }
	}



}