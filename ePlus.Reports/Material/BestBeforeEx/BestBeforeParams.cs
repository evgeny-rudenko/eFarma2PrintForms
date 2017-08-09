using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.IO;

namespace RCSBestBefore
{
	public partial class BestBeforeParams : ExternalReportForm, IExternalReportFormMethods
	{
		public BestBeforeParams()
		{
			InitializeComponent();	 
		}

		public void Print(string[] reportFiles)
		{
			string selfName = string.Empty;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(@"SELECT C.NAME FROM REPLICATION_CONFIG AS RC INNER JOIN CONTRACTOR AS C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1", con);
				con.Open();
				selfName = (string)command.ExecuteScalar();
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			string vtemp = "";

			if (includeExpiredComboBox.Checked)
				Utils.AddNode(root, "BEFORE", "1");
			if (comboSort.Text == "Название товара")
			{
				vtemp = "order by GOODS_NAME";
			}
			if (comboSort.Text == "Срок годности")
			{
				vtemp = "order by BEST_BEFORE";
			}

			Utils.AddNode(root, "ORDER_BY", vtemp);

			storesPluginMultiSelect.AddItems(root, "ID_STORE");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_BEST_BEFORE", doc.InnerXml);
			rep.BindDataSource("BestBefore_DS_Table", 0);

			ReportParameter[] parameters = new ReportParameter[4] {
				new ReportParameter("DATE_TO", DateTime.Now.ToString("d")),
				new ReportParameter("STORE", storesPluginMultiSelect.TextValues()),
				new ReportParameter("BEFORE", (includeExpiredComboBox.Checked) ? "С учетом просроченных" : string.Empty),
				new ReportParameter("NAME", selfName)
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			includeExpiredComboBox.Checked = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по сроку годности препаратов (4 журнал)"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

        private void BestBeforeParams_Load(object sender, EventArgs e)
        {
            ClearValues();
            comboSort.SelectedIndex = 0;
        }
	}
}