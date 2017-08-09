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
using ePlus.MetaData.Server;
using System.Data.SqlClient;
using System.IO;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
namespace RCChBonusRemAdd
{
	public partial class DefecturaParams : ExternalReportForm, IExternalReportFormMethods
	{
        public DefecturaParams()
        {
            InitializeComponent();
        }
		public void Print(string[] reportFiles)
		{
    		XmlDocument param = new XmlDocument();
			XmlNode root = Utils.AddNode(param, "XML");

			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
            Utils.AddNode(root, "CARD_ONLY_WITH_MOVE", cbCardWithMove.Checked ? 1 : 0);

            foreach (DataRowItem dr in ucContractors.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);
            }

            foreach (DataRowItem dr in ucDiscount2_Card.Items)
            {
                Utils.AddNode(root, "ID_DISCOUNT2_CARD_GLOBAL", dr.Guid);
            }
			
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("DBO.REPEX_BONUSREMADD", param.InnerXml);
			rep.BindDataSource("BonusRemAdd_DS_Table0", 0);

            rep.AddParameter("date_from", ucPeriod.DateFrom.ToString("g"));
            rep.AddParameter("date_to", ucPeriod.DateTo.ToString("g"));
			rep.AddParameter("discount2_card", ucDiscount2_Card.TextValues());
			rep.AddParameter("contr", ucContractors.TextValues());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Отчет по списанным и накопленным баллам"; }
		}

		public override string GroupName
		{
            get { return string.Empty; }
		}

		private void ClearValues()
		{
            ucPeriod.DateFrom = DateTime.Now.AddMonths(-1);
            ucPeriod.DateFrom = new DateTime(ucPeriod.DateFrom.Year,ucPeriod.DateFrom.Month,1);
            ucPeriod.DateTo = ucPeriod.DateFrom.AddMonths(1);
            ucPeriod.DateTo =ucPeriod.DateTo.AddDays(-1);
            ucDiscount2_Card.Items.Clear();
			ucContractors.Items.Clear();
            cbCardWithMove.Checked = true;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}


		private void DefecturaParams_Load(object sender, EventArgs e)
		{
			ClearValues();
			LoadSettings();
		}



		private string settingsFilePath = Path.Combine(Utils.TempDir(), "DefecturaReportExSettings.xml");
        private void ContractorSelf(out long  id, out string text ,out Guid guid )
        {
            SqlDataReader result = null;
            DataService_BL bl = new DataService_BL();
            id = 0;
            text = string.Empty;
            guid = Guid.Empty;
            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand(@"	SELECT FULL_NAME
			                                                ,ID_CONTRACTOR
			                                                ,ID_CONTRACTOR_GLOBAL
                                                        FROM CONTRACTOR
                                                        WHERE ID_CONTRACTOR = DBO.FN_CONST_CONTRACTOR_SELF()", connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                result = command.ExecuteReader();
                while (result.Read())
                {
                    text = result.GetString(0);
                    id = (long)result.GetValue(1);
                    guid = result.GetGuid(2);
                }
                result.Close();
            }
        }

		private void LoadSettings()
		{
            if (!File.Exists(settingsFilePath))
            {
                
                long id = 0;
                string text = string.Empty;
                Guid guid = Guid.Empty;
                string code = string.Empty;
                ContractorSelf(out id, out text, out guid);
                ucContractors.Items.Add(new DataRowItem(id, guid, code, text));
                return;
            }

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

            if (root == null)
            {

                return;
            }
            XmlNodeList Contr = root.SelectNodes("ID_CONTRACTOR");
            foreach (XmlNode node in Contr)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucContractors.Items.Add(new DataRowItem(id, guid, code, text));
            }

            XmlNodeList Discount = root.SelectNodes("ID_DISCOUNT2_CARD");
            foreach (XmlNode node in Discount)
            {
                long id = Utils.GetLong(node, "ID");
                string text = Utils.GetString(node, "TEXT");
                Guid guid = Utils.GetGuid(node, "GUID");
                string code = Utils.GetString(node, "CODE");
                ucDiscount2_Card.Items.Add(new DataRowItem(id, guid, code, text));
            }
            ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FR");
            ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
            cbCardWithMove.Checked = Utils.GetBool(root, "CARD_ONLY_WITH_MOVE");

		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root;			

			if (File.Exists(settingsFilePath))
			{
				doc.Load(settingsFilePath);
				root = doc.SelectSingleNode("//XML");
				root.RemoveAll();
			}
			else
			{
				root = Utils.AddNode(doc, "XML");
			}
            foreach (DataRowItem dri in ucContractors.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_CONTRACTOR");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "ID", dri.Id);
            }
            foreach (DataRowItem dri in ucDiscount2_Card.Items)
            {
                XmlNode node = Utils.AddNode(root, "ID_DISCOUNT2_CARD");
                Utils.AddNode(node, "GUID", dri.Guid);
                Utils.AddNode(node, "CODE", dri.Code);
                Utils.AddNode(node, "TEXT", dri.Text);
                Utils.AddNode(node, "ID", dri.Id);
            }
            
			Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
			Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
			Utils.AddNode(root, "CARD_ONLY_WITH_MOVE", cbCardWithMove.Checked ? 1 : 0);

			doc.Save(settingsFilePath);
		}

		private void DefecturaParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}
	}
}