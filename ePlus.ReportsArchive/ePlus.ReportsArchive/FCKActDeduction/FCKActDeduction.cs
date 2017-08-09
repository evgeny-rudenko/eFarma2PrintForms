using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace FCKActDeduction
{
	public class FCKActDeduction : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("FCKActDeduction.ActDeduction.sql");

			using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
			{
				string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

				SqlCommand comm = null;
				foreach (string statement in batch)
				{
					if (statement == string.Empty)
						continue;

					using (SqlConnection con = new SqlConnection(connectionString))
					{
						comm = new SqlCommand(statement, con);
						con.Open();
						comm.ExecuteNonQuery();
					}
				}
			}
		}

		void ExtractReport()
		{
			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
			if (!Directory.Exists(cachePath))
				Directory.CreateDirectory(cachePath);
			Stream s = this.GetType().Assembly.GetManifestResourceStream("FCKActDeduction.ActDeduction.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActDeduction.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_DEDUCTION", dataRowItem.Id);
			Utils.AddNode(root, "ID_ACT_DEDUCTION_GLOBAL", dataRowItem.Guid);
			Utils.AddNode(root, "DATE_FROM", dataRowItem.Id);
			Utils.AddNode(root, "DATE_TO", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActDeduction.rdlc");

			rep.LoadData("REP_FCK_ACT_DEDUCTION", doc.InnerXml);
			rep.BindDataSource("Act_Deduction_DS_Table", 0);
			rep.BindDataSource("Act_Deduction_DS_Table1", 1);
						
			decimal fPm_SumAll = 0.0M;
			int fPm_SumQTY = 0;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
			{
				fPm_SumQTY += AsInt(dataSet.Tables[1].Rows[i]["IT_D_QUANTITY"]);
				fPm_SumAll += AsDecimal(dataSet.Tables[1].Rows[i]["IT_D_SUM_ACC"]);
			}

			ReportParameter[] parameters = new ReportParameter[4] {
				//new ReportParameter("DATE_FROM", periodPeriod.DateFrText),
				//new ReportParameter("DATE_TO", periodPeriod.DateToText),
				new ReportParameter("Pm_SumAll", fPm_SumAll.ToString()),
				new ReportParameter("Pm_SumQTY", fPm_SumQTY.ToString()),
				//new ReportParameter("Pm_ActNumber", AsString(dataSet.Tables[0].Rows[0]["AD_NUMBER"])),
				new ReportParameter("Pm_RusWordsSumAll", AsRusCurrency((double)fPm_SumAll)),
				new ReportParameter("Pm_RusWordsSumQTY", AsRusNumWord((double)fPm_SumQTY))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

		static decimal AsDecimal(object value)
		{
			return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
		}

		static int AsInt(object value)
		{
			return value != DBNull.Value ? Convert.ToInt32(value) : 0;
		}


		static string AsString(object value)
		{
			return value != DBNull.Value ? Convert.ToString(value) : "";
		}

		static string AsRusCurrency(double doubleValue)
		{
			string stringValue = doubleValue < 0 ? "минус " : "";
			double doubleTemp = Math.Round(Math.Abs(doubleValue), 3);
			int intRub = (int) Math.Floor(doubleTemp), intKop = (int) Math.Round(100 * (doubleTemp - intRub), 0);
			string stringRub = IntToRusWords(intRub), stringKop = IntToRusWords(intKop);

			if (stringRub.EndsWith("один")) stringRub = stringRub + " рубль";
			else if (stringRub.EndsWith("два") || stringRub.EndsWith("три") || stringRub.EndsWith("четыре")) stringRub = stringRub + " рубл€";
			else stringRub = stringRub + " рублей";

			stringKop = stringKop.Replace(" один", " одна");
			stringKop = stringKop.Replace(" два", " две");
			if (stringKop.EndsWith("одна")) stringKop = stringKop + " копейка";
			else if (stringKop.EndsWith("две") || stringKop.EndsWith("три") || stringKop.EndsWith("четыре")) stringKop = stringKop + " копейки";
			else stringKop = stringKop + " копеек";

			stringValue = stringValue + stringRub + " " + stringKop;
			stringValue = stringValue.Trim();
			stringValue = char.ToUpper(stringValue[0]) + stringValue.Substring(1);
			return stringValue;
		}

		static string AsRusNumWord(double doubleValue)
		{
			string stringValue = doubleValue < 0 ? "минус " : "";
			double doubleTemp = Math.Round(Math.Abs(doubleValue), 3);
			int intRub = (int)Math.Floor(doubleTemp), intKop = (int)Math.Round(100 * (doubleTemp - intRub), 0);
			string stringRub = IntToRusWords(intRub), stringKop = IntToRusWords(intKop);

			if (stringRub.EndsWith("один")) stringRub = stringRub;
			else if (stringRub.EndsWith("два") || stringRub.EndsWith("три") || stringRub.EndsWith("четыре")) stringRub = stringRub;
			else stringRub = stringRub;

			stringKop = stringKop.Replace(" один", " одна");
			stringKop = stringKop.Replace(" два", " две");
			if (stringKop.EndsWith("одна")) stringKop = stringKop;
			else if (stringKop.EndsWith("две") || stringKop.EndsWith("три") || stringKop.EndsWith("четыре")) stringKop = stringKop;
			else stringKop = stringKop;

			stringValue = stringValue + stringRub;
			stringValue = stringValue.Trim();
			stringValue = char.ToUpper(stringValue[0]) + stringValue.Substring(1);
			return stringValue;
		}


		static string IntToRusWords(int intValue)
		{
			string stringValue = intValue < 0 ? "минус " : "";
			int intTemp = Math.Abs(intValue), intDenominator = 1000000000;
			Validation(ref stringValue, ref intTemp, ref intDenominator);
			if (stringValue == "") stringValue = "ноль";
			return stringValue.Trim();
		}

		static void Validation(ref string stringValue, ref int intValue, ref int intDenominator)
		{
			int intResult = intValue / intDenominator, intTemp;
			string stringTemp;
			if (intDenominator > 999999999)
			{
				stringTemp = "миллиард";
				intTemp = 1000000000;
				if (intResult > 0)
				{
					stringValue = stringValue.Replace("миллиардов ", "");
					stringValue = stringValue.Replace("миллиарда ", "");
					stringValue = stringValue.Replace("миллиард ", "");
				}
			}
			else if (intDenominator > 999999)
			{
				stringTemp = "миллион";
				intTemp = 1000000;
				if (intResult > 0)
				{
					stringValue = stringValue.Replace("миллионов ", "");
					stringValue = stringValue.Replace("миллиона ", "");
					stringValue = stringValue.Replace("миллион ", "");
				}
			}
			else if (intDenominator > 999)
			{
				stringTemp = "тыс€ч";
				intTemp = 1000;
				if (intResult > 0)
				{
					stringValue = stringValue.Replace("тыс€чи ", "");
					stringValue = stringValue.Replace("тыс€ча ", "");
					stringValue = stringValue.Replace("тыс€ч ", "");
				}
			}
			else
			{
				stringTemp = "";
				intTemp = 1;
			}
			switch (intDenominator / intTemp)
			{
				case 1: stringValue = stringValue + string.Format(Numerics(intValue, intDenominator, intTemp == 1000, stringValue.EndsWith("дес€ть ")), stringTemp); if (!stringValue.EndsWith("дес€ть ")) stringValue = stringValue.Replace("дес€ть ", ""); break;
				case 10: stringValue = stringValue + string.Format(Decimals(intValue, intDenominator, intTemp == 1000), stringTemp); break;
				case 100: stringValue = stringValue + string.Format(Hundreds(intValue, intDenominator, intTemp == 1000), stringTemp); break;
				default: break;
			}
			if (intValue < 1000)
			{
				stringValue = stringValue.Replace(" ов ", " ");
				stringValue = stringValue.Replace(" а ", " ");
				stringValue = stringValue.Replace(" и ", " ");
			}

			if (intDenominator < 10) return;

			intValue = intValue - intDenominator * intResult;
			intDenominator = intDenominator / 10;
			Validation(ref stringValue, ref intValue, ref intDenominator);
		}

		static string Numerics(int intNumerator, int intDenominator, bool IsThousand, bool IsTen)
		{
			string stringValue;

			if (IsTen)
			{
				switch (intNumerator / intDenominator)
				{
					case 1: stringValue = "одиннадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 2: stringValue = "двенадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 3: stringValue = "тринадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 4: stringValue = "четырнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 5: stringValue = "п€тнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 6: stringValue = "шестнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 7: stringValue = "семнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 8: stringValue = "восемнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 9: stringValue = "дев€тнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					default: stringValue = ""; break;
				}
			}
			else
			{
				switch (intNumerator / intDenominator)
				{
					case 1: stringValue = IsThousand ? "одна {0}а " : "один {0} "; break;
					case 2: stringValue = IsThousand ? "две {0}и " : "два {0}а "; break;
					case 3: stringValue = IsThousand ? "три {0}и " : "три {0}а "; break;
					case 4: stringValue = IsThousand ? "четыре {0}и " : "четыре {0}а "; break;
					case 5: stringValue = IsThousand ? "п€ть {0} " : "п€ть {0}ов "; break;
					case 6: stringValue = IsThousand ? "шесть {0} " : "шесть {0}ов "; break;
					case 7: stringValue = IsThousand ? "семь {0} " : "семь {0}ов "; break;
					case 8: stringValue = IsThousand ? "восемь {0} " : "восемь {0}ов "; break;
					case 9: stringValue = IsThousand ? "дев€ть {0} " : "дев€ть {0}ов "; break;
					default: stringValue = ""; break;
				}
			}

			return stringValue;
		}

		static string Decimals(int intNumerator, int intDenominator, bool IsThousand)
		{
			string stringValue;

			switch (intNumerator / intDenominator)
			{
				case 1: stringValue = IsThousand ? "дес€ть {0} " : "дес€ть {0}ов "; break;
				case 2: stringValue = IsThousand ? "двадцать {0} " : "двадцать {0}ов "; break;
				case 3: stringValue = IsThousand ? "тридцать {0} " : "тридцать {0}ов "; break;
				case 4: stringValue = IsThousand ? "сорок {0} " : "сорок {0}ов "; break;
				case 5: stringValue = IsThousand ? "п€тьдес€т {0} " : "п€тьдес€т {0}ов "; break;
				case 6: stringValue = IsThousand ? "шестьдес€т {0} " : "шестьдес€т {0}ов "; break;
				case 7: stringValue = IsThousand ? "семьдес€т {0} " : "семьдес€т {0}ов "; break;
				case 8: stringValue = IsThousand ? "восемьдес€т {0} " : "восемьдес€т {0}ов "; break;
				case 9: stringValue = IsThousand ? "дев€носто {0} " : "дев€носто {0}ов "; break;
				default: stringValue = ""; break;
			}

			return stringValue;
		}

		static string Hundreds(int intNumerator, int intDenominator, bool IsThousand)
		{
			string stringValue;

			switch (intNumerator / intDenominator)
			{
				case 1: stringValue = IsThousand ? "сто {0} " : "сто {0}ов "; break;
				case 2: stringValue = IsThousand ? "двести {0} " : "двести {0}ов "; break;
				case 3: stringValue = IsThousand ? "триста {0} " : "триста {0}ов "; break;
				case 4: stringValue = IsThousand ? "четыреста {0} " : "четыреста {0}ов "; break;
				case 5: stringValue = IsThousand ? "п€тьсот {0} " : "п€тьсот {0}ов "; break;
				case 6: stringValue = IsThousand ? "шестьсот {0} " : "шестьсот {0}ов "; break;
				case 7: stringValue = IsThousand ? "семьсот {0} " : "семьсот {0}ов "; break;
				case 8: stringValue = IsThousand ? "восемьсот {0} " : "восемьсот {0}ов "; break;
				case 9: stringValue = IsThousand ? "дев€тьсот {0} " : "дев€тьсот {0}ов "; break;
				default: stringValue = ""; break;
			}

			return stringValue;
		}

		public string PluginCode
		{
			get { return "ACT_DEDUCTION"; }
		}

		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
			ExtractReport();
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "јкты списани€: јкт списани€"; }
		}
	}

}
