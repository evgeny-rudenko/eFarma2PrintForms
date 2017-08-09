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

namespace RCRContractorSettlement
{
    public partial class ContractorSettlementParams : ExternalReportForm, IExternalReportFormMethods
    {
        public ContractorSettlementParams()
        {
            InitializeComponent();
        }

        private void SetDefaultValues()
        {
            period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            period.DateFrom = period.DateTo.AddDays(-5);
            ucFilter.Items.Clear();
        }

        public void Print(string[] reportFiles)
        {
            if (contractor.Id == 0)
            {
	            Logger.ShowMessage("Контрагент должен быть задан", 0, MessageBoxIcon.Error);
	            return;
            }

            ContractorGroupDescription cgd = cbFilter.SelectedItem as ContractorGroupDescription;
            if (cgd==null) return;

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "ID_CONTRACTOR", contractor.Id);
            Utils.AddNode(root, "SHOW_ON_THE_WAY", chkShowOnTheWay.Checked && IsGroupBySupplier(cgd.Group));

            SaveLastFilter(cgd);

            List<DataRowItem> items;
            ReportFormNew rep = new ReportFormNew();			
            switch (cgd.Group)
            {
                case ContractorGroup.Empty:
		            items = dict[cgd.Group];
	            foreach (DataRowItem dri in items)
	            {
		            Utils.AddNode(root, "SUPPLIER", dri.Id);
		            Utils.AddNode(root, "CLIENT", dri.Id);
		            Utils.AddNode(root, "BUYER", dri.Id);                    
	            }
	            Utils.AddNode(root, "ALL", true);
                rep.ReportPath = reportFiles[1];
	            break;
                case ContractorGroup.Supplier:
                case ContractorGroup.Client:
                case ContractorGroup.Buyer:
                if (!dict.ContainsKey(cgd.Group) || dict[cgd.Group]==null || dict[cgd.Group].Count==0)
                {
                    Logger.ShowMessage(string.Format("Необходимо выбрать хотя бы одно значение из фильтра \"{0}\"\r\nили выбрать фильтр \"По всем\"",
	                                 cgd.Name), 0, MessageBoxIcon.Error);
                    return;
                }
                    items = dict[cgd.Group];
                    rep.ReportPath = reportFiles[0];
                    foreach (DataRowItem dri in items)
                    Utils.AddNode(root, cgd.NodeName, dri.Id);          
                    break;
            }
        				
            rep.AddParameter("DATE_FROM", period.DateFrText);
            rep.AddParameter("DATE_TO", period.DateToText);
            rep.AddParameter("CONTRACTOR", contractor.Text);
            rep.AddParameter("SHOW_ON_THE_WAY", chkShowOnTheWay.Checked && IsGroupBySupplier(cgd.Group) ? "1" : "0");
            rep.AddParameter("SHORT", cbReportType.SelectedItem != null && cbReportType.SelectedItem.Equals(new ReportTypeDescription(ReportType.Short))?"1":"0");
            rep.LoadData("REP_CONTRACTOR_SETTLEMENT_EX", doc.InnerXml);
            rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
            rep.BindDataSource("ContractorSettlement_DS_Table0", 0);
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
            get { return "Расчеты с контрагентами"; }
        }

        ContractorGroupDescription lastDescr = null;
        System.Collections.Generic.Dictionary<ContractorGroup, List<DataRowItem>> dict = new System.Collections.Generic.Dictionary<ContractorGroup, List<DataRowItem>>();
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
          SaveLastFilter(lastDescr);

          ContractorGroupDescription descr = cbFilter.SelectedItem as ContractorGroupDescription;
          lastDescr = descr;
          LoadSavedFilter(descr);
          chkShowOnTheWay.Enabled = IsGroupBySupplier(descr.Group);
        }

        private bool IsGroupBySupplier(ContractorGroup descr)
        {
            return descr == ContractorGroup.Empty || descr == ContractorGroup.Supplier;
        }

        private void LoadSavedFilter(ContractorGroupDescription descr)
        {
          if (descr == null) return;
          if (!dict.ContainsKey(descr.Group))
            dict.Add(descr.Group, new List<DataRowItem>());

          ucFilter.SetPluginControl("CONTRACTOR");
          ucFilter.Caption = descr.Name;
          ucFilter.Items.Clear();
          foreach (DataRowItem item in dict[descr.Group])
            ucFilter.Items.Add(item);
        }

        private void SaveLastFilter(ContractorGroupDescription descr)
        {
          if (descr == null) return;
          if (!dict.ContainsKey(descr.Group))
            dict.Add(descr.Group, new List<DataRowItem>());
          dict[descr.Group].Clear();
          foreach (DataRowItem item in ucFilter.Items)
            dict[descr.Group].Add(item);
        }

        private void ContractorSettlementParams_Load(object sender, EventArgs e)
        {
            Array arr = Enum.GetValues(typeof(ContractorGroup));
            cbFilter.Items.Clear();

            foreach (ContractorGroup gr in arr)
            {
                ContractorGroupDescription descr = new ContractorGroupDescription(gr);
                cbFilter.Items.Add(descr);
            }
            cbFilter.SelectedIndex = 0;

            cbReportType.Items.Clear();
            Array repTypeArr = Enum.GetValues(typeof(ReportType));
            foreach (ReportType repType in repTypeArr)
            {
                ReportTypeDescription descr = new ReportTypeDescription(repType);
                cbReportType.Items.Add(descr);
            }
            cbReportType.SelectedIndex = 0;
            if (period != null)
            {
                period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                period.DateFrom = period.DateTo.AddDays(-5);
            }
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
    }

    public enum ContractorGroup
    {
        Empty, Supplier, Client, Buyer
    }

    public class ContractorGroupDescription
    {
        private ContractorGroup group;
        private string name = string.Empty;
        private string nodeName = string.Empty;

        public ContractorGroup Group
        {
        get { return group; }
        }

        public string Name
        {
        get { return name; }
        }

        public string NodeName
        {
            get { return nodeName; }
        }

        public ContractorGroupDescription(ContractorGroup group)
        {
            this.group = group;
            switch (group)
            {
                case ContractorGroup.Supplier:
                  this.name = "Поставщики";
                  this.nodeName = "SUPPLIER";
                  break;
                case ContractorGroup.Client:
                  this.name = "Аптеки (подразделения компании)";
                  this.nodeName = "CLIENT";
                  break;
                case ContractorGroup.Buyer:
                  this.name = "Покупатели";
                  this.nodeName = "BUYER";
                  break;
                default:
                  this.name = "По всем";
                  this.nodeName = string.Empty;		  
                  break;
            }
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            ContractorGroupDescription descr = obj as ContractorGroupDescription;
            if (descr != null)
            return descr.group == this.group;
            return base.Equals(obj);
        }
    }

    public enum ReportType{Full, Short}

    public class ReportTypeDescription
    {
        private ReportType type;
        private string name = string.Empty;

        public ReportTypeDescription(ReportType type)
        {
            this.type = type;
            switch (type)
            {
            case ReportType.Full:
                name = "Полный";
              break;
            case ReportType.Short:
                name = "Краткий";
              break;
            default:
              name = string.Empty;
              break;
            }
        }

        public string Name
        {
            get { return name; }
        }

        public ReportType ReportType
        {
            get { return type; }
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            ReportTypeDescription descr = obj as ReportTypeDescription;
            if (descr != null)
            return descr.type == this.type;
            return base.Equals(obj);
        }                
    }    
}