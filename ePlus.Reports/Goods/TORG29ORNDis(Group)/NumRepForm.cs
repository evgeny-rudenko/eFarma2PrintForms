using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Core;

namespace RCBTorg29ORNDisGroup
{    
    public partial class NumRepForm : Form
    {
        public int Num
        {
            get { return (int)nbNum.Value; }
        }

        public NumRepForm()
        {
            InitializeComponent();
        }

        private void NumRepForm_Load(object sender, EventArgs e)
        {            
            string s = Utils.RegistryLoad("NumRepTOGroup");            
            nbNum.Value = Utils.GetInt(s)+1;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Utils.RegistrySave("NumRepTOGroup", nbNum.Text);
            Close();
        }
    }
}