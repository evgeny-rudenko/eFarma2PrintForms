using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using ePlus.CommonEx.Controls;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;

namespace InvoiceExportXml
{
    public partial class InoiceExportForm : ExternalReportForm, IExternalReportFormMethods
    {
        public InoiceExportForm()
        {
            InitializeComponent();
            Load += new EventHandler(InoiceExportForm_Load);
            FormClosed += new FormClosedEventHandler(InoiceExportForm_FormClosed);
        }

        private void InoiceExportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.RegistrySave("REPEX_INVOICE_EXPORT_AP", ucAccessPoint.Id.ToString());
        }

        private void InoiceExportForm_Load(object sender, EventArgs e)
        {
            long id_ap = Utils.GetLong(Utils.RegistryLoad("REPEX_INVOICE_EXPORT_AP"));
            if (id_ap != 0)
            {
                ACCESS_POINT ap = (new ACCESS_POINT_BL()).Load(id_ap);
                ucAccessPoint.Id = ap.ID_ACCESS_POINT;
                ucAccessPoint.Code = ap.MNEMOCODE;
            }
        }

        public void Print(string[] reportFiles)
        {
            try
            {
                Guid id = ucInvoice.Guid;
                if (string.IsNullOrEmpty(ucAccessPoint.Code))
                {
                    ActiveControl = ucAccessPoint;
                    throw new Exception("Не заполнено куда");
                }

                Stream s = GetType().Assembly.GetManifestResourceStream("InvoiceExportXml.INVOICE_EXPORT.SQL");
                StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
                string procScript = sr.ReadToEnd();
                using (ProgressDialog pd = new ProgressDialog())
                {
                    pd.Cursor = Cursors.WaitCursor;
                    pd.Init("Идёт импорт...", false);
                    pd.DoWork += delegate
                                     {
                                         DataSet ds = Exec(procScript, id);
                                         if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count == 0)
                                             throw new Exception("Нет данных");
                                         CreateXml(ds);
                                     };
                    pd.ShowDialog();
                    if (pd.Error == null)
                        MessageBox.Show("Импорт завершён успешно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataSet Exec(string procScript, Guid id)
        {
            DataService_BL dataService = new DataService_BL(connectionString);
            if (string.IsNullOrEmpty(procScript)) return null;
            string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);
            foreach (string statement in batch)
            {
                dataService.Execute(statement);
            }

            XmlDocument doc = new XmlDocument();
            XmlNode node = Utils.AddNode(doc, "XML");
            if (id != Guid.Empty) Utils.AddNode(node, "ID_GLOBAL", id);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommandEx cmd = new SqlCommandEx("REPEX_INVOICE_EXPORT", conn))
                {
                    cmd.AddParameterIn("@XMLPARAM", SqlDbType.NText, doc.InnerXml);
                    SqlDataAdapter da = new SqlDataAdapter(cmd.SqlCommand);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    conn.Open();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        private void CreateXml(DataSet ds)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "windows-1251", "yes"));
            XmlNode nodeDoc = Utils.AddNode(doc, "PACKET");
            nodeDoc.Attributes.Append(doc.CreateAttribute("TYPE"));
            nodeDoc.Attributes["TYPE"].Value = "12";
            nodeDoc.Attributes.Append(doc.CreateAttribute("ID"));
            //По ТЗ
            //nodeDoc.Attributes["ID"].Value = "1/12";
            //Индивидуально для какого-то клиента(((
            nodeDoc.Attributes["ID"].Value = Utils.GetString(ds.Tables[0].Rows[0], "INVOICE_NUM");
            nodeDoc.Attributes.Append(doc.CreateAttribute("NAME"));
            nodeDoc.Attributes["NAME"].Value = "Электронная накладная";
            nodeDoc.Attributes.Append(doc.CreateAttribute("FROM"));
            nodeDoc.Attributes["FROM"].Value = "АХ";

            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    XmlNode node = AddNode(nodeDoc, "SUPPLY", ds.Tables[0], row);
            //    Guid id = Utils.GetGuid(row, "SYSTEM_ID");
            //    XmlNode nodeItem = Utils.AddNode(node, "ITEMS");
            //    foreach (DataRow rowItem in ds.Tables[1].Rows)
            //    {
            //        Guid idItem = Utils.GetGuid(rowItem, "SYSTEM_ID");
            //        if (idItem != id) continue;
            //        AddNode(nodeItem, "ITEM", ds.Tables[1], rowItem);
            //    }
            //}

            XmlNode node = AddNode(nodeDoc, "SUPPLY", ds.Tables[0], ds.Tables[0].Rows[0]);
            XmlNode nodeItem = Utils.AddNode(node, "ITEMS");
            foreach (DataRow rowItem in ds.Tables[1].Rows)
            {
                AddNode(nodeItem, "ITEM", ds.Tables[1], rowItem);
            }
            string fileName = Path.Combine(Utils.TempDir(), "Invoice_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xml");
            doc.Save(fileName);
            AccessPointManager apm = new AccessPointManager(ucAccessPoint.Code);
            apm.Send(fileName, Path.GetFileName(fileName));
        }

        private XmlNode AddNode(XmlNode nodeDoc, string nameNode, DataTable table, DataRow row)
        {
            XmlNode node = Utils.AddNode(nodeDoc, nameNode);
            foreach (DataColumn column in table.Columns)
            {
                string name = column.ColumnName;
                if (name.IndexOf("SYSTEM_") >= 0) continue;
                Utils.AddNode(node, name, Utils.GetString(row, name));
            }
            return node;
        }
        
        public string ReportName
        {
            get { return "Экспорт"; }
        }
    }
}