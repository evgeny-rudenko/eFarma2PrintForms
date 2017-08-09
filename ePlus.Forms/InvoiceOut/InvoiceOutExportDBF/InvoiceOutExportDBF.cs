using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
//using Microsoft.Reporting.WinForms;
//using ePlus.Client.Core.Export2Sst;
using ePlus.CommonEx.AccessPoint;


using ePlus.MetaData.ExternReport;

namespace FCChInvoiceOutExportDBF
{
    public class InvoiceOutExportDBF : AbstractDocumentReport, IExternalDocumentPrintForm
    {
        const string CACHE_FOLDER = "Cache";
        string connectionString;
        string folderPath;

        void CreateStoredProc(string connectionString)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCChInvoiceOutExportDBF.InvoiceOutExportDBF.sql");

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
        public void Execute(string connectionString, string folderPath)
        {
            this.connectionString = connectionString;
            this.folderPath = folderPath;
            CreateStoredProc(this.connectionString);
        }

        public override IReportForm GetReportForm(DataRowItem dataRowItem)
        {
            using (InvoiceOutExportDBFForm ParamForm = new InvoiceOutExportDBFForm())
            {
                int ID_CONTRACTOR_TO = 0;
                DataService_BL bl = new DataService_BL();
                DataSet ds = bl.Execute(string.Format("Select ID_CONTRACTOR_TO from INVOICE_OUT where ID_INVOICE_OUT_GLOBAL='{0}'", dataRowItem.Guid));
                if (ds.Tables[0].Rows.Count > 0)
                    ID_CONTRACTOR_TO = Utils.GetInt(ds.Tables[0].Rows[0][0]);
                LoadSettings(ID_CONTRACTOR_TO, ParamForm);
                if (ParamForm.ShowDialog() == DialogResult.OK)
                {
                    AccessPointManager apm = new AccessPointManager(ParamForm.AccessPoint);
                    ExportDocument ED = new ExportDocument();
                    ED.ExportAndSave("DBO.USP_EXPORT_INVOICE_OUT_2_DBF", dataRowItem.Guid, apm.AsFile.AccessPoint.FILE_DIRECTORY + "\\");
                    //ED.Save(apm.AsFile.AccessPoint.FILE_DIRECTORY + "\\", Encoding.GetEncoding(ParamForm.EncodingSave));
                    SaveSettings(ID_CONTRACTOR_TO, ParamForm);
                }
            }
            return null;
        }

        private string SettingsFilePath
        {
            get
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
            }
        }

        private void LoadSettings(int iD_CONTRACTOR_TO, InvoiceOutExportDBFForm paramForm)
        {
            if (!File.Exists(SettingsFilePath))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsFilePath);
            XmlNode root = doc.SelectSingleNode("//XML");
            if (root == null)
                return;
            foreach (XmlNode nodeContractors in root)
            {
                if (iD_CONTRACTOR_TO == Utils.GetInt(nodeContractors, "ID_CONTRACTOR"))
                {
                    string ACCESS_POINT = Utils.GetString(nodeContractors, "ACCESS_POINT");
                    paramForm.SetAccessPoint(ACCESS_POINT);
                }
            }
        }

        private void SaveSettings(int iD_CONTRACTOR_TO, InvoiceOutExportDBFForm paramForm)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root;

            if (File.Exists(SettingsFilePath))
            {
                doc.Load(SettingsFilePath);
                root = doc.SelectSingleNode("//XML");
                //root.RemoveAll();
            }
            else
            {
                root = Utils.AddNode(doc, "XML");
            }
            foreach (XmlNode nodeContractors in root)
            {
                if (iD_CONTRACTOR_TO == Utils.GetInt(nodeContractors, "ID_CONTRACTOR"))
                {
                    root.RemoveChild(nodeContractors);
                    break;
                }
            }
            XmlNode nodeContractor = Utils.AddNode(root, "CONTRACTOR");
            Utils.AddNode(nodeContractor, "ID_CONTRACTOR", iD_CONTRACTOR_TO);
            Utils.AddNode(nodeContractor, "ACCESS_POINT", paramForm.AccessPoint);
            doc.Save(SettingsFilePath);
        }





        public  string PluginCode
        {
            get { return "INVOICE_OUT"; }
        }

        public  string ReportName
        {
            get { return "Ёкспорт в формате DBF"; }
        }

        public  string GroupName
        {
            get { return string.Empty; }
        }
    }
}
