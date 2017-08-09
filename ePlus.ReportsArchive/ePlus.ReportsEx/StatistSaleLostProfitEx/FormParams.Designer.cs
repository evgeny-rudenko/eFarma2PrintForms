namespace StatistSaleLostProfitEx
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
			this.chbGroupGoods = new System.Windows.Forms.CheckBox();
			this.gbType = new System.Windows.Forms.GroupBox();
			this.rbCheckType = new System.Windows.Forms.RadioButton();
			this.rbAllType = new System.Windows.Forms.RadioButton();
			this.chbMovement = new System.Windows.Forms.CheckBox();
			this.chbOut = new System.Windows.Forms.CheckBox();
			this.chbKKM = new System.Windows.Forms.CheckBox();
			this.comboSort = new System.Windows.Forms.ComboBox();
			this.labelSort = new System.Windows.Forms.Label();
			this.labelPeriod = new System.Windows.Forms.Label();
			this.ucTradeName = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.goods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.selectGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.sortOrderComboBox = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.gbType.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(422, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(497, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 367);
			this.panel1.Size = new System.Drawing.Size(575, 29);
			// 
			// chbGroupGoods
			// 
			this.chbGroupGoods.AutoSize = true;
			this.chbGroupGoods.Location = new System.Drawing.Point(16, 334);
			this.chbGroupGoods.Name = "chbGroupGoods";
			this.chbGroupGoods.Size = new System.Drawing.Size(183, 17);
			this.chbGroupGoods.TabIndex = 157;
			this.chbGroupGoods.Text = "Сворачивать товар по группам";
			this.chbGroupGoods.UseVisualStyleBackColor = true;
			// 
			// gbType
			// 
			this.gbType.Controls.Add(this.rbCheckType);
			this.gbType.Controls.Add(this.rbAllType);
			this.gbType.Controls.Add(this.chbMovement);
			this.gbType.Controls.Add(this.chbOut);
			this.gbType.Controls.Add(this.chbKKM);
			this.gbType.Location = new System.Drawing.Point(348, 0);
			this.gbType.Name = "gbType";
			this.gbType.Size = new System.Drawing.Size(207, 101);
			this.gbType.TabIndex = 156;
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
			// comboSort
			// 
			this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSort.FormattingEnabled = true;
			this.comboSort.Location = new System.Drawing.Point(137, 33);
			this.comboSort.Name = "comboSort";
			this.comboSort.Size = new System.Drawing.Size(205, 21);
			this.comboSort.TabIndex = 153;
			// 
			// labelSort
			// 
			this.labelSort.Location = new System.Drawing.Point(12, 32);
			this.labelSort.Name = "labelSort";
			this.labelSort.Size = new System.Drawing.Size(100, 21);
			this.labelSort.TabIndex = 152;
			this.labelSort.Text = "Сортировать по : ";
			this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPeriod
			// 
			this.labelPeriod.Location = new System.Drawing.Point(12, 6);
			this.labelPeriod.Name = "labelPeriod";
			this.labelPeriod.Size = new System.Drawing.Size(51, 21);
			this.labelPeriod.TabIndex = 151;
			this.labelPeriod.Text = "Период : ";
			this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ucTradeName
			// 
			this.ucTradeName.AllowSaveState = false;
			this.ucTradeName.Caption = "Торговая марка/наименование";
			this.ucTradeName.Location = new System.Drawing.Point(235, 107);
			this.ucTradeName.Mnemocode = "TRADE_NAME";
			this.ucTradeName.Name = "ucTradeName";
			this.ucTradeName.Size = new System.Drawing.Size(320, 100);
			this.ucTradeName.TabIndex = 162;
			// 
			// goods
			// 
			this.goods.AllowSaveState = false;
			this.goods.Caption = "Товары";
			this.goods.Location = new System.Drawing.Point(235, 206);
			this.goods.Mnemocode = "GOODS2";
			this.goods.Name = "goods";
			this.goods.Size = new System.Drawing.Size(320, 117);
			this.goods.TabIndex = 161;
			this.goods.Visible = false;
			// 
			// stores
			// 
			this.stores.AllowSaveState = false;
			this.stores.Caption = "Склады";
			this.stores.Location = new System.Drawing.Point(15, 107);
			this.stores.Mnemocode = "STORE";
			this.stores.Name = "stores";
			this.stores.Size = new System.Drawing.Size(214, 100);
			this.stores.TabIndex = 160;
			// 
			// selectGoodsGroup
			// 
			this.selectGoodsGroup.Location = new System.Drawing.Point(18, 206);
			this.selectGoodsGroup.Name = "selectGoodsGroup";
			this.selectGoodsGroup.Size = new System.Drawing.Size(211, 124);
			this.selectGoodsGroup.TabIndex = 163;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.Location = new System.Drawing.Point(79, 6);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(233, 21);
			this.period.TabIndex = 124;
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(205, 336);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 164;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 13);
			this.label1.TabIndex = 166;
			this.label1.Text = "Порядок сортировки:";
			// 
			// sortOrderComboBox
			// 
			this.sortOrderComboBox.FormattingEnabled = true;
			this.sortOrderComboBox.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
			this.sortOrderComboBox.Location = new System.Drawing.Point(137, 62);
			this.sortOrderComboBox.Name = "sortOrderComboBox";
			this.sortOrderComboBox.Size = new System.Drawing.Size(205, 21);
			this.sortOrderComboBox.TabIndex = 165;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(575, 396);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.sortOrderComboBox);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.period);
			this.Controls.Add(this.ucTradeName);
			this.Controls.Add(this.goods);
			this.Controls.Add(this.stores);
			this.Controls.Add(this.selectGoodsGroup);
			this.Controls.Add(this.chbGroupGoods);
			this.Controls.Add(this.gbType);
			this.Controls.Add(this.comboSort);
			this.Controls.Add(this.labelSort);
			this.Controls.Add(this.labelPeriod);
			this.Name = "FormParams";
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.Shown += new System.EventHandler(this.FormParams_Shown);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
			this.Controls.SetChildIndex(this.labelPeriod, 0);
			this.Controls.SetChildIndex(this.labelSort, 0);
			this.Controls.SetChildIndex(this.comboSort, 0);
			this.Controls.SetChildIndex(this.gbType, 0);
			this.Controls.SetChildIndex(this.chbGroupGoods, 0);
			this.Controls.SetChildIndex(this.selectGoodsGroup, 0);
			this.Controls.SetChildIndex(this.stores, 0);
			this.Controls.SetChildIndex(this.goods, 0);
			this.Controls.SetChildIndex(this.ucTradeName, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.Controls.SetChildIndex(this.sortOrderComboBox, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.panel1.ResumeLayout(false);
			this.gbType.ResumeLayout(false);
			this.gbType.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbGroupGoods;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbCheckType;
        private System.Windows.Forms.RadioButton rbAllType;
        private System.Windows.Forms.CheckBox chbMovement;
        private System.Windows.Forms.CheckBox chbOut;
        private System.Windows.Forms.CheckBox chbKKM;
        public System.Windows.Forms.ComboBox comboSort;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPluginMultiSelect ucTradeName;
        public ePlus.MetaData.Client.UCPluginMultiSelect goods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public ePlus.CommonEx.Controls.SelectGoodsGroup selectGoodsGroup;
        public ePlus.MetaData.Client.UCPeriod period;
		private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox sortOrderComboBox;
    }
}