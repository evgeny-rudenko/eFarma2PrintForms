namespace BookSalesEx
{
	partial class BookSalesRegParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookSalesRegParams));
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.lbPeriod = new System.Windows.Forms.Label();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.discountCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(214, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(289, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 252);
			this.panel1.Size = new System.Drawing.Size(367, 29);
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(63, 38);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(253, 21);
			this.ucPeriod.TabIndex = 6;
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(12, 38);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(45, 13);
			this.lbPeriod.TabIndex = 7;
			this.lbPeriod.Text = "Период";
			// 
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = false;
			this.ucContractors.Caption = "Продавцы";
			this.ucContractors.Location = new System.Drawing.Point(15, 65);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(333, 123);
			this.ucContractors.TabIndex = 10;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(367, 25);
			this.toolStrip1.TabIndex = 11;
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
			// discountCheckBox
			// 
			this.discountCheckBox.AutoSize = true;
			this.discountCheckBox.Location = new System.Drawing.Point(15, 207);
			this.discountCheckBox.Name = "discountCheckBox";
			this.discountCheckBox.Size = new System.Drawing.Size(120, 17);
			this.discountCheckBox.TabIndex = 12;
			this.discountCheckBox.Text = "Учитывать скидки";
			this.discountCheckBox.UseVisualStyleBackColor = true;
			// 
			// BookSalesRegParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(367, 281);
			this.Controls.Add(this.discountCheckBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.ucContractors);
			this.Controls.Add(this.lbPeriod);
			this.Name = "BookSalesRegParams";
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.discountCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label lbPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.CheckBox discountCheckBox;
	}
}