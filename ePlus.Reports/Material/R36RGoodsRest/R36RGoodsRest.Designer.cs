namespace R36RGoodsRest
{
	partial class R36RGoodsRest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R36RGoodsRest));
            this.label1 = new System.Windows.Forms.Label();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.dateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cbGroups = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(202, 3);
            this.bOK.Size = new System.Drawing.Size(75, 27);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(277, 3);
            this.bClose.Size = new System.Drawing.Size(75, 27);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 345);
            this.panel1.Size = new System.Drawing.Size(355, 33);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Остатки на дату:";
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "";
            this.ucGoods.Location = new System.Drawing.Point(19, 72);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Size = new System.Drawing.Size(328, 106);
            this.ucGoods.TabIndex = 14;
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "";
            this.ucStore.Location = new System.Drawing.Point(19, 184);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Size = new System.Drawing.Size(328, 114);
            this.ucStore.TabIndex = 15;
            // 
            // dateDateTimePicker
            // 
            this.dateDateTimePicker.Location = new System.Drawing.Point(114, 36);
            this.dateDateTimePicker.Name = "dateDateTimePicker";
            this.dateDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.dateDateTimePicker.TabIndex = 17;
            // 
            // cbGroups
            // 
            this.cbGroups.AutoSize = true;
            this.cbGroups.Location = new System.Drawing.Point(19, 304);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(191, 17);
            this.cbGroups.TabIndex = 20;
            this.cbGroups.Text = "Сворачивать группы по товарам";
            this.cbGroups.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(355, 25);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // R36RGoodsRest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 378);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cbGroups);
            this.Controls.Add(this.dateDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.ucStore);
            this.Name = "R36RGoodsRest";
            this.Load += new System.EventHandler(this.PriceListParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PriceListParams_FormClosed);
            this.Controls.SetChildIndex(this.ucStore, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dateDateTimePicker, 0);
            this.Controls.SetChildIndex(this.cbGroups, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private System.Windows.Forms.DateTimePicker dateDateTimePicker;
        private System.Windows.Forms.CheckBox cbGroups;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
	}
}