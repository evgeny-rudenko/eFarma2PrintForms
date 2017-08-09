using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RCRImportOrdersExcel
{
    public partial class SettingForm : Form
    {
        private Settings setting;
        private List<string> columns = new List<string>();

        public List<string> Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        
        public Settings Setting
        {
            get { return setting; }
            set { setting = value; }
        }

        public SettingForm()
        {
            InitializeComponent();            
        }

        public SettingForm(Dictionary<string, string> _mapping, Settings _setting) :this()
        {                        
            foreach (KeyValuePair<string, string> keyValuePair in _mapping)
            {
                cbStore.Items.Add(keyValuePair.Key);
                cbContracts.Items.Add(keyValuePair.Key);
                cbBuyer.Items.Add(keyValuePair.Key);
                cbQuantity.Items.Add(keyValuePair.Key);
            }
            if (_setting == null)
                cbStore.SelectedIndex = cbContracts.SelectedIndex = cbBuyer.SelectedIndex = cbQuantity.SelectedIndex = 0;
            else
            {
                cbStore.SelectedItem = _setting.STORE_COL;
                cbContracts.SelectedItem = _setting.CONTRACT_COL;
                cbBuyer.SelectedItem = _setting.BUYER_COL;
                cbQuantity.SelectedItem = _setting.QUANTITY_COL;
                nbBegin.Value = _setting.BEGIN_ROW;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Setting = new Settings();
            Setting.BEGIN_ROW = (int)nbBegin.Value;
            Setting.STORE_COL = (string)cbStore.SelectedItem;
            //if (!Columns.Exists(delegate(string _set) { return _set == Setting.STORE_COL; }))
            //    Columns.Add(Setting.STORE_COL);

            Setting.QUANTITY_COL = (string)cbQuantity.SelectedItem;
            //if (!Columns.Exists(delegate(string _set) { return _set == Setting.QUANTITY_COL; }))
            //    Columns.Add(Setting.QUANTITY_COL);

            Setting.CONTRACT_COL = (string)cbContracts.SelectedItem;
            //if (!Columns.Exists(delegate(string _set) { return _set == Setting.CONTRACT_COL; }))
            //    Columns.Add(Setting.CONTRACT_COL);

            Setting.BUYER_COL = (string)cbBuyer.SelectedItem;
            //if (!Columns.Exists(delegate(string _set) { return _set == Setting.BUYER_COL; }))
            //    Columns.Add(Setting.BUYER_COL);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}