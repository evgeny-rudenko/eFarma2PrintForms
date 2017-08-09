namespace StatistSaleProfitableGoodsEx
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
            this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboSort = new System.Windows.Forms.ComboBox();
            this.labelSort = new System.Windows.Forms.Label();
            this.numericPercent = new System.Windows.Forms.NumericUpDown();
            this.labelPercent = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(190, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(265, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 289);
            this.panel1.Size = new System.Drawing.Size(343, 29);
            // 
            // chbGroupGoods
            // 
            this.chbGroupGoods.AutoSize = true;
            this.chbGroupGoods.Location = new System.Drawing.Point(13, 264);
            this.chbGroupGoods.Name = "chbGroupGoods";
            this.chbGroupGoods.Size = new System.Drawing.Size(183, 17);
            this.chbGroupGoods.TabIndex = 139;
            this.chbGroupGoods.Text = "Сворачивать товар по группам";
            this.chbGroupGoods.UseVisualStyleBackColor = true;
            // 
            // stores
            // 
            this.stores.AllowSaveState = false;
            this.stores.Caption = "Склады";
            this.stores.Location = new System.Drawing.Point(9, 157);
            this.stores.Mnemocode = "STORE";
            this.stores.Name = "stores";
            this.stores.Size = new System.Drawing.Size(305, 100);
            this.stores.TabIndex = 137;
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(104, 125);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(210, 21);
            this.comboType.TabIndex = 136;
            // 
            // labelType
            // 
            this.labelType.Location = new System.Drawing.Point(9, 125);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(80, 21);
            this.labelType.TabIndex = 135;
            this.labelType.Text = "Вид расхода : ";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboSort
            // 
            this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSort.FormattingEnabled = true;
            this.comboSort.Location = new System.Drawing.Point(104, 85);
            this.comboSort.Name = "comboSort";
            this.comboSort.Size = new System.Drawing.Size(210, 21);
            this.comboSort.TabIndex = 132;
            // 
            // labelSort
            // 
            this.labelSort.Location = new System.Drawing.Point(9, 85);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(100, 21);
            this.labelSort.TabIndex = 131;
            this.labelSort.Text = "Сортировать по : ";
            this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericPercent
            // 
            this.numericPercent.Location = new System.Drawing.Point(104, 48);
            this.numericPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPercent.Name = "numericPercent";
            this.numericPercent.Size = new System.Drawing.Size(80, 20);
            this.numericPercent.TabIndex = 130;
            this.numericPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(4, 48);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(100, 21);
            this.labelPercent.TabIndex = 129;
            this.labelPercent.Text = "Процент продаж : ";
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.period.Location = new System.Drawing.Point(104, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(195, 21);
            this.period.TabIndex = 123;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(343, 318);
            this.Controls.Add(this.chbGroupGoods);
            this.Controls.Add(this.stores);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboSort);
            this.Controls.Add(this.labelSort);
            this.Controls.Add(this.numericPercent);
            this.Controls.Add(this.labelPercent);
            this.Controls.Add(this.labelPeriod);
            this.Controls.Add(this.period);
            this.Name = "FormParams";
            this.Shown += new System.EventHandler(this.FormParams_Shown);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.labelPeriod, 0);
            this.Controls.SetChildIndex(this.labelPercent, 0);
            this.Controls.SetChildIndex(this.numericPercent, 0);
            this.Controls.SetChildIndex(this.labelSort, 0);
            this.Controls.SetChildIndex(this.comboSort, 0);
            this.Controls.SetChildIndex(this.labelType, 0);
            this.Controls.SetChildIndex(this.comboType, 0);
            this.Controls.SetChildIndex(this.stores, 0);
            this.Controls.SetChildIndex(this.chbGroupGoods, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbGroupGoods;
        public ePlus.MetaData.Client.UCPluginMultiSelect stores;
        public System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label labelType;
        public System.Windows.Forms.ComboBox comboSort;
        private System.Windows.Forms.Label labelSort;
        public System.Windows.Forms.NumericUpDown numericPercent;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
    }
}