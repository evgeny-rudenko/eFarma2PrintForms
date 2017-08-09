using System;
using System.Windows.Forms;

namespace UPD
{
	public partial class InvoiceForm : Form
	{
		public InvoiceForm()
		{
			InitializeComponent();
		}

		public string Number
		{
			get { return numberTextBox.Text; }
			set { numberTextBox.Text = value; }
		}

		public string ChiefName
		{
			get { return chiefName.Text; }
			set { chiefName.Text = value; }
		}

		public string AccountantName
		{
			get { return accountantName.Text; }
			set { accountantName.Text = value; }
		}

		public string DocDetails
		{
			get { return docDetails.Text; }
			set { docDetails.Text = value; }
		}

		public string DocStatus
		{
			get { return docStatus.Text; }
			set { docStatus.Text = value; }
		}

		private void OkButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void CancelButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}