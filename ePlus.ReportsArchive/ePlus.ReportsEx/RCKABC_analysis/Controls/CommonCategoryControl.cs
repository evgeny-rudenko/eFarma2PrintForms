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
  public partial class CommonCategoryControl : UserControl, IRequestParamMethods
  {
    public CommonCategoryControl()
    {
      InitializeComponent();
    }

    public void Object2Control(params IRequestParam[] param)
    {
      if (param == null) return;
      foreach (IRequestParam p in param)
      {
        if (p is DaysPeriodParam)
        {
          nbQty.Value = ((DaysPeriodParam)p).Days_period;
        }
        else if (p is StoreParam)
        {
          mpsStore.Items.Clear();
          foreach (long l in ((StoreParam)p).Stores)
          {
            mpsStore.AddItem(l);
          }
        }
        else if (p is SubOpTypeParam)
        {
          chkCheque.Checked = false;
          chkInvoiceOut.Checked = false;
          foreach (string s in ((SubOpTypeParam)p).Ops)
          {
            if (s == "CHEQUE")
              chkCheque.Checked = true;
            if (s == "INVOICE_OUT")
              chkInvoiceOut.Checked = true;
		    if (s == "MOVE")
		  	  chkMOVE.Checked = true;
          }
        }
      }
    }

    public void Control2Object(params IRequestParam[] param)
    {
      if (param == null) return;
      foreach (IRequestParam p in param)
      {
        if (p is DaysPeriodParam)
        {
          ((DaysPeriodParam)p).Days_period = (int)nbQty.Value;
        }
        else if (p is StoreParam)
        {
          ((StoreParam)p).Stores.Clear();
          ((StoreParam)p).StoreDesc.Clear();
          foreach (DataRowItem dri in mpsStore.Items)
          {
            ((StoreParam)p).Stores.Add(dri.Id);
            
            if (((StoreParam)p).StoreDesc.ContainsKey(dri.Id))
              continue;
            ((StoreParam)p).StoreDesc.Add(dri.Id, dri.Text);
          }
        }
        else if (p is SubOpTypeParam)
        {
          ((SubOpTypeParam)p).Ops.Clear();
          if (chkCheque.Checked)
            ((SubOpTypeParam)p).Ops.Add("CHEQUE");
          if (chkInvoiceOut.Checked)
            ((SubOpTypeParam)p).Ops.Add("INVOICE_OUT");
		if (chkMOVE.Checked)
			((SubOpTypeParam)p).Ops.Add("MOVE");
        }
      }
    }

    public void ClearData()
    {
      nbQty.Value = 0;
      mpsStore.Items.Clear();
      chkCheque.Checked = false;
      chkInvoiceOut.Checked = false;
    }

    public Control Control
    {
      get { return this; }
    }

    public Type[] SupportsParams
    {
      get { return new Type[] { typeof(DaysPeriodParam), typeof(StoreParam), typeof(SubOpTypeParam) }; }
    }

    public string Category
    {
      get { return "Извлечение данных"; }
    }

  }
}
