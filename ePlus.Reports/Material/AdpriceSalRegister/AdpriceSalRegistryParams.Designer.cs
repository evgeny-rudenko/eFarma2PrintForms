namespace RCBAdpriceSalRegister
{
  partial class AdpriceSalRegistryParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdpriceSalRegistryParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucSuppliers = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucSort = new ePlus.MetaData.Core.ComboBoxEx();
      this.label2 = new System.Windows.Forms.Label();
      this.cbZNVLS = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.cbRestsOnly = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(463, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(538, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 322);
      this.panel1.Size = new System.Drawing.Size(616, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(616, 25);
      this.toolStrip1.TabIndex = 183;
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 185;
      this.label1.Text = "Период: ";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(72, 40);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(228, 21);
      this.ucPeriod.TabIndex = 184;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(315, 37);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(292, 133);
      this.ucStores.TabIndex = 186;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucSuppliers
      // 
      this.ucSuppliers.AllowSaveState = true;
      this.ucSuppliers.Caption = "Поставщики";
      this.ucSuppliers.Location = new System.Drawing.Point(8, 176);
      this.ucSuppliers.Mnemocode = "CONTRACTOR";
      this.ucSuppliers.Name = "ucSuppliers";
      this.ucSuppliers.Size = new System.Drawing.Size(292, 133);
      this.ucSuppliers.TabIndex = 187;
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(315, 176);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(292, 133);
      this.ucGoods.TabIndex = 188;
      // 
      // ucSort
      // 
      this.ucSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ucSort.FormattingEnabled = true;
      this.ucSort.Location = new System.Drawing.Point(90, 87);
      this.ucSort.Name = "ucSort";
      this.ucSort.Size = new System.Drawing.Size(202, 21);
      this.ucSort.TabIndex = 189;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 90);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 190;
      this.label2.Text = "Сортировка: ";
      // 
      // cbZNVLS
      // 
      this.cbZNVLS.AutoSize = true;
      this.cbZNVLS.Location = new System.Drawing.Point(122, 140);
      this.cbZNVLS.Name = "cbZNVLS";
      this.cbZNVLS.Size = new System.Drawing.Size(15, 14);
      this.cbZNVLS.TabIndex = 191;
      this.cbZNVLS.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(11, 140);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 13);
      this.label3.TabIndex = 192;
      this.label3.Text = "Только ЖНВЛС: ";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(166, 140);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(93, 13);
      this.label4.TabIndex = 194;
      this.label4.Text = "Только остатки: ";
      // 
      // cbRestsOnly
      // 
      this.cbRestsOnly.AutoSize = true;
      this.cbRestsOnly.Location = new System.Drawing.Point(277, 140);
      this.cbRestsOnly.Name = "cbRestsOnly";
      this.cbRestsOnly.Size = new System.Drawing.Size(15, 14);
      this.cbRestsOnly.TabIndex = 193;
      this.cbRestsOnly.UseVisualStyleBackColor = true;
      // 
      // AdpriceSalRegistryParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(616, 351);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbRestsOnly);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbZNVLS);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ucSort);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucSuppliers);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.toolStrip1);
      this.Name = "AdpriceSalRegistryParams";
      this.Load += new System.EventHandler(this.AdpriceSalRegistryParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdpriceSalRegistryParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucSuppliers, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.ucSort, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.cbZNVLS, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbRestsOnly, 0);
      this.Controls.SetChildIndex(this.label4, 0);
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
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucSuppliers;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private ePlus.MetaData.Core.ComboBoxEx ucSort;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox cbZNVLS;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox cbRestsOnly;
  }
}