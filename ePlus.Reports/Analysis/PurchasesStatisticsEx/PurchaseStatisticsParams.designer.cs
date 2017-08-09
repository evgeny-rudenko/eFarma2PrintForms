namespace RCSPurchasesStatistics
{
    partial class PurchaseStatisticsParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseStatisticsParams));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCheque = new System.Windows.Forms.RadioButton();
            this.rbOut = new System.Windows.Forms.RadioButton();
            this.rbAllDoc = new System.Windows.Forms.RadioButton();
            this.chkES = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbSort = new System.Windows.Forms.GroupBox();
            this.rbRem = new System.Windows.Forms.RadioButton();
            this.rbGoods = new System.Windows.Forms.RadioButton();
            this.gbTopN = new System.Windows.Forms.GroupBox();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.rb200 = new System.Windows.Forms.RadioButton();
            this.rb300 = new System.Windows.Forms.RadioButton();
            this.rb400 = new System.Windows.Forms.RadioButton();
            this.rb500 = new System.Windows.Forms.RadioButton();
            this.rb1000 = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.nbOrderDays = new ePlus.CommonEx.Controls.ePlusNumericBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
            this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.chbGoodCode = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbSort.SuspendLayout();
            this.gbTopN.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(391, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(466, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 404);
            this.panel1.Size = new System.Drawing.Size(544, 29);
            this.panel1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCheque);
            this.groupBox1.Controls.Add(this.rbOut);
            this.groupBox1.Controls.Add(this.rbAllDoc);
            this.groupBox1.Location = new System.Drawing.Point(12, 341);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 53);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Обрабатываемые документы";
            // 
            // rbCheque
            // 
            this.rbCheque.AutoSize = true;
            this.rbCheque.Location = new System.Drawing.Point(298, 20);
            this.rbCheque.Name = "rbCheque";
            this.rbCheque.Size = new System.Drawing.Size(109, 17);
            this.rbCheque.TabIndex = 2;
            this.rbCheque.TabStop = true;
            this.rbCheque.Tag = "2";
            this.rbCheque.Text = "Расход по кассе";
            this.rbCheque.UseVisualStyleBackColor = true;
            // 
            // rbOut
            // 
            this.rbOut.AutoSize = true;
            this.rbOut.Location = new System.Drawing.Point(134, 20);
            this.rbOut.Name = "rbOut";
            this.rbOut.Size = new System.Drawing.Size(140, 17);
            this.rbOut.TabIndex = 1;
            this.rbOut.TabStop = true;
            this.rbOut.Tag = "1";
            this.rbOut.Text = "Расходные накладные";
            this.rbOut.UseVisualStyleBackColor = true;
            // 
            // rbAllDoc
            // 
            this.rbAllDoc.AutoSize = true;
            this.rbAllDoc.Checked = true;
            this.rbAllDoc.Location = new System.Drawing.Point(9, 19);
            this.rbAllDoc.Name = "rbAllDoc";
            this.rbAllDoc.Size = new System.Drawing.Size(103, 17);
            this.rbAllDoc.TabIndex = 0;
            this.rbAllDoc.TabStop = true;
            this.rbAllDoc.Tag = "0";
            this.rbAllDoc.Text = "Все документы";
            this.rbAllDoc.UseVisualStyleBackColor = true;
            // 
            // chkES
            // 
            this.chkES.AutoSize = true;
            this.chkES.Location = new System.Drawing.Point(326, 66);
            this.chkES.Name = "chkES";
            this.chkES.Size = new System.Drawing.Size(134, 17);
            this.chkES.TabIndex = 5;
            this.chkES.Text = "Сформировать по ЕС";
            this.chkES.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Количество дней заявки:";
            // 
            // gbSort
            // 
            this.gbSort.Controls.Add(this.rbRem);
            this.gbSort.Controls.Add(this.rbGoods);
            this.gbSort.Location = new System.Drawing.Point(12, 284);
            this.gbSort.Name = "gbSort";
            this.gbSort.Size = new System.Drawing.Size(517, 51);
            this.gbSort.TabIndex = 8;
            this.gbSort.TabStop = false;
            this.gbSort.Text = "Сортировка";
            // 
            // rbRem
            // 
            this.rbRem.AutoSize = true;
            this.rbRem.Location = new System.Drawing.Point(198, 19);
            this.rbRem.Name = "rbRem";
            this.rbRem.Size = new System.Drawing.Size(160, 17);
            this.rbRem.TabIndex = 1;
            this.rbRem.Text = "Остаток на конец периода";
            this.rbRem.UseVisualStyleBackColor = true;
            // 
            // rbGoods
            // 
            this.rbGoods.AutoSize = true;
            this.rbGoods.Checked = true;
            this.rbGoods.Location = new System.Drawing.Point(9, 19);
            this.rbGoods.Name = "rbGoods";
            this.rbGoods.Size = new System.Drawing.Size(163, 17);
            this.rbGoods.TabIndex = 0;
            this.rbGoods.TabStop = true;
            this.rbGoods.Text = "Медикаменты по алфавиту";
            this.rbGoods.UseVisualStyleBackColor = true;
            // 
            // gbTopN
            // 
            this.gbTopN.Controls.Add(this.rb100);
            this.gbTopN.Controls.Add(this.rb200);
            this.gbTopN.Controls.Add(this.rb300);
            this.gbTopN.Controls.Add(this.rb400);
            this.gbTopN.Controls.Add(this.rb500);
            this.gbTopN.Controls.Add(this.rb1000);
            this.gbTopN.Controls.Add(this.rbAll);
            this.gbTopN.Location = new System.Drawing.Point(411, 90);
            this.gbTopN.Name = "gbTopN";
            this.gbTopN.Size = new System.Drawing.Size(118, 188);
            this.gbTopN.TabIndex = 7;
            this.gbTopN.TabStop = false;
            this.gbTopN.Text = "Количество строк";
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Location = new System.Drawing.Point(6, 157);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(43, 17);
            this.rb100.TabIndex = 6;
            this.rb100.Tag = "100";
            this.rb100.Text = "100";
            this.rb100.UseVisualStyleBackColor = true;
            // 
            // rb200
            // 
            this.rb200.AutoSize = true;
            this.rb200.Location = new System.Drawing.Point(6, 134);
            this.rb200.Name = "rb200";
            this.rb200.Size = new System.Drawing.Size(43, 17);
            this.rb200.TabIndex = 5;
            this.rb200.Tag = "200";
            this.rb200.Text = "200";
            this.rb200.UseVisualStyleBackColor = true;
            // 
            // rb300
            // 
            this.rb300.AutoSize = true;
            this.rb300.Location = new System.Drawing.Point(6, 111);
            this.rb300.Name = "rb300";
            this.rb300.Size = new System.Drawing.Size(43, 17);
            this.rb300.TabIndex = 4;
            this.rb300.Tag = "300";
            this.rb300.Text = "300";
            this.rb300.UseVisualStyleBackColor = true;
            // 
            // rb400
            // 
            this.rb400.AutoSize = true;
            this.rb400.Location = new System.Drawing.Point(6, 88);
            this.rb400.Name = "rb400";
            this.rb400.Size = new System.Drawing.Size(43, 17);
            this.rb400.TabIndex = 3;
            this.rb400.Tag = "400";
            this.rb400.Text = "400";
            this.rb400.UseVisualStyleBackColor = true;
            // 
            // rb500
            // 
            this.rb500.AutoSize = true;
            this.rb500.Location = new System.Drawing.Point(6, 65);
            this.rb500.Name = "rb500";
            this.rb500.Size = new System.Drawing.Size(43, 17);
            this.rb500.TabIndex = 2;
            this.rb500.Tag = "500";
            this.rb500.Text = "500";
            this.rb500.UseVisualStyleBackColor = true;
            // 
            // rb1000
            // 
            this.rb1000.AutoSize = true;
            this.rb1000.Location = new System.Drawing.Point(6, 42);
            this.rb1000.Name = "rb1000";
            this.rb1000.Size = new System.Drawing.Size(49, 17);
            this.rb1000.TabIndex = 1;
            this.rb1000.Tag = "1000";
            this.rb1000.Text = "1000";
            this.rb1000.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(6, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(82, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Tag = "0";
            this.rbAll.Text = "Все строки";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // nbOrderDays
            // 
            this.nbOrderDays.DecimalPlaces = 0;
            this.nbOrderDays.DecimalSeparator = '.';
            this.nbOrderDays.Location = new System.Drawing.Point(157, 64);
            this.nbOrderDays.MaxLength = 3;
            this.nbOrderDays.Name = "nbOrderDays";
            this.nbOrderDays.Positive = true;
            this.nbOrderDays.Size = new System.Drawing.Size(138, 20);
            this.nbOrderDays.TabIndex = 4;
            this.nbOrderDays.Text = "7";
            this.nbOrderDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nbOrderDays.ThousandSeparator = '\0';
            this.nbOrderDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
            this.nbOrderDays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
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
            // ucStores
            // 
            this.ucStores.AllowSaveState = false;
            this.ucStores.Caption = "Склады";
            this.ucStores.Location = new System.Drawing.Point(12, 90);
            this.ucStores.Mnemocode = "STORE";
            this.ucStores.Name = "ucStores";
            this.ucStores.Pinnable = false;
            this.ucStores.Size = new System.Drawing.Size(389, 188);
            this.ucStores.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(544, 25);
            this.toolStrip1.TabIndex = 0;
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
            // chbGoodCode
            // 
            this.chbGoodCode.AutoSize = true;
            this.chbGoodCode.Location = new System.Drawing.Point(326, 40);
            this.chbGoodCode.Name = "chbGoodCode";
            this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
            this.chbGoodCode.TabIndex = 127;
            this.chbGoodCode.Text = "Отображать код товара ";
            this.chbGoodCode.UseVisualStyleBackColor = true;
            // 
            // PurchaseStatisticsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 433);
            this.Controls.Add(this.chbGoodCode);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkES);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbSort);
            this.Controls.Add(this.gbTopN);
            this.Controls.Add(this.nbOrderDays);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucStores);
            this.Name = "PurchaseStatisticsParams";
            this.Load += new System.EventHandler(this.PurchaseStatisticsParams_Load);
            this.Controls.SetChildIndex(this.ucStores, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.nbOrderDays, 0);
            this.Controls.SetChildIndex(this.gbTopN, 0);
            this.Controls.SetChildIndex(this.gbSort, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkES, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.chbGoodCode, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbSort.ResumeLayout(false);
            this.gbSort.PerformLayout();
            this.gbTopN.ResumeLayout(false);
            this.gbTopN.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCheque;
        private System.Windows.Forms.RadioButton rbOut;
        private System.Windows.Forms.RadioButton rbAllDoc;
        private System.Windows.Forms.CheckBox chkES;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbSort;
        private System.Windows.Forms.RadioButton rbRem;
        private System.Windows.Forms.RadioButton rbGoods;
        private System.Windows.Forms.GroupBox gbTopN;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.RadioButton rb200;
        private System.Windows.Forms.RadioButton rb300;
        private System.Windows.Forms.RadioButton rb400;
        private System.Windows.Forms.RadioButton rb500;
        private System.Windows.Forms.RadioButton rb1000;
        private System.Windows.Forms.RadioButton rbAll;
        private ePlus.CommonEx.Controls.ePlusNumericBox nbOrderDays;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.CheckBox chbGoodCode;
    }
}