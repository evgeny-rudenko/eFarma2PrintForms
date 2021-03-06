﻿using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace InventoryVedInostCompare
{
    public partial class InventoryVedInostCompare : ExternalReportForm, IExternalReportFormMethods
    {
        private string fileXml = Path.Combine(Utils.TempDir(), "InventoryVedInostCompareSettings.xml");

        public InventoryVedInostCompare()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            try
            {
                if (!chbQuant.Checked && !chbGrup.Checked)
                {
                    throw new Exception("Не выбран тип отчета");
                }
                XmlDocument doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML", null);
                DataRowCollection rows = LoadDbf().Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    XmlNode note = Utils.AddNode(root, "ROW", null);
                    Utils.AddNode(note, "STU", Utils.GetString(row, "STU"));
                    Utils.AddNode(note, "QUANT", Utils.GetDecimal(row, "QUANT"));
                    Utils.AddNode(note, "PRICE_PT", Utils.GetDecimal(row, "PRICE_PT"));
                    Utils.AddNode(note, "PRICE_RS", Utils.GetDecimal(row, "PRICE_RS"));
                    Utils.AddNode(note, "NAME", Utils.GetString(row, "NAME"));
                }
                foreach (DataRowItem dri in ucInventoryVed.Items)
                    Utils.AddNode(root, "ID_INVENTORY_VED_GLOBAL", dri.Guid);

                if (chbQuant.Checked) Print(reportFiles[0], doc, root, "QUANT");
                if (chbGrup.Checked) Print(reportFiles[0], doc, root, "GRUP");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Print(string file, XmlDocument doc, XmlNode root, string type)
        {
            XmlNode rootT = root.SelectSingleNode("TYPE");
            if (rootT == null)
            {
                Utils.AddNode(root, "TYPE", type);
            }
            else
            {
                rootT.InnerText = type;
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = file;
            rep.LoadData("REPEX_INVENTORY_VED_INOST_COMPARE", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.BindDataSource("InventoryVedInostCompare_DS_Table", 0);
            rep.AddParameter("INV", ucInventoryVed.TextValues());
            rep.AddParameter("NAME", type == "QUANT" ? "Сверка  ведомости СТУ и результатов пересчета в еФ2(количество и цены)" : "Сверка ведомости СТУ и результатов пересчета в еФ2(с группировкой по ценам)");
            rep.ExecuteReport(this);
        }

        private DataSet LoadDbf()
        {
            string fileName = textFileName.Text;
            if (string.IsNullOrEmpty(textFileName.Text))
            {
                btSelectFile.Focus();
                throw new Exception("Не выбран файл СТУ");
            }

            FileInfo file = new FileInfo(fileName);
            file.Attributes = FileAttributes.Normal;
            string dbfPath = file.DirectoryName;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(29, SeekOrigin.Begin);
                fs.WriteByte(101);
            }

            DataSet ds = new DataSet();
            string dbfConnectionString = string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Share Deny None;Extended Properties=\"\";User ID=\"\";Mask Password=False;Cache Authentication=False;Encrypt Password=False;Collating Sequence=MACHINE;DSN=\"\"", dbfPath);
            string dbfTableName = file.Name.Split(char.Parse("."))[0];
            using (OleDbConnection con = new OleDbConnection(dbfConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM " + dbfTableName, con))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 3600;
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public string ReportName
        {
            get { return "Сравнение данных прежней СТУ и еФарма2"; }
        }

        private void btSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "INOST.dbf|INOST.dbf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textFileName.Text = openFileDialog.FileName;
            }
        }

        private void InventoryVedInostCompare_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileXml)) return;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileXml);
            XmlNode root = doc.SelectSingleNode("XML");
            textFileName.Text = Utils.GetString(root, "DIR_FILE");
            XmlNodeList listNodes = root.SelectNodes("LIST/INVENTORY_VED");
            foreach (XmlNode node in listNodes)
            {
                DataRowItem dri = new DataRowItem(0, Utils.GetGuid(node, "ID_INVENTORY_VED_GLOBAL"), string.Empty, Utils.GetString(node, "DOC_NAME"));
                ucInventoryVed.Items.Add(dri);
            }
        }

        private void InventoryVedInostCompare_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DIR_FILE", textFileName.Text);
            XmlNode listNode = Utils.AddNode(root, "LIST");
            foreach (DataRowItem dri in ucInventoryVed.Items)
            {
                XmlNode driNode = Utils.AddNode(listNode, "INVENTORY_VED");
                Utils.AddNode(driNode, "ID_INVENTORY_VED_GLOBAL", dri.Guid);
                Utils.AddNode(driNode, "DOC_NAME", dri.Text);
            }
            doc.Save(fileXml);
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }
    }
}