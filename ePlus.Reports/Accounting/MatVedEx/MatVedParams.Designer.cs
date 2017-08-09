namespace MatVetEx
{
    partial class MatVedParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatVedParams));
			this.importantOnlyCheckBox = new System.Windows.Forms.CheckBox();
			this.showSeriesCheckBox = new System.Windows.Forms.CheckBox();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucGoodsKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucProducers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucSuppliers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.auCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(536, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(611, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 421);
			this.panel1.Size = new System.Drawing.Size(689, 29);
			// 
			// importantOnlyCheckBox
			// 
			this.importantOnlyCheckBox.AutoSize = true;
			this.importantOnlyCheckBox.Location = new System.Drawing.Point(156, 396);
			this.importantOnlyCheckBox.Name = "importantOnlyCheckBox";
			this.importantOnlyCheckBox.Size = new System.Drawing.Size(159, 17);
			this.importantOnlyCheckBox.TabIndex = 1;
			this.importantOnlyCheckBox.Text = "Только жизненно-важные";
			this.importantOnlyCheckBox.UseVisualStyleBackColor = true;
			// 
			// showSeriesCheckBox
			// 
			this.showSeriesCheckBox.AutoSize = true;
			this.showSeriesCheckBox.Location = new System.Drawing.Point(21, 396);
			this.showSeriesCheckBox.Name = "showSeriesCheckBox";
			this.showSeriesCheckBox.Size = new System.Drawing.Size(109, 17);
			this.showSeriesCheckBox.TabIndex = 0;
			this.showSeriesCheckBox.Text = "Выводить серии";
			this.showSeriesCheckBox.UseVisualStyleBackColor = true;
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "Товары:";
			this.ucGoods.Location = new System.Drawing.Point(12, 279);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(664, 98);
			this.ucGoods.TabIndex = 123;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.period.Location = new System.Drawing.Point(64, 37);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(229, 21);
			this.period.TabIndex = 120;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 119;
			this.label1.Text = "Период:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(689, 25);
			this.toolStrip1.TabIndex = 125;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// ucGoodsKind
			// 
			this.ucGoodsKind.AllowSaveState = false;
			this.ucGoodsKind.Caption = "Виды товаров:";
			this.ucGoodsKind.Location = new System.Drawing.Point(347, 175);
			this.ucGoodsKind.Mnemocode = "GOODS_KIND";
			this.ucGoodsKind.Name = "ucGoodsKind";
			this.ucGoodsKind.Size = new System.Drawing.Size(329, 98);
			this.ucGoodsKind.TabIndex = 126;
			// 
			// ucProducers
			// 
			this.ucProducers.AllowSaveState = false;
			this.ucProducers.Caption = "Производители:";
			this.ucProducers.Location = new System.Drawing.Point(347, 71);
			this.ucProducers.Mnemocode = "PRODUCER";
			this.ucProducers.Name = "ucProducers";
			this.ucProducers.Size = new System.Drawing.Size(329, 98);
			this.ucProducers.TabIndex = 127;
			// 
			// ucSuppliers
			// 
			this.ucSuppliers.AllowSaveState = false;
			this.ucSuppliers.Caption = "Поставщики:";
			this.ucSuppliers.Location = new System.Drawing.Point(12, 71);
			this.ucSuppliers.Mnemocode = "CONTRACTOR";
			this.ucSuppliers.Name = "ucSuppliers";
			this.ucSuppliers.Size = new System.Drawing.Size(329, 98);
			this.ucSuppliers.TabIndex = 128;
			// 
			// ucStores
			// 
			this.ucStores.AllowSaveState = false;
			this.ucStores.Caption = "Склады:";
			this.ucStores.Location = new System.Drawing.Point(12, 175);
			this.ucStores.Mnemocode = "STORE";
			this.ucStores.Name = "ucStores";
			this.ucStores.Size = new System.Drawing.Size(329, 98);
			this.ucStores.TabIndex = 129;
			// 
			// auCheckBox
			// 
			this.auCheckBox.AutoSize = true;
			this.auCheckBox.Location = new System.Drawing.Point(347, 396);
			this.auCheckBox.Name = "auCheckBox";
			this.auCheckBox.Size = new System.Drawing.Size(234, 17);
			this.auCheckBox.TabIndex = 130;
			this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
			this.auCheckBox.UseVisualStyleBackColor = true;
			// 
			// MatVedParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(689, 450);
			this.Controls.Add(this.auCheckBox);
			this.Controls.Add(this.ucStores);
			this.Controls.Add(this.ucSuppliers);
			this.Controls.Add(this.ucProducers);
			this.Controls.Add(this.ucGoodsKind);
			this.Controls.Add(this.importantOnlyCheckBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.showSeriesCheckBox);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.period);
			this.Controls.Add(this.label1);
			this.Name = "MatVedParams";
			this.Load += new System.EventHandler(this.MatVedParams_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.showSeriesCheckBox, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.importantOnlyCheckBox, 0);
			this.Controls.SetChildIndex(this.ucGoodsKind, 0);
			this.Controls.SetChildIndex(this.ucProducers, 0);
			this.Controls.SetChildIndex(this.ucSuppliers, 0);
			this.Controls.SetChildIndex(this.ucStores, 0);
			this.Controls.SetChildIndex(this.auCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.CheckBox importantOnlyCheckBox;
        private System.Windows.Forms.CheckBox showSeriesCheckBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsKind;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucProducers;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucSuppliers;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.CheckBox auCheckBox;
    }
}