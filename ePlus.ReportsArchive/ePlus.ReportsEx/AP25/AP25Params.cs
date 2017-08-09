using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace AP25
{
	public partial class AP25Params : Form, IReportParams, IExternalReport
	{
		public AP25Params()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		public bool IsShowPreview
		{
			get { return true; }
		}

		public string HeaderText
		{
			get { return "Товарный отчет УФ АП-25"; }
		}

		public string ReportName
		{
			get { return "УФ АП-25"; }
		}

		public string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.GoodsReports).Description; }
		}

		private void ExtractReport()
		{
			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
			if (!Directory.Exists(cachePath))
				Directory.CreateDirectory(cachePath);
			Stream s = this.GetType().Assembly.GetManifestResourceStream("AP25.AP25.rdlc");
			StreamReader sr = new StreamReader(s);
			string rep = sr.ReadToEnd();
			string reportPath = Path.Combine(cachePath, "AP25.rdlc");
			using (StreamWriter sw = new StreamWriter(reportPath))
			{
				sw.Write(rep);
				sw.Flush();
				sw.Close();
			}
		}

		private void ClearCache()
		{
			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
			if (Directory.Exists(cachePath))
			{
				try
				{
					Utils.ClearFolder(cachePath);
					Directory.Delete(cachePath);
				}
				catch
				{

				}
			}
		}

		private void CreateStoredProc(string connectionString)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("AP25.REPEX_AP25.sql");
			StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
			string procScript = sr.ReadToEnd();
			string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);

			SqlCommand comm = null;
			foreach (string statement in batch)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					comm = new SqlCommand(statement, con);
					con.Open();
					comm.ExecuteNonQuery();
				}
			}
		}

		private string connectionString;
		private string folderPath;
		private const string CACHE_FOLDER = "Cache";
		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			this.MdiParent = AppManager.ClientMainForm;
			AppManager.RegisterForm(this);
			this.Show();
		}

		private bool Sort_By_DocType
		{
			get { return rbDocType.Checked && !rbDocDate.Checked; }
			set
			{
				rbDocType.Checked = value;
				rbDocDate.Checked = !value;
			}
		}


		private string fileName = Path.Combine(Utils.TempDir(), "GoodsReportsAP25Settings.xml");
		private void AP25Params_Load(object sender, EventArgs e)
		{
			if (!File.Exists(fileName)) return;
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			XmlNode root = doc.SelectSingleNode("/XML");
			chkShowAdd.Checked = Utils.GetBool(root, "SHOW_ADD");
			chkShowSub.Checked = Utils.GetBool(root, "SHOW_SUB");
			XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
			foreach (XmlNode node in contractors)
			{
				long id = Utils.GetLong(node, "ID");
				string text = Utils.GetString(node, "TEXT");
				DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
				mpsContractor.Items.Add(dri);
			}
			XmlNodeList stores = root.SelectNodes("STORE");
			foreach (XmlNode node in stores)
			{
				long id = Utils.GetLong(node, "ID");
				string text = Utils.GetString(node, "TEXT");
				DataRowItem dri = new DataRowItem(id, Guid.Empty, string.Empty, text);
				mpsStore.Items.Add(dri);
			}
			chkContractorGroup.Checked = Utils.GetBool(root, "CONTRACTOR_GROUP");
			Sort_By_DocType = Utils.GetBool(root, "SORT_BY_DOC_TYPE");
			chkShowSumByDocType.Checked = Utils.GetBool(root, "SHOW_SUM_BY_DOCTYPE");
			chkShortReport.Checked = Utils.GetBool(root, "SHORT_REPORT");
		}

		private void AP25Params_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClearCache();
			// Save
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked);
			Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked);
			foreach (DataRowItem dri in mpsContractor.Items)
			{
				XmlNode contractor = Utils.AddNode(root, "CONTRACTOR");
				Utils.AddNode(contractor, "ID", dri.Id);
				Utils.AddNode(contractor, "TEXT", dri.Text);
			}
			foreach (DataRowItem dri in mpsStore.Items)
			{
				XmlNode store = Utils.AddNode(root, "STORE");
				Utils.AddNode(store, "ID", dri.Id);
				Utils.AddNode(store, "TEXT", dri.Text);
			}
			Utils.AddNode(root, "CONTRACTOR_GROUP", chkContractorGroup.Checked);
			Utils.AddNode(root, "SORT_BY_DOC_TYPE", Sort_By_DocType);
			Utils.AddNode(root, "SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked);
			Utils.AddNode(root, "SHORT_REPORT", chkShortReport.Checked);
			doc.Save(fileName);
		}

		private void bClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void bPrint_Click(object sender, EventArgs e)
		{
			CreateStoredProc(connectionString);
			ExtractReport();
			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML", null);
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			foreach (DataRowItem dri in mpsContractor.Items)
				Utils.AddNode(root, "ID_CONTRACTOR", dri.Id);
			foreach (DataRowItem dri in mpsStore.Items)
				Utils.AddNode(root, "ID_STORE", dri.Id);
			Utils.AddNode(root, "SORT_BY_DOCTYPE", this.Sort_By_DocType);
			Utils.AddNode(root, "REFRESH_DOC_MOV", this.chkRefreshDocMov.Checked);

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = Path.Combine(cachePath, "AP25.rdlc");

			if (serviceCheckBox.Checked)
			{
				rep.LoadData("REPEX_AP25_SERVICE", doc.InnerXml);
			}
			else
			{
				rep.LoadData("REPEX_AP25", doc.InnerXml);
			}


			rep.BindDataSource("AP25_DS_Table0", 0);
			rep.BindDataSource("AP25_DS_Table1", 1);
			rep.BindDataSource("AP25_DS_Table2", 2);
			rep.BindDataSource("AP25_DS_Table3", 4);
			rep.BindDataSource("AP25_DS_Table4", 5);
			rep.BindDataSource("AP25_DS_Table5", 3);

			rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
			rep.AddParameter("SHOW_SUB", chkShowSub.Checked ? "1" : "0");
			rep.AddParameter("CONTRACTOR_GROUP", chkContractorGroup.Checked ? "1" : "0");
			rep.AddParameter("DOCTYPE_GROUP", this.Sort_By_DocType ? "1" : "0");
			rep.AddParameter("SHORT_REPORT", this.chkShortReport.Checked ? "1" : "0");
			rep.AddParameter("SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked ? "1" : "0");

			rep.ReportFormName = ReportName;
			rep.ExecuteReport(this);
		}

		private void rbDocType_CheckedChanged(object sender, EventArgs e)
		{
			chkShowSumByDocType.Visible = rbDocType.Checked;
		}


	}
}