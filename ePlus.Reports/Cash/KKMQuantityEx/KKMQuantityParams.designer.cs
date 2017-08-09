namespace RCSKKMQuantity
{
	partial class KKMQuantityParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KKMQuantityParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.fromTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.timeCheckBox = new System.Windows.Forms.CheckBox();
			this.periodPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.cashRegistersPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.closeSessionCheckBox = new System.Windows.Forms.CheckBox();
			this.detailCheckBox = new System.Windows.Forms.CheckBox();
			this.kkmLabel = new System.Windows.Forms.Label();
			this.periodLabel = new System.Windows.Forms.Label();
			this.contractorsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.label1 = new System.Windows.Forms.Label();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(310, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(385, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 433);
			this.panel1.Size = new System.Drawing.Size(463, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(463, 25);
			this.toolStrip1.TabIndex = 9;
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
			// toTimeDateTimePicker
			// 
			this.toTimeDateTimePicker.CustomFormat = "HH.mm";
			this.toTimeDateTimePicker.Enabled = false;
			this.toTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.toTimeDateTimePicker.Location = new System.Drawing.Point(192, 69);
			this.toTimeDateTimePicker.Name = "toTimeDateTimePicker";
			this.toTimeDateTimePicker.ShowUpDown = true;
			this.toTimeDateTimePicker.Size = new System.Drawing.Size(92, 20);
			this.toTimeDateTimePicker.TabIndex = 149;
			this.toTimeDateTimePicker.ValueChanged += new System.EventHandler(this.toTimeDateTimePicker_ValueChanged);
			// 
			// fromTimeDateTimePicker
			// 
			this.fromTimeDateTimePicker.CustomFormat = "HH.mm";
			this.fromTimeDateTimePicker.Enabled = false;
			this.fromTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromTimeDateTimePicker.Location = new System.Drawing.Point(88, 69);
			this.fromTimeDateTimePicker.Name = "fromTimeDateTimePicker";
			this.fromTimeDateTimePicker.ShowUpDown = true;
			this.fromTimeDateTimePicker.Size = new System.Drawing.Size(92, 20);
			this.fromTimeDateTimePicker.TabIndex = 148;
			// 
			// timeCheckBox
			// 
			this.timeCheckBox.AutoSize = true;
			this.timeCheckBox.Location = new System.Drawing.Point(10, 69);
			this.timeCheckBox.Name = "timeCheckBox";
			this.timeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.timeCheckBox.Size = new System.Drawing.Size(59, 17);
			this.timeCheckBox.TabIndex = 147;
			this.timeCheckBox.Text = "Время";
			this.timeCheckBox.UseVisualStyleBackColor = false;
			this.timeCheckBox.CheckedChanged += new System.EventHandler(this.timeCheckBox_CheckedChanged);
			// 
			// periodPeriod
			// 
			this.periodPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.periodPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.periodPeriod.Location = new System.Drawing.Point(88, 42);
			this.periodPeriod.Name = "periodPeriod";
			this.periodPeriod.Size = new System.Drawing.Size(328, 21);
			this.periodPeriod.TabIndex = 146;
			// 
			// cashRegistersPluginMultiSelect
			// 
			this.cashRegistersPluginMultiSelect.AllowSaveState = false;
			this.cashRegistersPluginMultiSelect.Caption = null;
			this.cashRegistersPluginMultiSelect.Location = new System.Drawing.Point(87, 97);
			this.cashRegistersPluginMultiSelect.Mnemocode = "CASH_REGISTER";
			this.cashRegistersPluginMultiSelect.Name = "cashRegistersPluginMultiSelect";
			this.cashRegistersPluginMultiSelect.Size = new System.Drawing.Size(359, 84);
			this.cashRegistersPluginMultiSelect.TabIndex = 150;
			// 
			// closeSessionCheckBox
			// 
			this.closeSessionCheckBox.AutoSize = true;
			this.closeSessionCheckBox.Location = new System.Drawing.Point(237, 374);
			this.closeSessionCheckBox.Name = "closeSessionCheckBox";
			this.closeSessionCheckBox.Size = new System.Drawing.Size(115, 17);
			this.closeSessionCheckBox.TabIndex = 152;
			this.closeSessionCheckBox.Text = "Закрытые смены";
			this.closeSessionCheckBox.UseVisualStyleBackColor = true;
			// 
			// detailCheckBox
			// 
			this.detailCheckBox.AutoSize = true;
			this.detailCheckBox.Checked = true;
			this.detailCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.detailCheckBox.Location = new System.Drawing.Point(88, 374);
			this.detailCheckBox.Name = "detailCheckBox";
			this.detailCheckBox.Size = new System.Drawing.Size(143, 17);
			this.detailCheckBox.TabIndex = 151;
			this.detailCheckBox.Text = "Детализация по чекам";
			this.detailCheckBox.UseVisualStyleBackColor = true;
			// 
			// kkmLabel
			// 
			this.kkmLabel.AutoSize = true;
			this.kkmLabel.Location = new System.Drawing.Point(12, 112);
			this.kkmLabel.Name = "kkmLabel";
			this.kkmLabel.Size = new System.Drawing.Size(30, 13);
			this.kkmLabel.TabIndex = 153;
			this.kkmLabel.Text = "ККМ";
			// 
			// periodLabel
			// 
			this.periodLabel.AutoSize = true;
			this.periodLabel.Location = new System.Drawing.Point(12, 42);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(45, 13);
			this.periodLabel.TabIndex = 154;
			this.periodLabel.Text = "Период";
			// 
			// contractorsPluginMultiSelect
			// 
			this.contractorsPluginMultiSelect.AllowSaveState = false;
			this.contractorsPluginMultiSelect.Caption = null;
			this.contractorsPluginMultiSelect.Location = new System.Drawing.Point(87, 187);
			this.contractorsPluginMultiSelect.Mnemocode = "CONTRACTOR";
			this.contractorsPluginMultiSelect.Name = "contractorsPluginMultiSelect";
			this.contractorsPluginMultiSelect.Size = new System.Drawing.Size(359, 84);
			this.contractorsPluginMultiSelect.TabIndex = 155;
			// 
			// storesPluginMultiSelect
			// 
			this.storesPluginMultiSelect.AllowSaveState = false;
			this.storesPluginMultiSelect.Caption = null;
			this.storesPluginMultiSelect.Location = new System.Drawing.Point(87, 277);
			this.storesPluginMultiSelect.Mnemocode = "STORE";
			this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
			this.storesPluginMultiSelect.Size = new System.Drawing.Size(359, 84);
			this.storesPluginMultiSelect.TabIndex = 156;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(181, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(10, 13);
			this.label1.TabIndex = 157;
			this.label1.Text = "-";
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(88, 397);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
			this.serviceCheckBox.TabIndex = 158;
			this.serviceCheckBox.Text = "Учитывать услуги";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// KKMQuantityParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 462);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.storesPluginMultiSelect);
			this.Controls.Add(this.contractorsPluginMultiSelect);
			this.Controls.Add(this.periodLabel);
			this.Controls.Add(this.toTimeDateTimePicker);
			this.Controls.Add(this.fromTimeDateTimePicker);
			this.Controls.Add(this.timeCheckBox);
			this.Controls.Add(this.periodPeriod);
			this.Controls.Add(this.cashRegistersPluginMultiSelect);
			this.Controls.Add(this.closeSessionCheckBox);
			this.Controls.Add(this.detailCheckBox);
			this.Controls.Add(this.kkmLabel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "KKMQuantityParams";
			this.Load += new System.EventHandler(this.KKMQuantityParams_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.kkmLabel, 0);
			this.Controls.SetChildIndex(this.detailCheckBox, 0);
			this.Controls.SetChildIndex(this.closeSessionCheckBox, 0);
			this.Controls.SetChildIndex(this.cashRegistersPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.periodPeriod, 0);
			this.Controls.SetChildIndex(this.timeCheckBox, 0);
			this.Controls.SetChildIndex(this.fromTimeDateTimePicker, 0);
			this.Controls.SetChildIndex(this.toTimeDateTimePicker, 0);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.contractorsPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.serviceCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.DateTimePicker toTimeDateTimePicker;
		private System.Windows.Forms.DateTimePicker fromTimeDateTimePicker;
		private System.Windows.Forms.CheckBox timeCheckBox;
		private ePlus.MetaData.Client.UCPeriod periodPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect cashRegistersPluginMultiSelect;
		private System.Windows.Forms.CheckBox closeSessionCheckBox;
		private System.Windows.Forms.CheckBox detailCheckBox;
		private System.Windows.Forms.Label kkmLabel;
		private System.Windows.Forms.Label periodLabel;
		private ePlus.MetaData.Client.UCPluginMultiSelect contractorsPluginMultiSelect;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox serviceCheckBox;
	}
}