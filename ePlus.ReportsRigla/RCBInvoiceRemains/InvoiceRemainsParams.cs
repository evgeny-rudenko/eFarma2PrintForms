using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using Microsoft.Reporting.WinForms;

namespace InvoiceRemainsEx
{
    public partial class InvoiceRemainsParams : ExternalReportForm, IExternalReportFormMethods
    {
        private Dictionary<string, List<DataRowItem>> lists = new Dictionary<string, List<DataRowItem>>();
   
        public string ReportName
        {
            get
            {
                return "Остатки по приходу";
            }
        }
        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.MaterialReports).Description;
            }
        }
        public InvoiceRemainsParams()
        {
            this.InitializeComponent();
        }
        private void InvoiceRemainsParams_Load(object sender, EventArgs e)
        {
            this.ClearValues();
        }
        public void Print(string[] reportFiles)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = Utils.AddNode((XmlNode)xmlDocument, "XML");
            this.ucPeriod.AddValues(xmlNode);
            if (this.ucPluginMultiSelect.Enabled)
            {
                switch (this.ucPluginMultiSelect.Mnemocode)
                {
                    case "INVOICE":
                        this.ucPluginMultiSelect.AddItems(xmlNode, "ID_INVOICE");
                        break;
                    case "CONTRACTOR":
                        this.ucPluginMultiSelect.AddItems(xmlNode, "ID_SUPPLIER");
                        break;
                }
            }
            ReportFormNew reportFormNew = new ReportFormNew();
            reportFormNew.ReportPath = (reportFiles[0]);
            reportFormNew.LoadData("REPEX_INVOICE_REMAINS", xmlDocument.InnerXml);
            reportFormNew.BindDataSource("Invoice_rem_DS_Table", 0);
            ReportParameter[] reportParameterArray = new ReportParameter[3]
            {
                new ReportParameter("date_fr", this.ucPeriod.DateFrText),
                new ReportParameter("date_to", this.ucPeriod.DateToText),
                new ReportParameter("all_goods", "0")
            };
            reportFormNew.ReportViewer.LocalReport.SetParameters(reportParameterArray);
            reportFormNew.ExecuteReport(this);
        }
        private void ClearValues()
        {
            ucPeriod.SetPeriodMonth();
            this.filterComboBox.SelectedIndex = 0;
        }
        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            this.ClearValues();
        }
        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ucPluginMultiSelect.Enabled)
            {
                if (!this.lists.ContainsKey(this.ucPluginMultiSelect.Mnemocode))
                    this.lists.Add(this.ucPluginMultiSelect.Mnemocode, new List<DataRowItem>());
                else
                    this.lists[this.ucPluginMultiSelect.Mnemocode].Clear();
                using (IEnumerator<DataRowItem> enumerator = ((IEnumerable<DataRowItem>)this.ucPluginMultiSelect.Items).GetEnumerator())
                {
                    while (((IEnumerator)enumerator).MoveNext())
                        this.lists[this.ucPluginMultiSelect.Mnemocode].Add(enumerator.Current);
                }
            }
            List<DataRowItem> list = null;
            switch (this.filterComboBox.SelectedIndex)
            {
                case 0:
                    this.ucPluginMultiSelect.Enabled = true;
                    this.ucPluginMultiSelect.SetPluginControl("INVOICE");
                    this.ucPluginMultiSelect.Caption = ("Приходные документы");
                    this.ucPluginMultiSelect.Clear();
                    if (!this.lists.ContainsKey("INVOICE"))
                        this.lists.Add("INVOICE", new List<DataRowItem>());
                    list = this.lists["INVOICE"];
                    break;
                case 1:
                    this.ucPluginMultiSelect.Enabled = true;
                    this.ucPluginMultiSelect.SetPluginControl("CONTRACTOR");
                    this.ucPluginMultiSelect.Caption = ("Поставщики");
                    this.ucPluginMultiSelect.Clear();
                    if (!this.lists.ContainsKey("CONTRACTOR"))
                        this.lists.Add("CONTRACTOR", new List<DataRowItem>());
                    list = this.lists["CONTRACTOR"];
                    break;
                case 2:
                    this.ucPluginMultiSelect.Enabled = false;
                    this.ucPluginMultiSelect.Mnemocode = (string.Empty);
                    this.ucPluginMultiSelect.Caption = (string.Empty);
                    this.ucPluginMultiSelect.Clear();
                    list = null;
                    break;
            }
            this.ucPeriod.Enabled = list != this.lists["INVOICE"];
            if (list == null)
                return;
            using (List<DataRowItem>.Enumerator enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    this.ucPluginMultiSelect.AddRowItem(enumerator.Current);
            }
        }
    }
}
