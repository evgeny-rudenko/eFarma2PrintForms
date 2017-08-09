using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InvoiceInvoice
{
  public partial class InvoiceForm : Form
  {
    public InvoiceForm()
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