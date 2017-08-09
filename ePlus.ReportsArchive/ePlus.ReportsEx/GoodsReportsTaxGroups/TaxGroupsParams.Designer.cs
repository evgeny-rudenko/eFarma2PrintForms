
namespace GoodsReportsTaxGroups
{
	partial class TaxGroupsParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxGroupsParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
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
			this.label1 = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label2 = new System.Windows.Forms.Label();
			this.chbShowNal = new System.Windows.Forms.CheckBox();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.supVatRadioButton = new System.Windows.Forms.RadioButton();
			this.retVatRadioButton = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(524, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(599, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 322);
			this.panel1.Size = new System.Drawing.Size(677, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(677, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			// 
			// chkRefreshDocMov
			// 
			this.chkRefreshDocMov.AutoSize = true;
			this.chkRefreshDocMov.Location = new System.Drawing.Point(19, 245);
			this.chkRefreshDocMov.Name = "chkRefreshDocMov";
			this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
			this.chkRefreshDocMov.TabIndex = 138;
			this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
			this.chkRefreshDocMov.UseVisualStyleBackColor = true;
			// 
			// chkShortReport
			// 
			this.chkShortReport.AutoSize = true;
			this.chkShortReport.Location = new System.Drawing.Point(19, 291);
			this.chkShortReport.Name = "chkShortReport";
			this.chkShortReport.Size = new System.Drawing.Size(98, 17);
			this.chkShortReport.TabIndex = 137;
			this.chkShortReport.Text = "Краткий отчет";
			this.chkShortReport.UseVisualStyleBackColor = true;
			this.chkShortReport.Visible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkShowSub);
			this.groupBox2.Controls.Add(this.chkShowAdd);
			this.groupBox2.Location = new System.Drawing.Point(439, 133);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(230, 83);
			this.groupBox2.TabIndex = 136;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Отображение";
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkShowSumByDocType);
			this.groupBox1.Controls.Add(this.rbDocDate);
			this.groupBox1.Controls.Add(this.rbDocType);
			this.groupBox1.Location = new System.Drawing.Point(439, 46);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 81);
			this.groupBox1.TabIndex = 135;
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
			this.mpsStore.Location = new System.Drawing.Point(19, 133);
			this.mpsStore.Mnemocode = "STORE";
			this.mpsStore.Name = "mpsStore";
			this.mpsStore.Size = new System.Drawing.Size(408, 83);
			this.mpsStore.TabIndex = 134;
			// 
			// mpsContractor
			// 
			this.mpsContractor.AllowSaveState = false;
			this.mpsContractor.Caption = "Контрагенты";
			this.mpsContractor.Location = new System.Drawing.Point(19, 46);
			this.mpsContractor.Mnemocode = "CONTRACTOR";
			this.mpsContractor.Name = "mpsContractor";
			this.mpsContractor.Size = new System.Drawing.Size(408, 81);
			this.mpsContractor.TabIndex = 133;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-111, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 132;
			this.label1.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(((long) (0)));
			this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
			this.ucPeriod.Location = new System.Drawing.Point(80, 26);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 131;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 140;
			this.label2.Text = "Период:";
			// 
			// chbShowNal
			// 
			this.chbShowNal.AutoSize = true;
			this.chbShowNal.Location = new System.Drawing.Point(19, 222);
			this.chbShowNal.Name = "chbShowNal";
			this.chbShowNal.Size = new System.Drawing.Size(148, 17);
			this.chbShowNal.TabIndex = 141;
			this.chbShowNal.Text = "Показывать наложение";
			this.chbShowNal.UseVisualStyleBackColor = true;
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(19, 268);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
			this.serviceCheckBox.TabIndex = 142;
			this.serviceCheckBox.Text = "Учитывать услуги";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.retVatRadioButton);
			this.groupBox3.Controls.Add(this.supVatRadioButton);
			this.groupBox3.Location = new System.Drawing.Point(439, 222);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(230, 72);
			this.groupBox3.TabIndex = 143;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Разбивать суммы";
			// 
			// supVatRadioButton
			// 
			this.supVatRadioButton.AutoSize = true;
			this.supVatRadioButton.Checked = true;
			this.supVatRadioButton.Location = new System.Drawing.Point(7, 19);
			this.supVatRadioButton.Name = "supVatRadioButton";
			this.supVatRadioButton.Size = new System.Drawing.Size(117, 17);
			this.supVatRadioButton.TabIndex = 0;
			this.supVatRadioButton.TabStop = true;
			this.supVatRadioButton.Text = "По оптовому НДС";
			this.supVatRadioButton.UseVisualStyleBackColor = true;
			// 
			// retVatRadioButton
			// 
			this.retVatRadioButton.AutoSize = true;
			this.retVatRadioButton.Location = new System.Drawing.Point(7, 43);
			this.retVatRadioButton.Name = "retVatRadioButton";
			this.retVatRadioButton.Size = new System.Drawing.Size(129, 17);
			this.retVatRadioButton.TabIndex = 1;
			this.retVatRadioButton.Text = "По розничному НДС";
			this.retVatRadioButton.UseVisualStyleBackColor = true;
			// 
			// TaxGroupsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 351);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.chbShowNal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chkRefreshDocMov);
			this.Controls.Add(this.chkShortReport);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.mpsStore);
			this.Controls.Add(this.mpsContractor);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Name = "TaxGroupsParams";
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.mpsContractor, 0);
			this.Controls.SetChildIndex(this.mpsStore, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.Controls.SetChildIndex(this.chkShortReport, 0);
			this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.chbShowNal, 0);
			this.Controls.SetChildIndex(this.serviceCheckBox, 0);
			this.Controls.SetChildIndex(this.groupBox3, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
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
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbShowNal;
		private System.Windows.Forms.CheckBox serviceCheckBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton supVatRadioButton;
		private System.Windows.Forms.RadioButton retVatRadioButton;		
	}
}