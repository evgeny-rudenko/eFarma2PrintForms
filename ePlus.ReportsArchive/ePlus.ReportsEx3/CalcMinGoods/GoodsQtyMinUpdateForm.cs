using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Core.Enums;
using ePlus.MetaData.Server;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace CalcMinGoods
{
	public partial class GoodsQtyMinUpdateForm : Form
	{
		private CalcMinGoods parent = null;

		public GoodsQtyMinUpdateForm(CalcMinGoods parent)
		{
			InitializeComponent();
			this.parent = parent;
		}

		private List<GoodsMinQty> list;
		List<long> contractorList = new List<long>();
		List<long> storeList = new List<long>();

		public void Init(List<GoodsMinQty> list, List<long> contractorList, List<long> storeList)
		{
			this.list = list;
			this.contractorList = contractorList;
			this.storeList = storeList;
			Object2Control();
			SetControlsState();
		}

		private void Object2Control()
		{
			gridGoods.DataSource = list;
		}

		private void SetControlsState()
		{
			bool isEnabled = list.Count > 0;
			bMove.Enabled = bSave.Enabled = bOk.Enabled = isEnabled;
			bNewRequest.Enabled = isEnabled;
		}

		private void bClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void bMove_Click(object sender, EventArgs e)
		{
			foreach (GoodsMinQty item in list)
			{
				item.QTY_MIN = item.QTY_MIN_CALC;
			}
			gridGoods.BindingSource.CurrencyManager.Refresh();
		}

		private void bSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private bool Save()
		{
			if (!Validateobject()) return false;
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			XmlNode goodsNode = Utils.AddNode(root, "GOODS_LIST");
			foreach (GoodsMinQty goods in list)
			{
				XmlNode node = Utils.AddNode(goodsNode, "GOODS");
				goods.ToXml(node);
			}
			DataService_BL bl = new DataService_BL();
			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter())
			{
				SqlCommandEx comm = new SqlCommandEx("REPEX_UPDATE_GOOGS_QTY_MIN", new SqlConnection(bl.ConnectionString));
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.NText)).Value = doc.InnerXml;
				sqlda.SelectCommand = comm.SqlCommand;
				sqlda.Fill(ds);
			}
			return true;
		}

		private bool Validateobject()
		{
			foreach (GoodsMinQty item in list)
			{
				if (item.QTY_MIN < 0)
				{
					ShowError("Минимальное количество товара должно быть положительным или 0");
					gridGoods.BindingSource.Position = list.IndexOf(item);
					return false;
				}
			}
			return true;
		}

		private void ShowError(string s)
		{
			MessageBox.Show(s, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void bOk_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				this.DialogResult = DialogResult.OK;
			}
		}

		private void bNewRequest_Click(object sender, EventArgs e)
		{
			PluginFormView pfv = AppManager.GetPluginView("REQUEST");
			List<MetaActionPluginGrid> actions = pfv.Grid(0).MetaGrid.MetaActionGrid;
			MetaActionPluginGrid act = null;
			foreach (MetaActionPluginGrid action in actions)
			{
				if (action.Mnemocode == "AddNewRequestFromQtyMinAction")
				{
					act = action;
					break;
				}
			}
			if (act != null)
			{
				DataService_BL bl = new DataService_BL();
				XmlDocument doc = new XmlDocument();
				XmlNode root = Utils.AddNode(doc, "XML");
				foreach (GoodsMinQty gmq in list)
				{
					XmlNode goods = Utils.AddNode(root, "GOODS");
					Utils.AddNode(goods, "ID_GOODS", gmq.ID_GOODS);
					Utils.AddNode(goods, "MIN_QTY", gmq.QTY_MIN_CALC);
				}

				foreach (long c in contractorList)
				{
					Utils.AddNode(root, "ID_CONTRACTOR", c);
				}

				foreach (long s in storeList)
				{
					Utils.AddNode(root, "ID_STORE", s);
				}

				using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
				{
					SqlCommandEx comm = new SqlCommandEx("REPEX_GET_REMAINS_LESS_MIN_QTY", conn);
					comm.AddParameterIn("@XMLDATA", SqlDbType.NText, doc.InnerXml);
					comm.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
					DataSet ds = new DataSet();
					sqlda.Fill(ds);
					SqlLoader<GoodsMinQty> loader = new SqlLoader<GoodsMinQty>();
					MinQtyList.MinQty.Clear();
					MinQtyList.MinQty.AddRange(loader.GetList(ds.Tables[0]));
				}
				((GridController) pfv.Grid(0)).ExecuteAction(act);
			}
			return;
		}

		private void reportButton_Click(object sender, EventArgs e)
		{
			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = parent.ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(parent.folderPath, CalcMinGoods.CACHE_FOLDER), "CalcMinGoods.rdlc");

			DataSet ds = new DataSet();
			ds.Tables.Add("Table1");
			ds.Tables[0].Columns.Add("GOODS_NAME");
			ds.Tables[0].Columns.Add("QTY_MIN", typeof(decimal));
			ds.Tables[0].Columns.Add("QTY_MIN_CALC", typeof(decimal));

			rep.DataSource = ds;

			foreach (GoodsMinQty item in list)
			{
				DataRow row = ds.Tables[0].NewRow();
				row["GOODS_NAME"] = item.GOODS_NAME;
				row["QTY_MIN"] = item.QTY_MIN;
				row["QTY_MIN_CALC"] = item.QTY_MIN_CALC;

				ds.Tables[0].Rows.Add(row);
			}

			rep.BindDataSource("CalcMinGoods_DS_Table1", 0);
			rep.Show();
		}

	}

	public class MinQtyList
	{
		private static List<GoodsMinQty> minQty = new List<GoodsMinQty>();
		public static List<GoodsMinQty> MinQty
		{
			get { return minQty; }
		}
	}
}