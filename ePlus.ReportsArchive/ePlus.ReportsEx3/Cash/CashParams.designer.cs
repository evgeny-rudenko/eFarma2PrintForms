namespace Cash
{
    partial class CashParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashParams));
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.goodsMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.detailCheckBox = new System.Windows.Forms.CheckBox();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.cashierMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.kkmMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.producerMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.contractorMulti = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(546, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(621, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 466);
			this.panel1.Size = new System.Drawing.Size(699, 29);
			// 
			// ucPeriod
			// 
			this.ucPeriod.AutoSize = true;
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(63, 34);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(224, 23);
			this.ucPeriod.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Период";
			// 
			// goodsMulti
			// 
			this.goodsMulti.AllowSaveState = true;
			this.goodsMulti.Caption = "Товары";
			this.goodsMulti.Location = new System.Drawing.Point(12, 275);
			this.goodsMulti.Mnemocode = "GOODS2";
			this.goodsMulti.Name = "goodsMulti";
			this.goodsMulti.Size = new System.Drawing.Size(654, 100);
			this.goodsMulti.TabIndex = 6;
			// 
			// detailCheckBox
			// 
			this.detailCheckBox.AutoSize = true;
			this.detailCheckBox.Location = new System.Drawing.Point(15, 395);
			this.detailCheckBox.Name = "detailCheckBox";
			this.detailCheckBox.Size = new System.Drawing.Size(155, 17);
			this.detailCheckBox.TabIndex = 7;
			this.detailCheckBox.Text = "Детализация по товарам";
			this.detailCheckBox.UseVisualStyleBackColor = true;
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(15, 418);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
			this.serviceCheckBox.TabIndex = 8;
			this.serviceCheckBox.Text = "Учитывать услуги";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// cashierMulti
			// 
			this.cashierMulti.AllowSaveState = true;
			this.cashierMulti.Caption = "Кассир";
			this.cashierMulti.Location = new System.Drawing.Point(12, 63);
			this.cashierMulti.Mnemocode = "CASH_REGISTER_USER";
			this.cashierMulti.Name = "cashierMulti";
			this.cashierMulti.Size = new System.Drawing.Size(324, 100);
			this.cashierMulti.TabIndex = 2;
			// 
			// kkmMulti
			// 
			this.kkmMulti.AllowSaveState = true;
			this.kkmMulti.Caption = "ККМ";
			this.kkmMulti.Location = new System.Drawing.Point(12, 169);
			this.kkmMulti.Mnemocode = "CASH_REGISTER";
			this.kkmMulti.Name = "kkmMulti";
			this.kkmMulti.Size = new System.Drawing.Size(324, 100);
			this.kkmMulti.TabIndex = 3;
			// 
			// producerMulti
			// 
			this.producerMulti.AllowSaveState = true;
			this.producerMulti.Caption = "Изготовитель";
			this.producerMulti.Location = new System.Drawing.Point(342, 63);
			this.producerMulti.Mnemocode = "PRODUCER";
			this.producerMulti.Name = "producerMulti";
			this.producerMulti.Size = new System.Drawing.Size(324, 100);
			this.producerMulti.TabIndex = 4;
			// 
			// contractorMulti
			// 
			this.contractorMulti.AllowSaveState = true;
			this.contractorMulti.Caption = "Поставщик";
			this.contractorMulti.Location = new System.Drawing.Point(342, 169);
			this.contractorMulti.Mnemocode = "CONTRACTOR";
			this.contractorMulti.Name = "contractorMulti";
			this.contractorMulti.Size = new System.Drawing.Size(324, 100);
			this.contractorMulti.TabIndex = 5;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(699, 25);
			this.toolStrip1.TabIndex = 22;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(176, 395);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 125;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// CashParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(699, 495);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.contractorMulti);
			this.Controls.Add(this.producerMulti);
			this.Controls.Add(this.kkmMulti);
			this.Controls.Add(this.cashierMulti);
			this.Controls.Add(this.detailCheckBox);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.goodsMulti);
			this.Controls.Add(this.ucPeriod);
			this.Name = "CashParams";
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.goodsMulti, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.serviceCheckBox, 0);
			this.Controls.SetChildIndex(this.detailCheckBox, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.cashierMulti, 0);
			this.Controls.SetChildIndex(this.kkmMulti, 0);
			this.Controls.SetChildIndex(this.producerMulti, 0);
			this.Controls.SetChildIndex(this.contractorMulti, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
        public ePlus.MetaData.Client.UCPluginMultiSelect goodsMulti;
        private System.Windows.Forms.CheckBox detailCheckBox;
		private System.Windows.Forms.CheckBox serviceCheckBox;
		public ePlus.MetaData.Client.UCPluginMultiSelect cashierMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect kkmMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect producerMulti;
		public ePlus.MetaData.Client.UCPluginMultiSelect contractorMulti;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.CheckBox chbGoodCode;
    }
}