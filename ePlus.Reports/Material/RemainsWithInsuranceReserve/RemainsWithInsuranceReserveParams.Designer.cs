namespace RemainsWithInsuranceReserve
{
  partial class RemainsWithInsuranceReserveParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemainsWithInsuranceReserveParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.cbRestsOnly = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbWith = new System.Windows.Forms.RadioButton();
      this.rbLess = new System.Windows.Forms.RadioButton();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(218, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(293, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 389);
      this.panel1.Size = new System.Drawing.Size(371, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(371, 25);
      this.toolStrip1.TabIndex = 182;
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
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(12, 212);
      this.ucGoods.Mnemocode = "STOCK";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(347, 133);
      this.ucGoods.TabIndex = 186;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(12, 73);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(347, 133);
      this.ucStores.TabIndex = 185;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // cbRestsOnly
      // 
      this.cbRestsOnly.AutoSize = true;
      this.cbRestsOnly.Enabled = false;
      this.cbRestsOnly.Location = new System.Drawing.Point(15, 351);
      this.cbRestsOnly.Name = "cbRestsOnly";
      this.cbRestsOnly.Size = new System.Drawing.Size(129, 17);
      this.cbRestsOnly.TabIndex = 187;
      this.cbRestsOnly.Text = "Только по остаткам";
      this.cbRestsOnly.UseVisualStyleBackColor = true;
      this.cbRestsOnly.CheckedChanged += new System.EventHandler(this.cbRestsOnly_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rbLess);
      this.groupBox1.Controls.Add(this.rbWith);
      this.groupBox1.Location = new System.Drawing.Point(15, 28);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(341, 39);
      this.groupBox1.TabIndex = 188;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Вид отчета";
      // 
      // rbWith
      // 
      this.rbWith.AutoSize = true;
      this.rbWith.Checked = true;
      this.rbWith.Location = new System.Drawing.Point(6, 16);
      this.rbWith.Name = "rbWith";
      this.rbWith.Size = new System.Drawing.Size(144, 17);
      this.rbWith.TabIndex = 0;
      this.rbWith.TabStop = true;
      this.rbWith.Text = "Со страховым запасом";
      this.rbWith.UseVisualStyleBackColor = true;
      // 
      // rbLess
      // 
      this.rbLess.AutoSize = true;
      this.rbLess.Location = new System.Drawing.Point(168, 16);
      this.rbLess.Name = "rbLess";
      this.rbLess.Size = new System.Drawing.Size(157, 17);
      this.rbLess.TabIndex = 1;
      this.rbLess.Text = "Менее страхового запаса";
      this.rbLess.UseVisualStyleBackColor = true;
      this.rbLess.CheckedChanged += new System.EventHandler(this.rbLess_CheckedChanged);
      // 
      // RemainsWithInsuranceReserveParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(371, 418);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cbRestsOnly);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.toolStrip1);
      this.Name = "RemainsWithInsuranceReserveParams";
      this.Load += new System.EventHandler(this.RemainsWithInsuranceReserveParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemainsWithInsuranceReserveParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.cbRestsOnly, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private System.Windows.Forms.CheckBox cbRestsOnly;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbLess;
    private System.Windows.Forms.RadioButton rbWith;
  }
}