using System;
using System.Data;
using System.Globalization;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace ExpensiveLS
{
    public partial class ExpensiveLSForm : ExternalReportForm, IExternalReportFormMethods
    {
        public ExpensiveLSForm()
        {
            InitializeComponent();
            Text = "Поступление дорогостоящих ЛС";
            ucPeriod.SetPeriodMonth();
        }

        public string ReportName
        {
            
            get { return "Поступление дорогостоящих ЛС"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            ucPeriod.AddValues(root);
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            int day = DateTime.DaysInMonth(ucPeriod.DateFrom.Year, ucPeriod.DateFrom.Month);
            DateTime date_m_fr = new DateTime(ucPeriod.DateFrom.Year, ucPeriod.DateFrom.Month, 1);
            DateTime date_m_to = new DateTime(ucPeriod.DateFrom.Year, ucPeriod.DateFrom.Month, day);

            Utils.AddNode(root, "DATE_M_FR", Utils.GetSqlDate(date_m_fr));
            Utils.AddNode(root, "DATE_M_TO", Utils.GetSqlDate(date_m_to));
            
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_EXPENSIVE_LS", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;    
            string date, day_month, month;
            date = string.Format("{0}-{1}", ucPeriod.DateFrText, ucPeriod.DateToText);
            month =  string.Format("{0} {1} года", MonthStr1(ucPeriod.DateFrom.Month), ucPeriod.DateFrom.Year.ToString());
            day_month = DateTime.Now.ToShortDateString();
            rep.AddParameter("DATE", date);
            rep.AddParameter("DAY_MONTH", day_month);
            rep.AddParameter("MONTH", month);
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("ExpensiveLS_DS_Table", 0);
            rep.ExecuteReport(this);
        }

        private string MonthStr(int m)
        {
            switch(m)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                    
            }
            return "";
        }

        private string MonthStr1(int m)
        {
            switch (m)
            {
                case 1:
                    return "январь";
                case 2:
                    return "февраль";
                case 3:
                    return "март";
                case 4:
                    return "апрель";
                case 5:
                    return "май";
                case 6:
                    return "июнь";
                case 7:
                    return "июль";
                case 8:
                    return "август";
                case 9:
                    return "сентябрь";
                case 10:
                    return "октябрь";
                case 11:
                    return "ноябрь";
                case 12:
                    return "декабрь";

            }
            return "";
        }
    }
}