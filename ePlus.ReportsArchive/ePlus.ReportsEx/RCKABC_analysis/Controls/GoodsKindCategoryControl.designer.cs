namespace RCKABC_analysis.Controls
{
  partial class GoodsKindCategoryControl
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
      this.mpsGoodsKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.SuspendLayout();
      // 
      // mpsGoodsKind
      // 
      this.mpsGoodsKind.AllowSaveState = false;
      this.mpsGoodsKind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mpsGoodsKind.Caption = "Виды товара";
      this.mpsGoodsKind.Location = new System.Drawing.Point(3, 3);
      this.mpsGoodsKind.Mnemocode = "GOODS_KIND";
      this.mpsGoodsKind.Name = "mpsGoodsKind";
      this.mpsGoodsKind.Size = new System.Drawing.Size(250, 115);
      this.mpsGoodsKind.TabIndex = 3;
      // 
      // GoodsKindCategoryControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.mpsGoodsKind);
      this.Name = "GoodsKindCategoryControl";
      this.Size = new System.Drawing.Size(269, 130);
      this.ResumeLayout(false);

    }

    #endregion

    private ePlus.MetaData.Client.UCPluginMultiSelect mpsGoodsKind;
  }
}
