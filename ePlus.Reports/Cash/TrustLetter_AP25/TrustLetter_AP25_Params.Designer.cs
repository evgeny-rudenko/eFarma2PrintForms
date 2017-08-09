namespace RCBTrustLetter_AP25
{
  partial class TrustLetter_AP25_Params
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrustLetter_AP25_Params));
        this.ucIns = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.label1 = new System.Windows.Forms.Label();
        this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        this.ucLgot = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucPeriodShip = new ePlus.MetaData.Client.UCPeriod();
        this.label2 = new System.Windows.Forms.Label();
        this.ucLPU = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.panel1.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(541, 3);
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(616, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 355);
        this.panel1.Size = new System.Drawing.Size(694, 29);
        // 
        // ucIns
        // 
        this.ucIns.AllowSaveState = true;
        this.ucIns.Caption = "Страховые компании";
        this.ucIns.Location = new System.Drawing.Point(5, 73);
        this.ucIns.Mnemocode = "DISCOUNT2_INSURANCE_COMPANY";
        this.ucIns.MultiSelect = true;
        this.ucIns.Name = "ucIns";
        this.ucIns.Pinnable = false;
        this.ucIns.Size = new System.Drawing.Size(331, 85);
        this.ucIns.TabIndex = 143;
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
        this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
        this.ucPeriod.Location = new System.Drawing.Point(63, 41);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(229, 21);
        this.ucPeriod.TabIndex = 142;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(9, 45);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(48, 13);
        this.label1.TabIndex = 141;
        this.label1.Text = "Период:";
        // 
        // ucStores
        // 
        this.ucStores.AllowSaveState = true;
        this.ucStores.Caption = "Склады";
        this.ucStores.Location = new System.Drawing.Point(5, 164);
        this.ucStores.Mnemocode = "STORE";
        this.ucStores.MultiSelect = true;
        this.ucStores.Name = "ucStores";
        this.ucStores.Pinnable = false;
        this.ucStores.Size = new System.Drawing.Size(331, 85);
        this.ucStores.TabIndex = 140;
        // 
        // toolStrip1
        // 
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
        this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
        this.toolStrip1.Size = new System.Drawing.Size(694, 25);
        this.toolStrip1.TabIndex = 139;
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
        // ucLgot
        // 
        this.ucLgot.AllowSaveState = true;
        this.ucLgot.Caption = "Категории льготников";
        this.ucLgot.Location = new System.Drawing.Point(5, 255);
        this.ucLgot.Mnemocode = "DISCOUNT2_CAT_LGOT";
        this.ucLgot.MultiSelect = true;
        this.ucLgot.Name = "ucLgot";
        this.ucLgot.Pinnable = false;
        this.ucLgot.Size = new System.Drawing.Size(331, 85);
        this.ucLgot.TabIndex = 144;
        // 
        // ucContractors
        // 
        this.ucContractors.AllowSaveState = true;
        this.ucContractors.Caption = "Контрагенты";
        this.ucContractors.Location = new System.Drawing.Point(342, 73);
        this.ucContractors.Mnemocode = "CONTRACTOR";
        this.ucContractors.MultiSelect = true;
        this.ucContractors.Name = "ucContractors";
        this.ucContractors.Pinnable = false;
        this.ucContractors.Size = new System.Drawing.Size(340, 85);
        this.ucContractors.TabIndex = 145;
        // 
        // ucGoods
        // 
        this.ucGoods.AllowSaveState = true;
        this.ucGoods.Caption = "Товары";
        this.ucGoods.Location = new System.Drawing.Point(344, 164);
        this.ucGoods.Mnemocode = "GOODS2";
        this.ucGoods.MultiSelect = true;
        this.ucGoods.Name = "ucGoods";
        this.ucGoods.Pinnable = false;
        this.ucGoods.Size = new System.Drawing.Size(340, 85);
        this.ucGoods.TabIndex = 146;
        // 
        // ucPeriodShip
        // 
        this.ucPeriodShip.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
        this.ucPeriodShip.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
        this.ucPeriodShip.Location = new System.Drawing.Point(438, 41);
        this.ucPeriodShip.Name = "ucPeriodShip";
        this.ucPeriodShip.Size = new System.Drawing.Size(229, 21);
        this.ucPeriodShip.TabIndex = 148;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(341, 45);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(91, 13);
        this.label2.TabIndex = 147;
        this.label2.Text = "Период отпуска:";
        // 
        // ucLPU
        // 
        this.ucLPU.AllowSaveState = true;
        this.ucLPU.Caption = "ЛПУ";
        this.ucLPU.Location = new System.Drawing.Point(342, 255);
        this.ucLPU.Mnemocode = "DISCOUNT2_MEDICAL_ORGANIZATION";
        this.ucLPU.MultiSelect = true;
        this.ucLPU.Name = "ucLPU";
        this.ucLPU.Pinnable = false;
        this.ucLPU.Size = new System.Drawing.Size(340, 85);
        this.ucLPU.TabIndex = 149;
        // 
        // TrustLetter_AP25_Params
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(694, 384);
        this.Controls.Add(this.ucLPU);
        this.Controls.Add(this.ucPeriodShip);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.ucGoods);
        this.Controls.Add(this.ucContractors);
        this.Controls.Add(this.ucLgot);
        this.Controls.Add(this.ucIns);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucStores);
        this.Controls.Add(this.toolStrip1);
        this.Name = "TrustLetter_AP25_Params";
        this.Load += new System.EventHandler(this.TrustLetter_AP25_Params_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TrustLetter_AP25_Params_FormClosed);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.toolStrip1, 0);
        this.Controls.SetChildIndex(this.ucStores, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.ucIns, 0);
        this.Controls.SetChildIndex(this.ucLgot, 0);
        this.Controls.SetChildIndex(this.ucContractors, 0);
        this.Controls.SetChildIndex(this.ucGoods, 0);
        this.Controls.SetChildIndex(this.label2, 0);
        this.Controls.SetChildIndex(this.ucPeriodShip, 0);
        this.Controls.SetChildIndex(this.ucLPU, 0);
        this.panel1.ResumeLayout(false);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ePlus.MetaData.Client.UCPluginMultiSelect ucIns;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
      private ePlus.MetaData.Client.UCPluginMultiSelect ucLgot;
      private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
      public ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
      private ePlus.MetaData.Client.UCPeriod ucPeriodShip;
      private System.Windows.Forms.Label label2;
      public ePlus.MetaData.Client.UCPluginMultiSelect ucLPU;
  }
}