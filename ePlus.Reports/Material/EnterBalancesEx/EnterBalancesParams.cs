using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Server;
using ePlus.CommonEx.Reporting;

namespace EnterBalancesEx
{
    public partial class EnterBalancesParams : ExternalReportForm, IExternalReportFormMethods
    {
        public EnterBalancesParams()
        {
            InitializeComponent();
			ucRemains.AllowSaveState = true;
			ClearValues();
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            Utils.AddNode(root, "TYPE_REPORT", rbEnterBal.Checked ? "1" : "0");
            Utils.AddNode(root, "SORT_FIELD", cbSort.SelectedIndex);

            foreach (DataRowItem docs in ucRemains.Items)
                Utils.AddNode(root, "ID_DOC", docs.Id);

            ReportFormNew rep = new ReportFormNew();
			rep.LoadData("REPEX_ENTER_BALANCES", doc.InnerXml);

            if (rbEnterBal.Checked)
            {
                rep.BindDataSource("EnterBalancesEx_DS_Table1", 0);
                rep.BindDataSource("EnterBalancesEx_DS_Table3", 1);
                rep.BindDataSource("EnterBalancesEx_DS_Table4", 2);
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "EnterBalances.rdlc");
            }
            else
            {
                rep.BindDataSource("EnterBalancesEx_DS_Table2", 0);
                rep.BindDataSource("EnterBalancesEx_DS_Table3", 1);
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "SummaryStatement.rdlc");
            }

            string p_ch = "";
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
            }
            rep.AddParameter("p_ch", p_ch);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "¬вод остатков"; }
        }

        public void ClearValues()
        {
			cbSort.SelectedIndex = 0;

			rbSummatySt.Checked = true;
			ucRemains.Items.Clear();

			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
				checkedListBox1.SetItemChecked(i, true);

			checkedListBox1.Enabled = false;
			cbSort.Enabled = false;
        }

        private void rbEnterBal_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Enabled = rbEnterBal.Checked? true : false;
            cbSort.Enabled = rbEnterBal.Checked ? true : false;
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }
    }
}