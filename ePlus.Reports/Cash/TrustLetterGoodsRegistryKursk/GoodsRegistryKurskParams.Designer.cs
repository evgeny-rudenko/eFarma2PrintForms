namespace RCBTrustLetterGoodsRegistryKursk
{
  partial class GoodsRegistryKurskParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsRegistryKurskParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucIns = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.label1 = new System.Windows.Forms.Label();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucLgot = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(202, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(277, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 443);
      this.panel1.Size = new System.Drawing.Size(355, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(355, 25);
      this.toolStrip1.TabIndex = 22;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
      this.toolStripButton1.Text = "Очистить";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // ucIns
      // 
      this.ucIns.AllowSaveState = true;
      this.ucIns.Caption = "Страховые компании";
      this.ucIns.Location = new System.Drawing.Point(12, 253);
      this.ucIns.Mnemocode = "DISCOUNT2_INSURANCE_COMPANY";
      this.ucIns.Name = "ucIns";
      this.ucIns.Size = new System.Drawing.Size(331, 85);
      this.ucIns.TabIndex = 138;
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
      this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
      this.ucPeriod.Location = new System.Drawing.Point(70, 39);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(229, 21);
      this.ucPeriod.TabIndex = 137;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 136;
      this.label1.Text = "Период:";
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(12, 162);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(331, 85);
      this.ucStores.TabIndex = 135;
      // 
      // ucContractors
      // 
      this.ucContractors.AllowSaveState = true;
      this.ucContractors.Caption = "Аптеки";
      this.ucContractors.Location = new System.Drawing.Point(12, 71);
      this.ucContractors.Mnemocode = "CONTRACTOR";
      this.ucContractors.Name = "ucContractors";
      this.ucContractors.Size = new System.Drawing.Size(331, 85);
      this.ucContractors.TabIndex = 134;
      // 
      // ucLgot
      // 
      this.ucLgot.AllowSaveState = true;
      this.ucLgot.Caption = "Категории льготников";
      this.ucLgot.Location = new System.Drawing.Point(12, 344);
      this.ucLgot.Mnemocode = "DISCOUNT2_CAT_LGOT";
      this.ucLgot.Name = "ucLgot";
      this.ucLgot.Size = new System.Drawing.Size(331, 85);
      this.ucLgot.TabIndex = 145;
      // 
      // GoodsRegistryKurskParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(355, 472);
      this.Controls.Add(this.ucLgot);
      this.Controls.Add(this.ucIns);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucContractors);
      this.Controls.Add(this.toolStrip1);
      this.Name = "GoodsRegistryKurskParams";
      this.Load += new System.EventHandler(this.GoodsRegistryKurskParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoodsRegistryKurskParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucContractors, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.ucIns, 0);
      this.Controls.SetChildIndex(this.ucLgot, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucIns;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucLgot;
  }
}