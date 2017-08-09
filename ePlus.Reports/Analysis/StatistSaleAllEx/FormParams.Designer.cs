namespace RCBStatistSaleAll
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
          this.chbGroupGoodsClassifier = new System.Windows.Forms.CheckBox();
          this.goods = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.comboSort = new System.Windows.Forms.ComboBox();
          this.labelSort = new System.Windows.Forms.Label();
          this.labelPeriod = new System.Windows.Forms.Label();
          this.period = new ePlus.MetaData.Client.UCPeriod();
          this.gbType = new System.Windows.Forms.GroupBox();
          this.rbCheckType = new System.Windows.Forms.RadioButton();
          this.rbAllType = new System.Windows.Forms.RadioButton();
          this.chbMovement = new System.Windows.Forms.CheckBox();
          this.chbOut = new System.Windows.Forms.CheckBox();
          this.chbKKM = new System.Windows.Forms.CheckBox();
          this.comboRows = new System.Windows.Forms.ComboBox();
          this.labelRows = new System.Windows.Forms.Label();
          this.ucTradeName = new ePlus.MetaData.Client.UCPluginMultiSelect();
          this.selectGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
          this.chbGoodCode = new System.Windows.Forms.CheckBox();
          this.sortOrderComboBox = new System.Windows.Forms.ComboBox();
          this.label1 = new System.Windows.Forms.Label();
          this.chbGroupGoods = new System.Windows.Forms.CheckBox();
          this.auCheckBox = new System.Windows.Forms.CheckBox();
          this.typeComboBox = new System.Windows.Forms.ComboBox();
          this.label2 = new System.Windows.Forms.Label();
          this.znvlsCheckBox = new System.Windows.Forms.CheckBox();
          this.oaCheckBox = new System.Windows.Forms.CheckBox();
          this.panel1.SuspendLayout();
          this.gbType.SuspendLayout();
          this.SuspendLayout();
          // 
          // bOK
          // 
          this.bOK.Location = new System.Drawing.Point(419, 3);
          // 
          // bClose
          // 
          this.bClose.Location = new System.Drawing.Point(494, 3);
          // 
          // panel1
          // 
          this.panel1.Location = new System.Drawing.Point(0, 458);
          this.panel1.Size = new System.Drawing.Size(572, 29);
          // 
          // chbGroupGoodsClassifier
          // 
          this.chbGroupGoodsClassifier.AutoSize = true;
          this.chbGroupGoodsClassifier.Location = new System.Drawing.Point(10, 380);
          this.chbGroupGoodsClassifier.Name = "chbGroupGoodsClassifier";
          this.chbGroupGoodsClassifier.Size = new System.Drawing.Size(183, 17);
          this.chbGroupGoodsClassifier.TabIndex = 139;
          this.chbGroupGoodsClassifier.Text = "Сворачивать товар по группам";
          this.chbGroupGoodsClassifier.UseVisualStyleBackColor = true;
          // 
          // goods
          // 
          this.goods.AllowSaveState = true;
          this.goods.Caption = "Товары";
          this.goods.Location = new System.Drawing.Point(266, 252);
          this.goods.Mnemocode = "GOODS2";
          this.goods.Name = "goods";
          this.goods.Size = new System.Drawing.Size(298, 119);
          this.goods.TabIndex = 138;
          this.goods.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.goods_BeforePluginShow);
          // 
          // stores
          // 
          this.stores.AllowSaveState = true;
          this.stores.Caption = "Склады";
          this.stores.Location = new System.Drawing.Point(13, 146);
          this.stores.Mnemocode = "STORE";
          this.stores.Name = "stores";
          this.stores.Size = new System.Drawing.Size(245, 100);
          this.stores.TabIndex = 137;
          // 
          // comboSort
          // 
          this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboSort.FormattingEnabled = true;
          this.comboSort.Location = new System.Drawing.Point(132, 65);
          this.comboSort.Name = "comboSort";
          this.comboSort.Size = new System.Drawing.Size(219, 21);
          this.comboSort.TabIndex = 132;
          // 
          // labelSort
          // 
          this.labelSort.Location = new System.Drawing.Point(9, 64);
          this.labelSort.Name = "labelSort";
          this.labelSort.Size = new System.Drawing.Size(100, 21);
          this.labelSort.TabIndex = 131;
          this.labelSort.Text = "Сортировать по: ";
          this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labelPeriod
          // 
          this.labelPeriod.Location = new System.Drawing.Point(9, 12);
          this.labelPeriod.Name = "labelPeriod";
          this.labelPeriod.Size = new System.Drawing.Size(51, 21);
          this.labelPeriod.TabIndex = 124;
          this.labelPeriod.Text = "Период : ";
          this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // period
          // 
          this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
          this.period.Location = new System.Drawing.Point(106, 12);
          this.period.Name = "period";
          this.period.Size = new System.Drawing.Size(245, 21);
          this.period.TabIndex = 123;
          // 
          // gbType
          // 
          this.gbType.Controls.Add(this.rbCheckType);
          this.gbType.Controls.Add(this.rbAllType);
          this.gbType.Controls.Add(this.chbMovement);
          this.gbType.Controls.Add(this.chbOut);
          this.gbType.Controls.Add(this.chbKKM);
          this.gbType.Location = new System.Drawing.Point(357, 35);
          this.gbType.Name = "gbType";
          this.gbType.Size = new System.Drawing.Size(207, 104);
          this.gbType.TabIndex = 140;
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
          // comboRows
          // 
          this.comboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboRows.FormattingEnabled = true;
          this.comboRows.Location = new System.Drawing.Point(227, 119);
          this.comboRows.Name = "comboRows";
          this.comboRows.Size = new System.Drawing.Size(124, 21);
          this.comboRows.TabIndex = 134;
          // 
          // labelRows
          // 
          this.labelRows.Location = new System.Drawing.Point(9, 118);
          this.labelRows.Name = "labelRows";
          this.labelRows.Size = new System.Drawing.Size(172, 21);
          this.labelRows.TabIndex = 133;
          this.labelRows.Text = "Кол.позиций для отображения:";
          this.labelRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // ucTradeName
          // 
          this.ucTradeName.AllowSaveState = true;
          this.ucTradeName.Caption = "Торговая марка/наименование";
          this.ucTradeName.Location = new System.Drawing.Point(264, 146);
          this.ucTradeName.Mnemocode = "TRADE_NAME";
          this.ucTradeName.Name = "ucTradeName";
          this.ucTradeName.Size = new System.Drawing.Size(300, 100);
          this.ucTradeName.TabIndex = 142;
          // 
          // selectGoodsGroup
          // 
          this.selectGoodsGroup.Location = new System.Drawing.Point(13, 252);
          this.selectGoodsGroup.Name = "selectGoodsGroup";
          this.selectGoodsGroup.Size = new System.Drawing.Size(245, 124);
          this.selectGoodsGroup.TabIndex = 143;
          // 
          // chbGoodCode
          // 
          this.chbGoodCode.AutoSize = true;
          this.chbGoodCode.Location = new System.Drawing.Point(266, 403);
          this.chbGoodCode.Name = "chbGoodCode";
          this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
          this.chbGoodCode.TabIndex = 144;
          this.chbGoodCode.Text = "Отображать код товара ";
          this.chbGoodCode.UseVisualStyleBackColor = true;
          // 
          // sortOrderComboBox
          // 
          this.sortOrderComboBox.FormattingEnabled = true;
          this.sortOrderComboBox.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
          this.sortOrderComboBox.Location = new System.Drawing.Point(132, 92);
          this.sortOrderComboBox.Name = "sortOrderComboBox";
          this.sortOrderComboBox.Size = new System.Drawing.Size(219, 21);
          this.sortOrderComboBox.TabIndex = 145;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(9, 95);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(116, 13);
          this.label1.TabIndex = 146;
          this.label1.Text = "Порядок сортировки:";
          // 
          // chbGroupGoods
          // 
          this.chbGroupGoods.AutoSize = true;
          this.chbGroupGoods.Location = new System.Drawing.Point(266, 380);
          this.chbGroupGoods.Name = "chbGroupGoods";
          this.chbGroupGoods.Size = new System.Drawing.Size(215, 17);
          this.chbGroupGoods.TabIndex = 147;
          this.chbGroupGoods.Text = "Группировать(без учета поставщика)";
          this.chbGroupGoods.UseVisualStyleBackColor = true;
          // 
          // auCheckBox
          // 
          this.auCheckBox.AutoSize = true;
          this.auCheckBox.Location = new System.Drawing.Point(10, 403);
          this.auCheckBox.Name = "auCheckBox";
          this.auCheckBox.Size = new System.Drawing.Size(234, 17);
          this.auCheckBox.TabIndex = 148;
          this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
          this.auCheckBox.UseVisualStyleBackColor = true;
          // 
          // typeComboBox
          // 
          this.typeComboBox.FormattingEnabled = true;
          this.typeComboBox.Items.AddRange(new object[] {
            "В разрезе складов",
            "Стандартный"});
          this.typeComboBox.Location = new System.Drawing.Point(132, 39);
          this.typeComboBox.Name = "typeComboBox";
          this.typeComboBox.Size = new System.Drawing.Size(219, 21);
          this.typeComboBox.TabIndex = 149;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(9, 42);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(65, 13);
          this.label2.TabIndex = 150;
          this.label2.Text = "Вид отчёта:";
          // 
          // znvlsCheckBox
          // 
          this.znvlsCheckBox.AutoSize = true;
          this.znvlsCheckBox.Location = new System.Drawing.Point(10, 426);
          this.znvlsCheckBox.Name = "znvlsCheckBox";
          this.znvlsCheckBox.Size = new System.Drawing.Size(67, 17);
          this.znvlsCheckBox.TabIndex = 151;
          this.znvlsCheckBox.Text = "ЖНВЛС";
          this.znvlsCheckBox.UseVisualStyleBackColor = true;
          // 
          // oaCheckBox
          // 
          this.oaCheckBox.AutoSize = true;
          this.oaCheckBox.Location = new System.Drawing.Point(264, 426);
          this.oaCheckBox.Name = "oaCheckBox";
          this.oaCheckBox.Size = new System.Drawing.Size(41, 17);
          this.oaCheckBox.TabIndex = 152;
          this.oaCheckBox.Text = "ОА";
          this.oaCheckBox.UseVisualStyleBackColor = true;
          // 
          // FormParams
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(572, 487);
          this.Controls.Add(this.oaCheckBox);
          this.Controls.Add(this.znvlsCheckBox);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.typeComboBox);
          this.Controls.Add(this.auCheckBox);
          this.Controls.Add(this.chbGroupGoods);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.sortOrderComboBox);
          this.Controls.Add(this.chbGoodCode);
          this.Controls.Add(this.ucTradeName);
          this.Controls.Add(this.gbType);
          this.Controls.Add(this.comboSort);
          this.Controls.Add(this.goods);
          this.Controls.Add(this.chbGroupGoodsClassifier);
          this.Controls.Add(this.stores);
          this.Controls.Add(this.comboRows);
          this.Controls.Add(this.labelSort);
          this.Controls.Add(this.labelRows);
          this.Controls.Add(this.labelPeriod);
          this.Controls.Add(this.period);
          this.Controls.Add(this.selectGoodsGroup);
          this.Name = "FormParams";
          this.Load += new System.EventHandler(this.FormParams_Load);
          this.Shown += new System.EventHandler(this.FormParams_Shown);
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
          this.Controls.SetChildIndex(this.selectGoodsGroup, 0);
          this.Controls.SetChildIndex(this.period, 0);
          this.Controls.SetChildIndex(this.labelPeriod, 0);
          this.Controls.SetChildIndex(this.labelRows, 0);
          this.Controls.SetChildIndex(this.labelSort, 0);
          this.Controls.SetChildIndex(this.comboRows, 0);
          this.Controls.SetChildIndex(this.stores, 0);
          this.Controls.SetChildIndex(this.chbGroupGoodsClassifier, 0);
          this.Controls.SetChildIndex(this.goods, 0);
          this.Controls.SetChildIndex(this.comboSort, 0);
          this.Controls.SetChildIndex(this.gbType, 0);
          this.Controls.SetChildIndex(this.panel1, 0);
          this.Controls.SetChildIndex(this.ucTradeName, 0);
          this.Controls.SetChildIndex(this.chbGoodCode, 0);
          this.Controls.SetChildIndex(this.sortOrderComboBox, 0);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.chbGroupGoods, 0);
          this.Controls.SetChildIndex(this.auCheckBox, 0);
          this.Controls.SetChildIndex(this.typeComboBox, 0);
          this.Controls.SetChildIndex(this.label2, 0);
          this.Controls.SetChildIndex(this.znvlsCheckBox, 0);
          this.Controls.SetChildIndex(this.oaCheckBox, 0);
          this.panel1.ResumeLayout(false);
          this.gbType.ResumeLayout(false);
          this.gbType.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbGroupGoodsClassifier;
        public ePlus.MetaData.Client.UCPluginMultiSelect goods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public System.Windows.Forms.ComboBox comboSort;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.GroupBox gbType;
        public System.Windows.Forms.ComboBox comboRows;
        private System.Windows.Forms.Label labelRows;
        //public ePlus.Dictionary.Client.Goods.GoodsGroup.UserGroupControl ugcGroups;
        public ePlus.MetaData.Client.UCPluginMultiSelect ucTradeName;
        public ePlus.CommonEx.Controls.SelectGoodsGroup selectGoodsGroup;
        private System.Windows.Forms.CheckBox chbMovement;
        private System.Windows.Forms.CheckBox chbOut;
        private System.Windows.Forms.CheckBox chbKKM;
        private System.Windows.Forms.RadioButton rbAllType;
        private System.Windows.Forms.RadioButton rbCheckType;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.ComboBox sortOrderComboBox;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbGroupGoods;
		private System.Windows.Forms.CheckBox auCheckBox;
		private System.Windows.Forms.ComboBox typeComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox znvlsCheckBox;
		private System.Windows.Forms.CheckBox oaCheckBox;
    }
}