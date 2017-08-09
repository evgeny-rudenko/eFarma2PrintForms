namespace GoodsReportsDiscountGroup
{
	partial class DiscountGroupParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscountGroupParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.label2 = new System.Windows.Forms.Label();
			this.chkContractorGroup = new System.Windows.Forms.CheckBox();
			this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
			this.chkShortReport = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkShowSub = new System.Windows.Forms.CheckBox();
			this.chkShowAdd = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
			this.rbDocDate = new System.Windows.Forms.RadioButton();
			this.rbDocType = new System.Windows.Forms.RadioButton();
			this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.chkGroupDiscount = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(523, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(598, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 283);
			this.panel1.Size = new System.Drawing.Size(676, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(676, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 149;
			this.label2.Text = "Период:";
			// 
			// chkContractorGroup
			// 
			this.chkContractorGroup.AutoSize = true;
			this.chkContractorGroup.Location = new System.Drawing.Point(11, 212);
			this.chkContractorGroup.Name = "chkContractorGroup";
			this.chkContractorGroup.Size = new System.Drawing.Size(185, 17);
			this.chkContractorGroup.TabIndex = 148;
			this.chkContractorGroup.Text = "Группировать по контрагентам";
			this.chkContractorGroup.UseVisualStyleBackColor = true;
			// 
			// chkRefreshDocMov
			// 
			this.chkRefreshDocMov.AutoSize = true;
			this.chkRefreshDocMov.Location = new System.Drawing.Point(11, 235);
			this.chkRefreshDocMov.Name = "chkRefreshDocMov";
			this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
			this.chkRefreshDocMov.TabIndex = 147;
			this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
			this.chkRefreshDocMov.UseVisualStyleBackColor = true;
			// 
			// chkShortReport
			// 
			this.chkShortReport.AutoSize = true;
			this.chkShortReport.Location = new System.Drawing.Point(202, 212);
			this.chkShortReport.Name = "chkShortReport";
			this.chkShortReport.Size = new System.Drawing.Size(98, 17);
			this.chkShortReport.TabIndex = 146;
			this.chkShortReport.Text = "Краткий отчет";
			this.chkShortReport.UseVisualStyleBackColor = true;
			this.chkShortReport.Visible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkShowSub);
			this.groupBox2.Controls.Add(this.chkShowAdd);
			this.groupBox2.Location = new System.Drawing.Point(428, 139);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(230, 67);
			this.groupBox2.TabIndex = 145;
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
			this.groupBox1.Location = new System.Drawing.Point(428, 52);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 81);
			this.groupBox1.TabIndex = 144;
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
			this.chkShowSumByDocType.Visible = false;
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
			// 
			// rbDocType
			// 
			this.rbDocType.AutoSize = true;
			this.rbDocType.Checked = true;
			this.rbDocType.Location = new System.Drawing.Point(7, 19);
			this.rbDocType.Name = "rbDocType";
			this.rbDocType.Size = new System.Drawing.Size(137, 17);
			this.rbDocType.TabIndex = 0;
			this.rbDocType.TabStop = true;
			this.rbDocType.Text = "По видам документов";
			this.rbDocType.UseVisualStyleBackColor = true;
			// 
			// mpsStore
			// 
			this.mpsStore.AllowSaveState = false;
			this.mpsStore.Caption = "Склады";
			this.mpsStore.Location = new System.Drawing.Point(8, 132);
			this.mpsStore.Mnemocode = "STORE";
			this.mpsStore.Name = "mpsStore";
			this.mpsStore.Size = new System.Drawing.Size(408, 74);
			this.mpsStore.TabIndex = 143;
			// 
			// mpsContractor
			// 
			this.mpsContractor.AllowSaveState = false;
			this.mpsContractor.Caption = "Контрагенты";
			this.mpsContractor.Location = new System.Drawing.Point(8, 52);
			this.mpsContractor.Mnemocode = "CONTRACTOR";
			this.mpsContractor.Name = "mpsContractor";
			this.mpsContractor.Size = new System.Drawing.Size(408, 74);
			this.mpsContractor.TabIndex = 142;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
			this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
			this.ucPeriod.Location = new System.Drawing.Point(69, 32);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 141;
			// 
			// chkGroupDiscount
			// 
			this.chkGroupDiscount.AutoSize = true;
			this.chkGroupDiscount.Location = new System.Drawing.Point(11, 258);
			this.chkGroupDiscount.Name = "chkGroupDiscount";
			this.chkGroupDiscount.Size = new System.Drawing.Size(158, 17);
			this.chkGroupDiscount.TabIndex = 150;
			this.chkGroupDiscount.Text = "Группировать по скидкам";
			this.chkGroupDiscount.UseVisualStyleBackColor = true;
			// 
			// DiscountGroupParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(676, 312);
			this.Controls.Add(this.chkGroupDiscount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chkContractorGroup);
			this.Controls.Add(this.chkRefreshDocMov);
			this.Controls.Add(this.chkShortReport);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.mpsStore);
			this.Controls.Add(this.mpsContractor);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Name = "DiscountGroupParams";
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.mpsContractor, 0);
			this.Controls.SetChildIndex(this.mpsStore, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.Controls.SetChildIndex(this.chkShortReport, 0);
			this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
			this.Controls.SetChildIndex(this.chkContractorGroup, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.chkGroupDiscount, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkContractorGroup;
		private System.Windows.Forms.CheckBox chkRefreshDocMov;
		private System.Windows.Forms.CheckBox chkShortReport;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkShowSub;
		private System.Windows.Forms.CheckBox chkShowAdd;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkShowSumByDocType;
		private System.Windows.Forms.RadioButton rbDocDate;
		private System.Windows.Forms.RadioButton rbDocType;
		private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
		private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.CheckBox chkGroupDiscount;
	}
}