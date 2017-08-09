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

namespace DiscountInsuranceSOGAZEx
{
	public partial class DiscountInsuranceParams : ExternalReportForm, IExternalReportFormMethods
	{
		public DiscountInsuranceParams()
		{
			InitializeComponent();
			ClearValues();
		}

		private string GetSortOrder()
		{
			string date = dateSortingComboBox.SelectedIndex == 0 ?
					string.Empty : dateSortingComboBox.SelectedIndex == 1 ? "DATE ASC" : "DATE DESC";

			string member = discounteeSortingComboBox.SelectedIndex == 0 ?
					string.Empty : discounteeSortingComboBox.SelectedIndex == 1 ? "MEMBER ASC" : "MEMBER DESC";

			if (date == string.Empty)
			{
				return member;
			}
			else if (member == string.Empty)
			{
				return date;
			}
			else if (!sortingOrder—heckBox.Checked)
			{
				return date + ", " + member;
			}
			else
			{
				return member + ", " + date;
			}
		}

		public void Print(string[] reportFiles)
		{
			Guid? company_id = null;		

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL FROM DISCOUNT2_INSURANCE_COMPANY WHERE NAME = '—Œ√¿«'", con);
				con.Open();
				company_id = (Guid?) command.ExecuteScalar();
			}

			if (!company_id.HasValue)
			{
				MessageBox.Show("¬ ·‡ÁÂ ‰‡ÌÌ˚ı ÓÚÒÛÚÒÚ‚ÛÂÚ ÒÚ‡ıÓ‚‡ˇ ÍÓÏÔ‡ÌËˇ —Œ√¿«!", "Â‘‡Ï‡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			XmlNode lgotsNode = Utils.AddNode(root, "LGOTS");
			foreach (DataRowItem dri in discount2catLgotsPluginMultiSelect.Items)
			{
				XmlNode lgotNode = Utils.AddNode(lgotsNode, "LGOT");
				Utils.AddNode(lgotNode, "ID_DISCOUNT2_CAT_LGOT_GLOBAL", dri.Guid);
			}

			StringBuilder discountCaption = new StringBuilder();
			XmlNode pecentsNode = Utils.AddNode(root, "PRECENTS");
			foreach (DataRowItem dri in discountsPluginMultiSelect.Items)
			{
				XmlNode percent = Utils.AddNode(pecentsNode, "PRECENT");

				decimal pct = Utils.GetDecimal(dri.Code);
				Utils.AddNode(percent, "DISCOUNT_VALUE", pct);

				if (discountCaption.Length > 0)
					discountCaption.Append(", ");
				discountCaption.AppendFormat("{0:0.0}%", pct);
			}

			XmlNode policies = Utils.AddNode(root, "POLICIES");
			foreach (DataRowItem dri in policiesPluginMultiSelect.Items)
			{
				XmlNode policy = Utils.AddNode(policies, "POLICY");
				Utils.AddNode(policy, "ID_DISCOUNT2_INSURANCE_POLICY_GLOBAL", dri.Guid);
			}

			XmlNode members = Utils.AddNode(root, "MEMBERS");
			foreach (DataRowItem dri in membersPluginMultiSelect.Items)
			{
				XmlNode member = Utils.AddNode(members, "MEMBER");
				Utils.AddNode(member, "ID_DISCOUNT2_MEMBER_GLOBAL", dri.Guid);
			}

			XmlNode goodsNodes = Utils.AddNode(root, "GOODSLIST");
			foreach (DataRowItem dri in goodsPluginMultiSelect.Items)
			{
				XmlNode goods = Utils.AddNode(goodsNodes, "GOODS");
				Utils.AddNode(goods, "ID_GOODS", dri.Id);
			}

			Utils.AddNode(root, "ID_DISCOUNT2_INSURANCE_COMPANY_GLOBAL", company_id.Value);
			Utils.AddNode(root, "DATE_FROM", periodPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
			Utils.AddNode(root, "SORT_ORDER", GetSortOrder());
			
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_DISCOUNT_INSURANCE_SOGAZ", doc.InnerXml);
			rep.BindDataSource("Discount2Insurance_DS_Table0", 0, false);

			ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("discountCaption", discountCaption.ToString())
			};

			rep.ReportViewer.LocalReport.SetParameters(parameters);

			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			discount2catLgotsPluginMultiSelect.Items.Clear();
			discountsPluginMultiSelect.Items.Clear();
			policiesPluginMultiSelect.Items.Clear();
			membersPluginMultiSelect.Clear();
			goodsPluginMultiSelect.Items.Clear();

			dateSortingComboBox.SelectedIndex = 0;
			discounteeSortingComboBox.SelectedIndex = 0;

			sortingOrder—heckBox.Checked = false;
			periodPeriod.SetPeriodMonth();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "ŒÚ˜∏Ú ÔÓ ÒÍË‰Í‡Ï ‰Îˇ ÒÚ‡ıÓ‚˚ı ÍÓÏÔ‡ÌËÈ —Œ√¿«"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
		}

		private void sortingOrder—heckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SuspendLayout();
			Point temp = discounteeSortingComboBox.Location;
			discounteeSortingComboBox.Location = dateSortingComboBox.Location;
			dateSortingComboBox.Location = temp;

			temp = discounteeLabel.Location;
			discounteeLabel.Location = dateLabel.Location;
			dateLabel.Location = temp;
			this.ResumeLayout();
		}
	}
}