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
			this.ucStore = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.storeLabel = new System.Windows.Forms.Label();
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
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(253, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(328, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 373);
			this.panel1.Size = new System.Drawing.Size(406, 29);
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
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "Товар";
			this.ucGoods.Location = new System.Drawing.Point(15, 162);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(375, 77);
			this.ucGoods.TabIndex = 15;
			// 
			// ucStore
			// 
			this.ucStore.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.ucStore.Location = new System.Drawing.Point(158, 123);
			this.ucStore.Mnemocode = "STORE";
			this.ucStore.Name = "ucStore";
			this.ucStore.Size = new System.Drawing.Size(232, 20);
			this.ucStore.TabIndex = 16;
			// 
			// storeLabel
			// 
			this.storeLabel.AutoSize = true;
			this.storeLabel.Location = new System.Drawing.Point(12, 130);
			this.storeLabel.Name = "storeLabel";
			this.storeLabel.Size = new System.Drawing.Size(41, 13);
			this.storeLabel.TabIndex = 17;
			this.storeLabel.Text = "Склад:";
			// 
			// oaCheckBox
			// 
			this.oaCheckBox.AutoSize = true;
			this.oaCheckBox.Location = new System.Drawing.Point(15, 258);
			this.oaCheckBox.Name = "oaCheckBox";
			this.oaCheckBox.Size = new System.Drawing.Size(189, 17);
			this.oaCheckBox.TabIndex = 18;
			this.oaCheckBox.Text = "Только товары с признаком ОА";
			this.oaCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupCheckBox
			// 
			this.groupCheckBox.AutoSize = true;
			this.groupCheckBox.Location = new System.Drawing.Point(15, 281);
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
			this.esCheckBox.Location = new System.Drawing.Point(15, 327);
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
			this.toolStrip1.Size = new System.Drawing.Size(406, 25);
			this.toolStrip1.TabIndex = 21;
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
			this.reserveCheckBox.Location = new System.Drawing.Point(15, 304);
			this.reserveCheckBox.Name = "reserveCheckBox";
			this.reserveCheckBox.Size = new System.Drawing.Size(167, 17);
			this.reserveCheckBox.TabIndex = 27;
			this.reserveCheckBox.Text = "Учитывать товар в резерве";
			this.reserveCheckBox.UseVisualStyleBackColor = true;
			// 
			// docsCheckedListBox
			// 
			this.docsCheckedListBox.FormattingEnabled = true;
			this.docsCheckedListBox.Location = new System.Drawing.Point(224, 280);
			this.docsCheckedListBox.Name = "docsCheckedListBox";
			this.docsCheckedListBox.Size = new System.Drawing.Size(165, 64);
			this.docsCheckedListBox.TabIndex = 28;
			this.docsCheckedListBox.ThreeDCheckBoxes = true;
			// 
			// chargeLabel
			// 
			this.chargeLabel.AutoSize = true;
			this.chargeLabel.Location = new System.Drawing.Point(221, 259);
			this.chargeLabel.Name = "chargeLabel";
			this.chargeLabel.Size = new System.Drawing.Size(46, 13);
			this.chargeLabel.TabIndex = 29;
			this.chargeLabel.Text = "Расход:";
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(15, 350);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 124;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// DefecturaParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 402);
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
			this.Controls.Add(this.storeLabel);
			this.Controls.Add(this.sortComboBox);
			this.Controls.Add(this.sortLabel);
			this.Controls.Add(this.minValueLabel);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.ucStore);
			this.Name = "DefecturaParams";
			this.Load += new System.EventHandler(this.DefecturaParams_Load);
			this.Controls.SetChildIndex(this.ucStore, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.minValueLabel, 0);
			this.Controls.SetChildIndex(this.sortLabel, 0);
			this.Controls.SetChildIndex(this.sortComboBox, 0);
			this.Controls.SetChildIndex(this.storeLabel, 0);
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
		private ePlus.MetaData.Client.UCMetaPluginSelect ucStore;
		private System.Windows.Forms.Label storeLabel;
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
	}
}