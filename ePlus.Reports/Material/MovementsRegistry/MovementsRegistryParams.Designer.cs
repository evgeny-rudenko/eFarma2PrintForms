namespace MovementsRegistry
{
  partial class MovementsRegistryParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovementsRegistryParams));
      this.groupTO = new System.Windows.Forms.GroupBox();
      this.ucCGroupTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucContractorsTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStoresTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.groupFROM = new System.Windows.Forms.GroupBox();
      this.ucCGroupFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucContractorsFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucStoresFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.groupTYPE = new System.Windows.Forms.GroupBox();
      this.rbBoth = new System.Windows.Forms.RadioButton();
      this.cbReturn = new System.Windows.Forms.CheckBox();
      this.rbUnits = new System.Windows.Forms.RadioButton();
      this.rbUsual = new System.Windows.Forms.RadioButton();
      this.groupPERIOD = new System.Windows.Forms.GroupBox();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.panel1.SuspendLayout();
      this.groupTO.SuspendLayout();
      this.groupFROM.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupTYPE.SuspendLayout();
      this.groupPERIOD.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(471, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(546, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 475);
      this.panel1.Size = new System.Drawing.Size(624, 29);
      // 
      // groupTO
      // 
      this.groupTO.Controls.Add(this.ucCGroupTo);
      this.groupTO.Controls.Add(this.ucContractorsTo);
      this.groupTO.Controls.Add(this.ucStoresTo);
      this.groupTO.Location = new System.Drawing.Point(315, 93);
      this.groupTO.Name = "groupTO";
      this.groupTO.Size = new System.Drawing.Size(300, 378);
      this.groupTO.TabIndex = 8;
      this.groupTO.TabStop = false;
      this.groupTO.Text = "Кому";
      // 
      // ucCGroupTo
      // 
      this.ucCGroupTo.AllowSaveState = true;
      this.ucCGroupTo.Caption = "Группы контрагентов";
      this.ucCGroupTo.Location = new System.Drawing.Point(6, 19);
      this.ucCGroupTo.Mnemocode = "CONTRACTOR_GROUP";
      this.ucCGroupTo.Name = "ucCGroupTo";
      this.ucCGroupTo.Size = new System.Drawing.Size(286, 85);
      this.ucCGroupTo.TabIndex = 8;
      // 
      // ucContractorsTo
      // 
      this.ucContractorsTo.AllowSaveState = true;
      this.ucContractorsTo.Caption = "Контрагенты";
      this.ucContractorsTo.Location = new System.Drawing.Point(7, 106);
      this.ucContractorsTo.Mnemocode = "CONTRACTOR";
      this.ucContractorsTo.Name = "ucContractorsTo";
      this.ucContractorsTo.Size = new System.Drawing.Size(286, 133);
      this.ucContractorsTo.TabIndex = 7;
      // 
      // ucStoresTo
      // 
      this.ucStoresTo.AllowSaveState = true;
      this.ucStoresTo.Caption = "Склады";
      this.ucStoresTo.Location = new System.Drawing.Point(7, 240);
      this.ucStoresTo.Mnemocode = "STORE";
      this.ucStoresTo.Name = "ucStoresTo";
      this.ucStoresTo.Size = new System.Drawing.Size(286, 133);
      this.ucStoresTo.TabIndex = 6;
      this.ucStoresTo.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStoresTo_BeforePluginShow);
      // 
      // groupFROM
      // 
      this.groupFROM.Controls.Add(this.ucCGroupFrom);
      this.groupFROM.Controls.Add(this.ucContractorsFrom);
      this.groupFROM.Controls.Add(this.ucStoresFrom);
      this.groupFROM.Location = new System.Drawing.Point(9, 93);
      this.groupFROM.Name = "groupFROM";
      this.groupFROM.Size = new System.Drawing.Size(300, 378);
      this.groupFROM.TabIndex = 9;
      this.groupFROM.TabStop = false;
      this.groupFROM.Text = "От кого";
      this.groupFROM.Enter += new System.EventHandler(this.groupFROM_Enter);
      // 
      // ucCGroupFrom
      // 
      this.ucCGroupFrom.AllowSaveState = true;
      this.ucCGroupFrom.Caption = "Группы контрагентов";
      this.ucCGroupFrom.Location = new System.Drawing.Point(7, 19);
      this.ucCGroupFrom.Mnemocode = "CONTRACTOR_GROUP";
      this.ucCGroupFrom.Name = "ucCGroupFrom";
      this.ucCGroupFrom.Size = new System.Drawing.Size(286, 85);
      this.ucCGroupFrom.TabIndex = 9;
      // 
      // ucContractorsFrom
      // 
      this.ucContractorsFrom.AllowSaveState = true;
      this.ucContractorsFrom.Caption = "Контрагенты";
      this.ucContractorsFrom.Location = new System.Drawing.Point(7, 106);
      this.ucContractorsFrom.Mnemocode = "CONTRACTOR";
      this.ucContractorsFrom.Name = "ucContractorsFrom";
      this.ucContractorsFrom.Size = new System.Drawing.Size(286, 133);
      this.ucContractorsFrom.TabIndex = 6;
      // 
      // ucStoresFrom
      // 
      this.ucStoresFrom.AllowSaveState = true;
      this.ucStoresFrom.Caption = "Склады";
      this.ucStoresFrom.Location = new System.Drawing.Point(7, 240);
      this.ucStoresFrom.Mnemocode = "STORE";
      this.ucStoresFrom.Name = "ucStoresFrom";
      this.ucStoresFrom.Size = new System.Drawing.Size(286, 133);
      this.ucStoresFrom.TabIndex = 5;
      this.ucStoresFrom.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStoresFrom_BeforePluginShow);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(624, 25);
      this.toolStrip1.TabIndex = 182;
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
      // groupTYPE
      // 
      this.groupTYPE.Controls.Add(this.rbBoth);
      this.groupTYPE.Controls.Add(this.cbReturn);
      this.groupTYPE.Controls.Add(this.rbUnits);
      this.groupTYPE.Controls.Add(this.rbUsual);
      this.groupTYPE.Location = new System.Drawing.Point(315, 35);
      this.groupTYPE.Name = "groupTYPE";
      this.groupTYPE.Size = new System.Drawing.Size(300, 52);
      this.groupTYPE.TabIndex = 9;
      this.groupTYPE.TabStop = false;
      this.groupTYPE.Text = "Тип перемещения";
      // 
      // rbBoth
      // 
      this.rbBoth.AutoSize = true;
      this.rbBoth.Location = new System.Drawing.Point(197, 13);
      this.rbBoth.Name = "rbBoth";
      this.rbBoth.Size = new System.Drawing.Size(71, 17);
      this.rbBoth.TabIndex = 3;
      this.rbBoth.Text = "Оба типа";
      this.rbBoth.UseVisualStyleBackColor = true;
      // 
      // cbReturn
      // 
      this.cbReturn.AutoSize = true;
      this.cbReturn.Location = new System.Drawing.Point(197, 32);
      this.cbReturn.Name = "cbReturn";
      this.cbReturn.Size = new System.Drawing.Size(68, 17);
      this.cbReturn.TabIndex = 2;
      this.cbReturn.Text = "Возврат";
      this.cbReturn.UseVisualStyleBackColor = true;
      // 
      // rbUnits
      // 
      this.rbUnits.AutoSize = true;
      this.rbUnits.Location = new System.Drawing.Point(6, 31);
      this.rbUnits.Name = "rbUnits";
      this.rbUnits.Size = new System.Drawing.Size(154, 17);
      this.rbUnits.TabIndex = 1;
      this.rbUnits.Text = "Между подразделениями";
      this.rbUnits.UseVisualStyleBackColor = true;
      // 
      // rbUsual
      // 
      this.rbUsual.AutoSize = true;
      this.rbUsual.Checked = true;
      this.rbUsual.Location = new System.Drawing.Point(6, 13);
      this.rbUsual.Name = "rbUsual";
      this.rbUsual.Size = new System.Drawing.Size(70, 17);
      this.rbUsual.TabIndex = 0;
      this.rbUsual.TabStop = true;
      this.rbUsual.Text = "Обычное";
      this.rbUsual.UseVisualStyleBackColor = true;
      // 
      // groupPERIOD
      // 
      this.groupPERIOD.Controls.Add(this.ucPeriod);
      this.groupPERIOD.Location = new System.Drawing.Point(9, 35);
      this.groupPERIOD.Name = "groupPERIOD";
      this.groupPERIOD.Size = new System.Drawing.Size(300, 52);
      this.groupPERIOD.TabIndex = 10;
      this.groupPERIOD.TabStop = false;
      this.groupPERIOD.Text = "Период";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(37, 19);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(222, 21);
      this.ucPeriod.TabIndex = 7;
      // 
      // MovementsRegistryParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 504);
      this.Controls.Add(this.groupPERIOD);
      this.Controls.Add(this.groupTYPE);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.groupFROM);
      this.Controls.Add(this.groupTO);
      this.Name = "MovementsRegistryParams";
      this.Load += new System.EventHandler(this.MovementsRegistryParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MovementsRegistryParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.groupTO, 0);
      this.Controls.SetChildIndex(this.groupFROM, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.groupTYPE, 0);
      this.Controls.SetChildIndex(this.groupPERIOD, 0);
      this.panel1.ResumeLayout(false);
      this.groupTO.ResumeLayout(false);
      this.groupFROM.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupTYPE.ResumeLayout(false);
      this.groupTYPE.PerformLayout();
      this.groupPERIOD.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupTO;
    private System.Windows.Forms.GroupBox groupFROM;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStoresFrom;
    private System.Windows.Forms.GroupBox groupTYPE;
    private System.Windows.Forms.GroupBox groupPERIOD;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.CheckBox cbReturn;
    private System.Windows.Forms.RadioButton rbUnits;
    private System.Windows.Forms.RadioButton rbUsual;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucStoresTo;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorsTo;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorsFrom;
    private System.Windows.Forms.RadioButton rbBoth;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucCGroupTo;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucCGroupFrom;
  }
}