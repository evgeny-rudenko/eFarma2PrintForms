namespace RejectedGoods
{
	partial class RejectedGoodsParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RejectedGoodsParams));
			this.nameLabel = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.seriesTextBox = new System.Windows.Forms.TextBox();
			this.seriesLabel = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(242, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(317, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 303);
			this.panel1.Size = new System.Drawing.Size(395, 29);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(17, 224);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(199, 13);
			this.nameLabel.TabIndex = 11;
			this.nameLabel.Text = "Количество символов наименования:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(395, 25);
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
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(237, 221);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(64, 20);
			this.nameTextBox.TabIndex = 24;
			this.nameTextBox.Text = "0";
			this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(15, 39);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(347, 85);
			this.ucContractors.TabIndex = 125;
			// 
			// ucStores
			// 
			this.ucStores.AllowSaveState = true;
			this.ucStores.Caption = "Склады";
			this.ucStores.Location = new System.Drawing.Point(15, 130);
			this.ucStores.Mnemocode = "STORE";
			this.ucStores.Name = "ucStores";
			this.ucStores.Size = new System.Drawing.Size(347, 85);
			this.ucStores.TabIndex = 126;
			// 
			// seriesTextBox
			// 
			this.seriesTextBox.Location = new System.Drawing.Point(237, 247);
			this.seriesTextBox.Name = "seriesTextBox";
			this.seriesTextBox.Size = new System.Drawing.Size(64, 20);
			this.seriesTextBox.TabIndex = 128;
			this.seriesTextBox.Text = "0";
			this.seriesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// seriesLabel
			// 
			this.seriesLabel.AutoSize = true;
			this.seriesLabel.Location = new System.Drawing.Point(17, 250);
			this.seriesLabel.Name = "seriesLabel";
			this.seriesLabel.Size = new System.Drawing.Size(155, 13);
			this.seriesLabel.TabIndex = 127;
			this.seriesLabel.Text = "Количество символов серии:";
			// 
			// RejectedGoodsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 332);
			this.Controls.Add(this.seriesTextBox);
			this.Controls.Add(this.seriesLabel);
			this.Controls.Add(this.ucStores);
			this.Controls.Add(this.ucContractors);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.nameLabel);
			this.Name = "RejectedGoodsParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.nameLabel, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.nameTextBox, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucStores, 0);
			this.Controls.SetChildIndex(this.seriesLabel, 0);
			this.Controls.SetChildIndex(this.seriesTextBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TextBox nameTextBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.TextBox seriesTextBox;
		private System.Windows.Forms.Label seriesLabel;
	}
}