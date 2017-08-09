namespace RCBUnsatisfiedDemand
{
  partial class UnsatisfiedDemandParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnsatisfiedDemandParams));
      this.label1 = new System.Windows.Forms.Label();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.ucUsers = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(153, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(228, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 352);
      this.panel1.Size = new System.Drawing.Size(306, 29);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 45);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Период: ";
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(68, 42);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(228, 21);
      this.ucPeriod.TabIndex = 6;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(306, 25);
      this.toolStrip1.TabIndex = 182;
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
      // ucContractors
      // 
      this.ucContractors.AllowSaveState = true;
      this.ucContractors.Caption = "Наименование АУ";
      this.ucContractors.Location = new System.Drawing.Point(10, 69);
      this.ucContractors.Mnemocode = "CONTRACTOR";
      this.ucContractors.Name = "ucContractors";
      this.ucContractors.Size = new System.Drawing.Size(286, 133);
      this.ucContractors.TabIndex = 183;
      // 
      // ucUsers
      // 
      this.ucUsers.AllowSaveState = true;
      this.ucUsers.Caption = "Сотрудник АУ";
      this.ucUsers.Location = new System.Drawing.Point(10, 208);
      this.ucUsers.Mnemocode = "USERS";
      this.ucUsers.Name = "ucUsers";
      this.ucUsers.Size = new System.Drawing.Size(286, 133);
      this.ucUsers.TabIndex = 184;
      // 
      // UnsatisfiedDemandParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(306, 381);
      this.Controls.Add(this.ucUsers);
      this.Controls.Add(this.ucContractors);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Name = "UnsatisfiedDemandParams";
      this.Load += new System.EventHandler(this.UnsatisfiedDemandParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UnsatisfiedDemandParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.ucContractors, 0);
      this.Controls.SetChildIndex(this.ucUsers, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Client.UCPeriod ucPeriod;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
    private ePlus.MetaData.Client.UCPluginMultiSelect ucUsers;
  }
}