namespace DEFECTURA_EX
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Sort = new System.Windows.Forms.ComboBox();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.cStore = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.label3 = new System.Windows.Forms.Label();
			this.chbOA_ONLY = new System.Windows.Forms.CheckBox();
			this.chBGroups = new System.Windows.Forms.CheckBox();
			this.chBES = new System.Windows.Forms.CheckBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.lbPeriod = new System.Windows.Forms.Label();
			this.minValue = new System.Windows.Forms.TextBox();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(408, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(483, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 291);
			this.panel1.Size = new System.Drawing.Size(561, 29);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Максимальный остаток:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Сортировка:";
			// 
			// Sort
			// 
			this.Sort.FormattingEnabled = true;
			this.Sort.Items.AddRange(new object[] {
            "Товар",
            "Поставщик"});
			this.Sort.Location = new System.Drawing.Point(158, 96);
			this.Sort.Name = "Sort";
			this.Sort.Size = new System.Drawing.Size(232, 21);
			this.Sort.TabIndex = 14;
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "";
			this.ucGoods.Location = new System.Drawing.Point(15, 137);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(497, 77);
			this.ucGoods.TabIndex = 15;
			// 
			// cStore
			// 
			this.cStore.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.cStore.Location = new System.Drawing.Point(56, 220);
			this.cStore.Mnemocode = "STORE";
			this.cStore.Name = "cStore";
			this.cStore.Size = new System.Drawing.Size(456, 20);
			this.cStore.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 223);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "Склад";
			// 
			// chbOA_ONLY
			// 
			this.chbOA_ONLY.AutoSize = true;
			this.chbOA_ONLY.Location = new System.Drawing.Point(15, 253);
			this.chbOA_ONLY.Name = "chbOA_ONLY";
			this.chbOA_ONLY.Size = new System.Drawing.Size(189, 17);
			this.chbOA_ONLY.TabIndex = 18;
			this.chbOA_ONLY.Text = "Только товары с признаком ОА";
			this.chbOA_ONLY.UseVisualStyleBackColor = true;
			// 
			// chBGroups
			// 
			this.chBGroups.AutoSize = true;
			this.chBGroups.Location = new System.Drawing.Point(210, 253);
			this.chBGroups.Name = "chBGroups";
			this.chBGroups.Size = new System.Drawing.Size(191, 17);
			this.chBGroups.TabIndex = 19;
			this.chBGroups.Text = "Сворачивать группы по товарам";
			this.chBGroups.UseVisualStyleBackColor = true;
			// 
			// chBES
			// 
			this.chBES.AutoSize = true;
			this.chBES.Location = new System.Drawing.Point(407, 253);
			this.chBES.Name = "chBES";
			this.chBES.Size = new System.Drawing.Size(134, 17);
			this.chBES.TabIndex = 20;
			this.chBES.Text = "Сформировать по ЕС";
			this.chBES.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(561, 25);
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
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(12, 43);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(45, 13);
			this.lbPeriod.TabIndex = 23;
			this.lbPeriod.Text = "Период";
			// 
			// minValue
			// 
			this.minValue.Location = new System.Drawing.Point(158, 67);
			this.minValue.Name = "minValue";
			this.minValue.Size = new System.Drawing.Size(232, 20);
			this.minValue.TabIndex = 24;
			this.minValue.Text = "0";
			this.minValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
			this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDateTimePicker.Location = new System.Drawing.Point(158, 36);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(113, 20);
			this.fromDateTimePicker.TabIndex = 25;
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
			this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.toDateTimePicker.Location = new System.Drawing.Point(277, 36);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(113, 20);
			this.toDateTimePicker.TabIndex = 26;
			// 
			// DefecturaParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 320);
			this.Controls.Add(this.toDateTimePicker);
			this.Controls.Add(this.fromDateTimePicker);
			this.Controls.Add(this.minValue);
			this.Controls.Add(this.lbPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.chBES);
			this.Controls.Add(this.chBGroups);
			this.Controls.Add(this.chbOA_ONLY);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Sort);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.cStore);
			this.Name = "DefecturaParams";
			this.Controls.SetChildIndex(this.cStore, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.Sort, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.chbOA_ONLY, 0);
			this.Controls.SetChildIndex(this.chBGroups, 0);
			this.Controls.SetChildIndex(this.chBES, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.minValue, 0);
			this.Controls.SetChildIndex(this.fromDateTimePicker, 0);
			this.Controls.SetChildIndex(this.toDateTimePicker, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private System.Windows.Forms.ComboBox Sort;
		private ePlus.MetaData.Client.UCMetaPluginSelect cStore;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chbOA_ONLY;
		private System.Windows.Forms.CheckBox chBGroups;
		private System.Windows.Forms.CheckBox chBES;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label lbPeriod;
		private System.Windows.Forms.TextBox minValue;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
	}
}