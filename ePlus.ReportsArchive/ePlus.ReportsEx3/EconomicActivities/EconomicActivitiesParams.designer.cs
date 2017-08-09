namespace EconomicActivities
{
	partial class EconomicActivitiesParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EconomicActivitiesParams));
			this.lbPeriod = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(446, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(521, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 112);
			this.panel1.Size = new System.Drawing.Size(599, 29);
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(12, 38);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(48, 13);
			this.lbPeriod.TabIndex = 6;
			this.lbPeriod.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(95, 36);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(253, 21);
			this.ucPeriod.TabIndex = 5;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(599, 25);
			this.toolStrip1.TabIndex = 10;
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
			// ucContractor
			// 
			this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.ucContractor.Location = new System.Drawing.Point(95, 63);
			this.ucContractor.Mnemocode = "CONTRACTOR";
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.Size = new System.Drawing.Size(492, 21);
			this.ucContractor.TabIndex = 113;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 114;
			this.label1.Text = "Контрагент:";
			// 
			// EconomicActivitiesParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(599, 141);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucContractor);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lbPeriod);
			this.Controls.Add(this.ucPeriod);
			this.Name = "EconomicActivitiesParams";
			this.Load += new System.EventHandler(this.EconomicActivitiesParams_Load);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.ucContractor, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbPeriod;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
		private System.Windows.Forms.Label label1;
	}
}
