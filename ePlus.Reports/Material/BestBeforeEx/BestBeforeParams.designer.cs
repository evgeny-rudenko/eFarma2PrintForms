namespace RCSBestBefore
{
	partial class BestBeforeParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BestBeforeParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.includeExpiredComboBox = new System.Windows.Forms.CheckBox();
            this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.chbGoodCode = new System.Windows.Forms.CheckBox();
            this.comboSort = new System.Windows.Forms.ComboBox();
            this.labelSort = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(194, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(269, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 255);
            this.panel1.Size = new System.Drawing.Size(347, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(347, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "По текущим остаткам:";
            // 
            // includeExpiredComboBox
            // 
            this.includeExpiredComboBox.AutoSize = true;
            this.includeExpiredComboBox.Location = new System.Drawing.Point(9, 129);
            this.includeExpiredComboBox.Name = "includeExpiredComboBox";
            this.includeExpiredComboBox.Size = new System.Drawing.Size(157, 17);
            this.includeExpiredComboBox.TabIndex = 103;
            this.includeExpiredComboBox.Text = "Учитывать просроченные";
            this.includeExpiredComboBox.UseVisualStyleBackColor = true;
            // 
            // storesPluginMultiSelect
            // 
            this.storesPluginMultiSelect.AllowSaveState = false;
            this.storesPluginMultiSelect.Caption = null;
            this.storesPluginMultiSelect.Location = new System.Drawing.Point(6, 50);
            this.storesPluginMultiSelect.Mnemocode = "STORE";
            this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
            this.storesPluginMultiSelect.Pinnable = false;
            this.storesPluginMultiSelect.Size = new System.Drawing.Size(333, 73);
            this.storesPluginMultiSelect.TabIndex = 104;
            // 
            // chbGoodCode
            // 
            this.chbGoodCode.AutoSize = true;
            this.chbGoodCode.Location = new System.Drawing.Point(9, 152);
            this.chbGoodCode.Name = "chbGoodCode";
            this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
            this.chbGoodCode.TabIndex = 125;
            this.chbGoodCode.Text = "Отображать код товара ";
            this.chbGoodCode.UseVisualStyleBackColor = true;
            // 
            // comboSort
            // 
            this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSort.FormattingEnabled = true;
            this.comboSort.Items.AddRange(new object[] {
            "Название товара",
            "Срок годности"});
            this.comboSort.Location = new System.Drawing.Point(102, 175);
            this.comboSort.Name = "comboSort";
            this.comboSort.Size = new System.Drawing.Size(155, 21);
            this.comboSort.TabIndex = 134;
            // 
            // labelSort
            // 
            this.labelSort.Location = new System.Drawing.Point(7, 175);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(100, 21);
            this.labelSort.TabIndex = 133;
            this.labelSort.Text = "Сортировать по : ";
            this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BestBeforeParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 284);
            this.Controls.Add(this.comboSort);
            this.Controls.Add(this.labelSort);
            this.Controls.Add(this.chbGoodCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.includeExpiredComboBox);
            this.Controls.Add(this.storesPluginMultiSelect);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BestBeforeParams";
            this.Load += new System.EventHandler(this.BestBeforeParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
            this.Controls.SetChildIndex(this.includeExpiredComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chbGoodCode, 0);
            this.Controls.SetChildIndex(this.labelSort, 0);
            this.Controls.SetChildIndex(this.comboSort, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox includeExpiredComboBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.CheckBox chbGoodCode;
		public System.Windows.Forms.ComboBox comboSort;
		private System.Windows.Forms.Label labelSort;
	}
}