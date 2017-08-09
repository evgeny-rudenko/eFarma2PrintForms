namespace RCBStockPrognosis
{
  partial class StockPrognosis
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockPrognosis));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucDrugstores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.cbZeroPrognosisGoods = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(479, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(554, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 328);
      this.panel1.Size = new System.Drawing.Size(632, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(632, 25);
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
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(327, 181);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(292, 133);
      this.ucStores.TabIndex = 197;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucDrugstores
      // 
      this.ucDrugstores.AllowSaveState = true;
      this.ucDrugstores.Caption = "Аптеки";
      this.ucDrugstores.Location = new System.Drawing.Point(327, 38);
      this.ucDrugstores.Mnemocode = "CONTRACTOR";
      this.ucDrugstores.Name = "ucDrugstores";
      this.ucDrugstores.Size = new System.Drawing.Size(292, 133);
      this.ucDrugstores.TabIndex = 196;
      this.ucDrugstores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucDrugstores_BeforePluginShow);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 45);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 199;
      this.label1.Text = "Период: ";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(81, 42);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(228, 21);
      this.ucPeriod.TabIndex = 198;
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(13, 107);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(292, 207);
      this.ucGoods.TabIndex = 200;
      // 
      // cbZeroPrognosisGoods
      // 
      this.cbZeroPrognosisGoods.AutoSize = true;
      this.cbZeroPrognosisGoods.Location = new System.Drawing.Point(16, 72);
      this.cbZeroPrognosisGoods.Name = "cbZeroPrognosisGoods";
      this.cbZeroPrognosisGoods.Size = new System.Drawing.Size(190, 17);
      this.cbZeroPrognosisGoods.TabIndex = 201;
      this.cbZeroPrognosisGoods.Text = "Товары с \"нулевым\" прогнозом";
      this.cbZeroPrognosisGoods.UseVisualStyleBackColor = true;
      // 
      // StockPrognosis
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(632, 357);
      this.Controls.Add(this.cbZeroPrognosisGoods);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucDrugstores);
      this.Controls.Add(this.toolStrip1);
      this.Name = "StockPrognosis";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StockPrognosis_FormClosed);
      this.Load += new System.EventHandler(this.StockPrognosis_Load);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucDrugstores, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.cbZeroPrognosisGoods, 0);
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
    private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugstores;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.CheckBox cbZeroPrognosisGoods;
  }
}