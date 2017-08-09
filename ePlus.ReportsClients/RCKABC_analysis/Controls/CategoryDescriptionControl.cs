using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RCKABC_analysis.Controls
{
  public partial class CategoryDescriptionControl : UserControl
  {
    public CategoryDescriptionControl()
    {
      InitializeComponent();
    }

    public string CategoryName
    {
      get { return llCategory.Text; }
      set { llCategory.Text = value; }
    }

    public string CategoryDescription
    {
      get { return lDescr.Text; }
      set
      {
        lDescr.Text = value;
        this.Height = (int)Graphics.FromHwnd(lDescr.Handle).MeasureString(value, lDescr.Font).Height+lDescr.Padding.Top*2+llCategory.Height;
      }
    }
    
    public event EventHandler CategorySelected;
    private void llCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (CategorySelected != null)
        CategorySelected(this, e);
    }
  }
}
