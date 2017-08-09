using System;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.IO;

namespace RCSMarginControl_Rigla
{
	public partial class MarginControlParams : ExternalReportForm, IExternalReportFormMethods
	{
		private string settingsFilePath = Path.Combine(Utils.TempDir(), "MarginControl.xml");
		private const string MARGIN = "Margin";
		private const string IMPORTANT = "Important";

		public MarginControlParams()
		{
			InitializeComponent();
		}

		public void Print(string[] reportFiles)
		{
			int margin;

			if (!int.TryParse(marginTextBox.Text, out margin))
			{
				MessageBox.Show("Количество должно быть целым положительным числом", "еФарма", 
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucContractors.AddItems(root, "ID_CONTRACTOR");
			ucGoods.AddItems(root, "ID_GOODS");
			foreach (CatalogItem item in ucGoodsGroup.Items)
			{
				Utils.AddNode(root, "ID_GOODS_GROUP", item.Id);
			}

			Utils.AddNode(root, "MARGIN", margin);
			Utils.AddNode(root, "IMPORTANT", importantCheckBox.Checked ? "1" : "0");			
			
			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_MARGIN_CONTROL", doc.InnerXml);
			rep.BindDataSource("MarginControl_DS_Table0", 0);
			rep.BindDataSource("MarginControl_DS_Table1", 1);			

			rep.AddParameter("goods", ucGoods.TextValues());
			rep.AddParameter("groups", Utils.GetString(rep.DataSource.Tables[1].Rows[0]["GOODS_GROUPS"]));
			rep.AddParameter("important", importantCheckBox.Checked ? "есть" : "нет");
			rep.AddParameter("margin", margin.ToString());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucContractors.Items.Clear();
			ucGoods.Items.Clear();
			ucGoodsGroup.Clear();
			marginTextBox.Text = "40";
			importantCheckBox.Checked = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

		public string ReportName
		{
			get { return "Отчет по контролю наценок товаров"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

			marginTextBox.Text = Utils.GetString(root, MARGIN);
			importantCheckBox.Checked = Utils.GetBool(root, IMPORTANT);
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

			Utils.AddNode(root, MARGIN, marginTextBox.Text);
			Utils.AddNode(root, IMPORTANT, importantCheckBox.Checked);

			doc.Save(settingsFilePath);
		}

        private void MarginControlParams_Load(object sender, EventArgs e)
        {
            ClearValues();
        }

	}
}