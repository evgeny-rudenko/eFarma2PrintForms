using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;

namespace RCBSvyazCards_Rigla
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
		private string settingsFilePath = Path.Combine(Utils.TempDir(), "SvyazCardsSettings.xml");

        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);
			ucContractor.AddItems(root, "ID_CONTRACTOR");
			ucDiscountCard.AddItems(root, "ID_CARD");

			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "SvyazCards.rdlc");

			rep.LoadData("DBO.REPEX_SVYAZ_CARDS", doc.InnerXml);
			rep.BindDataSource("SvyazCards_DS_Table0", 0);
			rep.BindDataSource("SvyazCards_DS_Table1", 1);

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("contr", ucContractor.TextValues());
			rep.AddParameter("cards", ucDiscountCard.TextValues()); ;
			rep.AddParameter("code", codeCheckBox.Checked ? "1" : "0");
			rep.AddParameter("tran", tranCheckBox.Checked ? "1" : "0");
			rep.AddParameter("goods", goodsCheckBox.Checked ? "1" : "0");
			rep.AddParameter("card_type", cardsCheckBox.Checked ? "1" : "0");
			rep.AddParameter("cheque_type", chequeCheckBox.Checked ? "1" : "0");
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

			rep.ExecuteReport(this);
        }

        public string ReportName
        {
			get { return "Операции по Связному Клубу"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.CashReports).Description; }
        }

		private const string CODE = "CODE";
		private const string TRAN = "TRAN";
		private const string GOODS = "GOODS";
		private const string CARDS = "CARDS";
		private const string CHEQUE = "CHEQUE";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			codeCheckBox.Checked = Utils.GetBool(root, CODE);
			tranCheckBox.Checked = Utils.GetBool(root, TRAN);
			goodsCheckBox.Checked = Utils.GetBool(root, GOODS);
			cardsCheckBox.Checked = Utils.GetBool(root, CARDS);
			chequeCheckBox.Checked = Utils.GetBool(root, CHEQUE);
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

			Utils.AddNode(root, CODE, codeCheckBox.Checked);
			Utils.AddNode(root, TRAN, tranCheckBox.Checked);
			Utils.AddNode(root, GOODS, goodsCheckBox.Checked);
			Utils.AddNode(root, CARDS, cardsCheckBox.Checked);
			Utils.AddNode(root, CHEQUE, chequeCheckBox.Checked);

			doc.Save(settingsFilePath);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		private void ClearValues()
		{
			ucPeriod.SetPeriodMonth();
			ucContractor.Items.Clear();
			ucDiscountCard.Items.Clear();

			codeCheckBox.Checked = false;
			codeCheckBox.Enabled = false;
			tranCheckBox.Checked = false;
			goodsCheckBox.Checked = false;
			cardsCheckBox.Checked = false;
			chequeCheckBox.Checked = false;
		}

		private void FormParams_Load(object sender, EventArgs e)
		{
            ClearValues();
			LoadSettings();
		}

		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

		private void goodsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			codeCheckBox.Enabled = goodsCheckBox.Checked;
		}

    }
}
