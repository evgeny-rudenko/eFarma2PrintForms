namespace RCChSaleGoodsByGroup
{
	partial class SaleGoodsByGroupParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleGoodsByGroupParams));
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoodsGroup = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(203, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(278, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Size = new System.Drawing.Size(356, 29);
            // 
            // ucPeriod
            // 
            this.ucPeriod.AutoSize = true;
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(63, 34);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(224, 23);
            this.ucPeriod.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период";
            // 
            // ucContractor
            // 
            this.ucContractor.AllowSaveState = true;
            this.ucContractor.Caption = "Аптеки";
            this.ucContractor.Location = new System.Drawing.Point(15, 76);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Size = new System.Drawing.Size(324, 100);
            this.ucContractor.TabIndex = 2;
            // 
            // ucGoodsGroup
            // 
            this.ucGoodsGroup.AllowSaveState = true;
            this.ucGoodsGroup.Caption = "Группы товара";
            this.ucGoodsGroup.Location = new System.Drawing.Point(15, 182);
            this.ucGoodsGroup.Mnemocode = "GOODS_GROUP";
            this.ucGoodsGroup.Name = "ucGoodsGroup";
            this.ucGoodsGroup.Size = new System.Drawing.Size(324, 100);
            this.ucGoodsGroup.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(356, 25);
            this.toolStrip1.TabIndex = 22;
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
            // AccompanyingGoodsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 326);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ucGoodsGroup);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod);
            this.Name = "AccompanyingGoodsParams";
            this.Load += new System.EventHandler(this.AccompanyingGoodsParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccompanyingGoodsParams_FormClosed);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		public ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
		public ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroup;
		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}