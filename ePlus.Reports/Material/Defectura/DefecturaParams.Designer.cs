namespace Defectura
{
  partial class DefecturaParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefecturaParams));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucPeriodFrom = new System.Windows.Forms.DateTimePicker();
      this.ucPeriodTo = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ucDays = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.ucInsReserve = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.ucSort = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.cbGroups = new System.Windows.Forms.CheckBox();
      this.cbOA = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ucDays)).BeginInit();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(223, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(298, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 401);
      this.panel1.Size = new System.Drawing.Size(376, 29);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(376, 25);
      this.toolStrip1.TabIndex = 184;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(150, 22);
      this.toolStripButton1.Text = "Значения по умолчанию";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // ucPeriodFrom
      // 
      this.ucPeriodFrom.CustomFormat = "dd.MM.yyyy HH:mm";
      this.ucPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.ucPeriodFrom.Location = new System.Drawing.Point(121, 38);
      this.ucPeriodFrom.Name = "ucPeriodFrom";
      this.ucPeriodFrom.Size = new System.Drawing.Size(115, 20);
      this.ucPeriodFrom.TabIndex = 185;
      this.ucPeriodFrom.Value = new System.DateTime(2010, 10, 27, 0, 0, 0, 0);
      // 
      // ucPeriodTo
      // 
      this.ucPeriodTo.CustomFormat = "dd.MM.yyyy HH:mm";
      this.ucPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.ucPeriodTo.Location = new System.Drawing.Point(247, 38);
      this.ucPeriodTo.Name = "ucPeriodTo";
      this.ucPeriodTo.Size = new System.Drawing.Size(115, 20);
      this.ucPeriodTo.TabIndex = 186;
      this.ucPeriodTo.Value = new System.DateTime(2010, 10, 27, 0, 0, 0, 0);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 42);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 187;
      this.label1.Text = "Период:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(236, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(10, 13);
      this.label2.TabIndex = 188;
      this.label2.Text = "-";
      // 
      // ucDays
      // 
      this.ucDays.Location = new System.Drawing.Point(121, 89);
      this.ucDays.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
      this.ucDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ucDays.Name = "ucDays";
      this.ucDays.Size = new System.Drawing.Size(241, 20);
      this.ucDays.TabIndex = 189;
      this.ucDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ucDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 66);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(96, 13);
      this.label3.TabIndex = 190;
      this.label3.Text = "Страховой запас:";
      // 
      // ucInsReserve
      // 
      this.ucInsReserve.Location = new System.Drawing.Point(121, 63);
      this.ucInsReserve.Name = "ucInsReserve";
      this.ucInsReserve.Size = new System.Drawing.Size(241, 20);
      this.ucInsReserve.TabIndex = 191;
      this.ucInsReserve.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ucInsReserve.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucInsReserve_KeyPress);
      this.ucInsReserve.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ucInsReserve_KeyDown);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 91);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(91, 13);
      this.label4.TabIndex = 192;
      this.label4.Text = "Расчетных дней:";
      // 
      // ucSort
      // 
      this.ucSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ucSort.FormattingEnabled = true;
      this.ucSort.Items.AddRange(new object[] {
            "Товар",
            "Поставщик"});
      this.ucSort.Location = new System.Drawing.Point(121, 115);
      this.ucSort.Name = "ucSort";
      this.ucSort.Size = new System.Drawing.Size(241, 21);
      this.ucSort.TabIndex = 193;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 118);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(70, 13);
      this.label5.TabIndex = 194;
      this.label5.Text = "Сортировка:";
      // 
      // ucGoods
      // 
      this.ucGoods.AllowSaveState = true;
      this.ucGoods.Caption = "Товар";
      this.ucGoods.Location = new System.Drawing.Point(10, 169);
      this.ucGoods.Mnemocode = "GOODS2";
      this.ucGoods.Name = "ucGoods";
      this.ucGoods.Size = new System.Drawing.Size(354, 85);
      this.ucGoods.TabIndex = 195;
      // 
      // ucStores
      // 
      this.ucStores.AllowSaveState = true;
      this.ucStores.Caption = "Склад";
      this.ucStores.Location = new System.Drawing.Point(10, 260);
      this.ucStores.Mnemocode = "STORE";
      this.ucStores.Name = "ucStores";
      this.ucStores.Size = new System.Drawing.Size(354, 85);
      this.ucStores.TabIndex = 196;
      this.ucStores.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStores_BeforePluginShow);
      // 
      // cbGroups
      // 
      this.cbGroups.AutoSize = true;
      this.cbGroups.Location = new System.Drawing.Point(13, 374);
      this.cbGroups.Name = "cbGroups";
      this.cbGroups.Size = new System.Drawing.Size(191, 17);
      this.cbGroups.TabIndex = 198;
      this.cbGroups.Text = "Сворачивать группы по товарам";
      this.cbGroups.UseVisualStyleBackColor = true;
      this.cbGroups.CheckedChanged += new System.EventHandler(this.cbGroups_CheckedChanged);
      // 
      // cbOA
      // 
      this.cbOA.AutoSize = true;
      this.cbOA.Location = new System.Drawing.Point(13, 351);
      this.cbOA.Name = "cbOA";
      this.cbOA.Size = new System.Drawing.Size(189, 17);
      this.cbOA.TabIndex = 197;
      this.cbOA.Text = "Только товары с признаком ОА";
      this.cbOA.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(10, 145);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(43, 13);
      this.label6.TabIndex = 200;
      this.label6.Text = "Аптека";
      // 
      // ucContractor
      // 
      this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
      this.ucContractor.Location = new System.Drawing.Point(59, 142);
      this.ucContractor.Mnemocode = "CONTRACTOR";
      this.ucContractor.Name = "ucContractor";
      this.ucContractor.Size = new System.Drawing.Size(303, 21);
      this.ucContractor.TabIndex = 199;
      this.ucContractor.ValueChanged += new ePlus.MetaData.Client.UCMetaPluginSelect.ValueChangedDelegate(this.ucContractor_ValueChanged);
      // 
      // DefecturaParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(376, 430);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.ucContractor);
      this.Controls.Add(this.cbGroups);
      this.Controls.Add(this.cbOA);
      this.Controls.Add(this.ucStores);
      this.Controls.Add(this.ucGoods);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.ucSort);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.ucInsReserve);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.ucDays);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriodTo);
      this.Controls.Add(this.ucPeriodFrom);
      this.Controls.Add(this.toolStrip1);
      this.Name = "DefecturaParams";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefecturaParams_FormClosed);
      this.Load += new System.EventHandler(this.DefecturaParams_Load);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucPeriodFrom, 0);
      this.Controls.SetChildIndex(this.ucPeriodTo, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.ucDays, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.ucInsReserve, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.ucSort, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.ucGoods, 0);
      this.Controls.SetChildIndex(this.ucStores, 0);
      this.Controls.SetChildIndex(this.cbOA, 0);
      this.Controls.SetChildIndex(this.cbGroups, 0);
      this.Controls.SetChildIndex(this.ucContractor, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ucDays)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.DateTimePicker ucPeriodFrom;
    private System.Windows.Forms.DateTimePicker ucPeriodTo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown ucDays;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox ucInsReserve;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox ucSort;
    private System.Windows.Forms.Label label5;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
    private System.Windows.Forms.CheckBox cbGroups;
    private System.Windows.Forms.CheckBox cbOA;
    private System.Windows.Forms.Label label6;
    private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
  }
}