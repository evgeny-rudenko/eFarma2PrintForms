namespace RCSGoodsKeepingTimeList
{
	partial class GoodsKeepingTimeParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsKeepingTimeParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.conditionLabel = new System.Windows.Forms.Label();
            this.periodLabel = new System.Windows.Forms.Label();
            this.storeLabel = new System.Windows.Forms.Label();
            this.goodsLabel = new System.Windows.Forms.Label();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dateToLabel = new System.Windows.Forms.Label();
            this.ucMetaPlugin_Store = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.ucMetaPlugin_Goods = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.chbGoodCode = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(266, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(341, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 177);
            this.panel1.Size = new System.Drawing.Size(419, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(419, 25);
            this.toolStrip1.TabIndex = 9;
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
            // conditionLabel
            // 
            this.conditionLabel.AutoSize = true;
            this.conditionLabel.Location = new System.Drawing.Point(14, 43);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Size = new System.Drawing.Size(81, 13);
            this.conditionLabel.TabIndex = 10;
            this.conditionLabel.Text = "Срок годности";
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(14, 71);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(45, 13);
            this.periodLabel.TabIndex = 11;
            this.periodLabel.Text = "Период";
            // 
            // storeLabel
            // 
            this.storeLabel.AutoSize = true;
            this.storeLabel.Location = new System.Drawing.Point(14, 101);
            this.storeLabel.Name = "storeLabel";
            this.storeLabel.Size = new System.Drawing.Size(38, 13);
            this.storeLabel.TabIndex = 12;
            this.storeLabel.Text = "Склад";
            // 
            // goodsLabel
            // 
            this.goodsLabel.AutoSize = true;
            this.goodsLabel.Location = new System.Drawing.Point(14, 128);
            this.goodsLabel.Name = "goodsLabel";
            this.goodsLabel.Size = new System.Drawing.Size(38, 13);
            this.goodsLabel.TabIndex = 13;
            this.goodsLabel.Text = "Товар";
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Location = new System.Drawing.Point(111, 40);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(298, 21);
            this.conditionComboBox.TabIndex = 14;
            this.conditionComboBox.SelectedIndexChanged += new System.EventHandler(this.conditionComboBox_SelectedIndexChanged);
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Location = new System.Drawing.Point(111, 67);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(130, 20);
            this.fromDateTimePicker.TabIndex = 15;
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.Location = new System.Drawing.Point(279, 67);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(130, 20);
            this.toDateTimePicker.TabIndex = 16;
            // 
            // dateToLabel
            // 
            this.dateToLabel.AutoSize = true;
            this.dateToLabel.Location = new System.Drawing.Point(247, 71);
            this.dateToLabel.Name = "dateToLabel";
            this.dateToLabel.Size = new System.Drawing.Size(19, 13);
            this.dateToLabel.TabIndex = 17;
            this.dateToLabel.Text = "по";
            // 
            // ucMetaPlugin_Store
            // 
            this.ucMetaPlugin_Store.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucMetaPlugin_Store.Location = new System.Drawing.Point(111, 93);
            this.ucMetaPlugin_Store.Mnemocode = "STORE";
            this.ucMetaPlugin_Store.Name = "ucMetaPlugin_Store";
            this.ucMetaPlugin_Store.Size = new System.Drawing.Size(296, 21);
            this.ucMetaPlugin_Store.TabIndex = 107;
            // 
            // ucMetaPlugin_Goods
            // 
            this.ucMetaPlugin_Goods.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucMetaPlugin_Goods.Location = new System.Drawing.Point(111, 120);
            this.ucMetaPlugin_Goods.Mnemocode = "GOODS2";
            this.ucMetaPlugin_Goods.Name = "ucMetaPlugin_Goods";
            this.ucMetaPlugin_Goods.Size = new System.Drawing.Size(296, 21);
            this.ucMetaPlugin_Goods.TabIndex = 108;
            // 
            // chbGoodCode
            // 
            this.chbGoodCode.AutoSize = true;
            this.chbGoodCode.Location = new System.Drawing.Point(111, 147);
            this.chbGoodCode.Name = "chbGoodCode";
            this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
            this.chbGoodCode.TabIndex = 125;
            this.chbGoodCode.Text = "Отображать код товара ";
            this.chbGoodCode.UseVisualStyleBackColor = true;
            // 
            // GoodsKeepingTimeParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 206);
            this.Controls.Add(this.chbGoodCode);
            this.Controls.Add(this.ucMetaPlugin_Goods);
            this.Controls.Add(this.ucMetaPlugin_Store);
            this.Controls.Add(this.dateToLabel);
            this.Controls.Add(this.toDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.goodsLabel);
            this.Controls.Add(this.storeLabel);
            this.Controls.Add(this.periodLabel);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GoodsKeepingTimeParams";
            this.Load += new System.EventHandler(this.GoodsKeepingTimeParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.conditionLabel, 0);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.storeLabel, 0);
            this.Controls.SetChildIndex(this.goodsLabel, 0);
            this.Controls.SetChildIndex(this.conditionComboBox, 0);
            this.Controls.SetChildIndex(this.fromDateTimePicker, 0);
            this.Controls.SetChildIndex(this.toDateTimePicker, 0);
            this.Controls.SetChildIndex(this.dateToLabel, 0);
            this.Controls.SetChildIndex(this.ucMetaPlugin_Store, 0);
            this.Controls.SetChildIndex(this.ucMetaPlugin_Goods, 0);
            this.Controls.SetChildIndex(this.chbGoodCode, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label conditionLabel;
		private System.Windows.Forms.Label periodLabel;
		private System.Windows.Forms.Label storeLabel;
		private System.Windows.Forms.Label goodsLabel;
		private System.Windows.Forms.ComboBox conditionComboBox;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
		private System.Windows.Forms.Label dateToLabel;
		private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPlugin_Store;
		private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPlugin_Goods;
		private System.Windows.Forms.CheckBox chbGoodCode;
	}
}