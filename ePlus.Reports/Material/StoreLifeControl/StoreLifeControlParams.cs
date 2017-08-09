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
using ePlus.MetaData.Server;
//using ePlus.Dictionary.Client.Store;


namespace FCRStoreLifeControl
{
	public partial class StoreLifeControlParams : ExternalReportForm, IExternalReportFormMethods
	{
		public StoreLifeControlParams()
		{
			InitializeComponent();
			
		}

		public void Print(string[] reportFiles)
		{
			int days;
			if (!int.TryParse(daysTextBox.Text, out days))
			{
				MessageBox.Show(" оличество должно быть целым положительным числом", "е‘арма", 
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "DAYS", days);			
			storesPluginMultiSelect.AddItems(root, "ID_STORE");
            ucContractors.AddItems(root, "ID_CONTRACTOR");
            ucGoods.AddItems(root, "ID_GOODS");  


			ReportFormNew rep = new ReportFormNew();
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_STORE_LIFE_CONTROL", doc.InnerXml);
			rep.BindDataSource("StoreLifeControl_DS_Table0", 0);
            /*
			ReportParameter[] parameters = new ReportParameter[2] {
				new ReportParameter("store", storesPluginMultiSelect.TextValues()),
				new ReportParameter("days", days.ToString())
			};
          
			rep.ReportViewer.LocalReport.SetParameters(parameters);
            */
            rep.AddParameter("store", storesPluginMultiSelect.TextValues());
            rep.AddParameter("good", ucGoods.TextValues());
            rep.AddParameter("contractor", ucContractors.TextValues());
            rep.AddParameter("days", days.ToString());
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}
        private bool SelfIsCenter()
        {
            bool result = false;
            DataService_BL bl = new DataService_BL();

            using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                result = (bool)command.ExecuteScalar();
            }
            return result;
        }
		private void ClearValues()
		{
			storesPluginMultiSelect.Items.Clear();
            ucContractors.Items.Clear();
            ucGoods.Items.Clear();

			daysTextBox.Text = "14";
		}

		public string ReportName
		{
			get { return " онтроль превышени€ срока нахождени€ товара на складе"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

        private void storesPluginMultiSelect_BeforePluginShow(object sender, CancelEventArgs e)
        {
            if (!SelfIsCenter())
            {
                //storesPluginMultiSelect.PluginContol.ComponentList[2].ToString();
                //MessageBox.Show(storesPluginMultiSelect.PluginContol.ComponentList[1].ToString());
                
                CheckBox ChB = (CheckBox)(((ePlus.CommonEx.Store.UCStoreFilter)storesPluginMultiSelect.PluginContol.ComponentList[1]).Controls["chkSelf"]);
                ChB.Checked = true;// chkSelf = true;
                ChB.Enabled = false;
            }

        }

        private void StoreLifeControlParams_Load(object sender, EventArgs e)
        {
            ClearValues();
            ucGoods.MultiSelect = true;
            ucContractors.MultiSelect = true;
        }
	}
}