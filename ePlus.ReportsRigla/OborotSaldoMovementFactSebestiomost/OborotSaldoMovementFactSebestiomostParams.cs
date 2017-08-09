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
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace OborotSaldoMovementFactSebestiomost
{
	public partial class OborotSaldoMovementFactSebestiomostParam : ExternalReportForm, IExternalReportFormMethods
	{
		public OborotSaldoMovementFactSebestiomostParam()
		{
			InitializeComponent();
			if (ucPeriod != null)
			{
				ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
				ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
			}
		}

		public void Print(string[] reportFiles)
		{
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode( doc, "XML", null );
			ucPeriod.AddValues(root);			
			XmlNode goodsNode = Utils.AddNode(root, "GOODS", null);
			XmlNode storeNode = Utils.AddNode(root, "STORE", null);
			Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
			foreach (DataRowItem dri in ucGoods.Items)
				Utils.AddNode(goodsNode, "ID_GOODS", dri.Id);
			foreach (DataRowItem dri in ucStore.Items)
				Utils.AddNode(storeNode, "ID_STORE", dri.Id);
            ReportFormNew rep = new ReportFormNew(); 
			rep.ReportPath = reportFiles[0];
			rep.LoadData("REP_OBOROT_SALDO_MOVEMENT_FACT_SEBESTIOMOST", doc.InnerXml);
			rep.BindDataSource("OborotSaldoMovementFactSebestiomost_DS_Table", 0);
			rep.AddParameter("TITLE", string.Format("ОБОРОТНО-САЛЬДОВАЯ ВЕДОМОСТЬ ПО ДВИЖЕНИЮ ТОВАРА ЗА {0} ПО {1} ПО ФАКТИЧЕСКОЙ СЕБЕСТОИМОСТИ",
				ucPeriod.DateFrom.ToString("dd.MM.yyyy"), 
				ucPeriod.DateTo.ToString("dd.MM.yyyy")));
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			rep.ExecuteReport(this);
		}

		public string ReportName
		{
			get { return "Оборотно-Сальдовая ведомость по фактической себестоимости"; }
		}

		private void SetDefaultValues()
		{
			ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
			ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
		}

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.AccountingReports).Description;
            }
        }

		private void OborotSaldoMovementFactSebestiomostParam_Load(object sender, EventArgs e)
		{

		}

	}
}