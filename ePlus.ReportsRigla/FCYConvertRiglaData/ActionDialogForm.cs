using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCYConvertRiglaData;

namespace RCYConvertRiglaData
{
  public partial class ActionDialogForm : Form
  {
    public ActionDialogForm()
    {
      InitializeComponent();
    }

    private LoadAction currentAction = LoadAction.OnlyCheck;

    public LoadAction CurrentAction
    {
      get { return currentAction; }
    }

    private void bOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void rbCheck_CheckedChanged(object sender, EventArgs e)
    {
      currentAction = LoadAction.OnlyCheck;
    }

    private void rbLoad_CheckedChanged(object sender, EventArgs e)
    {
      currentAction = LoadAction.CheckAndLoad;
    }
  }
}