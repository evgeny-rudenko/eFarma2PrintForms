namespace Info74
{
    partial class Info74Form
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
        this.ucProgram = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.label1 = new System.Windows.Forms.Label();
        this.ucContracts = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.cmbYers = new ePlus.MetaData.Controls.ComboBoxEx();
        this.label2 = new System.Windows.Forms.Label();
        this.cmbHalfYear = new ePlus.MetaData.Controls.ComboBoxEx();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(263, 3);
        this.bOK.Text = "Отчет";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(338, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 249);
        this.panel1.Size = new System.Drawing.Size(416, 29);
        this.panel1.TabIndex = 4;
        // 
        // ucProgram
        // 
        this.ucProgram.AllowSaveState = false;
        this.ucProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucProgram.Caption = "Целевые программы";
        this.ucProgram.Location = new System.Drawing.Point(9, 39);
        this.ucProgram.Mnemocode = "TASK_PROGRAM";
        this.ucProgram.Name = "ucProgram";
        this.ucProgram.Size = new System.Drawing.Size(395, 98);
        this.ucProgram.TabIndex = 2;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(182, 16);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(25, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Год";
        // 
        // ucContracts
        // 
        this.ucContracts.AllowSaveState = false;
        this.ucContracts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContracts.Caption = "Договор";
        this.ucContracts.Location = new System.Drawing.Point(9, 143);
        this.ucContracts.Mnemocode = "CONTRACTS";
        this.ucContracts.Name = "ucContracts";
        this.ucContracts.Size = new System.Drawing.Size(395, 103);
        this.ucContracts.TabIndex = 3;
        // 
        // cmbYers
        // 
        this.cmbYers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbYers.FormattingEnabled = true;
        this.cmbYers.Location = new System.Drawing.Point(210, 12);
        this.cmbYers.Name = "cmbYers";
        this.cmbYers.Size = new System.Drawing.Size(63, 21);
        this.cmbYers.TabIndex = 1;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(16, 16);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(61, 13);
        this.label2.TabIndex = 0;
        this.label2.Text = "Полугодие";
        // 
        // cmbHalfYear
        // 
        this.cmbHalfYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbHalfYear.FormattingEnabled = true;
        this.cmbHalfYear.Items.AddRange(new object[] {
            "1-е полугодие",
            "2-е полугодие"});
        this.cmbHalfYear.Location = new System.Drawing.Point(78, 12);
        this.cmbHalfYear.Name = "cmbHalfYear";
        this.cmbHalfYear.Size = new System.Drawing.Size(98, 21);
        this.cmbHalfYear.TabIndex = 1;
        // 
        // Info74Form
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(416, 278);
        this.Controls.Add(this.cmbHalfYear);
        this.Controls.Add(this.cmbYers);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.ucContracts);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucProgram);
        this.MinimumSize = new System.Drawing.Size(424, 312);
        this.Name = "Info74Form";
        this.Text = "InventoryVedCompareParams";
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucProgram, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.Controls.SetChildIndex(this.ucContracts, 0);
        this.Controls.SetChildIndex(this.label2, 0);
        this.Controls.SetChildIndex(this.cmbYers, 0);
        this.Controls.SetChildIndex(this.cmbHalfYear, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucProgram;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContracts;
        private ePlus.MetaData.Controls.ComboBoxEx cmbYers;
        private System.Windows.Forms.Label label2;
        private ePlus.MetaData.Controls.ComboBoxEx cmbHalfYear;

}
}