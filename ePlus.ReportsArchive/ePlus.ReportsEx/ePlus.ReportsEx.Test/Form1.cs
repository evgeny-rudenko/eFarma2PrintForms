using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.ReferenceExchange;

namespace ePlus.ReportsEx.Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FrmImportYaroslavlCodes frm = new FrmImportYaroslavlCodes();
			frm.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			YaroslavlExporter.Export();
			
		}
	}
}