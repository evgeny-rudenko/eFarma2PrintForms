using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace StatistAddSubEx
{
	public partial class StatistAddSubParams : ExternalReportForm, IExternalReportFormMethods
	{
		internal class Doc
		{
			private string codeOp;
			private string description;
			private bool isAdd;
			private bool isSub;

			public string Description
			{
				get { return description; }
				set { description = value; }
			}

			public bool IsAdd
			{
				get { return isAdd; }
				set { isAdd = value; }
			}

			public bool IsSub
			{
				get { return isSub; }
				set { isSub = value; }
			}

			public string CodeOp
			{
				get { return codeOp; }
				set { codeOp = value; }
			}

			public Doc(string codeOp, string description)
			{
				this.codeOp = codeOp;
				this.description = description;
			}

		}

		private List<Doc> docs = new List<Doc>();

		public StatistAddSubParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			if (ucContractor.Id == 0)
			{
				MessageBox.Show("�� ������ ����������!", "������ 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!AreDocsSelected())
			{
				MessageBox.Show("�� ������� ���� ����������!", "������ 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlNode doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DATE_FROM", period.DateFrom);
			Utils.AddNode(root, "DATE_TO", period.DateTo);
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			Utils.AddNode(root, "DOCUMENTS", 0);

			foreach (Doc item in docs)
			{
				if (item.IsAdd || item.IsSub)
				{
					XmlNode codeOp = Utils.AddNode(root, item.CodeOp);
					Utils.AddNode(codeOp, "IS_INV", item.IsAdd);
					Utils.AddNode(codeOp, "IS_EXP", item.IsSub);
				}
			}

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_STATIST_ADD_SUB", doc.InnerXml);
			rep.BindDataSource("StatistAddSub_DS_Table1", 0);

			rep.AddParameter("Date_From", period.DateFrText);
			rep.AddParameter("Date_To", period.DateToText);

			rep.ExecuteReport(this);
		}

		private bool AreDocsSelected()
		{
			foreach (Doc doc in docs)
			{
				if (doc.IsAdd || doc.IsSub)
					return true;
			}
			return false;
		}

		public string ReportName
		{
			get { return "������� � ������� � ��������"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void ClearValues()
		{
			period.DateTo = DateTime.Now;
			period.DateFrom = DateTime.Now.AddDays(-13);

			ucContractor.Clear();

			documents.CurrentCell = documents.Rows[0].Cells["Description"];

			foreach (Doc doc in docs)
			{
				doc.IsAdd = false;
				doc.IsSub = false;
			}
						
			documents.Refresh();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void StatistAddSubParams_Load(object sender, EventArgs e)
		{
			docs.Add(new Doc("INVOICE", "��������� ���������"));
			docs.Add(new Doc("ACT_RETURN_TO_CONTRACTOR", "��� �������� ����������"));
			docs.Add(new Doc("ACT_RETURN_TO_BUYER", "��� �������� �� ����������"));
			docs.Add(new Doc("ACT_REVALUATION", "��� ����������"));
			docs.Add(new Doc("CASH_SESSION", "�������� �����"));
			docs.Add(new Doc("ACT_DEDUCTION", "��� ��������"));
			docs.Add(new Doc("INVOICE_OUT", "��������� ���������"));
			docs.Add(new Doc("BILL", "����"));
			docs.Add(new Doc("INVENTORY_SVED", "�������������� �������"));
			docs.Add(new Doc("VAT_CORRECT", "������������� ��������� ������"));
			docs.Add(new Doc("IMPORT_REMAINS", "���� ��������"));

			documents.ColumnHeadersVisible = true;
			documents.AllowUserToAddRows = false;
			documents.AllowUserToResizeRows = false;
			documents.RowHeadersVisible = false;
			documents.MultiSelect = false;
			documents.ReadOnly = false;
			documents.DataSource = docs;
			documents.Columns["Description"].Resizable = DataGridViewTriState.True;
			documents.Columns["Description"].HeaderText = "��������";
			documents.Columns["Description"].Width = 200;
			documents.Columns["Description"].ReadOnly = true;
			documents.Columns["IsAdd"].Resizable = DataGridViewTriState.False;
			documents.Columns["IsAdd"].Width = 60;
			documents.Columns["IsAdd"].HeaderText = "�����";
			documents.Columns["IsSub"].Resizable = DataGridViewTriState.False;
			documents.Columns["IsSub"].Width = 60;
			documents.Columns["IsSub"].HeaderText = "������";
			documents.Columns["CodeOp"].Visible = false;

			ClearValues();
		}
	}
}