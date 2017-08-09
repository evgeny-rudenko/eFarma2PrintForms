namespace AdressListByContractor
{
    partial class AdressListByContractorParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdressListByContractorParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ucMetaPluginSelect1 = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(279, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(354, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 156);
            this.panel1.Size = new System.Drawing.Size(432, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(432, 25);
            this.toolStrip1.TabIndex = 177;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "Очистить";
            // 
            // ucMetaPluginSelect1
            // 
            this.ucMetaPluginSelect1.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucMetaPluginSelect1.Location = new System.Drawing.Point(32, 80);
            this.ucMetaPluginSelect1.Mnemocode = "CONTRACTOR";
            this.ucMetaPluginSelect1.Name = "ucMetaPluginSelect1";
            this.ucMetaPluginSelect1.Size = new System.Drawing.Size(315, 21);
            this.ucMetaPluginSelect1.TabIndex = 187;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 188;
            this.label1.Text = "Контрагент";
            // 
            // AdressListByContractorParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 185);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucMetaPluginSelect1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AdressListByContractorParams";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucMetaPluginSelect1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPluginSelect1;
        private System.Windows.Forms.Label label1;
    }
}