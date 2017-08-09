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

namespace InventoryEmptyEx
{
    public partial class InventoryEmptyParams: Form
    {        
        public InventoryEmptyParams()
        {
            InitializeComponent();
        }
		
		public int Num
		{
			get { return int.Parse(numberTextBox.Text); }
			set { numberTextBox.Text = value.ToString(); }
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			int num = 2;
			if (!int.TryParse(numberTextBox.Text, out num))
			{
				MessageBox.Show("„исло страниц должно быть целым положительным числом!", "е‘арма", MessageBoxButtons.OK);
				return;
			}

			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
    }
}