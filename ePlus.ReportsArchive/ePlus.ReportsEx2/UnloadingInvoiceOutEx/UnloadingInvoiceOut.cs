using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using System.Xml;
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using ePlus.CommonEx.AccessPoint;
using System.Windows.Forms;

namespace UnloadingInvoiceOutEx
{
    public class UnloadingInvoiceOut : AbstractDocumentPrintForm   
    {
        private string Id;
        private string Ftp;
        private string fileName;
        private bool isShow;

        public string FTP
        {
            get { return Ftp; }
            set { Ftp = value; }
        }

        public string ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        
        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string PluginCode
        {
            get { return "INVOICE_OUT"; }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            this.FileName = dataRowItem.Text.Replace("/", "");
            FormParam formParam = new FormParam(this);
            formParam.ShowDialog();
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_INVOICE_OUT_GLOBAL", dataRowItem.Guid);

            DataService_BL bl = new DataService_BL();
            AccessPointManager apm = null;
            try
            {
                apm = new AccessPointManager(FTP);
            }
            catch
            {
                throw new Exception(string.Format("Не удалось найти или загрузить точку доступа [{0}] для экспорта данных", FTP == string.Empty ? "<Не задано>" : FTP));
            }

            DataSet ds = new DataSet();
            using (SqlDataAdapter sqlda = new SqlDataAdapter("REPEX_UNLOADING_INVOICE_OUT", bl.ConnectionString))
            {
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", doc.InnerXml));
                sqlda.Fill(ds);
            }
                       
            string tempDir = Utils.TempDir();
            string fn = string.Format("{0}.txt", FileName);
            using (StreamWriter sw1 = new StreamWriter(Path.Combine(tempDir, fn), false, Encoding.GetEncoding(1251)))
            {
               DataRow dr1 = ds.Tables[0].Rows[0];             
               sw1.WriteLine(string.Format("{0}\t{1}\t{2}", ID, "RUR", Utils.GetString(dr1, "DOC_NUM")));
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   sw1.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                       Utils.GetString(dr, "ID_GOODS"),
                       Utils.GetString(dr, "GOODS_NAME"),
                       Utils.GetInt(dr, "QUANTITY"),
                       Utils.GetDecimal(dr, "PRICE_SAL").ToString("F"),
                       Utils.GetString(dr, "VAT_SAL"),
                       Utils.GetDecimal(dr, "SUM_SAL").ToString("F"),
                       Utils.GetString(dr, "GTD_NUMBER"),
                       Utils.GetString(dr, "COUNTRY_NAME")));
               }

            }
            apm.Send(Path.Combine(tempDir, fn), fn);
            MessageBox.Show(string.Format("Выгрузка в файл {0} завершена в {1}", fn, tempDir));
            return null;
        }

        public override string ReportName
        {
            get { return "Выгрузка электронных накладных (Катрен)"; }
        }
    }
}
