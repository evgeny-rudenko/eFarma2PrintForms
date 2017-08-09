using System;
using System.Collections.Generic;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace InventoryOpisEx
{
    public class InventoryOpisEx : AbstractDocumentReport, IExternalDocumentPrintForm //AbstractDocumentPrintForm
    {
        string connectionString;
        string folderPath;

        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("InventoryOpisEx.INVENTORY_OPIS_EX.sql");

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

        public override IReportForm GetReportForm(DataRowItem dataRowItem)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVENTORY_GLOBAL", dataRowItem.Guid);

            DataSet ds = new DataSet();
            using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_INVENTORY_OPIS_EX", connectionString))
            {
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                sqlda.Fill(ds);
            }

            //decimal sum = 0;
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    sum += Utils.GetDecimal(row, "SUM_CONTRACTOR_PRICE_VAT");
            //}

            //string sumInText = RusCurrency.Str((double)sum);
            //string countInText = RusCurrency.Str(ds.Tables[0].Rows.Count, "NUM");

            //// параметры
            //AddParameter(ds, typeof(double), "summory", sum);
            //AddParameter(ds, typeof(string), "str_summory", sumInText);
            //AddParameter(ds, typeof(string), "count_rows", countInText);

            InventoryOpisRep report = new InventoryOpisRep();
            ReportFormCrystal reportForm = new ReportFormCrystal();
            reportForm.SetDataSource(ReportName, ds, report);
            return reportForm;
        }

        public string PluginCode
        {
            get { return "INVENTORY_SVED"; }
        }

        public void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
        }

        public string GroupName
        {
            get { return string.Empty; }
        }

        public string ReportName
        {
            get { return "Инвентаризационная опись ТМЦ Ригла"; }
        }
        //public override string GroupName
        //{
        //    get { return string.Empty; }
        //}

        //public override string PluginCode
        //{
        //    get { return "INVENTORY_SVED"; }
        //}

        //protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    XmlNode root = Utils.AddNode(doc, "XML");
        //    Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

        //    DataSet ds = new DataSet();
        //    using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_INVENTORY_OPIS_EX", connectionString))
        //    {
        //        sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
        //        sqlda.Fill(ds);
        //    };
        //    //ds.WriteXml(@"c:\inv.xml", XmlWriteMode.WriteSchema);
        //    InventoryOpisRep report = new InventoryOpisRep();
        //    ReportFormCrystal reportForm = new ReportFormCrystal();
        //    reportForm.SetDataSource(ReportName, ds, report);
        //    return reportForm;
            
        //}

        //public override string ReportName
        //{
        //    get { return "Инвентаризационная опись ТМЦ Ригла"; }
        //}
    }
}
