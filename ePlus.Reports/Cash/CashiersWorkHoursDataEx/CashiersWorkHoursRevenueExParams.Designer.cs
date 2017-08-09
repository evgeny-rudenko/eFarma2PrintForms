using System.Drawing;
using System.Windows.Forms;
using ePlus.MetaData.Controls;

namespace CashiersWorkHoursDataEx
{
  partial class CashiersWorkHoursRevenueExParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashiersWorkHoursRevenueExParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.ucDrugstores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucCashiers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucType = new ePlus.MetaData.Controls.ComboBoxEx();
			this.label2 = new System.Windows.Forms.Label();
			this.ucTypeCalculation = new ePlus.MetaData.Controls.ComboBoxEx();
			this.label3 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(163, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(238, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 403);
			this.panel1.Size = new System.Drawing.Size(316, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(316, 25);
			this.toolStrip1.TabIndex = 23;
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
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(76, 92);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(229, 21);
			this.ucPeriod.TabIndex = 196;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 195;
			this.label1.Text = "Период:";
			// 
			// ucDrugstores
			// 
			this.ucDrugstores.AllowSaveState = true;
			this.ucDrugstores.Caption = "Аптеки";
			this.ucDrugstores.Location = new System.Drawing.Point(16, 124);
			this.ucDrugstores.Mnemocode = "DRUGSTORE";
			this.ucDrugstores.MultiSelect = true;
			this.ucDrugstores.Name = "ucDrugstores";
			this.ucDrugstores.Pinnable = false;
			this.ucDrugstores.Size = new System.Drawing.Size(284, 124);
			this.ucDrugstores.TabIndex = 197;
			this.ucDrugstores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
			// 
			// ucCashiers
			// 
			this.ucCashiers.AllowSaveState = true;
			this.ucCashiers.Caption = "Кассиры";
			this.ucCashiers.Location = new System.Drawing.Point(16, 254);
			this.ucCashiers.Mnemocode = "USERS";
			this.ucCashiers.MultiSelect = true;
			this.ucCashiers.Name = "ucCashiers";
			this.ucCashiers.Pinnable = false;
			this.ucCashiers.Size = new System.Drawing.Size(284, 124);
			this.ucCashiers.TabIndex = 198;
			// 
			// ucType
			// 
			this.ucType.FormattingEnabled = true;
			this.ucType.Items.AddRange(new object[] {
            "1. Выручка",
            "2. Количество чеков",
            "3. Количество штук товара"});
			this.ucType.Location = new System.Drawing.Point(76, 35);
			this.ucType.Name = "ucType";
			this.ucType.Size = new System.Drawing.Size(222, 21);
			this.ucType.TabIndex = 199;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 200;
			this.label2.Text = "Вид отчета";
			// 
			// ucTypeCalculation
			// 
			this.ucTypeCalculation.FormattingEnabled = true;
			this.ucTypeCalculation.Items.AddRange(new object[] {
            "1. Все",
            "2. Наличный расчет",
            "3. Безналичный расчет"});
			this.ucTypeCalculation.Location = new System.Drawing.Point(87, 62);
			this.ucTypeCalculation.Name = "ucTypeCalculation";
			this.ucTypeCalculation.Size = new System.Drawing.Size(211, 21);
			this.ucTypeCalculation.TabIndex = 201;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 13);
			this.label3.TabIndex = 202;
			this.label3.Text = "Вид расчета";
			// 
			// CashiersWorkHoursRevenueExParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(316, 432);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ucTypeCalculation);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ucType);
			this.Controls.Add(this.ucCashiers);
			this.Controls.Add(this.ucDrugstores);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "CashiersWorkHoursRevenueExParams";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CashiersWorkHoursDataExParams_FormClosed);
			this.Load += new System.EventHandler(this.CashiersWorkHoursDataExParams_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.ucDrugstores, 0);
			this.Controls.SetChildIndex(this.ucCashiers, 0);
			this.Controls.SetChildIndex(this.ucType, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.ucTypeCalculation, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugstores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucCashiers;
    private ePlus.MetaData.Controls.ComboBoxEx ucType;
    private System.Windows.Forms.Label label2;
		private ePlus.MetaData.Controls.ComboBoxEx ucTypeCalculation;
    private Label label3;
  }
}