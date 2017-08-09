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

namespace StatistSaleLostProfitEx
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
			ClearValues();
        }

        public string ReportName
        {
            get { return "Анализ продаж (упущенная выгода)"; }
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

			long nCountDays = (Int64) period.DateTo.ToOADate() - (Int64) period.DateFrom.ToOADate() + 1;

			string sort = string.Empty;

			switch (comboSort.SelectedIndex)
			{
				case 0:
					sort = "GOODS_NAME";
					break;
				case 1:
					sort = "LOST_PROFIT";
					break;
				case 2:
					sort = "ZERO_DAYS";
					break;
				case 3:
					sort = "AVG_SPEED";
					break;
				case 4:
					sort = "SUMM";
					break;
			}

			string order = sortOrderComboBox.SelectedIndex == 0 ? "ASC" : "DESC";

			STATIST_SALE_LOST_DS.LostProfitDataTable lostProfit = new STATIST_SALE_LOST_DS.LostProfitDataTable();
			STATIST_SALE_LOST_DS.FlagsDataTable flags = new STATIST_SALE_LOST_DS.FlagsDataTable();

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlParameter param = new SqlParameter("@XMLPARAM", SqlDbType.NVarChar);
				param.Value = doc.InnerXml;

				SqlCommand command = new SqlCommand(@"DBO.STATIST_SALE_LOST_PROFIT", con);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(param);

				command.CommandTimeout = 60 * 60;
				con.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						STATIST_SALE_LOST_DS.LostProfitRow row = lostProfit.NewLostProfitRow();
						row.ID_GOODS = reader.GetInt64(0);
						if (!reader.IsDBNull(1))
							row.GOODS_CODE = reader.GetString(1);
						row.GOODS_NAME = reader.GetString(2);
						row.QUANTITY = reader.GetDecimal(3);
						row.LAST_PRICE = reader.GetDecimal(4);
						row.AU_G = reader.GetString(5);
						row.AU_G_AP = reader.GetString(6);
						lostProfit.AddLostProfitRow(row);
					}
					reader.NextResult();
					
					int goodsCount = lostProfit.Rows.Count;

					if (goodsCount > 0)
					{

						int i = 0;
						int j = 0;
						decimal rem_outer = lostProfit[i].QUANTITY;

						int zero_days = 0;
						int sale_days = 0;

						decimal summ = 0m;
						decimal quantity_sold = 0m;

						while (reader.Read())
						{
							decimal rem = reader.GetDecimal(0);

							rem_outer += rem;
							if (rem_outer == 0)
								zero_days++;

							decimal q1 = reader.GetDecimal(1);
							if (q1 > 0)
							{
								sale_days++;
								quantity_sold += q1;
								summ += reader.GetDecimal(2);
							}

							j++;
							if (j == nCountDays)
							{
								if (zero_days > 0 && zero_days != nCountDays)
								{
									lostProfit[i].ZERO_DAYS = zero_days;
									lostProfit[i].QUANTITY = quantity_sold;
									lostProfit[i].SALE_DAYS = sale_days;
									lostProfit[i].SUMM = summ;
									lostProfit[i].AVG_SPEED = quantity_sold / (nCountDays - zero_days);
									lostProfit[i].LOST_PROFIT = lostProfit[i].AVG_SPEED * zero_days * lostProfit[i].LAST_PRICE;
									lostProfit[i].SURROGATE = 0;
								}
								else
								{
									lostProfit[i].SURROGATE = 1;
								}

								j = 0;
								i++;

								if (i < goodsCount)
									rem_outer = lostProfit[i].QUANTITY;

								zero_days = 0;
								sale_days = 0;
								quantity_sold = 0m;
								summ = 0m;
							}
						}

						reader.NextResult();
						reader.Read();
						STATIST_SALE_LOST_DS.FlagsRow flagsRow = flags.NewFlagsRow();
						flagsRow[0] = reader.GetString(0);
						flagsRow[1] = reader.GetString(1);
						flags.AddFlagsRow(flagsRow);
					}
				}
			}

			STATIST_SALE_LOST_DS.LostProfitDataTable lostGrouped = null;
			if (chbGroupGoods.Checked && lostProfit.Count > 0)
			{
				lostGrouped = new STATIST_SALE_LOST_DS.LostProfitDataTable();				
				DataRow[] rows = lostProfit.Select("SURROGATE <> 1", "GOODS_NAME");
				
				DataRow last = null;
				DataRow dest = null;
				DataRow cur = null;
				bool sameRow = false;

				for (int i = 0; i < rows.Length; i++)
				{
					cur = rows[i];

					sameRow = false;
					if (last != null)
					{
						sameRow = true;
						if (!((string) last[1]).Equals((string) cur[1]))
						{
							sameRow = false;
						}
						if (!sameRow)
							lostGrouped.Rows.Add(dest);
					}
					if (!sameRow)
					{
						dest = lostGrouped.NewRow();
					}

					dest[1] = cur[1];
					for (int j = 5; j < 12; j++)
						dest[j] = SumHelper(dest[j], cur[j]);

					last = cur;
				}
				if (dest != null)
					lostGrouped.Rows.Add(dest);

				lostGrouped.DefaultView.Sort = sort + " " + order;
			}

			lostProfit.DefaultView.RowFilter = "SURROGATE <> 1";
			lostProfit.DefaultView.Sort = sort + " " + order;

			ReportDataSource ds1 = new ReportDataSource("STATIST_SALE_LOST_DS_LostProfit",
				chbGroupGoods.Checked ? lostGrouped.DefaultView : lostProfit.DefaultView);
			rep.ReportViewer.LocalReport.DataSources.Add(ds1);
			ReportDataSource ds2 = new ReportDataSource("STATIST_SALE_LOST_DS_Flags", flags.DefaultView.ToTable());
			rep.ReportViewer.LocalReport.DataSources.Add(ds2);

            rep.AddParameter("Pm_StoreName", stores.TextValues());
            rep.AddParameter("Pm_DateFrom", period.DateFrText);
            rep.AddParameter("Pm_DateTo", period.DateToText);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
			rep.AddParameter("groups", chbGroupGoods.Checked ? "1" : "0");

            rep.ExecuteReport(this);
			//для выгрузки в csv
			DataSet ds = new DataSet();
			lostProfit.AcceptChanges();
			ds.Tables.Add(lostProfit);
			for (int i = 0; i < lostProfit.Rows.Count; i++)
			{
				if (lostProfit[i].SURROGATE == 1)
					lostProfit.Rows[i].Delete();
			}
			lostProfit.AcceptChanges();
			rep.DataSource = ds;
        }

		private object SumHelper(object a, object b)
		{
			if (a is DBNull)
				return b;
			if (b is DBNull)
				return a;
			return (Convert.ToDecimal(a) + Convert.ToDecimal(b));
		}

        private void rbAllType_CheckedChanged(object sender, EventArgs e)
        {
			chbKKM.Enabled = rbCheckType.Checked;
			chbOut.Enabled = rbCheckType.Checked;
			chbMovement.Enabled = rbCheckType.Checked;
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
			LoadSettings();
        }

        private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
        {
			SaveSettings();
        }

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void ClearValues()
		{			
			period.DateTo = DateTime.Now;
			period.DateFrom = period.DateTo.AddDays(-13);

			sortOrderComboBox.SelectedIndex = 0;
			comboSort.SelectedIndex = 0;

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

		private string settingsFilePath = Path.Combine(Utils.TempDir(), "RCSStatistSaleLostProfit_9.xml");

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

			XmlNodeList groups = root.SelectNodes(GROUPS);
			foreach (XmlNode node in groups)
			{
				CatalogItem ci = new CatalogItem();
				ci.Id = Utils.GetLong(node, ID);
				ci.Name = Utils.GetString(node, TEXT);
				selectGoodsGroup.AddItem(ci);
			}
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

		private void chbGroupGoods_CheckedChanged(object sender, EventArgs e)
		{
			chbGoodCode.Enabled = !chbGroupGoods.Checked;
		}
    }
}