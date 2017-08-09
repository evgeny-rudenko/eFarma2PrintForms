using System;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
//using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace FCChInvoiceOutExportDBF
{
  public partial class InvoiceOutExportDBFForm : Form
  {
      private string settingsFilePath;
      public InvoiceOutExportDBFForm()
    {
      InitializeComponent();
    }

    public string AccessPoint
    {
      get { return accessPointAPExport.Text; }
      set { accessPointAPExport.Text = value; }
    }
    public string EncodingSave
    {
      get { return cbEncodList.Text; }
      //set { cbEncodList.Text = value; }
    }
    public void SetAccessPoint(string AP)
    {
        accessPointAPExport.Text = AP;
    }
    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

      private void label10_Click(object sender, EventArgs e)
      {

      }
      private void ClearValues()
      {
          accessPointAPExport.Clear();
      }
      private void LoadSettings()
      {
          if (!File.Exists(settingsFilePath))
          {
              cbEncodList.Text = "utf-8";
              return;
          }

          XmlDocument doc = new XmlDocument();
          doc.Load(settingsFilePath);
          XmlNode root = doc.SelectSingleNode("//XML");

          if (root == null)
              return;

          cbEncodList.Text = Utils.GetString(root, "ENCODE");
          accessPointAPExport.Text = Utils.GetString(root, "ACCES_POINT");
     }

      private void SaveSettings()
      {
          XmlDocument doc = new XmlDocument();
          XmlNode root;

          if (File.Exists(settingsFilePath))
          {
              doc.Load(settingsFilePath);
              root = doc.SelectSingleNode("//XML");
              root.RemoveAll();
          }
          else
          {
              root = Utils.AddNode(doc, "XML");
          }

          Utils.AddNode(root, "ENCODE", cbEncodList.Text);
          Utils.AddNode(root, "ACCES_POINT", accessPointAPExport.Text);
          doc.Save(settingsFilePath);
      }

      private void InvoiceOutExportXMLForm_Load(object sender, EventArgs e)
      {
          settingsFilePath = Path.Combine(Utils.TempDir(), System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName + ".xml");
          foreach (EncodingInfo ei in Encoding.GetEncodings())
          {
              //Encoding e = ei.GetEncoding();
              cbEncodList.Items.Add(ei.Name);
          }

          ClearValues();
          LoadSettings();
      }

      private void InvoiceOutExportXMLForm_FormClosed(object sender, FormClosedEventArgs e)
      {
          SaveSettings();
      }

  }
}