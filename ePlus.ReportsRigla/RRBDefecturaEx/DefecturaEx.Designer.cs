namespace RRBDefecturaEx
{
  partial class DefecturaEx
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefecturaEx));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.auCheckBox = new System.Windows.Forms.CheckBox();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.chbGoodCode = new System.Windows.Forms.CheckBox();
      this.chargeLabel = new System.Windows.Forms.Label();
      this.docsCheckedListBox = new System.Windows.Forms.CheckedListBox();
      this.reserveCheckBox = new System.Windows.Forms.CheckBox();
      this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.minValueTextBox = new System.Windows.Forms.TextBox();
      this.periodLabel = new System.Windows.Forms.Label();
      this.esCheckBox = new System.Windows.Forms.CheckBox();
      this.groupCheckBox = new System.Windows.Forms.CheckBox();
      this.oaCheckBox = new System.Windows.Forms.CheckBox();
      this.sortComboBox = new System.Windows.Forms.ComboBox();
      this.sortLabel = new System.Windows.Forms.Label();
      this.minValueLabel = new System.Windows.Forms.Label();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnCheckAll = new System.Windows.Forms.Button();
      this.cbX = new System.Windows.Forms.CheckBox();
      this.cbB = new System.Windows.Forms.CheckBox();
      this.cbC = new System.Windows.Forms.CheckBox();
      this.cbD = new System.Windows.Forms.CheckBox();
      this.cbA = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbDays = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(434, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(509, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 510);
      this.panel1.Size = new System.Drawing.Size(587, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(587, 25);
      this.toolStrip1.TabIndex = 22;
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
      // auCheckBox
      // 
      this.auCheckBox.AutoSize = true;
      this.auCheckBox.Location = new System.Drawing.Point(294, 359);
      this.auCheckBox.Name = "auCheckBox";
      this.auCheckBox.Size = new System.Drawing.Size(234, 17);
      this.auCheckBox.TabIndex = 145;
      this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
      this.auCheckBox.UseVisualStyleBackColor = true;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склады";
      this.ucStores.Location = new System.Drawing.Point(294, 131);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(281, 85);
      this.ucStores.TabIndex = 144;
      // 
      // ucContractors
      // 
      this.ucContractors.AllowSaveState = true;
      this.ucContractors.Caption = "Аптеки";
      this.ucContractors.Location = new System.Drawing.Point(15, 131);
      this.ucContractors.Mnemocode = "CONTRACTOR";
      this.ucContractors.Name = "ucContractors";
      this.ucContractors.Size = new System.Drawing.Size(273, 85);
      this.ucContractors.TabIndex = 143;
      // 
      // chbGoodCode
      // 
      this.chbGoodCode.AutoSize = true;
      this.chbGoodCode.Location = new System.Drawing.Point(294, 336);
      this.chbGoodCode.Name = "chbGoodCode";
      this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
      this.chbGoodCode.TabIndex = 142;
      this.chbGoodCode.Text = "Отображать код товара ";
      this.chbGoodCode.UseVisualStyleBackColor = true;
      // 
      // chargeLabel
      // 
      this.chargeLabel.AutoSize = true;
      this.chargeLabel.Location = new System.Drawing.Point(407, 40);
      this.chargeLabel.Name = "chargeLabel";
      this.chargeLabel.Size = new System.Drawing.Size(46, 13);
      this.chargeLabel.TabIndex = 141;
      this.chargeLabel.Text = "Расход:";
      // 
      // docsCheckedListBox
      // 
      this.docsCheckedListBox.FormattingEnabled = true;
      this.docsCheckedListBox.Location = new System.Drawing.Point(410, 61);
      this.docsCheckedListBox.Name = "docsCheckedListBox";
      this.docsCheckedListBox.Size = new System.Drawing.Size(165, 64);
      this.docsCheckedListBox.TabIndex = 140;
      this.docsCheckedListBox.ThreeDCheckBoxes = true;
      // 
      // reserveCheckBox
      // 
      this.reserveCheckBox.AutoSize = true;
      this.reserveCheckBox.Location = new System.Drawing.Point(15, 359);
      this.reserveCheckBox.Name = "reserveCheckBox";
      this.reserveCheckBox.Size = new System.Drawing.Size(167, 17);
      this.reserveCheckBox.TabIndex = 139;
      this.reserveCheckBox.Text = "Учитывать товар в резерве";
      this.reserveCheckBox.UseVisualStyleBackColor = true;
      // 
      // toDateTimePicker
      // 
      this.toDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
      this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.toDateTimePicker.Location = new System.Drawing.Point(277, 44);
      this.toDateTimePicker.Name = "toDateTimePicker";
      this.toDateTimePicker.Size = new System.Drawing.Size(113, 20);
      this.toDateTimePicker.TabIndex = 138;
      this.toDateTimePicker.ValueChanged += new System.EventHandler(this.toDateTimePicker_ValueChanged);
      // 
      // fromDateTimePicker
      // 
      this.fromDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
      this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.fromDateTimePicker.Location = new System.Drawing.Point(158, 44);
      this.fromDateTimePicker.Name = "fromDateTimePicker";
      this.fromDateTimePicker.Size = new System.Drawing.Size(113, 20);
      this.fromDateTimePicker.TabIndex = 137;
      this.fromDateTimePicker.ValueChanged += new System.EventHandler(this.fromDateTimePicker_ValueChanged);
      // 
      // minValueTextBox
      // 
      this.minValueTextBox.Location = new System.Drawing.Point(158, 75);
      this.minValueTextBox.Name = "minValueTextBox";
      this.minValueTextBox.Size = new System.Drawing.Size(232, 20);
      this.minValueTextBox.TabIndex = 136;
      this.minValueTextBox.Text = "0";
      this.minValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // periodLabel
      // 
      this.periodLabel.AutoSize = true;
      this.periodLabel.Location = new System.Drawing.Point(12, 48);
      this.periodLabel.Name = "periodLabel";
      this.periodLabel.Size = new System.Drawing.Size(48, 13);
      this.periodLabel.TabIndex = 135;
      this.periodLabel.Text = "Период:";
      // 
      // esCheckBox
      // 
      this.esCheckBox.AutoSize = true;
      this.esCheckBox.Location = new System.Drawing.Point(294, 313);
      this.esCheckBox.Name = "esCheckBox";
      this.esCheckBox.Size = new System.Drawing.Size(134, 17);
      this.esCheckBox.TabIndex = 134;
      this.esCheckBox.Text = "Сформировать по ЕС";
      this.esCheckBox.UseVisualStyleBackColor = true;
      // 
      // groupCheckBox
      // 
      this.groupCheckBox.AutoSize = true;
      this.groupCheckBox.Location = new System.Drawing.Point(15, 336);
      this.groupCheckBox.Name = "groupCheckBox";
      this.groupCheckBox.Size = new System.Drawing.Size(191, 17);
      this.groupCheckBox.TabIndex = 133;
      this.groupCheckBox.Text = "Сворачивать группы по товарам";
      this.groupCheckBox.UseVisualStyleBackColor = true;
      this.groupCheckBox.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
      // 
      // oaCheckBox
      // 
      this.oaCheckBox.AutoSize = true;
      this.oaCheckBox.Location = new System.Drawing.Point(15, 313);
      this.oaCheckBox.Name = "oaCheckBox";
      this.oaCheckBox.Size = new System.Drawing.Size(189, 17);
      this.oaCheckBox.TabIndex = 132;
      this.oaCheckBox.Text = "Только товары с признаком ОА";
      this.oaCheckBox.UseVisualStyleBackColor = true;
      // 
      // sortComboBox
      // 
      this.sortComboBox.FormattingEnabled = true;
      this.sortComboBox.Items.AddRange(new object[] {
            "Товар",
            "Поставщик"});
      this.sortComboBox.Location = new System.Drawing.Point(158, 104);
      this.sortComboBox.Name = "sortComboBox";
      this.sortComboBox.Size = new System.Drawing.Size(232, 21);
      this.sortComboBox.TabIndex = 130;
      // 
      // sortLabel
      // 
      this.sortLabel.AutoSize = true;
      this.sortLabel.Location = new System.Drawing.Point(12, 107);
      this.sortLabel.Name = "sortLabel";
      this.sortLabel.Size = new System.Drawing.Size(70, 13);
      this.sortLabel.TabIndex = 129;
      this.sortLabel.Text = "Сортировка:";
      // 
      // minValueLabel
      // 
      this.minValueLabel.AutoSize = true;
      this.minValueLabel.Location = new System.Drawing.Point(12, 78);
      this.minValueLabel.Name = "minValueLabel";
      this.minValueLabel.Size = new System.Drawing.Size(126, 13);
      this.minValueLabel.TabIndex = 128;
      this.minValueLabel.Text = "Минимальный остаток:";
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товар";
      this.ucGoods.Location = new System.Drawing.Point(15, 222);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(560, 85);
      this.ucGoods.TabIndex = 131;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnCheckAll);
      this.groupBox1.Controls.Add(this.cbX);
      this.groupBox1.Controls.Add(this.cbB);
      this.groupBox1.Controls.Add(this.cbC);
      this.groupBox1.Controls.Add(this.cbD);
      this.groupBox1.Controls.Add(this.cbA);
      this.groupBox1.Location = new System.Drawing.Point(15, 386);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 70);
      this.groupBox1.TabIndex = 148;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Отобразить товары категории:";
      // 
      // btnCheckAll
      // 
      this.btnCheckAll.Enabled = false;
      this.btnCheckAll.Location = new System.Drawing.Point(123, 19);
      this.btnCheckAll.Name = "btnCheckAll";
      this.btnCheckAll.Size = new System.Drawing.Size(66, 40);
      this.btnCheckAll.TabIndex = 5;
      this.btnCheckAll.Text = "Выделить все";
      this.btnCheckAll.UseVisualStyleBackColor = true;
      this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
      // 
      // cbX
      // 
      this.cbX.AutoSize = true;
      this.cbX.Checked = true;
      this.cbX.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbX.Location = new System.Drawing.Point(45, 42);
      this.cbX.Name = "cbX";
      this.cbX.Size = new System.Drawing.Size(33, 17);
      this.cbX.TabIndex = 4;
      this.cbX.Text = "X";
      this.cbX.UseVisualStyleBackColor = true;
      this.cbX.CheckedChanged += new System.EventHandler(this.cbLetter_CheckedChanged);
      // 
      // cbB
      // 
      this.cbB.AutoSize = true;
      this.cbB.Checked = true;
      this.cbB.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbB.Location = new System.Drawing.Point(45, 19);
      this.cbB.Name = "cbB";
      this.cbB.Size = new System.Drawing.Size(33, 17);
      this.cbB.TabIndex = 3;
      this.cbB.Text = "B";
      this.cbB.UseVisualStyleBackColor = true;
      this.cbB.CheckedChanged += new System.EventHandler(this.cbLetter_CheckedChanged);
      // 
      // cbC
      // 
      this.cbC.AutoSize = true;
      this.cbC.Checked = true;
      this.cbC.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbC.Location = new System.Drawing.Point(84, 19);
      this.cbC.Name = "cbC";
      this.cbC.Size = new System.Drawing.Size(33, 17);
      this.cbC.TabIndex = 2;
      this.cbC.Text = "C";
      this.cbC.UseVisualStyleBackColor = true;
      this.cbC.CheckedChanged += new System.EventHandler(this.cbLetter_CheckedChanged);
      // 
      // cbD
      // 
      this.cbD.AutoSize = true;
      this.cbD.Checked = true;
      this.cbD.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbD.Location = new System.Drawing.Point(6, 42);
      this.cbD.Name = "cbD";
      this.cbD.Size = new System.Drawing.Size(34, 17);
      this.cbD.TabIndex = 1;
      this.cbD.Text = "D";
      this.cbD.UseVisualStyleBackColor = true;
      this.cbD.CheckedChanged += new System.EventHandler(this.cbLetter_CheckedChanged);
      // 
      // cbA
      // 
      this.cbA.AutoSize = true;
      this.cbA.Checked = true;
      this.cbA.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbA.Location = new System.Drawing.Point(6, 19);
      this.cbA.Name = "cbA";
      this.cbA.Size = new System.Drawing.Size(33, 17);
      this.cbA.TabIndex = 0;
      this.cbA.Text = "A";
      this.cbA.UseVisualStyleBackColor = true;
      this.cbA.CheckedChanged += new System.EventHandler(this.cbLetter_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.tbDays);
      this.groupBox2.Location = new System.Drawing.Point(294, 386);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(281, 70);
      this.groupBox2.TabIndex = 152;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "С даты последней продажи партии прошло более";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(83, 33);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 13);
      this.label2.TabIndex = 153;
      this.label2.Text = "дней";
      // 
      // tbDays
      // 
      this.tbDays.Location = new System.Drawing.Point(22, 30);
      this.tbDays.Name = "tbDays";
      this.tbDays.Size = new System.Drawing.Size(55, 20);
      this.tbDays.TabIndex = 152;
      this.tbDays.Text = "0";
      this.tbDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // DefecturaEx
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(587, 539);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.auCheckBox);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucContractors);
      this.Controls.Add(this.chbGoodCode);
      this.Controls.Add(this.chargeLabel);
      this.Controls.Add(this.docsCheckedListBox);
      this.Controls.Add(this.reserveCheckBox);
      this.Controls.Add(this.toDateTimePicker);
      this.Controls.Add(this.fromDateTimePicker);
      this.Controls.Add(this.minValueTextBox);
      this.Controls.Add(this.periodLabel);
      this.Controls.Add(this.esCheckBox);
      this.Controls.Add(this.groupCheckBox);
      this.Controls.Add(this.oaCheckBox);
      this.Controls.Add(this.sortComboBox);
      this.Controls.Add(this.sortLabel);
      this.Controls.Add(this.minValueLabel);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.toolStrip1);
      this.Name = "DefecturaEx";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefecturaEx_FormClosed);
      this.Load += new System.EventHandler(this.DefecturaEx_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.minValueLabel, 0);
      this.Controls.SetChildIndex(this.sortLabel, 0);
      this.Controls.SetChildIndex(this.sortComboBox, 0);
      this.Controls.SetChildIndex(this.oaCheckBox, 0);
      this.Controls.SetChildIndex(this.groupCheckBox, 0);
      this.Controls.SetChildIndex(this.esCheckBox, 0);
      this.Controls.SetChildIndex(this.periodLabel, 0);
      this.Controls.SetChildIndex(this.minValueTextBox, 0);
      this.Controls.SetChildIndex(this.fromDateTimePicker, 0);
      this.Controls.SetChildIndex(this.toDateTimePicker, 0);
      this.Controls.SetChildIndex(this.reserveCheckBox, 0);
      this.Controls.SetChildIndex(this.docsCheckedListBox, 0);
      this.Controls.SetChildIndex(this.chargeLabel, 0);
      this.Controls.SetChildIndex(this.chbGoodCode, 0);
      this.Controls.SetChildIndex(this.ucContractors, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.auCheckBox, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.CheckBox auCheckBox;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
    private System.Windows.Forms.CheckBox chbGoodCode;
    private System.Windows.Forms.Label chargeLabel;
    private System.Windows.Forms.CheckedListBox docsCheckedListBox;
    private System.Windows.Forms.CheckBox reserveCheckBox;
    private System.Windows.Forms.DateTimePicker toDateTimePicker;
    private System.Windows.Forms.DateTimePicker fromDateTimePicker;
    private System.Windows.Forms.TextBox minValueTextBox;
    private System.Windows.Forms.Label periodLabel;
    private System.Windows.Forms.CheckBox esCheckBox;
    private System.Windows.Forms.CheckBox groupCheckBox;
    private System.Windows.Forms.CheckBox oaCheckBox;
    private System.Windows.Forms.ComboBox sortComboBox;
    private System.Windows.Forms.Label sortLabel;
    private System.Windows.Forms.Label minValueLabel;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnCheckAll;
    private System.Windows.Forms.CheckBox cbX;
    private System.Windows.Forms.CheckBox cbB;
    private System.Windows.Forms.CheckBox cbC;
    private System.Windows.Forms.CheckBox cbD;
    private System.Windows.Forms.CheckBox cbA;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbDays;
  }
}