namespace RCSMarginControl_Rigla
{
	partial class MarginControlParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarginControlParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
            this.importantCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.marginTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(461, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(536, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 367);
            this.panel1.Size = new System.Drawing.Size(614, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(614, 25);
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
            // ucContractors
            // 
            this.ucContractors.AllowSaveState = true;
            this.ucContractors.Caption = "Аптеки";
            this.ucContractors.Location = new System.Drawing.Point(12, 37);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Pinnable = false;
            this.ucContractors.Size = new System.Drawing.Size(337, 118);
            this.ucContractors.TabIndex = 104;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(12, 161);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(595, 119);
            this.ucGoods.TabIndex = 144;
            // 
            // ucGoodsGroup
            // 
            this.ucGoodsGroup.Location = new System.Drawing.Point(362, 37);
            this.ucGoodsGroup.Name = "ucGoodsGroup";
            this.ucGoodsGroup.Size = new System.Drawing.Size(245, 124);
            this.ucGoodsGroup.TabIndex = 145;
            // 
            // importantCheckBox
            // 
            this.importantCheckBox.AutoSize = true;
            this.importantCheckBox.Location = new System.Drawing.Point(15, 332);
            this.importantCheckBox.Name = "importantCheckBox";
            this.importantCheckBox.Size = new System.Drawing.Size(225, 17);
            this.importantCheckBox.TabIndex = 147;
            this.importantCheckBox.Text = "Проверять товар с признаком ЖНВЛС";
            this.importantCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 148;
            this.label1.Text = "Максимальная наценка:";
            // 
            // marginTextBox
            // 
            this.marginTextBox.Location = new System.Drawing.Point(160, 295);
            this.marginTextBox.MaxLength = 3;
            this.marginTextBox.Name = "marginTextBox";
            this.marginTextBox.Size = new System.Drawing.Size(83, 20);
            this.marginTextBox.TabIndex = 149;
            this.marginTextBox.Text = "40";
            this.marginTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MarginControlParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 396);
            this.Controls.Add(this.marginTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.importantCheckBox);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.ucGoodsGroup);
            this.Controls.Add(this.ucContractors);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MarginControlParams";
            this.Load += new System.EventHandler(this.MarginControlParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.importantCheckBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.marginTextBox, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private System.Windows.Forms.CheckBox importantCheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox marginTextBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private ePlus.CommonEx.Controls.SelectGoodsGroup ucGoodsGroup;
	}
}