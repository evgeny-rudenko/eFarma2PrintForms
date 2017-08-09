namespace RCBAllPharmaciesRetailSales
{
  partial class AllPharmaciesRetailSales
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllPharmaciesRetailSales));
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucDrugstores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.ucGroups = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.cbByGroups = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rbIO = new System.Windows.Forms.RadioButton();
      this.rbCheques = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(467, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(542, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 397);
      this.panel1.Size = new System.Drawing.Size(620, 29);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 40);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 187;
      this.label1.Text = "Период: ";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(76, 37);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(228, 21);
      this.ucPeriod.TabIndex = 186;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(620, 25);
      this.toolStrip1.TabIndex = 188;
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
      // ucDrugstores
      // 
      this.ucDrugstores.AllowSaveState = true;
      this.ucDrugstores.Caption = "Аптеки";
      this.ucDrugstores.Location = new System.Drawing.Point(8, 109);
      this.ucDrugstores.Mnemocode = "CONTRACTOR";
      this.ucDrugstores.Name = "ucDrugstores";
      this.ucDrugstores.Size = new System.Drawing.Size(292, 133);
      this.ucDrugstores.TabIndex = 189;
      this.ucDrugstores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucDrugstores_BeforePluginShow);
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(8, 251);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(292, 133);
      this.ucStores.TabIndex = 190;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(315, 251);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(292, 133);
      this.ucGoods.TabIndex = 191;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbByGroups);
      this.groupBox1.Controls.Add(this.ucGroups);
      this.groupBox1.Location = new System.Drawing.Point(311, 69);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(304, 176);
      this.groupBox1.TabIndex = 193;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Распределение на группы аналогов";
      // 
      // ucGroups
      // 
      this.ucGroups.AllowSaveState = true;
      this.ucGroups.Caption = "Группы аналогов";
      this.ucGroups.Enabled = false;
      this.ucGroups.Location = new System.Drawing.Point(4, 40);
      this.ucGroups.Mnemocode = "GOODS_CLASSIFIER";
      this.ucGroups.Name = "ucGroups";
      this.ucGroups.Size = new System.Drawing.Size(292, 133);
      this.ucGroups.TabIndex = 193;
      // 
      // cbByGroups
      // 
      this.cbByGroups.AutoSize = true;
      this.cbByGroups.Location = new System.Drawing.Point(7, 19);
      this.cbByGroups.Name = "cbByGroups";
      this.cbByGroups.Size = new System.Drawing.Size(151, 17);
      this.cbByGroups.TabIndex = 194;
      this.cbByGroups.Text = "Сворачивать по группам";
      this.cbByGroups.UseVisualStyleBackColor = true;
      this.cbByGroups.CheckedChanged += new System.EventHandler(this.cbByGroups_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rbCheques);
      this.groupBox2.Controls.Add(this.rbIO);
      this.groupBox2.Location = new System.Drawing.Point(114, 67);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(183, 36);
      this.groupBox2.TabIndex = 194;
      this.groupBox2.TabStop = false;
      // 
      // rbIO
      // 
      this.rbIO.AutoSize = true;
      this.rbIO.Location = new System.Drawing.Point(103, 13);
      this.rbIO.Name = "rbIO";
      this.rbIO.Size = new System.Drawing.Size(40, 17);
      this.rbIO.TabIndex = 0;
      this.rbIO.Text = "РН";
      this.rbIO.UseVisualStyleBackColor = true;
      // 
      // rbCheques
      // 
      this.rbCheques.AutoSize = true;
      this.rbCheques.Checked = true;
      this.rbCheques.Location = new System.Drawing.Point(29, 13);
      this.rbCheques.Name = "rbCheques";
      this.rbCheques.Size = new System.Drawing.Size(51, 17);
      this.rbCheques.TabIndex = 1;
      this.rbCheques.TabStop = true;
      this.rbCheques.Text = "Чеки";
      this.rbCheques.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(8, 82);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 195;
      this.label2.Text = "Вид расхода:";
      // 
      // AllPharmaciesRetailSales
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(620, 426);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucDrugstores);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Name = "AllPharmaciesRetailSales";
      this.Load += new System.EventHandler(this.AllPharmaciesRetailSales_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AllPharmaciesRetailSales_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucDrugstores, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugstores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.GroupBox groupBox1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGroups;
    private System.Windows.Forms.CheckBox cbByGroups;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rbCheques;
    private System.Windows.Forms.RadioButton rbIO;
    private System.Windows.Forms.Label label2;
  }
}