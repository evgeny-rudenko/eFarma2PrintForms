using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using RCKABC_analysis;

namespace RCKABC_analysis.Controls
{
  public partial class GoodsKindCategoryControl : UserControl, IRequestParamMethods
  {
    public GoodsKindCategoryControl()
    {
      InitializeComponent();
    }

    public void Object2Control(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is GoodsKindParam)
        {
          mpsGoodsKind.Items.Clear();
          foreach (long l in ((GoodsKindParam)p).GoodsKinds)
          {
            mpsGoodsKind.AddItem(l);
          }
        }
      }
    }

    public void Control2Object(params IRequestParam[] param)
    {
      foreach (IRequestParam p in param)
      {
        if (p is GoodsKindParam)
        {
          ((GoodsKindParam)p).GoodsKinds.Clear();
          ((GoodsKindParam)p).GoodsKindDesc.Clear();
          foreach (DataRowItem dri in mpsGoodsKind.Items)
          {
            ((GoodsKindParam)p).GoodsKinds.Add(dri.Id);

            if (((GoodsKindParam)p).GoodsKindDesc.ContainsKey(dri.Id))
              continue;
            ((GoodsKindParam)p).GoodsKindDesc.Add(dri.Id, dri.Text);
          }
          
        }
        
      }

    }

    public void ClearData()
    {
      
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[]{typeof(GoodsKindParam)}; }
    }

    public string Category
    {
      get { return "Виды товаров"; }
    }
  }
}
