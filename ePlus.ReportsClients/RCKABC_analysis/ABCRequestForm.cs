using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using ePlus.Dictionary.Client;
using ePlus.MetaData;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using RCKABC_analysis.Controls;
using Microsoft.Reporting.WinForms;


namespace RCKABC_analysis
{
  public partial class RequestCalculationForm : ExternalReportForm, IExternalReportFormMethods
  {
	AllCategoryControl allCategoryControl;
    ToolStripMenuItem tsmiClearFixed = new ToolStripMenuItem();
    ToolStripMenuItem tsmiDelItem = new ToolStripMenuItem();
    ToolStripMenuItem tsmiRefreshItem = new ToolStripMenuItem();        
    

    List<IRequestParamMethods> categories;

    List<RequestCategoryTag> tags;

    EEditMode editMode = EEditMode.Add;
    RequestCalculationParams requestParams = new RequestCalculationParams();
    public RequestCalculationForm()
    {
      InitializeComponent();
	  //if (!Utils.IsDesignMode(this))
	  //  LicenseChecker.IsModuleLicensed(LicensedModules.Request);
      //bl = new REQUEST_CALCULATION_BL();

	  //tecItems.BindingSource.CurrentChanged += new EventHandler(bindingSource1_CurrentChanged);
	  //tecItems.DataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(OnCellFormatting);
	  //tecItems.GridCellBeginEdit += new DataGridViewCellCancelEventHandler(OnCellBeginEdit);
	  //tecItems.GridCellEndEdit  += new DataGridViewCellEventHandler(OnCellEndEdit);
	  //tecItems.DataGridView.CellDoubleClick += new DataGridViewCellEventHandler(OnCellDoubleClick);
	  //tecItems.DataGridView.DataError += new DataGridViewDataErrorEventHandler(OnDataError);
 

    }

    private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      e.ToString();
    }


    decimal needs;


    private void OnCategoryClicked(object sender, EventArgs e)
    {
      foreach (TreeNode tn in tvParams.Nodes[0].Nodes)
      {
        if (tn.Text == ((CategoryDescriptionControl)sender).CategoryName)
        {
          tvParams.SelectedNode = tn;
          break;
        }        
      }
    }


    private void SetControlsState()
    {
      bool enabled = true;      
      beContractor.Enabled = enabled;
 //     beNumber.Enabled = enabled;
      dtpDate.Enabled = enabled;
//      bLoad.Enabled = enabled;
//      bApply.Enabled = enabled;

      bOK.Enabled = enabled;

    }

    private void SaveCurrentParam()
    {
      if (tvParams.SelectedNode == null || tvParams.SelectedNode.Parent == null) return;
      SaveParams((RequestCategoryTag)tvParams.SelectedNode.Tag);
    }


    #region Category
    private void InitCategories()
    {
      tvParams.Nodes[0].Nodes.Clear();
      tags = RequestCategoryTag.InitTagList(categories, requestParams.Parameters);
      
      allCategoryControl.Init(tags);
      foreach (RequestCategoryTag tag in tags)
      {
        TreeNode tn = new TreeNode(tag.Category);
        tn.Tag = tag;
        tvParams.Nodes[0].Nodes.Add(tn);
      }
      if (tvParams.Nodes[0].Nodes.Count > 0)
      {
        tvParams.SelectedNode = tvParams.Nodes[0].Nodes[0];
      }
      tvParams.ExpandAll();
    }
    private void tvParams_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Node == null || e.Node.Parent == null) InitParams(null);
      InitParams((RequestCategoryTag)e.Node.Tag);
    }
    private void InitParams(RequestCategoryTag tag)
    {
      pParams.Controls.Clear();
      if (tag == null)
      {
      //  bLoad.Visible = true;
      //  bApply.Text = "Применить";
        allCategoryControl.RefreshParams();
        pParams.Controls.Add(allCategoryControl);
        return;
      }
      else
      {
        foreach (IRequestParamMethods control in tag.Controls)
        {
          control.ClearData();
          control.Object2Control(tag.Params.ToArray());
          control.Control.Dock = DockStyle.Top;
          control.Control.Enabled = true;
          pParams.Controls.Add(control.Control);
        }
        bool isDb = false;
        foreach (IRequestParam param in tag.Params)
        {
          if (param.ParamType == RequestParameterType.Db)
          {
            isDb = true;
            break;
          }
        }
  //      bLoad.Visible = false;
  //      bApply.Text = isDb ? "Загрузить" : "Применить";
//        panel4.Visible = true;
      }
    }
    private void tvParams_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      if (tvParams.SelectedNode == null || tvParams.SelectedNode.Parent == null) return;
      SaveParams((RequestCategoryTag)tvParams.SelectedNode.Tag);
    }
    private void SaveParams(RequestCategoryTag tag)
    {
      if (tag == null) return;
      foreach (IRequestParamMethods control in tag.Controls)
      {
        control.Control2Object(tag.Params.ToArray());
      }
    }
    #endregion

	  private void bApply_Click(object sender, EventArgs e)
	  {
		  //SaveCurrentParam();
		  //List<IRequestParam> paramList;
		  //if (tvParams.SelectedNode.Parent == null && tvParams.SelectedNode.Tag == null)
		  //{
		  //    paramList = requestParams.Parameters;
		  //}
		  //else
		  //{
		  //    RequestCategoryTag tag = (RequestCategoryTag)tvParams.SelectedNode.Tag;
		  //    paramList = tag.Params;
		  //}
		  //bool needLoad = false;
		  //foreach (IRequestParam param in paramList)
		  //{
		  //    if (param.ParamType == RequestParameterType.Db)
		  //    {
		  //        needLoad = true;
		  //        break;
		  //    }
		  //}
		  //if (sender == bApply && tvParams.SelectedNode.Parent == null && tvParams.SelectedNode.Tag == null)
		  //    needLoad = false;

		  //if (needLoad)
		  //    LoadItems();
		  //RecalcItems();
	  }

    private void LoadItems()
    {
	  //XmlDocument doc = parameters.MakeDbXml();

	  //using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
	  //{
	  //    SqlCommand selectCommand = new SqlCommand("USP_REQUEST_CALCULATION_CALC_NEEDS", conn);
	  //    selectCommand.CommandTimeout = 0;
	  //    selectCommand.CommandType = CommandType.StoredProcedure;
	  //    selectCommand.Parameters.Add(new SqlParameter("@XMLPARAM", SqlDbType.NText)).Value = doc.InnerXml;
	  //    selectCommand.Parameters.Add(new SqlParameter("@ID_CONTRACTOR", SqlDbType.BigInt)).Value = id_contractor;
	  //    selectCommand.Parameters.Add(new SqlParameter("@DATE_TO", SqlDbType.DateTime)).Value = date_to;
	  //    conn.Open();
	  //    SqlDataReader reader = selectCommand.ExecuteReader();
	  //}
    }

    public void RecalcItems()
    {
    }

    private void CalculateSummary()
    {
    }

      private void Form1_Load(object sender, EventArgs e)
      {
          requestParams.Parameters.AddRange(
new IRequestParam[]{new DaysPeriodParam(), 
                            new StoreParam(), 
                            new SubOpTypeParam(),
                            new GoodsKindParam(),
                            new GoodsClassifierParam(),
                            new AbcGroupParam(),
                          //  new SalesSpeedCalcParam(),
                          //  new NeedsCalcParam()
                            });

          categories = new List<IRequestParamMethods>(
            new IRequestParamMethods[]{
                                    new CommonCategoryControl(),
                                    new GoodsKindCategoryControl(),
                                    new GroupByClassifierControl(),
                                    new AbcGroupCategoryControl(),
                            //        new SalesSpeedCategoryControl(),
                            //        new NeedsCategoryControl()
                                  });
          allCategoryControl = new AllCategoryControl();
          allCategoryControl.Dock = DockStyle.Fill;
          allCategoryControl.CategoryClicked += new EventHandler(OnCategoryClicked);
          InitCategories();
      }


    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveCurrentParam();
      XmlDocument doc = requestParams.ToXml();
      doc.Save(Utils.AppDir("RequestParams.xml"));
    }

    private void bClose_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void bOK_Click(object sender, EventArgs e)
    {
      if (!Save()) return;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private bool Save()
    {
		return true;
    }


    private void Control2Object()
    {
    }

    private void bSave_Click(object sender, EventArgs e)
    {
      Save();
    }

  //  GoodsSelectWrapper gsw = null;
	//private void searchPanel_BeforePluginShow(object sender, ePlus.Client.Core.Controls.BeforePluginShowEventArgs e)
	//{
	//  SaveCurrentParam();
	//  gsw = (GoodsSelectWrapper)e.PluginWrapper;       
	//  GoodsKindParam p = requestParams.GetParameter(typeof(GoodsKindParam)) as GoodsKindParam;
	//  if (p==null) return;
	//  if (p.GoodsKinds.Count==0) return;
      
	//  gsw.Plugin.Grid(0).SetParameterValue("@ADV_FILTER", string.Format("ID_GOODS_KIND IN ({0})", p.GetFilterString()));
	//}


    public void FocusItem(Guid idGoods)
    {

    }

    private void dtpDate_ValueChanged(object sender, EventArgs e)
    {

    }

	  private void RequestCalculationForm_Shown(object sender, EventArgs e)
	  {
		  if (dtpDate.Value.Date < DateTime.Now.Date &&
		  MessageBox.Show("Текущая дата отличается от даты расчета\r\nИзменить дату рассчета на текущую?",
					  "еФарма 2",
					  MessageBoxButtons.YesNo,
					  MessageBoxIcon.Question,
					  MessageBoxDefaultButton.Button1) == DialogResult.Yes)
		  {
			  dtpDate.Value = DateTime.Now;
		  }
		  foreach (UserControl var in categories)
		  {
			  //if (var is AbcGroupCategoryControl)
			  //{
			  //    ((AbcGroupCategoryControl)var).nbAGroup.Value = 75;
			  //    ((AbcGroupCategoryControl)var).nbAGroup.Text = "75";
			  //    ((AbcGroupCategoryControl)var).nbBGroup.Value = 20;
			  //    ((AbcGroupCategoryControl)var).nbBGroup.Text = "20";
			  //}
			  if (var is CommonCategoryControl)
			  {
				  ((CommonCategoryControl)var).nbQty.Value = 31;
				  ((CommonCategoryControl)var).nbQty.Text = "31";
			  }
			  //if (var is GroupByClassifierControl)
			  //{
			  //    ((GroupByClassifierControl)var).chkGroup.Checked = true;
			  //    ((GroupByClassifierControl)var).chkSort.Checked = false;
			  //}
			  if (var is CommonCategoryControl)
			  {
				  ((CommonCategoryControl)var).chkCheque.Checked = true;
				  ((CommonCategoryControl)var).chkInvoiceOut.Checked = true;
				  ((CommonCategoryControl)var).chkMOVE.Checked = true;
			  }
		  }
		    SaveCurrentParam();
	  }

//==================================================
	public void Print(string[] reportFiles)
	{
		//XmlDocument doc = new XmlDocument();
		//XmlNode root = Utils.AddNode(doc, "XML");
		//Utils.AddNode(root, "DATE_FROM", periodPeriod.DateFrom);
		//Utils.AddNode(root, "DATE_TO", periodPeriod.DateTo);
		//Utils.AddNode(root, "SHOW_LOTS", showLotsCheckBox.Checked);

		//storesPluginMultiSelect.AddItems(root, "STORE");
		SaveCurrentParam();
		RequestCalculationParams paramList = new RequestCalculationParams();
		//List<IRequestParam> paramList;
		//if (tvParams.SelectedNode.Parent == null && tvParams.SelectedNode.Tag == null)
		//{
		int itemp =0;
		string stemp ="";
		bool GroupbyABC = true;
		bool GroupbyABCSort = false;
		paramList = requestParams;
		AbcGroupParam AbcParam = new AbcGroupParam();
		foreach (UserControl var in categories)
		{
			if (var is AbcGroupCategoryControl)
			{				
				((AbcGroupCategoryControl)var).Control2Object(AbcParam);
			}
			if (var is CommonCategoryControl)
			{
				itemp = (int)((CommonCategoryControl)var).nbQty.Value;				
				stemp = string.IsNullOrEmpty(((CommonCategoryControl)var).mpsStore.ToCommaDelimetedStringList()) ? "Все склады"
					: ((CommonCategoryControl)var).mpsStore.ToCommaDelimetedStringList();
			}
			if (var is GroupByClassifierControl)
	        {
	            GroupbyABC =((GroupByClassifierControl)var).chkGroup.Checked;
				GroupbyABCSort = ((GroupByClassifierControl)var).chkSort.Checked;
			}
		}
			
		//}
		//else
		//{
		//    RequestCategoryTag tag = (RequestCategoryTag)tvParams.SelectedNode.Tag;
		//    //paramList = tag.Params;
		//    paramList.Parameters.AddRange(tag.Params); 
		//}
		XmlDocument doc = paramList.MakeDbXml();
		Utils.AddNode(doc.DocumentElement, "ID_CONTRACTOR", beContractor.Id == 0 ? 1 : beContractor.Id);
		Utils.AddNode(doc.DocumentElement, "DATE_TO", dtpDate.Value.ToString());
		Utils.AddNode(doc.DocumentElement, "DATE_TO_OLD", dtpDate.Value.AddDays(-1 * itemp).ToShortDateString());
		Utils.AddNode(doc.DocumentElement, "DATE_TO_OLD2", dtpDate.Value.AddDays(-1 * 2*itemp).ToShortDateString());
		if (GroupbyABCSort)
		{
			Utils.AddNode(doc.DocumentElement, "SORTKIND", "SORTNAME");
		}
		else
		{
			Utils.AddNode(doc.DocumentElement, "SORTKIND", "SORTAIMPERC");
		}
		//Utils.AddNode(doc.DocumentElement, "TYPEGROUP", dtpDate.Value.ToString());
		//Utils.AddNode(doc.DocumentElement, "APERC", dtpDate.Value.ToString());
		//Utils.AddNode(doc.DocumentElement, "BPERC", dtpDate.Value.ToString());
		//Utils.AddNode(doc.DocumentElement, "CPERC", dtpDate.Value.ToString());
		AbcParam.ToXml(doc.DocumentElement);
		//bool needLoad = false;
		//foreach (IRequestParam param in paramList)
		//{
		//    if (param.ParamType == RequestParameterType.Db)
		//    {
		//        needLoad = true;
		//        break;
		//    }
		//}
		//if (sender == bApply && tvParams.SelectedNode.Parent == null && tvParams.SelectedNode.Tag == null)
		//    needLoad = false;

		//if (needLoad)
		//    LoadItems();


		ReportFormNew rep = new ReportFormNew();
		if (GroupbyABC)
		{
			rep.ReportPath = reportFiles[1];
		}
		else
		{
			rep.ReportPath = reportFiles[0];
		}		
		rep.LoadData("REP_ABC_REQUEST_CALCULATION_CALC_NEEDS", doc.InnerXml);
		rep.BindDataSource("ABC_DataSet_Table1", 0);
		//rep.BindDataSource("NonLiquidGoods_DS_Table2", 1);

		ReportParameter[] parameters = new ReportParameter[4] {
		        new ReportParameter("DATE_TO", dtpDate.Value.ToShortDateString()),
		        new ReportParameter("DATE_FROM", dtpDate.Value.AddDays(-1*itemp).ToShortDateString()),
		        new ReportParameter("STORES", stemp),
            new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName)
		    };
         
		rep.ReportViewer.LocalReport.SetParameters(parameters);
		

		rep.ExecuteReport(this);
	}

	public string ReportName
	{
		get { return "Отчет \"ABC - анализ\""; }
	}

	public override string GroupName
	{
		get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
	}

  }
  
  public enum RequestCalculationRecalcType{All, Existing, Cancel}
}