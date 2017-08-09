using ePlus.CommonEx.Controls;

namespace RCKABC_analysis.Controls
{
  partial class NeedsCategoryControl
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
      this.nbDaysNeeds = new ePlusNumericBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cbCalcType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.nbDaysOnWay = new ePlusNumericBox();
      this.SuspendLayout();
      // 
      // nbDaysNeeds
      // 
      this.nbDaysNeeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nbDaysNeeds.DecimalPlaces = 0;
      this.nbDaysNeeds.DecimalSeparator = '.';
      this.nbDaysNeeds.Location = new System.Drawing.Point(211, 49);
      this.nbDaysNeeds.MaxLength = 2;
      this.nbDaysNeeds.Name = "nbDaysNeeds";
      this.nbDaysNeeds.Positive = true;
      this.nbDaysNeeds.Size = new System.Drawing.Size(80, 20);
      this.nbDaysNeeds.TabIndex = 18;
      this.nbDaysNeeds.Text = "0";
      this.nbDaysNeeds.ThousandSeparator = ' ';
      this.nbDaysNeeds.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 52);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(163, 13);
      this.label3.TabIndex = 17;
      this.label3.Text = "Количество дней потребности:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(163, 13);
      this.label1.TabIndex = 16;
      this.label1.Text = "Способ расчета потребности:";
      // 
      // cbCalcType
      // 
      this.cbCalcType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbCalcType.FormattingEnabled = true;
      this.cbCalcType.Location = new System.Drawing.Point(15, 22);
      this.cbCalcType.Name = "cbCalcType";
      this.cbCalcType.Size = new System.Drawing.Size(276, 21);
      this.cbCalcType.TabIndex = 15;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 78);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(189, 13);
      this.label2.TabIndex = 19;
      this.label2.Text = "Количество дней пока товар в пути:";
      // 
      // nbDaysOnWay
      // 
      this.nbDaysOnWay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nbDaysOnWay.DecimalPlaces = 0;
      this.nbDaysOnWay.DecimalSeparator = '.';
      this.nbDaysOnWay.Location = new System.Drawing.Point(211, 75);
      this.nbDaysOnWay.MaxLength = 2;
      this.nbDaysOnWay.Name = "nbDaysOnWay";
      this.nbDaysOnWay.Positive = true;
      this.nbDaysOnWay.Size = new System.Drawing.Size(80, 20);
      this.nbDaysOnWay.TabIndex = 20;
      this.nbDaysOnWay.Text = "0";
      this.nbDaysOnWay.ThousandSeparator = ' ';
      this.nbDaysOnWay.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      // 
      // NeedsCategoryControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nbDaysOnWay);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.nbDaysNeeds);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbCalcType);
      this.Name = "NeedsCategoryControl";
      this.Size = new System.Drawing.Size(307, 108);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ePlusNumericBox nbDaysNeeds;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbCalcType;
    private System.Windows.Forms.Label label2;
    private ePlusNumericBox nbDaysOnWay;
  }
}
