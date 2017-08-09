namespace GoodsRegistry
{
	partial class GoodsRegistryParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsRegistryParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.seriesLabel = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.pageComboBox = new System.Windows.Forms.ComboBox();
            this.shortCheckBox = new System.Windows.Forms.CheckBox();
            this.ucIns = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucLPU = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucLgot = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriodShip = new ePlus.MetaData.Client.UCPeriod();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(549, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(624, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 407);
            this.panel1.Size = new System.Drawing.Size(702, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(702, 25);
            this.toolStrip1.TabIndex = 21;
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
            // ucContractors
            // 
            this.ucContractors.AllowSaveState = true;
            this.ucContractors.Caption = "Аптеки";
            this.ucContractors.Location = new System.Drawing.Point(12, 64);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.MultiSelect = true;
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Pinnable = false;
            this.ucContractors.Size = new System.Drawing.Size(331, 85);
            this.ucContractors.TabIndex = 125;
            // 
            // ucStores
            // 
            this.ucStores.AllowSaveState = true;
            this.ucStores.Caption = "Склады";
            this.ucStores.Location = new System.Drawing.Point(12, 155);
            this.ucStores.Mnemocode = "STORE";
            this.ucStores.MultiSelect = true;
            this.ucStores.Name = "ucStores";
            this.ucStores.Pinnable = false;
            this.ucStores.Size = new System.Drawing.Size(331, 85);
            this.ucStores.TabIndex = 126;
            // 
            // seriesLabel
            // 
            this.seriesLabel.AutoSize = true;
            this.seriesLabel.Location = new System.Drawing.Point(15, 340);
            this.seriesLabel.Name = "seriesLabel";
            this.seriesLabel.Size = new System.Drawing.Size(71, 13);
            this.seriesLabel.TabIndex = 127;
            this.seriesLabel.Text = "Ориентация:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(70, 37);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(229, 21);
            this.ucPeriod.TabIndex = 130;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 129;
            this.label1.Text = "Период:";
            // 
            // pageComboBox
            // 
            this.pageComboBox.FormattingEnabled = true;
            this.pageComboBox.Items.AddRange(new object[] {
            "Книжный",
            "Альбомный"});
            this.pageComboBox.Location = new System.Drawing.Point(92, 337);
            this.pageComboBox.Name = "pageComboBox";
            this.pageComboBox.Size = new System.Drawing.Size(218, 21);
            this.pageComboBox.TabIndex = 131;
            // 
            // shortCheckBox
            // 
            this.shortCheckBox.AutoSize = true;
            this.shortCheckBox.Location = new System.Drawing.Point(19, 364);
            this.shortCheckBox.Name = "shortCheckBox";
            this.shortCheckBox.Size = new System.Drawing.Size(105, 17);
            this.shortCheckBox.TabIndex = 132;
            this.shortCheckBox.Text = "Краткая форма";
            this.shortCheckBox.UseVisualStyleBackColor = true;
            // 
            // ucIns
            // 
            this.ucIns.AllowSaveState = true;
            this.ucIns.Caption = "Страховые компании";
            this.ucIns.Location = new System.Drawing.Point(12, 246);
            this.ucIns.Mnemocode = "DISCOUNT2_INSURANCE_COMPANY";
            this.ucIns.MultiSelect = true;
            this.ucIns.Name = "ucIns";
            this.ucIns.Pinnable = false;
            this.ucIns.Size = new System.Drawing.Size(331, 85);
            this.ucIns.TabIndex = 133;
            // 
            // ucLPU
            // 
            this.ucLPU.AllowSaveState = true;
            this.ucLPU.Caption = "ЛПУ";
            this.ucLPU.Location = new System.Drawing.Point(352, 155);
            this.ucLPU.Mnemocode = "DISCOUNT2_MEDICAL_ORGANIZATION";
            this.ucLPU.MultiSelect = true;
            this.ucLPU.Name = "ucLPU";
            this.ucLPU.Pinnable = false;
            this.ucLPU.Size = new System.Drawing.Size(340, 85);
            this.ucLPU.TabIndex = 151;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = true;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(352, 64);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.MultiSelect = true;
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(340, 85);
            this.ucGoods.TabIndex = 150;
            // 
            // ucLgot
            // 
            this.ucLgot.AllowSaveState = true;
            this.ucLgot.Caption = "Категории льготников";
            this.ucLgot.Location = new System.Drawing.Point(352, 246);
            this.ucLgot.Mnemocode = "DISCOUNT2_CAT_LGOT";
            this.ucLgot.MultiSelect = true;
            this.ucLgot.Name = "ucLgot";
            this.ucLgot.Pinnable = false;
            this.ucLgot.Size = new System.Drawing.Size(340, 85);
            this.ucLgot.TabIndex = 152;
            // 
            // ucPeriodShip
            // 
            this.ucPeriodShip.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriodShip.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriodShip.Location = new System.Drawing.Point(448, 37);
            this.ucPeriodShip.Name = "ucPeriodShip";
            this.ucPeriodShip.Size = new System.Drawing.Size(229, 21);
            this.ucPeriodShip.TabIndex = 154;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 153;
            this.label2.Text = "Период отпуска:";
            // 
            // GoodsRegistryParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 436);
            this.Controls.Add(this.ucPeriodShip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucLgot);
            this.Controls.Add(this.ucLPU);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.ucIns);
            this.Controls.Add(this.shortCheckBox);
            this.Controls.Add(this.pageComboBox);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seriesLabel);
            this.Controls.Add(this.ucStores);
            this.Controls.Add(this.ucContractors);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GoodsRegistryParams";
            this.Load += new System.EventHandler(this.GoodsRegistryParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoodsRegistryParams_FormClosed);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.ucStores, 0);
            this.Controls.SetChildIndex(this.seriesLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.pageComboBox, 0);
            this.Controls.SetChildIndex(this.shortCheckBox, 0);
            this.Controls.SetChildIndex(this.ucIns, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.ucLPU, 0);
            this.Controls.SetChildIndex(this.ucLgot, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ucPeriodShip, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.Label seriesLabel;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox pageComboBox;
		private System.Windows.Forms.CheckBox shortCheckBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucIns;
        public ePlus.MetaData.Client.UCPluginMultiSelect ucLPU;
        public ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucLgot;
        private ePlus.MetaData.Client.UCPeriod ucPeriodShip;
        private System.Windows.Forms.Label label2;
	}
}