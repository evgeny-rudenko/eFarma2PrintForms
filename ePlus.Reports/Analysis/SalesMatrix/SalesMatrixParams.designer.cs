namespace RCSSalesMatrix
{
	partial class SalesMatrixParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesMatrixParams));
			this.label1 = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.codeCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(186, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(261, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 196);
			this.panel1.Size = new System.Drawing.Size(339, 29);
			this.panel1.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
			this.ucPeriod.DateTo = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
			this.ucPeriod.Location = new System.Drawing.Point(73, 36);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 2;
			// 
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(12, 63);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(318, 91);
			this.ucContractors.TabIndex = 6;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(339, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			// 
			// codeCheckBox
			// 
			this.codeCheckBox.AutoSize = true;
			this.codeCheckBox.Location = new System.Drawing.Point(19, 160);
			this.codeCheckBox.Name = "codeCheckBox";
			this.codeCheckBox.Size = new System.Drawing.Size(147, 17);
			this.codeCheckBox.TabIndex = 11;
			this.codeCheckBox.Text = "Отображать код товара";
			this.codeCheckBox.UseVisualStyleBackColor = true;
			// 
			// SalesMatrixParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 225);
			this.Controls.Add(this.codeCheckBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.ucContractors);
			this.Name = "SalesMatrixParams";
			this.Load += new System.EventHandler(this.SalesMatrixParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SalesMatrixParams_FormClosed);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.codeCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.CheckBox codeCheckBox;
    }
}