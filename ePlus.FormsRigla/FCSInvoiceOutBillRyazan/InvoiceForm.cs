// Type: InvoiceOutBillExRyazan.InvoiceForm
// Assembly: FCSInvoiceOutBillRyazan_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E955682C-D1E0-4105-9376-CCAC0C8967E2
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCSInvoiceOutBillRyazan_8.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InvoiceOutBillExRyazan
{
  public class InvoiceForm : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;
    private Button okButton;
    private Button cancelButton;
    private TextBox txtContract;

    public string Contract
    {
      get
      {
        return this.txtContract.Text;
      }
      set
      {
        this.txtContract.Text = value;
      }
    }

    public InvoiceForm()
    {
      this.InitializeComponent();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.okButton = new Button();
      this.cancelButton = new Button();
      this.txtContract = new TextBox();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(73, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(203, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "Введите номер договора";
      this.label1.Click += new EventHandler(this.label1_Click);
      this.okButton.Location = new Point(6, 103);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(110, 32);
      this.okButton.TabIndex = 7;
      this.okButton.Text = "Отчет";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Location = new Point(251, 103);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(108, 32);
      this.cancelButton.TabIndex = 9;
      this.cancelButton.Text = "Отмена";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.txtContract.Location = new Point(6, 61);
      this.txtContract.MaxLength = 150;
      this.txtContract.Name = "txtContract";
      this.txtContract.Size = new Size(368, 26);
      this.txtContract.TabIndex = 10;
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleDimensions = new SizeF(9f, 20f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.cancelButton;
      this.ClientSize = new Size(386, 149);
      this.Controls.Add((Control) this.txtContract);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Name = "InvoiceForm";
      this.Text = "Параметры";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
