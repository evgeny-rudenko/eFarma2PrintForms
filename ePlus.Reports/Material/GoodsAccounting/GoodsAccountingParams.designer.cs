namespace RCSGoodsAccounting
{
	partial class GoodsAccountingParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsAccountingParams));
			this.ucGoods = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.storeLabel = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ucStore = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(253, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(328, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 130);
			this.panel1.Size = new System.Drawing.Size(406, 29);
			// 
			// ucGoods
			// 
			this.ucGoods.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.ucGoods.Location = new System.Drawing.Point(66, 89);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(321, 20);
			this.ucGoods.TabIndex = 16;
			// 
			// storeLabel
			// 
			this.storeLabel.AutoSize = true;
			this.storeLabel.Location = new System.Drawing.Point(12, 93);
			this.storeLabel.Name = "storeLabel";
			this.storeLabel.Size = new System.Drawing.Size(41, 13);
			this.storeLabel.TabIndex = 17;
			this.storeLabel.Text = "Товар:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(406, 25);
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
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(66, 36);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(229, 21);
			this.ucPeriod.TabIndex = 122;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 121;
			this.label1.Text = "Период:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 124;
			this.label2.Text = "Склад:";
			// 
			// ucStore
			// 
			this.ucStore.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.ucStore.Location = new System.Drawing.Point(66, 63);
			this.ucStore.Mnemocode = "STORE";
			this.ucStore.Name = "ucStore";
			this.ucStore.Size = new System.Drawing.Size(321, 20);
			this.ucStore.TabIndex = 123;
			// 
			// GoodsAccountingParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 159);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ucStore);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.storeLabel);
			this.Controls.Add(this.ucGoods);
			this.Name = "GoodsAccountingParams";
			this.Load += new System.EventHandler(this.GoodsAccountingParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoodsAccountingParams_FormClosed);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.storeLabel, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.ucStore, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ePlus.MetaData.Client.UCMetaPluginSelect ucGoods;
		private System.Windows.Forms.Label storeLabel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private ePlus.MetaData.Client.UCMetaPluginSelect ucStore;
	}
}