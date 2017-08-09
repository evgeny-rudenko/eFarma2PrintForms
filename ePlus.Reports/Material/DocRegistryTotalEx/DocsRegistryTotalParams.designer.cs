namespace RCSDocRegistry
{
	partial class DocsRegistryTotalParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocsRegistryTotalParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.docStateCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucContractorFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStoreFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucTypesDocument = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucContractorTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStoreTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.sortLabel = new System.Windows.Forms.Label();
            this.sortComboBox = new System.Windows.Forms.ComboBox();
            this.showCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.repTypeComboBox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(542, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(617, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 533);
            this.panel1.Size = new System.Drawing.Size(695, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(695, 25);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 173;
            this.label3.Text = "Период:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 170;
            this.label1.Text = "Состояние документа:";
            // 
            // docStateCheckedListBox
            // 
            this.docStateCheckedListBox.CheckOnClick = true;
            this.docStateCheckedListBox.Location = new System.Drawing.Point(470, 59);
            this.docStateCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.docStateCheckedListBox.Name = "docStateCheckedListBox";
            this.docStateCheckedListBox.Size = new System.Drawing.Size(192, 64);
            this.docStateCheckedListBox.TabIndex = 169;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(75, 32);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(247, 21);
            this.ucPeriod.TabIndex = 167;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Документы содержат товар(ы)";
            this.ucGoods.Location = new System.Drawing.Point(12, 414);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(650, 74);
            this.ucGoods.TabIndex = 179;
            // 
            // ucContractorFrom
            // 
            this.ucContractorFrom.AllowSaveState = false;
            this.ucContractorFrom.Caption = "Контрагент(ы)";
            this.ucContractorFrom.Location = new System.Drawing.Point(6, 19);
            this.ucContractorFrom.Mnemocode = "CONTRACTOR";
            this.ucContractorFrom.Name = "ucContractorFrom";
            this.ucContractorFrom.Pinnable = false;
            this.ucContractorFrom.Size = new System.Drawing.Size(304, 74);
            this.ucContractorFrom.TabIndex = 177;
            // 
            // ucStoreFrom
            // 
            this.ucStoreFrom.AllowSaveState = false;
            this.ucStoreFrom.Caption = "Склад(ы)";
            this.ucStoreFrom.Location = new System.Drawing.Point(6, 99);
            this.ucStoreFrom.Mnemocode = "STORE";
            this.ucStoreFrom.Name = "ucStoreFrom";
            this.ucStoreFrom.Pinnable = false;
            this.ucStoreFrom.Size = new System.Drawing.Size(304, 74);
            this.ucStoreFrom.TabIndex = 178;
            // 
            // ucTypesDocument
            // 
            this.ucTypesDocument.AllowSaveState = false;
            this.ucTypesDocument.Caption = "Типы документа(ов)";
            this.ucTypesDocument.Location = new System.Drawing.Point(12, 59);
            this.ucTypesDocument.Mnemocode = "TYPE_DOCUMENT";
            this.ucTypesDocument.Name = "ucTypesDocument";
            this.ucTypesDocument.Pinnable = false;
            this.ucTypesDocument.Size = new System.Drawing.Size(449, 158);
            this.ucTypesDocument.TabIndex = 180;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucContractorFrom);
            this.groupBox1.Controls.Add(this.ucStoreFrom);
            this.groupBox1.Location = new System.Drawing.Point(12, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 180);
            this.groupBox1.TabIndex = 181;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "От кого";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucContractorTo);
            this.groupBox2.Controls.Add(this.ucStoreTo);
            this.groupBox2.Location = new System.Drawing.Point(340, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 180);
            this.groupBox2.TabIndex = 182;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Кому";
            // 
            // ucContractorTo
            // 
            this.ucContractorTo.AllowSaveState = false;
            this.ucContractorTo.Caption = "Контрагент(ы)";
            this.ucContractorTo.Location = new System.Drawing.Point(6, 19);
            this.ucContractorTo.Mnemocode = "CONTRACTOR";
            this.ucContractorTo.Name = "ucContractorTo";
            this.ucContractorTo.Pinnable = false;
            this.ucContractorTo.Size = new System.Drawing.Size(304, 74);
            this.ucContractorTo.TabIndex = 177;
            // 
            // ucStoreTo
            // 
            this.ucStoreTo.AllowSaveState = false;
            this.ucStoreTo.Caption = "Склад(ы)";
            this.ucStoreTo.Location = new System.Drawing.Point(6, 99);
            this.ucStoreTo.Mnemocode = "STORE";
            this.ucStoreTo.Name = "ucStoreTo";
            this.ucStoreTo.Pinnable = false;
            this.ucStoreTo.Size = new System.Drawing.Size(304, 74);
            this.ucStoreTo.TabIndex = 178;
            // 
            // sortLabel
            // 
            this.sortLabel.AutoSize = true;
            this.sortLabel.Location = new System.Drawing.Point(327, 497);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(70, 13);
            this.sortLabel.TabIndex = 183;
            this.sortLabel.Text = "Сортировка:";
            // 
            // sortComboBox
            // 
            this.sortComboBox.FormattingEnabled = true;
            this.sortComboBox.Items.AddRange(new object[] {
            "По дате документа",
            "По типу документа"});
            this.sortComboBox.Location = new System.Drawing.Point(403, 494);
            this.sortComboBox.Name = "sortComboBox";
            this.sortComboBox.Size = new System.Drawing.Size(226, 21);
            this.sortComboBox.TabIndex = 184;
            // 
            // showCheckedListBox
            // 
            this.showCheckedListBox.CheckOnClick = true;
            this.showCheckedListBox.Items.AddRange(new object[] {
            "Оптовая сумма с НДС",
            "НДС по ставкам"});
            this.showCheckedListBox.Location = new System.Drawing.Point(470, 153);
            this.showCheckedListBox.Name = "showCheckedListBox";
            this.showCheckedListBox.Size = new System.Drawing.Size(192, 64);
            this.showCheckedListBox.TabIndex = 185;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(467, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 186;
            this.label2.Text = "Выводить:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 497);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 188;
            this.label4.Text = "Вид отчета:";
            // 
            // repTypeComboBox
            // 
            this.repTypeComboBox.FormattingEnabled = true;
            this.repTypeComboBox.Items.AddRange(new object[] {
            "Оптовый НДС",
            "Розничный НДС"});
            this.repTypeComboBox.Location = new System.Drawing.Point(87, 494);
            this.repTypeComboBox.Name = "repTypeComboBox";
            this.repTypeComboBox.Size = new System.Drawing.Size(222, 21);
            this.repTypeComboBox.TabIndex = 187;
            // 
            // DocsRegistryTotalParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 562);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.repTypeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.showCheckedListBox);
            this.Controls.Add(this.sortComboBox);
            this.Controls.Add(this.sortLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucTypesDocument);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.docStateCheckedListBox);
            this.Controls.Add(this.ucPeriod);
            this.Name = "DocsRegistryTotalParams";
            this.Load += new System.EventHandler(this.DocsRegistryTotalParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.docStateCheckedListBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.ucTypesDocument, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.sortLabel, 0);
            this.Controls.SetChildIndex(this.sortComboBox, 0);
            this.Controls.SetChildIndex(this.showCheckedListBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.repTypeComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox docStateCheckedListBox;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorFrom;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStoreFrom;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucTypesDocument;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorTo;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStoreTo;
		private System.Windows.Forms.Label sortLabel;
		private System.Windows.Forms.ComboBox sortComboBox;
		private System.Windows.Forms.CheckedListBox showCheckedListBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox repTypeComboBox;
	}
}