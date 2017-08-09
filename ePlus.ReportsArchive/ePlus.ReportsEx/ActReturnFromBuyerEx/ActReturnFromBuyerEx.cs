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

namespace ActReturnFromBuyerEx
{
	public class ActReturnFromBuyerEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;

		void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActReturnFromBuyerEx.ActReturnFromBuyer.sql");

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
			Stream s = this.GetType().Assembly.GetManifestResourceStream("ActReturnFromBuyerEx.ActReturnFromBuyer.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ActReturnFromBuyer.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_ACT_RETURN_TO_BUYER", dataRowItem.Id);
			Utils.AddNode(root, "ID_ACT_RETURN_TO_BUYER_GLOBAL", dataRowItem.Guid);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;			
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ActReturnFromBuyer.rdlc");

			rep.LoadData("REPEX_ACT_RETURN_FROM_BUYER", doc.InnerXml);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table", 0);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table1", 1);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table2", 2);
						
			decimal sum = 0.0m;

			DataSet dataSet = rep.DataSource;
			for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
			{
				sum += AsDecimal(dataSet.Tables[1].Rows[i]["ABI_RETAILPRICEPERUNIT"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ABI_QUANTITY"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ABI_SCALINGMULTIPLY"]);
			}

			DataRow row = dataSet.Tables[0].Rows[0];

			ReportParameter[] parameters = new ReportParameter[8] {
				new ReportParameter("Chemistry", row["AB_CHEMISTRY"].ToString()),
				new ReportParameter("Store", row["AB_STORE"].ToString()),
				new ReportParameter("DocumentNumber", row["AB_NUMBER"].ToString()),
				new ReportParameter("DocumentDate", row["AB_DATE"].ToString()),
				new ReportParameter("ContractorNameFrom", row["AB_CONTRACTORFROM"].ToString()),
				new ReportParameter("ContractorNameTo", row["AB_CONTRACTORTO"].ToString()),
				new ReportParameter("BaseDocumentNumber", row["AB_NUMBERBASE"].ToString()),
				new ReportParameter("WritableSum", AsRusCurrency((double) sum))
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			return rep;
		}

		static decimal AsDecimal(object value)
		{
			return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
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
			else if (stringRub.EndsWith("два") || stringRub.EndsWith("три") || stringRub.EndsWith("четыре")) stringRub = stringRub + " рубля";
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
				stringTemp = "тысяч";
				intTemp = 1000;
				if (intResult > 0)
				{
					stringValue = stringValue.Replace("тысячи ", "");
					stringValue = stringValue.Replace("тысяча ", "");
					stringValue = stringValue.Replace("тысяч ", "");
				}
			}
			else
			{
				stringTemp = "";
				intTemp = 1;
			}
			switch (intDenominator / intTemp)
			{
				case 1: stringValue = stringValue + string.Format(Numerics(intValue, intDenominator, intTemp == 1000, stringValue.EndsWith("десять ")), stringTemp); if (!stringValue.EndsWith("десять ")) stringValue = stringValue.Replace("десять ", ""); break;
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
					case 5: stringValue = "пятнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 6: stringValue = "шестнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 7: stringValue = "семнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 8: stringValue = "восемнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
					case 9: stringValue = "девятнадцать" + (IsThousand ? " {0} " : " {0}ов "); break;
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
					case 5: stringValue = IsThousand ? "пять {0} " : "пять {0}ов "; break;
					case 6: stringValue = IsThousand ? "шесть {0} " : "шесть {0}ов "; break;
					case 7: stringValue = IsThousand ? "семь {0} " : "семь {0}ов "; break;
					case 8: stringValue = IsThousand ? "восемь {0} " : "восемь {0}ов "; break;
					case 9: stringValue = IsThousand ? "девять {0} " : "девять {0}ов "; break;
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
				case 1: stringValue = IsThousand ? "десять {0} " : "десять {0}ов "; break;
				case 2: stringValue = IsThousand ? "двадцать {0} " : "двадцать {0}ов "; break;
				case 3: stringValue = IsThousand ? "тридцать {0} " : "тридцать {0}ов "; break;
				case 4: stringValue = IsThousand ? "сорок {0} " : "сорок {0}ов "; break;
				case 5: stringValue = IsThousand ? "пятьдесят {0} " : "пятьдесят {0}ов "; break;
				case 6: stringValue = IsThousand ? "шестьдесят {0} " : "шестьдесят {0}ов "; break;
				case 7: stringValue = IsThousand ? "семьдесят {0} " : "семьдесят {0}ов "; break;
				case 8: stringValue = IsThousand ? "восемьдесят {0} " : "восемьдесят {0}ов "; break;
				case 9: stringValue = IsThousand ? "девяносто {0} " : "девяносто {0}ов "; break;
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
				case 5: stringValue = IsThousand ? "пятьсот {0} " : "пятьсот {0}ов "; break;
				case 6: stringValue = IsThousand ? "шестьсот {0} " : "шестьсот {0}ов "; break;
				case 7: stringValue = IsThousand ? "семьсот {0} " : "семьсот {0}ов "; break;
				case 8: stringValue = IsThousand ? "восемьсот {0} " : "восемьсот {0}ов "; break;
				case 9: stringValue = IsThousand ? "девятьсот {0} " : "девятьсот {0}ов "; break;
				default: stringValue = ""; break;
			}

			return stringValue;
		}

		public string PluginCode
		{
			get { return "ActReturnBuyer"; }
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
			get { return "Акты возврата от покупателя: Акт возврата от покупателя"; }
		}
	}

}
