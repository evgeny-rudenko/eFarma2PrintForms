using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using System.Data.SqlClient;

namespace ImportRemainsOpisEx
{
    public class ImportRemainsOpisEx : AbstractDocumentPrintForm
    {

        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string PluginCode
        {
            get { return "IMPORT_REMAINS"; }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_IMPORT_REMAINS_GLOBAL", dataRowItem.Guid);

            DataSet ds = new DataSet();
            using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_IMPORT_REMAINS_OPIS_EX", this.connectionString))
            {
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                sqlda.Fill(ds);
            }
            ImportRemainsOpisRep report = new ImportRemainsOpisRep();
            ReportFormCrystal reportForm = new ReportFormCrystal();
            reportForm.SetDataSource(ReportName,ds,report);            
            return reportForm;          
        }

        public override string ReportName
        {
            get { return "Инвентаризационная опись ТМЦ Ригла"; }
        }
    }
}
