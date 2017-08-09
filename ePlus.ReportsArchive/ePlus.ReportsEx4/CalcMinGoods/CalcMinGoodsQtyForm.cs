using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Core.MetaGe;
using ePlus.MetaData.Server;

namespace CalcMinGoods
{
	public partial class CalcMinGoodsQtyForm : Form
	{
		private enum EGoodsType { All = 0, Profit = 1, NonProfit = 2 }
		private enum ESortType { GoodsName = 0, CalcMinQty = 1, MinQty = 2 }
		private enum EExpenceType { All = 0, ChequeAndExpence = 1, ReturnToContractor = 2, Movement = 3 }

		private EGoodsType currentGoodsType = 0;
		private decimal profitPercent = 0;
		private ESortType currentSortType = 0;
		private EExpenceType currentExpenceType = 0;

		private CalcMinGoods parent = null;

		public CalcMinGoodsQtyForm(CalcMinGoods parent)
		{
			InitializeComponent();

			this.parent = parent;

			currentGoodsType = 0;
			profitPercent = 0;
			currentSortType = (ESortType) 1;
			currentExpenceType = 0;
			Init();
		}

		private void Init()
		{
			Object2Control();
			SetControlsState();
		}

		private void Object2Control()
		{
			cbGoodsType.SelectedIndexChanged -= new EventHandler(cbGoodsType_SelectedIndexChanged);
			cbGoodsType.SelectedIndex = (int) currentGoodsType;
			cbGoodsType.SelectedIndexChanged += new EventHandler(cbGoodsType_SelectedIndexChanged);
			cbSort.SelectedIndex = (int) currentSortType;
			chkDesc.Checked = true;
			cbRowCount.SelectedIndex = 0;
			
			nbDays.Value = 3;
			ucPeriod.SetPeriodMonth();
		}

		private void SetControlsState()
		{
			switch (cbGoodsType.SelectedIndex)
			{
				case (int) EGoodsType.Profit:
				case (int) EGoodsType.NonProfit:
					nbPercent.Enabled = true;
					if (cbGoodsType.SelectedIndex == (int) EGoodsType.Profit)
					{
						nbPercent.Value = 80;
					}
					else
					{
						nbPercent.Value = 10;
					}
					pluginGoods.Enabled = false;
					break;
				default:
					nbPercent.Enabled = false;
					nbPercent.Value = 0;
					pluginGoods.Enabled = true;
					break;
			}
		}

		private void cbGoodsType_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetControlsState();
		}

		private void pluginStore_BeforePluginShow(object sender, CancelEventArgs e)
		{
			string filter = string.Empty;
			foreach (DataRowItem dri in pluginContractor.Items)
			{
				if (string.IsNullOrEmpty(filter))
				{
					filter = dri.Id.ToString();
				}
				else
				{
					filter += ", " + dri.Id.ToString();
				}
			}
			if (!string.IsNullOrEmpty(filter))
			{
				pluginStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "ID_CONTRACTOR in (" + filter + ")");
			}
		}

		private List<long> contractorList = new List<long>();
		private List<long> storeList = new List<long>();

		public List<long> ContractorList
		{
			get { return contractorList; }
		}

		public List<long> StoreList
		{
			get { return storeList; }
		}

		private System.Collections.Generic.Dictionary<long, List<long>> contractorStoreDict = new System.Collections.Generic.Dictionary<long, List<long>>();

		private void pluginContractor_ValuesListChangedNew(object sender, ValuesListChangedEventArgs e)
		{
			switch (e.ChangeType)
			{
				case ValuesListChangeType.ItemAdded:
					DataRowItem dri = pluginContractor.Items[e.ItemIndex];
					contractorList.Add(dri.Id);
					contractorStoreDict.Add(dri.Id, new List<long>());
					break;
				case ValuesListChangeType.ItemsCleared:
					contractorList.Clear();
					contractorStoreDict.Clear();
					storeList.Clear();

					pluginStore.ValuesListChangedNew -= new ValuesListChangedEventHandler(pluginStore_ValuesListChangedNew);
					pluginStore.Items.Clear();
					pluginStore.ValuesListChangedNew += new ValuesListChangedEventHandler(pluginStore_ValuesListChangedNew);

					break;
				case ValuesListChangeType.ItemRemoved:
					long idContractor = contractorList[e.ItemIndex];
					contractorList.RemoveAt(e.ItemIndex);
					foreach (long id in contractorStoreDict[idContractor])
					{
						if (storeList.Contains(id))
						{
							storeList.Remove(id);

							pluginStore.ValuesListChangedNew -= new ValuesListChangedEventHandler(pluginStore_ValuesListChangedNew);
							List<DataRowItem> items = new List<DataRowItem>();
							foreach (DataRowItem i in pluginStore.Items)
							{
								if (i.Id == id)
								{
									items.Add(i);
								}
							}
							for (int i = 0; i < items.Count; i++)
							{
								pluginStore.Items.RemoveAt(items.IndexOf(items[i]));
							}
							items.Clear();
							pluginStore.ValuesListChangedNew += new ValuesListChangedEventHandler(pluginStore_ValuesListChangedNew);
						}
					}
					contractorStoreDict.Remove(idContractor);
					break;
			}
		}

		private void pluginStore_ValuesListChangedNew(object sender, ValuesListChangedEventArgs e)
		{
			switch (e.ChangeType)
			{
				case ValuesListChangeType.ItemAdded:
					DataRowItem dri = pluginStore.Items[e.ItemIndex];
					long idContractor = Utils.GetLong(pluginStore.PluginContol.Grid(0).SelectedRow(), "ID_CONTRACTOR");
					if (contractorStoreDict.ContainsKey(idContractor) && idContractor != 0)
					{
						contractorStoreDict[idContractor].Add(dri.Id);
					}
					else
					{
						contractorStoreDict.Add(idContractor, new List<long>(new long[] { dri.Id }));
						contractorList.Add(idContractor);

						pluginContractor.ValuesListChangedNew -= new ValuesListChangedEventHandler(pluginContractor_ValuesListChangedNew);
						string contractorName = Utils.GetString(pluginStore.PluginContol.Grid(0).SelectedRow(), "CONTRACTOR_NAME");
						pluginContractor.Items.Add(new DataRowItem(idContractor, Guid.Empty, string.Empty, contractorName));
						pluginContractor.ValuesListChangedNew += new ValuesListChangedEventHandler(pluginContractor_ValuesListChangedNew);
					}
					storeList.Add(dri.Id);
					break;
				case ValuesListChangeType.ItemsCleared:
					foreach (KeyValuePair<long, List<long>> kvp in contractorStoreDict)
					{
						kvp.Value.Clear();
					}
					storeList.Clear();
					break;
				case ValuesListChangeType.ItemRemoved:
					long idStore = storeList[e.ItemIndex];
					foreach (KeyValuePair<long, List<long>> kvp in contractorStoreDict)
					{
						kvp.Value.Remove(idStore);
					}
					storeList.RemoveAt(e.ItemIndex);
					break;
			}
		}

		private void bClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void bCalc_Click(object sender, EventArgs e)
		{
			XmlDocument xmlParams = Control2Object();
			DataService_BL bl = new DataService_BL();
			DataSet ds = new DataSet();
			using (SqlDataAdapter sqlda = new SqlDataAdapter())
			{
				SqlCommandEx comm = new SqlCommandEx("REPEX_CALCULATE_GOODS_MIN_QTY", new SqlConnection(bl.ConnectionString));
				comm.Parameters.Add(new SqlParameter("@XMLPARAMS", SqlDbType.NText)).Value = xmlParams.InnerXml;
				comm.CommandType = CommandType.StoredProcedure;
				sqlda.SelectCommand = comm.SqlCommand;
				sqlda.Fill(ds);
			}
			SqlLoader<GoodsMinQty> goodsLoader = new SqlLoader<GoodsMinQty>();
			List<GoodsMinQty> gL = goodsLoader.GetList(ds.Tables[0]);
			using (GoodsQtyMinUpdateForm form = new GoodsQtyMinUpdateForm(this.parent))
			{
				form.Init(gL, contractorList, storeList);
				form.ShowDialog(this);
			}
		}

		private XmlDocument Control2Object()
		{
			XmlDocument xmlParams = new XmlDocument();
			XmlNode root = Utils.AddNode(xmlParams, "XML");
			XmlNode paramsNode = Utils.AddNode(root, "PARAMS");			
			currentGoodsType = (EGoodsType) cbGoodsType.SelectedIndex;
			currentSortType = (ESortType) cbSort.SelectedIndex;
			switch (currentGoodsType)
			{
				case EGoodsType.All:
					profitPercent = 0;
					break;
				default:
					profitPercent = nbPercent.Value;
					goodsList.Clear();
					break;
			}
			Utils.AddNode(paramsNode, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(paramsNode, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(paramsNode, "GOODS_TYPE", (int) currentGoodsType);
			Utils.AddNode(paramsNode, "PROFIT_PERCENT", profitPercent);
			Utils.AddNode(paramsNode, "SORT_BY", (int) currentSortType + 1);
			Utils.AddNode(paramsNode, "SORT_DESC", chkDesc.Checked);
			Utils.AddNode(paramsNode, "DAYS", nbDays.Value);
			XmlNode expenceTypes = Utils.AddNode(paramsNode, "OPS");
			XmlNode op = Utils.AddNode(expenceTypes, "OP");

			if (rbAllType.Checked)
			{
				Utils.AddNode(op, "CODE_OP", "CHEQUE");
				op = Utils.AddNode(expenceTypes, "OP");
				Utils.AddNode(op, "CODE_OP", "INVOICE_OUT");
				op = Utils.AddNode(expenceTypes, "OP");
				Utils.AddNode(op, "CODE_OP", "ACT_R2C");
				op = Utils.AddNode(expenceTypes, "OP");
				Utils.AddNode(op, "CODE_OP", "MOVE");
			}
			else
			{
				if (chbKKM.Checked)
				{
					Utils.AddNode(op, "CODE_OP", "CHEQUE");
					op = Utils.AddNode(expenceTypes, "OP");
					Utils.AddNode(op, "CODE_OP", "INVOICE_OUT");
				}
				if (chbOut.Checked)
				{
					Utils.AddNode(op, "CODE_OP", "ACT_R2C");
				}
				if (chbMovement.Checked)
				{
					Utils.AddNode(op, "CODE_OP", "MOVE");
				}
			}

			long rowCount;
			bool res = long.TryParse(cbRowCount.SelectedItem.ToString(), out rowCount);
			if (!res)
			{
				rowCount = 0;
			}
			Utils.AddNode(paramsNode, "ROWSCOUNT", rowCount);
			XmlNode contractorsNode = Utils.AddNode(paramsNode, "CONTRACTORS");
			foreach (long id in contractorList)
			{
				XmlNode contractorNode = Utils.AddNode(contractorsNode, "CONTRACTOR");
				Utils.AddNode(contractorNode, "ID_CONTRACTOR", id);
			}

			XmlNode storesNode = Utils.AddNode(paramsNode, "STORES");
			foreach (long id in storeList)
			{
				XmlNode storeNode = Utils.AddNode(storesNode, "STORE");
				Utils.AddNode(storeNode, "ID_STORE", id);
			}
			XmlNode goodsNode = Utils.AddNode(paramsNode, "GOODS_LIST");
			foreach (long id in goodsList)
			{
				XmlNode goods = Utils.AddNode(goodsNode, "GOODS");
				Utils.AddNode(goods, "ID_GOODS", id);
			}

			return xmlParams;
		}


		private List<long> goodsList = new List<long>();
		private void pluginGoods_ValuesListChangedNew(object sender, ValuesListChangedEventArgs e)
		{
			switch (e.ChangeType)
			{
				case ValuesListChangeType.ItemAdded:
					goodsList.Add(pluginGoods.Items[e.ItemIndex].Id);
					break;
				case ValuesListChangeType.ItemRemoved:
					goodsList.RemoveAt(e.ItemIndex);
					break;
				case ValuesListChangeType.ItemsCleared:
					goodsList.Clear();
					break;
			}
		}

		private void rbAllType_CheckedChanged(object sender, EventArgs e)
		{
			chbKKM.Enabled = false;
			chbOut.Enabled = false;
			chbMovement.Enabled = false;
		}

		private void rbCheckType_CheckedChanged(object sender, EventArgs e)
		{
			chbKKM.Enabled = true;
			chbOut.Enabled = true;
			chbMovement.Enabled = true;
		}
	}

	public class GoodsMinQty : ITableEditorItem
	{
		long id_goods;
		string goods_name;
		decimal qty_min;
		decimal qty_min_calc;

		public long ID_GOODS
		{
			get { return id_goods; }
			set { id_goods = value; }
		}

		public string GOODS_NAME
		{
			get { return goods_name; }
			set { goods_name = value; }
		}

		public decimal QTY_MIN
		{
			get { return qty_min; }
			set { qty_min = value; }
		}

		public decimal QTY_MIN_CALC
		{
			get { return qty_min_calc; }
			set { qty_min_calc = value; }
		}

		public void ToXml(XmlNode node)
		{
			Utils.AddNode(node, "ID_GOODS", id_goods);
			Utils.AddNode(node, "QTY_MIN", qty_min);
		}
	}
}