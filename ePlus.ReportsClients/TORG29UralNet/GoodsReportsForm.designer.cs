using ePlus.CommonEx.Controls;

namespace RCRTORG29UralNet
{
  partial class GoodsReportsForm
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
		this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.nbDocNumber = new ePlus.CommonEx.Controls.ePlusNumericBox();
		this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.chkContractorGroup = new System.Windows.Forms.CheckBox();
		this.chkShowDate = new System.Windows.Forms.CheckBox();
		this.chkShowDiscount = new System.Windows.Forms.CheckBox();
		this.bPrint = new System.Windows.Forms.Button();
		this.bClose = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rbDocDate = new System.Windows.Forms.RadioButton();
		this.rbDocType = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chkShowSub = new System.Windows.Forms.CheckBox();
		this.chkShowAdd = new System.Windows.Forms.CheckBox();
		this.chkShortReport = new System.Windows.Forms.CheckBox();
		this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.SuspendLayout();
		// 
		// ucPeriod
		// 
		this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
		this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
		this.ucPeriod.Location = new System.Drawing.Point(132, 3);
		this.ucPeriod.Name = "ucPeriod";
		this.ucPeriod.Size = new System.Drawing.Size(222, 21);
		this.ucPeriod.TabIndex = 1;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(48, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Период:";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 33);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(101, 13);
		this.label2.TabIndex = 105;
		this.label2.Text = "Номер документа:";
		// 
		// nbDocNumber
		// 
		this.nbDocNumber.DecimalPlaces = 0;
		this.nbDocNumber.DecimalSeparator = '.';
		this.nbDocNumber.Location = new System.Drawing.Point(132, 30);
		this.nbDocNumber.MaxLength = 18;
		this.nbDocNumber.Name = "nbDocNumber";
		this.nbDocNumber.Positive = true;
		this.nbDocNumber.Size = new System.Drawing.Size(100, 20);
		this.nbDocNumber.TabIndex = 106;
		this.nbDocNumber.Text = "0";
		this.nbDocNumber.ThousandSeparator = '\0';
		this.nbDocNumber.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
		this.nbDocNumber.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
		// 
		// mpsContractor
		// 
		this.mpsContractor.AllowSaveState = false;
		this.mpsContractor.Caption = "Контрагенты";
		this.mpsContractor.Location = new System.Drawing.Point(12, 56);
		this.mpsContractor.Mnemocode = "CONTRACTOR";
		this.mpsContractor.Name = "mpsContractor";
		this.mpsContractor.Size = new System.Drawing.Size(438, 74);
		this.mpsContractor.TabIndex = 107;
		// 
		// mpsStore
		// 
		this.mpsStore.AllowSaveState = false;
		this.mpsStore.Caption = "Склады";
		this.mpsStore.Location = new System.Drawing.Point(12, 136);
		this.mpsStore.Mnemocode = "STORE";
		this.mpsStore.Name = "mpsStore";
		this.mpsStore.Size = new System.Drawing.Size(438, 74);
		this.mpsStore.TabIndex = 108;
		// 
		// chkContractorGroup
		// 
		this.chkContractorGroup.AutoSize = true;
		this.chkContractorGroup.Location = new System.Drawing.Point(12, 239);
		this.chkContractorGroup.Name = "chkContractorGroup";
		this.chkContractorGroup.Size = new System.Drawing.Size(185, 17);
		this.chkContractorGroup.TabIndex = 109;
		this.chkContractorGroup.Text = "Группировать по контрагентам";
		this.chkContractorGroup.UseVisualStyleBackColor = true;
		// 
		// chkShowDate
		// 
		this.chkShowDate.AutoSize = true;
		this.chkShowDate.Location = new System.Drawing.Point(208, 239);
		this.chkShowDate.Name = "chkShowDate";
		this.chkShowDate.Size = new System.Drawing.Size(193, 17);
		this.chkShowDate.TabIndex = 110;
		this.chkShowDate.Text = "Показывать дату формирования";
		this.chkShowDate.UseVisualStyleBackColor = true;
		// 
		// chkShowDiscount
		// 
		this.chkShowDiscount.AutoSize = true;
		this.chkShowDiscount.Location = new System.Drawing.Point(12, 262);
		this.chkShowDiscount.Name = "chkShowDiscount";
		this.chkShowDiscount.Size = new System.Drawing.Size(173, 17);
		this.chkShowDiscount.TabIndex = 111;
		this.chkShowDiscount.Text = "Показывать колонку Скидка";
		this.chkShowDiscount.UseVisualStyleBackColor = true;
		// 
		// bPrint
		// 
		this.bPrint.Location = new System.Drawing.Point(500, 262);
		this.bPrint.Name = "bPrint";
		this.bPrint.Size = new System.Drawing.Size(75, 23);
		this.bPrint.TabIndex = 112;
		this.bPrint.Text = "Печать";
		this.bPrint.UseVisualStyleBackColor = true;
		this.bPrint.Click += new System.EventHandler(this.bPrint_Click);
		// 
		// bClose
		// 
		this.bClose.Location = new System.Drawing.Point(581, 262);
		this.bClose.Name = "bClose";
		this.bClose.Size = new System.Drawing.Size(75, 23);
		this.bClose.TabIndex = 113;
		this.bClose.Text = "Закрыть";
		this.bClose.UseVisualStyleBackColor = true;
		this.bClose.Click += new System.EventHandler(this.bClose_Click);
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.rbDocDate);
		this.groupBox1.Controls.Add(this.rbDocType);
		this.groupBox1.Location = new System.Drawing.Point(456, 56);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(200, 67);
		this.groupBox1.TabIndex = 114;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Сортировка";
		// 
		// rbDocDate
		// 
		this.rbDocDate.AutoSize = true;
		this.rbDocDate.Location = new System.Drawing.Point(7, 43);
		this.rbDocDate.Name = "rbDocDate";
		this.rbDocDate.Size = new System.Drawing.Size(136, 17);
		this.rbDocDate.TabIndex = 1;
		this.rbDocDate.Text = "По датам документов";
		this.rbDocDate.UseVisualStyleBackColor = true;
		// 
		// rbDocType
		// 
		this.rbDocType.AutoSize = true;
		this.rbDocType.Checked = true;
		this.rbDocType.Location = new System.Drawing.Point(7, 20);
		this.rbDocType.Name = "rbDocType";
		this.rbDocType.Size = new System.Drawing.Size(137, 17);
		this.rbDocType.TabIndex = 0;
		this.rbDocType.TabStop = true;
		this.rbDocType.Text = "По видам документов";
		this.rbDocType.UseVisualStyleBackColor = true;
		// 
		// groupBox2
		// 
		this.groupBox2.Controls.Add(this.chkShowSub);
		this.groupBox2.Controls.Add(this.chkShowAdd);
		this.groupBox2.Location = new System.Drawing.Point(456, 129);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(200, 67);
		this.groupBox2.TabIndex = 115;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Отображение";
		// 
		// chkShowSub
		// 
		this.chkShowSub.AutoSize = true;
		this.chkShowSub.Location = new System.Drawing.Point(7, 42);
		this.chkShowSub.Name = "chkShowSub";
		this.chkShowSub.Size = new System.Drawing.Size(127, 17);
		this.chkShowSub.TabIndex = 1;
		this.chkShowSub.Text = "Показывать расход";
		this.chkShowSub.UseVisualStyleBackColor = true;
		// 
		// chkShowAdd
		// 
		this.chkShowAdd.AutoSize = true;
		this.chkShowAdd.Location = new System.Drawing.Point(7, 19);
		this.chkShowAdd.Name = "chkShowAdd";
		this.chkShowAdd.Size = new System.Drawing.Size(127, 17);
		this.chkShowAdd.TabIndex = 0;
		this.chkShowAdd.Text = "Показывать приход";
		this.chkShowAdd.UseVisualStyleBackColor = true;
		// 
		// chkShortReport
		// 
		this.chkShortReport.AutoSize = true;
		this.chkShortReport.Location = new System.Drawing.Point(208, 262);
		this.chkShortReport.Name = "chkShortReport";
		this.chkShortReport.Size = new System.Drawing.Size(98, 17);
		this.chkShortReport.TabIndex = 116;
		this.chkShortReport.Text = "Краткий отчет";
		this.chkShortReport.UseVisualStyleBackColor = true;
		// 
		// chkRefreshDocMov
		// 
		this.chkRefreshDocMov.AutoSize = true;
		this.chkRefreshDocMov.Location = new System.Drawing.Point(12, 216);
		this.chkRefreshDocMov.Name = "chkRefreshDocMov";
		this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
		this.chkRefreshDocMov.TabIndex = 117;
		this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
		this.chkRefreshDocMov.UseVisualStyleBackColor = true;
		// 
		// GoodsReportsForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(663, 289);
		this.Controls.Add(this.chkRefreshDocMov);
		this.Controls.Add(this.chkShortReport);
		this.Controls.Add(this.groupBox2);
		this.Controls.Add(this.groupBox1);
		this.Controls.Add(this.bClose);
		this.Controls.Add(this.bPrint);
		this.Controls.Add(this.chkShowDiscount);
		this.Controls.Add(this.chkShowDate);
		this.Controls.Add(this.chkContractorGroup);
		this.Controls.Add(this.mpsStore);
		this.Controls.Add(this.mpsContractor);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.ucPeriod);
		this.Controls.Add(this.nbDocNumber);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.Name = "GoodsReportsForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Параметры ТОРГ-29 (Опт-Розница-Наложение)";
		this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoodsReportsForm_FormClosed);
		this.Load += new System.EventHandler(this.GoodsReportsForm_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private ePlusNumericBox nbDocNumber;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
    private System.Windows.Forms.CheckBox chkContractorGroup;
    private System.Windows.Forms.CheckBox chkShowDate;
    private System.Windows.Forms.CheckBox chkShowDiscount;
    private System.Windows.Forms.Button bPrint;
    private System.Windows.Forms.Button bClose;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbDocDate;
    private System.Windows.Forms.RadioButton rbDocType;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox chkShowSub;
    private System.Windows.Forms.CheckBox chkShowAdd;
    private System.Windows.Forms.CheckBox chkShortReport;
    private System.Windows.Forms.CheckBox chkRefreshDocMov;
  }
}