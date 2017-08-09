using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace RCSStatistSaleProfitableGoods
{
	public partial class FormParam : ExternalReportForm, IExternalReportFormMethods
	{
		public FormParam()
		{
			InitializeComponent();
		}

		public string ReportName
		{
			get { return "Анализ продаж (прибыльные товары)"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}

		public void Print(string[] reportFiles)
		{
			if (rbCheckType.Checked && (!chbKKM.Checked && !chbOut.Checked && !chbMovement.Checked))
			{
				MessageBox.Show("Не выбраны виды расхода", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}		

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "DATE_FR", period.DateFrom);
			Utils.AddNode(root, "DATE_TO", period.DateTo);
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
			Utils.AddNode(root, "GROUPS", chbGroupGoods.Checked ? "1" : "0");
			Utils.AddNode(root, "PROFIT", profitComboBox.SelectedIndex);

			StringBuilder docs = new StringBuilder();
			if (rbAllType.Checked)
			{
				Utils.AddNode(root, "TYPE_NUM", "CHEQUE");
				Utils.AddNode(root, "TYPE_NUM", "INVOICE_OUT");
				Utils.AddNode(root, "TYPE_NUM", "MOVE");
				docs.Append("Все");
			}
			else
			{
				if (chbKKM.Checked)
				{
					Utils.AddNode(root, "TYPE_NUM", "CHEQUE");
					docs.Append("Чеки");
				}

				if (chbOut.Checked)
				{
					Utils.AddNode(root, "TYPE_NUM", "INVOICE_OUT");
					if (docs.Length != 0)
						docs.Append(',');
					docs.Append("Расходные накладные");
				}

				if (chbMovement.Checked)
				{
					Utils.AddNode(root, "TYPE_NUM", "MOVE");
					if (docs.Length != 0)
						docs.Append(',');
					docs.Append("Перемещения");
				}
			}

			stores.AddItems(root, "ID_STORE");
			goods.AddItems(root, "ID_GOODS");
			ucTradeName.AddItems(root, "ID_TRADE_NAME");

			foreach (CatalogItem item in selectGoodsGroup.Items)
			{
				Utils.AddNode(root, "ID_GROUP", item.Id);
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			StatistSale_Profitable_DS.ProfitableDataTable profitable = new StatistSale_Profitable_DS.ProfitableDataTable();
			StatistSale_Profitable_DS.FlagsDataTable flags = new StatistSale_Profitable_DS.FlagsDataTable();

			decimal totalIncome = 0m;
			decimal totalTurnover = 0m;
			decimal filter = 0m;			

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlParameter param = new SqlParameter("@XMLPARAM", SqlDbType.NVarChar);
				param.Value = doc.InnerXml;

				SqlCommand command = new SqlCommand(@"DBO.STATIST_SALE_PROFITABLE_GOODS", con);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(param);

				command.CommandTimeout = 60 * 60;
				con.Open();				
				
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						StatistSale_Profitable_DS.ProfitableRow row = profitable.NewProfitableRow();
						if (!reader.IsDBNull(0)) row.GOODS_CODE = reader.GetString(0);
						row.GOODS_NAME = reader.GetString(1);
						if (!reader.IsDBNull(2)) row.AU_G = reader.GetString(2);
						if (!reader.IsDBNull(3)) row.AU_G_AP = reader.GetString(3);
						if (!reader.IsDBNull(4)) row.PRODUCER_NAME = reader.GetString(4);
						if (!reader.IsDBNull(5)) row.SUPPLIER_NAME = reader.GetString(5);
						row.QUANTITY_SAL = reader.GetDecimal(6);
						row.QUANTITY_RET = reader.GetDecimal(7);
						row.TURNOVER = reader.GetDecimal(8);
						row.DISCOUNT = reader.GetDecimal(9);
						row.INCOME = reader.GetDecimal(10);
						row.QUANTITY = reader.GetDecimal(11);
						totalIncome += row.INCOME;
						totalTurnover += row.TURNOVER;
						profitable.AddProfitableRow(row);
					}

					reader.NextResult();
					reader.Read();
					StatistSale_Profitable_DS.FlagsRow flagsRow = flags.NewFlagsRow();
					flagsRow[0] = reader.GetString(0);
					flagsRow[1] = reader.GetString(1);
					flags.AddFlagsRow(flagsRow);
				}

				decimal level = (profitComboBox.SelectedIndex == 0 ? totalTurnover : totalIncome) * 0.01m * numericPercent.Value;
				decimal aggr = 0m;
				decimal totalTurnoverDivided = totalTurnover == 0m ? 0 : 100 / totalTurnover;
				decimal totalIncomeDivided = totalIncome == 0m ? 0 : 100 / totalIncome;
				bool set = false;
				for (int i = 0; i < profitable.Rows.Count; i++)
				{
					aggr += profitComboBox.SelectedIndex == 0 ? profitable[i].TURNOVER : profitable[i].INCOME;
					profitable[i].AGGR = aggr;					
					if (aggr >= level)
					{
						if (!set)
						{
							filter = aggr;
							set = true;
						}						
					}
					profitable[i].TURNOVER_P = profitable[i].TURNOVER * totalTurnoverDivided;
					profitable[i].INCOME_P = profitable[i].INCOME * totalIncomeDivided;
				}
			}

			string sort = string.Empty;

			switch (comboSort.SelectedIndex)
			{
				case 0:
					sort = "GOODS_NAME";
					break;
				case 1:
					sort = "TURNOVER";
					break;
				case 2:
					sort = "QUANTITY";
					break;
				case 3:
					sort = "INCOME";
					break;
				case 4:
					sort = "INCOME_P";
					break;
				case 5:
					sort = "TURNOVER_P";
					break;
				case 6:
					sort = "SUPPLIER_NAME";
					break;
			}

			string order = sortOrderComboBox.SelectedIndex == 0 ? "ASC" : "DESC";

			profitable.DefaultView.RowFilter = "AGGR <= " + filter.ToString().Replace(',', '.');
			profitable.DefaultView.Sort = sort + " " + order;
			
			DataTable final = profitable.DefaultView.ToTable(); // не работает без этого ToTable - непонято почему - в lost_profit получилось

			ReportDataSource ds1 = new ReportDataSource("StatistSale_Profitable_DS_Profitable", final);
			rep.ReportViewer.LocalReport.DataSources.Add(ds1);

            ReportDataSource ds2 = new ReportDataSource("StatistSale_Profitable_DS_Flags", flags.DefaultView.ToTable());
			rep.ReportViewer.LocalReport.DataSources.Add(ds2);

			rep.AddParameter("Pm_DateFrom", period.DateFrText);
			rep.AddParameter("Pm_DateTo", period.DateToText);
			rep.AddParameter("Pm_StoreName", stores.TextValues());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
			rep.AddParameter("Pm_TypeOutText", docs.ToString());
			rep.AddParameter("groups", chbGroupGoods.Checked ? "1" : "0");
			rep.AddParameter("total_row_count", profitable.Rows.Count.ToString());
			rep.AddParameter("percent", numericPercent.Value.ToString());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

			rep.ExecuteReport(this);

			//для выгрузки в csv
			DataSet ds = new DataSet();
			ds.Tables.Add(final);
			rep.DataSource = ds;
		}

		private void ClearValues()
		{
			period.DateTo = DateTime.Now;
			period.DateFrom = period.DateTo.AddDays(-13);

			sortOrderComboBox.SelectedIndex = 0;
			comboSort.SelectedIndex = 0;
			profitComboBox.SelectedIndex = 0;

			numericPercent.Value = 80;

			rbAllType.Checked = true;
			chbKKM.Checked = false;
			chbOut.Checked = false;
			chbMovement.Checked = false;

			chbGroupGoods.Checked = false;
			chbGoodCode.Checked = true;
			auCheckBox.Checked = false;

			stores.Items.Clear();
			goods.Items.Clear();
			ucTradeName.Items.Clear();
			selectGoodsGroup.Items.Clear();
		}

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "RCSStatistSaleProfitableGoods_11.xml");

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			comboSort.SelectedIndex = Utils.GetInt(root, SORT);
			sortOrderComboBox.SelectedIndex = Utils.GetInt(root, ORDER);
			rbAllType.Checked = Utils.GetBool(root, ALL);
			rbCheckType.Checked = Utils.GetBool(root, CHECK);
			chbKKM.Checked = Utils.GetBool(root, KKM);
			chbOut.Checked = Utils.GetBool(root, INVOICE_OUT);
			chbMovement.Checked = Utils.GetBool(root, MOVE);
			chbGroupGoods.Checked = Utils.GetBool(root, GG);
			chbGoodCode.Checked = Utils.GetBool(root, SHOW_CODE);
			auCheckBox.Checked = Utils.GetBool(root, AU);
			numericPercent.Value = Utils.GetDecimal(root, PERCENT);
			profitComboBox.SelectedIndex = Utils.GetInt(root, PROFIT);

			XmlNodeList groups = root.SelectNodes(GROUPS);
			foreach (XmlNode node in groups)
			{
				CatalogItem ci = new CatalogItem();
				ci.Id = Utils.GetLong(node, ID);
				ci.Name = Utils.GetString(node, TEXT);
				selectGoodsGroup.AddItem(ci);
			}

      chbKKM.Enabled = rbCheckType.Checked;
      chbOut.Enabled = rbCheckType.Checked;
      chbMovement.Enabled = rbCheckType.Checked;
		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root;

			if (File.Exists(settingsFilePath))
			{
				doc.Load(settingsFilePath);
				root = doc.SelectSingleNode("//XML");
				root.RemoveAll();
			}
			else
			{
				root = Utils.AddNode(doc, "XML");
			}

			Utils.AddNode(root, SORT, comboSort.SelectedIndex);
			Utils.AddNode(root, ORDER, sortOrderComboBox.SelectedIndex);
			Utils.AddNode(root, ALL, rbAllType.Checked);
			Utils.AddNode(root, CHECK, rbCheckType.Checked);
			Utils.AddNode(root, KKM, chbKKM.Checked);
			Utils.AddNode(root, INVOICE_OUT, chbOut.Checked);
			Utils.AddNode(root, MOVE, chbMovement.Checked);
			Utils.AddNode(root, GG, chbGroupGoods.Checked);
			Utils.AddNode(root, SHOW_CODE, chbGoodCode.Checked);
			Utils.AddNode(root, AU, auCheckBox.Checked);
			Utils.AddNode(root, PERCENT, numericPercent.Value);
			Utils.AddNode(root, PROFIT, profitComboBox.SelectedIndex);

			foreach (CatalogItem dr in selectGoodsGroup.Items)
			{
				XmlNode groups = Utils.AddNode(root, GROUPS);
				Utils.AddNode(groups, ID, dr.Id);
				Utils.AddNode(groups, TEXT, dr.Name);
			}

			doc.Save(settingsFilePath);
		}

		private const string SORT = "SORT";
		private const string ORDER = "ORDER";
		private const string ALL = "ALL";
		private const string CHECK = "CHECK";
		private const string KKM = "KKM";
		private const string INVOICE_OUT = "INVOICE_OUT";
		private const string MOVE = "MOVE";
		private const string GROUPS = "GROUPS";
		private const string ID = "ID";
		private const string TEXT = "TEXT";
		private const string GG = "GG";
		private const string SHOW_CODE = "SHOW_CODE";
		private const string AU = "AU";
		private const string PERCENT = "PERCENT";
		private const string PROFIT = "PROFIT";

		private void rbAllType_CheckedChanged(object sender, EventArgs e)
		{
			chbKKM.Enabled = rbCheckType.Checked;
			chbOut.Enabled = rbCheckType.Checked;
			chbMovement.Enabled = rbCheckType.Checked;
		}

		private void chbGroupGoods_CheckedChanged(object sender, EventArgs e)
		{
			chbGoodCode.Enabled = !chbGroupGoods.Checked;
		}

		private void FormParam_Load(object sender, EventArgs e)
		{
            ClearValues();
			LoadSettings();
		}

		private void FormParam_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
	}

}