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

namespace RCBTorg29ORNDis
{
  public partial class ORNDisFormParams : ExternalReportForm, IExternalReportFormMethods
  {
    public ORNDisFormParams()
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

    /// <summary>
    /// Метод для получения подтверждения или опровержения того, что МЫ является центром
    /// </summary>
    /// <returns>true - ЦЕНТР, false - не ЦЕНТР</returns>
    private bool SelfIsCenter()
    {
        //UCMetaPluginSelect f = new UCMetaPluginSelect();
        //f.SetId(70);
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

    /*private bool Sort_By_DocType
    {
      get { return rbDocType.Checked && !rbDocDate.Checked; }
      set
      {
        rbDocType.Checked = value;
        rbDocDate.Checked = !value;
      }
    }*/

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
        Utils.AddNode(root, "NOAU", auCheckBox.Checked ? "0" : "1");
        Utils.AddNode(root, "CO", SelfIsCenter());
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
        rep.BindDataSource("GoodsReportsNal_DS_Table0", 5);

        rep.AddParameter("date_fr", ucPeriod.DateFrText);
        rep.AddParameter("date_to", ucPeriod.DateToText);
        rep.AddParameter("no_detail", chkShortReport.Checked ? "1" : "0");
        rep.AddParameter("show_add", chkShowAdd.Checked ? "1" : "0");
        rep.AddParameter("show_sub", chkShowSub.Checked ? "1" : "0");
        rep.AddParameter("service", serviceCheckBox.Checked ? "1" : "0");
        //rep.AddParameter("DOCTYPE_GROUP", this.Sort_By_DocType ? "1" : "0");
        rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
        rep.AddParameter("SHOW_SUM_BY_DOCTYPE", this.chkShowSumByDocType.Visible && this.chkShowSumByDocType.Checked ? "1" : "0");
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
      chbGroupDiscount.Enabled = rbDocType.Checked;
      chkShowSumByDocType.Enabled = rbDocType.Checked;
      if (!chkShowSumByDocType.Enabled)
        chkShowSumByDocType.Checked = false;
    }

    public override string GroupName
    {
      get
      {
        return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
      }
    }

    private void ClearValues()
    {
      ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
      ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
      mpsContractor.Items.Clear();
      mpsStore.Items.Clear();
      rbDocType.Checked = true;
      chkShowSumByDocType.Checked = false;
      chkShowAdd.Checked = true;
      chkShowSub.Checked = true;
      chbShowReturn.Checked = false;
      chkShortReport.Checked = false;
      chkRefreshDocMov.Checked = false;
      chkColumnSale.Checked = true;
      chkDateReport.Checked = true;
      chbGroupDiscount.Checked = false;
      serviceCheckBox.Checked = false;
      auCheckBox.Checked = false;
    }

    private void LoadSettings()
    {
        //mpsContractor.BeforePluginShow -= new ContractsBeforePluginShowCancelEventHandler(contractsSelector_BeforePluginShow);

      ClearValues();
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

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");

      XmlNodeList contractors = root.SelectNodes("CONTRACTORS");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        mpsContractor.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      XmlNodeList stores = root.SelectNodes("STORES");
      foreach (XmlNode node in stores)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        mpsStore.AddRowItem(new DataRowItem(id, guid, code, text));
      }

      if (Utils.GetBool(root, "DocType"))
        rbDocType.Checked = true;
      else
        rbDocDate.Checked = true;

      chkShowSumByDocType.Checked = Utils.GetBool(root, "ShowSumByDocType");
      chkShowAdd.Checked = Utils.GetBool(root, "ShowAdd");
      chkShowSub.Checked = Utils.GetBool(root, "ShowSub");
      chbShowReturn.Checked = Utils.GetBool(root, "ShowReturn");
      chkShortReport.Checked = Utils.GetBool(root, "ShortReport");
      chkRefreshDocMov.Checked = Utils.GetBool(root, "RefreshDocMov");
      chkColumnSale.Checked = Utils.GetBool(root, "ColumnSale");
      chkDateReport.Checked = Utils.GetBool(root, "DateReport");
      chbGroupDiscount.Checked = Utils.GetBool(root, "GroupDiscount");
      serviceCheckBox.Checked = Utils.GetBool(root, "Service");
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

      foreach (DataRowItem dri in mpsContractor.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTORS");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      foreach (DataRowItem dri in mpsStore.Items)
      {
        XmlNode node = Utils.AddNode(root, "STORES");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      Utils.AddNode(root, "DocType", rbDocType.Checked);
      Utils.AddNode(root, "ShowSumByDocType", chkShowSumByDocType.Checked);
      Utils.AddNode(root, "ShowAdd", chkShowAdd.Checked);
      Utils.AddNode(root, "ShowSub", chkShowSub.Checked);
      Utils.AddNode(root, "ShowReturn", chbShowReturn.Checked);
      Utils.AddNode(root, "ShortReport", chkShortReport.Checked);
      Utils.AddNode(root, "RefreshDocMov", chkRefreshDocMov.Checked);
      Utils.AddNode(root, "ColumnSale", chkColumnSale.Checked);
      Utils.AddNode(root, "DateReport", chkDateReport.Checked);
      Utils.AddNode(root, "GroupDiscount", chbGroupDiscount.Checked);
      Utils.AddNode(root, "Service", serviceCheckBox.Checked);
      Utils.AddNode(root, "AU", auCheckBox.Checked);

      doc.Save(SettingsFilePath);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      ClearValues();
    }

    private void ORNDisFormParams_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void ORNDisFormParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }

    private void mpsContractor_BeforePluginShow(object sender, CancelEventArgs e)
    {
        mpsContractor.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", @"(C.ID_CONTRACTOR in(select ID_CONTRACTOR from dbo.CONTRACTOR_2_CONTRACTOR_GROUP where ID_CONTRACTOR_GROUP = (select TOP 1 ID_CONTRACTOR_GROUP from dbo.CONTRACTOR_GROUP where MNEMOCODE='CLIENT')))");
        mpsContractor.PluginContol.Caption = "Аптеки";
        //((ePlus.Dictionary.Client.Contractor.ContactorFilterComponent)mpsContractor.PluginContol.ComponentList[2]).Self = true;
    }

    private void mpsStore_BeforePluginShow(object sender, CancelEventArgs e)
    {
      if (mpsContractor.Items.Count > 0)
      {
        string stores = string.Empty;
        foreach (DataRowItem dri in mpsContractor.Items)
        {
          stores = String.IsNullOrEmpty(stores) ? dri.Id.ToString() : stores + "," + dri.Id.ToString();
        }
        if (!String.IsNullOrEmpty(stores))
          mpsStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER",
            String.Format("(STORE.ID_CONTRACTOR IN ({0}))", stores));
      }
      else
      {
        e.Cancel = true;
        MessageBox.Show("Выберите контрагента!");
      }
  }
  }
}