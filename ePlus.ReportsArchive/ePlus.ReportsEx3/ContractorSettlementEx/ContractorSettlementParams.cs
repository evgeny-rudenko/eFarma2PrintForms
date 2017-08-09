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
using System.Data.SqlClient;
using System.IO;

namespace ContractorSettlementEx
{
	public partial class ContractorSettlementParams : ExternalReportForm, IExternalReportFormMethods
	{
		public ContractorSettlementParams()
		{
			InitializeComponent();				
		}

		public void Print(string[] reportFiles)
		{
			if (ucContractor.Id == 0)
			{
				MessageBox.Show("Не задан контрагент!", "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (contractorsListBox.SelectedItems.Count == 0)
			{
				MessageBox.Show("Не заданы " + viewComboBox.Text, "еФарма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			Utils.AddNode(root, "SHOW_ON_THE_WAY", wayCheckBox.Checked ? "1" : "0");
			Utils.AddNode(root, "ID_SECTION", viewComboBox.SelectedIndex + 1);
			Utils.AddNode(root, "REP_TYPE", viewComboBox.SelectedItem.ToString());
			
			foreach (ListItem item in contractorsListBox.SelectedItems)
			{
				Utils.AddNode(root, "SUP", item.Id);
			}

			ReportFormNew rep = new ReportFormNew();

			if (viewComboBox.SelectedIndex != 3)
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "ContractorSettlement.rdlc");
				rep.LoadData("REPEX_CONTRACTOR_SETTLEMENT", doc.InnerXml);
			}
			else
			{
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "ContractorSettlement_2.rdlc");
				rep.LoadData("REPEX_CONTRACTOR_SETTLEMENT_2", doc.InnerXml);
			}

			rep.BindDataSource("ContractorSettlement_DS_Table0", 0);

			rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
			rep.AddParameter("DATE_TO", ucPeriod.DateToText);
			rep.AddParameter("CONTRACTOR", ucContractor.Text);
			rep.AddParameter("SHOW_ON_THE_WAY", wayCheckBox.Checked ? "1" : "0");
			rep.AddParameter("SHORT", shortCheckBox.Checked ? "1" : "0");
			string s = "";
			switch (viewComboBox.SelectedItem.ToString())
			{
				case "Поставщики":
					s = "поставщиками";
					break;

				case "Аптеки":
					s = "аптеками";
					break;

				case "Покупатели":
					s = "покупателями";
					break;
				case "Поставщики/Покупатели":
					s = "Поставщиками/Покупателями";
					break;

				default:
					break;
			}

			rep.AddParameter("REP_TYPE", s);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Расчеты с контрагентами"; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			viewComboBox.SelectedIndex = 0;			
			wayCheckBox.Checked = false;
			shortCheckBox.Checked = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();			
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void viewComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadValues();
		}

		private void LoadValues()
		{
			contractorsListBox.Items.Clear();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string filter = string.Empty;
				switch (viewComboBox.SelectedIndex)
				{
					case 0:
						filter = "'Поставщики'";
						break;
					case 1:
						filter = "'Аптеки'";
						break;
					case 2:
						filter = "'Покупатели'";
						break;
					case 3:
						filter = "'Поставщики' OR cg.name = 'Покупатели'";
						break;
				}

				SqlCommand command = new SqlCommand(@"select
c.id_contractor, c.name 
from contractor c 
	inner join contractor_2_contractor_group ccg on ccg.id_contractor = c.id_contractor
	inner join contractor_group cg on cg.id_contractor_group = ccg.id_contractor_group
where cg.name = " + filter + " group by c.id_contractor, c.name order by c.name", connection);

				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						try
						{
							ListItem li = new ListItem();
							li.Id = reader.GetInt64(0);
							li.Name = reader.GetString(1);
							contractorsListBox.Items.Add(li);
						}
						catch
						{
						}
					}
				}
			}
		}

		private class ListItem
		{
			private long id;
			private string name;

			public long Id
			{
				get { return id; }
				set { id = value; }
			}

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public override string ToString()
			{
				return name;
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
					return false;

				ListItem li = obj as ListItem;

				if (li == null)
					return false;

				return this.id == li.id && this.name == li.name;
			}
		}

		private void ContractorSettlementParams_Load(object sender, EventArgs e)
		{
			ClearValues();
		}
	}
}