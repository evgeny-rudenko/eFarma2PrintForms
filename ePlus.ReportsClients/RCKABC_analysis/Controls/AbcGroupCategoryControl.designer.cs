using ePlus.CommonEx.Controls;

namespace RCKABC_analysis.Controls
{
  partial class AbcGroupCategoryControl
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
		this.nbAGroup = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.nbBGroup = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.nbCGroup = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cbCalcType = new System.Windows.Forms.ComboBox();
		this.SuspendLayout();
		// 
		// nbAGroup
		// 
		this.nbAGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.nbAGroup.DecimalPlaces = 0;
		this.nbAGroup.DecimalSeparator = '.';
		this.nbAGroup.Location = new System.Drawing.Point(136, 29);
		this.nbAGroup.MaxLength = 2;
		this.nbAGroup.Name = "nbAGroup";
		this.nbAGroup.Positive = true;
		this.nbAGroup.Size = new System.Drawing.Size(285, 20);
		this.nbAGroup.TabIndex = 2;
		this.nbAGroup.Text = "75";
		this.nbAGroup.ThousandSeparator = ' ';
		this.nbAGroup.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbAGroup.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
		this.nbAGroup.ValueChanged += new System.EventHandler(this.nbBGroup_ValueChanged);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(7, 32);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(123, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Процент для группы А:";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(7, 58);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(123, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Процент для группы B:";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(7, 84);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(123, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Процент для группы C:";
		// 
		// nbBGroup
		// 
		this.nbBGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.nbBGroup.DecimalPlaces = 0;
		this.nbBGroup.DecimalSeparator = '.';
		this.nbBGroup.Location = new System.Drawing.Point(136, 55);
		this.nbBGroup.MaxLength = 2;
		this.nbBGroup.Name = "nbBGroup";
		this.nbBGroup.Positive = true;
		this.nbBGroup.Size = new System.Drawing.Size(285, 20);
		this.nbBGroup.TabIndex = 5;
		this.nbBGroup.Text = "20";
		this.nbBGroup.ThousandSeparator = ' ';
		this.nbBGroup.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbBGroup.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
		this.nbBGroup.ValueChanged += new System.EventHandler(this.nbBGroup_ValueChanged);
		// 
		// nbCGroup
		// 
		this.nbCGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.nbCGroup.DecimalPlaces = 0;
		this.nbCGroup.DecimalSeparator = '.';
		this.nbCGroup.Location = new System.Drawing.Point(136, 81);
		this.nbCGroup.MaxLength = 3;
		this.nbCGroup.Name = "nbCGroup";
		this.nbCGroup.Positive = true;
		this.nbCGroup.ReadOnly = true;
		this.nbCGroup.Size = new System.Drawing.Size(285, 20);
		this.nbCGroup.TabIndex = 6;
		this.nbCGroup.Text = "0";
		this.nbCGroup.ThousandSeparator = ' ';
		this.nbCGroup.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbCGroup.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(3, 6);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(121, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Способ расчета групп:";
		// 
		// cbCalcType
		// 
		this.cbCalcType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.cbCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbCalcType.FormattingEnabled = true;
		this.cbCalcType.Location = new System.Drawing.Point(136, 3);
		this.cbCalcType.Name = "cbCalcType";
		this.cbCalcType.Size = new System.Drawing.Size(285, 21);
		this.cbCalcType.TabIndex = 8;
		// 
		// AbcGroupCategoryControl
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.cbCalcType);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.nbCGroup);
		this.Controls.Add(this.nbBGroup);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.nbAGroup);
		this.Name = "AbcGroupCategoryControl";
		this.Size = new System.Drawing.Size(424, 111);
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

	private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
	  private System.Windows.Forms.Label label3;
    private ePlusNumericBox nbCGroup;
	  private System.Windows.Forms.Label label4;
	  public ePlusNumericBox nbAGroup;
	  public ePlusNumericBox nbBGroup;
	  public System.Windows.Forms.ComboBox cbCalcType;
  }
}
