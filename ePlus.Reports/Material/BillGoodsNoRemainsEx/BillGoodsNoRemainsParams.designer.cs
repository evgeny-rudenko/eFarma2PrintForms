namespace BillGoodsNoRemainsEx
{
    partial class BillGoodsNoRemainsParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillGoodsNoRemainsParams));
			this.ucBills = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucSellers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucBuyers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.reportFromGroupBox = new System.Windows.Forms.GroupBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.reportFromGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(533, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(608, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 395);
			this.panel1.Size = new System.Drawing.Size(686, 29);
			// 
			// ucBills
			// 
			this.ucBills.AllowSaveState = false;
			this.ucBills.Caption = "Счета:";
			this.ucBills.Location = new System.Drawing.Point(12, 175);
			this.ucBills.Mnemocode = "BILL";
			this.ucBills.Name = "ucBills";
			this.ucBills.Size = new System.Drawing.Size(664, 98);
			this.ucBills.TabIndex = 123;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.period.Location = new System.Drawing.Point(64, 37);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(229, 21);
			this.period.TabIndex = 120;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 119;
			this.label1.Text = "Период:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(686, 25);
			this.toolStrip1.TabIndex = 125;
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
			// ucSellers
			// 
			this.ucSellers.AllowSaveState = false;
			this.ucSellers.Caption = "Продавцы:";
			this.ucSellers.Location = new System.Drawing.Point(12, 71);
			this.ucSellers.Mnemocode = "CONTRACTOR";
			this.ucSellers.Name = "ucSellers";
			this.ucSellers.Size = new System.Drawing.Size(329, 98);
			this.ucSellers.TabIndex = 128;
			// 
			// ucBuyers
			// 
			this.ucBuyers.AllowSaveState = false;
			this.ucBuyers.Caption = "Покупатели:";
			this.ucBuyers.Location = new System.Drawing.Point(347, 71);
			this.ucBuyers.Mnemocode = "CONTRACTOR";
			this.ucBuyers.Name = "ucBuyers";
			this.ucBuyers.Size = new System.Drawing.Size(329, 98);
			this.ucBuyers.TabIndex = 129;
			// 
			// reportFromGroupBox
			// 
			this.reportFromGroupBox.Controls.Add(this.radioButton3);
			this.reportFromGroupBox.Controls.Add(this.radioButton2);
			this.reportFromGroupBox.Controls.Add(this.radioButton1);
			this.reportFromGroupBox.Location = new System.Drawing.Point(13, 279);
			this.reportFromGroupBox.Name = "reportFromGroupBox";
			this.reportFromGroupBox.Size = new System.Drawing.Size(663, 99);
			this.reportFromGroupBox.TabIndex = 130;
			this.reportFromGroupBox.TabStop = false;
			this.reportFromGroupBox.Text = "Форма отчёта";
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(18, 65);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(272, 17);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Дефицитная ведомость (счёта) по поставщикам";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(18, 42);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(227, 17);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Сводная дефицитная ведомость (счёта)";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(18, 19);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(184, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Дефицитная ведомость (счёта)";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// BillGoodsNoRemainsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 424);
			this.Controls.Add(this.reportFromGroupBox);
			this.Controls.Add(this.ucBuyers);
			this.Controls.Add(this.ucSellers);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucBills);
			this.Controls.Add(this.period);
			this.Controls.Add(this.label1);
			this.Name = "BillGoodsNoRemainsParams";
			this.Load += new System.EventHandler(this.BillGoodsNoRemainsParams_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.ucBills, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.ucSellers, 0);
			this.Controls.SetChildIndex(this.ucBuyers, 0);
			this.Controls.SetChildIndex(this.reportFromGroupBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.reportFromGroupBox.ResumeLayout(false);
			this.reportFromGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private ePlus.MetaData.Client.UCPluginMultiSelect ucBills;
        private ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucSellers;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucBuyers;
		private System.Windows.Forms.GroupBox reportFromGroupBox;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
    }
}