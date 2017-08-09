namespace SaleRating
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.mpsGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbGoodsMoving = new System.Windows.Forms.CheckBox();
            this.cbInvoiceOut = new System.Windows.Forms.CheckBox();
            this.cbSaleKKM = new System.Windows.Forms.CheckBox();
            this.cbAllKind = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.nUpDPersentC = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nUpDPersentB = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nUpDPersentA = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTypeReport = new System.Windows.Forms.ComboBox();
            this.cbFilterAU = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentA)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(382, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(457, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Size = new System.Drawing.Size(535, 29);
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(535, 25);
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
            // mpsStore
            // 
            this.mpsStore.AllowSaveState = true;
            this.mpsStore.Caption = "Склады";
            this.mpsStore.Location = new System.Drawing.Point(15, 92);
            this.mpsStore.Mnemocode = "STORE";
            this.mpsStore.Name = "mpsStore";
            this.mpsStore.Size = new System.Drawing.Size(308, 137);
            this.mpsStore.TabIndex = 189;
            this.mpsStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsStore_BeforePluginShow);
            // 
            // mpsGoods
            // 
            this.mpsGoods.AllowSaveState = true;
            this.mpsGoods.Caption = "Товары";
            this.mpsGoods.Location = new System.Drawing.Point(15, 246);
            this.mpsGoods.Mnemocode = "GOODS2";
            this.mpsGoods.Name = "mpsGoods";
            this.mpsGoods.Size = new System.Drawing.Size(308, 133);
            this.mpsGoods.TabIndex = 191;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbGoodsMoving);
            this.panel2.Controls.Add(this.cbInvoiceOut);
            this.panel2.Controls.Add(this.cbSaleKKM);
            this.panel2.Controls.Add(this.cbAllKind);
            this.panel2.Location = new System.Drawing.Point(344, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 100);
            this.panel2.TabIndex = 192;
            // 
            // cbGoodsMoving
            // 
            this.cbGoodsMoving.AutoSize = true;
            this.cbGoodsMoving.Enabled = false;
            this.cbGoodsMoving.Location = new System.Drawing.Point(31, 72);
            this.cbGoodsMoving.Name = "cbGoodsMoving";
            this.cbGoodsMoving.Size = new System.Drawing.Size(137, 17);
            this.cbGoodsMoving.TabIndex = 18;
            this.cbGoodsMoving.Text = "Перемещение товара";
            this.cbGoodsMoving.UseVisualStyleBackColor = true;
            // 
            // cbInvoiceOut
            // 
            this.cbInvoiceOut.AutoSize = true;
            this.cbInvoiceOut.Enabled = false;
            this.cbInvoiceOut.Location = new System.Drawing.Point(31, 49);
            this.cbInvoiceOut.Name = "cbInvoiceOut";
            this.cbInvoiceOut.Size = new System.Drawing.Size(141, 17);
            this.cbInvoiceOut.TabIndex = 17;
            this.cbInvoiceOut.Text = "Расходные накладные";
            this.cbInvoiceOut.UseVisualStyleBackColor = true;
            // 
            // cbSaleKKM
            // 
            this.cbSaleKKM.AutoSize = true;
            this.cbSaleKKM.Enabled = false;
            this.cbSaleKKM.Location = new System.Drawing.Point(31, 26);
            this.cbSaleKKM.Name = "cbSaleKKM";
            this.cbSaleKKM.Size = new System.Drawing.Size(113, 17);
            this.cbSaleKKM.TabIndex = 16;
            this.cbSaleKKM.Text = "Продажа по ККМ";
            this.cbSaleKKM.UseVisualStyleBackColor = true;
            // 
            // cbAllKind
            // 
            this.cbAllKind.AutoSize = true;
            this.cbAllKind.Checked = true;
            this.cbAllKind.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllKind.Location = new System.Drawing.Point(5, 3);
            this.cbAllKind.Name = "cbAllKind";
            this.cbAllKind.Size = new System.Drawing.Size(118, 17);
            this.cbAllKind.TabIndex = 15;
            this.cbAllKind.Text = "Все виды расхода";
            this.cbAllKind.UseVisualStyleBackColor = true;
            this.cbAllKind.CheckedChanged += new System.EventHandler(this.cbAllKind_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.nUpDPersentC);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.nUpDPersentB);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.nUpDPersentA);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(349, 171);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(152, 100);
            this.panel3.TabIndex = 193;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 197;
            this.label7.Text = "%";
            // 
            // nUpDPersentC
            // 
            this.nUpDPersentC.Enabled = false;
            this.nUpDPersentC.Location = new System.Drawing.Point(62, 66);
            this.nUpDPersentC.Name = "nUpDPersentC";
            this.nUpDPersentC.Size = new System.Drawing.Size(58, 20);
            this.nUpDPersentC.TabIndex = 196;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 195;
            this.label8.Text = "Группа C:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 194;
            this.label5.Text = "%";
            // 
            // nUpDPersentB
            // 
            this.nUpDPersentB.Location = new System.Drawing.Point(62, 36);
            this.nUpDPersentB.Name = "nUpDPersentB";
            this.nUpDPersentB.Size = new System.Drawing.Size(58, 20);
            this.nUpDPersentB.TabIndex = 193;
            this.nUpDPersentB.ValueChanged += new System.EventHandler(this.nUpDPersentB_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 192;
            this.label6.Text = "Группа B:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 191;
            this.label3.Text = "%";
            // 
            // nUpDPersentA
            // 
            this.nUpDPersentA.Location = new System.Drawing.Point(62, 5);
            this.nUpDPersentA.Name = "nUpDPersentA";
            this.nUpDPersentA.Size = new System.Drawing.Size(58, 20);
            this.nUpDPersentA.TabIndex = 190;
            this.nUpDPersentA.ValueChanged += new System.EventHandler(this.nUpDPersentA_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 189;
            this.label4.Text = "Группа А:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 194;
            this.label1.Text = "Вид отчета:";
            // 
            // cbTypeReport
            // 
            this.cbTypeReport.FormattingEnabled = true;
            this.cbTypeReport.Items.AddRange(new object[] {
            "Рейтинг продаж по прибыльности.",
            "Рейтинг по количеству продаж.",
            "Рейтинг по сумме реализации."});
            this.cbTypeReport.Location = new System.Drawing.Point(80, 65);
            this.cbTypeReport.Name = "cbTypeReport";
            this.cbTypeReport.Size = new System.Drawing.Size(243, 21);
            this.cbTypeReport.TabIndex = 195;
            // 
            // cbFilterAU
            // 
            this.cbFilterAU.AutoSize = true;
            this.cbFilterAU.Location = new System.Drawing.Point(15, 385);
            this.cbFilterAU.Name = "cbFilterAU";
            this.cbFilterAU.Size = new System.Drawing.Size(234, 17);
            this.cbFilterAU.TabIndex = 196;
            this.cbFilterAU.Text = "Отфильтровать перемещения внутри АУ";
            this.cbFilterAU.UseVisualStyleBackColor = true;
            this.cbFilterAU.Visible = false;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 440);
            this.Controls.Add(this.cbFilterAU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTypeReport);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.mpsGoods);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.mpsStore);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.periodLabel);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.mpsStore, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.mpsGoods, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.cbTypeReport, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbFilterAU, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDPersentA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Label periodLabel;
		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsGoods;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbGoodsMoving;
        private System.Windows.Forms.CheckBox cbInvoiceOut;
        private System.Windows.Forms.CheckBox cbSaleKKM;
        private System.Windows.Forms.CheckBox cbAllKind;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nUpDPersentC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nUpDPersentB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nUpDPersentA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTypeReport;
        private System.Windows.Forms.CheckBox cbFilterAU;
    }
}