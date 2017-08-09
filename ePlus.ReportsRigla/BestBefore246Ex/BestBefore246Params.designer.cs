namespace RCBBestBefore246_Rigla
{
	partial class BestBefore246Params
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BestBefore246Params));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.comboSort = new System.Windows.Forms.ComboBox();
      this.labelSort = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.periodCheckBox = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(263, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(338, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 313);
      this.panel1.Size = new System.Drawing.Size(416, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(416, 25);
      this.toolStrip1.TabIndex = 9;
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
      this.ucContractors.AllowSaveState = false;
      this.ucContractors.Caption = "Аптеки";
      this.ucContractors.Location = new System.Drawing.Point(12, 63);
      this.ucContractors.Mnemocode = "CONTRACTOR";
      this.ucContractors.Name = "ucContractors";
      this.ucContractors.Size = new System.Drawing.Size(391, 102);
      this.ucContractors.TabIndex = 104;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = false;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(12, 171);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(391, 102);
      this.ucStores.TabIndex = 105;
      // 
      // comboSort
      // 
      this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboSort.FormattingEnabled = true;
      this.comboSort.Items.AddRange(new object[] {
            "Название товара",
            "Срок годности"});
      this.comboSort.Location = new System.Drawing.Point(394, 275);
      this.comboSort.Name = "comboSort";
      this.comboSort.Size = new System.Drawing.Size(183, 21);
      this.comboSort.TabIndex = 136;
      this.comboSort.Visible = false;
      // 
      // labelSort
      // 
      this.labelSort.Location = new System.Drawing.Point(391, 251);
      this.labelSort.Name = "labelSort";
      this.labelSort.Size = new System.Drawing.Size(96, 21);
      this.labelSort.TabIndex = 135;
      this.labelSort.Text = "Сортировать по : ";
      this.labelSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.labelSort.Visible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(39, 13);
      this.label1.TabIndex = 137;
      this.label1.Text = "Отчет:";
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "Отчет по срокам годности товаров (2, 4, 6 месяцев)",
            "Отчет по управлению сроками годности товаров "});
      this.comboBox1.Location = new System.Drawing.Point(57, 36);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(346, 21);
      this.comboBox1.TabIndex = 138;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 283);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 153;
      this.label2.Text = "Период:";
      // 
      // ucPeriod
      // 
      this.ucPeriod.AutoSize = true;
      this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
      this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
      this.ucPeriod.Location = new System.Drawing.Point(68, 279);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(224, 23);
      this.ucPeriod.TabIndex = 154;
      // 
      // periodCheckBox
      // 
      this.periodCheckBox.AutoSize = true;
      this.periodCheckBox.Location = new System.Drawing.Point(298, 282);
      this.periodCheckBox.Name = "periodCheckBox";
      this.periodCheckBox.Size = new System.Drawing.Size(90, 17);
      this.periodCheckBox.TabIndex = 155;
      this.periodCheckBox.Text = "Без периода";
      this.periodCheckBox.UseVisualStyleBackColor = true;
      this.periodCheckBox.CheckedChanged += new System.EventHandler(this.periodCheckBox_CheckedChanged);
      // 
      // BestBefore246Params
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(416, 342);
      this.Controls.Add(this.periodCheckBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.comboSort);
      this.Controls.Add(this.labelSort);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucContractors);
      this.Controls.Add(this.toolStrip1);
      this.Name = "BestBefore246Params";
      this.Load += new System.EventHandler(this.BestBefore246Params_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BestBefore246Params_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucContractors, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.labelSort, 0);
      this.Controls.SetChildIndex(this.comboSort, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.comboBox1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.periodCheckBox, 0);
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
		public System.Windows.Forms.ComboBox comboSort;
		private System.Windows.Forms.Label labelSort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.CheckBox periodCheckBox;
	}
}