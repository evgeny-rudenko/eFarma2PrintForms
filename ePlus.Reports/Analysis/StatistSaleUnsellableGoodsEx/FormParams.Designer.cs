namespace RCBStatistSaleUnsellableGoods
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
          this.ucTradeName = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.goods = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.chbGroupGoods = new System.Windows.Forms.CheckBox();
          this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.selectGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
          this.gbType = new System.Windows.Forms.GroupBox();
          this.rbCheckType = new System.Windows.Forms.RadioButton();
          this.rbAllType = new System.Windows.Forms.RadioButton();
          this.chbMovement = new System.Windows.Forms.CheckBox();
          this.chbOut = new System.Windows.Forms.CheckBox();
          this.chbKKM = new System.Windows.Forms.CheckBox();
          this.comboSort = new System.Windows.Forms.ComboBox();
          this.comboRows = new System.Windows.Forms.ComboBox();
          this.period = new ePlus.MetaData.Client.UCPeriod();
          this.labelSort = new System.Windows.Forms.Label();
          this.labelPeriod = new System.Windows.Forms.Label();
          this.labelRows = new System.Windows.Forms.Label();
          this.numericPercent = new System.Windows.Forms.NumericUpDown();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.rbQuantity = new System.Windows.Forms.RadioButton();
          this.rbPercent = new System.Windows.Forms.RadioButton();
          this.chbGoodCode = new System.Windows.Forms.CheckBox();
          this.label2 = new System.Windows.Forms.Label();
          this.sortOrderComboBox = new System.Windows.Forms.ComboBox();
          this.auCheckBox = new System.Windows.Forms.CheckBox();
          this.panel1.SuspendLayout();
          this.gbType.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).BeginInit();
          this.groupBox1.SuspendLayout();
          this.SuspendLayout();
          // 
          // bOK
          // 
          this.bOK.Location = new System.Drawing.Point(401, 3);
          // 
          // bClose
          // 
          this.bClose.Location = new System.Drawing.Point(476, 3);
          // 
          // panel1
          // 
          this.panel1.Location = new System.Drawing.Point(0, 410);
          this.panel1.Size = new System.Drawing.Size(554, 29);
          // 
          // ucTradeName
          // 
          this.ucTradeName.AllowSaveState = true;
          this.ucTradeName.Caption = "Торговая марка/наименование";
          this.ucTradeName.Location = new System.Drawing.Point(236, 137);
          this.ucTradeName.Mnemocode = "TRADE_NAME";
          this.ucTradeName.Name = "ucTradeName";
          this.ucTradeName.Size = new System.Drawing.Size(300, 100);
          this.ucTradeName.TabIndex = 159;
          // 
          // goods
          // 
          this.goods.AllowSaveState = true;
          this.goods.Caption = "Товары";
          this.goods.Location = new System.Drawing.Point(235, 236);
          this.goods.Mnemocode = "GOODS2";
          this.goods.Name = "goods";
          this.goods.Size = new System.Drawing.Size(301, 117);
          this.goods.TabIndex = 157;
          this.goods.Visible = false;
          // 
          // chbGroupGoods
          // 
          this.chbGroupGoods.AutoSize = true;
          this.chbGroupGoods.Location = new System.Drawing.Point(24, 357);
          this.chbGroupGoods.Name = "chbGroupGoods";
          this.chbGroupGoods.Size = new System.Drawing.Size(183, 17);
          this.chbGroupGoods.TabIndex = 158;
          this.chbGroupGoods.Text = "Сворачивать товар по группам";
          this.chbGroupGoods.UseVisualStyleBackColor = true;
          // 
          // stores
          // 
          this.stores.AllowSaveState = true;
          this.stores.Caption = "Склады";
          this.stores.Location = new System.Drawing.Point(12, 137);
          this.stores.Mnemocode = "STORE";
          this.stores.Name = "stores";
          this.stores.Size = new System.Drawing.Size(218, 100);
          this.stores.TabIndex = 156;
          // 
          // selectGoodsGroup
          // 
          this.selectGoodsGroup.Location = new System.Drawing.Point(16, 236);
          this.selectGoodsGroup.Name = "selectGoodsGroup";
          this.selectGoodsGroup.Size = new System.Drawing.Size(214, 124);
          this.selectGoodsGroup.TabIndex = 160;
          // 
          // gbType
          // 
          this.gbType.Controls.Add(this.rbCheckType);
          this.gbType.Controls.Add(this.rbAllType);
          this.gbType.Controls.Add(this.chbMovement);
          this.gbType.Controls.Add(this.chbOut);
          this.gbType.Controls.Add(this.chbKKM);
          this.gbType.Location = new System.Drawing.Point(327, 26);
          this.gbType.Name = "gbType";
          this.gbType.Size = new System.Drawing.Size(207, 105);
          this.gbType.TabIndex = 155;
          this.gbType.TabStop = false;
          this.gbType.Text = "Вид расхода";
          // 
          // rbCheckType
          // 
          this.rbCheckType.AutoSize = true;
          this.rbCheckType.Checked = true;
          this.rbCheckType.Location = new System.Drawing.Point(5, 30);
          this.rbCheckType.Name = "rbCheckType";
          this.rbCheckType.Size = new System.Drawing.Size(195, 17);
          this.rbCheckType.TabIndex = 5;
          this.rbCheckType.TabStop = true;
          this.rbCheckType.Text = "Выбранные виды расхода товара";
          this.rbCheckType.UseVisualStyleBackColor = true;
          // 
          // rbAllType
          // 
          this.rbAllType.AutoSize = true;
          this.rbAllType.Location = new System.Drawing.Point(5, 13);
          this.rbAllType.Name = "rbAllType";
          this.rbAllType.Size = new System.Drawing.Size(155, 17);
          this.rbAllType.TabIndex = 4;
          this.rbAllType.Text = "Все виды расхода товара";
          this.rbAllType.UseVisualStyleBackColor = true;
          this.rbAllType.CheckedChanged += new System.EventHandler(this.rbAllType_CheckedChanged);
          // 
          // chbMovement
          // 
          this.chbMovement.AutoSize = true;
          this.chbMovement.Location = new System.Drawing.Point(22, 78);
          this.chbMovement.Name = "chbMovement";
          this.chbMovement.Size = new System.Drawing.Size(137, 17);
          this.chbMovement.TabIndex = 3;
          this.chbMovement.Tag = "3";
          this.chbMovement.Text = "Перемещение товара";
          this.chbMovement.UseVisualStyleBackColor = true;
          // 
          // chbOut
          // 
          this.chbOut.AutoSize = true;
          this.chbOut.Location = new System.Drawing.Point(22, 62);
          this.chbOut.Name = "chbOut";
          this.chbOut.Size = new System.Drawing.Size(141, 17);
          this.chbOut.TabIndex = 2;
          this.chbOut.Tag = "2";
          this.chbOut.Text = "Расходные накладные";
          this.chbOut.UseVisualStyleBackColor = true;
          // 
          // chbKKM
          // 
          this.chbKKM.AutoSize = true;
          this.chbKKM.Location = new System.Drawing.Point(22, 46);
          this.chbKKM.Name = "chbKKM";
          this.chbKKM.Size = new System.Drawing.Size(113, 17);
          this.chbKKM.TabIndex = 1;
          this.chbKKM.Tag = "1";
          this.chbKKM.Text = "Продажа по ККМ";
          this.chbKKM.UseVisualStyleBackColor = true;
          // 
          // comboSort
          // 
          this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboSort.FormattingEnabled = true;
          this.comboSort.Location = new System.Drawing.Point(102, 30);
          this.comboSort.Name = "comboSort";
          this.comboSort.Size = new System.Drawing.Size(219, 21);
          this.comboSort.TabIndex = 152;
          // 
          // comboRows
          // 
          this.comboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboRows.FormattingEnabled = true;
          this.comboRows.Location = new System.Drawing.Point(394, 355);
          this.comboRows.Name = "comboRows";
          this.comboRows.Size = new System.Drawing.Size(148, 21);
          this.comboRows.TabIndex = 154;
          this.comboRows.Visible = false;
          // 
          // period
          // 
          this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.Location = new System.Drawing.Point(102, 3);
          this.period.Name = "period";
          this.period.Size = new System.Drawing.Size(230, 21);
          this.period.TabIndex = 149;
          // 
          // labelSort
          // 
          this.labelSort.Location = new System.Drawing.Point(7, 30);
          this.labelSort.Name = "labelSort";
          this.labelSort.Size = new System.Drawing.Size(100, 21);
          this.labelSort.TabIndex = 151;
          this.labelSort.Text = "Сортировать по : ";
          this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labelPeriod
          // 
          this.labelPeriod.Location = new System.Drawing.Point(7, 3);
          this.labelPeriod.Name = "labelPeriod";
          this.labelPeriod.Size = new System.Drawing.Size(51, 21);
          this.labelPeriod.TabIndex = 150;
          this.labelPeriod.Text = "Период : ";
          this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labelRows
          // 
          this.labelRows.Location = new System.Drawing.Point(370, 3);
          this.labelRows.Name = "labelRows";
          this.labelRows.Size = new System.Drawing.Size(172, 21);
          this.labelRows.TabIndex = 153;
          this.labelRows.Text = "Кол.позиций для отображения:";
          this.labelRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.labelRows.Visible = false;
          // 
          // numericPercent
          // 
          this.numericPercent.Location = new System.Drawing.Point(139, 18);
          this.numericPercent.Name = "numericPercent";
          this.numericPercent.Size = new System.Drawing.Size(56, 20);
          this.numericPercent.TabIndex = 162;
          this.numericPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.numericPercent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.rbQuantity);
          this.groupBox1.Controls.Add(this.numericPercent);
          this.groupBox1.Controls.Add(this.rbPercent);
          this.groupBox1.Location = new System.Drawing.Point(12, 84);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(309, 47);
          this.groupBox1.TabIndex = 163;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Вид отбора";
          // 
          // rbQuantity
          // 
          this.rbQuantity.AutoSize = true;
          this.rbQuantity.Location = new System.Drawing.Point(4, 26);
          this.rbQuantity.Name = "rbQuantity";
          this.rbQuantity.Size = new System.Drawing.Size(131, 17);
          this.rbQuantity.TabIndex = 1;
          this.rbQuantity.Text = "Продано меньше(шт)";
          this.rbQuantity.UseVisualStyleBackColor = true;
          // 
          // rbPercent
          // 
          this.rbPercent.AutoSize = true;
          this.rbPercent.Checked = true;
          this.rbPercent.Location = new System.Drawing.Point(4, 11);
          this.rbPercent.Name = "rbPercent";
          this.rbPercent.Size = new System.Drawing.Size(109, 17);
          this.rbPercent.TabIndex = 0;
          this.rbPercent.TabStop = true;
          this.rbPercent.Text = "Процент продаж";
          this.rbPercent.UseVisualStyleBackColor = true;
          // 
          // chbGoodCode
          // 
          this.chbGoodCode.AutoSize = true;
          this.chbGoodCode.Location = new System.Drawing.Point(232, 357);
          this.chbGoodCode.Name = "chbGoodCode";
          this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
          this.chbGoodCode.TabIndex = 165;
          this.chbGoodCode.Text = "Отображать код товара ";
          this.chbGoodCode.UseVisualStyleBackColor = true;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(7, 60);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(116, 13);
          this.label2.TabIndex = 169;
          this.label2.Text = "Порядок сортировки:";
          // 
          // sortOrderComboBox
          // 
          this.sortOrderComboBox.FormattingEnabled = true;
          this.sortOrderComboBox.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
          this.sortOrderComboBox.Location = new System.Drawing.Point(135, 57);
          this.sortOrderComboBox.Name = "sortOrderComboBox";
          this.sortOrderComboBox.Size = new System.Drawing.Size(186, 21);
          this.sortOrderComboBox.TabIndex = 168;
          // 
          // auCheckBox
          // 
          this.auCheckBox.AutoSize = true;
          this.auCheckBox.Location = new System.Drawing.Point(24, 380);
          this.auCheckBox.Name = "auCheckBox";
          this.auCheckBox.Size = new System.Drawing.Size(234, 17);
          this.auCheckBox.TabIndex = 170;
          this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
          this.auCheckBox.UseVisualStyleBackColor = true;
          // 
          // FormParams
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(554, 439);
          this.Controls.Add(this.auCheckBox);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.sortOrderComboBox);
          this.Controls.Add(this.chbGoodCode);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.ucTradeName);
          this.Controls.Add(this.goods);
          this.Controls.Add(this.chbGroupGoods);
          this.Controls.Add(this.stores);
          this.Controls.Add(this.selectGoodsGroup);
          this.Controls.Add(this.gbType);
          this.Controls.Add(this.comboSort);
          this.Controls.Add(this.comboRows);
          this.Controls.Add(this.period);
          this.Controls.Add(this.labelSort);
          this.Controls.Add(this.labelPeriod);
          this.Controls.Add(this.labelRows);
          this.Name = "FormParams";
          this.Load += new System.EventHandler(this.FormParam_Load);
          this.Shown += new System.EventHandler(this.FormParam_Shown);
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParam_FormClosed);
          this.Controls.SetChildIndex(this.panel1, 0);
          this.Controls.SetChildIndex(this.labelRows, 0);
          this.Controls.SetChildIndex(this.labelPeriod, 0);
          this.Controls.SetChildIndex(this.labelSort, 0);
          this.Controls.SetChildIndex(this.period, 0);
          this.Controls.SetChildIndex(this.comboRows, 0);
          this.Controls.SetChildIndex(this.comboSort, 0);
          this.Controls.SetChildIndex(this.gbType, 0);
          this.Controls.SetChildIndex(this.selectGoodsGroup, 0);
          this.Controls.SetChildIndex(this.stores, 0);
          this.Controls.SetChildIndex(this.chbGroupGoods, 0);
          this.Controls.SetChildIndex(this.goods, 0);
          this.Controls.SetChildIndex(this.ucTradeName, 0);
          this.Controls.SetChildIndex(this.groupBox1, 0);
          this.Controls.SetChildIndex(this.chbGoodCode, 0);
          this.Controls.SetChildIndex(this.sortOrderComboBox, 0);
          this.Controls.SetChildIndex(this.label2, 0);
          this.Controls.SetChildIndex(this.auCheckBox, 0);
          this.panel1.ResumeLayout(false);
          this.gbType.ResumeLayout(false);
          this.gbType.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).EndInit();
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        public ePlus.MetaData.Client.UCPluginMultiSelect ucTradeName;
        public ePlus.MetaData.Client.UCPluginMultiSelect goods;
        private System.Windows.Forms.CheckBox chbGroupGoods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public ePlus.CommonEx.Controls.SelectGoodsGroup selectGoodsGroup;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbCheckType;
        private System.Windows.Forms.RadioButton rbAllType;
        private System.Windows.Forms.CheckBox chbMovement;
        private System.Windows.Forms.CheckBox chbOut;
        private System.Windows.Forms.CheckBox chbKKM;
        public System.Windows.Forms.ComboBox comboSort;
        public System.Windows.Forms.ComboBox comboRows;
        public ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label labelRows;
        public System.Windows.Forms.NumericUpDown numericPercent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbQuantity;
        private System.Windows.Forms.RadioButton rbPercent;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox sortOrderComboBox;
		private System.Windows.Forms.CheckBox auCheckBox;

    }
}