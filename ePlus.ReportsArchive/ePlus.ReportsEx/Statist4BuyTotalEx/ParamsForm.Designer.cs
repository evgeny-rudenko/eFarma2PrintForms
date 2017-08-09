namespace RCRStatist4BuyTotalEx
{
    partial class ParamsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCheque = new System.Windows.Forms.RadioButton();
            this.rbOut = new System.Windows.Forms.RadioButton();
            this.rbAllDoc = new System.Windows.Forms.RadioButton();
            this.chkES = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.period = new ePlus.MetaData.Controls.UCPeriod();
            this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.nbRemDays = new ePlus.CommonEx.Controls.ePlusNumericBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbSort.SuspendLayout();
            this.gbTopN.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(394, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(469, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 447);
            this.panel1.Size = new System.Drawing.Size(547, 29);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCheque);
            this.groupBox1.Controls.Add(this.rbOut);
            this.groupBox1.Controls.Add(this.rbAllDoc);
            this.groupBox1.Location = new System.Drawing.Point(11, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 53);
            this.groupBox1.TabIndex = 31;
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
            this.rbAllDoc.Location = new System.Drawing.Point(7, 20);
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
            this.chkES.Location = new System.Drawing.Point(8, 95);
            this.chkES.Name = "chkES";
            this.chkES.Size = new System.Drawing.Size(134, 17);
            this.chkES.TabIndex = 30;
            this.chkES.Text = "Сформировать по ЕС";
            this.chkES.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Количество дней заказа:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Количество дней доставки:";
            // 
            // gbSort
            // 
            this.gbSort.Controls.Add(this.rbRem);
            this.gbSort.Controls.Add(this.rbGoods);
            this.gbSort.Location = new System.Drawing.Point(288, 12);
            this.gbSort.Name = "gbSort";
            this.gbSort.Size = new System.Drawing.Size(248, 73);
            this.gbSort.TabIndex = 27;
            this.gbSort.TabStop = false;
            this.gbSort.Text = "Сотрировка";
            // 
            // rbRem
            // 
            this.rbRem.AutoSize = true;
            this.rbRem.Checked = true;
            this.rbRem.Location = new System.Drawing.Point(6, 42);
            this.rbRem.Name = "rbRem";
            this.rbRem.Size = new System.Drawing.Size(160, 17);
            this.rbRem.TabIndex = 1;
            this.rbRem.TabStop = true;
            this.rbRem.Text = "Остаток на конец периода";
            this.rbRem.UseVisualStyleBackColor = true;
            // 
            // rbGoods
            // 
            this.rbGoods.AutoSize = true;
            this.rbGoods.Location = new System.Drawing.Point(6, 19);
            this.rbGoods.Name = "rbGoods";
            this.rbGoods.Size = new System.Drawing.Size(163, 17);
            this.rbGoods.TabIndex = 0;
            this.rbGoods.Text = "Медикаменты по алфавиту";
            this.rbGoods.UseVisualStyleBackColor = true;
            // 
            // gbTopN
            // 
            this.gbTopN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbTopN.Controls.Add(this.rb100);
            this.gbTopN.Controls.Add(this.rb200);
            this.gbTopN.Controls.Add(this.rb300);
            this.gbTopN.Controls.Add(this.rb400);
            this.gbTopN.Controls.Add(this.rb500);
            this.gbTopN.Controls.Add(this.rb1000);
            this.gbTopN.Controls.Add(this.rbAll);
            this.gbTopN.Location = new System.Drawing.Point(404, 178);
            this.gbTopN.Name = "gbTopN";
            this.gbTopN.Size = new System.Drawing.Size(132, 185);
            this.gbTopN.TabIndex = 26;
            this.gbTopN.TabStop = false;
            this.gbTopN.Text = "Количество строк";
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Location = new System.Drawing.Point(6, 157);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(43, 17);
            this.rb100.TabIndex = 8;
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
            this.rb200.TabIndex = 7;
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
            this.rb300.TabIndex = 6;
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
            this.rb400.TabIndex = 5;
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
            this.rb500.TabIndex = 4;
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
            this.rb1000.TabIndex = 3;
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
            this.rbAll.TabIndex = 2;
            this.rbAll.TabStop = true;
            this.rbAll.Tag = "0";
            this.rbAll.Text = "Все строки";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // nbOrderDays
            // 
            this.nbOrderDays.DecimalPlaces = 0;
            this.nbOrderDays.DecimalSeparator = '.';
            this.nbOrderDays.Location = new System.Drawing.Point(156, 65);
            this.nbOrderDays.MaxLength = 3;
            this.nbOrderDays.Name = "nbOrderDays";
            this.nbOrderDays.Positive = true;
            this.nbOrderDays.Size = new System.Drawing.Size(126, 20);
            this.nbOrderDays.TabIndex = 25;
            this.nbOrderDays.Text = "0";
            this.nbOrderDays.ThousandSeparator = '\0';
            this.nbOrderDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
            this.nbOrderDays.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Период:";
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
            this.period.DateTo = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
            this.period.Location = new System.Drawing.Point(60, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(222, 21);
            this.period.TabIndex = 21;
            // 
            // stores
            // 
            this.stores.AllowSaveState = false;
            this.stores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.stores.Caption = "Склады";
            this.stores.Location = new System.Drawing.Point(8, 178);
            this.stores.Mnemocode = "STORE";
            this.stores.Name = "stores";
            //this.stores.Pinnable = false;
            this.stores.Size = new System.Drawing.Size(389, 187);
            this.stores.TabIndex = 22;
            // 
            // nbRemDays
            // 
            this.nbRemDays.DecimalPlaces = 0;
            this.nbRemDays.DecimalSeparator = '.';
            this.nbRemDays.Location = new System.Drawing.Point(156, 39);
            this.nbRemDays.MaxLength = 3;
            this.nbRemDays.Name = "nbRemDays";
            this.nbRemDays.Positive = true;
            this.nbRemDays.Size = new System.Drawing.Size(126, 20);
            this.nbRemDays.TabIndex = 24;
            this.nbRemDays.Text = "0";
            this.nbRemDays.ThousandSeparator = '\0';
            this.nbRemDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
            this.nbRemDays.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 476);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkES);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbSort);
            this.Controls.Add(this.gbTopN);
            this.Controls.Add(this.nbOrderDays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.period);
            this.Controls.Add(this.stores);
            this.Controls.Add(this.nbRemDays);
            this.Name = "ParamsForm";
            this.Load += new System.EventHandler(this.ParamsForm_Load);
            this.Controls.SetChildIndex(this.nbRemDays, 0);
            this.Controls.SetChildIndex(this.stores, 0);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.nbOrderDays, 0);
            this.Controls.SetChildIndex(this.gbTopN, 0);
            this.Controls.SetChildIndex(this.gbSort, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkES, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbSort.ResumeLayout(false);
            this.gbSort.PerformLayout();
            this.gbTopN.ResumeLayout(false);
            this.gbTopN.PerformLayout();
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
        private System.Windows.Forms.Label label2;
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
        private ePlus.MetaData.Controls.UCPeriod period;
        private ePlus.MetaData.Client.UCPluginMultiSelect stores;
        private ePlus.CommonEx.Controls.ePlusNumericBox nbRemDays;
    }
}