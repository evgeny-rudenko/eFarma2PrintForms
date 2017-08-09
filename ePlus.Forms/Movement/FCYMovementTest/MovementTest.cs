using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace FCYMovementTest
{
  public class MovementTest:AbstractDocumentPrintForm
  {
    protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
    {
      DataService_BL bl = new DataService_BL();
      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = reportFiles[0];
      rep.DataSource = bl.Execute(string.Format("REP_MOVEMENT_TEST '{0}'", dataRowItem.Guid));
      rep.BindDataSource("MovementTest_DS_Table0", 0);
      rep.BindDataSource("MovementTest_DS_Table1", 1);
      return rep;
    }

    public override string PluginCode
    {
      get { return "Movement"; }
    }

    public override string ReportName
    {
      get { return "Перемещение (интерактивное)"; }
    }

    public override string GroupName
    {
      get { return string.Empty; }
    }
  }
}
