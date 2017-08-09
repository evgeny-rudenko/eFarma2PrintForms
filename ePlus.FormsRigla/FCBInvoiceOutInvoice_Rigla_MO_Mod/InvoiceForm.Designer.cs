namespace FCChInvoiceOutInvoice_Rigla_Mod
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
        this.DoverBuhTextBox = new System.Windows.Forms.TextBox();
        this.FIOBuhTextBox = new System.Windows.Forms.TextBox();
        this.DoverDirTextBox = new System.Windows.Forms.TextBox();
        this.FIODirTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cancelButton
        // 
        this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cancelButton.Location = new System.Drawing.Point(103, 222);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 8;
        this.cancelButton.Text = "Отмена";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(22, 222);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 6;
        this.okButton.Text = "Отчет";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // numberLabel
        // 
        this.numberLabel.AutoSize = true;
        this.numberLabel.Location = new System.Drawing.Point(12, 9);
        this.numberLabel.Name = "numberLabel";
        this.numberLabel.Size = new System.Drawing.Size(162, 13);
        this.numberLabel.TabIndex = 7;
        this.numberLabel.Text = "Введите номер счета-фактуры";
        // 
        // numberTextBox
        // 
        this.numberTextBox.Location = new System.Drawing.Point(15, 37);
        this.numberTextBox.Name = "numberTextBox";
        this.numberTextBox.Size = new System.Drawing.Size(171, 20);
        this.numberTextBox.TabIndex = 5;
        // 
        // DoverBuhTextBox
        // 
        this.DoverBuhTextBox.Location = new System.Drawing.Point(15, 196);
        this.DoverBuhTextBox.Name = "DoverBuhTextBox";
        this.DoverBuhTextBox.Size = new System.Drawing.Size(171, 20);
        this.DoverBuhTextBox.TabIndex = 24;
        // 
        // FIOBuhTextBox
        // 
        this.FIOBuhTextBox.Location = new System.Drawing.Point(15, 157);
        this.FIOBuhTextBox.Name = "FIOBuhTextBox";
        this.FIOBuhTextBox.Size = new System.Drawing.Size(171, 20);
        this.FIOBuhTextBox.TabIndex = 23;
        // 
        // DoverDirTextBox
        // 
        this.DoverDirTextBox.Location = new System.Drawing.Point(15, 115);
        this.DoverDirTextBox.Name = "DoverDirTextBox";
        this.DoverDirTextBox.Size = new System.Drawing.Size(171, 20);
        this.DoverDirTextBox.TabIndex = 22;
        // 
        // FIODirTextBox
        // 
        this.FIODirTextBox.Location = new System.Drawing.Point(15, 76);
        this.FIODirTextBox.Name = "FIODirTextBox";
        this.FIODirTextBox.Size = new System.Drawing.Size(171, 20);
        this.FIODirTextBox.TabIndex = 21;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(12, 180);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(115, 13);
        this.label4.TabIndex = 20;
        this.label4.Text = "Номер доверенности";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(12, 99);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(115, 13);
        this.label3.TabIndex = 19;
        this.label3.Text = "Номер доверенности";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(12, 141);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(104, 13);
        this.label2.TabIndex = 18;
        this.label2.Text = "Главный бухгалтер";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 60);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(146, 13);
        this.label1.TabIndex = 17;
        this.label1.Text = "Руководитель организации";
        // 
        // InvoiceForm
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(201, 263);
        this.Controls.Add(this.DoverBuhTextBox);
        this.Controls.Add(this.FIOBuhTextBox);
        this.Controls.Add(this.DoverDirTextBox);
        this.Controls.Add(this.FIODirTextBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
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
      private System.Windows.Forms.TextBox DoverBuhTextBox;
      private System.Windows.Forms.TextBox FIOBuhTextBox;
      private System.Windows.Forms.TextBox DoverDirTextBox;
      private System.Windows.Forms.TextBox FIODirTextBox;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
  }
}