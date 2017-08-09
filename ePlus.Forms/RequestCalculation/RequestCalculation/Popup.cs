using System;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.IO;

namespace FCSRequestCalculation
{
    public partial class Popup: Form
    {
		public Popup()
        {
            InitializeComponent();
        }
		
		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void Popup_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
				checkedListBox1.SetItemChecked(i, true);
			LoadSettings();						
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "RequestCalculation.xml");		
		private const string ITEM = "ITEM";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;
			
			for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
				checkedListBox1.SetItemChecked(i, Utils.GetBool(root, ITEM + i.ToString()));
			}
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

			for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
				Utils.AddNode(root, ITEM + i.ToString(), checkedListBox1.GetItemChecked(i));
			}

			doc.Save(settingsFilePath);
		}

		private void Popup_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
    }
}