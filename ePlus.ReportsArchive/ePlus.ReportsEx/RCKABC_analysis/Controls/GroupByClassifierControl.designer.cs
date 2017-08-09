namespace RCKABC_analysis.Controls
{
  partial class GroupByClassifierControl
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
		this.chkGroup = new System.Windows.Forms.CheckBox();
		this.chkSort = new System.Windows.Forms.CheckBox();
		this.SuspendLayout();
		// 
		// chkGroup
		// 
		this.chkGroup.AutoSize = true;
		this.chkGroup.Checked = true;
		this.chkGroup.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkGroup.Dock = System.Windows.Forms.DockStyle.Top;
		this.chkGroup.Location = new System.Drawing.Point(4, 0);
		this.chkGroup.Name = "chkGroup";
		this.chkGroup.Size = new System.Drawing.Size(284, 17);
		this.chkGroup.TabIndex = 0;
		this.chkGroup.Text = "Сворачивать товары  по группам";
		this.chkGroup.UseVisualStyleBackColor = true;
		// 
		// chkSort
		// 
		this.chkSort.AutoSize = true;
		this.chkSort.Dock = System.Windows.Forms.DockStyle.Top;
		this.chkSort.Location = new System.Drawing.Point(4, 17);
		this.chkSort.Name = "chkSort";
		this.chkSort.Size = new System.Drawing.Size(284, 17);
		this.chkSort.TabIndex = 1;
		this.chkSort.Text = "Сортировать по наименованию товаров в группах";
		this.chkSort.UseVisualStyleBackColor = true;
		// 
		// GroupByClassifierControl
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.chkSort);
		this.Controls.Add(this.chkGroup);
		this.Name = "GroupByClassifierControl";
		this.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
		this.Size = new System.Drawing.Size(288, 41);
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

	  public System.Windows.Forms.CheckBox chkGroup;
	  public System.Windows.Forms.CheckBox chkSort;

}
}
