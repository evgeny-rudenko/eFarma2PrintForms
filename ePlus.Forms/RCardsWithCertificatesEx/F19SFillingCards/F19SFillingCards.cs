using System;
using System.Collections.Generic;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace F19SFillingCards
{
    public class RCardsWithCertificatesEx : AbstractDocumentPrintForm, IExternalDocumentPrintForm
    {
        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string PluginCode
        {
            get { return "INVOICE"; }
        }
        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F19SFillingCards.F19SFillingCards.sql");

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
            Stream s = this.GetType().Assembly.GetManifestResourceStream("F19SFillingCards.F19SFillingCards.rdlc");
            using (StreamReader sr = new StreamReader(s))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "F19SFillingCards.rdlc")))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVOICE", dataRowItem.Guid);
            ReportFormNew rep = new ReportFormNew();
            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "F19SFillingCards.rdlc");
            rep.LoadData("REP_RACKCARDS_EX", doc.InnerXml);
            rep.BindDataSource("RCardsWithCertificatesEx_DS_Table0", 0);
            ReportParameter[] parameters = new ReportParameter[1] {
                new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
			};
            rep.ReportViewer.LocalReport.SetParameters(parameters);
            return rep;
            /*

            DataSet ds = new DataSet();
            using (SqlDataAdapter sqlda = new SqlDataAdapter("REP_RACKCARDS_EX", connectionString))
            {
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                sqlda.Fill(ds);
            }

            // добавление колонки с порядковым номером строки
            ds.Tables[0].Columns.Add("NUM", typeof(int));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                ds.Tables[0].Rows[i]["NUM"] = i + 1;

            //#warning Используется для отладки, чтобы получить последнюю версию DataSet
            //ds.WriteXml(@"c:\Cards.xml", XmlWriteMode.WriteSchema); 
            RCardsWithCertificatesEx1 cards = new RCardsWithCertificatesEx1();
            ReportFormCrystal reportForm = new ReportFormCrystal();
            reportForm.SetDataSource(ReportName, ds, cards);
            return reportForm;
             * */
        }
        public override void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
            ExtractReport();
        }
        public override string ReportName
        {
            get { return "Фасовочная карточка"; }
        }
    }
}
