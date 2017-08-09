namespace CashRefusals
{
  partial class CashRefusalsParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashRefusalsParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.label1 = new System.Windows.Forms.Label();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucUsers = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label2 = new System.Windows.Forms.Label();
      this.ucSort = new ePlus.MetaData.Core.ComboBoxEx();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(435, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(510, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 322);
      this.panel1.Size = new System.Drawing.Size(588, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(588, 25);
      this.toolStrip1.TabIndex = 22;
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
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
      this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
      this.ucPeriod.Location = new System.Drawing.Point(67, 42);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(229, 21);
      this.ucPeriod.TabIndex = 132;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 46);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 131;
      this.label1.Text = "Период:";
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Аптеки";
      this.ucStores.Location = new System.Drawing.Point(296, 38);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(284, 124);
      this.ucStores.TabIndex = 133;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucUsers
      // 
      this.ucUsers.AllowSaveState = true;
      this.ucUsers.Caption = "Сотрудники АУ";
      this.ucUsers.Location = new System.Drawing.Point(8, 183);
      this.ucUsers.Mnemocode = "USERS";
      this.ucUsers.Name = "ucUsers";
      this.ucUsers.Size = new System.Drawing.Size(284, 124);
      this.ucUsers.TabIndex = 134;
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товары";
      this.ucGoods.Location = new System.Drawing.Point(296, 183);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(284, 124);
      this.ucGoods.TabIndex = 135;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 90);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 192;
      this.label2.Text = "Сортировка: ";
      // 
      // ucSort
      // 
      this.ucSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ucSort.FormattingEnabled = true;
      this.ucSort.Location = new System.Drawing.Point(88, 87);
      this.ucSort.Name = "ucSort";
      this.ucSort.Size = new System.Drawing.Size(202, 21);
      this.ucSort.TabIndex = 191;
      // 
      // CashRefusalsParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(588, 351);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ucSort);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucUsers);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.toolStrip1);
      this.Name = "CashRefusalsParams";
      this.Load += new System.EventHandler(this.CashRefusalsParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CashRefusalsParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucUsers, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.ucSort, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucUsers;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.Label label2;
    private ePlus.MetaData.Core.ComboBoxEx ucSort;
  }
}