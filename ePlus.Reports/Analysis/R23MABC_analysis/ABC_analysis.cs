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

namespace R23MABC_analysis
{
	public partial class R23MABC_analysisParams : ExternalReportForm, IExternalReportFormMethods
	{
        public R23MABC_analysisParams()
		{
			InitializeComponent();

            ucGoodsGroup.AllowSaveState = true;
            ucGoodsKind.AllowSaveState = true;
            ucGoods.AllowSaveState = true;
            ucStore.AllowSaveState = true;
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "ABC_analysis.rdlc");

            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

            Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
            Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

            rep.AddParameter("DATE_FROM", ucPeriod.DateFrom.ToString("dd.MM.yyyy"));
            rep.AddParameter("DATE_TO", ucPeriod.DateTo.ToString("dd.MM.yyyy"));
            //-----
            Utils.AddNode(root, "SELECTED_GOODS_KIND", rbGoodsKindChecked.Checked ? "1" : "0");
            foreach (DataRowItem goods_kind in ucGoodsKind.Items)
                Utils.AddNode(root, "ID_GOODS_KIND", goods_kind.Id);

            rep.AddParameter("SELECTED_GOODS_KIND", (rbGoodsKindChecked.Checked ? "1" : "0"));
            rep.AddParameter("GOODS_KIND_LIST", ucGoodsKind.TextValues());
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

            rep.AddParameter("ABC_PARAM", "Способ расчета: " + cbTypeCalc.Text + "  (" + numGrpA.Value.ToString() + " / " + numGrpB.Value.ToString() + " / " + numGrpC.Value.ToString() + ")");
            rep.AddParameter("TYPE_CALC", cbTypeCalc.SelectedIndex.ToString());
            //-----
            Utils.AddNode(root, "ANALOG_UNITE", chkbAnalogUnite.Checked ? "1" : "0");

            Utils.AddNode(root, "TYPE_CALC", cbTypeCalc.SelectedIndex.ToString());
            Utils.AddNode(root, "GROUP_A", numGrpA.Value.ToString());
            Utils.AddNode(root, "GROUP_B", numGrpB.Value.ToString());
            Utils.AddNode(root, "GROUP_C", numGrpC.Value.ToString());

            rep.LoadData("REPEX_2UH_ABC_ANALYSIS", doc.InnerXml);

            rep.BindDataSource("ABC_analysis_DS_Table1", 0);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
            get { return "ABC-анализ (НЗ)"; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}

		private void ClearValues()
		{
            ucPeriod.DateTo = DateTime.Now;
            ucPeriod.DateFrom = DateTime.Now.AddDays(-30);

            chkbAnalogUnite.Checked = false;

            chklbTypeOut.SetItemChecked(0, true);

            ucGoodsGroup.Clear();
            ucGoodsKind.Clear();
            ucStore.Clear();
            ucGoods.Clear();

            rbGoodsGroupChecked.Checked = true;
            rbGoodsKindChecked.Checked = true;
            rbStoreChecked.Checked = true;
            rbGoodsChecked.Checked = true;

            cbTypeCalc.SelectedIndex = 0;
            numGrpA.Value = 75;
            numGrpB.Value = 20;
            numGrpC.Value = 5;
        }

        private void R23MABC_analysisParams_Load(object sender, EventArgs e)
        {
            ClearValues();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;

            tabControl1.SelectedIndex = 0;
            lstbTabControl.SelectedIndex = 0;
        }

        private void btnDescription_Click(object sender, EventArgs e)
        {
            string msg = "";
            msg += "ABC-анализ (НЗ) - это аналитический отчет, разработанный для АС \"На здоровье\".\n";
            msg += "ABC-анализ позволяет классифицировать ресурсы по степени их важности, основываясь на принципе Парето.\n";
            msg += "Критерии фильтрации \"Выбранные\" и \"Кроме выбранных\" для товаров и групп товаров связаны: критерий \"Кроме выбранных\" имеет более высокий приоритет.\n";
            msg += "Варианты фильтрации:\n";
            msg += " * Товары - \"Выбранные\", Группы товаров - \"Выбранные\", то в отчет попадут выбранные в фильтре товары, а также все товары выбранных групп.\n";
            msg += " * Товары - \"Выбранные\", Группы товаров - \"Кроме выбранных\", то в отчет попадут выбранные в фильтре товары, кроме тех, которые попадают в выбранные группы товаров.\n";
            msg += " * Товары - \"Кроме выбранных\", Группы товаров - \"Выбранные\", то в отчет попадут товары выбранных групп, кроме тех товаров, которые указаны в фильтре товаров.\n";
            msg += " * Товары - \"Кроме выбранных\", Группы товаров - \"Кроме выбранных\", то в отчет попадут только те товары, которые не входят ни в список исключаемых товаров, ни входят в исключаемую группу.\n";
            msg += "Если в фильтрах не выбраны позиции, то фильтрация не происходит, независимо от выбранного критерия (\"Выбранные\" или \"Кроме выбранных\").\n";
            msg += "Все количества пересчитаны в целые упаковки, поэтому в этом поле возможно появление дробных значений в поле \"Продано упаковок\".";
            MessageBox.Show(msg);
        }

        private void numGrpA_ValueChanged(object sender, EventArgs e)
        {
            if (numGrpA.Value > 100) numGrpA.Value = 100;

            if (100 - numGrpA.Value - numGrpB.Value < 0)
            {
                numGrpC.Value = 0;
                numGrpB.Value = 100 - numGrpA.Value;
            }
            else
                numGrpC.Value = 100 - numGrpA.Value - numGrpB.Value;
        }

        private void numGrpB_ValueChanged(object sender, EventArgs e)
        {
            if (numGrpB.Value > 100) numGrpB.Value = 100;

            if (100 - numGrpA.Value - numGrpB.Value < 0)
            {
                numGrpC.Value = 0;
                numGrpA.Value = 100 - numGrpB.Value;
            }
            else
                numGrpC.Value = 100 - numGrpA.Value - numGrpB.Value;
        }

        private void lstbTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbTabControl.SelectedIndex < 0) return;
            tabControl1.SelectedIndex = lstbTabControl.SelectedIndex;
        }
	}



}