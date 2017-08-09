using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace FCKInventorySessionInv19_Rigla
{
    public partial class InventorySessionInv19UserControl_Rigla : UserControl, IExternalDocumentPrintFormParamsControl
    {
        private InventorySessionInv19_Rigla _param;

        public InventorySessionInv19UserControl_Rigla()
        {
            InitializeComponent();
        }

        public void Object2Control(IExternalDocumentPrintFormParams param)
        {
            _param = (InventorySessionInv19_Rigla)param;
            rbSAL.Checked = _param.IsSal == 1;
            rbSUP.Checked = _param.IsSal == 2;
            rbStore.Checked = _param.ByStore;
            rbContractor.Checked = !rbStore.Checked;
            ucStore.Clear();
            if (!_param.ByStore) return;
            foreach (DataRowItem store in _param.Stores)
            {
                ucStore.AddRowItem(store);
            }
        }

        public void Control2Object()
        {
            _param.IsSal = rbSAL.Checked ? 1 : 2;
            _param.ByStore = rbStore.Checked;
            _param.Stores = new DataRowItem[ucStore.Items.Count];
            int i = 0;
            foreach (DataRowItem store in ucStore.Items)
            {
                _param.Stores[i++] = store;
            }
        }

        public void Clear()
        {
            rbContractor.Checked = true;
            rbStore.Checked = false;
            rbSUP.Checked = false;
            rbSAL.Checked = true;
            ucStore.Clear();
        }

        public Control Control
        {
            get { return this; }
        }

        private void RadioCheckedChanged(object sender, EventArgs e)
        {
            ucStore.Enabled = rbStore.Checked;
        }
    }
}