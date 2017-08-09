using ePlus.MetaData.Client;

namespace MoveGoodsEsEx
{
	partial class MoveGoodsEsParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveGoodsEsParams));
            this.multiGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiProducer = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.multiKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.periodLot = new ePlus.MetaData.Client.UCPeriod();
            this.lbPeriod = new System.Windows.Forms.Label();
            this.checkMove = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUseLotDate = new System.Windows.Forms.CheckBox();
            this.checkSeries = new System.Windows.Forms.CheckBox();
            this.checkDoc = new System.Windows.Forms.CheckBox();
            this.checkInvoice = new System.Windows.Forms.CheckBox();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.checkPreview = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(493, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(568, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 395);
            this.panel1.Size = new System.Drawing.Size(646, 29);
            // 
            // multiGoods
            // 
            this.multiGoods.AllowSaveState = false;
            this.multiGoods.Caption = "";
            this.multiGoods.Location = new System.Drawing.Point(12, 264);
            this.multiGoods.Mnemocode = "ES_EF2";
            this.multiGoods.Name = "multiGoods";
            this.multiGoods.Size = new System.Drawing.Size(632, 87);
            this.multiGoods.TabIndex = 0;
            // 
            // multiStore
            // 
            this.multiStore.AllowSaveState = false;
            this.multiStore.Caption = "";
            this.multiStore.Location = new System.Drawing.Point(12, 171);
            this.multiStore.Mnemocode = "STORE";
            this.multiStore.Name = "multiStore";
            this.multiStore.Size = new System.Drawing.Size(328, 87);
            this.multiStore.TabIndex = 3;
            // 
            // multiProducer
            // 
            this.multiProducer.AllowSaveState = false;
            this.multiProducer.Caption = "";
            this.multiProducer.Location = new System.Drawing.Point(12, 79);
            this.multiProducer.Mnemocode = "ES_PRODUCER";
            this.multiProducer.Name = "multiProducer";
            this.multiProducer.Size = new System.Drawing.Size(328, 86);
            this.multiProducer.TabIndex = 4;
            // 
            // multiContractor
            // 
            this.multiContractor.AllowSaveState = false;
            this.multiContractor.Caption = "";
            this.multiContractor.Location = new System.Drawing.Point(346, 79);
            this.multiContractor.Mnemocode = "CONTRACTOR";
            this.multiContractor.Name = "multiContractor";
            this.multiContractor.Size = new System.Drawing.Size(298, 86);
            this.multiContractor.TabIndex = 5;
            // 
            // multiKind
            // 
            this.multiKind.AllowSaveState = false;
            this.multiKind.Caption = "";
            this.multiKind.Location = new System.Drawing.Point(346, 171);
            this.multiKind.Mnemocode = "GOODS_KIND";
            this.multiKind.Name = "multiKind";
            this.multiKind.Size = new System.Drawing.Size(298, 87);
            this.multiKind.TabIndex = 6;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
            this.ucPeriod.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 250);
            this.ucPeriod.Location = new System.Drawing.Point(57, 13);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 7;
            // 
            // periodLot
            // 
            this.periodLot.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.Enabled = false;
            this.periodLot.Location = new System.Drawing.Point(160, 357);
            this.periodLot.Name = "periodLot";
            this.periodLot.Size = new System.Drawing.Size(222, 21);
            this.periodLot.TabIndex = 8;
            // 
            // lbPeriod
            // 
            this.lbPeriod.AutoSize = true;
            this.lbPeriod.Location = new System.Drawing.Point(6, 16);
            this.lbPeriod.Name = "lbPeriod";
            this.lbPeriod.Size = new System.Drawing.Size(45, 13);
            this.lbPeriod.TabIndex = 9;
            this.lbPeriod.Text = "Период";
            // 
            // checkMove
            // 
            this.checkMove.Checked = true;
            this.checkMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMove.Location = new System.Drawing.Point(286, 11);
            this.checkMove.Name = "checkMove";
            this.checkMove.Size = new System.Drawing.Size(214, 24);
            this.checkMove.TabIndex = 10;
            this.checkMove.Text = "Не показывать товар без движения";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbPeriod);
            this.groupBox1.Controls.Add(this.ucPeriod);
            this.groupBox1.Controls.Add(this.checkMove);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 39);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // chkUseLotDate
            // 
            this.chkUseLotDate.AutoSize = true;
            this.chkUseLotDate.Location = new System.Drawing.Point(12, 357);
            this.chkUseLotDate.Name = "chkUseLotDate";
            this.chkUseLotDate.Size = new System.Drawing.Size(146, 17);
            this.chkUseLotDate.TabIndex = 152;
            this.chkUseLotDate.Text = "Период прихода товара";
            this.chkUseLotDate.UseVisualStyleBackColor = true;
            this.chkUseLotDate.CheckedChanged += new System.EventHandler(this.chkUseLotDate_CheckedChanged);
            // 
            // checkSeries
            // 
            this.checkSeries.AutoSize = true;
            this.checkSeries.Location = new System.Drawing.Point(112, 380);
            this.checkSeries.Name = "checkSeries";
            this.checkSeries.Size = new System.Drawing.Size(57, 17);
            this.checkSeries.TabIndex = 149;
            this.checkSeries.Text = "Серии";
            this.checkSeries.UseVisualStyleBackColor = true;
            this.checkSeries.Visible = false;
            // 
            // checkDoc
            // 
            this.checkDoc.AutoSize = true;
            this.checkDoc.Location = new System.Drawing.Point(169, 380);
            this.checkDoc.Name = "checkDoc";
            this.checkDoc.Size = new System.Drawing.Size(77, 17);
            this.checkDoc.TabIndex = 150;
            this.checkDoc.Text = "Документ";
            this.checkDoc.UseVisualStyleBackColor = true;
            this.checkDoc.Visible = false;
            // 
            // checkInvoice
            // 
            this.checkInvoice.AutoSize = true;
            this.checkInvoice.Location = new System.Drawing.Point(12, 380);
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
            "Не сортировать по дате прихода",
            "По возрастанию",
            "По убыванию"});
            this.cbSort.Location = new System.Drawing.Point(402, 357);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(199, 21);
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
            this.toolStrip1.Size = new System.Drawing.Size(646, 25);
            this.toolStrip1.TabIndex = 154;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "Очистить";
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
            // MoveGoodsEsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 424);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.chkUseLotDate);
            this.Controls.Add(this.cbSort);
            this.Controls.Add(this.checkSeries);
            this.Controls.Add(this.checkDoc);
            this.Controls.Add(this.checkInvoice);
            this.Controls.Add(this.multiProducer);
            this.Controls.Add(this.multiGoods);
            this.Controls.Add(this.multiContractor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.multiStore);
            this.Controls.Add(this.multiKind);
            this.Controls.Add(this.periodLot);
            this.Name = "MoveGoodsEsParams";
            this.Controls.SetChildIndex(this.periodLot, 0);
            this.Controls.SetChildIndex(this.multiKind, 0);
            this.Controls.SetChildIndex(this.multiStore, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.multiContractor, 0);
            this.Controls.SetChildIndex(this.multiGoods, 0);
            this.Controls.SetChildIndex(this.multiProducer, 0);
            this.Controls.SetChildIndex(this.checkInvoice, 0);
            this.Controls.SetChildIndex(this.checkDoc, 0);
            this.Controls.SetChildIndex(this.checkSeries, 0);
            this.Controls.SetChildIndex(this.cbSort, 0);
            this.Controls.SetChildIndex(this.chkUseLotDate, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkUseLotDate;
		private System.Windows.Forms.CheckBox checkSeries;
		private System.Windows.Forms.CheckBox checkDoc;
		private System.Windows.Forms.CheckBox checkInvoice;
		private System.Windows.Forms.ComboBox cbSort;
		private System.Windows.Forms.CheckBox checkPreview;
		
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		
		
	}
}