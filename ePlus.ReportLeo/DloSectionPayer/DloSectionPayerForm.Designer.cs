namespace DloSectionPayer
{
	partial class DloSectionPayerForm
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
		this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.ucSupplier = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.label1 = new System.Windows.Forms.Label();
		this.ucProgram = new ePlus.MetaData.Client.UCPluginMultiSelect();
		this.ucDate = new ePlus.MetaData.Controls.DateControl();
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
		this.panel1.Location = new System.Drawing.Point(0, 358);
		this.panel1.Size = new System.Drawing.Size(416, 29);
		this.panel1.TabIndex = 4;
		// 
		// ucStore
		// 
		this.ucStore.AllowSaveState = false;
		this.ucStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.ucStore.Caption = "";
		this.ucStore.Location = new System.Drawing.Point(9, 39);
		this.ucStore.Mnemocode = "STORE";
		this.ucStore.Name = "ucStore";
		this.ucStore.Size = new System.Drawing.Size(395, 98);
		this.ucStore.TabIndex = 2;
		// 
		// ucSupplier
		// 
		this.ucSupplier.AllowSaveState = false;
		this.ucSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.ucSupplier.Caption = "Покупатель";
		this.ucSupplier.Location = new System.Drawing.Point(9, 143);
		this.ucSupplier.Mnemocode = "CONTRACTOR";
		this.ucSupplier.Name = "ucSupplier";
		this.ucSupplier.Size = new System.Drawing.Size(395, 99);
		this.ucSupplier.TabIndex = 3;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(18, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Дата";
		// 
		// ucProgram
		// 
		this.ucProgram.AllowSaveState = false;
		this.ucProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.ucProgram.Caption = "Целевые программы";
		this.ucProgram.Location = new System.Drawing.Point(9, 248);
		this.ucProgram.Mnemocode = "TASK_PROGRAM";
		this.ucProgram.Name = "ucProgram";
		this.ucProgram.Size = new System.Drawing.Size(395, 98);
		this.ucProgram.TabIndex = 11;
		// 
		// ucDate
		// 
		this.ucDate.Checked = true;
		this.ucDate.DefaultDay = 22;
		this.ucDate.DefaultMonth = 10;
		this.ucDate.DefaultYear = 2010;
		this.ucDate.ErrorColor = System.Drawing.Color.Red;
		this.ucDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
		this.ucDate.Location = new System.Drawing.Point(55, 12);
		this.ucDate.Mask = "00/00/0000";
		this.ucDate.MaxDate = new System.DateTime(((long)(0)));
		this.ucDate.MinDate = new System.DateTime(((long)(0)));
		this.ucDate.Name = "ucDate";
		this.ucDate.NormalColor = System.Drawing.SystemColors.Window;
		this.ucDate.Size = new System.Drawing.Size(89, 20);
		this.ucDate.TabIndex = 12;
		this.ucDate.Value = new System.DateTime(2010, 10, 22, 12, 26, 36, 281);
		// 
		// DloSectionPayerForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(416, 387);
		this.Controls.Add(this.ucDate);
		this.Controls.Add(this.ucProgram);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.ucStore);
		this.Controls.Add(this.ucSupplier);
		this.MinimumSize = new System.Drawing.Size(424, 312);
		this.Name = "DloSectionPayerForm";
		this.Text = "InventoryVedCompareParams";
		this.Controls.SetChildIndex(this.panel1, 0);
		this.Controls.SetChildIndex(this.ucSupplier, 0);
		this.Controls.SetChildIndex(this.ucStore, 0);
		this.Controls.SetChildIndex(this.label1, 0);
		this.Controls.SetChildIndex(this.ucProgram, 0);
		this.Controls.SetChildIndex(this.ucDate, 0);
		this.panel1.ResumeLayout(false);
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

	private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucSupplier;
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucProgram;
		private ePlus.MetaData.Controls.DateControl ucDate;



}
}