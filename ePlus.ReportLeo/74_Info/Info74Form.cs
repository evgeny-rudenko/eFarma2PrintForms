using System;
using System.Xml;
using ePlus.CommonEx.ReplicationConfig;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;

namespace Info74
{
    public partial class Info74Form : ExternalReportForm, IExternalReportFormMethods
    {
        public Info74Form()
        {
            InitializeComponent();
            Text = "ИНФОРМАЦИЯ об исполнении ЗАЯВКИ";
            cmbYers.DisplayMember = "Description";
            cmbYers.DataSource = EEnum<Years>.All;
            string year = DateTime.Now.Year.ToString();
            cmbYers.SelectedIndex = (int)(EEnum<Years>)year;
            cmbHalfYear.SelectedIndex = 0;
        }

        public string ReportName
        {
            get { return "ИНФОРМАЦИЯ об исполнении ЗАЯВКИ"; }
        }

        public void Print(string[] reportFiles)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            int year = Utils.GetInt((EEnum<Years>)cmbYers.SelectedIndex);
            DateTime date1p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 1, 1) : new DateTime(year, 7, 1);
            DateTime date2p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 2, 1) : new DateTime(year, 8, 1);
            DateTime date3p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 3, 1) : new DateTime(year, 9, 1);
            DateTime date4p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 4, 1) : new DateTime(year, 10, 1);
            DateTime date5p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 5, 1) : new DateTime(year, 11, 1);
            DateTime date6p = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 6, 1) : new DateTime(year, 12, 1);
            DateTime dateEnd = cmbHalfYear.SelectedIndex == 0 ? new DateTime(year, 6, 30) : new DateTime(year, 12, 31);
            Utils.AddNode(root, "DATE_1P", Utils.SqlDate(date1p));
            Utils.AddNode(root, "DATE_2P", Utils.SqlDate(date2p));
            Utils.AddNode(root, "DATE_3P", Utils.SqlDate(date3p));
            Utils.AddNode(root, "DATE_4P", Utils.SqlDate(date4p));
            Utils.AddNode(root, "DATE_5P", Utils.SqlDate(date5p));
            Utils.AddNode(root, "DATE_6P", Utils.SqlDate(date6p));
            Utils.AddNode(root, "DATE_END", Utils.SqlDate(dateEnd));
            foreach (DataRowItem row in ucContracts.Items)
            {
                Utils.AddNode(root, "ID_CONTRACTS_GLOBAL", row.Guid);
            }
            foreach (DataRowItem row in ucProgram.Items)
            {
                Utils.AddNode(root, "ID_TASK_PROGRAM_GLOBAL", row.Guid);
            }
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_INFO_74", doc.InnerXml);
            //rep.SaveSchema(@"c:\data.xml");
            //return;
            rep.AddParameter("PERIOD", DateTime.Now.ToShortDateString());
            rep.AddParameter("HALF_YEAR", cmbHalfYear.SelectedText);
            rep.AddParameter("CONTRACTS", ucContracts.TextValues());
            rep.AddParameter("PROGRAM", ucProgram.TextValues());

            rep.BindDataSource("Info74_DS_Table", 0);
            rep.ExecuteReport(this);

        }

        private class Years : IEnumItems
        {
            private EnumItem[] items = {
                    new EnumItem(0, "2005", "2005"), 
                    new EnumItem(1, "2006", "2006"), 
                    new EnumItem(2, "2007", "2007"), 
                    new EnumItem(3, "2008", "2008"), 
                    new EnumItem(4, "2009", "2009"), 
                    new EnumItem(5, "2010", "2010"), 
                    new EnumItem(6, "2011", "2011"), 
                    new EnumItem(7, "2012", "2012"), 
                    new EnumItem(8, "2013", "2013"), 
                    new EnumItem(9, "2014", "2014"), 
                    new EnumItem(10, "2015", "2015"), 
                    new EnumItem(11, "2016", "2016")};

            public EnumItem[] Items
            {
                get { return items; }
            }
        }
    }
}