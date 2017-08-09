using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;


namespace FCKInventorySessionInv19_Rigla
{
    public class InventorySessionInv19_Rigla : AbstractDocumentPrintForm, IExternalDocumentPrintFormParams
    {
        private DataRowItem[] _stores = new DataRowItem[0];
        private bool _byStore;
        private int _isSal;

        public int IsSal
        {
            get { return _isSal; }
            set { _isSal = value; }
        }

        public bool ByStore
        {
            get { return _byStore; }
            set { _byStore = value; }
        }

        public DataRowItem[] Stores
        {
            get { return _stores; }
            set { _stores = value ?? new DataRowItem[0]; }
        }

        public override string PluginCode
        {
            get { return "INVENTORY_SESSION"; }
        }

        public override string ReportName
        {
            get { return "ИНВ-19 (Сессии инвентаризации, Ригла)"; }
        }

        public override string GroupName
        {
            get { return string.Empty; }
        }

        public IExternalDocumentPrintFormParamsControl Control
        {
            get { return new InventorySessionInv19UserControl_Rigla(); }
        }

        public bool HasPrintForm
        {
            get { return true; }
        }

        public void AfterPrint()
        {
        }

        public void Prepare()
        {
            ByStore = false;
            Stores = null;
            IsSal = 1;
        }

        public bool Validate()
        {
            if (ByStore && Stores.Length == 0) throw new Exception("Не выбрано ни одного склада !");
            return true;
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML");
            Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            Utils.AddNode(root, "IS_SAL", IsSal);
            foreach (DataRowItem store in Stores)
            {
                Utils.AddNode(root, "ID_STORE", store.Id);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.Text = rep.ReportFormName = ReportName;
            rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "InventorySessionInv19_Rigla.rdlc");
            rep.LoadData("REPEX_INVENTORY_SESSION_INV19_RIGLA", doc.InnerXml);
            rep.BindDataSource("InventorySessionInv19_DS_Table0", 0);
            rep.BindDataSource("InventorySessionInv19_DS_Table1", 1);
            rep.BindDataSource("InventorySessionInv19_DS_Table2", 2);
            List<ReportParameter> par = new List<ReportParameter>();
            par.Add(new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName));
            rep.ReportViewer.LocalReport.SetParameters(par);
            return rep;
        }
    }
}
