using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Server;

namespace Defectura
{
    public partial class FormParams : ExternalReportForm, IExternalReportFormMethods
    {
        public FormParams()
        {
            InitializeComponent();
        }

        public void Print(string[] reportFiles)
        {
            
        }

        public string ReportName
        {
            get { return "Дефектура"; }
        }

        public override string GroupName
        {
            get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
        }
    }
}