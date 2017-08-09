namespace RCBAptekaRuExport
{
  partial class AptekaRuExport
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.mpsAccessPoint = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
      this.ucDrugstores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(340, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(415, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 188);
      this.panel1.Size = new System.Drawing.Size(493, 29);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(-3, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Точка доступа:";
      // 
      // mpsAccessPoint
      // 
      this.mpsAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mpsAccessPoint.ClearTextOnValidatingIfValueIsEmpty = true;
      this.mpsAccessPoint.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
      this.mpsAccessPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
      this.mpsAccessPoint.Location = new System.Drawing.Point(86, 18);
      this.mpsAccessPoint.Name = "mpsAccessPoint";
      this.mpsAccessPoint.PluginMnemocode = "ACCESS_POINT";
      this.mpsAccessPoint.SelectNextControlAfterSelectEntity = false;
      this.mpsAccessPoint.Size = new System.Drawing.Size(407, 20);
      this.mpsAccessPoint.TabIndex = 4;
      this.mpsAccessPoint.UseSpaceToOpenPlugin = true;
      // 
      // ucDrugstores
      // 
      this.ucDrugstores.AllowSaveState = true;
      this.ucDrugstores.Caption = "Контрагенты";
      this.ucDrugstores.Location = new System.Drawing.Point(-1, 47);
      this.ucDrugstores.Mnemocode = "CONTRACTOR";
      this.ucDrugstores.Name = "ucDrugstores";
      this.ucDrugstores.Size = new System.Drawing.Size(495, 133);
      this.ucDrugstores.TabIndex = 199;
      this.ucDrugstores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucDrugstores_BeforePluginShow);
      // 
      // AptekaRuExport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(493, 217);
      this.Controls.Add(this.ucDrugstores);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.mpsAccessPoint);
      this.Name = "AptekaRuExport";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AptekaRuExport_FormClosed);
      this.Load += new System.EventHandler(this.AptekaRuExport_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.mpsAccessPoint, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucDrugstores, 0);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl mpsAccessPoint;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugstores;
  }
}