using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
//using ePlus.MetaData.Client.Reports;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using Microsoft.Reporting.WinForms;

namespace FCSReturnFromBuyerWithNDS
{
    public class ReturnFromBuyer : AbstractDocumentReport, IExternalDocumentPrintForm
    {
        //protected override string ToXml(Guid value) { return ToXml("ID_ACT_RETURN_TO_BUYER_GLOBAL", value); }
        //protected override string ToXml(long value) { return ToXml("ID_ACT_RETURN_TO_BUYER", value); }

        //protected override void Validate(DataSet dataSet)
        //{
        //    ReportFormName = "Отчёт: \"Акт возврата от покупателя\"";

        //    decimal fWritableSum = 0.0M;
        //    for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
        //    {
        //        fWritableSum += AsDecimal(dataSet.Tables[1].Rows[i]["ABI_RETAILPRICEPERUNIT"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ABI_QUANTITY"]) * AsDecimal(dataSet.Tables[1].Rows[i]["ABI_SCALINGMULTIPLY"]);
        //    }

        //    AddReportParameter("Chemistry", dataSet.Tables[0].Rows[0]["AB_CHEMISTRY"]);
        //    AddReportParameter("Store", dataSet.Tables[0].Rows[0]["AB_STORE"]);
        //    AddReportParameter("DocumentNumber", dataSet.Tables[0].Rows[0]["AB_NUMBER"]);
        //    AddReportParameter("DocumentDate", dataSet.Tables[0].Rows[0]["AB_DATE"]);
        //    AddReportParameter("ContractorNameFrom", dataSet.Tables[0].Rows[0]["AB_CONTRACTORFROM"]);
        //    AddReportParameter("ContractorNameTo", dataSet.Tables[0].Rows[0]["AB_CONTRACTORTO"]);
        //    AddReportParameter("BaseDocumentNumber", dataSet.Tables[0].Rows[0]["AB_NUMBERBASE"]);
        //    AddReportParameter("WritableSum", NumWords.DoubleToRusCurrency((double)fWritableSum));
        //}

        private const string CACHE_FOLDER = "Cache";
        string connectionString;
        string folderPath;

        private void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSReturnFromBuyerWithNDS.Act_ReturnFromBuyer_WithNDS.sql");
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
            string procScript = sr.ReadToEnd();
            string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);

            SqlCommand comm = null;
            foreach (string statement in batch)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    comm = new SqlCommand(statement, con);
                    con.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        private void ExtractReport()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSReturnFromBuyerWithNDS.ReturnFromBuyerWithNDS.rdlc");
            StreamReader sr = new StreamReader(s);
            string rep = sr.ReadToEnd();
            string reportPath = Path.Combine(cachePath, "ReturnFromBuyerWithNDS.rdlc");
            using (StreamWriter sw = new StreamWriter(reportPath))
            {
                sw.Write(rep);
                sw.Flush();
                sw.Close();
            }
        }

        private void ClearCache()
        {
            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            if (Directory.Exists(cachePath))
            {
                try
                {
                    Utils.ClearFolder(cachePath);
                    Directory.Delete(cachePath);
                }
                catch
                {

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

			rep.LoadData("REP_ACT_RETURNFROMBUYER_WITH_NDS", doc.InnerXml);
            rep.BindDataSource("Act_ReturnFromBuyer_DS_Table", 0);
            rep.BindDataSource("Act_ReturnFromBuyer_DS_Table1", 1);
			rep.BindDataSource("Act_ReturnFromBuyer_DS_Table2", 2);
            //rep.BindDataSource("Invoice_Table2", 2);

            string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
            // подсчет общей суммы
            //decimal dblSummory = 0;
            //decimal retailSummory = 0;
            //foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
            //{
            //    dblSummory += Utils.GetDecimal(Row, "CONTRACTOR_SUM_VAT");
            //    retailSummory += Utils.GetDecimal(Row, "SUM_RETAIL_SUM");
            //}
            //dblSummory = Utils.Round(dblSummory, 2);
            //retailSummory = Utils.Round(retailSummory, 2);
            //// преобразование суммы в строку
            //string strSummory = RusCurrency.Str((double)dblSummory);
            //string strRetailSummory = RusCurrency.Str((double)retailSummory);

            decimal fWritableSum = 0.0M;
            foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
            {
                fWritableSum += Utils.GetDecimal(Row, "ABI_RETAILPRICEPERUNIT") * Utils.GetDecimal(Row, "ABI_QUANTITY");
            }
            string file = Path.Combine(cachePath, "ReturnFromBuyerWithNDS.rdlc");
            rep.ReportViewer.LocalReport.ReportPath = file;

            ReportParameter p1 = new ReportParameter("Chemistry", Utils.GetString(rep.DataSource.Tables[0].Rows[0],"AB_CHEMISTRY"));
            ReportParameter p2 = new ReportParameter("Store", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_STORE"));
            ReportParameter p3 = new ReportParameter("DocumentNumber", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_NUMBER"));
            ReportParameter p4 = new ReportParameter("DocumentDate", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_DATE"));
            ReportParameter p5 = new ReportParameter("ContractorNameFrom", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_CONTRACTORFROM"));
            ReportParameter p6 = new ReportParameter("ContractorNameTo", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_CONTRACTORTO"));
            ReportParameter p7 = new ReportParameter("BaseDocumentNumber", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_NUMBERBASE"));
            ReportParameter p8 = new ReportParameter("WritableSum", RusCurrency.Str((double)fWritableSum));
            ReportParameter p9 = new ReportParameter("AddressContractor", Utils.GetString(rep.DataSource.Tables[0].Rows[0], "AB_ADDRESSCONTRACTOR"));
            ReportParameter p10 = new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 });
            return rep;
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

        public string ReportName
        {
			get { return "Акты возврата от покупателя: Возвратная накладная от покупателя (c НДС)"; }
        }

        public string GroupName
        {
            get { return string.Empty; }
        }

    }
}