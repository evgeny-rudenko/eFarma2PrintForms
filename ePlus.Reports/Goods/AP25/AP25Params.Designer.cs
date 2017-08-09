namespace AP25
{
  partial class AP25Params
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AP25Params));
      this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
      this.chkShortReport = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.chkShowSub = new System.Windows.Forms.CheckBox();
      this.chkShowAdd = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
      this.rbDocDate = new System.Windows.Forms.RadioButton();
      this.rbDocType = new System.Windows.Forms.RadioButton();
      this.bClose = new System.Windows.Forms.Button();
      this.bPrint = new System.Windows.Forms.Button();
      this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.serviceCheckBox = new System.Windows.Forms.CheckBox();
      this.auCheckBox = new System.Windows.Forms.CheckBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // chkRefreshDocMov
      // 
      this.chkRefreshDocMov.AutoSize = true;
      this.chkRefreshDocMov.Location = new System.Drawing.Point(15, 243);
      this.chkRefreshDocMov.Name = "chkRefreshDocMov";
      this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
      this.chkRefreshDocMov.TabIndex = 128;
      this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
      this.chkRefreshDocMov.UseVisualStyleBackColor = true;
      // 
      // chkShortReport
      // 
      this.chkShortReport.AutoSize = true;
      this.chkShortReport.Location = new System.Drawing.Point(15, 220);
      this.chkShortReport.Name = "chkShortReport";
      this.chkShortReport.Size = new System.Drawing.Size(98, 17);
      this.chkShortReport.TabIndex = 127;
      this.chkShortReport.Text = "Краткий отчет";
      this.chkShortReport.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.chkShowSub);
      this.groupBox2.Controls.Add(this.chkShowAdd);
      this.groupBox2.Location = new System.Drawing.Point(426, 147);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(230, 67);
      this.groupBox2.TabIndex = 126;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Отображение";
      // 
      // chkShowSub
      // 
      this.chkShowSub.AutoSize = true;
      this.chkShowSub.Location = new System.Drawing.Point(7, 42);
      this.chkShowSub.Name = "chkShowSub";
      this.chkShowSub.Size = new System.Drawing.Size(127, 17);
      this.chkShowSub.TabIndex = 1;
      this.chkShowSub.Text = "Показывать расход";
      this.chkShowSub.UseVisualStyleBackColor = true;
      // 
      // chkShowAdd
      // 
      this.chkShowAdd.AutoSize = true;
      this.chkShowAdd.Location = new System.Drawing.Point(7, 19);
      this.chkShowAdd.Name = "chkShowAdd";
      this.chkShowAdd.Size = new System.Drawing.Size(127, 17);
      this.chkShowAdd.TabIndex = 0;
      this.chkShowAdd.Text = "Показывать приход";
      this.chkShowAdd.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkShowSumByDocType);
      this.groupBox1.Controls.Add(this.rbDocDate);
      this.groupBox1.Controls.Add(this.rbDocType);
      this.groupBox1.Location = new System.Drawing.Point(426, 60);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(230, 81);
      this.groupBox1.TabIndex = 125;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Сортировка";
      // 
      // chkShowSumByDocType
      // 
      this.chkShowSumByDocType.AutoSize = true;
      this.chkShowSumByDocType.Location = new System.Drawing.Point(20, 39);
      this.chkShowSumByDocType.Name = "chkShowSumByDocType";
      this.chkShowSumByDocType.Size = new System.Drawing.Size(201, 17);
      this.chkShowSumByDocType.TabIndex = 2;
      this.chkShowSumByDocType.Text = "Показывать суммы по операциям";
      this.chkShowSumByDocType.UseVisualStyleBackColor = true;
      // 
      // rbDocDate
      // 
      this.rbDocDate.AutoSize = true;
      this.rbDocDate.Location = new System.Drawing.Point(7, 57);
      this.rbDocDate.Name = "rbDocDate";
      this.rbDocDate.Size = new System.Drawing.Size(136, 17);
      this.rbDocDate.TabIndex = 1;
      this.rbDocDate.Text = "По датам документов";
      this.rbDocDate.UseVisualStyleBackColor = true;
      this.rbDocDate.CheckedChanged += new System.EventHandler(this.rbDocType_CheckedChanged);
      // 
      // rbDocType
      // 
      this.rbDocType.AutoSize = true;
      this.rbDocType.Checked = true;
      this.rbDocType.Location = new System.Drawing.Point(7, 20);
      this.rbDocType.Name = "rbDocType";
      this.rbDocType.Size = new System.Drawing.Size(137, 17);
      this.rbDocType.TabIndex = 0;
      this.rbDocType.TabStop = true;
      this.rbDocType.Text = "По видам документов";
      this.rbDocType.UseVisualStyleBackColor = true;
      this.rbDocType.CheckedChanged += new System.EventHandler(this.rbDocType_CheckedChanged);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(582, 239);
      this.bClose.Name = "bClose";
      this.bClose.Size = new System.Drawing.Size(75, 23);
      this.bClose.TabIndex = 124;
      this.bClose.Text = "Закрыть";
      this.bClose.UseVisualStyleBackColor = true;
      this.bClose.Click += new System.EventHandler(this.bClose_Click);
      // 
      // bPrint
      // 
      this.bPrint.Location = new System.Drawing.Point(501, 239);
      this.bPrint.Name = "bPrint";
      this.bPrint.Size = new System.Drawing.Size(75, 23);
      this.bPrint.TabIndex = 123;
      this.bPrint.Text = "Печать";
      this.bPrint.UseVisualStyleBackColor = true;
      this.bPrint.Click += new System.EventHandler(this.bPrint_Click);
      // 
      // mpsStore
      // 
      this.mpsStore.AllowSaveState = false;
      this.mpsStore.Caption = "Склады";
      this.mpsStore.Location = new System.Drawing.Point(12, 140);
      this.mpsStore.Mnemocode = "STORE";
      this.mpsStore.Name = "mpsStore";
      this.mpsStore.Size = new System.Drawing.Size(408, 74);
      this.mpsStore.TabIndex = 122;
      this.mpsStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsStore_BeforePluginShow);
      // 
      // mpsContractor
      // 
      this.mpsContractor.AllowSaveState = false;
      this.mpsContractor.Caption = "Контрагенты";
      this.mpsContractor.Location = new System.Drawing.Point(12, 60);
      this.mpsContractor.Mnemocode = "CONTRACTOR";
      this.mpsContractor.Name = "mpsContractor";
      this.mpsContractor.Size = new System.Drawing.Size(408, 74);
      this.mpsContractor.TabIndex = 121;
      this.mpsContractor.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsContractor_BeforePluginShow);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 119;
      this.label1.Text = "Период:";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
      this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
      this.ucPeriod.Location = new System.Drawing.Point(66, 35);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(222, 21);
      this.ucPeriod.TabIndex = 118;
      // 
      // serviceCheckBox
      // 
      this.serviceCheckBox.AutoSize = true;
      this.serviceCheckBox.Checked = true;
      this.serviceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.serviceCheckBox.Location = new System.Drawing.Point(15, 266);
      this.serviceCheckBox.Name = "serviceCheckBox";
      this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
      this.serviceCheckBox.TabIndex = 131;
      this.serviceCheckBox.Text = "Учитывать услуги";
      this.serviceCheckBox.UseVisualStyleBackColor = true;
      // 
      // auCheckBox
      // 
      this.auCheckBox.AutoSize = true;
      this.auCheckBox.Location = new System.Drawing.Point(15, 289);
      this.auCheckBox.Name = "auCheckBox";
      this.auCheckBox.Size = new System.Drawing.Size(254, 17);
      this.auCheckBox.TabIndex = 180;
      this.auCheckBox.Text = "Отфильтровать перемещения внутри аптеки";
      this.auCheckBox.UseVisualStyleBackColor = true;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(664, 25);
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
      // AP25Params
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(664, 317);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.auCheckBox);
      this.Controls.Add(this.serviceCheckBox);
      this.Controls.Add(this.chkRefreshDocMov);
      this.Controls.Add(this.chkShortReport);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.bClose);
      this.Controls.Add(this.bPrint);
      this.Controls.Add(this.mpsStore);
      this.Controls.Add(this.mpsContractor);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "AP25Params";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Параметры отчета: Товарный отчет УФ АП-25";
      this.Load += new System.EventHandler(this.AP25Params_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AP25Params_FormClosed);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox chkRefreshDocMov;
    private System.Windows.Forms.CheckBox chkShortReport;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox chkShowSub;
    private System.Windows.Forms.CheckBox chkShowAdd;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbDocDate;
    private System.Windows.Forms.RadioButton rbDocType;
    private System.Windows.Forms.Button bClose;
    private System.Windows.Forms.Button bPrint;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.CheckBox chkShowSumByDocType;
	  private System.Windows.Forms.CheckBox serviceCheckBox;
	  private System.Windows.Forms.CheckBox auCheckBox;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
  }
}