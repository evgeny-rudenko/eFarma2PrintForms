namespace RemainsLessInsuranceReserve
{
  partial class RemainsLessInsuranceReserveParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemainsLessInsuranceReserveParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.cbRestsOnly = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
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
      this.panel1.Location = new System.Drawing.Point(0, 338);
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
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(12, 28);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(347, 133);
      this.ucStores.TabIndex = 183;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(12, 167);
      this.ucGoods.Mnemocode = "STOCK";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(347, 133);
      this.ucGoods.TabIndex = 184;
      // 
      // cbRestsOnly
      // 
      this.cbRestsOnly.AutoSize = true;
      this.cbRestsOnly.Location = new System.Drawing.Point(12, 306);
      this.cbRestsOnly.Name = "cbRestsOnly";
      this.cbRestsOnly.Size = new System.Drawing.Size(129, 17);
      this.cbRestsOnly.TabIndex = 185;
      this.cbRestsOnly.Text = "Только по остаткам";
      this.cbRestsOnly.UseVisualStyleBackColor = true;
      this.cbRestsOnly.CheckedChanged += new System.EventHandler(this.cbRestsOnly_CheckedChanged);
      // 
      // RemainsLessInsuranceReserveParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(371, 367);
      this.Controls.Add(this.cbRestsOnly);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.toolStrip1);
      this.Name = "RemainsLessInsuranceReserveParams";
      this.Load += new System.EventHandler(this.RemainsLessInsuranceReserveParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemainsLessInsuranceReserveParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.cbRestsOnly, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.CheckBox cbRestsOnly;
  }
}