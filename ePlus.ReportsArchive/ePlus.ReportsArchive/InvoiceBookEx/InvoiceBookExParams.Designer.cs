namespace InvoiceBookEx
{
  partial class InvoiceBookExParams
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
      this.period = new ePlus.MetaData.Controls.UCPeriod();
      this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.chkDetail = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(307, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(388, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 246);
      this.panel1.Size = new System.Drawing.Size(466, 29);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Период:";
      // 
      // period
      // 
      this.period.DateFrom = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
      this.period.DateTo = new System.DateTime(2008, 7, 1, 10, 52, 53, 812);
      this.period.Location = new System.Drawing.Point(58, 12);
      this.period.Name = "period";
      this.period.Size = new System.Drawing.Size(222, 21);
      this.period.TabIndex = 3;
      // 
      // mpsStore
      // 
      this.mpsStore.AllowSaveState = false;
      this.mpsStore.Caption = "Склады";
      this.mpsStore.Location = new System.Drawing.Point(6, 144);
      this.mpsStore.Mnemocode = "STORE";
      this.mpsStore.Name = "mpsStore";
      this.mpsStore.Size = new System.Drawing.Size(457, 96);
      this.mpsStore.TabIndex = 124;
      // 
      // mpsContractor
      // 
      this.mpsContractor.AllowSaveState = false;
      this.mpsContractor.Caption = "Контрагенты";
      this.mpsContractor.Location = new System.Drawing.Point(6, 39);
      this.mpsContractor.Mnemocode = "CONTRACTOR";
      this.mpsContractor.Name = "mpsContractor";
      this.mpsContractor.Size = new System.Drawing.Size(457, 99);
      this.mpsContractor.TabIndex = 123;
      // 
      // chkDetail
      // 
      this.chkDetail.AutoSize = true;
      this.chkDetail.Location = new System.Drawing.Point(286, 14);
      this.chkDetail.Name = "chkDetail";
      this.chkDetail.Size = new System.Drawing.Size(174, 17);
      this.chkDetail.TabIndex = 125;
      this.chkDetail.Text = "Детализация по документам";
      this.chkDetail.UseVisualStyleBackColor = true;
      // 
      // InvoiceBookExParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(466, 275);
      this.Controls.Add(this.chkDetail);
      this.Controls.Add(this.mpsStore);
      this.Controls.Add(this.mpsContractor);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.period);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "InvoiceBookExParams";
      this.Text = "Параметры отчета: Завозная книга";
      this.Controls.SetChildIndex(this.period, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.mpsContractor, 0);
      this.Controls.SetChildIndex(this.mpsStore, 0);
      this.Controls.SetChildIndex(this.chkDetail, 0);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private ePlus.MetaData.Controls.UCPeriod period;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
    private System.Windows.Forms.CheckBox chkDetail;
  }
}