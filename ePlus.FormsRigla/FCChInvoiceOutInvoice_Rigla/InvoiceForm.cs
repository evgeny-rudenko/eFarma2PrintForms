// Type: FCChInvoiceOutInvoice_Rigla.InvoiceForm
// Assembly: FCChInvoiceOutInvoice_Rigla_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 508D15D3-CFC0-430C-86B9-F207DB5E1844
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCChInvoiceOutInvoice_Rigla_8.dll

using ePlus.MetaData.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace FCChInvoiceOutInvoice_Rigla
{
  public class InvoiceForm : Form
  {
    private IContainer components = (IContainer) null;
    private Button cancelButton;
    private Button okButton;
    private Label numberLabel;
    private TextBox numberTextBox;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private TextBox FIODirTextBox;
    private TextBox DoverDirTextBox;
    private TextBox FIOBuhTextBox;
    private TextBox DoverBuhTextBox;

    public string DoverBuh
    {
      get
      {
        return this.DoverBuhTextBox.Text;
      }
      set
      {
        this.DoverBuhTextBox.Text = value;
      }
    }

    public string DoverDir
    {
      get
      {
        return this.DoverDirTextBox.Text;
      }
      set
      {
        this.DoverDirTextBox.Text = value;
      }
    }

    public string FIOBuh
    {
      get
      {
        return this.FIOBuhTextBox.Text;
      }
      set
      {
        this.FIOBuhTextBox.Text = value;
      }
    }

    public string FIODir
    {
      get
      {
        return this.FIODirTextBox.Text;
      }
      set
      {
        this.FIODirTextBox.Text = value;
      }
    }

    public string Number
    {
      get
      {
        return this.numberTextBox.Text;
      }
      set
      {
        this.numberTextBox.Text = value;
      }
    }

    private string SettingsFilePath
    {
      get
      {
        return Path.Combine(Utils.TempDir(), ((object) Assembly.GetExecutingAssembly().GetName().Name).ToString() + ".xml");
      }
    }

    public InvoiceForm()
    {
      this.InitializeComponent();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cancelButton = new Button();
      this.okButton = new Button();
      this.numberLabel = new Label();
      this.numberTextBox = new TextBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.FIODirTextBox = new TextBox();
      this.DoverDirTextBox = new TextBox();
      this.FIOBuhTextBox = new TextBox();
      this.DoverBuhTextBox = new TextBox();
      this.SuspendLayout();
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Location = new Point(102, 210);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(75, 23);
      this.cancelButton.TabIndex = 8;
      this.cancelButton.Text = "Отмена";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.okButton.Location = new Point(21, 210);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(75, 23);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "Отчет";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.numberLabel.AutoSize = true;
      this.numberLabel.Location = new Point(12, 9);
      this.numberLabel.Name = "numberLabel";
      this.numberLabel.Size = new Size(162, 13);
      this.numberLabel.TabIndex = 7;
      this.numberLabel.Text = "Введите номер счета-фактуры";
      this.numberTextBox.Location = new Point(15, 25);
      this.numberTextBox.Name = "numberTextBox";
      this.numberTextBox.Size = new Size(171, 20);
      this.numberTextBox.TabIndex = 5;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 48);
      this.label1.Name = "label1";
      this.label1.Size = new Size(146, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Руководитель организации";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 129);
      this.label2.Name = "label2";
      this.label2.Size = new Size(104, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Главный бухгалтер";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(12, 87);
      this.label3.Name = "label3";
      this.label3.Size = new Size(115, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Номер доверенности";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(12, 168);
      this.label4.Name = "label4";
      this.label4.Size = new Size(115, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "Номер доверенности";
      this.FIODirTextBox.Location = new Point(15, 64);
      this.FIODirTextBox.Name = "FIODirTextBox";
      this.FIODirTextBox.Size = new Size(171, 20);
      this.FIODirTextBox.TabIndex = 13;
      this.DoverDirTextBox.Location = new Point(15, 103);
      this.DoverDirTextBox.Name = "DoverDirTextBox";
      this.DoverDirTextBox.Size = new Size(171, 20);
      this.DoverDirTextBox.TabIndex = 14;
      this.FIOBuhTextBox.Location = new Point(15, 145);
      this.FIOBuhTextBox.Name = "FIOBuhTextBox";
      this.FIOBuhTextBox.Size = new Size(171, 20);
      this.FIOBuhTextBox.TabIndex = 15;
      this.DoverBuhTextBox.Location = new Point(15, 184);
      this.DoverBuhTextBox.Name = "DoverBuhTextBox";
      this.DoverBuhTextBox.Size = new Size(171, 20);
      this.DoverBuhTextBox.TabIndex = 16;
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.CancelButton = (IButtonControl) this.cancelButton;
      this.ClientSize = new Size(196, 243);
      this.Controls.Add((Control) this.DoverBuhTextBox);
      this.Controls.Add((Control) this.FIOBuhTextBox);
      this.Controls.Add((Control) this.DoverDirTextBox);
      this.Controls.Add((Control) this.FIODirTextBox);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.numberLabel);
      this.Controls.Add((Control) this.numberTextBox);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InvoiceForm";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Параметры";
      this.Load += new EventHandler(this.InvoiceForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void InvoiceForm_Load(object sender, EventArgs e)
    {
      this.LoadSettings();
    }

    private void LoadSettings()
    {
      if (!File.Exists(this.SettingsFilePath))
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.SettingsFilePath);
      XmlNode xmlNode = xmlDocument.SelectSingleNode("/XML");
      this.FIODir = Utils.GetString(xmlNode, "DIRECTOR");
      this.DoverDir = Utils.GetString(xmlNode, "DOC_DIRECTOR");
      this.FIOBuh = Utils.GetString(xmlNode, "BUHGALTER");
      this.DoverBuh = Utils.GetString(xmlNode, "DOC_BUHGALTER");
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.SaveSettings();
      this.DialogResult = DialogResult.OK;
    }

    private void SaveSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode xmlNode = Utils.AddNode((XmlNode) xmlDocument, "XML");
      Utils.AddNode(xmlNode, "DIRECTOR", this.FIODir);
      Utils.AddNode(xmlNode, "DOC_DIRECTOR", this.DoverDir);
      Utils.AddNode(xmlNode, "BUHGALTER", this.FIOBuh);
      Utils.AddNode(xmlNode, "DOC_BUHGALTER", this.DoverBuh);
      xmlDocument.Save(this.SettingsFilePath);
    }
  }
}
