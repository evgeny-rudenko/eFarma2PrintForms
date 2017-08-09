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
  public partial class AbcGroupCategoryControl : UserControl, IRequestParamMethods
  {
    public AbcGroupCategoryControl()
    {
      InitializeComponent();
      Array types = Enum.GetValues(typeof(ABCGroupCalcType));
      ABCGroupCalcTypeDescription[] descriptions = new ABCGroupCalcTypeDescription[types.Length];
      for (int i = 0; i < types.Length; i++)
      {
        ABCGroupCalcType type = (ABCGroupCalcType)types.GetValue(i);
        ABCGroupCalcTypeDescription desc = new ABCGroupCalcTypeDescription(type);
        descriptions[i] = desc;
      }
      cbCalcType.Items.AddRange(descriptions);
    }

    public void Object2Control(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is AbcGroupParam)
        {
          nbAGroup.Value = ((AbcGroupParam)p).APercent;
          nbBGroup.Value = ((AbcGroupParam)p).BPercent;
          cbCalcType.SelectedItem = new ABCGroupCalcTypeDescription(((AbcGroupParam)p).CalcType);
        }
      }
    }

    public void Control2Object(params IRequestParam[] param)
    {
      if (param == null) return;
      foreach (IRequestParam p in param)
      {
        if (p is AbcGroupParam)
        {
          ((AbcGroupParam)p).APercent = nbAGroup.Value;
          ((AbcGroupParam)p).BPercent = nbBGroup.Value;
          ((AbcGroupParam)p).CalcType = cbCalcType.SelectedItem != null ? ((ABCGroupCalcTypeDescription)cbCalcType.SelectedItem).Type : ABCGroupCalcType.Sum;
        }
      }
    }

    public void ClearData()
    {
      nbAGroup.Value = 10;
      nbBGroup.Value = 20;
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[] { typeof(AbcGroupParam) }; }
    }

    public string Category
    {
      get { return "Группы ABC"; }
    }

    private void nbBGroup_ValueChanged(object sender, EventArgs e)
    {
      nbCGroup.Value = 100 - (nbAGroup.Value + nbBGroup.Value);
    }
  }
}
