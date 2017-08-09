namespace RCSNonLiquidGoods
{
	partial class NonLiquidGoodsParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NonLiquidGoodsParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.periodLabel = new System.Windows.Forms.Label();
			this.periodPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.showLotsCheckBox = new System.Windows.Forms.CheckBox();
			this.reportTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.timeLabel = new System.Windows.Forms.Label();
			this.timeTextBox = new System.Windows.Forms.TextBox();
			this.percentTextBox = new System.Windows.Forms.TextBox();
			this.percentLabel = new System.Windows.Forms.Label();
			this.groupCheckBox = new System.Windows.Forms.CheckBox();
			this.chargeLabel = new System.Windows.Forms.Label();
			this.docsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(288, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(363, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 366);
			this.panel1.Size = new System.Drawing.Size(441, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(441, 25);
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
			// periodLabel
			// 
			this.periodLabel.AutoSize = true;
			this.periodLabel.Location = new System.Drawing.Point(12, 69);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(48, 13);
			this.periodLabel.TabIndex = 11;
			this.periodLabel.Text = "Период:";
			// 
			// periodPeriod
			// 
			this.periodPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.periodPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.periodPeriod.Location = new System.Drawing.Point(83, 65);
			this.periodPeriod.Name = "periodPeriod";
			this.periodPeriod.Size = new System.Drawing.Size(256, 21);
			this.periodPeriod.TabIndex = 12;
			// 
			// storesPluginMultiSelect
			// 
			this.storesPluginMultiSelect.AllowSaveState = false;
			this.storesPluginMultiSelect.Caption = "Склады";
			this.storesPluginMultiSelect.Location = new System.Drawing.Point(15, 102);
			this.storesPluginMultiSelect.Mnemocode = "STORE";
			this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
			this.storesPluginMultiSelect.Size = new System.Drawing.Size(404, 89);
			this.storesPluginMultiSelect.TabIndex = 112;
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(21, 290);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 126;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// showLotsCheckBox
			// 
			this.showLotsCheckBox.AutoSize = true;
			this.showLotsCheckBox.Location = new System.Drawing.Point(21, 311);
			this.showLotsCheckBox.Name = "showLotsCheckBox";
			this.showLotsCheckBox.Size = new System.Drawing.Size(127, 17);
			this.showLotsCheckBox.TabIndex = 113;
			this.showLotsCheckBox.Text = "Показывать партии";
			this.showLotsCheckBox.UseVisualStyleBackColor = true;
			// 
			// reportTypeComboBox
			// 
			this.reportTypeComboBox.FormattingEnabled = true;
			this.reportTypeComboBox.Items.AddRange(new object[] {
            "Без доп параметров",
            "С учетом непродаваемости в периоде",
            "Без учета непродаваемости в периоде"});
			this.reportTypeComboBox.Location = new System.Drawing.Point(83, 31);
			this.reportTypeComboBox.Name = "reportTypeComboBox";
			this.reportTypeComboBox.Size = new System.Drawing.Size(336, 21);
			this.reportTypeComboBox.TabIndex = 127;
			this.reportTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.reportTypeComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 128;
			this.label1.Text = "Вид отчёта:";
			// 
			// timeLabel
			// 
			this.timeLabel.AutoSize = true;
			this.timeLabel.Location = new System.Drawing.Point(18, 219);
			this.timeLabel.Name = "timeLabel";
			this.timeLabel.Size = new System.Drawing.Size(161, 13);
			this.timeLabel.TabIndex = 129;
			this.timeLabel.Text = "Время жизни поставки более:";
			// 
			// timeTextBox
			// 
			this.timeTextBox.Location = new System.Drawing.Point(185, 216);
			this.timeTextBox.MaxLength = 5;
			this.timeTextBox.Name = "timeTextBox";
			this.timeTextBox.Size = new System.Drawing.Size(44, 20);
			this.timeTextBox.TabIndex = 130;
			this.timeTextBox.Text = "75";
			this.timeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// percentTextBox
			// 
			this.percentTextBox.Location = new System.Drawing.Point(185, 244);
			this.percentTextBox.MaxLength = 3;
			this.percentTextBox.Name = "percentTextBox";
			this.percentTextBox.Size = new System.Drawing.Size(44, 20);
			this.percentTextBox.TabIndex = 132;
			this.percentTextBox.Text = "50";
			this.percentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// percentLabel
			// 
			this.percentLabel.AutoSize = true;
			this.percentLabel.Location = new System.Drawing.Point(18, 247);
			this.percentLabel.Name = "percentLabel";
			this.percentLabel.Size = new System.Drawing.Size(148, 13);
			this.percentLabel.TabIndex = 131;
			this.percentLabel.Text = "Уровень текущего остатка:";
			// 
			// groupCheckBox
			// 
			this.groupCheckBox.AutoSize = true;
			this.groupCheckBox.Location = new System.Drawing.Point(21, 334);
			this.groupCheckBox.Name = "groupCheckBox";
			this.groupCheckBox.Size = new System.Drawing.Size(151, 17);
			this.groupCheckBox.TabIndex = 133;
			this.groupCheckBox.Text = "Сворачивать по группам";
			this.groupCheckBox.UseVisualStyleBackColor = true;
			// 
			// chargeLabel
			// 
			this.chargeLabel.AutoSize = true;
			this.chargeLabel.Location = new System.Drawing.Point(251, 200);
			this.chargeLabel.Name = "chargeLabel";
			this.chargeLabel.Size = new System.Drawing.Size(46, 13);
			this.chargeLabel.TabIndex = 135;
			this.chargeLabel.Text = "Расход:";
			// 
			// docsCheckedListBox
			// 
			this.docsCheckedListBox.FormattingEnabled = true;
			this.docsCheckedListBox.Location = new System.Drawing.Point(254, 216);
			this.docsCheckedListBox.Name = "docsCheckedListBox";
			this.docsCheckedListBox.Size = new System.Drawing.Size(165, 64);
			this.docsCheckedListBox.TabIndex = 134;
			this.docsCheckedListBox.ThreeDCheckBoxes = true;
			// 
			// NonLiquidGoodsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 395);
			this.Controls.Add(this.chargeLabel);
			this.Controls.Add(this.docsCheckedListBox);
			this.Controls.Add(this.percentTextBox);
			this.Controls.Add(this.groupCheckBox);
			this.Controls.Add(this.percentLabel);
			this.Controls.Add(this.timeTextBox);
			this.Controls.Add(this.showLotsCheckBox);
			this.Controls.Add(this.timeLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.reportTypeComboBox);
			this.Controls.Add(this.periodPeriod);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.storesPluginMultiSelect);
			this.Controls.Add(this.periodLabel);
			this.Name = "NonLiquidGoodsParams";
			this.Load += new System.EventHandler(this.NonLiquidGoodsParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NonLiquidGoodsParams_FormClosed);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.Controls.SetChildIndex(this.periodPeriod, 0);
			this.Controls.SetChildIndex(this.reportTypeComboBox, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.timeLabel, 0);
			this.Controls.SetChildIndex(this.showLotsCheckBox, 0);
			this.Controls.SetChildIndex(this.timeTextBox, 0);
			this.Controls.SetChildIndex(this.percentLabel, 0);
			this.Controls.SetChildIndex(this.groupCheckBox, 0);
			this.Controls.SetChildIndex(this.percentTextBox, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.docsCheckedListBox, 0);
			this.Controls.SetChildIndex(this.chargeLabel, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label periodLabel;
		private ePlus.MetaData.Client.UCPeriod periodPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.CheckBox showLotsCheckBox;
		private System.Windows.Forms.ComboBox reportTypeComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label timeLabel;
		private System.Windows.Forms.TextBox timeTextBox;
		private System.Windows.Forms.TextBox percentTextBox;
		private System.Windows.Forms.Label percentLabel;
		private System.Windows.Forms.CheckBox groupCheckBox;
		private System.Windows.Forms.Label chargeLabel;
		private System.Windows.Forms.CheckedListBox docsCheckedListBox;
	}
}