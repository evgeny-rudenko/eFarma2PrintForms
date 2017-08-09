namespace RCBSupRestsSummNDS
{
  partial class SupRestsSummNDS
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupRestsSummNDS));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucDate = new System.Windows.Forms.DateTimePicker();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucDrugstores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(165, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(240, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 351);
      this.panel1.Size = new System.Drawing.Size(318, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(318, 25);
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
      // ucDate
      // 
      this.ucDate.CustomFormat = "dd.MM.yyyy";
      this.ucDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.ucDate.Location = new System.Drawing.Point(138, 39);
      this.ucDate.Name = "ucDate";
      this.ucDate.ShowUpDown = true;
      this.ucDate.Size = new System.Drawing.Size(164, 20);
      this.ucDate.TabIndex = 200;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(13, 208);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(292, 133);
      this.ucStores.TabIndex = 199;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucDrugstores
      // 
      this.ucDrugstores.AllowSaveState = true;
      this.ucDrugstores.Caption = "Аптеки";
      this.ucDrugstores.Location = new System.Drawing.Point(13, 69);
      this.ucDrugstores.Mnemocode = "CONTRACTOR";
      this.ucDrugstores.Name = "ucDrugstores";
      this.ucDrugstores.Size = new System.Drawing.Size(292, 133);
      this.ucDrugstores.TabIndex = 198;
      this.ucDrugstores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucDrugstores_BeforePluginShow);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(86, 13);
      this.label1.TabIndex = 197;
      this.label1.Text = "Отчетная дата: ";
      // 
      // SupRestsSummNDS
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(318, 380);
      this.Controls.Add(this.ucDate);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucDrugstores);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.toolStrip1);
      this.Name = "SupRestsSummNDS";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SupRestsSummNDS_FormClosed);
      this.Load += new System.EventHandler(this.SupRestsSummNDS_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucDrugstores, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucDate, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.DateTimePicker ucDate;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugstores;
    private System.Windows.Forms.Label label1;
  }
}