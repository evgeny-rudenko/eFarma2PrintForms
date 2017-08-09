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

namespace RCardsWithCertificatesEx
{
    public class RCardsWithCertificatesEx : AbstractDocumentPrintForm
    {
        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string PluginCode
        {
            get { return "INVOICE"; }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVOICE", dataRowItem.Guid);

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
        }

        public override string ReportName
        {
            get { return "Стеллажная карточка (сертифик)"; }
        }
    }
}
