namespace AnalizeAction
{
    partial class AnalizeActionForm
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
        this.ucProgram = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.label1 = new System.Windows.Forms.Label();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(273, 3);
        this.bOK.Text = "Отчет";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(348, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 146);
        this.panel1.Size = new System.Drawing.Size(426, 29);
        this.panel1.TabIndex = 6;
        // 
        // ucProgram
        // 
        this.ucProgram.AllowSaveState = false;
        this.ucProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucProgram.Caption = "Целевые программы";
        this.ucProgram.Location = new System.Drawing.Point(9, 39);
        this.ucProgram.Mnemocode = "TASK_PROGRAM";
        this.ucProgram.Name = "ucProgram";
        this.ucProgram.Size = new System.Drawing.Size(405, 98);
        this.ucProgram.TabIndex = 4;
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.DateTo = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.Location = new System.Drawing.Point(64, 12);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(222, 21);
        this.ucPeriod.TabIndex = 1;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(45, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Период";
        // 
        // AnalizeActionForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(426, 175);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.ucProgram);
        this.MinimumSize = new System.Drawing.Size(434, 209);
        this.Name = "AnalizeActionForm";
        this.Text = "InventoryVedCompareParams";
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucProgram, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ePlus.MetaData.Client.UCPluginMultiSelect ucProgram;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Label label1;

}
}