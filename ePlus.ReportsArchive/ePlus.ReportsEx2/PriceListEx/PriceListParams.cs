using System;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;

namespace PriceListEx
{
	public partial class PriceListParams : ExternalReportForm, IExternalReportFormMethods
	{
		public PriceListParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);

			ucGoods.AddItems(root, "ID_GOODS");
			ucStore.AddItems(root, "ID_STORE");

			Utils.AddNode(root, "DATE_OST", Utils.SqlDate(dateDateTimePicker.Value));
			Utils.AddNode(root, "SORT", cbSort.SelectedIndex);

			ReportFormNew rep = new ReportFormNew();

            if (cbForm.SelectedIndex == 0)
                rep.ReportPath = reportFiles[0];
			if (cbForm.SelectedIndex == 1)
				rep.ReportPath = reportFiles[1];

			rep.LoadData("REPEX_PRICE_LIST", doc.InnerXml);
			rep.BindDataSource("PriceListDS_Table", 0);
			rep.BindDataSource("PriceListDS_Table1", 1);

			string p_ch = "";
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
			    if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
			}

			rep.AddParameter("p_ch", p_ch);
			rep.AddParameter("STORE", ucStore.TextValues());
			rep.AddParameter("DATE_OST", string.Format("Прайс-лист на {0}", dateDateTimePicker.Value.ToString("dd.MM.yyyy")));

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Прайс-лист"; }
		}

		private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkedListBox1.Enabled = cbForm.SelectedIndex == 1;
		}

		private void ClearValues()
		{
			cbSort.SelectedIndex = 0;
			cbForm.SelectedIndex = 0;

			dateDateTimePicker.Value = DateTime.Now;

			ucGoods.Items.Clear();
			ucStore.Items.Clear();

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkedListBox1.Enabled = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }
	}
}