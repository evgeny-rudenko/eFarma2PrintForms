using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InvoiceBookEx
{
  public partial class InvoiceBookExParams : ExternalReportForm, IExternalReportFormMethods 
  {
    public InvoiceBookExParams()
    {
      InitializeComponent();
    }

    private string GetStoreString()
    {
      string storesValues = string.Empty;
      foreach (DataRowItem item in mpsStore.Items)
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

    private string GetContractorString()
    {
      string storesValues = string.Empty;
      foreach (DataRowItem item in mpsContractor.Items)
      {
        if (string.IsNullOrEmpty(storesValues))
          storesValues = item.Text;
        else
          storesValues = string.Format("{0}, {1}", storesValues, item.Text);
      }

      if (string.IsNullOrEmpty(storesValues))
        storesValues = "Все контрагенты";
      return storesValues;
    }

    public void Print(string[] reportFiles)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      Utils.AddNode(root, "DETAIL", chkDetail.Checked);      
      foreach (DataRowItem dr in mpsContractor.Items)
        Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

      foreach (DataRowItem dr in mpsStore.Items)
        Utils.AddNode(root, "ID_STORE", dr.Id);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];
      rep.LoadData("REPEX_INVOICE_BOOK", doc.InnerXml);

      rep.AddParameter("contractor", GetContractorString());
      rep.AddParameter("store", GetStoreString());
      rep.AddParameter("date_fr", period.DateFrText);
      rep.AddParameter("date_to", period.DateToText);
      rep.AddParameter("detail", chkDetail.Checked?"True":"False");

      rep.BindDataSource("InvoiceBookEx_DS_Table0", 0);
      rep.BindDataSource("InvoiceBookEx_DS_Table1", 1);

      rep.ExecuteReport(this);
    }

    
    public string ReportName
    {
      get { return "Завозная книга"; }
    }
  }
}