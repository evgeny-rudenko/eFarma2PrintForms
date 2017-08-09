using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActReturnTORG2Ex
{
    public partial class ChooseContractor : Form
    {
        long Input;
        public ChooseContractor()
        {
            InitializeComponent();
        }

        public long GetResult()
        {
            return ucContractor.Id;
        }

        public void Init(long Value)
        {
            ucContractor.SetId(Value);
            Input = Value;
        }
        // Закрытие
        private void button2_Click(object sender, EventArgs e)
        {
            bool Tmp = false;
            if (ucContractor.Id != Input)
            {
                if (MessageBox.Show("При закрытии окна контрагент будет выбран из документа.\nПродолжить?", "Внимание", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ucContractor.SetId(Input);
                    Tmp = true;
                }   
            }
            if (!Tmp)
                this.DialogResult = DialogResult.None;
        }
        // Подтверждение выбора
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}