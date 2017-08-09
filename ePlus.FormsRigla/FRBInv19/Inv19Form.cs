using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Inv19_Rigla
{
  public partial class Inv19Form : Form
  {
    public Inv19Form()
    {
      InitializeComponent();
    }

    public Int16 IsSal
    {
      get { return rbSAL.Checked ? (Int16)1 : (Int16)2; }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(
        string.Format("Будут использоваться системные константы,{0}при отсутствии которых по умолчанию отчет будет выведен в РОЗНИЧНЫХ ценах", Environment.NewLine), 
        "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
          DialogResult = DialogResult.Cancel;
    }
  }
}