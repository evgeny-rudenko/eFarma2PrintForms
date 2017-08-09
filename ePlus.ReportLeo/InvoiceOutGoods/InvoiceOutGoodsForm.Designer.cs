namespace InvoiceOutGoods
{
    partial class InvoiceOutGoodsForm
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
        this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucGoodsGroup = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucDate = new ePlus.MetaData.Controls.DateControl();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(263, 3);
        this.bOK.Text = "Отчет";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(338, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 371);
        this.panel1.Size = new System.Drawing.Size(416, 29);
        this.panel1.TabIndex = 4;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(33, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Дата";
        // 
        // ucStore
        // 
        this.ucStore.AllowSaveState = false;
        this.ucStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucStore.Caption = "";
        this.ucStore.Location = new System.Drawing.Point(12, 39);
        this.ucStore.Mnemocode = "STORE";
        this.ucStore.Name = "ucStore";
        this.ucStore.Size = new System.Drawing.Size(395, 105);
        this.ucStore.TabIndex = 2;
        // 
        // ucContractor
        // 
        this.ucContractor.AllowSaveState = false;
        this.ucContractor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContractor.Caption = "";
        this.ucContractor.Location = new System.Drawing.Point(12, 261);
        this.ucContractor.Mnemocode = "CONTRACTOR";
        this.ucContractor.Name = "ucContractor";
        this.ucContractor.Size = new System.Drawing.Size(395, 104);
        this.ucContractor.TabIndex = 3;
        // 
        // ucGoodsGroup
        // 
        this.ucGoodsGroup.AllowSaveState = false;
        this.ucGoodsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucGoodsGroup.Caption = "";
        this.ucGoodsGroup.Location = new System.Drawing.Point(12, 150);
        this.ucGoodsGroup.Mnemocode = "GOODS_GROUP";
        this.ucGoodsGroup.Name = "ucGoodsGroup";
        this.ucGoodsGroup.Size = new System.Drawing.Size(395, 105);
        this.ucGoodsGroup.TabIndex = 5;
        // 
        // ucDate
        // 
        this.ucDate.Checked = true;
        this.ucDate.DefaultDay = 30;
        this.ucDate.DefaultMonth = 6;
        this.ucDate.DefaultYear = 2010;
        this.ucDate.ErrorColor = System.Drawing.Color.Red;
        this.ucDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
        this.ucDate.Location = new System.Drawing.Point(55, 12);
        this.ucDate.Mask = "00/00/0000";
        this.ucDate.MaxDate = new System.DateTime(((long)(0)));
        this.ucDate.MinDate = new System.DateTime(((long)(0)));
        this.ucDate.Name = "ucDate";
        this.ucDate.NormalColor = System.Drawing.SystemColors.Window;
        this.ucDate.Size = new System.Drawing.Size(96, 20);
        this.ucDate.TabIndex = 6;
        this.ucDate.Text = "30062010";
        this.ucDate.Value = new System.DateTime(2010, 6, 30, 11, 35, 53, 484);
        // 
        // InvoiceOutGoodsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(416, 400);
        this.Controls.Add(this.ucDate);
        this.Controls.Add(this.ucGoodsGroup);
        this.Controls.Add(this.ucStore);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucContractor);
        this.MinimumSize = new System.Drawing.Size(424, 327);
        this.Name = "InvoiceOutGoodsForm";
        this.Text = "";
        this.Controls.SetChildIndex(this.ucContractor, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.ucStore, 0);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
        this.Controls.SetChildIndex(this.ucDate, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroup;
        private ePlus.MetaData.Controls.DateControl ucDate;

}
}