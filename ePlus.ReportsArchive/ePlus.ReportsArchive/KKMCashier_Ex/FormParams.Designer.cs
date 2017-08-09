namespace KKMCashier_Ex
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
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.goodsMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.checkGoods = new System.Windows.Forms.CheckBox();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.cashierMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.kkmMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.producerMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.contractorMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(530, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(605, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 431);
			this.panel1.Size = new System.Drawing.Size(683, 29);
			// 
			// period
			// 
			this.period.AutoSize = true;
			this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.period.Location = new System.Drawing.Point(63, 9);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(224, 23);
			this.period.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Период";
			// 
			// goodsMulti
			// 
			this.goodsMulti.AllowSaveState = false;
			this.goodsMulti.Caption = "Товары";
			this.goodsMulti.Location = new System.Drawing.Point(12, 250);
			this.goodsMulti.Mnemocode = "GOODS2";
			this.goodsMulti.Name = "goodsMulti";
			this.goodsMulti.Size = new System.Drawing.Size(654, 100);
			this.goodsMulti.TabIndex = 6;
			// 
			// checkGoods
			// 
			this.checkGoods.AutoSize = true;
			this.checkGoods.Location = new System.Drawing.Point(15, 370);
			this.checkGoods.Name = "checkGoods";
			this.checkGoods.Size = new System.Drawing.Size(155, 17);
			this.checkGoods.TabIndex = 7;
			this.checkGoods.Text = "Детализация по товарам";
			this.checkGoods.UseVisualStyleBackColor = true;
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(15, 393);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(120, 17);
			this.serviceCheckBox.TabIndex = 8;
			this.serviceCheckBox.Text = "Учитывать скидки";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// cashierMulti
			// 
			this.cashierMulti.AllowSaveState = false;
			this.cashierMulti.Caption = "Кассир";
			this.cashierMulti.Location = new System.Drawing.Point(12, 38);
			this.cashierMulti.Mnemocode = "CASH_REGISTER_USER";
			this.cashierMulti.Name = "cashierMulti";
			this.cashierMulti.Size = new System.Drawing.Size(324, 100);
			this.cashierMulti.TabIndex = 2;
			// 
			// kkmMulti
			// 
			this.kkmMulti.AllowSaveState = false;
			this.kkmMulti.Caption = "ККМ";
			this.kkmMulti.Location = new System.Drawing.Point(12, 144);
			this.kkmMulti.Mnemocode = "CASH_REGISTER";
			this.kkmMulti.Name = "kkmMulti";
			this.kkmMulti.Size = new System.Drawing.Size(324, 100);
			this.kkmMulti.TabIndex = 3;
			// 
			// producerMulti
			// 
			this.producerMulti.AllowSaveState = false;
			this.producerMulti.Caption = "Изготовитель";
			this.producerMulti.Location = new System.Drawing.Point(342, 38);
			this.producerMulti.Mnemocode = "PRODUCER";
			this.producerMulti.Name = "producerMulti";
			this.producerMulti.Size = new System.Drawing.Size(324, 100);
			this.producerMulti.TabIndex = 4;
			// 
			// contractorMulti
			// 
			this.contractorMulti.AllowSaveState = false;
			this.contractorMulti.Caption = "Поставщик";
			this.contractorMulti.Location = new System.Drawing.Point(342, 144);
			this.contractorMulti.Mnemocode = "CONTRACTOR";
			this.contractorMulti.Name = "contractorMulti";
			this.contractorMulti.Size = new System.Drawing.Size(324, 100);
			this.contractorMulti.TabIndex = 5;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(683, 460);
			this.Controls.Add(this.contractorMulti);
			this.Controls.Add(this.producerMulti);
			this.Controls.Add(this.kkmMulti);
			this.Controls.Add(this.cashierMulti);
			this.Controls.Add(this.checkGoods);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.goodsMulti);
			this.Controls.Add(this.period);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.goodsMulti, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.serviceCheckBox, 0);
			this.Controls.SetChildIndex(this.checkGoods, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.cashierMulti, 0);
			this.Controls.SetChildIndex(this.kkmMulti, 0);
			this.Controls.SetChildIndex(this.producerMulti, 0);
			this.Controls.SetChildIndex(this.contractorMulti, 0);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod period;
		private System.Windows.Forms.Label label1;
        public ePlus.MetaData.Client.UCPluginMultiSelect goodsMulti;
        private System.Windows.Forms.CheckBox checkGoods;
		private System.Windows.Forms.CheckBox serviceCheckBox;
		public ePlus.MetaData.Client.UCPluginMultiSelect cashierMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect kkmMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect producerMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect contractorMulti;
    }
}