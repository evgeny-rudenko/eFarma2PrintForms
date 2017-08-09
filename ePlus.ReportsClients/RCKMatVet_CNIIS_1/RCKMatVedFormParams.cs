using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.CommonEx.Reporting;
using System.Xml;

namespace RCKMatVet_CNIIS_1
{
    public partial class RCKMatVedFormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public RCKMatVedFormParams()
        {
            InitializeComponent();
            if (period != null)
            {
                period.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                period.DateFrom = period.DateTo.AddDays(-13);
            }
            clbFilters.Items.Clear();
            Array filters = Enum.GetValues(typeof(EMatVedFilter));
            foreach (EMatVedFilter filter in filters)
            {
                MatVedFilterDescription filterDescr = new MatVedFilterDescription(filter);
                clbFilters.Items.Add(filterDescr);
            }
        }

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

        System.Collections.Generic.Dictionary<EMatVedFilter, List<DataRowItem>> dict = new System.Collections.Generic.Dictionary<EMatVedFilter, List<DataRowItem>>();
        MatVedFilterDescription lastItem = null;
        
        public void Print(string[] reportFiles)
        {            
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "DATE_FROM", period.DateFrom);
            Utils.AddNode(root, "DATE_TO", period.DateTo);
            Utils.AddNode(root, "SHOW_SERIES", chkShowSeries.Checked ? 1 : 0);
            Utils.AddNode(root, "IMPORTANT_ONLY", chkImportantOnly.Checked ? 1 : 0);

            SaveLastFilter(clbFilters.SelectedItem as MatVedFilterDescription);

            foreach (MatVedFilterDescription descr in clbFilters.CheckedItems)
            {                
                if (dict.ContainsKey(descr.Filter))
                {
                    XmlNode filterNode = Utils.AddNode(root, descr.TableName);
                    foreach (DataRowItem dri in dict[descr.Filter])
                    {
                        XmlNode itemNode = Utils.AddNode(filterNode, "ITEM");
                        Utils.AddNode(itemNode, "ID", dri.Id);
                        Utils.AddNode(itemNode, "GUID", dri.Guid);
                        Utils.AddNode(itemNode, "TEXT", dri.Text);
                    }
                }
            }

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
			rep.LoadData("REP_RCKMatVed_CNIIS_1_EX", doc.InnerXml);
			rep.BindDataSource("RCKMatVed_CNIIS_1_DS_Table0", 0);            
            rep.AddParameter("DATE_FROM", period.DateFrText);
            rep.AddParameter("DATE_TO", period.DateToText);

            rep.AddParameter("ALL_STORE", dict.ContainsKey(EMatVedFilter.Store) && dict[EMatVedFilter.Store].Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_SUPPLIER", dict.ContainsKey(EMatVedFilter.Supplier) && dict[EMatVedFilter.Supplier].Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_PRODUCER", dict.ContainsKey(EMatVedFilter.Producer) && dict[EMatVedFilter.Producer].Count > 0 ? "0" : "1");
            rep.AddParameter("ALL_GOODS_KIND", dict.ContainsKey(EMatVedFilter.Kind) && dict[EMatVedFilter.Kind].Count > 0 ? "0" : "1");
            rep.ExecuteReport(this);
        }

        public string ReportName
        {
			get { return "Материальная ведомость CNIIS_1"; }
        }        
       
        private void clbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveLastFilter(lastItem);        
            MatVedFilterDescription descr = clbFilters.SelectedItem as MatVedFilterDescription;
            lastItem = descr;
            LoadSavedFilter(descr);
        }

        private void LoadSavedFilter(MatVedFilterDescription descr)
        {
            if (descr == null) return;
            if (!dict.ContainsKey(descr.Filter))
                dict.Add(descr.Filter, new List<DataRowItem>());

            ucFilter.SetPluginControl(descr.Plugin);
            ucFilter.Caption = descr.PluginCaption;
            ucFilter.Items.Clear();
            foreach (DataRowItem item in dict[descr.Filter])
                ucFilter.Items.Add(item);
        }

        private void SaveLastFilter(MatVedFilterDescription filter)
        {
            if (filter == null) return;
            if (!dict.ContainsKey(filter.Filter))
                dict.Add(filter.Filter, new List<DataRowItem>());
            dict[filter.Filter].Clear();
            if (clbFilters.GetItemChecked(clbFilters.Items.IndexOf(filter)))
                foreach (DataRowItem item in ucFilter.Items)
                    dict[filter.Filter].Add(item);
        }

        private void FormParams_Load(object sender, EventArgs e)
        {
            //string paramsFile = Path.Combine(Utils.TempDir(), this.Name);
            //if (File.Exists(paramsFile))
            //{
            //XmlDocument doc = new XmlDocument();
            //    doc.Load(paramsFile);
            //    chkShowSeries.Checked = Utils.GetBool(doc.SelectSingleNode("/XML"), "SHOW_SERIES");
            //    chkImportantOnly.Checked = Utils.GetBool(doc.SelectSingleNode("/XML"), "IMPORTANT_ONLY");

            //dict.Clear();
            //Array arr = Enum.GetValues(typeof(EMatVedFilter));
            //foreach (EMatVedFilter obj in arr)
            //{
            //    dict.Add(obj, new List<DataRowItem>());
            //    MatVedFilterDescription descr = new MatVedFilterDescription(obj);
            //    XmlNodeList filters = doc.SelectNodes(string.Format("/XML/{0}/ITEM", descr.TableName));
            //    foreach (XmlNode node in filters)
            //    {
            //        long id = Utils.GetLong(node, "ID");
            //        Guid guid = Utils.GetGuid(node, "GUID");
            //        string text = Utils.GetString(node, "TEXT");
            //        dict[obj].Add(new DataRowItem(id, guid, null, text));
            //    }
            //    clbFilters.SetItemChecked(clbFilters.Items.IndexOf(descr), dict[obj].Count > 0);
            //}
            ////}
            clbFilters.SelectedItem = new MatVedFilterDescription(EMatVedFilter.Goods);      
        }

        private void ucFilter_ValuesListChangedNew(object sender, ValuesListChangedEventArgs e)
        {
              if (clbFilters.SelectedIndex!=-1)
                clbFilters.SetItemChecked(clbFilters.SelectedIndex, ucFilter.Items.Count > 0);
        }
    }

    public enum EMatVedFilter
    {
        Store, Goods, Kind, Supplier, Producer
    }

    public class MatVedFilterDescription : IEquatable<EMatVedFilter>
    {
        private EMatVedFilter filter;
        private string name;
        private string pluginCaption;
        private string plugin;
        private string tableName;

        public string TableName
        {
            get { return tableName; }
        }

        public string PluginCaption
        {
            get { return pluginCaption; }
        }

        public EMatVedFilter Filter
        {
            get { return filter; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Plugin
        {
            get { return plugin; }
        }

        public MatVedFilterDescription(EMatVedFilter filter)
        {
            this.filter = filter;
            switch (filter)
            {
                case EMatVedFilter.Goods:
                    this.name = "По товарам";
                    pluginCaption = "Товары";
                    tableName = "GOODS";
                    plugin = "GOODS2";
                    break;
                case EMatVedFilter.Kind:
                    this.name = "По видам товаров";
                    pluginCaption = "Виды товаров";
                    tableName = "GOODS_KIND";
                    plugin = "GOODS_KIND";
                    break;
                case EMatVedFilter.Producer:
                    this.name = "По производителям";
                    pluginCaption = "Производители";
                    tableName = "PRODUCER";
                    plugin = "PRODUCER";
                    break;
                case EMatVedFilter.Store:
                    this.name = "По складам";
                    pluginCaption = "Склады";
                    tableName = "STORE";
                    plugin = "STORE";
                    break;
                case EMatVedFilter.Supplier:
                    this.name = "По поставщикам";
                    pluginCaption = "Поставщики";
                    tableName = "SUPPLIER";
                    plugin = "CONTRACTOR";
                    break;
            }
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is MatVedFilterDescription)
                return ((MatVedFilterDescription)obj).filter == this.filter;
            return base.Equals(obj);
        }

        public bool Equals(EMatVedFilter other)
        {
            return other == filter;
        }
    }
}