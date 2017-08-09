namespace CheckExchange
{
  partial class CheckExchangeParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckExchangeParams));
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.AUPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label2 = new System.Windows.Forms.Label();
      this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
      this.radioDetailed = new System.Windows.Forms.RadioButton();
      this.radioWhole = new System.Windows.Forms.RadioButton();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(216, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(291, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 322);
      this.panel1.Size = new System.Drawing.Size(369, 29);
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
      this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
      this.ucPeriod.Location = new System.Drawing.Point(96, 38);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(225, 21);
      this.ucPeriod.TabIndex = 122;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 42);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 121;
      this.label1.Text = "Период:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.AUPluginMultiSelect);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.ucContractor);
      this.groupBox1.Controls.Add(this.radioDetailed);
      this.groupBox1.Controls.Add(this.radioWhole);
      this.groupBox1.Location = new System.Drawing.Point(12, 75);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(346, 225);
      this.groupBox1.TabIndex = 124;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Тип отчета";
      // 
      // AUPluginMultiSelect
      // 
      this.AUPluginMultiSelect.AllowSaveState = true;
      this.AUPluginMultiSelect.Caption = "Аптеки";
      this.AUPluginMultiSelect.Location = new System.Drawing.Point(16, 35);
      this.AUPluginMultiSelect.Mnemocode = "REPLICATION_CONFIG";
      this.AUPluginMultiSelect.Name = "AUPluginMultiSelect";
      this.AUPluginMultiSelect.Size = new System.Drawing.Size(314, 117);
      this.AUPluginMultiSelect.TabIndex = 126;
      this.AUPluginMultiSelect.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.AUPluginMultiSelect_BeforePluginShow);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(17, 185);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 13);
      this.label2.TabIndex = 125;
      this.label2.Text = "Аптека";
      // 
      // ucContractor
      // 
      this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
      this.ucContractor.Location = new System.Drawing.Point(75, 182);
      this.ucContractor.Mnemocode = "REPLICATION_CONFIG";
      this.ucContractor.Name = "ucContractor";
      this.ucContractor.Size = new System.Drawing.Size(255, 21);
      this.ucContractor.TabIndex = 124;
      this.ucContractor.BeforePluginShow += new System.EventHandler(this.ucContractor_BeforePluginShow);
      // 
      // radioDetailed
      // 
      this.radioDetailed.AutoSize = true;
      this.radioDetailed.Location = new System.Drawing.Point(19, 160);
      this.radioDetailed.Name = "radioDetailed";
      this.radioDetailed.Size = new System.Drawing.Size(125, 17);
      this.radioDetailed.TabIndex = 1;
      this.radioDetailed.Text = "Детализированный";
      this.radioDetailed.UseVisualStyleBackColor = true;
      this.radioDetailed.CheckedChanged += new System.EventHandler(this.radioDetailed_CheckedChanged);
      // 
      // radioWhole
      // 
      this.radioWhole.AutoSize = true;
      this.radioWhole.Checked = true;
      this.radioWhole.Location = new System.Drawing.Point(19, 17);
      this.radioWhole.Name = "radioWhole";
      this.radioWhole.Size = new System.Drawing.Size(60, 17);
      this.radioWhole.TabIndex = 0;
      this.radioWhole.TabStop = true;
      this.radioWhole.Text = "Общий";
      this.radioWhole.UseVisualStyleBackColor = true;
      this.radioWhole.CheckedChanged += new System.EventHandler(this.radioWhole_CheckedChanged);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(369, 25);
      this.toolStrip1.TabIndex = 184;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(150, 22);
      this.toolStripButton1.Text = "Значения по умолчанию";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // CheckExchangeParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(369, 351);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.label1);
      this.Name = "CheckExchangeParams";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CheckExchangeParams_FormClosed);
      this.Load += new System.EventHandler(this.CheckExchangeParams_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
    private System.Windows.Forms.RadioButton radioDetailed;
    private System.Windows.Forms.RadioButton radioWhole;
    private System.Windows.Forms.Label label2;
    private ePlus.MetaData.Client.UCPluginMultiSelect AUPluginMultiSelect;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
  }
}