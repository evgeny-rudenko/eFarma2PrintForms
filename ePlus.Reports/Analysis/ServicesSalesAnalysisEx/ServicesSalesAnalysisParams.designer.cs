namespace RCSServicesSalesAnalysis
{
	partial class ServicesSalesAnalysisParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesSalesAnalysisParams));
            this.periodLabel = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ucServices = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(281, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(356, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 227);
            this.panel1.Size = new System.Drawing.Size(434, 29);
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(10, 35);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(48, 13);
            this.periodLabel.TabIndex = 106;
            this.periodLabel.Text = "Период:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(64, 31);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(247, 21);
            this.ucPeriod.TabIndex = 168;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(434, 25);
            this.toolStrip1.TabIndex = 169;
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
            // ucServices
            // 
            this.ucServices.AllowSaveState = false;
            this.ucServices.Caption = "Услуги";
            this.ucServices.Location = new System.Drawing.Point(12, 58);
            this.ucServices.Mnemocode = "SERVICE";
            this.ucServices.Name = "ucServices";
            this.ucServices.Pinnable = false;
            this.ucServices.Size = new System.Drawing.Size(400, 141);
            this.ucServices.TabIndex = 171;
            // 
            // ServicesSalesAnalysisParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 256);
            this.Controls.Add(this.ucServices);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.periodLabel);
            this.Name = "ServicesSalesAnalysisParams";
            this.Load += new System.EventHandler(this.ServicesSalesAnalysisParams_Load);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucServices, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label periodLabel;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucServices;
	}
}