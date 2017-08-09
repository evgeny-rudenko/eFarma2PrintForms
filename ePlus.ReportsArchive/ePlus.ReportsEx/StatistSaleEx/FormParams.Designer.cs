namespace StatistSaleEx
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
          this.goods = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.comboType = new System.Windows.Forms.ComboBox();
          this.labelType = new System.Windows.Forms.Label();
          this.comboRows = new System.Windows.Forms.ComboBox();
          this.labelRows = new System.Windows.Forms.Label();
          this.comboSort = new System.Windows.Forms.ComboBox();
          this.labelSort = new System.Windows.Forms.Label();
          this.numericPercent = new System.Windows.Forms.NumericUpDown();
          this.labelPercent = new System.Windows.Forms.Label();
          this.comboReport = new System.Windows.Forms.ComboBox();
          this.labelReport = new System.Windows.Forms.Label();
          this.checkParts = new System.Windows.Forms.CheckBox();
          this.labelPeriod = new System.Windows.Forms.Label();
          this.period = new ePlus.MetaData.Client.UCPeriod();
          this.labelParts = new System.Windows.Forms.Label();
          this.chbGroupGoods = new System.Windows.Forms.CheckBox();
          this.chbGoodCode = new System.Windows.Forms.CheckBox();
          this.cbZNVLS = new System.Windows.Forms.ComboBox();
          this.label1 = new System.Windows.Forms.Label();
          this.cbPKKN = new System.Windows.Forms.ComboBox();
          this.label2 = new System.Windows.Forms.Label();
          this.goodsKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.panel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).BeginInit();
          this.SuspendLayout();
          // 
          // bOK
          // 
          this.bOK.Location = new System.Drawing.Point(386, 3);
          // 
          // bClose
          // 
          this.bClose.Location = new System.Drawing.Point(461, 3);
          // 
          // panel1
          // 
          this.panel1.Location = new System.Drawing.Point(0, 349);
          this.panel1.Size = new System.Drawing.Size(539, 29);
          // 
          // goods
          // 
          this.goods.AllowSaveState = false;
          this.goods.Caption = "Товары";
          this.goods.Location = new System.Drawing.Point(190, 120);
          this.goods.Mnemocode = "GOODS2";
          this.goods.Name = "goods";
          this.goods.Size = new System.Drawing.Size(322, 100);
          this.goods.TabIndex = 121;
          // 
          // stores
          // 
          this.stores.AllowSaveState = false;
          this.stores.Caption = "Склады";
          this.stores.Location = new System.Drawing.Point(12, 120);
          this.stores.Mnemocode = "STORE";
          this.stores.Name = "stores";
          this.stores.Size = new System.Drawing.Size(180, 100);
          this.stores.TabIndex = 120;
          // 
          // comboType
          // 
          this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboType.FormattingEnabled = true;
          this.comboType.Location = new System.Drawing.Point(104, 93);
          this.comboType.Name = "comboType";
          this.comboType.Size = new System.Drawing.Size(210, 21);
          this.comboType.TabIndex = 119;
          // 
          // labelType
          // 
          this.labelType.Location = new System.Drawing.Point(9, 92);
          this.labelType.Name = "labelType";
          this.labelType.Size = new System.Drawing.Size(80, 21);
          this.labelType.TabIndex = 118;
          this.labelType.Text = "Вид расхода : ";
          this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // comboRows
          // 
          this.comboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboRows.FormattingEnabled = true;
          this.comboRows.Location = new System.Drawing.Point(429, 93);
          this.comboRows.Name = "comboRows";
          this.comboRows.Size = new System.Drawing.Size(80, 21);
          this.comboRows.TabIndex = 117;
          // 
          // labelRows
          // 
          this.labelRows.Location = new System.Drawing.Point(324, 92);
          this.labelRows.Name = "labelRows";
          this.labelRows.Size = new System.Drawing.Size(110, 21);
          this.labelRows.TabIndex = 116;
          this.labelRows.Text = "Вывести позиции : ";
          this.labelRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // comboSort
          // 
          this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboSort.FormattingEnabled = true;
          this.comboSort.Location = new System.Drawing.Point(104, 66);
          this.comboSort.Name = "comboSort";
          this.comboSort.Size = new System.Drawing.Size(405, 21);
          this.comboSort.TabIndex = 115;
          // 
          // labelSort
          // 
          this.labelSort.Location = new System.Drawing.Point(9, 65);
          this.labelSort.Name = "labelSort";
          this.labelSort.Size = new System.Drawing.Size(100, 21);
          this.labelSort.TabIndex = 114;
          this.labelSort.Text = "Сортировать по : ";
          this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // numericPercent
          // 
          this.numericPercent.Location = new System.Drawing.Point(429, 40);
          this.numericPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.numericPercent.Name = "numericPercent";
          this.numericPercent.Size = new System.Drawing.Size(80, 20);
          this.numericPercent.TabIndex = 113;
          this.numericPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // labelPercent
          // 
          this.labelPercent.Location = new System.Drawing.Point(329, 39);
          this.labelPercent.Name = "labelPercent";
          this.labelPercent.Size = new System.Drawing.Size(100, 21);
          this.labelPercent.TabIndex = 112;
          this.labelPercent.Text = "Процент продаж : ";
          this.labelPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // comboReport
          // 
          this.comboReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboReport.FormattingEnabled = true;
          this.comboReport.Location = new System.Drawing.Point(104, 39);
          this.comboReport.Name = "comboReport";
          this.comboReport.Size = new System.Drawing.Size(210, 21);
          this.comboReport.TabIndex = 111;
          // 
          // labelReport
          // 
          this.labelReport.Location = new System.Drawing.Point(9, 38);
          this.labelReport.Name = "labelReport";
          this.labelReport.Size = new System.Drawing.Size(70, 21);
          this.labelReport.TabIndex = 110;
          this.labelReport.Text = "Вид отчета : ";
          this.labelReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // checkParts
          // 
          this.checkParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.checkParts.Location = new System.Drawing.Point(313, 13);
          this.checkParts.Name = "checkParts";
          this.checkParts.Size = new System.Drawing.Size(11, 12);
          this.checkParts.TabIndex = 108;
          this.checkParts.UseVisualStyleBackColor = true;
          this.checkParts.Visible = false;
          // 
          // labelPeriod
          // 
          this.labelPeriod.Location = new System.Drawing.Point(9, 12);
          this.labelPeriod.Name = "labelPeriod";
          this.labelPeriod.Size = new System.Drawing.Size(51, 21);
          this.labelPeriod.TabIndex = 107;
          this.labelPeriod.Text = "Период : ";
          this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // period
          // 
          this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.Location = new System.Drawing.Point(104, 12);
          this.period.Name = "period";
          this.period.Size = new System.Drawing.Size(195, 21);
          this.period.TabIndex = 106;
          // 
          // labelParts
          // 
          this.labelParts.Location = new System.Drawing.Point(324, 11);
          this.labelParts.Name = "labelParts";
          this.labelParts.Size = new System.Drawing.Size(188, 41);
          this.labelParts.TabIndex = 109;
          this.labelParts.Text = "Отображать разукомплектованные товары отдельно";
          this.labelParts.Visible = false;
          // 
          // chbGroupGoods
          // 
          this.chbGroupGoods.AutoSize = true;
          this.chbGroupGoods.Location = new System.Drawing.Point(12, 226);
          this.chbGroupGoods.Name = "chbGroupGoods";
          this.chbGroupGoods.Size = new System.Drawing.Size(183, 17);
          this.chbGroupGoods.TabIndex = 122;
          this.chbGroupGoods.Text = "Сворачивать товар по группам";
          this.chbGroupGoods.UseVisualStyleBackColor = true;
          // 
          // chbGoodCode
          // 
          this.chbGoodCode.AutoSize = true;
          this.chbGoodCode.Location = new System.Drawing.Point(12, 249);
          this.chbGoodCode.Name = "chbGoodCode";
          this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
          this.chbGoodCode.TabIndex = 123;
          this.chbGoodCode.Text = "Отображать код товара ";
          this.chbGoodCode.UseVisualStyleBackColor = true;
          // 
          // cbZNVLS
          // 
          this.cbZNVLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbZNVLS.FormattingEnabled = true;
          this.cbZNVLS.Location = new System.Drawing.Point(67, 270);
          this.cbZNVLS.Name = "cbZNVLS";
          this.cbZNVLS.Size = new System.Drawing.Size(121, 21);
          this.cbZNVLS.TabIndex = 124;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(10, 300);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(40, 13);
          this.label1.TabIndex = 126;
          this.label1.Text = "ПККН:";
          // 
          // cbPKKN
          // 
          this.cbPKKN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbPKKN.FormattingEnabled = true;
          this.cbPKKN.Location = new System.Drawing.Point(67, 297);
          this.cbPKKN.Name = "cbPKKN";
          this.cbPKKN.Size = new System.Drawing.Size(121, 21);
          this.cbPKKN.TabIndex = 127;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(10, 273);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(51, 13);
          this.label2.TabIndex = 128;
          this.label2.Text = "ЖНВЛС:";
          // 
          // goodsKind
          // 
          this.goodsKind.AllowSaveState = false;
          this.goodsKind.Caption = "Товары";
          this.goodsKind.Location = new System.Drawing.Point(194, 226);
          this.goodsKind.Mnemocode = "GOODS_KIND";
          this.goodsKind.Name = "goodsKind";
          this.goodsKind.Size = new System.Drawing.Size(315, 100);
          this.goodsKind.TabIndex = 129;
          // 
          // FormParams
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(539, 378);
          this.Controls.Add(this.goodsKind);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.cbPKKN);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.cbZNVLS);
          this.Controls.Add(this.chbGroupGoods);
          this.Controls.Add(this.chbGoodCode);
          this.Controls.Add(this.goods);
          this.Controls.Add(this.stores);
          this.Controls.Add(this.comboType);
          this.Controls.Add(this.labelType);
          this.Controls.Add(this.comboRows);
          this.Controls.Add(this.labelRows);
          this.Controls.Add(this.comboSort);
          this.Controls.Add(this.labelSort);
          this.Controls.Add(this.numericPercent);
          this.Controls.Add(this.labelPercent);
          this.Controls.Add(this.comboReport);
          this.Controls.Add(this.labelReport);
          this.Controls.Add(this.checkParts);
          this.Controls.Add(this.labelPeriod);
          this.Controls.Add(this.period);
          this.Controls.Add(this.labelParts);
          this.Name = "FormParams";
          this.Shown += new System.EventHandler(this.FormParams_Shown);
          this.Controls.SetChildIndex(this.labelParts, 0);
          this.Controls.SetChildIndex(this.period, 0);
          this.Controls.SetChildIndex(this.labelPeriod, 0);
          this.Controls.SetChildIndex(this.checkParts, 0);
          this.Controls.SetChildIndex(this.labelReport, 0);
          this.Controls.SetChildIndex(this.comboReport, 0);
          this.Controls.SetChildIndex(this.labelPercent, 0);
          this.Controls.SetChildIndex(this.numericPercent, 0);
          this.Controls.SetChildIndex(this.labelSort, 0);
          this.Controls.SetChildIndex(this.comboSort, 0);
          this.Controls.SetChildIndex(this.labelRows, 0);
          this.Controls.SetChildIndex(this.comboRows, 0);
          this.Controls.SetChildIndex(this.labelType, 0);
          this.Controls.SetChildIndex(this.comboType, 0);
          this.Controls.SetChildIndex(this.stores, 0);
          this.Controls.SetChildIndex(this.goods, 0);
          this.Controls.SetChildIndex(this.chbGoodCode, 0);
          this.Controls.SetChildIndex(this.chbGroupGoods, 0);
          this.Controls.SetChildIndex(this.cbZNVLS, 0);
          this.Controls.SetChildIndex(this.panel1, 0);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.cbPKKN, 0);
          this.Controls.SetChildIndex(this.label2, 0);
          this.Controls.SetChildIndex(this.goodsKind, 0);
          this.panel1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        public ePlus.MetaData.Client.UCPluginMultiSelect goods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label labelType;
        public System.Windows.Forms.ComboBox comboRows;
        private System.Windows.Forms.Label labelRows;
        public System.Windows.Forms.ComboBox comboSort;
        private System.Windows.Forms.Label labelSort;
        public System.Windows.Forms.NumericUpDown numericPercent;
        private System.Windows.Forms.Label labelPercent;
        public System.Windows.Forms.ComboBox comboReport;
        private System.Windows.Forms.Label labelReport;
        public System.Windows.Forms.CheckBox checkParts;
        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label labelParts;
        private System.Windows.Forms.CheckBox chbGroupGoods;
		private System.Windows.Forms.CheckBox chbGoodCode;
      private System.Windows.Forms.ComboBox cbZNVLS;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox cbPKKN;
      private System.Windows.Forms.Label label2;
      public ePlus.MetaData.Client.UCPluginMultiSelect goodsKind;
    }
}