using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ePlus.MetaData.Server;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using System.Data.SqlClient;

namespace RCBAptekaRuExport
{
  public partial class AptekaRuExport : ExternalReportForm, IExternalReportFormMethods
  {
    public AptekaRuExport()
    {
      InitializeComponent();
    }

    private string SettingsFilePath
    {
      get
      {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        return Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
      }
    }

    /// <summary>
    /// Метод для получения подтверждения или опровержения того, что МЫ является центром
    /// </summary>
    /// <returns>true - ЦЕНТР, false - не ЦЕНТР</returns>
    private bool SelfIsCenter()
    {
      bool result = false;
      DataService_BL bl = new DataService_BL();

      using (SqlConnection connection = new SqlConnection(bl.ConnectionString))
      {
        SqlCommand command = new SqlCommand("SELECT DBO.REPL_REPL_CONFIG_SELF_IS_CENTER()", connection);
        command.CommandType = CommandType.Text;
        connection.Open();
        result = (bool)command.ExecuteScalar();
      }
      return result;
    }

    public void Print(string[] reportFiles)
    {
      if (string.IsNullOrEmpty(mpsAccessPoint.Code))
      {
        Logger.ShowMessage("Укажите точку доступа", 0, MessageBoxIcon.Error);
        return;
      }

      // создаем точку доступа
      AccessPointManager apm = new AccessPointManager(mpsAccessPoint.Code);
      if (apm == null) throw new Exception("Не удалось создать точку доступа");

      // готовим параметры хранимки
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      ucDrugstores.AddItems(root, "ID_CONTRACTOR");
      // центр или нет
      bool selfIsCenter = SelfIsCenter();
      Utils.AddNode(root, "CO", selfIsCenter);

      // выполняем процедуру, получаем данные из базы
      APTEKA_RU_EXPORT_BL bl = new APTEKA_RU_EXPORT_BL();
      List<EXPORT_ROW> rows;
      List<EXPORT_ROW> id_contractors;
      if (!bl.Export(doc.InnerXml, out rows, out id_contractors))
        throw new Exception("Ошибка при формировании файла для экспорта");

      List<ReportByContractors> reports = new List<ReportByContractors>();
      // ПОЛУЧАЕМ СПИСОК ОТЧЕТОВ ПО КАЖДОМУ КОНТРАГЕНТУ
      foreach (EXPORT_ROW id_c in id_contractors)
      {
        reports.Add(new ReportByContractors(id_c.ID_CONTRACTOR, rows));
      }

      // формируем файлы по каждому контрагенту
      DateTime now = DateTime.Now;
      string exportFileName = String.Empty;
      string tempFilePath = Utils.TempDir();
      int cnt = 0;
      foreach (ReportByContractors rbc in reports)
      {
        if (rbc.Rows.Count > 10000)
        {
          cnt = 0;
          while ((decimal)cnt < (decimal)rbc.Rows.Count / 10000m)
          {
            // формируем имя экспортируемого файла, временный путь для его формирования
            exportFileName = string.Format("osc{0:yyyy-MM-dd-HHmm}_{1}_{2:N0}.txt",
            now, rbc.ID_CONTRACTOR.ToString(), (cnt + 1));
            // экспортируем файл
            ExportFile(apm, Path.Combine(tempFilePath, exportFileName),
              CreateFileData(rbc.Rows, cnt * 10000), exportFileName);
            cnt++;
          }
        }
        else
        {
          // формируем имя экспортируемого файла, временный путь для его формирования
          exportFileName = string.Format("osc{0:yyyy-MM-dd-HHmm}_{1}.txt",
          now, rbc.ID_CONTRACTOR.ToString());
          // экспортируем файл
          ExportFile(apm, Path.Combine(tempFilePath, exportFileName),
            CreateFileData(rbc.Rows, 0), exportFileName);
        }
      }

      MessageBox.Show(string.Format("Данные по товарным остаткам экспортированы"),
        "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private String CreateFileData(List<EXPORT_ROW> rows, int index)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(EXPORT_ROW.HeaderText());

      for (int i = index; (i < index + 10000 && i < rows.Count); i++)
      {
        sb.AppendLine(rows[i].ToText());
      }

      return sb.ToString();
    }

    private void ExportFile(AccessPointManager apm, String filePath, String contents, String toFile)
    {
      try
      {
        File.WriteAllText(filePath, contents, Encoding.Default);

        apm.Send(filePath, toFile);
      }
      catch
      {
        throw new Exception("Ошибка при экспорте файла");
      }
      finally
      {
        if (File.Exists(filePath))
          File.Delete(filePath);
      }
    }

    public string ReportName
    {
      get { return "Товарные остатки для выгрузки в интернет магазин Apteka.Ru (OsCommerce)"; }
    }

    public override string GroupName
    {
      get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
    }

    private void ucDrugstores_BeforePluginShow(object sender, CancelEventArgs e)
    {
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void ClearValues()
    {
      mpsAccessPoint.Clear();
      ucDrugstores.Items.Clear();
    }

    private void LoadSettings()
    {
      ClearValues();
      if (!File.Exists(SettingsFilePath))
        return;

      XmlDocument doc = new XmlDocument();
      doc.Load(SettingsFilePath);
      XmlNode root = doc.SelectSingleNode("//XML");

      if (root == null)
        return;

      XmlNode ap = root.SelectSingleNode("ACCESS_POINT");
      mpsAccessPoint.Id = Utils.GetLong(ap, "ID");
      mpsAccessPoint.Text = Utils.GetString(ap, "TEXT");
      mpsAccessPoint.Code = Utils.GetString(ap, "CODE");
      //mpsAccessPoint.Guid = Utils.GetGuid(ap, "GUID");

      XmlNodeList contractors = root.SelectNodes("CONTRACTOR");
      foreach (XmlNode node in contractors)
      {
        long id = Utils.GetLong(node, "ID");
        string text = Utils.GetString(node, "TEXT");
        Guid guid = Utils.GetGuid(node, "GUID");
        string code = Utils.GetString(node, "CODE");
        ucDrugstores.AddRowItem(new DataRowItem(id, guid, code, text));
      }
    }

    private void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root;

      if (File.Exists(SettingsFilePath))
      {
        doc.Load(SettingsFilePath);
        root = doc.SelectSingleNode("//XML");
        root.RemoveAll();
      }
      else
      {
        root = Utils.AddNode(doc, "XML");
      }

      XmlNode ap = Utils.AddNode(root, "ACCESS_POINT");
      Utils.AddNode(ap, "ID", mpsAccessPoint.Id);
      Utils.AddNode(ap, "TEXT", mpsAccessPoint.Text);
      Utils.AddNode(ap, "CODE", mpsAccessPoint.Code);
      //Utils.AddNode(ap, "GUID", mpsAccessPoint.Guid);

      foreach (DataRowItem dri in ucDrugstores.Items)
      {
        XmlNode node = Utils.AddNode(root, "CONTRACTOR");
        Utils.AddNode(node, "ID", dri.Id);
        Utils.AddNode(node, "GUID", dri.Guid);
        Utils.AddNode(node, "CODE", dri.Code);
        Utils.AddNode(node, "TEXT", dri.Text);
      }

      doc.Save(SettingsFilePath);
    }

    private void AptekaRuExport_Load(object sender, EventArgs e)
    {
      LoadSettings();
      //если МЫ не является центром, то делаем плагин неактивным
      ucDrugstores.Enabled = SelfIsCenter();
    }

    private void AptekaRuExport_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
  }
}