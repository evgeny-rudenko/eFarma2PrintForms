namespace Torg29ORNEx
{
	partial class TORG29_ORN
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TORG29_ORN));
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
      this.rbDocDate = new System.Windows.Forms.RadioButton();
      this.rbDocType = new System.Windows.Forms.RadioButton();
      this.chkShowSub = new System.Windows.Forms.CheckBox();
      this.chkShowAdd = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
      this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.chShortReport = new System.Windows.Forms.CheckBox();
      this.serviceCheckBox = new System.Windows.Forms.CheckBox();
      this.auCheckBox = new System.Windows.Forms.CheckBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(502, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(577, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 309);
      this.panel1.Size = new System.Drawing.Size(655, 29);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkShowSumByDocType);
      this.groupBox1.Controls.Add(this.rbDocDate);
      this.groupBox1.Controls.Add(this.rbDocType);
      this.groupBox1.Location = new System.Drawing.Point(416, 56);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(230, 81);
      this.groupBox1.TabIndex = 162;
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
      // chkShowSub
      // 
      this.chkShowSub.AutoSize = true;
      this.chkShowSub.Checked = true;
      this.chkShowSub.CheckState = System.Windows.Forms.CheckState.Checked;
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
      this.chkShowAdd.Checked = true;
      this.chkShowAdd.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowAdd.Location = new System.Drawing.Point(7, 19);
      this.chkShowAdd.Name = "chkShowAdd";
      this.chkShowAdd.Size = new System.Drawing.Size(127, 17);
      this.chkShowAdd.TabIndex = 0;
      this.chkShowAdd.Text = "Показывать приход";
      this.chkShowAdd.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.chkShowSub);
      this.groupBox2.Controls.Add(this.chkShowAdd);
      this.groupBox2.Location = new System.Drawing.Point(416, 143);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(230, 67);
      this.groupBox2.TabIndex = 163;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Отображение";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(2, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 159;
      this.label1.Text = "Период:";
      // 
      // chkRefreshDocMov
      // 
      this.chkRefreshDocMov.AutoSize = true;
      this.chkRefreshDocMov.Location = new System.Drawing.Point(5, 238);
      this.chkRefreshDocMov.Name = "chkRefreshDocMov";
      this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
      this.chkRefreshDocMov.TabIndex = 165;
      this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
      this.chkRefreshDocMov.UseVisualStyleBackColor = true;
      // 
      // mpsContractor
      // 
      this.mpsContractor.AllowSaveState = false;
      this.mpsContractor.Caption = "Контрагенты";
      this.mpsContractor.Location = new System.Drawing.Point(2, 56);
      this.mpsContractor.Mnemocode = "CONTRACTOR";
      this.mpsContractor.Name = "mpsContractor";
      this.mpsContractor.Size = new System.Drawing.Size(408, 74);
      this.mpsContractor.TabIndex = 160;
      this.mpsContractor.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsContractor_BeforePluginShow);
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
      this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
      this.ucPeriod.Location = new System.Drawing.Point(56, 31);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(222, 21);
      this.ucPeriod.TabIndex = 158;
      // 
      // mpsStore
      // 
      this.mpsStore.AllowSaveState = false;
      this.mpsStore.Caption = "Склады";
      this.mpsStore.Location = new System.Drawing.Point(2, 136);
      this.mpsStore.Mnemocode = "STORE";
      this.mpsStore.Name = "mpsStore";
      this.mpsStore.Size = new System.Drawing.Size(408, 74);
      this.mpsStore.TabIndex = 161;
      this.mpsStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsStore_BeforePluginShow);
      // 
      // chShortReport
      // 
      this.chShortReport.AutoSize = true;
      this.chShortReport.Location = new System.Drawing.Point(5, 215);
      this.chShortReport.Name = "chShortReport";
      this.chShortReport.Size = new System.Drawing.Size(98, 17);
      this.chShortReport.TabIndex = 166;
      this.chShortReport.Text = "Краткий отчет";
      this.chShortReport.UseVisualStyleBackColor = true;
      // 
      // serviceCheckBox
      // 
      this.serviceCheckBox.AutoSize = true;
      this.serviceCheckBox.Location = new System.Drawing.Point(5, 261);
      this.serviceCheckBox.Name = "serviceCheckBox";
      this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
      this.serviceCheckBox.TabIndex = 167;
      this.serviceCheckBox.Text = "Учитывать услуги";
      this.serviceCheckBox.UseVisualStyleBackColor = true;
      // 
      // auCheckBox
      // 
      this.auCheckBox.AutoSize = true;
      this.auCheckBox.Location = new System.Drawing.Point(5, 284);
      this.auCheckBox.Name = "auCheckBox";
      this.auCheckBox.Size = new System.Drawing.Size(254, 17);
      this.auCheckBox.TabIndex = 178;
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
      this.toolStrip1.Size = new System.Drawing.Size(655, 25);
      this.toolStrip1.TabIndex = 183;
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
      // TORG29_ORN
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(655, 338);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.auCheckBox);
      this.Controls.Add(this.serviceCheckBox);
      this.Controls.Add(this.chShortReport);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.mpsContractor);
      this.Controls.Add(this.chkRefreshDocMov);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.mpsStore);
      this.Name = "TORG29_ORN";
      this.Load += new System.EventHandler(this.TORG29_ORN_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TORG29_ORN_FormClosed);
      this.Controls.SetChildIndex(this.mpsStore, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
      this.Controls.SetChildIndex(this.mpsContractor, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.chShortReport, 0);
      this.Controls.SetChildIndex(this.serviceCheckBox, 0);
      this.Controls.SetChildIndex(this.auCheckBox, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkShowSumByDocType;
		private System.Windows.Forms.RadioButton rbDocDate;
		private System.Windows.Forms.RadioButton rbDocType;
		private System.Windows.Forms.CheckBox chkShowSub;
		private System.Windows.Forms.CheckBox chkShowAdd;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkRefreshDocMov;
		private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
        private System.Windows.Forms.CheckBox chShortReport;
		private System.Windows.Forms.CheckBox serviceCheckBox;
		private System.Windows.Forms.CheckBox auCheckBox;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
	}
}