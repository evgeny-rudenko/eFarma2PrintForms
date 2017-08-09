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

namespace ListReturnToContractorAPT76Ex
{
    public class ListReturnToContractorAPT76 : AbstractDocumentPrintForm
    {

        //const string CACHE_FOLDER = "Cache";
        //string connectionString;
        //string folderPath;

        //void CreateStoredProc(string connectionString)
        //{
        //    Stream s = this.GetType().Assembly.GetManifestResourceStream("ListReturnToContractorAPT76Ex.ListReturnToContractorAPT76.sql");

        //    using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
        //    {
        //        string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

        //        SqlCommand comm = null;
        //        foreach (string statement in batch)
        //        {
        //            if (statement == string.Empty)
        //                continue;

        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                comm = new SqlCommand(statement, con);
        //                con.Open();
        //                comm.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}

        //void ExtractReport()
        //{
        //    string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
        //    if (!Directory.Exists(cachePath))
        //        Directory.CreateDirectory(cachePath);
        //    Stream s = this.GetType().Assembly.GetManifestResourceStream("ListReturnToContractorAPT76Ex.ListReturnToContractorAPT76.rdlc");
        //    using (StreamReader sr = new StreamReader(s))
        //    {
        //        using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ListReturnToContractorAPT76.rdlc")))
        //        {
        //            sw.Write(sr.ReadToEnd());
        //        }
        //    }
        //}

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_ACT_RETURN_TO_CONTRACTOR", dataRowItem.Id);
            Utils.AddNode(root, "ID_ACT_RETURN_TO_CONTRACTOR_GLOBAL", dataRowItem.Guid);

            ReportFormNew rep = new ReportFormNew();

            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = reportFiles[0];//Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ListReturnToContractorAPT76.rdlc");

            rep.LoadData("REPEX_LIST_RETURN_TO_CONTRACTOR_APT76", doc.InnerXml);
            rep.BindDataSource("ListReturnToContractorAPT76Ex_DS_Table", 0);
            rep.BindDataSource("ListReturnToContractorAPT76Ex_DS_Table1", 1);
            rep.BindDataSource("ListReturnToContractorAPT76Ex_DS_Table2", 2);

            decimal dPm_SumSupp = 0.0m, dPm_SumPvatSupp = 0.0m,
                dPm_SumSal = 0.0m, dPm_SumPvatSal = 0.0m,
                dPm_SumDiscount = 0.0m, dPm_SumTaxSale = 0.0m;

            DataSet dataSet = rep.DataSource;
            for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
            {
                dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"] = AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"]) == 0.0M ? AsDecimal(dataSet.Tables[2].Rows[i]["ACI_PRICESUPP"]) * AsDecimal(dataSet.Tables[2].Rows[i]["ACI_QUANTITY"]) : dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"];
                dPm_SumSal += AsDecimal(dataSet.Tables[2].Rows[i]["ACI_SUMSUPP"]);
            }

            ReportParameter[] parameters = new ReportParameter[9] {
				new ReportParameter("Pm_Header", "�������� ���������"),
				new ReportParameter("Pm_SumSupp", dPm_SumSupp.ToString()),
				new ReportParameter("Pm_SumPvatSupp", dPm_SumPvatSupp.ToString()),
				new ReportParameter("Pm_SumSal", dPm_SumSal.ToString()),
				new ReportParameter("Pm_SumSalRusWords", AsRusCurrency((double)dPm_SumSal)),
				new ReportParameter("Pm_SumPvatSal", dPm_SumPvatSal.ToString()),
				new ReportParameter("Pm_SumDiscount", dPm_SumDiscount.ToString()),
				new ReportParameter("Pm_SumTaxSale", dPm_SumTaxSale.ToString()),
				new ReportParameter("Pm_CountRusWords", IntToRusWords(dataSet.Tables[2].Rows.Count))
			};

            rep.ReportViewer.LocalReport.SetParameters(parameters);

            return rep;
        }
        //public override IReportForm GetReportForm(DataRowItem dataRowItem)
        //{
        //}

        public override string PluginCode
        {
            get { return "ActReturnToContractor"; }
        }

        public override string ReportName
        {
            get { return "���� �������� ����������: �������� ��������� (��� 76)"; }
        }

        public override string GroupName
        {
            get { return string.Empty; }
        }

        decimal AsDecimal(object value)
        {
            return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
        }

        string AsString(object value)
        {
            return value != DBNull.Value ? Convert.ToString(value) : "";
        }

        string AsRusCurrency(double doubleValue)
        {
            string stringValue = doubleValue < 0 ? "����� " : "";
            double doubleTemp = Math.Round(Math.Abs(doubleValue), 3);
            int intRub = (int)Math.Floor(doubleTemp), intKop = (int)Math.Round(100 * (doubleTemp - intRub), 0);
            string stringRub = IntToRusWords(intRub), stringKop = IntToRusWords(intKop);

            if (stringRub.EndsWith("����")) stringRub = stringRub + " �����";
            else if (stringRub.EndsWith("���") || stringRub.EndsWith("���") || stringRub.EndsWith("������")) stringRub = stringRub + " �����";
            else stringRub = stringRub + " ������";

            stringKop = stringKop.Replace(" ����", " ����");
            stringKop = stringKop.Replace(" ���", " ���");
            if (stringKop.EndsWith("����")) stringKop = stringKop + " �������";
            else if (stringKop.EndsWith("���") || stringKop.EndsWith("���") || stringKop.EndsWith("������")) stringKop = stringKop + " �������";
            else stringKop = stringKop + " ������";

            stringValue = stringValue + stringRub + " " + stringKop;
            stringValue = stringValue.Trim();
            stringValue = char.ToUpper(stringValue[0]) + stringValue.Substring(1);
            return stringValue;
        }

        string IntToRusWords(int intValue)
        {
            string stringValue = intValue < 0 ? "����� " : "";
            int intTemp = Math.Abs(intValue), intDenominator = 1000000000;
            Validation(ref stringValue, ref intTemp, ref intDenominator);
            if (stringValue == "") stringValue = "����";
            return stringValue.Trim();
        }

        void Validation(ref string stringValue, ref int intValue, ref int intDenominator)
        {
            int intResult = intValue / intDenominator, intTemp;
            string stringTemp;
            if (intDenominator > 999999999)
            {
                stringTemp = "��������";
                intTemp = 1000000000;
                if (intResult > 0)
                {
                    stringValue = stringValue.Replace("���������� ", "");
                    stringValue = stringValue.Replace("��������� ", "");
                    stringValue = stringValue.Replace("�������� ", "");
                }
            }
            else if (intDenominator > 999999)
            {
                stringTemp = "�������";
                intTemp = 1000000;
                if (intResult > 0)
                {
                    stringValue = stringValue.Replace("��������� ", "");
                    stringValue = stringValue.Replace("�������� ", "");
                    stringValue = stringValue.Replace("������� ", "");
                }
            }
            else if (intDenominator > 999)
            {
                stringTemp = "�����";
                intTemp = 1000;
                if (intResult > 0)
                {
                    stringValue = stringValue.Replace("������ ", "");
                    stringValue = stringValue.Replace("������ ", "");
                    stringValue = stringValue.Replace("����� ", "");
                }
            }
            else
            {
                stringTemp = "";
                intTemp = 1;
            }
            switch (intDenominator / intTemp)
            {
                case 1: stringValue = stringValue + string.Format(Numerics(intValue, intDenominator, intTemp == 1000, stringValue.EndsWith("������ ")), stringTemp); if (!stringValue.EndsWith("������ ")) stringValue = stringValue.Replace("������ ", ""); break;
                case 10: stringValue = stringValue + string.Format(Decimals(intValue, intDenominator, intTemp == 1000), stringTemp); break;
                case 100: stringValue = stringValue + string.Format(Hundreds(intValue, intDenominator, intTemp == 1000), stringTemp); break;
                default: break;
            }
            if (intValue < 1000)
            {
                stringValue = stringValue.Replace(" �� ", " ");
                stringValue = stringValue.Replace(" � ", " ");
                stringValue = stringValue.Replace(" � ", " ");
            }

            if (intDenominator < 10) return;

            intValue = intValue - intDenominator * intResult;
            intDenominator = intDenominator / 10;
            Validation(ref stringValue, ref intValue, ref intDenominator);
        }

        string Numerics(int intNumerator, int intDenominator, bool IsThousand, bool IsTen)
        {
            string stringValue;

            if (IsTen)
            {
                switch (intNumerator / intDenominator)
                {
                    case 1: stringValue = "�����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 2: stringValue = "����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 3: stringValue = "����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 4: stringValue = "������������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 5: stringValue = "����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 6: stringValue = "�����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 7: stringValue = "����������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 8: stringValue = "������������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    case 9: stringValue = "������������" + (IsThousand ? " {0} " : " {0}�� "); break;
                    default: stringValue = ""; break;
                }
            }
            else
            {
                switch (intNumerator / intDenominator)
                {
                    case 1: stringValue = IsThousand ? "���� {0}� " : "���� {0} "; break;
                    case 2: stringValue = IsThousand ? "��� {0}� " : "��� {0}� "; break;
                    case 3: stringValue = IsThousand ? "��� {0}� " : "��� {0}� "; break;
                    case 4: stringValue = IsThousand ? "������ {0}� " : "������ {0}� "; break;
                    case 5: stringValue = IsThousand ? "���� {0} " : "���� {0}�� "; break;
                    case 6: stringValue = IsThousand ? "����� {0} " : "����� {0}�� "; break;
                    case 7: stringValue = IsThousand ? "���� {0} " : "���� {0}�� "; break;
                    case 8: stringValue = IsThousand ? "������ {0} " : "������ {0}�� "; break;
                    case 9: stringValue = IsThousand ? "������ {0} " : "������ {0}�� "; break;
                    default: stringValue = ""; break;
                }
            }

            return stringValue;
        }

        string Decimals(int intNumerator, int intDenominator, bool IsThousand)
        {
            string stringValue;

            switch (intNumerator / intDenominator)
            {
                case 1: stringValue = IsThousand ? "������ {0} " : "������ {0}�� "; break;
                case 2: stringValue = IsThousand ? "�������� {0} " : "�������� {0}�� "; break;
                case 3: stringValue = IsThousand ? "�������� {0} " : "�������� {0}�� "; break;
                case 4: stringValue = IsThousand ? "����� {0} " : "����� {0}�� "; break;
                case 5: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                case 6: stringValue = IsThousand ? "���������� {0} " : "���������� {0}�� "; break;
                case 7: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                case 8: stringValue = IsThousand ? "����������� {0} " : "����������� {0}�� "; break;
                case 9: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                default: stringValue = ""; break;
            }

            return stringValue;
        }

        string Hundreds(int intNumerator, int intDenominator, bool IsThousand)
        {
            string stringValue;

            switch (intNumerator / intDenominator)
            {
                case 1: stringValue = IsThousand ? "��� {0} " : "��� {0}�� "; break;
                case 2: stringValue = IsThousand ? "������ {0} " : "������ {0}�� "; break;
                case 3: stringValue = IsThousand ? "������ {0} " : "������ {0}�� "; break;
                case 4: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                case 5: stringValue = IsThousand ? "������� {0} " : "������� {0}�� "; break;
                case 6: stringValue = IsThousand ? "�������� {0} " : "�������� {0}�� "; break;
                case 7: stringValue = IsThousand ? "������� {0} " : "������� {0}�� "; break;
                case 8: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                case 9: stringValue = IsThousand ? "��������� {0} " : "��������� {0}�� "; break;
                default: stringValue = ""; break;
            }

            return stringValue;
        }

        //public string PluginCode
        //{
        //    get { return "ActReturnToContractor"; }
        //}

        //public void Execute(string connectionString, string folderPath)
        //{
        //    this.connectionString = connectionString;
        //    this.folderPath = folderPath;
        //    CreateStoredProc(this.connectionString);
        //    ExtractReport();
        //}

        //public string GroupName
        //{
        //    get { return string.Empty; }
        //}

        //public string ReportName
        //{
        //    get { return "���� �������� ����������: �������� ��������� (��� 76)"; }
        //}
    }
}
