using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace TORG29ORNDis
{
    public partial class ORNDisFormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public ORNDisFormParams()
        {
            InitializeComponent();
            if (ucPeriod != null)
            {
                ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
            }
            chbGroupDiscount.Enabled = true;
        }

        public void Print(string[] reportFiles)
        {
            if (mpsContractor.Items.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML");
                Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
                Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
                Utils.AddNode(root, "NO_DETAIL", chkShortReport.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_ADD", chkShowAdd.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_SUB", chkShowSub.Checked ? "1" : "0");
                Utils.AddNode(root, "SORT_DOC", rbDocType.Checked ? "1" : "0");
                Utils.AddNode(root, "SHOW_RETURN", chbShowReturn.Checked ? "1" : "0");
                foreach (DataRowItem dr in mpsContractor.Items)
                    Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

                foreach (DataRowItem dr in mpsStore.Items)
                    Utils.AddNode(root, "ID_STORE", dr.Id);
                Utils.AddNode(root, "REFRESH_DOC_MOV", chkRefreshDocMov.Checked ? "1" : "0");

                ReportFormNew rep = new ReportFormNew();

                if (chbGroupDiscount.Checked && rbDocType.Checked)
                {
                    rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TORG29_NAL_DIS_GROUP_EX.rdlc"); //reportFiles[2]
                    rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);

                    DataService_BL bl = new DataService_BL();
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
                    {
                        using (SqlCommandEx comm = new SqlCommandEx("REP_GOODS_REPORTS_DISCOUNT_GROUP_EX", conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
                            SqlDataAdapter sqlda = new SqlDataAdapter(comm.SqlCommand);
                            sqlda.Fill(ds);
                        }
                    }
                    subReportTable = ds.Tables[0];
                }
                else
                {
//                    if (rbDocType.Checked)
                    rep.ReportPath = rbDocDate.Checked ? Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TORG29_NAL_BY_DATE_WITH_DIS_EX.rdlc") : Path.Combine(Path.GetDirectoryName(reportFiles[0]), "TORG29_NAL_WITH_DIS_EX.rdlc");//reportFiles[3] : reportFiles[0];
//                    else rep.ReportPath = reportFiles[1];
                }

				if (serviceCheckBox.Checked)
				{
					rep.LoadData("REP_GOODS_REPORTS_NAL_WITH_DIS_EX_SERVICE", doc.InnerXml);
				}
				else
				{
					rep.LoadData("REP_GOODS_REPORTS_NAL_WITH_DIS_EX", doc.InnerXml);
				}

                rep.BindDataSource("GoodsReportsNal_DS_dtBegin1", 0);
                rep.BindDataSource("GoodsReportsNal_DS_dtAdd1", 1);
                rep.BindDataSource("GoodsReportsNal_DS_dtSub1", 2);
                rep.BindDataSource("GoodsReportsNal_DS_dtEnd1", 3);
                rep.BindDataSource("GoodsReportsNal_DS_dtContractor", 4);

                rep.AddParameter("date_fr", ucPeriod.DateFrText);
                rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
                rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
                rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
                if (!(chbGroupDiscount.Checked && rbDocType.Checked))
                {
                    rep.AddParameter("show_date", chkDateReport.Checked ? "1" : "0");
                    rep.AddParameter("show_discount", chkColumnSale.Checked ? "1" : "0");
                }
                rep.ExecuteReport(this);
            }
            else MessageBox.Show("Выберите контрагента!");
        }

        DataTable subReportTable;
        private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DiscountGroupSub_DS_Table0", subReportTable));
        }

        public string ReportName
        {
            get { return "Торг 29 Опт-розница-наложение (со скидкой)"; }
        }

        private void rbDocDate_CheckedChanged(object sender, EventArgs e)
        {
            chbGroupDiscount.Enabled = false;
        }

        private void rbDocType_CheckedChanged(object sender, EventArgs e)
        {
            chbGroupDiscount.Enabled = true;
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
            }
        }
    }
}