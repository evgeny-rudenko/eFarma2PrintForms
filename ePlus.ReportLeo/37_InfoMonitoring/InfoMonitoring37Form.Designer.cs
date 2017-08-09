namespace InfoMonitoring37
{
    partial class InfoMonitoring37Form
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
        this.ucGroupGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucPayment = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.label1 = new System.Windows.Forms.Label();
        this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
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
        this.panel1.Location = new System.Drawing.Point(0, 353);
        this.panel1.Size = new System.Drawing.Size(416, 29);
        this.panel1.TabIndex = 5;
        // 
        // ucGroupGoods
        // 
        this.ucGroupGoods.AllowSaveState = false;
        this.ucGroupGoods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucGroupGoods.Caption = "";
        this.ucGroupGoods.Location = new System.Drawing.Point(12, 140);
        this.ucGroupGoods.Mnemocode = "GOODS_GROUP";
        this.ucGroupGoods.Name = "ucGroupGoods";
        this.ucGroupGoods.Size = new System.Drawing.Size(395, 98);
        this.ucGroupGoods.TabIndex = 3;
        // 
        // ucPayment
        // 
        this.ucPayment.AllowSaveState = false;
        this.ucPayment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucPayment.Caption = "";
        this.ucPayment.Location = new System.Drawing.Point(12, 244);
        this.ucPayment.Mnemocode = "CONTRACTOR_GROUP";
        this.ucPayment.Name = "ucPayment";
        this.ucPayment.Size = new System.Drawing.Size(395, 103);
        this.ucPayment.TabIndex = 4;
        // 
        // ucStore
        // 
        this.ucStore.AllowSaveState = false;
        this.ucStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ucStore.Caption = "";
        this.ucStore.Location = new System.Drawing.Point(12, 36);
        this.ucStore.Mnemocode = "STORE";
        this.ucStore.Name = "ucStore";
        this.ucStore.Size = new System.Drawing.Size(395, 98);
        this.ucStore.TabIndex = 2;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(14, 12);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(45, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Период";
        // 
        // ucPeriod
        // 
        this.ucPeriod.DateFrom = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.DateTo = new System.DateTime(2010, 3, 30, 15, 2, 20, 437);
        this.ucPeriod.Location = new System.Drawing.Point(62, 9);
        this.ucPeriod.Name = "ucPeriod";
        this.ucPeriod.Size = new System.Drawing.Size(222, 21);
        this.ucPeriod.TabIndex = 1;
        // 
        // InfoMonitoring37Form
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(416, 382);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ucPeriod);
        this.Controls.Add(this.ucPayment);
        this.Controls.Add(this.ucStore);
        this.Controls.Add(this.ucGroupGoods);
        this.MinimumSize = new System.Drawing.Size(424, 312);
        this.Name = "InfoMonitoring37Form";
        this.Text = "InventoryVedCompareParams";
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.ucGroupGoods, 0);
        this.Controls.SetChildIndex(this.ucStore, 0);
        this.Controls.SetChildIndex(this.ucPayment, 0);
        this.Controls.SetChildIndex(this.ucPeriod, 0);
        this.Controls.SetChildIndex(this.label1, 0);
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucGroupGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucPayment;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;

}
}