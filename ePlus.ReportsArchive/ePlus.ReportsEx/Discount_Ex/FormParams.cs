using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;

namespace Discount_Ex
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
            if (ucPeriod1 != null)
            {
                ucPeriod1.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod1.DateFrom = ucPeriod1.DateTo.AddDays(-13);
            }

        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            if (checkBox1.Checked) 
                Utils.AddNode(root, "SORT", "1");
            Utils.AddNode(root, "WITH_RETURN", chbReturn.Checked ? "1" : "0");

            PluginMultiSelect.AddItems(ucContractor, root, "ID_CONTRACTOR");            
            PluginMultiSelect.AddItems(ucStore, root, "ID_STORE");            
            PluginMultiSelect.AddItems(ucCashRegistr, root, "ID_CASH_REGISTER");            
            PluginMultiSelect.AddItems(ucDiscountCard, root, "ID_DISCOUNT2_CARD_GLOBAL", PluginMultiSelectItemField.RowGuid);            
            PluginMultiSelect.AddItems(ucDiscountCardOwner, root, "ID_DISCOUNT2_MEMBER_GLOBAL", PluginMultiSelectItemField.RowGuid);            
            PluginMultiSelect.AddItems(ucCardType, root, "ID_DISCOUNT2_CARD_TYPE_GLOBAL", PluginMultiSelectItemField.RowGuid);            
            PluginMultiSelect.AddItems(ucDiscountProgram, root, "ID_DISCOUNT2_PROGRAM_GLOBAL", PluginMultiSelectItemField.RowGuid);

            ucPeriod1.AddValues(root);
            ReportFormNew rep = new ReportFormNew();
            if (checkBox1.Checked) 
                rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]),"Discount_All_Ex.rdlc");
            else rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "Discount_Ex.rdlc");
            rep.LoadData("REP_CHEQUE_DISCOUNT_EX", doc.InnerXml);
            rep.BindDataSource("Discount_DS_Table", 0);
            rep.BindDataSource("Discount_DS_Table1", 1);

            rep.AddParameter("date_fr", ucPeriod1.DateFrText);
            rep.AddParameter("date_to", ucPeriod1.DateToText);
            rep.AddParameter("Contractor", ucContractor.TextValues());
            rep.AddParameter("Store", ucStore.TextValues());
            rep.AddParameter("CashRegistr", ucCashRegistr.TextValues());
            rep.AddParameter("DiscountCard", ucDiscountCard.TextValues());
            rep.AddParameter("DiscountCardOwner", ucDiscountCardOwner.TextValues());
            rep.AddParameter("BarCode", ucCardType.TextValues());
            rep.AddParameter("DiscountProgram", ucDiscountProgram.TextValues());
			rep.AddParameter("Pm_ViewGoodsCode", chbGoodCode.Checked ? "1" : "0");
            rep.ExecuteReport(this);
            //rep.ReportFormName = "Отчет: по скидкам";
        }

        public string ReportName
        {
            get { return "Отчет по скидкам"; }
        }

        public override string GroupName
        {
            get
            {
                return new ReportGroupDescription(ReportGroup.CashReports).Description;
            }
        }

        private void ucDiscountProgram_ValuesListChanged()
        {
            if (ucDiscountProgram.Items.Count > 0)
            {
                ucDiscountCard.Enabled = false;
                ucCardType.Enabled = false;
                ucDiscountCardOwner.Enabled = false;
            }
            if (ucDiscountProgram.Items.Count == 0)
            {
                ucDiscountCard.Enabled = true;
                ucCardType.Enabled = true;
                ucDiscountCardOwner.Enabled = true;
            }
        }

        private void ucDiscountCard_ValuesListChanged()
        {
            if (ucDiscountCard.Items.Count > 0) ucDiscountProgram.Enabled = false;
            if (ucDiscountCard.Items.Count == 0) ucDiscountProgram.Enabled = true;
        }

        private void ucBarCode_ValuesListChanged()
        {
            if (ucCardType.Items.Count > 0) ucDiscountProgram.Enabled = false;
            if (ucCardType.Items.Count == 0) ucDiscountProgram.Enabled = true;
        }

        private void ucDiscountCardOwner_ValuesListChanged()
        {
            if (ucDiscountCardOwner.Items.Count > 0) ucDiscountProgram.Enabled = false;
            if (ucDiscountCardOwner.Items.Count == 0) ucDiscountProgram.Enabled = true;
        }

        private void ucCardType_ValuesListChanged()
        {

        }
    }

    public enum PluginMultiSelectItemField 
    { 
        RowId, RowGuid, RowCode, RowText 
    };

    public class PluginMultiSelect
    {
        public static void AddItems(UCPluginMultiSelect plugin, XmlNode root, string nodeName)
        {
            AddItems(plugin, root, nodeName, PluginMultiSelectItemField.RowId);
        }

        public static void AddItems(UCPluginMultiSelect plugin, XmlNode root, string nodeName, PluginMultiSelectItemField field)
        {
            if (!plugin.Enabled) return;
            foreach (DataRowItem dri in plugin.Items)
            {
                switch (field)
                {
                    case PluginMultiSelectItemField.RowGuid:
                        Guid g = dri.Guid;
                        if (g != Guid.Empty)
                            Utils.AddNode(root, nodeName, g);
                        break;
                    case PluginMultiSelectItemField.RowCode:
                        string code = dri.Code;
                        if (code != null && !string.IsNullOrEmpty(code.Trim()))
                            Utils.AddNode(root, nodeName, code);
                        break;
                    case PluginMultiSelectItemField.RowText:
                        string text = dri.Text;
                        if (text != null && !string.IsNullOrEmpty(text.Trim()))
                            Utils.AddNode(root, nodeName, text);
                        break;
                    default:
                        long id = dri.Id;
                        if (id != 0)
                            Utils.AddNode(root, nodeName, id);
                        break;
                }
            }
        }
    }
}