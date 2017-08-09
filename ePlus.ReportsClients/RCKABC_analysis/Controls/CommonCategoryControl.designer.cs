using ePlus.CommonEx.Controls;
//using ePlus.Controls.NewControls;
using ePlus.MetaData.Client;

namespace RCKABC_analysis.Controls
{
  partial class CommonCategoryControl
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
		this.label1 = new System.Windows.Forms.Label();
		this.nbQty = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.chkInvoiceOut = new System.Windows.Forms.CheckBox();
		this.chkCheque = new System.Windows.Forms.CheckBox();
		this.label2 = new System.Windows.Forms.Label();
		this.chkMOVE = new System.Windows.Forms.CheckBox();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 6);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(162, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Количество дней для анализа:";
		// 
		// nbQty
		// 
		this.nbQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.nbQty.DecimalPlaces = 0;
		this.nbQty.DecimalSeparator = '.';
		this.nbQty.Location = new System.Drawing.Point(171, 3);
		this.nbQty.MaxLength = 3;
		this.nbQty.Name = "nbQty";
		this.nbQty.Positive = true;
		this.nbQty.Size = new System.Drawing.Size(402, 20);
		this.nbQty.TabIndex = 1;
		this.nbQty.Text = "31";
		this.nbQty.ThousandSeparator = ' ';
		this.nbQty.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbQty.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
		// 
		// mpsStore
		// 
		this.mpsStore.AllowSaveState = false;
		this.mpsStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.mpsStore.Caption = "Склады";
		this.mpsStore.Location = new System.Drawing.Point(7, 29);
		this.mpsStore.Mnemocode = "STORE";
		this.mpsStore.Name = "mpsStore";
		this.mpsStore.Size = new System.Drawing.Size(566, 75);
		this.mpsStore.TabIndex = 2;
		// 
		// chkInvoiceOut
		// 
		this.chkInvoiceOut.AutoSize = true;
		this.chkInvoiceOut.Checked = true;
		this.chkInvoiceOut.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkInvoiceOut.Location = new System.Drawing.Point(150, 110);
		this.chkInvoiceOut.Name = "chkInvoiceOut";
		this.chkInvoiceOut.Size = new System.Drawing.Size(141, 17);
		this.chkInvoiceOut.TabIndex = 3;
		this.chkInvoiceOut.Text = "Расходные накладные";
		this.chkInvoiceOut.UseVisualStyleBackColor = true;
		// 
		// chkCheque
		// 
		this.chkCheque.AutoSize = true;
		this.chkCheque.Checked = true;
		this.chkCheque.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkCheque.Location = new System.Drawing.Point(92, 110);
		this.chkCheque.Name = "chkCheque";
		this.chkCheque.Size = new System.Drawing.Size(52, 17);
		this.chkCheque.TabIndex = 4;
		this.chkCheque.Text = "Чеки";
		this.chkCheque.UseVisualStyleBackColor = true;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(5, 111);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(81, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Виды расхода:";
		// 
		// chkMOVE
		// 
		this.chkMOVE.AutoSize = true;
		this.chkMOVE.Checked = true;
		this.chkMOVE.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkMOVE.Location = new System.Drawing.Point(297, 110);
		this.chkMOVE.Name = "chkMOVE";
		this.chkMOVE.Size = new System.Drawing.Size(99, 17);
		this.chkMOVE.TabIndex = 6;
		this.chkMOVE.Text = "Перемещение";
		this.chkMOVE.UseVisualStyleBackColor = true;
		// 
		// CommonCategoryControl
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.chkMOVE);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.chkCheque);
		this.Controls.Add(this.chkInvoiceOut);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.nbQty);
		this.Controls.Add(this.mpsStore);
		this.MinimumSize = new System.Drawing.Size(323, 130);
		this.Name = "CommonCategoryControl";
		this.Size = new System.Drawing.Size(579, 130);
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

	  private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
	  public ePlusNumericBox nbQty;
	  public UCPluginMultiSelect mpsStore;
	  public System.Windows.Forms.CheckBox chkInvoiceOut;
	  public System.Windows.Forms.CheckBox chkCheque;
	  public System.Windows.Forms.CheckBox chkMOVE;

  }
}
