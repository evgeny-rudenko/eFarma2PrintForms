using ePlus.CommonEx.Controls;
namespace FactorReestr
{
    partial class FormParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParams));
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.lbPeriod = new System.Windows.Forms.Label();
			this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.znvlsCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(198, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(273, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 293);
			this.panel1.Size = new System.Drawing.Size(351, 29);
			this.panel1.TabIndex = 6;
			// 
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(13, 59);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(311, 94);
			this.ucContractors.TabIndex = 178;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(351, 25);
			this.toolStrip1.TabIndex = 180;
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
			this.ucPeriod.Location = new System.Drawing.Point(66, 32);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(232, 21);
			this.ucPeriod.TabIndex = 183;
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(12, 36);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(48, 13);
			this.lbPeriod.TabIndex = 184;
			this.lbPeriod.Text = "Период:";
			// 
			// ucStores
			// 
			this.ucStores.AllowSaveState = true;
			this.ucStores.Caption = "Склады";
			this.ucStores.Location = new System.Drawing.Point(15, 159);
			this.ucStores.Mnemocode = "STORE";
			this.ucStores.Name = "ucStores";
			this.ucStores.Size = new System.Drawing.Size(311, 94);
			this.ucStores.TabIndex = 185;
			// 
			// znvlsCheckBox
			// 
			this.znvlsCheckBox.AutoSize = true;
			this.znvlsCheckBox.Location = new System.Drawing.Point(15, 259);
			this.znvlsCheckBox.Name = "znvlsCheckBox";
			this.znvlsCheckBox.Size = new System.Drawing.Size(107, 17);
			this.znvlsCheckBox.TabIndex = 186;
			this.znvlsCheckBox.Text = "Только ЖНВЛС";
			this.znvlsCheckBox.UseVisualStyleBackColor = true;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(351, 322);
			this.Controls.Add(this.znvlsCheckBox);
			this.Controls.Add(this.ucStores);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.lbPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucContractors);
			this.Name = "FormParams";
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.ucStores, 0);
			this.Controls.SetChildIndex(this.znvlsCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label lbPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.CheckBox znvlsCheckBox;
    }
}