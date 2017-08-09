using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using Microsoft.Reporting.WinForms;

namespace Statist4Buy
{
  public partial class ParamsForm : Form, IExternalReport, IReportParams
  {
    public ParamsForm()
    {
      InitializeComponent();
    }

    private string connectionString;
    private string folderPath;
    private const string CACHE_FOLDER = "Cache";

    public void Execute(string connectionString, string folderPath)
    {
      this.connectionString = connectionString;
      this.folderPath = folderPath;
      period.SetPeriodMonth();
      this.MdiParent = AppManager.ClientMainForm;
      AppManager.RegisterForm(this);
      this.Show();
    }

    public string ReportName
    {
      get { return "Статистика для закупок"; }
    }

    public string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.AnalisysReports).Description; }
    }

    private void CreateStoredProc(string connectionString)
    {
      Stream s = this.GetType().Assembly.GetManifestResourceStream("Statist4Buy.REPEX_STATIST_4_BUY.sql");
      StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
      string procScript = sr.ReadToEnd();
      string [] batch = Regex.Split(procScript,"^GO.*$", RegexOptions.Multiline);

      SqlCommand comm = null;
      foreach (string statement in batch)
      {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
          comm = new SqlCommand(statement, con);
          con.Open();
          comm.ExecuteNonQuery();
        }
      }
    }
    
    private void ExtractReport()
    {
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
      if (!Directory.Exists(cachePath))
        Directory.CreateDirectory(cachePath);
      Stream s = this.GetType().Assembly.GetManifestResourceStream("Statist4Buy.Statist4Buy.rdlc");
      StreamReader sr = new StreamReader(s);
      string rep = sr.ReadToEnd();
      string reportPath = Path.Combine(cachePath, "Statist4Buy.rdlc");
      using(StreamWriter sw = new StreamWriter(reportPath))
      {
        sw.Write(rep);
        sw.Flush();
        sw.Close();
      }
      s = this.GetType().Assembly.GetManifestResourceStream("Statist4Buy.Statist4Buy_Sub1.rdlc");
      sr = new StreamReader(s);
      rep = sr.ReadToEnd();
      reportPath = Path.Combine(cachePath, "Statist4Buy_Sub1.rdlc");
      using (StreamWriter sw = new StreamWriter(reportPath))
      {
        sw.Write(rep);
        sw.Flush();
        sw.Close();
      }

      s = this.GetType().Assembly.GetManifestResourceStream("Statist4Buy.Statist4Buy_Sub2.rdlc");
      sr = new StreamReader(s);
      rep = sr.ReadToEnd();
      reportPath = Path.Combine(cachePath, "Statist4Buy_Sub2.rdlc");
      using (StreamWriter sw = new StreamWriter(reportPath))
      {
        sw.Write(rep);
        sw.Flush();
        sw.Close();
      }

      s = this.GetType().Assembly.GetManifestResourceStream("Statist4Buy.Statist4Buy_Sub3.rdlc");
      sr = new StreamReader(s);
      rep = sr.ReadToEnd();
      reportPath = Path.Combine(cachePath, "Statist4Buy_Sub3.rdlc");
      using (StreamWriter sw = new StreamWriter(reportPath))
      {
        sw.Write(rep);
        sw.Flush();
        sw.Close();
      }


    }
    
    private void ClearCache()
    {
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
      if (Directory.Exists(cachePath))
      {
        try
        {
          Utils.ClearFolder(cachePath);
          Directory.Delete(cachePath);
        }
        catch
        {
          
        }
      }
    }

    private string GetOrderExpr()
    {
      foreach (Control c in gbSort.Controls)
      {
        if (!(c is RadioButton)) continue;
        RadioButton rb = (RadioButton)c;
        if (!rb.Checked) continue;
        if (rb == rbGoods)
          return "ORDER BY GOODS ASC";          
        if (rb == rbRem)
          return "ORDER BY REMAIN_CO+REM_APT DESC";
      }      
      return string.Empty;
    }

    private string GetTopNExpr()
    {
      foreach (Control c in gbTopN.Controls)
      {
        if (!(c is RadioButton)) continue;
        RadioButton rb = (RadioButton)c;
        if (!rb.Checked) continue;
        string name = rb.Name.Substring(2, rb.Name.Length - 2);
        int i;
        if (int.TryParse(name, out i))
        {
          return string.Format(" TOP {0} ", i);
        }
        else
          return string.Empty;
      }
      return string.Empty;
    }

    private string GetStoreStirng()
    {
      string storesValues = string.Empty;
      foreach (DataRowItem item in stores.Items)
      {
        if (string.IsNullOrEmpty(storesValues))
          storesValues = item.Text;
        else
          storesValues = string.Format("{0}, {1}", storesValues, item.Text);
      }

      if (string.IsNullOrEmpty(storesValues))
        storesValues = "Все склады";
      return storesValues;
    }

    private XmlDocument GetProcParams()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      Utils.AddNode(root, "DATE_FROM", period.DateFrom);
      Utils.AddNode(root, "DATE_TO", period.DateTo);
      Utils.AddNode(root, "REM_DAYS", (int)nbRemDays.Value);
      Utils.AddNode(root, "ORDER_DAYS", (int)nbOrderDays.Value);
      Utils.AddNode(root, "TOPN", GetTopNExpr());
      Utils.AddNode(root, "ORDER_EXPR", GetOrderExpr());
      foreach (DataRowItem item in stores.Items)
        Utils.AddNode(root, "ID_STORE", item.Id);
      return doc;
    }

    private void bOK_Click(object sender, EventArgs e)
    {
      CreateStoredProc(connectionString);
      ExtractReport();
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
      XmlDocument doc = GetProcParams();

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(cachePath, "Statist4Buy.rdlc");
      rep.LoadData("REPEX_STATIST_4_BUY", doc.InnerXml);
      rep.BindDataSource("Statist4Buy_DS_Table0", 0);
      rep.BindDataSource("Statist4Buy_DS_Table1", 1);
      rep.BindDataSource("Statist4Buy_DS_Table2", 2);

      subReportTable1 = rep.DataSource.Tables[1];
      subReportTable2 = rep.DataSource.Tables[2];

      rep.AddParameter("DATE_FROM", period.DateFrText);
      rep.AddParameter("DATE_TO", period.DateToText);
      rep.AddParameter("STORES", GetStoreStirng());

      rep.ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OnSubReportProc);
      rep.ExecuteReport(this);
    }

    private DataTable subReportTable1;
    private DataTable subReportTable2;

    private void OnSubReportProc(object sender, SubreportProcessingEventArgs e)
    {
      if (Path.GetFileNameWithoutExtension(e.ReportPath) == "Statist4Buy_Sub2")
      {
        e.DataSources.Add(new ReportDataSource("Statist4Buy_DS_Table2", subReportTable2));        
      }
      else
      {
        e.DataSources.Add(new ReportDataSource("Statist4Buy_DS_Table1", subReportTable1));                
      }

    }

    private void bClose_Click(object sender, EventArgs e)
    {
      ClearCache();
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    public bool IsShowPreview
    {
      get { return true; }
    }

    public string HeaderText
    {
      get { return ReportName; }
    }
  }
}