using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RCKABC_analysis.Controls
{
  public partial class AllCategoryControl : UserControl
  {
    public AllCategoryControl()
    {
      InitializeComponent();
    }

    public event EventHandler CategoryClicked;
    
    public void Init(List<RequestCategoryTag> tags)
    {
      this.Controls.Clear();
      for (int i=tags.Count-1;i>=0;i--)
      {
        RequestCategoryTag tag = tags[i];
        CategoryDescriptionControl control = new CategoryDescriptionControl();
        control.CategoryName = tag.Category;
        control.Tag = tag;
        control.Dock = DockStyle.Top;
        control.CategorySelected += new EventHandler(OnCategorySelected);
        this.Controls.Add(control);
      }
    }

    private void OnCategorySelected(object sender, EventArgs e)
    {
      if (CategoryClicked != null)
        CategoryClicked(sender, e);
    }

    private string GetTagDescription(RequestCategoryTag tag)
    {
      StringBuilder sb = new StringBuilder(string.Empty);
      foreach (IRequestParam param in tag.Params)
      {
        sb.AppendLine(param.ToString());
      }
      return sb.ToString();
    }

    public void RefreshParams()
    {
       foreach (CategoryDescriptionControl control in this.Controls)
       {
         control.CategoryDescription = GetTagDescription((RequestCategoryTag)control.Tag);
       }     
    }
  }
}
