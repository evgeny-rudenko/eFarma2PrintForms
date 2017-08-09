using ePlus.CommonEx.Controls;

namespace RCBNDSGroups_Rigla
{
  partial class NDSGroups
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NDSGroups));
        this.ucDate = new System.Windows.Forms.DateTimePicker();
        this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.label1 = new System.Windows.Forms.Label();
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        this.ucGoodsGroups = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.cbRestsOnly = new System.Windows.Forms.CheckBox();
        this.gbSupSal = new System.Windows.Forms.GroupBox();
        this.rbSal = new System.Windows.Forms.RadioButton();
        this.rbSup = new System.Windows.Forms.RadioButton();
        this.cbNoGroups = new System.Windows.Forms.CheckBox();
        this.panel1.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.gbSupSal.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(458, 3);
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(533, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 216);
        this.panel1.Size = new System.Drawing.Size(611, 29);
        // 
        // ucDate
        // 
        this.ucDate.CustomFormat = "dd.MM.yyyy";
        this.ucDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.ucDate.Location = new System.Drawing.Point(106, 35);
        this.ucDate.Name = "ucDate";
        this.ucDate.ShowUpDown = true;
        this.ucDate.Size = new System.Drawing.Size(196, 20);
        this.ucDate.TabIndex = 205;
        // 
        // ucStores
        // 
        this.ucStores.AllowSaveState = true;
        this.ucStores.Caption = "Склады";
        this.ucStores.Location = new System.Drawing.Point(307, 63);
        this.ucStores.Mnemocode = "STORE";
        this.ucStores.Name = "ucStores";
        this.ucStores.Size = new System.Drawing.Size(292, 87);
        this.ucStores.TabIndex = 204;
        this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
        // 
        // ucContractors
        // 
        this.ucContractors.AllowSaveState = true;
        this.ucContractors.Caption = "Контрагенты";
        this.ucContractors.Location = new System.Drawing.Point(1, 65);
        this.ucContractors.Mnemocode = "CONTRACTOR";
        this.ucContractors.Name = "ucContractors";
        this.ucContractors.Size = new System.Drawing.Size(292, 85);
        this.ucContractors.TabIndex = 203;
        this.ucContractors.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucContractors_BeforePluginShow);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(10, 39);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(101, 13);
        this.label1.TabIndex = 202;
        this.label1.Text = "Отчетная дата на: ";
        // 
        // toolStrip1
        // 
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
        this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
        this.toolStrip1.Size = new System.Drawing.Size(611, 25);
        this.toolStrip1.TabIndex = 201;
        this.toolStrip1.Text = "toolStrip1";
        // 
        // toolStripButton1
        // 
        this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
        this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.toolStripButton1.Name = "toolStripButton1";
        this.toolStripButton1.Size = new System.Drawing.Size(166, 22);
        this.toolStripButton1.Text = "Значения по умолчанию";
        this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
        // 
        // ucGoodsGroups
        // 
        this.ucGoodsGroups.AllowSaveState = true;
        this.ucGoodsGroups.Caption = "Группы товаров";
        this.ucGoodsGroups.Location = new System.Drawing.Point(1, 156);
        this.ucGoodsGroups.Mnemocode = "GOODS_GROUP";
        this.ucGoodsGroups.Name = "ucGoodsGroups";
        this.ucGoodsGroups.Size = new System.Drawing.Size(292, 23);
        this.ucGoodsGroups.TabIndex = 206;
        this.ucGoodsGroups.Visible = false;
        // 
        // cbRestsOnly
        // 
        this.cbRestsOnly.AutoSize = true;
        this.cbRestsOnly.Location = new System.Drawing.Point(4, 185);
        this.cbRestsOnly.Name = "cbRestsOnly";
        this.cbRestsOnly.Size = new System.Drawing.Size(106, 17);
        this.cbRestsOnly.TabIndex = 207;
        this.cbRestsOnly.Text = "Только остатки";
        this.cbRestsOnly.UseVisualStyleBackColor = true;
        this.cbRestsOnly.Visible = false;
        // 
        // gbSupSal
        // 
        this.gbSupSal.Controls.Add(this.rbSal);
        this.gbSupSal.Controls.Add(this.rbSup);
        this.gbSupSal.Location = new System.Drawing.Point(319, 156);
        this.gbSupSal.Name = "gbSupSal";
        this.gbSupSal.Size = new System.Drawing.Size(156, 41);
        this.gbSupSal.TabIndex = 208;
        this.gbSupSal.TabStop = false;
        this.gbSupSal.Text = "Тип НДС";
        this.gbSupSal.Visible = false;
        // 
        // rbSal
        // 
        this.rbSal.AutoSize = true;
        this.rbSal.Location = new System.Drawing.Point(45, 48);
        this.rbSal.Name = "rbSal";
        this.rbSal.Size = new System.Drawing.Size(81, 17);
        this.rbSal.TabIndex = 1;
        this.rbSal.Text = "Розничный";
        this.rbSal.UseVisualStyleBackColor = true;
        // 
        // rbSup
        // 
        this.rbSup.AutoSize = true;
        this.rbSup.Checked = true;
        this.rbSup.Location = new System.Drawing.Point(45, 25);
        this.rbSup.Name = "rbSup";
        this.rbSup.Size = new System.Drawing.Size(89, 17);
        this.rbSup.TabIndex = 0;
        this.rbSup.TabStop = true;
        this.rbSup.Text = "Поставщика";
        this.rbSup.UseVisualStyleBackColor = true;
        // 
        // cbNoGroups
        // 
        this.cbNoGroups.AutoSize = true;
        this.cbNoGroups.Location = new System.Drawing.Point(143, 185);
        this.cbNoGroups.Name = "cbNoGroups";
        this.cbNoGroups.Size = new System.Drawing.Size(150, 17);
        this.cbNoGroups.TabIndex = 209;
        this.cbNoGroups.Text = "Не разбивать на группы";
        this.cbNoGroups.UseVisualStyleBackColor = true;
        this.cbNoGroups.Visible = false;
        // 
        // NDSGroups
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(611, 245);
        this.Controls.Add(this.cbNoGroups);
        this.Controls.Add(this.gbSupSal);
        this.Controls.Add(this.cbRestsOnly);
        this.Controls.Add(this.ucGoodsGroups);
        this.Controls.Add(this.ucDate);
        this.Controls.Add(this.ucStores);
        this.Controls.Add(this.ucContractors);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.toolStrip1);
        this.Name = "NDSGroups";
        this.Load += new System.EventHandler(this.NDSGroups_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NDSGroups_FormClosed);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.toolStrip1, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.ucContractors, 0);
        this.Controls.SetChildIndex(this.ucStores, 0);
        this.Controls.SetChildIndex(this.ucDate, 0);
        this.Controls.SetChildIndex(this.ucGoodsGroups, 0);
        this.Controls.SetChildIndex(this.cbRestsOnly, 0);
        this.Controls.SetChildIndex(this.gbSupSal, 0);
        this.Controls.SetChildIndex(this.cbNoGroups, 0);
        this.panel1.ResumeLayout(false);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.gbSupSal.ResumeLayout(false);
        this.gbSupSal.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker ucDate;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroups;
    private System.Windows.Forms.CheckBox cbRestsOnly;
    private System.Windows.Forms.GroupBox gbSupSal;
    private System.Windows.Forms.RadioButton rbSal;
    private System.Windows.Forms.RadioButton rbSup;
      private System.Windows.Forms.CheckBox cbNoGroups;
  }
}