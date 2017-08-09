﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ePlus.CommonEx.Base.BlUtils;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;
using System.IO;

namespace FCYInvoiceOutExport4Sale
{
  public class ExportInvoice:AbstractDocumentPrintForm
  {
    private void AddNodes(XmlNode root, DataRow row, string[] columns)
    {
      foreach (string column in columns)
        AddNode(root, row, column);
    }

    private void AddNode(XmlNode root, DataRow row, string colName)
    {
      if (!row.Table.Columns.Contains(colName)) return;
      DataColumn col = row.Table.Columns[colName];
      if (col.DataType == typeof(decimal))
        Utils.AddNode(root, colName, Utils.GetDecimal(row, colName));
      if (col.DataType == typeof(string))
        Utils.AddNode(root, colName, Utils.GetString(row, colName));
      if (col.DataType == typeof(DateTime))
        Utils.AddNode(root, colName, Utils.GetDate(row, colName));
      if (col.DataType == typeof(bool))
        Utils.AddNode(root, colName, Utils.GetBool(row, colName)?1:0);
      if (col.DataType == typeof(int))
        Utils.AddNode(root, colName, Utils.GetInt(row, colName));        
    }

    private string ReplaceInvalidFileNameChars(string fileName, char replacement)
    {
      List<char> chars = new List<char>(Path.GetInvalidFileNameChars());
      char[] fileNameChars = fileName.ToCharArray();
      if (string.IsNullOrEmpty(fileName))
        throw new ArgumentNullException(fileName, "Не задано имя файла");
      if (chars.Contains(replacement) || replacement==0)
        throw new ArgumentException(fileName, "Неверный символ подстановки");        
      
      for (int i=0;i<fileNameChars.Length;i++)
      {
        if (chars.Contains(fileNameChars[i]))
          fileNameChars[i] = replacement;          
      }
      return new string(fileNameChars);
    }
    
    protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
    {
      Guid g = Guid.Empty;
      if (dataRowItem != null)
        g = dataRowItem.Guid;
      DataSet ds = new DataService_BL().Execute(string.Format("EXEC REPEX_INVOICE_OUT_EXPORT_INVOICE_PRICE_SAL {0}", BlUtils.GuidToString(g)));
      if (ds.Tables[0].Rows.Count == 0)
        throw new Exception("Не найден документ");

      string num;
      DateTime dt;
      string fileName = MakeFileName(ds.Tables[0].Rows[0], out num, out dt);
      
      XmlDocument doc = new XmlDocument();
      XmlNode root = Utils.AddNode(doc, "XML");
      XmlNode invoice = Utils.AddNode(root, "INVOICE");

      MakeHeader(ds.Tables[0].Rows[0], invoice);
      XmlNode itemsNode = Utils.AddNode(invoice, "ITEMS");
      foreach (DataRow dr in ds.Tables[1].Rows)
      {
        XmlNode itemNode = Utils.AddNode(itemsNode, "ITEM");
        MakeItem(dr, itemNode);
        XmlNode certsNode = Utils.AddNode(root, "CERTIFICATES");
        long series = Utils.GetLong(dr, "ID_SERIES");
        DataRow[] certs = ds.Tables[2].Select(string.Format("ID_SERIES={0}", series));
        foreach (DataRow cert in certs)
        {
          XmlNode certNode = Utils.AddNode(certsNode, "CERTIFICATE");
          MakeCert(cert, certNode);
        }
      }

      string destDir = Path.Combine(Utils.AppDir(), "Export");
      SaveFile(destDir, fileName, doc);
      Logger.ShowInfo(string.Format("Экспорт документа заверщен\r\nДокумент {0} от {1:dd.MM.yyyy} экспортирован в {2}", num, dt, destDir), 2*1000);
      return null;
    }

    private void SaveFile(string destDir, string fileName, XmlDocument doc)
    {
      if (!Directory.Exists(destDir))
        Directory.CreateDirectory(destDir);
      doc.Save(Path.Combine(destDir, fileName));
    }

    private string MakeFileName(DataRow row, out string num, out DateTime dt)
    {
      num = Utils.GetString(row, "INCOMING_NUMBER");
      dt = Utils.GetDate(row, "INCOMING_DATE");

      string fileName = string.Format("{0}_{1:dd.MM.yyyy}", num, dt);
      fileName = ReplaceInvalidFileNameChars(fileName, '_');
      return Path.ChangeExtension(fileName, ".xml");
    }
    
    private void MakeHeader(DataRow row, XmlNode root)
    {
      AddNodes(root, row, new string[]{
                                        "SUPPLIER_NAME",
                                        "SVAT_SUPPLIER",
                                        "SUM_SUPPLIER",
                                        "SVAT_RETAIL",
                                        "SUM_RETAIL",
                                        "INCOMING_NUMBER",
                                        "INCOMING_DATE",
                                        "INCOMING_BILL_NUMBER",
                                        "INCOMING_BILL_DATE",
                                        "COMMENT"
                                      });
    }
    
    private void MakeItem(DataRow row, XmlNode root)
    {
      AddNodes(root, row, new string[]
                            {
                              "NUMERATOR",
                              "DENOMINATOR",
                              "UNIT_NAME",
                              
                              "GOODS_CODE",
                              "GOODS",
                              "PRODUCER",
                              "COUNTRY",
                              "IMPORTANT",
                              "REGISTER_PRICE",
                              "REGISTRATION_DATE",
                              
                              "QUANTITY",
                              "PRODUCER_PRICE",
                              
                              "SUPPLIER_VAT_PER_UNIT",
                              "SUPPLIER_ADPRICE",
                              "SUPPLIER_PRICE",
                              "SUPPLIER_VAT",
                              "SUPPLIER_PRICE_VAT",
                              "SUPPLIER_SUM",
                              "SUPPLIER_VAT_SUM",
                              "SUPPLIER_SUM_VAT",
                              
                              "RETAIL_ADPRICE",
                              "RETAIL_PRICE",
                              "RETAIL_VAT",
                              "RETAIL_PRICE_VAT",
                              "RETAIL_SUM",
                              "RETAIL_VAT_SUM",
                              "RETAIL_SUM_VAT",
                              
                              "SERIES_NUMBER",
                              "BEST_BEFORE",
                              "GTD_NUMBER",
                              "BAR_CODE"
                            });
    }

    private void MakeCert(DataRow row, XmlNode root)
    {
      AddNodes(root, row, new string[]
                            {
                              "CERT_NUMBER", 
                              "CERT_ORGAN",
                              "CERT_DATE",
                              "CERT_END_DATE"
                            });
    }

    public override string PluginCode
    {
      get { return "INVOICE_OUT"; }
    }

    public override string ReportName
    {
      get { return "Экспорт накладной (цены продажи)"; }
    }

    public override string GroupName
    {
      get { return string.Empty; }
    }
  }
}
