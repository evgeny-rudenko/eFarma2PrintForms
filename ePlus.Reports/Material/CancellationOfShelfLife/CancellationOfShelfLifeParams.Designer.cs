namespace RCBCancellationOfShelfLife_Rigla
{
  partial class CancellationOfShelfLifeParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancellationOfShelfLifeParams));
      this.label2 = new System.Windows.Forms.Label();
      this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
      this.ReportMonth = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(179, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(254, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 110);
      this.panel1.Size = new System.Drawing.Size(332, 29);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 75);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 13);
      this.label2.TabIndex = 127;
      this.label2.Text = "јптека";
      // 
      // ucContractor
      // 
      this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
      this.ucContractor.Location = new System.Drawing.Point(75, 72);
      this.ucContractor.Mnemocode = "CONTRACTOR";
      this.ucContractor.Name = "ucContractor";
      this.ucContractor.Size = new System.Drawing.Size(239, 21);
      this.ucContractor.TabIndex = 126;
      // 
      // ReportMonth
      // 
      this.ReportMonth.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.ReportMonth.CustomFormat = "MMMM yyyy";
      this.ReportMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.ReportMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.ReportMonth.Location = new System.Drawing.Point(109, 38);
      this.ReportMonth.Name = "ReportMonth";
      this.ReportMonth.ShowUpDown = true;
      this.ReportMonth.Size = new System.Drawing.Size(205, 20);
      this.ReportMonth.TabIndex = 128;
      this.ReportMonth.Value = new System.DateTime(2010, 8, 1, 0, 0, 0, 0);
      this.ReportMonth.ValueChanged += new System.EventHandler(this.ReportMonth_ValueChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 41);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(91, 13);
      this.label1.TabIndex = 129;
      this.label1.Text = "ќтчетный мес€ц";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(332, 25);
      this.toolStrip1.TabIndex = 181;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(150, 22);
      this.toolStripButton1.Text = "«начени€ по умолчанию";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // CancellationOfShelfLifeParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(332, 139);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ReportMonth);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ucContractor);
      this.Name = "CancellationOfShelfLifeParams";
      this.Load += new System.EventHandler(this.CancellationOfShelfLifeParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CancellationOfShelfLifeParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.ucContractor, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.ReportMonth, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
    private System.Windows.Forms.DateTimePicker ReportMonth;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
  }
}