using System;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using System.Windows.Forms;

namespace PartSupplier
{
    public partial class PartSupplierForm : ExternalReportForm, IExternalReportFormMethods
    {
        private const string name = "Доля поставщика в закупках";

        public PartSupplierForm()
        {
            InitializeComponent();
            Text = name;
            ucPeriod.SetPeriodMonth();
            }

        public string ReportName
        {
            get { return name; }
        }

        public void Print(string[] reportFiles)
        {
            if (ucContractor.Items.Count == 0)
            {
                MessageBox.Show("Не заполнен поставщик", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveControl = ucContractor;
                return;
            }

            if (ucGoodsGroup.Items.Count == 0)
            {
                MessageBox.Show("Не заполнена группа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveControl = ucGoodsGroup;
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            ucStore.AddItems(root, "ID_STORE");
            ucContractor.AddItems(root, "ID_CONTRACTOR");
            ucGoodsGroup.AddItems(root, "ID_GOODS_GROUP");

            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_PART_SUPPLIER", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            rep.AddParameter("DATE_FR", ucPeriod.DateFrText);
            rep.AddParameter("DATE_TO", ucPeriod.DateToText);

            rep.AddParameter("STORE", ucStore.TextValues());
            rep.AddParameter("CONTRACTOR", ucContractor.TextValues());
            rep.AddParameter("GOODS_GROUP", ucGoodsGroup.TextValues());

            rep.BindDataSource("PartSupplier_DS_Table", 0);
            rep.ExecuteReport(this);

        }
    }
}