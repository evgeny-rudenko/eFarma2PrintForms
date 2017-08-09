namespace RCChLostProfitSale
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
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.periodLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.nUpDPersent = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.mpsGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersent)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(201, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(276, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 513);
            this.panel1.Size = new System.Drawing.Size(354, 29);
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(66, 38);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(257, 21);
            this.ucPeriod.TabIndex = 152;
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(12, 41);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(48, 13);
            this.periodLabel.TabIndex = 151;
            this.periodLabel.Text = "Период:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 488);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 154;
            this.label2.Text = "Наличие товара:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(354, 25);
            this.toolStrip1.TabIndex = 156;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            // 
            // nUpDPersent
            // 
            this.nUpDPersent.Location = new System.Drawing.Point(106, 486);
            this.nUpDPersent.Name = "nUpDPersent";
            this.nUpDPersent.Size = new System.Drawing.Size(58, 20);
            this.nUpDPersent.TabIndex = 187;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 488);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 188;
            this.label1.Text = "%";
            // 
            // mpsStore
            // 
            this.mpsStore.AllowSaveState = true;
            this.mpsStore.Caption = "Склады";
            this.mpsStore.Location = new System.Drawing.Point(15, 205);
            this.mpsStore.Mnemocode = "STORE";
            this.mpsStore.Name = "mpsStore";
            this.mpsStore.Size = new System.Drawing.Size(308, 137);
            this.mpsStore.TabIndex = 189;
            this.mpsStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsStore_BeforePluginShow);
            // 
            // mpsContractor
            // 
            this.mpsContractor.AllowSaveState = false;
            this.mpsContractor.Caption = "Аптеки";
            this.mpsContractor.Location = new System.Drawing.Point(15, 65);
            this.mpsContractor.Mnemocode = "CONTRACTOR";
            this.mpsContractor.Name = "mpsContractor";
            this.mpsContractor.Size = new System.Drawing.Size(308, 134);
            this.mpsContractor.TabIndex = 190;
            this.mpsContractor.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsContractor_BeforePluginShow);
            // 
            // mpsGoods
            // 
            this.mpsGoods.AllowSaveState = true;
            this.mpsGoods.Caption = "Товары";
            this.mpsGoods.Location = new System.Drawing.Point(15, 348);
            this.mpsGoods.Mnemocode = "GOODS2";
            this.mpsGoods.Name = "mpsGoods";
            this.mpsGoods.Size = new System.Drawing.Size(308, 133);
            this.mpsGoods.TabIndex = 191;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 542);
            this.Controls.Add(this.mpsGoods);
            this.Controls.Add(this.mpsContractor);
            this.Controls.Add(this.mpsStore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nUpDPersent);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.periodLabel);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.nUpDPersent, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.mpsStore, 0);
            this.Controls.SetChildIndex(this.mpsContractor, 0);
            this.Controls.SetChildIndex(this.mpsGoods, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.NumericUpDown nUpDPersent;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsGoods;
    }
}