using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCChInv19_Rigla_300111_020211
{
  public partial class Inv19_Rigla_300111_020211Form : Form
  {
      public Inv19_Rigla_300111_020211Form()
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
        string.Format("����� �������������� ��������� ���������,{0}��� ���������� ������� �� ��������� ����� ����� ������� � ��������� �����", Environment.NewLine), 
        "��������", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
          DialogResult = DialogResult.Cancel;
    }
  }
}