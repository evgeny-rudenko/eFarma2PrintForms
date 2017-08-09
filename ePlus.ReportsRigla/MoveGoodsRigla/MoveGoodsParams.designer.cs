using ePlus.MetaData.Client;

namespace RCBMoveGoods_Rigla
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
            this.checkSeries = new System.Windows.Forms.CheckBox();
            this.checkDoc = new System.Windows.Forms.CheckBox();
            this.checkInvoice = new System.Windows.Forms.CheckBox();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.checkPreview = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(0, 402);
            this.panel1.Size = new System.Drawing.Size(646, 29);
            // 
            // multiGoods
            // 
            this.multiGoods.AllowSaveState = false;
            this.multiGoods.Caption = "";
            this.multiGoods.Location = new System.Drawing.Point(12, 252);
            this.multiGoods.Mnemocode = "GOODS2";
            this.multiGoods.Name = "multiGoods";
            this.multiGoods.Pinnable = false;
            this.multiGoods.Size = new System.Drawing.Size(632, 87);
            this.multiGoods.TabIndex = 0;
            // 
            // multiStore
            // 
            this.multiStore.AllowSaveState = false;
            this.multiStore.Caption = "";
            this.multiStore.Location = new System.Drawing.Point(12, 159);
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
            this.multiProducer.Location = new System.Drawing.Point(12, 67);
            this.multiProducer.Mnemocode = "PRODUCER";
            this.multiProducer.Name = "multiProducer";
            this.multiProducer.Pinnable = false;
            this.multiProducer.Size = new System.Drawing.Size(328, 86);
            this.multiProducer.TabIndex = 4;
            // 
            // multiContractor
            // 
            this.multiContractor.AccessibleDescription = "";
            this.multiContractor.AccessibleName = "";
            this.multiContractor.AllowSaveState = false;
            this.multiContractor.Caption = "Поставщики";
            this.multiContractor.Location = new System.Drawing.Point(346, 67);
            this.multiContractor.Mnemocode = "CONTRACTOR";
            this.multiContractor.Name = "multiContractor";
            this.multiContractor.Pinnable = false;
            this.multiContractor.Size = new System.Drawing.Size(298, 86);
            this.multiContractor.TabIndex = 5;
            this.multiContractor.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.multiContractor_BeforePluginShow);
            // 
            // multiKind
            // 
            this.multiKind.AllowSaveState = false;
            this.multiKind.Caption = "";
            this.multiKind.Location = new System.Drawing.Point(346, 159);
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
            this.ucPeriod.Location = new System.Drawing.Point(63, 37);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 7;
            // 
            // periodLot
            // 
            this.periodLot.DateFrom = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.DateTo = new System.DateTime(2008, 10, 1, 12, 1, 38, 265);
            this.periodLot.Enabled = false;
            this.periodLot.Location = new System.Drawing.Point(160, 345);
            this.periodLot.Name = "periodLot";
            this.periodLot.Size = new System.Drawing.Size(222, 21);
            this.periodLot.TabIndex = 8;
            // 
            // lbPeriod
            // 
            this.lbPeriod.AutoSize = true;
            this.lbPeriod.Location = new System.Drawing.Point(12, 42);
            this.lbPeriod.Name = "lbPeriod";
            this.lbPeriod.Size = new System.Drawing.Size(45, 13);
            this.lbPeriod.TabIndex = 9;
            this.lbPeriod.Text = "Период";
            // 
            // checkMove
            // 
            this.checkMove.Checked = true;
            this.checkMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMove.Location = new System.Drawing.Point(302, 37);
            this.checkMove.Name = "checkMove";
            this.checkMove.Size = new System.Drawing.Size(214, 24);
            this.checkMove.TabIndex = 10;
            this.checkMove.Text = "Не показывать товар без движения";
            // 
            // chkUseLotDate
            // 
            this.chkUseLotDate.AutoSize = true;
            this.chkUseLotDate.Location = new System.Drawing.Point(12, 345);
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
            this.checkSeries.Location = new System.Drawing.Point(112, 368);
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
            this.checkDoc.Location = new System.Drawing.Point(169, 368);
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
            this.checkInvoice.Location = new System.Drawing.Point(12, 368);
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
            this.cbSort.Location = new System.Drawing.Point(402, 345);
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
            // MoveGoodsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 431);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbPeriod);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.chkUseLotDate);
            this.Controls.Add(this.cbSort);
            this.Controls.Add(this.checkMove);
            this.Controls.Add(this.checkSeries);
            this.Controls.Add(this.checkDoc);
            this.Controls.Add(this.multiProducer);
            this.Controls.Add(this.checkInvoice);
            this.Controls.Add(this.periodLot);
            this.Controls.Add(this.multiGoods);
            this.Controls.Add(this.multiContractor);
            this.Controls.Add(this.multiStore);
            this.Controls.Add(this.multiKind);
            this.Name = "MoveGoodsParams";
            this.Load += new System.EventHandler(this.MoveGoodsParams_Load);
            this.Controls.SetChildIndex(this.multiKind, 0);
            this.Controls.SetChildIndex(this.multiStore, 0);
            this.Controls.SetChildIndex(this.multiContractor, 0);
            this.Controls.SetChildIndex(this.multiGoods, 0);
            this.Controls.SetChildIndex(this.periodLot, 0);
            this.Controls.SetChildIndex(this.checkInvoice, 0);
            this.Controls.SetChildIndex(this.multiProducer, 0);
            this.Controls.SetChildIndex(this.checkDoc, 0);
            this.Controls.SetChildIndex(this.checkSeries, 0);
            this.Controls.SetChildIndex(this.checkMove, 0);
            this.Controls.SetChildIndex(this.cbSort, 0);
            this.Controls.SetChildIndex(this.chkUseLotDate, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.lbPeriod, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
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
		private System.Windows.Forms.CheckBox checkSeries;
		private System.Windows.Forms.CheckBox checkDoc;
		private System.Windows.Forms.CheckBox checkInvoice;
		private System.Windows.Forms.ComboBox cbSort;
		private System.Windows.Forms.CheckBox checkPreview;
		
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		
		
	}
}