namespace RCBMoveGoodsDK
{
  partial class MoveGoodsDK
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveGoodsDK));
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        this.label1 = new System.Windows.Forms.Label();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucSuppliers = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.cbReportView = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.cbAU = new System.Windows.Forms.CheckBox();
        this.ucIn = new System.Windows.Forms.CheckedListBox();
        this.ucOut = new System.Windows.Forms.CheckedListBox();
        this.label3 = new System.Windows.Forms.Label();
        this.cbAllIn = new System.Windows.Forms.CheckBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.cbAllOut = new System.Windows.Forms.CheckBox();
        this.label6 = new System.Windows.Forms.Label();
        this.checkMove = new System.Windows.Forms.CheckBox();
        this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.panel1.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(505, 3);
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(580, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 518);
        this.panel1.Size = new System.Drawing.Size(658, 29);
        // 
        // toolStrip1
        // 
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
        this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
        this.toolStrip1.Size = new System.Drawing.Size(658, 25);
        this.toolStrip1.TabIndex = 184;
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
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 46);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(51, 13);
        this.label1.TabIndex = 201;
        this.label1.Text = "Период: ";
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
        this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
        this.ucPeriod.Location = new System.Drawing.Point(109, 43);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(228, 21);
        this.ucPeriod.TabIndex = 200;
        // 
        // ucStores
        // 
        this.ucStores.AllowSaveState = true;
        this.ucStores.Caption = "Склады";
        this.ucStores.Location = new System.Drawing.Point(355, 162);
        this.ucStores.Mnemocode = "STORE";
        this.ucStores.Name = "ucStores";
        this.ucStores.Size = new System.Drawing.Size(292, 111);
        this.ucStores.TabIndex = 203;
        this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
        // 
        // ucSuppliers
        // 
        this.ucSuppliers.AllowSaveState = true;
        this.ucSuppliers.Caption = "Поставщики";
        this.ucSuppliers.Location = new System.Drawing.Point(355, 396);
        this.ucSuppliers.Mnemocode = "CONTRACTOR";
        this.ucSuppliers.Name = "ucSuppliers";
        this.ucSuppliers.Size = new System.Drawing.Size(292, 111);
        this.ucSuppliers.TabIndex = 202;
        // 
        // cbReportView
        // 
        this.cbReportView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbReportView.FormattingEnabled = true;
        this.cbReportView.Items.AddRange(new object[] {
            "Краткий",
            "Полный"});
        this.cbReportView.Location = new System.Drawing.Point(109, 75);
        this.cbReportView.MaxDropDownItems = 3;
        this.cbReportView.Name = "cbReportView";
        this.cbReportView.Size = new System.Drawing.Size(222, 21);
        this.cbReportView.TabIndex = 204;
        this.cbReportView.SelectedIndexChanged += new System.EventHandler(this.cbReportView_SelectedIndexChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(13, 78);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(68, 13);
        this.label2.TabIndex = 205;
        this.label2.Text = "Вид отчета: ";
        // 
        // ucGoods
        // 
        this.ucGoods.AllowSaveState = true;
        this.ucGoods.Caption = "Товары";
        this.ucGoods.Location = new System.Drawing.Point(355, 279);
        this.ucGoods.Mnemocode = "GOODS2";
        this.ucGoods.Name = "ucGoods";
        this.ucGoods.Size = new System.Drawing.Size(292, 111);
        this.ucGoods.TabIndex = 206;
        // 
        // cbAU
        // 
        this.cbAU.AutoSize = true;
        this.cbAU.Location = new System.Drawing.Point(12, 334);
        this.cbAU.Name = "cbAU";
        this.cbAU.Size = new System.Drawing.Size(183, 17);
        this.cbAU.TabIndex = 207;
        this.cbAU.Text = "Сворачивать товар по группам";
        this.cbAU.UseVisualStyleBackColor = true;
        // 
        // ucIn
        // 
        this.ucIn.ColumnWidth = 150;
        this.ucIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.ucIn.FormattingEnabled = true;
        this.ucIn.HorizontalScrollbar = true;
        this.ucIn.Items.AddRange(new object[] {
            "ПН",
            "ПТ",
            "ПМП",
            "Возврат от покупателя",
            "Инвентаризация",
            "Переоценка",
            "Ввод остатков",
            "Разукомплектация"});
        this.ucIn.Location = new System.Drawing.Point(12, 130);
        this.ucIn.MultiColumn = true;
        this.ucIn.Name = "ucIn";
        this.ucIn.Size = new System.Drawing.Size(319, 64);
        this.ucIn.TabIndex = 208;
        this.ucIn.ThreeDCheckBoxes = true;
        this.ucIn.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ucIn_ItemCheck);
        // 
        // ucOut
        // 
        this.ucOut.ColumnWidth = 150;
        this.ucOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.ucOut.FormattingEnabled = true;
        this.ucOut.HorizontalScrollbar = true;
        this.ucOut.Items.AddRange(new object[] {
            "Кассовые чеки",
            "РН",
            "ПТ",
            "ПМП",
            "Инвентаризация",
            "Переоценка",
            "Разукомплектация",
            "Возврат поставщику",
            "Списание"});
        this.ucOut.Location = new System.Drawing.Point(12, 241);
        this.ucOut.MultiColumn = true;
        this.ucOut.Name = "ucOut";
        this.ucOut.Size = new System.Drawing.Size(319, 79);
        this.ucOut.TabIndex = 209;
        this.ucOut.ThreeDCheckBoxes = true;
        this.ucOut.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ucOut_ItemCheck);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(13, 114);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(137, 13);
        this.label3.TabIndex = 210;
        this.label3.Text = "Документы для прихода (";
        // 
        // cbAllIn
        // 
        this.cbAllIn.AutoSize = true;
        this.cbAllIn.Location = new System.Drawing.Point(150, 114);
        this.cbAllIn.Name = "cbAllIn";
        this.cbAllIn.Size = new System.Drawing.Size(15, 14);
        this.cbAllIn.TabIndex = 211;
        this.cbAllIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.cbAllIn.UseVisualStyleBackColor = true;
        this.cbAllIn.Click += new System.EventHandler(this.cbAllIn_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(165, 114);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(81, 13);
        this.label4.TabIndex = 212;
        this.label4.Text = "Выделить все)";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(165, 225);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(81, 13);
        this.label5.TabIndex = 215;
        this.label5.Text = "Выделить все)";
        // 
        // cbAllOut
        // 
        this.cbAllOut.AutoSize = true;
        this.cbAllOut.Location = new System.Drawing.Point(150, 225);
        this.cbAllOut.Name = "cbAllOut";
        this.cbAllOut.Size = new System.Drawing.Size(15, 14);
        this.cbAllOut.TabIndex = 214;
        this.cbAllOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.cbAllOut.UseVisualStyleBackColor = true;
        this.cbAllOut.Click += new System.EventHandler(this.cbAllOut_Click);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(13, 225);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(137, 13);
        this.label6.TabIndex = 213;
        this.label6.Text = "Документы для расхода (";
        // 
        // checkMove
        // 
        this.checkMove.Checked = true;
        this.checkMove.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkMove.Location = new System.Drawing.Point(12, 359);
        this.checkMove.Name = "checkMove";
        this.checkMove.Size = new System.Drawing.Size(214, 17);
        this.checkMove.TabIndex = 216;
        this.checkMove.Text = "Не показывать товар без движения";
        // 
        // ucContractor
        // 
        this.ucContractor.AllowSaveState = true;
        this.ucContractor.Caption = "Аптеки";
        this.ucContractor.Location = new System.Drawing.Point(354, 43);
        this.ucContractor.Mnemocode = "CONTRACTOR";
        this.ucContractor.Name = "ucContractor";
        this.ucContractor.Size = new System.Drawing.Size(292, 111);
        this.ucContractor.TabIndex = 217;
        this.ucContractor.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucContractor_BeforePluginShow);
        // 
        // MoveGoodsDK
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(658, 547);
        this.Controls.Add(this.ucContractor);
        this.Controls.Add(this.checkMove);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.cbAllOut);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cbAllIn);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.ucOut);
        this.Controls.Add(this.ucIn);
        this.Controls.Add(this.cbAU);
        this.Controls.Add(this.ucGoods);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.cbReportView);
        this.Controls.Add(this.ucStores);
        this.Controls.Add(this.ucSuppliers);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.toolStrip1);
        this.Name = "MoveGoodsDK";
        this.Load += new System.EventHandler(this.MoveGoodsDK_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MoveGoodsDK_FormClosed);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.toolStrip1, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.ucSuppliers, 0);
        this.Controls.SetChildIndex(this.ucStores, 0);
        this.Controls.SetChildIndex(this.cbReportView, 0);
        this.Controls.SetChildIndex(this.label2, 0);
        this.Controls.SetChildIndex(this.ucGoods, 0);
        this.Controls.SetChildIndex(this.cbAU, 0);
        this.Controls.SetChildIndex(this.ucIn, 0);
        this.Controls.SetChildIndex(this.ucOut, 0);
        this.Controls.SetChildIndex(this.label3, 0);
        this.Controls.SetChildIndex(this.cbAllIn, 0);
        this.Controls.SetChildIndex(this.label4, 0);
        this.Controls.SetChildIndex(this.label6, 0);
        this.Controls.SetChildIndex(this.cbAllOut, 0);
        this.Controls.SetChildIndex(this.label5, 0);
        this.Controls.SetChildIndex(this.checkMove, 0);
        this.Controls.SetChildIndex(this.ucContractor, 0);
        this.panel1.ResumeLayout(false);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucSuppliers;
    private System.Windows.Forms.ComboBox cbReportView;
    private System.Windows.Forms.Label label2;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private System.Windows.Forms.CheckBox cbAU;
    private System.Windows.Forms.CheckedListBox ucIn;
    private System.Windows.Forms.CheckedListBox ucOut;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox cbAllIn;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox cbAllOut;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.CheckBox checkMove;
      private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
  }
}