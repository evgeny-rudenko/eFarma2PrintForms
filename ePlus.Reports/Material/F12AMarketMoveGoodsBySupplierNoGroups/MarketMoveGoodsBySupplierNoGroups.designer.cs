namespace F12AMarketMoveGoodsBySupplierNoGroups
{
    partial class F12AMarketMoveGoodsBySupplierParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F12AMarketMoveGoodsBySupplierParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucGoodsGroup = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbGoodsGroupUnchecked = new System.Windows.Forms.RadioButton();
            this.rbGoodsGroupChecked = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbGoodsUnchecked = new System.Windows.Forms.RadioButton();
            this.rbGoodsChecked = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbStoreUnchecked = new System.Windows.Forms.RadioButton();
            this.rbStoreChecked = new System.Windows.Forms.RadioButton();
            this.btnDescription = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ucSupplier = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbSupplierUnchecked = new System.Windows.Forms.RadioButton();
            this.rbSupplierChecked = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chklbTypeIn = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chklbTypeOut = new System.Windows.Forms.CheckedListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkbShowEmptyGroup = new System.Windows.Forms.CheckBox();
            this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.chkbShowProd = new System.Windows.Forms.CheckBox();
            this.chkbShowGroupSum = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(473, 3);
            this.bOK.Size = new System.Drawing.Size(75, 26);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(548, 3);
            this.bClose.Size = new System.Drawing.Size(75, 26);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDescription);
            this.panel1.Location = new System.Drawing.Point(0, 501);
            this.panel1.Size = new System.Drawing.Size(626, 32);
            this.panel1.Controls.SetChildIndex(this.bClose, 0);
            this.panel1.Controls.SetChildIndex(this.bOK, 0);
            this.panel1.Controls.SetChildIndex(this.btnDescription, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(626, 25);
            this.toolStrip1.TabIndex = 176;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucGoodsGroup);
            this.groupBox2.Controls.Add(this.rbGoodsGroupUnchecked);
            this.groupBox2.Controls.Add(this.rbGoodsGroupChecked);
            this.groupBox2.Location = new System.Drawing.Point(12, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 128);
            this.groupBox2.TabIndex = 195;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Группы товаров";
            // 
            // ucGoodsGroup
            // 
            this.ucGoodsGroup.AllowSaveState = false;
            this.ucGoodsGroup.Caption = "Группы товаров";
            this.ucGoodsGroup.Location = new System.Drawing.Point(0, 42);
            this.ucGoodsGroup.Mnemocode = "GOODS_GROUP";
            this.ucGoodsGroup.Name = "ucGoodsGroup";
            this.ucGoodsGroup.Pinnable = false;
            this.ucGoodsGroup.Size = new System.Drawing.Size(295, 80);
            this.ucGoodsGroup.TabIndex = 195;
            // 
            // rbGoodsGroupUnchecked
            // 
            this.rbGoodsGroupUnchecked.AutoSize = true;
            this.rbGoodsGroupUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbGoodsGroupUnchecked.Name = "rbGoodsGroupUnchecked";
            this.rbGoodsGroupUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbGoodsGroupUnchecked.TabIndex = 2;
            this.rbGoodsGroupUnchecked.Text = "Кроме выбранных";
            this.rbGoodsGroupUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbGoodsGroupChecked
            // 
            this.rbGoodsGroupChecked.AutoSize = true;
            this.rbGoodsGroupChecked.Checked = true;
            this.rbGoodsGroupChecked.Location = new System.Drawing.Point(6, 19);
            this.rbGoodsGroupChecked.Name = "rbGoodsGroupChecked";
            this.rbGoodsGroupChecked.Size = new System.Drawing.Size(84, 17);
            this.rbGoodsGroupChecked.TabIndex = 0;
            this.rbGoodsGroupChecked.TabStop = true;
            this.rbGoodsGroupChecked.Text = "Выбранные";
            this.rbGoodsGroupChecked.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ucGoods);
            this.groupBox5.Controls.Add(this.rbGoodsUnchecked);
            this.groupBox5.Controls.Add(this.rbGoodsChecked);
            this.groupBox5.Location = new System.Drawing.Point(313, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(295, 128);
            this.groupBox5.TabIndex = 198;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Товары";
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(0, 42);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(295, 80);
            this.ucGoods.TabIndex = 195;
            // 
            // rbGoodsUnchecked
            // 
            this.rbGoodsUnchecked.AutoSize = true;
            this.rbGoodsUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbGoodsUnchecked.Name = "rbGoodsUnchecked";
            this.rbGoodsUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbGoodsUnchecked.TabIndex = 2;
            this.rbGoodsUnchecked.Text = "Кроме выбранных";
            this.rbGoodsUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbGoodsChecked
            // 
            this.rbGoodsChecked.AutoSize = true;
            this.rbGoodsChecked.Checked = true;
            this.rbGoodsChecked.Location = new System.Drawing.Point(6, 19);
            this.rbGoodsChecked.Name = "rbGoodsChecked";
            this.rbGoodsChecked.Size = new System.Drawing.Size(84, 17);
            this.rbGoodsChecked.TabIndex = 0;
            this.rbGoodsChecked.TabStop = true;
            this.rbGoodsChecked.Text = "Выбранные";
            this.rbGoodsChecked.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ucStore);
            this.groupBox6.Controls.Add(this.rbStoreUnchecked);
            this.groupBox6.Controls.Add(this.rbStoreChecked);
            this.groupBox6.Location = new System.Drawing.Point(12, 358);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(295, 128);
            this.groupBox6.TabIndex = 202;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Склады";
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склады";
            this.ucStore.Location = new System.Drawing.Point(0, 42);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Pinnable = false;
            this.ucStore.Size = new System.Drawing.Size(295, 80);
            this.ucStore.TabIndex = 195;
            // 
            // rbStoreUnchecked
            // 
            this.rbStoreUnchecked.AutoSize = true;
            this.rbStoreUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbStoreUnchecked.Name = "rbStoreUnchecked";
            this.rbStoreUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbStoreUnchecked.TabIndex = 2;
            this.rbStoreUnchecked.Text = "Кроме выбранных";
            this.rbStoreUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbStoreChecked
            // 
            this.rbStoreChecked.AutoSize = true;
            this.rbStoreChecked.Checked = true;
            this.rbStoreChecked.Location = new System.Drawing.Point(6, 19);
            this.rbStoreChecked.Name = "rbStoreChecked";
            this.rbStoreChecked.Size = new System.Drawing.Size(84, 17);
            this.rbStoreChecked.TabIndex = 0;
            this.rbStoreChecked.TabStop = true;
            this.rbStoreChecked.Text = "Выбранные";
            this.rbStoreChecked.UseVisualStyleBackColor = true;
            // 
            // btnDescription
            // 
            this.btnDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDescription.Location = new System.Drawing.Point(3, 3);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new System.Drawing.Size(141, 26);
            this.btnDescription.TabIndex = 204;
            this.btnDescription.Text = "Описание отчета";
            this.btnDescription.UseVisualStyleBackColor = true;
            this.btnDescription.Click += new System.EventHandler(this.btnDescription_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ucSupplier);
            this.groupBox4.Controls.Add(this.rbSupplierUnchecked);
            this.groupBox4.Controls.Add(this.rbSupplierChecked);
            this.groupBox4.Location = new System.Drawing.Point(313, 358);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(295, 128);
            this.groupBox4.TabIndex = 203;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Поставщики";
            // 
            // ucSupplier
            // 
            this.ucSupplier.AllowSaveState = false;
            this.ucSupplier.Caption = "Поставщики";
            this.ucSupplier.Location = new System.Drawing.Point(0, 42);
            this.ucSupplier.Mnemocode = "CONTRACTOR";
            this.ucSupplier.Name = "ucSupplier";
            this.ucSupplier.Pinnable = false;
            this.ucSupplier.Size = new System.Drawing.Size(295, 80);
            this.ucSupplier.TabIndex = 195;
            // 
            // rbSupplierUnchecked
            // 
            this.rbSupplierUnchecked.AutoSize = true;
            this.rbSupplierUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbSupplierUnchecked.Name = "rbSupplierUnchecked";
            this.rbSupplierUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbSupplierUnchecked.TabIndex = 2;
            this.rbSupplierUnchecked.Text = "Кроме выбранных";
            this.rbSupplierUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbSupplierChecked
            // 
            this.rbSupplierChecked.AutoSize = true;
            this.rbSupplierChecked.Checked = true;
            this.rbSupplierChecked.Location = new System.Drawing.Point(6, 19);
            this.rbSupplierChecked.Name = "rbSupplierChecked";
            this.rbSupplierChecked.Size = new System.Drawing.Size(84, 17);
            this.rbSupplierChecked.TabIndex = 0;
            this.rbSupplierChecked.TabStop = true;
            this.rbSupplierChecked.Text = "Выбранные";
            this.rbSupplierChecked.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklbTypeIn);
            this.groupBox1.Location = new System.Drawing.Point(12, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 102);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Виды прихода";
            // 
            // chklbTypeIn
            // 
            this.chklbTypeIn.BackColor = System.Drawing.SystemColors.Control;
            this.chklbTypeIn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklbTypeIn.FormattingEnabled = true;
            this.chklbTypeIn.Items.AddRange(new object[] {
            "Приходная накладная",
            "Перемещение из ЦО",
            "Внутреннее перемещение",
            "Перемещение между подразделениями"});
            this.chklbTypeIn.Location = new System.Drawing.Point(6, 27);
            this.chklbTypeIn.Name = "chklbTypeIn";
            this.chklbTypeIn.Size = new System.Drawing.Size(283, 60);
            this.chklbTypeIn.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chklbTypeOut);
            this.groupBox3.Location = new System.Drawing.Point(319, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 102);
            this.groupBox3.TabIndex = 205;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Виды расхода";
            // 
            // chklbTypeOut
            // 
            this.chklbTypeOut.BackColor = System.Drawing.SystemColors.Control;
            this.chklbTypeOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklbTypeOut.FormattingEnabled = true;
            this.chklbTypeOut.Items.AddRange(new object[] {
            "Чек",
            "Перемещение в ЦО",
            "Внутреннее перемещение",
            "Перемещение между подразделениями",
            "Расходная накладная"});
            this.chklbTypeOut.Location = new System.Drawing.Point(6, 19);
            this.chklbTypeOut.Name = "chklbTypeOut";
            this.chklbTypeOut.Size = new System.Drawing.Size(283, 75);
            this.chklbTypeOut.TabIndex = 2;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkbShowGroupSum);
            this.groupBox7.Controls.Add(this.chkbShowEmptyGroup);
            this.groupBox7.Controls.Add(this.ucPeriod);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.chkbShowProd);
            this.groupBox7.Location = new System.Drawing.Point(12, 28);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(596, 88);
            this.groupBox7.TabIndex = 204;
            this.groupBox7.TabStop = false;
            // 
            // chkbShowEmptyGroup
            // 
            this.chkbShowEmptyGroup.AutoSize = true;
            this.chkbShowEmptyGroup.Checked = true;
            this.chkbShowEmptyGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbShowEmptyGroup.Location = new System.Drawing.Point(313, 38);
            this.chkbShowEmptyGroup.Name = "chkbShowEmptyGroup";
            this.chkbShowEmptyGroup.Size = new System.Drawing.Size(180, 17);
            this.chkbShowEmptyGroup.TabIndex = 205;
            this.chkbShowEmptyGroup.Text = "Отображать товары вне групп";
            this.chkbShowEmptyGroup.UseVisualStyleBackColor = true;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2011, 2, 15, 14, 11, 25, 890);
            this.ucPeriod.DateTo = new System.DateTime(2011, 2, 15, 14, 11, 25, 890);
            this.ucPeriod.Location = new System.Drawing.Point(67, 16);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 204;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 203;
            this.label1.Text = "Период";
            // 
            // chkbShowProd
            // 
            this.chkbShowProd.AutoSize = true;
            this.chkbShowProd.Checked = true;
            this.chkbShowProd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbShowProd.Location = new System.Drawing.Point(313, 15);
            this.chkbShowProd.Name = "chkbShowProd";
            this.chkbShowProd.Size = new System.Drawing.Size(168, 17);
            this.chkbShowProd.TabIndex = 194;
            this.chkbShowProd.Text = "Отображать производителя";
            this.chkbShowProd.UseVisualStyleBackColor = true;
            // 
            // chkbShowGroupSum
            // 
            this.chkbShowGroupSum.AutoSize = true;
            this.chkbShowGroupSum.Location = new System.Drawing.Point(313, 62);
            this.chkbShowGroupSum.Name = "chkbShowGroupSum";
            this.chkbShowGroupSum.Size = new System.Drawing.Size(139, 17);
            this.chkbShowGroupSum.TabIndex = 206;
            this.chkbShowGroupSum.Text = "Отображать подытоги";
            this.chkbShowGroupSum.UseVisualStyleBackColor = true;
            this.chkbShowGroupSum.CheckedChanged += new System.EventHandler(this.chkbShowGroupSum_CheckedChanged);
            // 
            // F12AMarketMoveGoodsBySupplierParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 533);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "F12AMarketMoveGoodsBySupplierParams";
            this.Load += new System.EventHandler(this.F12AMarketMoveGoodsParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.Controls.SetChildIndex(this.groupBox6, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroup;
        private System.Windows.Forms.RadioButton rbGoodsGroupUnchecked;
        private System.Windows.Forms.RadioButton rbGoodsGroupChecked;
        private System.Windows.Forms.GroupBox groupBox5;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private System.Windows.Forms.RadioButton rbGoodsUnchecked;
        private System.Windows.Forms.RadioButton rbGoodsChecked;
        private System.Windows.Forms.GroupBox groupBox6;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private System.Windows.Forms.RadioButton rbStoreUnchecked;
        private System.Windows.Forms.RadioButton rbStoreChecked;
        private System.Windows.Forms.Button btnDescription;
        private System.Windows.Forms.GroupBox groupBox4;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucSupplier;
        private System.Windows.Forms.RadioButton rbSupplierUnchecked;
        private System.Windows.Forms.RadioButton rbSupplierChecked;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chklbTypeIn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox chklbTypeOut;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkbShowEmptyGroup;
        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkbShowProd;
        private System.Windows.Forms.CheckBox chkbShowGroupSum;
	}
}