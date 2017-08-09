namespace FCRStoreLifeControl
{
	partial class StoreLifeControlParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreLifeControlParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.daysTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(298, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(373, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 365);
            this.panel1.Size = new System.Drawing.Size(451, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(451, 25);
            this.toolStrip1.TabIndex = 10;
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
            // storesPluginMultiSelect
            // 
            this.storesPluginMultiSelect.AllowSaveState = true;
            this.storesPluginMultiSelect.Caption = null;
            this.storesPluginMultiSelect.Location = new System.Drawing.Point(12, 28);
            this.storesPluginMultiSelect.Mnemocode = "STORE";
            this.storesPluginMultiSelect.MultiSelect = true;
            this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
            this.storesPluginMultiSelect.Pinnable = false;
            this.storesPluginMultiSelect.Size = new System.Drawing.Size(414, 94);
            this.storesPluginMultiSelect.TabIndex = 105;
            this.storesPluginMultiSelect.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.storesPluginMultiSelect_BeforePluginShow);
            // 
            // daysTextBox
            // 
            this.daysTextBox.Location = new System.Drawing.Point(340, 330);
            this.daysTextBox.Name = "daysTextBox";
            this.daysTextBox.Size = new System.Drawing.Size(56, 20);
            this.daysTextBox.TabIndex = 106;
            this.daysTextBox.Text = "14";
            this.daysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "Количество дней на которое размещается товар на складе:";
            // 
            // ucContractors
            // 
            this.ucContractors.AllowSaveState = true;
            this.ucContractors.Caption = null;
            this.ucContractors.Location = new System.Drawing.Point(12, 128);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.MultiSelect = true;
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Pinnable = false;
            this.ucContractors.Size = new System.Drawing.Size(414, 94);
            this.ucContractors.TabIndex = 108;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = true;
            this.ucGoods.Caption = null;
            this.ucGoods.Location = new System.Drawing.Point(12, 228);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.MultiSelect = true;
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(414, 94);
            this.ucGoods.TabIndex = 109;
            // 
            // StoreLifeControlParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 394);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.ucContractors);
            this.Controls.Add(this.storesPluginMultiSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.daysTextBox);
            this.Name = "StoreLifeControlParams";
            this.Text = "Параметры отчета: Контроль превышения срока нахождения товара на складе";
            this.Load += new System.EventHandler(this.StoreLifeControlParams_Load);
            this.Controls.SetChildIndex(this.daysTextBox, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.TextBox daysTextBox;
		private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
	}
}