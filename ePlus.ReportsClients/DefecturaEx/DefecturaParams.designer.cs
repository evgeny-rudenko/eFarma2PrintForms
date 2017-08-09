namespace DefecturaEx
{
	partial class DefecturaParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefecturaParams));
			this.minValueLabel = new System.Windows.Forms.Label();
			this.sortLabel = new System.Windows.Forms.Label();
			this.sortComboBox = new System.Windows.Forms.ComboBox();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.oaCheckBox = new System.Windows.Forms.CheckBox();
			this.groupCheckBox = new System.Windows.Forms.CheckBox();
			this.esCheckBox = new System.Windows.Forms.CheckBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.periodLabel = new System.Windows.Forms.Label();
			this.minValueTextBox = new System.Windows.Forms.TextBox();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.reserveCheckBox = new System.Windows.Forms.CheckBox();
			this.docsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.chargeLabel = new System.Windows.Forms.Label();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.auCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(434, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(509, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 388);
			this.panel1.Size = new System.Drawing.Size(587, 29);
			// 
			// minValueLabel
			// 
			this.minValueLabel.AutoSize = true;
			this.minValueLabel.Location = new System.Drawing.Point(12, 70);
			this.minValueLabel.Name = "minValueLabel";
			this.minValueLabel.Size = new System.Drawing.Size(126, 13);
			this.minValueLabel.TabIndex = 11;
			this.minValueLabel.Text = "Минимальный остаток:";
			// 
			// sortLabel
			// 
			this.sortLabel.AutoSize = true;
			this.sortLabel.Location = new System.Drawing.Point(12, 99);
			this.sortLabel.Name = "sortLabel";
			this.sortLabel.Size = new System.Drawing.Size(70, 13);
			this.sortLabel.TabIndex = 12;
			this.sortLabel.Text = "Сортировка:";
			// 
			// sortComboBox
			// 
			this.sortComboBox.FormattingEnabled = true;
			this.sortComboBox.Items.AddRange(new object[] {
            "Товар",
            "Поставщик"});
			this.sortComboBox.Location = new System.Drawing.Point(158, 96);
			this.sortComboBox.Name = "sortComboBox";
			this.sortComboBox.Size = new System.Drawing.Size(232, 21);
			this.sortComboBox.TabIndex = 14;
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = true;
			this.ucGoods.Caption = "Товар";
			this.ucGoods.Location = new System.Drawing.Point(15, 214);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(560, 85);
			this.ucGoods.TabIndex = 15;
			// 
			// oaCheckBox
			// 
			this.oaCheckBox.AutoSize = true;
			this.oaCheckBox.Location = new System.Drawing.Point(15, 305);
			this.oaCheckBox.Name = "oaCheckBox";
			this.oaCheckBox.Size = new System.Drawing.Size(189, 17);
			this.oaCheckBox.TabIndex = 18;
			this.oaCheckBox.Text = "Только товары с признаком ОА";
			this.oaCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupCheckBox
			// 
			this.groupCheckBox.AutoSize = true;
			this.groupCheckBox.Location = new System.Drawing.Point(15, 328);
			this.groupCheckBox.Name = "groupCheckBox";
			this.groupCheckBox.Size = new System.Drawing.Size(191, 17);
			this.groupCheckBox.TabIndex = 19;
			this.groupCheckBox.Text = "Сворачивать группы по товарам";
			this.groupCheckBox.UseVisualStyleBackColor = true;
			this.groupCheckBox.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
			// 
			// esCheckBox
			// 
			this.esCheckBox.AutoSize = true;
			this.esCheckBox.Location = new System.Drawing.Point(294, 305);
			this.esCheckBox.Name = "esCheckBox";
			this.esCheckBox.Size = new System.Drawing.Size(134, 17);
			this.esCheckBox.TabIndex = 20;
			this.esCheckBox.Text = "Сформировать по ЕС";
			this.esCheckBox.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(587, 25);
			this.toolStrip1.TabIndex = 21;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// periodLabel
			// 
			this.periodLabel.AutoSize = true;
			this.periodLabel.Location = new System.Drawing.Point(12, 40);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(48, 13);
			this.periodLabel.TabIndex = 23;
			this.periodLabel.Text = "Период:";
			// 
			// minValueTextBox
			// 
			this.minValueTextBox.Location = new System.Drawing.Point(158, 67);
			this.minValueTextBox.Name = "minValueTextBox";
			this.minValueTextBox.Size = new System.Drawing.Size(232, 20);
			this.minValueTextBox.TabIndex = 24;
			this.minValueTextBox.Text = "0";
			this.minValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
			this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDateTimePicker.Location = new System.Drawing.Point(158, 36);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(113, 20);
			this.fromDateTimePicker.TabIndex = 25;
			this.fromDateTimePicker.ValueChanged += new System.EventHandler(this.fromDateTimePicker_ValueChanged);
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
			this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.toDateTimePicker.Location = new System.Drawing.Point(277, 36);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(113, 20);
			this.toDateTimePicker.TabIndex = 26;
			this.toDateTimePicker.ValueChanged += new System.EventHandler(this.toDateTimePicker_ValueChanged);
			// 
			// reserveCheckBox
			// 
			this.reserveCheckBox.AutoSize = true;
			this.reserveCheckBox.Location = new System.Drawing.Point(15, 351);
			this.reserveCheckBox.Name = "reserveCheckBox";
			this.reserveCheckBox.Size = new System.Drawing.Size(167, 17);
			this.reserveCheckBox.TabIndex = 27;
			this.reserveCheckBox.Text = "Учитывать товар в резерве";
			this.reserveCheckBox.UseVisualStyleBackColor = true;
			// 
			// docsCheckedListBox
			// 
			this.docsCheckedListBox.FormattingEnabled = true;
			this.docsCheckedListBox.Location = new System.Drawing.Point(410, 53);
			this.docsCheckedListBox.Name = "docsCheckedListBox";
			this.docsCheckedListBox.Size = new System.Drawing.Size(165, 64);
			this.docsCheckedListBox.TabIndex = 28;
			this.docsCheckedListBox.ThreeDCheckBoxes = true;
			// 
			// chargeLabel
			// 
			this.chargeLabel.AutoSize = true;
			this.chargeLabel.Location = new System.Drawing.Point(407, 32);
			this.chargeLabel.Name = "chargeLabel";
			this.chargeLabel.Size = new System.Drawing.Size(46, 13);
			this.chargeLabel.TabIndex = 29;
			this.chargeLabel.Text = "Расход:";
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(294, 328);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 124;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(15, 123);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(273, 85);
			this.ucContractors.TabIndex = 125;
			// 
			// ucStores
			// 
			this.ucStores.AllowSaveState = true;
			this.ucStores.Caption = "Склады";
			this.ucStores.Location = new System.Drawing.Point(294, 123);
			this.ucStores.Mnemocode = "STORE";
			this.ucStores.Name = "ucStores";
			this.ucStores.Size = new System.Drawing.Size(281, 85);
			this.ucStores.TabIndex = 126;
			// 
			// auCheckBox
			// 
			this.auCheckBox.AutoSize = true;
			this.auCheckBox.Location = new System.Drawing.Point(294, 351);
			this.auCheckBox.Name = "auCheckBox";
			this.auCheckBox.Size = new System.Drawing.Size(234, 17);
			this.auCheckBox.TabIndex = 127;
			this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
			this.auCheckBox.UseVisualStyleBackColor = true;
			// 
			// DefecturaParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 417);
			this.Controls.Add(this.auCheckBox);
			this.Controls.Add(this.ucStores);
			this.Controls.Add(this.ucContractors);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.chargeLabel);
			this.Controls.Add(this.docsCheckedListBox);
			this.Controls.Add(this.reserveCheckBox);
			this.Controls.Add(this.toDateTimePicker);
			this.Controls.Add(this.fromDateTimePicker);
			this.Controls.Add(this.minValueTextBox);
			this.Controls.Add(this.periodLabel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.esCheckBox);
			this.Controls.Add(this.groupCheckBox);
			this.Controls.Add(this.oaCheckBox);
			this.Controls.Add(this.sortComboBox);
			this.Controls.Add(this.sortLabel);
			this.Controls.Add(this.minValueLabel);
			this.Controls.Add(this.ucGoods);
			this.Name = "DefecturaParams";
			this.Load += new System.EventHandler(this.DefecturaParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefecturaParams_FormClosed);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.minValueLabel, 0);
			this.Controls.SetChildIndex(this.sortLabel, 0);
			this.Controls.SetChildIndex(this.sortComboBox, 0);
			this.Controls.SetChildIndex(this.oaCheckBox, 0);
			this.Controls.SetChildIndex(this.groupCheckBox, 0);
			this.Controls.SetChildIndex(this.esCheckBox, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.minValueTextBox, 0);
			this.Controls.SetChildIndex(this.fromDateTimePicker, 0);
			this.Controls.SetChildIndex(this.toDateTimePicker, 0);
			this.Controls.SetChildIndex(this.reserveCheckBox, 0);
			this.Controls.SetChildIndex(this.docsCheckedListBox, 0);
			this.Controls.SetChildIndex(this.chargeLabel, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucStores, 0);
			this.Controls.SetChildIndex(this.auCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label minValueLabel;
		private System.Windows.Forms.Label sortLabel;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private System.Windows.Forms.ComboBox sortComboBox;
		private System.Windows.Forms.CheckBox oaCheckBox;
		private System.Windows.Forms.CheckBox groupCheckBox;
		private System.Windows.Forms.CheckBox esCheckBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label periodLabel;
		private System.Windows.Forms.TextBox minValueTextBox;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
		private System.Windows.Forms.CheckBox reserveCheckBox;
		private System.Windows.Forms.CheckedListBox docsCheckedListBox;
		private System.Windows.Forms.Label chargeLabel;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.CheckBox auCheckBox;
	}
}