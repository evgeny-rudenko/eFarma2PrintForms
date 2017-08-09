namespace RCSReturnToContractor
{
    partial class ControlReturnToContractorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlReturnToContractorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
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
            this.panel1.Location = new System.Drawing.Point(0, 228);
            this.panel1.Size = new System.Drawing.Size(434, 29);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 168;
            this.label1.Text = "Период:";
            // 
            // mpsContractor
            // 
            this.mpsContractor.AllowSaveState = true;
            this.mpsContractor.Caption = "Контрагенты";
            this.mpsContractor.Location = new System.Drawing.Point(12, 62);
            this.mpsContractor.Mnemocode = "CONTRACTOR";
            this.mpsContractor.Name = "mpsContractor";
            this.mpsContractor.Pinnable = false;
            this.mpsContractor.Size = new System.Drawing.Size(408, 74);
            this.mpsContractor.TabIndex = 169;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
            this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.ucPeriod.Location = new System.Drawing.Point(66, 37);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 167;
            // 
            // mpsStore
            // 
            this.mpsStore.AllowSaveState = true;
            this.mpsStore.Caption = "Склады";
            this.mpsStore.Location = new System.Drawing.Point(12, 142);
            this.mpsStore.Mnemocode = "STORE";
            this.mpsStore.Name = "mpsStore";
            this.mpsStore.Pinnable = false;
            this.mpsStore.Size = new System.Drawing.Size(408, 74);
            this.mpsStore.TabIndex = 170;
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
            this.toolStrip1.TabIndex = 171;
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
            // ControlReturnToContractorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 257);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mpsContractor);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.mpsStore);
            this.Name = "ControlReturnToContractorForm";
            this.Text = "Параметры отчета \"Возвраты поставщикам\"";
            this.Load += new System.EventHandler(this.ControlReturnToContractorForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.mpsStore, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.mpsContractor, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;

    }
}