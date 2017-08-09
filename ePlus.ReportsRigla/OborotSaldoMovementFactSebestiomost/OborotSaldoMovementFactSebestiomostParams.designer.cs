namespace OborotSaldoMovementFactSebestiomost
{
	partial class OborotSaldoMovementFactSebestiomostParam
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OborotSaldoMovementFactSebestiomostParam));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.auCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(335, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(410, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 321);
			this.panel1.Size = new System.Drawing.Size(488, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(488, 25);
			this.toolStrip1.TabIndex = 3;
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
			this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 3, 10, 3, 23, 203);
			this.ucPeriod.DateTo = new System.DateTime(2008, 10, 3, 10, 3, 23, 203);
			this.ucPeriod.Location = new System.Drawing.Point(82, 38);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Период";
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "";
			this.ucGoods.Location = new System.Drawing.Point(12, 65);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(458, 108);
			this.ucGoods.TabIndex = 8;
			// 
			// ucStore
			// 
			this.ucStore.AllowSaveState = false;
			this.ucStore.Caption = "";
			this.ucStore.Location = new System.Drawing.Point(12, 179);
			this.ucStore.Mnemocode = "STORE";
			this.ucStore.Name = "ucStore";
			this.ucStore.Size = new System.Drawing.Size(458, 108);
			this.ucStore.TabIndex = 9;
			// 
			// auCheckBox
			// 
			this.auCheckBox.AutoSize = true;
			this.auCheckBox.Checked = true;
			this.auCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.auCheckBox.Location = new System.Drawing.Point(12, 293);
			this.auCheckBox.Name = "auCheckBox";
			this.auCheckBox.Size = new System.Drawing.Size(254, 17);
			this.auCheckBox.TabIndex = 181;
			this.auCheckBox.Text = "Отфильтровать перемещения внутри аптеки";
			this.auCheckBox.UseVisualStyleBackColor = true;
			// 
			// OborotSaldoMovementFactSebestiomostParam
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 350);
			this.Controls.Add(this.auCheckBox);
			this.Controls.Add(this.ucStore);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Name = "OborotSaldoMovementFactSebestiomostParam";
			this.Load += new System.EventHandler(this.OborotSaldoMovementFactSebestiomostParam_Load);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.ucStore, 0);
			this.Controls.SetChildIndex(this.auCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
		private System.Windows.Forms.CheckBox auCheckBox;
	}
}