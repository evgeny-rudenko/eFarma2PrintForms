namespace RCKChecksum
{
	partial class RCKChecksumParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RCKChecksumParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.periodLabel = new System.Windows.Forms.Label();
			this.periodPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.label1 = new System.Windows.Forms.Label();
			this.txtBxDelta = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(357, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(432, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 210);
			this.panel1.Size = new System.Drawing.Size(510, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(510, 25);
			this.toolStrip1.TabIndex = 9;
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
			this.periodLabel.Location = new System.Drawing.Point(10, 36);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(48, 13);
			this.periodLabel.TabIndex = 11;
			this.periodLabel.Text = "Период:";
			// 
			// periodPeriod
			// 
			this.periodPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.periodPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.periodPeriod.Location = new System.Drawing.Point(74, 36);
			this.periodPeriod.Name = "periodPeriod";
			this.periodPeriod.Size = new System.Drawing.Size(256, 21);
			this.periodPeriod.TabIndex = 12;
			// 
			// storesPluginMultiSelect
			// 
			this.storesPluginMultiSelect.AllowSaveState = false;
			this.storesPluginMultiSelect.Caption = "Склады";
			this.storesPluginMultiSelect.Location = new System.Drawing.Point(13, 63);
			this.storesPluginMultiSelect.Mnemocode = "STORE";
			this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
			this.storesPluginMultiSelect.Size = new System.Drawing.Size(467, 73);
			this.storesPluginMultiSelect.TabIndex = 112;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 113;
			this.label1.Text = "Дельта:";
			// 
			// txtBxDelta
			// 
			this.txtBxDelta.Location = new System.Drawing.Point(64, 149);
			this.txtBxDelta.Name = "txtBxDelta";
			this.txtBxDelta.Size = new System.Drawing.Size(77, 20);
			this.txtBxDelta.TabIndex = 114;
			this.txtBxDelta.Text = "100";
			// 
			// RCKChecksumParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(510, 239);
			this.Controls.Add(this.txtBxDelta);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.storesPluginMultiSelect);
			this.Controls.Add(this.periodPeriod);
			this.Controls.Add(this.periodLabel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "RCKChecksumParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.periodPeriod, 0);
			this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.txtBxDelta, 0);
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBxDelta;
	}
}