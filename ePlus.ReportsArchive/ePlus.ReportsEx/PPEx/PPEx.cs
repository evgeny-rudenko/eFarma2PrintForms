using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using System.Windows.Forms;

namespace PPEx
{
	public class PPEx : AbstractDocumentPrintForm
	{
		public override string GroupName
		{
			get { return string.Empty; }
		}

		public override string PluginCode
		{
			get { return "PAYMENT_ORDER"; }
		}

		protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_PAYMENT_ORDER", dataRowItem.Id);

			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = reportFiles[0];

			rep.LoadData("REPEX_PAYMENT", doc.InnerXml);
			rep.BindDataSource("PPEx_DS_Table1", 0);
			rep.BindDataSource("PPEx_DS_Table2", 1);


			decimal pvat_sum;
			string pvat_sum_param = string.Empty;
			foreach (DataRow row in rep.DataSource.Tables[0].Rows)
			{
				pvat_sum = Utils.GetDecimal(row, "PVAT_SUM");
				if ((pvat_sum - Math.Truncate(pvat_sum)) == 0)
					pvat_sum_param = Convert.ToString(Math.Truncate(pvat_sum)) + "=";
				else
					pvat_sum_param = Convert.ToString(pvat_sum).Replace(',', '-').Substring(0, Convert.ToString(pvat_sum).Replace(',', '-').Length - 2);
			}

			DataRow dr = rep.DataSource.Tables[1].Rows[0];
			string s = string.Empty;

			if (Convert.ToDecimal(dr[2]) > 0m)
				s = "НДС - " + Convert.ToDecimal(dr[2]).ToString("N2");
			else if (Convert.ToDecimal(dr[0]) == 0m && Convert.ToDecimal(dr[1]) == 0m)
				s = "Без НДС";
			else
			{
				if (Convert.ToDecimal(dr[0]) != 0m)
					s = "НДС (10%) - " + Convert.ToDecimal(dr[0]).ToString("N2") + " руб.";
				if (Convert.ToDecimal(dr[1]) != 0m)
				{
					if (s != "")
						s += ", ";
					s += "НДС (18%) - " + Convert.ToDecimal(dr[1]).ToString("N2") + " руб.";
				}
			}

			string numberPP = null;

			using (PP_Form paramForm = new PP_Form())
			{
				numberPP = paramForm.NumberPP = Utils.GetString(rep.DataSource.Tables[0].Rows[0], "MNEMOCODE");

				if (paramForm.ShowDialog() == DialogResult.OK)
					numberPP = paramForm.NumberPP;
				else
					return null;
			}

			ReportParameter[] p = new ReportParameter[4];
			p[0] = new ReportParameter("SUMM", RusCurrency.Str((double) Utils.GetDecimal(rep.DataSource.Tables[0].Rows[0], "PVAT_SUM")));
			p[1] = new ReportParameter("NDS", s);
			p[2] = new ReportParameter("PVAT_SUM", pvat_sum_param);
			p[3] = new ReportParameter("num", numberPP);
			rep.ReportViewer.LocalReport.SetParameters(p);

			return rep;
		}

		public override string ReportName
		{
			get { return "Платежное поручение"; }
		}
	}
}
