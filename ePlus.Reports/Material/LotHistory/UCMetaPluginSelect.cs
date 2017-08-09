using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace RCSLotHistory
{
    public partial class UCMetaPluginSelect : UserControl
    {
        private EButtonStyle buttonStyle = EButtonStyle.SelectClear;
        private PluginFormView pluginContol = null;
        DataRowItem rowItem = new DataRowItem();
        private string mnemocode = "";

        public delegate void ValueChangedDelegate();
        public event ValueChangedDelegate ValueChanged = null;

        public delegate void ValidatingItemDelegate(ref DataRowItem item);
        public event ValidatingItemDelegate ValidatingItem = null;

        public PluginFormView PluginContol
        {
            get
            {
                if (pluginContol == null)
                {
                    pluginContol = AppManager.GetPluginView(mnemocode);
                }
                return pluginContol;
            }
        }

        public UCMetaPluginSelect()
        {
            InitializeComponent();
            RefreshLayout();

            HelperStrip.SetImage(buttonSelect, "SYS_OPEN_PLUGIN");
            HelperStrip.SetImage(buttonClear, "SYS_CLEAR_VALUE");
        }

        public EButtonStyle ButtonStyle
        {
            get { return buttonStyle; }
            set
            {
                buttonStyle = value;
                RefreshLayout();
            }
        }

        [Browsable(true)]
        public string Mnemocode
        {
            get { return mnemocode; }
            set
            {
                mnemocode = value;
                if (DesignMode)
                {
                    textText.Text = string.Format("Плагин: {0}", mnemocode);
                }
            }
        }

        public void SetId(long value)
        {
            if (PluginContol == null) return;
            IGridController gridController = PluginContol.Grid(1);
            if (gridController == null) return;

            DataRow row = gridController.LoadRowById(value);
            SetValues(gridController, row);
        }

        public void SetGuid(Guid value)
        {
            if (PluginContol == null) return;
            IGridController gridController = PluginContol.Grid(1);
            if (gridController == null) return;

            DataRow row = gridController.LoadRowByGuid(value);
            SetValues(gridController, row);
        }

        public void SetCode(string value)
        {
            if (PluginContol == null) return;
            IGridController gridController = PluginContol.Grid(1);
            if (gridController == null) return;

            DataRow row = gridController.LoadRowByCode(value);
            SetValues(gridController, row);
        }

        public long Id
        {
            get { return rowItem.Id; }
        }

        public Guid Guid
        {
            get { return rowItem.Guid; }
        }

        public string Code
        {
            get { return rowItem.Code; }
        }

        public string CodeText
        {
            get { return textText.Text; }
        }

        public DataRowItem RowItem
        {
            get { return rowItem; }
        }

        new public string Text
        {
            get { return textText.Text; }
            set
            {
                textText.Text = value;
                rowItem.Text = value;
            }
        }

        public void PressSelectButton()
        {
            ButtonSelect_Click(null, null);
        }

        public event EventHandler BeforePluginShow;

        protected virtual void OnBeforePluginShow(EventArgs e)
        {
            if (BeforePluginShow != null)
            {
                BeforePluginShow(this, e);
            }
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            if (PluginContol == null) return;
            PluginContol.SelectMode = ESelectMode.SelectOne;
            OnBeforePluginShow(EventArgs.Empty);
            PluginContol.ShowDialog();
            if (PluginContol.SelectResult)
            {
                IGridController gridController = PluginContol.Grid(1);
                if (gridController != null)
                {
                    DataRow row = gridController.SelectedRow();
                    SetValues(gridController, row);
                }
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void RefreshLayout()
        {
            int dd = 1;
            int width = buttonClear.Width;

            if (buttonStyle == EButtonStyle.Select)
            {
                buttonSelect.Visible = true;
                buttonClear.Visible = false;

                textText.Width = Width - width - dd;
                buttonSelect.Left = Width - width;
            }
            else
            {
                buttonSelect.Visible = true;
                buttonClear.Visible = true;

                textText.Width = Width - (width + dd) * 2;
                buttonSelect.Left = Width - width * 2 - dd;
                buttonClear.Left = Width - width;
            }
        }

        private void ProcessKeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete || keyCode == Keys.Back)
            {
                ButtonClear_Click(null, null);
            }
            else if (IsSelectKey(keyCode))
            {
                ButtonSelect_Click(null, null);
            }
        }

        private static bool IsSelectKey(Keys keyCode)
        {
            return keyCode == Keys.Space || keyCode == Keys.Enter || keyCode == Keys.F4;
        }

        private void TextText_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessKeyDown(e.KeyCode);
        }

        private void ButtonSelect_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessKeyDown(e.KeyCode);
        }

        private void ButtonClear_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsSelectKey(e.KeyCode))
            {
                ButtonClear_Click(null, null);
                return;
            }
            ProcessKeyDown(e.KeyCode);
        }

        public void Clear()
        {
            SetValues(null, null);
        }

        public void SetValues(IGridController gridController, DataRow row)
        {
            if (gridController == null || row == null)
            {
                rowItem.Id = 0;
                rowItem.Code = null;
                rowItem.Guid = Guid.Empty;
                textText.Text = null;
                if (ValueChanged != null) ValueChanged();
                return;
            }

            DataRowItem rowItem2 = new DataRowItem(gridController, row);
            SetValues(rowItem2);
        }

        public void SetValues(DataRowItem dataRowItem)
        {
            if (ValidatingItem != null) ValidatingItem(ref dataRowItem);

            if (dataRowItem != null)
            {
                rowItem.Id = dataRowItem.Id;
                rowItem.Code = dataRowItem.Code;
                rowItem.Guid = dataRowItem.Guid;
                rowItem.Text = dataRowItem.Text;
                textText.Text = dataRowItem.Text;
                if (ValueChanged != null) ValueChanged();
            }
        }

        private void UCMetaPluginSelect_Leave(object sender, EventArgs e)
        {
            textText.BackColor = Color.White;
        }

        private void UCMetaPluginSelect_Enter(object sender, EventArgs e)
        {
            textText.BackColor = AppManager.FocusedColor;
        }
    }

    public enum EButtonStyle
    {
        Select, SelectClear
    }
}