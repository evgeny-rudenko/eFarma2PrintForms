namespace GoodsKeepingTimeListEx
{
	partial class InvoiceRemainsParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceRemainsParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.periodLabel = new System.Windows.Forms.Label();
			this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
			this.filterComboBox = new System.Windows.Forms.ComboBox();
			this.filterLabel = new System.Windows.Forms.Label();
			this.ucPluginMultiSelect1 = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(189, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(264, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 228);
			this.panel1.Size = new System.Drawing.Size(342, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(342, 25);
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
			this.periodLabel.Location = new System.Drawing.Point(10, 36);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(45, 13);
			this.periodLabel.TabIndex = 11;
			this.periodLabel.Text = "Период";
			// 
			// ucPeriod1
			// 
			this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod1.Location = new System.Drawing.Point(74, 36);
			this.ucPeriod1.Name = "ucPeriod1";
			this.ucPeriod1.Size = new System.Drawing.Size(256, 21);
			this.ucPeriod1.TabIndex = 12;
			// 
			// filterComboBox
			// 
			this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filterComboBox.FormattingEnabled = true;
			this.filterComboBox.Items.AddRange(new object[] {
            "Приходные документы",
            "Поставщики",
            "Пустой фильтр"});
			this.filterComboBox.Location = new System.Drawing.Point(74, 63);
			this.filterComboBox.Name = "filterComboBox";
			this.filterComboBox.Size = new System.Drawing.Size(223, 21);
			this.filterComboBox.TabIndex = 131;
			this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
			// 
			// filterLabel
			// 
			this.filterLabel.AutoSize = true;
			this.filterLabel.Location = new System.Drawing.Point(10, 63);
			this.filterLabel.Name = "filterLabel";
			this.filterLabel.Size = new System.Drawing.Size(47, 13);
			this.filterLabel.TabIndex = 132;
			this.filterLabel.Text = "Фильтр";
			// 
			// ucPluginMultiSelect1
			// 
			this.ucPluginMultiSelect1.AllowSaveState = false;
			this.ucPluginMultiSelect1.Caption = "Приходные документы";
			this.ucPluginMultiSelect1.Location = new System.Drawing.Point(13, 90);
			this.ucPluginMultiSelect1.Mnemocode = "INVOICE";
			this.ucPluginMultiSelect1.Name = "ucPluginMultiSelect1";
			this.ucPluginMultiSelect1.Size = new System.Drawing.Size(321, 129);
			this.ucPluginMultiSelect1.TabIndex = 133;
			// 
			// InvoiceRemainsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 257);
			this.Controls.Add(this.ucPluginMultiSelect1);
			this.Controls.Add(this.filterLabel);
			this.Controls.Add(this.filterComboBox);
			this.Controls.Add(this.ucPeriod1);
			this.Controls.Add(this.periodLabel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "InvoiceRemainsParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.ucPeriod1, 0);
			this.Controls.SetChildIndex(this.filterComboBox, 0);
			this.Controls.SetChildIndex(this.filterLabel, 0);
			this.Controls.SetChildIndex(this.ucPluginMultiSelect1, 0);
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
		private ePlus.MetaData.Client.UCPeriod ucPeriod1;
		private System.Windows.Forms.ComboBox filterComboBox;
		private System.Windows.Forms.Label filterLabel;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMultiSelect1;
	}
}