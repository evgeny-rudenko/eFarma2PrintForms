namespace InvoiceInvoice
{
  partial class InvoiceForm
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
      this.numberLabel = new System.Windows.Forms.Label();
      this.numberTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(97, 80);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 12;
      this.cancelButton.Text = "Отмена";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(16, 80);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 10;
      this.okButton.Text = "Отчет";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // numberLabel
      // 
      this.numberLabel.AutoSize = true;
      this.numberLabel.Location = new System.Drawing.Point(13, 9);
      this.numberLabel.Name = "numberLabel";
      this.numberLabel.Size = new System.Drawing.Size(162, 13);
      this.numberLabel.TabIndex = 11;
      this.numberLabel.Text = "Введите номер счета-фактуры";
      // 
      // numberTextBox
      // 
      this.numberTextBox.Location = new System.Drawing.Point(16, 37);
      this.numberTextBox.Name = "numberTextBox";
      this.numberTextBox.Size = new System.Drawing.Size(156, 20);
      this.numberTextBox.TabIndex = 9;
      // 
      // InvoiceForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(189, 114);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.numberLabel);
      this.Controls.Add(this.numberTextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InvoiceForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Параметры";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Label numberLabel;
    private System.Windows.Forms.TextBox numberTextBox;
  }
}