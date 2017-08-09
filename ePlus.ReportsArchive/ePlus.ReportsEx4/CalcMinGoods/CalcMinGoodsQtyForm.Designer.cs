using ePlus.CommonEx.Controls;
//using ePlus.Controls.NewControls;
using ePlus.MetaData.Client;

namespace CalcMinGoods
{
  partial class CalcMinGoodsQtyForm
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.bClose = new System.Windows.Forms.Button();
		this.bCalc = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
		this.label2 = new System.Windows.Forms.Label();
		this.cbGoodsType = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.nbPercent = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cbSort = new System.Windows.Forms.ComboBox();
		this.cbRowCount = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.pluginStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.pluginGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.pluginContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.chkDesc = new System.Windows.Forms.CheckBox();
		this.label7 = new System.Windows.Forms.Label();
		this.nbDays = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.gbType = new System.Windows.Forms.GroupBox();
		this.rbCheckType = new System.Windows.Forms.RadioButton();
		this.rbAllType = new System.Windows.Forms.RadioButton();
		this.chbMovement = new System.Windows.Forms.CheckBox();
		this.chbOut = new System.Windows.Forms.CheckBox();
		this.chbKKM = new System.Windows.Forms.CheckBox();
		this.panel1.SuspendLayout();
		this.gbType.SuspendLayout();
		this.SuspendLayout();
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.bClose);
		this.panel1.Controls.Add(this.bCalc);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 404);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(567, 30);
		this.panel1.TabIndex = 15;
		// 
		// bClose
		// 
		this.bClose.Location = new System.Drawing.Point(426, 3);
		this.bClose.Name = "bClose";
		this.bClose.Size = new System.Drawing.Size(75, 23);
		this.bClose.TabIndex = 1;
		this.bClose.Text = "Закрыть";
		this.bClose.UseVisualStyleBackColor = true;
		this.bClose.Click += new System.EventHandler(this.bClose_Click);
		// 
		// bCalc
		// 
		this.bCalc.Location = new System.Drawing.Point(338, 3);
		this.bCalc.Name = "bCalc";
		this.bCalc.Size = new System.Drawing.Size(82, 23);
		this.bCalc.TabIndex = 0;
		this.bCalc.Text = "Рассчитать";
		this.bCalc.UseVisualStyleBackColor = true;
		this.bCalc.Click += new System.EventHandler(this.bCalc_Click);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(89, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Период продаж:";
		// 
		// ucPeriod
		// 
		this.ucPeriod.DateFrom = new System.DateTime(2007, 9, 14, 11, 26, 9, 31);
		this.ucPeriod.DateTo = new System.DateTime(2007, 9, 14, 11, 26, 9, 31);
		this.ucPeriod.Location = new System.Drawing.Point(107, 12);
		this.ucPeriod.Name = "ucPeriod";
		this.ucPeriod.Size = new System.Drawing.Size(222, 21);
		this.ucPeriod.TabIndex = 1;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 42);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Вид товара:";
		// 
		// cbGoodsType
		// 
		this.cbGoodsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbGoodsType.FormattingEnabled = true;
		this.cbGoodsType.Items.AddRange(new object[] {
            "Все товары",
            "Прибыльные товары",
            "Непродаваемые товары"});
		this.cbGoodsType.Location = new System.Drawing.Point(107, 39);
		this.cbGoodsType.Name = "cbGoodsType";
		this.cbGoodsType.Size = new System.Drawing.Size(222, 21);
		this.cbGoodsType.TabIndex = 3;
		this.cbGoodsType.SelectedIndexChanged += new System.EventHandler(this.cbGoodsType_SelectedIndexChanged);
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(17, 341);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(94, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Процент продаж:";
		// 
		// nbPercent
		// 
		this.nbPercent.DecimalPlaces = 0;
		this.nbPercent.DecimalSeparator = '.';
		this.nbPercent.Location = new System.Drawing.Point(117, 338);
		this.nbPercent.MaxLength = 2;
		this.nbPercent.Name = "nbPercent";
		this.nbPercent.Positive = true;
		this.nbPercent.Size = new System.Drawing.Size(83, 20);
		this.nbPercent.TabIndex = 5;
		this.nbPercent.Text = "0";
		this.nbPercent.ThousandSeparator = ' ';
		this.nbPercent.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbPercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 69);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(90, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Сортировать по:";
		// 
		// cbSort
		// 
		this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbSort.FormattingEnabled = true;
		this.cbSort.Items.AddRange(new object[] {
            "Наименование товара",
            "Рассчитанное минимальное количество",
            "Минимальное количество"});
		this.cbSort.Location = new System.Drawing.Point(107, 66);
		this.cbSort.Name = "cbSort";
		this.cbSort.Size = new System.Drawing.Size(222, 21);
		this.cbSort.TabIndex = 7;
		// 
		// cbRowCount
		// 
		this.cbRowCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRowCount.FormattingEnabled = true;
		this.cbRowCount.Items.AddRange(new object[] {
            "Все",
            "1000",
            "500",
            "100",
            "50",
            "10"});
		this.cbRowCount.Location = new System.Drawing.Point(335, 338);
		this.cbRowCount.Name = "cbRowCount";
		this.cbRowCount.Size = new System.Drawing.Size(76, 21);
		this.cbRowCount.TabIndex = 11;
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(228, 341);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(101, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Количество строк:";
		// 
		// pluginStore
		// 
		this.pluginStore.AllowSaveState = false;
		this.pluginStore.Caption = "Склады";
		this.pluginStore.Location = new System.Drawing.Point(14, 223);
		this.pluginStore.Mnemocode = "STORE";
		this.pluginStore.Name = "pluginStore";
		this.pluginStore.Size = new System.Drawing.Size(221, 109);
		this.pluginStore.TabIndex = 13;
		this.pluginStore.ValuesListChangedNew += new ePlus.MetaData.Client.ValuesListChangedEventHandler(this.pluginStore_ValuesListChangedNew);
		this.pluginStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.pluginStore_BeforePluginShow);
		// 
		// pluginGoods
		// 
		this.pluginGoods.AllowSaveState = false;
		this.pluginGoods.Caption = "Товары";
		this.pluginGoods.Location = new System.Drawing.Point(241, 224);
		this.pluginGoods.Mnemocode = "GOODS2";
		this.pluginGoods.Name = "pluginGoods";
		this.pluginGoods.Size = new System.Drawing.Size(301, 109);
		this.pluginGoods.TabIndex = 14;
		this.pluginGoods.ValuesListChangedNew += new ePlus.MetaData.Client.ValuesListChangedEventHandler(this.pluginGoods_ValuesListChangedNew);
		// 
		// pluginContractor
		// 
		this.pluginContractor.AllowSaveState = false;
		this.pluginContractor.Caption = "Контрагенты";
		this.pluginContractor.Location = new System.Drawing.Point(12, 120);
		this.pluginContractor.Mnemocode = "CONTRACTOR";
		this.pluginContractor.Name = "pluginContractor";
		this.pluginContractor.Size = new System.Drawing.Size(530, 98);
		this.pluginContractor.TabIndex = 12;
		this.pluginContractor.ValuesListChangedNew += new ePlus.MetaData.Client.ValuesListChangedEventHandler(this.pluginContractor_ValuesListChangedNew);
		// 
		// chkDesc
		// 
		this.chkDesc.AutoSize = true;
		this.chkDesc.Location = new System.Drawing.Point(428, 342);
		this.chkDesc.Name = "chkDesc";
		this.chkDesc.Size = new System.Drawing.Size(94, 17);
		this.chkDesc.TabIndex = 16;
		this.chkDesc.Text = "По убыванию";
		this.chkDesc.UseVisualStyleBackColor = true;
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(16, 368);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(372, 13);
		this.label7.TabIndex = 17;
		this.label7.Text = "Количество дней, на которые нужно рассчитать минимальный остаток:";
		// 
		// nbDays
		// 
		this.nbDays.DecimalPlaces = 0;
		this.nbDays.DecimalSeparator = '.';
		this.nbDays.Location = new System.Drawing.Point(428, 365);
		this.nbDays.MaxLength = 3;
		this.nbDays.Name = "nbDays";
		this.nbDays.Positive = true;
		this.nbDays.Size = new System.Drawing.Size(94, 20);
		this.nbDays.TabIndex = 18;
		this.nbDays.Text = "0";
		this.nbDays.ThousandSeparator = ' ';
		this.nbDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbDays.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
		// 
		// gbType
		// 
		this.gbType.Controls.Add(this.rbCheckType);
		this.gbType.Controls.Add(this.rbAllType);
		this.gbType.Controls.Add(this.chbMovement);
		this.gbType.Controls.Add(this.chbOut);
		this.gbType.Controls.Add(this.chbKKM);
		this.gbType.Location = new System.Drawing.Point(335, 13);
		this.gbType.Name = "gbType";
		this.gbType.Size = new System.Drawing.Size(207, 101);
		this.gbType.TabIndex = 141;
		this.gbType.TabStop = false;
		this.gbType.Text = "Вид расхода";
		// 
		// rbCheckType
		// 
		this.rbCheckType.AutoSize = true;
		this.rbCheckType.Location = new System.Drawing.Point(5, 30);
		this.rbCheckType.Name = "rbCheckType";
		this.rbCheckType.Size = new System.Drawing.Size(195, 17);
		this.rbCheckType.TabIndex = 5;
		this.rbCheckType.Text = "Выбранные виды расхода товара";
		this.rbCheckType.UseVisualStyleBackColor = true;
		this.rbCheckType.CheckedChanged += new System.EventHandler(this.rbCheckType_CheckedChanged);
		// 
		// rbAllType
		// 
		this.rbAllType.AutoSize = true;
		this.rbAllType.Checked = true;
		this.rbAllType.Location = new System.Drawing.Point(5, 13);
		this.rbAllType.Name = "rbAllType";
		this.rbAllType.Size = new System.Drawing.Size(155, 17);
		this.rbAllType.TabIndex = 4;
		this.rbAllType.TabStop = true;
		this.rbAllType.Text = "Все виды расхода товара";
		this.rbAllType.UseVisualStyleBackColor = true;
		this.rbAllType.CheckedChanged += new System.EventHandler(this.rbAllType_CheckedChanged);
		// 
		// chbMovement
		// 
		this.chbMovement.AutoSize = true;
		this.chbMovement.Enabled = false;
		this.chbMovement.Location = new System.Drawing.Point(22, 78);
		this.chbMovement.Name = "chbMovement";
		this.chbMovement.Size = new System.Drawing.Size(137, 17);
		this.chbMovement.TabIndex = 3;
		this.chbMovement.Tag = "3";
		this.chbMovement.Text = "Перемещение товара";
		this.chbMovement.UseVisualStyleBackColor = true;
		// 
		// chbOut
		// 
		this.chbOut.AutoSize = true;
		this.chbOut.Enabled = false;
		this.chbOut.Location = new System.Drawing.Point(22, 62);
		this.chbOut.Name = "chbOut";
		this.chbOut.Size = new System.Drawing.Size(140, 17);
		this.chbOut.TabIndex = 2;
		this.chbOut.Tag = "2";
		this.chbOut.Text = "Возвраты поставщику";
		this.chbOut.UseVisualStyleBackColor = true;
		// 
		// chbKKM
		// 
		this.chbKKM.AutoSize = true;
		this.chbKKM.Enabled = false;
		this.chbKKM.Location = new System.Drawing.Point(22, 46);
		this.chbKKM.Name = "chbKKM";
		this.chbKKM.Size = new System.Drawing.Size(178, 17);
		this.chbKKM.TabIndex = 1;
		this.chbKKM.Tag = "1";
		this.chbKKM.Text = "Чеки и расходные накладные";
		this.chbKKM.UseVisualStyleBackColor = true;
		// 
		// CalcMinGoodsQtyForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(567, 434);
		this.Controls.Add(this.gbType);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.nbDays);
		this.Controls.Add(this.chkDesc);
		this.Controls.Add(this.pluginContractor);
		this.Controls.Add(this.pluginGoods);
		this.Controls.Add(this.cbRowCount);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.cbSort);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.cbGoodsType);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.ucPeriod);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.panel1);
		this.Controls.Add(this.nbPercent);
		this.Controls.Add(this.pluginStore);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "CalcMinGoodsQtyForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Рассчет минимального остатка";
		this.panel1.ResumeLayout(false);
		this.gbType.ResumeLayout(false);
		this.gbType.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button bClose;
    private System.Windows.Forms.Button bCalc;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Controls.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbGoodsType;
    private System.Windows.Forms.Label label3;
    private ePlusNumericBox nbPercent;
    private System.Windows.Forms.Label label4;
	  private System.Windows.Forms.ComboBox cbSort;
    private System.Windows.Forms.ComboBox cbRowCount;
    private System.Windows.Forms.Label label6;
    private ePlus.MetaData.Client.UCPluginMultiSelect pluginStore;
    private UCPluginMultiSelect pluginGoods;
    private UCPluginMultiSelect pluginContractor;
    private System.Windows.Forms.CheckBox chkDesc;
    private System.Windows.Forms.Label label7;
    private ePlusNumericBox nbDays;
	  private System.Windows.Forms.GroupBox gbType;
	  private System.Windows.Forms.RadioButton rbCheckType;
	  private System.Windows.Forms.RadioButton rbAllType;
	  private System.Windows.Forms.CheckBox chbMovement;
	  private System.Windows.Forms.CheckBox chbOut;
	  private System.Windows.Forms.CheckBox chbKKM;
    
  }
}