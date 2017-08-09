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
  public partial class NeedsCategoryControl : UserControl, IRequestParamMethods
  {
    public NeedsCategoryControl()
    {
      InitializeComponent();
      Array types = Enum.GetValues(typeof(NeedsCalcType));
      NeedsCalcTypeDescription[] descriptions = new NeedsCalcTypeDescription[types.Length];
      for (int i = 0; i < types.Length; i++)
      {
        NeedsCalcType type = (NeedsCalcType)types.GetValue(i);
        NeedsCalcTypeDescription desc = new NeedsCalcTypeDescription(type);
        descriptions[i] = desc;
      }
      cbCalcType.Items.AddRange(descriptions);
    }

    public void Object2Control(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is NeedsCalcParam)
        {
          cbCalcType.SelectedItem = new NeedsCalcTypeDescription(((NeedsCalcParam)p).CalcType);
          nbDaysNeeds.Value = ((NeedsCalcParam)p).DaysNeeds;
          nbDaysOnWay.Value = ((NeedsCalcParam)p).DaysOnWay;
        }
      }

    }

    public void Control2Object(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is NeedsCalcParam)
        {
          ((NeedsCalcParam)p).CalcType = cbCalcType.SelectedItem != null ? ((NeedsCalcTypeDescription)cbCalcType.SelectedItem).Type : NeedsCalcType.NoRemain;
          ((NeedsCalcParam)p).DaysNeeds = (int)nbDaysNeeds.Value;
          ((NeedsCalcParam)p).DaysOnWay = (int)nbDaysOnWay.Value;
        }
      }
    }

    public void ClearData()
    {
      cbCalcType.SelectedItem = new NeedsCalcTypeDescription(NeedsCalcType.NoRemain);
      nbDaysNeeds.Value = 0;
      nbDaysOnWay.Value = 0;
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[]{typeof(NeedsCalcParam)}; }
    }

    public string Category
    {
      get { return "Расчет потребности"; }
    }
  }
}
