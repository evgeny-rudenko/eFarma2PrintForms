using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using System.IO;

namespace RCBFactorReceipts
{
	public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		public FormParams()
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
			if (((cbType.SelectedIndex == 1) && (ucPeriod.DateFrom > ucPeriod.DateTo)) || ((cbType.SelectedIndex == 2) && (dtpDateFr.Value > dtpDateTo.Value)))
			{
				MessageBox.Show("Даты выбраны некорректно", "еФарма", MessageBoxButtons.OK);
				return;
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
            ucStore.AddItems(root, "ID_STORE");
			if (cbType.SelectedIndex != 3)
			{
				Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);
			}

			ReportFormNew rep = new ReportFormNew();
			if (cbType.SelectedIndex == 0)
			{
				//Utils.AddNode(root, "DATE_CHEQUE", dtpDay.Value);    
				Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				Utils.AddNode(root, "SHOW_AVG", !showSumCheckBox.Checked);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "HoursReport.rdlc");
				rep.LoadData("DBO.REP_FACTOR_RECEIPTS_HOUR", doc.InnerXml);
				rep.BindDataSource("FactorReceipts_DS_Table1", 0);
				rep.BindDataSource("FactorReceipts_DS_Table2", 1);
				rep.BindDataSource("FactorReceipts_DS_Table3", 2); 
				rep.BindDataSource("FactorReceipts_DS_Table4", 3);
				rep.AddParameter("date", ucPeriod.DateFrText + " - " + ucPeriod.DateToText);
			}
			if (cbType.SelectedIndex == 1)
			{
				Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
				Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "DaysReport.rdlc");
				rep.LoadData("REP_FACTOR_RECEIPTS_DAYS", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsDays_DS_Table1", 0);
				rep.AddParameter("date_fr", ucPeriod.DateFrText);
				rep.AddParameter("date_to", ucPeriod.DateToText);
				rep.AddParameter("period", (ucPeriod.DateTo.Subtract(ucPeriod.DateFrom).Days + 1).ToString());
			}
			if (cbType.SelectedIndex == 2) 
			{
				Utils.AddNode(root, "DATE_FROM", dtpDateFr.Value);
				Utils.AddNode(root, "DATE_TO", dtpDateTo.Value);
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MonthsReport.rdlc");
				rep.LoadData("REP_FACTOR_RECEIPTS_MONTHS", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsMonths_DS_Table1", 0);
				rep.AddParameter("date_fr", dtpDateFr.Text);
				rep.AddParameter("date_to", dtpDateTo.Text);
			}
			if (cbType.SelectedIndex == 3)
			{				
				Utils.AddNode(root, "DATE_FROM", new DateTime(yearDateTimePicker.Value.Year, 1, 1));
				Utils.AddNode(root, "DATE_TO", new DateTime(yearDateTimePicker.Value.Year, 12, 31));
				ucApt.AddItems(root, "ID_CONTRACTOR");
				rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "MonthsReportIncrement.rdlc");
				rep.LoadData("REPEX_FACTOR_RECEIPTS_MONTHS_INCREMENT", doc.InnerXml);
				rep.BindDataSource("FactorReceiptsMonthsIncrement_DS_Table1", 0);
				rep.BindDataSource("FactorReceiptsMonthsIncrement_DS_Table2", 1);
				rep.AddParameter("user", ucApt.TextValues());
				rep.AddParameter("year", yearDateTimePicker.Value.Year.ToString());
			}

			if (cbType.SelectedIndex != 3)
			{
				rep.AddParameter("user", ucContractor.Text);
			}
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.AddParameter("STORES", ucStore.TextValues());
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Показатели выручки аптеки"; }
		}

		public override string GroupName
		{
			get	{ return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		private void cbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			pDay.Visible = false;
			pPeriod.Visible = cbType.SelectedIndex != 2 && cbType.SelectedIndex != 3;
			pMonths.Visible = cbType.SelectedIndex == 2;
			yearPanel.Visible = cbType.SelectedIndex == 3;

			ucContractor.Enabled = cbType.SelectedIndex != 3;
			label6.Enabled = cbType.SelectedIndex != 3;
			ucApt.Enabled = cbType.SelectedIndex == 3;

			showSumCheckBox.Enabled = cbType.SelectedIndex == 0;
		}

		private void FormParams_Load(object sender, EventArgs e)
		{
      СlearValues();
			this.ucContractor.SetId(this.IdContractorDefault);

      if (!File.Exists(SettingsFilePath))
      {
        return;
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
      {
        return;
      }

			cbType.SelectedIndex = Utils.GetInt(root, "TYPE");
			XmlNodeList contractor = root.SelectNodes("CONTRACTOR");
			foreach (XmlNode node in contractor)
			{
				long id = Utils.GetLong(node, "ID");
				string text = Utils.GetString(node, "TEXT");
				ucContractor.SetId(id);
			}
			dtpDay.Value = Utils.GetDate(root, "DTP_DAY");
			ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
			ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
			dtpDateFr.Value = Utils.GetDate(root, "DTP_DATE_FR");
			dtpDateTo.Value = Utils.GetDate(root, "DTP_DATE_TO");
			showSumCheckBox.Checked = Utils.GetBool("SHOW_SUM");

      XmlNodeList contractors = root.SelectNodes("APT");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucApt.AddRowItem(new DataRowItem(id, guid, code, text));
      }
		}

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
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

			Utils.AddNode(root, "TYPE", cbType.SelectedIndex);
			Utils.AddNode(root, "DTP_DAY", dtpDay.Value);
			Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "DTP_DATE_FR", dtpDateFr.Value);
			Utils.AddNode(root, "DTP_DATE_TO", dtpDateTo.Value);
			Utils.AddNode(root, "SHOW_SUM", showSumCheckBox.Checked);

      foreach (DataRowItem dri in ucApt.Items)
      {
        XmlNode node = Utils.AddNode(root, "APT");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      XmlNode contr = Utils.AddNode(root, "CONTRACTOR");
      Utils.AddNode(contr, "ID", ucContractor.Id);
      Utils.AddNode(contr, "TEXT", ucContractor.Text);

			doc.Save(SettingsFilePath);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			СlearValues();
		}

		private void СlearValues()
		{
			dtpDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);

			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);

			dtpDateFr.Value = DateTime.Now;
			dtpDateFr.Value = dtpDateFr.Value.AddMonths(-1);

			yearDateTimePicker.Value = DateTime.Now;
			cbType.SelectedIndex = 0;
			ucApt.Items.Clear();

			showSumCheckBox.Checked = false;
		}

        private void ucApt_BeforePluginShow(object sender, CancelEventArgs e)
        {

        }

        private void ucStore_BeforePluginShow(object sender, CancelEventArgs e)
        {
            /*Убираем работу с ЦО
      // Выбираем склады только для МЫ, если МЫ не ЦО
      if (!SelfIsCenter())
      {
        //ucStores.PluginContol.Grid(0).SetParameterValue("@IS_SELF", "1");
        ucStores.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
            String.Format("(STORE.ID_CONTRACTOR IN (SELECT C.ID_CONTRACTOR FROM REPLICATION_CONFIG RC INNER JOIN CONTRACTOR C ON RC.ID_CONTRACTOR_GLOBAL = C.ID_CONTRACTOR_GLOBAL WHERE RC.IS_SELF = 1 AND RC.IS_ACTIVE = 1))"));
      }
      // если МЫ = ЦО, отбираем склады только для выбранных аптек или для всех, если список аптек пуст
      else
      {*/
            if (ucApt.Items.Count > 0)
            {
                string stores = string.Empty;
                foreach (DataRowItem dri in ucApt.Items)
                {
                    stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
                }
                if (!String.IsNullOrEmpty(stores))
                    ucStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
                      String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));

            }
          /*  else
            {
                e.Cancel = true;
                MessageBox.Show("Выберите контрагента!");
            }
           * */
            /*
            if (ucApt.Items.Count > 0)
            {
                string stores = string.Empty;
                foreach (DataRowItem dri in ucApt.Items)
                {
                    stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
                }
                if (!String.IsNullOrEmpty(stores))
                    ucStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
                      String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));

            }
            else
            {
                ucStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
                    "(1=1)");
            }
            */
        }
	}
}
