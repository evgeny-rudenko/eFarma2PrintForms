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

namespace DocsRegistryTotalEx
{
	public partial class DocsRegistryParams : ExternalReportForm, IExternalReportFormMethods
	{
		public DocsRegistryParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			
			foreach (DataRowItem td in ucTypesDocument.Items)
				Utils.AddNode(root, "DOC_TYPE", td.Code);			
			
            foreach (DOC_STATE state in chkLbDocStatе.CheckedItems)
            {
                XmlNode docStateNode = Utils.AddNode(root, "DOC_STATE");
                Utils.AddNode(docStateNode, "CODE", state.CODE);
            }
			
			foreach (DataRowItem dri in ucStoreFrom.Items)
                Utils.AddNode(root, "STORE_FROM", dri.Id);

			foreach (DataRowItem dri in ucStoreTo.Items)
                Utils.AddNode(root, "STORE_TO", dri.Id);			

			foreach (DataRowItem dri in ucContractorFrom.Items)
                Utils.AddNode(root, "CONTRACTOR_FROM", dri.Id);

			foreach (DataRowItem dri in ucContractorTo.Items)
                Utils.AddNode(root, "CONTRACTOR_TO", dri.Id);

			foreach (DataRowItem dri in ucGoods.Items)
               Utils.AddNode(root, "GOODS", dri.Id);

           if (rbTypeDoc.Checked)
               Utils.AddNode(root, "SORT_DOC", 1);  //по видам док
           else Utils.AddNode(root, "SORT_DOC", 0);  //по датам док
            
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];								
			rep.LoadData("REPEX_DOCSREGISTRY_TOTAL_EX", doc.InnerXml);
			rep.BindDataSource("DocsRegistry_DS_Table", 0);

			rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("STORE", ucStoreFrom.TextValues());
			rep.AddParameter("CONTRACTOR", ucContractorFrom.TextValues());
			rep.AddParameter("GOODS", ucGoods.TextValues());

			foreach (SHOW_PARAM sp in (List<SHOW_PARAM>)chkLbShowParams.DataSource)
			{
				if (chkLbShowParams.CheckedItems.IndexOf(sp) == -1)			
					rep.AddParameter(sp.CODE, "0");			
				else			
					rep.AddParameter(sp.CODE, "1");			
			}
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Реестр документов общий"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
		}

		protected void SetDefaultValues()
		{
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		private void DocsRegistryParams_Load(object sender, EventArgs e)
		{
			//DataService_BL bl = new DataService_BL();
			//DataSet ds = bl.Execute("EXEC USP_EX_TABLE_DATA_LIST");
			//SqlLoader<TABLE_DATA> tdLoader = new SqlLoader<TABLE_DATA>();
			//List<TABLE_DATA> list = tdLoader.GetList(ds.Tables[0]);

			//ucDocuments.DescriptionMember = "DESCRIPTION";
			//ucDocuments.DisplayMember = "DESCRIPTION";
			//ucDocuments.KeyFieldName = "ID_TABLE_DATA";
			//ucDocuments.SearchMember = "DESCRIPTION";
			//ucDocuments.DataSource = list;

			chkLbDocStatе.DataSource = new List<DOC_STATE>(new DOC_STATE[]{new DOC_STATE("SAVE", "Сохранен"), 
                                                                     new DOC_STATE("PROC", "Отработан"),
                                                                     new DOC_STATE("DEL", "Удален")});

			chkLbShowParams.DataSource = new List<SHOW_PARAM>(new SHOW_PARAM[]{new SHOW_PARAM("SHOW_SUM_SUP", "Опт. сумма"),
                                                                         new SHOW_PARAM("SHOW_SVAT_SUP", "Сумма НДС пост."),
                                                                         new SHOW_PARAM("SHOW_TOTAL", "Итоги") });
			SetDefaultValues();
		}

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }
	}

	internal class TABLE_DATA
	{
		private long id_table_data;
		private string description;

		public long ID_TABLE_DATA
		{
			get { return id_table_data; }
			set { id_table_data = value; }
		}

		public string DESCRIPTION
		{
			get { return description; }
			set { description = value; }
		}
	}
	internal class DOC_STATE
	{
		private string code;
		private string name;

		public string CODE
		{
			get { return code; }
			set { code = value; }
		}

		public string NAME
		{
			get { return name; }
			set { name = value; }
		}
		public DOC_STATE(string code, string name)
		{
			this.code = code;
			this.name = name;
		}
		public override string ToString()
		{
			return name;
		}
	}
	internal class SHOW_PARAM
	{
		private string code;
		private string name;

		public string CODE
		{
			get { return code; }
			set { code = value; }
		}

		public string NAME
		{
			get { return name; }
			set { name = value; }
		}
		public SHOW_PARAM(string code, string name)
		{
			this.code = code;
			this.name = name;
		}
		public override string ToString()
		{
			return name;
		}        
	}
}