using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;

namespace PrintFormSample
{
  public class PrintFormSample:AbstractDocumentPrintForm
  {
    protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
    {
      // создаем xml для параметров (потому что все отчеты (и ПФ) должны иметь параметр @XMLPARAM)
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      // записываем в параметры идентификатор документа
      Utils.AddNode(root, "ID_BILL_GLOBAL", dataRowItem.Guid);
      
      // создаем форму отчета
      ReportFormNew rep = new ReportFormNew();
      // устанавливаем ее текст
      rep.Text = ReportName;
      // сообщаем отчету откуда брать файл отчета
      rep.ReportPath = reportFiles[0];

      // процедура должна быть уже создана. если все правильно
      PostLoadDataCompleteAsyncArgs completeSignal = new PostLoadDataCompleteAsyncArgs();
      rep.LoadDataAsync("REPEX_BILL", doc.InnerXml, delegate(object obj)
                                                      {
                                                        // возвращенные наборы данных прицепляем к DataSet
                                                        rep.BindDataSource("BillReportData_Table", 0);
                                                        rep.BindDataSource("BillReportData_Table1", 1);

                                                        // задаем параметры отчета
                                                        // необязательно. Исключительно в целях демонстрации
                                                        decimal dblSummory = 0, dblVatSummory = 0;
                                                        decimal vatSum10 = 0; decimal vatSum18 = 0;
                                                        foreach (DataRow Row in rep.DataSource.Tables[1].Rows)
                                                        {
                                                          dblSummory += Utils.GetDecimal(Row, "RETAIL_SUMV");
                                                          dblVatSummory += Utils.GetDecimal(Row, "VAT");
                                                          vatSum10 += Utils.GetDecimal(Row, "VAT_SUM10");
                                                          vatSum18 += Utils.GetDecimal(Row, "VAT_SUM18");
                                                        }
                                                        dblSummory = Utils.Round(dblSummory, 2);
                                                        dblVatSummory = Utils.Round(dblVatSummory, 2);

                                                        ReportParameter[] ps = new ReportParameter[5];
                                                        ps[0] = new ReportParameter("SUMM", dblSummory.ToString("n2"));
                                                        ps[1] = new ReportParameter("VAT_SUMM", dblVatSummory.ToString("n2"));
                                                        ps[2] = new ReportParameter("SUMM_RUS", RusCurrency.Str((double)dblSummory));
                                                        ps[3] = new ReportParameter("VAT_SUM10", vatSum10.ToString("n2"));
                                                        ps[4] = new ReportParameter("VAT_SUM18", vatSum18.ToString("n2"));

                                                        rep.ReportViewer.LocalReport.SetParameters(ps);
                                                        // возвращаем созданный отчет. Он сам будет показан и напечатан
                                                      }, completeSignal);
      while (!completeSignal.Completed)
      {
        Application.DoEvents();
      }
      if (completeSignal.Exception!=null)
        return null;
      return rep;
    }

    public override string PluginCode
    {
      get { return "BILL"; }
    }

    public override string ReportName
    {
      get { return "Пример печатной формы"; }
    }

    public override string GroupName
    {
      get { return string.Empty; }
    }
  }
}
