namespace RCKABC_analysis.Controls
{
  partial class CategoryDescriptionControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
		this.llCategory = new System.Windows.Forms.LinkLabel();
		this.lDescr = new System.Windows.Forms.Label();
		this.SuspendLayout();
		// 
		// llCategory
		// 
		this.llCategory.AutoSize = true;
		this.llCategory.Dock = System.Windows.Forms.DockStyle.Top;
		this.llCategory.Location = new System.Drawing.Point(0, 0);
		this.llCategory.Name = "llCategory";
		this.llCategory.Size = new System.Drawing.Size(58, 13);
		this.llCategory.TabIndex = 0;
		this.llCategory.TabStop = true;
		this.llCategory.Text = "Параметр";
		this.llCategory.VisitedLinkColor = System.Drawing.Color.Blue;
		this.llCategory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCategory_LinkClicked);
		// 
		// lDescr
		// 
		this.lDescr.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lDescr.Location = new System.Drawing.Point(0, 13);
		this.lDescr.Name = "lDescr";
		this.lDescr.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
		this.lDescr.Size = new System.Drawing.Size(150, 137);
		this.lDescr.TabIndex = 1;
		this.lDescr.Text = "Описание параметра с его настройками";
		// 
		// CategoryDescriptionControl
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.lDescr);
		this.Controls.Add(this.llCategory);
		this.Name = "CategoryDescriptionControl";
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.LinkLabel llCategory;
    private System.Windows.Forms.Label lDescr;
  }
}
