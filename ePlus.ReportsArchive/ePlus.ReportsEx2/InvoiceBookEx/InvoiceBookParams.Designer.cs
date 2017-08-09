using ePlus.MetaData.Client;
namespace InvoiceBookEx
{
	partial class InvoiceBookParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceBookParams));
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.chbDetail = new System.Windows.Forms.CheckBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.remainCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(321, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(396, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 233);
			this.panel1.Size = new System.Drawing.Size(474, 29);
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 1, 16, 53, 33, 812);
			this.ucPeriod.DateTo = new System.DateTime(2008, 10, 1, 16, 53, 33, 812);
			this.ucPeriod.Location = new System.Drawing.Point(63, 37);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Период";
			// 
			// chbDetail
			// 
			this.chbDetail.AutoSize = true;
			this.chbDetail.Checked = true;
			this.chbDetail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbDetail.Location = new System.Drawing.Point(15, 164);
			this.chbDetail.Name = "chbDetail";
			this.chbDetail.Size = new System.Drawing.Size(174, 17);
			this.chbDetail.TabIndex = 6;
			this.chbDetail.Text = "Детализация по документам";
			this.chbDetail.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(474, 25);
			this.toolStrip1.TabIndex = 8;
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
			// storesPluginMultiSelect
			// 
			this.storesPluginMultiSelect.AllowSaveState = false;
			this.storesPluginMultiSelect.Caption = "Склады";
			this.storesPluginMultiSelect.Location = new System.Drawing.Point(15, 64);
			this.storesPluginMultiSelect.Mnemocode = "STORE";
			this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
			this.storesPluginMultiSelect.Size = new System.Drawing.Size(448, 94);
			this.storesPluginMultiSelect.TabIndex = 13;
			// 
			// remainCheckBox
			// 
			this.remainCheckBox.AutoSize = true;
			this.remainCheckBox.Location = new System.Drawing.Point(15, 187);
			this.remainCheckBox.Name = "remainCheckBox";
			this.remainCheckBox.Size = new System.Drawing.Size(157, 17);
			this.remainCheckBox.TabIndex = 14;
			this.remainCheckBox.Text = "Учитывать ввод остатков";
			this.remainCheckBox.UseVisualStyleBackColor = true;
			// 
			// InvoiceBookParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 262);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.remainCheckBox);
			this.Controls.Add(this.storesPluginMultiSelect);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chbDetail);
			this.Controls.Add(this.ucPeriod);
			this.Name = "InvoiceBookParams";
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.chbDetail, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.remainCheckBox, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbDetail;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.CheckBox remainCheckBox;
		//private ePlus.Client.Core.SmartContractorStoreSelect m_oContractorStore;		 
	}
}