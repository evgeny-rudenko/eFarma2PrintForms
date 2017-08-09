using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace FCChInvoiceOutExportXMLEncode
{
  public abstract class ExportDocument
  {
    public void Export(string procName, Guid idGlobal)
    {
      DataService_BL bl = new DataService_BL(); 
      string globalPar = idGlobal ==Guid.Empty?"NULL": string.Format("'{0}'", idGlobal);
      DataSet ds = bl.Execute(string.Format("EXEC {0} {1}", procName, globalPar));
      FillDocument(ds);
    }

    protected abstract void FillDocument(DataSet ds);

    public abstract string Save(string uri);
  }
  
  public class Export2Invoice: ExportDocument
  {
    private System.Collections.Generic.Dictionary<string, XmlDocument> docList = new System.Collections.Generic.Dictionary<string, XmlDocument>();
    
    string doc_name_suffix = string.Empty;
    protected override void FillDocument(DataSet ds)
    {
      if (ds.Tables.Count != 4) return;
      foreach (DataRow row in ds.Tables[0].Rows)
      {
        string doc_name = Utils.GetString(row, "DOC_NAME");

        doc_name_suffix = docList.ContainsKey(doc_name) ? string.Format("_" + ds.Tables[0].Rows.IndexOf(row)) : string.Empty;

        doc_name += doc_name_suffix;

        XmlDocument doc = new XmlDocument();
        XmlNode root = Utils.AddNode(doc, "XML");
        XmlNode docNode = Utils.AddNode(root, "INVOICE");
        
        docList.Add(doc_name, doc);
        
        long fid = Utils.GetLong(row, "FID");
        DataRow[] header = ds.Tables[1].Select(string.Format("FID={0}", fid));
        if (header.Length != 1) continue;
        long sid = Utils.GetLong(header[0], "SID");
        FillHeader(header[0], docNode);
        XmlNode itemsNode = Utils.AddNode(docNode, "ITEMS");
        DataRow[] items = ds.Tables[2].Select(string.Format("FID={0} AND SID={1}", fid, sid));
        foreach (DataRow item in items)
        {
          long tid = Utils.GetLong(item, "TID");
          XmlNode itemNode = Utils.AddNode(itemsNode, "ITEM");
          FillItem(item, itemNode);
          XmlNode certsNode = Utils.AddNode(itemNode, "CERTIFICATES");          
          DataRow[] certs = ds.Tables[3].Select(string.Format("FID={0} AND SID={1} AND TID={2}", fid, sid, tid));
          foreach (DataRow cert in certs)
          {
            XmlNode certNode = Utils.AddNode(certsNode, "CERTIFICATE");
            FillCert(cert, certNode);
          }
        }
      }
    }

    private void FillCert(DataRow cert, XmlNode certNode)
    {
      Utils.AddNode(certNode, "CERT_NUMBER", Utils.GetString(cert, "CERT_NUMBER"));
      Utils.AddNode(certNode, "CERT_ORGAN", Utils.GetString(cert, "CERT_ORGAN"));
      Utils.AddNode(certNode, "CERT_DATE", Utils.GetDate(cert, "CERT_DATE"));
      Utils.AddNode(certNode, "CERT_END_DATE", Utils.GetDate(cert, "CERT_END_DATE"));
    }

    private void FillItem(DataRow item, XmlNode itemNode)
    {
      Utils.AddNode(itemNode, "NUMERATOR", Utils.GetString(item, "NUMERATOR"));
      Utils.AddNode(itemNode, "DENOMINATOR", Utils.GetString(item, "DENOMINATOR"));
      Utils.AddNode(itemNode, "UNIT_NAME", Utils.GetString(item, "UNIT_NAME"));

      Utils.AddNode(itemNode, "GOODS_CODE", Utils.GetString(item, "GOODS_CODE"));
      Utils.AddNode(itemNode, "GOODS", Utils.GetString(item, "GOODS"));
      Utils.AddNode(itemNode, "PRODUCER", Utils.GetString(item, "PRODUCER"));
      Utils.AddNode(itemNode, "COUNTRY", Utils.GetString(item, "COUNTRY"));    
      Utils.AddNode(itemNode, "IMPORTANT", Utils.GetBool(item, "IMPORTANT"));
      Utils.AddNode(itemNode, "REGISTER_PRICE", Utils.GetDecimal(item, "REGISTER_PRICE"));
      Utils.AddNode(itemNode, "REGISTRATION_DATE", Utils.GetDate(item, "REGISTRATION_DATE"));

      Utils.AddNode(itemNode, "QUANTITY", Utils.GetDecimal(item, "QUANTITY"));
      Utils.AddNode(itemNode, "PRODUCER_PRICE", Utils.GetDecimal(item, "PRODUCER_PRICE"));

      Utils.AddNode(itemNode, "SUPPLIER_VAT_PER_UNIT", Utils.GetDecimal(item, "SUPPLIER_VAT_PER_UNIT"));
      Utils.AddNode(itemNode, "SUPPLIER_ADPRICE", Utils.GetDecimal(item, "SUPPLIER_ADPRICE"));
      Utils.AddNode(itemNode, "SUPPLIER_PRICE", Utils.GetDecimal(item, "SUPPLIER_PRICE"));
      Utils.AddNode(itemNode, "SUPPLIER_VAT", Utils.GetDecimal(item, "SUPPLIER_VAT"));
      Utils.AddNode(itemNode, "SUPPLIER_PRICE_VAT", Utils.GetDecimal(item, "SUPPLIER_PRICE_VAT"));
      Utils.AddNode(itemNode, "SUPPLIER_SUM", Utils.GetDecimal(item, "SUPPLIER_SUM"));
      Utils.AddNode(itemNode, "SUPPLIER_VAT_SUM", Utils.GetDecimal(item, "SUPPLIER_VAT_SUM"));
      Utils.AddNode(itemNode, "SUPPLIER_SUM_VAT", Utils.GetDecimal(item, "SUPPLIER_SUM_VAT"));

      Utils.AddNode(itemNode, "RETAIL_ADPRICE", Utils.GetDecimal(item, "RETAIL_ADPRICE"));
      Utils.AddNode(itemNode, "RETAIL_PRICE", Utils.GetDecimal(item, "RETAIL_PRICE"));
      Utils.AddNode(itemNode, "RETAIL_VAT", Utils.GetDecimal(item, "RETAIL_VAT"));
      Utils.AddNode(itemNode, "RETAIL_PRICE_VAT", Utils.GetDecimal(item, "RETAIL_PRICE_VAT"));
      Utils.AddNode(itemNode, "RETAIL_SUM", Utils.GetDecimal(item, "RETAIL_SUM"));
      Utils.AddNode(itemNode, "RETAIL_VAT_SUM", Utils.GetDecimal(item, "RETAIL_VAT_SUM"));
      Utils.AddNode(itemNode, "RETAIL_SUM_VAT", Utils.GetDecimal(item, "RETAIL_SUM_VAT"));

      Utils.AddNode(itemNode, "SERIES_NUMBER", Utils.GetString(item, "SERIES_NUMBER"));
      Utils.AddNode(itemNode, "BEST_BEFORE", Utils.GetString(item, "BEST_BEFORE"));
      Utils.AddNode(itemNode, "GTD_NUMBER", Utils.GetString(item, "GTD_NUMBER"));
      Utils.AddNode(itemNode, "BAR_CODE", Utils.GetString(item, "BAR_CODE"));
    }

    private void FillHeader(DataRow header, XmlNode headerNode)
    {
      Utils.AddNode(headerNode, "SUPPLIER_NAME", Utils.GetString(header, "SUPPLIER_NAME"));
      Utils.AddNode(headerNode, "SVAT_SUPPLIER", Utils.GetDecimal(header, "SVAT_SUPPLIER"));
      Utils.AddNode(headerNode, "SUM_SUPPLIER", Utils.GetDecimal(header, "SUM_SUPPLIER"));
      Utils.AddNode(headerNode, "SVAT_RETAIL", Utils.GetDecimal(header, "SVAT_RETAIL"));
      Utils.AddNode(headerNode, "SUM_RETAIL", Utils.GetDecimal(header, "SUM_RETAIL"));
      Utils.AddNode(headerNode, "INCOMING_NUMBER", Utils.GetString(header, "INCOMING_NUMBER")+doc_name_suffix);
      Utils.AddNode(headerNode, "INCOMING_DATE", Utils.GetDate(header, "INCOMING_DATE"));
      Utils.AddNode(headerNode, "INCOMING_BILL_NUMBER", Utils.GetString(header, "INCOMING_BILL_NUMBER"));
      Utils.AddNode(headerNode, "INCOMING_BILL_DATE", Utils.GetDate(header, "INCOMING_BILL_DATE"));
      Utils.AddNode(headerNode, "COMMENT", Utils.GetString(header, "COMMENT"));
    }

    public override string Save(string dirName)
    {
      if (!Directory.Exists(dirName))
      {
        Directory.CreateDirectory(dirName);        
      }
      foreach (KeyValuePair<string, XmlDocument> kvp in docList)
      {
        fileName = kvp.Key.Replace("/", "_")+".xml";
        kvp.Value.Save(Path.Combine(dirName, fileName));
      }
      return dirName;
    }
      public string Save(string dirName, Encoding saveEncoding)
      {
          if (!Directory.Exists(dirName))
          {
              Directory.CreateDirectory(dirName);
          }
          // Stream sw = new Stream(@"C:\temp\tt.xml");

          foreach (KeyValuePair<string, XmlDocument> kvp in docList)
          {
              fileName = kvp.Key.Replace("/", "_") + ".xml";
              //kvp.Value.Save(Path.Combine(dirName, fileName));
              using (FileStream fs = new FileStream(Path.Combine(dirName, fileName), FileMode.Create))
              {
                  using (StreamWriter tw = new StreamWriter(fs, saveEncoding))
                  {
                      kvp.Value.Save(tw);
                      tw.Close();
                  }
              }
          }
          return dirName;
      }

      private string fileName;
      public string FileName
      {
          get { return fileName;}
      }
  }
}
