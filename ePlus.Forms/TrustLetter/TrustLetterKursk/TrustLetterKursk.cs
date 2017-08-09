using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace FCHTrustLetterKursk
{
    public class TrustLetterKursk : AbstractDocumentPrintForm
    {
        public override string GroupName
        {
            get { return String.Empty; }
        }

        public override string PluginCode
        {
            get { return "TRUST_LETTER"; }
        }

        public override string ReportName
        {
            get { return "Отчет по доверительным письмам 230-ФЗ"; }
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
        {
            DataService_BL bl = new DataService_BL();
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];   
            rep.DataSource = bl.Execute("REP_TRUST_LETTER_KURSK");
            rep.BindDataSource("TrustLetterKursk_DS__T1", 0);
            rep.BindDataSource("TrustLetterKursk_DS__T2", 1);
            rep.BindDataSource("TrustLetterKursk_DS__T3", 2);           
            return rep;            
            
        }
    }
}
