using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.ReferenceExchange
{
	public partial class FrmYaroslavlExport : Form
	{
		public FrmYaroslavlExport()
		{
			InitializeComponent();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			YaroslavlExporter.Export();
		}
	}
}