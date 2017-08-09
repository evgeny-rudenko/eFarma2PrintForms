using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace RCICancellationOfShelfLife_Rigla
{
  public partial class CancellationOfShelfLifeParams : ExternalReportForm, IExternalReportFormMethods
  {
    private string fileName;// = Path.Combine(Utils.TempDir(), "RCSFactorReceipts_13.xml");

    public CancellationOfShelfLifeParams()
    {
      InitializeComponent(); 
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      SetDefaultValues();
    }

    private void SetDefaultValues()
    {
      DateTime dt = DateTime.Now;
      int days_in_month = DateTime.DaysInMonth(dt.Year, dt.Month);
      DateTime next_month = dt.AddDays(1 + days_in_month - dt.Day);
      ReportMonth.Value = next_month;

      this.ucContractor.SetId(this.IdContractorDefault);
    }

    public void Print(string[] reportFiles)
    {
      if (String.IsNullOrEmpty(ucContractor.Guid.ToString()))
      {
        MessageBox.Show("Не указана аптека", "Предупреждение");
        return;
      }

      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      DateTime dt = ReportMonth.Value;
      Utils.AddNode(root, "MONTH", (dt.Month.ToString().Length == 1 ? "0" + dt.Month.ToString() : dt.Month.ToString()));
      Utils.AddNode(root, "YEAR", dt.Year.ToString());
      Utils.AddNode(root, "ID_CONTRACTOR", ucContractor.Id);

      ReportFormNew rep = new ReportFormNew();
      rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "CancellationOfShelfLife_Rigla.rdlc");
      rep.LoadData("CANCELLATION_OF_SHELF_LIFE_RIGLA", doc.InnerXml);
      rep.BindDataSource("CancellationOfShelfLife_DS_Table0", 0);

      rep.AddParameter("month", Months[dt.Month]);
      rep.AddParameter("year", dt.Year.ToString());
      rep.AddParameter("contractor", ucContractor.Text);
      rep.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);

      rep.ExecuteReport(this);
    }

    public string ReportName
    {
      get { return "Списание по сроку годности, форма А-10 (Ригла)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void CancellationOfShelfLifeParams_Load(object sender, EventArgs e)
    {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        fileName = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");

        Months = new Dictionary<int, string>();
        Months.Add(1, "Январь");
        Months.Add(2, "Февраль");
        Months.Add(3, "Март");
        Months.Add(4, "Апрель");
        Months.Add(5, "Май");
        Months.Add(6, "Июнь");
        Months.Add(7, "Июль");
        Months.Add(8, "Август");
        Months.Add(9, "Сентябрь");
        Months.Add(10, "Октябрь");
        Months.Add(11, "Ноябрь");
        Months.Add(12, "Декабрь");

      SetDefaultValues();
      if (!File.Exists(fileName)) return;
      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      XmlNode root = doc.SelectSingleNode("/XML");
      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        ucContractor.SetId(id);
      }
      ReportMonth.Value = Utils.GetDate(root, "REPORT_DATE");
    }

    private void CancellationOfShelfLifeParams_FormClosed(object sender, FormClosedEventArgs e)
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");

      XmlNode contractor = Utils.AddNode(root, "CONTRACTOR");
      Utils.AddNode(contractor, "ID", ucContractor.Id);
      Utils.AddNode(contractor, "TEXT", ucContractor.Text);
      Utils.AddNode(root, "REPORT_DATE", ReportMonth.Value);
      doc.Save(fileName);
    }

    private Dictionary<int, string> Months;

    private void ReportMonth_ValueChanged(object sender, EventArgs e)
    {
      
    }
  }
}