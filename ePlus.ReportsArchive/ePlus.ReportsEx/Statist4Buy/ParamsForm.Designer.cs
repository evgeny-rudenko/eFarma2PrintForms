using ePlus.CommonEx.Controls;
using ePlus.MetaData.Client;
using UCPeriod=ePlus.MetaData.Controls.UCPeriod;

namespace Statist4Buy
{
  partial class ParamsForm
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
      this.bOK = new System.Windows.Forms.Button();
      this.bClose = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.period = new ePlus.MetaData.Controls.UCPeriod();
      this.stores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.nbRemDays = new ePlus.CommonEx.Controls.ePlusNumericBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.nbOrderDays = new ePlus.CommonEx.Controls.ePlusNumericBox();
      this.gbTopN = new System.Windows.Forms.GroupBox();
      this.rb100 = new System.Windows.Forms.RadioButton();
      this.rb200 = new System.Windows.Forms.RadioButton();
      this.rb300 = new System.Windows.Forms.RadioButton();
      this.rb400 = new System.Windows.Forms.RadioButton();
      this.rb500 = new System.Windows.Forms.RadioButton();
      this.rb1000 = new System.Windows.Forms.RadioButton();
      this.rbAll = new System.Windows.Forms.RadioButton();
      this.gbSort = new System.Windows.Forms.GroupBox();
      this.rbRem = new System.Windows.Forms.RadioButton();
      this.rbGoods = new System.Windows.Forms.RadioButton();
      this.gbTopN.SuspendLayout();
      this.gbSort.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bOK.Location = new System.Drawing.Point(440, 278);
      this.bOK.Name = "bOK";
      this.bOK.Size = new System.Drawing.Size(75, 23);
      this.bOK.TabIndex = 0;
      this.bOK.Text = "OK";
      this.bOK.UseVisualStyleBackColor = true;
      this.bOK.Click += new System.EventHandler(this.bOK_Click);
      // 
      // bClose
      // 
      this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bClose.Location = new System.Drawing.Point(521, 278);
      this.bClose.Name = "bClose";
      this.bClose.Size = new System.Drawing.Size(75, 23);
      this.bClose.TabIndex = 1;
      this.bClose.Text = "Закрыть";
      this.bClose.UseVisualStyleBackColor = true;
      this.bClose.Click += new System.EventHandler(this.bClose_Click);
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
      // period
      // 
      this.period.DateFrom = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
      this.period.DateTo = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
      this.period.Location = new System.Drawing.Point(67, 6);
      this.period.Name = "period";
      this.period.Size = new System.Drawing.Size(222, 21);
      this.period.TabIndex = 0;
      // 
      // stores
      // 
      this.stores.AllowSaveState = false;
      this.stores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.stores.Caption = "Склады";
      this.stores.Location = new System.Drawing.Point(15, 85);
      this.stores.Mnemocode = "STORE";
      this.stores.Name = "stores";
      this.stores.Size = new System.Drawing.Size(389, 187);
      this.stores.TabIndex = 1;
      // 
      // nbRemDays
      // 
      this.nbRemDays.DecimalPlaces = 0;
      this.nbRemDays.DecimalSeparator = '.';
      this.nbRemDays.Location = new System.Drawing.Point(163, 33);
      this.nbRemDays.MaxLength = 3;
      this.nbRemDays.Name = "nbRemDays";
      this.nbRemDays.Positive = true;
      this.nbRemDays.Size = new System.Drawing.Size(126, 20);
      this.nbRemDays.TabIndex = 3;
      this.nbRemDays.Text = "0";
      this.nbRemDays.ThousandSeparator = '\0';
      this.nbRemDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
      this.nbRemDays.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(145, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Количество дней остатков:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 62);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(135, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Количество дней заявки:";
      // 
      // nbOrderDays
      // 
      this.nbOrderDays.DecimalPlaces = 0;
      this.nbOrderDays.DecimalSeparator = '.';
      this.nbOrderDays.Location = new System.Drawing.Point(163, 59);
      this.nbOrderDays.MaxLength = 3;
      this.nbOrderDays.Name = "nbOrderDays";
      this.nbOrderDays.Positive = true;
      this.nbOrderDays.Size = new System.Drawing.Size(126, 20);
      this.nbOrderDays.TabIndex = 6;
      this.nbOrderDays.Text = "0";
      this.nbOrderDays.ThousandSeparator = '\0';
      this.nbOrderDays.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
      this.nbOrderDays.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      // 
      // gbTopN
      // 
      this.gbTopN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.gbTopN.Controls.Add(this.rb100);
      this.gbTopN.Controls.Add(this.rb200);
      this.gbTopN.Controls.Add(this.rb300);
      this.gbTopN.Controls.Add(this.rb400);
      this.gbTopN.Controls.Add(this.rb500);
      this.gbTopN.Controls.Add(this.rb1000);
      this.gbTopN.Controls.Add(this.rbAll);
      this.gbTopN.Location = new System.Drawing.Point(411, 85);
      this.gbTopN.Name = "gbTopN";
      this.gbTopN.Size = new System.Drawing.Size(185, 185);
      this.gbTopN.TabIndex = 8;
      this.gbTopN.TabStop = false;
      this.gbTopN.Text = "Количество строк";
      // 
      // rb100
      // 
      this.rb100.AutoSize = true;
      this.rb100.Location = new System.Drawing.Point(6, 157);
      this.rb100.Name = "rb100";
      this.rb100.Size = new System.Drawing.Size(43, 17);
      this.rb100.TabIndex = 8;
      this.rb100.Text = "100";
      this.rb100.UseVisualStyleBackColor = true;
      // 
      // rb200
      // 
      this.rb200.AutoSize = true;
      this.rb200.Location = new System.Drawing.Point(6, 134);
      this.rb200.Name = "rb200";
      this.rb200.Size = new System.Drawing.Size(43, 17);
      this.rb200.TabIndex = 7;
      this.rb200.Text = "200";
      this.rb200.UseVisualStyleBackColor = true;
      // 
      // rb300
      // 
      this.rb300.AutoSize = true;
      this.rb300.Location = new System.Drawing.Point(6, 111);
      this.rb300.Name = "rb300";
      this.rb300.Size = new System.Drawing.Size(43, 17);
      this.rb300.TabIndex = 6;
      this.rb300.Text = "300";
      this.rb300.UseVisualStyleBackColor = true;
      // 
      // rb400
      // 
      this.rb400.AutoSize = true;
      this.rb400.Location = new System.Drawing.Point(6, 88);
      this.rb400.Name = "rb400";
      this.rb400.Size = new System.Drawing.Size(43, 17);
      this.rb400.TabIndex = 5;
      this.rb400.Text = "400";
      this.rb400.UseVisualStyleBackColor = true;
      // 
      // rb500
      // 
      this.rb500.AutoSize = true;
      this.rb500.Location = new System.Drawing.Point(6, 65);
      this.rb500.Name = "rb500";
      this.rb500.Size = new System.Drawing.Size(43, 17);
      this.rb500.TabIndex = 4;
      this.rb500.Text = "500";
      this.rb500.UseVisualStyleBackColor = true;
      // 
      // rb1000
      // 
      this.rb1000.AutoSize = true;
      this.rb1000.Location = new System.Drawing.Point(6, 42);
      this.rb1000.Name = "rb1000";
      this.rb1000.Size = new System.Drawing.Size(49, 17);
      this.rb1000.TabIndex = 3;
      this.rb1000.Text = "1000";
      this.rb1000.UseVisualStyleBackColor = true;
      // 
      // rbAll
      // 
      this.rbAll.AutoSize = true;
      this.rbAll.Checked = true;
      this.rbAll.Location = new System.Drawing.Point(6, 19);
      this.rbAll.Name = "rbAll";
      this.rbAll.Size = new System.Drawing.Size(82, 17);
      this.rbAll.TabIndex = 2;
      this.rbAll.TabStop = true;
      this.rbAll.Text = "Все строки";
      this.rbAll.UseVisualStyleBackColor = true;
      // 
      // gbSort
      // 
      this.gbSort.Controls.Add(this.rbRem);
      this.gbSort.Controls.Add(this.rbGoods);
      this.gbSort.Location = new System.Drawing.Point(295, 6);
      this.gbSort.Name = "gbSort";
      this.gbSort.Size = new System.Drawing.Size(301, 73);
      this.gbSort.TabIndex = 9;
      this.gbSort.TabStop = false;
      this.gbSort.Text = "Сотрировка";
      // 
      // rbRem
      // 
      this.rbRem.AutoSize = true;
      this.rbRem.Checked = true;
      this.rbRem.Location = new System.Drawing.Point(6, 42);
      this.rbRem.Name = "rbRem";
      this.rbRem.Size = new System.Drawing.Size(157, 17);
      this.rbRem.TabIndex = 1;
      this.rbRem.TabStop = true;
      this.rbRem.Text = "Общий остаток на складе";
      this.rbRem.UseVisualStyleBackColor = true;
      // 
      // rbGoods
      // 
      this.rbGoods.AutoSize = true;
      this.rbGoods.Location = new System.Drawing.Point(6, 19);
      this.rbGoods.Name = "rbGoods";
      this.rbGoods.Size = new System.Drawing.Size(163, 17);
      this.rbGoods.TabIndex = 0;
      this.rbGoods.Text = "Медикаменты по алфавиту";
      this.rbGoods.UseVisualStyleBackColor = true;
      // 
      // ParamsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(608, 313);
      this.Controls.Add(this.gbSort);
      this.Controls.Add(this.gbTopN);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.nbOrderDays);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.period);
      this.Controls.Add(this.stores);
      this.Controls.Add(this.bClose);
      this.Controls.Add(this.bOK);
      this.Controls.Add(this.nbRemDays);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MinimumSize = new System.Drawing.Size(614, 281);
      this.Name = "ParamsForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Параметры отчета \"Статистика для закупок\"";
      this.gbTopN.ResumeLayout(false);
      this.gbTopN.PerformLayout();
      this.gbSort.ResumeLayout(false);
      this.gbSort.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button bOK;
    private System.Windows.Forms.Button bClose;
    private UCPeriod period;
    private UCPluginMultiSelect stores;
    private System.Windows.Forms.Label label1;
    private ePlusNumericBox nbRemDays;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private ePlusNumericBox nbOrderDays;
    private System.Windows.Forms.GroupBox gbTopN;
    private System.Windows.Forms.GroupBox gbSort;
    private System.Windows.Forms.RadioButton rbGoods;
    private System.Windows.Forms.RadioButton rb100;
    private System.Windows.Forms.RadioButton rb200;
    private System.Windows.Forms.RadioButton rb300;
    private System.Windows.Forms.RadioButton rb400;
    private System.Windows.Forms.RadioButton rb500;
    private System.Windows.Forms.RadioButton rb1000;
    private System.Windows.Forms.RadioButton rbAll;
    private System.Windows.Forms.RadioButton rbRem;
  }
}