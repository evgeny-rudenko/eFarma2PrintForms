namespace RCLReestrPayNoLS
{
    partial class ReestrPayNoLSForm
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
        this.components = new System.ComponentModel.Container();
        this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucGoodsGroup = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.label1 = new System.Windows.Forms.Label();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.checkShowGroup = new System.Windows.Forms.CheckBox();
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
        this.panel1.Location = new System.Drawing.Point(0, 416);
        this.panel1.Size = new System.Drawing.Size(416, 29);
        this.panel1.TabIndex = 5;
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
        this.ucContractor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContractor.Caption = "Поставщики";
        this.ucContractor.Location = new System.Drawing.Point(12, 150);
        this.ucContractor.Mnemocode = "CONTRACTOR";
        this.ucContractor.Name = "ucContractor";
        this.ucContractor.Size = new System.Drawing.Size(395, 110);
        this.ucContractor.TabIndex = 3;
        // 
        // ucGoodsGroup
        // 
        this.ucGoodsGroup.AllowSaveState = false;
        this.ucGoodsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucGoodsGroup.Caption = "";
        this.ucGoodsGroup.Location = new System.Drawing.Point(12, 266);
        this.ucGoodsGroup.Mnemocode = "GOODS_GROUP";
        this.ucGoodsGroup.Name = "ucGoodsGroup";
        this.ucGoodsGroup.Size = new System.Drawing.Size(395, 122);
        this.ucGoodsGroup.TabIndex = 4;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(45, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Период";
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.DateTo = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.Location = new System.Drawing.Point(61, 12);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(222, 21);
        this.ucPeriod.TabIndex = 1;
        // 
        // checkShowGroup
        // 
        this.checkShowGroup.AutoSize = true;
        this.checkShowGroup.Checked = true;
        this.checkShowGroup.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkShowGroup.Location = new System.Drawing.Point(16, 393);
        this.checkShowGroup.Name = "checkShowGroup";
        this.checkShowGroup.Size = new System.Drawing.Size(352, 17);
        this.checkShowGroup.TabIndex = 6;
        this.checkShowGroup.Text = "Выводить поаптечно(в разрезе каждого склада каждой аптеки)";
        this.checkShowGroup.UseVisualStyleBackColor = true;
        // 
        // ReestrPayNoLSForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(416, 445);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.checkShowGroup);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.ucStore);
        this.Controls.Add(this.ucContractor);
        this.Controls.Add(this.ucGoodsGroup);
        this.MinimumSize = new System.Drawing.Size(424, 327);
        this.Name = "ReestrPayNoLSForm";
        this.Load += new System.EventHandler(this.ReestrPayNoLSForm_Load);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReestrPayNoLSForm_FormClosing);
        this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
        this.Controls.SetChildIndex(this.ucContractor, 0);
        this.Controls.SetChildIndex(this.ucStore, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.checkShowGroup, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroup;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.CheckBox checkShowGroup;

}
}