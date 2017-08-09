using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RCKABC_analysis;

namespace RCKABC_analysis.Controls
{
  public partial class GroupByClassifierControl : UserControl, IRequestParamMethods
  {
    public GroupByClassifierControl()
    {
      InitializeComponent();
    }

    public void Object2Control(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is GoodsClassifierParam)
        {
          chkGroup.Checked = ((GoodsClassifierParam)p).Group;
        }
      }
    }

    public void Control2Object(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is GoodsClassifierParam)
        {
          ((GoodsClassifierParam)p).Group = chkGroup.Checked;
        }
      }
    }

    public void ClearData()
    {
      chkGroup.Checked = false;
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[] { typeof(GoodsClassifierParam) }; }
    }

    public string Category
    {
      get { return "Группы товаров"; }
    }
  }
}
