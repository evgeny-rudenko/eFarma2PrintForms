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
using System.IO;

namespace F12AMarketMoveGoodsBySupplierNoGroups
{
	public partial class F12AMarketMoveGoodsBySupplierParams : ExternalReportForm, IExternalReportFormMethods
	{
        public F12AMarketMoveGoodsBySupplierParams()
		{
			InitializeComponent();

            ucGoodsGroup.AllowSaveState = true;
            ucGoods.AllowSaveState = true;
            ucStore.AllowSaveState = true;
            ucSupplier.AllowSaveState = true;
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MarketMoveGoodsBySupplierNoGroups.rdlc");

            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            rep.AddParameter("DATE_FROM", ucPeriod.DateFrom.ToString("dd.MM.yyyy"));
            rep.AddParameter("DATE_TO", ucPeriod.DateTo.ToString("dd.MM.yyyy"));
            //-----
            Utils.AddNode(root, "SELECTED_GOODS_GROUP", rbGoodsGroupChecked.Checked ? "1" : "0");
            foreach (DataRowItem goods_group in ucGoodsGroup.Items)
                Utils.AddNode(root, "ID_GOODS_GROUP", goods_group.Id);

            rep.AddParameter("SELECTED_GOODS_GROUP", (rbGoodsGroupChecked.Checked ? "1" : "0"));
            rep.AddParameter("GOODS_GROUP_LIST", ucGoodsGroup.TextValues());
            //-----
            Utils.AddNode(root, "SELECTED_GOODS", rbGoodsChecked.Checked ? "1" : "0");
            foreach (DataRowItem goods in ucGoods.Items)
                Utils.AddNode(root, "ID_GOODS", goods.Id);

            rep.AddParameter("SELECTED_GOODS", (rbGoodsChecked.Checked ? "1" : "0"));
            rep.AddParameter("GOODS_LIST", ucGoods.TextValues());
            //-----
            Utils.AddNode(root, "SELECTED_STORES", rbStoreChecked.Checked ? "1" : "0");
            foreach (DataRowItem store in ucStore.Items)
                Utils.AddNode(root, "ID_STORE", store.Id);

            rep.AddParameter("SELECTED_STORES", (rbStoreChecked.Checked ? "1" : "0"));
            rep.AddParameter("STORES_LIST", ucStore.TextValues());
            //-----
            Utils.AddNode(root, "SELECTED_SUPPLIERS", rbSupplierChecked.Checked ? "1" : "0");
            foreach (DataRowItem supplier in ucSupplier.Items)
                Utils.AddNode(root, "ID_SUPPLIER", supplier.Id);

            rep.AddParameter("SELECTED_SUPPLIERS", (rbSupplierChecked.Checked ? "1" : "0"));
            rep.AddParameter("SUPPLIERS_LIST", ucSupplier.TextValues());
            rep.AddParameter("SHOW_GROUPSUM", (chkbShowGroupSum.Checked ? "1" : "0"));
   
            //-----

            string type_in_list = "";
            for (int i = 0; i < chklbTypeIn.Items.Count; i++)
            {
                switch (chklbTypeIn.Items[i].ToString())
                {
                    case "Приходная накладная": Utils.AddNode(root, "IN1_INVOICE", chklbTypeIn.GetItemChecked(i) ? "1" : "0"); break;
                    case "Перемещение из ЦО": Utils.AddNode(root, "IN2_MOVE_CO", chklbTypeIn.GetItemChecked(i) ? "1" : "0"); break;
                    case "Внутреннее перемещение": Utils.AddNode(root, "IN3_MOVE_INSIDE", chklbTypeIn.GetItemChecked(i) ? "1" : "0"); break;
                    case "Перемещение между подразделениями": Utils.AddNode(root, "IN4_MOVE_OUTSIDE", chklbTypeIn.GetItemChecked(i) ? "1" : "0"); break;
                }
                if (chklbTypeIn.GetItemChecked(i))
                    type_in_list += chklbTypeIn.Items[i].ToString() + ", ";
            }
            if (type_in_list == "")
                type_in_list = "(без прихода)  ";
            rep.AddParameter("TYPE_IN_LIST", type_in_list.Substring(0, type_in_list.Length - 2));

            //-----

            string type_out_list = "";
            for (int i = 0; i < chklbTypeOut.Items.Count; i++)
            {
                switch (chklbTypeOut.Items[i].ToString())
                {
                    case "Чек": Utils.AddNode(root, "OUT1_CHEQUE", chklbTypeOut.GetItemChecked(i) ? "1" : "0"); break;
                    case "Перемещение в ЦО": Utils.AddNode(root, "OUT2_MOVE_CO", chklbTypeOut.GetItemChecked(i) ? "1" : "0"); break;
                    case "Внутреннее перемещение": Utils.AddNode(root, "OUT3_MOVE_INSIDE", chklbTypeOut.GetItemChecked(i) ? "1" : "0"); break;
                    case "Перемещение между подразделениями": Utils.AddNode(root, "OUT4_MOVE_OUTSIDE", chklbTypeOut.GetItemChecked(i) ? "1" : "0"); break;
                    case "Расходная накладная": Utils.AddNode(root, "OUT5_INVOICE_OUT", chklbTypeOut.GetItemChecked(i) ? "1" : "0"); break;
                }
                if (chklbTypeOut.GetItemChecked(i))
                    type_out_list += chklbTypeOut.Items[i].ToString() + ", ";
            }
            if (type_out_list == "")
                type_out_list = "(без расхода)  ";
            rep.AddParameter("TYPE_OUT_LIST", type_out_list.Substring(0, type_out_list.Length - 2));

            //-----
            
            Utils.AddNode(root, "SHOW_PROD", chkbShowProd.Checked ? "1" : "0");
            Utils.AddNode(root, "SHOW_EMPTY_GROUP", chkbShowEmptyGroup.Checked ? "1" : "0");

            rep.LoadData("REPEX_F12A_MARKET_MOVE_GOODS_BY_SUPPLIER_NO_GROUPS", doc.InnerXml);

            rep.BindDataSource("MarketMoveGoodsBySupplier_DS_Table1", 0);
            rep.BindDataSource("MarketMoveGoodsBySupplier_DS_Table2", 1);

			rep.ExecuteReport(this);
		}


		public string ReportName
		{
            get { return "Движение товаров по поставщикам (Самара)"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void ClearValues()
		{
            ucPeriod.DateTo = DateTime.Now;
            ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

            chkbShowProd.Checked = false;
            chkbShowEmptyGroup.Checked = false;

            for (int i = 0; i < chklbTypeIn.Items.Count; i++)
                chklbTypeIn.SetItemChecked(i, true);

            for (int i = 0; i < chklbTypeOut.Items.Count; i++)
                chklbTypeOut.SetItemChecked(i, true);

            ucGoodsGroup.Clear();
            ucStore.Clear();
            ucGoods.Clear();
            ucSupplier.Clear();

            rbGoodsGroupChecked.Checked = true;
            rbStoreChecked.Checked = true;
            rbGoodsChecked.Checked = true;
            rbSupplierChecked.Checked = true;
        }

        private void F12AMarketMoveGoodsParams_Load(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void btnDescription_Click(object sender, EventArgs e)
        {
            string msg = "";
            msg += "Движение товаров по поставщикам (Самара) - это маркетинговый отчет, модификация отчета R23MMarketMoveGoodsBySupplier.\n";
            msg += "Данный отчет НЕ будет сходиться с товарными отчетами ввиду своей специфики учета определенных типов прихода и расхода.\n";
            msg += "Данный отчет анализирует ТОЛЬКО документы \"Приходная накладная\", \"Перемещение\", \"Перемещение между подразделениями\", а также \"Чек\" и \"Расходная накладная\", в случае, если выбраны данные опции.\n";
            msg += "Критерии фильтрации \"Выбранные\" и \"Кроме выбранных\" для товаров и групп товаров связаны: критерий \"Кроме выбранных\" имеет более высокий приоритет.\n";
            msg += "Варианты фильтрации:\n";
            msg += " * Товары - \"Выбранные\", Группы товаров - \"Выбранные\", то в отчет попадут выбранные в фильтре товары, а также все товары выбранных групп.\n";
            msg += " * Товары - \"Выбранные\", Группы товаров - \"Кроме выбранных\", то в отчет попадут выбранные в фильтре товары, кроме тех, которые попадают в выбранные группы товаров.\n";
            msg += " * Товары - \"Кроме выбранных\", Группы товаров - \"Выбранные\", то в отчет попадут товары выбранных групп, кроме тех товаров, которые указаны в фильтре товаров.\n";
            msg += " * Товары - \"Кроме выбранных\", Группы товаров - \"Кроме выбранных\", то в отчет попадут только те товары, которые не входят ни в список исключаемых товаров, ни входят в исключаемую группу.\n";
            msg += "Если в фильтрах не выбраны позиции, то фильтрация не происходит, независимо от выбранного критерия (\"Выбранные\" или \"Кроме выбранных\").\n";
            msg += "Доступна также функция скрытия товаров, которые не входят ни в одну из групп.\n";
            msg += "Обратите внимание на то, что отображение товаров вне групп происходит независимо от заданных значений в фильтрах.\n";
            msg += "Все количества пересчитаны в целые упаковки, поэтому в этом поле возможно появление дробных значений.";
            msg += "\n\n";
            msg += "Изменения:\n";
            msg += "2014-07-17 Самара - добавлен флаг 'Отображать подытоги', отключена группировка по группам товаров.";
       

            MessageBox.Show(msg);
        }

        private void chkbShowGroupSum_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}