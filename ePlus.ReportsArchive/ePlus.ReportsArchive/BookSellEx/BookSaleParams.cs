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

namespace BookSaleEx
{
	public partial class BookSaleParams : ExternalReportForm, IExternalReportFormMethods
	{
		public BookSaleParams()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		public override void Execute(string connectionString, string folderPath)
		{
			base.Execute(connectionString, folderPath);
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

        //private string GetContractorString()
        //{
        //    string storesValues = string.Empty;
        //    if (string.IsNullOrEmpty(storesValues))
        //        storesValues = ucContractor.Text;
        //    else
        //        storesValues = string.Format("{0}, {1}", storesValues, ucContractor.Text);

        //    if (string.IsNullOrEmpty(storesValues))
        //        storesValues = "Все контрагенты";
        //    return storesValues;
        //}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            foreach (DataRowItem dr in ucContractors.Items)
                Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
			rep.LoadData("BOOK_SALE", doc.InnerXml);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);

			rep.BindDataSource("BookSale_DS_Table0", 0);
			rep.BindDataSource("BookSale_DS_Table1", 1);
            //string inn_kpp = string.Empty;
            //string buyer_name = string.Empty;
            //string buh_fio = string.Empty;
            //foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
            //{
            //    inn_kpp = Utils.GetString(Row, "BUYER_INN_KPP");
            //    buyer_name = Utils.GetString(Row, "BUYER_NAME");
            //    buh_fio = Utils.GetString(Row, "BUH_FIO");				
            //}
            //rep.AddParameter("inn_kpp", inn_kpp.ToString());
            //rep.AddParameter("buyer_name", buyer_name.ToString());
            //rep.AddParameter("buh_fio", buh_fio.ToString());
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Книга продаж"; }
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