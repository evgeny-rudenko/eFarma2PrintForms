using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;

namespace TO_Planet_Ex
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
            if (ucPeriod != null)
            {
                ucPeriod.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                ucPeriod.DateFrom = ucPeriod.DateTo.AddDays(-13);
            }
        }


        //public override void Execute(string connectionString, string folderPath)
        //{                        
        //    string s = Utils.RegistryLoad("TOPlanetEx111");
        //    if (s != null && Convert.ToInt16(s) > 3)
        //        return;
        //    else
        //    {
        //        if (s == null)
        //            s = "1";
        //        else if (Convert.ToInt16(s) <= 3)
        //        {
        //            int i = Convert.ToInt16(s) + 1;
        //            s = Convert.ToString(i);
        //        }
        //        Utils.RegistrySave("TOPlanetEx111", s);
        //        base.Execute(connectionString, folderPath);
        //    }
        //}

        public void Print(string[] reportFiles)
        {
            //string s = Utils.RegistryLoad("TOPlanetExParam1");
            //if (s != null && Convert.ToInt16(s) >= 19)
            //    return;
            //else
            //{
            //    if (s == null)
            //        s = "1";
            //    else if (Convert.ToInt16(s) < 19)
            //    {
            //        int i = Convert.ToInt16(s) + 1;
            //        s = Convert.ToString(i);
            //    }
            //    Utils.RegistrySave("TOPlanetExParam1", s);
                XmlDocument doc = new XmlDocument();
                XmlNode root = Utils.AddNode(doc, "XML");
                Utils.AddNode(root, "DATE_FR", ucPeriod.DateFrom);
                Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);
                foreach (DataRowItem dr in mpsContractor.Items)
                    Utils.AddNode(root, "ID_CONTRACTOR", dr.Id);

                ReportFormNew rep = new ReportFormNew();
                rep.ReportPath = reportFiles[0];
                rep.LoadData("TO_PLANET_EX", doc.InnerXml);

                rep.BindDataSource("TO_Planet_DS_Table0", 0);
                rep.BindDataSource("TO_Planet_DS_Table1", 1);
                rep.BindDataSource("TO_Planet_DS_Table2", 2);
                rep.BindDataSource("TO_Planet_DS_Table3", 3);
                rep.BindDataSource("TO_Planet_DS_Table4", 4);
                rep.BindDataSource("TO_Planet_DS_Table5", 5);
                decimal parSum_Acc_Vat = 0;
                foreach (DataRow Row in rep.DataSource.Tables[3].Rows)
                    parSum_Acc_Vat += Utils.GetDecimal(Row, "SUM_ACC_VAT");

                rep.AddParameter("date_fr", ucPeriod.DateFrText);
                rep.AddParameter("date_to", ucPeriod.DateToText);
                rep.AddParameter("summ_acc_vat", RusCurrency.Str((double)parSum_Acc_Vat));
                rep.ExecuteReport(this);                                                    
            //}
        }

        public string ReportName
        {
            get { return "Товарный отчет (Планета)"; }
        }

        //public override string GroupName
        //{
        //    get
        //    {
        //        return new ReportGroupDescription(ReportGroup.GoodsReports).Description;
        //    }
        //}
    }
}