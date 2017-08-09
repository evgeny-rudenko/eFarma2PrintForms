using ePlus.MetaData.Client;

namespace AP10
{
	partial class AP10Params
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AP10Params));
			this.multiGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.multiStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.lbPeriod = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.checkPreview = new System.Windows.Forms.CheckBox();
			this.shortCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(232, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(307, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 278);
			this.panel1.Size = new System.Drawing.Size(385, 29);
			// 
			// multiGoods
			// 
			this.multiGoods.AllowSaveState = false;
			this.multiGoods.Caption = "";
			this.multiGoods.Location = new System.Drawing.Point(14, 150);
			this.multiGoods.Mnemocode = "GOODS2";
			this.multiGoods.Name = "multiGoods";
			this.multiGoods.Size = new System.Drawing.Size(355, 97);
			this.multiGoods.TabIndex = 0;
			// 
			// multiStore
			// 
			this.multiStore.AllowSaveState = false;
			this.multiStore.Caption = "";
			this.multiStore.Location = new System.Drawing.Point(14, 57);
			this.multiStore.Mnemocode = "STORE";
			this.multiStore.Name = "multiStore";
			this.multiStore.Size = new System.Drawing.Size(355, 87);
			this.multiStore.TabIndex = 3;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
			this.ucPeriod.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
			this.ucPeriod.Location = new System.Drawing.Point(65, 30);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 7;
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(11, 34);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(48, 13);
			this.lbPeriod.TabIndex = 9;
			this.lbPeriod.Text = "Период:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(385, 25);
			this.toolStrip1.TabIndex = 154;
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
			// checkPreview
			// 
			this.checkPreview.AutoSize = true;
			this.checkPreview.Checked = true;
			this.checkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPreview.Location = new System.Drawing.Point(4444, 350);
			this.checkPreview.Name = "checkPreview";
			this.checkPreview.Size = new System.Drawing.Size(171, 17);
			this.checkPreview.TabIndex = 101;
			this.checkPreview.Text = "Предварительный просмотр";
			this.checkPreview.UseVisualStyleBackColor = true;
			// 
			// shortCheckBox
			// 
			this.shortCheckBox.AutoSize = true;
			this.shortCheckBox.Checked = true;
			this.shortCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.shortCheckBox.Location = new System.Drawing.Point(14, 253);
			this.shortCheckBox.Name = "shortCheckBox";
			this.shortCheckBox.Size = new System.Drawing.Size(68, 17);
			this.shortCheckBox.TabIndex = 155;
			this.shortCheckBox.Text = "Краткий";
			this.shortCheckBox.UseVisualStyleBackColor = true;
			// 
			// AP10Params
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 307);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.shortCheckBox);
			this.Controls.Add(this.lbPeriod);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.multiGoods);
			this.Controls.Add(this.multiStore);
			this.Name = "AP10Params";
			this.Controls.SetChildIndex(this.multiStore, 0);
			this.Controls.SetChildIndex(this.multiGoods, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.shortCheckBox, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UCPluginMultiSelect multiGoods;
		private UCPluginMultiSelect multiStore;
		private UCPeriod ucPeriod;
		private System.Windows.Forms.Label lbPeriod;
		private System.Windows.Forms.CheckBox checkPreview;
		
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.CheckBox shortCheckBox;
		
		
	}
}