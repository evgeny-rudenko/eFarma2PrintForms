namespace InvoiceLS103
{
    partial class InvoiceLS103Form
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
        this.label1 = new System.Windows.Forms.Label();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.ucContracts = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucSupplier = new ePlus.MetaData.Client.UCPluginMultiSelect();
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
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(14, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(45, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Период";
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.DateTo = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.Location = new System.Drawing.Point(62, 10);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(222, 21);
        this.ucPeriod.TabIndex = 1;
        // 
        // ucContracts
        // 
        this.ucContracts.AllowSaveState = false;
        this.ucContracts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContracts.Caption = "Договор";
        this.ucContracts.Location = new System.Drawing.Point(8, 144);
        this.ucContracts.Mnemocode = "CONTRACTS";
        this.ucContracts.Name = "ucContracts";
        this.ucContracts.Size = new System.Drawing.Size(395, 99);
        this.ucContracts.TabIndex = 3;
        // 
        // ucSupplier
        // 
        this.ucSupplier.AllowSaveState = false;
        this.ucSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucSupplier.Caption = "Поставщик";
        this.ucSupplier.Location = new System.Drawing.Point(8, 39);
        this.ucSupplier.Mnemocode = "CONTRACTOR";
        this.ucSupplier.Name = "ucSupplier";
        this.ucSupplier.Size = new System.Drawing.Size(395, 99);
        this.ucSupplier.TabIndex = 2;
        // 
        // InvoiceLS103Form
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(416, 278);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.ucContracts);
        this.Controls.Add(this.ucSupplier);
        this.MinimumSize = new System.Drawing.Size(424, 312);
        this.Name = "InvoiceLS103Form";
        this.Text = "InventoryVedCompareParams";
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucSupplier, 0);
        this.Controls.SetChildIndex(this.ucContracts, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContracts;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucSupplier;


}
}