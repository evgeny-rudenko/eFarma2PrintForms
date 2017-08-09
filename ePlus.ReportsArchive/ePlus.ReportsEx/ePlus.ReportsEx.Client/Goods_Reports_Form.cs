using System;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;  
using ePlus.Client.Core.OptionsEx;
using ePlus.Dictionary.Server;
using ePlus.Dictionary.BusinessObjects;
using ePlus.Common;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using FastReport;

namespace ePlus.ReportsEx
{
    public partial class Goods_Reports_Form : ReportBaseForm
    {
        #region Fields

        CONTRACTOR contractor = null;
        REPORTS_BL repBl = new REPORTS_BL();
        TfrxReportClass report = new TfrxReportClass();
        DataService_BL dataService = new DataService_BL();
        readonly string GOODS_REPORT_NUM = "GOODS_REPORT_NUM";

        #endregion

        #region Initialize

        public Goods_Reports_Form()
        {
            InitializeComponent();

            string lastGoodsReportType = Utils.RegistryLoad("GOODS_REPORT_TYPE");
            comboBox1.SelectedIndex = Utils.GetInt(lastGoodsReportType);            
  
            valDocNumber.Value = ClientConfig.GetInt(GOODS_REPORT_NUM);
            CONTRACTOR_BL blContractor = (CONTRACTOR_BL)BLProvider.Instance.GetBL(typeof(CONTRACTOR_BL));
            contractor = blContractor.GetSelfContractor();
            if (contractor != null)
            {
                selectContractorStoreControl1.add_contractor(contractor.ID_CONTRACTOR);
            }
            comboBox1.Enabled = false;
            comboBox1.SelectedIndex = 4;
        }

        protected override void ClearValues()
        {
            base.ClearValues();
            if (contractor != null)
            {
                selectContractorStoreControl1.add_contractor(contractor.ID_CONTRACTOR);
            }
        }

        #endregion

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox_Add_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_Add.Checked)
            {
                if (!checkBox_Sub.Checked) checkBox_Sub.Checked = true;
            }
        }

        private void checkBox_Sub_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_Sub.Checked)
            {
                if (!checkBox_Add.Checked) checkBox_Add.Checked = true;
            }
        }

        private void Goods_Reports_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.RegistrySave("GOODS_REPORT_TYPE", comboBox1.SelectedIndex.ToString());
        }

        private void GoodsReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientConfig.Save(GOODS_REPORT_NUM, (valDocNumber.Value+1).ToString());
        }

        #endregion

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!ValidControls()) return;
            report.ClearReport();
            report.ClearDatasets();
            report.LoadReportFromFile(Utils.AppDir("GoodsReport.fr3"));

            DataSet ds = GetData();
            BindDataSource("Table1", "Data1", ds.Tables[1]);
            BindDataSource("Table2", "Data2", ds.Tables[2]);

            decimal sum = Utils.GetDecimal(ds.Tables[0].Rows[0]["PRICE_BEGIN"]);
            decimal sumRet = Utils.GetDecimal(ds.Tables[0].Rows[0]["PRICE_BEGIN_RETAIL"]);
            SetReportParameter("MemoSum", sum.ToString("#################0.00"));
            SetReportParameter("MemoSumRet", sumRet.ToString("#################0.00"));
            SetReportParameter("MemoDiff", (sumRet - sum).ToString("#################0.00"));
            SetReportParameter("MemoCompany", Utils.GetString(ds.Tables[3].Rows[0]["CONTRACTORS"]));
            SetReportParameter("MemoStore", Utils.GetString(ds.Tables[3].Rows[0]["STORES"]));
            SetReportParameter("MemoNum", valDocNumber.Value.ToString());
            SetReportParameter("MemoDate", DateTime.Now.ToString("dd.MM.yyyy"));
            SetReportParameter("MemoFrom", ucPeriod1.DateFrText);
            SetReportParameter("MemoTo", ucPeriod1.DateToText);
            SetReportParameter("MemoRemDate", "Остаток на " + ucPeriod1.DateFrText);
            SetReportParameter("MemoRemDateTo", "Остаток на " + ucPeriod1.DateToText);
            SetReportParameter("MemoDetail", checkBox_Detail.Checked ? "1" : "0");
            SetReportParameter("MemoShowAdd", checkBox_Add.Checked ? "1" : "0");
            SetReportParameter("MemoShowSub", checkBox_Sub.Checked ? "1" : "0");

            //report.PreviewOptions.MDIChild = true;
            report.MainWindowHandle = (int)Handle;
            report.ShowReport();
        }

        private bool ValidControls()
        {
            if (valDocNumber.Value < 0)
            {
                MessageBox.Show("Номер документа д.б. положительным", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valDocNumber.Focus();
                return false;
            }
            if (ucPeriod1.DateTo < ucPeriod1.DateFrom)
            {
                MessageBox.Show("Начальная дата д.б. меньше конечной", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucPeriod1.Focus();
                return false;
            }
            if (selectContractorStoreControl1.Contractors.Count == 0)
            {
                MessageBox.Show("Не заполнен контрагент!", "еФарма 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                selectContractorStoreControl1.Focus();
                return false;
            }
            return true;
        }

        private DataSet GetData()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            //Период
            ucPeriod1.AddValues(root);
            //Краткий отчет
            Utils.AddNode(root, "NO_DETAIL", (checkBox_Detail.Checked ? 1 : 0));
            //Контрагенты и склады
            selectContractorStoreControl1.AddItemContractor(root, "ID_CONTRACTOR");
            selectContractorStoreControl1.AddItemStore(root, "ID_STORE");
            //Сортировка
            Utils.AddNode(root, "SORT_DOC", (radioButton_TypeDoc.Checked ? 1 : 0));
            //Показывать "Приход/Расход"
            Utils.AddNode(root, "SHOW_ADD", (checkBox_Add.Checked ? 1 : 0));
            Utils.AddNode(root, "SHOW_SUB", (checkBox_Sub.Checked ? 1 : 0));
            return repBl.GetData("REP_GOODS_REPORTS1", doc.InnerXml);
        }

        private void SetReportParameter(string paramName, string value)
        {
            IfrxMemoView memo = report.FindObject(paramName) as IfrxMemoView;
            if (memo != null) memo.Memo = value;
        }

        private void BindDataSource(string tableName, string bandName, DataTable table)
        {
            table.TableName = tableName;
            FrxDataTable frxTab = new FrxDataTable(table);
            frxTab.AssignToReport(true, report);
            frxTab.AssignToDataBand(bandName, report);
        }
    }
}