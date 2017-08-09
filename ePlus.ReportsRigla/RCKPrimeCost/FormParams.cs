using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using Aspose.Cells;

namespace RCKPrimeCost
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public byte m_startCol = 2;
		public int m_startRow = 6;
		public FormParams()
		{
			InitializeComponent();
			if (period != null)
			{
				period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				period.DateFrom = period.DateTo.AddDays(-13);
			};
		}
		private static DataTable GetData(string sqlCommand)
		{
			DataService_BL v_dataService = new DataService_BL();
			SqlConnection v_cn = new SqlConnection(v_dataService.ConnectionString);
			SqlCommand command = new SqlCommand(sqlCommand, v_cn);
			command.CommandTimeout = 600;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = command;

			DataTable table = new DataTable();
			table.Locale = System.Globalization.CultureInfo.InvariantCulture;
			v_cn.Open();
			adapter.Fill(table);
			v_cn.Close();
			return table;
		}

		private string GetDMLTakings(DateTime vBegin, DateTime vEnd, bool wHead)
		{
			string result = "";
			string AliasSelect = "A";
			string QUERYLIST = "";
			string QUERYSOURCE = "";
			int i = 0;
			//DateTime vBegin = DateTime.Parse(period.DateFrom.ToShortDateString());
			//DateTime vEnd = DateTime.Parse(period.DateTo.ToShortDateString());
			while (vBegin <= vEnd) 
			{
				i++;
				AliasSelect = "A" + i.ToString();
				QUERYLIST = QUERYLIST + ", " + AliasSelect + ".f_SUM_SAL_w_VAT as " + " '" + vBegin.ToShortDateString() + "' \r\n";
				QUERYSOURCE = QUERYSOURCE +
			   " left join (select \r\n" +
			   "    S_C.ID_CONTRACTOR, \r\n" +
					//			   "    S_C.NAME CONTRACTOR_NAME, \r\n" +
					//			   "    S_C.ADDRESS, \r\n" +
			   "    convert(money, sum(lm.SUM_ACC * SIGN(lm.QUANTITY_SUB))) AS f_SUM_SAL_w_VAT \r\n" +
			   "from lot_movement lm \r\n" +
			   "inner join lot l on l.id_lot_global = lm.id_lot_global \r\n" +
			   "INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE \r\n" +
			   "INNER JOIN CONTRACTOR S_C ON S_C.ID_CONTRACTOR = S.ID_CONTRACTOR \r\n" +
			   "where (convert(datetime, convert(varchar(500), lm.DATE_OP, 104), 104)) = convert(datetime,'" + vBegin.ToShortDateString() + "', 104) \r\n" +
			   "   AND (Ltrim(lm.CODE_OP)='CHEQUE') \r\n" +

			   //"AND EXISTS(Select 1 \r\n" +
					//"from CHEQUE_ITEM chi \r\n" +
					//"INNER JOIN CHEQUE ch ON chi.ID_CHEQUE_GLOBAL = ch.ID_CHEQUE_GLOBAL And ch.PAY_TYPE_NAME = \'Оплата наличными\' \r\n" +
					//"WHERE chi.ID_CHEQUE_ITEM_GLOBAL = lm.ID_DOCUMENT_ITEM) \r\n" +

			   "group by S_C.ID_CONTRACTOR, S_C.NAME, S_C.ADDRESS) as " + AliasSelect + " on " + AliasSelect + ".ID_CONTRACTOR = C.ID_CONTRACTOR \r\n";
				vBegin = vBegin.AddDays(1);
			}
			result =
				"SELECT  \r\n";
			if (wHead)
			{
				result += "    C.A_COD as 'Код Ригла', \r\n" +
						  "    C.NAME as '№ аптеки' \r\n";
				//			"    C.ADDRESS as 'Адрес' \r\n" +
			}
			else
			{
				QUERYLIST = QUERYLIST.Remove(0, 1);
			}
			result += QUERYLIST +
			" FROM CONTRACTOR C \r\n" +
			QUERYSOURCE +
			" WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM STORE) \r\n" +
			" order by name \r\n";
			return result;
		}
		private string GetDMLPrimeCost(DateTime vBegin, DateTime vEnd, bool wHead)
		{
			string result = "";
			string AliasSelect = "A";
			string QUERYLIST = "";
			string QUERYSOURCE = "";
			int i = 0;
			//DateTime vBegin = DateTime.Parse(period.DateFrom.ToShortDateString());
			//DateTime vEnd = DateTime.Parse(period.DateTo.ToShortDateString());
			while (vBegin <= vEnd)
			{
				i++;
				AliasSelect = "A" + i.ToString();
				QUERYLIST = QUERYLIST + ", " + AliasSelect + ".f_SUM_SAL_w_VAT as " + " '" + vBegin.ToShortDateString() + "' \r\n";
				QUERYSOURCE = QUERYSOURCE +
			   " left join (select \r\n" +
			   "    S_C.ID_CONTRACTOR, \r\n" +
					//			   "    S_C.NAME CONTRACTOR_NAME, \r\n" +
					//			   "    S_C.ADDRESS, \r\n" +
			   "    convert(money, round(sum(lm.SUM_SUP * SIGN(lm.QUANTITY_SUB)),2)) AS f_SUM_SAL_w_VAT \r\n" +
			   "from lot_movement lm \r\n" +
			   "inner join lot l on l.id_lot_global = lm.id_lot_global \r\n" +
			   "INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE \r\n" +
			   "INNER JOIN CONTRACTOR S_C ON S_C.ID_CONTRACTOR = S.ID_CONTRACTOR \r\n" +
			   "where (convert(datetime, convert(varchar(500), lm.DATE_OP, 104), 104)) = convert(datetime,'" + vBegin.ToShortDateString() + "', 104) \r\n" +
			   "   AND (Ltrim(lm.CODE_OP)='CHEQUE') \r\n" +

			   //"AND EXISTS(Select 1 \r\n" +
					//"from CHEQUE_ITEM chi \r\n" +
					//"INNER JOIN CHEQUE ch ON chi.ID_CHEQUE_GLOBAL = ch.ID_CHEQUE_GLOBAL And ch.PAY_TYPE_NAME = \'Оплата наличными\' \r\n" +
					//"WHERE chi.ID_CHEQUE_ITEM_GLOBAL = lm.ID_DOCUMENT_ITEM) \r\n" +

			   "group by S_C.ID_CONTRACTOR, S_C.NAME, S_C.ADDRESS) as " + AliasSelect + " on " + AliasSelect + ".ID_CONTRACTOR = C.ID_CONTRACTOR \r\n";
				vBegin = vBegin.AddDays(1);
			}
			result =
				"SELECT  \r\n";
			if (wHead)
			{
				result += "    C.A_COD as 'Код Ригла', \r\n" +
						  "    C.NAME as '№ аптеки' \r\n";
				//			"    C.ADDRESS as 'Адрес' \r\n" +
			}
			else
			{
				QUERYLIST = QUERYLIST.Remove(0, 1);
			}
			result += QUERYLIST +
			" FROM CONTRACTOR C \r\n" +
			QUERYSOURCE +
			" WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM STORE) \r\n" +
			" order by name \r\n";
			return result;
		}
		private string GetDMLCustomers(DateTime vBegin, DateTime vEnd, bool wHead)
		{
			string result = "";
			string AliasSelect = "AS";
			string AliasSelect2 = "AR";
			string QUERYLIST = "";
			string QUERYSOURCE = "";
			int i = 0;
			//DateTime vBegin = DateTime.Parse(period.DateFrom.ToShortDateString());
			//DateTime vEnd = DateTime.Parse(period.DateTo.ToShortDateString());
			while (vBegin <= vEnd)
			{
				i++;
				AliasSelect = "AS" + i.ToString();
				AliasSelect2 = "AR" + i.ToString();
				QUERYLIST = QUERYLIST + ", " + "isnull(" + AliasSelect + ".f_SUM_SAL_w_VAT,0) - isnull(" + AliasSelect2 + ".f_SUM_SAL_w_VAT,0) " + " as " + " '" + vBegin.ToShortDateString() + "' \r\n";
				QUERYSOURCE = QUERYSOURCE +
			   " left join (select \r\n" +
			   "    S_C.ID_CONTRACTOR, \r\n" +
			   "    Count(distinct cast(chi.ID_CHEQUE_GLOBAL as varchar(36))) AS f_SUM_SAL_w_VAT \r\n" +
			   "from lot_movement lm \r\n" +
			   "inner join lot l on l.id_lot_global = lm.id_lot_global \r\n" +
			   "INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE \r\n" +
			   "INNER JOIN CONTRACTOR S_C ON S_C.ID_CONTRACTOR = S.ID_CONTRACTOR \r\n" +
			   "INNER JOIN CHEQUE_ITEM chi ON chi.ID_CHEQUE_ITEM_GLOBAL = lm.ID_DOCUMENT_ITEM \r\n" +
					//"INNER JOIN CHEQUE ch ON chi.ID_CHEQUE_GLOBAL = ch.ID_CHEQUE_GLOBAL And ch.PAY_TYPE_NAME = \'Оплата наличными\'  \r\n" +
			   "where (convert(datetime, convert(varchar(500), lm.DATE_OP, 104), 104)) = convert(datetime,'" + vBegin.ToShortDateString() + "', 104) \r\n" +
			   "   AND (Ltrim(lm.CODE_OP)='CHEQUE') \r\n" +
			   "   AND LM.QUANTITY_SUB > 0 \r\n" +
			   "group by S_C.ID_CONTRACTOR) as " + AliasSelect + " on " + AliasSelect + ".ID_CONTRACTOR = C.ID_CONTRACTOR \r\n" +

				" left join (select \r\n" +
				"    S_C.ID_CONTRACTOR, \r\n" +
				"    Count(distinct cast(chi.ID_CHEQUE_GLOBAL as varchar(36))) AS f_SUM_SAL_w_VAT \r\n" +
				"from lot_movement lm \r\n" +
				"inner join lot l on l.id_lot_global = lm.id_lot_global \r\n" +
				"INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE \r\n" +
				"INNER JOIN CONTRACTOR S_C ON S_C.ID_CONTRACTOR = S.ID_CONTRACTOR \r\n" +
				"INNER JOIN CHEQUE_ITEM chi ON chi.ID_CHEQUE_ITEM_GLOBAL = lm.ID_DOCUMENT_ITEM \r\n" +
					//"INNER JOIN CHEQUE ch ON chi.ID_CHEQUE_GLOBAL = ch.ID_CHEQUE_GLOBAL And ch.PAY_TYPE_NAME = \'Оплата наличными\'  \r\n" +
				"where (convert(datetime, convert(varchar(500), lm.DATE_OP, 104), 104)) = convert(datetime,'" + vBegin.ToShortDateString() + "', 104) \r\n" +
				"   AND (Ltrim(lm.CODE_OP)='CHEQUE') \r\n" +
				"   AND LM.QUANTITY_SUB < 0 \r\n" +
				"group by S_C.ID_CONTRACTOR) as " + AliasSelect2 + " on " + AliasSelect2 + ".ID_CONTRACTOR = C.ID_CONTRACTOR \r\n";

				vBegin = vBegin.AddDays(1);
			}
			result =
				"SELECT  \r\n";
			if (wHead)
			{
				result += "    C.A_COD as 'Код Ригла', \r\n" +
						  "    C.NAME as '№ аптеки' \r\n";
				//			"    C.ADDRESS as 'Адрес' \r\n" +
			}
			else
			{
				QUERYLIST = QUERYLIST.Remove(0, 1);
			}
			result += QUERYLIST +
			" FROM CONTRACTOR C \r\n" +
			QUERYSOURCE +
			" WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM STORE) \r\n" +
			" order by name \r\n";
			return result;
		}
		private string GetDMLDiscountACC(DateTime vBegin, DateTime vEnd, bool wHead)
		{
			string result = "";
			string AliasSelect = "A";
			string QUERYLIST = "";
			string QUERYSOURCE = "";
			int i = 0;
			//DateTime vBegin = DateTime.Parse(period.DateFrom.ToShortDateString());
			//DateTime vEnd = DateTime.Parse(period.DateTo.ToShortDateString());
			while (vBegin <= vEnd)
			{
				i++;
				AliasSelect = "A" + i.ToString();
				QUERYLIST = QUERYLIST + ", " + AliasSelect + ".f_SUM_SAL_w_VAT as " + " '" + vBegin.ToShortDateString() + "' \r\n";
				QUERYSOURCE = QUERYSOURCE +
			   " left join (select \r\n" +
			   "    S_C.ID_CONTRACTOR, \r\n" +
					//			   "    S_C.NAME CONTRACTOR_NAME, \r\n" +
					//			   "    S_C.ADDRESS, \r\n" +
			   "    convert(money, sum(lm.DISCOUNT_ACC * SIGN(lm.QUANTITY_SUB))) AS f_SUM_SAL_w_VAT \r\n" +
			   "from lot_movement lm \r\n" +
			   "inner join lot l on l.id_lot_global = lm.id_lot_global \r\n" +
			   "INNER JOIN STORE S ON S.ID_STORE = L.ID_STORE \r\n" +
			   "INNER JOIN CONTRACTOR S_C ON S_C.ID_CONTRACTOR = S.ID_CONTRACTOR \r\n" +
			   "where (convert(datetime, convert(varchar(500), lm.DATE_OP, 104), 104)) = convert(datetime,'" + vBegin.ToShortDateString() + "', 104) \r\n" +
			   "   AND (Ltrim(lm.CODE_OP)='CHEQUE') \r\n" +

			   //"AND EXISTS(Select 1 \r\n" +
					//"from CHEQUE_ITEM chi \r\n" +
					//"INNER JOIN CHEQUE ch ON chi.ID_CHEQUE_GLOBAL = ch.ID_CHEQUE_GLOBAL And ch.PAY_TYPE_NAME = \'Оплата наличными\' \r\n" +
					//"WHERE chi.ID_CHEQUE_ITEM_GLOBAL = lm.ID_DOCUMENT_ITEM) \r\n"+

			   "group by S_C.ID_CONTRACTOR, S_C.NAME, S_C.ADDRESS) as " + AliasSelect + " on " + AliasSelect + ".ID_CONTRACTOR = C.ID_CONTRACTOR \r\n";
				vBegin = vBegin.AddDays(1);
			}
			result =
				"SELECT  \r\n";
			if (wHead)
			{
				result += "    C.A_COD as 'Код Ригла', \r\n" +
						  "    C.NAME as '№ аптеки' \r\n";
				//			"    C.ADDRESS as 'Адрес' \r\n" +
			}
			else
			{
				QUERYLIST = QUERYLIST.Remove(0, 1);
			}
			result += QUERYLIST +
			" FROM CONTRACTOR C \r\n" +
			QUERYSOURCE +
			" WHERE C.ID_CONTRACTOR IN (SELECT ID_CONTRACTOR FROM STORE) \r\n" +
			" order by name \r\n";
			return result;
		}

		public void SetNumBar(int r, int c, Worksheet pWorksheet, int pCount)
		{
			for (int i = 1; i < pCount + 1; i++)
			{
				if (i == 1)
				{
					pWorksheet.Cells[r, c].PutValue("№ п/п");
				}
				pWorksheet.Cells[++r, c].PutValue(i);
			}
		}

		public void SetTotalBarR(int startR, int startC, Worksheet pWorksheet, int stopR, int stopC)
		{
			string vFormula = "";
			for (int j = startR; j <= stopR; j++)
			{
				if (j == startR)
				{
					//pWorksheet.Cells[j, stopC+1].PutValue("Итого");
					continue;
				}
				for (int i = startC; i <= stopC; i++)
				{
					if (i == startC)
					{
						vFormula = pWorksheet.Cells[j, i].Name;
					}
					else
					{
						vFormula = vFormula + "+" + pWorksheet.Cells[j, i].Name;
					}
				}
				pWorksheet.Cells[j, stopC + 1].Formula = "=" + vFormula;
				pWorksheet.Cells[startR, stopC + 1].PutValue("Итого");
			}
			pWorksheet.AutoFitColumns();
		}


		public void SetTotalBarRAVG(int startR, int startC, Worksheet pWorksheet, int stopR, int stopC)
		{
			int a_i = startR + 1;
			int a_j = stopC + 1;

			pWorksheet.Cells[startR, stopC + 1].PutValue("Средняя");

			decimal taking = 0;
			decimal primeCost = 0;

			for (int k = 0; k < vTableTakingsLeftPlus.Rows.Count; k++)
			{
				for (int i = 2; i < vTableTakingsLeftPlus.Columns.Count; i++)
				{
					taking += Utils.GetDecimal(vTableTakingsLeftPlus.Rows[k][i]);
					primeCost += Utils.GetDecimal(vTablePrimeCostLeftPlus.Rows[k][i]);
				}
				decimal value = primeCost == 0 ? 0 : (taking / primeCost - 1) * 100;
				pWorksheet.Cells[a_i, a_j].PutValue(Math.Round(value, 2));
				a_i++;
			}

			a_i = stopR;
			a_j = startC;

			decimal v1 = 0;
			decimal v2 = 0;

			for (int i = 2; i < vTableTakingsLeftPlus.Columns.Count; i++)
			{
				for (int k = 0; k < vTableTakingsLeftPlus.Rows.Count; k++)
				{
					v1 += Utils.GetDecimal(vTableTakingsLeftPlus.Rows[k][i]);
					v2 += Utils.GetDecimal(vTablePrimeCostLeftPlus.Rows[k][i]);
				}

				decimal value = v2 == 0 ? 0 : (v1 / v2 - 1) * 100;
				pWorksheet.Cells[a_i, a_j].PutValue(Math.Round(value, 2));
				a_j++;
			}

			pWorksheet.AutoFitColumns();
		}


		//=============
		public DataTable ImportXls(Worksheet pWorksheet, GetDML vGetDML, DateTime vBegin, DateTime vEnd, int pstartRow, byte pstartCol)
		{
			int vRow = pstartRow;
			byte vCol = pstartCol;
			DataTable vTable = null;
			DataTable vvTable = new DataTable();
			TableLeftPlus vTotalTablePlus = null;

			DateTime vtBegin = vBegin;
			DateTime vtEnd = vBegin.AddDays(20);
			if (vEnd.Subtract(vBegin).Days > 20)
			{
				bool insert = true;
				while (vtEnd.Subtract(vtBegin).Days >= 19)
				{
					vTable = GetData(vGetDML(vtBegin, vtEnd, vCol > 20 ? false : true));
					DataColumn[] DataColumnM = new DataColumn[vTable.Columns.Count];
					for (int i = 0; i < vTable.Columns.Count; i++)
					{
						DataColumnM[i] = new DataColumn();
						DataColumnM[i].Caption = vTable.Columns[i].Caption;
						DataColumnM[i].DataType = vTable.Columns[i].DataType;
					}
					vvTable.Columns.AddRange(DataColumnM);

					if (vCol > 20)
					{
						//insert = false;
						vTotalTablePlus.AddTableLeft(vTable);
					}
					else
					{
						vTotalTablePlus = new TableLeftPlus(vTable.Rows.Count);
						vTotalTablePlus.AddTableLeft(vTable);
					}
					pWorksheet.Cells.ImportDataTable(vTable, true, pstartRow, vCol, vCol > 20 ? false : true);
					vCol += (byte) vTable.Columns.Count;
					vtBegin = vtEnd.AddDays(1);
					vtEnd = vtEnd.AddDays(20);
					if (vtEnd >= vEnd)
					{
						vtEnd = vEnd;
						vTable = GetData(vGetDML(vtBegin, vtEnd, vCol > 20 ? false : true));
						vTotalTablePlus.AddTableLeft(vTable);
						DataColumnM = new DataColumn[vTable.Columns.Count];
						for (int i = 0; i < vTable.Columns.Count; i++)
						{
							DataColumnM[i] = new DataColumn();
							DataColumnM[i].Caption = vTable.Columns[i].Caption;
							DataColumnM[i].DataType = vTable.Columns[i].DataType;
						}
						vvTable.Columns.AddRange(DataColumnM);
						//vvTable.

						pWorksheet.Cells.ImportDataTable(vTable, true, pstartRow, vCol, vCol > 20 ? false : true);
						vCol += (byte) vTable.Columns.Count;
					}
				}

			}
			else
			{
				vTable = GetData(vGetDML(vBegin, vEnd, true));
				pWorksheet.Cells.ImportDataTable(vTable, true, pstartRow, pstartCol);
				vCol += (byte) vTable.Columns.Count;
				vTotalTablePlus = new TableLeftPlus(vTable.Rows.Count);
				vTotalTablePlus.AddTableLeft(vTable);

				DataColumn[] DataColumnM = new DataColumn[vTable.Columns.Count];
				for (int i = 0; i < vTable.Columns.Count; i++)
				{
					DataColumnM[i] = new DataColumn();
					DataColumnM[i].Caption = vTable.Columns[i].Caption;
					DataColumnM[i].DataType = vTable.Columns[i].DataType;
				}
				vvTable.Columns.AddRange(DataColumnM);
			}
			byte vstopCol = vCol;

			vRow = pstartRow + vTable.Rows.Count + 1;
			for (int i = pstartCol + 2; i < vstopCol; i++)
			{
				string vFormula = "";
				double vFormulaSum = 0;
				for (int j = pstartRow + 1; j <= vRow - 1; j++)
				{
					if (j == pstartRow + 1)
					{
						vFormula = pWorksheet.Cells[j, i].Name;
						//vFormulaSum = (double)pWorksheet.Cells[j, i].Value;
					}
					else
					{
						vFormula = vFormula + "+" + pWorksheet.Cells[j, i].Name;
						//vFormulaSum = vFormulaSum + (double)pWorksheet.Cells[j, i].Value;
					}
				}
				pWorksheet.Cells[vRow, i].Formula = "=" + vFormula;
				//pWorksheet.Cells[vRow, i].PutValue(Math.Round(vFormulaSum / (stopR - startR - 1), 2));
				if (i == pstartCol + 2)
				{
					pWorksheet.Cells[vRow + 1, i].Formula = String.Format("={0}", pWorksheet.Cells[vRow, i].Name);
				}
				else
				{
					pWorksheet.Cells[vRow + 1, i].Formula = String.Format("={0}+{1}", pWorksheet.Cells[vRow + 1, i - 1].Name, pWorksheet.Cells[vRow, i].Name);
				}
			}
			SetTotalBarR(pstartRow, pstartCol + 2, pWorksheet, vRow, vstopCol - 1);
			SetNumBar(pstartRow, pstartCol - 1, pWorksheet, vTable.Rows.Count);
			pWorksheet.Cells[vRow, pstartCol + 1].PutValue("ИТОГО");
			pWorksheet.Cells[vRow + 1, pstartCol + 1].PutValue("НАКОПИТЕЛЬНЫМ ИТОГОМ");

			pWorksheet.AutoFitColumns();

			//for (int i = 0; i < vTotalTablePlus[0].Count; i++)
			//{
			//    vvTable.Columns.Add();
			//}
			for (int i = 0; i < vTotalTablePlus.Count; i++)
			{
				DataRow vDataRow = vvTable.NewRow();
				for (int j = 0; j < vTotalTablePlus[i].Count; j++)
				{
					vDataRow[j] = vTotalTablePlus[i][j];
				}
				vvTable.Rows.Add(vDataRow);

			}
			return vvTable;
		}
		//============

		public void ImportXlsTable(Worksheet pWorksheet, DataTable vTable, int pstartRow, byte pstartCol)
		{
			int vRow = pstartRow;
			byte vCol = pstartCol;

			pWorksheet.Cells.ImportDataTable(vTable, true, pstartRow, pstartCol);
			vCol += (byte) vTable.Columns.Count;

			byte vstopCol = vCol;

			vRow = pstartRow + vTable.Rows.Count + 1;
			for (int i = pstartCol + 2; i < vstopCol; i++)
			{
				string vFormula = "";
				double vFormulaSum = 0;
				for (int j = pstartRow + 1; j <= vRow - 1; j++)
				{
					if (j == pstartRow + 1)
					{
						//vFormula = pWorksheet.Cells[j, i].Name;
						vFormulaSum = (double) pWorksheet.Cells[j, i].Value;
					}
					else
					{
						//vFormula = vFormula + "+" + pWorksheet.Cells[j, i].Name;
						vFormulaSum = vFormulaSum + (double) pWorksheet.Cells[j, i].Value;
					}
				}
				//pWorksheet.Cells[vRow, i].Formula = "=" + vFormula;
				pWorksheet.Cells[vRow, i].PutValue(vFormulaSum);
			}

			SetTotalBarRAVG(pstartRow, pstartCol + 2, pWorksheet, vRow, vstopCol - 1);
			SetNumBar(pstartRow, pstartCol - 1, pWorksheet, vTable.Rows.Count);
			pWorksheet.Cells[vRow, pstartCol + 1].PutValue("ИТОГО");

			pWorksheet.AutoFitColumns();
		}

		public void ImportLastTable(Worksheet pWorksheet, DataTable vTable, int pstartRow, byte pstartCol)
		{
			int vRow = pstartRow;
			byte vCol = pstartCol;
			vRow = pstartRow + vTable.Rows.Count + 1;

			pWorksheet.Cells.ImportDataTable(vTable, true, pstartRow, pstartCol);
			SetTotalBarLast(pstartRow, pstartCol + 2, pWorksheet, vRow, pstartCol - 1);
			SetNumBar(pstartRow, pstartCol - 1, pWorksheet, vTable.Rows.Count);
			
			pWorksheet.Cells[vRow, pstartCol + 1].PutValue("ИТОГО");

			pWorksheet.AutoFitColumns();
		}

		private void SetTotalBarLast(int startR, int startC, Worksheet pWorksheet, int stopR, int stopC)
		{
			int a_i = startR + 1;
			int a_j = stopC + vTableTakingsLeftPlus.Columns.Count + 1;

			pWorksheet.Cells[startR, a_j].PutValue("Средняя");

			decimal taking = 0;
			decimal primeCost = 0;

			for (int k = 0; k < vTableTakingsLeftPlus.Rows.Count; k++)
			{
				for (int i = 2; i < vTableTakingsLeftPlus.Columns.Count; i++)
				{
					taking += Utils.GetDecimal(vTableTakingsLeftPlus.Rows[k][i]);
					primeCost += Utils.GetDecimal(vTableLeftPlus.Rows[k][i]);
				}
				decimal value = taking == 0 ? 0 : primeCost / taking * 100;
				pWorksheet.Cells[a_i, a_j].PutValue(Math.Round(value, 2));
				a_i++;
			}

			a_i = stopR;
			a_j = startC;

			decimal v1 = 0;
			decimal v2 = 0;

			for (int i = 2; i < vTableTakingsLeftPlus.Columns.Count; i++)
			{
				for (int k = 0; k < vTableTakingsLeftPlus.Rows.Count; k++)
				{
					v1 += Utils.GetDecimal(vTableTakingsLeftPlus.Rows[k][i]);
					v2 += Utils.GetDecimal(vTableLeftPlus.Rows[k][i]);
				}

				decimal value = v1 == 0 ? 0 : v2 / v1 * 100;
				pWorksheet.Cells[a_i, a_j].PutValue(Math.Round(value, 2));
				a_j++;
			}

			pWorksheet.AutoFitColumns();
		}

		DataTable vTableLeftPlus = null;
		DataTable vTablePrimeCostLeftPlus = null;
		DataTable vTableTakingsLeftPlus = null;
		DataTable vTableExtraChargeLeftPlus = null;
		Workbook vWorkbook;

		public void Print(string[] reportFiles)
		{
			string StartPoint = "";
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);
			Utils.AddNode(root, "DATE_FR", period.DateFrom);
			Utils.AddNode(root, "DATE_TO", period.DateTo);
			int vRow = 0;

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Файлы Excel (*.xls)|*.xls";
			dlg.RestoreDirectory = true;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				//File.Create(dlg.FileName);
			}
			vWorkbook = new Workbook();

			//============Worksheets[0]  vTableTakings
			Worksheet vWorksheet = vWorkbook.Worksheets[0];
			vWorksheet.PageSetup.FitToPagesWide = 1;
			vWorksheet.PageSetup.Orientation = PageOrientationType.Landscape;
			vWorksheet.Name = "Отчет";
			//====================
			vWorksheet.Cells["B5"].PutValue("Выручка от реализации");
			vTableTakingsLeftPlus = ImportXls(vWorksheet, new GetDML(GetDMLTakings), period.DateFrom, period.DateTo, 6, 2);
			vRow = vTableTakingsLeftPlus.Rows.Count + 6 + 1;

			vRow += 3;
			//========================================
			vWorksheet.Cells[vRow, 1].PutValue("Количество клиентов");
			vTableLeftPlus = ImportXls(vWorksheet, new GetDML(GetDMLCustomers), period.DateFrom, period.DateTo, vRow + 2, 2);
			vRow = vTableLeftPlus.Rows.Count + 6 + 1;
			//========================================

			vWorksheet.AutoFitColumns();

			//  vWorksheet[1]
			vWorkbook.Worksheets.Add();
			vWorksheet = vWorkbook.Worksheets[1];
			vWorksheet.PageSetup.FitToPagesWide = 1;
			vWorksheet.PageSetup.Orientation = PageOrientationType.Landscape;
			vWorksheet.Name = "Себестоимость и скидки";
			//====================
			vWorksheet.Cells["B5"].PutValue("Себеcтоимость реализованного товара (с НДС)");
			vTablePrimeCostLeftPlus = ImportXls(vWorksheet, new GetDML(GetDMLPrimeCost), period.DateFrom, period.DateTo, 6, 2);
			vRow = vTablePrimeCostLeftPlus.Rows.Count + 6 + 1;
			vRow += 3;
			//========================================
			vWorksheet.Cells[vRow, 1].PutValue("Сумма скидки за отчетный период");
			vTableLeftPlus = ImportXls(vWorksheet, new GetDML(GetDMLDiscountACC), period.DateFrom, period.DateTo, vRow + 2, 2);
			vRow = vTableLeftPlus.Rows.Count;
			//========================================

			vWorksheet.AutoFitColumns();

			//  vWorksheet[2]
			vWorkbook.Worksheets.Add();
			vWorksheet = vWorkbook.Worksheets[2];
			vWorksheet.PageSetup.FitToPagesWide = 1;
			vWorksheet.PageSetup.Orientation = PageOrientationType.Landscape;
			vWorksheet.Name = "Наценка";
			//====================
			vWorksheet.Cells["B5"].PutValue("Наценка с учетом скидки");
			vTableExtraChargeLeftPlus = vTableLeftPlus.Copy();
			for (int i = 0; i < vTableTakingsLeftPlus.Rows.Count; i++)
			{
				for (int j = 2; j < vTableTakingsLeftPlus.Columns.Count; j++)
				{
					double A = vTableTakingsLeftPlus.Rows[i][j] is DBNull ? 0 : double.Parse(vTableTakingsLeftPlus.Rows[i][j].ToString());
					double B = vTablePrimeCostLeftPlus.Rows[i][j] is DBNull ? 0 : double.Parse(vTablePrimeCostLeftPlus.Rows[i][j].ToString());
					if (B == 0)
					{
						vTableExtraChargeLeftPlus.Rows[i][j] = 0;
					}
					else
					{
						vTableExtraChargeLeftPlus.Rows[i][j] = Math.Round((A / B - 1) * 100, 2);
					}
				}
			}
			ImportXlsTable(vWorksheet, vTableExtraChargeLeftPlus, 6, 2);

			vRow = vTableExtraChargeLeftPlus.Rows.Count + 6 + 1;
			vRow += 3;
						
			vWorksheet.Cells[vRow, 1].PutValue("Процент скидки от выручки");

			for (int i = 2; i < vTableTakingsLeftPlus.Columns.Count; i++)
			{
				for (int k = 0; k < vTableTakingsLeftPlus.Rows.Count; k++)
				{
					decimal v1 = Utils.GetDecimal(vTableLeftPlus.Rows[k][i]);
					decimal v2 = Utils.GetDecimal(vTableTakingsLeftPlus.Rows[k][i]);
					vTableExtraChargeLeftPlus.Rows[k][i] = v2 == 0 ? 0 : Math.Round(v1 / v2 * 100, 2);
				}
			}

			ImportLastTable(vWorksheet, vTableExtraChargeLeftPlus, vRow + 2, 2);
			
			vWorksheet.AutoFitColumns();
			
			vWorkbook.Save(dlg.FileName);
		}

		public string ReportName
		{
			get { return "Отчет по себестоимости"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
		}
	}

	public delegate string GetDML(DateTime vBegin, DateTime vEnd, bool wHead);
	public class TableLeftPlus : List<List<object>>
	{
		public TableLeftPlus(int n)
		{
			for (int i = 0; i < n; i++)
			{
				this.Add(new List<object>());
			}
		}
		public void AddTableLeft(DataTable vTable)
		{
			for (int i = 0; i < vTable.Rows.Count; i++)
			{
				for (int j = 0; j < vTable.Columns.Count; j++)
				{
					this[i].Add(vTable.Rows[i][j]);
				}
			}
		}
	}
}