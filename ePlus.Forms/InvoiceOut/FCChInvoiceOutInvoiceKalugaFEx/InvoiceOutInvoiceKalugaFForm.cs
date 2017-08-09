using System;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace FCChInvoiceOutInvoice_KalugaFEx
{
  public partial class InvoiceOutInvoiceKalugaFForm : Form
  {
      public InvoiceOutInvoiceKalugaFForm()
    {
      InitializeComponent();
    }

    public string Number
    {
      get { return numberTextBox.Text; }
      set { numberTextBox.Text = value; }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
  }
}