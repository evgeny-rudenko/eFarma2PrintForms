using ePlus.CommonEx.Controls;

namespace RCKABC_analysis.Controls
{
  partial class SalesSpeedCategoryControl
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
      this.cbCalcType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.nbSmoothCoeff = new ePlusNumericBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nbStatDaysCount = new ePlusNumericBox();
      this.SuspendLayout();
      // 
      // cbCalcType
      // 
      this.cbCalcType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbCalcType.FormattingEnabled = true;
      this.cbCalcType.Location = new System.Drawing.Point(6, 26);
      this.cbCalcType.Name = "cbCalcType";
      this.cbCalcType.Size = new System.Drawing.Size(379, 21);
      this.cbCalcType.TabIndex = 9;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(187, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Способ расчета скорости продаж:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(224, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Коэффициент слагживания пиков продаж:";
      // 
      // nbSmoothCoeff
      // 
      this.nbSmoothCoeff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nbSmoothCoeff.DecimalPlaces = 2;
      this.nbSmoothCoeff.DecimalSeparator = '.';
      this.nbSmoothCoeff.Location = new System.Drawing.Point(309, 53);
      this.nbSmoothCoeff.MaxLength = 4;
      this.nbSmoothCoeff.Name = "nbSmoothCoeff";
      this.nbSmoothCoeff.Positive = true;
      this.nbSmoothCoeff.Size = new System.Drawing.Size(76, 20);
      this.nbSmoothCoeff.TabIndex = 12;
      this.nbSmoothCoeff.Text = "0.00";
      this.nbSmoothCoeff.ThousandSeparator = ' ';
      this.nbSmoothCoeff.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 83);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(295, 13);
      this.label3.TabIndex = 13;
      this.label3.Text = "Минимальное количество дней для успешного анализа::";
      // 
      // nbStatDaysCount
      // 
      this.nbStatDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nbStatDaysCount.DecimalPlaces = 0;
      this.nbStatDaysCount.DecimalSeparator = '.';
      this.nbStatDaysCount.Location = new System.Drawing.Point(309, 80);
      this.nbStatDaysCount.MaxLength = 2;
      this.nbStatDaysCount.Name = "nbStatDaysCount";
      this.nbStatDaysCount.Positive = true;
      this.nbStatDaysCount.Size = new System.Drawing.Size(76, 20);
      this.nbStatDaysCount.TabIndex = 14;
      this.nbStatDaysCount.Text = "0";
      this.nbStatDaysCount.ThousandSeparator = ' ';
      this.nbStatDaysCount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      // 
      // SalesSpeedCategoryControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nbStatDaysCount);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.nbSmoothCoeff);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbCalcType);
      this.Name = "SalesSpeedCategoryControl";
      this.Size = new System.Drawing.Size(406, 111);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cbCalcType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private ePlusNumericBox nbSmoothCoeff;
    private System.Windows.Forms.Label label3;
    private ePlusNumericBox nbStatDaysCount;
  }
}
