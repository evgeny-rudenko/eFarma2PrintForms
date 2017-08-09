using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.IO;

namespace RCChCheckDataIntegrity
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public FormParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
            DataTable DTRes = new DataTable();
            DataSet DS = new DataSet();
            DTRes = (new CHECK_DATABASE_BL()).Check((List<CHECK_DATABASE>)gridDocs.DataSource);
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];
            DS.Tables.Add(DTRes);
            rep.DataSource = DS;
            rep.BindDataSource("CHECK_DATA_INTEGRITY_DS_Table", 0);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
		 	rep.ExecuteReport(this);
		}

		public string ReportName
		{
            get { return "Контроль целостности данных"; }
		}

		public override string GroupName
		{
            get { return string.Empty; }
		}

        private void FormParams_Load(object sender, EventArgs e)
        {
            List<CHECK_DATABASE> ChList = new List<CHECK_DATABASE>();
            List<CHECK_DATABASE> ChAll = (new CHECK_DATABASE_BL()).List();
            foreach(CHECK_DATABASE itm in ChAll)
            {
                if (itm.CODE!="STARTUP_CHECK" &&
                    itm.CODE != "REFRESH_ALL_DOCUMENT" &&
                    itm.CODE != "REFRESH_DOC_MOVEMENT" &&
                    itm.CODE != "CHECK_CHEQUE_LOT" &&
                    itm.CODE != "STOCK_RECORD_CHECK_QUANTITY" &&
                    itm.CODE != "CHECK_SUSPECT_LOT_MOVEMENT" &&
                    itm.CODE != "STOCK_RECORD_CHECK_LOT" &&
                    itm.CODE != "CHECK_LOT_MOVEMENT_BY_DOC" &&
                    itm.CODE != "CHECK_LOT_MOVEMENT_NOT_PROC_DOC" &&
                    itm.CODE != "CHECK_PROC_DOC_WOUT_LOT_MOVEMENT" &&
                    itm.CODE != "CHECK_GOODS_MNEMOCODE" &&
                    itm.CODE != "CORRECT_LOGIN_LANGUAGE" &&
                    itm.CODE != "PRICE_LIST_ITEM_PRICE_DELETE_OLD")
                ChList.Add(itm);
            }
            gridDocs.DataSource = ChList;
            gridDocs.DataGridView.Columns[2].ReadOnly = true;
            gridDocs.DataGridView.MultiSelect = true;
            gridDocs.DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDocs.DataGridView.KeyDown += new KeyEventHandler(OnGridDocsKeyDon);
            foreach (DataGridViewRow row in gridDocs.DataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!(cell.Value is string)) continue;
                    cell.Style.WrapMode = DataGridViewTriState.True;
                }
            }
            gridDocs.DataGridView.CellBeginEdit += delegate(object obj, DataGridViewCellCancelEventArgs e1)
            {
                e1.Cancel = (!((List<CHECK_DATABASE>)gridDocs.DataSource)[e1.RowIndex].IS_CORRECTABLE && gridDocs.DataGridView.Columns[e1.ColumnIndex].Name == "IS_CORRECT");
            };

            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            Invalidate();
        }
        private void OnGridDocsKeyDon(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                ((DataGridView)sender).BeginEdit(false);
                for (int i = 0; i < ((DataGridView)sender).SelectedRows.Count; i++)
                {
                    DataGridViewRow row = ((DataGridView)sender).SelectedRows[i];
                    CHECK_DATABASE chd = (CHECK_DATABASE)gridDocs.BindingSource[((DataGridView)sender).Rows.IndexOf(row)];
                    chd.IS_RUN = !chd.IS_RUN;
                    gridDocs.BindingSource.ResetItem(((DataGridView)sender).Rows.IndexOf(row));
                }
                ((DataGridView)sender).EndEdit();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Space)
            {
                ((DataGridView)sender).BeginEdit(false);
                for (int i = 0; i < ((DataGridView)sender).SelectedRows.Count; i++)
                {
                    DataGridViewRow row = ((DataGridView)sender).SelectedRows[i];
                    CHECK_DATABASE chd = (CHECK_DATABASE)gridDocs.BindingSource[((DataGridView)sender).Rows.IndexOf(row)];
                    chd.IS_CORRECT = chd.IS_CORRECTABLE ? !chd.IS_CORRECT : false;
                    gridDocs.BindingSource.ResetItem(((DataGridView)sender).Rows.IndexOf(row));
                }
                ((DataGridView)sender).EndEdit();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            gridDocs.DataGridView.BeginEdit(false);
            gridDocs.DataGridView.SelectAll();
            for (int i = 0; i < gridDocs.DataGridView.SelectedRows.Count; i++)
            {
                DataGridViewRow row = gridDocs.DataGridView.SelectedRows[i];
                CHECK_DATABASE chd = (CHECK_DATABASE)gridDocs.BindingSource[gridDocs.DataGridView.Rows.IndexOf(row)];
                chd.IS_RUN = true;
                gridDocs.BindingSource.ResetItem(gridDocs.DataGridView.Rows.IndexOf(row));
            }
            gridDocs.DataGridView.EndEdit();
            gridDocs.DataGridView.ClearSelection();
        }

        private void buttonDeselectAll_Click(object sender, EventArgs e)
        {
            gridDocs.DataGridView.BeginEdit(false);
            gridDocs.DataGridView.SelectAll();
            for (int i = 0; i < gridDocs.DataGridView.SelectedRows.Count; i++)
            {
                DataGridViewRow row = gridDocs.DataGridView.SelectedRows[i];
                CHECK_DATABASE chd = (CHECK_DATABASE)gridDocs.BindingSource[gridDocs.DataGridView.Rows.IndexOf(row)];
                chd.IS_RUN = false;
                gridDocs.BindingSource.ResetItem(gridDocs.DataGridView.Rows.IndexOf(row));
            }
            gridDocs.DataGridView.EndEdit();
            gridDocs.DataGridView.ClearSelection();
        }
	}
}