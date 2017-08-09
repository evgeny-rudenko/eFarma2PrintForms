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

namespace GoodsKeepingTimeListEx
{
	public partial class GoodsKeepingTimeParams : ExternalReportForm, IExternalReportFormMethods
	{
		class ConditionItem
		{
			long _conditionId;
			bool _controlsVisible;
			string _conditionName;

			public ConditionItem(long conditionId, bool controlsVisible, string conditionName)
			{
				_conditionId = conditionId;
				_controlsVisible = controlsVisible;
				_conditionName = conditionName;
			}

			public long ConditionId 
			{
				get	{ return _conditionId; }
			}

			public bool ControlsVisible
			{
				get	{ return _controlsVisible; } }

			public string ConditionName
			{
				get	{ return _conditionName; }
			}

			public override string ToString()
			{
				return _conditionName;
			}
		}

		public GoodsKeepingTimeParams()
		{
			InitializeComponent();

			conditionComboBox.Items.Add(new ConditionItem(0, false, "Больше"));
			conditionComboBox.Items.Add(new ConditionItem(1, false, "Меньше"));
			conditionComboBox.Items.Add(new ConditionItem(2, false, "Равен"));
			conditionComboBox.Items.Add(new ConditionItem(3, true, "Период"));

			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			if (conditionComboBox.SelectedIndex == 3 && fromDateTimePicker.Value > toDateTimePicker.Value)
			{
				MessageBox.Show("Некорректный период", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				fromDateTimePicker.Focus();
				return;
			}

			ReportFormNew rep = new ReportFormNew();
			StringBuilder param = new StringBuilder("<XML>");

			if (ucMetaPlugin_Goods.Id > 0)
			{
				rep.AddParameter("GOODS", ucMetaPlugin_Goods.Text);
				param.Append(string.Format("<GOODSID>{0}</GOODSID>", ucMetaPlugin_Goods.Id));
			}

			if (ucMetaPlugin_Store.Id > 0)
			{
				rep.AddParameter("STORE", ucMetaPlugin_Store.Text);
				param.Append(string.Format("<STOREID>{0}</STOREID>", ucMetaPlugin_Store.Id));
			}

			param.Append(string.Format("<CONDITIONID>{0}</CONDITIONID>", SelectedCondition.ConditionId));
			param.Append(string.Format("<DATEFROM>{0}</DATEFROM>", Utils.SqlDate(fromDateTimePicker.Value)));
			param.Append(string.Format("<DATETO>{0}</DATETO>", Utils.SqlDate(toDateTimePicker.Value)));
			param.Append("</XML>");
						
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_GOODS_KEEPING_TIME_LIST", param.ToString());
			rep.BindDataSource("GoodsKeepingTimeList_rep_GoodsKeepingTimeList", 0);

			rep.AddParameter("BEST_BEFORE", GetConditionString());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");

			rep.ExecuteReport(this);
		}

		string GetConditionString()
		{
			switch (SelectedCondition.ConditionId)
			{
				case 0:
				case 1:
					return SelectedCondition.ConditionName.ToLower() + ", чем " + fromDateTimePicker.Value.ToShortDateString();
				case 2:
					return SelectedCondition.ConditionName.ToLower() + " " + fromDateTimePicker.Value.ToShortDateString();
				case 3:
					return SelectedCondition.ConditionName.ToLower() + " с " + fromDateTimePicker.Value.ToShortDateString() + " по " + toDateTimePicker.Value.ToShortDateString();
			}
			return string.Empty;
		}

		void ClearValues()
		{
			fromDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
			toDateTimePicker.Value = DateTime.Now;
			ucMetaPlugin_Store.SetId(0);
			ucMetaPlugin_Goods.SetId(0);
			conditionComboBox.SelectedIndex = 0;
		}

		void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		void conditionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			periodLabel.Text = SelectedCondition.ConditionName;
			dateToLabel.Visible = SelectedCondition.ControlsVisible;
			toDateTimePicker.Visible = SelectedCondition.ControlsVisible;
		}

		public string ReportName
		{
			get { return "Отчет о сроке годности товаров"; }
		}

		ConditionItem SelectedCondition
		{
			get { return conditionComboBox.SelectedItem as ConditionItem; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}
	}
}