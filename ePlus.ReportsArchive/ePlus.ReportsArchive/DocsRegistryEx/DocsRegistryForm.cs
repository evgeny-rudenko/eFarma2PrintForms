using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;

namespace DocsRegistryEx
{  
    public partial class DocsRegistryForm : Form, IReportParams, IExternalReport 
    {
    public DocsRegistryForm()
    {
      InitializeComponent();     
    }

    public bool IsShowPreview
    {
	    get { return true; }
    }

    public string HeaderText
    {
	    get { return ReportName; }
    }

    public string ReportName
    {
	    get { return "–ÂÂÒÚ ‰ÓÍÛÏÂÌÚÓ‚"; }
    }

    public string GroupName
    {
	    get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

      private void ExtractReport()
      {
	      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
	      if (!Directory.Exists(cachePath))
		      Directory.CreateDirectory(cachePath);
	      Stream s = this.GetType().Assembly.GetManifestResourceStream("DocsRegistryEx.DocsRegistryEx.rdlc");
	      StreamReader sr = new StreamReader(s);
	      string rep = sr.ReadToEnd();
	      string reportPath = Path.Combine(cachePath, "DocsRegistryEx.rdlc");
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

      private void CreateStoredProc(string connectionString)
      {
	      Stream s = this.GetType().Assembly.GetManifestResourceStream("DocsRegistryEx.DocsRegistryEx.sql");
	      StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251));
	      string procScript = sr.ReadToEnd();
	      string[] batch = Regex.Split(procScript, "^GO.*$", RegexOptions.Multiline);

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

      private string connectionString;
      private string folderPath;
      private const string CACHE_FOLDER = "Cache";

      public void Execute(string connectionString, string folderPath)
      {
	      this.connectionString = connectionString;
	      this.folderPath = folderPath;
	      this.MdiParent = AppManager.ClientMainForm;
	      AppManager.RegisterForm(this);
	      this.Show();
      }

      private string fileName = Path.Combine(Utils.TempDir(), "DocsRegistrySettings.xml");

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public void Init()
    {
    }

    private void btnReport_Click(object sender, EventArgs e)
    {
      CreateStoredProc(connectionString);
      ExtractReport();

      XmlDocument doc = new XmlDocument(); 
      XmlNode root = Utils.AddNode(doc, "XML", null);
      XmlNode docStateNode = Utils.AddNode(root, "DOC_STATE");	 
      foreach (DOC_STATE state in  chkLbDocStatÂ.CheckedItems)
      {
        XmlNode stateNode = Utils.AddNode(docStateNode, "STATE");
        Utils.AddNode(stateNode, "CODE", state.CODE);
      }
      XmlNode docType = Utils.AddNode(root, "DOC_TYPE");
      foreach (TABLE_DATA td in ucDocuments.SelectedList)
      {
        XmlNode typeNode = Utils.AddNode(docType, "TYPE");
        Utils.AddNode(typeNode, "ID_TABLE_DATA", td.ID_TABLE_DATA);
      }

      XmlNode periodNode = Utils.AddNode(root, "PERIOD");
      Utils.AddNode(periodNode, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(periodNode, "DATE_TO", ucPeriod.DateTo);

      XmlNode storesNode = Utils.AddNode(root, "STORES");
      foreach (DataRowItem dri in ucStore.Items)
      {
        XmlNode storeNode = Utils.AddNode(storesNode, "STORE");
        Utils.AddNode(storeNode, "ID_STORE", dri.Id);
      }

      XmlNode contractorsNode = Utils.AddNode(root, "CONTRACTORS");
      foreach (DataRowItem dri in ucContractor.Items)
      {
        XmlNode contractorNode = Utils.AddNode(contractorsNode, "CONTRACTOR");
        Utils.AddNode(contractorNode, "ID_CONTRACTOR", dri.Id);
      }

      XmlNode goodsListNode = Utils.AddNode(root, "GOODS_LIST");
      foreach (DataRowItem dri in ucGoods.Items)
      {
        XmlNode goodsNode = Utils.AddNode(goodsListNode, "GOODS");
        Utils.AddNode(goodsNode, "ID_GOODS", dri.Id);
      }
      string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(cachePath, "DocsRegistryEx.rdlc");
      rep.LoadData("REPEX_DOCSREGISTRY", doc.InnerXml);
      rep.BindDataSource("DocsRegistry_DS_Table", 0);

      rep.AddParameter("DATE_FROM", ucPeriod.DateFrText);
      rep.AddParameter("DATE_TO", ucPeriod.DateToText);
      rep.AddParameter("STORE", ucStore.TextValues());
      rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
      rep.AddParameter("GOODS", ucGoods.TextValues());
      
      foreach (SHOW_PARAM sp in (List<SHOW_PARAM>)chkLbShowParams.DataSource)
      {
        if (chkLbShowParams.CheckedItems.IndexOf(sp)== -1)
        {
          rep.AddParameter(sp.CODE, "0");        
        }
        else
        {
          rep.AddParameter(sp.CODE, "1");                  
        }
      }
      rep.ExecuteReport(this);
    }

      private void DocsRegistryForm_Load(object sender, EventArgs e)
      {
	      DataService_BL bl = new DataService_BL();
	      DataSet ds = bl.Execute("EXEC USP_EX_TABLE_DATA_LIST");
	      SqlLoader<TABLE_DATA> tdLoader = new SqlLoader<TABLE_DATA>();
	      List<TABLE_DATA> list = tdLoader.GetList(ds.Tables[0]);

	      ucDocuments.DescriptionMember = "DESCRIPTION";
	      ucDocuments.DisplayMember = "DESCRIPTION";
	      ucDocuments.KeyFieldName = "ID_TABLE_DATA";
	      ucDocuments.SearchMember = "DESCRIPTION";
	      ucDocuments.DataSource = list;

	      chkLbDocStatÂ.DataSource = new List<DOC_STATE>(new DOC_STATE[]{new DOC_STATE("SAVE", "—Óı‡ÌÂÌ"), 
                                                                     new DOC_STATE("PROC", "ŒÚ‡·ÓÚ‡Ì"),
                                                                     new DOC_STATE("DEL", "”‰‡ÎÂÌ")});

	      chkLbShowParams.DataSource = new List<SHOW_PARAM>(new SHOW_PARAM[]{new SHOW_PARAM("SHOW_SUM_SUP", "ŒÔÚ. ÒÛÏÏ‡"),
                                                                         new SHOW_PARAM("SHOW_SVAT_SUP", "—ÛÏÏ‡ Õƒ— ÔÓÒÚ."),
                                                                         new SHOW_PARAM("SHOW_TOTAL", "»ÚÓ„Ë") });
	      SetDefaultValues();
      }

      protected void SetDefaultValues()
      {
	      if (ucPeriod != null)
	      {
		      ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
		      ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
	      }
      }

      private void toolStripButton1_Click(object sender, EventArgs e)
      {
	      SetDefaultValues();
      }
    }

    internal class TABLE_DATA
    {
      private long id_table_data;
      private string description;

      public long ID_TABLE_DATA
      {
	      get { return id_table_data; }
	      set { id_table_data = value; }
      }

      public string DESCRIPTION
      {
	      get { return description; }
	      set { description = value; }
      }
    }
    internal class DOC_STATE
    {
      private string code;
      private string name;

      public string CODE
      {
	      get { return code; }
	      set { code = value; }
      }

      public string NAME
      {
	      get { return name; }
	      set { name = value; }
      }
      public DOC_STATE(string code, string name)
      {
	      this.code = code;
	      this.name = name;
      }
      public override string ToString()
      {
	      return name;
      }
    }
    internal class SHOW_PARAM
    {
      private string code;
      private string name;

      public string CODE
      {
	      get { return code; }
	      set { code = value; }
      }

      public string NAME
      {
	      get { return name; }
	      set { name = value; }
      }
      public SHOW_PARAM(string code, string name)
      {
	      this.code = code;
	      this.name = name;
      }
      public override string ToString()
      {
	      return name;
      }

    }
}