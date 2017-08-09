using System;
using System.IO;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;


namespace PriceListEx
{
	public partial class PriceListParams : ExternalReportForm, IExternalReportFormMethods
	{
		public PriceListParams()
		{
			InitializeComponent();
			dateOst.Value = DateTime.Now;
			cbSort.SelectedIndex = 0;
			cbForm.SelectedIndex = 0;
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
				checkedListBox1.SetItemChecked(i, true);
			checkedListBox1.Enabled = false;
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);
			ucGoods.AddItems(root, "ID_GOODS");
			ucStore.AddItems(root, "ID_STORE");
			Utils.AddNode(root, "DATE_OST", Utils.SqlDate(dateOst.Value));
			Utils.AddNode(root, "SORT", cbSort.SelectedIndex);

			ReportFormNew rep = new ReportFormNew();
            if (cbForm.SelectedIndex == 0)
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),"PriceList1Ex.rdlc");				
			if (cbForm.SelectedIndex == 1)
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "PriceList2Ex.rdlc");				
			rep.LoadData("REP_PRICELISTNEW_EX", doc.InnerXml);
			rep.BindDataSource("PriceListDS_Table", 0);
			rep.BindDataSource("PriceListDS_Table1", 1);
			string p_ch = "";
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
			    if (checkedListBox1.GetItemChecked(i)) p_ch += "1"; else p_ch += "0";
			}
			rep.AddParameter("p_ch", p_ch);
			rep.AddParameter("STORE", ucStore.TextValues());
			rep.AddParameter("DATE_OST", string.Format("Прайс-лист на {0}", dateOst.Value.ToString("dd.MM.yyyy")));
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Прайс-лист"; }
		}

		private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbForm.SelectedIndex == 1) checkedListBox1.Enabled = true;
			else checkedListBox1.Enabled = false;
		}

		private void SetDefaultValues()
		{
			cbSort.SelectedIndex = 0;
			cbForm.SelectedIndex = 0;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkedListBox1.Enabled = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
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