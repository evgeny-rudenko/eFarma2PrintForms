using System;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Windows.Forms;

namespace FCBInvoiceOutInvoice_Rigla
{
  public partial class InvoiceForm : Form
  {
    public InvoiceForm()
    {
      InitializeComponent();
    }
      public string FIODirContractor;
      public string FIOBuhContractor;
      public string DoverDirContractor;
      public string DoverBuhContractor;
    public string Number
    {
      get { return numberTextBox.Text; }
      set { numberTextBox.Text = value; }
    }
      public string FIODir
      {
          get { return FIODirTextBox.Text; }
          set { FIODirTextBox.Text = value; }
      }
      public string FIOBuh
      {
          get { return FIOBuhTextBox.Text; }
          set { FIOBuhTextBox.Text = value; }
      }
      public string DoverDir
      {
          get { return DoverDirTextBox.Text; }
          set { DoverDirTextBox.Text = value; }
      }
      public string DoverBuh
      {
          get { return DoverBuhTextBox.Text; }
          set { DoverBuhTextBox.Text = value; }
      }
    private void okButton_Click(object sender, EventArgs e)
    {
      SaveSettings();
      DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

    private void InvoiceForm_Load(object sender, EventArgs e)
    {
        LoadSettings();
    }
      private void LoadSettings()
      {
          if (!File.Exists(SettingsFilePath)) return;
          XmlDocument doc = new XmlDocument();
          doc.Load(SettingsFilePath);

          XmlNode root = doc.SelectSingleNode("/XML");
          FIODir = Utils.GetString(root, "DIRECTOR");
          DoverDir = Utils.GetString(root, "DOC_DIRECTOR");
          FIOBuh = Utils.GetString(root, "BUHGALTER");
          DoverBuh = Utils.GetString(root, "DOC_BUHGALTER");
      }
      private void SaveSettings()
      {
          XmlDocument docSave = new XmlDocument();
          XmlNode root = Utils.AddNode(docSave, "XML");
          Utils.AddNode(root, "DIRECTOR", FIODir);
          Utils.AddNode(root, "DOC_DIRECTOR", DoverDir);
          Utils.AddNode(root, "BUHGALTER", FIOBuh);
          Utils.AddNode(root, "DOC_BUHGALTER", DoverBuh);

          docSave.Save(SettingsFilePath);
      }
    private string SettingsFilePath
    {
        get
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
        }
    }

    private void InvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        
    }

      private void button1_Click(object sender, EventArgs e)
      {
          /*
        FIODir = FIODirContractor;
        DoverDir = DoverDirContractor;
        FIOBuh = FIOBuhContractor;
        DoverBuh = DoverBuhContractor;
           * */
      }
  }
}