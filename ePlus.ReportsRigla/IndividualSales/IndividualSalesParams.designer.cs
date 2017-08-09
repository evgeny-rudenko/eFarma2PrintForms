namespace RCBIndividualSales_Rigla
{
	partial class IndividualSalesParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndividualSalesParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
			this.cashierCheckBox = new System.Windows.Forms.CheckBox();
			this.ucCashier = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.label1 = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.goodsCheckBox = new System.Windows.Forms.CheckBox();
			this.codeCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(480, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(555, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 411);
			this.panel1.Size = new System.Drawing.Size(633, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(633, 25);
			this.toolStrip1.TabIndex = 9;
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
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(15, 69);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(337, 118);
			this.ucContractors.TabIndex = 104;
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "Товары";
			this.ucGoods.Location = new System.Drawing.Point(18, 193);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(334, 119);
			this.ucGoods.TabIndex = 144;
			// 
			// ucGoodsGroup
			// 
			this.ucGoodsGroup.Location = new System.Drawing.Point(360, 193);
			this.ucGoodsGroup.Name = "ucGoodsGroup";
			this.ucGoodsGroup.Size = new System.Drawing.Size(254, 127);
			this.ucGoodsGroup.TabIndex = 145;
			// 
			// cashierCheckBox
			// 
			this.cashierCheckBox.AutoSize = true;
			this.cashierCheckBox.Location = new System.Drawing.Point(18, 328);
			this.cashierCheckBox.Name = "cashierCheckBox";
			this.cashierCheckBox.Size = new System.Drawing.Size(162, 17);
			this.cashierCheckBox.TabIndex = 147;
			this.cashierCheckBox.Text = "Детализация по кассирам";
			this.cashierCheckBox.UseVisualStyleBackColor = true;
			// 
			// ucCashier
			// 
			this.ucCashier.AllowSaveState = true;
			this.ucCashier.Caption = "Кассир";
			this.ucCashier.Location = new System.Drawing.Point(360, 69);
			this.ucCashier.Mnemocode = "CASH_REGISTER_USER";
			this.ucCashier.Name = "ucCashier";
			this.ucCashier.Size = new System.Drawing.Size(254, 118);
			this.ucCashier.TabIndex = 150;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 151;
			this.label1.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.AutoSize = true;
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(70, 39);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(224, 23);
			this.ucPeriod.TabIndex = 152;
			// 
			// goodsCheckBox
			// 
			this.goodsCheckBox.AutoSize = true;
			this.goodsCheckBox.Location = new System.Drawing.Point(18, 351);
			this.goodsCheckBox.Name = "goodsCheckBox";
			this.goodsCheckBox.Size = new System.Drawing.Size(155, 17);
			this.goodsCheckBox.TabIndex = 153;
			this.goodsCheckBox.Text = "Детализация по товарам";
			this.goodsCheckBox.UseVisualStyleBackColor = true;
			this.goodsCheckBox.CheckedChanged += new System.EventHandler(this.goodsCheckBox_CheckedChanged);
			// 
			// codeCheckBox
			// 
			this.codeCheckBox.AutoSize = true;
			this.codeCheckBox.Location = new System.Drawing.Point(18, 374);
			this.codeCheckBox.Name = "codeCheckBox";
			this.codeCheckBox.Size = new System.Drawing.Size(109, 17);
			this.codeCheckBox.TabIndex = 154;
			this.codeCheckBox.Text = "Отображать код";
			this.codeCheckBox.UseVisualStyleBackColor = true;
			// 
			// IndividualSalesParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(633, 440);
			this.Controls.Add(this.codeCheckBox);
			this.Controls.Add(this.goodsCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.ucCashier);
			this.Controls.Add(this.cashierCheckBox);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.ucGoodsGroup);
			this.Controls.Add(this.ucContractors);
			this.Controls.Add(this.toolStrip1);
			this.Name = "IndividualSalesParams";
			this.Load += new System.EventHandler(this.IndividualSalesParams_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IndividualSalesParams_FormClosing);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
			this.Controls.SetChildIndex(this.ucGoods, 0);
			this.Controls.SetChildIndex(this.cashierCheckBox, 0);
			this.Controls.SetChildIndex(this.ucCashier, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.goodsCheckBox, 0);
			this.Controls.SetChildIndex(this.codeCheckBox, 0);
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
		private System.Windows.Forms.CheckBox cashierCheckBox;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private ePlus.CommonEx.Controls.SelectGoodsGroup ucGoodsGroup;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucCashier;
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.CheckBox goodsCheckBox;
		private System.Windows.Forms.CheckBox codeCheckBox;
	}
}