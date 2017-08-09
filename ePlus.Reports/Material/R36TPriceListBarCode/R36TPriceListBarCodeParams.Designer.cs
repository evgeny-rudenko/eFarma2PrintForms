namespace R36TPriceListBarCode
{
	partial class F36RPriceListParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F36RPriceListParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.cbForm = new System.Windows.Forms.ComboBox();
      this.cbSort = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.dateDateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(385, 3);
      this.bOK.Size = new System.Drawing.Size(75, 27);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(460, 3);
      this.bClose.Size = new System.Drawing.Size(75, 27);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 345);
      this.panel1.Size = new System.Drawing.Size(538, 33);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(538, 25);
      this.toolStrip1.TabIndex = 6;
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
      // cbForm
      // 
      this.cbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbForm.FormattingEnabled = true;
      this.cbForm.Items.AddRange(new object[] {
            "Краткая",
            "Полная"});
      this.cbForm.Location = new System.Drawing.Point(362, 35);
      this.cbForm.Name = "cbForm";
      this.cbForm.Size = new System.Drawing.Size(153, 21);
      this.cbForm.Sorted = true;
      this.cbForm.TabIndex = 7;
      this.cbForm.SelectedIndexChanged += new System.EventHandler(this.cbForm_SelectedIndexChanged);
      // 
      // cbSort
      // 
      this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbSort.FormattingEnabled = true;
      this.cbSort.Items.AddRange(new object[] {
            "Товар",
            "Поставщик",
            "Дата документа"});
      this.cbSort.Location = new System.Drawing.Point(362, 67);
      this.cbSort.Name = "cbSort";
      this.cbSort.Size = new System.Drawing.Size(153, 21);
      this.cbSort.TabIndex = 8;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 38);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Остатки на дату:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(276, 38);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(83, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Форма отчета:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(289, 70);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(70, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Сортировка:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(16, 91);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(115, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "Показывать колонки";
      // 
      // checkedListBox1
      // 
      this.checkedListBox1.FormattingEnabled = true;
      this.checkedListBox1.Items.AddRange(new object[] {
            "Код",
            "Товар",
            "Производитель",
            "Количество",
            "Ед. изм.",
            "Поставщик",
            "Опт. цена",
            "Розн. цена",
            "Опт. сумма",
            "Розн. сумма",
            "Годен до",
            "Серия/сертификат",
            "Страна",
            "ЖНВЛС",
            "ПКУ",
            "Документ поступления",
            "Резерв"});
      this.checkedListBox1.Location = new System.Drawing.Point(12, 107);
      this.checkedListBox1.Name = "checkedListBox1";
      this.checkedListBox1.Size = new System.Drawing.Size(165, 229);
      this.checkedListBox1.TabIndex = 13;
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = false;
      this.ucGoods.Caption = "";
      this.ucGoods.Location = new System.Drawing.Point(197, 107);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(328, 106);
      this.ucGoods.TabIndex = 14;
      // 
      // ucStore
      // 
      this.ucStore.AllowSaveState = false;
      this.ucStore.Caption = "";
      this.ucStore.Location = new System.Drawing.Point(197, 219);
      this.ucStore.Mnemocode = "STORE";
      this.ucStore.Name = "ucStore";
      this.ucStore.Size = new System.Drawing.Size(328, 114);
      this.ucStore.TabIndex = 15;
      // 
      // dateDateTimePicker
      // 
      this.dateDateTimePicker.Location = new System.Drawing.Point(114, 36);
      this.dateDateTimePicker.Name = "dateDateTimePicker";
      this.dateDateTimePicker.Size = new System.Drawing.Size(143, 20);
      this.dateDateTimePicker.TabIndex = 17;
      // 
      // PriceListParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(538, 378);
      this.Controls.Add(this.dateDateTimePicker);
      this.Controls.Add(this.checkedListBox1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbSort);
      this.Controls.Add(this.cbForm);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.ucStore);
      this.Name = "PriceListParams";
      this.Load += new System.EventHandler(this.PriceListParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PriceListParams_FormClosed);
      this.Controls.SetChildIndex(this.ucStore, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.cbForm, 0);
      this.Controls.SetChildIndex(this.cbSort, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.checkedListBox1, 0);
      this.Controls.SetChildIndex(this.dateDateTimePicker, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ComboBox cbForm;
		private System.Windows.Forms.ComboBox cbSort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private System.Windows.Forms.DateTimePicker dateDateTimePicker;
	}
}