namespace RCChBonusRemAdd
{
	partial class DefecturaParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefecturaParams));
            this.ucDiscount2_Card = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.label1 = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.cbCardWithMove = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(177, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(252, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Size = new System.Drawing.Size(330, 29);
            // 
            // ucDiscount2_Card
            // 
            this.ucDiscount2_Card.AllowSaveState = true;
            this.ucDiscount2_Card.Caption = "Дисконтные карты";
            this.ucDiscount2_Card.Location = new System.Drawing.Point(12, 226);
            this.ucDiscount2_Card.Mnemocode = "DISCOUNT2_CARD";
            this.ucDiscount2_Card.Name = "ucDiscount2_Card";
            this.ucDiscount2_Card.Size = new System.Drawing.Size(304, 144);
            this.ucDiscount2_Card.TabIndex = 15;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(330, 25);
            this.toolStrip1.TabIndex = 21;
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
            this.ucContractors.Caption = "Контрагенты";
            this.ucContractors.Location = new System.Drawing.Point(12, 66);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Size = new System.Drawing.Size(304, 154);
            this.ucContractors.TabIndex = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "Период:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
            this.ucPeriod.DateTo = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
            this.ucPeriod.Location = new System.Drawing.Point(86, 39);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(230, 21);
            this.ucPeriod.TabIndex = 126;
            // 
            // cbCardWithMove
            // 
            this.cbCardWithMove.AutoSize = true;
            this.cbCardWithMove.Checked = true;
            this.cbCardWithMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCardWithMove.Location = new System.Drawing.Point(15, 376);
            this.cbCardWithMove.Name = "cbCardWithMove";
            this.cbCardWithMove.Size = new System.Drawing.Size(218, 17);
            this.cbCardWithMove.TabIndex = 128;
            this.cbCardWithMove.Text = "Выводить только карты с движением";
            this.cbCardWithMove.UseVisualStyleBackColor = true;
            // 
            // DefecturaParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 429);
            this.Controls.Add(this.cbCardWithMove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucContractors);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ucDiscount2_Card);
            this.Name = "DefecturaParams";
            this.Load += new System.EventHandler(this.DefecturaParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefecturaParams_FormClosed);
            this.Controls.SetChildIndex(this.ucDiscount2_Card, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbCardWithMove, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucDiscount2_Card;
		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.CheckBox cbCardWithMove;
	}
}