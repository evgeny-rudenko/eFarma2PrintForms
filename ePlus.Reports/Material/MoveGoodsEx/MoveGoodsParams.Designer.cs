using ePlus.MetaData.Client;

namespace RCBMoveGoods
{
	partial class MoveGoodsParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveGoodsParams));
            this.multiGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiProducer = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.periodLot = new ePlus.MetaData.Client.UCPeriod();
            this.lbPeriod = new System.Windows.Forms.Label();
            this.checkMove = new System.Windows.Forms.CheckBox();
            this.chkUseLotDate = new System.Windows.Forms.CheckBox();
            this.checkInvoice = new System.Windows.Forms.CheckBox();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.checkPreview = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbForm = new System.Windows.Forms.ComboBox();
            this.chbGoodCode = new System.Windows.Forms.CheckBox();
            this.repTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vatCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.auCheckBox = new System.Windows.Forms.CheckBox();
            this.ggCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(499, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(574, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 491);
            this.panel1.Size = new System.Drawing.Size(652, 29);
            // 
            // multiGoods
            // 
            this.multiGoods.AllowSaveState = false;
            this.multiGoods.Caption = "";
            this.multiGoods.Location = new System.Drawing.Point(14, 301);
            this.multiGoods.Mnemocode = "STOCK3";
            this.multiGoods.Name = "multiGoods";
            this.multiGoods.Pinnable = true;
            this.multiGoods.Size = new System.Drawing.Size(627, 87);
            this.multiGoods.TabIndex = 0;
            // 
            // multiStore
            // 
            this.multiStore.AllowSaveState = false;
            this.multiStore.Caption = "";
            this.multiStore.Location = new System.Drawing.Point(14, 208);
            this.multiStore.Mnemocode = "STORE";
            this.multiStore.Name = "multiStore";
            this.multiStore.Pinnable = false;
            this.multiStore.Size = new System.Drawing.Size(328, 87);
            this.multiStore.TabIndex = 3;
            // 
            // multiProducer
            // 
            this.multiProducer.AllowSaveState = false;
            this.multiProducer.Caption = "";
            this.multiProducer.Location = new System.Drawing.Point(14, 116);
            this.multiProducer.Mnemocode = "PRODUCER";
            this.multiProducer.Name = "multiProducer";
            this.multiProducer.Pinnable = false;
            this.multiProducer.Size = new System.Drawing.Size(328, 86);
            this.multiProducer.TabIndex = 4;
            // 
            // multiContractor
            // 
            this.multiContractor.AllowSaveState = false;
            this.multiContractor.Caption = "";
            this.multiContractor.Location = new System.Drawing.Point(345, 116);
            this.multiContractor.Mnemocode = "CONTRACTOR";
            this.multiContractor.Name = "multiContractor";
            this.multiContractor.Pinnable = false;
            this.multiContractor.Size = new System.Drawing.Size(298, 86);
            this.multiContractor.TabIndex = 5;
            // 
            // multiKind
            // 
            this.multiKind.AllowSaveState = false;
            this.multiKind.Caption = "";
            this.multiKind.Location = new System.Drawing.Point(345, 208);
            this.multiKind.Mnemocode = "GOODS_KIND";
            this.multiKind.Name = "multiKind";
            this.multiKind.Pinnable = false;
            this.multiKind.Size = new System.Drawing.Size(298, 87);
            this.multiKind.TabIndex = 6;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
            this.ucPeriod.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
            this.ucPeriod.Location = new System.Drawing.Point(120, 34);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 7;
            // 
            // periodLot
            // 
            this.periodLot.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.Enabled = false;
            this.periodLot.Location = new System.Drawing.Point(165, 394);
            this.periodLot.Name = "periodLot";
            this.periodLot.Size = new System.Drawing.Size(222, 21);
            this.periodLot.TabIndex = 8;
            // 
            // lbPeriod
            // 
            this.lbPeriod.AutoSize = true;
            this.lbPeriod.Location = new System.Drawing.Point(11, 38);
            this.lbPeriod.Name = "lbPeriod";
            this.lbPeriod.Size = new System.Drawing.Size(48, 13);
            this.lbPeriod.TabIndex = 9;
            this.lbPeriod.Text = "Период:";
            // 
            // checkMove
            // 
            this.checkMove.Checked = true;
            this.checkMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMove.Location = new System.Drawing.Point(259, 419);
            this.checkMove.Name = "checkMove";
            this.checkMove.Size = new System.Drawing.Size(214, 17);
            this.checkMove.TabIndex = 10;
            this.checkMove.Text = "Не показывать товар без движения";
            // 
            // chkUseLotDate
            // 
            this.chkUseLotDate.AutoSize = true;
            this.chkUseLotDate.Location = new System.Drawing.Point(14, 396);
            this.chkUseLotDate.Name = "chkUseLotDate";
            this.chkUseLotDate.Size = new System.Drawing.Size(146, 17);
            this.chkUseLotDate.TabIndex = 152;
            this.chkUseLotDate.Text = "Период прихода товара";
            this.chkUseLotDate.UseVisualStyleBackColor = true;
            this.chkUseLotDate.CheckedChanged += new System.EventHandler(this.chkUseLotDate_CheckedChanged);
            // 
            // checkInvoice
            // 
            this.checkInvoice.AutoSize = true;
            this.checkInvoice.Location = new System.Drawing.Point(14, 419);
            this.checkInvoice.Name = "checkInvoice";
            this.checkInvoice.Size = new System.Drawing.Size(101, 17);
            this.checkInvoice.TabIndex = 151;
            this.checkInvoice.Text = "Только приход";
            this.checkInvoice.UseVisualStyleBackColor = true;
            // 
            // cbSort
            // 
            this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSort.Enabled = false;
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Items.AddRange(new object[] {
            "Не сортировать",
            "По возрастанию",
            "По убыванию"});
            this.cbSort.Location = new System.Drawing.Point(406, 394);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(237, 21);
            this.cbSort.TabIndex = 153;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(652, 25);
            this.toolStrip1.TabIndex = 154;
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
            // checkPreview
            // 
            this.checkPreview.AutoSize = true;
            this.checkPreview.Checked = true;
            this.checkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPreview.Location = new System.Drawing.Point(4444, 350);
            this.checkPreview.Name = "checkPreview";
            this.checkPreview.Size = new System.Drawing.Size(171, 17);
            this.checkPreview.TabIndex = 101;
            this.checkPreview.Text = "Предварительный просмотр";
            this.checkPreview.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 160;
            this.label2.Text = "Ориентация отчета:";
            // 
            // cbForm
            // 
            this.cbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForm.FormattingEnabled = true;
            this.cbForm.Items.AddRange(new object[] {
            "Альбомная",
            "Книжная"});
            this.cbForm.Location = new System.Drawing.Point(120, 88);
            this.cbForm.Name = "cbForm";
            this.cbForm.Size = new System.Drawing.Size(222, 21);
            this.cbForm.Sorted = true;
            this.cbForm.TabIndex = 159;
            // 
            // chbGoodCode
            // 
            this.chbGoodCode.AutoSize = true;
            this.chbGoodCode.Location = new System.Drawing.Point(14, 442);
            this.chbGoodCode.Name = "chbGoodCode";
            this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
            this.chbGoodCode.TabIndex = 158;
            this.chbGoodCode.Text = "Отображать код товара ";
            this.chbGoodCode.UseVisualStyleBackColor = true;
            // 
            // repTypeComboBox
            // 
            this.repTypeComboBox.FormattingEnabled = true;
            this.repTypeComboBox.Items.AddRange(new object[] {
            "Базовый",
            "Оптовый НДС",
            "Розничный НДС"});
            this.repTypeComboBox.Location = new System.Drawing.Point(120, 61);
            this.repTypeComboBox.Name = "repTypeComboBox";
            this.repTypeComboBox.Size = new System.Drawing.Size(222, 21);
            this.repTypeComboBox.TabIndex = 161;
            this.repTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.repTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "Вид отчета:";
            // 
            // vatCheckedListBox
            // 
            this.vatCheckedListBox.FormattingEnabled = true;
            this.vatCheckedListBox.Items.AddRange(new object[] {
            "Без НДС",
            "НДС 10%",
            "НДС 18%"});
            this.vatCheckedListBox.Location = new System.Drawing.Point(348, 61);
            this.vatCheckedListBox.Name = "vatCheckedListBox";
            this.vatCheckedListBox.Size = new System.Drawing.Size(261, 49);
            this.vatCheckedListBox.TabIndex = 163;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 164;
            this.label3.Text = "НДС:";
            // 
            // auCheckBox
            // 
            this.auCheckBox.AutoSize = true;
            this.auCheckBox.Location = new System.Drawing.Point(259, 442);
            this.auCheckBox.Name = "auCheckBox";
            this.auCheckBox.Size = new System.Drawing.Size(234, 17);
            this.auCheckBox.TabIndex = 165;
            this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
            this.auCheckBox.UseVisualStyleBackColor = true;
            // 
            // ggCheckBox
            // 
            this.ggCheckBox.AutoSize = true;
            this.ggCheckBox.Location = new System.Drawing.Point(14, 465);
            this.ggCheckBox.Name = "ggCheckBox";
            this.ggCheckBox.Size = new System.Drawing.Size(148, 17);
            this.ggCheckBox.TabIndex = 166;
            this.ggCheckBox.Text = "Группировать по товару";
            this.ggCheckBox.UseVisualStyleBackColor = true;
            // 
            // MoveGoodsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 520);
            this.Controls.Add(this.ggCheckBox);
            this.Controls.Add(this.auCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vatCheckedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.repTypeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbForm);
            this.Controls.Add(this.chbGoodCode);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbPeriod);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.chkUseLotDate);
            this.Controls.Add(this.cbSort);
            this.Controls.Add(this.checkMove);
            this.Controls.Add(this.multiProducer);
            this.Controls.Add(this.checkInvoice);
            this.Controls.Add(this.periodLot);
            this.Controls.Add(this.multiGoods);
            this.Controls.Add(this.multiContractor);
            this.Controls.Add(this.multiStore);
            this.Controls.Add(this.multiKind);
            this.Name = "MoveGoodsParams";
            this.Load += new System.EventHandler(this.MoveGoodsParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MoveGoodsParams_FormClosed);
            this.Controls.SetChildIndex(this.multiKind, 0);
            this.Controls.SetChildIndex(this.multiStore, 0);
            this.Controls.SetChildIndex(this.multiContractor, 0);
            this.Controls.SetChildIndex(this.multiGoods, 0);
            this.Controls.SetChildIndex(this.periodLot, 0);
            this.Controls.SetChildIndex(this.checkInvoice, 0);
            this.Controls.SetChildIndex(this.multiProducer, 0);
            this.Controls.SetChildIndex(this.checkMove, 0);
            this.Controls.SetChildIndex(this.cbSort, 0);
            this.Controls.SetChildIndex(this.chkUseLotDate, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.lbPeriod, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.chbGoodCode, 0);
            this.Controls.SetChildIndex(this.cbForm, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.repTypeComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.vatCheckedListBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.auCheckBox, 0);
            this.Controls.SetChildIndex(this.ggCheckBox, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private UCPluginMultiSelect multiGoods;
		private UCPluginMultiSelect multiStore;
		private UCPluginMultiSelect multiProducer;
		private UCPluginMultiSelect multiContractor;
		private UCPluginMultiSelect multiKind;
		private UCPeriod ucPeriod;
		private UCPeriod periodLot;
		private System.Windows.Forms.Label lbPeriod;
		private System.Windows.Forms.CheckBox checkMove;
		private System.Windows.Forms.CheckBox chkUseLotDate;
		private System.Windows.Forms.CheckBox checkInvoice;
		private System.Windows.Forms.ComboBox cbSort;
		private System.Windows.Forms.CheckBox checkPreview;
		
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbForm;
        private System.Windows.Forms.CheckBox chbGoodCode;
		private System.Windows.Forms.ComboBox repTypeComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox vatCheckedListBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox auCheckBox;
		private System.Windows.Forms.CheckBox ggCheckBox;
		
		
	}
}