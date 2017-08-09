namespace RCSChecksum
{
	partial class RCKChecksumParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RCKChecksumParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.periodLabel = new System.Windows.Forms.Label();
            this.periodPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxDelta = new System.Windows.Forms.TextBox();
            this.cardCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lmTextBox = new System.Windows.Forms.TextBox();
            this.cashPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(334, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(409, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 401);
            this.panel1.Size = new System.Drawing.Size(487, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(487, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "��������";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(12, 40);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(48, 13);
            this.periodLabel.TabIndex = 11;
            this.periodLabel.Text = "������:";
            // 
            // periodPeriod
            // 
            this.periodPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.periodPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.periodPeriod.Location = new System.Drawing.Point(74, 36);
            this.periodPeriod.Name = "periodPeriod";
            this.periodPeriod.Size = new System.Drawing.Size(256, 21);
            this.periodPeriod.TabIndex = 12;
            // 
            // storesPluginMultiSelect
            // 
            this.storesPluginMultiSelect.AllowSaveState = false;
            this.storesPluginMultiSelect.Caption = "������";
            this.storesPluginMultiSelect.Location = new System.Drawing.Point(13, 63);
            this.storesPluginMultiSelect.Mnemocode = "STORE";
            this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
            this.storesPluginMultiSelect.Pinnable = false;
            this.storesPluginMultiSelect.Size = new System.Drawing.Size(467, 118);
            this.storesPluginMultiSelect.TabIndex = 112;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "������:";
            // 
            // txtBxDelta
            // 
            this.txtBxDelta.Location = new System.Drawing.Point(64, 337);
            this.txtBxDelta.Name = "txtBxDelta";
            this.txtBxDelta.Size = new System.Drawing.Size(77, 20);
            this.txtBxDelta.TabIndex = 114;
            this.txtBxDelta.Text = "100";
            // 
            // cardCheckBox
            // 
            this.cardCheckBox.AutoSize = true;
            this.cardCheckBox.Location = new System.Drawing.Point(15, 373);
            this.cardCheckBox.Name = "cardCheckBox";
            this.cardCheckBox.Size = new System.Drawing.Size(263, 17);
            this.cardCheckBox.TabIndex = 115;
            this.cardCheckBox.Text = "�� ��������� ������� �� ���������� ������";
            this.cardCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 116;
            this.label2.Text = "������ ������� �����:";
            // 
            // lmTextBox
            // 
            this.lmTextBox.Location = new System.Drawing.Point(285, 337);
            this.lmTextBox.Name = "lmTextBox";
            this.lmTextBox.Size = new System.Drawing.Size(77, 20);
            this.lmTextBox.TabIndex = 117;
            this.lmTextBox.Text = "0,01";
            // 
            // cashPluginMultiSelect
            // 
            this.cashPluginMultiSelect.AllowSaveState = false;
            this.cashPluginMultiSelect.Caption = "�����";
            this.cashPluginMultiSelect.Location = new System.Drawing.Point(8, 187);
            this.cashPluginMultiSelect.Mnemocode = "CASH_REGISTER";
            this.cashPluginMultiSelect.Name = "cashPluginMultiSelect";
            this.cashPluginMultiSelect.Pinnable = false;
            this.cashPluginMultiSelect.Size = new System.Drawing.Size(467, 118);
            this.cashPluginMultiSelect.TabIndex = 118;
            // 
            // RCKChecksumParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 430);
            this.Controls.Add(this.cashPluginMultiSelect);
            this.Controls.Add(this.lmTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cardCheckBox);
            this.Controls.Add(this.txtBxDelta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storesPluginMultiSelect);
            this.Controls.Add(this.periodPeriod);
            this.Controls.Add(this.periodLabel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RCKChecksumParams";
            this.Load += new System.EventHandler(this.RCKChecksumParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RCKChecksumParams_FormClosed);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.periodPeriod, 0);
            this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtBxDelta, 0);
            this.Controls.SetChildIndex(this.cardCheckBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lmTextBox, 0);
            this.Controls.SetChildIndex(this.cashPluginMultiSelect, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label periodLabel;
		private ePlus.MetaData.Client.UCPeriod periodPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBxDelta;
		private System.Windows.Forms.CheckBox cardCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox lmTextBox;
        private ePlus.MetaData.Client.UCPluginMultiSelect cashPluginMultiSelect;
	}
}