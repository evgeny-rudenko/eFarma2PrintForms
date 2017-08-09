using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;

namespace DEFECTURA_EX
{
	public partial class DefecturaParams : ExternalReportForm, IExternalReportFormMethods
	{
		public DefecturaParams()
		{
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			int min = 0;

			if (!int.TryParse(minValue.Text, out min))
			{
				MessageBox.Show("Значение в поле Максимальный остаток должно быть целым числом!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "QTY_MIN", minValue.Text);
			Utils.AddNode(root, "SORT", Sort.SelectedIndex);
			Utils.AddNode(root, "DATE", toDateTimePicker.Value.ToString("yyyy-MM-ddTHH:mm:00.000"));
			Utils.AddNode(root, "DATE_FROM", fromDateTimePicker.Value.ToString("yyyy-MM-ddTHH:mm:00.000"));
			Utils.AddNode(root, "OA_ONLY", chbOA_ONLY.Checked);
			Utils.AddNode(root, "USE_GOODS_REPORT_NAME", chBGroups.Checked);
			Utils.AddNode(root, "IS_ES", chBES.Checked);

			if (cStore.Text != string.Empty)
				Utils.AddNode(root, "ID_STORE", cStore.Id);

			foreach (DataRowItem good in ucGoods.Items)
				Utils.AddNode(root, "ID_GOODS", good.Id);
				Utils.AddNode(root, "ID_USER", SecurityContextEx.USER_GUID.ToString());

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REP_DEFECTURA_EX", doc.InnerXml);
			rep.BindDataSource("DefecturaDS_Table", 0);

			rep.AddParameter("STORE", Sort.Text);
			rep.AddParameter("NAME", "");
			rep.AddParameter("PHONE", "");
			rep.AddParameter("CAPTION", string.Format("Дефектура c {0} на {1}", fromDateTimePicker.Value.ToString("dd.MM.yyyy HH:mm"), toDateTimePicker.Value.ToString("dd.MM.yyyy HH:mm")));
			rep.AddParameter("PARAM1", "Товар: " + ucGoods.TextValues());
			rep.AddParameter("OA_ONLY", chbOA_ONLY.Checked ? "1" : "0");

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Дефектура"; }
		}

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }

		private void ClearValues()
		{
			toDateTimePicker.Value = DateTime.Now;
			fromDateTimePicker.Value = DateTime.Now.AddMonths(-1);

			minValue.Text = "0";
			Sort.SelectedIndex = 0;

			ucGoods.Items.Clear();
			cStore.Clear();

			chbOA_ONLY.Checked = false;
			chBGroups.Checked = false;
			chBES.Checked = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}
	}
}