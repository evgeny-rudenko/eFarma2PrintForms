namespace RCBCheques
{
  partial class Cheques
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cheques));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.label3 = new System.Windows.Forms.Label();
      this.toTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.fromTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.timeCheckBox = new System.Windows.Forms.CheckBox();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucKKM = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucKKMUsers = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucCheques = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(430, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(505, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 461);
      this.panel1.Size = new System.Drawing.Size(583, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(583, 25);
      this.toolStrip1.TabIndex = 119;
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
      this.ucPeriod.Location = new System.Drawing.Point(64, 39);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(226, 21);
      this.ucPeriod.TabIndex = 120;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 42);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(48, 13);
      this.label3.TabIndex = 121;
      this.label3.Text = "Период:";
      // 
      // toTimeDateTimePicker
      // 
      this.toTimeDateTimePicker.CustomFormat = "HH.mm";
      this.toTimeDateTimePicker.Enabled = false;
      this.toTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.toTimeDateTimePicker.Location = new System.Drawing.Point(484, 40);
      this.toTimeDateTimePicker.Name = "toTimeDateTimePicker";
      this.toTimeDateTimePicker.ShowUpDown = true;
      this.toTimeDateTimePicker.Size = new System.Drawing.Size(92, 20);
      this.toTimeDateTimePicker.TabIndex = 152;
      this.toTimeDateTimePicker.ValueChanged += new System.EventHandler(this.toTimeDateTimePicker_ValueChanged);
      // 
      // fromTimeDateTimePicker
      // 
      this.fromTimeDateTimePicker.CustomFormat = "HH.mm";
      this.fromTimeDateTimePicker.Enabled = false;
      this.fromTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.fromTimeDateTimePicker.Location = new System.Drawing.Point(379, 40);
      this.fromTimeDateTimePicker.Name = "fromTimeDateTimePicker";
      this.fromTimeDateTimePicker.ShowUpDown = true;
      this.fromTimeDateTimePicker.Size = new System.Drawing.Size(92, 20);
      this.fromTimeDateTimePicker.TabIndex = 151;
      this.fromTimeDateTimePicker.ValueChanged += new System.EventHandler(this.fromTimeDateTimePicker_ValueChanged);
      // 
      // timeCheckBox
      // 
      this.timeCheckBox.AutoSize = true;
      this.timeCheckBox.Location = new System.Drawing.Point(293, 42);
      this.timeCheckBox.Name = "timeCheckBox";
      this.timeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.timeCheckBox.Size = new System.Drawing.Size(59, 17);
      this.timeCheckBox.TabIndex = 150;
      this.timeCheckBox.Text = "Время";
      this.timeCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.timeCheckBox.UseVisualStyleBackColor = false;
      this.timeCheckBox.CheckedChanged += new System.EventHandler(this.timeCheckBox_CheckedChanged);
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склад";
      this.ucStores.Location = new System.Drawing.Point(4, 201);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(284, 124);
      this.ucStores.TabIndex = 153;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // ucContractors
      // 
      this.ucContractors.AllowSaveState = true;
      this.ucContractors.Caption = "Аптека";
      this.ucContractors.Location = new System.Drawing.Point(4, 71);
      this.ucContractors.Mnemocode = "CONTRACTOR";
      this.ucContractors.Name = "ucContractors";
      this.ucContractors.Size = new System.Drawing.Size(284, 124);
      this.ucContractors.TabIndex = 154;
      this.ucContractors.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucContractors_BeforePluginShow);
      // 
      // ucKKM
      // 
      this.ucKKM.AllowSaveState = true;
      this.ucKKM.Caption = "Касса";
      this.ucKKM.Location = new System.Drawing.Point(294, 71);
      this.ucKKM.Mnemocode = "CASH_REGISTER";
      this.ucKKM.Name = "ucKKM";
      this.ucKKM.Size = new System.Drawing.Size(284, 124);
      this.ucKKM.TabIndex = 155;
      // 
      // ucKKMUsers
      // 
      this.ucKKMUsers.AllowSaveState = true;
      this.ucKKMUsers.Caption = "Кассир";
      this.ucKKMUsers.Location = new System.Drawing.Point(294, 201);
      this.ucKKMUsers.Mnemocode = "CASH_REGISTER_USER";
      this.ucKKMUsers.Name = "ucKKMUsers";
      this.ucKKMUsers.Size = new System.Drawing.Size(284, 124);
      this.ucKKMUsers.TabIndex = 156;
      // 
      // ucCheques
      // 
      this.ucCheques.AllowSaveState = true;
      this.ucCheques.Caption = "Чек";
      this.ucCheques.Location = new System.Drawing.Point(4, 331);
      this.ucCheques.Mnemocode = "CHEQUE";
      this.ucCheques.Name = "ucCheques";
      this.ucCheques.Size = new System.Drawing.Size(284, 124);
      this.ucCheques.TabIndex = 157;
      this.ucCheques.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucCheques_BeforePluginShow);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(472, 44);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(10, 13);
      this.label1.TabIndex = 158;
      this.label1.Text = "-";
      // 
      // Cheques
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(583, 490);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucCheques);
      this.Controls.Add(this.ucKKMUsers);
      this.Controls.Add(this.ucKKM);
      this.Controls.Add(this.ucContractors);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.toTimeDateTimePicker);
      this.Controls.Add(this.fromTimeDateTimePicker);
      this.Controls.Add(this.timeCheckBox);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.toolStrip1);
      this.MinimumSize = new System.Drawing.Size(591, 517);
      this.Name = "Cheques";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Cheques_FormClosed);
      this.Load += new System.EventHandler(this.Cheques_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.timeCheckBox, 0);
      this.Controls.SetChildIndex(this.fromTimeDateTimePicker, 0);
      this.Controls.SetChildIndex(this.toTimeDateTimePicker, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.ucContractors, 0);
      this.Controls.SetChildIndex(this.ucKKM, 0);
      this.Controls.SetChildIndex(this.ucKKMUsers, 0);
      this.Controls.SetChildIndex(this.ucCheques, 0);
      this.Controls.SetChildIndex(this.label1, 0);
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
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DateTimePicker toTimeDateTimePicker;
    private System.Windows.Forms.DateTimePicker fromTimeDateTimePicker;
    private System.Windows.Forms.CheckBox timeCheckBox;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucKKM;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucKKMUsers;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucCheques;
    private System.Windows.Forms.Label label1;
  }
}