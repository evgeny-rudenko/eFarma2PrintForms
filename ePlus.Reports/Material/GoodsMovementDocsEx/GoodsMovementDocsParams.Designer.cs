namespace GoodsMovementDocsEx
{
    partial class GoodsMovementParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsMovementParams));
			this.label1 = new System.Windows.Forms.Label();
			this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
			this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucSupplier = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucProducer = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbShortRep = new System.Windows.Forms.RadioButton();
			this.rbFullRep = new System.Windows.Forms.RadioButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.sortComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.auCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(467, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(542, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 352);
			this.panel1.Size = new System.Drawing.Size(620, 29);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Период";
			// 
			// ucPeriod1
			// 
			this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod1.Location = new System.Drawing.Point(62, 39);
			this.ucPeriod1.Name = "ucPeriod1";
			this.ucPeriod1.Size = new System.Drawing.Size(247, 21);
			this.ucPeriod1.TabIndex = 19;
			// 
			// ucContractor
			// 
			this.ucContractor.AllowSaveState = false;
			this.ucContractor.Caption = "Контрагент";
			this.ucContractor.Location = new System.Drawing.Point(12, 66);
			this.ucContractor.Mnemocode = "CONTRACTOR";
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.Size = new System.Drawing.Size(295, 74);
			this.ucContractor.TabIndex = 21;
			// 
			// ucStore
			// 
			this.ucStore.AllowSaveState = false;
			this.ucStore.Caption = "Склады";
			this.ucStore.Location = new System.Drawing.Point(12, 146);
			this.ucStore.Mnemocode = "STORE";
			this.ucStore.Name = "ucStore";
			this.ucStore.Size = new System.Drawing.Size(295, 74);
			this.ucStore.TabIndex = 22;
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "Товары";
			this.ucGoods.Location = new System.Drawing.Point(14, 226);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(295, 74);
			this.ucGoods.TabIndex = 23;
			// 
			// ucSupplier
			// 
			this.ucSupplier.AllowSaveState = false;
			this.ucSupplier.Caption = "Поставщик";
			this.ucSupplier.Location = new System.Drawing.Point(313, 146);
			this.ucSupplier.Mnemocode = "CONTRACTOR";
			this.ucSupplier.Name = "ucSupplier";
			this.ucSupplier.Size = new System.Drawing.Size(295, 74);
			this.ucSupplier.TabIndex = 24;
			// 
			// ucProducer
			// 
			this.ucProducer.AllowSaveState = false;
			this.ucProducer.Caption = "Производитель";
			this.ucProducer.Location = new System.Drawing.Point(315, 226);
			this.ucProducer.Mnemocode = "PRODUCER";
			this.ucProducer.Name = "ucProducer";
			this.ucProducer.Size = new System.Drawing.Size(295, 74);
			this.ucProducer.TabIndex = 25;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbShortRep);
			this.groupBox1.Controls.Add(this.rbFullRep);
			this.groupBox1.Location = new System.Drawing.Point(313, 66);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(293, 73);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Вид отчета:";
			// 
			// rbShortRep
			// 
			this.rbShortRep.AutoSize = true;
			this.rbShortRep.Location = new System.Drawing.Point(7, 44);
			this.rbShortRep.Name = "rbShortRep";
			this.rbShortRep.Size = new System.Drawing.Size(67, 17);
			this.rbShortRep.TabIndex = 1;
			this.rbShortRep.Text = "Краткий";
			this.rbShortRep.UseVisualStyleBackColor = true;
			// 
			// rbFullRep
			// 
			this.rbFullRep.AutoSize = true;
			this.rbFullRep.Checked = true;
			this.rbFullRep.Location = new System.Drawing.Point(7, 20);
			this.rbFullRep.Name = "rbFullRep";
			this.rbFullRep.Size = new System.Drawing.Size(195, 17);
			this.rbFullRep.TabIndex = 0;
			this.rbFullRep.TabStop = true;
			this.rbFullRep.Text = "Полный (детализ.по документам)";
			this.rbFullRep.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(620, 25);
			this.toolStrip1.TabIndex = 177;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			// 
			// sortComboBox
			// 
			this.sortComboBox.FormattingEnabled = true;
			this.sortComboBox.Items.AddRange(new object[] {
            "Дате",
            "Типу документа и дате"});
			this.sortComboBox.Location = new System.Drawing.Point(116, 312);
			this.sortComboBox.Name = "sortComboBox";
			this.sortComboBox.Size = new System.Drawing.Size(191, 21);
			this.sortComboBox.TabIndex = 178;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 315);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 13);
			this.label2.TabIndex = 179;
			this.label2.Text = "Сортировать по:";
			// 
			// auCheckBox
			// 
			this.auCheckBox.AutoSize = true;
			this.auCheckBox.Location = new System.Drawing.Point(315, 316);
			this.auCheckBox.Name = "auCheckBox";
			this.auCheckBox.Size = new System.Drawing.Size(234, 17);
			this.auCheckBox.TabIndex = 180;
			this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
			this.auCheckBox.UseVisualStyleBackColor = true;
			// 
			// GoodsMovementParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 381);
			this.Controls.Add(this.auCheckBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.sortComboBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucPeriod1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ucProducer);
			this.Controls.Add(this.ucSupplier);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.ucStore);
			this.Controls.Add(this.ucContractor);
			this.Controls.Add(this.label1);
			this.Name = "GoodsMovementParams";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ucContractor, 0);
			this.Controls.SetChildIndex(this.ucStore, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.ucSupplier, 0);
			this.Controls.SetChildIndex(this.ucProducer, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.ucPeriod1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.sortComboBox, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.auCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucSupplier;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucProducer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbShortRep;
        private System.Windows.Forms.RadioButton rbFullRep;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ComboBox sortComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox auCheckBox;
    }
}