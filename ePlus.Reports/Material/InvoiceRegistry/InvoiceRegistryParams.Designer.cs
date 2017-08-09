namespace InvoiceRegistry
{
  partial class InvoiceRegistryParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceRegistryParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.ucSuppliers = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(190, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(265, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 377);
      this.panel1.Size = new System.Drawing.Size(343, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(343, 25);
      this.toolStrip1.TabIndex = 182;
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 186;
      this.label1.Text = "Период: ";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(107, 34);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(224, 21);
      this.ucPeriod.TabIndex = 185;
      // 
      // ucSuppliers
      // 
      this.ucSuppliers.AllowSaveState = true;
      this.ucSuppliers.Caption = "Поставщики";
      this.ucSuppliers.Location = new System.Drawing.Point(12, 208);
      this.ucSuppliers.Mnemocode = "CONTRACTOR";
      this.ucSuppliers.Name = "ucSuppliers";
      this.ucSuppliers.Size = new System.Drawing.Size(319, 133);
      this.ucSuppliers.TabIndex = 187;
      this.ucSuppliers.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucSuppliers_BeforePluginShow);
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады \"Кому\"";
      this.ucStores.Location = new System.Drawing.Point(12, 64);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(319, 133);
      this.ucStores.TabIndex = 188;
      // 
      // InvoiceRegistryParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(343, 406);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucSuppliers);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.toolStrip1);
      this.Name = "InvoiceRegistryParams";
      this.Load += new System.EventHandler(this.InvoiceRegistryParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InvoiceRegistryParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucSuppliers, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucSuppliers;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
  }
}