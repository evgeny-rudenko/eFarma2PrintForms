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
using System.Windows.Forms;

namespace FCChInvoiceOutInvoice_KalugaF
{
    public class ActReturnToContractorInvoice : AbstractDocumentPrintForm
  {
        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");

            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);

            ReportFormNew rep = new ReportFormNew();

            rep.Text = rep.ReportFormName = ReportName;
            if (GetDateDocument(dataRowItem.Guid) < Convert.ToDateTime("24.01.2012"))
                rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutInvoiceKalugaF.rdlc");
            else
                rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InvoiceOutInvoiceKalugaFAfter240112.rdlc");
           

            rep.LoadData("DBO.REPEX_INVOICE_OUT_INVOICE_KALUGA_F", doc.InnerXml);
            rep.BindDataSource("INVOICE_OUT_INVOICE_KALUGA_F_DS_Table0", 0);
            rep.BindDataSource("INVOICE_OUT_INVOICE_KALUGA_F_DS_Table1", 1);
            rep.BindDataSource("INVOICE_OUT_INVOICE_KALUGA_F_DS_Table2", 2);

            string number;
            using (InvoiceOutInvoiceKalugaFForm paramForm = new InvoiceOutInvoiceKalugaFForm())
            {
                string num = Utils.GetString(dataRowItem.Row, "DOC_NUM");

                number = paramForm.Number = !string.IsNullOrEmpty(num) ? num : Utils.GetString(dataRowItem.Row, "MNEMOCODE");

                if (paramForm.ShowDialog() == DialogResult.OK)
                    number = paramForm.Number;
            }
            List<ReportParameter> par = new List<ReportParameter>();
            par.Add(new ReportParameter("INVOICE_NAME", number + " от " + Utils.GetDate(rep.DataSource.Tables[0].Rows[0], "INVOICE_OUT_DATE").ToString("D")));
            par.Add(new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName));
            rep.ReportViewer.LocalReport.SetParameters(par);

            return rep;
        }

        public override string PluginCode
        {
            get { return "INVOICE_OUT"; }
        }
        public override string GroupName
        {
            get { return string.Empty; }
        }

        public override string ReportName
        {
            get { return "Счет-фактура (Калугафармация)"; }
        }
        private DateTime GetDateDocument(Guid guid)
        {
            DateTime Result = DateTime.Now;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT DOC_DATE FROM INVOICE_OUT WHERE ID_INVOICE_OUT_GLOBAL = '{0}'", guid), con);
                command.CommandTimeout = 60 * 60;
                con.Open();
                try
                {
                    Result = Convert.ToDateTime(command.ExecuteScalar());
                }
                catch
                { }
            }
            return Result;
        }
  }
}
