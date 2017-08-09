using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCBInvoiceInvoice_Rigla
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
    public string FIODir
    {
        get { return FIODirTextBox.Text; }
        set { FIODirTextBox.Text = value; }
    }
    public string FIOBuh
    {
        get { return FIOBuhTextBox.Text; }
        set { FIOBuhTextBox.Text = value; }
    }/*
    public string DoverDir
    {
        get { return DoverDirTextBox.Text; }
        set { DoverDirTextBox.Text = value; }
    }
    public string DoverBuh
    {
        get { return DoverBuhTextBox.Text; }
        set { DoverBuhTextBox.Text = value; }
    }*/
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