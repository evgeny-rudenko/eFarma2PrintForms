using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace GoodsKeepingTimeListEx
{
	public partial class InvoiceRemainsParams : ExternalReportForm, IExternalReportFormMethods
	{
		private class ConditionItem
		{
			private long _conditionId;
			private bool _controlsVisible;
			private string _conditionName;
			private string _conditionFullName;

			public ConditionItem(long conditionId, bool controlsVisible, string conditionName, string conditionFullName)
			{
				_conditionId = conditionId;
				_controlsVisible = controlsVisible;
				_conditionName = conditionName;
				_conditionFullName = conditionFullName;
			}

			public long ConditionId
			{
				get { return _conditionId; }
			}

			public bool ControlsVisible
			{
				get { return _controlsVisible; }
			}

			public string ConditionName
			{
				get { return _conditionName; }
			}

			public string ConditionFullName
			{
				get { return _conditionFullName; }
			}

			public override string ToString()
			{
				return _conditionName;
			}
		}

		private class SortItem
		{
			private long _sortId;
			private string _sortName;

			public SortItem(long sortId, string sortName)
			{
				_sortId = sortId;
				_sortName = sortName;
			}
			public long SortId
			{
				get { return _sortId; }
			}

			public override string ToString()
			{
				return _sortName;
			}
		}

		private DataTable paramsTable = new DataTable();

		private ConditionItem SelectedCondition
		{
			get { return bestBeforeComboBox.SelectedItem as ConditionItem; }
		}

		private SortItem SelectedStoreSort
		{
			get { return storeNameSortingComboBox.SelectedItem as SortItem; }
		}

		private SortItem SelectedStorePlaceSort
		{
			get { return storePlaceSortingComboBox.SelectedItem as SortItem; }
		}

		private SortItem SelectedGoodsSort
		{
			get	{ return goodsNameSortingComboBox.SelectedItem as SortItem;	}
		}

		public InvoiceRemainsParams()
		{
			InitializeComponent();

			paramsTable.Columns.Add("Имя фильтра", typeof(String));
			paramsTable.Columns.Add("Значение фильтра", typeof(String));
			paramsTable.Columns.Add("Код фильтра", typeof(String));

			filtersDataGridView.DataSource = this.paramsTable;
			filtersDataGridView.AutoGenerateColumns = true;
			filtersDataGridView.AutoGenerateColumns = true;
			filtersDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			filtersDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			filtersDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders);
			filtersDataGridView.Columns[2].Visible = false;
						
			bestBeforeComboBox.Items.Add(new ConditionItem(0, false, "Больше", "Больше"));
			bestBeforeComboBox.Items.Add(new ConditionItem(1, false, "Меньше", "Меньше"));
			bestBeforeComboBox.Items.Add(new ConditionItem(2, false, "Равен", "Равен"));
			bestBeforeComboBox.Items.Add(new ConditionItem(3, true, "Период", "Период с"));
			bestBeforeComboBox.Items.Add(new ConditionItem(4, false, "Любой", "Любой"));

			storeNameSortingComboBox.Items.Add(new SortItem(0, "Без сортировки"));
			storeNameSortingComboBox.Items.Add(new SortItem(1, "По возрастанию"));
			storeNameSortingComboBox.Items.Add(new SortItem(2, "По убыванию"));			

			storePlaceSortingComboBox.Items.Add(new SortItem(0, "Без сортировки"));
			storePlaceSortingComboBox.Items.Add(new SortItem(1, "По возрастанию"));
			storePlaceSortingComboBox.Items.Add(new SortItem(2, "По убыванию"));
						
			goodsNameSortingComboBox.Items.Add(new SortItem(0, "Без сортировки"));
			goodsNameSortingComboBox.Items.Add(new SortItem(1, "По возрастанию"));
			goodsNameSortingComboBox.Items.Add(new SortItem(2, "По убыванию"));
			
			ClearValues();
		}

		private void AppendList(string keyName, IList<DataRowItem> list, StringBuilder stringBuilder)
		{
			foreach (DataRowItem row in list)
				stringBuilder.AppendFormat("<{0} ID_{0}=\"{1}\"/>", keyName, row.Id);
		}

		private void AppendStorePlace(string key, IList<DataRowItem> storePlaces, StringBuilder param)
		{
			foreach (DataRowItem row in storePlaces)
				param.AppendFormat("<{0} ID_{0}=\"{1}\"/>", key, row.Guid);
		}

		private string GetXml()
		{
			StringBuilder xmlParam = new StringBuilder("<XML>");

			xmlParam.AppendFormat("<DOC_DATE DATE_FROM=\"{0}\" DATE_TO=\"{1}\" ID_CONDITION=\"{2}\"/>",
								  Utils.SqlDate(fromDateTimePicker.Value), Utils.SqlDate(toDateTimePicker.Value),
								  SelectedCondition.ConditionId);

			xmlParam.AppendFormat("<SORT ID_GOODS_SORT=\"{0}\" ID_STORE_SORT=\"{1}\" ID_STORE_PLACE_SORT=\"{2}\"/>",
								  SelectedGoodsSort.SortId, SelectedStoreSort.SortId, SelectedStorePlaceSort.SortId);

			if (remainDateCheckBox.Checked)
				xmlParam.AppendFormat("<STOCK_DATE STOCK_DATE=\"{0}\"/>", Utils.SqlDate(remainsDateDateTimePicker.Value));

			AppendList("CONTRACTOR", contractorsPluginMultiSelect.Items, xmlParam);
			AppendList("ATS_CLASSIFIER", farmGroupsPluginMultiSelect.Items, xmlParam);
			AppendList("GOODS", goodsPluginMultiSelect.Items, xmlParam);
			AppendList("PRODUCER", producersPluginMultiSelect.Items, xmlParam);
			AppendList("STORE", storesPluginMultiSelect.Items, xmlParam);
			AppendList("TAX_TYPE", vatRatesPluginMultiSelect.Items, xmlParam);
			AppendStorePlace("STORE_PLACE", storePlacesPluginMultiSelect.Items, xmlParam);
			xmlParam.Append("</XML>");

			return xmlParam.ToString();
		}

		public void Print(string[] reportFiles)
		{
			if (SelectedCondition.ConditionId == 3 && fromDateTimePicker.Value > toDateTimePicker.Value)
			{
				MessageBox.Show("Некорректный период", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				filtersTabContol.SelectTab("bestBeforesTabPage");				
				return;
			}
						
			ReportFormNew rep = new ReportFormNew();

			if (shortFormCheckBox.Checked)
				rep.ReportPath = reportFiles[0];
			else
				rep.ReportPath = reportFiles[1];
						
			rep.LoadData("REPEX_GOODS_STOCK", GetXml());
			rep.BindDataSource("GoodsStockDataSet_Table", 0, false);
			rep.BindDataSource("GoodsStockDataSet_Table1", 1, false);
			rep.BindDataSource("GoodsStockDataSet_Table2", 2, false);
			rep.BindDataSource("GoodsStockDataSet_Table3", 3, false);
			rep.BindDataSource("GoodsStockDataSet_Table4", 4, false);
			rep.BindDataSource("GoodsStockDataSet_Table5", 5, false);
			rep.BindDataSource("GoodsStockDataSet_Table6", 6, false);
			rep.BindDataSource("GoodsStockDataSet_Table7", 7);
			rep.BindDataSource("GoodsStockDataSet_Table8", 8, false);

			ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("STOCK_DATE", remainDateCheckBox.Checked ?
					remainsDateDateTimePicker.Value.ToShortDateString() : string.Empty)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			fromDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
			toDateTimePicker.Value = DateTime.Now;
			remainsDateDateTimePicker.Value = DateTime.Now;

			storesPluginMultiSelect.Items.Clear();
			contractorsPluginMultiSelect.Items.Clear();
			producersPluginMultiSelect.Items.Clear();
			goodsPluginMultiSelect.Items.Clear();
			farmGroupsPluginMultiSelect.Clear();
			vatRatesPluginMultiSelect.Items.Clear();
			storePlacesPluginMultiSelect.Items.Clear();

			bestBeforeComboBox.SelectedIndex = 4;
			storeNameSortingComboBox.SelectedIndex = 0;
			storePlaceSortingComboBox.SelectedIndex = 0;
			goodsNameSortingComboBox.SelectedIndex = 0;

			remainDateCheckBox.Checked = true;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Информация по остаткам медикаментов"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ChangeGrid(UCPluginMultiSelect plugin)
		{
			bool isCleared = false;
			while (isCleared == false)
			{
				isCleared = true;
				foreach (DataRow dr in paramsTable.Rows)
				{
					if (dr["Имя фильтра"].ToString() == plugin.Caption)
					{
						dr.Delete();
						isCleared = false;
						break;
					}
				}
			}

			foreach (DataRowItem dri in plugin.Items)
			{
				DataRow newDr = paramsTable.NewRow();
				newDr["Имя фильтра"] = plugin.Caption;
				newDr["Значение фильтра"] = dri.Text;
				paramsTable.Rows.Add(newDr);
			}
			filtersDataGridView.Refresh();
		}

		private void storesPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(storesPluginMultiSelect);
		}

		private void contractorsPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(contractorsPluginMultiSelect);
		}

		private void producersPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(producersPluginMultiSelect);
		}

		private void goodsPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(goodsPluginMultiSelect);
		}

		private void farmGroupsPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(farmGroupsPluginMultiSelect);
		}

		private void vatRatesPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(vatRatesPluginMultiSelect);
		}

		private void storePlacesPluginMultiSelect_ValuesListChanged()
		{
			ChangeGrid(storePlacesPluginMultiSelect);
		}

		private void remainDateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			string filterName = remainsDateLabel.Text;
			bool isCleared = false;
			while (isCleared == false)
			{
				isCleared = true;
				foreach (DataRow dr in paramsTable.Rows)
				{
					if (dr["Имя фильтра"].ToString().Contains(filterName))
					{
						dr.Delete();
						isCleared = false;
						break;
					}
				}
			}
			if (remainDateCheckBox.Checked)
			{
				DataRow newDr = paramsTable.NewRow();
				newDr["Имя фильтра"] = filterName;
				newDr["Значение фильтра"] = remainsDateDateTimePicker.Value.ToLongDateString();
				paramsTable.Rows.Add(newDr);
			}
			filtersDataGridView.Refresh();
		}

		private void storeNameSortingComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string filterName = sortingTabPage.Text;
			bool isCleared = false;
			while (isCleared == false)
			{
				isCleared = true;
				foreach (DataRow dr in paramsTable.Rows)
				{
					if (dr["Имя фильтра"].ToString().Contains(filterName))
					{
						dr.Delete();
						isCleared = false;
						break;
					}
				}
			}

			string filterValue = string.Empty;

			StringBuilder value = new StringBuilder();

			if (storeNameSortingComboBox.SelectedIndex > 0)
			{
				value.Append(storeNameSortingLabel.Text);
				value.Append(' ');
				value.Append(storeNameSortingComboBox.Text);
				value.Append("; ");
			}
			if (storePlaceSortingComboBox.SelectedIndex > 0)
			{
				value.Append(storePlaceSortingLabel.Text);
				value.Append(' ');
				value.Append(storePlaceSortingComboBox.Text);
				value.Append("; ");
			}
			if (goodsNameSortingComboBox.SelectedIndex > 0)
			{
				value.Append(goodsNameSortingLabel.Text);
				value.Append(' ');
				value.Append(goodsNameSortingComboBox.Text);
				value.Append("; ");
			}

			if (string.IsNullOrEmpty(filterValue) == false)
			{
				DataRow newDr = paramsTable.NewRow();
				newDr["Имя фильтра"] = filterName;
				newDr["Значение фильтра"] = filterValue;
				paramsTable.Rows.Add(newDr);
			}
			filtersDataGridView.Refresh();
		}

		private void bestBeforeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string filterName = bestBeforeLabel.Text;
			bool isCleared = false;
			while (isCleared == false)
			{
				isCleared = true;
				foreach (DataRow dr in paramsTable.Rows)
				{
					if (dr["Имя фильтра"].ToString().Contains(filterName))
					{
						dr.Delete();
						isCleared = false;
						break;
					}
				}
			}

			fromLabel.Text = SelectedCondition.ConditionFullName;
			toLabel.Visible = SelectedCondition.ControlsVisible;
			toDateTimePicker.Visible = SelectedCondition.ControlsVisible;

			if (bestBeforeComboBox.SelectedIndex == 4)
			{
				fromDateTimePicker.Enabled = false;
			}
			else
			{
				fromDateTimePicker.Enabled = true;

				string filterDates = string.Empty;
				string filterValue = string.Empty;
				filterDates = (SelectedCondition.ControlsVisible)
								? string.Format("с " + fromDateTimePicker.Text + "по " + toDateTimePicker.Text)
								: string.Format(fromDateTimePicker.Text);
				filterValue = bestBeforeComboBox.Text + " " + filterDates;

				DataRow newDr = paramsTable.NewRow();
				newDr["Имя фильтра"] = filterName;
				newDr["Значение фильтра"] = filterValue;
				paramsTable.Rows.Add(newDr);
			}

			filtersDataGridView.Refresh();
		}
	}
}