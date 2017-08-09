using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ePlus.MetaData.Core;
using System.Xml;

namespace UnloadingInvoiceOutEx
{
    public partial class FormParam : Form
    {
        private UnloadingInvoiceOut unIO;
        private string fileN = Path.Combine(Utils.TempDir(), "UnloadingInvoiceOutSettings.xml");

        public FormParam()
        {
            InitializeComponent();
        }

        public FormParam(UnloadingInvoiceOut _unIO)
        {
            InitializeComponent();
            unIO = _unIO;
            tbFileName.Text = unIO.FileName;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if ((ucSelectFTP.Text != string.Empty) && (tbIdSupplier.Text != string.Empty) && (tbFileName.Text != string.Empty))
            {
                unIO.FTP = ucSelectFTP.Text;
                unIO.ID = tbIdSupplier.Text;
                unIO.FileName = tbFileName.Text;
                Close();
            }
            else
                MessageBox.Show("Заполните все поля формы");
        }

        private void FormParam_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileN)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileN);
            XmlNode root = doc.SelectSingleNode("/XML");
            ucSelectFTP.Guid = Utils.GetGuid(root, "GUID");
            ucSelectFTP.Text = Utils.GetString(root, "FTP");
            tbIdSupplier.Text = Utils.GetString(root, "IdSupplier");
        }

        private void FormParam_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "GUID", ucSelectFTP.Guid);
            Utils.AddNode(root, "FTP", ucSelectFTP.Text);
            Utils.AddNode(root, "IdSupplier", tbIdSupplier.Text);
            doc.Save(fileN);
        }
    }
}