using System;
namespace FCChInvoiceOutExportXMLEncode
{
    partial class FCChInvoiceOutExportXMLEncodeForm
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
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.accessPointAPExport = new ePlus.MetaData.Client.UCMetaPluginSelect();
        this.label10 = new System.Windows.Forms.Label();
        this.cbEncodList = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cancelButton
        // 
        this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cancelButton.Location = new System.Drawing.Point(298, 103);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 8;
        this.cancelButton.Text = "Отмена";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(217, 103);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 6;
        this.okButton.Text = "Экспорт";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // accessPointAPExport
        // 
        this.accessPointAPExport.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
        this.accessPointAPExport.Location = new System.Drawing.Point(15, 30);
        this.accessPointAPExport.Margin = new System.Windows.Forms.Padding(4);
        this.accessPointAPExport.Mnemocode = "ACCESS_POINT";
        this.accessPointAPExport.Name = "accessPointAPExport";
        this.accessPointAPExport.Size = new System.Drawing.Size(358, 21);
        this.accessPointAPExport.TabIndex = 10;
        // 
        // label10
        // 
        this.label10.Location = new System.Drawing.Point(12, 9);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(211, 18);
        this.label10.TabIndex = 9;
        this.label10.Text = "Точка доступа для экспорта:";
        this.label10.Click += new System.EventHandler(this.label10_Click);
        // 
        // cbEncodList
        // 
        this.cbEncodList.FormattingEnabled = true;
        this.cbEncodList.Location = new System.Drawing.Point(15, 76);
        this.cbEncodList.Name = "cbEncodList";
        this.cbEncodList.Size = new System.Drawing.Size(358, 21);
        this.cbEncodList.TabIndex = 11;
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(12, 55);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(211, 18);
        this.label1.TabIndex = 12;
        this.label1.Text = "Кодировка:";
        // 
        // InvoiceOutExportXMLForm
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(380, 134);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cbEncodList);
        this.Controls.Add(this.accessPointAPExport);
        this.Controls.Add(this.label10);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "InvoiceOutExportXMLForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Параметры";
        this.Load += new System.EventHandler(this.InvoiceOutExportXMLForm_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InvoiceOutExportXMLForm_FormClosed);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private ePlus.MetaData.Client.UCMetaPluginSelect accessPointAPExport;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbEncodList;
        private System.Windows.Forms.Label label1;
  }
}