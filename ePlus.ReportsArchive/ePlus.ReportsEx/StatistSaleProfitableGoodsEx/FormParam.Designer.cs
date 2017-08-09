namespace StatistSaleProfitableGoodsEx
{
    partial class FormParam
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
			this.comboSort = new System.Windows.Forms.ComboBox();
			this.comboRows = new System.Windows.Forms.ComboBox();
			this.labelSort = new System.Windows.Forms.Label();
			this.labelRows = new System.Windows.Forms.Label();
			this.labelPeriod = new System.Windows.Forms.Label();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.gbType = new System.Windows.Forms.GroupBox();
			this.rbCheckType = new System.Windows.Forms.RadioButton();
			this.rbAllType = new System.Windows.Forms.RadioButton();
			this.chbMovement = new System.Windows.Forms.CheckBox();
			this.chbOut = new System.Windows.Forms.CheckBox();
			this.chbKKM = new System.Windows.Forms.CheckBox();
			this.ucTradeName = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.goods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.chbGroupGoods = new System.Windows.Forms.CheckBox();
			this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.selectGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
			this.label1 = new System.Windows.Forms.Label();
			this.numericPercent = new System.Windows.Forms.NumericUpDown();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.sortOrderComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.gbType.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericPercent)).BeginInit();
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
			this.panel1.Location = new System.Drawing.Point(0, 403);
			this.panel1.Size = new System.Drawing.Size(554, 29);
			// 
			// comboSort
			// 
			this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSort.FormattingEnabled = true;
			this.comboSort.Location = new System.Drawing.Point(137, 39);
			this.comboSort.Name = "comboSort";
			this.comboSort.Size = new System.Drawing.Size(186, 21);
			this.comboSort.TabIndex = 138;
			// 
			// comboRows
			// 
			this.comboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRows.FormattingEnabled = true;
			this.comboRows.Location = new System.Drawing.Point(183, 119);
			this.comboRows.Name = "comboRows";
			this.comboRows.Size = new System.Drawing.Size(140, 21);
			this.comboRows.TabIndex = 140;
			this.comboRows.Visible = false;
			// 
			// labelSort
			// 
			this.labelSort.Location = new System.Drawing.Point(7, 38);
			this.labelSort.Name = "labelSort";
			this.labelSort.Size = new System.Drawing.Size(100, 21);
			this.labelSort.TabIndex = 137;
			this.labelSort.Text = "Сортировать по: ";
			this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelRows
			// 
			this.labelRows.Location = new System.Drawing.Point(7, 119);
			this.labelRows.Name = "labelRows";
			this.labelRows.Size = new System.Drawing.Size(172, 21);
			this.labelRows.TabIndex = 139;
			this.labelRows.Text = "Кол.позиций для отображения:";
			this.labelRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelRows.Visible = false;
			// 
			// labelPeriod
			// 
			this.labelPeriod.Location = new System.Drawing.Point(7, 12);
			this.labelPeriod.Name = "labelPeriod";
			this.labelPeriod.Size = new System.Drawing.Size(51, 21);
			this.labelPeriod.TabIndex = 136;
			this.labelPeriod.Text = "Период: ";
			this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.Location = new System.Drawing.Point(102, 12);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(254, 21);
			this.period.TabIndex = 135;
			// 
			// gbType
			// 
			this.gbType.Controls.Add(this.rbCheckType);
			this.gbType.Controls.Add(this.rbAllType);
			this.gbType.Controls.Add(this.chbMovement);
			this.gbType.Controls.Add(this.chbOut);
			this.gbType.Controls.Add(this.chbKKM);
			this.gbType.Location = new System.Drawing.Point(329, 39);
			this.gbType.Name = "gbType";
			this.gbType.Size = new System.Drawing.Size(207, 101);
			this.gbType.TabIndex = 141;
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
			this.chbMovement.CheckedChanged += new System.EventHandler(this.chbMovement_CheckedChanged);
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
			this.chbOut.CheckedChanged += new System.EventHandler(this.chbOut_CheckedChanged);
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
			this.chbKKM.CheckedChanged += new System.EventHandler(this.chbKKM_CheckedChanged);
			// 
			// ucTradeName
			// 
			this.ucTradeName.AllowSaveState = false;
			this.ucTradeName.Caption = "Торговая марка/наименование";
			this.ucTradeName.Location = new System.Drawing.Point(261, 146);
			this.ucTradeName.Mnemocode = "TRADE_NAME";
			this.ucTradeName.Name = "ucTradeName";
			this.ucTradeName.Size = new System.Drawing.Size(275, 100);
			this.ucTradeName.TabIndex = 147;
			// 
			// goods
			// 
			this.goods.AllowSaveState = false;
			this.goods.Caption = "Товары";
			this.goods.Location = new System.Drawing.Point(261, 245);
			this.goods.Mnemocode = "GOODS2";
			this.goods.Name = "goods";
			this.goods.Size = new System.Drawing.Size(275, 117);
			this.goods.TabIndex = 145;
			// 
			// chbGroupGoods
			// 
			this.chbGroupGoods.AutoSize = true;
			this.chbGroupGoods.Location = new System.Drawing.Point(17, 373);
			this.chbGroupGoods.Name = "chbGroupGoods";
			this.chbGroupGoods.Size = new System.Drawing.Size(183, 17);
			this.chbGroupGoods.TabIndex = 146;
			this.chbGroupGoods.Text = "Сворачивать товар по группам";
			this.chbGroupGoods.UseVisualStyleBackColor = true;
			// 
			// stores
			// 
			this.stores.AllowSaveState = false;
			this.stores.Caption = "Склады";
			this.stores.Location = new System.Drawing.Point(10, 146);
			this.stores.Mnemocode = "STORE";
			this.stores.Name = "stores";
			this.stores.Size = new System.Drawing.Size(245, 100);
			this.stores.TabIndex = 144;
			// 
			// selectGoodsGroup
			// 
			this.selectGoodsGroup.Location = new System.Drawing.Point(14, 245);
			this.selectGoodsGroup.Name = "selectGoodsGroup";
			this.selectGoodsGroup.Size = new System.Drawing.Size(241, 124);
			this.selectGoodsGroup.TabIndex = 148;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 149;
			this.label1.Text = "Процент продаж:";
			// 
			// numericPercent
			// 
			this.numericPercent.Location = new System.Drawing.Point(183, 93);
			this.numericPercent.Name = "numericPercent";
			this.numericPercent.Size = new System.Drawing.Size(140, 20);
			this.numericPercent.TabIndex = 150;
			this.numericPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericPercent.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(217, 375);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 165;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// sortOrderComboBox
			// 
			this.sortOrderComboBox.FormattingEnabled = true;
			this.sortOrderComboBox.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
			this.sortOrderComboBox.Location = new System.Drawing.Point(137, 66);
			this.sortOrderComboBox.Name = "sortOrderComboBox";
			this.sortOrderComboBox.Size = new System.Drawing.Size(186, 21);
			this.sortOrderComboBox.TabIndex = 166;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 13);
			this.label2.TabIndex = 167;
			this.label2.Text = "Порядок сортировки:";
			// 
			// FormParam
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(554, 432);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.sortOrderComboBox);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.label1);
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
			this.Controls.Add(this.numericPercent);
			this.Name = "FormParam";
			this.Load += new System.EventHandler(this.FormParam_Load);
			this.Shown += new System.EventHandler(this.FormParam_Shown);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParam_FormClosed);
			this.Controls.SetChildIndex(this.numericPercent, 0);
			this.Controls.SetChildIndex(this.labelRows, 0);
			this.Controls.SetChildIndex(this.labelPeriod, 0);
			this.Controls.SetChildIndex(this.labelSort, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.comboRows, 0);
			this.Controls.SetChildIndex(this.comboSort, 0);
			this.Controls.SetChildIndex(this.gbType, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.selectGoodsGroup, 0);
			this.Controls.SetChildIndex(this.stores, 0);
			this.Controls.SetChildIndex(this.chbGroupGoods, 0);
			this.Controls.SetChildIndex(this.goods, 0);
			this.Controls.SetChildIndex(this.ucTradeName, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.Controls.SetChildIndex(this.sortOrderComboBox, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.panel1.ResumeLayout(false);
			this.gbType.ResumeLayout(false);
			this.gbType.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericPercent)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboSort;
        public System.Windows.Forms.ComboBox comboRows;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.Label labelRows;
        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbCheckType;
        private System.Windows.Forms.RadioButton rbAllType;
        private System.Windows.Forms.CheckBox chbMovement;
        private System.Windows.Forms.CheckBox chbOut;
        private System.Windows.Forms.CheckBox chbKKM;
        public ePlus.MetaData.Client.UCPluginMultiSelect ucTradeName;
        public ePlus.MetaData.Client.UCPluginMultiSelect goods;
        private System.Windows.Forms.CheckBox chbGroupGoods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public ePlus.CommonEx.Controls.SelectGoodsGroup selectGoodsGroup;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericPercent;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.ComboBox sortOrderComboBox;
		private System.Windows.Forms.Label label2;
    }
}