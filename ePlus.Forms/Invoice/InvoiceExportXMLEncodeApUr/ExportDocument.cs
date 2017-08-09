using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace FCChInvoiceExportXMLEncodeApUr
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
        XmlNode root = Utils.AddNode(doc, "Документ");
        XmlAttribute attrID = doc.CreateAttribute("Идентификатор");
        attrID.Value = "";
        root.Attributes.Append(attrID);
        XmlNode header_doc = Utils.AddNode(root, "ЗаголовокДокумента");
        docList.Add(doc_name, doc);
        long fid = Utils.GetLong(row, "FID");
        DataRow[] header = ds.Tables[1].Select(string.Format("FID={0}", fid));
        if (header.Length != 1) continue;
        long sid = Utils.GetLong(header[0], "SID");
        FillHeader(header[0], header_doc);
        //XmlNode suplier = Utils.AddNode(header_doc, "РеквизитыПоставщика");
        XmlNode itemsNode = Utils.AddNode(root, "ТоварныеПозиции");
        DataRow[] items = ds.Tables[2].Select(string.Format("FID={0} AND SID={1}", fid, sid));
        foreach (DataRow item in items)
        {
          long tid = Utils.GetLong(item, "TID");
          XmlNode itemNode = Utils.AddNode(itemsNode, "ТоварнаяПозиция");
          FillItem(item, itemNode, doc);
          XmlNode seriesNode = Utils.AddNode(itemNode, "Серии");          
          DataRow[] series = ds.Tables[3].Select(string.Format("FID={0} AND SID={1} AND TID={2}", fid, sid, tid));
          foreach (DataRow seriya in series)
          {
              XmlNode seriyaNode = Utils.AddNode(seriesNode, "Серия");
              FillSeries(seriya, seriyaNode);
          }
          XmlNode farmGroup = Utils.AddNode(itemNode, "Фармгруппа");
          Utils.AddNode(farmGroup, "Идентификатор", Utils.GetString(item, "ID_GOODS_KIND"));
          Utils.AddNode(farmGroup, "КодГруппы", Utils.GetString(item, "GOODS_KIND_MNEMOCODE"));
          Utils.AddNode(farmGroup, "Наименование", Utils.GetString(item, "GOODS_KIND_NAME"));
          Utils.AddNode(farmGroup, "Примечание", Utils.GetString(item, "GOODS_KIND_COMMENT"));
        }
      }
    }

      private void FillSeries(DataRow seriya, XmlNode seriyaNode)
    {
        Utils.AddNode(seriyaNode, "СерияТовара", Utils.GetString(seriya, "SERIES_NUMBER"));
        Utils.AddNode(seriyaNode, "НомерСертиф", Utils.GetString(seriya, "CERT_NUMBER"));
        Utils.AddNode(seriyaNode, "ОрганСертиф", Utils.GetString(seriya, "ISSUED_BY"));
        Utils.AddNode(seriyaNode, "ДатаВыдачиСертиф", Utils.GetDate(seriya, "CERT_DATE"));
        Utils.AddNode(seriyaNode, "СрокДействияСертиф", Utils.GetDate(seriya, "CERT_END_DATE"));
        Utils.AddNode(seriyaNode, "СрокГодностиТовара", Utils.GetDate(seriya, "BEST_BEFORE"));
        Utils.AddNode(seriyaNode, "РегНомер","");
        Utils.AddNode(seriyaNode, "РегДатаСертиф", "");
        Utils.AddNode(seriyaNode, "РегОрганСертиф", "");
        Utils.AddNode(seriyaNode, "Валюта", "");
        Utils.AddNode(seriyaNode, "ЦенаВВалюте", "");
        Utils.AddNode(seriyaNode, "ПредДопРознЦена", "");
        Utils.AddNode(seriyaNode, "Код1", "");
        Utils.AddNode(seriyaNode, "Код2", "");
        Utils.AddNode(seriyaNode, "Код3", "");
    }

      private void FillItem(DataRow item, XmlNode itemNode, XmlDocument doc)
    {
      Utils.AddNode(itemNode, "КодТовара", Utils.GetString(item, "GOODS_CODE"));
      Utils.AddNode(itemNode, "Товар", Utils.GetString(item, "GOODS"));
      Utils.AddNode(itemNode, "Изготовитель", Utils.GetString(item, "PRODUCER"));
      Utils.AddNode(itemNode, "СтранаИзготовителя", Utils.GetString(item, "COUNTRY"));
      Utils.AddNode(itemNode, "Акция","");
      XmlNode kol = Utils.AddNode(itemNode, "Количество", Utils.GetDecimal(item, "QUANTITY"));
      XmlAttribute attrID = doc.CreateAttribute("Распак");
      attrID.Value = Utils.GetString(item, "DENOMINATOR");
      kol.Attributes.Append(attrID);

      Utils.AddNode(itemNode, "ЦенаГР", "");
      Utils.AddNode(itemNode, "ЦенаПрайс1", "");
      Utils.AddNode(itemNode, "ЖНВЛС", Utils.GetBool(item, "IMPORTANT"));
      Utils.AddNode(itemNode, "Артикул", "");
      Utils.AddNode(itemNode, "ПервичныйАртикул", "");
      Utils.AddNode(itemNode, "БазовыйАртикул", "");
      Utils.AddNode(itemNode, "ВнешнийАртикул", "");
      Utils.AddNode(itemNode, "ЦенаИзг", Utils.GetDecimal(item, "PRODUCER_PRICE"));
      Utils.AddNode(itemNode, "ЦенаГР", Utils.GetDecimal(item, "REGISTER_PRICE"));
      Utils.AddNode(itemNode, "НаценОпт", Utils.GetDecimal(item,"SUPPLIER_ADPRICE"));
      Utils.AddNode(itemNode, "ЦенаОпт", Utils.GetDecimal(item, "SUPPLIER_PRICE"));
      Utils.AddNode(itemNode, "СуммаОпт", Utils.GetDecimal(item, "SUPPLIER_SUM"));
      Utils.AddNode(itemNode, "СтавкаНДС", Utils.GetDecimal(item, "SUPPLIER_VAT"));
      Utils.AddNode(itemNode, "СуммаНДС", Utils.GetDecimal(item, "SUPPLIER_VAT_SUM"));
      Utils.AddNode(itemNode, "ЦенаОптВклНДС", Utils.GetDecimal(item, "SUPPLIER_PRICE_VAT"));
      Utils.AddNode(itemNode, "СуммаОптВклНДС", Utils.GetDecimal(item, "SUPPLIER_SUM_VAT"));
      Utils.AddNode(itemNode, "НаценРозн", Utils.GetDecimal(item, "RETAIL_ADPRICE"));
      Utils.AddNode(itemNode, "ЦенаРознБезНалогов", Utils.GetDecimal(item, "RETAIL_PRICE"));
      Utils.AddNode(itemNode, "СуммаРознБезНалогов", Utils.GetDecimal(item, "RETAIL_SUM"));
      Utils.AddNode(itemNode, "ЦенаРозн", Utils.GetDecimal(item, "RETAIL_PRICE_VAT"));
      Utils.AddNode(itemNode, "СуммаРозн", Utils.GetDecimal(item, "RETAIL_SUM_VAT"));
      Utils.AddNode(itemNode, "СуммаРеализ", "");
      Utils.AddNode(itemNode, "СуммаРеализСкид", "");
      Utils.AddNode(itemNode, "НаценПрайс1", "");
      Utils.AddNode(itemNode, "ЦенаПрайс1", "");
      Utils.AddNode(itemNode, "СуммаПрайс1", "");
      Utils.AddNode(itemNode, "НаценПрайс2", "");
      Utils.AddNode(itemNode, "ЦенаПрайс2", "");
      Utils.AddNode(itemNode, "СуммаПрайс2", "");
      Utils.AddNode(itemNode, "НаценПрайс3", "");
      Utils.AddNode(itemNode, "ЦенаПрайс3", "");
      Utils.AddNode(itemNode, "СуммаПрайс3", "");
      Utils.AddNode(itemNode, "ГТД", Utils.GetString(item, "GTD_NUMBER"));
      Utils.AddNode(itemNode, "Поставщик", Utils.GetString(item, "SUPPLIER_NAME"));
      Utils.AddNode(itemNode, "НомерДокПост", Utils.GetString(item, "INCOMING_NUM"));
      Utils.AddNode(itemNode, "ДатаДокПост", Utils.GetString(item, "INCOMING_DATE"));
      Utils.AddNode(itemNode, "НомерРеестра", "");
      Utils.AddNode(itemNode, "Сильнодействующий", "");
      Utils.AddNode(itemNode, "Рецептурный", "");
      Utils.AddNode(itemNode, "ДатаРеестра", "");
      Utils.AddNode(itemNode, "ЕАН13", Utils.GetString(item, "EAN13"));
      Utils.AddNode(itemNode, "ШтрихКод", Utils.GetString(item, "BAR_CODE"));
      Utils.AddNode(itemNode, "КодСвязи", "");
      /*

            Utils.AddNode(itemNode, "NUMERATOR", Utils.GetString(item, "NUMERATOR"));
            Utils.AddNode(itemNode, "UNIT_NAME", Utils.GetString(item, "UNIT_NAME"));

           
            Utils.AddNode(itemNode, "REGISTRATION_DATE", Utils.GetDate(item, "REGISTRATION_DATE"));


            Utils.AddNode(itemNode, "SUPPLIER_VAT_PER_UNIT", Utils.GetDecimal(item, "SUPPLIER_VAT_PER_UNIT"));
            Utils.AddNode(itemNode, "RETAIL_VAT", Utils.GetDecimal(item, "RETAIL_VAT"));

            Utils.AddNode(itemNode, "RETAIL_VAT_SUM", Utils.GetDecimal(item, "RETAIL_VAT_SUM"));


            Utils.AddNode(itemNode, "SERIES_NUMBER", Utils.GetString(item, "SERIES_NUMBER"));
            Utils.AddNode(itemNode, "BEST_BEFORE", Utils.GetString(item, "BEST_BEFORE"));

           */
  }

    private void FillHeader(DataRow header, XmlNode headerNode)
    {
      Utils.AddNode(headerNode, "ТипДок", "ПРХ");
      Utils.AddNode(headerNode, "НомерДок", Utils.GetString(header, "INCOMING_NUMBER") + doc_name_suffix);
      Utils.AddNode(headerNode, "ДатаДок", Utils.GetString(header, "INCOMING_DATE"));
      Utils.AddNode(headerNode, "Поставщик", Utils.GetString(header, "SUPPLIER_NAME"));
      Utils.AddNode(headerNode, "Получатель", Utils.GetString(header, "CONTRACTOR_TO_NAME"));
      Utils.AddNode(headerNode, "Грузополучатель", Utils.GetString(header, "CONTRACTOR_TO_NAME"));
      Utils.AddNode(headerNode, "УсловияОплаты", "");
      Utils.AddNode(headerNode, "ТоварнаяГруппа", "");
      Utils.AddNode(headerNode, "Позиций", Utils.GetDecimal(header, "COUNT_POSITION"));
      Utils.AddNode(headerNode, "СуммаОпт", Utils.GetDecimal(header, "SUM_SUPPLIER"));
      Utils.AddNode(headerNode, "СуммаОптВклНДС", Utils.GetDecimal(header, "SUM_SUPPLIER_WITH_PVAT"));
      Utils.AddNode(headerNode, "СуммаНДС", Utils.GetDecimal(header, "SVAT_SUPPLIER"));

      XmlNode suplier_recvis = Utils.AddNode(headerNode, "РеквизитыПоставщика");

      Utils.AddNode(suplier_recvis, "Адрес", Utils.GetString(header, "SUPPLIER_ADDRESS"));
      Utils.AddNode(suplier_recvis, "ИНН", Utils.GetString(header, "SUPPLIER_INN"));
      Utils.AddNode(suplier_recvis, "КПП", Utils.GetString(header, "SUPPLIER_KPP"));
      Utils.AddNode(suplier_recvis, "ОКОНХ", Utils.GetString(header, "SUPPLIER_OKONH"));
      Utils.AddNode(suplier_recvis, "ОКПО", Utils.GetString(header, "SUPPLIER_OKPO"));
      Utils.AddNode(suplier_recvis, "Телефоны", Utils.GetString(header, "SUPPLIER_PHONE"));
      Utils.AddNode(suplier_recvis, "РасчетныйСчет", Utils.GetString(header, "SUPPLIER_ACCOUNT"));
      Utils.AddNode(suplier_recvis, "Город", Utils.GetString(header, "SUPPLIER_CITY"));
      Utils.AddNode(suplier_recvis, "Банк", Utils.GetString(header, "SUPPLIER_BANK"));
      Utils.AddNode(suplier_recvis, "ОтделениеБанка", Utils.GetString(header, "SUPPLIER_BANK_OFFICE"));
      Utils.AddNode(suplier_recvis, "БИК", Utils.GetString(header, "SUPPLIER_BIK"));
      Utils.AddNode(suplier_recvis, "КорСчет", Utils.GetString(header, "SUPPLIER_CORR_ACCOUNT"));
      Utils.AddNode(suplier_recvis, "ЭлПочта", Utils.GetString(header, "SUPPLIER_EMAIL"));
       
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
