namespace ContractMov
{
    partial class ContractMovForm
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
        this.ucSupplier = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucContracts = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
        this.label1 = new System.Windows.Forms.Label();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(271, 3);
        this.bOK.Text = "Отчет";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(346, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 455);
        this.panel1.Size = new System.Drawing.Size(424, 29);
        this.panel1.TabIndex = 6;
        // 
        // ucSupplier
        // 
        this.ucSupplier.AllowSaveState = false;
        this.ucSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucSupplier.Caption = "Контрагент-получатель";
        this.ucSupplier.Location = new System.Drawing.Point(12, 40);
        this.ucSupplier.Mnemocode = "CONTRACTOR";
        this.ucSupplier.Name = "ucSupplier";
        this.ucSupplier.Size = new System.Drawing.Size(407, 99);
        this.ucSupplier.TabIndex = 2;
        // 
        // ucStore
        // 
        this.ucStore.AllowSaveState = false;
        this.ucStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucStore.Caption = "Склады";
        this.ucStore.Location = new System.Drawing.Point(12, 145);
        this.ucStore.Mnemocode = "STORE";
        this.ucStore.Name = "ucStore";
        this.ucStore.Size = new System.Drawing.Size(407, 99);
        this.ucStore.TabIndex = 3;
        // 
        // ucContractor
        // 
        this.ucContractor.AllowSaveState = false;
        this.ucContractor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContractor.Caption = "Поставщик";
        this.ucContractor.Location = new System.Drawing.Point(12, 250);
        this.ucContractor.Mnemocode = "CONTRACTOR";
        this.ucContractor.Name = "ucContractor";
        this.ucContractor.Size = new System.Drawing.Size(407, 99);
        this.ucContractor.TabIndex = 4;
        // 
        // ucContracts
        // 
        this.ucContracts.AllowSaveState = false;
        this.ucContracts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucContracts.Caption = "Договор";
        this.ucContracts.Location = new System.Drawing.Point(12, 355);
        this.ucContracts.Mnemocode = "CONTRACTS";
        this.ucContracts.Name = "ucContracts";
        this.ucContracts.Size = new System.Drawing.Size(407, 93);
        this.ucContracts.TabIndex = 5;
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
        // ContractMovForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(424, 484);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.ucContracts);
        this.Controls.Add(this.ucContractor);
        this.Controls.Add(this.ucStore);
        this.Controls.Add(this.ucSupplier);
        this.MinimumSize = new System.Drawing.Size(432, 518);
        this.Name = "ContractMovForm";
        this.Text = "InventoryVedCompareParams";
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucSupplier, 0);
        this.Controls.SetChildIndex(this.ucStore, 0);
        this.Controls.SetChildIndex(this.ucContractor, 0);
        this.Controls.SetChildIndex(this.ucContracts, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucSupplier;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContracts;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Label label1;

}
}