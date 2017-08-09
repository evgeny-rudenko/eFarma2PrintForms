using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;

namespace BookPurchasesEx
{
	public partial class BookPurchasesParams : ExternalReportForm, IExternalReportFormMethods
	{
		public BookPurchasesParams()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}

		}

		private string GetStoreString()
		{
			string storesValues = string.Empty;
			foreach (DataRowItem item in ucContractors.Items)
			{
				if (string.IsNullOrEmpty(storesValues))
					storesValues = item.Text;
				else
					storesValues = string.Format("{0}, {1}", storesValues, item.Text);
			}

			if (string.IsNullOrEmpty(storesValues))
				storesValues = "Все склады";
			return storesValues;
		}

		public void Print(string[] reportFiles)
		{
            if (ucContractors.Items.Count == 0)           
                return;            
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);			
            
			foreach (DataRowItem dr in ucContractors.Items)
				Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);
			
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
			rep.LoadData("BOOK_PURCHASES", doc.InnerXml);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);			

			rep.BindDataSource("BookPurchases_DS_Table0", 0);
			rep.BindDataSource("BookPurchases_DS_Table1", 1);            
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Книга покупок"; }
		}

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
            }
        }
	}
}

