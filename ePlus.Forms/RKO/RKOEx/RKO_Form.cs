using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RKOEx
{
    public partial class RKO_Form : Form
    {
        public RKO_Form()
        {
            InitializeComponent();
        }

        public string NumberRKO
        {
            get { return numberTextBox.Text; }
            set { numberTextBox.Text = value; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}