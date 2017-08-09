namespace StatistSaleEsEx
{
    partial class StatistEsForm
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
            this.chkGoodsGroup = new System.Windows.Forms.CheckBox();
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(371, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(446, 3);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkGoodsGroup);
            this.panel1.Location = new System.Drawing.Point(0, 283);
            this.panel1.Size = new System.Drawing.Size(524, 29);
            this.panel1.Controls.SetChildIndex(this.bClose, 0);
            this.panel1.Controls.SetChildIndex(this.chkGoodsGroup, 0);
            this.panel1.Controls.SetChildIndex(this.bOK, 0);
            // 
            // chkGoodsGroup
            // 
            this.chkGoodsGroup.AutoSize = true;
            this.chkGoodsGroup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGoodsGroup.Location = new System.Drawing.Point(21, 7);
            this.chkGoodsGroup.Name = "chkGoodsGroup";
            this.chkGoodsGroup.Size = new System.Drawing.Size(191, 17);
            this.chkGoodsGroup.TabIndex = 129;
            this.chkGoodsGroup.Text = "Сворачивать товары по группам";
            this.chkGoodsGroup.UseVisualStyleBackColor = true;
            // 
            // goods
            // 
            this.goods.AllowSaveState = false;
            this.goods.Caption = "Товары";
            this.goods.Location = new System.Drawing.Point(198, 181);
            this.goods.Mnemocode = "ES_EF2";
            this.goods.Name = "goods";
            this.goods.Size = new System.Drawing.Size(322, 100);
            this.goods.TabIndex = 128;
            // 
            // stores
            // 
            this.stores.AllowSaveState = false;
            this.stores.Caption = "Склады";
            this.stores.Location = new System.Drawing.Point(18, 181);
            this.stores.Mnemocode = "STORE";
            this.stores.Name = "stores";
            this.stores.Size = new System.Drawing.Size(180, 100);
            this.stores.TabIndex = 127;
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(113, 141);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(210, 21);
            this.comboType.TabIndex = 126;
            // 
            // labelType
            // 
            this.labelType.Location = new System.Drawing.Point(18, 141);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(80, 21);
            this.labelType.TabIndex = 125;
            this.labelType.Text = "Вид расхода : ";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboRows
            // 
            this.comboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRows.FormattingEnabled = true;
            this.comboRows.Location = new System.Drawing.Point(438, 141);
            this.comboRows.Name = "comboRows";
            this.comboRows.Size = new System.Drawing.Size(80, 21);
            this.comboRows.TabIndex = 124;
            // 
            // labelRows
            // 
            this.labelRows.Location = new System.Drawing.Point(338, 141);
            this.labelRows.Name = "labelRows";
            this.labelRows.Size = new System.Drawing.Size(110, 21);
            this.labelRows.TabIndex = 123;
            this.labelRows.Text = "Вывести позиции : ";
            this.labelRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboSort
            // 
            this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSort.FormattingEnabled = true;
            this.comboSort.Location = new System.Drawing.Point(113, 101);
            this.comboSort.Name = "comboSort";
            this.comboSort.Size = new System.Drawing.Size(405, 21);
            this.comboSort.TabIndex = 122;
            // 
            // labelSort
            // 
            this.labelSort.Location = new System.Drawing.Point(18, 101);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(100, 21);
            this.labelSort.TabIndex = 121;
            this.labelSort.Text = "Сортировать по : ";
            this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericPercent
            // 
            this.numericPercent.Location = new System.Drawing.Point(438, 61);
            this.numericPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPercent.Name = "numericPercent";
            this.numericPercent.Size = new System.Drawing.Size(80, 20);
            this.numericPercent.TabIndex = 120;
            this.numericPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(338, 61);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(100, 21);
            this.labelPercent.TabIndex = 119;
            this.labelPercent.Text = "Процент продаж : ";
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboReport
            // 
            this.comboReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReport.FormattingEnabled = true;
            this.comboReport.Location = new System.Drawing.Point(113, 61);
            this.comboReport.Name = "comboReport";
            this.comboReport.Size = new System.Drawing.Size(210, 21);
            this.comboReport.TabIndex = 118;
            this.comboReport.SelectedIndexChanged += new System.EventHandler(this.comboReport_SelectedIndexChanged);
            // 
            // labelReport
            // 
            this.labelReport.Location = new System.Drawing.Point(18, 61);
            this.labelReport.Name = "labelReport";
            this.labelReport.Size = new System.Drawing.Size(70, 21);
            this.labelReport.TabIndex = 117;
            this.labelReport.Text = "Вид отчета : ";
            this.labelReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkParts
            // 
            this.checkParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkParts.Location = new System.Drawing.Point(347, 25);
            this.checkParts.Name = "checkParts";
            this.checkParts.Size = new System.Drawing.Size(11, 12);
            this.checkParts.TabIndex = 115;
            this.checkParts.UseVisualStyleBackColor = true;
            // 
            // labelPeriod
            // 
            this.labelPeriod.Location = new System.Drawing.Point(18, 21);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(51, 21);
            this.labelPeriod.TabIndex = 114;
            this.labelPeriod.Text = "Период : ";
            this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.Location = new System.Drawing.Point(113, 21);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(228, 21);
            this.period.TabIndex = 113;
            // 
            // labelParts
            // 
            this.labelParts.Location = new System.Drawing.Point(364, 9);
            this.labelParts.Name = "labelParts";
            this.labelParts.Size = new System.Drawing.Size(154, 49);
            this.labelParts.TabIndex = 116;
            this.labelParts.Text = "Отображать разукомплектованные товары отдельно";
            // 
            // StatistEsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 312);
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
            this.Controls.Add(this.labelPeriod);
            this.Controls.Add(this.period);
            this.Controls.Add(this.checkParts);
            this.Controls.Add(this.labelParts);
            this.Name = "StatistEsForm";
            this.Controls.SetChildIndex(this.labelParts, 0);
            this.Controls.SetChildIndex(this.checkParts, 0);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.labelPeriod, 0);
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
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGoodsGroup;
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
    }
}