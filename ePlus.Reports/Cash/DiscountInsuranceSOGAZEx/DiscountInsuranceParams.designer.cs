namespace DiscountInsuranceSOGAZEx
{
	partial class DiscountInsuranceParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscountInsuranceParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.periodLabel = new System.Windows.Forms.Label();
			this.periodPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.goodsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.discountsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.policiesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.discount2catLgotsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.membersPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.dateLabel = new System.Windows.Forms.Label();
			this.discounteeLabel = new System.Windows.Forms.Label();
			this.dateSortingComboBox = new System.Windows.Forms.ComboBox();
			this.discounteeSortingComboBox = new System.Windows.Forms.ComboBox();
			this.sortingParamsGroupBox = new System.Windows.Forms.GroupBox();
			this.sortingOrderÑheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.sortingParamsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(534, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(609, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 458);
			this.panel1.Size = new System.Drawing.Size(687, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(687, 25);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Î÷èñòèòü";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// periodLabel
			// 
			this.periodLabel.AutoSize = true;
			this.periodLabel.Location = new System.Drawing.Point(10, 39);
			this.periodLabel.Name = "periodLabel";
			this.periodLabel.Size = new System.Drawing.Size(48, 13);
			this.periodLabel.TabIndex = 11;
			this.periodLabel.Text = "Ïåðèîä:";
			// 
			// periodPeriod
			// 
			this.periodPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.periodPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.periodPeriod.Location = new System.Drawing.Point(74, 36);
			this.periodPeriod.Name = "periodPeriod";
			this.periodPeriod.Size = new System.Drawing.Size(256, 21);
			this.periodPeriod.TabIndex = 12;
			// 
			// goodsPluginMultiSelect
			// 
			this.goodsPluginMultiSelect.AllowSaveState = false;
			this.goodsPluginMultiSelect.Caption = "ËÑ";
			this.goodsPluginMultiSelect.Location = new System.Drawing.Point(13, 253);
			this.goodsPluginMultiSelect.Mnemocode = "GOODS2";
			this.goodsPluginMultiSelect.Name = "goodsPluginMultiSelect";
			this.goodsPluginMultiSelect.Size = new System.Drawing.Size(654, 85);
			this.goodsPluginMultiSelect.TabIndex = 140;
			// 
			// discountsPluginMultiSelect
			// 
			this.discountsPluginMultiSelect.AllowSaveState = false;
			this.discountsPluginMultiSelect.Caption = "% ñêèäêè";
			this.discountsPluginMultiSelect.Location = new System.Drawing.Point(345, 63);
			this.discountsPluginMultiSelect.Mnemocode = "DISCOUNT2_INSURANCE_CALC_SCHEMA_PLUGIN";
			this.discountsPluginMultiSelect.Name = "discountsPluginMultiSelect";
			this.discountsPluginMultiSelect.Size = new System.Drawing.Size(323, 93);
			this.discountsPluginMultiSelect.TabIndex = 139;
			// 
			// policiesPluginMultiSelect
			// 
			this.policiesPluginMultiSelect.AllowSaveState = false;
			this.policiesPluginMultiSelect.Caption = "Ñòðàõîâûå ïîëèñû";
			this.policiesPluginMultiSelect.Location = new System.Drawing.Point(13, 162);
			this.policiesPluginMultiSelect.Mnemocode = "DISCOUNT2_INSURANCE_POLICY";
			this.policiesPluginMultiSelect.Name = "policiesPluginMultiSelect";
			this.policiesPluginMultiSelect.Size = new System.Drawing.Size(326, 85);
			this.policiesPluginMultiSelect.TabIndex = 138;
			// 
			// discount2catLgotsPluginMultiSelect
			// 
			this.discount2catLgotsPluginMultiSelect.AllowSaveState = false;
			this.discount2catLgotsPluginMultiSelect.Caption = "Êàòåãîðèè ëüãîòíîñòè";
			this.discount2catLgotsPluginMultiSelect.Location = new System.Drawing.Point(13, 63);
			this.discount2catLgotsPluginMultiSelect.Mnemocode = "DISCOUNT2_CAT_LGOT";
			this.discount2catLgotsPluginMultiSelect.Name = "discount2catLgotsPluginMultiSelect";
			this.discount2catLgotsPluginMultiSelect.Size = new System.Drawing.Size(326, 93);
			this.discount2catLgotsPluginMultiSelect.TabIndex = 137;
			// 
			// membersPluginMultiSelect
			// 
			this.membersPluginMultiSelect.AllowSaveState = false;
			this.membersPluginMultiSelect.Caption = "Ëüãîòíèêè";
			this.membersPluginMultiSelect.Location = new System.Drawing.Point(345, 162);
			this.membersPluginMultiSelect.Mnemocode = "DISCOUNT2_MEMBER";
			this.membersPluginMultiSelect.Name = "membersPluginMultiSelect";
			this.membersPluginMultiSelect.Size = new System.Drawing.Size(323, 85);
			this.membersPluginMultiSelect.TabIndex = 136;
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new System.Drawing.Point(303, 22);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(33, 13);
			this.dateLabel.TabIndex = 141;
			this.dateLabel.Text = "Äàòà";
			// 
			// discounteeLabel
			// 
			this.discounteeLabel.AutoSize = true;
			this.discounteeLabel.Location = new System.Drawing.Point(303, 54);
			this.discounteeLabel.Name = "discounteeLabel";
			this.discounteeLabel.Size = new System.Drawing.Size(55, 13);
			this.discounteeLabel.TabIndex = 142;
			this.discounteeLabel.Text = "Ëüãîòíèê";
			// 
			// dateSortingComboBox
			// 
			this.dateSortingComboBox.FormattingEnabled = true;
			this.dateSortingComboBox.Items.AddRange(new object[] {
            "Áåç ñîðòèðîâêè",
            "Ïî âîçðàñòàíèþ",
            "Ïî óáûâàíèþ"});
			this.dateSortingComboBox.Location = new System.Drawing.Point(367, 19);
			this.dateSortingComboBox.Name = "dateSortingComboBox";
			this.dateSortingComboBox.Size = new System.Drawing.Size(225, 21);
			this.dateSortingComboBox.TabIndex = 143;
			// 
			// discounteeSortingComboBox
			// 
			this.discounteeSortingComboBox.FormattingEnabled = true;
			this.discounteeSortingComboBox.Items.AddRange(new object[] {
            "Áåç ñîðòèðîâêè",
            "Ïî âîçðàñòàíèþ",
            "Ïî óáûâàíèþ"});
			this.discounteeSortingComboBox.Location = new System.Drawing.Point(367, 51);
			this.discounteeSortingComboBox.Name = "discounteeSortingComboBox";
			this.discounteeSortingComboBox.Size = new System.Drawing.Size(225, 21);
			this.discounteeSortingComboBox.TabIndex = 144;
			// 
			// sortingParamsGroupBox
			// 
			this.sortingParamsGroupBox.Controls.Add(this.sortingOrderÑheckBox);
			this.sortingParamsGroupBox.Controls.Add(this.dateSortingComboBox);
			this.sortingParamsGroupBox.Controls.Add(this.discounteeSortingComboBox);
			this.sortingParamsGroupBox.Controls.Add(this.dateLabel);
			this.sortingParamsGroupBox.Controls.Add(this.discounteeLabel);
			this.sortingParamsGroupBox.Location = new System.Drawing.Point(17, 346);
			this.sortingParamsGroupBox.Name = "sortingParamsGroupBox";
			this.sortingParamsGroupBox.Size = new System.Drawing.Size(616, 88);
			this.sortingParamsGroupBox.TabIndex = 145;
			this.sortingParamsGroupBox.TabStop = false;
			this.sortingParamsGroupBox.Text = "Ïàðàìåòðû ñîðòèðîâêè";
			// 
			// sortingOrderÑheckBox
			// 
			this.sortingOrderÑheckBox.AutoSize = true;
			this.sortingOrderÑheckBox.Location = new System.Drawing.Point(22, 23);
			this.sortingOrderÑheckBox.Name = "sortingOrderÑheckBox";
			this.sortingOrderÑheckBox.Size = new System.Drawing.Size(184, 17);
			this.sortingOrderÑheckBox.TabIndex = 145;
			this.sortingOrderÑheckBox.Text = "Èçìåíèòü ïîðÿäîê ñîðòèðîâêè";
			this.sortingOrderÑheckBox.UseVisualStyleBackColor = true;
			this.sortingOrderÑheckBox.CheckedChanged += new System.EventHandler(this.sortingOrderÑheckBox_CheckedChanged);
			// 
			// DiscountInsuranceParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687, 487);
			this.Controls.Add(this.sortingParamsGroupBox);
			this.Controls.Add(this.goodsPluginMultiSelect);
			this.Controls.Add(this.discountsPluginMultiSelect);
			this.Controls.Add(this.policiesPluginMultiSelect);
			this.Controls.Add(this.discount2catLgotsPluginMultiSelect);
			this.Controls.Add(this.membersPluginMultiSelect);
			this.Controls.Add(this.periodPeriod);
			this.Controls.Add(this.periodLabel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "DiscountInsuranceParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.periodLabel, 0);
			this.Controls.SetChildIndex(this.periodPeriod, 0);
			this.Controls.SetChildIndex(this.membersPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.discount2catLgotsPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.policiesPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.discountsPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.goodsPluginMultiSelect, 0);
			this.Controls.SetChildIndex(this.sortingParamsGroupBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.sortingParamsGroupBox.ResumeLayout(false);
			this.sortingParamsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label periodLabel;
		private ePlus.MetaData.Client.UCPeriod periodPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect goodsPluginMultiSelect;
		private ePlus.MetaData.Client.UCPluginMultiSelect discountsPluginMultiSelect;
		private ePlus.MetaData.Client.UCPluginMultiSelect policiesPluginMultiSelect;
		private ePlus.MetaData.Client.UCPluginMultiSelect discount2catLgotsPluginMultiSelect;
		private ePlus.MetaData.Client.UCPluginMultiSelect membersPluginMultiSelect;
		private System.Windows.Forms.Label dateLabel;
		private System.Windows.Forms.Label discounteeLabel;
		private System.Windows.Forms.ComboBox dateSortingComboBox;
		private System.Windows.Forms.ComboBox discounteeSortingComboBox;
		private System.Windows.Forms.GroupBox sortingParamsGroupBox;
		private System.Windows.Forms.CheckBox sortingOrderÑheckBox;
	}
}