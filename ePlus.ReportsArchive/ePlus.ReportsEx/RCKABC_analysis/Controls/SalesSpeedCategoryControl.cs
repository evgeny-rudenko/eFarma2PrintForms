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
  public partial class SalesSpeedCategoryControl : UserControl, IRequestParamMethods
  {
    public SalesSpeedCategoryControl()
    {
      InitializeComponent();

      Array types = Enum.GetValues(typeof(SalesSpeedCalcType));
      SalesSpeedCalcTypeDescription[] descriptions = new SalesSpeedCalcTypeDescription[types.Length];
      for (int i = 0; i < types.Length; i++)
      {
        SalesSpeedCalcType type = (SalesSpeedCalcType)types.GetValue(i);
        SalesSpeedCalcTypeDescription desc = new SalesSpeedCalcTypeDescription(type);
        descriptions[i] = desc;
      }
      cbCalcType.Items.AddRange(descriptions);
    }

    public void Object2Control(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is SalesSpeedCalcParam)
        {
          cbCalcType.SelectedItem = new SalesSpeedCalcTypeDescription(((SalesSpeedCalcParam)p).CalcType);
          nbSmoothCoeff.Value = ((SalesSpeedCalcParam)p).SmoothCoeff;
          nbStatDaysCount.Value = ((SalesSpeedCalcParam)p).StatDaysCount;
          }
      }
    }

    public void Control2Object(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is SalesSpeedCalcParam)
        {
          ((SalesSpeedCalcParam)p).CalcType = cbCalcType.SelectedItem != null ? ((SalesSpeedCalcTypeDescription)cbCalcType.SelectedItem).Type : SalesSpeedCalcType.Avg;
          ((SalesSpeedCalcParam)p).SmoothCoeff = nbSmoothCoeff.Value;
          ((SalesSpeedCalcParam)p).StatDaysCount = (int)nbStatDaysCount.Value;

        }
      }
    }

    public void ClearData()
    {
      cbCalcType.SelectedItem = new SalesSpeedCalcTypeDescription(SalesSpeedCalcType.Avg);
      nbSmoothCoeff.Value = 0;
      nbStatDaysCount.Value = 0;
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[]{typeof (SalesSpeedCalcParam)}; }
    }

    public string Category
    {
      get { return "Скорость продаж"; }
    }
  }
}
