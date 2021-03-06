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

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

		public void Print(string[] reportFiles)
		{
			if (ucContractor.Id == 0)
			{
				MessageBox.Show("�� ����� ����������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (contractorsListBox.SelectedItems.Count == 0)
			{
				MessageBox.Show("�� ������ " + viewComboBox.Text, "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
			
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
				case "����������":
					s = "������������";
					break;

				case "������":
					s = "��������";
					break;

				case "����������":
					s = "������������";
					break;
				case "����������/����������":
					s = "������������/������������";
					break;

				default:
					break;
			}

			rep.AddParameter("REP_TYPE", s);

			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "������� � �������������"; }
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);
			viewComboBox.SelectedIndex = 0;			
			wayCheckBox.Checked = false;
			shortCheckBox.Checked = false;
      contractorsListBox.Items.Clear();
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
						filter = "'����������'";
						break;
					case 1:
						filter = "'������'";
						break;
					case 2:
						filter = "'����������'";
						break;
					case 3:
						filter = "'����������' OR cg.name = '����������'";
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
			LoadSettings();
		}

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
        return;

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNode node = root.SelectSingleNode("CONTRACTOR");
      ucContractor.SetValues(new DataRowItem(Utils.GetLong(node, "ID"),
        Utils.GetGuid(node, "GUID"), Utils.GetString(node, "CODE"), Utils.GetString(node, "TEXT")));
      viewComboBox.SelectedIndex = Utils.GetInt(root, "VIEW");

      XmlNodeList nodes = root.SelectNodes("CONTRACTORS_ITEM");
      foreach (XmlNode nd in nodes)
      {
        ListItem li = new ListItem();
        li.Id = Utils.GetLong(nd, "ID");
        li.Name = Utils.GetString(nd, "NAME");
        contractorsListBox.Items.Add(li);
      }

      wayCheckBox.Checked = Utils.GetBool(root, "WAY");
      shortCheckBox.Checked = Utils.GetBool(root, "SHORT");
      auCheckBox.Checked = Utils.GetBool(root, "AU");
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root;

      if (File.Exists(SettingsFilePath))
      {
        doc.Load(SettingsFilePath);
        root = doc.SelectSingleNode("//XML");
        root.RemoveAll();
      }
      else
      {
        root = Utils.AddNode(doc, "XML");
      }

      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

      XmlNode node = Utils.AddNode(root, "CONTRACTOR");
      Utils.AddNode(node, "ID", ucContractor.Id);
      Utils.AddNode(node, "GUID", ucContractor.Guid);
      Utils.AddNode(node, "CODE", ucContractor.Code);
      Utils.AddNode(node, "TEXT", ucContractor.Text);

      Utils.AddNode(root, "VIEW", viewComboBox.SelectedIndex);

      foreach(ListItem li in contractorsListBox.Items)
      {
        node = Utils.AddNode(root, "CONTRACTORS_ITEM");
        Utils.AddNode(node, "ID", li.Id);
        Utils.AddNode(node, "NAME", li.Name);
      }

      Utils.AddNode(root, "WAY", wayCheckBox.Checked);
      Utils.AddNode(root, "SHORT", shortCheckBox.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);

      doc.Save(SettingsFilePath);
    }

    private void ContractorSettlementParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
	}
}