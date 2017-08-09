using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RCGConvertRiglaData
{
  public partial class ProgressStatusForm : Form
  {
    AsyncOperation man;
    public ProgressStatusForm()
    {
      InitializeComponent();
      man = AsyncOperationManager.CreateOperation(null);
    }

    public DoWorkEventHandler DoWork;
    private void ProgressStatusForm_Shown(object sender, EventArgs e)
    {
      worker.DoWork += DoWork;
      worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleted);
      worker.ProgressChanged += OnProgressChanged;
      ps = new ProgressState();
      ps.MainState = "Состояние импорта данных";
      worker.RunWorkerAsync();
      
    }

    private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
    {
       man.Post(delegate(object state)
                  {
                    ChangeStatus((ProgressChangedEventArgs)state);
                  }, e);
    }

    ProgressState ps;
    private void ChangeStatus(ProgressChangedEventArgs args)
    {
      if (args == null)
        return;
      string state = (string)args.UserState;
      if (string.IsNullOrEmpty(state)) return;
      if(args.ProgressPercentage==0)
        ps.MainState = state;
      else 
        ps.SubState = state;
      lStatus.Text = ps.ToString();
    }

    internal class ProgressState
    {
      private string mainState;
      private string subState;

      public string MainState
      {
        get { return mainState; }
        set
        {
          mainState = value;
          subState = string.Empty;
        }
      }

      public string SubState
      {
        get { return subState; }
        set { subState = value; }
      }

      public override string ToString()
      {
        return string.Format("{0} {1}", mainState, string.IsNullOrEmpty(subState) ? "..." : string.Format("({0})", subState));
      }
    }

    private Exception error;

    public Exception Error
    {
      get { return error; }
    }

    private void OnCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      try
      {
        if (e.Error != null)
        {
          error = e.Error;
          throw error;
        }
      }
      finally
      {
        this.Close();        
      }
    }
  }
}